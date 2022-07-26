/*Print some variables to the Serial
 *Version 1.0 Beta vom 2.06.2021
 */
void toSer() {
  Serial.print("& ");
  Serial.print(Motion.phase);
  Serial.print(", wait:");
  Serial.print(nWait);
  Serial.print(", coll:");
  Serial.print(bColl);
  Serial.print(", acc:");
  Serial.print(Motion.Ax1.acc);
  Serial.print(", vel_1:");
  Serial.print(Motion.Ax1.vel);
  Serial.print(", vel_2:");
  Serial.print(Motion.Ax2.vel);
  Serial.print(", lamda:");
  Serial.print(Motion.lamda);
  Serial.print(", pos_1:");
  Serial.print(Motion.Ax1.pos);
  Serial.print(", pos_2:");
  Serial.print(Motion.Ax2.pos);
  Serial.print(", posx_1:");
  Serial.print(Motion.Ax1.posx);
  Serial.print(", posx_2:");
  Serial.print(Motion.Ax2.posx);
  Serial.println(" ");
  isCommandAvailable = false;
}

void getBuffMsg(CAN_message_t msg) {
  Serial.print("$ ");
  Serial.print(msg.buf[0], HEX );
  Serial.print(" ");
  Serial.print(msg.buf[1], HEX );
  Serial.print(" ");
  Serial.print(msg.buf[2], HEX );
  Serial.print(" ");
  Serial.print(msg.buf[3], HEX );
  Serial.print(" ");
  Serial.print(msg.buf[4], HEX );
  Serial.print(" ");
  Serial.print(msg.buf[5], HEX );
  Serial.print(" ");
  Serial.print(msg.buf[6], HEX );
  Serial.print(" ");
  Serial.print(msg.buf[7], HEX );
}

void printStepAndPos(unsigned state, float *step, float posicionMotor1, float posicionMotor2, float MagnetEncoder1, float MagnetEncoder2) {
  Serial.print("* 0x");
  Serial.print(state); // missing digits by printing
  Serial.print(" ");
  Serial.print(step[0], 4); // missing digits by printing
  Serial.print(" ");
  Serial.print(step[1], 4);
  Serial.print(" ");
  Serial.print(step[2], 4);
  Serial.print(" ");
  Serial.print(step[3], 4);
  Serial.print(" ");
  Serial.print(posicionMotor1, 4);
  Serial.print(" ");
  Serial.print(posicionMotor2, 4);
  Serial.print(" ");
  Serial.print(MagnetEncoder1, 4);
  Serial.print(" ");
  Serial.print(MagnetEncoder2, 4);
  Serial.print(" ");
}

void getMotorData(float xx, float xy, float dx1, float dy1) {
  Serial.print("% ");
  Serial.print(xx, 4); // missing digits by printing
  Serial.print(" ");
  Serial.print(xy, 4);
  Serial.print(" ");
  Serial.print(dx1, 4);
  Serial.print(" ");
  Serial.print(dy1, 4);
  Serial.print(" ");
  Serial.print(posicionMotor1);
  Serial.print(" ");
  Serial.print(posicionMotor2);
  Serial.print(" ");
  //#ifdef TEENSY
  Serial.print(S1.getRawAngle());
  Serial.print(" ");
  Serial.print(S2.getRawAngle());
  Serial.print(" ");
  //#endif
  Serial.print("0x601");
  Serial.print(" ");
  Serial.print(canOpen.positionStatus(0x601));
  Serial.print(" ");
  Serial.print("0x602");
  Serial.print(" ");
  Serial.print(canOpen.positionStatus(0x602));
  Serial.println(" ");
}

void printCommand()
{
  Serial.print(">>> ");
  Serial.print(scmd.cmd);
  Serial.print(" ");
  Serial.print(scmd.sub);
  Serial.print(" ");
  Serial.print(scmd.icmd);

  if (scmd.value != NULL   && (strlen(scmd.value) > 0)) {
    Serial.print(" v:");
    Serial.print(scmd.value);
  }
  if (scmd.sdata != NULL && (strlen(scmd.sdata) > 0)) {
    Serial.print(" s:");
    Serial.print(scmd.sdata);
  }
  for (int i = 0; i < scmd.ndata; i++) {
    Serial.print(" i:");
    Serial.print(scmd.idata[i]);
    Serial.print(" f:");
    Serial.print(scmd.fdata[i]);
  }
  Serial.println(" ");
}

//Add msg to the SD
void printError(int level, const char*  errMsg) {
  switch (level) {
    case 0:
      COMM.print("#Info: ");
      break;
    case 1:
      COMM.print("#Warning: ");
      break;
    case 2:
      COMM.print("#Error: ");
	  setState(ERRORBit);					 
      break;
    case 3:
      COMM.print("#Fatal Error: ");
	  setState(ERRORBit);					 
      break;
  }
  COMM.println(errMsg);
}
