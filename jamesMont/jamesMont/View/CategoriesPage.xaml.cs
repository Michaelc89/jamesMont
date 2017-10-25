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
        public static ObservableCollection<Categories> ListViewItems2 { get; } = new ObservableCollection<Categories>();
        public CategoriesPage()
        {
            InitializeComponent();
            loadCategories();

            // var listView = new ListView();
            listView.ItemsSource = ListViewItems2;
           // Content = listView;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selection = e.SelectedItem as Categories;
               
                await Navigation.PushAsync(new BookingPage(selection.CategoryName));
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