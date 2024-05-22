using Microsoft.Extensions.Options;
using OpenAI_API;
using OpenAI_API.Completions;
using OpenAI_API.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using WizardAi.Core.External;
using WizardAi.Service.Configurations;
using OpenAI_API.Audio;
using OpenAI_API.Images;
using OpenAI_API.Chat;
using WizardAi.Core.Enums;
using WizardAi.Core.Extensions;
using WizardAi.Core.Helpers;
using Microsoft.AspNetCore.SignalR;
using WizardAi.WebSocket.Hubs;
using WizardAi.WebSocket.Hubs.Interfaces;

namespace WizardAi.Service.External
{
    public class OpenAiService : IOpenAiService
    {
        private readonly AiConfigurations _aiConfigurations;
        private readonly OpenAIAPI _openAIAPI;
        private readonly IHubContext<CompletionHub, ICompletionHub> _completionHubContext;
        public OpenAiService(IOptions<AiConfigurations> aiConfigurations, IHubContext<CompletionHub, ICompletionHub> completionHubContext)
        {
            _aiConfigurations = aiConfigurations.Value;
            _openAIAPI = new(_aiConfigurations.OpenAi.SecretKey);
            _completionHubContext = completionHubContext;
        }

        public async Task CreateStreamingCompletionAsync(CompletionRequest request, string connectionId)
        {
            var streamingCompletion = _openAIAPI.Completions
                                                .StreamCompletionEnumerableAsync(request);

            await foreach (var completion in streamingCompletion)
            {
                foreach (var chunk in completion.Completions)
                {
                    await _completionHubContext.Clients
                                     .Client(connectionId)
                                     .ReceiveTextGenerationAssistantMessage(chunk.Text);
                    await Task.Delay(30);
                }
            }
        }

        public async Task<CompletionResult> CreateTextCompletionAsync(CompletionRequest request, int optionCount = 1)
         => await _openAIAPI.Completions
            .CreateCompletionsAsync(request, optionCount);

        public async Task<AudioResultVerbose> GetSpeechToTextDetailsAsync(byte[] audioArray)
        {
            using MemoryStream stream = new(audioArray);
            var audioDetails = await _openAIAPI.Transcriptions
                .GetWithDetailsAsync(stream, "audio.mp3", "tr");

            return audioDetails;
        }

        public async Task<ImageResult> GenerateImageFromTextAsync(ImageGenerationRequest request)
         => await _openAIAPI.ImageGenerations
                .CreateImageAsync(request);

        public async Task<ChatResult> GenerateSingleImageUrlToTextAsync(string prompt, string imageUrl, CreativityType creativityType, int requestedOption)
        {
            List<ChatMessage> messages = new()
            {
                new ChatMessage
                {
                    Role = ChatMessageRole.User,
                    TextContent = prompt,
                },
                new ChatMessage
                {
                    Role = ChatMessageRole.User,
                    Images = new List<ChatMessage.ImageInput>
                    {
                        new ChatMessage.ImageInput(imageUrl)
                    }
                }
            };
            
            var request = VisionHelper.GenerateVisionRequestByCreativity(creativityType, messages, requestedOption);

            return await _openAIAPI.Chat
                .CreateChatCompletionAsync(request);
        }

        public async Task<ChatResult> GenerateSingleBase64ImageToTextAsync(string prompt, byte[] base64Image, CreativityType creativityType, int requestedOption)
        {
            List<ChatMessage> messages = new()
            {
                new ChatMessage
                {
                    Role = ChatMessageRole.User,
                    TextContent = prompt,
                },
                new ChatMessage
                {
                    Role = ChatMessageRole.User,
                    Images = new List<ChatMessage.ImageInput>
                    {
                        new ChatMessage.ImageInput(base64Image)
                    }
                }
            };

            var request = VisionHelper.GenerateVisionRequestByCreativity(creativityType, messages, requestedOption);

            return await _openAIAPI.Chat
                .CreateChatCompletionAsync(request);
        }

        public async Task<ChatResult> GenerateMultipleImageToTextAsync(string prompt, List<string> urlImages, List<byte[]> base64Images, CreativityType creativityType, int requestedOption)
        {
            List<ChatMessage> initialMessages = new()
            {
                new ChatMessage
                {
                    Role = ChatMessageRole.User,
                    TextContent = prompt,
                }
            };

            var request = VisionHelper.GenerateVisionRequestByCreativity(creativityType, initialMessages, requestedOption);
            ChatMessage imageMessage = new()
            {
                Role = ChatMessageRole.User,
                Images = new()
            };

            AddUrlImagesToMessage(urlImages, imageMessage);
            AddBase64ImagesToMessage(base64Images, imageMessage);
            request.Messages.Add(imageMessage);

            return await _openAIAPI.Chat
                .CreateChatCompletionAsync(request);
        }

        #region Private Methods

        private static void AddUrlImagesToMessage(List<string> urlImages, ChatMessage imageMessage)
        {
            if (!urlImages.IsNullOrNotAny())
            {
                foreach (var urlImage in urlImages)
                {
                    var image = new ChatMessage.ImageInput(urlImage);
                    imageMessage.Images.Add(image);
                }
            }
        }

        private static void AddBase64ImagesToMessage(List<byte[]> base64Images, ChatMessage imageMessage)
        {
            if (!base64Images.IsNullOrNotAny())
            {
                foreach (var base64Image in base64Images)
                {
                    var image = new ChatMessage.ImageInput(base64Image);
                    imageMessage.Images.Add(image);
                }
            }
        }

        #endregion


    }

}
