using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using ZWaveJS.NET;

namespace Demo_Application;

public partial class Nodes : ContentPage
{
	public Nodes()
	{
		InitializeComponent();
		BindingContext = new NodesViewModel();

	}

    private async void StartInclude(object sender, EventArgs e)
	{
		InclusionOptions O = new InclusionOptions();
		O.strategy = Enums.InclusionStrategy.Insecure;
		
		ZWaveJS.NET.CMDResult Res = await App._Instance._Driver.Controller.BeginInclusion(O);
		if (Res.Success && Res.ResultPayloadAs<bool>())
		{
			_ = MainThread.InvokeOnMainThreadAsync(async () =>
		   {
			   await DisplayAlertAsync("Inclusion Start", "Place your device in to Inclusion Mode", "OK");
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

	private async void StartExclude(object sender, EventArgs e)
	{
		ExclusionOptions O = new ExclusionOptions();
		O.strategy = Enums.ExclusionStrategy.ExcludeOnly;
		

		ZWaveJS.NET.CMDResult Res = await App._Instance._Driver.Controller.BeginExclusion(O);
		if (Res.Success && Res.ResultPayloadAs<bool>())
		{
			_ = MainThread.InvokeOnMainThreadAsync(async () =>
		   {
			   await DisplayAlertAsync("Exclusion Start", "Place your device in to Exclusion Mode", "OK");
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

public class NodesViewModel : ObservableObject
{
	public NodesViewModel()
	{
		Nodes = App._Instance._Driver.Controller.Nodes;
	}

	public NodesCollection Nodes { get; }
}

public class ReadyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
		if((bool)value!)
		{
			return Color.FromArgb("#0C5A92");
		}
		else
		{
			return Color.FromArgb("#404040");
		}
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StatusConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
		ZWaveJS.NET.Enums.NodeStatus Status = (Enums.NodeStatus)value!;
		switch (Status)
		{
			case Enums.NodeStatus.Asleep:
			case Enums.NodeStatus.Unknown:
			return Color.FromArgb("#08345A");

			case Enums.NodeStatus.Dead:
			return Color.FromArgb("#404040");

			default:
			return Color.FromArgb("#0C5A92");
		}
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class InterviewStatusConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value?.ToString() == "Complete")
		{
			return Color.FromArgb("#0C5A92");
		}
		else
		{
			return Color.FromArgb("#404040");
		}
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}