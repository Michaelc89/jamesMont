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
	public partial class BookingPage : ContentPage
	{
       // public static ObservableCollection<BookingTBL> ListBookings { get; } = new ObservableCollection<BookingTBL>();
        public BookingPage (string category)
		{
			InitializeComponent ();
            //loadBookings();
            Title = category;
            datez.MinimumDate = DateTime.Now;
            
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
                azureService2.LoadBookings();
                
            }
            catch (Exception er)
            {
                DisplayAlert("Alert", "Could not load bookings" + er, "Ok");
            }
            Navigation.PushAsync(new TimesPage(styler, picked));
        }
        
    }
}