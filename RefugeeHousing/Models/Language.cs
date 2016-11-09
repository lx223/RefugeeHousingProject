using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefugeeHousing.Models
{
    public enum Language
    {
        English,
        Greek 
    }

    public static class LanguageExtensions
    {
        public static string GetName(this Language language)
        {
            switch (language)
            {
                case Language.English:
                    return "English";
                case Language.Greek:
                    return "ελληνικά";
                default:
                    throw new InvalidOperationException();
            }
        }

        public static string GetCode(this Language language)
        {
            switch (language)
            {
                case Language.English:
                    return "en";
                case Language.Greek:
                    return "el";
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}