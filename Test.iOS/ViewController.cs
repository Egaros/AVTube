using System;

using UIKit;

using AVTube;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Test.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            string url = "https://www.youtube.com/watch?v=ALUhXkqXuHs";

            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(url, false);

            VideoInfo video = videoInfos
                .First(info => info.VideoType == VideoType.Mp4);

            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }

            string DestinationFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test.mp4");

            Download download = new Download();
            download.Run(video.DownloadUrl, DestinationFile);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}