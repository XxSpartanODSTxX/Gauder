/*
  Motioninterpolater for Gauder Acustic
  Version 1.0 Beta vom 2.06.2021
  Klaus-Dieter Rupp
  Last Change: 19-04-2021  Refactoring code
  Last Change: 28-04-2021  Getter Methods added
  Last Change: 07-05-2021  Test MoveOut
  Last Change: 18-05-2021  optimize Interpolation
*/

#include <math.h>
#include "axis.h"
#include "mainTargets.h"
#include "motion.h"

float theta[] = { 0, 0 };  // Angles from System

float thetaState[] = { 0, 0, 0, 0 };  // Angles and Speeds from System

float lastTheta[2] = { theta[0], theta[1] }; // Last Angles and Speeds from System to calculate Speed


motionSegment m_set = { 0,0,0,0,0 };   // set Velocyty to keep memory




AxisLib Ax0;  // Dummy Axis for zero Interpolation
int dominantAxis = 0;
AxisLib* p_dominantAxis = &Ax0;
float tba = 0;
float tbd = 0;

int nStopFunctionPara = 0;
float fStopFunctionPara = 1.0;

float motionBuffer[MAX_MOTION_STEPS][4];

/**
* sets the delta Time to 
*/
void MotionLib::setdT(float deltaTime) {
    dT = deltaTime;
    Ax1.dT = dT;
    Ax2.dT = dT;
};
 /*
 * resets the Motion
 * init the Angles to theta 1, theta 2
 * set Start target to actual Position
 */
void MotionLib::reset(float  theta1, float theta2) { 
    theta[0] = theta1;  
    theta[1] = theta2; 
    lastTheta[0] = theta[0];
    lastTheta[1] = theta[1];
    reset();
    setStartTarget(theta1, theta2, 0.1, 1);

};

void MotionLib::reset() {

    bTagetAvailable = false;
    act_targetIndex = 16; // set the value to the first user Target
    motionBufferStartIndex = 0;
    motionBufferLastIndex = 0;
    motionBufferActIndex = 0;
}


float* MotionLib::getThetaState() {
    return thetaState;
}
int MotionLib::addProgPoint(int index) {
    programm.progSeq[programm.segmentsInProgramm] = index;
    return programm.segmentsInProgramm++;
};

progSegment* MotionLib::resetProgramm() {
    programm.segmentsInProgramm=0;
    programm.actIndex = 0;
    return &programm;
};

/*
* Motion for Akima Spline Interpolation
*/
float* MotionLib::moveaSpline() {
    return moveaSpline(&programm,5);
};

float* MotionLib::moveaSpline(progSegment* prg , int i_ord) {
    float weight  = 0.6;
    float weight0 = 0.0;
    int id_1 = 0;
    int id0 = prg->progSeq[0];
    int id1 = prg->progSeq[1];
    prg->t[0][0] = (targets[id1][0] - targets[id0][0]) * weight0;
    prg->t[0][1] = (targets[id1][1] - targets[id0][1]) * weight0;
    for (int i = 1; i < prg->segmentsInProgramm; i++) {
        id_1 = prg->progSeq[i-1];
        id0 = prg->progSeq[i];
        id1 = prg->progSeq[i+1];
        prg->t[i][0] = (targets[id1][0] - targets[id_1][0]) * weight;
        prg->t[i][1] = (targets[id1][1] - targets[id_1][1]) * weight;
    }
    id0  = prg->progSeq[prg->segmentsInProgramm - 1];
    id_1 = prg->progSeq[prg->segmentsInProgramm - 2];
    prg->t[prg->segmentsInProgramm - 1][0] = (targets[id0][0] - targets[id_1][0])*weight0;
    prg->t[prg->segmentsInProgramm - 1][1] = (targets[id0][1] - targets[id_1][1])*weight0;

    switch (i_ord) {
    case 5:
        for (int i = 1; i < prg->segmentsInProgramm; i++) {
            id_1 = prg->progSeq[i - 1];
            id0 = prg->progSeq[i];
            float b1[] = { targets[id0][0], targets[id_1][0],   prg->t[i][0],   prg->t[i - 1][0], 0, 0 };
            float b2[] = { targets[id0][1], targets[id_1][1],   prg->t[i][1],   prg->t[i - 1][1], 0, 0 };
            for (float u = 0; u < 1; u += dT * targets[prg->progSeq[i]][2]) {
                theta[0] = aSpline5(b1, u);
                theta[1] = aSpline5(b2, u);
                setData(theta[0], theta[1], aSplined5(b1, u), aSplined5(b2, u));
            }
        }

        break;

    case 3:
        for (int i = 1; i < prg->segmentsInProgramm; i++) {
            id_1 = prg->progSeq[i - 1];
            id0 = prg->progSeq[i];
            float b1[] = { targets[id0][0], targets[id_1][0],   prg->t[i][0],   prg->t[i - 1][0] };
            float b2[] = { targets[id0][1], targets[id_1][1],   prg->t[i][1],   prg->t[i - 1][1] };

            for (float u = 0; u < 1; u += dT * targets[prg->progSeq[i]][2]) {
                theta[0] = aSpline3(b1, u);
                theta[1] = aSpline3(b2, u);
                setData(theta[0], theta[1], aSplined3(b1, u), aSplined3(b2, u));
            }
        }
        break;
    }
        return theta;
}

float MotionLib::aSpline3(float* b, float lamda) {
    float p[] = { -2 * b[0] + 2 * b[1] + b[2] + b[3],
                   3 * b[0] - 3 * b[1] - b[2] - 2 * b[3],
                                                     b[3],
                                   b[1] };
        return polyval3(p, lamda);
}

float MotionLib::aSpline5(float* b, float lamda) {
    float p[] = { (6 * b[0] - 6 * b[1] - 3 * b[2] - 3 * b[3] + 1 / 2 * b[4] - b[5]),
                (-15 * b[0] + 15 * b[1] + 7 * b[2] + 8 * b[3] - b[4] + 3 * b[5]),
                 (10 * b[0] - 10 * b[1] - 4 * b[2] - 6 * b[3] + 1 / 2 * b[4] - 3 * b[5]),
             b[5],
            b[3],
            b[1] };
    return polyval5(p, lamda);
}


float MotionLib::aSplined3(float* b, float lamda) {
    float p[] = { -2 * b[0] + 2 * b[1] + b[2] + b[3],
                   3 * b[0] - 3 * b[1] - b[2] - 2 * b[3],
                                                     b[3],
                                   b[1] };
    float pd[] = { p[0] * 3, p[1] * 2, p[2] };

    return polyval2(pd, lamda);
   
}

