#define _USE_MATH_DEFINES
#include <math.h>
#include "Kinematik.h"

float norm2(float* x, float* y) {
  return sqrt((y[0] + x[0]) * (y[0] + x[0]) + (y[1] + x[1]) * (y[1] + x[1]));
}

int  Kinematik::setTCP(int num) {
    switch (num) {
    case 0:
        L2 = 0;
        theta2o = 0;
        _setTCPnum = 0;
        break;
    case 1:
        L2 = sqrt(ox * ox + oy * oy);
        theta2o = atan2(oy, ox);
        _setTCPnum = 1;
        break;
    case 2:
        L2 = norm2(po,p1);
        theta2o = atan2(po[1] + p1[1], po[0] + p1[0]);
        _setTCPnum=2;
        break;
    case 3:
        L2 = norm2(po, p2);
        theta2o = atan2(po[1] + p2[1], po[0] + p2[0]);
        _setTCPnum=2;
        break;    
    case 4:
        L2 = norm2(po, p3);
        theta2o = atan2(po[1] + p3[1], po[0] + p3[0]);
        _setTCPnum=4;
        break;
    case 5:
        L2 = norm2(po, p4);
        theta2o = atan2(po[1] + p4[1], po[0] + p4[0]);
        _setTCPnum=5;
        break;


    }
return _setTCPnum;
}


Kinematik::Array22* Kinematik::invJacobian(float theta1, float theta2) {

    float c1 = cos(theta1);
    float s1 = sin(theta1);
    float c2 = cos(theta2);
    float s2 = sin(theta2);
    float lf = 1 / (L1 * s1);
    Ji[0][0] = -lf;
    Ji[0][1] = -(L2 * c1 * s2 + L2 * c2 * s1)*lf;
    Ji[1][0] =  lf;
    Ji[1][1] =  (L1 * s1 + L2 * c1 * s2 + L2 * c2 * s1)*lf;
    return &Ji;
};

Kinematik::Array33* Kinematik::vkin(float theta1, float theta2) {
   // float t12 = theta1 + theta1o + theta2 + theta2o + theta2o0;
    float t12 = theta1 + theta2 ;
    float c = cos(t12);
    float s = sin(t12);
    D12[0][0] = c;
    D12[0][1] = -s;
    D12[0][2] = L2 * c + L1 * cos(theta1);

    D12[1][0] = s;
    D12[1][1] = c;
    D12[1][2] = L2 * s + L1 * sin(theta1);

    D12[2][0] = 0;
    D12[2][1] = 0;
    D12[2][2] = 1;
    return &D12;
    //return { { c, -s, L2 * c + L1 * cos(theta1)},
    //    {s, c, L2* s + L1 * sin(theta1)},
    //    {0, 0, 1 }};
 }
/*
* dTheta = J-1 * dx 
*/
float* Kinematik::invkinstep(float dx, float dtheta) {
    dt[0] = Ji[0][0] * dx + Ji[0][1] * dtheta;
    dt[1] = Ji[1][0] * dx + Ji[1][1] * dtheta;
    return dt;
}

/*
* invers Kinematik
*/
float* Kinematik::invkin(float x, float phi) {
    float  xo[] = { x_old, y_old,  phi_old };
    float thetax[] = { theta[0] + theta1o, theta[1]+ theta2o + theta2o0 };
    Array33* D = vkin(thetax[0], thetax[1]);
    xo[0] = *D[0][2];
    xo[1] = *D[1][2];
    xo[2] = atan2(*D[1][0], *D[0][0]);

    for (int i = 0; i < 3; i++) {
        invJacobian(thetax[0], thetax[1]);
        invkinstep(x - xo[0], phi - xo[2]);
        if (dt[0] >  0.5) dt[0] =  0.5;
        if (dt[0] < -0.5) dt[0] = -0.5;
        if (dt[1] >  0.5) dt[1] =  0.5;
        if (dt[1] < -0.5) dt[1] = -0.5;
        thetax[0] += dt[0]*0.9;
        thetax[1] += dt[1]*0.9;
        D = vkin(thetax[0], thetax[1]);
        xo[0] = *D[0][2];
        xo[1] = *D[1][2];
        xo[2] = atan2(*D[1][0], *D[0][0]);
    }

    theta[0] = thetax[0] - theta1o;
    theta[1] = thetax[1] - (theta2o + theta2o0);
    return theta;
}


