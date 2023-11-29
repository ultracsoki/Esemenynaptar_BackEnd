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
                dateInput.Text = this.esemeny.Datum.ToString();
                egeszNaposInput.IsChecked = this.esemeny.EgeszNapos;
                if (egeszNaposInput.IsChecked == false)
                {
                    idoInput.IsEnabled = true;
                }
                else
                {
					idoInput.IsEnabled = false;
				}
                idoInput.Text = this.esemeny.Ido;
                prioritasCombobox.Text = this.esemeny.Prioritas;
                emlekeztetoInput.Text = this.esemeny.Emlekezteto.ToString();
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
                    dateInput.SelectedDate = null;
                    dateInput.DisplayDate = DateTime.Today; ;
                    egeszNaposInput.IsChecked = false;
                    idoInput.Text = "";
                    prioritasCombobox.SelectedIndex = 0;
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

			}
}

        private Event CreateEventFromFields()
        {
            bool sikeres = true;
            string nev = "";
            string reszletek = "";
            string datum = "";
            bool egeszNapos = false;
            TimeOnly ido;
            string nincsIdo = "";
            string prioritas = "";
            string emlekezteto = "";
            try
            {
                nev = nameInput.Text.Trim();
                reszletek = detailsInput.Text.Trim();
                prioritas = prioritasCombobox.Text.Trim();
                string[] datumTomb = dateInput.Text.Trim().Split('/');
                if (dateInput.Text.Trim() != "")
                {
                    datum = dateInput.Text;
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
                    emlekezteto = emlekeztetoInput.Text;
                }
            }
            catch (Exception)
            {
                sikeres = false;
                MessageBox.Show("Rossz formátumban adta meg az adatokat!");
            }
            if (sikeres == true)
            {
				if (string.IsNullOrEmpty(nev))
				{
					throw new Exception("A név kitöltése kötelező!");
				}
				if (string.IsNullOrEmpty(dateInput.Text))
				{
					throw new Exception("A dátum kitöltése kötelező!");
				}
				Event esemeny = new Event();
				esemeny.Nev = nev;
				esemeny.Reszletek = reszletek;
				esemeny.Datum = Convert.ToDateTime(datum);
				esemeny.EgeszNapos = egeszNapos;
				esemeny.Ido = ido.ToString();
				esemeny.Prioritas = prioritas;
				if (emlekeztetoInput.Text.Trim() != "")
				{
					esemeny.Emlekezteto = Convert.ToDateTime(emlekezteto);
				}
				else
				{
					//esemeny.Emlekezteto = Convert.ToDateTime(nincsEmlekezteto);
					esemeny.Emlekezteto = new DateTime();
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
            else
            {
                return null;
            }
            
        }

		private void egeszNaposInput_Click(object sender, RoutedEventArgs e)
		{
			if (idoInput.IsEnabled == false)
			{
				idoInput.Text = string.Empty;
				idoInput.IsEnabled = true;
			}
			else
			{
				idoInput.IsEnabled = false;
			}
		}

	}
}
