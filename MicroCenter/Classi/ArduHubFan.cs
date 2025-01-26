using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCenter.Classi
{
    public class ArduHubFan
    {

        public string Stato_Software { get; set; }

        public int Arduino { get; set; }

        public int Progetto { get; set; }

        public string Versione { get; set; }

        public int TempDS { get; set; }

        public decimal V5 { get; set; }

        public decimal V12 { get; set; }

        public int S_Pro_12V { get; set; }

        public int PowerLimitLED_Stato { get; set; }

        public int S_Pro_5V { get; set; }

        public decimal VAREF { get; set; }

        public int ROM_Dati { get; set; }

        public int EN_OV { get; set; }

        public int PowerLimitLED { get; set; }


        public int[] NUM_LEDS_OUT { get; set; } // * Elemento
        public int[] LumLED { get; set; } // * Elemento
        public int[] ColoreLED { get; set; } // * Elemento
        public int[] Saturazione { get; set; } // * Elemento


        public int ModLED_Fan { get; set; }
        public int ModFAN_SPEED { get; set; }
        public int ModRGB_LED { get; set; }


        public int[] FanSpeed { get; set; } // * Elemento
        public int[] Fan_Mod_Speed { get; set; } // * Elemento
        public int[] RPM_Fan { get; set; } // * Elemento

        public int[] Animation_RGBS { get; set; } // * Elemento


        ArduHubFan(List<List<string>> SerialDataRead)
        {
            StringaCaricamentoDati_HUB(SerialDataRead);
        }



        private void StringaCaricamentoDati_HUB(List<List<string>> SerialDataRead)
        {


            Stato_Software = SerialDataRead[0][1];

            Arduino = int.Parse(SerialDataRead[0][0]);

            Progetto = int.Parse(SerialDataRead[0][0]);

            Versione = SerialDataRead[0][0];

            TempDS = int.Parse(SerialDataRead[0][0]);

            V5 = decimal.Parse(SerialDataRead[0][0]);

            V12 = decimal.Parse(SerialDataRead[0][0]);

            S_Pro_12V = int.Parse(SerialDataRead[0][0]);

            PowerLimitLED_Stato = int.Parse(SerialDataRead[0][0]);

            S_Pro_5V = int.Parse(SerialDataRead[0][0]);

            VAREF = decimal.Parse(SerialDataRead[0][0]);

            ROM_Dati = int.Parse(SerialDataRead[0][0]);

            EN_OV = int.Parse(SerialDataRead[0][0]);

            PowerLimitLED = int.Parse(SerialDataRead[0][0]);

            //foreach ()
            //{
            //    NUM_LEDS_OUT  // * Elemento
            //    LumLED  // * Elemento
            //    ColoreLED  // * Elemento
            //    Saturazione  // * Elemento
            //}





            ModLED_Fan = int.Parse(SerialDataRead[0][0]);
            ModFAN_SPEED = int.Parse(SerialDataRead[0][0]);
            ModRGB_LED = int.Parse(SerialDataRead[0][0]);


            //  FanSpeed  // * Elemento
            //   Fan_Mod_Speed // * Elemento
            //   RPM_Fan // * Elemento

            //   Animation_RGBS  // * Elemento









        }


    }
}
