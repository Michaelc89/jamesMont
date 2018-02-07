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

        public static ObservableCollection<float> Prices3 { get; } = new ObservableCollection<float>();
        public static ObservableCollection<string> images{ get; } = new ObservableCollection<string>();
        public MobileServiceClient client { get; set; } = null;

        IMobileServiceSyncTable<Shop_Two> shopz;

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


            store.DefineTable<Shop_Two>();

            await this.client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            shopz = this.client.GetSyncTable<Shop_Two>();
            isInitialised = true;
        }

    

        public async Task SyncBookings()
        {
            try
            {
                await shopz.PullAsync("allusers", shopz.CreateQuery());
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
                 List<Shop_Two> item = await shopz
              .Where(todoItem => todoItem.ProductName != null)
              .ToListAsync();
                 CategoriesPage.ListViewItems2.Clear();
                 foreach (var x in item)
                 {
                    Shop_Two one = new Shop_Two( x.ProductName, x.Price);
                    Shop.ListViewItems2.Add(one);

                     answer = "true";
                 }
                 
                return answer;
            }

            catch (Exception er)
            {
                await DisplayAlert("Alert", "da error: " + er, "Ok");
                return answer;

            }


        }

      

        public async void BuyProducts(string Pname, int numb)
        {
            string productname = Pname;
            int number = numb;
            float stock;
            await Initialize();
            await SyncBookings();
            try
            {
              List<Shop_Two> item = await shopz
             .Where(todoItem => todoItem.ProductName == productname)
             
             .ToListAsync();
                
                foreach (var x in item)
                {
                    stock = x.Quantity;
                    if (stock <= 0 || stock - number < 0)
                    {
                        await DisplayAlert("Alert", "Not enough stock", "Ok");
                        //break;
                    }
                    else
                    {
                        stock = stock - number;
                        x.Quantity = stock;

                        await shopz.UpdateAsync(x);
                        await SyncBookings();
                        await DisplayAlert("Alert", "Done", "Ok");
                    }
                }
                
            }
            catch (Exception er)
            {
                await DisplayAlert("Alert", "Error: "+er, "Ok");

            }

            
        }


        public async Task Initialize2()
        {
            if (isInitialised)
            {
                return;
            }

            this.client = new MobileServiceClient("http://commtest1996.azurewebsites.net");
            MobileServiceClient client = new MobileServiceClient("http://commtest1996.azurewebsites.net");

            const string path = "user.db";

            var store = new MobileServiceSQLiteStore(path);


            store.DefineTable<Shop_Two>();

            await this.client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            shopz = this.client.GetSyncTable<Shop_Two>();
            isInitialised = true;

        }


        public async Task SyncBookings2()
        {
            try
            {
                await shopz.PullAsync("allusers", shopz.CreateQuery());
                await client.SyncContext.PushAsync();
                
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync coffees, that is alright as we have offline capabilities: " + ex);
            }
        }


        //  public async void getPrice(string pname)
        public async Task<double> GetPrice(string pname)
        {
            await Initialize2();
            await SyncBookings2();
            Prices3.Clear();
            string productname;
            float price4;
            double answer = 0;

            productname = pname;

            try
            {


                List<Shop_Two> item = await shopz
             .Where(todoItem => todoItem.ProductName == productname)
             .ToListAsync();

                Prices3.Clear();
                foreach (var y in item)
                {
                    Prices3.Add(y.Price);
                }

                foreach (var x in Prices3)
                {

                    answer = Convert.ToDouble(x);
                    // ProductPage.JOhn.Add(g);
                }
            }


            catch (Exception er)
            {
                await DisplayAlert("Alert", "da error: " + er, "Ok");

            }
            return answer;
        }


        public async Task<string> GetImage(string pname)
        {
            await Initialize2();
            await SyncBookings2();
            Prices3.Clear();
            string productname;
           
            string answer ="False";

            productname = pname;

            try
            {
                List<Shop_Two> item = await shopz
             .Where(todoItem => todoItem.ProductName == productname)
             .ToListAsync();

                images.Clear();
                foreach (var y in item)
                {
                    images.Add(y.imageURL);
                }

                foreach (var x in images)
                {
                   answer = x;
                }
            }


            catch (Exception er)
            {
                await DisplayAlert("Alert", "da error: " + er, "Ok");

            }
            return answer;
        }







    }
}
