using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Xamarin.Essentials;
using Plugin.Media;
//*using ImageResizer;
//using FreeImageResizer;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using System.Drawing;
using Plugin.Media.Abstractions;
using Network;

namespace LearningXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CameraViewPage : ContentPage
    {
        
        public CameraViewPage()
        {
            InitializeComponent();
            imgViewPanel.IsVisible = false;

        }

        async private void CaptureImage(object sender, EventArgs e)
        {
            //xctCameraView.Shutter();
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 100,
                //PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 40,
                SaveToAlbum=true,
                Name = "myimage.jpg",
                Directory = "sample"

            });
            if (file != null)
            {
                String photoBytes = null;
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    photoBytes = memoryStream.ToString();
                    Console.Write(photoBytes);
                    Preferences.Set("file2", file.Path);
                    Preferences.Set("file", photoBytes);
                    file.Dispose();
                    memoryStream.Flush();
                }
                var nextPage = new ImageViewPage();
                //memoryStream.Flush();
                await Navigation.PushAsync(nextPage);

            }
            // Используйте Navigation.PushAsync() для перехода на новую страницу
            //await Navigation.PushAsync(nextPage);
            //Preferences.Set("Image", photoBytes);
           // Image image = Image.FromStream(file.GetStream());
            //using (var originalImageStream = file.GetStream())
            //{
            //var originalImageStream = file.GetStream();
            //    string modifiedImagePath = "path/to/modified/image.jpg";
            //   ImageBuilder a = new ImageBuilder(); 
            //   a.Build(new ImageJob(originalImageStream, modifiedImagePath, new Instructions("maxwidth=100&maxheight=100&format=jpg")), false);
            // }
            // ImageBuilder a=ImageBuilder.Current()
            //ImageResizer a = new FreeImageResizer();
            //          byte[] resizedImage = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(originalImageStream, 500, 1000);
            //file.GetStream
            if (file == null)
            {
                await DisplayAlert("Success", "Login successful", "OK");
                return;
            }
          
        }
        private void RecordVideo(object sender, EventArgs e)
        {
            //xctCameraView.Shutter();
            btnstopVideo.IsEnabled = true;
            btnrecordVideo.IsEnabled = false;


        }
        private void StopVideo(object sender, EventArgs e)
        {
            xctCameraView.Shutter();
            btnrecordVideo.IsEnabled = true;
            btnstopVideo.IsEnabled = false;

        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (xctCameraView.CaptureMode == CameraCaptureMode.Photo)
            {
                captureMode.Text = "Video";
                xctCameraView.CaptureMode = CameraCaptureMode.Video;

                captureBtn.IsEnabled = false;
                btnrecordVideo.IsEnabled = true;
                btnstopVideo.IsEnabled = false;
            }
            else
            {
                captureMode.Text = "Photo";
                xctCameraView.CaptureMode = CameraCaptureMode.Photo;

                captureBtn.IsEnabled = true;
                btnrecordVideo.IsEnabled = false;
                btnstopVideo.IsEnabled = false;
            }
        }

        private void MediaCaptured(object sender, MediaCapturedEventArgs e)
        {

            imgView.Source = e.Image;
            imgViewPanel.IsVisible = true;
        }

        private void CloseImageView(object sender, EventArgs e)
        {
            imgViewPanel.IsVisible = false;
        }
    }
}