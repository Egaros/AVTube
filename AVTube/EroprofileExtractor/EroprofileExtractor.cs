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
    public static class EroprofileExtractor
    {
        public static String DownloadUrl(string Url)
        {
            string content = Web.getContentFromUrl(Url);

            if (Log.getMode())
                Log.println("Content : " + content);

            String URL = Helper.ExtractValue(content, "<source src=\"", "\"").Replace("amp;", "");

            if (Log.getMode())
                Log.println("Url : " + URL);

            if (Log.getMode())
                Log.println("********************************");

            return URL;
        }
    }
}
