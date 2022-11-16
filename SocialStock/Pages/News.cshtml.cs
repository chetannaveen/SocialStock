using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using SocialStock.CompanyNews;

namespace SocialStock.Pages
{
    public class NewsModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public FinHubCompanyNews[]? news { get; set; }
        public async Task<IActionResult> OnGetAsync(string CompanySymbol)
        {
            var queryParams = new Dictionary<string, string>()
            {
                ["symbol"] = CompanySymbol,
                ["from"] = "2022-09-01",
                ["to"] = "2022-10-09",
                ["token"] = "cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg"
            };
            var financialNewsUrl = QueryHelpers.AddQueryString("https://finnhub.io/api/v1/company-news", queryParams);
            try
            {
                HttpResponseMessage responseNews = await client.GetAsync(financialNewsUrl);
                if (responseNews.IsSuccessStatusCode)
                {
                    string financialNewsResult = await responseNews.Content.ReadAsStringAsync();
                    if (financialNewsResult != null && !financialNewsResult.Equals(""))
                    {
                        news = FinHubCompanyNews.FromJson(financialNewsResult);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception occurred:", e);
            }
            
            return Page();
        }
    }
}

