using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardAi.Core.Enums
{
    public enum LanguageType
    {
        [Display(Name = "Türkçe")]
        TR = 1,

        [Display(Name = "İngilizce")]
        US = 2
    }
}
