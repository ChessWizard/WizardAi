using OpenAI_API.Audio;
using OpenAI_API.Chat;
using OpenAI_API.Completions;
using OpenAI_API.Images;
using WizardAi.Core.Enums;

namespace WizardAi.Core.External
{
    public interface IOpenAiService
    {
        Task<CompletionResult> CreateTextCompletionAsync(CompletionRequest request, int optionCount = 1);

        Task CreateStreamingCompletionAsync(CompletionRequest request, string connectionId);
        
        Task<AudioResultVerbose> GetSpeechToTextDetailsAsync(byte[] audioArray);

        Task<ImageResult> GenerateImageFromTextAsync(ImageGenerationRequest request);

        Task<ChatResult> GenerateSingleImageUrlToTextAsync(string prompt, string? imageUrl, CreativityType creativityType, int requestedOption);
        
        Task<ChatResult> GenerateSingleBase64ImageToTextAsync(string prompt, byte[] base64Image, CreativityType creativityType, int requestedOption);

        Task<ChatResult> GenerateMultipleImageToTextAsync(string prompt, List<string> urlImages, List<byte[]> base64Images, CreativityType creativityType, int requestedOption);
    }
}
