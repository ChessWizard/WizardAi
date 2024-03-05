using System.ComponentModel.DataAnnotations;

namespace WizardAi.Core.Enums
{
    public enum PromptType
    {
        [Display(Name = "Açıklama")]
        None = 0,// anything
        
        [Display(Name = "Makale")]
        Article = 1,
        
        [Display(Name = "Blog")]
        Blog = 2
    }
}
