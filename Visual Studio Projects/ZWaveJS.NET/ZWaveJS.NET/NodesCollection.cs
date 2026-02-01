using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System;

namespace ZWaveJS.NET
{
    public class NodesCollection : INotifyPropertyChanged
    {
        internal NodesCollection()
        {
        }

        private static readonly IEnumerable<PropertyInfo> UpdatebaleNodeProbs = typeof(ZWaveNode)
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(UpdateableNodePropertyAttribute)));

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

        internal void ReplaceInformation(ZWaveNode source, ZWaveNode target)
        {
            foreach (PropertyInfo prop in UpdatebaleNodeProbs)
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    object value = prop.GetValue(source);
                    prop.SetValue(target, value);
                }
            }
            OnPropertyChanged(nameof(Collection));
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
