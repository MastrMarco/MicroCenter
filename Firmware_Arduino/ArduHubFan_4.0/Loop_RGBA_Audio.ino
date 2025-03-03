//*****************************************************************************************************************************//
//                                           Ver: X.00 Firmware data 00/00/25                                                  //
//*****************************************************************************************************************************//

int audio_input;
long RGB_MusicaA;
byte FrequenzaAggiornamento = 2;  // Frequenza di aggiornamento LED
byte decay_check = 0;

int Audio_pre_react[9] = { 0 };
int Audio_react[9] = { 0 };

const int numCampionamenti = 2;  // Numero di campionamenti da effettuare
int media;
int MaxValueAudio = 1023;  // Valore fondo scala LED

void RGB_Musica() {
  if (Boot_SetUp != 0) {
    audio_input = Animation_RGBS[0];  // Lettura del valore dal sensore

    static int campionamenti[numCampionamenti] = { 0 };
    static int indiceCampionamento = 0;
    static int sommaCampioni = 0;

    sommaCampioni += audio_input - campionamenti[indiceCampionamento];
    campionamenti[indiceCampionamento] = audio_input;

    indiceCampionamento = (indiceCampionamento + 1) % numCampionamenti;

    if (indiceCampionamento == 0) {
      media = max(sommaCampioni / numCampionamenti - 50, 0);
    }

    if (audio_input > MaxValueAudio) MaxValueAudio = audio_input;
    if (media < MaxValueAudio - 25) MaxValueAudio--;
  } else {
    audio_input = 2;
    media = 0;
    MaxValueAudio = 1;
  }

  RGB_MusicaA = (RGB_MusicaA < 327424) ? RGB_MusicaA + 256 : 0;

  for (int i = 0; i < 9; i++) {
    Audio_pre_react[i] = map(audio_input, media, MaxValueAudio, 0, NUM_LEDS_OUT[i + 1]);
    Audio_react[i] = max(Audio_react[i], Audio_pre_react[i]);
  }

  if (++decay_check > FrequenzaAggiornamento) {
    decay_check = 0;
    for (int i = 0; i < 9; i++) {
      if (Audio_react[i] > 0) Audio_react[i]--;
    }
  }

  for (int i = 0; i < 9; i++) {
    for (int j = NUM_LEDS_OUT[i + 1] - 1; j >= 0; j--) {
      if (j < Audio_react[i]) {
        int pixelHue = RGB_MusicaA + (j * 65536L / NUM_LEDS_OUT[i + 1]);
        Strip[i].setPixelColor(j, Strip[i].gamma32(Strip[i].ColorHSV(pixelHue)));
      } else {
        Strip[i].setPixelColor(j, 0, 0, 0);
      }
    }
  }
}
