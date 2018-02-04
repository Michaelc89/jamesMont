using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace jamesMont.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class cvc : ContentPage
    {


        string productN;
        int numb;
        public cvc(string productName, int number)
        {
            InitializeComponent();
            productN = productName;
            numb = number;


        }
         void SendPayment(object sender, System.EventArgs e)
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_BEPrGyKARA5fbK1rcLbAixdd");

                var chargeOptions = new StripeChargeCreateOptions()
                {
                    Amount = 2000,
                    Currency = "gbp",
                    SourceTokenOrExistingSourceId = "tok_visa",
                    Description = "thank you and enjoy your new "+productN,
                    ReceiptEmail ="s00164997@mail.itsligo.ie",
                    Metadata = new Dictionary<String, String>()
                    {
                        { "OrderId", "6735"}
                    }
                    
                };

                var chargeService = new StripeChargeService();
                StripeCharge charge = chargeService.Create(chargeOptions);
            
            }
            catch (StripeException ex)
            {
                switch (ex.StripeError.ErrorType)
                {
                    case "card_error":
                        System.Diagnostics.Debug.WriteLine("   Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("Message: " + ex.StripeError.Message);
                        break;
                    case "api_connection_error":
                        System.Diagnostics.Debug.WriteLine(" apic  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("apic Message: " + ex.StripeError.Message);
                        break;
                    case "api_error":
                        System.Diagnostics.Debug.WriteLine("api   Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("api Message: " + ex.StripeError.Message);
                        break;
                    case "authentication_error":
                        System.Diagnostics.Debug.WriteLine(" auth  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("auth Message: " + ex.StripeError.Message);
                        break;
                    case "invalid_request_error":
                        System.Diagnostics.Debug.WriteLine(" invreq  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("invreq Message: " + ex.StripeError.Message);
                        break;
                    case "rate_limit_error":
                        System.Diagnostics.Debug.WriteLine("  rl Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("rl Message: " + ex.StripeError.Message);
                        break;
                    case "validation_error":
                        System.Diagnostics.Debug.WriteLine(" val  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("val Message: " + ex.StripeError.Message);
                        break;
                    default:
                        // Unknown Error Type
                        break;
                }
            }
        }
        
}

}
