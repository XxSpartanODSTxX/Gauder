bool preConditionInit(bool force) {
  bool isOK = true;
  resetCommand();
  if (peekCommand()->isOverride) {
        removeCommand();
  }
  if (!getState(REFERENCEBit)) {
    //Set the value all the time
//    Serial.println("set Homming");
//    Serial.print("Val 1 motor: ");
//    Serial.print(dir * axisMagnet1 * gear1 * 2000 / (M_PI * 2));
//    Serial.print(" ,Val 1 magnet senor rad: ");
//    Serial.println(axisMagnet1);
//    Serial.print("Val 2 motor: ");
//    Serial.println(dir * axisMagnet2 * gear2 * 2000 / (M_PI * 2));
//    Serial.print(" ,Val 2 magnet senor rad: ");
//    Serial.println(axisMagnet2);
//    canOpen.setOffset(dir * axisMagnet1 * gear1 * 2000 / (M_PI * 2), 0x601);
//    canOpen.setOffset(dir * axisMagnet2 * gear2 * 2000 / (M_PI * 2), 0x602);
    //just asking if it is in the correct position
    //setCommand('R', '0', 0, false);
    isOK = false;
  }
     if (getState(ACTIVBit)) {
        if (force) {
            Serial.println("Stop actual Motion");
            setCommand('D', '0', 0, false);
        }
    }
  if (!getState(DRIVEBit)) {
    if (force) {
      setCommand('P', '0', 0, false);
      Serial.println("Power ON make P");
    }
    isOK = false;
  }
  if (!getState(POSITION_OutBit)) {
    if (force) {
      //Move Out
      setCommand('O', '0', 0,false);

      Serial.println("Move OUT");				 
    }		 
    isOK = false;
  }
if (!getState(POSITION_OutBit)) {
        if (force) {
            //Move Out
           // doCommands('O', '0', 0);
            setCommand('O', '0', 0, false);

            Serial.println("Move OUT");
        }
        isOK = false;
    }									 
  return isOK;
}












bool preConditionShutdown(bool force) {
  bool isOK = true;
  resetCommand();
      if (getState(ACTIVBit)) {
        if (force) {
            Serial.println("Stop actual Motion");
            setState(STOPBit); // Interpolation should Stop immediately									 
            setCommand('D', '0', 0, false);
        } 
    }
  if (!getState(POSITION_InBit)) {
    if (!getState(DRIVEBit)) {
      if (force) {
        setCommand('P', '0', 0, false);
        //Switch ON make R and make P
      }
    }
    Serial.println("Go In");
    setCommand('I', '0', 0, false);
  }
  if (getState(DRIVEBit)) {
    //Power off
    //doCommands('S ', '0', 0);
    setCommand('S', '0', 0, false);
    Serial.println("Power OFF");
  }
  return isOK;
}


/*
  Routin to Init The Systemextendet using all avalable Information
*/
int systemInit() {
  // check Magnetic Encoder
  float eps = 0.01;
  int iwm = S1.getAngle();  // get Magnetic angle
  float wm = iwm; // Calculate the right Angle using simple scaling factor
  if (wm < 0) printError(3, "");

  //Check Motor
  // Get 6041h Statusword (Nonotec Doumentation Page 333)
  //  https://en.nanotec.com/products/manual/PD4C_CAN_DE/object_dictionary%252Fod_motion_0x6041.html?cHash=2ff9ec26e9531d337c5f2f3a9bb33b9a
  uint16_t driveSatate = canOpen.getDriveState(0x601); // check if Motor is responding
  if (driveSatate != 0x34) printError(3, " "); // check all possible Error

  // Check position using Motor and Magnetic
  int iwd = canOpen.positionStatus(0x601);  // Get Motor Position
  float wd = iwd;
  if (wd > 0) printError(3, " ");  // check Position
  if ((wm - wd) > eps || (wm - wd) < -eps) // Compare Motor posiiton
    printError(3, " ");
  return 0;
}

/*
  Routine to make System Test at Startup
  Check Magnet Sesiors
  Check Motors
  Check Comiunikation
*/
int systemTest() {

  systemInit();
  float wm = 0, wd = 0, eps = 0.01;

  // check differenz between Magnet and Motor
  if ((wm - wd) > eps || (wm - wd) < -eps) // Compare Motor posiiton
    printError(3, " ");

  // check Communication
  // check if a Monitor is responding
  COMM.print("System DARC1000 ");
  COMM.print(dir);
  COMM.println("");

  // check if communication controller is responding
  return 0;
}


/*
  System Calibration
*/
int SystemCalibrate() {
  int iwm = S1.getAngle();  // get Magnetic angle
  // move al lite to the left
  // check Magnet and Motor
  // Move a litle tio the right
  // check magnet

  //Calculate Direction
  //Calculate Offset
  // check all Errors
  // MoveOut
  // sampling Motot an Magnetig values

  // Calculat the calibration

  // Write calibration Values to EEProm
  return 0;
}
