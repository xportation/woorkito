using Android.App;
using Android.OS;
using woorkito;

namespace Woorkito.Android
{
    [Activity(Label = "Woorkito", Theme = "@android:style/Theme.Material.Light", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ActionBar.SetIcon(null);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

