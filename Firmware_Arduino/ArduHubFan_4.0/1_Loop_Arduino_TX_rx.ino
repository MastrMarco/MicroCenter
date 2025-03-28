//*****************************************************************************************************************************//
//                                           Ver: X.00 Firmware data 00/00/25                                                  //
//*****************************************************************************************************************************//

void loop() {
  //----------------------------------------------------------------------

  //digitalWrite(SL, LOW);
  PORTD &= !B00000000;

  //digitalWrite(CK, LOW);
  PORTD &= !B00000000;

  delayMicroseconds(5);

  //digitalWrite(CK, HIGH);
  PORTD |= B00100000;

  //digitalWrite(SL, HIGH);
  PORTD |= B10000000;

  Dati74166 = shiftIn(QH, CK, MSBFIRST);

  PinA = Dati74166 & 0B00000001;
  PinB = Dati74166 & 0B00000010;
  PinC = Dati74166 & 0B00000100;
  PinD = Dati74166 & 0B00001000;

  //----------------------------------------------------------------------
  /*  millis();  //Avvia il Delay non bloccante: 
  * Seriale RX 1, Animazione Avvio HUB, 
  * Alimentazione, l'auto reset dello stato delle protezioni, 
  * Temperatura, RPM ventole, RGB M1 RainBow, RGB M2 Temperatura, 
  * RGB M3 Transiozione, RGB M4 Musica, RGB M5 Disocontinuo,
  */
  //----------------------------------------------------------------------
  Void_Fan_Mod();
  if (Aniamzione_Avvio == true or Debug == 1) {
    TimerVirtuale = millis();
    //Richiama il loop per la configurazione delle periferiche, Richiama il loop dove ci sono tutte le modalità LED
    (ROM_Dati == 3 && ModLED_Fan > 0) ? Set_LED_Config() : Void_LED_Mod();
  }
  TemperaturaDS();  //Lettura srnsore Temperatura
  Voltaggio();      //Lettura Tensione 5V 12V


  //---------------------------------------------------------------------- AnimazioneAVVIO
  if (Aniamzione_Avvio == false and (V5 > 4.50 or Debug != 0)) {

    for (byte x = 0; x <= NUM_LEDS_ALL[9]; x++) {
      // SingleStripLED(x, (8 * 128), 255, 255); //RossoArancio
      // SingleStripLED(x, (171 * 128), 220, 255); //VerdeChiaro
      SingleStripLED(x, (200 * 128), 255, 255);  //Azzurro
      Set_LED();
      delay(DelayVirtuale[10]);
    }
    Aniamzione_Avvio = true;
  }
  //---------------------------------------------------------------------- Funzione Delay per l'invio dei dati al PC tramite Seriale
  if (TimerVirtuale >= (ResetTimerVirtuale[0] + DelayVirtuale[0])) {
    //----------------------
    //Invio dei dati al PC tramite Seriale
    if (Boot_SetUp != 100 or Debug == 1) {

      DelayVirtuale[0] = (Debug == 1) ? DelayLoopPrimario_ON : DelayLoopPrimario_OFF;

      String output;
      output.reserve(250);  // Evita frammentazione della memoria

      //                  (0)                   (1)     (2)       (3)              (4)
      output = String(Stato_Software) + ',' + Arduino + ",," + Progetto + ',' + Versione + ';';
      //                  (0)         (1)        (2)            (3)                     (4)                   (5)            (6)
      output += String(TempDS) + ',' + V5 + ',' + V12 + ',' + S_Pro_12V + ',' + PowerLimitLED_Stato + ',' + S_Pro_5V + ',' + VAREF + ';';
      //                  (0)             (1)               (2)
      output += String(ROM_Dati) + ',' + EN_OV + ',' + PowerLimitLED + ';';
      //     (0) (1) (2) (3) (4) (5) (6) (7) (8) (9)
      for (int i = 0; i < 10; i++) output += String(NUM_LEDS_OUT[i]) + (i < 9 ? "," : ";");
      //     (0) (1) (2) (3) (4) (5) (6) (7) (8) (9)
      for (int i = 0; i < 10; i++) output += String(LumLED[i]) + (i < 9 ? "," : ";");
      //     (0) (1) (2) (3) (4) (5) (6) (7) (8) (9)
      for (int i = 0; i < 10; i++) output += String(ColoreLED[i]) + (i < 9 ? "," : ";");
      //     (0) (1) (2) (3) (4) (5) (6) (7) (8) (9)
      for (int i = 0; i < 10; i++) output += String(Saturazione[i]) + (i < 9 ? "," : ";");
      //                  (0)                  (1)
      output += String(ModLED_Fan) + ',' + ModRGB_LED + ';';
      //     (0) (1) (2) (3) (4)
      for (int i = 0; i < 5; i++) output += String(FanSpeed[i]) + (i < 4 ? "," : ";");
      //     (0) (1) (2) (3) (4)
      for (int i = 0; i < 5; i++) output += String(Fan_Mod_Speed[i]) + (i < 4 ? "," : ";");
      //                  (0)              (1)               (2)              (3)
      output += String(RPM_Fan1) + ',' + RPM_Fan2 + ',' + RPM_Fan3 + ',' + RPM_Fan4 + ';';
      //     (0) (1) (2) (3) (4) (5) (6)
      for (int i = 0; i < 7; i++) output += String(Animation_RGBS[i]) + (i < 6 ? "," : ";");

      Serial.println(output);
    }
    if (Boot_SetUp == 100 and Debug != 1) {
      DelayVirtuale[0] = DelayLoopPrimario_ON;
      //   //                            (0)                (1)
      //   Serial.println((String)Stato_Software + "," + Progetto + ";"  //0 Informa il software //0 *
      //                  //  (0)          (1)        (2)            (3)                 (4)                       (5)            (6)
      //                  + TempDS + "," + V5 + "," + V12 + "," + S_Pro_12V + "," + PowerLimitLED_Stato + "," + S_Pro_5V + "," + VAREF + ";"  //1
      //                  //  (0)             (1)               (2)
      //                  + ROM_Dati + "," + EN_OV + "," + PowerLimitLED + ";"  //2 *
      //                  //    (0)
      //                  + NUM_LEDS_OUT[0] + ";"  //3 Nummero LED *
      //                  //   (0)
      //                  + LumLED[ModLED_Fan] + ";"  //4 Luminosità *
      //                  //    (0)
      //                  + ColoreLED[ModLED_Fan] + ";"  //5 Colore LED *
      //                  //    (0)
      //                  + Saturazione[ModLED_Fan] + ";"  //6 Saturazione LED *
      //                  //    (0)               (1)                  (2)
      //                  + ModLED_Fan + "," + ModFAN_SPEED + "," + ModRGB_LED + ";"  //7 Modalità Sincornizata / Desinc *
      //                  //     (0)
      //                  + FanSpeed[ModFAN_SPEED] + ";"  //8 Velocità Rotazione Ventole *
      //                  //     (0)
      //                  + Fan_Mod_Speed[ModFAN_SPEED] + ";"  //9 Modalita Adatamento velocità Ventola *
      //                  //   (0)             (1)              (2)              (3)
      //                  + RPM_Fan1 + "," + RPM_Fan2 + "," + RPM_Fan3 + "," + RPM_Fan4 + ";"  //10 Lettura RPM delle ventole
      //                  //    (0)
      //                  + Animation_RGBS[ModRGB_LED] + ";");  //11 Acquisizione Animeazione LED Select
      // }


      //                       (0)          (1)        (2)            (3)                 (4)                       (5)            (6)
      Serial.println((String)TempDS + "," + V5 + "," + V12 + "," + S_Pro_12V + "," + PowerLimitLED_Stato + "," + S_Pro_5V + "," + VAREF + ";"  //0
                     //   (0)             (1)              (2)              (3)
                     + RPM_Fan1 + "," + RPM_Fan2 + "," + RPM_Fan3 + "," + RPM_Fan4 + ";"  //1 Lettura RPM delle ventole
                     //           (0)
                     + Animation_RGBS[ModRGB_LED] + ";");  //2 Acquisizione Animeazione LED Select
    }
    ResetTimerVirtuale[0] = TimerVirtuale;
  }
  //----------------------------------------------------------------------Modalità d'attesa o standby
  //
  //if ((Volt_5V < 4.00) and (Volt_12V < 11.00) and (Boot_SetUp == 0) and ((Aniamzione_Avvio == true) or (Aniamzione_Avvio == false and Volt_12V > 11.50)) ) {
  if (Boot_SetUp == 0) {
    if ((V5 < 1.00) and (V12 < 1.00)) {
      Mod_attesa = true;
      //
      DelayFanPower = millis();
      LumLimitLED = 0;
      PowerLimitLED_Stato = false;
      //
      S_Pro_5V = false;   //0
      S_Pro_12V = false;  //0
      //
    } else {
      Mod_attesa = false;
    }
  }
  //
  //-------------------------------------Modalità Offline USB
  //
  // if ((millis() >= (ResetTimerVirtuale[1] + DelayVirtuale[1])) and (Serial.available() == false)) {
  if ((millis() >= (ResetTimerVirtuale[1] + 40)) and (Serial.available() == false)) {

    if (ROM_Dati == 3) {
      byte d = EEPROM.read(EEPROMaddress[0]);

      (d < 3) ? ROM_Dati = d : ROM_Dati = 0;

      // if (valLED >= 1 and valLED <= 30) {
      if (stDef == false) {
        Reset_LED();
        Set_LED();
        // NUM_LEDS_OUT[ModLED_Fan] = valLED;
        Set_LED_ROM();

        (ROM_Dati == 1) ? ModLED_Fan = EEPROM.read(EEPROMaddress[4]) : ModLED_Fan = 0;

        stDef = true;
      }
    }

    // Salva i dati Modificati nella EEPROM
    if (ROM_Dati == 1 and Boot_SetUp != 0) {
      Dati_EEPROM();
    }

    Boot_SetUp = 0;

    Set_LED();

    ResetTimerVirtuale[1] = millis();
  }
  //-------------------------------------Modalità Oline USB
  //Funzione ricezione dati dal PC (quando riceve dei dati tramite la Seriale avvia il loop DatiRX)
  if (Serial.available()) {

    ResetTimerVirtuale[1] = (millis() + DelayVirtuale[1]);  // USB Connessione ON

    String SerialRX = Serial.readStringUntil('\n');

    if (SerialRX.length() > 0) {
      int m = 0;
      int n = SerialRX.indexOf(";");
      int indice = 0;
      while (n > 0) {
        String token = SerialRX.substring(n, m);
        m = n + 1;
        n = SerialRX.indexOf(";", n + 1);

        DatiRXloop(indice, token);
        indice++;
      }
      //applico i risultati
      if (stDef == false and (ROM_Dati < 3 or ModLED_Fan != ElementoPRE)) {
        ElementoPRE = ModLED_Fan;
        Reset_LED();
        Set_LED();
        ArrayLED();  //
        Set_LED_ROM();
        stDef = true;
      }

      Set_LED();
    }
  }
  //-------------------------------------
}


// Applica i risultati ai LED
void Set_LED() {

  //if (ROM_Dati != 3) {
  //Void_LED_Mod();  //Richiama il loop dove ci sono tutte le modalità LED
  //}
  // if (Aniamzione_Avvio == true or Debug == 1) {
  //   if (ROM_Dati == 3 and ModLED_Fan > 0) {
  //     Set_LED_Config();  //Richiama il loop per la configurazione delle periferiche
  //   } else {
  //     Void_LED_Mod();  //Richiama il loop dove ci sono tutte le modalità LED
  //   }
  // }

  for (byte s = 0; s <= 8; s++) {
    if (Strip[s].getBrightness() != (BRIGHTNESS - LumLimitLED)) {
      Strip[s].setBrightness(BRIGHTNESS - LumLimitLED);
    }
    Strip[s].show();
  }
}