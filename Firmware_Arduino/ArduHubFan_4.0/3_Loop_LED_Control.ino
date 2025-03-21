//*****************************************************************************************************************************//
//                                           Ver: X.00 Firmware data 00/00/25                                                  //
//*****************************************************************************************************************************//

byte H_P = 128;  // H_P = 65536 / 512 = 128

//Imposta la luminosità tra Mod_Colore e MOD_Animazione
void LumLED_Set() {
  if (ROM_Dati < 3) {
    // if ((ColoreLED[ModLED_Fan] > 512) and (PowerLimitLED_Stato == false)) {
    if (ColoreLED[ModLED_Fan] > 512 and BRIGHTNESS != LumLED[ModLED_Fan]) {
      BRIGHTNESS = LumLED[ModLED_Fan];
    } else if (ColoreLED[ModLED_Fan] <= 512 and BRIGHTNESS != 255) {
      BRIGHTNESS = 255;
    }
  } else {
    BRIGHTNESS = 255;
  }
}


//Spegne Tutti i LED
void Reset_LED() {
  for (byte s = 0; s <= 8; s++) {
    Strip[s].clear();
  }
}

//Serve a creare una strisca LED Unica pre le varie Animnazioni
void ArrayLED() {
  //Reset_Array
  // memset(NUM_LEDS_ALL, 0, sizeof(NUM_LEDS_ALL));
  for (byte s = 1; s <= 9; s++) {
    NUM_LEDS_ALL[s] = 0;
  }

  for (byte s = 1; s <= 9; s++) {
    NUM_LEDS_ALL[s] = NUM_LEDS_ALL[s] + NUM_LEDS_OUT[s];
    if (s < 9) {
      NUM_LEDS_ALL[s + 1] = NUM_LEDS_ALL[s];
    }
  }
  // NUM_LEDS_ALL[0] = NUM_LEDS_OUT[0];  // Inizializza il primo valore
  // for (byte s = 1; s < 9; s++) {      // Usa "< 9" per non superare i limiti
  //   NUM_LEDS_ALL[s] = NUM_LEDS_ALL[s - 1] + NUM_LEDS_OUT[s];
  // }
}

//Avvio loop dove ci sono tutte le modalità LED
void Void_LED_Mod() {
  //----------------------------------------------------------------------
  if (S_Pro_5V) {
    Reset_LED();
    return;
  }

  LumLED_Set();

  if ((ModLED_Fan == 0) || (ModLED_Fan == 0 && Boot_SetUp == 0)) {

    if ((ColoreLED[0] <= 512) and (BRIGHTNESS == 255)) {
      //Imposta la modalità dei LED
      //                      Colore,  Saturazione,  Intensità
      ParallelStripLED((ColoreLED[0] * H_P), Saturazione[0], LumLED[0]);
    }

    if (BRIGHTNESS == LumLED[0]) {
      RGB_Animazioni(ColoreLED[0]);
      // switch (ColoreLED[0]) {
      //   case 600:
      //     RGB_Mod_Fan_All = 1;  //Modalità 1 = RGB Effetto Ciclo Discontinuo
      //     // (Animazione, Delay)
      //     RGB_Animazioni(2, 9);
      //     break;
      //   case 601:
      //     RGB_Mod_Fan_All = 2;  //Modalità 2 = RGB Effetto Transizione*
      //     RGB_Animazioni(0, 7);
      //     break;
      //   case 602:
      //     RGB_Mod_Fan_All = 3;  //Modalità 3 = RGB Effetto RainBow*
      //     RGB_Animazioni(1, 5);
      //     break;
      //   case 603:
      //     RGB_Mod_Fan_All = 4;  //Modalità 4 = Effetto Music*
      //     RGB_Musica();
      //     break;
      //   case 604:
      //     RGB_Mod_Fan_All = 5;  //Modalità 5 = Effetto Temperatura*
      //     RGB_Animazioni(4, 6);
      //     break;
      //   case 605:
      //     RGB_Mod_Fan_All = 6;  //Modalità 6 = Effetto Festività*
      //     RGB_Animazioni(5, 13);
      //     break;
      // }
    }
  }

  if (ModLED_Fan > 0 || ColoreLED[ModLED_Fan] <= 512) {
    RGB_Mod_Fan_All = 0;
  }

  if (((ModLED_Fan > 0) && (ColoreLED[ModLED_Fan] <= 512)) && (BRIGHTNESS == 255)) {
    //Colore per ogni singolo elemento
    //                                           Colore,             Saturazione,      Intensità
    for (byte s = 0; s < 9; s++) {
      Strip[s].fill(Strip[s].ColorHSV((ColoreLED[s + 1] * H_P), Saturazione[s + 1], LumLED[s + 1]));  //Modalità RGB HSV
    }
  }

  /******************************--------------------------------------------***********************************************/
}


void SingleStripLED(byte N, uint32_t H, byte S, byte L) {
  // if (N <= NUM_LEDS_ALL[1]) {
  //   Strip[0].setPixelColor(N, Strip[0].ColorHSV(H, S, L));

  // } else if (N <= NUM_LEDS_ALL[2]) {
  //   Strip[1].setPixelColor(N - (NUM_LEDS_ALL[1]), Strip[1].ColorHSV(H, S, L));

  // } else if (N <= NUM_LEDS_ALL[3]) {
  //   Strip[2].setPixelColor(N - (NUM_LEDS_ALL[2]), Strip[2].ColorHSV(H, S, L));

  // } else if (N <= NUM_LEDS_ALL[4]) {
  //   Strip[3].setPixelColor(N - (NUM_LEDS_ALL[3]), Strip[3].ColorHSV(H, S, L));

  // } else if (N <= NUM_LEDS_ALL[5]) {
  //   Strip[4].setPixelColor(N - (NUM_LEDS_ALL[4]), Strip[4].ColorHSV(H, S, L));

  // } else if (N <= NUM_LEDS_ALL[6]) {
  //   Strip[5].setPixelColor(N - (NUM_LEDS_ALL[5]), Strip[5].ColorHSV(H, S, L));

  // } else if (N <= NUM_LEDS_ALL[7]) {
  //   Strip[6].setPixelColor(N - (NUM_LEDS_ALL[6]), Strip[6].ColorHSV(H, S, L));

  // } else if (N <= NUM_LEDS_ALL[8]) {
  //   Strip[7].setPixelColor(N - (NUM_LEDS_ALL[7]), Strip[7].ColorHSV(H, S, L));

  // } else if (N <= NUM_LEDS_ALL[9]) {
  //   Strip[8].setPixelColor(N - (NUM_LEDS_ALL[8]), Strip[8].ColorHSV(H, S, L));
  // }

  for (byte i = 0; i < 9; i++) {
    if (N <= NUM_LEDS_ALL[i + 1]) {
      Strip[i].setPixelColor(N - NUM_LEDS_ALL[i], Strip[i].ColorHSV(H, S, L));
      break;  // Esce dal ciclo una volta trovato l'intervallo
    }
  }
}

void ParallelStripLED(uint32_t H, byte S, byte L) {

  for (byte s = 0; s <= 8; s++) {
    Strip[s].fill(Strip[s].ColorHSV(H, S, L));
  }
}