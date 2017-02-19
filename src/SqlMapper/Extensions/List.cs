using System.Collections;
using SqlMapper.Common.Models;

namespace SqlMapper.Extensions
{
    internal static class List
    {
        internal static void AddFromList<T>(this IList holderList, T model, Property propData)
        {
            var originalValues = propData.PropertyInfo.GetValue(model, null) as IEnumerable;

            if(originalValues == null) return;

            foreach (var val in originalValues)
                holderList.Add(val);
        }
    }
}