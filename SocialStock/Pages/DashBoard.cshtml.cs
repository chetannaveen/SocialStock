using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SocialStock.BasicCompanyInfo;
using SocialStock.CompanyNews;
using SocialStock.FhSentiment;
using SocialStock.Financials;
using SocialStock.InsiderInfo;
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

            await UpdateSSResponseAsync(CompanySymbol);

            return Page();
        }
        public async Task UpdateSSResponseAsync(string CompanySymbol)
        {
            SSResponse.CompanySymbol = CompanySymbol;
            await GetCompanyProfile(CompanySymbol);
            if (!SSResponse.IncorrectCompanySymbol)
            {
                await GetSocialMediaSentiment(CompanySymbol);
                await GetStockMetrics(CompanySymbol);
                await GetTrendingTweets(CompanySymbol);
                await GetCompanyNews(CompanySymbol);
                await GetInSiderSentimentCharts(CompanySymbol);
            }

        }
        private async Task GetInSiderSentimentCharts(string CompanySymbol)
        {
            int year = DateTime.Now.AddYears(-1).Year;
            DateTime FromDate = new DateTime(year, 1, 1);

            string url = "https://finnhub.io/api/v1/stock/insider-sentiment?symbol=" + CompanySymbol +
                "&from=" + FromDate.ToString("yyyy-MM-dd") + "&to=" + DateTime.Now.ToString("yyyy-MM-dd")
                + "&token=cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg";
            HttpResponseMessage responseInside = await client.GetAsync(url);
            if (responseInside.IsSuccessStatusCode)
            {
                string insiderSentimentResult = await responseInside.Content.ReadAsStringAsync();
                SSResponse.InsiderSentiment = InsiderSentiment.FromJson(insiderSentimentResult);
                CreateCharts(SSResponse.InsiderSentiment);

            }
        }
        private void CreateCharts(InsiderSentiment insiderSentiment)
        {
            List<string> labels = new List<string>();
            List<string> dataChange = new List<string>();
            List<string> dataMSPR = new List<string>();
            if (insiderSentiment.Data.Length != 0)
            {
                foreach (Datum datum in insiderSentiment.Data)
                {
                    labels.Add(datum.Year.ToString() + "-" + datum.Month.ToString());

                    dataChange.Add(datum.Change.ToString());
                    dataMSPR.Add(datum.Mspr.ToString());
                }
                string labelString = string.Join(",", labels);
                string dataChangeString = string.Join(",", dataChange);
                string dataMSPRString = string.Join(",", dataMSPR);
                SSResponse.InsiderChangeGraph = "https://quickchart.io/chart/render/zm-e57c31a5-9ff5-40f8-a807-e938f0c840ac?" + "data1=" + dataChangeString + "&labels=" + labelString;
                SSResponse.InsiderMsprGraph = "https://quickchart.io/chart/render/zm-cc269a4a-32fa-4c84-a7be-cd610ee5c972?" + "data1=" + dataMSPRString + "&labels=" + labelString;

            }

        }
        private async Task GetSocialMediaSentiment(string CompanySymbol)
        {
            HttpResponseMessage response = await client.GetAsync("https://finnhub.io/api/v1/stock/social-sentiment?symbol=" + CompanySymbol +
                            "&from=" + DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd") + "&to=" + DateTime.Now.ToString("yyyy-MM-dd")
                            + "&token=cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg");
            if (response.IsSuccessStatusCode)
            {
                string socialAPIResult = await response.Content.ReadAsStringAsync();
                var social = FinHubSentiment.FromJson(socialAPIResult);
                PostDataAnalysis(social);
            }
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
        public void PostDataAnalysis(FinHubSentiment social)
        {
            if (social.Twitter.Length != 0)
            {
                foreach (Sentiment sentiment in social.Twitter)
                {
                    SSResponse.TwitterMention = SSResponse.TwitterMention + sentiment.Mention;
                    SSResponse.TwitterScore = SSResponse.TwitterScore + sentiment.Score;

                    SSResponse.TwitterPositiveMention = SSResponse.TwitterPositiveMention + sentiment.PositiveMention;
                    SSResponse.TwitterNegativeMention = SSResponse.TwitterNegativeMention + sentiment.NegativeMention;

                    SSResponse.TwitterPositiveScore = SSResponse.TwitterPositiveScore + sentiment.PositiveScore;
                    SSResponse.TwitterNegativeScore = SSResponse.TwitterNegativeScore + sentiment.NegativeScore;
                }

                SSResponse.TwitterScore = Math.Round(SSResponse.TwitterScore / social.Twitter.Length, 2);
                SSResponse.TwitterMention = SSResponse.TwitterMention / social.Twitter.Length;

                SSResponse.TwitterNegativeScore = Math.Round(SSResponse.TwitterNegativeScore / social.Twitter.Length, 2);
                SSResponse.TwitterPositiveScore = Math.Round(SSResponse.TwitterPositiveScore / social.Twitter.Length, 2);

                SSResponse.TwitterPositiveMention = SSResponse.TwitterPositiveMention / social.Twitter.Length;
                SSResponse.TwitterNegativeMention = SSResponse.TwitterNegativeMention / social.Twitter.Length;
            }
            if (social.Reddit.Length != 0)
            {
                foreach (Sentiment sentiment in social.Reddit)
                {
                    SSResponse.RedditMention = SSResponse.RedditMention + sentiment.Mention;
                    SSResponse.RedditScore = SSResponse.RedditScore + sentiment.Score;

                    SSResponse.RedditPositiveMention = SSResponse.RedditPositiveMention + sentiment.PositiveMention;
                    SSResponse.RedditNegativeMention = SSResponse.RedditNegativeMention + sentiment.NegativeMention;

                    SSResponse.RedditPositiveScore = SSResponse.RedditPositiveScore + sentiment.PositiveScore;
                    SSResponse.RedditNegativeScore = SSResponse.RedditNegativeScore + sentiment.NegativeScore;
                }
                SSResponse.RedditScore = Math.Round(SSResponse.RedditScore / social.Reddit.Length, 2);
                SSResponse.RedditMention = SSResponse.RedditMention / social.Reddit.Length;

                SSResponse.RedditNegativeScore = Math.Round(SSResponse.RedditNegativeScore / social.Reddit.Length, 2);
                SSResponse.RedditPositiveScore = Math.Round(SSResponse.RedditPositiveScore / social.Reddit.Length, 2);

                SSResponse.RedditPositiveMention = SSResponse.RedditPositiveMention / social.Reddit.Length;
                SSResponse.RedditNegativeMention = SSResponse.RedditNegativeMention / social.Reddit.Length;
            }

        }
        public async Task<string> apiCallAsync(string CompanySymbol)
        {
            string responseJson = "";
            JsonSerializerSettings SerializerNullIgnoreSetting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            JsonSerializerSettings SerializerNullIgnoreDefaultIgnoreSetting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            await UpdateSSResponseAsync(CompanySymbol);
            if (SSResponse.IncorrectCompanySymbol)
            {

                SSResponse.message = "please provide a valid Company Symbol in the query";
                responseJson = JsonConvert.SerializeObject(SSResponse, Formatting.Indented, SerializerNullIgnoreDefaultIgnoreSetting);
            }
            else
            {
                responseJson = JsonConvert.SerializeObject(SSResponse, Formatting.Indented, SerializerNullIgnoreSetting);
            }




            return responseJson;
        }
    }
}