float MotionLib::aSplined5(float* b, float lamda) {
    float p[] = { (6 * b[0] - 6 * b[1] - 3 * b[2] - 3 * b[3] + 1 / 2 * b[4] - b[5]),
                (-15 * b[0] + 15 * b[1] + 7 * b[2] + 8 * b[3] - b[4] + 3 * b[5]),
                 (10 * b[0] - 10 * b[1] - 4 * b[2] - 6 * b[3] + 1 / 2 * b[4] - 3 * b[5]),
             b[5],
            b[3],
            b[1] };
    float pd[] = { p[0] * 5, p[1] * 4,p[2] * 3, p[3] * 2, p[4] };

    return polyval4(pd, lamda);

}



/* Moves a certain Sequenz as a Spline
* could be used for Move out
*/
float* MotionLib::movebSpline() {
    return  movebSpline(&programm);
};


int MotionLib::getLastTarget() {
    return findNearestNeigbour(theta, outIndex, EUpDown::next);
}

float* MotionLib::getOutTarget() {
    return targets[outTarget];
}
float MotionLib::getOutTarget(int k) {
    return targets[outTarget][k];
}

float MotionLib::getTarget(int index, int k) {
    return targets[index][k];
}

float MotionLib::getPathLen(progSegment* prg) {
    int n1,n2;
    float f1, f2, f = 0;
    int num = prg->segmentsInProgramm;
    for (int i = 1; i < num; i++) {
        n1 = prg->progSeq[i-1];
        n2 = prg->progSeq[i];
        f1=fabs(targets[n2][0] - targets[n1][0]);
        f2=fabs(targets[n2][1] - targets[n1][1]);
        f += (f1>f2)? f1 : f2;
    }
    return f;
}
/*
* Moves a Seqence as a Spline
*/
 
float* MotionLib::movebSpline(progSegment* prg ) {
    int ind = motionBufferLastIndex;
    int prt[32];
    
    float lastTheta[2] = {theta[0], theta[1] };
    // Add Start and Endsegment for Spline Interpolation
    int num = prg->segmentsInProgramm + 3;
    prt[0] = prg->progSeq[0];
    prt[1] = prt[0];
    for (int k = 0; k < prg->segmentsInProgramm; k++) // Doubel start and end Segment
      prt[k+2] = prg->progSeq[k];
    prt[num-1] = prt[num-2];
    prt[num  ] = prt[num-2];

    float* step =theta;
    float u = 1;
    for (int k = 0; k < num-2; k++) {
        float(*p)[4] = &targets[0];
        //float vel = p[prt[k]][2];
        float vel = 0.5;
        
        prg->actIndex = k;
      //  for ( u = 1.0-u+dT/4; u < 1; u += (dT*vel)) {
        for (int i = 0; i < 1.0/(dT*vel*override) ; i++) {
                u = i * dT * vel * override;
             step= bSpline(u, p, &(prt[k]));
             setData(step[0], step[1]);

        }
    }
   // motionBufferLastIndex = ind;
   // theta[0]=step[0];
   // theta[1]=step[1];
    return step;
}

float* MotionLib::bSpline(float u, float(*p)[4], int* index) {
    float u2 = u*u;
    float u3 = u2*u; 
  //  int i0 = index[0];
 //   int i1 = index[1];
 //   int i2 = index[2];
 //   int i3 = index[3];
    for (int i = 0; i < 2; i++) {
        theta[i] = ((-u3 + 3.0 * u2 - 3.0 * u + 1.0) * p[index[0]][i]
            + (3.0 * u3 - 6.0 * u2 + 4.0) * p[index[1]][i]
            + (-3.0 * u3 + 3.0 * u2 + 3.0 * u + 1.0) * p[index[2]][i] + u3 * p[index[3]][i]) / 6.0;
    }
       
    return theta;
}
float* MotionLib::getnextBSplineStep() {
    //  if (ind >= 0 && ind < motionBufferLastIndex && ind < MAX_MOTION_STEPS)

    //      return bSpline( u, float(*p)[4], int* index);
    //  else 
    return NULL;
};

float* MotionLib::getNextStep() {
    
    if (motionBufferActIndex >= 0 
        && motionBufferActIndex < motionBufferLastIndex 
        && motionBufferActIndex < MAX_MOTION_STEPS)
        return motionBuffer[motionBufferActIndex++];
    else return NULL;
}

//* Returns the next Step of Motion buffer
float* MotionLib::getNextStep(int ind) {
    if (ind >= 0 && ind < motionBufferLastIndex && ind < MAX_MOTION_STEPS)
        return motionBuffer[ind];
    else return NULL;
}

void MotionLib::setNextTarget(int targetIndex) {
    act_targetIndex = targetIndex;
    // setNextTarget(targets[act_targetIndex][0], targets[act_targetIndex][0], 30, 60);
}

/**
* Set final Target for an Index
* this Target should have indexes >16 and will be saved in EEPROM
*/
int  MotionLib::setFinalTarget(int index, float  theta1, float theta2, float vel, float acc) {
   act_targetIndex = index;
   return setNextTarget(theta1, theta2, vel,acc);
}



/**
*Sets the Sart target Position
* Index for Start Target is 0
*/
int  MotionLib::setStartTarget(float  theta1, float theta2, float vel, float acc) {
    targets[0][0] = theta1;
    targets[0][1] = theta2;
    if (vel > 0)
    targets[0][2] = vel;
    if (acc > 0)
    targets[0][3] = acc;
    return 0;
}

/**
 * Sets the next target Position
 */
int  MotionLib::setNextTarget(float  theta1, float theta2, float vel, float acc) {
    if (act_targetIndex >= MAX_TARGETS) 
         act_targetIndex = 16;
    targets[act_targetIndex][0] = theta1;
    targets[act_targetIndex][1] = theta2;
    if(vel>0)
    targets[act_targetIndex][2] = vel;
    if (acc > 0)
    targets[act_targetIndex][3] = acc;
    _hasNextSegment = true;
    return act_targetIndex++;
   
    //vo = vel;
}
void MotionLib::getNextTarget(){
    
  Ax1.xs = targets[iNextTarget][0];
  Ax2.xs = targets[iNextTarget][1];
  vn =     targets[iNextTarget][2]; // Speed of next Target
  float ac = targets[iNextTarget][2]; // Speed of next Target
  iNextTarget++;
  dominantAxis = set_vel(vn);
  tba = fabs(15 * vn) / (ac * 8);// Beschleunigungszeit
  tbd = fabs(15 * vn) / (ac * 8);// Verz�gerungzeit

  Ax1.initMotion5(tba, tbd);
  Ax2.initMotion5(tba, tbd);
  bTagetAvailable = true;
  phase=1;
}
/**
* Checks if a new target is avalable
*/
int MotionLib::checkNextTarget() {
    if (iNextTarget <= act_targetIndex){
        if (bTagetAvailable) {
            return iNextTarget;
        }
}
        bTagetAvailable = false;
        return -1;
};

