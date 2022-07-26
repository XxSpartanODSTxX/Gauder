#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>


// Need to link with Ws2_32.lib, Mswsock.lib, and Advapi32.lib
#pragma comment (lib, "Ws2_32.lib")
#pragma comment (lib, "Mswsock.lib")
#pragma comment (lib, "AdvApi32.lib")


#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "27015"

unsigned long bytes_available = 0;;
long nAnswers = 0; 

SOCKET ConnectSocket = INVALID_SOCKET;


int StartClient() {
    WSADATA wsaData;
  
    struct addrinfo* result = NULL,
        * ptr = NULL,
        hints;

    int iResult;
    int recvbuflen = DEFAULT_BUFLEN;
    const char* servername = "localhost";
    printf("Startup Socket on  %s : %s\n", servername, DEFAULT_PORT);

    // Initialize Winsock
    iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
    if (iResult != 0) {
        printf("WSAStartup failed with error: %d\n", iResult);
        return 1;
    }

    ZeroMemory(&hints, sizeof(hints));
    hints.ai_family = AF_UNSPEC;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_protocol = IPPROTO_TCP;

    // Resolve the server address and port
    iResult = getaddrinfo(servername, DEFAULT_PORT, &hints, &result);
    if (iResult != 0) {
        printf("getaddrinfo failed with error: %d\n", iResult);
        WSACleanup();
        return 1;
    }
    // Attempt to connect to an address until one succeeds
    for (ptr = result; ptr != NULL; ptr = ptr->ai_next) {

        // Create a SOCKET for connecting to server
        ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype,
            ptr->ai_protocol);
        if (ConnectSocket == INVALID_SOCKET) {
            printf("socket failed with error: %ld\n", WSAGetLastError());
            WSACleanup();
            return 1;
        }

        // Connect to server.
        iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
        if (iResult == SOCKET_ERROR) {
            closesocket(ConnectSocket);
            ConnectSocket = INVALID_SOCKET;
            continue;
        }
        break;
    }

    freeaddrinfo(result);

    if (ConnectSocket == INVALID_SOCKET) {
        printf("Unable to connect to server!\n");
        WSACleanup();
        return 1;
    }

    printf("Connection establisched\n");
};

int CloseSocket(SOCKET ConnectSocket)
{
      // shutdown the connection since no more data will be sent
    int iResult; 
    iResult = shutdown(ConnectSocket, SD_SEND);
       if (iResult == SOCKET_ERROR) {
           printf("shutdown failed with error: %d\n", WSAGetLastError());
           closesocket(ConnectSocket);
           WSACleanup();
           return 1;
       }
       

    // cleanup
    closesocket(ConnectSocket);
    WSACleanup();
}

int  write(SOCKET ConnectSocket, const char* sendText) {
    char sendbuf[DEFAULT_BUFLEN];
    int iResult;
    sprintf_s(sendbuf, "%s\n", sendText);
//    sprintf_s(sendbuf, "%s<EOF>\n", sendText);
    iResult = send(ConnectSocket, sendbuf, (int)strlen(sendbuf), 0);
    if (iResult == SOCKET_ERROR) {
        printf("send failed with error: %d\n", WSAGetLastError());
        closesocket(ConnectSocket);
        WSACleanup();
        return iResult;
    }
    return iResult;
}

int read(SOCKET ConnectSocket, char* buffer)
{
    int iResult=0;
    char recvbuf[DEFAULT_BUFLEN];
    int recvbuflen = DEFAULT_BUFLEN;
    ioctlsocket(ConnectSocket, FIONREAD, &bytes_available);
    if (bytes_available > 0) {
        iResult = recv(ConnectSocket, recvbuf, recvbuflen, 0);
        if (iResult > 0) {
            recvbuf[iResult] = '\0';
            printf("Bytes received( %d, %d ): %s \n", iResult, bytes_available, recvbuf);
            recvbuf[iResult] = '\0';
            strcpy_s(buffer, DEFAULT_BUFLEN, recvbuf);
        }
        else if (iResult == 0)
            printf("Connection closed\n");
        else
            printf("recv failed with error: %d\n", WSAGetLastError());
    }
    return iResult;
}

