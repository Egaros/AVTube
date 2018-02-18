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
using System.Text;

using Newtonsoft.Json.Linq;

namespace AVTube
{
    public static class DailymotionExtractor
    {
        public static String DownloadUrl(String url)
        {
            StringBuilder sb = new StringBuilder(url);
            sb.Insert(27, "json/");
            sb.Append("?fields=title,stream_h264_url,stream_h264_ld_url,stream_h264_hq_url,stream_h264_hd_url,stream_h264_hd1080_url");

            String JUrl = sb.ToString();

            String content = Web.getContentFromUrl(JUrl);

            if ((content == null) || (String.IsNullOrEmpty(content)))
            {
                return String.Empty;
            }

            JToken videourl = null;

            try
            {
                var obj = JObject.Parse(content.ToString());

                videourl = obj["stream_h264_hq_url"];

                if ((videourl == null) || (String.IsNullOrEmpty((string)videourl)) || (videourl.Equals(null)))
                {
                    videourl = obj["stream_h264_ld_url"];
                }
                if ((videourl == null) || (String.IsNullOrEmpty((string)videourl)) || (videourl.Equals(null)))
                {
                    videourl = obj["stream_h264_url"];
                }
                if ((videourl == null) || (String.IsNullOrEmpty((string)videourl)) || (videourl.Equals(null)))
                {
                    videourl = obj["stream_h264_hd1080_url"];
                }
                if ((videourl == null) || (String.IsNullOrEmpty((string)videourl)) || (videourl.Equals(null)))
                {
                    videourl = obj["stream_h264_hd_url"];
                }
                if ((videourl == null) || (String.IsNullOrEmpty((string)videourl)) || (videourl.Equals(null)))
                {
                    videourl = null;
                }
            }
            catch (Exception ex)
            {
                if (Log.getMode())
                    Log.println(ex.ToString());
            }

            if (videourl != null)
            {
                if (Log.getMode())
                    Log.println("Video url: " + (string)videourl);

                if (Log.getMode())
                    Log.println("********************************");

                return (string)videourl;
            }
            else
                return String.Empty;
        }
    }
}
