using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialStock.BasicCompanyInfo;
using SocialStock.CompanyNews;
using SocialStock.Financials;
using SocialStock.Response;
using SocialStock.Tweets;

namespace SocialStock.Pages
{
    public class DashBoardModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public SSResponse SSResponse = new SSResponse();

        public async Task<IActionResult> OnGetAsync(string CompanySymbol)
        {

            await UpdateSSResponse(CompanySymbol);

            return Page();
        }
        public async Task UpdateSSResponse(string CompanySymbol)
        {
            SSResponse.CompanySymbol = CompanySymbol;
            await GetStockMetrics(CompanySymbol);
            await GetCompanyProfile(CompanySymbol);
            await GetTrendingTweets(CompanySymbol);
            await GetCompanyNews(CompanySymbol);

        }
        private async Task GetCompanyNews(string CompanySymbol)
        {
            HttpResponseMessage responseNews = await client.GetAsync("https://finnhub.io/api/v1/company-news?symbol=" + CompanySymbol +
                "&from=" + DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd") + "&to=" + DateTime.Now.ToString("yyyy-MM-dd") +
                "&token=cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg");
            if (responseNews.IsSuccessStatusCode)
            {
                string financialNewsResult = await responseNews.Content.ReadAsStringAsync();
                FinHubCompanyNews[] News = FinHubCompanyNews.FromJson(financialNewsResult);

                if (News.Length > 10)
                {
                    SSResponse.News = News[1..10];
                }
                else
                {
                    SSResponse.News = News;
                }
            }
        }
        private async Task GetTrendingTweets(string CompanySymbol)
        {
            HttpResponseMessage responseTweets = await client.GetAsync("https://api.social-searcher.com/v2/trends?q=" + CompanySymbol + "&key=84115b4028964b26ea46f08761beb279&network=twitter");
            if (responseTweets.IsSuccessStatusCode)
            {
                string tweetsResult = await responseTweets.Content.ReadAsStringAsync();
                TrendingTweets trendingTweets = TrendingTweets.FromJson(tweetsResult);
                SSResponse.Tweets = trendingTweets;
                if (trendingTweets.Posts.Length > 16)
                {
                    SSResponse.Tweets.Posts = trendingTweets.Posts[1..10];
                }


            }
        }
        private async Task GetCompanyProfile(string CompanySymbol)
        {
            HttpResponseMessage responseCompanyData = await client.GetAsync("https://finnhub.io/api/v1/stock/profile2?symbol=" + CompanySymbol + "&token=cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg");
            if (responseCompanyData.IsSuccessStatusCode)
            {
                string companyDataResult = await responseCompanyData.Content.ReadAsStringAsync();
                if (companyDataResult != "{}")
                {
                    SSResponse.CompanyData = CompanyData.FromJson(companyDataResult);
                }
                else
                {
                    SSResponse.IncorrectCompanySymbol = true;
                }


            }
        }
        private async Task GetStockMetrics(string CompanySymbol)
        {
            HttpResponseMessage responseFinancials = await client.GetAsync("https://finnhub.io/api/v1/stock/metric?symbol=" + CompanySymbol + "&metric=all&token=cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg");
            if (responseFinancials.IsSuccessStatusCode)
            {
                string financialAPIResult = await responseFinancials.Content.ReadAsStringAsync();
                SSResponse.FinHubFinancials = FinHubFinancials.FromJson(financialAPIResult);

            }
        }
    }
}
