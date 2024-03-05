namespace WizardAi.Service.Result.Paging
{
    public class PagingMetaData
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        public PagingMetaData(int pageSize, int currentPage, int totalCount)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
