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
using System.Text;

namespace AVTube
{
    public static class Helper
    {
        public static String ExtractValue(String Source, String Start, String End)
        {
            int start, end;

            try
            {
                if (Source.Contains(Start) && Source.Contains(End))
                {
                    start = Source.IndexOf(Start, 0) + Start.Length;
                    end = Source.IndexOf(End, start);

                    return Source.Substring(start, end - start);
                }
                else
                    return printZero();
            }
            catch (Exception ex)
            {
                if (Log.getMode())
                    Log.println(ex.ToString());

                return printZero();
            }
        }

        public static String Concat(String a, String b)
        {
            return new StringBuilder().Append(a).Append(b).ToString();
        }

        public static String Concat(String a, String b, String c)
        {
            return new StringBuilder().Append(a).Append(b).Append(c).ToString();
        }

        public static String printZero()
        {
            return " ";
        }

        public static String formatDuration(String Duration)
        {
            int timeInSeconds = Int32.Parse(Duration);

            int hours = timeInSeconds / 3600;
            int secondsLeft = timeInSeconds - hours * 3600;
            int minutes = secondsLeft / 60;
            int seconds = secondsLeft - minutes * 60;

            String formattedTime = "";

            if (hours < 10)
                formattedTime += "0";
            formattedTime += hours + ":";

            if (formattedTime.Equals("00:"))
                formattedTime = "";

            if (minutes < 10)
                formattedTime += "0";
            formattedTime += minutes + ":";

            if (seconds < 10)
                formattedTime += "0";
            formattedTime += seconds;

            return formattedTime;
        }
    }
}
