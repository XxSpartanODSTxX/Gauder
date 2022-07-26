void setup() {
  COMM.begin(115200);
  delay(200);
  COMM.println("MC Switch 'ON' V1.0");
#ifdef TEENSY
  LC.begin(115200); //9600
  can1.begin();
  can1.setBaudRate(1000000);
  can1.setClock(CLK_80MHz);
  Wire.begin();
  Wire1.begin();
#endif
  COMM.println("Finish Serial Setup");
  ///////////////////////////////////////////////////////
//#ifdef TEENSY
//  EepromStream eepromStream(0, EEPROM.length());
//  deserializeJson(REPdoc, eepromStream);
//  //Get Data from EEPROM
//  if (REPdoc["dt"]) {
//    //Interpolation time Motor
//    dT = REPdoc["dt"]; //interpolation mode , coliition mode, activ tcp , overide(scaling from speed), last teach target(Another EEPROM Adress), OffsetVal
//    dir = REPdoc["dir"]; //Open the satellite to the left side + open satellite to the right side
//    gear1 = REPdoc["gear1"], gear2 = REPdoc["gear2"];
//    interpolation = REPdoc["interpolation"];
//    collition = REPdoc["collition"];
//    tcp = REPdoc["tcp"];
//    serializeJson(REPdoc, Serial), Serial.println();
//  } else
//#endif
//  {
    dT = 0.025f;
    dir = -1; //- Open the satellite to the left side + open satellite to the right side
    gear1 = -50; // First harmonic drive big box
    gear2 = -50; // Second Gear ratio
//  }
  ///////////////////////////////////////////////////////
  //Con una sola vez es activado se queda los valores pero no permite conectarte al motor ya que se encuentra en otro modo
  //--Aqui en esta linea habia un reset
  //delay(2000);
  Serial.println("Set Operational Mode"); //tiene que ser (1,1)o (1,2)
  canOpen.setOperationalMode(1, 1);
  delay(5);
  canOpen.setOperationalMode(1, 2);
  delay(5);
  Serial.println("Finish Operational Mode");
  Motion.setdT(dT);
  //send InterpolationTime (read and set)
#ifdef TEENSY
  if (REPdoc["interpolation"] && REPdoc["collition"] && REPdoc["tcp"]) {
    Collision.init(REPdoc["collition"]);
    Motion.setInterpolator(REPdoc["interpolation"]);
    Robot.setTCP(REPdoc["tcp"]);
  }
  else
#endif
  {
    Collision.init(2);
    Motion.setInterpolator(3);
    Robot.setTCP(0);
  }

  axis1 = getDrivePosition(0x601);
  axis2 = getDrivePosition(0x602);

#ifndef TEENSY  // Simulate Magnetencoders
  S1.sim(axis1 * 2048 / (M_PI * offsetVal[2]) + offsetVal[0]);
  S2.sim(axis2 * 2048 / (M_PI * offsetVal[3]) + offsetVal[1]);
#endif
//Should be a reset + offset of the magnet encoder
  doCommands('R', '0', 0 );
  axisMagnet1 = (S1.getRawAngle() - offsetVal[0]) * offsetVal[2] * M_PI / 2048;
  axisMagnet2 = (S2.getRawAngle() - offsetVal[1]) * offsetVal[3] * M_PI / 2048;
   
   if(axisMagnet2 < -3.0){ //Correct values 
   axisMagnet2+=2*M_PI;
  }
  
  motorOffset[0] = (int)(dir * axisMagnet1 * gear1 * 2048 / (M_PI * 2)); //2048
  motorOffset[1] = (int)(dir * axisMagnet2 * gear2 * 2048 / (M_PI * 2));
  Serial.print("Motor Offset: ");
  Serial.print(motorOffset[0]);
  Serial.print(" ");
  Serial.println(motorOffset[1]);
  Serial.print("S1: ");
  Serial.print(S1.getRawAngle());
  Serial.print("  S2: ");
  Serial.println(S2.getRawAngle());
  Serial.print("Motor Position:");
  Serial.print(axis1);
  Serial.print(" ");
  Serial.println(axis2);
  Serial.print("Magnet in Rad:");
  Serial.print(axisMagnet1);
  Serial.print(" ");
  Serial.println(axisMagnet2);
#ifdef SDFAT
  if (!SD.begin(SD_CONFIG)) {
    Serial.println(F("begin failed"));
    return;
  }
  while (SD.exists(fileName)) {
    if (fileName[BASE_NAME_SIZE + 1] != '9') {
      fileName[BASE_NAME_SIZE + 1]++;
    } else if (fileName[BASE_NAME_SIZE] != '9') {
      fileName[BASE_NAME_SIZE + 1] = '0';
      fileName[BASE_NAME_SIZE]++;
    } else {
      Serial.println(F("Can't create file name"));
      return;
    }
  }
  if (!file.open(fileName, O_RDWR | O_CREAT | O_TRUNC)) {
    Serial.println(F("open failed"));
    return;
  }
  Serial.print(F("opened: "));
  Serial.println(fileName);
  //  JsonArray error = EDoc.createNestedArray("Error");
  //  error.add("E401");
  //  error.add("E101");
  //
  //  JsonArray fit = EDoc.createNestedArray("fit");
  //  fit.add("sfcxz");
  //  fit.add("xced");
  //  serializeJsonPretty(EDoc, file);
  //
  file.close();
#endif
  toSer();
  delay(200);
  COMM.println("Ready to recive commands:");
}

/***********************************************************************************
  get current Druve Position
  return value in Radians
*/
float getDrivePosition(int id) {
  float mValue = 0;
  switch (id) {
    case 0x601:
      mValue = (dir * (canOpen.positionStatus(id) + (motorOffset[0])) * 2 * M_PI) / (gear1 * 2000); //(dir * canOpen.positionStatus(id)* 2 * M_PI) / (gear1 * 2000) in hdh
      break;
    case 0x602:
      mValue = (dir * (canOpen.positionStatus(id) + (motorOffset[1])) * 2 * M_PI) / (gear2 * 2000);
      break;
    default :
      mValue = 0;
      break;
  }
  return mValue;
}
