	/*
  Motioninterpolater for Gauder Acustic
  Version 1.2 Beta vom 17.03.2022
  Roberto Martinez
  Klaus-Dieter Rupp
  Last Change: 01-04.2021  Do Commands, CanCommand, JsonCommand
  Last Change: 02-04.2021  Store Commands bug fix
  Last Change: 05-04-2021  JSON in EEPROM (streamUtils.h)
  Last Change: 10-04-2021  Refactoring code
  Last Change  24.04.2021  Add some Commands
  Last Change  28.04.2021  JSonCommand, Get, Set commands Optimiced by testresult
  Last Change  29.04.2021  EEPROM Get Data
  Last Change  03.04.2021  Code Refactoring
  Last change lib gauder pos and id position changed in case it dosent move is that the problem
  Last change  17.03.2022 Changed the scaling Factor of the magnet sensors
*/
/*  Todo
  -Refactoring all the code and just read a value one tine no more than one because could cause a error
  -filtrar los valores del sensor leer los primeros 2 si el tercero tiene el valor muy alto(debo definirlo) sacar el valor y volver a leer (+20/-20) or 15
  leer 2 bits checar en el sensor 
 */

#include <math.h>
#include "axis.h"
#include "motion.h"
#include "CollisionAvoidance.h"
#include "Kinematik.h"

//On Windows Computer  Teensy will be undefined
#define TEENSY
//#define MAG_ENCODER					 
#ifdef _WIN32
#undef TEENSY
#undef MAG_ENCODER				  
#endif

#ifdef TEENSY
//#define SDFAT
#ifdef SDFAT
#include "SdFat.h"
#include "sdios.h"
#endif
#include <TimeLib.h>
#include <ArduinoJson.h>
#include <StreamUtils.h>
#include "GauderCAN.h"
#include "Sensor1.h"
#include "Sensor2.h"
#else
#include "..\\GauderCAN_Sim.h"
#include "..\\Sensor1_Sim.h"
#include "..\src\Json.h"

long millis() {
  return (std::chrono::system_clock::now().time_since_epoch()).count()/1000;
};

#endif

#ifdef MAG_ENCODER
#include "Sensor1.h"
#include "Sensor2.h"
#else
//#include "..\\Sensor1_Sim.h"
//#include "Sensor_Sim.h"
#endif				  		  
					
float* posTheta;
float* posWorld;




#define relay 2
#define EPS 0.05  // Tollerance Angle to be in


float getDrivePosition(int id);
void getBuffMsg(CAN_message_t msg);

int doCommands(char c, char sub, int icmd);
void printError(int level, const char* errMsg);
void printCommand();
int moveTarget(char sub, int icmd);
int secondCommand(char sub, int icmd);

#define SHORT_BUFFER_LEN 32
#define BUFFER_LEN 128

typedef struct _command {
  char cmd;
  char sub;
  int  icmd;
  bool bvalid;
  bool isPush;     // shows tha the Comman has higest Prriority an interrups othe commands
  bool isOverride; // shows the the Command is Overrideable													   
  char value[SHORT_BUFFER_LEN];
  char sdata[BUFFER_LEN];
  int ndata;
  int idata[16];
  float fdata[16];
} Command;

bool isCommandValid(char* cmd, int len , Command* pcmd);
Command* scannCmd(char* cmd, int len, Command* pcmd);
int pharsJson(char* cmd, int len, Command* pcmd);

Command CommandBuffer[8];
int bufend = 0;  // index for add Command
int actbuf = 0;  // index to perform Commands

Command scmd;
Command* pcmd;

/*
  Resets the commandbuffer all commands ar invalid
  return Pointer to first Command
*/
Command* resetCommand() {
  for (int i = 0; i < 8; i++) {
    CommandBuffer[i].bvalid = false;
    CommandBuffer[i].isPush = false;
	CommandBuffer[i].isOverride = false;									
    CommandBuffer[i].ndata = 0;
  }
  bufend = 0;
  actbuf = 0;
  return  &CommandBuffer[0];
}

/*
  Gets Pointet to first Command
  Commmand could be valid or not
  return Pointer to first Command
*/
Command* firstCommand() {
  actbuf = 0;
  return  &CommandBuffer[0];
}
/*
  peeks the last set Command
*/
Command* peekCommand() {
    return  &CommandBuffer[bufend];
}

/*
* Removes the Last set Command in the Buffer
*/
void removeCommand(){
    bufend--;
    CommandBuffer[bufend].bvalid = false;
    CommandBuffer[bufend].isPush = false;
    CommandBuffer[bufend].isOverride = false;
    CommandBuffer[bufend].ndata = 0;
}
/* 
  Gets Pointet to next Command
  Commmand could be valid or not
  return Pointer to next  Command of NULL at the end of the Buffer
*/
Command* getNextCommand() {
  if (actbuf >= bufend)
    return NULL;
  else
    return  &CommandBuffer[actbuf++];
}

