using LearningXamarin.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Drawing;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Win32;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Drawing.Imaging;
using SixLabors.ImageSharp.Formats.Png;
using System.Runtime.InteropServices.ComTypes;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Shapes;
using SixLabors.ImageSharp.Formats.Jpeg;
using SkiaSharp;
using Network;
using System.Reflection;
using static Xamarin.Essentials.Permissions;
using Network.Models;
using System.Collections;


namespace Network
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageViewPage : ContentPage
    {
        String sd2;
        SKImageInfo inputInfo;
        byte[] byteData;
        public ImageViewPage()
        {
            InitializeComponent();
            sd2 = Preferences.Get("file2", "");
            using (MemoryStream inStream = new MemoryStream())
            {
                image.Source = ImageSource.FromFile(sd2);
                image.MinimumHeightRequest = 900;
                image.MinimumWidthRequest = 800;
                inStream.Flush();
            }

          
        }
        public byte[] Getimagebytes()
        {
            // Путь к изображению
            string imagePath =Preferences.Get("file2", "");
            // Загрузка изображения с помощью SkiaSharp
            SKBitmap bitmap = SKBitmap.Decode(imagePath);
            // Приведение размера изображения к необходимым размерам для входа в нейронную сеть
            inputInfo = new SKImageInfo(100, 100);
            SKBitmap resizedBitmap = bitmap.Resize(inputInfo, SKFilterQuality.High);
            // Преобразование изображения в массив байтов
            using (SKData data = resizedBitmap.Encode(SKEncodedImageFormat.Jpeg, 100))
            {
                byteData = data.ToArray();
            }
            //DisplayAlert("Success", System.Text.Encoding.Default.GetString(byteData), "OK");
            return byteData;
        }
        public async Task SendReguest(string username, string password)
        {
            // Создаем экземпляр HttpClient
            HttpClient client = new HttpClient();
            // Отправляем POST-запрос на сервер
            var values = new Dictionary<string, string>
             {
                { "accessToken", username },
                { "image", password }
            };
            var content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await client.PostAsync("https://true-rules-like.loca.lt/api/auth/image", content);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var Items = JsonConvert.DeserializeObject<Network.Models.ImageData>(responseContent);
                Console.WriteLine("");
                await DisplayAlert("Изображение", Items.Image.Class, "Принять");
                var nextPage = new AddGoods();
                
                // Используйте Navigation.PushAsync() для перехода на новую страницу
                await Navigation.PushAsync(nextPage);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count-2]);
                Preferences.Set("classname", Items.Image.Class);
                //await DisplayAlert("Авторизация", Items.accessToken, "Принять");
                //await DisplayAlert("Авторизация", "Авторизация прошла успешно", "Принять");
                // var nextPage = new CameraViewPage();
                //Preferences.Clear();
                //Preferences.Set("token", Items.accessToken);
                // Используйте Navigation.PushAsync() для перехода на новую страницу
                //await Navigation.PushAsync(nextPage);
            }
            else
            {
                await DisplayAlert("Ошибка", "Ошибка обработки запроса", "Принять");

            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
           //LoadTensorFlowModel();
           //await DisplayAlert("Success", LoadModel(), "OK");
            //LoadTensorFlowModel();
            string image = Convert.ToBase64String(Getimagebytes());
            String token = Preferences.Get("token", "");
            await SendReguest(token, image);
            //await DisplayAlert("Success", LoadModel(), "OK");


        }

       
        public async void LoadTensorFlowModel()
        {
            
            string modelPath = "/storage/emulated/0/Pictures/saved_model.pb";

            if (File.Exists(modelPath))
            {
                await DisplayAlert("Success", System.Text.Encoding.Default.GetString(Getimagebytes()), "OK");
            }
            else
            {
                await DisplayAlert("Error", "No", "OK");
            }
           //var graph = new ScopedTFGraph();
        }
        
    }
}