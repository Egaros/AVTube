// This file is part of AVTube.
//
// Copyright (c) 2018 Torsten Klinger.
// E-Mail: torsten.klinger@googlemail.com
//
// AVTube is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// AVTube is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with AVTube. If not, see<http://www.gnu.org/licenses/>.

using System;

using FileDownloader;

namespace AVTube
{
    public class Download
    {
        IFileDownloader fileDownloader;

        public void Run(String Url, String DestinationFile)
        {
            fileDownloader = new FileDownloader.FileDownloader();

            fileDownloader.DownloadFileCompleted += DownloadFileCompleted;
            fileDownloader.DownloadProgressChanged += OnDownloadProgressChanged;

            fileDownloader.DownloadFileAsync(new Uri(Url), DestinationFile);
        }

        public void Cancel()
        {
            if (fileDownloader != null)
                fileDownloader.CancelDownloadAsync();
        }
        
        void DownloadFileCompleted(object sender, DownloadFileCompletedArgs eventArgs)
        {
            if (eventArgs.State == CompletedState.Succeeded)
            {
                Console.WriteLine("Download completed");
            }
            else if (eventArgs.State == CompletedState.Failed)
            {
                Console.WriteLine("Download failed");
            }
        }

        void OnDownloadProgressChanged(object sender, DownloadFileProgressChangedArgs args)
        {
            Console.WriteLine(args.BytesReceived + " of " + args.TotalBytesToReceive + " bytes " +
                    (args.BytesReceived * 100) / args.TotalBytesToReceive + " %");
        }
    }
}
