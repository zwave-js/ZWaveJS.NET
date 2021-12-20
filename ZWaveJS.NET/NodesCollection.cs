using System;
using System.Linq;
namespace ZWaveJS.NET
{
    public class NodesCollection
    {
        internal NodesCollection(ZWaveNode[] Nodes)
        {
            this.Nodes = Nodes;
        }

        private ZWaveNode[] Nodes { get;  set; }

        public ZWaveNode Get(int Node)
        {
            return Nodes.FirstOrDefault((N) => N.nodeId.Equals(Node));
        }
    }
}
