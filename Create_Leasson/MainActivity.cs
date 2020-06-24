using System;
using System.IO;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.IO;
using static Android.Views.View;
using ActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Environment = System.Environment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace Create_Leasson
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            View main = FindViewById(Resource.Id.includeMain);
            main.Visibility = ViewStates.Visible;
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                Intent intent = new Intent(this, typeof(SettingsActivity));
                StartActivity(intent);
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            FragmentManager manager = SupportFragmentManager;
            AddLessonDialog Adddialog = new AddLessonDialog();
            Adddialog.Show(manager, GetString(Resource.String.initial_steps));
            //Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            //LayoutInflater inflater = LayoutInflater.From(this);
            //var view = inflater.Inflate(Resource.Layout.content_add_leasson, null);
            //builder.SetView(view);
            //builder.SetTitle(GetString(Resource.String.initial_steps));
            //builder.SetPositiveButton(GetString(Resource.String.continue_text), new EventHandler<DialogClickEventArgs>((object sender, DialogClickEventArgs @event) => {

            //    RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.BibleOrQuran);
            //    if (!radioGroup.Selected)
            //    {

            //    }
            //    else
            //    {
            //        if (FindViewById<RadioButton>(Resource.Id.rdBible).Selected)
            //        {
            //            Intent intent = new Intent(this, typeof(AddBibleLessonActivity));
            //            StartActivity(intent);
            //        }
            //        else
            //        {

            //        }
            //    }
            //}));
            //builder.SetNegativeButton(GetString(Resource.String.cancel), new EventHandler<DialogClickEventArgs>((object sender, DialogClickEventArgs @event) => {
            //    View view = (View)FindViewById(Resource.Id.fab);
            //    Snackbar.Make(view, GetString(Resource.String.not_ready), Snackbar.LengthLong)
            //        .SetAction("Action", (View.IOnClickListener)null).Show();
            //}));
            //var dialog = builder.Create();
            //dialog.Show();
        }
        private bool PermissionsAcepteds() {
            if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == Android.Content.PM.Permission.Denied)
            {
                string requestPermission = GetString(Resource.String.ask_for_permissions);
                var view = (View)FindViewById(Resource.Id.fab);
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
                if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == Android.Content.PM.Permission.Denied)
                {
                    return false;
                }
            }
            return true;
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            switch (id)
            {
                case Resource.Id.nav_settings:
                    Intent intent = new Intent(this, typeof(SettingsActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.nav_bible:
                    bool accepted = PermissionsAcepteds();
                    if (accepted)
                    {
                        View main = FindViewById(Resource.Id.includeMain);
                        main.Visibility = ViewStates.Gone;

                        View loading = FindViewById(Resource.Id.includeLoading);
                        loading.Visibility = ViewStates.Visible;

                        ImageView imageView = FindViewById<ImageView>(Resource.Id.ImgViewLoading);
                        imageView.SetImageResource(Resource.Drawable.animation_loading);

                        Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                        toolbar.Title = GetString(Resource.String.bible);

                        AnimationDrawable animation = (AnimationDrawable)imageView.Drawable;
                        animation.Start();
                    }
                    else
                    {
                        
                    }
                    break;
                default:
                    View view = (View)FindViewById(Resource.Id.fab);
                    Snackbar.Make(view, "This action does not implemented!", Snackbar.LengthLong)
                        .SetAction("Action", (View.IOnClickListener)null).Show();
                    break;
            }
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

