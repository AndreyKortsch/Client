using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace Приложение_с_нейросетью.Droid
{
    [Activity(Label = "Приложение_с_нейросетью", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            //CheckAppPermissions("/storage/emulated/0/Pictures/saved_model.pb");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
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
    //        }
            //CheckAppPermissions("/storage/emulated/0/Pictures/saved_model.pb");
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
    }
}