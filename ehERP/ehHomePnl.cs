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
    public partial class ehHomePnl : UserControl
    {
        public ehHomePnl()
        {
            InitializeComponent();
        }

        private void refreahBtn_Click(object sender, EventArgs e)
        {
            string query = @"select PartyName, OrderNo, ReceivableBalance from final_rec_record where AccountStatus = @acStatus ";
            using (MySqlConnection conn = new MySqlConnection(@"datasource= localhost; port = 3306; database= eh_db; username= root; password=;"))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@acStatus", "Neg");


                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                        metroGrid1.DataSource = dt;
                        conn.Close();
                    }
                    catch (MySqlException)
                    {
                        throw;
                    }
                }
            }
        }
        
 
    }
}
