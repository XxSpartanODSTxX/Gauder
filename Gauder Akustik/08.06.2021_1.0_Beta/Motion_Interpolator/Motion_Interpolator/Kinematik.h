
#ifndef kinematikLib_h
#define kinematikLib_h


#include <math.h>
#define  MagOffsetUnknown 22222
#define  MagOffsetResolution  4
#define  MagFiledSize 12

class Kinematik
{

    int _setTCPnum=0;

    float L1 = 240.97;
    float L2 = 140;

    float hm = ld / (2 * sqrt(3));//% Mittelpunktslage MHT

    float oy = 25.0;			// Lage x Drehpunkt MHT
    float ox = 148.14 - hm;		// Lage y Drehpunkt MHT

    float theta1o = asin(ox/L1); // Nulloffset Theta1    
    float theta2o = 0; // Nulloffset Theta2
    float theta2o0 = (180 - 16.52) * M_PI / 180;  // Nulloffset Eingefahrener Zustand



    float ld = 259.2;// Basisbreit MHT
    float ldr = ld / 2 * sqrt(3) - hm; //% halb Diagonale der Box
    float lhr = 20;
   

    float po[2] = { ox,oy }; // Offset von Box Mitte zu Drehpunkt
    float p1[2] = { -ldr, 0 };
    float p2[2] = { hm, -ld / 2.0f };
    float p3[2] = { hm,  ld  / 2.0f };
    float p4[2] = { hm+ lhr, 0 };


    float x_old;
    float y_old;
    float phi_old;

    float theta[2] = { 0,0 }; // Position in Rad
    int nx[2] = { 0,0 };      // Position in inkrementen

    float dt[2] = { 0,0 };
    float D12[3][3] = { {1,0, 0},{0, 1, 0 },{0, 0, 1 } };
    float Ji[2][2] = { {1,0},{0, 1} }; // Inverse Jacobimatrix

    int n0_mag = 1000; // offset Nullage Magnet Encoder
    float sc_mag = 2048 / M_PI; // Skalierung Magnet Encoder

    int n0_mot = 0; // offset Nullage Motor
    float sc_mot = 40 * 1000 / M_PI; // Skalierung Motor

    float xo[3] = { 0, 0,0 };// Last Position x,y,phi

    typedef float Array22[2][2];
    typedef float Array33[3][3];

    Array33* vkin(float theta1, float theta2);
    Array22* invJacobian(float theta1, float theta2);
    float* invkinstep(float dx, float dtheta);
    float* invkin(float x, float phi);

public:

    int setTCP(int num);

    int getTCP(){return _setTCPnum;};

    int getMagOffset(int driveID, int i);
    int getMagOffset(int driveID, float magValue);

    int  teachAbsolutEncoderOffset(int driveID, float drivePos, float evalue);
    float getAbsolutEncoder(int driveID, int magoffset, int evalue);
    int fillMagOffset();
   
    float* jointWorld(float theta1, float theta2);
    float* jointWorld(float* theta) { return jointWorld(theta[0], theta[1]); };
   

    float* worldJoint(float x, float phi) { return invkin(x, phi); }

    float* inc2winkel(int n1x, int n2x) {
        nx[0] = n1x;
        nx[1] = n2x;
        theta[0] = (n1x - n0_mot) * sc_mot;
        theta[1] = (n2x - n0_mot) * sc_mot;
        return theta;
    }

    int* winkel2inc(float theta1, float theta2){
      
        nx[0] = theta1 / sc_mot + n0_mot;
        nx[1] = theta2 / sc_mot + n0_mot;
        return nx;
 
}

    float* magInc2winkel(int n1x, int n2x) {
        nx[0] = n1x;
        nx[1] = n2x;
        theta[0] = (n1x - n0_mag) * sc_mag;
        theta[1] = (n2x - n0_mag) * sc_mag;
        return theta;
    }

    int* winkel2MagInc(float theta1, float theta2) {

        nx[0] = theta1 / sc_mag + n0_mag;
        nx[1] = theta2 / sc_mag + n0_mag;
        return nx;

    }

    

   
};
#endif
#pragma once
