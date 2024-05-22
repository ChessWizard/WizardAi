using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardAi.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrNotAny<T>(this IEnumerable<T> source)
         => source is null || !source.Any();
    }
}
