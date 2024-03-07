namespace RestaurantAppClient.Models
{
    public class QueryParameters
    {
        public Search Search { get; set; }
        public OrderBy OrderBy { get; set; }
        public Paging Paging { get; set; }

        public QueryParameters(Search search, OrderBy orderBy, Paging paging) : this(orderBy, paging)
        {
            Search = search;
            OrderBy = orderBy;
            Paging = paging;
        }

        public QueryParameters(OrderBy orderBy, Paging paging)
        {
            OrderBy = orderBy;
            Paging = paging;
        }
    }

    public class Search
    {
        public string SearchParameter { get; set; } = string.Empty;
        public string SearchTerm { get; set; } = string.Empty;

        public Search(){ }
        public Search(string searchParameter, string searchTerm)
        {
            SearchParameter = searchParameter;
            SearchTerm = searchTerm;
        }
    }

    public class OrderBy
    {
        public string Value { get; set; } = string.Empty;
        public bool Ascending { get; set; } = false;

        public OrderBy(){ }
        public OrderBy(string value, bool ascending)
        {
            Value = value;
            Ascending = ascending;
        }
    }

    public class Paging
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public Paging(){ }
        public Paging(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
