using System.Collections.Generic;

namespace SqlMapper.Tests.Extensions
{
    public static class DictionaryExtensions
    {
        public static List<KeyValuePair<string, object>> ToKeyValuePairs(this Dictionary<string, object> source)
        {
            var list = new List<KeyValuePair<string, object>>();

            foreach (var keyValuePair in source)
                list.Add(keyValuePair);

            return list;
        }
    }
}