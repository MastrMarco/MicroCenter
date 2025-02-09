using MicroCenter.Classi;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
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

namespace MicroCenter.Pagine.HubPC
{
    /// <summary>
    /// Logica di interazione per Elementi.xaml
    /// </summary>
    public partial class Elementi : Page
    {
        //Crea un Alias della Porta Seriale
        public static SerialPort SerialPort => Connessione._serialPort;
        //Crea un Alias dei Dati dell' HUB
        public static ArduHubFan Dispositivo => Connessione.Dispositivo;


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
                UI_Load = true;
            }
        }







        private void CreaBottoniDinamici()
        {
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



        private void GenerateColorButtons()
        {
            List<string> colors = new List<string>
            {
                "Red", "Green", "Blue", "Yellow",
                "Orange", "Magenta", "Cyan", "White"
            };

            for (int i = 0; i < colors.Count; i++)
            {
                Button button = new Button
                {
                    Name = "Btn_" + i.ToString(),
                    Height = 15,
                    Width = 24,
                    Margin = new Thickness(10),
                    ToolTip = new Label { Content = $"Bottone {colors[i]}" },
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[i]))
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

            if (sender is Button btn)
            {
                //  MessageBox.Show($"Hai cliccato: {btn.Name}");


                if (SerialPort.IsOpen)
                {
                    switch (btn.Name)
                    {
                        case "Btn_0":
                            Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 0; // Rosso
                            break;

                        case "Btn_1":
                            Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 171; // Verde
                            break;

                        case "Btn_2":
                            Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 341; // Blu
                            break;

                        case "Btn_3":
                            Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 85; // Giallo
                            break;
                        case "Btn_4":
                            Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 20; // Arancione
                            break;

                        case "Btn_5":
                            Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 427; // Fucsia
                            break;

                        case "Btn_6":
                            Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 256; // Ciano
                            break;

                        case "Btn_7":
                            Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 9;
                            break;


                        default:

                            break;

                    }
                }

            }

        }



        private void GenerateAniamzioneButtons()
        {
            List<string> colors = new List<string>
            {
                "Red", "Green", "Blue",
                "Yellow", "Orange", "Magenta"
            };

            for (int i = 0; i < colors.Count; i++)
            {
                Button button = new Button
                {
                    Name = "Btn_" + i.ToString(),
                    Height = 35,
                    Width = 35,
                    Margin = new Thickness(10),
                    ToolTip = new Label { Content = $"Bottone {colors[i]}" },
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[i])),
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

            if (sender is Button btn)
            {
                // MessageBox.Show($"Hai cliccato: {btn.Name}");


                if (SerialPort.IsOpen)
                {
                    if (Dispositivo.ModLED_Fan == 0)
                    {
                        switch (btn.Name)
                        {
                            case "Btn_0":
                                Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 600; // 
                                break;

                            case "Btn_1":
                                Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 601; // 
                                break;

                            case "Btn_2":
                                Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 602; // 
                                break;

                            case "Btn_3":
                                Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 603; // 
                                break;
                            case "Btn_4":
                                Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 604; // 
                                break;

                            case "Btn_5":
                                Dispositivo.ColoreLED[Dispositivo.ModLED_Fan] = 605; // 
                                break;

                            default:

                                break;

                        }
                    }
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
