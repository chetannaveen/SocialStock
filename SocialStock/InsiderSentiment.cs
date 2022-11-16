﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using ApiCallTry.InsiderInfo;
//
//    var insiderSentiment = InsiderSentiment.FromJson(jsonString);

namespace SocialStock.InsiderInfo
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class InsiderSentiment
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Datum[] Data { get; set; }

        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("year", NullValueHandling = NullValueHandling.Ignore)]
        public long? Year { get; set; }

        [JsonProperty("month", NullValueHandling = NullValueHandling.Ignore)]
        public long? Month { get; set; }

        [JsonProperty("change", NullValueHandling = NullValueHandling.Ignore)]
        public long Change { get; set; } = 0;

        [JsonProperty("mspr", NullValueHandling = NullValueHandling.Ignore)]
        public double Mspr { get; set; } = 0.0;
    }

    public partial class InsiderSentiment
    {
        public static InsiderSentiment FromJson(string sentiment) => JsonConvert.DeserializeObject<InsiderSentiment>(sentiment, SocialStock.InsiderInfo.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this InsiderSentiment self) => JsonConvert.SerializeObject(self, SocialStock.InsiderInfo.Converter.Settings);
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
