using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uExtensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns true if the list does not contain the passed parameter(s)
        /// From: http://stackoverflow.com/a/833477/1158845
        /// </summary>
        /// <param name="source">the object to search in</param>
        /// <param name="list">the list of items to search for</param>
        /// <returns>true if the object is not in the list passed</returns>
        public static bool NotContains<T>(this T source, params T[] list)
        {
            if (source == null) throw new ArgumentNullException("source");
            return !list.Contains(source);
        }
    }
}
