/*
* Datei zu Simulation eines TEENSY CAN treibers mit den wichtigsten beötigten Befehlen
* Prof. Dr.-Ing Klaus Dieter Rupp
* 19.04.2021
*/
#ifndef GauderCAN_dummy_h
#define GauderCAN_dummy_h

typedef struct CAN_message_t {
    uint32_t id = 0;          // can identifier
    uint16_t timestamp = 0;   // FlexCAN time when message arrived
    uint8_t idhit = 0; // filter that id came from
    struct {
        bool extended = 0; // identifier is extended (29-bit)
        bool remote = 0;  // remote transmission request packet type
        bool overrun = 0; // message overrun
        bool reserved = 0;
    } flags;
    uint8_t len = 8;      // length of data
    uint8_t buf[8] = { 0 };       // data
    int8_t mb = 0;       // used to identify mailbox reception
    uint8_t bus = 0;      // used to identify where the message came from when events() is used.
    bool seq = 0;         // sequential frames
} CAN_message_t;

class GauderCAN {
    int w1 = 0;
    int w2 = 0;
public:
    boolean read(CAN_message_t msg) {
        return false;
    };

    void setValuesToCAN(int bytes, char index1, char index2, char subIndex, int inputValue, uint32_t id);
    void setMotorMode(int mode, uint32_t id);
    void standByMode(int input, uint32_t id) {};
    void setupAndStart(int);
    void startPositionMode(int positionValue, uint32_t id);
    void startVelocityMode(int speedValue, uint32_t id);
    int positionStatus(uint32_t id) {
        switch (id) {
        case 0x601: return w1; break;
        case 0x602: return w2; break;
        }
    };
    int motorPosition();
    void readDataFromL();
    void startHomingMode(int homingType, uint32_t id);
    void setHomingType(int homingType, uint32_t id);
    void startUpZeroPosition(uint32_t id) {
        switch (id) {
        case 0x601:  w1 = 0; break;
        case 0x602:  w2 = 0; break;
        }
    };
    void interpolatedPositionCLSetup(uint32_t id) {};
    void setInterPosition(uint32_t id, uint32_t pos) {
        switch (id) {
        case 0x601:  w1 = pos; break;
        case 0x602:  w2 = pos; break;
        }
    };

};
#endif
#pragma once
