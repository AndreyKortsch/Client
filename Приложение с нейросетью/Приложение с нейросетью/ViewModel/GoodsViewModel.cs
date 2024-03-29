using LearningXamarin.Views;
using Network.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Network.ViewModel
{
    public partial class GoodsViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Manufacturer> Manufacturer { get; set; }
         =new ObservableCollection<Manufacturer>();
        public ObservableCollection<Subbrand> Subbrand { get; set; }
        =new ObservableCollection<Subbrand>();
        public ObservableCollection<Model> Model { get; set; }
        = new ObservableCollection<Model>();
        public Command LoadDataCommand { get; set; }
        public async Task SendReguest(string token, string classname)
        {
            // Создаем экземпляр HttpClient
            HttpClient client = new HttpClient();
            Manufacturer.Clear();
            // Отправляем POST-запрос на сервер
            try
            {
                var values = new Dictionary<string, string>
             {
                { "accessToken", token },
                { "classname", classname }
            };
                var content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = await client.PostAsync(server+"/api/test/class", content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var Items = JsonConvert.DeserializeObject<ListManufacturer>(responseContent);
                    foreach (Manufacturer manufacturer in Items.manufactors)
                    {
                       Manufacturer.Add(new Manufacturer(manufacturer.id,manufacturer.name));
                    }
                }
                else
                {
                    Manufacturer.Add(new Manufacturer("none"));
                }
            }
            catch (Exception ex)
            {
                Manufacturer.Add(new Manufacturer(ex.Message));
            }
            finally
            { 
                client.Dispose(); 
            }
        }
        public async Task SendReguest2(string token, string manufactorid)
        {
            // Создаем экземпляр HttpClient
            HttpClient client = new HttpClient();
            // Отправляем POST-запрос на сервер
            try
            {
                var values = new Dictionary<string,string>
             {
                { "accessToken", token },
                { "manufactorid", manufactorid }
            };
                var content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = await client.PostAsync(server+"/api/test/manufactor", content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var Items = JsonConvert.DeserializeObject<ListSubbrand>(responseContent);
                    foreach (Subbrand subbrand in Items.subbrands)
                    {
                        Subbrand.Add(new Subbrand(subbrand.id,subbrand.name));
                    }
                }
                else
                {
                    Subbrand.Add(new Subbrand("none"));
                }
            }
            catch (Exception ex)
            {
                Manufacturer.Add(new Manufacturer(ex.Message));
            }
            finally
            {
                client.Dispose();
            }
        }
        public async Task SendReguest3(string token, string subbrandid)
        {
            // Создаем экземпляр HttpClient
            HttpClient client = new HttpClient();
            // Отправляем POST-запрос на сервер
            try
            {
                var values = new Dictionary<string, string>
             {
                { "accessToken", token },
                { "subbrandid", subbrandid }
            };
                var content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = await client.PostAsync(server + "/api/test/subbrand", content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var Items = JsonConvert.DeserializeObject<ListModel>(responseContent);
                    foreach (Model model in Items.models)
                    {
                        Model.Add(new Model(model.id,model.name,model.count,model.price));
                    }
                }
                else
                {
                    Model.Add(new Model("none"));
                }
            }
            catch (Exception ex)
            {
                Manufacturer.Add(new Manufacturer(ex.Message));
            }
            finally
            {
                client.Dispose();
            }
        }
        public async Task SendReguest4(string token, string subbrandid, string count)
        {
            // Создаем экземпляр HttpClient
            HttpClient client = new HttpClient();
            // Отправляем POST-запрос на сервер
            try
            {
                var values = new Dictionary<string, string>
             {
                { "accessToken", token },
                { "modelid", subbrandid },
                { "count", count }

            };
                var content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = await client.PostAsync(server + "/api/test/updatemodel", content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var Items = JsonConvert.DeserializeObject<ListModel>(responseContent);
                    //InputText = Items.models.ElementAt(0).count.ToString();
                    await page.DisplayAlert("Сообщение", "Количество товара успешно изменено\n" +
                        "Новое значение:" +
                        Items.models.ElementAt(0).count.ToString(), "Принять");
                }
                else
                {
                    await page.DisplayAlert("Сообщение", "Неверные данные запроса", "Принять");
                }
            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Сообщение", ex.Message, "Принять");
                //InputText = ex.Message;
            }
            finally
            {
                client.Dispose();
            }
        }
        private Goods _labelValue;//=new Goods();
        public Goods LabelValue
        {
            get { return _labelValue; }
            set
            {
                if (_labelValue != value)
                {
                    _labelValue = value;
                    OnPropertyChanged(nameof(LabelValue));
                }
            }
        }
        private string _inputText;
        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                OnPropertyChanged(nameof(InputText));
            }
        }
        Page page;
        public Command SubmitCommand { get; set; }
        public INavigation Navigation { get; set; }
        String server;
        public GoodsViewModel(Page page, INavigation navigation)
        {
            this.page = page;
            this.Navigation = navigation;
            
            Stream resourceStream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Network.appsettings.json");
            var configuration = new ConfigurationBuilder()
                .AddJsonStream(resourceStream)
                .Build();
            //LabelValue.Class= configuration["ChatHubUrl"];

            //Manufacturer = new ObservableCollection<Manufacturer>();
            String token = Preferences.Get("token", "");
            String classname = Preferences.Get("classname", "");
            //classname = "ABIS_BOOK";
            _labelValue = new Goods();
            server = configuration["Url"];
            LabelValue.Class = classname;
            //LabelValue.Count= 12;
            //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwiaWF0IjoxNzEwMTY1NTA1LCJleHAiOjE3MTAyNTE5MDV9.boHtTEEUYzk7fZI4o6l5x37bIVFW3hfPYdjPGzbKZ3g";
            LoadDataCommand = new Command(async () => await SendReguest(token, classname));
            SubmitCommand = new Command(OnSubmit);
            //DisplayAlert("Ошибка авторизации", "Неверный логин или пароль", "Принять");
            //await SendReguest(token, classname);
            //Subbrand = new ObservableCollection<Subbrand>();
            //Model= new ObservableCollection<Model>();
        }
        private async void OnSubmit()
        {
            String token = Preferences.Get("token", "");
            //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwiaWF0IjoxNzEwMTY1NTA1LCJleHAiOjE3MTAyNTE5MDV9.boHtTEEUYzk7fZI4o6l5x37bIVFW3hfPYdjPGzbKZ3g";
            await SendReguest4(token, SelectedModel.id.ToString(), InputText);

            //await page.DisplayAlert("Сообщение", "Количество товара успешно изменено", "Принять");
            // Обработка введенного var nextPage = new CameraViewPage();
            var nextPage = new CameraViewPage();
            // Используйте Navigation.PushAsync() для перехода на новую страницу
            await Navigation.PushAsync(nextPage);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

        }
        private Manufacturer _selectedCountry;
        public Manufacturer SelectedCountry
            {
                get { return _selectedCountry; }
                set
                {

                //IsSecondListVisible = true;
                if (_selectedCountry != value)
                    {
                        _selectedCountry = value;
                        IsSecondListVisible = true;
                        OnPropertyChanged(nameof(SelectedCountry));
                        LoadCitiesForCountry(_selectedCountry.id);
                        // Метод для загрузки городов для выбранной страны
                     }
                }
            }
          private Subbrand _selectedCity;
          public Subbrand SelectedCity
          {
                get { return _selectedCity; }
                set
                {
                    _selectedCity = value;
                    IsSecond2ListVisible = true;
                    OnPropertyChanged(nameof(SelectedCity));
                if (_selectedCity != null) 
                    LoadForCountry(_selectedCity.id);
                else
                {
                    IsSecond3ListVisible = false;
                }
            }
        }
        private Model _selectedModel;
        public Model SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                _selectedModel = value;
               // LabelValue = new Goods();

               

                if (_selectedCity != null)
                {
                    if (_selectedModel != null)
                    {
                        //LabelValue2 = "678";
                        LabelValue.Count = _selectedModel.count;
                        LabelValue.Price = _selectedModel.price;
                    }
                    IsSecond3ListVisible = true;
                }
                else
                {
                    IsSecond2ListVisible = false;
                    IsSecond3ListVisible = false;
                }
                if (_selectedModel == null) 
                    IsSecond3ListVisible = false;
                else
                    IsSecond3ListVisible = true;
                OnPropertyChanged(nameof(SelectedModel));
                OnPropertyChanged(nameof(LabelValue));
            }
        }
        // Метод загрузки городов для выбранной страны
        private async void LoadCitiesForCountry(int countryId)
        {
            Subbrand.Clear();
            Model.Clear();
            String token = Preferences.Get("token", "");
            //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwiaWF0IjoxNzEwMTY1NTA1LCJleHAiOjE3MTAyNTE5MDV9.boHtTEEUYzk7fZI4o6l5x37bIVFW3hfPYdjPGzbKZ3g";
            await SendReguest2(token, countryId.ToString());

        }
        private async void LoadForCountry(int countryId)
        {
            Model.Clear();
            String token = Preferences.Get("token", "");
            //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwiaWF0IjoxNzEwMTY1NTA1LCJleHAiOjE3MTAyNTE5MDV9.boHtTEEUYzk7fZI4o6l5x37bIVFW3hfPYdjPGzbKZ3g";
            await SendReguest3(token, countryId.ToString());
        }
        private bool _isSecondListVisible;
        public bool IsSecondListVisible
        {
            get { return _isSecondListVisible; }
            set
            {
                _isSecondListVisible = value;
                OnPropertyChanged(nameof(IsSecondListVisible));
            }
        }
        private bool _isSecond2ListVisible;
        public bool IsSecond2ListVisible
        {
            get { return _isSecond2ListVisible; }
            set
            {
                _isSecond2ListVisible = value;
                OnPropertyChanged(nameof(IsSecond2ListVisible));
            }
        }
        private bool _isSecond3ListVisible;
        public bool IsSecond3ListVisible
        {
            get { return _isSecond3ListVisible; }
            set
            {
                _isSecond3ListVisible = value;
                OnPropertyChanged(nameof(IsSecond3ListVisible));
            }
        }
        // Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

