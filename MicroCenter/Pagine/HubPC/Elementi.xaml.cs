using Gestionale_WEB.Models;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace MicroCenter.Pagine.HubPC
{
    /// <summary>
    /// Logica di interazione per Elementi.xaml
    /// </summary>
    /// 



    public class ArduFanHub
    {
        public List<Dictionary<string, List<Item>>> ArduFanHub_4_0 { get; set; }
    }

    public class Item
    {
        public string Nome { get; set; }
    }











    public partial class Elementi : Page
    {
        //Crea un Alias della Porta Seriale
        public static SerialPort SerialPort => Connessione._serialPort;
        //Crea un Alias dei Dati dell' HUB
        public static ArduHubFan Dispositivo => Connessione.Dispositivo;
        // Lista colori salvati
        private string filePath = Path.Combine("Dati", "Colori.json");
       



        private bool UI_Load = false;

        public Elementi()
        {
            InitializeComponent();

            CreaBottoniDinamici();
            GenerateColorButtons();
            GenerateAniamzioneButtons();


            if (Dispositivo.StatoConnessione)
            {
                if (Dispositivo.ModLED_Fan < 5) TrackVelocità.Value = Dispositivo.FanSpeed[Dispositivo.ModFAN_SPEED];
                TrackLuminosità.Value = Dispositivo.LumLED[Dispositivo.ModLED_Fan];

                Br_Temperatura.Value = Dispositivo.TempDS;

                UI_Load = true;
            }
        }






















        //public static List<string> GetElementiPrincipali(Lib_JSON_CRUD<ArduFanHub> jsonManager)
        //{
        //    var data = jsonManager.GetData();
        //    return data.ArduFanHub_4_0?.SelectMany(d => d.Keys).ToList() ?? new List<string>();
        //}

        //public static List<string> GetContenutoElemento(Lib_JSON_CRUD<ArduFanHub> jsonManager, string elemento)
        //{
        //    var data = jsonManager.GetData();
        //    var elementoTrovato = data.ArduFanHub_4_0?
        //        .FirstOrDefault(d => d.ContainsKey(elemento));

        //    return elementoTrovato != null
        //        ? elementoTrovato[elemento].Select(i => i.Nome).ToList()
        //        : new List<string>();
        //}

        public static List<string> GetElementiPrincipali(List<Dictionary<string, List<Item>>> arduFanHub)
        {
            return arduFanHub?.SelectMany(d => d.Keys).ToList() ?? new List<string>();
        }

        public static List<string> GetContenutoElemento(List<Dictionary<string, List<Item>>> arduFanHub, string elemento)
        {
            var elementoTrovato = arduFanHub?
                .FirstOrDefault(d => d.ContainsKey(elemento));

            return elementoTrovato != null
                ? elementoTrovato[elemento].Select(i => i.Nome).ToList()
                : new List<string>();
        }










        private void CreaBottoniDinamici()
        {

            //List<string> Elementi = new List<string>();

            //var _elementi = new Lib_JSON_CRUD<ColoreHubPC>(filePath);
            //var list = _elementi.GetData();

            //foreach (var item in list.ArduFanHub_4_0)
            //{

            //    Elementi.Add(item.HUB.ToString());
            //    //MessageBox.Show(item.Nome);

            //    //if (item.UI_stato == true)
            //    //{
            //    //    colors.Add(item.Nome);
            //    //    colorsCode.Add(item.Colore);
            //    //    saturazione.Add(item.Saturazione);
            //    //}
            //}






            // Inizializza la classe di gestione JSON
            Lib_JSON_CRUD<ArduFanHub> jsonManager = new Lib_JSON_CRUD<ArduFanHub>(filePath);

            // Ottieni gli elementi principali
           // List<string> elementiPrincipali = GetElementiPrincipali(jsonManager);
            //Console.WriteLine("Elementi principali:");
            //elementiPrincipali.ForEach(Console.WriteLine);

            // Ottieni il contenuto di un elemento specifico
           // string elementoDaCercare = "Ventole";
           // List<string> contenuto = GetContenutoElemento(jsonManager, elementoDaCercare);
            //Console.WriteLine($"\nContenuto di {elementoDaCercare}:");
            //contenuto.ForEach(Console.WriteLine);

            var data = jsonManager.GetData();

            // Ottieni gli elementi principali
            List<string> elementiPrincipali = GetElementiPrincipali(data.ArduFanHub_4_0);
            //Console.WriteLine("Elementi principali:");
            //elementiPrincipali.ForEach(Console.WriteLine);

            // Ottieni il contenuto di un elemento specifico
            string elementoDaCercare = "Ventole";
            List<string> contenuto = GetContenutoElemento(data.ArduFanHub_4_0, elementoDaCercare);
            //Console.WriteLine($"\nContenuto di {elementoDaCercare}:");
            //contenuto.ForEach(Console.WriteLine);























            //                                              1        2                  3                   4               5
            List<string> nomiBottoni = new List<string> { "HUB", "Ventole", "Dissipatore__Casse_Audio", "Scheda_Video", "Strisca_LED" };

            int mrg = 0;
            if (nomiBottoni.Count == 5)
            {
                mrg = 25;
            }
            else if (nomiBottoni.Count == 4)
            {
                mrg = 40;
            }

            foreach (string nome in nomiBottoni)
            {
                Button btn = new Button
                {
                    Name = $"btn_{nome}",
                    Height = 80,
                    Width = 80,
                    Margin = new Thickness(mrg, 0, mrg, 0), // Aggiunge un margine di 10
                    Content = $"Btn {nome}",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Cursor = System.Windows.Input.Cursors.Hand,
                    ToolTip = new Label { Content = $"Bottone {nome}" },
                    //    Style = (Style)FindResource("IconButtonsForms") // Stile preso dalle risorse
                };

                // btn.SetResourceReference(Button.ContentProperty, "IconaUSBConnetti");


                btn.Click += Btn_Click;

                StackPanelContainer.Children.Add(btn); // Aggiunge il bottone al contenitore
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SerialPort.IsOpen)
            {

                if (sender is Button btn)
                {
                    MessageBox.Show($"Hai cliccato: {btn.Name}");



                    switch (btn.Name)
                    {
                        case "btn_HUB":
                            Dispositivo.ModLED_Fan = 0;
                            // MessageBox.Show($"Hai Selezionato l'elemento: HUB {Dispositivo.ModLED_Fan}");
                            break;

                        case "btn_Ventole":
                            Dispositivo.ModLED_Fan = 1;
                            break;

                        case "btn_Scheda_Video":
                            Dispositivo.ModLED_Fan = 8;
                            break;

                        case "btn_Strisca_LED":
                            Dispositivo.ModLED_Fan = 9;
                            break;

                        default:

                            break;

                    }

                }



            }
            else
            {
                MessageBox.Show($"Errore Porta Seriale Chiusa");
            }


        }



        // Crea otto bottoni Colorati
        private void GenerateColorButtons()
        {
            List<string> colors = new List<string>();
            List<int> colorsCode = new List<int>();
            List<int> saturazione = new List<int>();

            var _colori = new Lib_JSON_CRUD<ColoreHubPC>(filePath);
            var list = _colori.GetData();

            foreach (var item in list.Colori)
            {
                //MessageBox.Show(item.Nome);

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
            var _colori = new Lib_JSON_CRUD<ColoreHubPC>(filePath);
            if (sender is Button btn)
            {
                var list = _colori.Trova<Colori>(p => p.Nome == btn.Name.ToString());

                if (SerialPort.IsOpen)
                {
                    Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = list.Colore;
                    Dispositivo.Saturazione[Dispositivo.ModLED_Fan] = list.Saturazione;
                }

            }

        }


        // Crea bottoni Animazione
        private void GenerateAniamzioneButtons()
        {
            List<string> colors = new List<string>();
            List<int> colorsCode = new List<int>();

            var _animazioni = new Lib_JSON_CRUD<ColoreHubPC>(filePath);
            var list = _animazioni.GetData();

            foreach (var item in list.Animazioni)
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
            var _animnazioni = new Lib_JSON_CRUD<ColoreHubPC>(filePath);
            if (sender is Button btn)
            {
                var list = _animnazioni.Trova<Animazioni>(p => p.Nome == btn.Name.ToString());

                if (SerialPort.IsOpen)
                {
                    Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = list.Colore;
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
