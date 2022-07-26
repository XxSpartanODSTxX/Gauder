
/*
Library for Motion Collision Avoidance
Author : Klaus - Dieter Rupp
Version 1.0 Beta vom 2.06.2021
*/

#include <math.h>
#include "CollisionAvoidance.h"
#include "collisiondata.h"
#include "collisiondata_rle.h"
//#include "mainTargets.h"

bool (Collision_avoidanve::* checkColission)(float theta1, float theta2);   // Pointer for Interpolat function


int Collision_avoidanve::dataSize() {
	return sizeof(data);
};

int Collision_avoidanve::setCheckMode(int mode) {
	switch (mode) {
	case 0:
		checkColission = &Collision_avoidanve::checkNone;
		_actualMode = mode;
		break;
	case 1:
		checkColission = &Collision_avoidanve::checkLimit;
		_actualMode = mode;
		break;
	case 2:  
		checkColission = &Collision_avoidanve::checkColl_RLE;
		_actualMode = mode;
		break;
	case 3: 
		checkColission = &Collision_avoidanve::checkCollisionQuadTree;
		_actualMode = mode;
		break;
	case 4:
		checkColission = &Collision_avoidanve::checkCollissionGeom;
		_actualMode = mode;
		break;
	default:
		checkColission = &Collision_avoidanve::checkNone;
		_actualMode = 0;
		break;

	}
	return _actualMode;
}

int Collision_avoidanve::doCheckCollisiion(float theta1, float theta2) {
	(this->*checkColission)( theta1,  theta2);
	return 0;
}
/**
* Decodiert den Quadtree rekursiev
* wi1 und wi1 sind die Indizes der Winkel
* ik ist der Index f�r den Knoten
* nn und mm sind die startkoordinaten des Quadfeldes
* level ist die Gr��e des aktuellen quad-Feldes (breite, h�he)
*/
int Collision_avoidanve::checkDecodeDat(int wi1, int wi2, int ik, int nn, int mm, int level) {
	int n = 0; int m = 0; int breturn;
	if (wi1 > level + nn) {
		n = 1;
	}
	if (wi2 > level + mm) {
		m = 1;
	}

	int nm = n + m + m;  // Index des Feldes (0 : 3)
	if (data[ik][nm] > 0) {  // wenn Index
		breturn = checkDecodeDat(wi1, wi2, data[ik][nm], nn + n * level, mm + m * level, level / 2);
	}
	else { // wenn frei oder belegt
		breturn = -data[ik][nm];
	}
	return breturn;
};


/**
* Pr�ft die Collision mit einem Quadtree
*/
bool  Collision_avoidanve::checkCollisionQuadTree(float theta1, float theta2) {
	int wi1 = (int)((theta1 - theta1_min_quad) / theta1_step_quad);
	int wi2 = (int)((theta2 - theta2_min_quad) / theta2_step_quad);
	if (wi1 < 0 || wi1 >= MAX_SIZE)
		return  true;
	if (wi2 < 0 || wi2 >= MAX_SIZE)
		return  true;
	return (checkDecodeDat(wi1, wi2, sizeof(data) / (4 * sizeof(short)) - 1, 0, 0, MAX_SIZE / 2) == 77);
};
/**
* pr�ft Collision mit unterer und oberer Grenzlinie
* winkel theta 1 und theta2 in Rad
*/
bool Collision_avoidanve::checkColl_RLE(float theta1, float theta2) {
	bool bReturn = false;
	float delta1;
	float delta2;
	int index = (int)((theta2 - theta2_min_rle) / DELTA_RLE);
	if (index < 0 || index >= MAXLEN_RLE) {
		bReturn = true;
	}
	else {
		delta1 = (theta1 - theta1_min_rle) / DELTA_RLE - data_rle[index][0];
		delta2 = (theta1 - theta1_max_rle) / DELTA_RLE - data_rle[index][1];

		if (delta1 > 0 && delta2 < 0) {
			bReturn = false;
		}
		else {
			bReturn = true;
		}
	}

	return bReturn;
};

/*
* Collision Check with Pointer Function 
* first set with init  mode
*/
bool Collision_avoidanve::check(float theta1, float theta2) {
	if (this->pcheck != NULL)
		return (this->*pcheck)(theta1, theta2);
	else return false;
};



