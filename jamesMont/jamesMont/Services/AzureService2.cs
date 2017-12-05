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
        public MobileServiceClient MobileService { get; set; } = null;
        IMobileServiceSyncTable<ShoppingCategory> ShopList;
        IMobileServiceSyncTable<Booking> BookingsTable2;
        bool isInitialised;
        public ObservableCollection<int> TakenSlots { get; } = new ObservableCollection<int>();
        public static ObservableCollection<int> Holder { get; } = new ObservableCollection<int>();
        public static ObservableCollection<int> AvailableSlots { get; } = new ObservableCollection<int>();
        public static ObservableCollection<string> Bookings3 { get; } = new ObservableCollection<string>();

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


            store.DefineTable<Booking>();
            //store.DefineTable<ShoppingCategory>();

            await this.client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            BookingsTable2 = this.client.GetSyncTable<Booking>();
            // ShopList = this.client.GetSyncTable<ShoppingCategory>();

            isInitialised = true;
        }

        public async Task InitializeShop()
        {
            // var mainPage = new MainPage();

            if (isInitialised)
            {
                return;
            }
            MobileService = new MobileServiceClient("http://commtest1996.azurewebsites.net");
            MobileServiceClient client = new MobileServiceClient("http://commtest1996.azurewebsites.net");

            // const string path = "syncstore.db";
            const string path = "user.db";

            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<ShoppingCategory>();
            //store.DefineTable<TheBookingTable>();

            await DisplayAlert("alerrt", "inital", "Ok");
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            ShopList = MobileService.GetSyncTable<ShoppingCategory>();
            
            // BookTable = MobileService.GetSyncTable<TheBookingTable>();

            isInitialised = true;
        }

        public async Task<string> LoadBookings(DateTime xyz)
        {
            await Initialize();
            await SyncBookings();
           
            string answer = "false";

            try
            {
                List<Booking> item = new List<Booking>();
                //item.Clear();

                 item =  await BookingsTable2
               .Where(todoItem => todoItem.Date == xyz)
                  .ToListAsync();



                TakenSlots.Clear();
                foreach (var x in item)
                {
                    TakenSlots.Add(x.Slot);
                }

                AvailableSlots.Clear();
                for (int i = 1; i <= 9; i++)
                {
                    if (!(TakenSlots.Contains(i)))
                    {
                        AvailableSlots.Add(i);
                    }
                }

                foreach (var xy in AvailableSlots)
                {
                     if (xy == 1)
                       {
                           TimesPage.Holder.Add("9AM");
                       }
                       else if (xy == 2)
                       {
                           TimesPage.Holder.Add("10AM");
                       }
                       else if (xy == 3)
                       {
                           TimesPage.Holder.Add("11AM");
                       }
                       else if (xy == 4)
                       {
                           TimesPage.Holder.Add("12PM");
                       }
                       else if (xy == 5)
                       {
                           TimesPage.Holder.Add("1PM");
                       }

                       else if (xy == 6)
                       {
                           TimesPage.Holder.Add("2PM");
                           
                       }
                       else if (xy == 7)
                       {
                           TimesPage.Holder.Add("3PM");
                           
                       }
                       else if (xy == 8)
                       {
                           TimesPage.Holder.Add("4PM");
                       }
                       else if (xy == 9)
                       {
                           TimesPage.Holder.Add("5PM");
                       }
                       else
                       {
                           TimesPage.Holder.Add("Sorry, No times are available for this date.");
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

        public async Task<Booking> AddBooking(string clientName, int slot, DateTime picked, string pro, string email)
        {
            await Initialize();
            Random rnd = new Random();
            int Id = rnd.Next(1, 10000);

            var coffee = new Booking()
            {
                Id = Id.ToString(),
                BookingName = clientName,
                Date = picked,
                Slot = slot,
                Procedure = pro,
                Email = email
            };


            await BookingsTable2.InsertAsync(coffee);

            await SyncBookings();
            await DisplayAlert("Alert", "Booking is complete", "ok");
            return coffee;
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

        public async Task<string> DeleteBooking(string idz)
        {
            string answer = "no";
            await Initialize();
            await SyncBookings();

            try
            {

                List<Booking> item = new List<Booking>();
                 item = await BookingsTable2
                .Where(todoItem => todoItem.Id == idz)
                   .ToListAsync();

                foreach (var xyz in item)
                {
                    await BookingsTable2.DeleteAsync(xyz);
                }
                
            }
            catch( Exception er)
            {
                await DisplayAlert("Alert","Da Error: "+ er,"Ok");
            }

                return answer;
        }

        public async Task<string> EditBookings()
        {
            await Initialize();
            await SyncBookings();

            string answer = "false", email="";

            //make sure it only loads the bookings matched with the user's email
            foreach (var item in MainPage.UserEmail)
            {
                email = item;
            }

            try
            {
                List<Booking> item = new List<Booking>();
               

                item = await BookingsTable2
              .Where(todoItem => todoItem.Email == email && todoItem.Date >= DateTime.Now)
                 .ToListAsync();
                Bookings3.Clear();

                foreach (var x in item)
                {
                    EditBookingPage.Bookings2.Add(x.Date.ToString(x.Id + " " + "dd/MM/yyyy") + "\t\t\t" + x.Procedure);
                   // EditBookingPage.BookingID.Add(x.Id);
                }
               
                return answer;
            }
            
            catch (Exception er)
            {
                await DisplayAlert("Alert", "da error: " + er, "Ok");
                return answer;

            }

        }

        public async Task<string> LoadCategories()
        {
           

            string answer = "false";
         
            await InitializeShop();
            await DisplayAlert("Alert", "WEll", "Ok");
            return answer;
            /*
            await SyncCoffee();
            return answer;
           /*
            
            try
            {
                await DisplayAlert("Alert", "ProductName", "Ok");
                List<ShoppingCategory> item = await ShopList
             .Where(todoItem => todoItem.ProductName != null)
             .ToListAsync();
                Shop.ShopCategories.Clear();
                return answer;
                foreach (var x in item)
                {
                    ShoppingCategory one = new ShoppingCategory(x.ProductName);
                    Shop.ShopCategories.Add(one);


                    await DisplayAlert("Alert", one.ProductName, "Ok");
                    answer = "true";
                }

                return answer;
            }

            catch (Exception er)
            {
                await DisplayAlert("Alert", "da error: " + er, "Ok");
                return answer;

            }*/


        }

        public async Task SyncCoffee()
        {
            await DisplayAlert("Alert", "well 2", "Ok");
            try
            {
                
                await ShopList.PullAsync("allusers", ShopList.CreateQuery());
                await client.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync coffees, that is alright as we have offline capabilities: " + ex);
            }
        }
    }
}
