using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Концерты2
{
    public partial class ZaprosVCOUNTRY : Form
    {
        string s;
      
        public ZaprosVCOUNTRY()
        {
            InitializeComponent();
            FillCombo();
        }
        void FillCombo()
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select namecountry from COUNTRY group by namecountry", con);
       
            SqlDataReader dr;
            try
            {
                con.Open();
                dr = fb1.ExecuteReader();
                while (dr.Read())
                {
                    string team = dr.GetString(0);
                    comboBox1.Items.Add(team);
                }
                dr.Close(); 
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb3 = new SqlCommand("select namegcountry from COUNTRY where namecountry = '" + comboBox1.Text + "'", con);
            SqlDataReader dr;
            try
            {
                con.Open();
                dr = fb3.ExecuteReader();
                while (dr.Read())
                {
                    string team = dr.GetString(0);
                    comboBox2.Items.Add(team);
                }
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "drop view Poiskpogorodu";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "create view Poiskpogorodu as select namecountry, namegcountry, namevist, datavist, timevist, adreskp from COUNTRY left join KP on idgcountry = idgkp  left join  VISTUPLENIE on idkpvist=idkp  where namecountry = '" + comboBox1.Text+"'  and namegcountry = '"+comboBox2.Text+"'";

            cmd.ExecuteNonQuery();

            cmd.CommandText = "select * from Poiskpogorodu ";
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            while (rdr.Read())
            {

                dataGridView1.ColumnCount = 6;
                dataGridView1.Rows.Add();

                dataGridView1[0, i].Value = rdr["namecountry"].ToString();
                dataGridView1[1, i].Value = rdr["namegcountry"].ToString();
                dataGridView1[2, i].Value = rdr["namevist"].ToString();
                dataGridView1[3, i].Value = rdr["datavist"].ToString();
                dataGridView1[4, i].Value = rdr["timevist"].ToString();
                dataGridView1[5, i].Value = rdr["adreskp"].ToString();
                i++;
            }
        }
    }
}
