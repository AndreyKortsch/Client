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
using Android.Content.PM;
using Android;
using Java.Util.Jar;


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

        //String path = "/storage/emulated/0/Pictures/saved_model.pb";
        //if ((int)Build.VERSION.SdkInt < 23)
        //{
        //    return;
        //}
        //else
        //{
        //   if (PackageManager.CheckPermission(Android.Manifest.Permission.ReadExternalStorage, path) != Permission.Granted
        //     && PackageManager.CheckPermission(Android.Manifest.Permission.WriteExternalStorage, path) != Permission.Granted)
        //  {
        //      var permissions = new string[] { Android.Manifest.Permission.ReadExternalStorage, Android.Manifest.Permission.WriteExternalStorage };

        //                    RequestPermissions(permissions, 1);
        //              }


    }
}