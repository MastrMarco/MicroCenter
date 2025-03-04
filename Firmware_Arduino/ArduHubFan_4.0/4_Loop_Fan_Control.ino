//*****************************************************************************************************************************//
//                                           Ver: X.00 Firmware data 00/00/25                                                  //
//*****************************************************************************************************************************//

void Void_Fan_Mod() {
  //-------------------------- Controllo Speed RPM Fan ----------------------
  if (!PinA_pre && PinA) Count_RPM_1++;
  PinA_pre = PinA;

  if (!PinB_pre && PinB) Count_RPM_2++;
  PinB_pre = PinB;

  if (!PinC_pre && PinC) Count_RPM_3++;
  PinC_pre = PinC;

  if (!PinD_pre && PinD) Count_RPM_4++;
  PinD_pre = PinD;

  if (millis() >= (ResetTimerVirtuale[5] + DelayVirtuale[11])) {
    ResetTimerVirtuale[5] = millis();
    RPM_Fan1 = Count_RPM_1;
    Count_RPM_1 = 0;
    RPM_Fan2 = Count_RPM_2;
    Count_RPM_2 = 0;
    RPM_Fan3 = Count_RPM_3;
    Count_RPM_3 = 0;
    RPM_Fan4 = Count_RPM_4;
    Count_RPM_4 = 0;
  }

  //---------------------------- Controllo Speed Fan -----------------------
  if (S_Pro_12V) {
    SetAllVentola(0);
    return;
  }

  if ((millis() < (StartFanPower + DelayFanPower)) && ControlloFan) {
    SetAllVentola(255);
  } else {
    if (ModLED_Fan == 0 && Fan_Mod_Speed[0] == 0) {
      SetAllVentola(FanSpeed[0]);
    } else {
      if (ModLED_Fan == 1) analogWrite(PWM_Fan_1, FanSpeed[ModLED_Fan]);
      if (ModLED_Fan == 2) analogWrite(PWM_Fan_2, FanSpeed[ModLED_Fan]);
      if (ModLED_Fan == 3) analogWrite(PWM_Fan_3, FanSpeed[ModLED_Fan]);
      if (ModLED_Fan == 4) analogWrite(PWM_Fan_4, FanSpeed[ModLED_Fan]);
    }
  }
}

void SetAllVentola(byte PWM) {
  analogWrite(PWM_Fan_1, PWM);
  analogWrite(PWM_Fan_2, PWM);
  analogWrite(PWM_Fan_3, PWM);
  analogWrite(PWM_Fan_4, PWM);
}
