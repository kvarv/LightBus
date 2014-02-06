using System;
using System.Collections.Generic;
using System.Linq;

namespace LightBus
{
    internal static class HandlersExtensions
    {
        public static void CheckIfThereIsMoreThanOneFor(this IEnumerable<object> handlers, Type messageType)
        {
            if (handlers.Count() > 1)
            {
                throw new NotSupportedException(string.Format("There are more than one handler registered for {0}. This message should only have one handler.", messageType.FullName));
            }
        }

        public static void CheckIfThereAreAnyFor(this IEnumerable<object> handlers, Type messageType)
        {
            if (!handlers.Any())
            {
                throw new NotSupportedException(string.Format("There is no handler registered for the {0}.", messageType.FullName));
            }
        }
    }
}