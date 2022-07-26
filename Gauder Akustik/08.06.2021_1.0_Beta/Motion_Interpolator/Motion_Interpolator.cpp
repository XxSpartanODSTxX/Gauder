// Motion_Interpolator.cpp : Diese Datei enthält die Funktion "main". Hier beginnt und endet die Ausführung des Programms.
//

#include <iostream>
#include <fstream>
#include<windows.h>
#include <chrono>
#include <conio.h>
#include "arduino.h"


void delay(int milliseconds) {
    Sleep(milliseconds);

}


SerialLib Serial;
SerialLib Serial1;

#define _USE_MATH_DEFINES
#define F(text) (text)

#include ".\\Motion_Interpolator\\Motion_Interpolator.ino"
#include ".\\Motion_Interpolator\\properties.ino"
#include ".\\Motion_Interpolator\\commands.ino"
#include ".\\Motion_Interpolator\\serialPrint.ino"
#include ".\\Motion_Interpolator\\preConditions.ino"
#include ".\\Motion_Interpolator\\readFromSerialAndIsValid.ino"
#include ".\\Motion_Interpolator\\setupAndDriverVal.ino"
#include ".\\Motion_Interpolator\\testCode.ino"
#include ".\\Motion_Interpolator\\voidLoop.ino"


int main()
{
   

    std::cout << "Adruino Simulator Programm\n";
    setup();
    for (;;) {
        if (GetKeyState(VK_ESCAPE)<0)
            break;
        if (GetKeyState(VK_HOME) < 0)
            Serial.reset();
        loop();
    }
    std::cout << "Adruino Simulator finished!\n";
    Serial.close();
}

// Programm ausführen: STRG+F5 oder Menüeintrag "Debuggen" > "Starten ohne Debuggen starten"
// Programm debuggen: F5 oder "Debuggen" > Menü "Debuggen starten"

// Tipps für den Einstieg: 
//   1. Verwenden Sie das Projektmappen-Explorer-Fenster zum Hinzufügen/Verwalten von Dateien.
//   2. Verwenden Sie das Team Explorer-Fenster zum Herstellen einer Verbindung mit der Quellcodeverwaltung.
//   3. Verwenden Sie das Ausgabefenster, um die Buildausgabe und andere Nachrichten anzuzeigen.
//   4. Verwenden Sie das Fenster "Fehlerliste", um Fehler anzuzeigen.
//   5. Wechseln Sie zu "Projekt" > "Neues Element hinzufügen", um neue Codedateien zu erstellen, bzw. zu "Projekt" > "Vorhandenes Element hinzufügen", um dem Projekt vorhandene Codedateien hinzuzufügen.
//   6. Um dieses Projekt später erneut zu öffnen, wechseln Sie zu "Datei" > "Öffnen" > "Projekt", und wählen Sie die SLN-Datei aus.
