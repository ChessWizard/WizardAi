using OpenAI_API.Audio;
using OpenAI_API.Completions;
using OpenAI_API.Images;

namespace WizardAi.Core.External
{
    public interface IOpenAiService
    {
        Task<CompletionResult> CreateTextCompletionAsync(CompletionRequest request, int optionCount = 3);

        Task<List<CompletionResult>> CreateStreamingCompletionAsync(CompletionRequest request);
        
        Task<AudioResultVerbose> GetSpeechToTextDetailsAsync(byte[] audioArray);

        Task<ImageResult> GenerateImageFromTextAsync(ImageGenerationRequest request);
    }
}
