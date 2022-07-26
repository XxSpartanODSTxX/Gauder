#pragma once

#ifndef sClientLib_h
#define sClientLib_h

#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "27015"

SOCKET StartClient();
int readSocket(SOCKET ConnectSocket, char* buffer);
int writeSocket(SOCKET ConnectSocket, const char* buffer);
int availableSocket(SOCKET ConnectSocket);
int closeSocket(SOCKET ConnectSocket);
#endif