/*
* Gets the next Command in the Buffer
*/  								 
Command* nextCommand() {
  if (bufend >= 8) {
    printError(2, "Command Buffer is Full");
    return  &CommandBuffer[7];  // if buffer ist Full override last Command
  }
  else
    return  &CommandBuffer[bufend++];
}

/*
  Sets the Content of an Command
*/
Command*  setCommand(char cmd, char sub, int idata, bool bOverride) {
  pcmd = nextCommand();
  pcmd->cmd = cmd;
  pcmd->sub = sub;
  pcmd->icmd = idata;
  pcmd->bvalid = true;
  pcmd->isOverride = bOverride;						   
  return pcmd;
}

/* determin is next Command is Push command to Interrupt interpolation*/
bool isPushCommand() {
  return CommandBuffer[actbuf].isPush;
}
/*
  Adds a Command directly after the actuall command
  if foce is set the actual Command will be interrupted
*/
Command* pushCommand(char cmd, char sub, int idata, bool force) {
  pcmd = &CommandBuffer[actbuf];
  pcmd->cmd = cmd;
  pcmd->sub = sub;
  pcmd->icmd = idata;
  pcmd->bvalid = true;
  pcmd->isPush = force;					   
  return pcmd;
}

/*
  DoCommands with Pointer to Commandstructur
*/
int doCommands(Command* plcmd) {
  return doCommands(plcmd->cmd, plcmd->sub,  plcmd->icmd);
}

//Gear in the real box is 40
//#define gear1 40
//#define gear2 40
//Direccion for left and Right Box salida derecha positivo
//#define dir   -1

// Defines for State
#define DRIVEBit      0x0001 // Drives on off
#define STOPBit       0x0004 // Interpolation should Stop immediately
#define COLLISIONBit  0x0008 // Collision?
#define ACTIVBit      0x0010 // Job is Running
#define ERRORBit	  0x0020 // An Error Ocured
#define CANCELBit	  0x0040 // An Job was Canceled												 
#define REFERENCEBit  0x0100 // Drive is Refferenced
#define POSITION_OutBit   0x0200 // Is Out
#define POSITION_InBit 0x0400 //Is In
// Define SD Card
#ifdef TEENSY
#define SD_CONFIG SdioConfig(FIFO_SDIO) // SD Config
#else
#define SD_CONFIG 1
#endif
#define FILE_BASE_NAME "Status"

//Define Serial
#define LC Serial1
#define COMM Serial

bool firstError = true;

int arrayIndex = 0;
int arrayIndexS = 0;
int posicionMotor1;
int posicionMotor2;
int interpolation,collition,tcp;
int gear1, gear2, dir;
//value for the Interpolation and the time is going to wait until everything is finish in the void loop
int timeBetweenAction=30;

long nWait = 0;
unsigned long startTime = 0;
unsigned int actState = 0;

float lamda, vx, dT, vo, vn, xs, as;
float xx, xy, dx1, dy1, axis1, axis2, axisMagnet1, axisMagnet2;
float offsetVal[4] = {1314,3128,1.05,0.97}; //Real Box Open left (1314,3128,1,1)// open right { 3966,2136,1.05,1.05}
float magTollerance = 0.3;
int motorOffset[2];
bool bColl = true;
bool bPrintCan = false;
bool bStartUp = false;
bool isCommandAvailable = false;

char ccmd = 0;
char subcmd = 0;
int  icmd = 0;
char charsFromL[BUFFER_LEN];
char charsFromS[BUFFER_LEN];
char charsFromK[BUFFER_LEN];
char strValue[BUFFER_LEN];
char cmdvalue[SHORT_BUFFER_LEN];
char cmddata[BUFFER_LEN];
float jData[16];

int nt = 0, ntold = 0;
const char*  jscmd;
char json[] = "{\"cmd\":\"G\",\"time\":1351824120,\"sub\":\"s\", \"para\":[48.756080,2.302038]}";// Test Command
const uint8_t BASE_NAME_SIZE = sizeof(FILE_BASE_NAME) - 1; char fileName[] = FILE_BASE_NAME "00.dat";
void mainTestProgram();

GauderCAN canOpen;
MotionLib Motion;
Kinematik Robot;
Collision_avoidanve Collision;

CAN_message_t msg;
Sensor1 S1;
Sensor2 S2;
#ifdef SDFAT
SdFat SD;
File file;
#endif
#ifdef TEENSY
FlexCAN_T4<CAN1, RX_SIZE_256, TX_SIZE_256> can1;


//StaticJsonDocument<200> EDoc;
//StaticJsonDocument<200> CmdDoc;
StaticJsonDocument<250> jdoc;
StaticJsonDocument<200> REPdoc;
StaticJsonDocument<200> WEPdoc;
#else
std::ofstream  file;
GauderCAN can1;
JSONValue* pCmdDoc;
JSONValue* pEDoc;
JSONValue* pjdoc;
JSONValue* pEEPdoc;
#define JsonArray JSonValue
#endif
