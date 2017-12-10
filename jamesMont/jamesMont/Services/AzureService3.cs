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
    public class AzureService3:ContentPage
    {
        MobileServiceClient client = null;
        public MobileServiceClient MobileService { get; set; } = null;

        IMobileServiceSyncTable<ShopTBL> BookingsTable2;
        bool isInitialised;
       
        public async Task Initialize()
        {
            if (isInitialised)
            {
                return;
            }

            this.client = new MobileServiceClient("http://commtest1996.azurewebsites.net");
            MobileServiceClient client = new MobileServiceClient("http://commtest1996.azurewebsites.net");

            const string path = "user.db";

            var store = new MobileServiceSQLiteStore(path);


            store.DefineTable<ShopTBL>();
            
            await this.client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            BookingsTable2 = this.client.GetSyncTable<ShopTBL>();
            isInitialised = true;
        }

        public async Task SyncBookings()
        {
            try
            {
                await BookingsTable2.PullAsync("allusers3", BookingsTable2.CreateQuery());
                await client.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync coffees, that is alright as we have offline capabilities: " + ex);
            }
        }


        public async Task<string> LoadCategories()
        {
            await Initialize();
            await SyncBookings();
            string answer = "false";
            //System.Diagnostics.Debug.WriteLine((await App.MobileService.GetTable<User>().LookupAsync(1) as User).firstName);
            //User item = await coffeeTable.LookupAsync("6cc1aca348714a26af9c1d9d1757d0c2");

            try
            {
                 List<ShopTBL> item = await BookingsTable2
              .Where(todoItem => todoItem.ProductName != null)
              .ToListAsync();
                 CategoriesPage.ListViewItems2.Clear();
                 foreach (var x in item)
                 {
                     ShopTBL one = new ShopTBL( x.ProductName);
                     Shop.ListViewItems2.Add(one);

                     answer = "true";
                 }



                 /*foreach (var x in ListViewItems)
                 {
                     await DisplayAlert("Alert", "The Catagory boi: " +x , "Ok");
                 }*/

                return answer;
            }

            catch (Exception er)
            {
                await DisplayAlert("Alert", "da error: " + er, "Ok");
                return answer;

            }


        }



    }
}
