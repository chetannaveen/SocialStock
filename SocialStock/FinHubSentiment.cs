namespace SocialStock.FhSentiment
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class FinHubSentiment
    {
        [JsonProperty("reddit", NullValueHandling = NullValueHandling.Ignore)]
        public Sentiment[] Reddit { get; set; }

        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("twitter", NullValueHandling = NullValueHandling.Ignore)]
        public Sentiment[] Twitter { get; set; }
    }

    public partial class Sentiment
    {
        [JsonProperty("atTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? AtTime { get; set; }

        [JsonProperty("mention", NullValueHandling = NullValueHandling.Ignore)]
        public long Mention { get; set; } = 0;

        [JsonProperty("positiveScore", NullValueHandling = NullValueHandling.Ignore)]
        public double PositiveScore { get; set; } = 0.0;

        [JsonProperty("negativeScore", NullValueHandling = NullValueHandling.Ignore)]
        public double NegativeScore { get; set; } = 0;

        [JsonProperty("positiveMention", NullValueHandling = NullValueHandling.Ignore)]
        public long PositiveMention { get; set; } = 0;

        [JsonProperty("negativeMention", NullValueHandling = NullValueHandling.Ignore)]
        public long NegativeMention { get; set; } = 0;

        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public double Score { get; set; } = 0;
    }

    public partial class FinHubSentiment
    {
        public static FinHubSentiment FromJson(string sentiment) => JsonConvert.DeserializeObject<FinHubSentiment>(sentiment, SocialStock.FhSentiment.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this FinHubSentiment self) => JsonConvert.SerializeObject(self, SocialStock.FhSentiment.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
