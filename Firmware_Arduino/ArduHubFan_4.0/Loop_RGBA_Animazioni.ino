//*****************************************************************************************************************************//
//                                           Ver: X.00 Firmware data 00/00/25                                                  //
//*****************************************************************************************************************************//


byte H;
// byte S;
byte V;
uint32_t H_12;
int dato;

void RGB_Animazioni(byte index, int freqenza) {

  if (millis() >= (ResetTimerVirtuale[8] + DelayVirtuale[freqenza])) {
    ResetTimerVirtuale[8] = millis();


    switch (index) {

      case 2:
        //-------------------------------------------------------------------------- "Discontinuo":
        // la variabbile: "dato" viene usata come indice per colorare i LED
        // la variabbile: "H" viene usata per impostare il colore dei LED
        // if (dato <= NUM_LEDS_ALL[9]) dato++;
        // if (dato >= NUM_LEDS_ALL[9]) {
        //   dato = 0;
        //   H = random(0, 255);
        // }
        dato = (dato < NUM_LEDS_ALL[9]) ? dato + 1 : (H = random(0, 255), 0);
        //uint32_t H = map(H, 0, 255, 0, 65536);
        SingleStripLED(dato, (map(H, 0, 255, 0, 65536)), 255, 255);
        break;


      case 5:
        //-------------------------------------------------------------------------- "Mix":
        // la variabbile: "dato" viene usata come pulsazione luminosità per i LED
        // la variabbile: "H" viene usata per impostare il colore dei LED
        // la variabbile: "V" viene usata per impostare la luminosità dei LED
        // if (V == dato) {
        // dato = random(13, 255);
        // H = random(0, 14);
        // }

        // if (V != dato) V += (V < dato) ? 1 : -1;

        (V == dato) ? (dato = random(13, 255)), (H = random(0, 14)) : V += (V < dato) ? 1 : -1;

        //         Colore,  Saturazione, Luminosità
        ParallelStripLED(H, 255, V);
        break;


      case 1:
        //-------------------------------------------------------------------------- "RainBow":
        // la variabbile: "H_12" viene usata per impostare il colore dei LED
        H_12 = (H_12 == 327424) ? 0 : H_12 + 256;

        for (byte i = 0; i < NUM_LEDS_ALL[9]; i++) {
          uint32_t pixelHue = H_12 + (i * 65536L / NUM_LEDS_ALL[9]);
          SingleStripLED(i, pixelHue, 255, 255);
        }
        break;


      case 0:
        //-------------------------------------------------------------------------- "Transizione":
        // la variabbile: "dato" viene usata come incremento lineare colorazione per i LED
        // la variabbile: "H_12" viene usata per impostare il colore dei LED
        dato = (dato < 4024) ? dato + 1 : 0;
        H_12 = map(dato, 0, 4024, 0, 65536);
        //                  Colore,  Saturazione, Intensità
        ParallelStripLED(H_12, 255, 255);
        break;


      case 4:
        //-------------------------------------------------------------------------- "Temperatura":
        // la variabbile: "dato" viene usata come incremento lineare colorazione per i LED
        // Inizializzazione incremento
        byte increment = 0;

        // Logica per TempDS e TempLED combinata
        // MIN
        // if (TempDS <= 24 && dato < 128) increment = 1;
        // // MAX
        // else if (TempDS > 33 && dato > 0) increment = -1;
        // // INTEMEDIO
        // else if (TempDS > 24 && TempDS <= 26) {
        //   if (dato < 96) increment = 1;
        //   else if (dato > 96) increment = -1;
        // }
        // // INTEMEDIO
        // else if (TempDS > 26 && TempDS <= 30) {
        //   if (dato < 34) increment = 1;
        //   else if (dato > 34) increment = -1;
        // }
        // // INTEMEDIO
        // else if (TempDS > 30 && TempDS <= 33) {
        //   if (dato < 24) increment = 1;
        //   else if (dato > 24) increment = -1;
        // }

        if (TempDS <= 24 && dato < 128) increment = 1;
        else if (TempDS > 33 && dato > 0) increment = -1;
        else if (TempDS > 24 && TempDS <= 26) increment = (dato < 96) ? 1 : (dato > 96) ? -1 : 0;
        else if (TempDS > 26 && TempDS <= 30) increment = (dato < 34) ? 1 : (dato > 34) ? -1 : 0;
        else if (TempDS > 30 && TempDS <= 33) increment = (dato < 24) ? 1 : (dato > 24) ? -1 : 0;


        // Aggiorna TempLED
        dato += increment;
        //                              Colore,     Saturazione, Intensità
        ParallelStripLED(map(dato, 0, 255, 0, 65536), 255, 255);
        break;


    }  // Swich Case



  }  //Delay



}  // End Loop Void