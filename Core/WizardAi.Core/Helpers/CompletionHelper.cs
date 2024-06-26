﻿using OpenAI_API.Completions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Enums;

namespace WizardAi.Core.Helpers
{
    public static class CompletionHelper
    {
        /// <summary>
        /// Yaratıcılık düzeyine göre Completion oluşturur
        /// </summary>
        /// <param name="creativityType"></param>
        /// <param name="maxTokens"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public static CompletionRequest GenerateCompletionRequestByCreativity(CreativityType creativityType, string prompt, int maxTokens = 1000)
        {
            var (Temperature, FrequencyPenalty, PresencePenalty) = GetCreativitySettings(creativityType);

            CompletionRequest completionRequest = new()
            {
                Model = OpenAI_API.Models.Model.ChatGPTTurboInstruct,
                MaxTokens = maxTokens,
                Temperature = Temperature,
                FrequencyPenalty = FrequencyPenalty,
                PresencePenalty = PresencePenalty,
                Prompt = prompt,
            };

            return completionRequest;
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
