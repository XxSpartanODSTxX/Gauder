//unsigned actState=0;
unsigned int setState(unsigned int bit) {
  return actState |= bit;
};
unsigned int resetState(unsigned int bit) {
  return actState &= ~bit;
};
unsigned int getState() {
  return actState;
};

bool getState(unsigned int bit) {
  return (actState & bit) != 0;
};

bool resetError() {
  return actState &= ~(ERRORBit | CANCELBit);
};

/*
  Reset of a single Bit
*/
uint32_t resetBit(uint32_t value, int bitNum)
{
  uint32_t bit = ~(0x01u << bitNum);
  return (value & bit);
}

/*
  Setting of a single bit
*/
uint32_t setBit(uint32_t value, uint32_t bitNum)
{
  uint32_t bit = 0x01u << (int)bitNum;
  return (value | bit);
}

/*
  checks a singel bit if set or not
*/
bool CheckBit(uint32_t value, uint32_t bitNum)
{
  uint32_t bit = 0x01 << (int)bitNum;
  return ((value & bit) == bit);
}

#ifdef TEENSY
char* strcpy_s(const char* dest, unsigned int len, const char* src) {
  return strncpy(dest, src, len);
}
#endif

/*
  Seting of Variables from Command
  value is the String to Identify the Command
*/
int setProperty(Command* pscmd) {
  if (scmd.value != NULL) {
      if (strcmp(pscmd->value, "Override") == 0)
          Motion.override = pscmd->idata[0] / 100.0;
      else if (strcmp(pscmd->value, "InterpolationMode") == 0)
          Motion.setInterpolator(pscmd->idata[0]);
      else if (strcmp(pscmd->value, "OutTarget") == 0)
          Motion.outIndex = pscmd->idata[0];
      else if (strcmp(pscmd->value, "TCP") == 0) {
          Robot.setTCP(pscmd->idata[0]);
      }
      else if (strcmp(pscmd->value, "CollisionMode") == 0)
          Collision.setCheckMode(pscmd->idata[0]);
else if (strcmp(pscmd->value, "MoveOutIndex") == 0)
          Motion.outTarget=pscmd->idata[0];
      else if (strcmp(pscmd->value, "OperationalMode") == 0) {
          canOpen.setOperationalMode(pscmd->idata[0],1);
          canOpen.setOperationalMode(pscmd->idata[0],2);
	 
													   
									 
																 
      }
													   
									 
																 
	 

      else if (strcmp(pscmd->value, "LowerJointLimit") == 0) {
          int ind = pscmd->sub - 'v';
          Collision.setJointLimit(ind,0,  pscmd->idata[0]*M_PI/180);
          strcpy_s(pscmd->value, SHORT_BUFFER_LEN, "JointLimit");
      }

      
      else if (strcmp(pscmd->value, "MagOffset") == 0) {
          int ind = pscmd->sub - 'v';
          offsetVal[ind] = pscmd->idata[0];
          strcpy_s(pscmd->value, SHORT_BUFFER_LEN, "MagneticOffset");
      }

      else if (strcmp(pscmd->value, "MagScale") == 0) {
          int ind = pscmd->sub - 'v';
          offsetVal[ind+2] = pscmd->idata[0] / 100.0;
          strcpy_s(pscmd->value, SHORT_BUFFER_LEN, "MagneticOffset");
      }
      else if (strcmp(pscmd->value, "MagTollerance") == 0) {
          magTollerance = pscmd->idata[0] / 100.0;
          strcpy_s(pscmd->value, SHORT_BUFFER_LEN, "MagneticOffset");
      }

      else if (strcmp(pscmd->value, "0x601") == 0) {
          COMM.println("?Set CAN 0x601");  //todo Test this Command!
          canOpen.setValuesToCAN(pscmd->idata[0],pscmd->idata[1], pscmd->idata[2], pscmd->idata[3],pscmd->idata[4], 0x601);
      }
      else if (strcmp(pscmd->value, "0x602") == 0) {
          COMM.println("?Set CAN 0x602");
          canOpen.setValuesToCAN(pscmd->idata[0], pscmd->idata[1], pscmd->idata[2], pscmd->idata[3], pscmd->idata[4], 0x602);
      }
  }
  return 0;
}

