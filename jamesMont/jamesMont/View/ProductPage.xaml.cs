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
	public partial class ProductPage : ContentPage
	{
        string productName;

        public ProductPage (string pName)
		{
			InitializeComponent ();

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
        }
        
        async private void Buy_Product(object sender, EventArgs e)
        {
            AzureService3 azureService;
            azureService = new AzureService3();
            try
            {
                 azureService.BuyProducts(productName);
            }
            catch (Exception er)
            {
                await DisplayAlert("Alert", "ERror: "+er, "Ok");
            }
        }
    }
}