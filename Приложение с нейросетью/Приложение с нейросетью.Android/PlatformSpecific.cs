using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Приложение_с_нейросетью.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformSpecific))]

namespace Приложение_с_нейросетью.Droid
{
    internal class PlatformSpecific:IPlatformSpecific
    {
        public string GetResourcePath(string resourceName)
        {
            string path = "";
            AssetManager assets = Android.App.Application.Context.Assets;
            using (StreamReader sr = new StreamReader(assets.Open(resourceName)))
            {
                path = sr.BaseStream.ToString();
            }
            return path;
        }
    
    }
}