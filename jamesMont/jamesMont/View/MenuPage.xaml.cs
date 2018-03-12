using jamesMont.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace jamesMont.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        string clientName;
        string TheEmail;
        public MenuPage(string Name)
        {
            InitializeComponent();
            clientName = Name;
           
            foreach (var item in MainPage.UserEmail)
            {
                TheEmail = item;
            }
          
            HiLabel.Text = "Hi, "+Name;
            HiLabel.FontSize =20;
            //icon2.Image = "http://www.clker.com/cliparts/n/5/p/D/9/H/glossy-black-icon-button-hi.png";

        }
        async void MakeABooking(object sender, System.EventArgs e)
        {
            try
            {
                 await Navigation.PushAsync(new CategoriesPage(clientName));
            }
            catch (System.Exception er)
            {
                await DisplayAlert("Alert", "da error: "+er, "Ok");
            }
        }
        async void EditABooking(object sender, System.EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new EditBookingPage(clientName));
            }
            catch (System.Exception er)
            {
                await DisplayAlert("Alert", "da error: " + er, "Ok");
            }
        }

        async void Shop(object sender, System.EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new Shop(clientName, TheEmail));
            }
            catch (System.Exception er)
            {
                await DisplayAlert("Alert", "da error: " + er, "Ok");
            }
        }
        async void contact(object sender, System.EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ContactPage(clientName));
            }
            catch (System.Exception er)
            {
                await DisplayAlert("Alert", "da error: " + er, "Ok");
            }
            }
        

        async void logOut(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}