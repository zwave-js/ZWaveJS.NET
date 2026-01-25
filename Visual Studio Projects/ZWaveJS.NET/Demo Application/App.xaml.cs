using Microsoft.Extensions.DependencyInjection;
using ZWaveJS.NET;

namespace Demo_Application;

public partial class App : Application
{
	internal Driver _Driver;
	internal static App _Instance;
	internal static Driver Driver => _Instance._Driver; // xmal support

	public App()
	{
		InitializeComponent();
		_Instance = this;
	}

	private void Quit()
	{
#if MACCATALYST
		UIKit.UIApplication.SharedApplication.PerformSelector(new ObjCRuntime.Selector("terminate:"), null, 0);
#elif WINDOWS
			System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
	}


	internal void StartDriver(bool Host, string PoretOrURI)
	{
		string DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		Server.PSIRoot = DocumentsPath;
		switch (Host)
		{
			case true:
				ZWaveOptions Options = new ZWaveOptions();

				Options.inclusionUserCallbacks.abort += HandleAbort;
				Options.inclusionUserCallbacks.validateDSKAndEnterPIN += HandleDSK;
				Options.inclusionUserCallbacks.grantSecurityClasses += HandleGrant;
				Options.features.softReset = false;
				Options.storage.cacheDir = Path.Join(DocumentsPath,"zwave-js-cache");

				_Driver = new Driver(PoretOrURI.ToString(), Options);
				_Driver.DriverReady += HandleReady;
				_Driver.ServerConnectionError += HandleError;
				break;

			default:
				_Driver = new Driver(new Uri(PoretOrURI.ToString()), null);
				_Driver.DriverReady += HandleReady;
				_Driver.ServerConnectionError += HandleError;
				break;

		}

		_Driver.Start();

	}

	private void HandleAbort()
	{
		
	}

	private  string HandleDSK(string DSK)
	{
		return DSK;
	}

	private InclusionGrant HandleGrant(InclusionGrant Requested)
	{
		return Requested;
	}

	private async void HandleError(string ErrorCode, string Message, Action<bool, int?> Retry)
	{
		if (Retry == null)
		{
			_ = MainThread.InvokeOnMainThreadAsync(async () =>
			{
				await Current.Windows[0].Page.DisplayAlertAsync($"Error Code : {ErrorCode}", Message, "Quit");
				Quit();

			});
		}
		else
		{
			_ = MainThread.InvokeOnMainThreadAsync(async () =>
		   {
			   bool Reset = await Current.Windows[0].Page.DisplayAlertAsync($"Error Code : {ErrorCode}", Message, "Retry", "Quit");
			   if (Reset)
			   {
				   Retry(true, null);
			   }
			   else
			   {
				   Retry(false, null);
				   Quit();
			   }
		   });
		}
	}

	private void HandleReady()
	{
		_ = MainThread.InvokeOnMainThreadAsync(async () =>
			{
				Window window = Application.Current.Windows[0];
				window.Page = new MainAppShell();
			});
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}