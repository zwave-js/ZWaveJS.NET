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
}

public class NodesViewModel : ObservableObject
{
	public NodesViewModel()
	{
		this.NodeCollection = new ObservableCollection<ZWaveNode>(App._Instance._Driver.Controller.Nodes.Array);
	}
	public ObservableCollection<ZWaveNode> NodeCollection { get; }
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