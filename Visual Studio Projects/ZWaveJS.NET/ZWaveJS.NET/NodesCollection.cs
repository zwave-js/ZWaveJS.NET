using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;

namespace ZWaveJS.NET
{
    public class NodesCollection : INotifyPropertyChanged
    {
        internal NodesCollection()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal List<ZWaveNode> Nodes { get; set; }

        internal NodesCollection(ZWaveNode[] Nodes)
        {
            this.Nodes = Nodes.ToList();
        }

        internal void AddNodeToCollection(ZWaveNode Node)
        {
            Nodes.Add(Node);
            OnPropertyChanged(nameof(Collection));
        }

        internal void ReplaceInformation(ZWaveNode Source, ZWaveNode Target)
        {
            PropertyInfo[] infos = typeof(ZWaveNode).GetProperties();
            foreach (PropertyInfo info in infos)
            {
                info.SetValue(Target, info.GetValue(Source, null), null);
            }
        }

        internal void RemoveNodeFromCollection(int Node)
        {
            ZWaveNode N = Get(Node);
            if (N != null)
            {
                Nodes.Remove(N);
                OnPropertyChanged(nameof(Collection));
            }
        }

        public ZWaveNode Get(int Node)
        {
            return Nodes.FirstOrDefault((N) => N.id.Equals(Node));
        }

        public ZWaveNode[] Collection => Nodes.ToArray();

    }
}
