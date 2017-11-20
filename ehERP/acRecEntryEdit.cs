using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ehERP
{
    public partial class acRecEntryEdit : Form
    {
        MySqlConnection con = new MySqlConnection(@"datasource= localhost; port = 3306; database= eh_db; username= root; password=;");
        ToolTip tip = new ToolTip();
        public acRecEntryEdit()
        {
            InitializeComponent();
            //hidePnl.BringToFront();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            SpName.Text = "";
            SoNo.Text = "";
            SiNo.Text = "";
            SprName.Text = "";

            eC1.Text = "";
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

            //hidePnl.BringToFront();

        }

        private void ret_MouseHover(object sender, EventArgs e)
        {
            tip.Show("Retrive from the previous Calculation", ret);
        }

        private void eSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            string q1 = $"select * from new_record where PartyName like '%{SpName.Text}%' and OrderNo like '%{SoNo.Text}%' and InvoiceNo like '%{SiNo.Text}%'and ItemName like '%{SprName.Text}%'";
            MySqlCommand cm = new MySqlCommand(q1, con);
            MySqlDataReader dr = cm.ExecuteReader();
            dr.Read();
            if(dr.HasRows)
            {
                try
                {
                    eDate.Text = dr.GetString(1);
                    eC1.Text = dr.GetString(2);
                    epName.Text = dr.GetString(3);
                    eoNo.Text = dr.GetString(4);
                    eiNo.Text = dr.GetString(5);
                    eprName.Text = dr.GetString(6);
                    euPrice.Text = dr.GetString(7);
                    eQty.Text = dr.GetString(8);
                    eUnit.Text = dr.GetString(10);
                    eTotal.Text = dr.GetString(11);
                    eRemarks.Text = dr.GetString(12);
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
            string q2 = $"select * from final_rec_record where PartyName like '%{SpName.Text}%' and OrderNo like '%{SoNo.Text}%' and InvoiceNo like '%{SiNo.Text}%'";
            cm = new MySqlCommand(q2, con);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {
                    sBlnc.Text = dr.GetString(5);
                    eAdvanced.Text = dr.GetString(6);                    
                }
                catch (Exception a)
                {
                    MessageBox.Show("Errors: " + a);
                }

            }
            else
            {
                MessageBox.Show("Nothing to show");
            }
            con.Close();
        }

        private void ret_Click(object sender, EventArgs e)
        {
            sBlnc.Text = (float.Parse(sBlnc.Text) - float.Parse(eTotal.Text)).ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            sBlnc.Text = (float.Parse(sBlnc.Text) + float.Parse(eTotal.Text)).ToString();
        }

        private void eSave_Click(object sender, EventArgs e)
        {
            try
            {
                /* con.Open();
                 MySqlCommand cmd = con.CreateCommand();
                 cmd.CommandType = CommandType.Text;
                 cmd.CommandText = $"{$"update new_record set Dept = '"}{eC1.Text}', PartyName = '{epName.Text}', OrderNo= '{eoNo.Text}',InvoiceNo= '{eiNo.Text}', ItemName= '{eprName.Text}', UnitPrice= '{euPrice.Text}',Quantity= '{eQty.Text}', Unit= '{eUnit.Text}', Total= '{eTotal.Text}', Remarks= '{eRemarks.Text}',  Date= @a, Deadline=@a1 where PartyName like '%{SpName.Text}%' and OrderNo like '%{SoNo.Text}%' and InvoiceNo like '%{SiNo.Text}%' ItemName like '%{SprName.Text}%';" +
                     $"{$"update final_rec_record set PartyName = '"}{epName.Text}', OrderNo= '{eoNo.Text}' InvoiceNo= '{eiNo.Text}',Total= '{eTotal.Text}',Advanced= '{eAdvanced.Text}', Date= @a where PartyName like '%{SpName.Text}%' and OrderNo like '%{SoNo.Text}%' and InvoiceNo like '%{SiNo.Text}%'";
                 cmd.Parameters.Add("@a", MySqlDbType.Date).Value = eDate.Value.Date;
                 cmd.Parameters.Add("@a1", MySqlDbType.Date).Value = deadline.Value.Date;
                 int x = cmd.ExecuteNonQuery();

                 if (x > 0)
                 {
                     MessageBox.Show("Success");
                 }
                 else
                 {
                     MessageBox.Show("there are some Errors");
                 } */


                string query = "UPDATE new_record SET Remarks= '" + eRemarks.Text + "' WHERE PartyName like '%"+epName.Text+"%'";

                //Open connection
                con.Open();
                
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand();
                    //Assign the query using CommandText
                    cmd.CommandText = query;
                    //Assign the connection using Connection
                    cmd.Connection = con;

                    //Execute query
                    cmd.ExecuteNonQuery();

                //close connection
                con.Close();
                

            }
            catch (Exception a)
            {
                MessageBox.Show("Errors: " + a);
            }
           
            con.Close();
        }
    }
}
