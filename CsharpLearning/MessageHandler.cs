using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CsharpLearning
{
    class MessageHandler:HttpClientHandler
    {
        string displayMessage = "";
        public MessageHandler(string message)
        {
            displayMessage = message;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,System.Threading.CancellationToken cancellationToken)
        {
            Console.WriteLine("In DisplayMessageHandler " + displayMessage);
            if (displayMessage=="error")
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
