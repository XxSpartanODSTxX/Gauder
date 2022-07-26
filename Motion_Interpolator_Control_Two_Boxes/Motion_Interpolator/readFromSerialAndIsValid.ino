/*
  read Data from Keeybord
  Version 1.0 Beta vom 2.06.2021
*/
void readDataFromK() {
  char c;
  pcmd =  &scmd;
#ifndef TEENSY
  while (_kbhit()) // Nur wenn auch eine Taste gedr�ckt ist
  {
    c = _getch();
    charsFromL[arrayIndex++] = c;
    if (isCommandValid(charsFromL, arrayIndex, pcmd)) {

      Serial.print("K>> ");
      Serial.print(charsFromL);
      Serial.print("  ");
      Serial.println(arrayIndex);
      Serial.println("Start doCommands");
      doCommands(ccmd, subcmd, icmd);
      arrayIndex = 0;
    }
  }
#endif
}

/*
  read Data from Monitor
*/
void readDataFromL() {
  char c;
  Command* pcmd = &scmd;
  while (COMM.available() > 0) {//COM Serial Usb
    c = COMM.read();
    if (arrayIndex >= BUFFER_LEN - 1) {
      Serial.print("L>> error! ");
      Serial.println(charsFromL);
      charsFromL[arrayIndex] = '\0';
      arrayIndex = 0;
      break;
    }

    charsFromL[arrayIndex++] = c;

    if (isCommandValid(charsFromL, arrayIndex, pcmd)) {
      Serial.print("L>> ");
      Serial.print(charsFromL);
      Serial.print("  ");
      Serial.println(arrayIndex);
      Serial.println("Start doCommands");
      doCommands(ccmd, subcmd, icmd);
      arrayIndex = 0;
    }
    else if (c == '\0' || c == '\n' || c == '\r') {
      arrayIndex = 0;
    }
  }
}

