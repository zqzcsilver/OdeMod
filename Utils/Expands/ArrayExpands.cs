using System;

namespace OdeMod.Utils.Expands
{
    internal static class ArrayExpands
    {
        public static bool EqualWithArray(this Array array1, Array array2)
        {
            if (array1 == array2)
                return true;
            if (array1.Length != array2.Length)
                return false;
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1.GetValue(i) != array2.GetValue(i))
                    return true;
            }
            return true;
        }
    }
}