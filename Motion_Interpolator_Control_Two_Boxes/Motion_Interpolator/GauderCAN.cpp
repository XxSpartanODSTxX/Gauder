#include "GauderCAN.h"

uint32_t  _state = 0;   // Variable fpr aktual state

void GauderCAN::setValuesToCAN(int bytes, char index2, char index1, char subIndex, int inputValue, uint32_t id)
{
  char dataLenght = 0;
  switch (bytes) {
    case 1:
      dataLenght = 0x2F;
      break;
    case 2:
      dataLenght = 0x2B;
      break;
    case 3:
      dataLenght = 0x27;
      break;
    case 4:
      dataLenght = 0x23;
      break;
    case 5:
      dataLenght = 0x40;
  }
  static CAN_message_t msg;
  //msg.ext = 0;
  msg.id = id; //0x601
  msg.len = 8;
  msg.buf[0] = dataLenght;
  msg.buf[1] = index2;
  msg.buf[2] = index1;
  msg.buf[3] = subIndex;
  msg.buf[4] = (inputValue >>  0) & 0xFF;
  msg.buf[5] = (inputValue >>  8) & 0xFF;
  msg.buf[6] = (inputValue >> 16) & 0xFF;
  msg.buf[7] = (inputValue >> 24) & 0xFF;
  can1.write(msg);
}

/*
  Input values:
  1=Possition Mode
  2=Velocity Mode
  3=Torque Mode
  4=Homing Mode
  5=Auto-Setup Mode
*/
void GauderCAN::setMotorMode(int mode, uint32_t id) {
  static CAN_message_t msg;
  msg.id = id; //0x601
  msg.len = 8;
  switch (mode) {
    case 1:
      msg.buf[0] = positionMode[0];
      msg.buf[1] = positionMode[1];
      msg.buf[2] = positionMode[2];
      msg.buf[3] = positionMode[3];
      msg.buf[4] = positionMode[4];
      msg.buf[5] = positionMode[5];
      msg.buf[6] = positionMode[6];
      msg.buf[7] = positionMode[7];
      break;
    case 2:
      msg.buf[0] = velocityMode[0];
      msg.buf[1] = velocityMode[1];
      msg.buf[2] = velocityMode[2];
      msg.buf[3] = velocityMode[3];
      msg.buf[4] = velocityMode[4];
      msg.buf[5] = velocityMode[5];
      msg.buf[6] = velocityMode[6];
      msg.buf[7] = velocityMode[7];
      break;
    case 3:
      msg.buf[0] = torqueMode[0];
      msg.buf[1] = torqueMode[1];
      msg.buf[2] = torqueMode[2];
      msg.buf[3] = torqueMode[3];
      msg.buf[4] = torqueMode[4];
      msg.buf[5] = torqueMode[5];
      msg.buf[6] = torqueMode[6];
      msg.buf[7] = torqueMode[7];
      break;
    case 4:
      msg.buf[0] = homingSetupMode[0];
      msg.buf[1] = homingSetupMode[1];
      msg.buf[2] = homingSetupMode[2];
      msg.buf[3] = homingSetupMode[3];
      msg.buf[4] = homingSetupMode[4];
      msg.buf[5] = homingSetupMode[5];
      msg.buf[6] = homingSetupMode[6];
      msg.buf[7] = homingSetupMode[7];
      break;
    case 5:
      msg.buf[0] = autoSetupMode[0];
      msg.buf[1] = autoSetupMode[1];
      msg.buf[2] = autoSetupMode[2];
      msg.buf[3] = autoSetupMode[3];
      msg.buf[4] = autoSetupMode[4];
      msg.buf[5] = autoSetupMode[5];
      msg.buf[6] = autoSetupMode[6];
      msg.buf[7] = autoSetupMode[7];
      break;
    case 6:
      msg.buf[0] = interpolationMode[0];
      msg.buf[1] = interpolationMode[1];
      msg.buf[2] = interpolationMode[2];
      msg.buf[3] = interpolationMode[3];
      msg.buf[4] = interpolationMode[4];
      msg.buf[5] = interpolationMode[5];
      msg.buf[6] = interpolationMode[6];
      msg.buf[7] = interpolationMode[7];
      break;
  }
  can1.write(msg);
}

