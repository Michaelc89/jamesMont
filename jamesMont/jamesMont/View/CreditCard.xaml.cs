using jamesMont.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace jamesMont.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditCard : ContentPage
    {
        string productN, clientName;
        int numb, quan;
        double test;
        AzureService3 azureService;
        public CreditCard(string productName, string name, int quantity)
        {
            InitializeComponent();
            clientName = name;
            productN = productName;
            quan = quantity;

        }

        async void Shop22(object sender, System.EventArgs e)
        {
            try
            {
                azureService = new AzureService3();


                test = await azureService.GetPrice(productN);

                await Navigation.PushAsync(new cvc(productN, numb, clientName, quan, test));
            }
            catch (System.Exception er)
            {
                Debug.WriteLine("da error: " + er);
            }
        }

    }
}