void Collision_avoidanve::init(int mode) {
	setCheckMode(mode);
	

	float RAD = M_PI / 180.0;
	float rd = 650;// Aussenradius
	float l_spalt = 3; // Spaltma�
	float r_eck = 10; // Eckradius
	float rdB = rd + l_spalt;// Radius Innenb�gen
	float rd1 = rd - r_eck;// Radius Eckpunkte
	float ld = 259.2;// Basisbreit MHT
	float xPb = 235.67;//% XKoordinate der Pilotbox in Nullstellung
	float yPb = 50.25; //% Y Koordinate der Pilotbox in Nullstellung

	float kx = 17.21;// Krorrekturl�nge
	float lhx0 = 81.8;//% L�nge Bogen bis gesetzter Punkt
	float hd = ld * sqrt(3) / 2;	// h�he MHT Dreieck
	float hm = ld / (2 * sqrt(3));	// Mittelpunktslage MHT
	float pd = asin(ld / (2 * rd1));// Bogenwinkel MHT
	float rd2 = sqrt(rd1 * rd1 - (ld / 2) * (ld / 2));// H�he �ber Sekante bis Mittelpunkt
	float hr = rd2 - hm;		// H�he Mittelpunkt Aussenradius
	float pr[] = { -pd, pd };	//  -pd:pd;  Bogen f�r MHT
	float ox = 25.0;			// Lage x Drehpunkt MHT
	float oy = hm- 148.14;		// Lage y Drehpunkt MHT
	float lmb = 163.18 + kx * sin(-16.52 * RAD);// Abstand x MHT, Bass
	float lmh = -30.79 - kx * cos(-16.52 * RAD);// Abstand x MHT, Bass
	PoB.set(lmb, lmh);
	float alfa0 = -asin(lmb / (hr));// Drehung MHD in Nullstellung
	float alfa1 = alfa0 + 2 * M_PI / 3; // 120�;
	alfa1 = alfa0;

	float thetaMin[] = { -5 * RAD, -140 * RAD };
	float thetaMax[] = {100 * RAD,   30 * RAD };

	Po.set(ox, oy);
	P0.set(PoB);
	P1.set(-hd + hm, 0);
	P2.set(hm, -ld / 2);
	P3.set(hm,  ld / 2);
	M1.set(-hr, 0);
	M2.set(hr/2, -hr * sqrt(3)/2);
	M3.set(hr/2,  hr * sqrt(3) / 2);
	Po.rotadd(Po, alfa1,PoB);
	P1.rotadd(P1, alfa1, PoB);
	P2.rotadd(P2, alfa1, PoB);
	P3.rotadd(P3, alfa1, PoB);
	M1.rotadd(M1, alfa1, PoB);
	M2.rotadd(M2, alfa1, PoB);
	M3.rotadd(M3, alfa1, PoB);

	pr[0] = pr[0] + alfa1;
	pr[1] = pr[1] + alfa1;
	//mBox.pr = pr;
	float bb = 400; // % breite der BassBox
	float ym = bb / 2 - 57.34 - r_eck; //% Mittelline der Box

	M4a.set(-25.72, rd - 57.34 - r_eck);

	M4b.set(lmb,  -300);// Mittelpunkt f�r Ausenkreis
	float lhr = hr * (1.0 - cos(alfa0));
	//float ym = (hr + M4b.y) / 2 - lhr / 2;// Mittelline der Box
	M5.set( -360, ym);// Mittelpunkt hinterer Boxabschluss
	M0.set(0, ym);// Box Center
	
	// Definition der Kreisb�gen
	 k1.set(M1, rd, pr[0], pr[1], true);
	//mBox.k1.m = mBox.M1;
	//mBox.k1.r = rd;
	//mBox.k1.p= pr-0*RAD;
	//mBox.k1.b = true;

	 k2.set(M2, rd, pr[0]+120*RAD, pr[1] + 120 * RAD, true);
	//mBox.k2.m = mBox.M2;
	//mBox.k2.r = rd;
	//mBox.k2.p= pr+120*RAD;
	//mBox.k1.b = true;

	k3.set(M3, rd, pr[0] - 120 * RAD, pr[1] - 120 * RAD, true);
	//mBox.k3.m = mBox.M3;
	//mBox.k3.r = rd;
	//mBox.k3.p= pr-120*RAD;
	//mBox.k1.b = true;

	kx1.set(M1, rdB, pr[0] , pr[1], false);
	//mBox.kx1.m = mBox.M1;
	//mBox.kx1.r = rdB;
	//mBox.kx1.p= pr-0*RAD;
	//mBox.kx1.b = false;

	kx2.set(M2, rdB, pr[0] + 120 * RAD, pr[1] + 120 * RAD, false);
	//mBox.kx2.m = mBox.M2;
	//mBox.kx2.r = rdB;
	//mBox.kx2.p = pr + 120 * RAD;
	//mBox.kx2.b = false;

	kx3.set(M3, rdB, pr[0] - 120 * RAD, pr[1] - 120 * RAD, false);
	//mBox.kx3.m = mBox.M3;
	//mBox.kx3.r = rdB;
	//mBox.kx3.p= pr-120*RAD;
	//mBox.kx3.b = false;

	kx4.set(M4a, rd, -72 * RAD, -90 * RAD, true);
	//mBox.kx4.m = mBox.M4a;
	//mBox.kx4.r = rd;
	//mBox.kx4.p = -(72:1 : 90) * RAD;% Bogen Ausenkreis
	//mBox.kx4.b = true;

	kx5.set(M4a, rd, -117 * RAD, -130 * RAD ,  true);
	//mBox.kx5.m = mBox.M4a;
	//mBox.kx5.r = rd;
	//mBox.kx5.p = -(117:1 : 130) * RAD;% Bogen Ausenkreis
	//mBox.kx5.b = true;

	


	PB1.set(P1);
	PB2.set(P3);
	PB3.set(P3);
	// Bogen Mittelpunkte f�r feststehende Bassbox
	MB1.set(M1);
	MB2.set(M1);
	MB3.set(M3);


	PoB.set(lmb, lmh);

	Po1.set(25.73, 57.34);
	Po0.rotX(M4a, Po1, 200/rd);
	Po2.rotX(M4a, Po1, -(2 * pd + (4 * r_eck + 2 * l_spalt) / rd));
	M5.set(-360, ym);// Mittelpunkt hinterer Boxabschluss
	M0.set(lmb, ym);// Box Center
	//plot = 3;
	isInizialized = true;
	


	/*
	//// Durchf�hren der Simulation
	// Anlegen der Grafik und der Videaufzeichnung

	vw = VideoWriter('Animation_Kolission_1.avi');
	open(vw);

	fig4 = figure(4)
	axis equal
	grid on
	hold on

	// Definition des Definitionsbereiches der Schwenkwinkel

	step = 2.5;
	theta1 = -5:step:115;
	theta2 = -135:step:55;
	// theta1 = 12:step:32;
	// theta2 = -35:-step : -50;

	nx = length(theta1);
	ny = length(theta2);
	flt = ones(nx, ny);

	// Simulation in 2 Schleifen
	for w1 = 1:nx
	for w2 = 1 : ny

	clf;
	axis equal
	grid on
	hold on
	xlim([-700 600])
	ylim([-500 550])
	plotBassBox(mBox);


	rBox = rotateBox(mBox, theta1(w1), theta2(w2));
	Plot_Box(rBox);
	kol = checkCollision(rBox);
	drawnow

	frame = getframe(fig4);
	writeVideo(vw, frame);
	flt(w1, w2) = kol;
	end
	end

	close(vw);

	figure(5);
	[X, Y] = meshgrid(theta2, theta1);
	surf(X, Y, flt);


	l = norm(mBox.PoB - mBox.Po)
	*/
	// axis equal
	// grid on
	// plotBassBox(mBox);
	// hold off
	// mBox


//************************************
};