void GauderCAN::standByMode(int input, uint32_t id) {
  static CAN_message_t msg;
  msg.id = id; //0x601 First Motor
  msg.len = 8;
  switch (input) {
    case 1:
      //Prepare to Begin setup
      msg.buf[0] = readySwitch[0];
      msg.buf[1] = readySwitch[1];
      msg.buf[2] = readySwitch[2];
      msg.buf[3] = readySwitch[3];
      msg.buf[4] = readySwitch[4];
      msg.buf[5] = readySwitch[5];
      msg.buf[6] = readySwitch[6];
      msg.buf[7] = readySwitch[7];
      break;
    case 2:
      //Switch On
      msg.buf[0] = switchOn[0];
      msg.buf[1] = switchOn[1];
      msg.buf[2] = switchOn[2];
      msg.buf[3] = switchOn[3];
      msg.buf[4] = switchOn[4];
      msg.buf[5] = switchOn[5];
      msg.buf[6] = switchOn[6];
      msg.buf[7] = switchOn[7];
      break;
    case 3:
      //Enable Operation
      msg.buf[0] = operationEnable[0];
      msg.buf[1] = operationEnable[1];
      msg.buf[2] = operationEnable[2];
      msg.buf[3] = operationEnable[3];
      msg.buf[4] = operationEnable[4];
      msg.buf[5] = operationEnable[5];
      msg.buf[6] = operationEnable[6];
      msg.buf[7] = operationEnable[7];
      break;
    case 4:
      //Start Command
      msg.buf[0] = startMotor[0];
      msg.buf[1] = startMotor[1];
      msg.buf[2] = startMotor[2];
      msg.buf[3] = startMotor[3];
      msg.buf[4] = startMotor[4];
      msg.buf[5] = startMotor[5];
      msg.buf[6] = startMotor[6];
      msg.buf[7] = startMotor[7];
      break;
    case 5:
      //Stop Command
      msg.buf[0] = stopAndClear[0];
      msg.buf[1] = stopAndClear[1];
      msg.buf[2] = stopAndClear[2];
      msg.buf[3] = stopAndClear[3];
      msg.buf[4] = stopAndClear[4];
      msg.buf[5] = stopAndClear[5];
      msg.buf[6] = stopAndClear[6];
      msg.buf[7] = stopAndClear[7];
      break;
  }
  can1.write(msg);
}

void GauderCAN::startPositionMode(int positionValue, uint32_t id) {
  setMotorMode(1, id);
  delay(2);
  setValuesToCAN(4, 0x7A, 0x60, 0x00, positionValue, id);
  delay(2);
  standByMode(1, id);
  delay(2);
  standByMode(2, id);
  delay(2);
  standByMode(3, id);
  delay(2);
  standByMode(4, id);
  //Funcion para leer lo que pasa introducir I2C

}

int GauderCAN::positionStatus(uint32_t id) {
  static CAN_message_t msg;
  msg.id = id;
  msg.len = 8;
  msg.buf[0] = 0x4B;
  msg.buf[1] = 0x64;
  msg.buf[2] = 0x60;
  msg.buf[3] = 0x00;
  msg.buf[4] = 0x00;
  msg.buf[5] = 0x00;
  msg.buf[6] = 0x00;
  msg.buf[7] = 0x00;
  can1.write(msg);
  delay(4);
  return motorPosition(id);
}

void GauderCAN::startVelocityMode(int speedValue, uint32_t id) {
  //standByMode(5, id);
  setMotorMode(2, id);
  delay(2);
  setValuesToCAN(2, 0x42, 0x60, 0x00, speedValue, id);
  delay(2);
  standByMode(1, id);
  delay(2);
  standByMode(2, id);
  delay(2);
  standByMode(3, id);
  delay(2);
  standByMode(4, id);
}

void GauderCAN::startHomingMode(int homingType, uint32_t id) {
  setMotorMode(4, id);
  delay(2);
  setHomingType(homingType, id);
  delay(2);
  standByMode(1, id);
  delay(2);
  standByMode(2, id);
  delay(2);
  standByMode(3, id);
  delay(2);
  standByMode(4, id);
  delay(2);
  standByMode(5, id);
}

void GauderCAN::setHomingType(int homingType, uint32_t id) {
  static CAN_message_t msg;
  msg.id = id; //0x601 First Motor
  msg.len = 8;
  switch (homingType) {
    case 1:
      //Index in current position
      msg.buf[0] = indexCurrentPosition[0];
      msg.buf[1] = indexCurrentPosition[1];
      msg.buf[2] = indexCurrentPosition[2];
      msg.buf[3] = indexCurrentPosition[3];
      msg.buf[4] = indexCurrentPosition[4];
      msg.buf[5] = indexCurrentPosition[5];
      msg.buf[6] = indexCurrentPosition[6];
      msg.buf[7] = indexCurrentPosition[7];
      break;
    case 2:
      //Encoder Index when it hit it go to negativ direction
      msg.buf[0] = encoderIndexAHittingN[0];
      msg.buf[1] = encoderIndexAHittingN[1];
      msg.buf[2] = encoderIndexAHittingN[2];
      msg.buf[3] = encoderIndexAHittingN[3];
      msg.buf[4] = encoderIndexAHittingN[4];
      msg.buf[5] = encoderIndexAHittingN[5];
      msg.buf[6] = encoderIndexAHittingN[6];
      msg.buf[7] = encoderIndexAHittingN[7];
      break;
    case 3:
      //Encoder Index when it hit it go to positiv direction}
      msg.buf[0] = encoderIndexAHittingP[0];
      msg.buf[1] = encoderIndexAHittingP[1];
      msg.buf[2] = encoderIndexAHittingP[2];
      msg.buf[3] = encoderIndexAHittingP[3];
      msg.buf[4] = encoderIndexAHittingP[4];
      msg.buf[5] = encoderIndexAHittingP[5];
      msg.buf[6] = encoderIndexAHittingP[6];
      msg.buf[7] = encoderIndexAHittingP[7];
      break;
    case 4:
      //Blocking Time
      msg.buf[0] = blockHoming[0];
      msg.buf[1] = blockHoming[1];
      msg.buf[2] = blockHoming[2];
      msg.buf[3] = blockHoming[3];
      msg.buf[4] = blockHoming[4];
      msg.buf[5] = blockHoming[5];
      msg.buf[6] = blockHoming[6];
      msg.buf[7] = blockHoming[7];
      break;
  }
  can1.write(msg);
}

