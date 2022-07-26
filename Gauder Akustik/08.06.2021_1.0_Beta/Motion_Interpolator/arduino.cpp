/*
* Datei zu Simulation eines Arduino mit den wichtigsten beötigten Befehlen
* und implementieren einer Pipe zur Gauder App
* Prof. Dr.-Ing Klaus Dieter Rupp
* 19.04.2021
*/

#include <iostream>
#include <iomanip>
#include <fstream>
#include <windows.h>
//#include <winsock2.h>
#include <iostream>
#include <sstream>
#include "sClient.h"
#include "arduino.h"
#include <conio.h>

HANDLE pipeHandle;
SOCKET socketHandle;  // Socket to Serial
SOCKET socketHandle1; // Socket to Serial 1

char socketbuffer[DEFAULT_BUFLEN];
char readbuffer[DEFAULT_BUFLEN];
int readpos = 0;
int bufflen = 0;

std::ofstream DataFile("data.txt");
int handlePipe(const char str[]);
int handleSocket(const char str[]);


void digitalWrite(int port, int state) {};

bool SdFat::exists(char* fileName) {
	std::ifstream file(fileName);
	if (!file)            // If the file was not found, then file is 0, i.e. !file=1 or true.
		return false;    // The file was not found.
	else                 // If the file was found, then file is non-0.
		return true;     // The file was found.
};


void SerialLib::close()
{
	DataFile.close();
}

void SerialLib::print(const char str[]){
	std::cout << str;
	DataFile << str;
	if (pipeHandle != 0)
		handlePipe(str);
	if (socketHandle > 1)
		handleSocket(str);
	
}
void SerialLib::print(int value, char mode) {
	std::stringstream ss;
	switch (mode) {
	case 'h':
		//std::cout << "0x" << std::hex << value;
		//DataFile << "0x" << std::hex << value;
		//ss << "0x" << std::hex << value;
		std::cout  << std::hex << value;
		DataFile  <<  std::hex << value;
		ss <<  std::hex << value;
		break;
		default:
			std::cout  << value;
			DataFile << value;
			ss << value;
			break;
	}
	if (pipeHandle != 0)
		handlePipe(ss.str().c_str());
	if (socketHandle > 1)
		handleSocket(ss.str().c_str());
}

void SerialLib::print(float f, int n=2) {
	std::cout << std::setprecision(n) <<f;
	DataFile << std::setprecision(n) << f;
	std::stringstream ss;
	ss << f;
	if (pipeHandle != 0)
		handlePipe(ss.str().c_str());
	if (socketHandle > 1)
		handleSocket(ss.str().c_str());
}

void SerialLib::println(int n) {
	std::cout << n << std::endl;
	DataFile << n << std::endl;
	std::stringstream ss;
	ss << n << std::endl;
	if (pipeHandle != 0)
		handlePipe(ss.str().c_str());
	if (socketHandle > 1)
		handleSocket(ss.str().c_str());
}

void SerialLib::println(float f, int n=2) {
	std::cout << std::setprecision(n) << f << std::endl;
	DataFile << std::setprecision(n) << f << std::endl;
	std::stringstream ss;
	ss << f << std::endl;
	if (pipeHandle != 0)
		handlePipe(ss.str().c_str());
	if (socketHandle > 1)
		handleSocket(ss.str().c_str());
}

void SerialLib::println(std::wstring str) {
	std::wcout << str << std::endl;
	DataFile << str.c_str() << std::endl;
	std::stringstream ss;
	ss << str.c_str() << std::endl;
	if (pipeHandle != 0)
		handlePipe(ss.str().c_str());
	if (socketHandle > 1)
		handleSocket(ss.str().c_str());
}


void SerialLib::println(const char str[]) {
	std::cout << str << std::endl;
	DataFile << str << std::endl;
	std::stringstream ss;
	ss << str << std::endl;
	if (pipeHandle != 0)
		handlePipe(ss.str().c_str());
	if (socketHandle > 1)
		handleSocket(ss.str().c_str());
}

//CloseHandle(handlePipe);

