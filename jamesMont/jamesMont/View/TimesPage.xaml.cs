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
        public static ObservableCollection<string> Holder { get; } = new ObservableCollection<string>();
        public static ObservableCollection<string> TimesAvailable { get; } = new ObservableCollection<string>();
        string clientName4, procedure;
        DateTime picked;

        public TimesPage (string stylist, string clientName, DateTime p, string pro)
		{
            InitializeComponent ();
            picked = p;
            clientName4 = clientName;
            procedure = pro;
            listView2.ItemsSource = Holder;
            Holder.Clear();
        }
        
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selection = e.SelectedItem as string;
                int slot;
                if (selection == "9AM")
                {
                    slot = 1;
                }
                else if (selection == "10AM")
                {
                    slot = 2;
                }
                else if(selection == "11AM")
                {
                    slot = 3;
                }
                else if(selection == "12AM")
                {
                    slot = 4;
                }
                else if(selection == "1PM")
                {
                    slot = 5;
                }
                else if(selection == "2PM")
                {
                    slot = 6;
                }
                else if(selection == "3PM")
                {
                    slot = 7;
                }
                else if(selection == "4PM")
                {
                    slot = 8;
                }
                else
                {
                    slot = 9;
                }
                
                await Navigation.PushAsync(new BookingConfirm(selection, clientName4, slot, picked, procedure));
            }
        }
    }
}