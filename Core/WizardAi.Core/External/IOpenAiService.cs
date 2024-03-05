using OpenAI_API.Audio;
using OpenAI_API.Completions;

namespace WizardAi.Core.External
{
    public interface IOpenAiService
    {
        public Task<CompletionResult> CreateTextCompletionsAsync(CompletionRequest request, int optionCount = 3);

        public Task<AudioResultVerbose> GetSpeechToTextDetailsAsync(byte[] audioArray);
    }
}