void GauderCAN::startUpZeroPosition(uint32_t id) {
  setMotorMode(4, id);
  delay(2);
  setHomingType(1, id);
  delay(2);
  standByMode(1, id);
  delay(2);
  standByMode(2, id);
  delay(2);
  standByMode(3, id);
  delay(2);
  standByMode(4, id);
  delay(2);
  standByMode(5, id);
}
/*
   Close Loop
   initial Setup
   uint32_t id,uint32_t timeBetweenPos, uint32_t desaceleration, uint32_t maxDesaceleration, uint32_t speedLimit
*/
void GauderCAN::interpolatedPositionCLSetup(int interpolTime, uint32_t id) {
  //reset motor
  int positionMotor;
  positionMotor=positionStatus(id);
  Serial.print("PositionMotor:");
  Serial.println(positionMotor);
  standByMode(5, id);
  delay(500);
  //setPo2sitionMode
  setMotorMode(6, id);
  delay(10);
  //setOveridePosition
  setValuesToCAN(1, 0xC4, 0x60, 0x06, 1, id);
  delay(10);
  //setTimeBetweenPosition
  setValuesToCAN(1, 0xC2, 0x60, 0x01, interpolTime, id);  // 20ms
  delay(10);  // 2ms
  setValuesToCAN(4, 0xC1, 0x60, 0x01,positionMotor , id);
  delay(10);
  ////setMaximunSpeed
  //setValuesToCAN(4, 0x81, 0x60, 0x00, speedLimit, id);
  //delay(10);
  ////setDesiredDceleration
  //setValuesToCAN(4, 0x84, 0x60, 0x00, desaceleration, id);  // 200
  //delay(10);
  ////setMaximunDeceleration
  //setValuesToCAN(4, 0xC6, 0x60, 0x00, maxDesaceleration, id); 500
  standByMode(1, id);
  delay(10);
  standByMode(2, id);
  delay(10);
  standByMode(3, id);
  delay(1000);
  standByMode(4, id);
  Serial.println("Finish: Interpolation Mode Setup");
}

void GauderCAN::setInterPosition(uint32_t pos, uint32_t id) {
  //setPosition
  setValuesToCAN(4, 0xC1, 0x60, 0x01, pos, id);
}

int GauderCAN::motorPosition(int id) {
  int x = 0;
  int idn = id & 0x000f;
  static CAN_message_t msg;
  while ( can1.read(msg) ) {
    if (msg.id == 0x280 + idn) {
      x = msg.buf[0] | msg.buf[1] << 8 | msg.buf[2] << 16 | msg.buf[3] << 24;
      continue;
    }
    if (msg.id == 0x180 + idn || msg.id == 0x580 + idn) {
      x = msg.buf[4] | msg.buf[5] << 8 | msg.buf[6] << 16 | msg.buf[7] << 24;
      continue;
    }
  }
  return x;
}

/*
  Set to operatioal Mode
  mode 1 is operational Mode
  mode 0 is preoperational mode
  drive ID are 1 and 2
*/
int GauderCAN::setOperationalMode(int mode, uint32_t driveID) {
  // Operationsmodus auswahlen
  static CAN_message_t msg;
  msg.id = 0;
  msg.len = 2;
  msg.buf[0] = mode;
  msg.buf[1] = driveID;
  can1.write(msg);
}
/*
  Prototype of Function to get Drive State
*/

uint16_t GauderCAN::getDriveState(uint32_t id) {
  static CAN_message_t msg;
  setValuesToCAN(4, 0x41, 0x60, 0, 0, id);
  while (can1.read(msg)) {
    // COMM.printCanMsg();
    if (msg.id == 0x181 || msg.id == 0x581) { //PDO or SDO for drive one
      continue;
    }
    return  msg.buf[4] | msg.buf[5] << 8 ;
  }
}

/*
    prüft ob der Motor bereit ist
    <returns> True wenn Motor Bereit</returns>
*/
bool IsMotorReady() {
  return (_state & 0x77) == 0x037;  // Active
}
/*
  Set a value to the position that is a offSet it should be the value of the magnet sensor in rad
*/
void GauderCAN::setOffset(int offSetVal, uint32_t id) {
  //set OffSet for the position set value
  setValuesToCAN(5, 0x7C, 0x60, 0x00, offSetVal, id);
  delay(10);
}
