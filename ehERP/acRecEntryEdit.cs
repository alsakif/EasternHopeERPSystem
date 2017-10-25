using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ehERP
{
    public partial class acRecEntryEdit : Form
    {
        public acRecEntryEdit()
        {
            InitializeComponent();
            hidePnl.BringToFront();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            SpName.Text = "";
            SoNo.Text = "";
            SiNo.Text = "";
            SprName.Text = "";

            ec1.Text = "";
            epName.Text = "";
            eoNo.Text = "";
            eiNo.Text = "";
            eprName.Text = "";
            euPrice.Text = "";
            eQty.Text = "";
            eUnit.Text = "";
            eTotal.Text = "";
            eAdvanced.Text = "";
            eRemarks.Text = "";

            hidePnl.BringToFront();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            hidePnl.SendToBack();
        }
    }
}
