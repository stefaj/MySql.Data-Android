using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MySql.Data.MySqlClient;

namespace masekindxamarin
{
    [Activity(Label = "masekindxamarin", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            try
            {
                MySqlConnection connection = new MySqlConnection(@"Database=ingenkia_reii422;Server=197.242.144.172;Uid=ingenkia_rei422;Pwd=123456");
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("Select * from reii422_estates_list");
                //command.Parameters.AddWithValue("@columnName", columnName);
                //command.Parameters.AddWithValue("@tableName", tableName);

                MySqlDataReader reader = command.ExecuteReader();

                var textView = FindViewById<TextView>(Resource.Id.textView1);
                while (reader.Read())
                {
                    textView.Text += reader.GetString(1) + "\n";

                }
                textView.SetHeight(40);
                textView.RefreshDrawableState();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);         
            }

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
    }
}

