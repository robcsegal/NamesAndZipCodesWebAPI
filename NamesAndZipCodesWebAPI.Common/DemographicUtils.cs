using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamesAndZipCodesWebAPI.Common
{
    public static class DemographicUtils
    {
        /// <summary>
        /// Get 5 Digit Zip Code 
        /// </summary>
        /// <param name="ZipCode">Zip Code (xxxxx or xxxxx-xxxx)</param>
        /// <returns></returns>
        public static string Get5DigitZipCode(string ZipCode)
        {
            if (ZipCode.Trim().IndexOf('-') > 0)
            {
                return ZipCode.Trim().Substring(0, ZipCode.Trim().IndexOf('-'));
            }
            else
            {
                return ZipCode;
            }
        }
    }
}
