namespace Demo_Application;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
	}

	private void ConnectClick(object? sender, EventArgs e)
	{
		AC_Wait.IsRunning = true;
		AC_Wait.IsVisible = true;
		switch (PK_Mode.SelectedItem.ToString())
		{
			case "Host":
			 App._Instance.StartDriver(true,TX_Target.Text);
			break;

			case "Client":
			 App._Instance.StartDriver(false,TX_Target.Text);
			break;
		}
		
		
	}
}
