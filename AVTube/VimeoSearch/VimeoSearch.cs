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
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AVTube
{
    public static class VimeoSearch
    {
        static List<VimeoSearchComponents> items;

        static String title;
        static String author;
        static String duration;
        static String url;
        static String thumbnail;

        public static List<VimeoSearchComponents> Query(string querystring, int querypages)
        {
            items = new List<VimeoSearchComponents>();

            // Do search
            for (int i = 1; i <= querypages; i++)
            {
                // Search address
                string content = Web.getContentFromUrlWithProperty("https://www.vimeo.com/search/page:" + i + "?q=" + querystring.Replace(" ", "+"));

                if (Log.getMode())
                    Log.println("Content: " + content);

                // Search string
                string pattern = "\"uri\":.*?clip\",\"clip";
                MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

                for (int ctr = 0; ctr <= result.Count - 1; ctr++)
                {
                    if (Log.getMode())
                        Log.println("Match: " + result[ctr].Value);

                    title = Helper.ExtractValue(result[ctr].Value, "\"name\":\"", "\",\"link\"");

                    if (Log.getMode())
                        Log.println("Title : " + title);

                    author = Helper.ExtractValue(result[ctr].Value, "\"user\":{\"name\":\"", "\",\"link\":");

                    if (Log.getMode())
                        Log.println("Author : " + author);

                    duration = Helper.formatDuration(Helper.ExtractValue(result[ctr].Value, "duration\":", ",\"c"));

                    if (Log.getMode())
                        Log.println("Duration : " + duration);

                    url = Helper.Concat("https://vimeo.com", Helper.ExtractValue(result[ctr].Value, "videos", "\",\"").Replace("\\", ""));

                    if (Log.getMode())
                        Log.println("Url : " + url);

                    thumbnail = Helper.ExtractValue(Helper.ExtractValue(result[ctr].Value, "pictures\":{\"sizes\":", "r=pad"), "\"link\":\"", "?").Replace("\\", "");

                    if (Log.getMode())
                        Log.println("Thumbnail : " + thumbnail);

                    if (Log.getMode())
                        Log.println("********************************");

                    VimeoSearchComponents comp = new VimeoSearchComponents(title, author, duration, url, thumbnail);

                    items.Add(comp);
                }
            }

            return items;
        }
    }
}
