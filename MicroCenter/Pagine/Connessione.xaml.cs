using MicroCenter.Classi;
using System.IO.Ports;
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


        private SerialPort _serialPort;
        public string com_Name_set;


        public Connessione()
        {
            InitializeComponent();

            _ = LoadAvailableCH340PortsAsync();
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
            _serialPort = new SerialPort
            {
                PortName = com_Name_set,
                BaudRate = 115200,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeout = 500,
                WriteTimeout = 500
            };
            _serialPort.DataReceived += SerialPort_DataReceived;
            _serialPort.Open();
            //_serialPort.Close();
        }

        private void SerialPortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            com_Name_set = SerialPortComboBox.SelectedItem.ToString();
        }




        SerialParser EncodeSeralData = new SerialParser();

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // Leggi i dati dalla seriale
                //string data = _serialPort.ReadExisting().ToString();

                // Legge i dati dalla seriale
                var SerialDatiRicevuto = _serialPort.ReadLine().ToString();
               
                var ParsData = EncodeSeralData.ParseString(SerialDatiRicevuto);


                // Aggiorna la UI con i dati ricevuti
                Dispatcher.Invoke(() =>
                {
                    if(TxSerialText.Text == "SerialPort")
                    TxSerialText.Text = ParsData[0][4].ToString();
                });
            }
            catch (Exception ex)
            {

            }
        }






    }
}
