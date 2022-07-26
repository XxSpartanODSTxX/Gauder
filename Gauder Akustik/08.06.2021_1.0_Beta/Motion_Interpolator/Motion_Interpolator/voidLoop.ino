void loop() {
  startTime = millis();
  if (!isCommandAvailable) {
    readDataFromL();
    readDataFromS();
  }
  if (isCommandAvailable) {
    resetError();
  }
  axisPos.t1++;
  axisPos.t2++;
  if (bPrintCan)
    readCanMsg();
//  axis1 = getDrivePosition(0x601);
//  axis2 = getDrivePosition(0x602);

#ifndef TEENSY  // Simulate Magnetencoders
  S1.sim(axisPos.a1 * 2048 / (M_PI * offsetVal[2]) + offsetVal[0]);
  S2.sim(axisPos.a2 * 2048 / (M_PI * offsetVal[3]) + offsetVal[1]);
#endif
//    Serial.print("Magnet Encoder: ");
//    Serial.print(S1.getRawAngle());
//    Serial.print("  ");
//    Serial.println(S2.getRawAngle());

  axisMagnet1 = (S1.getRawAngle() - offsetVal[0]) * offsetVal[2] * M_PI / 2048;
  axisMagnet2 = (S2.getRawAngle() - offsetVal[1]) * offsetVal[3] * M_PI / 2048;
  float *step = Motion.getNextStep_StopFunction(getState(STOPBit));//bStop
  if (step != NULL) {
    if (getState(DRIVEBit)) {
      if (!isPushCommand()) {
        if (!getState(ERRORBit) && !getState(CANCELBit)) {
          posicionMotor1 = dir * step[0] * gear1 * 2000 / (M_PI * 2);
          posicionMotor2 = dir * step[1] * gear2 * 2000 / (M_PI * 2);
//          Serial.print("MPosicion 1:");
//          Serial.print(posicionMotor1);
//          Serial.print(" MPosicion 2:");
//          Serial.println(posicionMotor2);
          canOpen.setInterPosition((posicionMotor1 - (motorOffset[0])), 0x601); //canOpen.setInterPosition((posicionMotor1-(motorOffset[0])), 0x601);
          delay(4);
          canOpen.setInterPosition((posicionMotor2 - (motorOffset[1])), 0x602); //canOpen.setInterPosition((posicionMotor2-(motorOffset[1])), 0x602);
          delay(4);
          printStepAndPos(getState(), step, axisPos.a1, axisPos.a2, axisMagnet1, axisMagnet2);
          Serial.println(millis() - startTime);
          //          if ((fabs(axisMagnet1 - axis1) < magTollerance) && (fabs(axisMagnet2 - axis2) < magTollerance)) {
          //            canOpen.setInterPosition((posicionMotor1-(motorOffset[0])), 0x601); //canOpen.setInterPosition((posicionMotor1-(motorOffset[0])), 0x601);
          //            delay(4);
          //            canOpen.setInterPosition((posicionMotor2-(motorOffset[1])), 0x602); //canOpen.setInterPosition((posicionMotor2-(motorOffset[1])), 0x602);
          //            delay(4);
          //            printStepAndPos(getState(), step, axis1, axis2, axisMagnet1, axisMagnet2);
          //            Serial.println(millis() - startTime);
          //          } else {
          //            printError(2, "Axis not Match");
          //            printStepAndPos(getState(), step, axis1, axis2, axisMagnet1, axisMagnet2);
          //            Serial.println(millis() - startTime);
          //            Motion.reset();
          //          }
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
    if (nWait++ > 200) {
      nWait = 0;
      Serial.print("0x");
      Serial.print(getState(), HEX);
      Serial.print(" ");
      Serial.print(axisPos.a1, 4);
      Serial.print(" ");
      Serial.print(axisPos.a2, 4);
      Serial.print("   ");
      Serial.print(axisMagnet1, 4);
      Serial.print(" ");
      Serial.print(axisMagnet2, 4);
      Serial.println(" ");

    }
  }
  // check for POSITION_InBit
  if (axisPos.a1 < EPS && axisPos.a1 > -EPS && axisPos.a2 < EPS && axisPos.a2 > -EPS)
    setState(POSITION_InBit);
  else
    resetState(POSITION_InBit);

  // check for POSITION_OutBit
  if ((axisPos.a1 > 1.0 || axisPos.a1 < -1.0))
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
