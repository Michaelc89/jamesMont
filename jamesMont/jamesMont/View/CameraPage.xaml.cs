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

namespace jamesMont.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {

        static string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=commblob2;AccountKey=fU7XsTmlYv6VgnFvYlPxWEcT8KBAineKA5JO+iMoBzAo0x5cB0ELj/m5clUl8X8OhrsVoYXCo4ELyhillPyPIA==;EndpointSuffix=core.windows.net";
        //  DefaultEndpointsProtocol=https;AccountName=commblob2;AccountKey=fU7XsTmlYv6VgnFvYlPxWEcT8KBAineKA5JO+iMoBzAo0x5cB0ELj/m5clUl8X8OhrsVoYXCo4ELyhillPyPIA==;EndpointSuffix=core.windows.net
        private Android.Net.Uri photo;//image uri android
        static CloudBlobClient blobClient;
        const string blobContainerName = "webappstoragedotnet-imagecontainer";
        static CloudBlobContainer blobContainer;
        public byte[] memoryStream;
        string address;

        public CameraPage()
        {
            InitializeComponent();
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
                await DisplayAlert("File Location", photo.Path, "OK");

                //Get private path
                var path = photo.Path;

                await DisplayAlert("Alert", "Payment Successful! Thank You", "Ok");

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

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob.jpg");

                // Create the "myblob" blob with the text "Hello, world!"
                //await blockBlob.UploadTextAsync("Hello, world!");
                //  MediaFile j = await CrossMedia.Current.PickPhotoAsync();
                // await blockBlob.UploadFromFileAsync(j);
                // j.cop
                await DisplayAlert("Alert", address, "OK");
                await blockBlob.UploadFromFileAsync(address);//needs to be path
            }
            catch (Exception a)
            {
                System.Diagnostics.Debug.WriteLine("   Code: " + a.Data);
                System.Diagnostics.Debug.WriteLine("Message: " + a.Message);
                System.Diagnostics.Debug.WriteLine("Source: " + a.StackTrace);
                throw;
            }
        }


        private async void Exit_Clicked(object sender, EventArgs e)
        {
            try
            {



                // Microsoft.Azure.Storage.CloudStorageAccount account = Microsoft.Azure.Storage.CloudStorageAccount.Parse(storageConnectionString);
                await DisplayAlert("Alert", "Image present1", "OK");
                Microsoft.WindowsAzure.Storage.CloudStorageAccount account2 = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(storageConnectionString);
                await DisplayAlert("Alert", "Image present2", "OK");
                //var SaveImage = CrossMedia.Current.PickPhotoAsync();


                // Create the blob client.
                CloudBlobClient blobClient = account2.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("images");

                await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                // Create the container if it doesn't already exist.
                await container.CreateIfNotExistsAsync();

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob.jpg");

                // Create the "myblob" blob with the text "Hello, world!"
                //await blockBlob.UploadTextAsync("Hello, world!");
                MediaFile j = await CrossMedia.Current.PickPhotoAsync();
                // await blockBlob.UploadFromFileAsync(j);
                // j.cop
                await blockBlob.UploadFromFileAsync(j.Path);
                //using (var memoryStream = new MemoryStream())
                //{
                //    j.GetStream().CopyTo(memoryStream);
                //    j.Dispose();
                //    memoryStream.ToArray();
                //    //create blob with image
                //   // await blockBlob.UploadFromByteArrayAsync(memoryStream, 1, memoryStream.Length);
                //   // await blockBlob.UploadFromByteArrayAsync(memoryStream);
                //}
                //options.AccessCondition = AccessCondition.None;
                //create blob with image
                //await blockBlob.UploadFromByteArrayAsync(memoryStream, 0, memoryStream.Length);
                //await blockBlob.UploadFromStreamAsync();

                await DisplayAlert("Alert", "Connection Successful! Thank You", "Close");

                await Navigation.PushAsync(new CameraPage());
            }
            catch (Exception a)
            {
                System.Diagnostics.Debug.WriteLine("   Code: " + a.Data);
                System.Diagnostics.Debug.WriteLine("Message: " + a.Message);
                System.Diagnostics.Debug.WriteLine("Source: " + a.StackTrace);
                throw;
            }
        }


     

    }
}