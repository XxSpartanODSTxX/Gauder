/*
   Library for Motion
   Author: Klaus-Dieter Rupp
*/
#ifndef motionLib_h
#define motionLib_h

#include <math.h>
//#include <Arduino.h>
#define RAD  M_PI/180.0
#define MAX_MOTION_STEPS 2048

enum EUpDown { next = 0, up = 1, down = 2 };  // Search order for Sequenz
//enum EDir { left = -1, right = 1 }; // Drive drection for Left and Right Box
enum EDrive { Bass= 0x601, MHT = 0x602 }; // CAN IDs for Drives

typedef struct _progSegment { // Structure for Progamsegment
    int progSeq[16];
    int segmentsInProgramm = 0;
    int actIndex = 0;
    float t[16][2]; // Tangent for each Point
}progSegment;


typedef struct _motionSegment { // Structure for Progamsegment
    float vset[2];
    float xset[2];
    int dominatAxis = 0;
}motionSegment;


class MotionLib
{
  bool _hasNextSegment = false; // Liegt neues Segment vor
  int _interpolationMode=0;
  
  public:
  float override = 1.0;
  int phase=0;
  int nWait=0; 
  
  float dT=0.1f;
  
  float lamda=0.0f; // Parameter for Indexin the interpolation

  float as= 1.0f;  // demand Acceleration
  float vo=0.0f;// demand Speed
  float vn = 0.0f; // Speed of next Target

  int act_targetIndex = 0;  // Index of aktual Target

  bool bTagetAvailable = false; // is next target available
  //int progSeq[16];
  //int segmentsInProgramm = 0;

  

  progSegment programm;

  
  int doInterpolation();
  
  void setInterpolator(int nInterpo); // Funtion to set aktual Interpolator

  int iNextTarget = -1;  // -1 wenn kein Target mehr da ist sonst index des n�chsten Zieles 
  int outIndex = 9;      // Index of als valid Target to go Out
  int outTarget = 16;      // Index of all valid Target to go Out  last saved Target

  float v1;

  int motionBufferLastIndex = 0;  // Last index for Motionbuffer with command MoveTo
  int motionBufferStartIndex = 0;  // Last index for Motionbuffer with command MoveTo
  int motionBufferActIndex = 0;  // Last index for Motionbuffer with command MoveTo

 AxisLib Ax1; 
 AxisLib Ax2; 

 float* getThetaState();

 float* (MotionLib::* pathInterpolate)();   // Pointer for Interpolat function
 float* (MotionLib::* profileInterpolate)();   // Pointer for Interpolat function

 int getLastTarget();
 int setOutTarget(int value){
  if(value > 4 && value <16)
     outIndex=value; 
     return outIndex;
     }
     
 float* getOutTarget();
 float  getOutTarget(int k);
 float  getTarget(int index, int k);

 int getInterpolationMode(){return _interpolationMode;}
/**
 * Predefinition of Ramp types
 */

 float getPathLen(progSegment* prg);
 //void toSer();
// void MotionLib::delay(long time);

 float* getNextStep(int ind);
 float* getNextStep();

 float* getNextStep_StopFunction(bool bStop);
 void reset();
 void reset(float  theta1, float theta2);

 void moveStart() { motionBufferLastIndex = 0; motionBufferStartIndex = 0; };
 void moveContinue() { motionBufferStartIndex = motionBufferLastIndex;  };

 int  setFinalTarget(int index, float  theta1, float theta2, float vel, float acc);
 int  setStartTarget(float  theta1, float theta2, float vel, float acc);
int setNextTarget (float  theta1,float theta2,  float vel, float acc);
void setNextTarget(int targetIndex);
void getNextTarget();
float* getnextBSplineStep();

int addProgPoint(int index);
progSegment* resetProgramm();

int checkNextTarget();
int findNearestNeigbour(float theta1, float theta2, int aOutIndex, EUpDown bUpDown);
int findNearestNeigbour(float* theta, int aOutIndex, EUpDown eUpDown) {
    return findNearestNeigbour(theta[0], theta[1], aOutIndex, eUpDown);
}


bool hasNextSegmet() {
    return _hasNextSegment;
}

    int set_vel(float vel); 

    float*  move(); // Incrementell Motiron usinf Phase and Lamda
     // B- Spline
    float* movebSpline(progSegment* prg);
    float* movebSpline();

    float* bSpline(float u, float(*p)[4], int* index);


    // Akima Spline
    float* moveaSpline();
    float* moveaSpline(progSegment* prg, int );
    float aSpline3(float* b, float lamda);
    float aSpline5(float* b, float lamda);
    float aSplined3(float* b, float lamda);
    float aSplined5(float* b, float lamda);

    float* movecSpline(); // Spline with nodes on have distance like flyby


    float  polyval5(float* p, float x) {
        return ((((p[0] * x + p[1]) * x + p[2]) * x + p[3]) * x + p[4]) * x + p[5];
    }

    float  polyval4(float* p, float x) {
        return (((p[0] * x + p[1]) * x + p[2]) * x + p[3])* x + p[4];
    }

    float  polyval3(float* p, float x) {
        return ((p[0] * x + p[1]) * x + p[2]) * x + p[3];
    }
    float  polyval2(float* p, float x) {
        return (p[0] * x + p[1]) * x + p[2];
    }



    void moveTo(float xs1, float xs2, float vel_soll, float acc_soll);  // Absolut Motion to Position
    void moveTo(int index);  // Absolut Motion to Position

    void moveVia(int index);  // Absolut Motion via Position
    void moveVia(float xs1, float xs2, float vel_soll, float acc_soll);  // Absolut Motion via Position

    float* movePath();  //  Motion via Position
    float* movePath(progSegment* prg);

    void movePath_online();

    void setdT(float deltaTime);
    progSegment* moveOut();
    progSegment* moveIn();
    progSegment* moveTarget(int num);

    int setData(float w1, float w2);
    int setData(float w1, float w2, float v1, float v2);
    // just for Test
    motionSegment* set_vel(float vel, float xs1, float xs2);
    float* SpeedProfile_poly3(float px1, float px2, float vx, float* acc, float dT);
    float* SpeedProfile_poly5(float px1, float px2, float vx, float* acc, float dT);
  
};

#endif