void SerialLib::begin(int baudRate) {
	// create file
	pipeHandle = CreateFile(
		TEXT("\\\\.\\pipe\\DARC1000Pipe"),   // pipe name 
		GENERIC_READ | GENERIC_WRITE,// read and write access 
		FILE_SHARE_WRITE,              // no sharing 
		NULL,           // default security attributes
		OPEN_EXISTING,  // opens existing pipe 
		0,              // default attributes 
		NULL);          // no template file 

	if (pipeHandle == INVALID_HANDLE_VALUE)
		std::cout << "Invalid Pipe\n";
	//unsigned long newMode = PIPE_READMODE_MESSAGE;
	//SetNamedPipeHandleState(pipeHandle, &newMode, 0, 0);
	socketHandle= StartClient();
}

int SerialLib::setHost(const char  name[], const char  port[]) {
	servername = name; 
	portnumber = port;
	return 0;
}

char SerialLib::read() {
	if (readpos == 0) {
		bufflen = readSocket(socketHandle, socketbuffer);
		strncpy_s(readbuffer, socketbuffer, bufflen);
		readpos = bufflen;
	}
	return readbuffer[bufflen- readpos--];
	
	
};
int SerialLib::available() {
	return readpos + availableSocket(socketHandle);
};


int SerialLib::available_k() {
	if (_kbhit()) // Nur wenn auch eine Taste gedrückt ist
	{
		int n =std::cin.rdbuf()->in_avail();
		return n;
		//c = std::cin.peek(); // Muss auf keine Eingabe warten, Taste ist bereits gedrückt
	}
	else
		return 0;
};

void SerialLib::reset() {
	if(socketHandle<=1)
	  socketHandle = StartClient();
};

void ReadString(char* output) {
	ULONG read = 0;
	int index = 0;
	do {
		ReadFile(pipeHandle, output + index++, 1, &read, NULL);
		if (*(output + index - 1) == '\r')
			break;
		if (*(output + index - 1) == '\n')
			break;

	} while (read > 0 && *(output + index - 1) != 0);
}




int handleSocket(const char str[]) {
	int iResult=0;
	if ((iResult = writeSocket(socketHandle, str)) == SOCKET_ERROR) {
		closeSocket(socketHandle);
		socketHandle =0;
	}

	return iResult;
};


int handlePipe(const char str[])
{
	if (pipeHandle == INVALID_HANDLE_VALUE)
		return 0;
	// create file
	PCWSTR name = TEXT("\\\\.\\pipe\\DARC1000Pipe");
	 if (pipeHandle == INVALID_HANDLE_VALUE || pipeHandle == 0) {
		// if (WaitNamedPipeW((PCWSTR)name, NMPWAIT_USE_DEFAULT_WAIT))
		     pipeHandle = CreateFileW((PCWSTR)name, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_WRITE, NULL, OPEN_EXISTING, 0, NULL);
//		if (pipeHandle == INVALID_HANDLE_VALUE)
//			std::cout << "Invalid Pipe\n";
	}
	// read from pipe server
	char* buffer = new char[256];
	memset(buffer, 0, 256);
	unsigned long cbRead;
//	ReadString(buffer);
//	bool fSuccess = ReadFile(
//		pipeHandle,    // pipe handle 
//		buffer,    // buffer to receive reply 
//		256,  // size of buffer 
//		&cbRead,  // number of bytes read 
//		NULL);    // not overlapped 

//	std::cout << "read from pipe server: " << buffer << "\r\n";

	// send data to server
//	const char* msg = "c++\r\n\0";
	int r = strcpy_s(buffer, 256, str);
	int len = strlen(buffer);
//	WriteFile(fileHandle, msg, strlen(msg), nullptr, NULL);
	unsigned long dataWritten;
	//static OVERLAPPED overlapped = { }; // Kommt mir komisch vor, aber nur so geht es
	if (pipeHandle != INVALID_HANDLE_VALUE)
	 WriteFile(pipeHandle, str, len, &dataWritten, NULL);
//	WriteFile(pipeHandle, msg, sizeof(msg), &dataWritten, NULL);

	return 0;
}



