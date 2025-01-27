using MicroCenter.Classi;
using MicroCenter.Lingue;
using System.Globalization;
using System.IO.Ports;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MicroCenter.Pagine
{
    /// <summary>
    /// Logica di interazione per Connessione.xaml
    /// </summary>
    public partial class Connessione : Page
    {
        public SerialPort _serialPort;
        public string com_Name_set;

        public ArduHubFan Dispositivo = new ArduHubFan();


        private DispatcherTimer _timer;
        private TimeSpan _time;




        public Connessione()
        {
            InitializeComponent();

            _ = LoadAvailableCH340PortsAsync();



            InitializeTimer();
            _timer.Start();




            //Visualizza l'impostazione Memorizata e la esegue
                SetLinguaPaginaTitoli(Set_Lingua());  
        }












        private async Task LoadAvailableCH340PortsAsync()
        {
            SerialPortComboBox.Items.Clear();
            string[] portNames = SerialPort.GetPortNames();
            List<string> ch340Ports = new List<string>();

            await Task.Run(() =>
            {
                foreach (string portName in portNames)
                {
                    string? description = ContollerSerialPort.GetPortDescription(portName);

                    // Verifica se la descrizione inizia con "USB-SERIAL CH340"
                    if (description.StartsWith("USB-SERIAL CH340"))
                    {
                        ch340Ports.Add(portName); // Aggiungi solo il nome della porta alla lista temporanea
                    }
                }
            });

            // Aggiorna la ComboBox con i risultati trovati dopo aver completato l'operazione in background
            foreach (string port in ch340Ports)
            {
                SerialPortComboBox.Items.Add(port);
            }

            if (SerialPortComboBox.Items.Count > 0)
                SerialPortComboBox.SelectedIndex = 0; // Se ci sono elementi, seleziona il primo


        }



        private void btnIconaConnetti(object sender, RoutedEventArgs e)
        {
            if (_serialPort == null)
            {
                _serialPort = new SerialPort
                {
                    PortName = com_Name_set,
                    BaudRate = 115200,
                    Parity = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None,
                    //WriteTimeout = 500,
                    Encoding = System.Text.Encoding.UTF8, // Usa l'encoding UTF-8 per supportare caratteri speciali
                    NewLine = "\n", // Definisci il carattere di fine stringa (ad esempio "\n")
                    ReadTimeout = 2000,

                };
                //  _serialPort.DataReceived += SerialPort_DataReceived;
                _serialPort.Open();
            }
            else
            {
                _serialPort.Close();
                _serialPort = null;
            }
        }

        private void SerialPortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            com_Name_set = SerialPortComboBox.SelectedItem.ToString();
        }



        int lgo;


        private void InitializeTimer()
        {
            _time = TimeSpan.Zero;

            _timer = new DispatcherTimer
            {
                // Interval = TimeSpan.FromSeconds(1)
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _timer.Tick += Timer_Tick;
        }



        private void Timer_Tick(object? sender, EventArgs e)
        {
            //_time = _time.Add(TimeSpan.FromSeconds(1));
            _time = _time.Add(TimeSpan.FromMilliseconds(10));
            // TxSerialText.Text = _time.ToString(@"hh\:mm\:ss");


            if (_serialPort != null)
            {

                if (_serialPort.IsOpen)
                {
                    // Leggi i dati ricevuti
                    string serialDatiRicevuto = _serialPort.ReadLine();
                    Dispositivo.SetSrialToList(serialDatiRicevuto);
                    TxSerialText.Text = serialDatiRicevuto;
                    lgo = Dispositivo.Get_CountListSeriale();


                }
                else
                {
                    // Porta non aperta: gestisci l'errore
                    MessageBox.Show("La porta seriale non è aperta.");
                    _timer.Stop();
                }

            } else
            {
                Dispositivo.StatoConnessione = false;
            }






            Application.Current.Dispatcher.Invoke(() =>
            {

                if (lgo == 13)
                {

                    //LaV1.Text = parsData[1][1].ToString() + " V";
                    //LaV2.Text = parsData[1][2].ToString() + " V";
                    //LaVref.Text = parsData[1][6].ToString() + " V";


                    LabNomeDispositivo.Content = Dispositivo.Get_NomeDispositivo();
                    LaVerSoft.Text = "v" + Dispositivo.Versione;
                    LaVerTipo.Text = Dispositivo.Get_TipoVersine();
                    LaSoC.Text = Dispositivo.Get_TipoSoC();
                    LaTempScheda.Text = Dispositivo.TempDS.ToString() + " °C";
                    LaVref.Text = Dispositivo.VAREF.ToString("0.00") + " V";



                    //SerialPortComboBox.
                    LaConnessione.Content = ConnessioneDispositivo(Dispositivo.StatoConnessione);

                }


            });



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




    }
}
