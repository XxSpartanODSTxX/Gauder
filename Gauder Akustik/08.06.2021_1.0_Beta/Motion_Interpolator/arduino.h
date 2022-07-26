/*
* Datei zu Simulation eines Arduino mit den wichtigsten beötigten Befehlen
* Prof. Dr.-Ing Klaus Dieter Rupp
* 19.04.2021
*/

#ifndef ArduinoSim_h
#define ArduinoSim_h




#define HEX 'h'
#define HIGH 1
#define LOW 0
void digitalWrite(int port, int state);


class SdFat {
public:
	bool begin(int mode) { return true; };
	bool exists(char* fileName);
};


class SerialLib {
public:
	const char* servername= "localhost"; // For Simulation with Socketcom
	const char* portnumber; // For Simulation with Socket com
	//void print(int n);
	void print(int value, char mode = 'd');
	void print(float f, int);

	void print(const char str[]);
	void println(std::wstring);

	void println(int n);

	void println(float f, int);

	void println(const char str[]);

	void begin(int baudRate);
	void reset();

	int available();
	int available_k();
	int setHost(const char  name[], const char  port[]);
	

char read_k() { char single; std::cin >> single;  return single; };
char read();
void close();

	
};
#endif
#pragma once
