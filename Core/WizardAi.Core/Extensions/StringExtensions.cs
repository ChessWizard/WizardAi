using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardAi.Core.Extensions
{
    public static class StringExtensions
    {
        public static int GetWordCount(this string source)
         => source.Split(new char[] { ' ', '.', '?', '!', ';', ':', ',' }
                        , StringSplitOptions.RemoveEmptyEntries)
                        .Length;
    }
}