/*
  read Data from "nd Serial Communication controller
*/
void readDataFromS() {
  char c;
  Command* pcmd = &scmd;
#ifndef TEENSY
  while (_kbhit()) // Nur wenn auch eine Taste gedr�ckt ist
  {
    c = _getch();
    if (c == '\r' || c == '!')
      c = '\0';
    std::cout << c;

#else
  while (LC.available() > 0) {//COM Serial Usb
    c = LC.read();
#endif
    charsFromS[arrayIndexS++] = c;
    switch (charsFromS[0]) {
      case 'O':
        preConditionShutdown(true);
        setCommand('S', '0', 0, false);
        firstCommand();

        arrayIndexS = 0;
        break;
      case 'I': {
          Serial.println("Commando I entro:   x");
          if (c == '\0' || c == '\n' || c == '\r') {
            icmd = atoi(&charsFromS[1]);
            Serial.println("Command I");
            preConditionInit(true);
            scmd.fdata[0] = M_PI * icmd / 180.0;
		  setCommand('K', 'g', dir * icmd, false);
            firstCommand();
            arrayIndexS = 0;
          } else {
            if (arrayIndexS > 5)
              arrayIndexS = 0;
          }
        } break;
      case 'A': {
          if (c == '\0' || c == '\n' || c == '\r') {
            icmd = atoi(&charsFromS[1]);
            preConditionInit(true);
            scmd.fdata[0] = M_PI * icmd / 180.0;
            setCommand('K', 'g', dir * icmd, false);
            firstCommand();
            arrayIndexS = 0;
          }
          else {
            if (arrayIndexS > 5)
              arrayIndexS = 0;
          }
        }
        break;
      case '+': {
          if (c == '\0' || c == '\n' || c == '\r') {
            icmd = atoi(charsFromS);
            preConditionInit(true);
            scmd.fdata[0] = M_PI * icmd / 180.0;
            setCommand('K', 'g',dir * icmd, false);
            firstCommand();
            arrayIndexS = 0;
          }
          else {
            if (arrayIndexS > 4)
              arrayIndexS = 0;
          }
        }
        break;
      case '-': {
          if (c == '\0' || c == '\n' || c == '\r') {
            icmd = atoi(charsFromS);
            preConditionInit(true);
            scmd.fdata[0] = M_PI * icmd / 180.0;
            setCommand('K', 'g', dir * icmd, false);
            firstCommand();
            arrayIndexS = 0;
          }
          else {
            if (arrayIndexS > 4)
              arrayIndexS = 0;
          }
        }
        break;
      default: {

          if (isCommandValid(charsFromS, arrayIndexS, pcmd)) {
            Serial.print(" S>> ");
            Serial.print(charsFromS);
            Serial.print("  ");
            Serial.println(arrayIndexS);
            Serial.println("Start doCommands");
            doCommands(ccmd, subcmd, icmd);
            arrayIndexS = 0;
          } else if (c == '\0' || c == '\n' || c == '\r') {
            arrayIndexS = 0;
          }
          break;
        }
    }
  }
}

//Agregar una funcion para cuando se cambie el status en 6060
int readCanMsg() {
  int x = 0;
  short x16 = 0;
  int y = 0;
  int cmd = 0;
  int sub_cmd = 0;
  while ( can1.read(msg) ) {// Static CAN_message_t msg;
    Serial.print("$ ");
    Serial.print("CanMsg: ");
    Serial.print(msg.id, HEX);
    Serial.print(">> ");
    switch (msg.id) {
      // Status & Position
      case 0x181:
      case 0x182:
        getBuffMsg(msg);
        break;
      // Position
      case 0x281:
      case 0x282:
        x = msg.buf[0] | msg.buf[1] << 8 | msg.buf[2] << 16 | msg.buf[3] << 24;
        Serial.print(x);
        break;
      // Velocity
      case 0x381:
      case 0x382:
        x16 = msg.buf[0] | msg.buf[1] << 8 | msg.buf[2] << 16 | msg.buf[3] << 24;
        Serial.print(x16);
        break;
      // Command via CAN Bus
      case 0x610:
        cmd = msg.buf[0];
        sub_cmd = msg.buf[1];
        icmd = msg.buf[2] | msg.buf[3] << 8 ;
        x = msg.buf[4] | msg.buf[5] << 8 | msg.buf[6] << 16 | msg.buf[7] << 24;

        scmd.cmd = cmd;
        scmd.sub = sub_cmd;
        scmd.icmd = icmd;
        scmd.idata[0] = x;
        scmd.ndata = 1;
        Serial.print("CanCommand: ");
        Serial.print(msg.buf[0], HEX);
        Serial.print(">> ");
        doCommands(cmd, sub_cmd, icmd);
        break;
      case 0x481:
      case 0x482: // Inputs
      case 0x581:
      case 0x582:  // SDO service Data Objects
      case 0x681:
      case 0x602:  // SDO service Data Objects
      // All the others
      default:
        getBuffMsg(msg);
        Serial.print(" ");
        x = msg.buf[0] | msg.buf[1] << 8 | msg.buf[2] << 16 | msg.buf[3] << 24;
        Serial.print(x);
        break;
    }
    Serial.println(" ");
  };
  return x;
}

/*
   checks if Command is Valis
   cmd Comandline
   len index of last char
*/
bool isCommandValid(char* cmd, int len , Command* pcmd) {
  int aIndex = len;
  Command* pscmd;
  if ((cmd[len - 1] == '\r') || (cmd[len - 1] == '\n') || (cmd[len - 1] == '}') || (cmd[len - 1] == '\0') || (cmd[len - 1] == '!')) {
    cmd[len] = '\0';
    aIndex = 0;
  }
  int i = 0;

  ccmd = cmd[i];
  icmd = 0;

  scmd.cmd = ccmd;
  if (pcmd != NULL) {
    pcmd->bvalid = 0;
    pcmd->cmd = ccmd;
  }
  scmd.sub = '0';
  scmd.ndata = 0;
  //scmd.value = cmdvalue;
  scmd.value[0] = '\0';
  //cmdvalue[0] = '\0';
  //scmd.sdata = cmddata;
  scmd.sdata[0] = '\0';

  //cmddata[0] = '\0';



  switch (ccmd) {
    //case 'L':
    case 'F':
      {
#ifdef TEENSY
        //serializeJsonPretty(EDoc, file);
        //file.close();
#else
        // Parse example data
        pCmdDoc = JSON::Parse(json);
        Serial.println(pCmdDoc->Stringify(true));
        if (file.is_open()) {
          file << pCmdDoc->Stringify(true).c_str();
          file.close();
        }
        delete pCmdDoc;
#endif
        break;
      }
    case 'O': //Out old
    case 'B': //Out

    case 'I': //In
    case 'H': // In Home

    case 'M':
    case 'P': // Power on
    case 'S':
    case 'R':
    case 'D': // Stop Motion  (without Switch off Drives)
      {
        subcmd = '0';
        return true;
      }
      break;
    case 'N': // Setdigital Output to High
      {
        Serial.println("HIGH");
        break;
      }
    case 'L': // Setdigital Output to Low
      {
        Serial.println("Low");
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
    case 'T':
    case 'K':
      {
        if (aIndex == 0) { // w for World Target
          Serial.println("Entro en cierto");
          pscmd  = scannCmd(cmd, len, pcmd);
          memcpy(pcmd, pscmd, sizeof(Command));
          Serial.print(pcmd->cmd);
          Serial.print(" ");
          Serial.print(subcmd);
          Serial.print(" ");
          Serial.println(icmd);
          return true;
        }
        else
          return false;
        break;
      }
    //EEPROM should read the sub index for another type of action
    case 'E': {
        return true;
        break;
      }
    case 'W': // Set Value
    case 'G': // Get Value
      if (aIndex == 0) {
        pcmd = scannCmd(cmd, len, pcmd);
        cmd[len] = 0;
        char ch;
        return true;
      }
      break;
    case 'Z':
      break;

    case 'V': {
        if (aIndex == 0) {
          pcmd = scannCmd(cmd, len, pcmd);
          cmd[len] = 0;
          char ch;
        }
        break;
      }
    // Json Command String
    case '{': {
        if (aIndex == 0) {
          //        if (len < 5)
          //          cmd = json;
#ifdef TEENSY
          cmd[len] = '\0';
          for (int i = 1; i < len; i++) {
            if (cmd[i] == '\'') cmd[i] = '\"';
          }
          Serial.print(F("Json root: "));
          Serial.println(cmd);

          DeserializationError error = deserializeJson(jdoc, cmd , len);
          if (error) {
            Serial.print(F("deserializeJson() failed: "));
            Serial.println(error.f_str());
            return false;
          }

          Serial.print(F("Json root:: "));
          Serial.println(cmd);


          if (jdoc.containsKey("cmd")) {
            jscmd = jdoc["cmd"];
            ccmd = jscmd[0];
            pcmd->cmd = ccmd;
          }
          Serial.print(F("Json cmd::: "));
          Serial.print(jscmd);
          Serial.print(" ");
          Serial.println(ccmd);

          if (jdoc.containsKey("sub")) {
            jscmd = jdoc["sub"];
            subcmd = jscmd[0];
            pcmd->sub = subcmd;
          }
          if (jdoc.containsKey("id")) {
              if (jdoc["id"].is<signed int>()) {
                  icmd = jdoc["id"];
              }
              else {
                  jscmd = jdoc["id"];
                  if ((jscmd[1] == 'x') || (jscmd[1] == 'X'))
                      icmd =(strtol(&jscmd[2], (char**)0, 16));
                  else
                      icmd =atoi(jscmd);
              }
            pcmd->icmd  = icmd;
            pcmd->idata[0] = icmd;
            pcmd->ndata = 1;
          }

          if (jdoc.containsKey("value")) {
            jscmd = jdoc["value"];
            strcpy(  pcmd->value , jscmd);
          }
          if (jdoc.containsKey("data")) {
              JsonArray array = jdoc["data"].as<JsonArray>();
              for (JsonVariant v : array) {
                  jscmd = v.as<const char*>();
                  Serial.println(jscmd);
                  pcmd->fdata[pcmd->ndata  ] = atof(jscmd);
                  if ((jscmd[1] == 'x') || (jscmd[1] == 'X'))
                      pcmd->idata[pcmd->ndata++] = (strtol(&jscmd[2], (char**)0, 16));
                  else
                      pcmd->idata[pcmd->ndata++] = atoi(jscmd);												 
              }
           
            strcpy( pcmd->sdata , jscmd);
          }


          if (jdoc.containsKey("t1")) {
            Serial.print(F("Json t1: "));
            pcmd->fdata[0] = atof(jscmd);
            pcmd->ndata = 1;
          }
          if (jdoc.containsKey("t2")) {
            Serial.print(F("Json t2:: "));
            pcmd->fdata[1] = atof(jscmd);
            pcmd->ndata = 2;
          }

          Serial.print(F("Json sub:::: "));
          Serial.print(subcmd);
          Serial.print(" i:");
          Serial.print(icmd);
          if (pcmd->value != NULL && strlen(pcmd->value) > 0 ) {
            Serial.print(" v:");
            Serial.print(pcmd->value);
          }
          if (pcmd->sdata != NULL && strlen(pcmd->sdata) > 0 ) {
            Serial.print(" s:");
            Serial.print(pcmd->sdata);
          }
          Serial.println(" ");
          return true;

#else
          Serial.print("Win Json CMD: ");
          cmd[len] = '\0';
          pharsJson(cmd, len, pcmd);
          //      para = root[L"data"];
          //     long time = root["time"]->AsLong();;
          Serial.print("Json CMD: ");
          Serial.print(cmd);
          Serial.print(">>  ");
          //       Serial.print(scmd);
          Serial.print(" ");
          Serial.print((char)ccmd);
          Serial.print(" ");
          Serial.print((char)subcmd);
          Serial.print(" ");
          Serial.println(icmd);
          delete pCmdDoc;
          return true;
#endif
        }
        break;
      }
    case '\n':
    case '\r':
    case 0: {
        len = 0;
        break;
      }
    default:
      //arrayIndex = 0;
      break;
  }
  return false;
};

Command* scannCmd(char* cmd, int len, Command* pcmd) {
  ccmd = 0;
  subcmd = 0;
  int valcount = 0;
  int j = 0;
  int cmdval = 0;  // State of value  0 notheing 1 waiting 2 processing 3 finished 4 equaloperator
  scmd.ndata = 0;
  char buffer[64];
  int k = 0, cmdvl = 0, cmddl = 0, icmdvl = 0;
  bool bHex, bquote = false;
  cmdvalue[0] = '\0';
  cmddata[0] = '\0';

  for (int i = 0; i < len; i++) {
    if (isspace(cmd[i])) {
      if (cmdval == 1) {
        cmdval = 2;
      }
      else if (cmdval == 2) {
        cmdval = 3;
        cmdvalue[cmdvl++] = '\0';
      }
    }
    else if (cmd[i] == '=' || cmd[i] == ':') {
      if (cmdval == 2) {
        cmdval = 4;
        cmdvalue[cmdvl++] = '\0';
      }
    }
    else if (cmd[i] == '\'' || cmd[i] == '\"') {
      bquote = !bquote;
      if (cmdval == 1) {
        cmdval = 2;
      }
    }

    else  if (isalpha(cmd[i])) {
      if (ccmd == 0 && isupper(cmd[i])) {
        ccmd = cmd[i];  // set cmd
      }
      else if (subcmd == 0 && islower(cmd[i])) {
        subcmd = cmd[i]; // set sub
        cmdval = 1;
      }
    }
    else while (cmdval == 2 && isdigit(cmd[i])) {
        buffer[icmdvl++] = cmd[i++];  // set value
        buffer[icmdvl] = '\0';
        icmd = atoi(buffer);
        cmdval = 2;
      }
    if (cmdval == 2 || bquote) {
      if (isalpha(cmd[i]) || isdigit(cmd[i]))
      {
        cmdvalue[cmdvl++] = cmd[i];  // set value
      }
    }
    else {
      if (cmdval == 3 || cmdval == 4) {
        if (cmdvl > 0) {
          icmd = atoi(cmdvalue);
          scmd.icmd = icmd;

          cmdvl = 0;
        }
        cmddl = 0;
        cmdval = 5;
        j = i;
        while (j < len &&  cmddl < 64  && cmd[j] != '\n' && cmd[j] != '\r') {
          cmddata[cmddl++] = cmd[j++];
        }
        cmddata[cmddl++] = '\0';
      }
      if (isdigit(cmd[i]) || cmd[i] == '.' || cmd[i] == '-' || cmd[i] == '+') {
        if (cmd[i] == '0' && cmd[i + 1] == 'x')
        {
          bHex = true;
          buffer[k++] = cmd[i++];
          buffer[k++] = cmd[i++];
          while (isdigit(cmd[i]) || isalpha(cmd[i])) {
            buffer[k++] = cmd[i++];
          }
        }
        else
        {
          bHex = false;
          buffer[k++] = cmd[i++];
          while (isdigit(cmd[i]) || cmd[i] == '.' || cmd[i] == '-' || cmd[i] == '+') {
            buffer[k++] = cmd[i++];
          }

        }
        buffer[k] = '\0';
        if (bHex) {
          scmd.idata[valcount] = (int)strtol(buffer, NULL, 16);
          if (pcmd != NULL)
            pcmd->idata[valcount] = (int)strtol(buffer, NULL, 16);;
        }
        else {
          scmd.idata[valcount] = atoi(buffer);
          if (pcmd != NULL)
            pcmd->idata[valcount] = atoi(buffer);
        }
        scmd.fdata[valcount] = atof(buffer);
        scmd.ndata = ++valcount;
        if (pcmd != NULL)
          pcmd->ndata = valcount;
        k = 0;
        if (valcount >= 16) break;
      }

    }
  }
  if (pcmd != NULL) {
    pcmd->cmd = ccmd;
    pcmd->sub = subcmd;
    pcmd->icmd = icmd;
#ifdef TEENSY
    strcpy(pcmd->value,  cmdvalue);
    strcpy(pcmd->sdata,  cmddata);
#else
    strcpy_s(pcmd->value, SHORT_BUFFER_LEN, cmdvalue);
    strcpy_s(pcmd->sdata, BUFFER_LEN, cmddata);

#endif
    return pcmd;
  }

  scmd.cmd = ccmd;
  scmd.sub = subcmd;
  scmd.icmd = icmd;
#ifdef TEENSY
  strcpy(scmd.value , cmdvalue);
  strcpy(scmd.sdata , cmddata);
#else
  strcpy_s(scmd.value , SHORT_BUFFER_LEN, cmdvalue);
  strcpy_s(scmd.sdata , BUFFER_LEN,  cmddata);

#endif
  return &scmd;
}

//readSerial
#ifndef TEENSY
int pharsJson(char* cmd, int len, Command* pcmd) {
  std::wstring wstcmd;
  for (int i = 1; i < len; i++) {
    if (cmd[i] == '\'') cmd[i] = '\"';
  }
  pCmdDoc = JSON::Parse(cmd);
  if (pCmdDoc == NULL)
    return false;
  JSONObject root = pCmdDoc->AsObject();

  JSONValue* jscmd = root[L"cmd"];
  if (jscmd != NULL) {
    wstcmd = jscmd->AsString();
    ccmd = static_cast<char>(wstcmd[0]);
    pcmd->cmd = ccmd;
  }
  jscmd = root[L"sub"];
  if (jscmd != NULL) {
    wstcmd = jscmd->AsString();
    subcmd = static_cast<char>(wstcmd[0]);
    pcmd->sub = subcmd;
  }
  jscmd = root[L"id"];
  if (jscmd != NULL) {
    wstcmd = jscmd->AsString();
    if(wstcmd[1]=='x')
     icmd = std::stoul(wstcmd, nullptr, 16);
    else
     icmd = std::stoi(wstcmd);
    pcmd->icmd = icmd;
    pcmd->idata[0] = icmd;
    pcmd->ndata = 1;
  }
  jscmd = root[L"value"];
  if (jscmd != NULL) {
    wstcmd = jscmd->AsString();
    sprintf_s(strValue, "%ls", wstcmd.c_str());
    strcpy_s(pcmd->value , SHORT_BUFFER_LEN, strValue);

  }
  jscmd = root[L"data"];
  if (jscmd != NULL && jscmd->IsArray()) {

    JSONArray ja = jscmd->AsArray();
    int i = 0;
    for (i = 0; i < ja.size(); i++) {
      wstcmd = ja[i]->AsString();
      pcmd->idata[i] = std::stoul(wstcmd, nullptr, 16);
      pcmd->fdata[i] = std::stof(wstcmd);
    }
    pcmd->ndata = i;
  }
  jscmd = root[L"t1"];
  if (jscmd != NULL) {
    wstcmd = jscmd->AsString();
    jData[0] = std::stof(wstcmd);
    pcmd->fdata[0] = jData[0];
    pcmd->ndata = 1;
  }
  jscmd = root[L"t2"];
  if (jscmd != NULL) {
    wstcmd = jscmd->AsString();
    jData[1] = std::stof(wstcmd);
    pcmd->fdata[1] = jData[1];
    pcmd->ndata = 2;

  }
  jscmd = root[L"v"];
  if (jscmd != NULL) {
    wstcmd = jscmd->AsString();
    jData[2] = std::stof(wstcmd);
    pcmd->fdata[2] = jData[2];
    pcmd->ndata = 3;

  }
  jscmd = root[L"a"];
  if (jscmd != NULL) {
    wstcmd = jscmd->AsString();
    jData[3] = std::stof(wstcmd);
    pcmd->fdata[3] = jData[3];
    pcmd->ndata = 4;
  }
}
#endif
