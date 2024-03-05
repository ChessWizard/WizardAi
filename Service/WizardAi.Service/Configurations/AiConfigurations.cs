namespace WizardAi.Service.Configurations
{
    public class AiConfigurations
    {
        public OpenAi OpenAi { get; set; }
    }

    public class OpenAi : BaseAiConfigurations
    {

    }

    public class BaseAiConfigurations
    {
        public string SecretKey { get; set; }
    }
}
