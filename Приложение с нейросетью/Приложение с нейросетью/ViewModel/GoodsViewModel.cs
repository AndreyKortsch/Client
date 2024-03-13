using LearningXamarin.Views;
using Network.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

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
                HttpResponseMessage response = await client.PostAsync("https://fine-spies-feel.loca.lt/api/test/class", content);
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

                    //await DisplayAlert("Ошибка запроса", "Неверный логин или пароль", "Принять");

                }
            }
            catch (Exception ex)
            {
                Manufacturer.Add(new Manufacturer(ex.Message));

                //await DisplayAlert("Ошибка подключения", ex.Message, "Принять");
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
                HttpResponseMessage response = await client.PostAsync("https://fine-spies-feel.loca.lt/api/test/manufactor", content);
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

                    //await DisplayAlert("Ошибка запроса", "Неверный логин или пароль", "Принять");

                }
            }
            catch (Exception ex)
            {
                Manufacturer.Add(new Manufacturer(ex.Message));

                //await DisplayAlert("Ошибка подключения", ex.Message, "Принять");
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
                HttpResponseMessage response = await client.PostAsync("https://fine-spies-feel.loca.lt/api/test/subbrand", content);
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

                    //await DisplayAlert("Ошибка запроса", "Неверный логин или пароль", "Принять");

                }
            }
            catch (Exception ex)
            {
                Manufacturer.Add(new Manufacturer(ex.Message));

                //await DisplayAlert("Ошибка подключения", ex.Message, "Принять");
            }
            finally
            {
                client.Dispose();
            }
        }
        private Goods _labelValue=new Goods();
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
        public GoodsViewModel()
        {
            //Manufacturer = new ObservableCollection<Manufacturer>();
            String token = Preferences.Get("token", "");
            String classname = Preferences.Get("classname", "");
            classname = "ABIS_BOOK";
            LabelValue.Class = classname;
           //LabelValue.Count = 67;

            token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwiaWF0IjoxNzEwMTY1NTA1LCJleHAiOjE3MTAyNTE5MDV9.boHtTEEUYzk7fZI4o6l5x37bIVFW3hfPYdjPGzbKZ3g";
            LoadDataCommand = new Command(async () => await SendReguest(token, classname));
            //await SendReguest(token, classname);
            //List<Manufacturer> Manufacturer1 = new List<Manufacturer>();
            //Manufacturer.Add(new Manufacturer("Samsung"));
            //Manufacturer.Add(new Manufacturer("LG"));
            //Manufacturer.Add(new Manufacturer("Apple"));
            //Manufacturer.Add(new Manufacturer("Apple"));
            //Subbrand = new ObservableCollection<Subbrand>();
            //Model= new ObservableCollection<Model>();
            //Manufacturer = Manufacturer1;

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
                        //_selectedCountry = null;
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
                    //IsSecond3ListVisible = true;
                    OnPropertyChanged(nameof(SelectedCity));
                if (_selectedCity != null) LoadForCountry(_selectedCity.id);
                else
                {
                    IsSecond3ListVisible = false;
                }
                // _selectedCity = null;



            }
        }
        private Model _selectedModel;
        public Model SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                _selectedModel = value;
                //LabelValue.Count = 67;
                //LabelValue.Price = _selectedModel.price;
                if (_selectedCity != null)
                {
                    //if (_selectedModel == null) IsSecond3ListVisible = false;
                    //else 
                    LabelValue.Count = 67;
                    LabelValue.Price = _selectedModel.price;
                    IsSecond3ListVisible = true;
                }
                else
                {
                    IsSecond2ListVisible = false;
                    IsSecond3ListVisible = false;
                }
                if (_selectedModel == null) IsSecond3ListVisible = false;
                else IsSecond3ListVisible = true;
                //_selectedModel = value;
                //IsSecond3ListVisible = true;
                OnPropertyChanged(nameof(SelectedModel));
            }
        }
        // Метод загрузки городов для выбранной страны
        private async void LoadCitiesForCountry(int countryId)
            {
            //Subbrand = new ObservableCollection<Subbrand>
            //{
            //List<Manufacturer> Manufacturer1 = new List<Manufacturer>();
            //    new Subbrand("Samsung"),
            //    new Subbrand("LG"),
            //   new Subbrand("Apple"),
            //    new Subbrand("Apple")
            //};
            Subbrand.Clear();
            Model.Clear();
            String token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwiaWF0IjoxNzEwMTY1NTA1LCJleHAiOjE3MTAyNTE5MDV9.boHtTEEUYzk7fZI4o6l5x37bIVFW3hfPYdjPGzbKZ3g";
            await SendReguest2(token, countryId.ToString());
            //Subbrand.Add(new Subbrand(countryId.ToString()));
            //Subbrand.Add(new Subbrand("LG"));
            //Subbrand.Add(new Subbrand("Apple"));
            //Products = new ObservableCollection<Product>();
            //Products.Add(new Product() { Name = "ProductA" });
            //Products.Add(new Product() { Name = "ProductB" });
            //Cities();
            // Реализация загрузки городов для выбранной страны
            // Например, вызов сервиса или обращение к базе данных
            // Затем обновите свойство Cities с полученными данными
        }
        private async void LoadForCountry(int countryId)
        {
            //Subbrand = new ObservableCollection<Subbrand>
            //{
            //List<Manufacturer> Manufacturer1 = new List<Manufacturer>();
            //    new Subbrand("Samsung"),
            //    new Subbrand("LG"),
            //   new Subbrand("Apple"),
            //    new Subbrand("Apple")
            //};
            Model.Clear();
            String token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwiaWF0IjoxNzEwMTY1NTA1LCJleHAiOjE3MTAyNTE5MDV9.boHtTEEUYzk7fZI4o6l5x37bIVFW3hfPYdjPGzbKZ3g";
            await SendReguest3(token, countryId.ToString());
            //Model.Add(new Model(countryId.ToString()));
            //Model.Add(new Model("Samsung"));
            //Model.Add(new Model("LG"));
            //Model.Add(new Model("Apple"));
            //Products = new ObservableCollection<Product>();
            //Products.Add(new Product() { Name = "ProductA" });
            //Products.Add(new Product() { Name = "ProductB" });
            //Cities();
            // Реализация загрузки городов для выбранной страны
            // Например, вызов сервиса или обращение к базе данных
            // Затем обновите свойство Cities с полученными данными
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

