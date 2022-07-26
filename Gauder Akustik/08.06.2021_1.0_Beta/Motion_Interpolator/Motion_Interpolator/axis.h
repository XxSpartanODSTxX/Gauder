/*
   Library for Motion
   Author: Klaus-Dieter Rupp
   Version 1.0 Beta vom 2.06.2021
*/
#ifndef axisLib_h
#define axisLib_h

#include <math.h>


class AxisLib
{
  public:
    float dT = 0.1f;

    float xs = 0.0f; // target Pos
    float vo = 0.0f; // demanded Speed
    float vo_last = 0.0f; // demanded Speed of last Motion
    float as = 1.0f; // demand Acceleration
    float x0 = 0.0f; // Starting Position of the motion

    float pos = 0; // interpolated Position
    float posx = 0.0; // Summed Velocyty (Just for Test) 
    float acc = 0; // interpolated Acceleration
    float vel = 0; // interpolated Velocity
    float lamda = 0; // RampIndex 0:1

    float fa = 0;  // Scaling Factor for smal Motions 
    float tba = 0;  // Time for Acceleration
    float tbd = 0;  // Time for Deceleration
    int ntc; // Anzahl der Zeitschritte f�r konstantfahrt

    void set_dT(float deltaTime) {
      dT = deltaTime;
    };

    float* (AxisLib::* profileInterpolate)(float lamda);   // Pointer for Interpolate function

    void setInterpolator(float* (AxisLib::* profile)(float lamda)) {
        profileInterpolate = profile;
    }

    void setInterpolator(int id);
     
    void initMotion3(float a, float d);
    void initMotion5(float a, float d);

    float* poly5(float lamda);
    float* poly3(float lamda);
    float* poly3Via(float lamda);
    float* sin2(float lamda);
    float* holdPos(float lamda);
    float* constSpeed(float lamda);


    void interpolate(float lamda) {
        (this->*profileInterpolate)(lamda);
    };


    bool checkforDeceleration();
    bool checkforMaxSpeed();

};

#endif
