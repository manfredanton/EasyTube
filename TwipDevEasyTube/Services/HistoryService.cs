using Microsoft.JSInterop;
using System.Text.Json;
using TwipDevEasyTube.Models;

namespace TwipDevEasyTube.Services
{
	public sealed class HistoryService : IHistoryService
	{
		private const string StorageKey = "easytube_history";
		private readonly IJSRuntime _js;

		private static readonly JsonSerializerOptions JsonOptions = new()
		{
			WriteIndented = false
		};

		public HistoryService(IJSRuntime js)
		{
			_js = js;
		}

		public async Task<IReadOnlyList<WatchHistoryEntry>> GetAllAsync()
		{
			try
			{
				var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
				if (string.IsNullOrEmpty(json))
					return [];

				return JsonSerializer.Deserialize<List<WatchHistoryEntry>>(json, JsonOptions) ?? [];
			}
			catch
			{
				return [];
			}
		}

		public async Task AddAsync(WatchHistoryEntry entry)
		{
			var entries = (await GetAllAsync()).ToList();

			// Bereits vorhandenen Eintrag entfernen → neueste zuerst
			entries.RemoveAll(e => e.VideoId == entry.VideoId);
			entries.Insert(0, entry);

			// Auf Maximum begrenzen
			if (entries.Count > Constants.MaxHistoryEntries)
				entries = entries.Take(Constants.MaxHistoryEntries).ToList();

			await SaveAsync(entries);
		}

		public async Task RemoveAsync(string videoId)
		{
			var entries = (await GetAllAsync()).ToList();
			entries.RemoveAll(e => e.VideoId == videoId);
			await SaveAsync(entries);
		}

		public async Task ClearAllAsync()
		{
			await _js.InvokeVoidAsync("localStorage.removeItem", StorageKey);
		}

		private async Task SaveAsync(List<WatchHistoryEntry> entries)
		{
			var json = JsonSerializer.Serialize(entries, JsonOptions);
			await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
		}
	}
}
