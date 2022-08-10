/*
   WebSocketClient.ino

    Created on: 24.05.2015

*/

#include <Arduino.h>

#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>

#include <WebSocketsClient.h>

#include <Hash.h>
#include "EEPROM.h"

ESP8266WiFiMulti WiFiMulti;
WebSocketsClient webSocket;

#define USE_SERIAL Serial

volatile bool flag = false;

String channels[] = {
  "/BicycleA",
  "/BicycleB",
  "/BicycleC"
};

byte chn = 255;

volatile unsigned long intervals[6] = {10000, 10000, 10000, 10000, 10000, 10000};
int curs = 0;

void webSocketEvent(WStype_t type, uint8_t * payload, size_t length) {

  switch (type) {
    case WStype_DISCONNECTED:
      USE_SERIAL.printf("[WSc] Disconnected!\n");
      break;
    case WStype_CONNECTED: {
        USE_SERIAL.printf("[WSc] Connected to url: %s\n", payload);

        // send message to server when Connected
        webSocket.sendTXT("Connected");
      }
      break;
    case WStype_TEXT:
      USE_SERIAL.printf("[WSc] get text: %s\n", payload);

      // send message to server
      // webSocket.sendTXT("message here");
      break;
    case WStype_BIN:
      USE_SERIAL.printf("[WSc] get binary length: %u\n", length);
      hexdump(payload, length);

      // send data to server
      // webSocket.sendBIN(payload, length);
      break;
    case WStype_PING:
      // pong will be send automatically
      USE_SERIAL.printf("[WSc] get ping\n");
      break;
    case WStype_PONG:
      // answer to a ping we send
      USE_SERIAL.printf("[WSc] get pong\n");
      break;
  }

}


void IRAM_ATTR onMagnetDetect() {
  static unsigned long lastDetect = 0;
  unsigned long now = millis();
  unsigned long diff = now - lastDetect;
  if (diff > 10) {
    intervals[curs] = diff > 10000 ? 10000 : diff;
    curs++;
    if (curs >= 6) {
      curs = 0;
    }
    flag = true;
  }
  lastDetect = now;
}

void setup() {
  Serial.begin(115200);
  
  USE_SERIAL.setDebugOutput(true);

  USE_SERIAL.println();
  USE_SERIAL.println();
  USE_SERIAL.println();

  for (uint8_t t = 4; t > 0; t--) {
    USE_SERIAL.printf("[SETUP] BOOT WAIT %d...\n", t);
    USE_SERIAL.flush();
    delay(1000);
  }
  
  EEPROM.begin(1);
  //EEPROM.put(0, 0);
  //EEPROM.commit();

  Serial.print("Channel: ");
  EEPROM.get(0, chn);
  Serial.println(channels[chn]);
  EEPROM.end();


  // USE_SERIAL.begin(921600);

  //Serial.setDebugOutput(true);


  WiFiMulti.addAP("bringamapping", "zymzymstudio");
  WiFiMulti.addAP("Mokustorony24", "seGGembt1982");
  //WiFiMulti.addAP("UPC2DFAA71", "wyQs3nnjmhef");

  //WiFi.disconnect();
  while (WiFiMulti.run() != WL_CONNECTED) {
    delay(100);
  }

  // server address, port and URL
  webSocket.begin("192.168.0.100", 81, channels[chn]);

  // event handler
  webSocket.onEvent(webSocketEvent);

  // use HTTP Basic Authorization this is optional remove if not needed
  //webSocket.setAuthorization("user", "Password");

  // try ever 5000 again if connection has failed
  webSocket.setReconnectInterval(5000);

  // start heartbeat (optional)
  // ping server every 15000 ms
  // expect pong from server within 3000 ms
  // consider connection disconnected if pong is not received 2 times
  //webSocket.enableHeartbeat(15000, 3000, 3);
  pinMode(D1, INPUT_PULLUP);
  pinMode(D2, OUTPUT);
  digitalWrite(D2, LOW);
  attachInterrupt(digitalPinToInterrupt(D1), onMagnetDetect, FALLING);

}

void loop() {
  static unsigned long lastSent = 0;
  static unsigned long sendInterval = 500;
  unsigned long now = millis();

  webSocket.loop();

  if (now - lastSent > sendInterval) {
    flag = false;
    if (webSocket.isConnected()) {
      Serial.println("Sending");
      unsigned long sum = 0;
      for (int i = 0; i < 6; i++) {
        sum += intervals[i];
        intervals[i] = intervals[i] > 10000 ? 10000 : intervals[i] * 1.1;
      }
      sum /= 6;
      String msg = String(sum);
      webSocket.sendTXT(msg);
    } else {
      Serial.println("Not connected");
    }
    lastSent = now;
  }
}
