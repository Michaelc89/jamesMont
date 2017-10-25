using System;
using Xamarin.Forms;
using jamesMont.Services;
using System.Diagnostics;
using jamesMont.View;

namespace jamesMont
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            email.Text = "";
            pass.Text = "";
        }

        async private void Login_Method(object sender, EventArgs e)
        {
              ExecuteLoadCoffeesCommandAsync();
        }

        async void makeAccount(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }


        AzureService azureService;

        //1st method
        // async Task ExecuteLoadCoffeesCommandAsync()
        public async void ExecuteLoadCoffeesCommandAsync()
        {
            azureService = new AzureService();
            string enteredEmail = email.Text;
            string enteredPassword = pass.Text;

            bool login = false;
            string Name;
           
            try
            {
                login = await azureService.GetCoffee(enteredEmail, enteredPassword);
                Name = await azureService.GetUserName(enteredEmail, enteredPassword);

                if (login == true)
                {
                    
                    await Navigation.PushAsync(new MenuPage(Name));
                    pass.Text = "";
                }
                if (login == false)
                {
                    await DisplayAlert("Alert", "Email and/or Password is incorrect", "ok");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO! " + ex);
            }
        }
    }

}