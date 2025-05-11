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

        public async Task<string> SearchMoviesByTitleAsync(string title)
        {
            string url = $"{_baseUrl}/search/movie?api_key={_apiKey}&language=es-ES&query={Uri.EscapeDataString(title)}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
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

        public async Task<string> GetMoviesByGenreAsync(int genreId, int page = 1)
        {
            string url = $"{_baseUrl}/discover/movie?api_key={_apiKey}&language=es-ES&with_genres={genreId}&page={page}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode){
                return await response.Content.ReadAsStringAsync();
            }

            return null;
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
    }
}