float* Kinematik::jointWorld(float theta1, float theta2) { 
   
    theta[0] = theta1; 
    theta[1] = theta2; 
    Array33* D = vkin(theta1+ theta1o, theta2+ theta2o+ theta2o0);
    xo[0] = (*D)[0][2];
    xo[1] = (*D)[1][2];
    xo[2] = atan2((*D)[1][0], (*D)[0][0]);
    return xo;
};


int mag_offset[2][MagFiledSize];

int Kinematik::fillMagOffset()
{
    for (int driveID = 0; driveID < 2; driveID++) {
        int  i=0, j = 0;
        float l = 0;
        while (mag_offset[driveID][j] == MagOffsetUnknown)
            j++;// find first not unknown
        for (i = 0; i < j - 1; i++)  // Fill first unknowns
            mag_offset[driveID][i] = mag_offset[driveID][j];

        while (j < MagFiledSize) {  // until the end of field
            while ((mag_offset[driveID][j] != MagOffsetUnknown) && (j < MagFiledSize))// find next not unknown
                j++;
            i = j - 1;
            while ((mag_offset[driveID][j] == MagOffsetUnknown) && (j < MagFiledSize)) 
                j++; // find next not unknown
            if (j == MagFiledSize) {
                for (int k = i; k < j; k++)
                    mag_offset[driveID][k] = mag_offset[driveID][i];  // interpolate all missing values
            }
            else {
                for (int k = i; k < j; k++) {
                    l = (k - i) / (j - i);
                    mag_offset[driveID][k] = (1-l)*mag_offset[driveID][j] + (l)*mag_offset[driveID][j];  // interpolate all missing values
                }
            }
        }
    }
    return 0;
}


int Kinematik::getMagOffset(int driveID,int i)
{
    if (i<0 || i >= MagFiledSize)
     return MagOffsetUnknown;
    else
    return mag_offset[driveID][i];
}


int Kinematik::getMagOffset(int driveID, float magValue)
{
    if (magValue < 0 || magValue* MagOffsetResolution >= MagFiledSize)
        return 0;
    else {
        int index = magValue * MagOffsetResolution;
        float f = (magValue*MagOffsetResolution - index) / MagOffsetResolution;

        return (1-f)*mag_offset[driveID][index] + (f)*mag_offset[driveID][index+1];
    }
}


bool bfirstOffset = true;


/*
* Drive Id equal 1 od 2
* drive Pos is value vom Morotr in a scaling 0:2000 adapted to Mag Encoder
* drive Pos is value - offset in a scaling 0:2000
*/
int  Kinematik::teachAbsolutEncoderOffset(int driveID, float drivePos, float evalue) {
   
    if (bfirstOffset) {
        for (int i = 0; i < MagFiledSize; i++)
            mag_offset[driveID][i] = MagOffsetUnknown;  // Set to a value unknown
        bfirstOffset = false;
    }
    int index = evalue * MagOffsetResolution;
    float f = (evalue - index / MagOffsetResolution) * MagOffsetResolution;
     if(mag_offset[driveID][index] != MagOffsetUnknown)
         mag_offset[driveID][index] = (drivePos - evalue);
     else
        mag_offset[driveID][index] = f*mag_offset[driveID][index]  + (1-f)*(drivePos - evalue);

     if (mag_offset[driveID][index+1] == MagOffsetUnknown)
         mag_offset[driveID][index + 1] = (drivePos - evalue);
     else
         mag_offset[driveID][index + 1] = (1 - f) * mag_offset[driveID][index + 1] + f * (drivePos - evalue);
     return 0;
}

/*
* Calculates MagEncoder value for Raw Data
*/
float Kinematik::getAbsolutEncoder(int driveID, int magoffset, int evalue) {
    if (magoffset > 1000) {
        if (evalue < (magoffset - 1000))
            evalue = evalue + 4096;
    }else{ if (evalue > (magoffset + 2000))
        evalue = evalue - 4096;
     }
    float magValue = (evalue - magoffset)* M_PI / 2048;
    float dm = getMagOffset(driveID, magValue);
    return magValue - dm;
}