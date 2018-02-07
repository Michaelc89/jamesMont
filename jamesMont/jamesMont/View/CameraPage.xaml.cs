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
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;


namespace jamesMont.View
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraPage : ContentPage
	{
        
		public CameraPage ()
		{
			InitializeComponent ();
		}
        
        
        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            try
            {

            
             var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync
                (new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                { Directory = "CommDir",SaveToAlbum = true, /*Name = "Comm",*/ DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front, CompressionQuality = 92, });
                

                if (photo != null)
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });//displays on page

                
                //var files = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                //{
                //    SaveToAlbum = true
                //});

                //Get the public album path
                //var aPpath = files.AlbumPath;




                //var DefCamera = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                //{
                //     DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front

                //});

                //var CompressPic = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                //{
                //    CompressionQuality = 92,
                //});

                //Get the public album path
                var aPpath = photo.AlbumPath;
                await DisplayAlert("File Location", photo.Path, "OK");

                //Get private path
                var path = photo.Path;

                DisplayAlert("Alert", "Payment Successful! Thank You", "Ok");

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
            if (CrossMedia.Current.IsPickPhotoSupported)
                 await CrossMedia.Current.PickPhotoAsync();
            var SaveImage = CrossMedia.Current.PickPhotoAsync();//save the image

            /******************************************/
            //container for blob 
            // Parse the connection string and return a reference to the storage account.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            //    CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Retrieve storage account from connection string.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=commblob2;AccountKey=fU7XsTmlYv6VgnFvYlPxWEcT8KBAineKA5JO+iMoBzAo0x5cB0ELj/m5clUl8X8OhrsVoYXCo4ELyhillPyPIA==;EndpointSuffix=core.windows.net");

            //// Create the blob client.
            //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            
            ////storageAccount.
            

            //// Retrieve reference to a previously created container.
            //CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            //// Create the container if it doesn't already exist.
            //await container.CreateIfNotExistsAsync();

            //// Retrieve reference to a blob named "myblob".
            //CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

            //// Create the "myblob" blob with the text "Hello, world!"
            //await blockBlob.UploadTextAsync("Hello, world!");
        }

        private async void Exit_Clicked(object sender, EventArgs e)
        {
            
        }
        //private void CreateDirectoryForPictures()
        //{// pathToNewFolder was a var
        //    string pathToNewFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/AppPictures";
        //    Directory.CreateDirectory(pathToNewFolder);
        //    if (!pathToNewFolder.Contains(null))
        //    {//if doesnt exist make one
        //        pathToNewFolder.();

        //    }
        //}

        //private void CreateDirectoryForPictures2()
        //{
        //    App._dir = new File(
        //        Environment.GetExternalStoragePublicDirectory(
        //            Environment.DirectoryPictures), "CameraAppDemo");
        //    if (!App._dir.Exists())
        //    {
        //        App._dir.Mkdirs();
        //    }
        //}


        //private void CreateDirectory()
        //{
        //      String appDirectoryName = "XYZ";
        //     File imageRoot = new File(Environment.getExternalStoragePublicDirectory(
        //            Environment.DIRECTORY_PICTURES), appDirectoryName);
        //    //https://stackoverflow.com/questions/20523658/how-to-create-application-specific-folder-in-android-gallery/20523934
        //}

    }
}
