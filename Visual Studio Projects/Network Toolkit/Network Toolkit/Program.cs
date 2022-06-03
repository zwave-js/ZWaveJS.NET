using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network_Toolkit
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Splash());
        }
    }

    public class ComboObject
    {
        public ComboObject(string Label, object Value)
        {
            this.Label = Label;
            this.Value = Value;
        }

        public override string ToString()
        {
            return this.Label;
        }


        public string Label { get; private set; }
        public object Value { get; private set; }
    }
        

}
