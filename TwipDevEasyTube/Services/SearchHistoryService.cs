using Microsoft.JSInterop;
using System.Text.Json;

namespace TwipDevEasyTube.Services
{
	public class SearchHistoryService
	{
		private const string StorageKey = "easytube_searchhistory";
		private const int MaxEntries = 20;
		private readonly IJSRuntime _js;

		public SearchHistoryService(IJSRuntime js)
		{
			_js = js;
		}

		public async Task<List<string>> GetAllAsync()
		{
			try
			{
				var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
				if (string.IsNullOrEmpty(json))
					return [];
				return JsonSerializer.Deserialize<List<string>>(json) ?? [];
			}
			catch
			{
				return [];
			}
		}

		public async Task AddAsync(string term)
		{
			if (string.IsNullOrWhiteSpace(term))
				return;

			var entries = await GetAllAsync();
			entries.RemoveAll(e => e.Equals(term, StringComparison.OrdinalIgnoreCase));
			entries.Insert(0, term.Trim());

			if (entries.Count > MaxEntries)
				entries = entries.Take(MaxEntries).ToList();

			var json = JsonSerializer.Serialize(entries);
			await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
		}

		public async Task<List<string>> GetSuggestionsAsync(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				return [];

			var entries = await GetAllAsync();
			return entries
				.Where(e => e.Contains(input.Trim(), StringComparison.OrdinalIgnoreCase))
				.Take(5)
				.ToList();
		}

		public async Task RemoveAsync(string term)
		{
			var entries = await GetAllAsync();
			entries.RemoveAll(e => e.Equals(term, StringComparison.OrdinalIgnoreCase));
			var json = JsonSerializer.Serialize(entries);
			await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
		}

		public async Task ClearAllAsync()
		{
			await _js.InvokeVoidAsync("localStorage.removeItem", StorageKey);
		}
	}
}
