using jamesMont.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace jamesMont.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        string clientName;
        public MenuPage(string Name)
        {
            InitializeComponent();
            clientName = Name;
            HiLabel.Text = "Hi, "+Name;
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
                await Navigation.PushAsync(new EditBookingPage());
            }
            catch (System.Exception er)
            {
                await DisplayAlert("Alert", "da error: " + er, "Ok");
            }
        }

        async void Shop(object sender, System.EventArgs e)
        {
            await DisplayAlert("Alert", "Shop", "Ok");
        }

        async void logOut(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}