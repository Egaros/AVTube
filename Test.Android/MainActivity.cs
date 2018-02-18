using Android.App;
using Android.Widget;
using Android.OS;

using AVTube;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Test.Android
{
    [Activity(Label = "Test.Android", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate {
                string url = "https://www.youtube.com/watch?v=ALUhXkqXuHs";

                IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(url, false);

                VideoInfo video = videoInfos
                    .First(info => info.VideoType == VideoType.Mp4);

                if (video.RequiresDecryption)
                {
                    DownloadUrlResolver.DecryptDownloadUrl(video);
                }

                string DestinationFile = Path.Combine(Environment.ExternalStorageDirectory.AbsolutePath, "test.mp4");

                Download download = new Download();
                download.Run(video.DownloadUrl, DestinationFile);
            };
        }
    }
}

