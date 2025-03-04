//*****************************************************************************************************************************//
//                                           Ver: X.00 Firmware data 00/00/25                                                  //
//*****************************************************************************************************************************//

// AREF
byte CampNum_AREF = 0;
float VoltMedia_AREF = 0;

// Limiti di tensione
const float V12_Limit = 0.60;
const float V5_Limit = 0.25;
const float LED_Limt = 4.80;
const int StartLEDPowerProt = 500;

// Lettura Tensione Arduino 5V PIN [VCC] Riferimento Analogico di Tensione
float ReadVref() {
  ADMUX = _BV(REFS0) | _BV(MUX3) | _BV(MUX2) | _BV(MUX1);
  delayMicroseconds(350);
  ADCSRA |= _BV(ADSC);
  while (bit_is_set(ADCSRA, ADSC)) {};
  return (1.1 * 1024.0 / ADC) - VoltRef;
}

// Lettura tensione generica tramite partitore resistivo
float Voltmetro(int Pin, float R1, float R2, float ADJ_Error) {
  int Analog_Data = analogRead(Pin);
  float DataCon = ((Analog_Data + 0.5) * VAREF) / 1024.0;
  return (DataCon / (R2 / (R1 + R2))) + ADJ_Error;
}

void Voltaggio() {

  // Calcolo Media Analog Reference (VAREF)
  if (millis() < (TimerVAREF + 15000)) {
    if (CampNum_AREF < Campionamento) {
      VoltMedia_AREF += ReadVref();
      CampNum_AREF++;
    } else {
      VAREF = VoltMedia_AREF / CampNum_AREF;
      CampNum_AREF = 0;
      VoltMedia_AREF = 0;
    }
  }

  // Se il debug è attivo, bypassa la lettura
  if (Debug == 2) {
    V5 = 5.00;
    V12 = 12.00;
    return;
  }

  // Lettura tensioni 5V e 12V
  if (millis() >= (ResetTimerVirtuale[2] + DelayVirtuale[2])) {
    ResetTimerVirtuale[2] = millis();
    V5 = Voltmetro(Pin_5V, R1_5V, R2_5V, ADJ_Error_5V);
    V12 = Voltmetro(Pin_12V, R1_12V, R2_12V, ADJ_Error_12V);
  }

  // Protezione alimentazione 5V e 12V
  if (EN_OV && !Mod_attesa) {
    if ((V5 < (5.00 - V5_Limit) || V5 > (5.00 + V5_Limit)) && Aniamzione_Avvio && (!PowerLimitLED || LumLimitLED > 240)) {
      S_Pro_5V = true;
    } else if (V5 >= (5.00 - V5_Limit) && V5 <= (5.00 + V5_Limit) && !Aniamzione_Avvio) {
      S_Pro_5V = false;
    }

    if ((V12 < (12.00 - V12_Limit) || V12 > (12.00 + V12_Limit)) && Aniamzione_Avvio) {
      S_Pro_12V = true;
    } else if (V12 >= (12.00 - V12_Limit) && V12 <= (12.00 + V12_Limit) && !Aniamzione_Avvio) {
      S_Pro_12V = false;
    }
  }

  // Limitazione della potenza LED
  if (PowerLimitLED && V5 < LED_Limt && !Mod_attesa && Aniamzione_Avvio && millis() > (DelayFanPower + StartLEDPowerProt)) {
    PowerLimitLED_Stato = true;
    if (millis() >= (ResetTimerVirtuale[6] + DelayVirtuale[12])) {
      ResetTimerVirtuale[6] = millis();
      LumLimitLED = constrain(LumLimitLED + ((BRIGHTNESS - LumLimitLED > 13) ? 20 : 1), 0, BRIGHTNESS - 13);
    }
  } else {
    PowerLimitLED_Stato = false;
    LumLimitLED = 0;
  }

  // Disabilitazione protezioni in caso di override
  if (!EN_OV && Aniamzione_Avvio) {
    S_Pro_5V = false;
    S_Pro_12V = false;
  }
}
