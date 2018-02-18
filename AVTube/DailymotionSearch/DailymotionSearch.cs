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

using Newtonsoft.Json.Linq;

namespace AVTube
{
    public static class DailymotionSearch
    {
        static List<DailymotionSearchComponents> items;

        static String title;
        static String author;
        static String url;

        public static List<DailymotionSearchComponents> Query(string querystring, int querypages)
        {
            items = new List<DailymotionSearchComponents>();

            string content = Web.getContentFromUrl("https://api.dailymotion.com/videos?search=" + querystring.Replace(" ", "+") + "&limit=" + querypages * 15);

            try
            {
                if (Log.getMode())
                    Log.println("Source : " + content);

                var obj = JObject.Parse(content.ToString());

                foreach (JObject element in obj["list"])
                {
                    title = (string)element["title"];

                    if (Log.getMode())
                        Log.println("Title : " + title);

                    author = (string)element["owner"];

                    if (Log.getMode())
                        Log.println("Author : " + author);

                    if (Log.getMode())
                        Log.println("********************************");

                    url = "http://www.dailymotion.com/video/" + (string)element["id"];

                    if (Log.getMode())
                        Log.println("Url : " + url);

                    if (Log.getMode())
                        Log.println("********************************");

                    DailymotionSearchComponents comp = new DailymotionSearchComponents(title, author, url);

                    items.Add(comp);
                }

                return items;
            }
            catch (Exception ex)
            {
                if (Log.getMode())
                    Log.println(ex.ToString());

                return items;
            }
        }
    }
}
