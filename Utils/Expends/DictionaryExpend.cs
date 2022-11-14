using System.Collections.Generic;

namespace OdeMod.Utils.Expends
{
    internal static class DictionaryExpend
    {
        public static bool ContainsKeys<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            foreach (var k in keys)
            {
                if (!dictionary.ContainsKey(k)) return false;
            }
            return true;
        }
    }
}