progSegment*  MotionLib::moveOut() {
    //int ipos = setNextTarget(theta[0], theta[1], 0.1, 1);  // Set Startvalue as beginn of Splineinterpolation
    int ipos = setStartTarget(theta[0], theta[1], 0.1, 1);  // Set Startvalue as begin of interpolation

    programm.progSeq[0] = ipos;
    programm.segmentsInProgramm=1;
    programm.actIndex = 0;
    int istart= findNearestNeigbour(theta, outIndex, EUpDown::up);
    int i,j;
    for (i = 1, j = istart; j < outIndex; j++) {
        programm.progSeq[i++] = j;
        programm.segmentsInProgramm = i;
         // setNextTargetSeqenz()
    }
    programm.progSeq[i++] = outTarget;
    programm.segmentsInProgramm = i;

    iNextTarget = 0;
    phase = 1;
    return &programm;
};


/******************************************************************
* generates a Sequenz of Points to a specified Target
*/
progSegment* MotionLib::moveTarget(int num) {
    int nTarget = 0;
    if (num<0 || num >=MAX_TARGETS) num = 0;

    int ipos = setStartTarget(theta[0], theta[1], 0.1, 1);  // Set Startvalue as begin of interpolation
    int istart = findNearestNeigbour(theta, outIndex,  EUpDown::up);
    programm.progSeq[0] = ipos;

    if (num > istart && num <= outIndex) { // Move Out to Target
        nTarget = istart;
        int maxNum = (num > outIndex) ? outIndex : num;
        int i = 1;
        for ( ;  nTarget <= maxNum; nTarget++) {
            programm.progSeq[i++] = nTarget;
        }
        programm.progSeq[i++] = num;
      
        programm.segmentsInProgramm = i;
      
    } 
    else if (num < istart ) { // Move In to Target
        nTarget = istart;
        for (int i = 1; nTarget >= num; nTarget--) {
            programm.progSeq[i++] = nTarget;
        }

        programm.segmentsInProgramm = nTarget;
    }
    else {// direkt move
      
        programm.progSeq[1] = num;
        programm.segmentsInProgramm = 2;
      
    }


    programm.actIndex = 0;
   
   
    return &programm;
}

/******************************************************************
* generates a Sequenz of Points to Move in
*/
progSegment* MotionLib::moveIn() {
    
    int ipos= setStartTarget(theta[0], theta[1], 0.1,1);  // Set Startvalue as beginn of Splineinterpolation
    int istart = findNearestNeigbour(theta, outIndex, EUpDown::down);

    programm.progSeq[0] = ipos;
    programm.segmentsInProgramm = 1;;
    programm.actIndex = 0;

    for (int i = 1, j = istart; j > 0; j--) {
       
        programm.segmentsInProgramm++;
        programm.progSeq[i++] = j;
       
    }
    iNextTarget = 0;
    return &programm;
};


int MotionLib::doInterpolation() {
    if(this->pathInterpolate != NULL)
      (this->*pathInterpolate)();
    return 0;
}

/*
* Sets the Interpolator
* move
* moveSpline
*/
void MotionLib::setInterpolator(int nInterpo) {
    switch (nInterpo) {
    case 1:  // Move with Linear Interpolation
        pathInterpolate = &MotionLib::movePath;
        _interpolationMode=1;
        break;
    case 2: // Move with akkima Spline Interpolation
        pathInterpolate = &MotionLib::movecSpline;
        _interpolationMode=2;
        break;
    case 3: // Move using BSpline Interpolation
        pathInterpolate = &MotionLib::movebSpline;
       _interpolationMode=3;
        break;
    case 4: // Move using the Buffer
        pathInterpolate = &MotionLib::getNextStep;
        _interpolationMode=4;
        break;
    case 5: // Move using BSpline Interpolation
        pathInterpolate = &MotionLib::getnextBSplineStep;
        _interpolationMode=5;
        break;


    }

}

/***************************************************************************************
* Move over serveral Points with akima Spline Interpolation
*/
float* MotionLib::movecSpline() {
    tba = 1;
    lamda += dT/tba; // Hängt von der Bahnlänge ab
    float u = lamda;
    switch (phase) {
    case 0:
        if (lamda > 1.0) {
            return theta;
        }
        break;
    case 1: // Acceleration
        u = (-u + 2) * u * u * u;
        if (lamda >= 1.0f) {
            lamda = 0;
            phase = 2;
            u = 0;
            programm.actIndex++;
        }
        break;
    case 2: // const Speed
        if (lamda >= 1.0f) {
            lamda = 0;
            phase = 2;
            u = 0;
            programm.actIndex++;
        }
         if(programm.actIndex >= programm.segmentsInProgramm-1)
           phase = 3;
        break;
    case 3: //Deceleration
        u = 1 - (-(1 - u) + 2) * (1 - u) * (1 - u) * (1 - u);
        if (lamda >= (1.0f - dT / tba))
            phase = 0;
        break;
    }
    int segIndex = programm.progSeq[programm.actIndex];
    int segIndex1 = programm.progSeq[programm.actIndex+1];
    int segIndex_1 = programm.progSeq[programm.actIndex-1];
    if (programm.actIndex == 0) {
        float tar0[] = { (targets[segIndex ][0] + targets[segIndex1][0]) / 2, (targets[segIndex][1] + targets[segIndex1][1]) / 2 };

        theta[0] = (1 - u) * targets[segIndex][0] + u * tar0[0];
        theta[1] = (1 - u) * targets[segIndex][1] + u * tar0[1];
    }
    else if (programm.actIndex == programm.segmentsInProgramm-1) {
        float tar0[] = { (targets[segIndex_1][0] + targets[segIndex][0]) / 2, (targets[segIndex_1][1] + targets[segIndex][1]) / 2 };

        theta[0] = (1 - u) * tar0[0] + u* targets[segIndex][0];
        theta[1] = (1 - u) * tar0[1] + u* targets[segIndex][1];

    }
    else{
        float tar1[] = { (targets[segIndex_1][0] + targets[segIndex][0]) / 2, (targets[segIndex_1][1] + targets[segIndex][1]) / 2 };
        float tar2[] = { (targets[segIndex][0] + targets[segIndex1][0]) / 2, (targets[segIndex][1] + targets[segIndex1][1]) / 2 };
        theta[0] = (1 - u) * (1 - u) * tar1[0] + 2 * (1 - u) * u * targets[segIndex][0] + u * u * tar2[0];
        theta[1] = (1 - u) * (1 - u) * tar1[1] + 2 * (1 - u) * u * targets[segIndex][1] + u * u * tar2[1];
    }
    setData(theta[0], theta[1]);
 
    return theta;
}

