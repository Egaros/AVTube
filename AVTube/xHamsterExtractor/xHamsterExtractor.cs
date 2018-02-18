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
    public static class xHamsterExtractor
    {
        static List<xHamsterExtractorComponents> items;

        static String title;
        static String format;
        static String resolution;
        static String url;

        public static List<xHamsterExtractorComponents> Query(String Url)
        {
            items = new List<xHamsterExtractorComponents>();

            // Search address
            string content = Web.getContentFromUrlWithProperty(Url);

            if (Log.getMode())
                Log.println("Match: " + content);

            // Search string
            string pattern = "players:.*?}\"}},";
            MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

            for (int ctr = 0; ctr <= result.Count - 1; ctr++)
            {
                if (!result[ctr].Value.Contains("</script>"))
                {
                    if (Log.getMode())
                        Log.println("Content: " + result[ctr].Value);

                    title = Helper.ExtractValue(result[ctr].Value, "\"title\":\"", "\",\"");

                    /* FLV */

                    if (Log.getMode())
                        Log.println("Title: " + title);

                    format = "flv";

                    if (Log.getMode())
                        Log.println("Format: " + format);

                    resolution = "";

                    if (Log.getMode())
                        Log.println("Resolution: " + resolution);

                    url = Helper.ExtractValue(result[ctr].Value, "\"file\":\"", "\",\"").Replace("\\", "");

                    if (Log.getMode())
                        Log.println("Url: " + url);

                    xHamsterExtractorComponents comp = new xHamsterExtractorComponents(title, format, resolution, url);

                    items.Add(comp);

                    /* **************************************************************************** */

                    /* MP4 */
                    
                    // Search string
                    string pattern_ = "sources:.*?},";
                    MatchCollection result_ = Regex.Matches(content, pattern_, RegexOptions.Singleline);

                    for (int ctr_ = 0; ctr_ <= result_.Count - 1; ctr_++)
                    {
                        if (Log.getMode())
                            Log.println("Match: " + result_[ctr_].Value);

                        /* 240p */

                        if (Log.getMode())
                            Log.println("Title: " + title);

                        format = "mp4";

                        if (Log.getMode())
                            Log.println("Format: " + format);

                        resolution = "240p";

                        if (Log.getMode())
                            Log.println("Resolution: " + resolution);

                        url = Helper.ExtractValue(result_[ctr_].Value, "\"240p\":\"", "\",").Replace("\\", "");

                        if (Log.getMode())
                            Log.println("Url: " + url);

                        comp = new xHamsterExtractorComponents(title, format, resolution, url);

                        items.Add(comp);

                        /* 480p */

                        if (Log.getMode())
                            Log.println("Title: " + title);

                        format = "mp4";

                        if (Log.getMode())
                            Log.println("Format: " + format);

                        resolution = "480p";

                        if (Log.getMode())
                            Log.println("Resolution: " + resolution);

                        url = Helper.ExtractValue(result_[ctr_].Value, "\"480p\":\"", "\"").Replace("\\", "");

                        if (Log.getMode())
                            Log.println("Url: " + url);

                        comp = new xHamsterExtractorComponents(title, format, resolution, url);

                        items.Add(comp);

                        /* 720p */

                        if (Log.getMode())
                            Log.println("Title: " + title);

                        format = "mp4";

                        if (Log.getMode())
                            Log.println("Format: " + format);

                        resolution = "720p";

                        if (Log.getMode())
                            Log.println("Resolution: " + resolution);

                        url = Helper.ExtractValue(result_[ctr_].Value, "\"720p\":\"", "\"}").Replace("\\", "");

                        if (Log.getMode())
                            Log.println("Url: " + url);

                        comp = new xHamsterExtractorComponents(title, format, resolution, url);

                        items.Add(comp);
                    }
                }
            }

            return items;
        }
    }
}
