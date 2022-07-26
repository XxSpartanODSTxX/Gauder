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
#ifdef TEENSY
  EepromStream eepromStream(0, EEPROM.length());
  deserializeJson(REPdoc, eepromStream);
  //Get Data from EEPROM
  if (REPdoc["dt"]) {
    //Interpolation time Motor
    dT = REPdoc["dt"]; //interpolation mode , coliition mode, activ tcp , overide(scaling from speed), last teach target(Another EEPROM Adress), OffsetVal
    dir = REPdoc["dir"]; //Open the satellite to the left side + open satellite to the right side
    gear1 = REPdoc["gear1"], gear2 = REPdoc["gear2"];
    interpolation = REPdoc["interpolation"];
    collition = REPdoc["collition"];
    tcp = REPdoc["tcp"];
    serializeJson(REPdoc, Serial), Serial.println();
  } else
#endif
  {
    dT = 0.025f;
    dir = -1; //- Open the satellite to the left side + open satellite to the right side
    gear1 = 40;
    gear2 = 40;
  }
  ///////////////////////////////////////////////////////
  //Con una sola vez es activado se queda los valores pero no permite conectarte al motor ya que se encuentra en otro modo
  delay(2000);
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

  //  axis1 = getDrivePosition(0x601);
  //  axis2 = getDrivePosition(0x602);

#ifndef TEENSY  // Simulate Magnetencoders
  S1.sim(axis1 * 2048 / (M_PI * offsetVal[2]) + offsetVal[0]);
  S2.sim(axis2 * 2048 / (M_PI * offsetVal[3]) + offsetVal[1]);
#endif
  doCommands('R', '0', 0 );
  axisMagnet1 = (S1.getRawAngle() - offsetVal[0]) * offsetVal[2] * M_PI / 2048;
  axisMagnet2 = (S2.getRawAngle() - offsetVal[1]) * offsetVal[3] * M_PI / 2048;
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
  Serial.print(axisPos.a1);
  Serial.print(" ");
  Serial.println(axisPos.a2);
  Serial.print("Magnet in Rad:");
  Serial.print(axisMagnet1);
  Serial.print(" ");
  Serial.println(axisMagnet1);

  //  Serial.println("set Homming");
  //  Serial.print("Val 1 motor: ");
  //  Serial.print(dir * axisMagnet1 * gear1 * 2048  / (M_PI * 2));
  //  Serial.print(" ,Val 1 magnet senor rad: ");
  //  Serial.println(axisMagnet1);
  //  Serial.print("Val 2 motor: ");
  //  Serial.print(dir * axisMagnet2 * gear2 * 2048 / (M_PI * 2));
  //  Serial.print(" ,Val 2 magnet senor rad: ");
  //  Serial.println(axisMagnet2);
  // canOpen.setOffset((dir * axisMagnet1 * gear1 * 2048 / (M_PI * 2)), 0x601);
  // canOpen.setOffset((dir * axisMagnet2 * gear2 * 2048 / (M_PI * 2)), 0x602);
  //  delay(200);
  //doCommands('R', '0', 0 );
  //  delay(200);
  //  Serial.print("Motor 1 Value direct: ");
  //  Serial.println(axis1);
  //  Serial.print("Motor 2 Value direct: ");
  //  Serial.println(axis2);
  //
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

  //
  //
  //   StaticJsonDocument<200> doc;
  //  char jsson[] =
  //    "{\"sensor\":\"gps\",\"time\":1351824120,\"data\":[48.756080,2.302038]}";
  //
  //  // Deserialize the JSON document
  //  DeserializationError errors = deserializeJson(doc, jsson);
  //
  //  const char* sensor = doc["sensor"];
  //  long time = doc["time"];
  //  double latitude = doc["data"][0];
  //  double longitude = doc["data"][1];
  //
  //  // Print values.
  //  Serial.println(sensor);
  //  Serial.println(time);
  //  Serial.println(latitude, 6);
  //  Serial.println(longitude, 6);

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
AxisPos* getDrivePosition(AxisPos* paxisPos) {
  if(paxisPos->t1>0)
  paxisPos->axis1 = canOpen.positionStatus(0x601);
  if (paxisPos->axis1 != 0){
    paxisPos->a1 = (dir * (paxisPos->axis1 + (motorOffset[0])) * 2 * M_PI) / (gear1 * 2000);
    paxisPos->t1 = 0;
  }
  if(paxisPos->t2>0)
  paxisPos->axis2 = canOpen.positionStatus(0x602);
  if (paxisPos->axis2 != 0) {
    paxisPos->a2 = (dir * (paxisPos->axis2 + (motorOffset[1])) * 2 * M_PI) / (gear2 * 2000);
    paxisPos->t2 = 0;
  }
  return paxisPos;
}
