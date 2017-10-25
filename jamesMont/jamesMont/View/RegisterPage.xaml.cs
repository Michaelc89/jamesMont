using jamesMont.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MvvmHelpers;
using jamesMont.Services;
using System.Diagnostics;

namespace jamesMont.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
        }

        async void createAccount(object sender, System.EventArgs e)
        {
            if (Fname.Text == null || Sname.Text == null || email.Text == null || pass.Text == null || conf_Pass.Text == null || phone.Text == null)
            {
                await DisplayAlert("Alert", "Not all fields have been entered", "Ok");
            }
            else
            {
                User userOne = new User(Fname.Text, Sname.Text, email.Text, pass.Text, phone.Text);

                await ExecuteAddCoffeeCommandAsync(userOne.FirstName, userOne.Surname, userOne.Email, userOne.Password, userOne.Phone);
            }
        }

        AzureService azureService;

        public ObservableRangeCollection<User> Coffees { get; } = new ObservableRangeCollection<User>();

        async Task ExecuteAddCoffeeCommandAsync(string first, string second, string em, string pass, string phone)
        {
            string confirm = conf_Pass.Text;
            azureService = new AzureService();
            
            if (IsBusy)
                return;
            bool exists ;
            try
            {
                IsBusy = true;
                exists = await azureService.CheckUser(em);
                if (exists == false)
                {
                    if (pass == confirm)
                    {
                        var coffee = await azureService.AddUser(first, second, em, pass, phone);
                        Coffees.Add(coffee);
                        await DisplayAlert("Alert", "Account Created", "Ok");
                        await Navigation.PushAsync(new MenuPage(first));
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Passwords do not match!", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("da error: " + ex);

            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}