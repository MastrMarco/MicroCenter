using MicroCenter.Pagine;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
using System.Windows.Shapes;

namespace MicroCenter.Finestre
{
    /// <summary>
    /// Logica di interazione per InfoSerialData.xaml
    /// </summary>
    public partial class InfoSerialData : Window
    {
        public InfoSerialData()
        {
            InitializeComponent();
            CreateListBox();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Connessione.window2 = null;
        }





        private void CreateListBox()
        {
            // Creazione dinamica del ListBox
            ListBox listBox = new ListBox
            {
                Margin = new Thickness(10)
            };

            // Popoliamo il ListBox con dati
            List<string> items = new List<string>();

            //foreach (var item in Connessione.Dispositivo.parsData[][]) {
            //    items.Add(item.ToString());
            //}
            if (Connessione.Dispositivo.parsData != null)
            {
                foreach (var row in Connessione.Dispositivo.parsData)
                {
                    foreach (var item in row)
                    {
                        items.Add(item);
                    }
                }
            }

            listBox.ItemsSource = items;

            // Aggiungiamo il ListBox al Grid principale
            MainGrid.Children.Add(listBox);
        }









    }
}
