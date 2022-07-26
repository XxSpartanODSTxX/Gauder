/*
   Routine to handle Commands
   Version 1.0 Beta vom 2.06.2021
*/
int doCommands(char c, char sub, int icmd) {
  Serial.print("Entro a do command ");
  Serial.print(c);
  Serial.print(sub);
  Serial.print(" ");
  Serial.println(icmd);
  setState(ACTIVBit);
  int n = 0;
  printCommand();
  switch (c) {
    case 'H': //Motion In
    case 'I': //Motion In
      Serial.println("Motion In");
      isCommandAvailable = true;

      // Check for Drives on  if not switch them on
      axisPos.axis1 = getDrivePosition(0x601);
      axisPos.axis2 = getDrivePosition(0x602);
      Serial.print(axisPos.axis1);
      Serial.print(" ");
      Serial.println(axisPos.axis2);
      resetState(POSITION_OutBit);
      Motion.reset(axisPos.axis1, axisPos.axis2); //Actual Value  position
      Motion.moveIn();
      Motion.movebSpline();
      setState(POSITION_InBit);
      arrayIndex = 0;
      break;
    case 'B':
    case 'O': //Motion Out
      Serial.println("Motion Out");
      isCommandAvailable = true;
      // Check for Drives on  if not switch them on
      axisPos.a1 = getDrivePosition(0x601);
      axisPos.a2 = getDrivePosition(0x602);
      resetState(POSITION_InBit);
      Motion.reset(axisPos.a1, axisPos.a2); //Actual Value  position
      Motion.moveOut();
      Motion.doInterpolation();
      setState(POSITION_OutBit);
      arrayIndex = 0;
      break;
    case 'M': //Main Program
      Serial.println("Main Program");
      isCommandAvailable = true;
      mainTestProgram();
      arrayIndex = 0;
      break;
    case 'P': //Turn On
      Serial.println("Turn On");
      isCommandAvailable = true;
      Serial.print("Position Rad:");
      Serial.print(getDrivePosition(0x601));
      Serial.print(" ");
      Serial.println(getDrivePosition(0x602));
      Serial.print("Motor position: ");
      Serial.print(canOpen.positionStatus(0x601));
      Serial.print(" ");
      Serial.println(canOpen.positionStatus(0x602));
      canOpen.interpolatedPositionCLSetup(30, 0x601);
      delay(5);
      canOpen.interpolatedPositionCLSetup(30, 0x602);
      delay(5);
      setState(DRIVEBit);
      arrayIndex = 0;
      break;
    case 'Q': // Store Target
      Motion.setFinalTarget(sub, getDrivePosition(0x601), getDrivePosition(0x602), 0.1, 1);
      isCommandAvailable = true;
      arrayIndex = 0;
      break;
    case 'R': //Reset Motor Values
      Serial.println("Reset Motor Values");
      isCommandAvailable = true;
      //canOpen.setOffset(0, 0x601);
      //delay(5);
      canOpen.startUpZeroPosition(0x601);
      //delay(5);
      //canOpen.setOffset(0, 0x601);
      delay(5);
      canOpen.startUpZeroPosition(0x602);
      delay(5);
      Serial.print("Position Rad:");
      Serial.print(axisPos.a1);
      Serial.print(" ");
      Serial.println(axisPos.a2);
      setState(REFERENCEBit);
      arrayIndex = 0;
      break;

    case 'S': //Turn Off Motor
      Serial.println("Turn Off Motor");
      isCommandAvailable = true;
      canOpen.standByMode(5, 0x601);
      delay(10);
      canOpen.standByMode(5, 0x602);
      delay(10);
      arrayIndex = 0;
      resetState(DRIVEBit);
      break;
    case 'T':
    case 'K': { // Move to Target  (Satellite)
        Serial.print("Move Satellite ");
        Serial.print(sub);
        Serial.print(" ");
        Serial.println(icmd);
        moveTarget(sub, icmd);
        isCommandAvailable = true;
      }

      break;
    case 'W': {// Write Values
        setProperty(&scmd);
        getProperty(&scmd);
      }

      break; // ("falling-through")
    case 'E': {
        Serial.println("Print to the EEPROM");
        secondCommand(sub, icmd);
        isCommandAvailable = true;
      }
      break;
    case 'G': {
        getProperty(&scmd);
      }
      break;
    case 'N': {
        Serial.println("relay HIGH");
        digitalWrite(relay, HIGH);
        break;
      //   case 'S':
      case 'L':
        Serial.println("relay LOW");
        digitalWrite(relay, LOW);
        break;
      }
    case 'A': {
        Serial.print("Changing angle to: ");
        break;
      }
    case 'C': {
        Serial.println("Changing color!");
        break;
      }
  }
  return c;
}

