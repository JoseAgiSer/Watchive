using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcHive.Domain.API
{
    internal class TMDBMovieResponse
    {
        [JsonProperty("results")]
        public List<TMDBMovie> results { get; set; }
    }

    internal class TMDBMovie
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("release_date")]
        public string releaseDate { get; set; }

        [JsonProperty("poster_path")]
        public string poster_path { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("vote_average")]
        public float VoteAverage { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> generoId { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }
    }
}
