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
using MvvmHelpers;
using System.Collections.ObjectModel;
using jamesMont.View;

namespace jamesMont.Services
{
    public class AzureService : ContentPage
    {
        public MobileServiceClient MobileService { get; set; } = null;

        IMobileServiceSyncTable<User> coffeeTable;
        IMobileServiceSyncTable<Categories> CategoriesTable;
       

        // ObservableRangeCollection<Categories> Categories {get;} = new ObservableRangeCollection<Categories>();

        //public ObservableCollection<string> ListViewItems { get; } = new ObservableCollection<string>();
        //public ObservableCollection<Booking> Bookings { get; } = new ObservableCollection<Booking>();

        bool isInitialised;

        /*****************************************************************
            *INITIALIZE METHOD - CONNECTS TO THE DATABASE
        ****************************************************************/
        public async Task Initialize()
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

            store.DefineTable<User>();
            store.DefineTable<Categories>();
            //store.DefineTable<TheBookingTable>();
            

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            coffeeTable = MobileService.GetSyncTable<User>();
            CategoriesTable = MobileService.GetSyncTable<Categories>();
           // BookTable = MobileService.GetSyncTable<TheBookingTable>();

            isInitialised = true;
        }
        /*****************************************************************
             *METHOD TO SEE IF THE USER HAS AN ACCOUNT
        ****************************************************************/
        public async Task<bool> GetCoffee(string email, string password)
        {
            bool isThere = false;
            try
            {
                await Initialize();
                await SyncCoffee();

                List<User> item = await coffeeTable
                .Where(todoItem => todoItem.Email == email && todoItem.Password == password)
                .ToListAsync();

                foreach (var x in item)
                {
                    string daEmail = x.Email;
                    string daPassword = x.Password;

                    if (item.Count() > 0)
                    {
                        await DisplayAlert("Alert", "Welcome " + x.FirstName, "Ok");
                        isThere = true;
                        break;
                    }
                    else
                    {
                        isThere = false;
                    }
                }

                return isThere;
            }

            catch (Exception er)
            {
                Debug.WriteLine("the error: " + er);
                return false;

            }
        }

        /*****************************************************************
          *METHOD TO GET THE USER'S NAME
      ****************************************************************/

        public async Task<string> GetUserName(string email, string password)
        {
            try
            {
                await Initialize();
                await SyncCoffee();

                List<User> item = await coffeeTable
                .Where(todoItem => todoItem.Email == email && todoItem.Password == password)
                .ToListAsync();

                foreach (var x in item)
                {
                    string daEmail = x.Email;
                    string daPassword = x.Password;

                    if (item.Count() > 0)
                    {
                        return x.FirstName;
                    }
                    else
                    {
                        return "";
                    }
                }

                return "";
            }

            catch (Exception er)
            {
                Debug.WriteLine("the error: " + er);
                return "";
            }
        }

        public async Task<IEnumerable<User>> GetCoffees()
        {
            //Initialize & Sync
            await Initialize();
            await SyncCoffee();
            return await coffeeTable.OrderBy(c => c.Id).ToEnumerableAsync(); ;
        }

        /*****************************************************************
         *METHOD TO ADD A USER
         ****************************************************************/
        public async Task<User> AddUser(string first, string second, string email, string password, string phone)
        {
            await Initialize();


            var coffee = new User()
            {
                FirstName = first,
                Surname = second,
                Email = email,
                Password = password,
                Phone = phone

            };
            

            await coffeeTable.InsertAsync(coffee);

            await SyncCoffee();
            return coffee;
        }

        /*****************************************************************
        *METHOD TO CHECK IF A USER IS ALREADY IN DATABASE
        ****************************************************************/
        public async Task<bool> CheckUser(string Email)
        {
            await Initialize();
            await SyncCoffee();
            bool answer = false;
            //System.Diagnostics.Debug.WriteLine((await App.MobileService.GetTable<User>().LookupAsync(1) as User).firstName);
            //User item = await coffeeTable.LookupAsync("6cc1aca348714a26af9c1d9d1757d0c2");

            List<User> item = await coffeeTable
            .Where(todoItem => todoItem.Email == Email)
            .ToListAsync();

            foreach (var x in item)
            {
                //await DisplayAlert("alert", x.Email+" "+x.Password, "Ok");
                string daEmail = x.Email;

                if (item.Count() > 0)
                {
                    await DisplayAlert("Alert", "Email: " + Email + " already exists, Please try again", "Ok");
                    answer = true;
                    break;
                }
                else
                {
                    answer = false;
                    break;
                }

            }
            return answer;

        }

        /*****************************************************************
                *METHOD TO LOAD THE CATEGORIES ONTO CATEGORIES PAGE
         ****************************************************************/
        public async Task<string> LoadCategories()
        {
            await Initialize();
            await SyncCategory();
            string answer = "false";
            //System.Diagnostics.Debug.WriteLine((await App.MobileService.GetTable<User>().LookupAsync(1) as User).firstName);
            //User item = await coffeeTable.LookupAsync("6cc1aca348714a26af9c1d9d1757d0c2");

            try
            {
                List<Categories> item = await CategoriesTable
             .Where(todoItem => todoItem.CategoryName != null)
             .ToListAsync();
                CategoriesPage.ListViewItems2.Clear();
                foreach (var x in item)
                {
                    Categories one = new Categories(x.Id, x.CategoryName);
                    CategoriesPage.ListViewItems2.Add(one);
                   
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




        /*public async Task<string> LoadBookings()
        {
            await Initialize();
            await SyncBookings();
            string answer = "false";
            //System.Diagnostics.Debug.WriteLine((await App.MobileService.GetTable<User>().LookupAsync(1) as User).firstName);
            //User item = await coffeeTable.LookupAsync("6cc1aca348714a26af9c1d9d1757d0c2");

            try
            {
                List<TheBookingTable> item = await BookTable
             .Where(todoItem => todoItem.BookingName == "Michael")
             .ToListAsync();
             
                foreach (var x in item)
                {
                    await DisplayAlert("Alert", "Name: "+x.BookingName, "Ok");
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
        */




        /*****************************************************************
            *METHOD TO SYNC THE DATABSE TO THE APP
        ****************************************************************/
        public async Task SyncCoffee()
    {
        try
        {
            await coffeeTable.PullAsync("allusers", coffeeTable.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Unable to sync coffees, that is alright as we have offline capabilities: " + ex);
        }
    }
        public async Task SyncCategory()
        {
            try
            {
                await CategoriesTable.PullAsync("allusers", CategoriesTable.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync coffees, that is alright as we have offline capabilities: " + ex);
            }
        }
       /* public async Task SyncBookings()
        {
            try
            {
                await coffeeTable.PullAsync("allusers3", BookTable.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync coffees, that is alright as we have offline capabilities: " + ex);
            }
        }*/



    }
}




