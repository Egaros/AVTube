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

namespace AVTube
{
    public static class FacebookVideoExtractor
    {
        public static String Title(string Url)
        {
            string content = Web.getContentFromUrlWithProperty(Url);

            if (Log.getMode())
                Log.println("Content : " + content);

            String title = Helper.ExtractValue(content, "\"pageTitle\">", "</title><link");

            if (Log.getMode())
                Log.println("Title : " + title);

            if (Log.getMode())
                Log.println("********************************");

            return title;
        }

        public static String DownloadUrl(string Url)
        {
            string content = Web.getContentFromUrlWithProperty(Url);

            if (Log.getMode())
                Log.println("Content : " + content);

            String URL = Helper.ExtractValue(content.Replace("\r\n", ""), "sd_src_no_ratelimit:\"", "\",hd_src_no_ratelimit:");

            if (Log.getMode())
                Log.println("Url : " + URL);

            if (Log.getMode())
                Log.println("********************************");

            return URL;
        }
    }
}
