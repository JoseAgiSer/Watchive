using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using WatcHive.Domain.API;
using WatcHive.Domain;
using MySqlX.XDevAPI.Common;

namespace WatcHive.Persistence.Manages
{
    internal class APIManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "3a844aef6215be52d1e060ccea372b00";
        private readonly string _baseUrl = "https://api.themoviedb.org/3";

        public APIManager()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<TMDBMovie>> GetPopularMoviesAsync()
        {
            string url = $"{_baseUrl}/movie/popular?api_key={_apiKey}&language=es-ES&page=1";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var tmdbResponse = JsonConvert.DeserializeObject<TMDBMovieResponse>(jsonResponse);
                return tmdbResponse.results;
            }

            return new List<TMDBMovie>();
        }

        public async Task<List<TVShowDTO>> GetPopularSeriesAsync()
        {
            string url = $"{_baseUrl}/tv/popular?api_key={_apiKey}&language=es-ES&page=1";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TMDBTVSearchResponse>(jsonResponse);
                return result.results;
            }

            return new List<TVShowDTO>();
        }

        public async Task<List<TMDBMovie>> SearchMoviesByTitleAsync(string title)
        {
            string url = $"{_baseUrl}/search/movie?api_key={_apiKey}&language=es-ES&query={Uri.EscapeDataString(title)}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var tmdbResponse = JsonConvert.DeserializeObject<TMDBMovieResponse>(jsonResponse);
                return tmdbResponse.results;
            }

            return null;
        }

        public async Task<List<TVShowDTO>> SearchSeriesByTitleAsync(string query)
        {
            string url = $"{_baseUrl}/search/tv?api_key={_apiKey}&language=es-ES&query={Uri.EscapeDataString(query)}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TMDBTVSearchResponse>(jsonResponse);
                return result.results;
            }

            return new List<TVShowDTO>();
        }

        public async Task<List<TMDBMovie>> GetMoviesByGenreAsync(int genreId, int page = 1)
        {
            string url = $"{_baseUrl}/discover/movie?api_key={_apiKey}&language=es-ES&with_genres={genreId}&page={page}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TMDBMovieResponse>(jsonResponse);
                return result.results;
            }

            return new List<TMDBMovie>();

        }

        public async Task<List<TVShowDTO>> GetSeriesByGenreAsync(int genreId)
        {
            string url = $"{_baseUrl}/discover/tv?api_key={_apiKey}&language=es-ES&with_genres={genreId}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TMDBTVSearchResponse>(jsonResponse);
                return result.results;
            }

            return new List<TVShowDTO>();
        }

        public async Task<Dictionary<int, string>> GetGenresAsync()
        {
            string url = $"{_baseUrl}/genre/movie/list?api_key={_apiKey}&language=es-ES";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode){
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var genreResponse = JsonConvert.DeserializeObject<TMDBGenreResponse>(jsonResponse);

                return genreResponse.genres.ToDictionary(g => g.id, g => g.nombreGenero);
            }

            return new Dictionary<int, string>();
        }

        public async Task<List<TMDBMovie>> GetMoviesByGenresAsync(List<int> genreIds)
        {
            string genres = string.Join(",", genreIds);
            string url = $"{_baseUrl}/discover/movie?api_key={_apiKey}&language=es-ES&with_genres={genres}&sort_by=popularity.desc&page=1";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var tmdbResponse = JsonConvert.DeserializeObject<TMDBMovieResponse>(jsonResponse);
                return tmdbResponse.results.Take(8).ToList();
            }

            return new List<TMDBMovie>();
        }

        public async Task<List<TVShowDTO>> GetSeriesByGenresAsync(List<int> genreIds)
        {
            string genres = string.Join(",", genreIds);
            string url = $"{_baseUrl}/discover/tv?api_key={_apiKey}&language=es-ES&with_genres={genres}&sort_by=popularity.desc&page=1";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var tmdbResponse = JsonConvert.DeserializeObject<TMDBTVSearchResponse>(jsonResponse);
                return tmdbResponse.results.Take(8).ToList();
            }

            return new List<TVShowDTO>();
        }

        public async Task<List<TMDBMovie>> GetMoviesByProviderAsync(int providerId, string region = "ES")
        {
            string url = $"{_baseUrl}/discover/movie?api_key={_apiKey}&language=es-ES&watch_region={region}&with_watch_providers={providerId}&sort_by=popularity.desc";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var tmdbResponse = JsonConvert.DeserializeObject<TMDBMovieResponse>(jsonResponse);
                return tmdbResponse.results;
            }

            return new List<TMDBMovie>();
        }

        public async Task<List<TVShowDTO>> GetSeriesByProviderAsync(int providerId, string region = "ES")
        {
            string url = $"{_baseUrl}/discover/tv?api_key={_apiKey}&language=es-ES&watch_region={region}&with_watch_providers={providerId}&sort_by=popularity.desc";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var tmdbResponse = JsonConvert.DeserializeObject<TMDBTVSearchResponse>(jsonResponse);
                return tmdbResponse.results;
            }

            return new List<TVShowDTO>();
        }

        public async Task<Dictionary<string, int>> GetProvidersAsync(string region = "ES")
        {
            string url = $"{_baseUrl}/watch/providers/movie?api_key={_apiKey}&language=es-ES&watch_region={region}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var providerResponse = JsonConvert.DeserializeObject<TMDBProviderResponse>(jsonResponse);
                return providerResponse.results.ToDictionary(p => p.provider_name, p => p.provider_id);
            }

            return new Dictionary<string, int>();
        }
    }
}
