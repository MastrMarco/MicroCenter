using MicroCenter.Classi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
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



        private List<string> ListDatiElemento_Full = new List<string> { "Colore", "Luminosità", "Gestione Rotazione", "Velocità", "RPM" };
        private List<string> ListDatiElemento_LED = new List<string> { "Colore", "Luminosità" };
        private List<string> ListDatiElemento_Fan = new List<string> { "Gestione Rotazione", "Velocità", "RPM" };



        private bool UI_Load = false;


        public Elementi()
        {
            InitializeComponent();



            if (Dispositivo.StatoConnessione)
            {
                // Genera i bottoni Colorati
                GenerateColorButtons();
                GenerateAniamzioneButtons();
                CreaBottoniGruppiElementi();
                GestioneLuminosità("", Dispositivo.LumLED[Dispositivo.ModLED_Fan], false);


                TrackCommand();


                Br_Temperatura.Maximum = Dispositivo.TempDS;
                Br_Temperatura.Value = Dispositivo.TempDS;
                LaTemp.Text = Br_Temperatura.Value.ToString() + "°C";

                UI_Load = true;

                ElementoDato();
               // ClearTextBlock();
                UI_dati();
            }

            DataContext = this; // Imposta il DataContext per il binding

            // ElementoDato();
            //  ClearTextBlock();

            // UI_dati();
        }


        //Aggirna posizione TrackBar Luminosità e Velocità ventole
        private void TrackCommand()
        {
            if (Dispositivo.StatoConnessione)
            {
                if (Dispositivo.ModLED_Fan < 5) TrackVelocità.Value = Dispositivo.FanSpeed[Dispositivo.ModLED_Fan];
                TrackLuminosità.Value = Dispositivo.LumLED[Dispositivo.ModLED_Fan];
                // Br_Temperatura.Value = Dispositivo.TempDS;
                // UI_Load = true;
            }
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






        private void UI_dati()
        {
            //Gestione visualizazione dati Elemento selezionato
            if (ListLaDatiElementov.Children.Count > 0)
            {
                for (int i = 0; i < ListLaDatiElementov.Children.Count; i++)
                {

                    if (ListLaDatiElementov.Children[i] is TextBlock item)
                    {
                        if (ListDatiElemento_Full[0] == item.Tag.ToString())
                        {
                            //Colore
                            string categorial = "Colori";
                            var _list = new Lib_JSON_CRUD_3<Colori>(filePathColori);
                            var el = _list.FindElement("", categorial, c => c.Colore == Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] && c.Saturazione == Dispositivo.Saturazione[Dispositivo.ModLED_Fan] && Dispositivo.LumLED[Dispositivo.ModLED_Fan] > 0);
                            if (el != null)
                            {
                                item.Text = el.Nome;
                            }
                            else
                            {
                                categorial = "Animazioni";
                                var __list = new Lib_JSON_CRUD_3<Animazioni>(filePathColori);
                                var _el = __list.FindElement("", categorial, c => c.Colore == Dispositivo.ColoreLED[Dispositivo.ModLED_Fan]);
                                if (_el != null)
                                {
                                    item.Text = _el.Nome;
                                }
                                else
                                {
                                    item.Text = "ErNomeColore";
                                }
                            }

                        }
                        else if (ListDatiElemento_Full[1] == item.Tag.ToString())
                        {
                            //Luminosità
                            decimal MaxValLum = 255;
                            decimal ValDataLum = Dispositivo.LumLED[Dispositivo.ModLED_Fan];
                            decimal lumperc = (ValDataLum / MaxValLum) * 100;
                            item.Text = ((int)lumperc).ToString() + "%";
                        }
                        else if (ListDatiElemento_Full[2] == item.Tag.ToString())
                        {
                            // Modalità Rotazione - Controllo Ventole
                            // item.Text = Dispositivo.[Dispositivo.ModLED_Fan].ToString();
                            item.Text = "Manuale";
                        }
                        else if (ListDatiElemento_Full[3] == item.Tag.ToString())
                        {
                            // Velocitò Rotazione Ventola
                            decimal MaxValSpeed = 255;
                            decimal ValDataSpeed = Dispositivo.FanSpeed[Dispositivo.ModLED_Fan < 5 ? Dispositivo.ModLED_Fan : 0];
                            decimal Speedperc = (ValDataSpeed / MaxValSpeed) * 100;
                            item.Text = ((int)Speedperc).ToString() + "%";
                        }
                        else if (ListDatiElemento_Full[4] == item.Tag.ToString())
                        {
                            // RPM Ventola
                            item.Text = (Dispositivo.RPM_Fan[Dispositivo.ModLED_Fan < 4 ? Dispositivo.ModLED_Fan : 0] * 60).ToString();
                        }



                    }

                }
                //  MessageBox.Show($"Il primo pulsante esiste ed è stato creato. {firstButton.Tag}");
            }


            //Gestione Colore Elemento Selezionato Grafica
            //Colore
            string categoria = "Colori";
            var ListColore = new Lib_JSON_CRUD_3<Colori>(filePathColori);
            var co = ListColore.FindElement("", categoria, c => c.Colore == Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] && c.Saturazione == Dispositivo.Saturazione[Dispositivo.ModLED_Fan] && Dispositivo.LumLED[Dispositivo.ModLED_Fan] > 0);
            categoria = "Animazioni";
            var ListAnimazione = new Lib_JSON_CRUD_3<Animazioni>(filePathColori);
            var an = ListAnimazione.FindElement("", categoria, c => c.Colore == Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] && Dispositivo.LumLED[Dispositivo.ModLED_Fan] == 0);


            if (co != null)
            {
                if (Color0.Children.Count > 0)
                {
                    for (int i = 0; i < Color0.Children.Count; i++)
                    {
                        if (Color0.Children[i] is Button item)
                        {
                            if (co.Nome.ToString() == item.Tag.ToString())
                            {
                                SelectButton(item);
                            }

                        }
                    }
                }

                if (Color1.Children.Count > 0)
                {
                    for (int i = 0; i < Color1.Children.Count; i++)
                    {
                        if (Color1.Children[i] is Button item)
                        {
                            if (co.Nome.ToString() == item.Tag.ToString())
                            {
                                SelectButton(item);
                            }

                        }
                    }
                }
            }
            else if (an != null)
            {
                if (Animazione0.Children.Count > 0)
                {
                    for (int i = 0; i < Animazione0.Children.Count; i++)
                    {
                        if (Animazione0.Children[i] is Button item)
                        {
                            if (an.Nome.ToString() == item.Tag.ToString())
                            {
                                SelectButton(item);
                            }

                        }
                    }
                }

                if (Animazione1.Children.Count > 0)
                {
                    for (int i = 0; i < Animazione1.Children.Count; i++)
                    {
                        if (Animazione1.Children[i] is Button item)
                        {
                            if (an.Nome.ToString() == item.Tag.ToString())
                            {
                                SelectButton(item);
                            }

                        }
                    }
                }

            }

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

            if (ListSelectElemento != null)
            {
                Animazione0.Children.Clear();
                Animazione1.Children.Clear();
                //ListSelectElemento = null;
            }
        }













        // visualizza info dati elemento selezionato
        public void ElementoDato()
        {
            if (Dispositivo.StatoConnessione)
            {
                // Usa Lib_JSON_CRUD con la classe Ventola
                var list = new Lib_JSON_CRUD_3<InfoElementi>(filePathElementi);
                string versione = Dispositivo.Get_NomeDispositivo();
                // Stampare gli elementi principali
                var elementiPrincipali = list.GetElementiPrincipali(versione);
                var _list = GetInfoElemento(versione);
                List<string> ls = new List<string>();



                if (_list[0].LED && _list[0].FAN)
                {
                    ls = ListDatiElemento_Full;
                }
                else if (_list[0].LED && !_list[0].FAN)
                {
                    ls = ListDatiElemento_LED;
                }
                else if (!_list[0].LED && _list[0].FAN)
                {
                    ls = ListDatiElemento_Fan;
                }



                // Titolo Elemento Selezionato
                LaNomeElemento.Text = _list[0].Nome;


                // Lista di Dati testo
                for (int i = 0; i < ls.Count; i++)
                {
                    // Color buttoncolor = RGB__HSV.ConvertHsvToRgb(colorsCode[i], saturazione[i], 255);
                    TextBlock text = new TextBlock
                    {
                        Name = $"La_Txt_{i}",
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
                        Name = $"La_Dati_{i}",
                        Text = "null",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(5),
                        Tag = ls[i]
                    };
                    ListLaDatiElementov.SetResourceReference(TextBlock.ForegroundProperty, "PrimaryTextColor");
                    ListLaDatiElementov.Children.Add(text);
                }



                // Creazione bottoni sotto elemnti


                string GruppoElementoSelezionato = "";
                foreach (var index in elementiPrincipali)
                {

                    var el = list.FindElement(versione, index, c => c.Mod_LED_Fan == Dispositivo.ModLED_Fan);

                    if (el != null)
                    {
                        if (SerialPort.IsOpen)
                        {
                            GruppoElementoSelezionato = el.Nome;
                            break;
                        }
                    }
                    else
                    {
                        GruppoElementoSelezionato = "ErElemento";
                    }

                }


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
                            Tag = sottoelementi[i].Nome,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Cursor = System.Windows.Input.Cursors.Hand,
                            ToolTip = new Label { Content = sottoelementi[i].Nome },
                            BorderThickness = new Thickness(1),
                            BorderBrush = Brushes.Transparent // Nessun bordo iniziale
                            //    Style = (Style)FindResource("IconButtonsForms") // Stile preso dalle risorse
                        };

                        // btn.SetResourceReference(Button.ContentProperty, "IconaUSBConnetti");

                        btn.Click += BtnGruppiElementiBar_Click;

                        ListSelectElemento.Children.Add(btn); // Aggiunge il bottone al contenitore

                        if (sottoelementi[i].Nome == GruppoElementoSelezionato) // Se il pulsante è "Red", lo selezioniamo di default
                        {
                            SelectButtonUnderGrupElemento(btn);
                        }
                    }
                }
            }
        }
        private void BtnGruppiElementiBar_Click(object sender, RoutedEventArgs e)
        {
            if (SerialPort.IsOpen)
            {
                string versione = Dispositivo.Get_NomeDispositivo();
                string gruppo = GetNomeElemento(versione);
                if (sender is Button btn)
                {
                    string Newelemento = btn.Tag.ToString();
                    // Usa Lib_JSON_CRUD con la classe Ventola
                    var list = new Lib_JSON_CRUD_3<InfoElementi>(filePathElementi);
                    // Stampare gli elementi principali
                    var elementiPrincipali = list.GetElementiPrincipali(versione);
                    // Ottieni il contenuto degli elementi per la categoria corrente
                    var _list = list.GetContenutoElemento(versione, gruppo);

                    for (int i = 0; i < _list.Count; i++)
                    {
                        if (_list[i].Nome == Newelemento)
                        {
                            Dispositivo.ModLED_Fan = _list[i].Mod_LED_Fan;
                            ClearTextBlock();
                            ElementoDato();
                            GenerateAniamzioneButtons();
                            UI_dati();
                            TrackCommand();
                            GestioneLuminosità("", Dispositivo.LumLED[Dispositivo.ModLED_Fan], false);
                        }
                    }

                }
            }
        }
        // Grafica Selezione Colore
        private Button selectedButtonUnderGrupElemento = null;
        private void SelectButtonUnderGrupElemento(Button button)
        {
            if (selectedButtonUnderGrupElemento != null)
            {
                selectedButtonUnderGrupElemento.BorderBrush = Brushes.Transparent; // Ripristina il precedente pulsante
            }

            selectedButtonUnderGrupElemento = button;
            selectedButtonUnderGrupElemento.BorderBrush = Brushes.Black; // Evidenzia il pulsante selezionato



            //if (ListLaDatiElementov.Children[i] is TextBlock item)
            //{

            //}
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


            // var _list = new Lib_JSON_CRUD_3<InfoElementi>(filePathColori);
            string GruppoElementoSelezionato = "";
            foreach (var index in elementiPrincipali)
            {

                var el = list.FindElement(versione, index, c => c.Mod_LED_Fan == Dispositivo.ModLED_Fan);

                if (el != null)
                {
                    if (SerialPort.IsOpen)
                    {
                        GruppoElementoSelezionato = index;
                        break;
                    }
                }
                else
                {
                    GruppoElementoSelezionato = "ErElemento";
                }

            }



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
                    Tag = nome,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Cursor = System.Windows.Input.Cursors.Hand,
                    ToolTip = new Label { Content = nome },
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Transparent // Nessun bordo iniziale
                    //    Style = (Style)FindResource("IconButtonsForms") // Stile preso dalle risorse
                };

                // btn.SetResourceReference(Button.ContentProperty, "IconaUSBConnetti");


                btn.Click += BtnGruppiElementi_Click;

                StackPanelContainer.Children.Add(btn); // Aggiunge il bottone al contenitore


                if (nome == GruppoElementoSelezionato) // Se il pulsante è "Red", lo selezioniamo di default
                {
                    SelectButtonGrupElementi(btn);
                }
            }
        }
        private void BtnGruppiElementi_Click(object sender, RoutedEventArgs e)
        {
            if (SerialPort.IsOpen)
            {
                string versione = Dispositivo.Get_NomeDispositivo();
                string gruppo = GetNomeElemento(versione);
                if (sender is Button btn)
                {
                    string Newcategoria = btn.Name;
                    // Usa Lib_JSON_CRUD con la classe Ventola
                    var list = new Lib_JSON_CRUD_3<InfoElementi>(filePathElementi);
                    // Stampare gli elementi principali
                    var elementiPrincipali = list.GetElementiPrincipali(versione);
                    // Ottieni il contenuto degli elementi per la categoria corrente
                    var _list = list.GetContenutoElemento(versione, Newcategoria);
                    if (Newcategoria != gruppo)
                    {
                        Dispositivo.ModLED_Fan = _list[0].Mod_LED_Fan;
                        ClearTextBlock();
                        ElementoDato();
                        GenerateAniamzioneButtons();
                        SelectButtonGrupElementi(btn);
                        UI_dati();
                        TrackCommand();
                        GestioneLuminosità("", Dispositivo.LumLED[Dispositivo.ModLED_Fan], false);

                    }
                }
            }
        }
        // Grafica Selezione Colore
        private Button selectedButtonGrup = null;
        private void SelectButtonGrupElementi(Button button)
        {
            if (selectedButtonGrup != null)
            {
                selectedButtonGrup.BorderBrush = Brushes.Transparent; // Ripristina il precedente pulsante
            }
            selectedButtonGrup = button;
            selectedButtonGrup.BorderBrush = Brushes.Black; // Evidenzia il pulsante selezionato
        }



        //****
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
                    Tag = colors[i],
                    Background = new SolidColorBrush(buttoncolor),
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Transparent // Nessun bordo iniziale
                };

                button.Click += BtnColor_Click;

                if (i < 4)
                    Color0.Children.Add(button);
                else
                    Color1.Children.Add(button);


                if (colorsCode[i] == Dispositivo.ColoreLED[Dispositivo.ModLED_Fan]) // Se il pulsante è "Red", lo selezioniamo di default
                {
                    SelectButton(button);
                }
            }
        }
        // Crea bottoni Animazione
        private void GenerateAniamzioneButtons()
        {
            List<string> colors = new List<string>();
            List<int> colorsCode = new List<int>();

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
                    Tag = colors[i],
                    // Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[i])),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Transparent // Nessun bordo iniziale
                };

                button.Click += BtnAnimazioni_Click;

                if (i < 3)
                    Animazione0.Children.Add(button);
                else
                    Animazione1.Children.Add(button);

                if (colorsCode[i] == Dispositivo.ColoreLED[Dispositivo.ModLED_Fan]) // Se il pulsante è "Red", lo selezioniamo di default
                {
                    SelectButton(button);
                }

                // Nascondi Animazioni se è in modalità siongolo elemento
                if (Dispositivo.ModLED_Fan != 0)
                {
                    break;
                }
            }
        }

        // Al Click
        private void BtnColor_Click(object sender, RoutedEventArgs e)
        {
            string categoria = "Colori";

            if (sender is Button btn)
            {

                var _list = new Lib_JSON_CRUD_3<Colori>(filePathColori);

                var el = _list.FindElement("", categoria, c => c.Nome == btn.Tag.ToString());

                if (el != null)
                {
                    if (SerialPort.IsOpen)
                    {
                        Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = el.Colore;
                        Dispositivo.Saturazione[Dispositivo.ModLED_Fan] = el.Saturazione;
                        Dispositivo.LumLED[Dispositivo.ModLED_Fan] = GestioneLuminosità(el.Nome, Dispositivo.LumLED[Dispositivo.ModLED_Fan], true);

                        SelectButton(btn);
                        UI_dati();
                    }
                }
                else
                {

                }

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
                        Dispositivo.LumLED[Dispositivo.ModLED_Fan] = GestioneLuminosità(el.Nome, Dispositivo.LumLED[Dispositivo.ModLED_Fan], true);
                        SelectButton(btn);
                        UI_dati();
                    }
                }
                else
                {

                }

            }

        }
        //****




        // Grafica Selezione Colore
        private Button selectedButton = null;
        private void SelectButton(Button button)
        {
            if (selectedButton != null)
            {
                selectedButton.BorderBrush = Brushes.Transparent; // Ripristina il precedente pulsante
            }

            selectedButton = button;
            selectedButton.BorderBrush = Brushes.Black; // Evidenzia il pulsante selezionato



            //if (ListLaDatiElementov.Children[i] is TextBlock item)
            //{

            //}
        }






        private int GestioneLuminosità(string Ncolore, int LumLED, bool tipo)
        {
            if (Ncolore == "Spento")
            {
                //   Dispositivo.LumLED[Dispositivo.ModLED_Fan] = 0;
                TrackLuminosità.IsEnabled = false;
                TrackLuminosità.Value = TrackLuminosità.Minimum;
                return 0;
            }
            // else if (Dispositivo.LumLED[Dispositivo.ModLED_Fan] == 0)
            // {
            //   Dispositivo.LumLED[Dispositivo.ModLED_Fan] = 128;
            if (tipo)
            {
                LumLED = (LumLED > 0) ? LumLED : 128;
                //TrackLuminosità.Value = LumLED;
            }
            else
            {
                LumLED = (LumLED > 0) ? LumLED : ((LumLED != 0) ? 128 : 0);
                //TrackLuminosità.Value = LumLED;

            }
            TrackLuminosità.Value = (LumLED >= TrackLuminosità.Minimum) ? LumLED : TrackLuminosità.Minimum;
            TrackLuminosità.IsEnabled = (LumLED == 0) ? false : true;
            return LumLED;
            //   return 128;
            //}

        }




        private void TrackVelocità_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Dispositivo.StatoConnessione && UI_Load)
            {
                Dispositivo.FanSpeed[Dispositivo.ModLED_Fan < 5 ? Dispositivo.ModLED_Fan : 0] = (int)TrackVelocità.Value;
                UI_dati();
            }
        }

        private void TrackLuminosità_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Dispositivo.StatoConnessione && UI_Load && Dispositivo.LumLED[Dispositivo.ModLED_Fan] != 0)
            {
                Dispositivo.LumLED[Dispositivo.ModLED_Fan] = (int)TrackLuminosità.Value;
                UI_dati();
            }
        }





    }
}
