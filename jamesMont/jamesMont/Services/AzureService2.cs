using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using jamesMont.Model;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using jamesMont.View;

namespace jamesMont.Services
{
    public class AzureService2 : ContentPage
    {
        MobileServiceClient client = null;

        IMobileServiceSyncTable<TheBookingTable> BookingsTable;
        bool isInitialised;
        public ObservableCollection<int> TakenSlots { get; } = new ObservableCollection<int>();
        
        ObservableCollection<int> AvailableSlots = new ObservableCollection<int>();
        public async Task Initialize()
        {
            // var mainPage = new MainPage();

            if (isInitialised)
            {

                return;
            }
            this.client = new MobileServiceClient("http://commtest1996.azurewebsites.net");
            MobileServiceClient client = new MobileServiceClient("http://commtest1996.azurewebsites.net");

            const string path = "user.db";

            var store = new MobileServiceSQLiteStore(path);


            store.DefineTable<TheBookingTable>();

            await this.client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            BookingsTable = this.client.GetSyncTable<TheBookingTable>();

            isInitialised = true;
        }

        public async Task<string> LoadBookings()
        {

            await Initialize();
            await SyncBookings();
            string answer = "false";
            //MMMM dd, yyyy
            DateTime d = new DateTime(10-28-2017);
            try
            {
                //put stylist in the where && stylist == Amber
                List<TheBookingTable> item = await BookingsTable
             .Where(todoItem => todoItem.Date == d.Date)
             .ToListAsync();
                
                foreach (var x in item)
                {
                    await DisplayAlert("Alert", "Dates: "+x.Date.ToString(), "Ok");
                    TakenSlots.Add(x.Slot);
                    answer = "true";
                }
                int y = 1;
                //TimesPage.Holder.Clear();
                for (int i = 1; i <= 8; i++)
                {
                    if (!(TakenSlots.Contains(i)))
                    {
                        TimesPage.Holder.Add(i);
                    }
                   
                }
                return answer;
            }


            catch (Exception er)
            {
                await DisplayAlert("Alert", "da error: " + er, "Ok");
                return answer;

            }
        }

        public async Task SyncBookings()
        {
            try
            {
                await BookingsTable.PullAsync("allusers3", BookingsTable.CreateQuery());
                await client.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync coffees, that is alright as we have offline capabilities: " + ex);
            }
        }

    }
}
