﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepad_Application.Services
{
    internal static class FormatService
    {
        public static string FormatText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var words = text.ToCharArray();

            if (words.Length <= 10)
                return text;

            return string.Join("", words.Take(10)) + "...";
        }
    }
}
