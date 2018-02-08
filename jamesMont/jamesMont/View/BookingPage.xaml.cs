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
        public static ObservableCollection<int> Holder { get; } = new ObservableCollection<int>();
        public BookingPage (string category, string clientName)
		{
			InitializeComponent ();



            boom.Items.Add("Select a stylist");
            boom.Items.Add("Christie");
            boom.Items.Add("Chrystal");
            boom.Items.Add("Owen");
            boom.Items.Add("Conor");
            boom.TextColor = Color.White;
            boom.SelectedIndex = 0;
            
            

            procedure = category;
            clientName3 = clientName;
            //loadBookings();
            Title = category;
            datez.MinimumDate = DateTime.Now;
            image.Source = "calandar.png";

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
                var selectedValue = boom.Items[boom.SelectedIndex];
                string stylist;
                stylist = selectedValue.ToString();
                if (stylist != "Select a stylist")
                {
                    azureService2.LoadBookings(picked);

                    Navigation.PushAsync(new TimesPage(styler, clientName3, picked, procedure));
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