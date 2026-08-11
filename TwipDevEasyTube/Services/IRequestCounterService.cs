namespace TwipDevEasyTube.Services
{
	public interface IRequestCounterService
	{
		int TodayCount { get; }
		int DailyLimit { get; }
		Task IncrementAsync();
		Task LoadAsync();
	}
}
