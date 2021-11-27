using System.Collections.Specialized;

namespace TaxMicroserviceTakeHomeAssesment.Extensions
{
    public static class QueryStringExtenstions
    {
        public static void AddIfNotNull(this NameValueCollection queryString, string key, string value)
        {
            if (value != null)
            {
                queryString.Add(key, value);
            }
        }
    }
}
