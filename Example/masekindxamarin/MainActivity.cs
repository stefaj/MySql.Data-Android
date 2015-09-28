using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MySql.Data.MySqlClient;
using System.Net;
using Newtonsoft.Json.Linq;

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

            json_example();

            // mysql_example

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }

        void json_example()
        {
            var textView = FindViewById<TextView>(Resource.Id.textView1);

            var w = new WebClient();

            string json = "";
            try
            {
                json = w.DownloadString(@"http://192.168.1.100/xampp/Kuraudo/control/test.php");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            var dec_json = JArray.Parse(json);
            foreach (var row in dec_json)
            {
                try
                {
                    int id = row[0].ToObject<int>();

                    string desc = row[1].ToObject<string>();
                    int space = row[2].ToObject<int>();

                    textView.Text += string.Format("{0}, {1}, {2}\n", id, desc, space);
                }
                catch
                {
                    continue;
                }
            }
        }

        void mysql_example()
        {
            var textView = FindViewById<TextView>(Resource.Id.textView1);
            try
            {
                MySqlConnection connection = new MySqlConnection(@"Database=ingenkia_reii422;Server=197.242.144.172;Uid=ingenkia_rei422;Pwd=h-wDoSBR2{*9;");
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("Select * from reii422_estates_list");
                //command.Parameters.AddWithValue("@columnName", columnName);
                //command.Parameters.AddWithValue("@tableName", tableName);

                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    textView.Text += reader.GetString(1) + "\n";

                }
                textView.SetHeight(40);
                textView.RefreshDrawableState();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

    

