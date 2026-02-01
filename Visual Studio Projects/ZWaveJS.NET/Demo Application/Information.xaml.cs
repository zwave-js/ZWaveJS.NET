using Microsoft.VisualBasic;

namespace Demo_Application;

public partial class Information : ContentPage
{
	public Information()
	{
		InitializeComponent();
	}

	private async void SoftReset(object sender, EventArgs e)
	{
		ZWaveJS.NET.CMDResult Res = await App._Instance._Driver.SoftReset();
		if (Res.Success)
		{
			_ = MainThread.InvokeOnMainThreadAsync(async () =>
		   {
			   await DisplayAlertAsync("Soft Reset", "Soft Reset Completed, the library will restart and re-initialize.", "OK");
		   });
		}
		else
		{
			_ = MainThread.InvokeOnMainThreadAsync(async () =>
		   {
			   await DisplayAlertAsync(Res.ErrorCode,Res.Message, "OK");
		   });
		}
	}

	private async void HardReset(object sender, EventArgs e)
	{

		ZWaveJS.NET.CMDResult Res = await App._Instance._Driver.HardReset();
		if (Res.Success)
		{
			_ = MainThread.InvokeOnMainThreadAsync(async () =>
		   {
			   await DisplayAlertAsync("Hard Reset", "Hard Reset Completed, the library will restart and re-initialize.", "OK");
		   });
		}
		else
		{
			_ = MainThread.InvokeOnMainThreadAsync(async () =>
		   {
			   await DisplayAlertAsync(Res.ErrorCode,Res.Message, "OK");
		   });
		}

	}
}