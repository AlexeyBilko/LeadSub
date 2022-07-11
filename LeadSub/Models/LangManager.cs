using System.Globalization;

namespace LeadSub.Models
{
    public class LangManager
    {
        public static List<Languages> AvailableLanguages = new List<Languages> {
            new Languages {
                LanguageFullName = "English", LanguageCultureName = "en"
            },
            new Languages {
                LanguageFullName = "Ukrainian", LanguageCultureName = "uk"
            },
        };
        public static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.Where(a => a.LanguageCultureName.Equals(lang)).FirstOrDefault() != null ? true : false;
        }
        public static string GetDefaultLanguage()
        {
            return AvailableLanguages[1].LanguageCultureName;
        }
        //public void SetLanguage(string lang)
        //{
        //    try
        //    {
        //        if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();
        //        var cultureInfo = new CultureInfo(lang);
        //        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        //        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
        //        HttpCookie langCookie = new HttpCookie("culture", lang);
        //        langCookie.Expires = DateTime.Now.AddYears(1);
        //        HttpContext.Current.Response.Cookies.Add(langCookie);
        //    }
        //    catch (Exception) { }
        //}
    }
    public class Languages
    {
        public string LanguageFullName
        {
            get;
            set;
        }
        public string LanguageCultureName
        {
            get;
            set;
        }
    }
}
