using jamesMont.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace jamesMont.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditTimes : ContentPage
	{
        string clientName, procedure;
		public EditTimes (string name, string pro)
		{
			InitializeComponent ();
            clientName = name;
            boom.Items.Add("Select a stylist");
            boom.Items.Add("Christie");
            boom.Items.Add("Chrystal");
            boom.Items.Add("Owen");
            boom.Items.Add("Conor");
            boom.TextColor = Color.White;
            boom.SelectedIndex = 0;
            procedure = pro;
		}

        DateTime picked;
        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            MainLable.Text = e.NewDate.ToString("MMMM dd, yyyy");
            picked = e.NewDate;
            AzureService2 azureService2;
            azureService2 = new AzureService2();
            string styler = "Amber";

            try
            {

                // azureService2.LoadBookings(picked);
                var selectedValue = boom.Items[boom.SelectedIndex];
                string stylist;
                stylist = selectedValue.ToString();
                if (stylist != "Select a stylist")
                {
                     azureService2.LoadBookings(picked, stylist);

                   
                    Navigation.PushAsync(new TimesPage(stylist, clientName, picked, procedure));
                }

            }
            catch (Exception er)
            {
                DisplayAlert("Alert", "Could not load bookings" + er, "Ok");
            }




        }
    }
}