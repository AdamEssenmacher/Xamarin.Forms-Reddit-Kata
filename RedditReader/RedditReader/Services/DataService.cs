using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using RedditReader.ViewModels;

namespace RedditReader.Services
{
    public interface IDataService
    {
        Task<IList<ItemViewModel>> GetItemsAsync();
    }

    internal class DataService : IDataService
    {
        private readonly HttpClient _httpClient;

        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<ItemViewModel>> GetItemsAsync()
        {
            HttpResponseMessage responseMessage =
                await _httpClient.GetAsync(
                    "https://www.reddit.com/r/catpictures/.json"); // URL would be parameterized or configured somehow in a real app.

            // Throws exception if GetAsync was not successful.
            responseMessage.EnsureSuccessStatusCode();

            using Stream resultStream = await responseMessage.Content.ReadAsStreamAsync();

            var listModel = await JsonSerializer.DeserializeAsync<RedditListModel>(resultStream,
                new JsonSerializerOptions
                {
                    // So we can use C# PascalCase naming conventions
                    PropertyNameCaseInsensitive = true
                });

            return listModel?.Data?.Children?.Select(child => new ItemViewModel
            {
                SelfText = child?.Data?.SelfText,
                Title = child?.Data?.Title,
                Thumbnail = child?.Data?.Thumbnail,
                Image = HttpUtility.HtmlDecode(child?.Data?.Preview?.Images?.FirstOrDefault()?.Source?.Url)
            }).ToArray() ?? Array.Empty<ItemViewModel>();
        }
    }
}