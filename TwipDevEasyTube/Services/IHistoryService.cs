using TwipDevEasyTube.Models;

namespace TwipDevEasyTube.Services
{
	public interface IHistoryService
	{
		Task<IReadOnlyList<WatchHistoryEntry>> GetAllAsync();
		Task AddAsync(WatchHistoryEntry entry);
		Task RemoveAsync(string videoId);
		Task ClearAllAsync();
	}
}
