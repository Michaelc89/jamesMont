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
	public partial class TimesPage : ContentPage
	{
        public static ObservableCollection<int> Holder { get; } = new ObservableCollection<int>();
        public TimesPage (string stylist, DateTime date)
		{
            InitializeComponent ();
            //stylists.Text = stylist;
            //dates.Text = date.ToString("MMMM dd, yyyy");
           // load();
            Holder.Clear();
            listView2.ItemsSource = Holder;
           
        }

        public async void load()
        {
           
            foreach (var item in Holder)
            {
                await DisplayAlert("Alert", "Items: " + item, "Ok");
            }
           
        }
	}
}