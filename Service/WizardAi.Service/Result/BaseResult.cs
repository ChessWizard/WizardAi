namespace WizardAi.Service.Result
{
    public class BaseResult<TData>
    {
        public TData Data { get; set; }

        public string Message { get; set; }

        public int HttpStatusCode { get; set; }
    }
}
