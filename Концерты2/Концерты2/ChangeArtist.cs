using System;//ТАБЛИЦА ИЗМЕНЕНИЯ АКТЕРОВ
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
    public partial class ChangeArtist : Form
    {
        string s;
        public ChangeArtist()
        {
            InitializeComponent();
            FillCombo();
        }
        void refreshBase()
        {
            dataGridView2.Rows.Clear();
            DB db = new DB();
            var con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from ARTIST";
            SqlDataReader rar = cmd.ExecuteReader();
            int i = 0;
            dataGridView2.Refresh();
            while (rar.Read())
            {
                dataGridView2.ColumnCount = 4;
                dataGridView2.Rows.Add();
                dataGridView2[0, i].Value = rar["innartist"].ToString();
                dataGridView2[1, i].Value = rar["fioartist"].ToString();
                dataGridView2[2, i].Value = rar["idjartist"].ToString();
                dataGridView2[3, i].Value = rar["innmartist"].ToString();
                i++;
            }
        }
        private void button1_Click(object sender, EventArgs e)//вызов всех таблиц
        {
            refreshBase();
        }

        private void button2_Click(object sender, EventArgs e) //удаление строки
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from ARTIST where innartist ='" + s + "'";
            cmd.ExecuteNonQuery();
            refreshBase();
        }
      
        private void button4_Click(object sender, EventArgs e)//добавить 
        {
            var innartist = Convert.ToInt16(textBox1.Text);
            var fioartist = textBox2.Text;
            var idjartist = Convert.ToInt16(textBox3.Text);
            var innmartist = Convert.ToInt16(textBox4.Text);

            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into ARTIST values (@innartist, @fioartist, @idjartist, @innmartist )";

            cmd.Parameters.Add("@innartist", innartist);
            cmd.Parameters.Add("@fioartist", fioartist);
            cmd.Parameters.Add("@idjartist", idjartist);
            cmd.Parameters.Add("@innmartist", innmartist);

            dataGridView2.Refresh();
            cmd.ExecuteNonQuery();
            refreshBase();
        }
        void FillCombo()
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select fiomanager from MANAGER", con);
            SqlCommand fb2 = new SqlCommand("select namejanr from JANR", con);
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
                    //comboBox1.Text = team;
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
       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select idjanr from JANR where namejanr = '"+ comboBox2.Text+ "'", con);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select innmanager from MANAGER where fiomanager = '" + comboBox1.Text + "'", con);
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0 && !DBNull.Value.Equals(dataGridView2.SelectedCells[0].Value) && dataGridView2.SelectedCells[0].Value != null)
            {
                textBox5.Text = dataGridView2[0, dataGridView2.SelectedCells[0].RowIndex].Value.ToString();
                textBox6.Text = dataGridView2[1, dataGridView2.SelectedCells[0].RowIndex].Value.ToString();
                textBox7.Text = dataGridView2[2, dataGridView2.SelectedCells[0].RowIndex].Value.ToString();
                textBox8.Text = dataGridView2[3, dataGridView2.SelectedCells[0].RowIndex].Value.ToString();
                s = dataGridView2[0, dataGridView2.SelectedCells[0].RowIndex].Value.ToString();
            }

            if (dataGridView2.SelectedRows.Count > 0 && !DBNull.Value.Equals(dataGridView2.SelectedRows[0].Cells[1].Value) && dataGridView2.SelectedRows[0].Cells[4].Value != null)
            {
                textBox5.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                textBox6.Text = dataGridView2.SelectedRows[1].Cells[0].Value.ToString();
                textBox7.Text = dataGridView2.SelectedRows[2].Cells[0].Value.ToString();
                textBox8.Text = dataGridView2.SelectedRows[3].Cells[0].Value.ToString();
                s = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
      
            {

                DB db = new DB();
                SqlConnection con = new SqlConnection(db.connectionstring);
                SqlCommand fb1 = new SqlCommand("select idjanr from JANR where namejanr = '" + comboBox3.Text + "'", con);
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

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("select innmanager from MANAGER where fiomanager = '" + comboBox4.Text + "'", con);
            try
            {
                con.Open();
                fb1.ExecuteNonQuery();
                textBox8.Text = Convert.ToString(fb1.ExecuteScalar());

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

        private void button5_Click(object sender, EventArgs e)//редактировать
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("update ARTIST set innartist = '" + textBox5.Text + "'where innartist = '" + s + "'", con);
            SqlCommand fb2 = new SqlCommand("update ARTIST set fioartist = '" + textBox6.Text + "'where innartist = '" + s + "'", con);
            SqlCommand fb3 = new SqlCommand("update ARTIST set idjartist = '" + textBox7.Text + "'where innartist = '" + s + "'", con);
            SqlCommand fb4 = new SqlCommand("update ARTIST set innmartist = '" + textBox8.Text + "'where innartist = '" + s + "'", con);
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

        private void ChangeArtist_Load(object sender, EventArgs e)
        {

        }
    }
    }