float*  MotionLib::movePath() {
    return movePath(&programm);
}

float* MotionLib::movePath(progSegment* prg){
    int* prt = prg->progSeq;

    int segIndex = prg->progSeq[0];
    //moveTo(segIndex);
    prg->actIndex = 0;
    for (int k = 0; k < prg->segmentsInProgramm-1; k++) {
        segIndex = prg->progSeq[k];
        moveVia(segIndex);
        prg->actIndex++;
      
      
    }
    moveTo(prg->progSeq[prg->actIndex]);
    return theta;

}
void MotionLib::movePath_online() {
    float u = 0;
   // interpolate = &MotionLib::accelerate();
    
    switch (phase) {
    case 0:
        getNextTarget();
        break;
    case 1: // Acceleration
        Ax1.setInterpolator(&AxisLib::poly5);
        Ax2.setInterpolator(&AxisLib::poly5);

       
      
        if (u >= (1.0f - dT / tba))
            phase = 2;
        break;
    case 2: // const Speed
        Ax1.setInterpolator(&AxisLib::constSpeed);
        Ax2.setInterpolator(&AxisLib::constSpeed);
        if (u == 0)
            programm.actIndex++;
        if (programm.actIndex > programm.segmentsInProgramm)
            phase = 3;
        break;
    case 3: //Deceleration
        Ax1.setInterpolator(&AxisLib::poly5);
        Ax2.setInterpolator(&AxisLib::poly5);
        u = 1 + (u + 1) * (u - 1) * (u - 1) * (u - 1);
        if (u >= (1.0f - dT / tba))
            phase = 0;
        break;
    case 4: // Change Speed
        Ax1.setInterpolator(&AxisLib::poly3Via);
        Ax2.setInterpolator(&AxisLib::poly3Via);
        u = 1 + (u + 1) * (u - 1) * (u - 1) * (u - 1);
        if (u >= (1.0f - dT / tba))
            phase = 0;
        break;
    }

    (this->*pathInterpolate)();
};
/**
 * Motion to a certen Target
 */
float* MotionLib::move(){
  float f=2.0/Ax1.vo;
  switch(phase){
     case 0:
      Ax1.holdPos(0);
      Ax2.holdPos(0);
      lamda = 0;
    break;

    case 1: // acceleration
        if (dominantAxis > 0) {
            lamda += dT / tba;
            Ax1.poly5(lamda);
            Ax1.pos = Ax1.x0 + Ax1.pos;

            Ax2.poly5(lamda);
            Ax2.pos = Ax2.x0 + Ax2.pos;

            if (lamda >= (1.0f-dT/tba)) {
                if (Ax1.fa > 1.0f)
                    phase = 2;  // Motion with constand Speed
                else
                    phase = 3;   // Motion with Ramp-up Ramp-down
              // Ax2.pos = 0.5 * Ax2.vo * tba;
               Ax1.acc = 0;
               Ax2.acc = 0;
           }
        }
        else {
            phase = 0;
        }


      break;
       case 2: // const speed
       Ax1.constSpeed( 0.0f);        
       Ax2.constSpeed( 0.0f);  
       switch(dominantAxis){
       case 0:
           phase = 0;
           lamda = 1;
           break;
       case 1:
           if(Ax1.checkforDeceleration()){
              phase=3;
              lamda=1;
              if (hasNextSegmet()) {
                  phase = 4;
                  lamda = 0;
              }

          }
       case 2:
           if (Ax2.checkforDeceleration()) {
               phase = 3;
               lamda = 1;
               if (hasNextSegmet()) {
                   phase = 4;
                   lamda = 0;
               }

           }
       }
        
      break;
      case 3: // deceleration
          lamda -= dT / tbd;
          Ax1.poly5(lamda);  
          Ax1.acc = -Ax1.acc;
          Ax1.pos = Ax1.xs  - Ax1.pos;

          Ax2.poly5(lamda);        
          Ax2.acc = -Ax2.acc;
          Ax2.pos = Ax2.xs  - Ax2.pos;

         
         if(lamda <= (0.0+dT/tbd)) {
           Ax1.acc=0.0;
           Ax1.vel=0.0;
           Ax2.acc=0.0;
           Ax2.vel=0.0;
 
           phase=0;
         }
      break;
      case 4: // change speed by flyby with poly 3
         lamda += dT*as;
         Ax1.acc=vo*6*(-lamda+1)*lamda;             // acceleration
         Ax1.vel=vo +(vn- Ax1.vo)*(-2.0*lamda+3.0)*lamda*lamda;
         Ax1.pos += Ax1.vel*dT*as;
         if(lamda >= 1.0) {
           vo = vn;
           phase=2;
           Ax1.acc=0;
           Ax1.vel=vo;
           Ax1.pos=vo*0.5;
           Ax2.acc=0;
           Ax2.vel=vo;
           Ax2.pos=vo*0.5;

        } 
      break;
  }
  theta[0] = Ax1.posx;
  theta[1] = Ax2.posx;
  return theta;
}



/**
 * Calculate the Velocety for all axis 
 * Finds the dominant Axis and scales all the other axis
 * checks also it the Motion is in negativ direction and flips the Sign of Velociy
 * set the paramter vo for Axis 1 and 2
 * returns the number of the dominant Axis
 */
motionSegment*  MotionLib::set_vel(float vel, float xs1, float xs2) {

    // find leading axis 
    m_set.xset[0] = fabs(xs1 - theta[0]);
    m_set.xset[1] = fabs(xs1 - theta[1]);

    if ((m_set.xset[0] + m_set.xset[1]) < 1e-6) {   // no motion nessesarry
        p_dominantAxis = &Ax0;  // Set Function Pointer of Dominant Axis to Dummy Axis
        m_set.dominatAxis = -1;
    }
    else {

        if (m_set.xset[0] > m_set.xset[1]) {
            m_set.vset[0] = vel;
            m_set.vset[1] = vel * m_set.xset[1] / m_set.xset[0];
            p_dominantAxis = &Ax1;  // Set Function Pointer of Dominant Axis to  Axis 1
            m_set.dominatAxis = 0;

        }
        else {
            m_set.vset[0] = vel * m_set.xset[0] / m_set.xset[1];;
            m_set.vset[1] = vel;
            p_dominantAxis = &Ax2;  // Set Function Pointer of Dominant Axis to  Axis 2
            m_set.dominatAxis = 1;

        }

        if (xs1 < theta[0])
            m_set.vset[0] = -m_set.vset[0];

        if (xs2 < theta[1])
            m_set.vset[1] = -m_set.vset[1];
    }
    return &m_set;
}




