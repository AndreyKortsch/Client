using LearningXamarin.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorFlow;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Drawing;
using Xamarin.Forms.Xaml;
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

namespace Network
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageViewPage : ContentPage
    {
        float[,,,] inputArray = new float[1, 100, 100, 3];
        String sd2;
        SKImageInfo inputInfo;
        byte[] byteData;
        public ImageViewPage()
        {
            InitializeComponent();
            // Установка фото в элемент Image
            //var bitmap = BitmapFactory.DecodeByteArray(photoBytes, 0, photoBytes.Length);
            //imageView.SetImageBitmap(bitmap);
            //String sd ="";
            String sd = Preferences.Get("file", "");
            sd2 = Preferences.Get("file2", "");

            //byte[] byteArray = Encoding.UTF8.GetBytes(sd);
            //Stream stream = new MemoryStream(byteArray);
            //MemoryStream stream = new MemoryStream(byteArray);
            //image.Source = ImageSource.FromStream(() => new MemoryStream(byteArray));
            //var skBitmap = ImageSource.FromFile(sd2);
            // Установите желаемое качество изображения (от 0 до 100)
            //var adjustedBitmap = ImageHelper.AdjustImageQuality(skBitmap,100);
            //var adjustedImageSource = SKBitmapImageSource.FromBitmap(adjustedBitmap);
            //image.Source = adjustedImageSource;
            using (MemoryStream inStream = new MemoryStream())
            {
                image.Source = ImageSource.FromFile(sd2);
                image.MinimumHeightRequest = 900;
                image.MinimumWidthRequest = 800;
                inStream.Flush();
            }
            // Путь к изображению
            string imagePath = sd2;
            
            // Загрузка изображения с помощью SkiaSharp
            SKBitmap bitmap = SKBitmap.Decode(imagePath);

            // Приведение размера изображения к необходимым размерам для входа в нейронную сеть
            inputInfo = new SKImageInfo(100,100);
            SKBitmap resizedBitmap = bitmap.Resize(inputInfo, SKFilterQuality.High);

            // Преобразование изображения в массив байтов
            using (SKData data = resizedBitmap.Encode(SKEncodedImageFormat.Jpeg, 100))
            {
                byteData = data.ToArray();
            }
            //image.
            //InitializeComponent();
            // Загрузка изображения
            //var image2 = SixLabors.ImageSharp.Image.Load(sd2);
            //byte[] imageBytes;
            //var image2 = new Image { Source = sd2 };
            ///using (MemoryStream inStream = new MemoryStream())
            //{
              //  using (MemoryStream outStream = new MemoryStream())
              //  {
                //    using (SixLabors.ImageSharp.Image image2 = SixLabors.ImageSharp.Image.Load(sd2))
                  //  {
                        // Изменение размера изображения
                    //    image2.Mutate(x => x.
                        //Resize(new ResizeOptions
                        //{
                         //   Size = new SixLabors.ImageSharp.Size(100, 100),
                          //  Mode = ResizeMode.Pad,
                        //    Position = AnchorPositionMode.Center
                      //  }));
                        //path = Android.Environment.GetExternalStoragePublicDirectory(
                        //Android.OS.Environment.DirectoryPictures).AbsolutePath;

                        //string myPath = System.IO.Path.Combine(sd2, "file.name");
                        // Bitmap myImage = (Bitmap)Bitmap.FromFile(sd2);
                        //image2.SaveAsJpegAsync("/storage/emulated/0/Android/data/com.companyname.x_______________________/files/Pictures/sample/myimage.jpg");
                        //image2.SaveAsJpeg(Path.ChangeExtension(sd2, ".png"), new PngEncoder());
                        //imageBytes = outStream.ToArray();
                        //image2.Dispose();
                    //}
                   //outStream.Flush();
                //}
                //inStream.Flush();
            
            //image2.Save(Path.ChangeExtension(sd2, ".jpg"), new PngEncoder());
            //image2.Save(sd2+".png");
            //image2.Dispose();
            //   Bitmap myImage = (Bitmap)Bitmap.FromFile(sd2);
            // Преобразование изображения в массив байт
            //    byte[] imageBytes;
            //    using (var ms = new MemoryStream())
            //  {
            //      myImage.Save(ms, ImageFormat.Png);
            //      imageBytes = ms.ToArray();
            //  }

            // Создание целевого массива для входных данных модели
           //float[,,,] inputArray = new float[1,100,100,3];
           //Bitmap myImage = (Bitmap)Bitmap.FromFile("/storage/emulated/0/Android/data/com.companyname.x_______________________/files/Pictures/sample/шты.jpg");

            // Нормализация и заполнение целевого массива
            //for (int y = 0; y < 100; y++)
           // {
             //   for (int x = 0; x < 100; x++)
               // {
                   // var pixel = myImage.GetPixel(x, y);

                 //   inputArray[0, y, x, 0] = (pixel.R); // Канал красный
                  //  inputArray[0, y, x, 1] = (pixel.G); // Канал зеленый
                //    inputArray[0, y, x, 2] = (pixel.B); // Канал синий
                //}
           // }
       //}
        }
        public string ResizeImage(byte[] imageBytes, int height, int width)
        {
            byte[] image = new byte[] { };

            using (MemoryStream inStream = new MemoryStream(imageBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (SixLabors.ImageSharp.Image imageSharp = SixLabors.ImageSharp.Image.Load(inStream))
                    {
                        imageSharp.Mutate(x => x.Resize(width, height));
                        imageSharp.SaveAsJpeg(outStream);
                        imageSharp.Dispose();
                    }

                    image = outStream.ToArray();
                    outStream.Flush();
                    inStream.Flush();
                }
            }
            return Convert.ToBase64String(image);
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //var image2 = SixLabors.ImageSharp.Image.Load(sd2);

            //var image2 = new Image { Source = sd2 };
            // Изменение размера изображения
            //image2.Mutate(x => x.
            //    Resize(new ResizeOptions
            //    {
             //       Size = new SixLabors.ImageSharp.Size(100, 100),
             //       Mode = ResizeMode.Pad,
             //       Position = AnchorPositionMode.Center
             //   }));
            // Bitmap myImage = (Bitmap)Bitmap.FromFile(sd2);
            //image2.Save(Path.ChangeExtension(sd2, ".jpg"));
            //image2.Dispose();
            //image2.Save(Path.C
            LoadTensorFlowModel();
            //RunInference(inputArray);
           await DisplayAlert("Success", RunInference(inputArray), "OK");
            //await DisplayAlert("Success", "Good", "OK");
        }
        public async void LoadTensorFlowModel()
    {
        string modelPath = "/storage/emulated/0/Android/data/com.companyname.x_______________________/files/Pictures/sample/saved_model.pb";
            // string modelPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "/Assets/model.pb");
            //EmbeddedResourceHelper helper = new EmbeddedResourceHelper();
            //string modelPath = helper.GetFileContent("AboutResources.txt");
            // путь до сохраненной модели TensorFlow
            //string modelPath=System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //await DisplayAlert("Success", modelPath, "OK");
            //string resourceName = "model.pb";
            ///string resourcePath = DependencyService.Get<IPlatformSpecific>().GetResourcePath(resourceName);
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ResourceLocation)).Assembly;
            String stream = assembly.GetManifestResourceInfo("Network.model.pb").ResourceLocation.ToString();
            await DisplayAlert("Success", stream, "OK");

            //stream.
            TFGraph graph = new TFGraph();
            TFSession session = new TFSession(graph);
            //graph.Import(File.ReadAllBytes("Assets/model.pb"), "");
            
    }
        public String RunInference(float[,,,] input)
        {
            var graph = new TFGraph();
            using (var session = new TFSession(graph))
            {
                TFGraph graph2 = session.Graph;
                TFSession.Runner runner = session.GetRunner();
                TFOperation inputOperation = graph2["input"]; // имя операции входных данных в модели
                TFOutput inputTensor = inputOperation[0];
                TFTensor tensor = TFTensor.CreateString(byteData);
                runner.AddInput(inputTensor, tensor);
                //runner.AddInput(inputTensor, new TFTensor(input));
                TFOperation outputOperation = graph2["output"]; // имя операции выходных данных в модели
                TFOutput outputTensor = outputOperation[0];
                runner.Fetch(outputTensor);
                TFTensor[] output = runner.Run();
                return output[0].GetValue() as String;
            }
            //return output[0].GetValue() as float[];
        }

    }
}