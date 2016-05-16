// This file is part of RosedaleEQ1.
//
// RosedaleEQ1 is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// RosedaleEQ1 is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with RosedaleEQ1. If not, see <http://www.gnu.org/licenses/>.
//
// Copyright © Guy Webb 2010

const int pinEast = 2;      // RA+   (red)
const int pinNorth = 3;     // DEC+  (green)
const int pinSouth = 4;     // DEC-  (yellow)
const int pinWest = 5;      // RA-   (blue)
const int MonE = 6;      // RA+   (red)
const int MonW = 7;      // RA-   (blue)
void setup()
{
  pinMode(pinEast, OUTPUT);
  pinMode(MonE, OUTPUT);
  pinMode(pinNorth, OUTPUT);
  pinMode(pinSouth, OUTPUT);
  pinMode(pinWest, OUTPUT);
  pinMode(MonW, OUTPUT);


  digitalWrite(pinEast, LOW);
  digitalWrite(MonE, LOW);
  digitalWrite(pinNorth, LOW);
  digitalWrite(pinSouth, LOW);
  digitalWrite(pinWest, LOW);
  digitalWrite(MonW, LOW);

  Serial.begin(9600);
}

void loop()
{      
  int inByte = 0;  
  if (Serial.available() > 0) {        
    inByte = Serial.read();
    switch(inByte) {
    case 'E': // Toggle RA+ movement
      while (Serial.available() < 1) {
        // nop
      }
      inByte = Serial.read();
      if (inByte == '1')
      {
        // Prevent both axis direction being active at once!
        digitalWrite(pinWest, LOW);
        digitalWrite(pinEast, HIGH);
        digitalWrite(MonW, LOW);
        digitalWrite(MonE, HIGH);
      }
      if (inByte == '0')
      {
        digitalWrite(pinEast, LOW);
        digitalWrite(MonE, LOW);
      }
      break;
    case 'N': // Toggle DEC+ movement
      while (Serial.available() < 1) {
        // nop
      }
      inByte = Serial.read();
      if (inByte == '1')
      {
        // Prevent both axis direction being active at once!
        digitalWrite(pinSouth, LOW);
        digitalWrite(pinNorth, HIGH);
      }
      if (inByte == '0')
        digitalWrite(pinNorth, LOW);
      break;
    case 'S': // Toggle DEC- movement
      while (Serial.available() < 1) {
        // nop
      }
      inByte = Serial.read();
      if (inByte == '1')
      {
        // Prevent both axis direction being active at once!
        digitalWrite(pinNorth, LOW);
        digitalWrite(pinSouth, HIGH);
      }
      if (inByte == '0')
        digitalWrite(pinSouth, LOW);
      break;
    case 'W': // Toggle RA- movement
      while (Serial.available() < 1) {
        // nop
      }
      inByte = Serial.read();
      if (inByte == '1')
      {
        // Prevent both axis direction being active at once!
        digitalWrite(pinEast, LOW);
        digitalWrite(pinWest, HIGH);
        digitalWrite(MonE, LOW);
        digitalWrite(MonW, HIGH);
      }
      if (inByte == '0')
      {
        digitalWrite(pinWest, LOW);
        digitalWrite(MonW, LOW);
      }
      break;      
    case 'R': // Reset all pins to low
      digitalWrite(pinEast, LOW);
      digitalWrite(pinWest, LOW);
      digitalWrite(pinNorth, LOW);
      digitalWrite(pinSouth, LOW);
      digitalWrite(MonE, LOW);
      digitalWrite(MonW, LOW);
      break;
    }
  }
}



