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
        public static ObservableCollection<Booking> Bookings { get; } = new ObservableCollection<Booking>();
        public static ObservableCollection<BookingDetails> BookingDetails { get; } = new ObservableCollection<BookingDetails>();

        public EditBookingPage ()
		{
			InitializeComponent ();
            Bookings.Clear();
            loadBookings();
           



            BookingsList.ItemsSource = Bookings;
            
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