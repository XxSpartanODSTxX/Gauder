/**
 * Sensor1 Driver
 *
 * Arduino Library
 * for Sensor1 Magnet Rotary Sensor
 *
 * License: BSD 3-Clause
 *
 * Luis Martinez,
 * 02/2021, v0.0.3
 */

// include these dependencies in your top-level .ino!


// prevent redefinitions


#ifndef Sensor2_driver
#define Sensor2_driver

class Sensor2
{
public:
    /* :: Configuration :: */

    // 7-bit device address
    unsigned char address = 0x36;

    // 8-bit register addresses
    static struct ByteRegister
    {
        static const unsigned char
            ZMCO = 0x00,
            ABN = 0x09,
            PUSHTHR = 0x0A,
            STATUS = 0x0B,
            AGC = 0x1A,
            BURN = 0xFF;
    } ByteRegister;

    // 16-bit register start (high byte) addresses
    static struct WordRegister
    {
        static const unsigned char
            ZPOS = 0x01,
            CONF = 0x03,
            RAWANGLE = 0x0C,
            ANGLE = 0x0E,
            MAGNITUDE = 0x1B;
    } WordRegister;

    /* :: Low-Level Access :: */

    // low-level: read one byte from an 8-bit register
    unsigned char readRaw8(unsigned char registerAddress)
    {
        return 0;
    }

    // low-level: read two bytes as 16-bit word from two 8-bit registers
    unsigned int readRaw16(unsigned char registerStartAddress)
    {
        // get high byte, then low byte
        unsigned char highByte = this->readRaw8(registerStartAddress);
        unsigned char lowByte = this->readRaw8(registerStartAddress + 1);

        // combine to 16-bit word
      // return word(highByte, lowByte);
        return 0;
    }

    // low-level: write one byte to an 8-bit register
    void writeRaw8(unsigned char registerAddress, unsigned char value)
    {
      
    }

    // low-level: write 16-bit word as two bytes to two 8-bit registers
    void writeRaw16(unsigned char registerStartAddress, unsigned int value)
    {
      
    }

    /* :: Higher-Level Methods :: */

    // query status to find out if magnet is detected
    bool magnetDetected()
    {
        // query status register
        unsigned char status = this->readRaw8(Sensor1::ByteRegister::STATUS);

        // return true if bit 5 is set
     //   return bitRead(status, 5) == 1 ? true : false;
        return true;
    }


    // get current magnetic magnitude (12 bit)
    unsigned int getMagnitude()
    {
        // read and return two-byte magnitude
        return this->readRaw16(Sensor1::WordRegister::MAGNITUDE);
    }

    // get current gain of AGC (8 bit)
    unsigned char getGain()
    {
        // read and return one-byte gain
        return this->readRaw8(Sensor1::ByteRegister::AGC);
    }

    // get raw angle (12 bit)
    unsigned int getRawAngle()
    {
        // read and return two-byte raw angle
        return this->readRaw16(Sensor1::WordRegister::RAWANGLE);
    }

    void burnZeroPosition()
    {
      this->writeRaw8(Sensor1::ByteRegister::BURN,0x80);
    }

    void burnSetup()
    {
      this->writeRaw8(Sensor1::ByteRegister::BURN,0x40);
    }
     
     unsigned int readZMCO()
    {
      return this->readRaw8(Sensor1::ByteRegister::ZMCO);
    }
    
    // set zero-position to specified raw angle (12 bit)
    void setZeroPosition(unsigned int rawAngle)
    {
        // send position setting command
        //if its necesary to change the main point you can make rawAngle-1000 for example and it will be the 1000 the current value
        this->writeRaw16(Sensor1::WordRegister::ZPOS, rawAngle);
    }

    // convenience method: read current raw angle and pass it to .setZeroPosition(rawAngle)
    void setZeroPosition() { this->setZeroPosition(this->getRawAngle()); }

    // set angle resolution (affecting output value range and update speed)
    void setResolution(unsigned int angleSteps)
    {
        char power = -1;

        // coerce angle steps to supported values (8, 16, 32, …, 2048)
      //  angleSteps = min(max(angleSteps, 8), 2048);
        angleSteps = 1;
        // find dual logarithm (2^power >= angleSteps)
        // (by comparing increasing powers of two with angleSteps)
        while ((1 << ++power) < angleSteps)
            ;

        // send ABN setting command (-3 (2^3 = 8) shifts the powers 3…11 (for 8…2048) to 0…8)
        this->writeRaw8(Sensor1::ByteRegister::ABN, power - 3);
    }

    // get zero-adjusted and filtered angle (12 bit)
    unsigned int getAngle()
    {
        // read and return two-byte clean angle
        return this->readRaw16(Sensor1::WordRegister::ANGLE);
    }
};
#endif
