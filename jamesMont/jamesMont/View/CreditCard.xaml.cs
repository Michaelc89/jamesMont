﻿using System;
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
		public CreditCard (string productName, int number, string name, int quantity)
		{
			InitializeComponent ();
            clientName = name;
            productN = productName;
            numb = number;
            quan = quantity;

		}

        async void Shop22(object sender, System.EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new cvc(productN, numb, clientName, quan));
            }
            catch (System.Exception er)
            {
                Debug.WriteLine("da error: " + er);
            }
        }

    }
}