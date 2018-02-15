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

        IMobileServiceSyncTable<Booking> BookingsTable2;
        IMobileServiceSyncTable<Categories> Categories;
        bool isInitialised;
        public ObservableCollection<int> TakenSlots { get; } = new ObservableCollection<int>();
        public static ObservableCollection<int> Holder { get; } = new ObservableCollection<int>();
        public static ObservableCollection<int> AvailableSlots { get; } = new ObservableCollection<int>();
        public static ObservableCollection<string> Bookings3 { get; } = new ObservableCollection<string>();
        public static ObservableCollection<int> initalSlot { get; } = new ObservableCollection<int>();
        public static ObservableCollection<float> length { get; } = new ObservableCollection<float>();
        public static ObservableCollection<int> testg { get; } = new ObservableCollection<int>();

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
            store.DefineTable<Categories>();
            //store.DefineTable<ShoppingCategory>();

            await this.client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            BookingsTable2 = this.client.GetSyncTable<Booking>();
            Categories = this.client.GetSyncTable<Categories>();
            // ShopList = this.client.GetSyncTable<ShoppingCategory>();

            isInitialised = true;
        }

        public async Task<float> loadLength(string procedure)
        {
            await Initialize();
            await SyncBookings();

            float answer =0 ;

            try
            {
                List<Categories> item = new List<Categories>();
                //item.Clear();

                item = await Categories
              .Where(todoItem => todoItem.CategoryName == procedure)
                 .ToListAsync();

                foreach (var x  in item)
                {
                   answer =  x.Length;
                }
                await DisplayAlert("Alert", "Length: "+ answer, "Ok");

                return answer;
                
            }
            catch (Exception )
            {
                await DisplayAlert("Alert", "Something went wrong, please try again later", "Ok");
                return answer;
            }
           
            }

                public async Task<string> LoadBookings(DateTime xyz, string stylist)
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
                .Where(todoItem => todoItem.Stylist == stylist)
                  .ToListAsync();

                TakenSlots.Clear();
                initalSlot.Clear();
                AvailableSlots.Clear();
                //how many slots are needed?????
               
                foreach (var x in item)
                {
                    //add the inital slot into collection
                    int slots;
                    float lengths;
                    //take the slot and length numbers

                    slots = x.Slot ;
                    lengths = x.Length;

                    TakenSlots.Add(slots);
                    //add the length individually to the taken slots 
                    int z = 1;
                    for (int i = 0; i < lengths-1; i++)
                    {   
                        if (z <= lengths)
                        {
                            TakenSlots.Add(slots+z);
                        }
                        z++;
                    }
                }

            
              
                
                for (int i = 1; i <= 18; i++)
                {
                    if (!(TakenSlots.Contains(i)))
                    {
                        AvailableSlots.Add(i);
                    }
                }

                foreach (var xy in AvailableSlots)
                {
                    TimesClass t = new TimesClass();
                    
                  
                    if (xy == 1)
                       {
                           TimesPage.Holder.Add("9AM");
                        t.Time = "9AM";
                           TimesPage.times.Add(t);
                       }
                       else if (xy == 2)
                       {
                           TimesPage.Holder.Add("9:30AM");
                        t.Time = "9:30AM";
                        TimesPage.times.Add(t);
                    }
                       else if (xy == 3)
                       {
                           TimesPage.Holder.Add("10AM");
                        t.Time = "10AM";
                        TimesPage.times.Add(t);
                    }
                       else if (xy == 4)
                       {
                           TimesPage.Holder.Add("10:30AM");
                        t.Time = "10:30AM";
                        TimesPage.times.Add(t);
                    }
                       else if (xy == 5)
                       {
                           TimesPage.Holder.Add("11AM");
                        t.Time = "11AM";
                        TimesPage.times.Add(t);
                    }

                       else if (xy == 6)
                       {
                           TimesPage.Holder.Add("11:30AM");
                        t.Time = "11:30AM";
                        TimesPage.times.Add(t);

                    }
                       else if (xy == 7)
                       {
                           TimesPage.Holder.Add("12PM");
                        t.Time = "12PM";
                        TimesPage.times.Add(t);

                    }
                       else if (xy == 8)
                       {
                           TimesPage.Holder.Add("12:30PM");
                        t.Time = "12:30PM";
                        TimesPage.times.Add(t);
                    }
                    else if (xy == 9)
                    {
                        TimesPage.Holder.Add("1PM");
                        t.Time = "1PM";
                        TimesPage.times.Add(t);
                    }
                    else if (xy == 10)
                    {
                        TimesPage.Holder.Add("1:30PM");
                        t.Time = "1:30PM";
                        TimesPage.times.Add(t);
                    }
                    else if (xy == 11)
                    {
                        TimesPage.Holder.Add("2PM");
                        t.Time = "2PM";
                        TimesPage.times.Add(t);
                    }
                    else if (xy == 12)
                    {
                        TimesPage.Holder.Add("2:30PM");
                        t.Time = "2:30PM";
                        TimesPage.times.Add(t);
                    }
                    else if (xy == 13)
                    {
                        TimesPage.Holder.Add("3PM");
                        t.Time = "3PM";
                        TimesPage.times.Add(t);
                    }
                    else if (xy == 14)
                    {
                        TimesPage.Holder.Add("3:30PM");
                        t.Time = "3:30PM";
                        TimesPage.times.Add(t);
                    }
                    else if (xy == 15)
                    {
                        TimesPage.Holder.Add("4PM");
                        t.Time = "4PM";
                        TimesPage.times.Add(t);
                    }
                    else if (xy == 16)
                    {
                        TimesPage.Holder.Add("4:30PM");
                        t.Time = "4:30PM";
                        TimesPage.times.Add(t);
                    }
                    else if (xy == 17)
                    {
                        TimesPage.Holder.Add("5PM");
                        t.Time = "5PM";
                        TimesPage.times.Add(t);
                    }
                    else if (xy == 18)
                    {
                        TimesPage.Holder.Add("5:30PM");
                        t.Time = "5:30PM";
                        TimesPage.times.Add(t);
                    }

                    else
                       {
                           TimesPage.Holder.Add("Sorry, No times are available for this date.");
                        t.Time = "Sorry, No times are available for this date.";
                        TimesPage.times.Add(t);
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

        public async Task<Booking> AddBooking(string clientName, int slot, DateTime picked, string pro, string email, float length, string style)
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
                Email = email,
                Length =  length,
                Stylist = style
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

       
      
    }
}
