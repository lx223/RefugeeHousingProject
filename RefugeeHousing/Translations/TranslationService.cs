﻿using System;
using System.Globalization;
using System.Threading;
using System.Web;
using RefugeeHousing.Models;

namespace RefugeeHousing.Translations
{
    public class TranslationService
    {
        private const string TranslationCookieName = "refugee_language";

        public void SetLanguage(string languageCode)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(languageCode);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(languageCode);
        }

        public void SetLanguage(Language language)
        {
            SetLanguage(language.GetCode());
        }

        public void SetTranslationCookie(Language language)
        {
            var cookie = new HttpCookie(TranslationCookieName, ((int) language).ToString())
            {
                Expires = DateTime.Now.AddDays(30)
            };
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public void SetTranslationFromCookieIfExists()
        {
            var language = GetLanguageFromCookie();
            if (language != null)
            {
                SetLanguage((Language) language);
            }
        }

        public Language GetLanguageFromCookieOrDefault()
        {
            return GetLanguageFromCookie() ?? LanguageExtensions.GetDefault();
        }

        private Language? GetLanguageFromCookie()
        {
            var myCookie = HttpContext.Current.Request.Cookies.Get(TranslationCookieName);
            if (myCookie == null) return null;
            try
            {
                var cookieCode = Convert.ToInt32(myCookie.Value);
                return Enum.IsDefined(typeof (Language), cookieCode) ? (Language)cookieCode : (Language?) null;
            }
            //TODO SMH Log exceptions
            catch (FormatException) { return null; }
            catch (OverflowException) { return null; }
        }
    }
}