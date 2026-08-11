using System.Text.Json.Serialization;

namespace TwipDevEasyTube.Services.Dto
{
	internal sealed class YouTubeSearchResponse
	{
		[JsonPropertyName("items")]
		public List<YouTubeSearchItem> Items { get; set; } = [];
	}

	internal sealed class YouTubeSearchItem
	{
		[JsonPropertyName("id")]
		public YouTubeItemId Id { get; set; } = new();

		[JsonPropertyName("snippet")]
		public YouTubeSnippet Snippet { get; set; } = new();
	}

	internal sealed class YouTubeItemId
	{
		[JsonPropertyName("videoId")]
		public string VideoId { get; set; } = string.Empty;
	}

	internal sealed class YouTubeSnippet
	{
		[JsonPropertyName("title")]
		public string Title { get; set; } = string.Empty;

		[JsonPropertyName("channelTitle")]
		public string ChannelTitle { get; set; } = string.Empty;

		[JsonPropertyName("thumbnails")]
		public YouTubeThumbnails Thumbnails { get; set; } = new();
	}

	internal sealed class YouTubeThumbnails
	{
		[JsonPropertyName("medium")]
		public YouTubeThumbnail Medium { get; set; } = new();
	}

	internal sealed class YouTubeThumbnail
	{
		[JsonPropertyName("url")]
		public string Url { get; set; } = string.Empty;
	}
}
