using MicroCenter.Pagine;
using System.IO.Ports;
using System.Windows;
using System.Windows.Input;

using System;
using System.Management;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security.RightsManagement;
using MicroCenter.Lingue;
using MicroCenter.Finestre;
using MicroCenter.Pagine.HubPC;


namespace MicroCenter
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            btnConnessione_Click(null, null);

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (Connessione._serialPort != null)
            {
                if (Connessione._serialPort.IsOpen)
                {
                    Connessione._serialPort.Close();
                }
            }

            if (Connessione.window2 != null)
            {
                Connessione.window2.Close(); // Chiude Window2 se è aperta
            }

        }











        //Sposta Finestra
        private void BarraSuperiore_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //Chiudi Finestra
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();

            Connessione.Disconnessione();

            Close();
        }

        //Nascondi Finestra
        private void btnNascondi_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Minimizza la finestra
        }




        private Impostazioni? ImpostazioniPage = null;
        private Connessione? connessionePage = null;
        //private Arduino? ArduinoPage = null;
        private Elementi? ArduinoPage = null;

        //Visualiza le Impostazioni
        private void btnImpostazioni_Click(object sender, RoutedEventArgs e)
        {
            // frameContent.Padding = new Thickness(10);
            if (ImpostazioniPage == null)
            {
                ImpostazioniPage = new Impostazioni();
            }
            frameContent.Navigate(ImpostazioniPage);
            LabInfoPagine.Content = Lingua.impostazioni;
            LabToolImpostazioni.Content = Lingua.impostazioni;
        }



        //Visualiza la Connessione
        private void btnConnessione_Click(object sender, RoutedEventArgs e)
        {
            if (connessionePage == null)
            {
                connessionePage = new Connessione();
            }

            frameContent.Navigate(connessionePage);

            LabInfoPagine.Content = Lingua.fConnessione;
            LabToolTipConnessione.Content = Lingua.fConnessione;
        }
        //Visualiza Set Arduino 
        private void btnArduino_Click(object sender, RoutedEventArgs e)
        {
            if (ArduinoPage == null)
            {
                // ArduinoPage = new Arduino();
                ArduinoPage = new Elementi();
            }
            frameContent.Navigate(ArduinoPage);
            LabInfoPagine.Content = Lingua.fArduino;
            LabToolTipArduino.Content = Lingua.fArduino;
        }


    }
}