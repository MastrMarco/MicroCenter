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

        public Elementi()
        {
            InitializeComponent();
            CreaBottoniDinamici();




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















    }
}
