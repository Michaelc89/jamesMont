using jamesMont.Model;
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
        public static ObservableCollection<TimesClass> times { get; } = new ObservableCollection<TimesClass>();
        public static ObservableCollection<string> TimesAvailable { get; } = new ObservableCollection<string>();
        string clientName4, procedure, styler;
        DateTime picked;

        public TimesPage (string stylist, string clientName, DateTime p, string pro)
		{
            InitializeComponent ();
          
            picked = p;
            styler = stylist;
            clientName4 = clientName;
            procedure = pro;
            listView2.ItemsSource = times;
            times.Clear();
            Holder.Clear();
        }
        
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                TimesClass tc = new TimesClass(e.SelectedItem.ToString());
                
                var selection = tc.Time.ToString() as string;
                int slot;
                if (selection == "9:00")
                {
                    slot = 1;
                }
                else if (selection == "9:30")
                {
                    slot = 2;
                }
                else if (selection == "10:00")
                {
                    slot = 3;
                }
                else if (selection == "10:30")
                {
                    slot = 4;
                }
                else if (selection == "11:00")
                {
                    slot = 5;
                }
                else if (selection == "11:30")
                {
                    slot = 6;
                }
                else if (selection == "12:00")
                {
                    slot = 7;
                }
                else if (selection == "12:30")
                {
                    slot = 8;
                }
                else if (selection == "13:00")
                {
                    slot = 9;
                }
                else if (selection == "13:30")
                {
                    slot = 10;
                }
                else if (selection == "14:00")
                {
                    slot = 11;
                }
                else if (selection == "14:30")
                {
                    slot = 12;
                }
                else if (selection == "15:00")
                {
                    slot = 13;
                }
                else if (selection == "15:30")
                {
                    slot = 14;
                }
                else if (selection == "16:00")
                {
                    slot = 15;
                }
                else if (selection == "16:30")
                {
                    slot = 16;
                }
                else if (selection == "17:00")
                {
                    slot = 17;
                }

                else
                {
                    slot = 18;
                }


                await Navigation.PushAsync(new BookingConfirm(selection, clientName4, slot, picked, procedure, styler));
            }
        }
    }
}