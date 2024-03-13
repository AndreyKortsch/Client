using Network.Models;
using Network.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Network
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddGoods : ContentPage
    {
      public AddGoods()
       {
           InitializeComponent();
           this.BindingContext = new GoodsViewModel();


        }
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //newItemEntry2.
            //var item = e.SelectedItem;
            // Your code here
            //pr.isvisible
        }
        protected override void OnAppearing()
        {
            //base.OnAppearing();
            ((GoodsViewModel)BindingContext).LoadDataCommand.Execute(null);
            base.OnAppearing();

        }
    }
  
}