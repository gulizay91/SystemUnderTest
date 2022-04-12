﻿// <auto-generated https://app.quicktype.io/ />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Sample.Contract;
//
//    var movie = Movie.FromJson(jsonString);
namespace Sample.Contract
{

    using Newtonsoft.Json;
    using Sample.Infrastructure;

    public partial class Movie
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("rank")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Rank { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class Movie
    {
        public static Movie[] FromJson(string json) => JsonConvert.DeserializeObject<Movie[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Movie[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }


}
