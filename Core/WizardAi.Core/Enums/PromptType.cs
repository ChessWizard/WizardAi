using System.ComponentModel.DataAnnotations;

namespace WizardAi.Core.Enums
{
    public enum TextPromptType
    {
        [Display(Name = "Write")]
        None = 0,
        
        [Display(Name = "Article")]
        Article = 1,
        
        [Display(Name = "Blog")]
        Blog = 2,

        [Display(Name = "Summary")]
        Summary = 3
    }
}
