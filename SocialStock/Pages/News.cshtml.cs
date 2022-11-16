using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialStock.CompanyNews;

namespace SocialStock.Pages
{
    public class NewsModel : PageModel
    {
        static HttpClient client = new HttpClient();
        DateTime dateTime = DateTime.UtcNow.Date;
        public FinHubCompanyNews[]? news { get; set; }
        public async Task<IActionResult> OnGetAsync(string CompanySymbol)
        {

            HttpResponseMessage responseNews = await client.GetAsync("https://finnhub.io/api/v1/company-news?symbol=" + CompanySymbol + "dateTime");
            if (responseNews.IsSuccessStatusCode)
            {
                string financialNewsResult = await responseNews.Content.ReadAsStringAsync();
                news = FinHubCompanyNews.FromJson(financialNewsResult);

            }
            return Page();
        }
    }
}

