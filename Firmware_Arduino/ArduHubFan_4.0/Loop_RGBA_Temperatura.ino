//*****************************************************************************************************************************//
//                                           Ver: X.09 Firmware data 00/00/25                                                  //
//*****************************************************************************************************************************//
byte TempLED = 128;

void RGB_Temperatura() {

  if (millis() >= (ResetTimerVirtuale[6] + DelayVirtuale[6])) {
    ResetTimerVirtuale[6] = millis();
    // Inizializzazione incremento
    byte increment = 0;

    // Logica per TempDS e TempLED combinata
    // MIN
    if (TempDS <= 24 && TempLED < 128) increment = 1;
    // MAX
    else if (TempDS > 33 && TempLED > 0) increment = -1;
    // INTEMEDIO
    else if (TempDS > 24 && TempDS <= 26) {
      if (TempLED < 96) increment = 1;
      else if (TempLED > 96) increment = -1;
    }
    // INTEMEDIO
    else if (TempDS > 26 && TempDS <= 30) {
      if (TempLED < 34) increment = 1;
      else if (TempLED > 34) increment = -1;
    }
    // INTEMEDIO
    else if (TempDS > 30 && TempDS <= 33) {
      if (TempLED < 24) increment = 1;
      else if (TempLED > 24) increment = -1;
    }

    // Aggiorna TempLED
    TempLED += increment;

    //                              Colore,     Saturazione, Intensità
    ParallelStripLED(map(TempLED, 0, 255, 0, 65536), 255, 255);
  }
}
