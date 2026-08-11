using TwipDevEasyTube.Models;

namespace TwipDevEasyTube.Services
{
	public class SearchStateService
	{
		public string LastSearchText { get; set; } = string.Empty;
		public List<VideoItem> LastResults { get; set; } = [];
	}
}
