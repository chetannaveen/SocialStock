using SocialStock.BasicCompanyInfo;
using SocialStock.CompanyNews;
using SocialStock.Financials;
using SocialStock.InsiderInfo;
using SocialStock.Tweets;

namespace SocialStock.Response
{
    public class SSResponse
    {
        public long TwitterMention = 0;
        public long TwitterPositiveMention = 0;
        public long TwitterNegativeMention = 0;
        public double TwitterScore = 0;
        public double TwitterPositiveScore = 0;
        public double TwitterNegativeScore = 0;
        public long RedditMention = 0;
        public long RedditPositiveMention = 0;
        public long RedditNegativeMention = 0;
        public double RedditScore = 0;
        public double RedditPositiveScore = 0;
        public double RedditNegativeScore = 0;
        public FinHubCompanyNews[] News { get; set; } = new FinHubCompanyNews[10];

        public TrendingTweets Tweets { get; set; } = new TrendingTweets();
        public CompanyData CompanyData { get; set; } = new CompanyData();
        public string? CompanySymbol { get; set; }
        public FinHubFinancials FinHubFinancials { get; set; } = new FinHubFinancials();
        public InsiderSentiment InsiderSentiment { get; set; } = new InsiderSentiment();
        public string? InsiderChangeGraph { get; set; }
        public string? InsiderMsprGraph { get; set; }

       
    }
}
