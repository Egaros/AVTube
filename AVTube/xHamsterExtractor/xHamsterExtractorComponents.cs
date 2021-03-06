﻿// This file is part of AVTube.
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
    public class xHamsterExtractorComponents
    {
        private String Title;
        private String Format;
        private String Resolution;
        private String Url;

        public xHamsterExtractorComponents(String Title, String Format, String Resolution, String Url)
        {
            this.setTitle(Title);
            this.setFormat(Format);
            this.setResolution(Resolution);
            this.setUrl(Url);
        }

        public String getTitle()
        {
            return Title;
        }

        public void setTitle(String title)
        {
            Title = title;
        }

        public String getFormat()
        {
            return Format;
        }

        public void setFormat(String format)
        {
            Format = format;
        }

        public String getResolution()
        {
            return Resolution;
        }

        public void setResolution(String resolution)
        {
            Resolution = resolution;
        }

        public String getUrl()
        {
            return Url;
        }

        public void setUrl(String url)
        {
            Url = url;
        }
    }
}
