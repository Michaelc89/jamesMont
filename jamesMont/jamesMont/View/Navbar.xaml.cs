using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace jamesMont.View
{
	public partial class Navbar : ContentPage
	{
        void Icon1_Tapped(object sender, System.EventArgs e)
        {
            var page = new RegisterPage();
            PlaceHolder.Content = page.Content;
        }

        void Icon2_Tapped(object sender, System.EventArgs e)
        {
            var page = new RegisterPage();
            PlaceHolder.Content = page.Content;
        }

        void Icon3_Tapped(object sender, System.EventArgs e)
        {
            var page = new RegisterPage();
            PlaceHolder.Content = page.Content;

        }

        void Icon4_Tapped(object sender, System.EventArgs e)
        {
            var page = new RegisterPage();
            PlaceHolder.Content = page.Content;
        }

        void Icon5_Tapped(object sender, System.EventArgs e)
        {
            var page = new RegisterPage();
            PlaceHolder.Content = page.Content;
        }


        public Navbar ()
		{
			InitializeComponent ();
		}
	}
}