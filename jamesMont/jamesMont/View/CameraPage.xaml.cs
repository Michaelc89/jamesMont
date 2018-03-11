using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;//for camera
using Plugin.Media.Abstractions;
using Android.App;
using Microsoft.WindowsAzure; // Namespace for CloudConfigurationManager
using Microsoft.Azure;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using Android.Content;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using jamesMont.Model;
using System.Diagnostics;
using System.Collections.ObjectModel;
using JulMar.Serialization;
//notifications
using Android.Util;

namespace jamesMont.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        string clientName;
        static string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=commblob2;AccountKey=fU7XsTmlYv6VgnFvYlPxWEcT8KBAineKA5JO+iMoBzAo0x5cB0ELj/m5clUl8X8OhrsVoYXCo4ELyhillPyPIA==;EndpointSuffix=core.windows.net";
        //  DefaultEndpointsProtocol=https;AccountName=commblob2;AccountKey=fU7XsTmlYv6VgnFvYlPxWEcT8KBAineKA5JO+iMoBzAo0x5cB0ELj/m5clUl8X8OhrsVoYXCo4ELyhillPyPIA==;EndpointSuffix=core.windows.net
        private Android.Net.Uri photo;//image uri android
        static CloudBlobClient blobClient;
        const string blobContainerName = "webappstoragedotnet-imagecontainer";
        static CloudBlobContainer blobContainer;
        public byte[] memoryStream;
        string address;
        string age;
        private static string gender;
        //private static string hair;
        public static string hair;
        private static string glasses2;
        private static string nameForBlob;


        // *** Update or verify the following values. ***
        // **********************************************

        // Replace the subscriptionKey string value with your valid subscription key for face API.
        const string subscriptionKey = " 8ab035cf5d6a421d9c6a0ebfd399d5b7";
        //687d47c1dd3144d39d484310a391b73f
        // Replace or verify the region.
        //
        // You must use the same region in your REST API call as you used to obtain your subscription keys.
        // For example, if you obtained your subscription keys from the westus region, replace 
        // "westcentralus" in the URI below with "westus".
        //
        // NOTE: Free trial subscription keys are generated in the westcentralus region, so if you are using
        // a free trial subscription key, you should not need to change this region.
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";
        //https://westcentralus.api.cognitive.microsoft.com/face/v1.0

        public CameraPage(string Name)
        {
            InitializeComponent();
            genderLabel.Text = "We think you are a: " + gender;
            clientName = Name;
            HiLabel.Text = "Hi, " + Name;
            HiLabel.FontSize = 20;
            nameForBlob = Name;

            // genderLabel.IsVisible = true;
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync
                   (new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                   { Directory = "CommDir", SaveToAlbum = true, /*Name = "Comm",*/ DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front, CompressionQuality = 92, });

                address = photo.Path;

                if (photo != null)
                {

                    PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });//displays on page

                }

                //Get the public album path
                var aPpath = photo.AlbumPath;
                //await DisplayAlert("File Location", photo.Path, "OK");

                //Get private path
                var path = photo.Path;

                // await DisplayAlert("Alert", "Payment Successful! Thank You", "Ok");

                //await Navigation.PushAsync(new ContactPage());

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("   Code: " + ex.Data);
                System.Diagnostics.Debug.WriteLine("Message: " + ex.Message);
                throw;
            }
        }

        private async void Select_Image(object sender, EventArgs e)
        {
            try
            {
                if (address == null)
                {
                    await DisplayAlert("Alert", "Please take a Picture or choose one from your gallery", "OK");
                }
                else
                {
                    sendToBlob(address);
                    await DisplayAlert("Alert", "Image successfully sent to your stylist", "OK");
                }

            }
            catch (Exception a)
            {
                System.Diagnostics.Debug.WriteLine("   Code: " + a.Data);
                System.Diagnostics.Debug.WriteLine("Message: " + a.Message);
                System.Diagnostics.Debug.WriteLine("Source: " + a.StackTrace);
                throw;
            }
        }

        private async void Choose_Image_and_Send_Clicked(object sender, EventArgs e)
        {
            try
            {
                //DateTime dT = DateTime.Now;

                //// Microsoft.Azure.Storage.CloudStorageAccount account = Microsoft.Azure.Storage.CloudStorageAccount.Parse(storageConnectionString);

                //Microsoft.WindowsAzure.Storage.CloudStorageAccount account2 = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(storageConnectionString);

                ////var SaveImage = CrossMedia.Current.PickPhotoAsync();

                //// Create the blob client.
                //CloudBlobClient blobClient = account2.CreateCloudBlobClient();

                //// Retrieve reference to a previously created container.
                //CloudBlobContainer container = blobClient.GetContainerReference("images");

                //await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                //// Create the container if it doesn't already exist.
                //await container.CreateIfNotExistsAsync();

                //// Retrieve reference to a blob named "myblob".
                //CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob.jpg");

                //// Create the "myblob" blob with the text "Hello, world!"
                ////await blockBlob.UploadTextAsync("Hello, world!");
                MediaFile j = await CrossMedia.Current.PickPhotoAsync();
                // await blockBlob.UploadFromFileAsync(j);
                // j.cop
                //await blockBlob.UploadFromFileAsync(j.Path);
                sendToBlob(j.Path);
                PhotoImage.Source = ImageSource.FromStream(() => { return j.GetStream(); });
                await DisplayAlert("Alert", "Image successfully sent to your stylist", "OK");

            }
            catch (Exception a)
            {
                System.Diagnostics.Debug.WriteLine("   Code: " + a.Data);
                System.Diagnostics.Debug.WriteLine("Message: " + a.Message);
                System.Diagnostics.Debug.WriteLine("Source: " + a.StackTrace);
                throw;
            }
        }

        private async void Detect_Face(object sender, EventArgs e)
        {
            try
            {
                //ref the storage a/c string              
                Microsoft.WindowsAzure.Storage.CloudStorageAccount account2 = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(storageConnectionString);

                // Create the blob client.
                CloudBlobClient blobClient = account2.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("images");

                await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                // Create the container if it doesn't already exist.
                await container.CreateIfNotExistsAsync();

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob2.jpg");

                // Create the "myblob" blob with the text "Hello, world!"
                //set j = to what ever image is chosen
                MediaFile j = await CrossMedia.Current.PickPhotoAsync();
                //upload the blob with j
                await blockBlob.UploadFromFileAsync(j.Path);

                string imageFilePath = j.Path;
                PhotoImage.Source = ImageSource.FromStream(() => { return j.GetStream(); });

                // Execute the REST API call.
                MakeAnalysisRequest(imageFilePath);

                await Task.Delay(3000);


                //await DisplayAlert("Alert", "Glasses: " + glasses, "Close");
                //await DisplayAlert("Alert", "Beard: " + beard, "Close");
                //await DisplayAlert("Alert", "Gender: " + gender, "Close");
                //await DisplayAlert("Alert", "I Said Bitch " + gender, "Close");
                //  await Navigation.PushAsync(new CameraPage());


            }
            catch (Exception a)
            {
                System.Diagnostics.Debug.WriteLine("   Code: " + a.Data);
                System.Diagnostics.Debug.WriteLine("Message: " + a.Message);
                System.Diagnostics.Debug.WriteLine("Source: " + a.StackTrace);
                throw;
            }


        }

        async void MakeAnalysisRequest(string imageFilePath)
        {
            string boom;
            string Gender;
            try
            {

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
                        Debug.WriteLine("Glasses: " + myObj.faceAttributes.glasses);
                        Debug.WriteLine("beard: " + myObj.faceAttributes.facialHair.beard);
                        Debug.WriteLine("Gender: " + myObj.faceAttributes.gender);
                        //fill var to display on page
                        glasses2 = myObj.faceAttributes.glasses;
                        gender = myObj.faceAttributes.gender;


                        hair = myObj.faceAttributes.hair.hairColor[0].color;

                        this.genderLabel.IsVisible = true;
                        // this.genderLabel.Text = "We think you are : " + gender;
                        this.genderLabel.Text = "We think you are : " + gender;
                        this.genderLabel.FontSize = 22;

                        this.glassesLabel.IsVisible = true;
                        this.glassesLabel.Text = "You Have " + glasses2 + " on your face ";
                        this.glassesLabel.FontSize = 22;


                        await DisplayAlert("", "You Have " + glasses2 + " on your face", "Ok");
                        await DisplayAlert("", "We think you are : " + gender, "Ok");
                        /*  this.hairColorLabel.IsVisible = true;
                            this.hairColorLabel.Text = "You Have " + hair + " hair \n";
                            this.hairColorLabel.FontSize = 22;
                            */

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


        /// <summary>
        /// Formats the given JSON string by adding line breaks and indents.
        /// </summary>
        /// <param name="json">The raw JSON string to format.</param>
        /// <returns>The formatted JSON string.</returns>
        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }

            return sb.ToString().Trim();
        }

        async static void sendToBlob(string address)
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

            DateTime dt = DateTime.Now;
            string jpgName = nameForBlob.ToString() + dt.ToString();
            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(jpgName + ".jpg");

            //await DisplayAlert("Alert", address, "OK");
            await blockBlob.UploadFromFileAsync(address);//needs to be path

        }
    }
}