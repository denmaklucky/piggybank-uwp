using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piggy_bank_uwp.Utilities
{
    public static class SystemUtility
    {
        public static string GetLocale()
        {
            string locale = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            if (!Constants.Locales.Any(l => l == locale))
                locale = Constants.DefaultLocale;

            return locale;
        }
    }
}
