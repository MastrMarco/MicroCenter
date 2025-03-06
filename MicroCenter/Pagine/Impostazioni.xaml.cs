using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Globalization;
using MicroCenter.Temi;
using MicroCenter.Lingue;
using MicroCenter.Collegamenti_WEB;

namespace MicroCenter.Pagine
{
    /// <summary>
    /// Logica di interazione per Impostazioni.xaml
    /// </summary>
    public partial class Impostazioni : Page
    {

        WebLink SitiWeb = new WebLink(); //Chiama la classe dove si trovano Link di siti web


        public Impostazioni()
        {
            InitializeComponent();



            if (Properties.Settings.Default.TemaApp == "Bianco")
            {
                ControllerTemi.SetTheme(ControllerTemi.ThemeTypes.Light);
                Themes.Tag = Properties.Settings.Default.TemaApp;
            }
            else if (Properties.Settings.Default.TemaApp == "Scuro")
            {
                Themes.Tag = Properties.Settings.Default.TemaApp;
                ControllerTemi.SetTheme(ControllerTemi.ThemeTypes.Dark);


            }



            // Lingue ComboBox
            SetLingua.Items.Add("Italiano");
            SetLingua.Items.Add("English");

            //Visualizza l'impostazione Memorizata e la esegue
            if (Properties.Settings.Default.Lingua == "Italiano")
            {
                Lingue("");

            }
            else if (Properties.Settings.Default.Lingua == "English")
            {
                Lingue("en");
            }
            SetLingua.Text = Properties.Settings.Default.Lingua;
        }





        //Swich Imposta il Tema
        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            if (Themes.IsChecked == true)
            {
                ControllerTemi.SetTheme(ControllerTemi.ThemeTypes.Dark);
                Properties.Settings.Default.TemaApp = "Scuro";
                //Themes.Tag = "Scuro";
            }
            else
            {
                ControllerTemi.SetTheme(ControllerTemi.ThemeTypes.Light);
                Properties.Settings.Default.TemaApp = "Bianco";
                Themes.Tag = Properties.Settings.Default.TemaApp;
            }

            //MessageBox.Show("");
        }



        private void SetLingua_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            // Esegui uno switch in base alla selezione del ComboBox per cambiare la lingua dei testi
            switch (SetLingua.SelectedItem.ToString())
            {
                case "Italiano":
                    Lingue("");
                    break;
                case "English":
                    Lingue("en");
                    break;
                default:
                    Lingue("");
                    break;
            }

            //Memoriza l'Impostazione Lingua
            Properties.Settings.Default.Lingua = SetLingua.SelectedItem.ToString();
        }


        private void Lingue(string linguaSet)
        {
            Lingua.Culture = new CultureInfo(linguaSet);


            LabTema.Content = Lingua.tema;
            // LabToolTipTema.Content = Line.
            LabTemaInfo.Content = Lingua.modificailTema;

            LabNomeApp.Content = Lingua.nomeprogetto;
            LabToolTipGitHub.Content = Lingua.github;
            LabToolTipYouTube.Content = Lingua.youtube;
            LabToolTipTelegram.Content = Lingua.telegram;
            LabInfoCreatore.Content = Lingua.infoCreatore;

            LabLingiaApp.Content = Lingua.linguaApp;
            LabLingaAppInfo.Content = Lingua.modificaLingua;

            LabDonazioni.Content = Lingua.donazioni;
            LabToolTipKo_Fi.Content = Lingua.ko_fi;
            LabDonazioniInfo.Content = Lingua.supportoDonazione;

            LabInformazioni.Content = Lingua.info;
            LabInformazioniInfo.Content = Lingua.infoSorgente;


            //Reset Pagine
            MainWindow.connessionePage = null;
            MainWindow.ArduinoPage = null;
        }




        // Bottone GitHub Apertura Pagina Web
        private void btnIconaGitHub(object sender, RoutedEventArgs e)
        {
            OpenUrlInBrowser(SitiWeb.GitHub);
        }

        // Bottone YouTube Apertura Pagina Web
        private void btnIconaYouTube(object sender, RoutedEventArgs e)
        {
            OpenUrlInBrowser(SitiWeb.Youtube);
        }

        // Bottone Telegram Apertura Pagina Web
        private void btnIconaTelegram(object sender, RoutedEventArgs e)
        {
            OpenUrlInBrowser(SitiWeb.Telegram);
        }





        // Bottone Donazioni Apertura Pagina Web
        private void btnIconaDonazioni(object sender, RoutedEventArgs e)
        {
            OpenUrlInBrowser(SitiWeb.Donazioni);
        }


        // Apre la pagina WEB
        private void OpenUrlInBrowser(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Ensures it opens in the default browser
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open URL: {ex.Message}");
            }
        }







    }
}
