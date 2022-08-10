volatile bool flag = false;

void IRAM_ATTR onMagnetDetect() {
  flag = true;
}

void setup() {
  Serial.begin(115200);

  pinMode(D1, INPUT_PULLUP);
  pinMode(D2, OUTPUT);
  digitalWrite(D2, LOW);
  attachInterrupt(digitalPinToInterrupt(D1), onMagnetDetect, FALLING);

}

void loop() {
  if (flag) {
    flag = false;
    Serial.println(millis());
  }
  // put your main code here, to run repeatedly:

}
