using jamesMont.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace jamesMont.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangeBooking : ContentPage
	{
        string clientname, id, procedure;
        public ChangeBooking (string ID, string name, string pro)
		{
           

            id = ID;
            clientname = name;
            procedure = pro;
			InitializeComponent ();

            image.Source = "change2.png";
            foreach (var x in MainPage.UserName)
            {
                name = x;
            }
		}
        AzureService2 azureService;
        async private void deleteBooking(object sender, EventArgs e)
        {
            azureService = new AzureService2();
            await azureService.DeleteBooking(id);

            await DisplayAlert("Alert", "Booking Deleted", "Ok");
            await Navigation.PushAsync(new MenuPage(clientname));   
    }

        async private void changeBooking(object sender, EventArgs e)
        {
            try
            {
                var answer = await DisplayAlert("Are you sure?", "If you continue your booking will be changed", "Continue", "Cancel");

                //await DisplayAlert("Alert", answer.ToString(), "ok");

                if (answer.ToString() == "True")
                {

                    azureService = new AzureService2();
                    await azureService.DeleteBooking(id);
                    await Navigation.PushAsync(new EditTimes(clientname, procedure));
                }
                else
                {
                    await DisplayAlert("Alert", "Cancelled", "Ok");
                }

            }
            catch (Exception)
            {

                throw;
            }
          


        }
    }
}