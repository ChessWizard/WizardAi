using OpenAI_API.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Enums;

namespace WizardAi.Core.Helpers
{
    public static class ImageGenerationHelper
    {
        /// <summary>
        /// Kullanıcının seçtiği formata göre resim boyutu ayarlama işlemi. DALLE3 modelinde yalnızca 1024x1024, 1792x1024(portrait) ve _1024x1792(landscape) boyutları bulunur.
        /// </summary>
        /// <param name="imageSizeType"></param>
        /// <returns></returns>
        public static ImageSize GetImageSizeByType(ImageSizeType imageSizeType)
            => imageSizeType switch
            {
                ImageSizeType.Small => ImageSize._256,
                ImageSizeType.Medium => ImageSize._512,
                ImageSizeType.Normal => ImageSize._1024,
                ImageSizeType.Landscape => ImageSize._1792x1024,
                ImageSizeType.Portrait => ImageSize._1024x1792
            }; 
    }
}
