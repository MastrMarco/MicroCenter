using MicroCenter.Classi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Path = System.IO.Path;

namespace MicroCenter.Pagine.HubPC
{
    /// <summary>
    /// Logica di interazione per Elementi.xaml
    /// </summary>
    /// 

    public partial class Elementi : Page
    {
        //Crea un Alias della Porta Seriale
        public static SerialPort SerialPort => Connessione._serialPort;
        //Crea un Alias dei Dati dell' HUB
        public static ArduHubFan Dispositivo => Connessione.Dispositivo;
        // Lista colori salvati
        private string filePathColori = Path.Combine("Dati", "HUB_PC", "Colori.json");
        private string filePathElementi = Path.Combine("Dati", "HUB_PC", "Lista_Dispositivi_Elementi.json");




        private bool UI_Load = false;

        public Elementi()
        {
            InitializeComponent();

            CreaBottoniGruppiElementi();
            GenerateColorButtons();
            GenerateAniamzioneButtons();


            if (Dispositivo.StatoConnessione)
            {
                if (Dispositivo.ModLED_Fan < 5) TrackVelocità.Value = Dispositivo.FanSpeed[Dispositivo.ModFAN_SPEED];
                TrackLuminosità.Value = Dispositivo.LumLED[Dispositivo.ModLED_Fan];

                Br_Temperatura.Value = Dispositivo.TempDS;

                UI_Load = true;
            }



            ElementoDato();
            //  ClearTextBlock();
            //  ElementoDato();
        }


        // Ricerca Info dell'elemento selezionato
        public List<InfoElementi> GetInfoElemento(string versione)
        {
            List<InfoElementi> elementiFiltrati = new List<InfoElementi>();

            // Usa Lib_JSON_CRUD con la classe InfoElementi
            var list = new Lib_JSON_CRUD_3<InfoElementi>(filePathElementi);

            // Ottieni gli elementi principali per la versione specificata
            var elementiPrincipali = list.GetElementiPrincipali(versione);

            foreach (var categoria in elementiPrincipali)
            {
                // Ottieni il contenuto degli elementi per la categoria corrente
                var _list = list.GetContenutoElemento(versione, categoria);

                foreach (var ls in _list)
                {
                    if (ls.Mod_LED_Fan == Dispositivo.ModLED_Fan) // Se soddisfa la condizione
                    {
                        elementiFiltrati.Add(ls); // Aggiunge l'intero oggetto InfoElementi alla lista
                    }
                }
            }

            return elementiFiltrati;
        }
        // Ricerca Nome del Gruppo di Elementi
        public string GetNomeElemento(string versione)
        {
            // Usa Lib_JSON_CRUD con la classe Ventola
            var list = new Lib_JSON_CRUD_3<InfoElementi>(filePathElementi);
            // Stampare gli elementi principali
            var elementiPrincipali = list.GetElementiPrincipali(versione);
            foreach (var item in elementiPrincipali)
            {
                string categoria = item;
                var _list = list.GetContenutoElemento(versione, categoria);
                foreach (var ls in _list)
                {
                    if (ls.Mod_LED_Fan == Dispositivo.ModLED_Fan)
                    {
                        return item;
                    }
                }
            }
            return "";
        }






        private void ClearTextBlock()
        {
            if (ListLaDatiElemento != null)
            {
                ListLaDatiElemento.Children.Clear();
                //ListLaDatiElemento = null;
            }

            if (ListLaDatiElementov != null)
            {
                ListLaDatiElementov.Children.Clear();
                //ListLaDatiElementov = null;
            }

            if (ListSelectElemento != null)
            {
                ListSelectElemento.Children.Clear();
                //ListSelectElemento = null;
            }
        }


        // visualizza info dati elemento selezionato
        public void ElementoDato()
        {
            List<string> ListDatiElemeno_Full = new List<string>
            {
                "Colore", "Luminosità", "Gestione Rotazione", "Velocità", "RPM"
            };
            List<string> ListDatiElemeno_LED = new List<string>
            {
                "Colore", "Luminosità"
            };
            List<string> ListDatiElemeno_Fan = new List<string>
            {
               "Gestione Rotazione", "Velocità", "RPM"
            };



            // Usa Lib_JSON_CRUD con la classe Ventola
            var list = new Lib_JSON_CRUD_3<InfoElementi>(filePathElementi);
            string versione = Dispositivo.Get_NomeDispositivo();
            // Stampare gli elementi principali
            var elementiPrincipali = list.GetElementiPrincipali(versione);
            var _list = GetInfoElemento(versione);
            List<string> ls = new List<string>();

            if (SerialPort.IsOpen)
            {

                if (_list[0].LED && _list[0].FAN)
                {
                    ls = ListDatiElemeno_Full;
                }
                else if (_list[0].LED && !_list[0].FAN)
                {
                    ls = ListDatiElemeno_LED;
                }
                else if (!_list[0].LED && _list[0].FAN)
                {
                    ls = ListDatiElemeno_Fan;
                }

            }

            // Titolo Elemento Selezionato
            LaNomeElemento.Text = _list[0].Nome;


            // Lista di Dati testo
            for (int i = 0; i < ls.Count; i++)
            {
                // Color buttoncolor = RGB__HSV.ConvertHsvToRgb(colorsCode[i], saturazione[i], 255);
                TextBlock text = new TextBlock
                {
                    Name = $"La_{i}",
                    Text = ls[i],
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5)
                };
                ListLaDatiElemento.SetResourceReference(TextBlock.ForegroundProperty, "PrimaryTextColor");
                // button.Click += BtnColor_Click;

                ListLaDatiElemento.Children.Add(text);
            }


