void loop() {
  startTime = millis();
  if (!isCommandAvailable) {
    readDataFromL();
    // readDataFromK();
    readDataFromS();
  }
  if (isCommandAvailable) {
    resetError();
  }
  if (bPrintCan)
    readCanMsg();
  axis1 = getDrivePosition(0x601);
  axis2 = getDrivePosition(0x602);


#ifndef TEENSY  // Simulate Magnetencoders
  S1.sim(axis1 * 2048 / (M_PI * offsetVal[2]) + offsetVal[0]);
  S2.sim(axis2 * 2048 / (M_PI * offsetVal[3]) + offsetVal[1]);
#endif
    //Serial.print("Magnet Encoder 1 & 2: ");
    //Serial.print(S1.getRawAngle());
    //Serial.print("  ");
    //Serial.println(S2.getRawAngle());
    

  axisMagnet1 = (S1.getRawAngle() - offsetVal[0]) * offsetVal[2] * M_PI / 2048;
  axisMagnet2 = (S2.getRawAngle() - offsetVal[1]) * offsetVal[3] * M_PI / 2048;
  
   if(axisMagnet2 < -3.0){ //Correct values 
   axisMagnet2+=2*M_PI;
  }
  
  float *step = Motion.getNextStep_StopFunction(getState(STOPBit));//bStop
  if (step != NULL) {
    if (getState(DRIVEBit)) {
      if (!isPushCommand()) {
        if (!getState(ERRORBit) && !getState(CANCELBit)) {
          posicionMotor1 = dir * step[0] * gear1 * 2000 / (M_PI * 2);
          posicionMotor2 = dir * step[1] * gear2 * 2000 / (M_PI * 2);
          canOpen.setInterPosition((posicionMotor1 - (motorOffset[0])), 0x601);
          delay(4);
          canOpen.setInterPosition((posicionMotor2 - (motorOffset[1])), 0x602);
          delay(4);
          printStepAndPos(getState(), step, axis1, axis2, axisMagnet1, axisMagnet2);
          Serial.println(millis() - startTime);
        }
      }
      else if (firstError) {
        printError(1, "Interrupted by Push comand"); firstError = false;
      }
    }
    else if (firstError) {
      printError(1, "Drives are not on!"); firstError = false;
      Motion.reset();
    }
  }
  else {
    firstError = true;
    // Do all the stuff in the Motionbuffer
    pcmd = getNextCommand();
    if (pcmd != NULL && pcmd->bvalid) {
      doCommands(pcmd);
    }

    isCommandAvailable = false; // todo: make posible to Receive command while still executing especially for Stopp
    resetState(ACTIVBit);
    if (nWait++ > 100) { // 5 sec = 500
      nWait = 0;
      Serial.print("0x");
      Serial.print(getState(), HEX);
      Serial.print(" ");
      Serial.print(axis1, 4);
      Serial.print(" ");
      Serial.print(axis2, 4);
      Serial.print("   ");
      Serial.print(axisMagnet1, 4);
      Serial.print(" ");
      Serial.print(axisMagnet2, 4);
      Serial.print(" ");
      Serial.print(S1.getRawAngle());
      Serial.print(" ");
      Serial.print(S2.getRawAngle());
      Serial.print(" S1-offset:");
      Serial.print(S1.getRawAngle() - offsetVal[0]);
      Serial.print(" before pi");
      Serial.print((S1.getRawAngle() - offsetVal[0]) * offsetVal[2]);
      Serial.print(" result");
      Serial.println((S1.getRawAngle() - offsetVal[0]) * offsetVal[2] * M_PI / 2048);
    }
  }
  // check for POSITION_InBit
  if (axis1 < EPS && axis1 > -EPS && axis2 < EPS && axis2 > -EPS)
    setState(POSITION_InBit);
  else
    resetState(POSITION_InBit);

  // check for POSITION_OutBit
  if ((axis1 > 1.0 || axis1 < -1.0))
    setState(POSITION_OutBit);
  else
    resetState(POSITION_OutBit);

  // find Akt Target
  nt = Motion.getLastTarget();
  if (nt != ntold) {
    ntold = nt;
    COMM.print("#Target:");
    COMM.println(nt);
  } if (millis() - startTime < 30)
    delay(30 - (millis() - startTime));
}
