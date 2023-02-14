//*/
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CollectionToExport
{
    class Program 
    {

        private static string cid = Environment.GetEnvironmentVariable("Tokped_CID");
        private static string cis = Environment.GetEnvironmentVariable("Tokped_CIS");

        private static string tokpedHost="https://fs.tokopedia.net";
        private static string tokpedAccountHost="https://accounts.tokopedia.com";
        private static string appId = "17689";
        private static string talithaBedsetShopId = "709444";
        private static string tokpedToken = "";
        static HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            string response = await ObtainAccessToken();
            dynamic data = JObject.Parse(response);

            Console.WriteLine(data.access_token);
            tokpedToken=data.access_token;
            response = await GetProductInfo(appId, talithaBedsetShopId, tokpedToken);
            Console.WriteLine(response);   
        }

        static async Task<string> ObtainAccessToken()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{cid}:{cis}")));

            HttpResponseMessage response = await client.PostAsync($"{tokpedAccountHost}/token?grant_type=client_credentials", null);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> GetProductInfo(string app_id, string shop_id, string token)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{tokpedHost}/inventory/v1/fs/{app_id}/product/info?shop_id={shop_id}&page=1&per_page=10&sort=1"),
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

    }
}