void Collision_avoidanve::rotateBox(float phi1, float phi2) {
	
	Po.rotXX(PoB, Po, Po, phi1, phi2);
	P0.rotXX(PoB, Po, PB0, phi1, phi2);
	P1.rotXX(PoB, Po, PB1, phi1, phi2);
	P3.rotXX(PoB, Po, PB3, phi1, phi2);
	M1.rotXX(PoB, Po, MB1, phi1, phi2);
	M2.rotXX(PoB, Po, MB2, phi1, phi2);
	M3.rotXX(PoB, Po, MB3, phi1, phi2);

	
	k1.m.set(M1);
	k2.m.set(M2);
	k3.m.set(M3);
	k1.offset = phi1 + phi2;
	k2.offset = phi1 + phi2;
	k3.offset = phi1 + phi2;
};

int Collision_avoidanve::checkCollissionGeom() {
	int nk = 0;

	// nk = nk + checkLimit();

	nk = nk + checkPointCircle(Po0, k1, false, 1); // Eckpunkt Bassbox Front MHT
	nk = nk + checkPointCircle(Po0, k2, false, 2); // Eckpunkt Bassbox Rechts MHT
	nk = nk + checkPointCircle(Po0, k3, false, 3); // Eckpunkt Bassbox Rechts MHT

	nk = nk + checkPointCircle(Po1, k1, false, 4); // Eckpunkt Bassbox Front MHT
	nk = nk + checkPointCircle(Po1, k2, false, 5); // Eckpunkt Bassbox Rechts MHT
	nk = nk + checkPointCircle(Po1, k3, false, 6); // Eckpunkt Bassbox Rechts MHT

	nk = nk + checkPointCircle(Po2, k1, false, 7); // Eckpunkt Bassbox Front MHT
	nk = nk + checkPointCircle(Po2, k2, false, 8); // Eckpunkt Bassbox Rechts MHT
	nk = nk + checkPointCircle(Po2, k3, false, 9); // Eckpunkt Bassbox Rechts MHT

	nk = nk + checkPointCircle(P2, kx1, true, 10); // rechte Ecke MHT Buch Vorn Bassbox
	nk = nk + checkPointCircle(P2, kx3, true, 11); // rechte Ecke MHT Buch Vorn Bassbox
	nk = nk + checkPointCircle(P2, kx4, false, 12); // rechte Ecke MHT Buch Vorn Bassbox
	nk = nk + checkPointCircle(P2, kx5, false, 13); // rechte Ecke MHT Buch Vorn Bassbox

	nk = nk + checkPointCircle(P3, kx1, true, 14); // rechte Ecke MHT Buch Vorn Bassbox
	nk = nk + checkPointCircle(P3, kx3, true, 15); // rechte Ecke MHT Buch Vorn Bassbox
	nk = nk + checkPointCircle(P3, kx4, false, 16); // rechte Ecke MHT Buch Vorn Bassbox
	nk = nk + checkPointCircle(P3, kx5, false, 17); // rechte Ecke MHT Buch Vorn Bassbox

	nk = nk + checkPointCircle(P1, kx1, true, 18); // rechte Ecke MHT Buch Vorn Bassbox
	nk = nk + checkPointCircle(P1, kx3, true, 19); // rechte Ecke MHT Buch Vorn Bassbox
	nk = nk + checkPointCircle(P1, kx4, false, 20); // rechte Ecke MHT Buch Vorn Bassbox
	nk = nk + checkPointCircle(P1, kx5, false, 21); // rechte Ecke MHT Buch Vorn Bassbox

	nk = nk + checkPoints(P1, Po1, 30);
	nk = nk + checkPoints(P2, Po1, 31);
	nk = nk + checkPoints(P3, Po1, 32);
	nk = nk + checkPoints(P1, Po2, 33);
	nk = nk + checkPoints(P2, Po2, 34);
	nk = nk + checkPoints(P3, Po2, 35);
	nk = nk + checkPoints(P1, Po0, 36);
	nk = nk + checkPoints(P2, Po0, 37);
	nk = nk + checkPoints(P3, Po0, 38);
	return nk;
};


