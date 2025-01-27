using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MicroCenter.Pagine.Connessione;

namespace MicroCenter.Classi
{
    public class ArduHubFan
    {

        public string Stato_Software { get; set; }

        public int SoC { get; set; }

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




        // "Calcolati"
        // public string Nome {  get; set; }
        // public string SoC { get; set; }
        public bool StatoConnessione { get; set; }


        // Lista Seriale Conversione


        //ArduHubFan(List<List<string>> SerialDataRead)
        //{
        //    StringaCaricamentoDati_HUB(SerialDataRead);
        //}
       





        // Start SerialString Conversione in Lista
        public List<List<string>> parsData;
        public void SetSrialToList(string SerialRead)
        {
            // Conversione Dati Seriali in Lista
            if (SerialRead != null && SerialRead.Length > 50)
            {
                parsData = DecodeSeralData_0.ParseString(SerialRead);
            }

            // Assegnazione dati Dalla Lista alle Singole Vriabbili
            if (Get_CountListSeriale() == 13)
            {
                StringaCaricamentoDati_HUB(parsData);
                StatoConnessione = true;
            } else
            {
                StatoConnessione = false;
            }
        }

        // Split in Colonne
        private static class DecodeSeralData_0
        {
            public static List<List<string>> ParseString(string input)
            {
                // Lista finale per contenere i dati
                var result = new List<List<string>>();

                // Suddividi le righe principali usando il delimitatore ";"
                var rows = input.Split(';');

                foreach (var row in rows)
                {
                    // Suddividi ogni riga in colonne usando il delimitatore ","
                    var columns = row.Split(',');
                    result.Add(columns.ToList());
                }

                return result;
            }
        }

        public int Get_CountListSeriale()
        {
            return parsData.Count;
        }
        // End SerialString Conversione in Lista



        private void StringaCaricamentoDati_HUB(List<List<string>> SerialDataRead)
        {


            Stato_Software = SerialDataRead[0][0]; // Serve a Capire il Tipo di Versione del Firmware del Dispositivo Riaga ID-0; Colonna ID-0, Es Beta

            SoC = int.Parse(SerialDataRead[0][1]); // Serve a Capire il Tipo di Microcontrollore del Dispositivo Riaga ID-0; Colonna ID-1, Es Arduino Uno ATMega 328P

            Progetto = int.Parse(SerialDataRead[0][3]); // Serve a Capire il Nome del Progetto / Dispositivo e il Tipo Riaga ID-0; Colonna ID-3, Es ArduFanHub 2.0

            Versione = SerialDataRead[0][4]; // Serve a Capire la Versione del Firmware del Dispositivo Riaga ID-0; Colonna ID-4, Es v1.00

            TempDS = int.Parse(SerialDataRead[1][0]); // Sensore Temperatura Riaga ID-1; Colonna ID-0, Es 23 °C

            V5 = decimal.Parse(SerialDataRead[1][0]);

            V12 = decimal.Parse(SerialDataRead[1][0]);

            S_Pro_12V = int.Parse(SerialDataRead[1][0]);

            PowerLimitLED_Stato = int.Parse(SerialDataRead[1][0]);

            S_Pro_5V = int.Parse(SerialDataRead[1][0]);

            VAREF = decimal.Parse(SerialDataRead[1][6], CultureInfo.InvariantCulture); // Tensione VoltRef Microcontrollore Riaga ID-1; Colonna ID-6, Es 4.89 V

            ROM_Dati = int.Parse(SerialDataRead[1][0]);

            EN_OV = int.Parse(SerialDataRead[1][0]);

            PowerLimitLED = int.Parse(SerialDataRead[1][0]);

            //foreach ()
            //{
            //    NUM_LEDS_OUT  // * Elemento
            //    LumLED  // * Elemento
            //    ColoreLED  // * Elemento
            //    Saturazione  // * Elemento
            //}





            ModLED_Fan = int.Parse(SerialDataRead[1][0]);
            ModFAN_SPEED = int.Parse(SerialDataRead[1][0]);
            ModRGB_LED = int.Parse(SerialDataRead[1][0]);


            //  FanSpeed  // * Elemento
            //   Fan_Mod_Speed // * Elemento
            //   RPM_Fan // * Elemento

            //   Animation_RGBS  // * Elemento


        }











        // Nome Dispositivo Connesso
        public string Get_NomeDispositivo()
        {

            switch (Progetto)
            {
                case 3:
                    return "ArduHubFanAudio 2.0";
                //break;
                case 4:
                    // code block
                    return "ArduHubFan 3.0";
                //break;
                case 5:
                    // code block
                    return "ArduFanHub 4.0";
                //break;
                default:
                    // code block
                    return "Non Identificato";
                    //break;
            }
        }

        // Tipo di Versione Firmware Dispositivo
        public string Get_TipoVersine()
        {
            switch (Stato_Software)
            {
                case "D":
                    return "Debug";
                //break;
                case "B":
                    // code block
                    return "Beta";
                //break;
                case "R":
                    // code block
                    return "Release";
                //break;
                default:
                    // code block
                    return "Er";
                    //break;
            }
        }

        // Tipo di SoC
        public string Get_TipoSoC()
        {
            switch (SoC)
            {
                case 1:
                    return "Arduino Nano ATmega328P";
                //break;
                case 2:
                    // code block
                    return "Arduino Nano ATmega328P";
                //break;
                case 3:
                    // code block
                    return "Arduino Uno ATmega328P";
                //break;
                default:
                    // code block
                    return "Non Identificato";
                    //break;
            }
        }
    }
}
