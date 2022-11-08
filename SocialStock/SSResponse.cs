using SocialStock.BasicCompanyInfo;
using SocialStock.CompanyNews;
using SocialStock.Financials;
using SocialStock.InsiderInfo;
using SocialStock.Tweets;

namespace SocialStock.Response
{
    public class SSResponse
    {
        public bool IncorrectCompanySymbol = false;
        public string message = "success";
        public long TwitterMention;
        public long TwitterPositiveMention;
        public long TwitterNegativeMention;
        public double TwitterScore;
        public double TwitterPositiveScore;
        public double TwitterNegativeScore;
        public long RedditMention;
        public long RedditPositiveMention;
        public long RedditNegativeMention;
        public double RedditScore;
        public double RedditPositiveScore;
        public double RedditNegativeScore;
        public string? CompanySymbol { get; set; }
        public CompanyData? CompanyData { get; set; }
        public InsiderSentiment? InsiderSentiment { get; set; }
        public string? InsiderChangeGraph { get; set; }
        public string? InsiderMsprGraph { get; set; }
        public FinHubCompanyNews[]? News { get; set; }
        public TrendingTweets? Tweets { get; set; }
        public FinHubFinancials? FinHubFinancials { get; set; }


    }
}
