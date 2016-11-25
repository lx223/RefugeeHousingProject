using System.ComponentModel;

namespace RefugeeHousing.Translations
{
    public enum Language
    {
        English = 0,
        Greek = 1 
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
                    // ReSharper disable once LocalizableElement
                    throw new InvalidEnumArgumentException("Language not recognised. Language provided was: " + language);
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
                    // ReSharper disable once LocalizableElement
                    throw new InvalidEnumArgumentException("Language not recognised. Language provided was: " + language);
            }
        }

        public static Language GetDefault()
        {
            return Language.English;
        }
    }
}