int Collision_avoidanve::setJointLimit(int driveID, int id, float value) {
	switch (driveID * 2 + id){
	case 0: theta1_min = value; break;
	case 1: theta1_max = value; break;
	case 2: theta2_min = value; break;
	case 3: theta2_max = value; break;
    }
	return 0; 
}

//************************************
bool Collision_avoidanve::checkLimit(float theta1, float theta2){
int nk = 0;

if ((theta1 < theta1_min)) nk++;
else if ((theta1 > theta1_max)) nk++;
if ((theta2 < theta2_min)) nk++;
else if ((theta2 > theta2_max)) nk++;

return (nk>0);
};
//************************************

float Collision_avoidanve::checkPoints(Pos P1, Pos P2, int num) {
	// pr�fe ecke
	float nk = 0;
	float l = norm(P1.x - P2.x, P1.y - P2.y);
	char col = 'y'; int lw = 1;
	if (l < 30) {
		col = 'c'; lw = 3;
		nk = 0.3;   // Achtung nk ist Int
		if (l < 24) {
			col = 'm'; lw = 5;
			nk = 1;
		}
	}
	//if true // box.plot > 2
	//	plot([P1(1)  P2(1)], [P1(2)  P2(2)], col, 'LineWidth', lw);
//  end
	return nk;
}
		//************************************


		//************************************************
		// pr�ft Punkt und Kreisbogenauf Kollision
