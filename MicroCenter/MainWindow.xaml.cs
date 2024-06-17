using MicroCenter.Pagine;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MicroCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            Close();
        }

        //Nascondi Finestra
        private void btnNascondi_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Minimizza la finestra
        }


        //Visualiza le Impostazioni
        private void btnImpostazioni_Click(object sender, RoutedEventArgs e)
        {
           // frameContent.Padding = new Thickness(10);
            frameContent.Navigate(new Impostazioni());
        }
        //Visualiza la Connessione
        private void btnConnessione_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Connessione());
        }
        //Visualiza Set Arduino 
        private void btnArduino_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Arduino());
        }
    }


}