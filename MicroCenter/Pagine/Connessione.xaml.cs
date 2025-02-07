using MicroCenter.Classi;
using MicroCenter.Finestre;
using MicroCenter.Lingue;
using System.Globalization;
using System.IO.Ports;
using System.Reflection.Metadata;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Timers;

namespace MicroCenter.Pagine
{
    /// <summary>
    /// Logica di interazione per Connessione.xaml
    /// </summary>
    public partial class Connessione : Page
    {
        public static SerialPort _serialPort = new SerialPort();
        public string com_Name_set;

        public SerialParser FunzioniSeriali = new SerialParser();
        public static ArduHubFan Dispositivo = new ArduHubFan();





        // Timer che non bloccano la UI e vengono avviati dopo la connesione seriale e blocati alla disconnessione
        public static bool isRunning = false;




        // Setup Pagina Connessione
        public Connessione()
        {
            InitializeComponent();

            // Aggiorna la ComboBox con i risultati trovati dopo aver completato l'operazione in background
            ListaCOM();

            //Visualizza l'impostazione Memorizata e la esegue
            SetLinguaPaginaTitoli(Set_Lingua());

        }




        // Ricerca a Agiornameto lista Porte Seriali con Filtro
        public async void ListaCOM()
        {
            // Aggiorna la ComboBox con i risultati trovati dopo aver completato l'operazione in background
            SerialPortComboBox.Items.Clear();
            // foreach (string port in FunzioniSeriali.ListaPorteConFiltro(""))
            foreach (string port in await FunzioniSeriali.ListaPorteConFiltroAsync())
            {
                SerialPortComboBox.Items.Add(port);
            }
            if (SerialPortComboBox.Items.Count > 0)
            {
                SerialPortComboBox.SelectedIndex = 0; // Se ci sono elementi, seleziona il primo
            }
        }

        // Bottone Connetti Disconnetti
        private void btnIconaConnetti(object sender, RoutedEventArgs e)
        {
            // if (_serialPort == null)
            if (!_serialPort.IsOpen)
            {
                _serialPort = new SerialPort
                {
                    BaudRate = 115200,
                    DataBits = 8,
                    DiscardNull = false,
                    DtrEnable = false,
                    Handshake = Handshake.None,
                    Parity = Parity.None,
                    ParityReplace = 63,
                    PortName = com_Name_set,
                    ReadBufferSize = 4096,
                    ReadTimeout = 5000,
                    ReceivedBytesThreshold = 1,
                    RtsEnable = false,
                    StopBits = StopBits.One,
                    WriteBufferSize = 2048,
                    WriteTimeout = 5000,
                    Encoding = System.Text.Encoding.UTF8, // Usa l'encoding UTF-8 per supportare caratteri speciali
                    NewLine = "\n", // Definisci il carattere di fine stringa (ad esempio "\n")

                };
                //_serialPort.DataReceived += SerialPort_DataReceived;
                _serialPort.Open();


                if (!isRunning)
                {
                    StartTimer_Click(null, null);
                    UI_Timer(null, null);
                }


            }
            else
            {
                Disconnessione();
                if (btnConnetti.Content == FindResource("IconaUSBDisconnetti"))
                {
                    btnConnetti.Content = FindResource("IconaUSBConnetti");
                }
                LaConnessione.Content = ConnessioneDispositivo(Dispositivo.StatoConnessione);
                LabToolTipConnetti.Content = LaConnessione.Content;
            }
        }

     
        public static void Disconnessione()
        {
            isRunning = false;
            Dispositivo.StatoConnessione = false;
            _serialPort.Close();
       
        }

        // Aggiorna Lista POrte COM / Seriali
        private void SerialPortComboBox_DropDownOpened(object sender, EventArgs e)
        {
            ListaCOM();
        }

