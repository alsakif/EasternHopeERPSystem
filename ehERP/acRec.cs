using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace ehERP
{
    public partial class acRec : UserControl
    {
        MySqlConnection con = new MySqlConnection(@"datasource= localhost; port = 3306; database= eh_db; username= root; password=;");
        public acRec()
        {
            InitializeComponent();
            addItemBtn.Enabled = false;// disable add button initially.
            hidePnl.BringToFront();
        }

        private void qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                total.Text = (float.Parse(uPrice.Text) * float.Parse(qty.Text)).ToString(); // total = qty X unit price
            }
            catch (Exception x)
            {
                // MessageBox.Show("Errors: " + x);
            }
        }
        private void recSaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"insert into new_record(ItemType, PartyName, OrderNo,InvoiceNo,ItemName,UnitPrice,Quantity, Unit,Total, Remarks, Date) values" +
                    $"('{c1.Text}','{pName.Text}','{oNo.Text}','{iNo.Text}','{prName.Text}','{uPrice.Text}','{qty.Text}','{unit.Text}','{total.Text}','{remark.Text}', @a);" +
                    $" insert into final_rec_record(PartyName,OrderNo,InvoiceNo,Total,Advanced, Date) values " +
                    $"('{pName.Text}','{oNo.Text}','{iNo.Text}','{total.Text}','{advanced.Text}', @a)";
                cmd.Parameters.Add("@a", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;
                int x = cmd.ExecuteNonQuery();

                if (x > 0)
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("there are some Errors");
                }
                con.Close();

                // Enable add item button
                addItemBtn.Enabled = true;
                hidePnl.SendToBack();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors: " + ex);
            }
        }

        private void addItemBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
