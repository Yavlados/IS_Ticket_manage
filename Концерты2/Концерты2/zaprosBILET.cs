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
    public partial class zaprosBILET : Form
    {
        string s;
        string a;
        public zaprosBILET()
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
            cmd.CommandText = "drop view poiskbil";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "create view poiskbil as select idbil, namevist, typebil, ostbil, pricebil from BILET left join VISTUPLENIE on idvist=idvistbilet where namevist ='" + comboBox1.Text + "'";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "select * from poiskbil ";
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            while (rdr.Read())
            {

                dataGridView1.ColumnCount = 5;
                dataGridView1.Rows.Add();

                dataGridView1[0, i].Value = rdr["idbil"].ToString();
                dataGridView1[1, i].Value = rdr["namevist"].ToString();
                dataGridView1[2, i].Value = rdr["typebil"].ToString();
                dataGridView1[3, i].Value = rdr["ostbil"].ToString();
                dataGridView1[4, i].Value = rdr["pricebil"].ToString();

                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Подтвердите покупку", "Вы уверены?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes) //Если нажал Да
            {
                MessageBox.Show("Спасибо за покупку!");
                dataGridView1.Rows.Clear();
                DB db = new DB();
                SqlConnection con = new SqlConnection(db.connectionstring);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "update BILET set ostbil = '"+s+ "'-1 where idbil ='"+a+"'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "select * from poiskbil ";
                SqlDataReader rdr = cmd.ExecuteReader();
                int i = 0;
                while (rdr.Read())
                {
                    dataGridView1.ColumnCount = 5;
                    dataGridView1.Rows.Add();

                    dataGridView1[0, i].Value = rdr["idbil"].ToString();
                    dataGridView1[1, i].Value = rdr["namevist"].ToString();
                    dataGridView1[2, i].Value = rdr["typebil"].ToString();
                    dataGridView1[3, i].Value = rdr["ostbil"].ToString();
                    dataGridView1[4, i].Value = rdr["pricebil"].ToString();

                    i++;
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedCells[0].Value) && dataGridView1.SelectedCells[0].Value != null)
            {
                textBox1.Text = dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox2.Text = dataGridView1[2, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox3.Text = dataGridView1[4, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                s = dataGridView1[3, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                a = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
            }
            if (dataGridView1.SelectedRows.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedRows[0].Cells[1].Value) && dataGridView1.SelectedRows[0].Cells[5].Value != null)
            {
                textBox1.Text = dataGridView1.SelectedRows[1].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[2].Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[4].Cells[0].Value.ToString();
                s = dataGridView1.SelectedRows[3].Cells[0].Value.ToString();
                a = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void zaprosBILET_Load(object sender, EventArgs e)
        {

        }
    }
}