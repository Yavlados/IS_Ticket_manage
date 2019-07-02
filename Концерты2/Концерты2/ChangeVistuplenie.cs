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
    public partial class ChangeVistuplenie : Form
    {
        string s;
        public ChangeVistuplenie()
        {
            InitializeComponent();
            FillCombo();
        }
        void FillCombo()
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select adreskp from KP", con);

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
        void refreshBase()
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            var con = new SqlConnection(db.connectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from VISTUPLENIE";
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            dataGridView1.Refresh();

            while (rdr.Read())
            {
                dataGridView1.ColumnCount = 5;
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = rdr["idvist"].ToString();
                dataGridView1[1, i].Value = rdr["namevist"].ToString();
                dataGridView1[2, i].Value = rdr["datavist"].ToString();
                dataGridView1[3, i].Value = rdr["timevist"].ToString();
                dataGridView1[4, i].Value = rdr["idkpvist"].ToString();
                i++;
            }
            con.Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedCells[0].Value) && dataGridView1.SelectedCells[0].Value != null)
            {
                textBox1.Text = dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox2.Text = dataGridView1[2, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox3.Text = dataGridView1[3, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox4.Text = dataGridView1[4, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                s = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
            }

            if (dataGridView1.SelectedRows.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedRows[0].Cells[1].Value) && dataGridView1.SelectedRows[0].Cells[5].Value != null)
            {
                textBox1.Text = dataGridView1.SelectedRows[1].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[2].Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[3].Cells[0].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[4].Cells[0].Value.ToString();
                s = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select idkp from KP where adreskp = '" + comboBox2.Text + "'", con);
            try
            {
                con.Open();
                fb1.ExecuteNonQuery();
                textBox7.Text = Convert.ToString(fb1.ExecuteScalar());
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
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select idkp from KP where adreskp = '" + comboBox1.Text + "'", con);
            try
            {
                con.Open();
                fb1.ExecuteNonQuery();
                textBox4.Text = Convert.ToString(fb1.ExecuteScalar());
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
            cmd.CommandText = "delete from VISTUPLENIE where idvist ='" + s + "'";
            cmd.ExecuteNonQuery();
            refreshBase();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var namevist = textBox5.Text;
            var datavist = textBox8.Text;
            var timevist = textBox6.Text;
            var idkpvist = Convert.ToInt16(textBox7.Text);

            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into VISTUPLENIE values (@namevist, @datavist, @timevist, @idkpvist )";

            cmd.Parameters.Add("@namevist", namevist);
            cmd.Parameters.Add("@datavist", datavist);
            cmd.Parameters.Add("@timevist", timevist);
            cmd.Parameters.Add("@idkpvist", idkpvist);

            dataGridView1.Refresh();
            cmd.ExecuteNonQuery();
            refreshBase();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("update VISTUPLENIE set namevist = '" + textBox1.Text + "'where idvist = '" + s + "'", con);
            SqlCommand fb2 = new SqlCommand("update VISTUPLENIE set datavist = '" + textBox2.Text + "'where idvist = '" + s + "'", con);
            SqlCommand fb3 = new SqlCommand("update VISTUPLENIE set timevist = '" + textBox3.Text + "'where idvist = '" + s + "'", con);
            SqlCommand fb4 = new SqlCommand("update VISTUPLENIE set idkpvist = '" + textBox4.Text + "'where idvist = '" + s + "'", con);
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
    }
    }
