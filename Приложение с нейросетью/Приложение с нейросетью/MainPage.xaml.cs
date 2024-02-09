using LearningXamarin.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Drawing;
using Newtonsoft.Json;
using Xamarin.CommunityToolkit.Behaviors;
using Xamarin.Essentials;
using Network.Models;

namespace Приложение_с_нейросетью
{
    public partial class MainPage : ContentPage
    {
        Entry usernameEntry, passwordEntry;
        Button loginButton;
        public MainPage()
        {
            InitializeUI();

        }
        private void InitializeUI()
        {
            Title = "Авторизация";
            usernameEntry = new Entry
            {
                Placeholder = "Username"
            };
            passwordEntry = new Entry
            {
                Placeholder = "Password",
                IsPassword = true
            };
            loginButton = new Button
            {
                Text = "Login"
            };
            loginButton.Clicked += OnLoginButtonClicked;

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    usernameEntry,
                    passwordEntry,
                    loginButton
                }
            };
        }
        public async void SendReguest(string username, string password) {
            // Получаем переданное фото
            //Bitmap photo = (Bitmap)Intent.GetParcelableExtra("photo");

            // Преобразуем фото в массив байтов
            //ByteArrayOutputStream stream = new ByteArrayOutputStream();
            //photo.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            //byte[] byteArray = stream.ToByteArray();

            // Создаем экземпляр HttpClient
            HttpClient client = new HttpClient();

            // Создаем экземпляр MultipartFormDataContent для отправки файла на сервер
            //MultipartFormDataContent content = new MultipartFormDataContent();
            //ByteArrayContent imageContent = new ByteArrayContent(byteArray);
            //.Content
            //    Content
            // Устанавливаем Content-Disposition заголовок для указания имени файла
            //imageContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            //{
            //    Name = "photo",
            //    FileName = "photo.jpg"
            //};

            // Добавляем фото в контент
            //content.Add(imageContent);
            //var content = new StringContent(JsonConvert.SerializeObject(new { username = username, password = password }));

            // Отправляем POST-запрос на сервер
            try
            {
                var values = new Dictionary<string, string>
             {
                { "username", username },
                { "password", password }
            };
                var content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = await client.PostAsync("https://shy-grapes-stand.loca.lt/api/auth/signin", content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    // Получаем ответ от сервера
                    //string responseContent = await response.Content.ReadAsStringAsync();
                    var Items = JsonConvert.DeserializeObject<User>(responseContent);
                    Console.WriteLine("");
                    await DisplayAlert("Авторизация", "Авторизация прошла успешно", "Принять");
                    //await DisplayAlert("Авторизация", "Авторизация прошла успешно", "Принять");
                    var nextPage = new CameraViewPage();
                    Preferences.Clear();
                    Preferences.Set("token", Items.accessToken);
                    // Используйте Navigation.PushAsync() для перехода на новую страницу
                    await Navigation.PushAsync(nextPage);
                }
                else
                {
                    await DisplayAlert("Ошибка авторизации", "Неверный логин или пароль", "Принять");

                }
            } catch(Exception ex)
            {
                await DisplayAlert("Ошибка подключения",ex.Message, "Принять");
            }
        }
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            loginButton.IsEnabled = false;
            SendReguest(username, password);
           loginButton.IsEnabled = true;
            // Здесь вы можете выполнить проверку логина и пароля с вашим сервером или базой данных
            // Здесь пример простой проверки, использующий захардкоженные данные

            //if (username == "admin" && password == "admin")
            //{
            // Авторизация прошла успешно, выполните необходимые действия
            //  await DisplayAlert("Авторизация", "Авторизация прошла успешно", "Принять");
            //  var nextPage = new CameraViewPage();

            // Используйте Navigation.PushAsync() для перехода на новую страницу
            // await Navigation.PushAsync(nextPage);
            //}
            //else
            //{
            // Неправильный логин или пароль, отобразите сообщение об ошибке
            //  await DisplayAlert("Ошибка авторизации", "Неверный логин или пароль", "Принять");
            //}
        }

    }
}
