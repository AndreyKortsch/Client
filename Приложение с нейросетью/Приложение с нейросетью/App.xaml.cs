using Network;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Приложение_с_нейросетью
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new AddGoods();
        }

        protected override void OnStart()
        {
            //MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(new AddGoods());

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
