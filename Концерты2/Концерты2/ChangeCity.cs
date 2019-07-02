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
    public partial class ChangeCity : Form
    {
        string s;
        public ChangeCity()
        {
            InitializeComponent();
        }
        void refreshBase()
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            var con = new SqlConnection(db.connectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from COUNTRY";
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            dataGridView1.Refresh();

            while (rdr.Read())
            {
                dataGridView1.ColumnCount = 3;
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = rdr["idgcountry"].ToString();
                dataGridView1[1, i].Value = rdr["namegcountry"].ToString();
                dataGridView1[2, i].Value = rdr["namecountry"].ToString();
                i++;
            }
            con.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {

            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("update COUNTRY set namegcountry = '" + textBox3.Text + "'where idgcountry = '" + s + "'", con);
            SqlCommand fb2 = new SqlCommand("update COUNTRY set namecountry = '" + textBox4.Text + "'where idgcountry = '" + s + "'", con);
            try
            {
                con.Open();
                fb1.ExecuteNonQuery();
                fb2.ExecuteNonQuery();
              
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.SelectedCells.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedCells[0].Value) && dataGridView1.SelectedCells[0].Value != null)
            {
                textBox3.Text = dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox4.Text = dataGridView1[2, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                s = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
            }

            if (dataGridView1.SelectedRows.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedRows[0].Cells[1].Value) && dataGridView1.SelectedRows[0].Cells[2].Value != null)
            {
                textBox3.Text = dataGridView1.SelectedRows[1].Cells[0].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[2].Cells[0].Value.ToString();
                s = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)//показвть таблицу
        {
            refreshBase();
        }

        private void button3_Click(object sender, EventArgs e)
       
            {
                var namegcountry = textBox1.Text;
                var namecountry = textBox2.Text;

              DB db = new DB();
                SqlConnection con = new SqlConnection(db.connectionstring);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "insert into COUNTRY values (@namegcountry, @namecountry)";

                cmd.Parameters.Add("@namegcountry", namegcountry);
                cmd.Parameters.Add("@namecountry", namecountry);
        
                dataGridView1.Refresh();
                cmd.ExecuteNonQuery();
                refreshBase();
            }

        private void button2_Click(object sender, EventArgs e)//удалить
        {
          
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from COUNTRY where idgcounty ='" + s + "'";
            cmd.ExecuteNonQuery();
            refreshBase();
        }
    }
}
