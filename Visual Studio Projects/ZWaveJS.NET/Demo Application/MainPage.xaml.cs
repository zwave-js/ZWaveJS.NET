using CommunityToolkit.Mvvm.ComponentModel;

namespace Demo_Application;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
		BindingContext = new ConnectViewModel();
	}

	private void ConnectClick(object? sender, EventArgs e)
	{
		AC_Wait.IsRunning = true;
		AC_Wait.IsVisible = true;
		switch (PK_Mode.SelectedItem.ToString())
		{
			case "Host":
			 App._Instance.StartDriver(true,PK_Port.SelectedItem.ToString()!);
			break;

			case "Client":
			 App._Instance.StartDriver(false,TX_Target.Text);
			break;
		}
		
	}
}

public partial class ConnectViewModel : ObservableObject
{
	public ConnectViewModel()
	{
		Ports = ZWaveJS.NET.Lib.SerialPorts();
		Mode = "Select One";
	}

	partial void OnModeChanged(string value)
	{
		OnPropertyChanged(nameof(IsHost));
		OnPropertyChanged(nameof(IsClient));
	}

	public string[] Ports {get;}

	[ObservableProperty]
	public string mode;

	public bool IsHost => Mode == "Host";
	public bool IsClient => Mode == "Client";

	
}


