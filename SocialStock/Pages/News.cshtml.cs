using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialStock.CompanyNews;

namespace SocialStock.Pages
{
    public class NewsModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public FinHubCompanyNews[]? news { get; set; }
        public async Task<IActionResult> OnGetAsync(string CompanySymbol)
        {

            HttpResponseMessage responseNews = await client.GetAsync("https://finnhub.io/api/v1/company-news?symbol=" + CompanySymbol + "&from=2022-09-01&to=2022-10-09&token=cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg");
            if (responseNews.IsSuccessStatusCode)
            {
                string financialNewsResult = await responseNews.Content.ReadAsStringAsync();
                news = FinHubCompanyNews.FromJson(financialNewsResult);

            }
            return Page();
        }
    }
}

