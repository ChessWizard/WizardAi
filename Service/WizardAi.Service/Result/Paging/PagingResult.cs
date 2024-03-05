namespace WizardAi.Service.Result.Paging
{
    public class PagingResult<T> : BaseResult<T>
    {
        public PagingMetaData PagingMetaData { get; set; }
    }
}
