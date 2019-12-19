using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SellerCloud.Presentation.Multithreading.Tests.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Continues the <see cref="Task"/> and returning the <see cref="HttpStatusCode"/> .
        /// </summary>
        public static Task<HttpStatusCode> ContinueWithResultAsync(this Task<HttpResponseMessage> task)
        {
            var taskCompletionSource = new TaskCompletionSource<HttpStatusCode>();

            task.ContinueWith(continuation =>
            {
                taskCompletionSource.SetResult(continuation.Result.StatusCode);
            }, TaskScheduler.Default);

            return taskCompletionSource.Task;
        }

        public static Task<Tuple<HttpStatusCode, TResult>> ContinueWithResultAsync<TResult>(this Task<HttpResponseMessage> task)
        {
            var taskCompletionSource = new TaskCompletionSource<Tuple<HttpStatusCode, TResult>>();

            task.ContinueWith(async continuation =>
            {
                string getResponseBody = await continuation.Result.Content.ReadAsStringAsync();
                TResult responseObject = JsonConvert.DeserializeObject<TResult>(getResponseBody);

                taskCompletionSource.SetResult(new Tuple<HttpStatusCode, TResult>(continuation.Result.StatusCode, responseObject));
            }, TaskScheduler.Default);

            return taskCompletionSource.Task;
        }
    }
}
