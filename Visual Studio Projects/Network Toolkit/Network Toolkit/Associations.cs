using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWaveJS.NET;

namespace Network_Toolkit
{
    public partial class Associations : Form
    {
        ZWaveNode _Node;
        public Associations(ZWaveNode Node)
        {
            InitializeComponent();
            _Node = Node;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
