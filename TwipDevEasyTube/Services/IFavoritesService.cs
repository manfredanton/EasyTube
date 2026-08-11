using TwipDevEasyTube.Models;

namespace TwipDevEasyTube.Services
{
	public interface IFavoritesService
	{
		event Action? Changed;

		Task<IReadOnlyList<VideoItem>> GetAllAsync();
		Task<bool> ContainsAsync(string videoId);
		Task AddAsync(VideoItem video);
		Task RemoveAsync(string videoId);
	}
}
