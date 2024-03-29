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
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

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
        public async Task SendReguest(string username, string password) {
            // Создаем экземпляр HttpClient
            HttpClient client = new HttpClient();
            // Отправляем POST-запрос на сервер
            try
            {
                var values = new Dictionary<string, string>
             {
                {"username",username},
                {"password",password}
             };
                var content = new FormUrlEncodedContent(values);
                Stream resourceStream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Network.appsettings.json");
                var configuration = new ConfigurationBuilder()
                    .AddJsonStream(resourceStream)
                    .Build();
                string server = configuration["Url"];
                HttpResponseMessage response = await client.PostAsync(server+"/api/auth/signin", content);
                if (response.IsSuccessStatusCode)
                {
                    //Получаем ответ от сервера
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var Items = JsonConvert.DeserializeObject<User>(responseContent);
                    await DisplayAlert("Авторизация", "Авторизация прошла успешно", "Принять");
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
            await SendReguest(username, password);
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
