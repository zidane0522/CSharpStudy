using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CsharpLearning
{
    class HttpClientCls
    {
        public static async void GetData()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = null;
            response = await httpClient.GetAsync("http://www.baidu.com");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Response Statue Code: " + response.StatusCode + "" + response.ReasonPhrase);
                string responseBodyAsText = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBodyAsText);
            }
        }
    }
}
