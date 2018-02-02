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
	public partial class cvc : ContentPage
	{
        string productN;
        int numb;
        public cvc (string productName, int number)
		{
			InitializeComponent ();
            productN = productName;
            numb = number;
            
        }
	}
}