int  MotionLib::set_vel(float vel){
float vset[2];
float dx[2];
float v1,v2;
int dominant = 0;
// find leading axis 

dx[0]= fabsf(Ax1.xs-Ax1.pos); // Weg f�r Achse 1
dx[1]= fabsf(Ax2.xs-Ax2.pos); // Weg f�r Achse 2
if ((dx[0] + dx[1]) < 1e-6) {
    Ax0.vo = 0;
    Ax1.vo = 0;
    Ax2.vo = 0;
    vo = 0;
    dominant = 0;
    p_dominantAxis = &Ax0;  // Set Function Pointer of Dominant Axis to Dummy Axis
}
else {

    if (dx[0] > dx[1]) {
        v1 = vel;
        v2 = vel * dx[1] / dx[0];
        dominant = 1;
        p_dominantAxis = &Ax1;  // Set Function Pointer of Dominant Axis to  Axis 1

    }
    else {
        v1 = vel * dx[0] / dx[1];;
        v2 = vel;
        dominant = 2;
        p_dominantAxis = &Ax2;  // Set Function Pointer of Dominant Axis to  Axis 2

    }

    if (Ax1.xs > Ax1.pos)
        Ax1.vo = v1;
    else
        Ax1.vo = -v1;

    if (Ax2.xs > Ax2.pos)
        Ax2.vo = v2;
    else
        Ax2.vo = -v2;
}
return dominant;

}

void MotionLib::moveVia(int index) {
    moveVia(targets[index][0], targets[index][1], targets[index][2], targets[index][3]);  // Absolut Motion to Position
}

void MotionLib::moveVia(float xs01, float xs02, float vel_soll, float acc_soll) {
    float vo1 = Ax1.vel;
    float vo2 = Ax2.vel;

    Ax1.xs = xs01;
    Ax2.xs = xs02;

    Ax1.pos = Ax1.posx;
    Ax2.pos = Ax2.posx;

    int dom = set_vel(vel_soll*override);
    Ax1.initMotion3(acc_soll, acc_soll);
    Ax2.initMotion3(acc_soll, acc_soll);

    float vn1 = Ax1.vo;
    float vn2 = Ax2.vo;
   
    if (motionBufferLastIndex > MAX_MOTION_STEPS - 100)  // Just to prevent overflow
        motionBufferLastIndex = 0;

    int ind = motionBufferLastIndex;
    if (dom == 0)
        return;

        for (float lamda = dT; lamda < 1; lamda += dT/p_dominantAxis->tba) { // Aceleration

            Ax1.acc = (vn1- vo1) * 6 * (-lamda + 1) * lamda;             // acceleration
            Ax1.vel = vo1 + (vn1 - vo1) * (-2.0 * lamda + 3.0) * lamda * lamda;
            Ax1.pos += Ax1.vel * dT * as;
            Ax1.posx += Ax1.vel * dT * as;

            Ax2.acc = (vn2-vo2) * 6 * (-lamda + 1) * lamda;             // acceleration
            Ax2.vel = vo2 + (vn2 - vo2) * (-2.0 * lamda + 3.0) * lamda * lamda;
            Ax2.pos += Ax2.vel * dT * as;
            Ax2.posx += Ax2.vel * dT * as;


            motionBuffer[ind  ][0] = Ax1.posx;
            motionBuffer[ind  ][1] = Ax2.posx;
            motionBuffer[ind  ][2] = Ax1.vel;
            motionBuffer[ind++][3] = Ax2.vel;

        }

    
        while (!p_dominantAxis->checkforDeceleration()) { // Const Speed
            
            if (ind > MAX_MOTION_STEPS - 50)
                break;

            Ax1.posx += Ax1.vo * dT;
            Ax2.posx += Ax2.vo * dT;
          
            motionBuffer[ind][0] = Ax1.posx;
            motionBuffer[ind][1] = Ax2.posx;
            motionBuffer[ind][2] = Ax1.vel;
            motionBuffer[ind++][3] = Ax2.vel;


        }
        motionBufferLastIndex = ind;
        theta[0] = Ax1.posx;
        theta[1] = Ax2.posx;

}



void MotionLib::moveTo(int index) {
    moveTo(targets[index][0], targets[index][1], targets[index][2], targets[index][3]);  // Absolut Motion to Position
}

/**
 * Move to a certain position
 */
 void MotionLib::moveTo(float xs01, float xs02, float vel_soll, float acc_soll ){
  float f=2.0/3.0;
  float v1 = 0.0f;
  float v2 = 0.0f;

  float vo1 = Ax1.vel;
  float vo2 = Ax2.vel;


  Ax1.xs = xs01;
  Ax2.xs = xs02;
    
  Ax1.pos = Ax1.posx;
  Ax2.pos = Ax2.posx;

  int dom = set_vel(vel_soll*override);
  Ax1.initMotion3(acc_soll, acc_soll);
  Ax2.initMotion3(acc_soll, acc_soll);
  

  phase=1; 
  if (motionBufferLastIndex > MAX_MOTION_STEPS - 200)  // Just to prevent overflow
      motionBufferLastIndex = 0;

 // int ind = motionBufferLastIndex;

  if ((fabs(vo1)+fabs(vo2)) <  0.01)  {// Starting Speed equals zero
      if (dom == 0)
          return; // keine Bewegung erforderlich
      if (p_dominantAxis->checkforMaxSpeed()) { // max Speed will not be reached
          if (p_dominantAxis->tba < 0.1) 
              p_dominantAxis->tba = 0.1;
          float vel=fabsf(p_dominantAxis->posx - p_dominantAxis->xs)/ p_dominantAxis->tba ;
          Ax1.vo = vel;
          Ax2.vo = vel;
      }
      for (float lamda = dT; lamda < 1; lamda += dT/p_dominantAxis->tba) { // Aceleration
          Ax1.poly3(lamda);
          Ax2.poly3(lamda);
          setData(Ax1.posx, Ax2.posx, Ax1.vel, Ax2.vel);
//          motionBuffer[ind][0] = Ax1.posx;
//          motionBuffer[ind][1] = Ax2.posx;
//          motionBuffer[ind][2] = Ax1.vel;
//          motionBuffer[ind++][3] = Ax2.vel;
     }
  }
  else {
      float vn1 = Ax1.vo;
      float vn2 = Ax2.vo;
      if (p_dominantAxis->checkforDeceleration()) {
          Ax1.vo = 2*Ax1.vel;
          Ax2.vo = 2*Ax2.vel;
      }
      else{
      
          for (float lamda = dT; lamda < 1; lamda += dT / p_dominantAxis->tba) { // Aceleration

              Ax1.acc = (vn1 - vo1) * 6 * (-lamda + 1) * lamda;             // acceleration
              Ax1.vel = vo1 + (vn1 - vo1) * (-2.0 * lamda + 3.0) * lamda * lamda;
              Ax1.pos += Ax1.vel * dT * as;
              Ax1.posx += Ax1.vel * dT * as;

              Ax2.acc = (vn2 - vo2) * 6 * (-lamda + 1) * lamda;             // acceleration
              Ax2.vel = vo2 + (vn2 - vo2) * (-2.0 * lamda + 3.0) * lamda * lamda;
              Ax2.pos += Ax2.vel * dT * as;
              Ax2.posx += Ax2.vel * dT * as;

              setData(Ax1.posx, Ax2.posx, Ax1.vel, Ax2.vel);

//              motionBuffer[ind][0] = Ax1.posx;
//              motionBuffer[ind][1] = Ax2.posx;
//              motionBuffer[ind][2] = Ax1.vel;
//              motionBuffer[ind++][3] = Ax2.vel;
          }
     


  
  
    Ax1.acc=0;
    Ax1.vel = Ax1.vo;
    Ax2.acc = 0;
    Ax2.vel = Ax2.vo;
    }
  }
    phase=2;


  while(!p_dominantAxis->checkforDeceleration()){ // Const Speed

      if (motionBufferLastIndex > MAX_MOTION_STEPS - 50)
          break;

    Ax1.posx += Ax1.vo * dT;
    Ax2.posx += Ax2.vo * dT;
    setData(Ax1.posx, Ax2.posx, Ax1.vel, Ax2.vel);

   // motionBuffer[ind  ][0] = Ax1.posx;
   // motionBuffer[ind  ][1] = Ax2.posx;
   // motionBuffer[ind  ][2] = Ax1.vel;
   // motionBuffer[ind++][3] = Ax2.vel;


  }
    phase=3;
  
  for( float lamda = 1; lamda>=0; lamda-=dT / p_dominantAxis->tbd){ // Deceleration
         Ax1.poly3(lamda);
         Ax2.poly3(lamda);
         setData(Ax1.posx, Ax2.posx, Ax1.vel, Ax2.vel);

      //   motionBuffer[ind  ][0] =   Ax1.posx;
      //   motionBuffer[ind  ][1] = Ax2.posx;
      //   motionBuffer[ind  ][2] = Ax1.vel;
      //   motionBuffer[ind++][3] = Ax2.vel;


 


  } 
 // motionBufferLastIndex = ind;
   phase=0;
   theta[0] = Ax1.posx;
   theta[1] = Ax2.posx;
 
   
}

