using jamesMont.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MvvmHelpers;
using jamesMont.Services;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage.Blob;
using JulMar.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace jamesMont.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        string address;
        private static string nameForBlob;
        public static string hair;
        public static string gender;

        static string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=commblob2;AccountKey=fU7XsTmlYv6VgnFvYlPxWEcT8KBAineKA5JO+iMoBzAo0x5cB0ELj/m5clUl8X8OhrsVoYXCo4ELyhillPyPIA==;EndpointSuffix=core.windows.net";
        // *** Update or verify the following values. ***
        // **********************************************

        // Replace the subscriptionKey string value with your valid subscription key for face API.
        const string subscriptionKey = " 8ab035cf5d6a421d9c6a0ebfd399d5b7";
        // Replace or verify the region.
        //
        // You must use the same region in your REST API call as you used to obtain your subscription keys.
        // For example, if you obtained your subscription keys from the westus region, replace 
        // "westcentralus" in the URI below with "westus".
        //
        // NOTE: Free trial subscription keys are generated in the westcentralus region, so if you are using
        // a free trial subscription key, you should not need to change this region.
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";






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
                    if (gender != null && hair != null)
                    {
                        if (pass == confirm)
                        {
                            var coffee = await azureService.AddUser(first, second, em, pass, phone, hair, gender);
                            Coffees.Add(coffee);
                            await DisplayAlert("Alert", "Account Created", "Ok");
                            await Navigation.PushAsync(new MenuPage(first));
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Passwords do not match!", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Account cannot be created without a photo", "Ok");
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

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            try
            {

                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync
                       (new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                       { Directory = "CommDir", SaveToAlbum = true, /*Name = "Comm",*/ DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front, CompressionQuality = 92, });

                address = photo.Path;
                //====================================================
                sendToBlob(address);

                //=================================================
                async  void sendToBlob(string address)
                {
                    // Microsoft.Azure.Storage.CloudStorageAccount account = Microsoft.Azure.Storage.CloudStorageAccount.Parse(storageConnectionString);

                    Microsoft.WindowsAzure.Storage.CloudStorageAccount account2 = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(storageConnectionString);

                    //var SaveImage = CrossMedia.Current.PickPhotoAsync();

                    // Create the blob client.
                    CloudBlobClient blobClient = account2.CreateCloudBlobClient();
                    // Retrieve reference to a previously created container.
                    CloudBlobContainer container = blobClient.GetContainerReference("images");
                    await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                    // Create the container if it doesn't already exist.
                    await container.CreateIfNotExistsAsync();
                   
                    //string jpgName = nameForBlob.ToString() + dt.ToString();
                    // Retrieve reference to a blob named "myblob".
                    nameForBlob = email.Text;
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nameForBlob+ ".jpg");
                    //await DisplayAlert("Alert", address, "OK");
                    await blockBlob.UploadFromFileAsync(address);//needs to be path
                   

                    string boom = "https://commblob2.blob.core.windows.net/images";

                    string uri = boom + "/" + nameForBlob + ".jpg";
                    
                    MakeAnalysisRequest(address);
                }
            }

            catch (Exception er)
            {
                await DisplayAlert("Alert", "Error: "+er, "Ok");
            }
        }


        //=================================
        async void MakeAnalysisRequest(string imageFilePath)
        {
            try
            {
                await DisplayAlert("Alert", "Please Wait while we process the image: ", "Ok");
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                //string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";


                // Request parameters. A third optional parameter is "details".
                string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";



                // Assemble the URI for the REST API Call.
                string uri = uriBase + "?" + requestParameters;

                HttpResponseMessage response;

                // Request body. Posts a locally stored JPEG image.
                byte[] byteData = GetImageAsByteArray(imageFilePath);

                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses content type "application/octet-stream".
                    // The other content types you can use are "application/json" and "multipart/form-data".
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    // Execute the REST API call.
                    response = await client.PostAsync(uri, content);
                    // Get the JSON response.
                    var contentString = await response.Content.ReadAsStringAsync();
                    if (!contentString.Contains("face"))
                    {
                        await DisplayAlert("No face detected", "No face Detected in Image", "OK");

                    }
                    else
                    {
                        Debug.WriteLine("json back: " + contentString);
                        // Face face1 = Newtonsoft.Json.JsonConvert.DeserializeObject<jamesMont.Model.Face>(contentString);
                        // var results = JsonConvert.DeserializeObject<Face>(contentString);
                        var myobjList = Json.Deserialize<List<Face>>(contentString);
                        var myObj = myobjList[0];
                        // Debug.WriteLine(results.faceAttributes.age);
                       
                        //fill var to display on page
                       
                        gender = myObj.faceAttributes.gender;


                        hair = myObj.faceAttributes.hair.hairColor[0].color;
                        
                        await DisplayAlert("Alert","You are a "+gender+" with "+hair+" hair,","OK");
                    }
                }
            }
            catch (Exception analysis)
            {
                System.Diagnostics.Debug.WriteLine("   Code: " + analysis.Data);
                System.Diagnostics.Debug.WriteLine("Message: " + analysis.Message);
                System.Diagnostics.Debug.WriteLine("Source: " + analysis.StackTrace);
                throw;
            }
        }


        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }




    }

        }