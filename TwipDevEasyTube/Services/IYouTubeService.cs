using TwipDevEasyTube.Models;

namespace TwipDevEasyTube.Services
{
	public interface IYouTubeService
	{
		Task<IReadOnlyList<VideoItem>> SearchAsync(string query, CancellationToken cancellationToken = default);
	}
}
