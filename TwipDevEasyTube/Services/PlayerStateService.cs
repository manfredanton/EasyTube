using TwipDevEasyTube.Models;

namespace TwipDevEasyTube.Services
{
	public sealed class PlayerStateService
	{
		public VideoItem? CurrentVideo { get; private set; }
		public string OriginPath { get; private set; } = string.Empty;

		public event Action? Changed;

		public void Play(VideoItem video, string originPath)
		{
			CurrentVideo = video;
			OriginPath = NormalizePath(originPath);
			Changed?.Invoke();
		}

		public void Stop()
		{
			CurrentVideo = null;
			Changed?.Invoke();
		}

		public static string NormalizePath(string path)
		{
			return path.Trim('/');
		}
	}
}
