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

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AVTube
{
    public static class YouTubeSearch
    {
        static List<YouTubeSearchComponents> items;

        static string title;
        static string author;
        static string description;
        static string duration;
        static string url;
        static string thumbnail;

        public static List<YouTubeSearchComponents> Query(string querystring, int querypages)
        {
            items = new List<YouTubeSearchComponents>();

            // Do search
            for (int i = 1; i <= querypages; i++)
            {
                // Search address
                string content = Web.getContentFromUrl("https://www.youtube.com/results?search_query=" + querystring + "&page=" + i);

                // Search string
                string pattern = "<div class=\"yt-lockup-content\">.*?title=\"(?<NAME>.*?)\".*?</div></div></div></li>";
                MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

                for (int ctr = 0; ctr <= result.Count - 1; ctr++)
                {
                    if (Log.getMode())
                        Log.println("Match: " + result[ctr].Value);

                    // Title
                    title = result[ctr].Groups[1].Value;

                    if (Log.getMode())
                        Log.println("Title: " + title);

                    // Author
                    author = Helper.ExtractValue(result[ctr].Value, "/user/", "class").Replace('"', ' ').TrimStart().TrimEnd();

                    if (Log.getMode())
                        Log.println("Author: " + author);

                    // Description
                    description = Helper.ExtractValue(result[ctr].Value, "dir=\"ltr\" class=\"yt-uix-redirect-link\">", "</div>");

                    if (Log.getMode())
                        Log.println("Description: " + description);

                    // Duration
                    duration = Helper.ExtractValue(Helper.ExtractValue(result[ctr].Value, "id=\"description-id-", "span"), ": ", "<").Replace(".", "");

                    if (Log.getMode())
                        Log.println("Duration: " + duration);

                    // Url
                    url = string.Concat("http://www.youtube.com/watch?v=", Helper.ExtractValue(result[ctr].Value, "watch?v=", "\""));

                    if (Log.getMode())
                        Log.println("Url: " + url);

                    // Thumbnail
                    thumbnail = "https://i.ytimg.com/vi/" + Helper.ExtractValue(result[ctr].Value, "watch?v=", "\"") + "/mqdefault.jpg";

                    if (Log.getMode())
                        Log.println("Thumbnail: " + thumbnail);

                    if (Log.getMode())
                        Log.println("********************************");

                    // Remove playlists
                    if (title != "__title__")
                    {
                        if (duration != "")
                        {
                            // Add item to list
                            items.Add(new YouTubeSearchComponents(title, author, description, duration, url, thumbnail));
                        }
                    }
                }
            }

            return items;
        }
    }
}
