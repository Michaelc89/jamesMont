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
        string productN;
        int numb;
		public CreditCard (string productName, int number)
		{
			InitializeComponent ();
            productN = productName;
            numb = number;
		}

        async void Shop22(object sender, System.EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new cvc(productN, numb));
            }
            catch (System.Exception er)
            {
                Debug.WriteLine("da error: " + er);
            }
        }

    }
}