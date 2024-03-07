using Network.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Network.ViewModel
{
    public partial class GoodsViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Manufacturer> Manufacturer { get; set; }
        public ObservableCollection<Subbrand> Cities { get; set; }
        public GoodsViewModel()
        {
            Manufacturer = new ObservableCollection<Manufacturer>();
            //List<Manufacturer> Manufacturer1 = new List<Manufacturer>();
            Manufacturer.Add(new Manufacturer("Samsung"));
            Manufacturer.Add(new Manufacturer("LG"));
            Manufacturer.Add(new Manufacturer("Apple"));
            Manufacturer.Add(new Manufacturer("Apple"));

            //Manufacturer = Manufacturer1;

        }
        private Manufacturer _selectedCountry;
        public Manufacturer SelectedCountry
            {
                get { return _selectedCountry; }
                set
                {
                IsSecondListVisible = true;
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
                    OnPropertyChanged(nameof(SelectedCity));
                }
            }

            // Метод загрузки городов для выбранной страны
            private void LoadCitiesForCountry(int countryId)
            {
            Cities = new ObservableCollection<Subbrand>
            {
                //List<Manufacturer> Manufacturer1 = new List<Manufacturer>();
                new Subbrand("Samsung"),
                new Subbrand("LG"),
                new Subbrand("Apple"),
                new Subbrand("Apple")
            };
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
        // Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

