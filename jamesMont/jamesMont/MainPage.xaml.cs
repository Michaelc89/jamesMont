using System;
using Xamarin.Forms;
using jamesMont.Services;
using System.Diagnostics;
using jamesMont.View;
using System.Collections.ObjectModel;

namespace jamesMont
{
    public partial class MainPage : ContentPage
    {
        public static ObservableCollection<string> UserEmail { get; } = new ObservableCollection<string>();
        public static ObservableCollection<string> UserName { get; } = new ObservableCollection<string>();
        public static string emailBoi;
        public MainPage()
        {
            InitializeComponent();
            email.Text = "";
            pass.Text = "";
            UserEmail.Clear();
            UserName.Clear();
            image.Source = "http://thsthehairsalon.com/wp-content/uploads/2016/04/logo-1-e1461630552354.png";
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
                UserName.Add(Name);
                if (login == true)
                {
                    UserEmail.Add(enteredEmail);
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