float	Collision_avoidanve::checkPointCircle(Pos P, Collision_Circle cir, bool flag, int num) {
	float nk = 0; // keine Kollision
	int ret = 0;
	// pr�fe Ecke 1 Mht auf bogen 1
	float l = norm(P.x - cir.m.x, P.y- cir.m.y);

	// pr�fe ob auch Winkel stimmt
	float dx1 = P.x - cir.m.x;
	float dx2 = P.y - cir.m.y;
	float dxn = norm(dx1, dx2); // normierter Richtungsvektor
	float dxn1 = dx1 / dxn; // normierter Richtungsvektor
	float dxn2 = dx2 / dxn; // normierter Richtungsvektor
	float a = atan2(dx2, dx1);
	if (a < 0) {
		a = a + 2*M_PI;  // Winkel jetzt in Rad
	}

	float mi = cir.offset + ((cir.p.x < cir.p.y) ? cir.p.x : cir.p.y);
	float ma = cir.offset + ((cir.p.x < cir.p.y) ? cir.p.y : cir.p.x);
	if (mi < 0) {
		mi = mi + 2 * M_PI;
	}
	if (ma < 0) {
		ma = ma + 2 * M_PI;

	}
	char col = 'y'; int lw = 1;
	if (a >= mi && a <= ma) { // Ecke ist innerhalb Kreisbogen
		//kdr dl = l - cir->r;
		float dl = l- cir.r;
		/*
		switch (num) {
			case {4, 5, 6, 30, 31, 32}// Ecke 1
			ret = 4;
			case {10, 11, 14, 15, 18, 19}// Box innen
			ret = 3;
			case {1, 2, 3, 7, 8, 9, 12, 13, 16, 17, 20, 21, 33, 34, 35, 36, 37, 38}// Box aussen
			ret = 2;
			default:
				ret = 1;
		} // end switch
		*/
		if ((num == 12 || num == 16 || num == 20) && (fabs(dl) < 25)) {
			//dl;
		}
		if (((l + 13 > cir.r) && flag) || ((l - 13 < cir.r) && !flag)) {
			if (fabs(l - cir.r) > 150) {
				// nk = nk + 0.05; // M�gliche Fehlinterpr�tation wenn Hinderniss
				// Hinter der Box
				col = 'c'; lw = 2;
			}
			else if (fabs(l - cir.r) < 3) {
				nk = nk + 0.3;
				lw = 5;
			}
			else {
				nk = nk + 1;
				col = 'm'; lw = 3;
			}
		}
		//	plot([P(1)  P(1) - dxn(1) * dl], [P(2)  P(2) - dxn(2) * dl], col, 'LineWidth', lw);

	}

	if (a<mi || a>ma) { // Auserhalb des Winkelfensters
		col = 'g';
	}
	//		if true // box.plot > 2
	//			plot([P(1)  cir.m(1)], [P(2)  cir.m(2)], col, 'LineWidth', 1);
	//	end
	return nk;
};



	//************************************
Pos Collision_avoidanve::rotP(float xo, float yo, float phi) {
	float s = sind(phi);
	float c = cosd(phi);
	return Pos(c * xo - s * yo, s * xo + c * yo);
};

				//************************************

Pos Collision_avoidanve::rot(float* P, float phi) {
	float s = sind(phi);
	float c = cosd(phi);
	return Pos(c*P[0]- s*P[1], s*P[0] + c*P[1]);
	};


Pos  Collision_avoidanve::rotX(float* X, float* P, float phi) {
	   float s = sind(phi);
	   float c = cosd(phi);
	   return Pos( c* (P[0] - X[0]) -s*(P[1] - X[1]) + X[0], s*(P[0] - X[0]) +c*(P[1] - X[1]) + X[1]);
	};


// Implementieren der 2. Idee  mit pr�fen auf 2 Dimensionales Bahnfeld
// Array mit lowwer limits, upper limits otimal trajaectory
// Methode f�r theta1 und 2
//float maxTheta[] = { 0, 0, 0, 0 };
//float minTheta[] = { 0, 0, 0, 0 };
//float optTheta[] = { 0, 0, 0, 0 };

bool Collision_avoidanve::inArea(float theta1, float theta2) {
	// find index
	int in = ceil(theta1 / 10); // Index in Feld
	bool b = 0;// (theta2 > minTheta[in] && theta2 < maxTheta[in]);
	return b;
};
/*
	bool Collision_avoidanve::inArea(float mf, float x, float y) {
		// find index
		int in = ceil(x / 10);
		bool b1 = (y > mf(in, 2) && y < (mf(in, 3)));
		bool b2 = (y< mf(in, 4) || y>(mf(in, 5)));
		bool b = b1 && (isnan(mf(in, 4)) || b2);
		return b;
	};
	*/
