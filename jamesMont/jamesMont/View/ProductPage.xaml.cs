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