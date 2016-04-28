using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public static class Extensions
    {
        public static bool ValidateAndAdd<T>(this ICollection<T> L, T item) where T : IValidatableObject
        {
            if (item.IsValid())
            {
                L.Add(item);
                return true;
            }
            return false;
        }

    }
}
