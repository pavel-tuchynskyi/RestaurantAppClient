namespace RestaurantAppClient.Models
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public ResponseException Exception { get; set; }
    }

    public class Response
    {
        public int StatusCode { get; set; }
        public Empty Data { get; set; }
        public ResponseException? Exception { get; set; }
    }

    public class Empty : IEquatable<Empty>, IComparable<Empty>, IComparable
    {
        public static Empty Value => new Empty();

        public int CompareTo(Empty other)
        {
            return 0;
        }

        public int CompareTo(object obj)
        {
            return 0;
        }

        public bool Equals(Empty other)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    public class ResponseException
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
        public string? Detail { get; set; }
    }
}
