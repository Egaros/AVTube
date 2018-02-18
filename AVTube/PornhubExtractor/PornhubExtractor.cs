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
using System.Text.RegularExpressions;

namespace AVTube
{
    public static class PornhubExtractor
    {
        public static string DownloadUrl(string Url)
        {
            string content = Web.getContentFromUrlWithProperty(Url);

            if (Log.getMode())
                Log.println("Content: " + content);

            // Search string
            string pattern = "720\",\"videoUrl\":\".*?\"";
            MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

            String URL = String.Empty;

            for (int ctr = 0; ctr <= result.Count - 1; ctr++)
            {
                if (Log.getMode())
                    Log.println("Match: " + content);

                URL = result[ctr].Value.Replace("\\", "").Replace("720\",\"videoUrl\":\"", "")
                    .Replace("\"", "");

                if (Log.getMode())
                    Log.println("Url : " + URL);

                if (Log.getMode())
                    Log.println("********************************");
            }

            return URL;
        }
    }
}
