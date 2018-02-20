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


            await Navigation.PushAsync(new MenuPage(clientName5));
        }
    }
}