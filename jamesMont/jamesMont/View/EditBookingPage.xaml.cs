using jamesMont.Model;
using jamesMont.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace jamesMont.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditBookingPage : ContentPage
	{
        public static ObservableCollection<string> Bookings2 { get; } = new ObservableCollection<string>();

        public EditBookingPage ()
		{
			InitializeComponent ();
            Bookings2.Clear();
            loadBookings();
            
            BookingsList.ItemsSource = Bookings2;
		}

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selection = e.SelectedItem as Categories;

                await Navigation.PushAsync(new ChangeBooking());
            }
        }

        public async void loadBookings()
        {
            AzureService2 azureService;
            azureService = new AzureService2();

            try
            {
                await azureService.EditBookings();
            }
            catch (Exception er)
            {
                await DisplayAlert("Alert", "Could not load categories" + er, "Ok");
            }

        }
    }
}