            // Creazione lista di dati
            for (int i = 0; i < ls.Count; i++)
            {
                TextBlock text = new TextBlock
                {
                    Name = $"La_{i}",
                    Text = "null",
                    HorizontalAlignment = HorizontalAlignment.Right,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5)
                };
                ListLaDatiElementov.SetResourceReference(TextBlock.ForegroundProperty, "PrimaryTextColor");
                ListLaDatiElementov.Children.Add(text);
            }



            // Creazione bottoni sotto elemnti
            //string x = GetNomeElemento(versione);
            var sottoelementi = list.GetContenutoElemento(versione, GetNomeElemento(versione));

            if (sottoelementi != null && sottoelementi.Count > 1)
            {
                for (int i = 0; i < sottoelementi.Count; i++)
                {
                    Button btn = new Button
                    {
                        Name = $"Btn_{i}",
                        //Height = 25,
                        //Width = 45,
                        Margin = new Thickness(15, 0, 15, 0), // Aggiunge un margine di 10
                        Content = sottoelementi[i].Nome,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Cursor = System.Windows.Input.Cursors.Hand,
                        ToolTip = new Label { Content = sottoelementi[i].Nome },
                        //    Style = (Style)FindResource("IconButtonsForms") // Stile preso dalle risorse
                    };

                    // btn.SetResourceReference(Button.ContentProperty, "IconaUSBConnetti");

                    // btn.Click += Btn_Click;

                    ListSelectElemento.Children.Add(btn); // Aggiunge il bottone al contenitore
                }
            }
        }






        // Seleziona Gruppo elemento
        private void CreaBottoniGruppiElementi()
        {
            // Usa Lib_JSON_CRUD con la classe Ventola
            var list = new Lib_JSON_CRUD_3<InfoElementi>(filePathElementi);

            string versione = Dispositivo.Get_NomeDispositivo();
            // string categoria = "Ventole";

            // Stampare gli elementi principali
            var elementiPrincipali = list.GetElementiPrincipali(versione);

            // Aggiungere una nuova ventola
            //Ventola nuovaVentola = new Ventola { Nome = "Ventola 5", Velocità = 1200 };
            //jsonManager.AddElement(versione, categoria, nuovaVentola);


            // Stampare il contenuto della categoria "Ventole"
            // var ventole = jsonManager.GetContenutoElemento(versione, categoria);



            int mrg = 0;
            if (elementiPrincipali.Count == 5)
            {
                mrg = 25;
            }
            else if (elementiPrincipali.Count == 4)
            {
                mrg = 40;
            }

            foreach (string nome in elementiPrincipali)
            {
                Button btn = new Button
                {
                    Name = nome,
                    Height = 80,
                    Width = 80,
                    Margin = new Thickness(mrg, 0, mrg, 0), // Aggiunge un margine di 10
                    Content = nome,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Cursor = System.Windows.Input.Cursors.Hand,
                    ToolTip = new Label { Content = nome },
                    //    Style = (Style)FindResource("IconButtonsForms") // Stile preso dalle risorse
                };

                // btn.SetResourceReference(Button.ContentProperty, "IconaUSBConnetti");


                btn.Click += BtnGruppiElementi_Click;

                StackPanelContainer.Children.Add(btn); // Aggiunge il bottone al contenitore
            }
        }
        private void BtnGruppiElementi_Click(object sender, RoutedEventArgs e)
        {
            string versione = Dispositivo.Get_NomeDispositivo();

            if (sender is Button btn)
            {
                string categoria = btn.Name;

                var _list = new Lib_JSON_CRUD_3<InfoElementi>(filePathElementi);

                var el = _list.FindElement(versione, categoria, c => c.Nome == btn.Name);

                if (el != null)
                {
                    if (SerialPort.IsOpen)
                    {
                        if (Dispositivo.ModLED_Fan == el.Mod_LED_Fan)
                        {
                            Dispositivo.ModLED_Fan = el.Mod_LED_Fan;

                            ClearTextBlock();
                            ElementoDato();
                        }
                    }
                }
            }

        }


            //  MessageBox.Show($"Errore Porta Seriale Chiusa");

        }

        // Crea otto bottoni Colorati
        private void GenerateColorButtons()
        {
            List<string> colors = new List<string>();
            List<int> colorsCode = new List<int>();
            List<int> saturazione = new List<int>();


            // Usa Lib_JSON_CRUD con la classe Ventola
            var _colori = new Lib_JSON_CRUD_3<Colori>(filePathColori);

            // Stampare gli elementi principali
            string contenitore = "";
            // Stampare il contenuto della categoria
            string categoria = "Colori";
            var list = _colori.GetContenutoElemento(contenitore, categoria);


            foreach (var item in list)
            {
                // MessageBox.Show(item.Nome);

                if (item.UI_stato == true)
                {
                    colors.Add(item.Nome);
                    colorsCode.Add(item.Colore);
                    saturazione.Add(item.Saturazione);
                }
            }




            for (int i = 0; i < colors.Count; i++)
            {
                Color buttoncolor = RGB__HSV.ConvertHsvToRgb(colorsCode[i], saturazione[i], 255);
                Button button = new Button
                {
                    Name = colors[i],
                    Height = 15,
                    Width = 24,
                    Margin = new Thickness(10),
                    ToolTip = new Label { Content = colors[i] },
                    Background = new SolidColorBrush(buttoncolor)
                };

                button.Click += BtnColor_Click;

                if (i < 4)
                    Color0.Children.Add(button);
                else
                    Color1.Children.Add(button);
            }
        }
        private void BtnColor_Click(object sender, RoutedEventArgs e)
        {
            string categoria = "Colori";

            if (sender is Button btn)
            {

                var _list = new Lib_JSON_CRUD_3<Colori>(filePathColori);

                var el = _list.FindElement("", categoria, c => c.Nome == btn.Name);

                if (el != null)
                {
                    if (SerialPort.IsOpen)
                    {
                        Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = el.Colore;
                        Dispositivo.Saturazione[Dispositivo.ModLED_Fan] = el.Saturazione;
                    }
                }
                else
                {

                }

            }


        }


        // Crea bottoni Animazione
        private void GenerateAniamzioneButtons()
        {
            List<string> colors = new List<string>();
            List<int> colorsCode = new List<int>();

            //var _animazioni = new Lib_JSON_CRUD<ColoreHubPC>(filePathColori);
            //var list = _animazioni.GetData();


            // Usa Lib_JSON_CRUD con la classe Ventola
            var _colori = new Lib_JSON_CRUD_3<Animazioni>(filePathColori);

            // Stampare gli elementi principali
            string contenitore = "";
            // Stampare il contenuto della categoria
            string categoria = "Animazioni";
            var list = _colori.GetContenutoElemento(contenitore, categoria);


            foreach (var item in list)
            {
                //MessageBox.Show(item.Nome);

                if (item.UI_stato == true)
                {
                    colors.Add(item.Nome);
                    colorsCode.Add(item.Colore);
                }
            }

            for (int i = 0; i < colors.Count; i++)
            {
                Button button = new Button
                {
                    Name = colors[i],
                    Height = 35,
                    Width = 35,
                    Margin = new Thickness(10),
                    ToolTip = new Label { Content = colors[i] },
                    // Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[i])),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };

                button.Click += BtnAnimazioni_Click;

                if (i < 3)
                    Animazione0.Children.Add(button);
                else
                    Animazione1.Children.Add(button);
            }
        }
        private void BtnAnimazioni_Click(object sender, RoutedEventArgs e)
        {
            string categoria = "Animazioni";

            if (sender is Button btn)
            {

                var _list = new Lib_JSON_CRUD_3<Animazioni>(filePathColori);

                var el = _list.FindElement("", categoria, c => c.Nome == btn.Name);

                if (el != null)
                {
                    if (SerialPort.IsOpen)
                    {
                        Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = el.Colore;
                    }
                }
                else
                {

                }

            }

        }










        private void TrackVelocità_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Dispositivo.StatoConnessione && UI_Load)
            {
                Dispositivo.FanSpeed[Dispositivo.ModFAN_SPEED] = (int)TrackVelocità.Value;
            }
        }

        private void TrackLuminosità_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Dispositivo.StatoConnessione && UI_Load)
            {
                Dispositivo.LumLED[Dispositivo.ModLED_Fan] = (int)TrackLuminosità.Value;
            }
        }





    }
}
