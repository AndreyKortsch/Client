using LearningXamarin.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tensorflow;
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
using Microsoft.ML;
using Microsoft.ML.Data;
using Network;
using System.Reflection;
using static Xamarin.Essentials.Permissions;
using Tensorflow.Framework.Models;
using Tensorflow.Contexts;
using Network.Models;
using Microsoft.ML.Transforms;
using Tensorflow.Keras.Engine;
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
        public async void SendReguest(string username, string password)
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
            HttpResponseMessage response = await client.PostAsync("http://192.168.0.104:8080/api/auth/image", content);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var Items = JsonConvert.DeserializeObject<Network.Models.ImageData>(responseContent);
                Console.WriteLine("");
                await DisplayAlert("Изображение", Items.Image.Class, "Принять");
                var nextPage = new CameraViewPage();
                
                // Используйте Navigation.PushAsync() для перехода на новую страницу
                await Navigation.PushAsync(nextPage);
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count-2]);

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
                await DisplayAlert("Ошибка авторизации", "Неверный логин или пароль", "Принять");

            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
           //LoadTensorFlowModel();
           //await DisplayAlert("Success", LoadModel(), "OK");
            //LoadTensorFlowModel();
            string image = Convert.ToBase64String(Getimagebytes());
            String token = Preferences.Get("token", "");
            SendReguest(token, image);
            //await DisplayAlert("Success", LoadModel(), "OK");


        }

        public String LoadModel()
        {
            //string modelFile = "path_to_your_model.pb";
            //graph = new TFGraph();
            //session = new TFSession(graph);
            //graph.Import(File.ReadAllBytes(modelFile));
            // Создание среды ML.NET
            MLContext mlContext = new MLContext();
            var Path = "/storage/emulated/0/Pictures/model";
            // Создание экземпляра предварительно обученной модели
            //ITransformer trainedModel = mlContext.Model.Load("/storage/emulated/0/Pictures/saved_model.pb", out var modelSchema);
            var trainedModel = mlContext.Model.LoadTensorFlowModel(Path)            ;
            //var trainedModel2 = mlContext.Transforms.Model.LoadTensorFlowModel(Path);

            //IEstimator<ITransformer> estimator = trainedModel.ScoreTensorFlowModel("Const", "serving_default_inputs");
            //IDataView dataView = mlContext.Data.LoadFromEnumerable(new List<InputData>());
            //ITransformer transformer = estimator.Fit(dataView);

            //var predictionEngine = mlContext.Model.CreatePredictionEngine<InputData, OutputData>(transformer);
            //Getimagebytes();
            string base64String = Convert.ToBase64String(Getimagebytes());
            String x = Preferences.Get("token", "");
            // Передача входных данных для предсказания
            //var prediction = predictionEngine.Predict(new InputData() { Image = Getimagebytes() });

            // Использование предсказанной метки
            //Console.WriteLine($"Predicted label: {prediction.Scores}");
            return sd2;
            //return prediction.Scores;
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