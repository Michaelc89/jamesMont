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
	public partial class ContactPage : ContentPage
	{
		public ContactPage ()
		{
			InitializeComponent ();
		}

        private void SendEmail(object sender, EventArgs e)
        {

            // DisplayAlert("Alert", "Email and/or Password is incorrect", "ok");
            Device.OpenUri(new Uri("mailto:mikeychristie@gmail.com?subject=Book an Appointment &body= I Would Like to Book an appointment"));

        }

        private void OpenMap(object sender, EventArgs e)
        {


            //54.2786970, -8.4600899 It sligo co-ordinates
            Device.OpenUri(new Uri("https://www.google.com/maps/dir/?api=1&query=54.2786970, -8.4600899&query_place_id=ChIJN0v7807oXkgRefuF2SFba8c"));
            //https://www.google.com/maps/dir/?api=1&parameters

        }

        private void CallUs(object sender, EventArgs e)
        {

            Device.OpenUri(new Uri("tel:0861948974"));

        }
    }
}