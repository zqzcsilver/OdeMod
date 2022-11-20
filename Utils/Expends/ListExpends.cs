using System.Collections.Generic;

namespace OdeMod.Utils.Expends
{
    internal static class ListExpends
    {
        public static bool Contains<T>(this List<T> me, List<T> list)
        {
            if (list == null || list.Count == 0) return true;
            foreach (var t in list)
            {
                if (!me.Contains(t)) return false;
            }
            return true;
        }
        public static bool Intersect<T>(this List<T> me, List<T> list)
        {
            if (list == null || list.Count == 0) return false;
            foreach (var t in list)
            {
                if (me.Contains(t)) return true;
            }
            return false;
        }
    }
}
