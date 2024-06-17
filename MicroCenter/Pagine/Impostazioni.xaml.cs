using System;
using System.Collections.Generic;
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
using MicroCenter.Temi;

namespace MicroCenter.Pagine
{
    /// <summary>
    /// Logica di interazione per Impostazioni.xaml
    /// </summary>
    public partial class Impostazioni : Page
    {

        public Impostazioni()
        {
            InitializeComponent();
        }





        //Swich Imposta il Tema
        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            if (Themes.IsChecked == true)
                ControllerTemi.SetTheme(ControllerTemi.ThemeTypes.Dark);
            else
                ControllerTemi.SetTheme(ControllerTemi.ThemeTypes.Light);
        }




    }
}
