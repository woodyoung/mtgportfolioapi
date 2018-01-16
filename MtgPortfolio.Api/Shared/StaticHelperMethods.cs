using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.Api.Shared
{
    public static class StaticHelperMethods
    {
        public static decimal? TryParseNullableDecimal(string value)
        {
            Decimal decimalValue;
            Decimal? tryParseResult = null;
            if (Decimal.TryParse(value, out decimalValue)) tryParseResult = decimalValue;

            return tryParseResult;
        }

        public static DateTime? TryParseNullableDate(string date)
        {
            DateTime nonNullableDate;
            DateTime? dateResult = null;
            if (DateTime.TryParse(date, out nonNullableDate)) dateResult = nonNullableDate;

            return dateResult;
        }
    }
}
