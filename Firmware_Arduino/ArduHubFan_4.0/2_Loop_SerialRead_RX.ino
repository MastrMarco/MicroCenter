//*****************************************************************************************************************************//
//                                           Ver: X.00 Firmware data 00/00/25                                                  //
//*****************************************************************************************************************************//

//Avvio Sistema di lettura dati in arrivo dalla Seriale
void DatiRXloop(int i, String DatoRX) {
  int valore = DatoRX.toInt();  // Converte una volta sola

  //
  if (i == 0) {
    // ---------------------------- Boot di avvio / verifica connessione seriale
    if (valore == 100 && Debug != 1) {
      Boot_SetUp = valore;
    }
    return;
  }
  //
  if (Boot_SetUp != 100) return;
  if (DatoRX == "") return;



  switch (i) {
    case 1:
      // ---------------------------- Seleziona Elemento
      if (valore >= 0 && valore <= 9 && ModLED_Fan != valore) {
        PowerLimitLED_Stato = false;
        ModLED_Fan = valore;
      }
      break;

    case 2:
      // ---------------------------- Luminosità
      if (LumLED[ModLED_Fan] != valore) {
        LumLED[ModLED_Fan] = valore;
        TimerVAREF = millis();  // Ricalcola Tensione AREF
      }
      break;

    case 3:
      // ---------------------------- Colore
      if (valore < 700 && ColoreLED[ModLED_Fan] != valore) {
        PowerLimitLED_Stato = false;
        ColoreLED[ModLED_Fan] = valore;
        TimerVAREF = millis();  // Ricalcola Tensione AREF
      }
      break;

    case 4:
      // ---------------------------- Saturazione Bianco / Nero
      if (valore >= 0 && valore <= 255 && Saturazione[ModLED_Fan] != valore) {
        PowerLimitLED_Stato = false;
        Saturazione[ModLED_Fan] = valore;
      }
      break;

    case 5:
      // ---------------------------- Velocità Ventole
      if (ModLED_Fan < 5) {
        FanSpeed[ModLED_Fan] = valore;
        TimerVAREF = millis();  // Ricalcola Tensione AREF
      }
      break;

    case 9:
      // ---------------------------- Animazione RGB
      Animation_RGBS[0] = valore;
      break;

    case 10:
      // ---------------------------- Memorizzazione dati utente
      if (valore < 4) {
        EEPROM.update(EEPROMaddress[0], valore);
      }
      break;

    case 11:
      // ---------------------------- Protezione Alimentazione 5V - 12V
      if (valore < 3 && EN_OV != valore) {
        EN_OV = valore;
      }
      break;

    case 12:
      // ---------------------------- Protezione limite luminosità LED
      if (valore < 3 && PowerLimitLED != valore) {
        PowerLimitLED = valore;
      }
      break;

    case 13:
      // ---------------------------- Configurazione dispositivi connessi
      if (valore > 0 && valLED != valore) {
        valLED = valore;
      }
      break;

    default:
      break;
  }
}