/*
float  targets[][4] = { { 1000, 1000, 100, 50 },
                        { 1250, 1500, 100, 50 },
                        { 1270, 1800, 100, 50 },
                        { 1280, 1940, 100, 50 },
                        { 1400, 2100, 100, 50 },
                        { 2000, 2457, 100, 50 },
                        { 2100, 2200, 100, 50 },
                        { 2100, 1964, 100, 50 },
                        { 2100, 2600, 100, 50 },
                        { 2100, 2700, 100, 50 } };

*/

 /*
 * finds the next Point on Path in targets fro Target to Outindex
 */
int MotionLib::findNearestNeigbour(float theta1, float theta2, int aOutIndex, EUpDown eUpDown) {

    float lMin = 10000, rmin = 0;;
    int id = 0;
    float l,r;
    int num = sizeof(targets) / sizeof(*targets);
    if (num > aOutIndex) num = aOutIndex;
    for (int ni = 1; ni < num; ni++) {
        l = 10* (theta1 - targets[ni][0]) * (theta1 - targets[ni][0]) + (theta2 - targets[ni][1]) * (theta2 - targets[ni][1]);
        r = atan2(10*(theta1 - targets[ni][0]),( theta2 - targets[ni][1]));  // Direction to Target positiv or begativ
        if (l < lMin) {
            lMin = l;
            rmin = r;
            id = ni;
        }
    }
    return id;
};

/*
* stores the step Data in the Motionbuffer
* w1, w2 demandes Positions v1 v2 demandes Speeds
* returns the last Index in Buffer
*/
int MotionLib::setData(float w1, float w2) {
    return setData(w1,w2, (w1 - lastTheta[0]) / dT, (w2- lastTheta[1]) / dT);
}

int MotionLib::setData(float w1, float w2, float v1, float v2) {
    motionBuffer[motionBufferLastIndex][0] = w1;
    motionBuffer[motionBufferLastIndex][1] = w2;
    motionBuffer[motionBufferLastIndex][2] = v1;
    motionBuffer[motionBufferLastIndex][3] = v2;
    lastTheta[0] = w1;
    lastTheta[1] = w2;

    if(motionBufferLastIndex>=(MAX_MOTION_STEPS-1)){
      /*
       * Print Errors
       */
      //printError(3,"Buffer overrun");
      return motionBufferLastIndex;
    }
    

    return motionBufferLastIndex++;
}

float*  MotionLib::getNextStep_StopFunction(bool bStop) {
    float sfa = 1.0;
    float l = fStopFunctionPara;
    if (motionBufferLastIndex >= (MAX_MOTION_STEPS - 1))
        motionBufferLastIndex = MAX_MOTION_STEPS - 1;
    if (motionBufferActIndex >= 0
        && motionBufferActIndex < motionBufferLastIndex) {

        if (bStop) {
            sfa = (-2 * l + 3) * l * l;
            fStopFunctionPara -= 0.1;
            if (fStopFunctionPara < 0) {
                fStopFunctionPara = 1.0;
                return NULL;
            }
        }
        if (l == 1) {
            thetaState[0] = motionBuffer[motionBufferActIndex][0];
            thetaState[1] = motionBuffer[motionBufferActIndex][1];
            thetaState[2] = motionBuffer[motionBufferActIndex][2]*sfa;
            thetaState[3] = motionBuffer[motionBufferActIndex][3]*sfa;
            nStopFunctionPara = motionBufferActIndex;
        }
        else {
            thetaState[0] = motionBuffer[nStopFunctionPara][0] + sfa * motionBuffer[motionBufferActIndex][2] * dT;
            thetaState[1] = motionBuffer[nStopFunctionPara][1] + sfa * motionBuffer[motionBufferActIndex][3] * dT;
            thetaState[2] = motionBuffer[motionBufferActIndex][2] * sfa;
            thetaState[3] = motionBuffer[motionBufferActIndex][3] * sfa;

        }

        while ((motionBufferActIndex < motionBufferLastIndex ) &&
            motionBuffer[motionBufferActIndex][0] == motionBuffer[motionBufferActIndex + 1][0] &&
            motionBuffer[motionBufferActIndex][1] == motionBuffer[motionBufferActIndex + 1][1]) {  // Values are not changing
            motionBufferActIndex++;  // goto Next
        }
        motionBufferActIndex++;
        return thetaState;
    }
    else return NULL;
}


