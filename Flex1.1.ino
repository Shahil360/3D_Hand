int flexpin0 = A0;
int flexpin1 = A1;
int flexpin2 = A2;
int flexpin3 = A3;
int flexpin4 = A4;

int flexMin0 = 310;
int flexMax0 = 125;

int flexMin1 = 310;
int flexMax1 = 125;

int flexMin2 = 320;
int flexMax2 = 125;

int flexMin3= 310;
int flexMax3 = 125;

int flexMin4 = 300;
int flexMax4 = 125;


void setup() {
  // put your setup code here, to run once:
Serial.begin(9600);

}



void loop() {
  // put your main code here, to run repeatedly:
  int flexVal0;
  int flexVal1;
  int flexVal2;
  int flexVal3;
  int flexVal4;

  flexVal0 = analogRead(flexpin0);
  flexVal1= analogRead(flexpin1);
  flexVal2= analogRead(flexpin2);
  flexVal3= analogRead(flexpin3);
  flexVal4= analogRead(flexpin4);

  float angle0 = map(flexVal0, flexMin0, flexMax0, 0, 18);
  float angle1 = map(flexVal1, flexMin1, flexMax1, 0, 18);
  float angle2 = map(flexVal2, flexMin2, flexMax2, 0, 18);
  float angle3 = map(flexVal3, flexMin3, flexMax3, 0, 18);
  float angle4 = map(flexVal4, flexMin4, flexMax4, 0, 9);

  Serial.print(angle0);
  Serial.print(", ");
  Serial.print(angle1);
  Serial.print(", ");
  Serial.print(angle2);
  Serial.print(", ");
  Serial.print(angle3);
  Serial.print(", ");
  Serial.println(angle4);

  delay(20);

}
