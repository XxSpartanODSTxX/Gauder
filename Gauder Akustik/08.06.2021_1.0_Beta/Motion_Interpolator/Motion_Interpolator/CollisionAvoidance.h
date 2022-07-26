
/*
Library for Motion Collision Avoidance
Author : Klaus - Dieter Rupp
Version 1.0 Beta vom 2.06.2021
*/

#define _USE_MATH_DEFINES

#ifndef collision_avoidanve_h
#define collision_avoidanve_h


#include <math.h>

class Pos {
public:
	float x;
	float y;
	Pos() { x = 0; y = 0; }

	Pos(float ax, float ay) :x( ax ), y( ay )
	{}

	void set(float ax, float ay)
	{
		x = ax;
		y = ay;
	}

	void set(Pos P)
	{
		x = P.x;
		y = P.y;
	}

	void add(float dx)
	{
		x += dx;
		y += dx;
	}
	void rot(Pos P, float phi) {
		float s = sin(phi);
		float c = cos(phi);
		x= c * P.x - s * P.y,
		y= s * P.x + c * P.y;
	};

	void rotadd(Pos P, float phi,Pos P1) {
		float s = sin(phi);
		float c = cos(phi);
		    x = c * P.x - s * P.y+P1.x,
			y = s * P.x + c * P.y+P1.y;
	};

	void  rotX(Pos X, Pos P, float phi) {
		float s = sin(phi);
		float c = cos(phi);
		x = (c * (P.x - X.x) - s * (P.y - X.y) + X.x);
		y = (s * (P.x - X.x) + c * (P.y - X.y) + X.y);
	};

	void  rotXX(Pos X1, Pos X2, Pos P, float phi,float psi) {
		float s = sin(phi);
		float c = cos(phi);
	//	float x1 = (c * (P.x - X1.x) - s * (P.y - X1.y) + X1.x);
	//	float y1 = (s * (P.x - X1.x) + c * (P.y - X1.y) + X1.y);
		s = sin(psi);
		c = cos(psi);
		x = (c * (P.x - X2.x) - s * (P.y - X2.y) + X2.x);
		y = (s * (P.x - X2.x) + c * (P.y - X2.y) + X2.y);
	};

};

class Collision_Circle {
 public:
	 Pos m;   // Mittelpunkt
	 Pos p;  // Winkel fesster
	 float offset=0; // Winkeloffset bei Drehung
	 float   r = 0; // Radius
	 bool    b = true; // Chech In or out

	

	Collision_Circle() { }

	// Initialize a Box with custom dimensions
	Collision_Circle(float mx, float my, float rd, float  pmin, float pmax, bool flag):m(mx,my), p(pmin,pmax)
	{
		r = rd;
		b = flag;
	}

	void set(Pos P,  float rd, float  pmin, float pmax, bool flag)
	{
		m.set(P.x, P.y);
		p.set(pmin, pmax);
		r = rd;
		b = flag;
	}


};


class Collision_avoidanve
{

	
	bool isInizialized = false;
	int _actualMode =0;


	Collision_Circle k1, k2, k3; // ariable Kreis MhT
	Collision_Circle kx1, kx2, kx3, kx4, kx5; // Feste Kreise Basbox

	Pos Po;             // Drehpunkt MHT
	Pos P0 = { 0, 0 };  // Null Basbox
	Pos P1, P2,  P3;    // Variable Ecken der MHT
	Pos Po0, Po1, Po2; // Feste Eckpunkte der Box
	Pos M0,  M1, M2, M3, M4a, M4b, M5;
	Pos MB1, MB2, MB3; // Feste Mittelpunkte der Kreise
	Pos PB0, PB1, PB2, PB3;  // Eckpunkte MHT im Grundzustand
	Pos PoB;

	int doCheckCollisiion(float theta1, float theta2);

	int   checkDecodeDat(int wi1, int wi2, int ik, int nn, int mm, int level);
	float checkPointCircle(Pos P, Collision_Circle cir, bool flag = false, int num = 0);
	float checkPoints(Pos P1, Pos P2, int num);

	Pos  rotP(float xo, float yo, float phi);
	Pos  rot(float* P, float phi);
	Pos rotX(float* X, float* P, float phi);

	float sind(float angle) {
		return sin(M_PI * angle / 180.0f);
	}
	float cosd(float angle) {
		return cos(M_PI * angle / 180.0f);
	}
	float asind(float value) {
		return 180.0f * asin(value) / M_PI;
	}

	float norm(float* P) {
		return sqrt(P[0] * P[0] + P[1] * P[1]);
	}

	 float norm(float x1, float x2) {
		return sqrt(x1 * x1 + x2 * x2);
	}

	 void rotateBox(float phi1, float phi2);
	 bool inArea(float theta1, float theta2);

	 int checkCollissionGeom();  // checks Collision using explicit Calculation

public:

	float theta1_min = 0;
	float theta1_max = 1.9;
	float theta2_min = -3.8;
	float theta2_max =  3.8;

	bool (Collision_avoidanve::* pcheck)(float theta1, float theta2);

	int setJointLimit(int driveID, int id, float value);


	int setCheckMode(int mode);
	int getCheckMode() { return _actualMode; };
	bool check(float theta1, float theta2); 


	void init(int mode);

	bool checkLimit(float theta1, float theta2);
	bool checkNone(float theta1, float theta2) { return true; }

	bool checkCollissionGeom(float theta1, float theta2) { // checks Collision using explicit Calculation
		rotateBox(theta1, theta2);
		return checkCollissionGeom();
	};

    bool  checkCollisionQuadTree(float theta1, float theta2);  // Checks Collision using QuadTree

	bool checkColl_RLE(float theta1, float theta2);// Checks Collision using 2 Lines

	static int dataSize();
	

	

	//static float min(float a, float b) {
	//	return (a < b ? a : b);

//	}
	//static float max(float a, float b) {
		//return (a > b ? a: b);
	//}


};



#endif
#pragma once
