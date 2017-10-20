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
    public partial class ehHome : Form
    {
        public ehHome()
        {
            InitializeComponent();
            selector.Height = pnlDash.Height;
            selector.Top = pnlDash.Top;
        }

        private void pnlDash_Click(object sender, EventArgs e)
        {
            selector.Height = pnlDash.Height;
            selector.Top = pnlDash.Top;
        }

        private void pnlRec_Click(object sender, EventArgs e)
        {
            selector.Height = pnlRec.Height;
            selector.Top = pnlRec.Top;
            acRec1.BringToFront();
        }

        private void pnlPay_Click(object sender, EventArgs e)
        {
            selector.Height = pnlPay.Height;
            selector.Top = pnlPay.Top;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            GetMin();
        }
        private void GetMin()
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
