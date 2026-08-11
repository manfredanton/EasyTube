using System.Net.Http.Json;
using TwipDevEasyTube.Models;
using TwipDevEasyTube.Services.Dto;

namespace TwipDevEasyTube.Services
{
	public sealed class YouTubeService : IYouTubeService
	{
		private readonly HttpClient _httpClient;

		public YouTubeService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IReadOnlyList<VideoItem>> SearchAsync(
			string query,
			CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(query))
				return [];

			var url = $"/youtube/v3/search" +
					  $"?part=snippet" +
					  $"&q={Uri.EscapeDataString(query.Trim())}" +
					  $"&type=video" +
					  $"&maxResults={Constants.MaxSearchResults}" +
					  $"&key={Constants.YouTubeApiKey}";

			var response = await _httpClient.GetAsync(url, cancellationToken);

			if (!response.IsSuccessStatusCode)
			{
				var body = await response.Content.ReadAsStringAsync(cancellationToken);
				System.Diagnostics.Debug.WriteLine($"[YouTubeService] Fehler {(int)response.StatusCode}: {body}");
				throw new HttpRequestException(
					$"API-Fehler: {(int)response.StatusCode}",
					inner: null,
					statusCode: response.StatusCode);
			}

			var searchResponse = await response.Content
				.ReadFromJsonAsync<YouTubeSearchResponse>(cancellationToken: cancellationToken);

			if (searchResponse is null)
				return [];

			return searchResponse.Items
				.Where(item => !string.IsNullOrEmpty(item.Id.VideoId))
				.Select(item => new VideoItem
				{
					VideoId = item.Id.VideoId,
					Title = System.Net.WebUtility.HtmlDecode(item.Snippet.Title),
					ChannelName = System.Net.WebUtility.HtmlDecode(item.Snippet.ChannelTitle),
					ThumbnailUrl = item.Snippet.Thumbnails.Medium.Url
				})
				.ToList();
		}
	}
}
