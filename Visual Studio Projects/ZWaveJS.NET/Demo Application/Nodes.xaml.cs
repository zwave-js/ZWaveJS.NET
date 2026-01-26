using System.Collections.ObjectModel;
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