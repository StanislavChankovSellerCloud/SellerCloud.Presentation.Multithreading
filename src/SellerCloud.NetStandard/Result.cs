namespace SellerCloud.NetStandard
{
    public class Result
    {
        public Result(string message, bool isSuccessful = true)
        {
            Message = message;
            IsSuccessful = isSuccessful;
        }

        public static Result Success(string message)
            => new Result(message);

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }
    }
}
