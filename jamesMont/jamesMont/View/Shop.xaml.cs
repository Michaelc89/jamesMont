using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using jamesMont.Model;
using jamesMont.Services;
using jamesMont.View;
using System.Collections.ObjectModel;


namespace jamesMont.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Shop : ContentPage
    {

        public static ObservableCollection<Shop_Two> ListViewItems2 { get; } = new ObservableCollection<Shop_Two>();
        string clientName;
        string theEmail;
        public Shop(string Name, string email)
        {
            InitializeComponent();
            clientName = Name;
            theEmail = email;
            ListViewItems2.Clear();
            loadCategories();
            
            listView.ItemsSource = ListViewItems2;

        }

        public void loadCategories()
        {
            AzureService3 azureService;
            azureService = new AzureService3();
            try
            {
                azureService.LoadCategories();

            }
            catch (Exception er)
            {
                DisplayAlert("Alert", "Could not load categories" + er, "Ok");
            }

        }



        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selection = e.SelectedItem as Shop_Two;

                await Navigation.PushAsync(new ProductPage(selection.ProductName, clientName, selection.Id, theEmail));

            }
        }
    }
}