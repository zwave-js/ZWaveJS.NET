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
    public partial class HealthCheck : Form
    {
        ZWaveNode _Node;
        public HealthCheck(ZWaveNode Node)
        {
            _Node = Node;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LBL_Rating.Text = "Health Rating: Please wait...0%";
            LST_Rounds.Items.Clear();

            _Node.CheckLifelineHealth(TB_Rounds.Value, LifelineHealthCheckProgress).ContinueWith((R) => {

                if (R.Result.Success)
                {
                    LifelineHealthCheckSummary Result = R.Result.ResultPayload as LifelineHealthCheckSummary;


                    this.Invoke((MethodInvoker)delegate () {

                        LBL_Rating.Text = "Health Rating: " + Result.rating.ToString();

                        int i = 1;
                        foreach (LifelineHealthCheckResult HCR in Result.results)
                        {
                            ListViewItem LVI = new ListViewItem(string.Format("#{0}", i));
                            LVI.SubItems.Add(string.Format("C:{0}, N:{1}", HCR.failedPingsController, HCR.failedPingsNode));
                            LVI.SubItems.Add(HCR.latency.ToString());
                            LVI.SubItems.Add(HCR.routeChanges.ToString());
                            LVI.SubItems.Add(HCR.minPowerlevel.ToString());
                            LVI.SubItems.Add(HCR.numNeighbors.ToString());
                            LVI.SubItems.Add(HCR.snrMargin.ToString());
                            LVI.SubItems.Add(HCR.rating.ToString());


                            LST_Rounds.Items.Add(LVI);
                            i++;
                        }

                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate () {

                        MessageBox.Show(R.Result.Message, "Failed To Complete Health Check", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    });
                }
            
            });
        }

        private void LifelineHealthCheckProgress(int Round, int TotalRounds, int LastRating)
        {
            this.Invoke((MethodInvoker)delegate () {

                LBL_Rating.Text = "Health Rating: Please wait..." + Math.Round(((decimal)(Round * 100))/TotalRounds)+"%";

            });
        }
    }
}
