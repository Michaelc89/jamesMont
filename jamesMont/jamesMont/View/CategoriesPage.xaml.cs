using jamesMont.Model;
using jamesMont.Services;
using jamesMont.View;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace jamesMont
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        string clientName2;
        public static ObservableCollection<Categories> ListViewItems2 { get; } = new ObservableCollection<Categories>();
        public CategoriesPage(string clientName)
        {
            InitializeComponent();
            clientName2 = clientName;
            loadCategories();
            
            listView.ItemsSource = ListViewItems2;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selection = e.SelectedItem as Categories;
               
                await Navigation.PushAsync(new BookingPage(selection.CategoryName, clientName2));
            }
        }

        public void loadCategories()
        {
            AzureService azureService;
            azureService = new AzureService();

            try
            {
                azureService.LoadCategories();
            }
            catch (Exception er)
            {
                 DisplayAlert("Alert", "Could not load categories" + er, "Ok");
            }

        }
    }
}