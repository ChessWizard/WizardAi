using OpenAI_API.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Enums;

namespace WizardAi.Core.Helpers
{
    public static class VisionHelper
    {
        public static ChatRequest GenerateVisionRequestByCreativity(CreativityType creativityType, IList<ChatMessage> chatMessages, int requestedOption, int maxTokens = 1000)
        {
            var (Temperature, FrequencyPenalty, PresencePenalty) = GetCreativitySettings(creativityType);

            ChatRequest chatRequest = new()
            {
                Model = OpenAI_API.Models.Model.GPT4_Vision,
                MaxTokens = maxTokens,
                Temperature = Temperature,
                FrequencyPenalty = FrequencyPenalty,
                PresencePenalty = PresencePenalty,
                Messages = chatMessages,
                NumChoicesPerMessage = requestedOption
            };

            return chatRequest;
        }

        private static (double Temperature, double FrequencyPenalty, double PresencePenalty) GetCreativitySettings(CreativityType creativityType)
        {
            return creativityType switch
            {
                CreativityType.Basic => (0.3, 0, 0),
                CreativityType.Advanced => (0.7, 0.2, 0.2),
                CreativityType.Creative => (1.2, 0.5, 0.5),
                _ => (0.5, 0, 0),
            };
        }
    }

    
}
