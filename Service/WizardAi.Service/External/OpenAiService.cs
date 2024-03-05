using Microsoft.Extensions.Options;
using OpenAI_API;
using OpenAI_API.Completions;
using OpenAI_API.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using WizardAi.Core.External;
using WizardAi.Service.Configurations;
using OpenAI_API.Audio;

namespace WizardAi.Service.External
{
    public class OpenAiService : IOpenAiService
    {
        private readonly AiConfigurations _aiConfigurations;
        private readonly OpenAIAPI _openAIAPI;
        public OpenAiService(IOptions<AiConfigurations> aiConfigurations)
        {
            _aiConfigurations = aiConfigurations.Value;
            _openAIAPI = new(_aiConfigurations.OpenAi.SecretKey);
        }

        public async Task<CompletionResult> CreateTextCompletionsAsync(CompletionRequest request, int optionCount = 3)
         => await _openAIAPI.Completions
            .CreateCompletionsAsync(request, optionCount);

        public async Task<AudioResultVerbose> GetSpeechToTextDetailsAsync(byte[] audioArray)
        {
            using MemoryStream stream = new(audioArray);
            var audioDetails = await _openAIAPI.Transcriptions
                .GetWithDetailsAsync(stream, "audio.mp3", "tr");

            return audioDetails;
        }
    }
}
