

class SerialLib {
public :
	void print(int n);
	void print(float f);
	void print(const char str[]);

	void println(const char str[]);

	void begin(int baudRate);
	void close();
};

#pragma once
