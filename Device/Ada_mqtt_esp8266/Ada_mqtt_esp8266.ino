/***************************************************
  Adafruit MQTT Library ESP8266 Example

  Must use ESP8266 Arduino from:
    https://github.com/esp8266/Arduino

  Works great with Adafruit's Huzzah ESP board & Feather
  ----> https://www.adafruit.com/product/2471
  ----> https://www.adafruit.com/products/2821

  Adafruit invests time and resources providing this open source code,
  please support Adafruit and open-source hardware by purchasing
  products from Adafruit!

  Written by Tony DiCola for Adafruit Industries.
  MIT license, all text above must be included in any redistribution
 ****************************************************/
#include <ESP8266WiFi.h>
#include "Adafruit_MQTT.h"
#include "Adafruit_MQTT_Client.h"

/************************* WiFi Access Point *********************************/

#define WLAN_SSID       "PhucAnCaFe"
#define WLAN_PASS       "123456789"

/************************* Adafruit.io Setup *********************************/

#define AIO_SERVER      "45.117.80.39"
#define AIO_SERVERPORT  1883                   // use 8883 for SSL
#define AIO_USERNAME    ""
#define AIO_KEY         ""


#define DEVICEID        "DeviceTest1"
#define TOPIC_ACTION    "Home/" DEVICEID "/Action"

/************ Global State (you don't need to change this!) ******************/

// Create an ESP8266 WiFiClient class to connect to the MQTT server.
WiFiClient client;
// or... use WiFiFlientSecure for SSL

// Setup the MQTT client class by passing in the WiFi client and MQTT server and login details.
Adafruit_MQTT_Client mqtt(&client, AIO_SERVER, AIO_SERVERPORT, AIO_USERNAME, AIO_KEY);

/****************************** Feeds ***************************************/

// Notice MQTT paths for AIO follow the form: <username>/feeds/<feedname>
Adafruit_MQTT_Publish photocell = Adafruit_MQTT_Publish(&mqtt, "Home/" DEVICEID "/Periodic");

// Setup a feed called 'onoff' for subscribing to changes.
Adafruit_MQTT_Subscribe onoffbutton = Adafruit_MQTT_Subscribe(&mqtt, "Home/" DEVICEID "/Action");
Adafruit_MQTT_Subscribe line1 = Adafruit_MQTT_Subscribe(&mqtt, "Home/" DEVICEID "/Action/Line1");

/*************************** Sketch Code ************************************/

// Bug workaround for Arduino 1.6.6, it seems to need a function declaration
// for some reason (only affects ESP8266, likely an arduino-builder bug).
void MQTT_connect();

void setup() {
  Serial.begin(115200);
  delay(10);
  pinMode(2, OUTPUT);
  digitalWrite(2, 0);
  
  Serial.println(F("Adafruit MQTT demo"));

  // Connect to WiFi access point.
  Serial.println(); Serial.println();
  Serial.print("Connecting to ");
  Serial.println(WLAN_SSID);

  WiFi.begin(WLAN_SSID, WLAN_PASS);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println();

  Serial.println("WiFi connected");
  Serial.println("IP address: "); Serial.println(WiFi.localIP());

  // Setup MQTT subscription for onoff feed.
  mqtt.subscribe(&onoffbutton);
  mqtt.subscribe(&line1);
}

uint32_t x=0;

void loop() {
  // Ensure the connection to the MQTT server is alive (this will make the first
  // connection and automatically reconnect when disconnected).  See the MQTT_connect
  // function definition further below.
  MQTT_connect();

  // this is our 'wait for incoming subscription packets' busy subloop
  // try to spend your time here
  byte *listbyte;  
  
  Adafruit_MQTT_Subscribe *subscription;
  while ((subscription = mqtt.readSubscription(5000))) {
    if(subscription == &line1){
      //listbyte = (byte *)line1.lastread;
      Serial.println(F("DUNG!"));
      
      //if(listbyte[0] == 0x01)
      //{
      //  Serial.println(F("DUNG!"));
      //}
    }
  }

  // Now we can publish stuff!
  Serial.print(F("\nSending photocell val Publish message: 0x01, 0x0A, 0x02, 0x00, 0x03, 0x00 "));
  
  if (! photocell.publish("Publish message: 0x01")) {
    Serial.println(F("Failed"));
  } else {
    Serial.println(F("OK!"));
  }

  if(digitalRead(2) == 0){
    photocell.publish("Pin 2: OFF");
  }
  else{
    photocell.publish("Pin 2: ON");
  }

  if(digitalRead(4) == 0){
    photocell.publish("Pin 4: OFF");
  }
  else{
    photocell.publish("Pin 4: ON");
  }

  if(digitalRead(5) == 0){
    photocell.publish("Pin 5: OFF");
  }
  else{
    photocell.publish("Pin 5: ON");
  }

  if(digitalRead(12) == 0){
    photocell.publish("Pin 12: OFF");
  }
  else{
    photocell.publish("Pin 12: ON");
  }

  if(digitalRead(13) == 0){
    photocell.publish("Pin 13: OFF");
  }
  else{
    photocell.publish("Pin 13: ON");
  }

  if(digitalRead(14) == 0){
    photocell.publish("Pin 14: OFF");
  }
  else{
    photocell.publish("Pin 14: ON");
  }

  if(digitalRead(15) == 0){
    photocell.publish("Pin 15: OFF");
  }
  else{
    photocell.publish("Pin 15: ON");
  }

  if(digitalRead(16) == 0){
    photocell.publish("Pin 16: OFF");
  }
  else{
    photocell.publish("Pin 16: ON");
  }

  if(digitalRead(100) == 0){
    photocell.publish("Pin 100: OFF");
  }
  else{
    photocell.publish("Pin 100: ON");
  }
  
  // ping the server to keep the mqtt connection alive
  if(! mqtt.ping()) {
    mqtt.disconnect();
  }
}

// Function to connect and reconnect as necessary to the MQTT server.
// Should be called in the loop function and it will take care if connecting.
void MQTT_connect() {
  int8_t ret;

  // Stop if already connected.
  if (mqtt.connected()) {
    return;
  }

  Serial.print("Connecting to MQTT... ");

  uint8_t retries = 3;
  while ((ret = mqtt.connect()) != 0) { // connect will return 0 for connected
       Serial.println(mqtt.connectErrorString(ret));
       Serial.println("Retrying MQTT connection in 5 seconds...");
       mqtt.disconnect();
       delay(5000);  // wait 5 seconds
       retries--;
       if (retries == 0) {
         // basically die and wait for WDT to reset me
         while (1);
       }
  }
  Serial.println("MQTT Connected!");
}
