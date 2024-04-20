using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Enums;
using WizardAi.Core.Extensions;

namespace WizardAi.Core.Patterns.Builder
{
    public class TextPromptBuilder : IPromptBuilder<TextPromptBuilder>
    {
        private string _subject;
        private string _description;
        private int? _wordCount;
        private TextPromptType _promptType;
        private LanguageType? _languageType;

        public TextPromptBuilder SetSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public TextPromptBuilder SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public TextPromptBuilder SetWordCount(int? wordCount)
        {
            _wordCount = wordCount;
            return this;
        }

        public TextPromptBuilder SetPromptType(TextPromptType promptType)
        {
            _promptType = promptType;
            return this;
        }

        public TextPromptBuilder SetLanguageType(LanguageType languageType)
        {
            _languageType = languageType;
            return this;
        }

        public string Build()
         => _promptType switch
         {
             TextPromptType.Summary => BuildSummaryPrompt(),
             TextPromptType.None => BuildWritingPrompt(),
             TextPromptType.Article => BuildWritingPrompt(),
             TextPromptType.Blog => BuildWritingPrompt(),
             _ => throw new Exception("Not existed prompt type.")
         };

        private string BuildSummaryPrompt()
        {
            string wordCountFilter = GetWordCountFilter();
            var inputText = new StringBuilder($"Summarize this text, {wordCountFilter}: {_description}");
            AppendLanguage(inputText);
            return inputText.ToString();
        }

        private string BuildWritingPrompt()
        {
            var inputText = new StringBuilder();
            AppendLanguage(inputText);
            AppendSubject(inputText);
            AppendDescription(inputText);
            string wordCountFilter = GetWordCountFilter();
            return $"Write an {_promptType.GetDisplayName()} in which the last sentence must be finished meaningfully without leaving it unfinished, and it should be {wordCountFilter}.\n{inputText}";
        }

        private void AppendLanguage(StringBuilder inputText)
        {
            if (_languageType.HasValue)
                inputText.Append($"Output Language: {_languageType.Value.GetDisplayName()}\n");
        }

        private void AppendSubject(StringBuilder inputText)
        {
            if (!string.IsNullOrWhiteSpace(_subject))
                inputText.Append($"Subject: {_subject}\n");
        }

        private void AppendDescription(StringBuilder inputText)
        {
            if (!string.IsNullOrWhiteSpace(_description))
                inputText.Append($"Description: {_description}\n");
        }

        private string GetWordCountFilter()
        {
            return _wordCount.HasValue ? $"up to {_wordCount.Value} characters in length" : "";
        }
    }
}
