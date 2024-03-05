using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Enums;
using WizardAi.Core.Extensions;

namespace WizardAi.Core.Patterns.Builder
{
    public class PromptBuilder
    {
        private string _subject;
        private string _description;
        private int? _wordCount;
        private PromptType _promptType;

        public PromptBuilder SetSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public PromptBuilder SetDescription(string description)
        {
            _description = description; 
            return this;
        }

        public PromptBuilder SetWordCount(int? wordCount)
        {
            _wordCount = wordCount;
            return this;
        }

        public PromptBuilder SetPromptType(PromptType promptType)
        {
            _promptType = promptType;
            return this;
        }

        public string Build()
            => GetFormattedPromptByPromptType(_promptType);

        private string GetFormattedPromptByPromptType(PromptType promptType)
        {
            string inputText = $"Konu: {_subject}\n";
            if (!string.IsNullOrWhiteSpace(_description))
                inputText += $"Açıklama: {_description}\n";

            string wordCountFilter = "";
            if (_wordCount.HasValue)
                wordCountFilter = $"en az {_wordCount.Value} kelimelik";

            return $"Son cümlenin yarım bırakılmadan mutlaka anlamlı bir biçimde bitirildiği {wordCountFilter} Türkçe dilinde bir {promptType.GetDisplayName()} yazısı yaz.\n{inputText}";
        }
    }
}
