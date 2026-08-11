using Microsoft.JSInterop;
using System.Text.Json;

namespace TwipDevEasyTube.Services
{
	public class RequestCounterService : IRequestCounterService
	{
		private const string StorageKey = "easytube_requestcounter";
		private readonly IJSRuntime _js;

		private int _count;
		private DateOnly _date;

		public int TodayCount => _count;
		public int DailyLimit => Constants.DailyRequestLimit;

		public RequestCounterService(IJSRuntime js)
		{
			_js = js;
		}

		public async Task LoadAsync()
		{
			try
			{
				var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
				if (string.IsNullOrEmpty(json))
				{
					Reset();
					return;
				}

				var data = JsonSerializer.Deserialize<CounterData>(json);
				if (data is null || data.Date != DateOnly.FromDateTime(DateTime.Now))
				{
					Reset();
					await SaveAsync();
				}
				else
				{
					_count = data.Count;
					_date = data.Date;
				}
			}
			catch
			{
				Reset();
			}
		}

		public async Task IncrementAsync()
		{
			if (_date != DateOnly.FromDateTime(DateTime.Now))
				Reset();

			_count++;
			await SaveAsync();
		}

		private void Reset()
		{
			_count = 0;
			_date = DateOnly.FromDateTime(DateTime.Now);
		}

		private async Task SaveAsync()
		{
			var json = JsonSerializer.Serialize(new CounterData { Count = _count, Date = _date });
			await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
		}

		private sealed class CounterData
		{
			public DateOnly Date { get; set; }
			public int Count { get; set; }
		}
	}
}
