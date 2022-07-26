#define _USE_MATH_DEFINES // for C++
#include <math.h>
#include "axis.h"


void AxisLib::setInterpolator(int id ) {
    profileInterpolate = &AxisLib::poly5;
}

void AxisLib::initMotion3(float accel, float decel) {
    vo_last = vo;
     tba = fabs(3 * vo) / (accel * 2);// Beschleunigungszeit
     tbd = fabs(3 * vo) / (decel * 2);// Verzögerungszeit
    float tMax = fabs((xs - pos) / vo) + (tba + tbd) / 2; // Gesamtzeit für die Bewegung

    ntc = (tMax - tba - tbd) / dT; // Anzahl der Zeitschritte für konstantfahrt
    float yx_1 = 0.5;
    float s1 = fabs(vo * yx_1 * tba); // Beschleunigungsweg
    float s2 = fabs(vo * yx_1 * tbd); // Verzögerungsweg
    if ((s1 + s2) > 1e-6) {
        fa = fabs(xs - pos) / (s1 + s2); // Bestimmen ob Bahngeschwindigkeit erreicht wird
        if (fa < 1.0f) {
            vo *= fa;    // Wenn nicht:  reduzieren der Geschwindigkeit
            tba = fabs(3 * vo) / (accel * 2);// neuberechung der Beschleunigungszeit
            tbd = fabs(3 * vo) / (accel * 2);// neuberechne der Verzögerungszeit
        }

    }
    else {
        vo = 0;
    }

}

void AxisLib::initMotion5(float accel, float decel) {
    vo_last = vo;
   
    if (fabs(vo) < 1e-6) {
        tba = fabs(15 * vo) / (accel * 8);// Beschleunigungszeit
        tbd = fabs(15 * vo) / (decel * 8);// Verzögerungszeit
        
    }
    else {
        tba = fabs(15 * vo) / (accel * 8);// Beschleunigungszeit
        tbd = fabs(15 * vo) / (decel * 8);// Verzögerungszeit
  //      tba = accel;
  //      tbd = decel;
        float tMax = fabs((xs - pos) / vo) + (tba + tbd) / 2; // Gesamtzeit für die Bewegung

        ntc = (tMax - tba - tbd) / dT; // Anzahl der Zeitschritte für konstantfahrt
        float yx_1 = 0.5;
        float s1 = fabs(vo * yx_1 * tba); // Beschleunigungsweg
        float s2 = fabs(vo * yx_1 * tbd); // Verzögerungsweg
        if ((s1 + s2) > 1e-6) {
            fa = fabs(xs - pos) / (s1 + s2); // Bestimmen ob Bahngeschwindigkeit erreicht wird
            if (fa < 1.0f) {
                vo *= fa;    // Wenn nicht:  reduzieren der Geschwindigkeit
                tba = fabs(15 * vo) / (accel * 8);// neuberechung der Beschleunigungszeit
                tbd = fabs(15 * vo) / (decel * 8);// neuberechne der Verzögerungszeit
            }

        }
        else {
            vo = 0;
        }
    }
}

 /**
 * Acceleration with Polyramp 5
 * l = 0 : 1
 */
float* AxisLib::poly5(float lamda){
  float l2 = lamda * lamda;
  float l3 = l2 * lamda;
  acc = vo* 30.0 * ((lamda-2)*lamda +1) * l2/tba;
  vel = vo* ((6.0*lamda -15)*lamda +  10)* l3;
  pos = vo*   ((lamda   -3.0)*lamda +   2.5)*l3*lamda*tba;
  posx +=  vel*dT; // position
  return &posx;
}

/**
 * Acceleration with Polyramp 3
 * l = 0 : 1
 */
float*  AxisLib::poly3( float l){
  acc = vo*6*(-l+1)*l/tba;             // acceleration
  vel = vo* (-2*l+3)*l*l;          // velocyty
  pos = vo* (-1/2*l+1)*l*l*l*tba; // position
  posx +=  vel*dT*as; // position
  return &posx;
}

/**
*Acceleration with Polyramp 3 to change speed
* l = 0 : 1
*/
float* AxisLib::poly3Via(float lamda) {
    acc = (vo - vo_last) * 6 * (-lamda + 1) * lamda;             // acceleration
    vel = vo_last + (vo - vo_last) * (-2.0 * lamda + 3.0) * lamda * lamda;
//    pos = pos_last + vo * (-1 / 2 * l + 1) * l * l * l * tba; // position
    pos += vel * dT * as;
    posx += vel * dT * as;
    return &posx;
}

/**
 * Acceleration with Sin square Ramp
 * l = 0 : 1
 */
float*  AxisLib::sin2( float lamda){
  float a= M_PI_2;
  float sl= sin(lamda *M_PI_2);
  float cl= cos(lamda * M_PI_2);
  acc = vo*3.1415*sl*cl/tba;    // acceleration
  vel = vo*sl*sl ;          // velocyty
  pos = vo* (lamda /2 - sin(2*a* lamda)/(4*a))*tba;// position
  posx +=  vel*dT*as; // position
  return &posx;
}

/**
* halte aktuelle Position
*/
 float*  AxisLib::holdPos(float lamda){
   x0= pos; // Setzt dei letzte Position auf aktuelle Position
   return &posx;
}

 /**
 * fahre mit konstanter Geschwindigkeit
 */
 float* AxisLib::constSpeed( float lamda){
           pos  += vo*dT;
           posx += vo*dT;
           return &posx;
}

  bool AxisLib::checkforDeceleration(){
      if ( fabs(vo)<0.001 ) 
          return true; // no Speed 
      if ((xs-posx)*vo<0) 
          return true; // overshoot
      return (fabsf(posx-xs)<=fabs(tba*vo/(2.0)));
  }
  bool AxisLib::checkforMaxSpeed() {
      if (fabs(vo) < 0.01) return true;
      return (fabsf(posx - xs) <= fabs(tba * vo ));
  }

  
