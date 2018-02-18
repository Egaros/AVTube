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
    public static class InstagramPictureExtractor
    {
        public static String Title(string Url)
        {
            string content = Web.getContentFromUrlWithProperty(Url);

            if (Log.getMode())
                Log.println("Content : " + content);

            int startIndex = content.IndexOf("og:image") + 19;

            content = content.Substring(startIndex, content.Length - startIndex);

            int startIndexFile = content.IndexOf(".ak.instagram.com/") + 18;
            int endIndex = content.IndexOf(".jpg");

            String title = content.Substring(startIndexFile, endIndex - startIndexFile);

            if (Log.getMode())
                Log.println("Title : " + title);

            return title;
        }

        public static String DownloadUrl(string Url)
        {
            string content = Web.getContentFromUrlWithProperty(Url);

            if (Log.getMode())
                Log.println("Content : " + content);

            if (Log.getMode())
                Log.println("Content : " + content);

            int startIndex = content.IndexOf("og:image") + 19;

            content = content.Substring(startIndex, content.Length - startIndex);

            int startIndexFile = content.IndexOf(".ak.instagram.com/") + 18;
            int endIndex = content.IndexOf(".jpg");

            String url = content.Substring(0, endIndex + 4);

            if (Log.getMode())
                Log.println("Url : " + url);

            return url;
        }
    }
}
