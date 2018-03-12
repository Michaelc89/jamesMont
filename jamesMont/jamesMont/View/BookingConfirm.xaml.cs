using Android.App;
using Android.Content;
using Android.Provider;
using jamesMont.Services;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace jamesMont.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingConfirm : ContentPage
    {
        string clientName5, procedure, email, styler, timeB;
        int slots;
        double Dslot;
        DateTime picked;

        public BookingConfirm(string bookingTime, string clientName, int slot, DateTime p, string pro, string stylist)
        {
            InitializeComponent();
            image.Source = "confirm.png";
            clientName5 = clientName;
            styler = stylist;
            picked = p;
            slots = slot;
            procedure = pro;
            timeB = bookingTime;
            double Dslot = Convert.ToDouble(slots);

            foreach (var item in MainPage.UserEmail)
            {
                email = item;
            }

            if (email == null)
            {
                foreach (var item in RegisterPage.UserEmail)
                {
                    email = item;
                }
            }
            
            LabelBook.Text = "Booking time: " + bookingTime;
            LabelBook2.Text = "Booking Date: " + p.ToString("dd/MM/yyyy");

        }
        AzureService2 azureService;
        async private void book_method(object sender, EventArgs e)
        {
            try
            {


                azureService = new AzureService2();



                float len;
                len = await azureService.loadLength(procedure);
                await azureService.AddBooking(clientName5, slots, picked, procedure, email, len, styler);


                var fromAddress = new MailAddress("commBooking@gmail.com", ".Comm");
                var toAddress = new MailAddress("michaelchrystal89@gmail.com", clientName5);
                const string fromPassword = "ShaneBanks";
                const string subject = "Booking Confirmed";
                string b = "Thank you " + clientName5 + " your booking at " + picked + " For " + procedure + " is confirmed, We look forword to seeing you !!";
                string body = b;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }


                string Pstring = picked.ToString();
                Intent intent = new Intent(Intent.ActionInsert);
                intent.PutExtra(CalendarContract.Events.InterfaceConsts.Title, ".COMM Salon Booking");
                intent.PutExtra(CalendarContract.Events.InterfaceConsts.Description, clientName5 + " your booking at " + picked + " For " + procedure + " is confirmed, We look forword to seeing you !!");
                intent.PutExtra(CalendarContract.Events.InterfaceConsts.EventLocation, "IT Sligo");

                //********************* Set end Times****************************
                int Length = Convert.ToInt32(len);
                string StartTime = timeB;//for testing
                TimeSpan ts = TimeSpan.Parse(StartTime);//accepts 1:00 not am/pm TimeB
                TimeSpan ThirtyMin = new TimeSpan(0, 0, 30, 0, 0);//30 minutes
                TimeSpan Duration = TimeSpan.FromTicks(ThirtyMin.Ticks * Length);//minutes * length
                TimeSpan endTime = ts.Add(Duration);//add duration to start time
                System.Diagnostics.Debug.WriteLine(" ****  end: " + endTime);



                DateTime CalStartDate = picked.Add(ts);
                DateTime CalEndDate = picked.Add(endTime);

                //*******************************************************************************
                intent.PutExtra(CalendarContract.ExtraEventBeginTime, GetDateTimeMS(CalStartDate.Year, CalStartDate.Month, CalStartDate.Day, CalStartDate.Hour, CalStartDate.Minute));
                intent.PutExtra(CalendarContract.ExtraEventEndTime, GetDateTimeMS(CalEndDate.Year, CalEndDate.Month, CalEndDate.Day, CalEndDate.Hour, CalEndDate.Minute));

                //intent.PutExtra(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(2018, 02, 22, 15, 25));
                //intent.PutExtra(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(2018, 02, 22, 16, 0));

                intent.PutExtra(CalendarContract.Events.InterfaceConsts.DisplayColor, "#3897f0");
                intent.PutExtra(CalendarContract.Events.InterfaceConsts.AccountName, "michaelchrystal89@gmail.com");

                intent.PutExtra(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
                intent.PutExtra(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");
                intent.SetData(CalendarContract.Events.ContentUri);
                //intent.SetDataAndNormalize(CalendarContract.SyncColumns.AccountName, "michaelchrystal89@gmail.com");



                ((Activity)Forms.Context).StartActivity(intent);
                //((Activity)Forms.Context).s(intent);


                long GetDateTimeMS(int yr, int month, int day, int hr, int min)
                {
                    Android.Icu.Util.Calendar c = Android.Icu.Util.Calendar.GetInstance(Android.Icu.Util.TimeZone.Default);
                    // Android.Icu.Util.Calendar c = Calendar.
                    c.Set(Android.Icu.Util.Calendar.DayOfMonth, day);
                    c.Set(Android.Icu.Util.Calendar.HourOfDay, hr);
                    c.Set(Android.Icu.Util.Calendar.Minute, min);
                    c.Set(Android.Icu.Util.Calendar.Month, month - 1);
                    c.Set(Android.Icu.Util.Calendar.Year, yr);


                    return c.TimeInMillis;
                }

            }
            catch (Exception cal)
            {
                System.Diagnostics.Debug.WriteLine("   Code: " + cal.Data);
                System.Diagnostics.Debug.WriteLine("Message: " + cal.Message);
                System.Diagnostics.Debug.WriteLine("Source: " + cal.StackTrace);
                throw;
            }
            //====================================================================================================

            await Navigation.PushAsync(new MenuPage(clientName5));
        }
    }
}