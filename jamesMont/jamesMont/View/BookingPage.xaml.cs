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
        string clientName3, procedure;
        float len;
        public static ObservableCollection<int> Holder { get; } = new ObservableCollection<int>();
        public static ObservableCollection<string> ListViewItems2 { get; } = new ObservableCollection<string>();
        public BookingPage (string category, string clientName, float length)
		{
			InitializeComponent ();
           // loadStylists();
            len = length;

             boom.Items.Add("Select a stylist");
             boom.Items.Add("Christie");
             boom.Items.Add("Chrystal");
             boom.Items.Add("Owen");
             boom.Items.Add("Conor");
             boom.TextColor = Color.White;
             boom.SelectedIndex = 0;
             

           /* foreach (var item in ListViewItems2)
            {
                DisplayAlert("", "Name: "+item, "Ok");
            }*/

       //     boom.ItemsSource = ListViewItems2;
            procedure = category;
            clientName3 = clientName;
            //loadBookings();
            Title = category;
            datez.MinimumDate = DateTime.Now;
            image.Source = "calandar.png";

        }

        public async void loadStylists()
        {
            AzureService3 azureService;
            azureService = new AzureService3();
            try
            {
               await  azureService.LoadStylists();

            }
            catch (Exception er)
            {
               await  DisplayAlert("Alert", "Could not load categories" + er, "Ok");
            }

        }

        DateTime picked;
        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            MainLable.Text = e.NewDate.ToString("MMMM dd, yyyy");
           
            picked = e.NewDate;
            AzureService2 azureService2;
            azureService2 = new AzureService2();
          
            try
            {
                var selectedValue = boom.Items[boom.SelectedIndex];
                string stylist;
                stylist = selectedValue.ToString();
                if (stylist != "Select a stylist")
                {
                    azureService2.LoadBookings(picked, stylist);

                    Navigation.PushAsync(new TimesPage(stylist, clientName3, picked, procedure));
                }
               else if (stylist == "Select a stylist")
                {
                    DisplayAlert("Alert", "Please select a stylist", "Ok");
                }

            }
            catch (Exception )
            {
                    DisplayAlert("Alert", "Please select a stylist", "Ok");

                
            }
            
        }


        
    }
}