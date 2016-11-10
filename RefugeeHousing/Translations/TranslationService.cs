using System.Globalization;
using System.Threading;
using RefugeeHousing.Models;

namespace RefugeeHousing.Translations
{
    public class TranslationService
    {
        public void SetLanguage(string languageCode)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(languageCode);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(languageCode);
        }

        public void SetLanguage(Language language)
        {
            SetLanguage(language.GetCode());
        }
    }
}