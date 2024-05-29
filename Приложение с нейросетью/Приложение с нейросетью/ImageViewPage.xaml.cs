using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using SkiaSharp;
using Microsoft.Extensions.Configuration;
using System.Reflection;


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
            Stream resourceStream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Network.appsettings.json");
            var configuration = new ConfigurationBuilder()
                .AddJsonStream(resourceStream)
                .Build();
            string server = configuration["Url"];
            HttpResponseMessage response = await client.PostAsync(server+"/api/auth/image", content);
            string responseContent = await response.Content.ReadAsStringAsync();
            //Preferences.Set("classb", "вва");

            if (response.IsSuccessStatusCode)
            {
                var Items = JsonConvert.DeserializeObject<Models.ImageData>(responseContent);
                Preferences.Set("classb", Items.Image.Class);

                await DisplayAlert("Изображение", Items.Image.Class, "Принять");
                var nextPage = new AddGoods();
               // Preferences.Set("classb", "вва");
                // Используйте Navigation.PushAsync() для перехода на новую страницу
                await Navigation.PushAsync(nextPage);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count-2]);
                //Preferences.Set("classname", Items.Image.Class);
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
            Button loginButton=(Button)sender;
            loginButton.IsEnabled = false;
            String token = Preferences.Get("token", "");
            await SendReguest(token, image);
            loginButton.IsEnabled = true;
            // Preferences.Set("classname", Items.Image.Class);
            //await DisplayAlert("Success", LoadModel(), "OK");


        }




    }
}