using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jamesMont.Model;
using jamesMont.Services;
using jamesMont.View;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace jamesMont.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Shop : ContentPage
	{
        public static ObservableCollection<ShoppingCategory> ShopCategories { get; } = new ObservableCollection<ShoppingCategory>();
        public Shop ()
		{
			InitializeComponent ();
            listView.ItemsSource = ShopCategories;
            loadCategories();

        }

        public void loadCategories()
        {
            AzureService2 azureService;
            azureService = new AzureService2();

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