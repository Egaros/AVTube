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
    public class ccMixterSearch
    {
        static List<ccMixterSearchComponents> items;

        static string title;
        static string author;
        static string url;

        public static List<ccMixterSearchComponents> Query(string querystring, int querypages)
        {
            items = new List<ccMixterSearchComponents>();

            // Do search
            for (int i = 1; i <= querypages; i++)
            {
                int ii = i * 15;

                // Search address
                string content = Web.getContentFromUrl("http://ccmixter.org/search?search_text=" + querystring.Replace(" ", "+") + "&search_type=any&search_in=uploads&offset=" + ii);

                // Search string
                string pattern = "<div class=\"search_results_link\">.*?</div>";
                MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

                for (int ctr = 0; ctr <= result.Count - 1; ctr++)
                {
                    if (Log.getMode())
                        Log.println("Match: " + result[ctr].Value);

                    title = Helper.ExtractValue(result[ctr].Value, "class=\"cc_file_link\">", "</a>");

                    if (Log.getMode())
                        Log.println("Title : " + title);

                    author = Helper.ExtractValue(
                        Helper.ExtractValue(result[ctr].Value, "<a class=\"cc_user_link\" href=\"", "/a"), ">", "<");

                    if (Log.getMode())
                        Log.println("Author : " + author);

                    url = Helper.ExtractValue(result[ctr].Value, "<a href=\"", "\" class=");

                    if (Log.getMode())
                        Log.println("Url : " + url);

                    if (Log.getMode())
                        Log.println("********************************");

                    items.Add(new ccMixterSearchComponents(title, author, url));
                }
            }

            return items;
        }
    }
}
