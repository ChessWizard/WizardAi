using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardAi.Core.Patterns.Builder
{
    public interface IPromptBuilder<TType>
    {
        TType SetDescription(string description);
        string Build();
    }
}
