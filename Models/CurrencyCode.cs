using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CurrencyCode
    {
        static List<string> currencyCodes;

        static CurrencyCode()
        {
            currencyCodes = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(x => (new RegionInfo(x.LCID)).ISOCurrencySymbol)
                .Distinct()
                .OrderBy(x => x).ToList();
        }

        public static bool IsValid(string currencyCode)
        {
            return currencyCodes.Where(x => x.Contains(currencyCode)).Any();
        }
    }
}
