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
    public partial class ChangeDogovor : Form
    {
        string s;
        public ChangeDogovor()
        {
            InitializeComponent();
            FillCombo();
        }
        void FillCombo()
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select fioartist from ARTIST", con);
            SqlCommand fb2 = new SqlCommand("select namevist from VISTUPLENIE", con);
            SqlDataReader dr;
            try
            {
                con.Open();
                dr = fb1.ExecuteReader();
                while (dr.Read())
                {
                    string team = dr.GetString(0);
                    comboBox1.Items.Add(team);
                    comboBox4.Items.Add(team);
                }
                dr.Close();
                dr = fb2.ExecuteReader();
                while (dr.Read())
                {
                    string team = dr.GetString(0);
                    comboBox2.Items.Add(team);
                    comboBox3.Items.Add(team);
                    //comboBox1.Text = team;
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
            cmd.CommandText = "select * from  DOGOVOR";
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            dataGridView1.Refresh();

            while (rdr.Read())
            {
                dataGridView1.ColumnCount = 4;
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = rdr["iddogovor"].ToString();
                dataGridView1[1, i].Value = rdr["innartdogovor"].ToString();
                dataGridView1[2, i].Value = rdr["idvistupdogovor"].ToString();
                dataGridView1[3, i].Value = rdr["gonorardogovor"].ToString();
               
                i++;
            }
            con.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var innartdogovor = Convert.ToInt16(textBox1.Text);
            var idvistupdogovor = Convert.ToInt16(textBox2.Text);
            var gonorardogovor = Convert.ToInt32(textBox3.Text);

            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into DOGOVOR values (@innartdogovor, @idvistupdogovor, @gonorardogovor)";

            cmd.Parameters.Add("@innartdogovor", innartdogovor);
            cmd.Parameters.Add("@idvistupdogovor", idvistupdogovor);
            cmd.Parameters.Add("@gonorardogovor", gonorardogovor);
            dataGridView1.Refresh();
            cmd.ExecuteNonQuery();
            refreshBase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshBase();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from DOGOVOR where iddogovor ='" + s + "'";
            cmd.ExecuteNonQuery();
            refreshBase();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select innartist from ARTIST where fioartist = '" + comboBox1.Text + "'", con);
            try
            {
                con.Open();
                fb1.ExecuteNonQuery();
                textBox1.Text = Convert.ToString(fb1.ExecuteScalar());


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
            SqlCommand fb1 = new SqlCommand("select idvist from VISTUPLENIE where namevist = '" + comboBox2.Text + "'", con);
            try
            {
                con.Open();
                fb1.ExecuteNonQuery();
                textBox2.Text = Convert.ToString(fb1.ExecuteScalar());
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

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select innartist from ARTIST where fioartist = '" + comboBox4.Text + "'", con);
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select idvist from VISTUPLENIE where namevist = '" + comboBox3.Text + "'", con);
            try
            {
                con.Open();
                fb1.ExecuteNonQuery();
                textBox5.Text = Convert.ToString(fb1.ExecuteScalar());
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedCells[0].Value) && dataGridView1.SelectedCells[0].Value != null)
            {
                textBox6.Text = dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox5.Text = dataGridView1[2, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox4.Text = dataGridView1[3, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                s = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
            }

            if (dataGridView1.SelectedRows.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedRows[0].Cells[1].Value) && dataGridView1.SelectedRows[0].Cells[5].Value != null)
            {
                textBox6.Text = dataGridView1.SelectedRows[1].Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[2].Cells[0].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[3].Cells[0].Value.ToString();
                s = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("update DOGOVOR set innartdogovor = '" + textBox6.Text + "'where iddogovor = '" + s + "'", con);
            SqlCommand fb2 = new SqlCommand("update DOGOVOR set idvistupdogovor = '" + textBox5.Text + "'where iddogovor = '" + s + "'", con);
            SqlCommand fb3 = new SqlCommand("update DOGOVOR set gonorardogovor = '" + textBox4.Text + "'where iddogovor = '" + s + "'", con);
            try
            {
                con.Open();
                fb1.ExecuteNonQuery();
                fb2.ExecuteNonQuery();
                fb3.ExecuteNonQuery();
             
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
