using Microsoft.JSInterop;
using System.Text.Json;
using TwipDevEasyTube.Models;

namespace TwipDevEasyTube.Services
{
	public sealed class FavoritesService : IFavoritesService
	{
		private const string StorageKey = "easytube_favorites";
		private readonly IJSRuntime _js;

		public FavoritesService(IJSRuntime js)
		{
			_js = js;
		}

		public async Task<IReadOnlyList<VideoItem>> GetAllAsync()
		{
			var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
			if (string.IsNullOrEmpty(json))
				return [];

			return JsonSerializer.Deserialize<List<VideoItem>>(json) ?? [];
		}

		public async Task<bool> ContainsAsync(string videoId)
		{
			return (await GetAllAsync()).Any(video => video.VideoId == videoId);
		}

		public async Task AddAsync(VideoItem video)
		{
			var videos = (await GetAllAsync()).ToList();
			videos.RemoveAll(item => item.VideoId == video.VideoId);
			videos.Insert(0, video);
			await SaveAsync(videos);
		}

		public async Task RemoveAsync(string videoId)
		{
			var videos = (await GetAllAsync()).ToList();
			videos.RemoveAll(item => item.VideoId == videoId);
			await SaveAsync(videos);
		}

		private async Task SaveAsync(List<VideoItem> videos)
		{
			var json = JsonSerializer.Serialize(videos);
			await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
		}
	}
}
