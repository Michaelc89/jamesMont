using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using jamesMont.Model;
using jamesMont.Services;
using System.Diagnostics;

namespace jamesMont.ViewModel
{
    public class CoffeesViewModel:BaseViewModel
    {
       /* string Email,  Pass;
        AzureService azureService;
        public CoffeesViewModel(string e, string pass){
           
            Email = e;
            Pass = pass;
            
            azureService = new AzureService();
          //  xy = jamesMont.ViewModel.CoffeesV
        }

        public ObservableRangeCollection<User> Coffees { get; } = new ObservableRangeCollection<User>();
        
       
        //Inital Command
         ICommand loadCoffeesCommand;
         public ICommand LoadCoffeesCommand =>
             loadCoffeesCommand ?? (loadCoffeesCommand = new Command(async () =>  await ExecuteLoadCoffeesCommandAsync()));
             
        //1st method
        async Task ExecuteLoadCoffeesCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var coffees =  azureService.GetCoffees();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO! "+ ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        ICommand addCoffeeCommand;
        public ICommand AddCoffeeCommand =>
            addCoffeeCommand ?? (addCoffeeCommand = new Command(async () => await ExecuteAddCoffeeCommandAsync()));


        async Task ExecuteAddCoffeeCommandAsync()
        {


            Debug.Write("Email: " + Email);
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var coffee = await azureService.AddCoffee(Email, Pass);

                Coffees.Add(coffee);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("da error: " + ex);

            }
            finally
            {
                IsBusy = false;
            }

        }*/

        
    }
}
