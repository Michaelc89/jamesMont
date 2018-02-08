using jamesMont.Model;
using jamesMont.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public static ObservableCollection<string> JOhn { get; } = new ObservableCollection<string>();
        string productName, clientName;
        AzureService3 azureService;
        int number;
        double test;
        string image2;
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

           
            try
            {

                GetPrice();


            }
            catch (Exception er)
            {
                DisplayAlert("Alert", "error: " + er.Message, "Ok");
            }

        }


        async private void Buy_Product(object sender, EventArgs e)
        {


            // AzureService3 azureService;
            // azureService = new AzureService3();
            try
            {
                var selectedValue = boom.Items[boom.SelectedIndex];

                number = Convert.ToInt32(selectedValue);
                // azureService.BuyProducts(productName, number );
                await Navigation.PushAsync(new CreditCard(productName, clientName, number));
            }
            catch (Exception er)
            {
                await DisplayAlert("Alert", "Please select a quantity", "Ok");
            }
        }


        private async Task<double> GetPrice()
        {
            azureService = new AzureService3();

            try
            {
                test = await azureService.GetPrice(productName);
                image2 = await azureService.GetImage(productName);

                image.Source = image2;
                labelxx.Text = "€" + test.ToString(); ;
                return test;
            }

            catch (Exception ex)
            {
                await DisplayAlert("Alert", "error: " + ex.Message, "Ok");
                return test;

            }

        }
    }
}