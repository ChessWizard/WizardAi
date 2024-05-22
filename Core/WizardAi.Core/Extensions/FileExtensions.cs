using Microsoft.AspNetCore.Http;

namespace WizardAi.Core.Extensions
{
    public static class FileExtensions
    {
        /// <summary>
        /// Client'tan gelen IFormFileCollection tipindeki dosyaları base64 string'e çevirir
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static async Task<Dictionary<string, string>> ToBase64StringsAsync(this IFormFileCollection files)
        {
            var base64Files = new Dictionary<string, string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    var base64String = Convert.ToBase64String(fileBytes);
                    base64Files.Add(file.FileName, base64String);
                }
            }

            return base64Files;
        }

        /// <summary>
        /// Client'tan gelen IFormFileCollection tipindeki dosyaları byte array'e çevirir
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static async Task<Dictionary<string, byte[]>> ToByteArraysAsync(this IFormFileCollection files)
        {
            var byteArrayFiles = new Dictionary<string, byte[]>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        byteArrayFiles.Add(file.FileName, fileBytes);
                    }
                }
            }

            return byteArrayFiles;
        }
    }
}
