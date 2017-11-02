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
            add.Enabled = false;// page1: entry--disable add button initially.
            addItemBtn.Enabled = false;//page1: entry
            hidePnl.BringToFront();//page1: entry
            hidePnl01.BringToFront();// page1: entry
            hidePnl02.SendToBack();
        }

        /* ************************************************** page 1 : entry starts************************************************************************* */
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
                cmd.CommandText = $"insert into new_record(Dept,PartyName, OrderNo,InvoiceNo,ItemName,UnitPrice,Quantity, Unit,Total, Remarks, Date, Deadline) values" +
                    $"('{dept.Text}','{pName.Text}','{oNo.Text}','{iNo.Text}','{prName.Text}','{uPrice.Text}','{qty.Text}','{unit.Text}','{total.Text}','{remark.Text}', @a, @deadline);" +
                    $" insert into final_rec_record(PartyName,OrderNo,InvoiceNo,Total,Advanced, Date) values " +
                    $"('{pName.Text}','{oNo.Text}','{iNo.Text}','{total.Text}','{advanced.Text}', @a)";
                cmd.Parameters.Add("@a", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;
                cmd.Parameters.Add("@deadline", MySqlDbType.Date).Value = deadline.Value.Date;
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
                add.Enabled = true;
                addItemBtn.Enabled = true;
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors: " + ex);
            }
        }


        private void refreahBtn_Click(object sender, EventArgs e)
        {
            try
            {
                dept.Text = "";
                pName.Text = "";
                oNo.Text = "";
                iNo.Text = "";
                prName.Text = "";
                uPrice.Text = "";
                qty.Text = "";
                unit.Text = "";
                total.Text = "";
                advanced.Text = "";
                remark.Text = "";

                add.Enabled = false;// disable add button
                addItemBtn.Enabled = false;
                hidePnl.BringToFront(); //checkbox will be hidden
                hidePnl01.BringToFront(); //total will be hidden
                cBox1.Checked = false;
                hidePnl02.SendToBack();
            }
            catch (Exception x)
            {
                // MessageBox.Show("Errors: " + x);
            }
            
        }

        private void addItemBtn_Click(object sender, EventArgs e)
        {
            /*Retrive previous items data from db and'd show upto invoice number. total will be shown on 
             the total RHS label*/
            hidePnl.SendToBack();
            cBox1.Checked = false;
            hidePnl01.SendToBack();
            hidePnl02.BringToFront();
            recSaveBtn.Enabled = false;
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string q1 = $"select * from new_record order by Serial desc limit 1";
            MySqlCommand cm = new MySqlCommand(q1, con);
            MySqlDataReader dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {
                    dept.Text = dr.GetString(2);
                    pName.Text = dr.GetString(3);
                    oNo.Text = dr.GetString(4);
                    iNo.Text = dr.GetString(5);
                    prName.Text = "";
                    uPrice.Text = "";
                    qty.Text = "";
                    unit.Text = "";
                    total.Text = "";
                    advanced.Text = "";
                    remark.Text = "";
                }
                catch (Exception q)
                {
                    MessageBox.Show("Errors: " + q);
                }
            }
            else
            {
                MessageBox.Show("There's no information");
            }
            con.Close();

            con.Open();
            cmd.CommandType = CommandType.Text;
            string q2 = $"select final_rec_record.Total from final_rec_record order by Serial desc limit 1";
            cm = new MySqlCommand(q2, con);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {
                    lbltotal.Text = dr.GetString(0);
                }
                catch (Exception q)
                {
                    MessageBox.Show("Errors: " + q);
                }
            }
            else
            {
                MessageBox.Show("There's no information");
            }
            con.Close();



        }
        private void cBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cBox1.Checked)
            {
                lbltotal.Text = (float.Parse(lbltotal.Text) + float.Parse(total.Text)).ToString();
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            //update the total from the total label to datbase total on same order and invoice number 
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"update final_rec_record set Total = '{lbltotal.Text}' order by Serial desc limit 1";
        
            int x = cmd.ExecuteNonQuery();

            if (x > 0)
            {
               // MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("there are some Errors");
            }
            con.Close();


            try
            {
                con.Open();
             
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"insert into new_record(Dept,PartyName, OrderNo,InvoiceNo,ItemName,UnitPrice,Quantity, Unit,Total, Remarks, Date) values" +
                    $"('{dept.Text}','{pName.Text}','{oNo.Text}','{iNo.Text}','{prName.Text}','{uPrice.Text}','{qty.Text}','{unit.Text}','{total.Text}','{remark.Text}', @a)";
                cmd.Parameters.Add("@a", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;
                int y = cmd.ExecuteNonQuery();

                if (y > 0)
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("there are some Errors");
                }
                con.Close();

                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors: " + ex);
            }
        }

        private void eEditBtn_Click(object sender, EventArgs e)
        {
            acRecEntryEdit obj = new acRecEntryEdit();
            obj.Show();
        }





        /* *********************************************************Page: 1-- Entry Ends***************************************************************** */
    }
}