        // Imposta il Nome COM della Porta Selezionata
        private void SerialPortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SerialPortComboBox.Items.Count > 0)
            {
                com_Name_set = SerialPortComboBox.SelectedItem.ToString();
            }
        }



        // Timer Serial Read and Write
        private async void StartTimer_Click(object sender, RoutedEventArgs e)
        {
            string DatiTX = "";

            isRunning = true;
            while (isRunning)
            {

                if (_serialPort.IsOpen)
                {
                    // Leggi i dati ricevuti
                    string serialDatiRicevuto = "";
                    //_serialPort.DiscardInBuffer();
                    try { serialDatiRicevuto = _serialPort.ReadLine(); } catch (Exception ex) { }
                    Dispositivo.SetSrialToList(serialDatiRicevuto);
                    // TxSerialText.Text = DatiTX;
                    // lgo = Dispositivo.Get_CountListSeriale();

                    La4.Text = Dispositivo.Get_CountListSeriale().ToString();


                    //Stringa da inviare non Pronta con Abilitazione per la comunicazione
                    if ((Dispositivo.StatoConnessione) && (serialDatiRicevuto.Length > 100))
                    {
                        DatiTX = (
                        "200" + ";" + //0
                        "" + ";" + //1
                        "" + ";" + //2
                        "" + ";" + //3
                        "" + ";" + //4
                        "" + ";" + //5
                        "" + ";" + //6
                        "" + ";" + //7
                        "" + ";" + //8
                        "" + ";" + //9
                        "" + ";" + //10
                        "" + ";" + //11
                        "" + ";" + //12
                        "" + ";" + //13
                        "" + ";" + //14
                        "" + ";" + //15
                        "" + ";"   //16
                        );
                        _serialPort.WriteLine(DatiTX);
                    }
                    //Stringa da inviare Pronta con Abilitazione per la comunicazione
                    else if ((Dispositivo.StatoConnessione) && (serialDatiRicevuto.Length < 100))
                    {
                        DatiTX = (
                        "200" + ";" + //0
                        Dispositivo.ModLED_Fan.ToString() + ";" + //1
                        Dispositivo.LumLED[Dispositivo.ModLED_Fan].ToString() + ";" + //2
                        Dispositivo.ColoreLED[Dispositivo.ModLED_Fan].ToString() + ";" + //3
                        Dispositivo.Saturazione[Dispositivo.ModLED_Fan].ToString() + ";" + //4
                        Dispositivo.FanSpeed[Dispositivo.ModLED_Fan].ToString() + ";" + //5
                        "" + ";" + //6
                        "" + ";" + //7
                        "" + ";" + //8
                        "" + ";" + //9
                        "" + ";" + //10
                        "" + ";" + //11
                        "" + ";" + //12
                        "" + ";" + //13
                        "" + ";" + //14
                        "" + ";" + //15
                        "" + ";"   //16
                        );
                        _serialPort.WriteLine(DatiTX);
                    }

                }
                else
                {
                    // Porta non aperta: gestisci l'errore o disconnessa
                    MessageBox.Show("La porta seriale non è aperta.");
                    Disconnessione();
                }


                await Task.Delay(40); // millisecondi
            }
        }




        // Timer Serial Read and Write
        private async void UI_Timer(object sender, RoutedEventArgs e)
        {
            int lgo;
            isRunning = true;
            while (isRunning)
            {

                if (_serialPort.IsOpen)
                {
                    lgo = Dispositivo.Get_CountListSeriale();

                    Application.Current.Dispatcher.Invoke(() =>
                    {

                        if (lgo == 13 || lgo == 4)
                        {

                            LabNomeDispositivo.Content = Dispositivo.Get_NomeDispositivo();
                            LaVerSoft.Text = "v" + Dispositivo.Versione;
                            LaVerTipo.Text = Dispositivo.Get_TipoVersine();
                            LaSoC.Text = Dispositivo.Get_TipoSoC();
                            LaTempScheda.Text = Dispositivo.TempDS.ToString() + " °C";
                            LaVref.Text = Dispositivo.VAREF.ToString("0.00") + " V";



                            LaConnessione.Content = ConnessioneDispositivo(Dispositivo.StatoConnessione);
                            LabToolTipConnetti.Content = LaConnessione.Content;

                            if (btnConnetti.Content == FindResource("IconaUSBConnetti"))
                            {
                                btnConnetti.Content = FindResource("IconaUSBDisconnetti");
                            }
                            TxSerialText.Text = Dispositivo.RX;

                        }


                    });


                }


                await Task.Delay(100); // millisecondi
            }
        }







        //Traduzione 

        public string Set_Lingua()
        {
            switch (Properties.Settings.Default.Lingua)
            {
                case "Italiano":
                    return "";

                case "English":
                    return "en";

                default:
                    return "";
            }
        }

        public void SetLinguaPaginaTitoli(string linguaSet)
        {
            Lingua.Culture = new CultureInfo(linguaSet);


            LabTinfoSoft.Content = Lingua.nomeDispositivo;
            LabTSerialPort.Content = Lingua.selezionaDispositivo;
            LabTConnessione.Content = Lingua.statoCollegamento;
            LaTSerialData.Text = Lingua.datiSeriali;
            LaConnessione.Content = ConnessioneDispositivo(Dispositivo.StatoConnessione);
        }

        private string ConnessioneDispositivo(bool stato)
        {
            Lingua.Culture = new CultureInfo(Set_Lingua());
            if (!stato)
            {
                return Lingua.statoConnessioneNo;
            }
            else
            {
                return Lingua.statoConnessioneSi;
            }
        }


        public static InfoSerialData window2;

        private void btnInfoSerialData(object sender, RoutedEventArgs e)
        {
            if (window2 == null) // Evita di aprire più finestre
            {
                window2 = new InfoSerialData();
                window2.Show();
            }

        }
    }
}
