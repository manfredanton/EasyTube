namespace TwipDevEasyTube.Models
{
	public class VideoItem
	{
		public string VideoId { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string ChannelName { get; set; } = string.Empty;
		public string ThumbnailUrl { get; set; } = string.Empty;

		public string WatchUrl => $"https://www.youtube.com/watch?v={VideoId}";
		public string EmbedUrl => $"https://www.youtube.com/embed/{VideoId}?autoplay=1&rel=0&modestbranding=1&playsinline=1";
	}
}
