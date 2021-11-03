﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QLDPKS.Libs
{
    public class Images
    {
        public static string GetFirstImage(string input)
        {
            string matchString = Regex.Match(input, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
            if (!String.IsNullOrEmpty(matchString))
                return matchString;
            else
                return "/Images/noimage.png";
        }
    }
}