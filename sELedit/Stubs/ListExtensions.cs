using System.Collections.Generic;

namespace System.Linq
{
    public static class ListExtensions
    {
        public static int Length<T>(this List<T> list)
        {
            return list?.Count ?? 0;
        }
    }
}