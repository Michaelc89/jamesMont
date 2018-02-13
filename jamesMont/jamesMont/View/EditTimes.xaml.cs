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

                Navigation.PushAsync(new TimesPage(styler, clientName, picked, procedure));

            }
            catch (Exception er)
            {
                DisplayAlert("Alert", "Could not load bookings" + er, "Ok");
            }




        }
    }
}