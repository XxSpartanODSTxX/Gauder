/*
   Library for Teensy 4.1
   Author: Roberto Martinez
   Version 1.0 Beta vom 2.06.2021
*/
#ifndef GauderCAN_h
#define GauderCAN_h
#include "FlexCAN_T4.h"
#include <i2c_driver_wire.h>

class GauderCAN
{
  public:

    void setValuesToCAN(int bytes, char index1, char index2, char subIndex, int inputValue, uint32_t id);
    void setMotorMode(int mode, uint32_t id);
    void standByMode(int input, uint32_t id);
    void setupAndStart(int);
    void startPositionMode(int positionValue, uint32_t id);
    void startVelocityMode(int speedValue, uint32_t id);
    void readDataFromL();
    void startHomingMode(int homingType, uint32_t id);
    void setHomingType(int homingType, uint32_t id);
    void startUpZeroPosition(uint32_t id);
    void interpolatedPositionCLSetup(int interpolTime, uint32_t id);
    void setInterPosition(uint32_t id, uint32_t pos);
    void setOffset(int offSetVal, uint32_t id);
    void positionStatus();

    uint16_t getDriveState(uint32_t id);  // Function to get Drivestate

    int setOperationalMode(int mode, uint32_t driveID); // function to set to operational Mode
    int positionStatus(uint32_t id);
    int motorPosition(int id);

  private:
    FlexCAN_T4<CAN1, RX_SIZE_256, TX_SIZE_16> can1;
    static CAN_message_t msg;
    static uint8_t hex[17];
    //Motor Modes:
    unsigned char autoSetupMode[8] = {0x2F, 0x60, 0x60, 0x00, 0xFE, 0x00, 0x00, 0x00};
    unsigned char homingSetupMode[8] = {0x2F, 0x60, 0x60, 0x00, 0x06, 0x00, 0x00, 0x00};
    unsigned char torqueMode[8] = {0x2F, 0x60, 0x60, 0x00, 0x04, 0x00, 0x00, 0x00};
    unsigned char velocityMode[8] = {0x2F, 0x60, 0x60, 0x00, 0x02, 0x00, 0x00, 0x00};
    unsigned char positionMode[8] = {0x2F, 0x60, 0x60, 0x00, 0x01, 0x00, 0x00, 0x00};
    unsigned char interpolationMode[8] = {0x2F, 0x60, 0x60, 0x00, 0x07, 0x00, 0x00, 0x00};
    //Motor State:
    unsigned char switchOn[8] = {0x2B, 0x40, 0x60, 0x00, 0x07, 0x00, 0x00, 0x00};
    unsigned char readySwitch[8] = {0x2B, 0x40, 0x60, 0x00, 0x06, 0x00, 0x00, 0x00};
    unsigned char operationEnable[8] = {0x2B, 0x40, 0x60, 0x00, 0x0F, 0x00, 0x00, 0x00};
    unsigned char  startMotor[8] = {0x2B, 0x40, 0x60, 0x00, 0x1F, 0x00, 0x00, 0x00};
    unsigned char stopAndClear[8] = {0x2B, 0x40, 0x60, 0x00, 0x00, 0x00, 0x00, 0x00};
    //Motor Direction:
    unsigned char rightDirection[8] = {0x2F, 0x7E, 0x60, 0x00, 0x00, 0x00, 0x00, 0x00};
    unsigned char leftDirection[8] = {0x2F, 0x7E, 0x60, 0x00, 0xF0, 0x00, 0x00, 0x00};
    /*
      Homing Type:
      Method 35 references to the current position (we use this one at the last option)
      Methods -17 to -18 and -23 to -30 have no index pulse (first)
      Methods -1 to -2 and -7 to -14 contain an index pulse (second option)
    */
    unsigned char indexCurrentPosition[8] = {0x2F, 0x98, 0x60, 0x00, 0x23, 0x00, 0x00, 0x00};
    unsigned char encoderIndexAHittingN[8] = {0x2F, 0x98, 0x60, 0x00, 0xFF, 0x00, 0x00, 0x00};
    unsigned char encoderIndexAHittingP[8] = {0x2F, 0x98, 0x60, 0x00, 0xFE, 0x00, 0x00, 0x00};
    unsigned char blockHoming[8] = {0x2F, 0x98, 0x60, 0x00, 0xEF, 0x00, 0x00, 0x00};
    /*
       Minimum Current For Block Detection: mA
       Period of Blocking: in ms
       Homing Acceleration:
       Speed During Search For Zero:
       Speed During Search For Switch:
    */
    unsigned char minCurrentBlocking[8] = {0x23, 0x3A, 0x20, 0x01, 0x00, 0x00, 0x00, 0x00};
    unsigned char blockingPeriod[8] = {0x23, 0x3A, 0x20, 0x02, 0x00, 0x00, 0x00, 0x00};
    unsigned char homingAcceleration[8] = {0x23, 0x9A, 0x60, 0x00, 0x00, 0x00, 0x00, 0x00};
    unsigned char speedSearchingZero[8] = {0x23, 0x99, 0x60, 0x02, 0x00, 0x00, 0x00, 0x00};
    unsigned char speedSearchingSwitch[8] = {0x23, 0x99, 0x60, 0x01, 0x00, 0x00, 0x00, 0x00};
    bool wire0;
    unsigned char registerAddress;
    unsigned char registerStartAddress;
    const unsigned char MAGNITUDE = 0x1B;
    const unsigned char ANGLE = 0x0E;
    const unsigned char RAWANGLE = 0X0C;
    const unsigned char ZPOS = 0x01;
#define READ     0x20
#define WRITE    0x10
#define SETRATE  0x30
};
#endif
