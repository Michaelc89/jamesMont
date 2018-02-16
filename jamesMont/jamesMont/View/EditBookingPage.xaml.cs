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
        public static ObservableCollection<string> BookingID { get; } = new ObservableCollection<string>();
        string clientName;
        public EditBookingPage (string name)
		{
			InitializeComponent ();
            Bookings2.Clear();
            loadBookings();

            clientName = name;
            BookingsList.ItemsSource = Bookings2;
		}

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            { 
               

                try
                {
                  
                      
                        

                        string boom;
                string[] words;
                string[] words2;
                string IdNumber="";
                string pro = "";

                boom = e.SelectedItem.ToString();

                words = boom.Split(' ');

                IdNumber = words[0].ToString();
                    pro = words[1].ToString();

                    
                    words2 = pro.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                    
                    pro = words2[1];

                   
                    var selection = e.SelectedItem as Categories;

                await Navigation.PushAsync(new ChangeBooking(IdNumber, clientName, pro));
                }
                catch (Exception er)
                {
                    Console.Write("Blah: "+er.ToString());
                }
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