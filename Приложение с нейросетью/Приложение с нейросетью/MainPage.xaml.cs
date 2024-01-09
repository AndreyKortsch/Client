using LearningXamarin.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            // Здесь вы можете выполнить проверку логина и пароля с вашим сервером или базой данных
            // Здесь пример простой проверки, использующий захардкоженные данные

            if (username == "admin" && password == "admin")
            {
                // Авторизация прошла успешно, выполните необходимые действия
                await DisplayAlert("Success", "Login successful", "OK");
                var nextPage = new CameraViewPage();

                // Используйте Navigation.PushAsync() для перехода на новую страницу
                await Navigation.PushAsync(nextPage);
            }
            else
            {
                // Неправильный логин или пароль, отобразите сообщение об ошибке
                await DisplayAlert("Error", "Invalid username or password", "OK");
            }
        }

    }
}
