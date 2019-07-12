using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gardenation.Domain
{
    public static class UtilityService
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> e) where T : class
        {
            return e == null && !e.Any();
        }
    }
}
