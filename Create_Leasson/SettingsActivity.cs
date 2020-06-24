using System;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Widget;
namespace Create_Leasson
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_settings);

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            ListView settings = FindViewById<ListView>(Resource.Id.ListSettings);
            if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == Android.Content.PM.Permission.Denied)
            {
                string requestPermission = GetString(Resource.String.ask_for_permissions);
                var view = (View)settings;
                var snackebar = Snackbar.Make(view, requestPermission, Snackbar.LengthLong);
                snackebar.SetAction(GetString(Resource.String.lets_go), (obj) =>
                {
                    RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage }, 0);
                    if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == Android.Content.PM.Permission.Denied)
                    {
                        requestPermission = GetString(Resource.String.permisson_denied);
                        Snackbar.Make(view, requestPermission, Snackbar.LengthLong).Show();
                    }
                }).Show();
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    return true;
                default:
                    if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == Android.Content.PM.Permission.Denied)
                    {
                        string requestPermission = GetString(Resource.String.ask_for_permissions);
                        var view = (View)FindViewById<ListView>(Resource.Id.ListSettings);
                        var snackebar = Snackbar.Make(view, requestPermission, Snackbar.LengthLong);
                        snackebar.SetAction(GetString(Resource.String.lets_go), (obj) =>
                        {
                            RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage }, 0);
                            if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == Android.Content.PM.Permission.Denied)
                            {
                                requestPermission = GetString(Resource.String.permisson_denied);
                                Snackbar.Make(view, requestPermission, Snackbar.LengthLong).Show();
                            }
                        }).Show();
                    }
                    return false;

            }
        }

    }
}