int __cdecl main(int argc, char** argv)
{
  //  WSADATA wsaData;
 //   SOCKET ConnectSocket = INVALID_SOCKET;
 //   struct addrinfo* result = NULL,
 //       * ptr = NULL,
 //       hints;
    const char* sendText = "this is a test!";
 //   char sendbuf[DEFAULT_BUFLEN];

  //  char recvbuf[DEFAULT_BUFLEN];
    int iResult;
  //  int recvbuflen = DEFAULT_BUFLEN;
//    const char* servername = "localhost";

    StartClient();
    /*
    // Validate the parameters
    if (argc != 2) {
        printf("usage: %s server-name\n", argv[0]);
      //  return 1;
    }

    // Initialize Winsock
    iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
    if (iResult != 0) {
        printf("WSAStartup failed with error: %d\n", iResult);
        return 1;
    }
    */
    /*
    ZeroMemory(&hints, sizeof(hints));
    hints.ai_family = AF_UNSPEC;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_protocol = IPPROTO_TCP;

    // Resolve the server address and port
    iResult = getaddrinfo(servername, DEFAULT_PORT, &hints, &result);
    if (iResult != 0) {
        printf("getaddrinfo failed with error: %d\n", iResult);
        WSACleanup();
        return 1;
    }

    // Attempt to connect to an address until one succeeds
    for (ptr = result; ptr != NULL; ptr = ptr->ai_next) {

        // Create a SOCKET for connecting to server
        ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype,
            ptr->ai_protocol);
        if (ConnectSocket == INVALID_SOCKET) {
            printf("socket failed with error: %ld\n", WSAGetLastError());
            WSACleanup();
            return 1;
        }

        // Connect to server.
        iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
        if (iResult == SOCKET_ERROR) {
            closesocket(ConnectSocket);
            ConnectSocket = INVALID_SOCKET;
            continue;
        }
        break;
    }

    freeaddrinfo(result);

    if (ConnectSocket == INVALID_SOCKET) {
        printf("Unable to connect to server!\n");
        WSACleanup();
        return 1;
    }
    */


    for (long nindex = 0; nindex < 2000000000; nindex++) {
        // Send an initial buffer
  
        if (nAnswers > 0) {
            nAnswers--;
            iResult = write( ConnectSocket, sendText);
            /*            sprintf_s(sendbuf, "%s %d %d<EOF>\n", sendText, nindex,nAnswers);
            iResult = send(ConnectSocket, sendbuf, (int)strlen(sendbuf), 0);
            if (iResult == SOCKET_ERROR) {
                printf("send failed with error: %d\n", WSAGetLastError());
                closesocket(ConnectSocket);
                WSACleanup();
                return 1;
            }
            */

            printf("Bytes Sent: %ld\n", iResult);
            if (iResult < 0)
                break;
        }
     //   mySleep(0, 1000);

       

        // Receive until the peer closes the connection
      //  do {
        char buffer[DEFAULT_BUFLEN];
        iResult = read(ConnectSocket, buffer);
        if (iResult > 0)
            nAnswers = 5;
        /*
        ioctlsocket(ConnectSocket, FIONREAD, &bytes_available);
        if (bytes_available > 0) {
            iResult = recv(ConnectSocket, recvbuf, recvbuflen, 0);
            if (iResult > 0) {
                recvbuf[iResult] = '\0';
                printf("Bytes received( %d, %d ): %s \n", iResult, bytes_available, recvbuf);
                nAnswers = bytes_available;
            }
            else if (iResult == 0)
                printf("Connection closed\n");
            else
                printf("recv failed with error: %d\n", WSAGetLastError());
        }
        */
       // } while (iResult > 0);
        Sleep(100);

    }
 
    CloseSocket(ConnectSocket);
 
    return 0;
}