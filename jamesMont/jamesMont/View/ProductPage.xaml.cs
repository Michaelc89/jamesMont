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
    public partial class ProductPage : ContentPage
    {
       // public static ObservableCollection<float> Prices3 { get; } = new ObservableCollection<float>();
        string productName, clientName;
        AzureService3 azureService;
        int number;
        string gh;
        float price2;
        List<int> listz = new List<int>();
        public ProductPage(string pName, string cName)
        {
            InitializeComponent();

            clientName = cName;
            listz.Add(1);
            listz.Add(2);
            listz.Add(3);
            listz.Add(4);
            listz.Add(5);
            listz.Add(6);
            listz.Add(7);
            listz.Add(8);

            boom.ItemsSource = listz;

            productName = pName;

            product.Text = productName;

            if (pName == "Gel")
            {
                image.Source = "http://bit.ly/2iTDjO4";

            }
            else if (pName == "Shampoo")
            {
                image.Source = "http://bit.ly/2B6rnCA";
            }
            else if (pName == "Highlights")
            {
                image.Source = "http://bit.ly/2l13Mxn";
            }
            else
            {
                image.Source = "http://bit.ly/2ygy5ky";
            }
            try
            {
                GetPrice();
                DisplayPrice();
            }
            catch (Exception er)
            {
                DisplayAlert("Alert", "error: " + er.Message, "Ok");
            }
        }

        public void DisplayPrice()
        {
            try
            {
                //Prices3.Add(16);
               
                foreach (var item in AzureService3.Prices3)
                {

                  
                    price2 = item;
                    // price2.ToString();
                }

                PriceLbl.Text = "Price: " + price2.ToString();
            }
            catch (Exception er)
            {
                DisplayAlert("Alert", "error: "+er, "Ok");
            }
        }

        async private void Buy_Product(object sender, EventArgs e)
        {
            var selectedValue = boom.Items[boom.SelectedIndex];

            number = Convert.ToInt32(selectedValue);

            // AzureService3 azureService;
            // azureService = new AzureService3();
            try
            {
                // azureService.BuyProducts(productName, number );
                await Navigation.PushAsync(new CreditCard(productName, number, clientName, number));
            }
            catch (Exception er)
            {
                await DisplayAlert("Alert", "ERror: " + er, "Ok");
            }
        }

        
        private void GetPrice()
        {
             azureService = new AzureService3();
             azureService.getPrice(productName);
            
        }
    }
}