using Android.App;
using Android.Content;
using Android.Provider;
using jamesMont.Services;
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
        string clientName5, procedure, email, styler;
        int slots;
        DateTime picked;

		public BookingConfirm ( string bookingTime, string clientName, int slot, DateTime p, string pro, string stylist)
		{
			InitializeComponent ();
            clientName5 = clientName;
            styler = stylist;
            picked = p;
            slots = slot;
            procedure = pro;

            foreach (var item in MainPage.UserEmail)
            {
                email = item;
            }
            LabelBook.Text = "Booking time: "+ bookingTime;
            LabelBook2.Text = "Booking Date: " + p.ToString("dd/MM/yyyy");

		}
        AzureService2 azureService;
        async private void book_method(object sender, EventArgs e)
        {
            azureService = new AzureService2();
           
            float len;
            len = await azureService.loadLength(procedure);
            await azureService.AddBooking(clientName5, slots, picked, procedure, email, len, styler);


            await azureService.CheckDiscount();

            var fromAddress = new MailAddress("commBooking@gmail.com", ".Comm");
            var toAddress = new MailAddress("mikeychristie@gmail.com", clientName5);
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

            Intent intent = new Intent(Intent.ActionInsert);
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Title, "Booking");
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Description, ".COMM Salon");
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(2018, 02, 22, 15, 25));
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(2018, 02, 22, 16, 0));
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.DisplayColor, "#3897f0");
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.AccountName, "michaelchrystal89@gmail.com");

            intent.PutExtra(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");
            intent.SetData(CalendarContract.Events.ContentUri);
            ((Activity)Forms.Context).StartActivity(intent);
            //((Activity)Forms.Context).s(intent);


            long GetDateTimeMS(int yr, int month, int day, int hr, int min)
            {
                Android.Icu.Util.Calendar c = Android.Icu.Util.Calendar.GetInstance(Android.Icu.Util.TimeZone.Default);
                // Android.Icu.Util.Calendar c = Calendar.
                c.Set(Android.Icu.Util.Calendar.DayOfMonth, day);
                c.Set(Android.Icu.Util.Calendar.HourOfDay, hr);
                c.Set(Android.Icu.Util.Calendar.Minute, min);
                c.Set(Android.Icu.Util.Calendar.Month, Android.Icu.Util.Calendar.December);
                c.Set(Android.Icu.Util.Calendar.Year, yr);


                return c.TimeInMillis;
            }


            await Navigation.PushAsync(new MenuPage(clientName5));
        }
    }
}