/*
 Name:		Controller.ino
 Created:	12/18/2022 3:08:06 PM
 Author:	Jakkapan
*/
#include "PINOUT.h"
#include "BUTTON.h"

BUTTON button_1(8); // RST

PINOUT mes(10,true);
PINOUT mesLED(7,true);
PINOUT imagePrepare(4, true); //
PINOUT alarm(2,true);

bool stringComplete = false; // whether the string is complete
String inputString = "";
bool state_1 = false;

void serialEvent() {
    while (Serial.available()) {
        // get the new byte:
        char inChar = (char)Serial.read();
        if (inChar == '#') {
            stringComplete = true;  // Set state complete to true
			inputString.trim();     // Remove space
            break;
        }
        if (inChar == '>' || inChar == '<' || inChar == '\n' || inChar == '\r' || inChar == '\t' || inChar == ' ' || inChar == '?') {
            continue;
        }
        inputString += inChar;
    }
}
// the setup function runs once when you press reset or power the board
void setup() {
    Serial.begin(115200);

    mes.off();
    mesLED.off();
    imagePrepare.on();
    alarm.off();
}

void loop() {
	// check the status of the button press.
	if (button_1.isPressed() && state_1) { // RST
        mes.off();
        mesLED.off();
        imagePrepare.on();
        alarm.off();
        state_1 = false;
        Serial.println("RST - PIN");
    }
	// check to see if the string is complete:
	if (stringComplete) { // If state complete is true
        if (inputString == "OK") {
            mes.on();
            mesLED.on();
            imagePrepare.off();
            alarm.off();
            state_1 = true;
        }
        else if (inputString == "NG")
        {
            state_1 = true;
            alarm.on();
        }
        Serial.println("Received");
        inputString = "";
        stringComplete = false;
    }
}
