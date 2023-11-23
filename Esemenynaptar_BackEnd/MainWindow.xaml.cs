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

namespace Esemenynaptar_BackEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EventService service = new EventService();
        public MainWindow()
        {
            InitializeComponent();
            eventsTable.ItemsSource = service.GetAll();
        }

        private void HozzaAd_Click(object sender, RoutedEventArgs e)
        {
            EventForm personForm = new EventForm();
            personForm.Closed += (_, _) =>
            {
                eventsTable.ItemsSource = service.GetAll();
            };
            personForm.ShowDialog();
        }

        private void Torles_Click(object sender, RoutedEventArgs e)
        {
            Event selected = eventsTable.SelectedItem as Event;
            if (selected == null)
            {
                MessageBox.Show("Törléshez előbb válasszon ki elemet!");
                return;
            }
            MessageBoxResult dialogResult = MessageBox.Show($"Biztos törölni szeretnéd az alábbi eseményt: {selected.Nev} ?", "Törlés", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                if (service.Delete(selected))
                {
                    MessageBox.Show("Sikeres törlés!");
                }
                else
                {
                    MessageBox.Show("Hiba történt a törlés során!");
                }
                eventsTable.ItemsSource = service.GetAll();
            }
        }

        private void Modositas_Click(object sender, RoutedEventArgs e)
        {
            Event selected = eventsTable.SelectedItem as Event;
            if (selected == null)
            {
                MessageBox.Show("Módosításhoz előbb válasszon ki elemet!");
                return;
            }

            EventForm personForm = new EventForm(selected);
            personForm.Closed += (_, _) =>
            {
                eventsTable.ItemsSource = service.GetAll();

            };
            personForm.ShowDialog();
        }
    }
}
