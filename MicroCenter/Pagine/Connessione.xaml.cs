using MicroCenter.Collegamenti_WEB;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
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
using System.Management;

namespace MicroCenter.Pagine
{
    /// <summary>
    /// Logica di interazione per Connessione.xaml
    /// </summary>
    public partial class Connessione : Page
    {

        // MainWindow ClasseSerialPort = new MainWindow();
        // private MainWindow _MainWindow;

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























    }
}
