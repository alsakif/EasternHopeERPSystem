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
            hidePnl.SendToBack();//page1: entry
            hidePnl01.BringToFront();// page1: entry
            hidePnl02.BringToFront();
            addNew.Enabled = false;
            addP.Enabled = false;
            invHidePnl.BringToFront();
            invHidePnl01.BringToFront();
            inSearch.Enabled = false;
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
                    $" insert into final_rec_record(PartyName,OrderNo,InvoiceNo,Total,Advanced, Balance, Date) values " +
                    $"('{pName.Text}','{oNo.Text}','{iNo.Text}','{total.Text}','{advanced.Text}','{advanced.Text}', @a)";
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
                hidePnl.SendToBack(); //checkbox will be hidden
                hidePnl01.BringToFront(); //total will be hidden
                cBox1.Checked = false;
                hidePnl02.BringToFront();
                recSaveBtn.Enabled = true;
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
            hidePnl.BringToFront();
            cBox1.Checked = false;
            hidePnl01.SendToBack();
            hidePnl02.SendToBack();
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

            con.Open();
            try
            {


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




            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors: " + ex);
            }
            con.Close();
        }

        private void eEditBtn_Click(object sender, EventArgs e)
        {
            acRecEntryEdit obj = new acRecEntryEdit();
            obj.Show();
        }


        private void iEditBtn_Click_1(object sender, EventArgs e)
        {
            invoiceEntryEdit obj = new invoiceEntryEdit();
            obj.Show();
        }


        /* *********************************************************Orderpage Ends***************************************************************** */
        /* *********************************************************Shipment page starts***************************************************************** */

        private void shpSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            string q1 = $"select * from new_record where PartyName like '%{SpName.Text}%' and OrderNo like '%{SoNo.Text}%' and InvoiceNo like '%{SiNo.Text}%'and ItemName like '%{SprName.Text}%'";
            MySqlCommand cm = new MySqlCommand(q1, con);
            MySqlDataReader dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {

                    shpDept.Text = dr.GetString(2);
                    shpPn.Text = dr.GetString(3);
                    shpOn.Text = dr.GetString(4);
                    shpIn.Text = dr.GetString(6);
                    shpQty.Text = dr.GetString(8);
                    UpQty.Text = dr.GetString(9);
                    shpUnit.Text = dr.GetString(10);
                    label5.Text = dr.GetString(10);


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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                UpQty.Text = (float.Parse(UpQty.Text) + float.Parse(shipmentQty.Text)).ToString();
            }
        }

        private void shpSave_Click(object sender, EventArgs e)
        {
            con.Open();
            try
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"insert into shipment_record(PartyName, OrderNo,ItemName,shipmentQty,shipmentNo,Remarks, Date) values" +
                    $"('{shpPn.Text}','{shpOn.Text}','{shpIn.Text}','{shipmentQty.Text}','{shipNo.Text}','{shpRemarks.Text}', @a)";
                cmd.Parameters.Add("@a", MySqlDbType.Date).Value = shpDate.Value.Date;
                int y = cmd.ExecuteNonQuery();

                if (y > 0)
                {
                    // MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("there are some Errors");
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors: " + ex);
            }
            con.Close();

            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"{$"update new_record set UpdatedQty = '"}{UpQty.Text}' where PartyName like '%{SpName.Text}%' and OrderNo like '%{SoNo.Text}%' and InvoiceNo like '%{SiNo.Text}%' and ItemName like '%{SprName.Text}%';";
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


            }
            catch (Exception a)
            {
                MessageBox.Show("Errors: " + a);
            }

            con.Close();
        }


        /* *********************************************************Shipment page Ends***************************************************************** */
        /* *********************************************************Receipt page starts***************************************************************** */


        private void rSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            string q1 = $"select * from final_rec_record where PartyName like '%{rSpName.Text}%' and OrderNo like '%{rSoNo.Text}%' and InvoiceNo like '%{rSiNo.Text}%'";
            MySqlCommand cm = new MySqlCommand(q1, con);
            MySqlDataReader dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {
                    rPn.Text = dr.GetString(2);
                    rOn.Text = dr.GetString(3);
                    rTamt.Text = dr.GetString(5);
                    rTblnc.Text = dr.GetString(7);
                    UpdatedBlnc.Text = dr.GetString(7);
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

            con.Open();
            string q2 = $"select * from final_inv_record where PartyName like '%{rSpName.Text}%' and OrderNo like '%{rSoNo.Text}%'";
            cm = new MySqlCommand(q2, con);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {
                    rTspnt.Text = dr.GetString(4);

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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                UpdatedBlnc.Text = (float.Parse(UpdatedBlnc.Text) + float.Parse(UpBlnc.Text)).ToString();
                rTblnc.Text = UpdatedBlnc.Text;
            }
        }

        private void rSave_Click(object sender, EventArgs e)
        {
            

            //calculation +/- balance
            rBalance.Text = (float.Parse(rTblnc.Text) - float.Parse(rTspnt.Text)).ToString();

            //Update the ac receivable 
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"update final_rec_record set ReceivableBalance  = '{rBalance.Text}' where PartyName like '%{rPn.Text}%' and OrderNo like '%{rOn.Text}%'";

            int x = cmd.ExecuteNonQuery();

            if (x > 0)
            {
                MessageBox.Show("Alhamdulillah!! Saved Successfully!");
            }
            else
            {
                MessageBox.Show("there are some Errors");
            }
            con.Close();

            con.Open();
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"insert into payment_record(PartyName, OrderNo,PaymentAmt,paymentNo,Remarks, Date) values" +
                    $"('{rPn.Text}','{rOn.Text}','{UpBlnc.Text}','{payNo.Text}','{rRemarks.Text}', @a)";
                cmd.Parameters.Add("@a", MySqlDbType.Date).Value = rDate.Value.Date;
                int y = cmd.ExecuteNonQuery();

                if (y > 0)
                {
                    // MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("there are some Errors");
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors: " + ex);
            }
            con.Close();

            try
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"{$"update final_rec_record set Balance = '"}{UpdatedBlnc.Text}' where PartyName like '%{rSpName.Text}%' and OrderNo like '%{rSoNo.Text}%' and InvoiceNo like '%{rSiNo.Text}%';";
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
            catch (Exception a)
            {
                MessageBox.Show("Errors: " + a);
            }
          
            con.Close();
            /* *********************************************************Receipt page ends***************************************************************** */
        }


        /* *********************************************************Invoice page starts***************************************************************** */
        private void iQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                iTotal.Text = (float.Parse(iuPrice.Text) * float.Parse(iQty.Text)).ToString(); // total = qty X unit price
            }
            catch (Exception x)
            {
                // MessageBox.Show("Errors: " + x);
            }
        }
        private void invSave_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"insert into invoice_enrty(Dept,PartyName, OrderNo,InvoiceNo,ItemName,ItemType,UnitPrice,inQty, Unit,upBalance, Remarks, Date) values" +
                    $"('{iDept.Text}','{ipartyName.Text}','{inorderNo.Text}','{iiNo.Text}','{iprName.Text}','{iType.Text}','{iuPrice.Text}','{iQty.Text}','{iUnit.Text}','{iTotal.Text}','{iRemarks.Text}', @a);" +
                    $" insert into final_inv_record(PartyName,OrderNo,Total,Date) values " +
                    $"('{ipartyName.Text}','{inorderNo.Text}','{iTotal.Text}', @a)";
                cmd.Parameters.Add("@a", MySqlDbType.Date).Value = iDate.Value.Date;

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
                addP.Enabled = true;
                addNew.Enabled = true;
                invHidePnl01.SendToBack();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors: " + ex);
            }
        }

        private void addNew_Click(object sender, EventArgs e)
        {
            insertionDn.Checked = false;
            invSave.Enabled = false;
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string q1 = $"select * from invoice_enrty order by Serial desc limit 1";
            MySqlCommand cm = new MySqlCommand(q1, con);
            MySqlDataReader dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {
                    iDept.Text = dr.GetString(2);
                    ipartyName.Text = dr.GetString(3);
                    inorderNo.Text = dr.GetString(4);
                    iiNo.Text = dr.GetString(5);
                    iprName.Text = "";
                    iType.Text = "";
                    iuPrice.Text = "";
                    iQty.Text = "";
                    iUnit.Text = "";
                    iTotal.Text = "";
                    iRemarks.Text = "";
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
            string q2 = $"select final_inv_record.Total from final_inv_record order by Serial desc limit 1";
            cm = new MySqlCommand(q2, con);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {
                    iStatus.Text = dr.GetString(0);
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

        private void addP_Click(object sender, EventArgs e)
        {
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"update final_inv_record set Total = '{iStatus.Text}' where PartyName like '%{ipartyName.Text}%' and OrderNo like '%{inorderNo.Text}%'";

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

            con.Open();
            try
            {


                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"insert into invoice_enrty(Dept,PartyName, OrderNo,InvoiceNo,ItemName,ItemType,UnitPrice,inQty, Unit,upBalance, Remarks, Date) values" +
                    $"('{iDept.Text}','{ipartyName.Text}','{inorderNo.Text}','{iiNo.Text}','{iprName.Text}','{iType.Text}','{iuPrice.Text}','{iQty.Text}','{iUnit.Text}','{iTotal.Text}','{iRemarks.Text}', @a)";
                cmd.Parameters.Add("@a", MySqlDbType.Date).Value = iDate.Value.Date;

                int y = cmd.ExecuteNonQuery();

                if (y > 0)
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("there are some Errors");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors: " + ex);
            }
            con.Close();
        }

        private void insertionDn_CheckedChanged(object sender, EventArgs e)
        {
            if (insertionDn.Checked)
            {
                iStatus.Text = (float.Parse(iStatus.Text) + float.Parse(iTotal.Text)).ToString();

            }
        }



        private void inSearch_Click(object sender, EventArgs e)
        {
            

            con.Open();
            string q2 = $"select * from final_inv_record where PartyName like '%{ipName.Text}%' and OrderNo like '%{inoNo.Text}%'";
            MySqlCommand cm = new MySqlCommand(q2, con);
            MySqlDataReader dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {
                    MessageBox.Show("SubhhanAllah !!! The order Exists!! :)");
                    ipartyName.Text = dr.GetString(2);
                    inorderNo.Text = dr.GetString(3);
                    invSave.Enabled = false;
                    addP.Enabled = true;
                    invHidePnl01.SendToBack();
                }
                catch (Exception a)
                {
                    MessageBox.Show("Errors: " + a);
                }

            }
            else
            {
                MessageBox.Show("You are making the invoice for the first time for this order.");
                invSave.Enabled = true;
                addP.Enabled = false;
            }
            con.Close();
        }

        private void btnSaveDtl_Click(object sender, EventArgs e)
        {
            con.Open();
            string q1 = $"select * from final_rec_record where PartyName like '%{ipartyName.Text}%' and OrderNo like '%{inorderNo.Text}%'";
            MySqlCommand cm = new MySqlCommand(q1, con);
            MySqlDataReader dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {
                    invPname.Text = dr.GetString(2);
                    invOno.Text = dr.GetString(3);
                    orderTotal.Text = dr.GetString(7);
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

            con.Open();
            string q2 = $"select * from final_inv_record where PartyName like '%{ipartyName.Text}%' and OrderNo like '%{inorderNo.Text}%'";
            cm = new MySqlCommand(q2, con);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                try
                {
                    TotalSpent.Text = dr.GetString(4);

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

            //Calculate +/-balance
            Balance.Text = (float.Parse(orderTotal.Text) - float.Parse(TotalSpent.Text)).ToString();
           
            //Update the ac receivable 
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"update final_rec_record set ReceivableBalance  = '{Balance.Text}' where PartyName like '%{ipartyName.Text}%' and OrderNo like '%{inorderNo.Text}%'";

            int x = cmd.ExecuteNonQuery();

            if (x > 0)
            {
                MessageBox.Show("Alhamdulillah!! Saved Successfully!");
            }
            else
            {
                MessageBox.Show("there are some Errors");
            }
            con.Close();

        }

        private void chkRegular_CheckedChanged(object sender, EventArgs e)
        {
            if(chkRegular.Checked)
            {
                invHidePnl.SendToBack();
                inSearch.Enabled = true;
                invSave.Enabled = false;
                addP.Enabled = false;
                chkExcep.Checked = false;
            }
        }

        private void chkExcep_CheckedChanged(object sender, EventArgs e)
        {
            if(chkExcep.Checked)
            {
                inSearch.Enabled = false;
                chkRegular.Checked = false;
                invSave.Enabled = true;
                invHidePnl.BringToFront();
            }
        }
    }

   }