/*******************************************************+
  hadles teh Move Target Command
*/
int moveTarget(char sub, int icmd) {
  getDrivePosition(&  axisPos);
  switch (sub) {
    case 's':  // Teach Targert   Target 16 or OutTarget is last saved Target
      Motion.setFinalTarget(icmd, axisPos.a1, axisPos.a2, 0.3, 1);
      break;
    case 'g': // Turn Targert to absolut Position (without using invers Kinematik)
//      axis1 = Motion.getTarget(Motion.outIndex, 0);
//      axis2 = Motion.getTarget(Motion.outIndex, 1);
      Motion.reset(getDrivePosition(0x601), getDrivePosition(0x602));
      axisPos.a2 +=  icmd * M_PI / 180.0;  // Set orientation Angle to absolut value
      Motion.setFinalTarget(Motion.outTarget, axisPos.a1, axisPos.a2, 0, 0);
      Motion.moveTarget(Motion.outTarget);
      Motion.doInterpolation();
      break;
    case 'h': // Turn Targert to absolut Position
      //   float axis2offset = Motion.getTarget(Motion.outIndex, 2); // Zero position for Absolut Angle of MHTBox;

      axisPos.a1 = Motion.getOutTarget(0);
      axisPos.a2 = Motion.getOutTarget(1);
      Motion.reset(getDrivePosition(0x601), getDrivePosition(0x602));
      if (Robot.getTCP() == 0) {
        posTheta[0] = axisPos.a1;
        posTheta[1] = axisPos.a2 + icmd * M_PI / 180.0;  // Set orientation Angle to absolut value
      }
      else {
        posWorld = Robot.jointWorld(axisPos.a1, axisPos.a2);
        posWorld[2] = axisPos.a2 + icmd * M_PI / 180.0; // Set orientation Angle to absolut value
        posTheta = Robot.worldJoint(posWorld[0], posWorld[2]);
      }
      Motion.setFinalTarget(Motion.outTarget, posTheta[0], posTheta[1], 0, 0);
      Motion.moveTarget(Motion.outTarget);
      Motion.doInterpolation();
      break;
    case 't': // Turn Targert
      posWorld = Robot.jointWorld(axisPos.a1, axisPos.a2);
      posWorld[2] += scmd.fdata[0];
      posTheta = Robot.worldJoint(posWorld[0], posWorld[2]);
      Motion.setFinalTarget(icmd, posTheta[0], posTheta[1], 0, 0);
      Motion.moveTarget(icmd);
      Motion.doInterpolation();
      break;
    case 'j': // Teach Targert
      Motion.setFinalTarget(icmd, scmd.fdata[0], scmd.fdata[1], scmd.fdata[2], scmd.fdata[3]);
      break;
    case 'w':
      posTheta = Robot.worldJoint(scmd.fdata[0], scmd.fdata[1]);
      Motion.setFinalTarget(icmd, posTheta[0], posTheta[1], scmd.fdata[2], scmd.fdata[3]);
      break;
    case 'a': // Move Targert angle
      Motion.setFinalTarget(icmd, scmd.fdata[0], scmd.fdata[1], scmd.fdata[2], scmd.fdata[3]);
      Motion.moveTarget(icmd);
      Motion.doInterpolation();
      break;
    case 'k':// Move Targert kartesian
      posTheta = Robot.worldJoint(scmd.fdata[0], scmd.fdata[1]);
      Motion.setFinalTarget(icmd, posTheta[0], posTheta[1], scmd.fdata[2], scmd.fdata[3]);
      Motion.moveTarget(icmd);
      Motion.doInterpolation();
      break;
    case 'r':// Get Aktual Target Position
      Motion.reset(axisPos.a1, axisPos.a2); //Actual Value  position
      COMM.print("#TargetPos ");
      COMM.print(icmd);
      COMM.print(" = ");
      COMM.print(Motion.getTarget(icmd, 0), 4);
      COMM.print(" ");
      COMM.println(Motion.getTarget(icmd, 1), 4);
      break;
    default: {
        Motion.reset(axisPos.a1, axisPos.a2); //Actual Value  position
        //Motion.reset(getDrivePosition(0x601), getDrivePosition(0x602));
        if (subcmd >= 32)
          Motion.moveTarget(icmd);
        else
          Motion.moveTarget(subcmd);
        Motion.doInterpolation();
        // Motion.movebSpline();
      }
      break;
  }
  return 0;
}

/*******************************************************+
  hadles teh Move Target Command
*/
int secondCommand(char sub, int icmd) {
  switch (sub) {
    case 's':// Set Val
      Serial.println("Set EEPROM");
      Serial.print("val interno: ");
      Serial.println(icmd);
      break;
    case 'r':// Read Value
      Serial.println("Read EEPROM Values");
      Serial.print("val interno: ");
      Serial.println(icmd);
      break;
    case 'o':// Set Offset
      Serial.println("Set offset to motor:");
      Serial.print("value: ");
      Serial.println(icmd);
      canOpen.setOffset(icmd, 0x601);
      break;
  }
  return 0;
}
