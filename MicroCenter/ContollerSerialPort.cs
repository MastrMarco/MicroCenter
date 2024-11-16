using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MicroCenter
{
    public class ContollerSerialPort
    {


        // Legge gli elementi Seriali USB connessi e da il Nome, Descrizione, Creatore, ....
        public static string? GetPortDescription(string portName)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(" + portName + ")%'"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        return obj["Caption"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine($"Errore nella lettura della descrizione per {portName}: {ex.Message}");
                MessageBox.Show(ex.Message);
            }

            return string.Empty;
        }


        // Legge tutte le porte seriali disponibbili e le visualizza
        //private async Task LoadAvailableCH340PortsAsync()
        //{
        //     SerialPortComboBox.Items.Clear();
        //    string[] portNames = SerialPort.GetPortNames();
        //    List<string> ch340Ports = new List<string>();

        //    await Task.Run(() =>
        //    {
        //        foreach (string portName in portNames)
        //        {
        //            string description = ContollerSerialPort.GetPortDescription(portName);

        //            // Verifica se la descrizione inizia con "USB-SERIAL CH340"
        //            if (description.StartsWith("USB-SERIAL CH340"))
        //            {
        //                ch340Ports.Add(portName); // Aggiungi solo il nome della porta alla lista temporanea
        //            }
        //        }
        //    });

        //    // Aggiorna la ComboBox con i risultati trovati dopo aver completato l'operazione in background
        //    foreach (string port in ch340Ports)
        //    {
        //        SerialPortComboBox.Items.Add(port);
        //    }

        //    if (SerialPortComboBox.Items.Count > 0)
        //        SerialPortComboBox.SelectedIndex = 0; // Se ci sono elementi, seleziona il primo


        //}






    }

}