/*
  gets a Value od a State fom Motioncontrolle
  value is the String to Identify the Command
*/
int getProperty(Command* pscmd) {
  if (strcmp(pscmd->value, "DrivePosition") == 0) {
    COMM.print("#DrivePosition= ");
    COMM.print(getDrivePosition(0x601), 4);
    COMM.print(" ");
    COMM.println(getDrivePosition(0x602), 4);
  }
  else if (strcmp(pscmd->value, "MagnetEncoder") == 0) {
    COMM.print("#MagnetEncoder= ");
    COMM.print(S1.getAngle());
    COMM.print(" ");
    COMM.println(S2.getAngle());
  }
  else if (strcmp(pscmd->value, "Interpolation") == 0) {
    COMM.print("#Interpolation= ");
    COMM.print(Motion.getThetaState()[0]);
    COMM.print(" ");
    COMM.println(Motion.getThetaState()[1]);
  }
  else if (strcmp(pscmd->value, "Target") == 0) {
    COMM.print("#Target= ");
    COMM.println(Motion.getLastTarget());
  }
  else if (strcmp(pscmd->value, "OutTarget") == 0) {
    COMM.print("#OutTarget= ");
    COMM.print(Motion.getOutTarget(0));
    COMM.print(" ");
    COMM.println(Motion.getOutTarget(1));
  }
  else if (strcmp(pscmd->value, "InterpolationMode") == 0) {
    COMM.print("#InterpolationMode= ");
    COMM.println(Motion.getInterpolationMode());
  }


  else if (strcmp(pscmd->value, "CollisionMode") == 0) {
    COMM.print("#CollisionMode= ");
    COMM.println(Collision.getCheckMode());
  }
  else if (strcmp(pscmd->value, "Override") == 0) {
    COMM.print("#Override= ");
    COMM.println(Motion.override * 100);
  }
  else if (strcmp(pscmd->value, "TCP") == 0) {
    COMM.print("#TCP= ");
    COMM.println(Robot.getTCP());
  }
  else if (strcmp(pscmd->value, "Direction") == 0) {
    COMM.print("#Direction= ");
    COMM.println(dir);
  }
  else if (strcmp(pscmd->value, "GearRatio") == 0) {
    COMM.print("#GearRatio= ");
    COMM.print(gear1);
    COMM.print(" ");
    COMM.println(gear2);
  }
  else if (strcmp(pscmd->value, "ListTargets") == 0) {
    COMM.print("#ListTargets= ");
    for (int index = 0; index < 32; index++) {
      COMM.print(index);
      COMM.print(" ");
      COMM.print(Motion.getTarget(index, 0), 4);
      COMM.print(" ");
      COMM.print(Motion.getTarget(index, 1), 4);
      COMM.println(", ");
    }
    COMM.println("#end");
  }
  else if (strcmp(pscmd->value, "DriveState") == 0) {
    COMM.print("#DriveState= 0x");
    COMM.print(canOpen.getDriveState(0x601), HEX);
    COMM.print("  0x");
    COMM.print(canOpen.getDriveState(0x602), HEX);
    COMM.println(" ;");
  }
  else if (strcmp(pscmd->value, "MagneticOffset") == 0) {
    COMM.print("#MagneticOffset= [ ");
    COMM.print(offsetVal[0]);
    COMM.print(" ");
    COMM.print(offsetVal[1]);
    COMM.print(" ");
    COMM.print(offsetVal[2] * 100);
    COMM.print(" ");
    COMM.print(offsetVal[3] * 100);
    COMM.print(" ]");
	COMM.println(magTollerance,4);
  }
  else if (strcmp(pscmd->value, "MagneticCalibaration") == 0) {
    COMM.print("#MagneticCalibaration1= ");
    int off = 0;
    int i = 0;
    while ((off = Robot.getMagOffset(0, i++)) != MagOffsetUnknown) {
      COMM.print(off);
      COMM.print(" ");
    }
    i = 0;
    COMM.println(" ");
    COMM.print("#MagneticCalibaration2= ");
    while ((off = Robot.getMagOffset(1, i++)) != MagOffsetUnknown) {
      COMM.print(off);
      COMM.print(" ");
    }
    COMM.println(" ");
  }
  else if (strcmp(pscmd->value, "Program") == 0) {
    COMM.print("#Program= [ ");
    for (int i = 0; i < Motion.programm.segmentsInProgramm; i++) {
      COMM.print(Motion.programm.progSeq[i]);
      COMM.print(" ");
    }
    COMM.println("]");
  }
  else {// get State
    COMM.print("#State= 0x");
    COMM.print(getState(), HEX);
    COMM.print(" ");
    COMM.print(getDrivePosition(0x601), 4);
    COMM.print(" ");
    COMM.print(getDrivePosition(0x602), 4);
    COMM.print("   ");
    COMM.print(S1.getAngle());
    COMM.print(" ");
    COMM.println(S2.getAngle());

  }
  return 0;
}
