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
using System.Windows.Shapes;

namespace Esemenynaptar_BackEnd
{
    /// <summary>
    /// Interaction logic for EventForm.xaml
    /// </summary>
    public partial class EventForm : Window
    {
        EventService service = new EventService();
        Event esemeny;
        public EventForm()
        {
            InitializeComponent();
        }

        public EventForm(Event esemeny)
        {
            InitializeComponent();
            this.esemeny = esemeny;
            LoadEvent();
            buttonAdd.Visibility = Visibility.Collapsed;
            buttonUpdate.Visibility = Visibility.Visible;
        }

        private void LoadEvent()
        {
            try
            {
                nameInput.Text = this.esemeny.Nev;
                detailsInput.Text = this.esemeny.Reszletek;
                datumInput.Text = this.esemeny.Datum;
                egeszNaposInput.IsChecked = this.esemeny.EgeszNapos;
                idoInput.Text = this.esemeny.Ido;
                prioritasInput.Text = this.esemeny.Prioritas;
                emlekeztetoInput.Text = this.esemeny.Emlekezteto;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Event esemeny = CreateEventFromFields();
                Event ujEsemeny = service.Add(esemeny);
                if (ujEsemeny.ID != 0)
                {
                    MessageBox.Show("Az adatfelvétel sikeres volt!");
                    nameInput.Text = "";
                    detailsInput.Text = "";
                    datumInput.Text = "";
                    egeszNaposInput.IsChecked = false;
                    idoInput.Text = "";
                    prioritasInput.Text = "";
                    emlekeztetoInput.Text = "";
                }
                else
                {
                    MessageBox.Show("Hiba történt a felvétel során!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Event esemeny = CreateEventFromFields();
                Event updated = service.Update(this.esemeny.ID, esemeny);
                if (updated.ID != 0)
                {
                    MessageBox.Show("Sikeres módosítás!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hiba történt a módosítás során!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private Event CreateEventFromFields()
        {
            string nev = "";
            string reszletek = "";
            DateOnly datum;
            bool egeszNapos = false;
            TimeOnly ido;
            string nincsIdo = "";
            string prioritas = "";
            DateOnly emlekezteto;
            string nincsEmlekezteto = "";
            try
            {
                nev = nameInput.Text.Trim();
                reszletek = detailsInput.Text.Trim();
                prioritas = prioritasInput.Text.Trim();
                string[] datumTomb = datumInput.Text.Trim().Split('/');
                if (datumInput.Text.Trim() != "")
                {
                    datum = new DateOnly(int.Parse(datumTomb[2]), int.Parse(datumTomb[0]), int.Parse(datumTomb[1]));
                }
                egeszNapos = Convert.ToBoolean(egeszNaposInput.IsChecked);
                string[] datumIdo = idoInput.Text.Trim().Split(':');
                if (idoInput.Text.Trim() != "")
                {
                    ido = new TimeOnly(int.Parse(datumIdo[0]), int.Parse(datumIdo[1]));
                }
                string[] datumEmlekezteto = emlekeztetoInput.Text.Trim().Split('/');
                if (emlekeztetoInput.Text.Trim() != "")
                {
                    emlekezteto = new DateOnly(int.Parse(datumEmlekezteto[2]), int.Parse(datumEmlekezteto[0]), int.Parse(datumEmlekezteto[1]));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Rossz formátumban adta meg az adatokat!");
            }

            if (string.IsNullOrEmpty(nev))
            {
                throw new Exception("A név kitöltése kötelező!");
            }
            if (string.IsNullOrEmpty(datumInput.Text))
            {
                throw new Exception("A dátum kitöltése kötelező!");
            }
            //else if (string.IsNullOrEmpty(idoInput.Text))
            //{
            //    idoInput.Text = "9:00";
            //    ido = new TimeOnly(9,00);
            //}
            Event esemeny = new Event();
            esemeny.Nev = nev;
            esemeny.Reszletek = reszletek;
            esemeny.Datum = datum.ToString();
            esemeny.EgeszNapos = egeszNapos;
            esemeny.Ido = ido.ToString();
            esemeny.Prioritas = prioritas;
            if (emlekeztetoInput.Text.Trim() != "")
            {
                esemeny.Emlekezteto = emlekezteto.ToString();
            }
            else
            {
                esemeny.Emlekezteto = nincsEmlekezteto;
            }
            if (idoInput.Text.Trim() != "")
            {
                esemeny.Ido = ido.ToString();
            }
            else
            {
                esemeny.Ido = nincsIdo;
            }
            return esemeny;
        }

        private void egeszNaposInput_Checked(object sender, RoutedEventArgs e)
        {
            idoInput.Text = "";
        }
    }
}
