#include <iostream>
#include <fstream>

//#include <stdlib.h>
//#include <stdio.h>

#include "arduino.h"

std::ofstream DataFile("data.txt");


void SerialLib::close()
{
	DataFile.close();
}

void SerialLib::print(const char str[]){
	std::cout << str;
	DataFile << str;

}
void SerialLib::print(int n) {
	std::cout << n;
	DataFile << n;
}
void SerialLib::print(float f) {
	std::cout << f;
	DataFile << f;
}

void SerialLib::println(const char str[]) {
	std::cout << str << std::endl;
	DataFile << str << std::endl;;
}

void SerialLib::begin(int baudRate) {
	

}

