using MicroCenter.Classi;
using System.IO.Ports;
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


        private SerialPort _serialPort;
        public string com_Name_set;



        private DispatcherTimer _timer;
        private TimeSpan _time;

        public Connessione()
        {
            InitializeComponent();

            _ = LoadAvailableCH340PortsAsync();



            InitializeTimer();
            _timer.Start();

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
                //WriteTimeout = 500,
                Encoding = System.Text.Encoding.UTF8, // Usa l'encoding UTF-8 per supportare caratteri speciali
                NewLine = "\n", // Definisci il carattere di fine stringa (ad esempio "\n")
                ReadTimeout = 20000,

            };
           //  _serialPort.DataReceived += SerialPort_DataReceived;
            _serialPort.Open();
            //_serialPort.Close();
        }

        private void SerialPortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            com_Name_set = SerialPortComboBox.SelectedItem.ToString();
        }





        int x;

        //public static class DecodeSeralData
        //{
        //    public static List<List<string>> ParseString(string input)
        //    {
        //        // Esempio di parsing: dividere i dati ricevuti per righe e colonne
        //        var rows = input.Split('\n'); // Suddivide in righe
        //        var result = new List<List<string>>();

        //        foreach (var row in rows)
        //        {
        //            var columns = row.Split(','); // Suddivide in colonne
        //            result.Add(columns.ToList());
        //        }

        //        return result;
        //    }
        //}


        public static class DecodeSeralData
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


        List<List<string>> parsData;
        int lgo;
        string h;
        private StringBuilder serialBuffer = new StringBuilder();

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {



            //if (_serialPort.IsOpen)
            //{
            //    // Leggi i dati ricevuti
            //    string serialDatiRicevuto = _serialPort.ReadLine();




            //    //// Parse dei dati ricevuti
            //    //var
            //    parsData = DecodeSeralData.ParseString(serialDatiRicevuto);


            //    h = serialDatiRicevuto.Length.ToString();
            //    lgo = parsData.Count;

            //    //// Invia i dati al thread dell'interfaccia utente
            //    //Application.Current.Dispatcher.Invoke(() =>
            //    //{
            //    //    // Aggiorna i controlli WPF
            //    //    // AggiornaUI(parsData);
            //    //});



            //}
            //else
            //{
            //    // Porta non aperta: gestisci l'errore
            //    MessageBox.Show("La porta seriale non è aperta.");
            //}
        }



        private void AggiornaUI(List<List<string>> ParsData)
        {
            //try
            //{
            // Assumi che ParsData[1][1] esista e aggiorna il controllo corrispondente
            //  if (ParsData[0][0].ToString() != null)
            //  {


            // Controlla che esistano almeno 2 righe e 2 colonne

            // Aggiorna il valore
            LaVerSoft.Text = ParsData[0][4].ToString() + " v";
            LaVerTipo.Text = ParsData[0][0].ToString();
            LaTempScheda.Text = ParsData[1][0].ToString() + " °C";

            LaV1.Text = ParsData[1][1].ToString() + " V";
            LaV2.Text = ParsData[1][2].ToString() + " V";
            LaVref.Text = ParsData[1][6].ToString() + " V";

            x++;
            TxSerialText.Text = x.ToString();



            //  }

            //}
            //catch (Exception ex)
            //{
            //    // Gestione degli errori (ad esempio: array fuori limite)
            //    MessageBox.Show($"Errore nell'elaborazione dei dati: {ex.Message}");
            //}
        }









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


        int g;

        private void Timer_Tick(object? sender, EventArgs e)
        {
            //_time = _time.Add(TimeSpan.FromSeconds(1));
            _time = _time.Add(TimeSpan.FromMilliseconds(100));
            // TxSerialText.Text = _time.ToString(@"hh\:mm\:ss");


            //  LaVerSoft.Text = lgo;
            //int? g = int.Parse(lgo);

            if (_serialPort != null)
            {

                if (_serialPort.IsOpen)
                {
                    // Leggi i dati ricevuti
                    string serialDatiRicevuto = _serialPort.ReadLine();
                    parsData = DecodeSeralData.ParseString(serialDatiRicevuto);
                    h = serialDatiRicevuto.Length.ToString();
                    lgo = parsData.Count;
                }
                else
                {
                    // Porta non aperta: gestisci l'errore
                    MessageBox.Show("La porta seriale non è aperta.");
                }

            }






            Application.Current.Dispatcher.Invoke(() =>
            { 

            if (lgo == 13)
            {
                LaVerSoft.Text = parsData[0][4].ToString() + " v";
                LaVerTipo.Text = parsData[0][0].ToString();
                LaTempScheda.Text = parsData[1][0].ToString() + " °C";

                LaV1.Text = parsData[1][1].ToString() + " V";
                LaV2.Text = parsData[1][2].ToString() + " V";
                LaVref.Text = parsData[1][6].ToString() + " V";


            }

            g++;

            TxSerialText.Text = g.ToString();

        });



        }





































    //SerialParser EncodeSeralData = new SerialParser();


    //int x;
    //private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    //{
    //    try
    //    {
    //        // Leggi i dati dalla seriale
    //        //string data = _serialPort.ReadExisting().ToString();

    //        // Legge i dati dalla seriale
    //        var SerialDatiRicevuto = _serialPort.ReadLine().ToString();

    //        var ParsData = EncodeSeralData.ParseString(SerialDatiRicevuto);


    //        // Aggiorna la UI con i dati ricevuti
    //        Dispatcher.Invoke(() =>
    //        {
    //          //  if (ParsData[0][0].ToString() != null)
    //          //  {
    //                LaVerSoft.Text = ParsData[0][4].ToString() + " v";
    //                LaVerTipo.Text = ParsData[0][0].ToString();
    //                LaTempScheda.Text = ParsData[1][0].ToString() + " °C";

    //                LaV1.Text = ParsData[1][1].ToString() + " V";
    //                LaV2.Text = ParsData[1][2].ToString() + " V";
    //                LaVref.Text = ParsData[1][6].ToString() + " V";


    //                x++;
    //                TxSerialText.Text = x.ToString();
    //          //  }
    //        });
    //    }
    //    catch (Exception ex)
    //    {


    //    }
    //}









}
}