/*
* Ramp functons for Poly3
*/
float ya3(float tx) { return 6 * ((1 - tx) * tx); }; //ya = @(tx)6 * ((1 - tx).*tx);
float yv3(float tx) { return ((-2 * tx + 3)*tx * tx); };// yv = @(tx)((-2 * tx + 3).*tx. ^ 2);
float yx3(float tx) { return ((1 - tx / 2) * tx * tx * tx);};//yx = @(tx)((1 - tx / 2).*tx. ^ 3);

/*
* Test interpolation with Poly 3 Ramp
*/

float* MotionLib::SpeedProfile_poly3(float px1, float px2, float vx, float* acc_soll, float dT) {

    motionSegment* pm_set = set_vel(vx, px1, px2);


    float Tba = (3 * vx) / (acc_soll[0] * 2);// Beschleunigungszeit
    float Tbd = (3 * vx) / (acc_soll[1] * 2);// Verz�gerungszeit

    float px = pm_set->xset[pm_set->dominatAxis];

    float tMax = px / vx + (Tba + Tbd) / 2;// Gesamtzeit
 

    float dTc = dT / Tba; // Relatives Zeitinterval zur normierten Berechnung
      //  tbx_a = 0:dTc:1; // Zeitvektor zum Beschleunigen
    int t1i = 1 / dTc; // length(tbx_a); // Anzahl der Zeitschritte f�r Beschleunigung

     float    dTcd = dT / Tbd; // Relatives Zeitinterval zur normierten Berechnung
      //  tbx_d = 1:-dTcd : 0;// Zeitschritte f�r Verz�gerung
     int   t2i = 1 / dTcd;// length(tbx_d); // Anzahl der Zeitschritte f�r Verz�gerung

    int ntc = (tMax/dT - t1i - t2i); // Anzahl der Zeitschritte f�r konstantfahrt
    float vp, xp, ap;

    if (ntc > 0) {
        for (float tbx_a = 0; tbx_a <= 1; tbx_a += dTc) { //  tbx_a = 0:dTc:1; // Zeitvektor zum Beschleunigen
            vp = yv3(tbx_a);
            xp = yx3(tbx_a) * Tba;
            ap = vx * ya3(tbx_a) / Tba;
            setData(pm_set->vset[0] * xp, pm_set->vset[1] * xp, pm_set->vset[0]*vp, pm_set->vset[1] * vp);

        }
        for (float t = 0; t <= ntc; t += dT) {   // t = 0:dT:tMax;
            vp = vx;
            xp = ( yx3(1) * Tba + t);
            ap = vx *  0 ;
            setData(pm_set->vset[0] * xp, pm_set->vset[1] * xp, pm_set->vset[0], pm_set->vset[1]);
        }

        for (float tbx_d = 1; tbx_d >0; tbx_d -= dTcd) {  //  tbx_d = 1:-dTcd : 0;// Zeitschritte f�r Verz�gerung
            vp = yv3(tbx_d);
            xp = yx3(tbx_d) * Tbd;
            ap = vx *  -ya3(tbx_d) / Tbd;
            setData(px1- pm_set->vset[0] * xp, px2- pm_set->vset[1] * xp, pm_set->vset[0] * vp, pm_set->vset[1] * vp);


        }

     
      //  vp = vx * [yv(tbx_a), ones(1, ntc), yv(tbx_d)];
     //   xp = vx * [yx(tbx_a) * Tba, yx(1) * Tba + t(1:ntc), px / vx - yx(tbx_d) * Tbd];
      //  ap = vx * [ya(tbx_a) / Tba, 0 * t(1:ntc), -ya(tbx_d) / Tbd];
        // t = linspace(0, tMax, length(vp));
    }
    else {
        //fprintf('not on speed')
        float xb_a = vx * yx3(1) * Tba; // Weg nach Beschleunigung
        float xb_d = vx * yx3(1) * Tbd; // Weg zur Verzögerung
        float fa = px / (xb_a + xb_d);
     
        for (float tbx_a = 0; tbx_a <= 1; tbx_a += dTc) {
            vp = fa * yv3(tbx_a);
            xp = fa * yx3(tbx_a) * Tba;
            ap = fa * vx * ya3(tbx_a) / Tba;
            setData(pm_set->vset[0] * xp, pm_set->vset[1] * xp, pm_set->vset[0] * vp, pm_set->vset[1] * vp);

        }
        // tbx_d = tbx_d(2:end);
        for (float tbx_d = 1; tbx_d >0; tbx_d -= dTcd) {//  tbx_d = 1:-dTcd : 0;// Zeitschritte f�r Verz�gerung
            vp = fa *   yv3(tbx_d);
            xp = fa * vx * ( px / (vx * fa) - yx3(tbx_d) * Tbd);
            ap = fa * vx *  -ya3(tbx_d) / Tbd;
            setData(px1- pm_set->vset[0] * xp, px2- pm_set->vset[1] * xp, pm_set->vset[0] * vp, pm_set->vset[1] * vp);
        }

       // vp = fa * vx * [yv(tbx_a), yv(tbx_d)];
       // xp = fa * vx * [yx(tbx_a) * Tba, px / (vx * fa) - yx(tbx_d) * Tbd];
       // ap = fa * vx * [ya(tbx_a) / Tba, -ya(tbx_d) / Tbd];
       // t = 0:dT:(Tba + Tbd);
       // t = linspace(0, (Tba + Tbd), length(vp));
    }
    return theta;
}

float ya5(float tx){   
    return 30 * ((tx * tx - 2 * tx + 1)*tx * tx); 
}; // @(tx)30 * ((tx. ^ 2 - 2 * tx + 1).*tx. ^ 2); % 30 - 60    30     0     0
float yv5(float tx) {
    return ((6 * tx * tx - 15 * tx + 10) * tx * tx * tx);
}; //  yv = @(tx)((6 * tx. ^ 2 - 15 * tx + 10).*tx. ^ 3); % 6 - 15    10     0     0     0
float yx5(float tx) {
    return((tx * tx - 3 * tx + 2.5) * tx * tx * tx * tx);
}; // yx = @(tx) (  (tx.^2 -3*tx +2.5).*tx.^4);  % 1 - 3     2.5    0     0     0    0



