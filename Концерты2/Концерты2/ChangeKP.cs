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
    public partial class ChangeKP : Form
    {
        string s;
        void refreshBase()
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            var con = new SqlConnection(db.connectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from KP";
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            dataGridView1.Refresh();

            while (rdr.Read())
            {
                dataGridView1.ColumnCount = 5;
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = rdr["idkp"].ToString();
                dataGridView1[1, i].Value = rdr["typekp"].ToString();
                dataGridView1[2, i].Value = rdr["graphickp"].ToString();
                dataGridView1[3, i].Value = rdr["idgkp"].ToString();
                dataGridView1[4, i].Value = rdr["adreskp"].ToString();
                i++;
            }
            con.Close();
        }
        public ChangeKP()
        {
            InitializeComponent();
            FillCombo();
        }
        void FillCombo()
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select namegcountry from COUNTRY", con);
           
            SqlDataReader dr;
            try
            {
                con.Open();
                dr = fb1.ExecuteReader();
                while (dr.Read())
                {
                    string team = dr.GetString(0);
                    comboBox1.Items.Add(team);
                    comboBox2.Items.Add(team);
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedCells[0].Value) && dataGridView1.SelectedCells[0].Value != null)
            {
                textBox4.Text = dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox5.Text = dataGridView1[2, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox6.Text = dataGridView1[3, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox7.Text = dataGridView1[4, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                s = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
            }

            if (dataGridView1.SelectedRows.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedRows[0].Cells[1].Value) && dataGridView1.SelectedRows[0].Cells[5].Value != null)
            {
                textBox4.Text = dataGridView1.SelectedRows[1].Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[2].Cells[0].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[3].Cells[0].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[4].Cells[0].Value.ToString();
                s = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshBase();
        }

        private void button2_Click(object sender, EventArgs e)//УДаление
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from KP where idkp ='" + s + "'";
            cmd.ExecuteNonQuery();
            refreshBase();
        }

        private void button3_Click(object sender, EventArgs e)//добавить
        {
            var typekp =textBox1.Text;
            var graphickp = textBox2.Text;
            var idgkp = Convert.ToInt16(textBox3.Text);
            var adreskp = textBox8.Text;

            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into KP values (@typekp, @graphickp, @idgkp, @adreskp )";

            cmd.Parameters.Add("@typekp", typekp);
            cmd.Parameters.Add("@graphickp", graphickp);
            cmd.Parameters.Add("@idgkp", idgkp);
            cmd.Parameters.Add("@adreskp", adreskp);

            dataGridView1.Refresh();
            cmd.ExecuteNonQuery();
            refreshBase();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select idgcountry from COUNTRY where namegcountry = '" + comboBox1.Text + "'", con);
            try
            {
                con.Open();
                fb1.ExecuteNonQuery();
                textBox3.Text = Convert.ToString(fb1.ExecuteScalar());


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
                DB db = new DB();
                SqlConnection con = new SqlConnection(db.connectionstring);
                SqlCommand fb1 = new SqlCommand("select idgcountry from COUNTRY where namegcountry = '" + comboBox2.Text + "'", con);
                try
                {
                    con.Open();
                    fb1.ExecuteNonQuery();
                    textBox6.Text = Convert.ToString(fb1.ExecuteScalar());
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

        private void button4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("update KP set typekp = '" + textBox4.Text + "'where idkp = '" + s + "'", con);
            SqlCommand fb2 = new SqlCommand("update KP set graphickp = '" + textBox5.Text + "'where idkp = '" + s + "'", con);
            SqlCommand fb3 = new SqlCommand("update KP set idgkp = '" + textBox6.Text + "'where idkp = '" + s + "'", con);
            SqlCommand fb4 = new SqlCommand("update KP set adreskp = '" + textBox7.Text + "'where idkp = '" + s + "'", con);
            try
            {
                con.Open();
                fb1.ExecuteNonQuery();
                fb2.ExecuteNonQuery();
                fb3.ExecuteNonQuery();
                fb4.ExecuteNonQuery();

            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                con.Close();
            }
            refreshBase();
        }

        private void ChangeKP_Load(object sender, EventArgs e)
        {

        }
    }
}
