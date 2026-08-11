using Microsoft.JSInterop;

namespace TwipDevEasyTube.Services
{
	public static class JsInteropExtensions
	{
		/// <summary>
		/// Blendet auf mobilen Geräten die Bildschirmtastatur aus, indem der Fokus
		/// vom aktiven Element genommen wird. Reine Komfortfunktion – schlägt der
		/// Aufruf fehl, darf das den Suchablauf nicht unterbrechen.
		/// </summary>
		public static async Task BlurActiveElementAsync(this IJSRuntime js)
		{
			try
			{
				await js.InvokeVoidAsync("easyTube.blurActiveElement");
			}
			catch (JSException)
			{
			}
		}
	}
}
