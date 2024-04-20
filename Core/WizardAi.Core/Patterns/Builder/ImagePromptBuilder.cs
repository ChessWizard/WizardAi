using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Enums;

namespace WizardAi.Core.Patterns.Builder
{
    public class ImagePromptBuilder : IPromptBuilder<ImagePromptBuilder>
    {
        private string _description;
        private ImageType _imageType;
        
        public ImagePromptBuilder SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public ImagePromptBuilder SetImageType(ImageType imageType)
        {
            _imageType = imageType;
            return this;
        }

        public string Build()
        {
            return $"Suitable for this content, Content: {_description} Produce {_imageType.ToString()
                                                                                           .ToLower()} image";
        }
    }
}
