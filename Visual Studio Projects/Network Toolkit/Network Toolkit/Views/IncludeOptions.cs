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

namespace Network_Toolkit.Views
{
    
    public partial class IncludeOptions : UserControl
    {
        public delegate void StartInclusionExclusion(object Options);
        public event StartInclusionExclusion StartInclusionExclusionEvent;

        public IncludeOptions()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InclusionOptions IO = new InclusionOptions();
            IO.strategy = Enums.InclusionStrategy.Insecure;


            StartInclusionExclusionEvent?.Invoke(IO);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            StartInclusionExclusionEvent?.Invoke(checkBox1.Checked);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InclusionOptions IO = new InclusionOptions();
            IO.strategy = Enums.InclusionStrategy.Default;
            IO.forceSecurity = checkBox2.Checked;


            StartInclusionExclusionEvent?.Invoke(IO);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InclusionOptions IO = new InclusionOptions();
            IO.strategy = Enums.InclusionStrategy.Security_S2;


            StartInclusionExclusionEvent?.Invoke(IO);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InclusionOptions IO = new InclusionOptions();
            IO.strategy = Enums.InclusionStrategy.Security_S0;


            StartInclusionExclusionEvent?.Invoke(IO);
        }
    }
}
