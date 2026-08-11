namespace TwipDevEasyTube.Models
{
	public class WatchHistoryEntry
	{
		public string VideoId { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string ChannelName { get; set; } = string.Empty;
		public string ThumbnailUrl { get; set; } = string.Empty;
		public DateTime WatchedAt { get; set; }

		public string EmbedUrl => $"https://www.youtube.com/embed/{VideoId}?autoplay=1&rel=0&modestbranding=1&playsinline=1";
		public string WatchedAtFormatted => WatchedAt.ToString("dd.MM.yyyy HH:mm");

		public static WatchHistoryEntry FromVideoItem(VideoItem video) => new()
		{
			VideoId = video.VideoId,
			Title = video.Title,
			ChannelName = video.ChannelName,
			ThumbnailUrl = video.ThumbnailUrl,
			WatchedAt = DateTime.Now
		};
	}
}
