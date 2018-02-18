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
    public static class EroprofileSearch
    {
        static List<EroprofileSearchComponents> items;

        static String title;
        static String duration;
        static String url;
        static String thumbnail;

        public static List<EroprofileSearchComponents> Query(string querystring, int querypages)
        {
            items = new List<EroprofileSearchComponents>();

            // Do search
            for (int i = 1; i <= querypages; i++)
            {
                // Search address
                string content = Web.getContentFromUrl("http://www.eroprofile.com/m/videos/search?text=" + querystring.Replace(" ", "+") + "&pnum=" + i);

                // Search string
                string pattern = "<div class=\"video\">.*?</span> <a href=.*?</a></div></div>";
                MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

                for (int ctr = 0; ctr <= result.Count - 1; ctr++)
                {
                    if (Log.getMode())
                        Log.println("Match: " + result[ctr].Value);

                    title = Helper.ExtractValue(result[ctr].Value, "videoTtl", "</div><div class");
                    title = Helper.Concat(title, "\"");
                    title = Helper.ExtractValue(title, "\">", "\"");

                    if (Log.getMode())
                        Log.println("Title : " + title);

                    duration = Helper.ExtractValue(result[ctr].Value, "\"videoDur\">", "</div></a> <");

                    if (Log.getMode())
                        Log.println("Duration : " + duration);

                    url = Helper.Concat("http://www.eroprofile.com", Helper.ExtractValue(result[ctr].Value, "><div><a href=\"", "\" class=\"cbox\""));

                    if (Log.getMode())
                        Log.println("Url : " + url);

                    thumbnail = Helper.Concat("http://", Helper.ExtractValue(result[ctr].Value, "src=\"//", "\" class=\"vi").Replace("amp;", ""));

                    if (Log.getMode())
                        Log.println("Thumbnail : " + thumbnail);

                    if (Log.getMode())
                        Log.println("********************************");

                    EroprofileSearchComponents comp = new EroprofileSearchComponents(title, duration, url, thumbnail);

                    items.Add(comp);
                }
            }

            return items;
        }
    }
}
