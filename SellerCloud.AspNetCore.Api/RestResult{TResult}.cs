namespace SellerCloud.AspNetCore.Api
{
    public class RestResult<TResult>
        where TResult : new()
    {
        public string Message { get; set; }

        public TResult Value { get; set; }

        public static RestResult<TResult> Empty(string message)
            => new RestResult<TResult>
            {
                Value = new TResult(),
                Message = message
            };
    }
}