float* MotionLib::SpeedProfile_poly5(float px1, float px2, float vx, float* acc, float dT) {

    //% px = 1.;% Soll position die erreicht werden soll
    //% vx = 1.2;% Soll Geschwindigkiet
    //% acc = [1., 1.];% Beschleunigung
    //% dT = 0.01;% Abtastintervall

    motionSegment* pm_set = set_vel(vx, px1, px2);
    

    float    Tba = (15 * vx) / (acc[0] * 8); // Beschleunigungszeit
    float    Tbd = (15 * vx) / (acc[1] * 8);// Verz�gerungszeit

    float px = pm_set->xset[pm_set->dominatAxis];
    float tMax = px / vx + (Tba + Tbd) / 2;// Gesamtzeit

    
   // t = 0:dT:tMax;

    float dTc = dT / Tba; //% Relatives Zeitinterval zur normierten Berechnung
      //  tbx_a = 0:dTc:1; //% Zeitvektor zum Beschleunigen
    int     t1i = 1 / dTc; // = length(tbx_a) //% Anzahl der Zeitschritte f�r Beschleunigung

    float    dTcd = dT / Tbd; //% Relatives Zeitinterval zur normierten Berechnung
      //  tbx_d = 1:-dTcd : 0;//% Zeitschritte f�r Verz�gerung
    int    t2i = 1 / dTcd; /// = length(tbx_d) //% Anzahl der Zeitschritte f�r Verz�gerung

    //    ya = @(tx)30 * ((tx. ^ 2 - 2 * tx + 1).*tx. ^ 2); % 30 - 60    30     0     0
     //   yv = @(tx)((6 * tx. ^ 2 - 15 * tx + 10).*tx. ^ 3); % 6 - 15    10     0     0     0
    //    yx = @(tx)((tx. ^ 2 - 3 * tx + 2.5).*tx. ^ 4); % 1 - 3     2.5    0     0     0    0

      int   ntc = (tMax/dT - t1i - t2i); //% Anzahl der Zeitschritte f�r konstantfahrt
      float vp, xp, ap;
            if (ntc > 0) {
                for (float tbx_a = 0; tbx_a <= 1; tbx_a += dTc) {
                    vp = vx * yv5(tbx_a);
                    xp = vx * yx5(tbx_a) * Tba;
                    ap = vx * ya5(tbx_a) / Tba;
                    setData(pm_set->vset[0] * xp, pm_set->vset[1] * xp, pm_set->vset[0] * vp, pm_set->vset[1] * vp);
                }
                for (float t = 0; t <= ntc; t += dT) {   // t = 0:dT:tMax;
                    vp = vx ;
                    xp = vx *  yx5(1) * Tba + t;
                    ap = vx *  0 ;
                    setData(pm_set->vset[0] * xp, pm_set->vset[1] * xp, pm_set->vset[0], pm_set->vset[1]);
                }
                for (float tbx_d = 1; tbx_d > 0; tbx_d -= dTcd) {
                    vp = vx * yv5(tbx_d);
                    xp = vx * px / vx - yx5(tbx_d) * Tbd;
                    ap = vx *  -ya5(tbx_d) / Tbd;
                    setData(px1 - pm_set->vset[0] * xp, px2 - pm_set->vset[1] * xp, pm_set->vset[0] * vp, pm_set->vset[1] * vp);
                }

             //   vp = vx * [yv(tbx_a), 1 * ones(1, ntc), yv(tbx_d)];
             //   xp = vx * [yx(tbx_a) * Tba, yx(1) * Tba + t(1:ntc), px / vx - yx(tbx_d) * Tbd];
             //   ap = vx * [ya(tbx_a) / Tba, 0 * t(1:ntc), -ya(tbx_d) / Tbd];
                //s1 = yx(1) * Tba
                //    s2 = yx(1) * Tbd
            }
            else {
              //  fprintf('not on speed\n')
                 float   s1 = vx * yx5(1) * Tba;
                 float   s2 = vx * yx5(1) * Tbd;
                float   fa = px / (s1 + s2);
                for (float tbx_a = 0; tbx_a <= 1; tbx_a += dTc) {
                    vp = fa * vx * yv5(tbx_a);
                    xp = fa * vx * yx5(tbx_a) * Tba;
                    ap = fa * vx * ya5(tbx_a) / Tba;
                    setData(pm_set->vset[0] * xp, pm_set->vset[1] * xp, pm_set->vset[0] * vp, pm_set->vset[1] * vp);
                }
                    for (float tbx_d = 1; tbx_d > 0; tbx_d -= dTcd) {
                        vp = fa * vx * yv5(tbx_d);
                        xp = fa * vx * px / (vx * fa) - yx5(tbx_d) * Tbd;
                        ap = fa * vx *  -ya5(tbx_d) / Tbd;
                        setData(px1 - pm_set->vset[0] * xp, px2 - pm_set->vset[1] * xp, pm_set->vset[0] * vp, pm_set->vset[1] * vp);
                    }

             //   vp = fa * vx * [yv(tbx_a), yv(tbx_d)];
             //   xp = fa * vx * [yx(tbx_a) * Tba,  px / (vx * fa) - yx(tbx_d) * Tbd];
            //    ap = fa * vx * [ya(tbx_a) / Tba, -ya(tbx_d) / Tbd];
            }
    return theta;
}

/*
/// <summary>
        /// Startet eine Bewegung
        /// der Status mus bei Erreichen der Zielposition auf 0x0f gesetzt werden
        /// dann kan mit Togel die Bewegugn gestartet werden
        /// </summary>
        /// <returns></returns>
 int StartTravelProfile() {
    control |= 0x1f;
    err = can.SetState((byte)driveId, control);
    Thread.Sleep(3);  //todo sollte nicht erforderlich sein!  ( könnte auch später zurüchgesetzt werden)
    control &= 0xef;  // 0x10 zurücksetzen
    control = 0x0f;
    err = can.SetState((byte)driveId, control);
   _state = GetState();
    isMove = true;
    return 0;
}
*/
 // Operationsmodus auswählen
        /* Voraussetzung
            Änderungen des Modus können im Status „Operation Enabled“ erfolgen.
            Es sollte dabei sichergestellt werden, dass sich der Motor beim Kommandieren eines Moduswechsels nicht bewegt.
            Beispiel
            Am Beispiel des PP-Modus (Profile Position bzw. Positionier-Modus) wird die Auswahl eines Modus aufgezeigt:
            COB-ID
            Datenbytes
            Beschreibung
            622            2F 60 60 00 01            Modus: Profile Position(PP)
            5A2            60 60 60 00 00 00 00 00   Antwort: OK */
/*
 TPCANStatus SetOperationalMode(uint driveID, byte mode)
 {
     return sendSDO(0x600 + driveID, 0x2f, 0x6060, 0, mode);


 }

 /// <summary>
        /// Ausführen des RunThreads.
        /// </summary>
 public void Execute()
 {
     StartMove(null); <== Seting al nessecary Parameters
     isExeRuinning = true;
     WaitMove(null); <== is Drive in Control is Target Reached

     isExeRuinning = false;
 }

 public TPCANStatus GetOperationalMode(uint driveID)
 {
     return sendSDO(0x600 + driveID, 0x40, 0x6060, 0, 0);
 }

 */
