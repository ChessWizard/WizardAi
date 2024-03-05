namespace WizardAi.Service.Result
{
    public class ErrorResult
    {
        public List<string> Errors { get; private set; } = new();
        public bool IsShow { get; private set; }

        public ErrorResult(string error, bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }

        public ErrorResult(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
