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
    public partial class ZaprosARTIST : Form
    {
        public ZaprosARTIST()
        {
            InitializeComponent();
            FillCombo();
        }
        void FillCombo()
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select namevist from VISTUPLENIE", con);
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
            cmd.CommandText = "drop view poiskart";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "create view poiskart as	 select fioartist, namevist, datavist, timevist, namecountry, namegcountry, adreskp  from ARTIST  left join DOGOVOR on innartdogovor=innartist left join VISTUPLENIE on idvistupdogovor = idvist left join  KP on idkp=idkpvist left join  COUNTRY on idgkp = idgcountry  where namevist = '" + comboBox1.Text + "'";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "select * from poiskart ";
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            while (rdr.Read())
            {

                dataGridView1.ColumnCount = 7;
                dataGridView1.Rows.Add();

                dataGridView1[0, i].Value = rdr["fioartist"].ToString();
                dataGridView1[1, i].Value = rdr["namevist"].ToString();
                dataGridView1[2, i].Value = rdr["datavist"].ToString();
                dataGridView1[3, i].Value = rdr["timevist"].ToString();
                dataGridView1[4, i].Value = rdr["namecountry"].ToString();
                dataGridView1[5, i].Value = rdr["namegcountry"].ToString();
                dataGridView1[6, i].Value = rdr["adreskp"].ToString();
                i++;
            }
        }
    }
}
