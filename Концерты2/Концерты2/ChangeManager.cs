using System;//ТАБЛИЦА ИЗМЕНЕНИЯ МЕНЕДЖЕРОВ
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
    public partial class ChangeManager : Form
    {
        string s;
        public ChangeManager()
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
            cmd.CommandText = "select * from MANAGER";
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            dataGridView1.Refresh();
            while (rdr.Read())
            {
                dataGridView1.ColumnCount = 2;
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = rdr["innmanager"].ToString();
                dataGridView1[1, i].Value = rdr["fiomanager"].ToString();
                i++;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            refreshBase();
        }
        private void button2_Click(object sender, EventArgs e) //удаление строки
        {
            var d = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from MANAGER where innmanager ='" + d + "'";
            cmd.ExecuteNonQuery();
            refreshBase();

        }
        private void button3_Click(object sender, EventArgs e)
        
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("update MANAGER set innmanager  = '" + textBox3.Text + "'where innmanager = '" + s + "'", con);
            SqlCommand fb2 = new SqlCommand("update MANAGER set fiomanager  = '" + textBox4.Text + "'where innmanager = '" + s + "'", con);
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
 
    }//редактировать

        private void button4_Click(object sender, EventArgs e)//добавить жанр
        {
            var innmanager = Convert.ToInt16(textBox1.Text);
            var fiomanager = textBox2.Text;

            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into MANAGER values (@innmanager, @fiomanager)";

            cmd.Parameters.Add("@innmanager", innmanager);
            cmd.Parameters.Add("@fiomanager", fiomanager);

            dataGridView1.Refresh();
            cmd.ExecuteNonQuery();
            refreshBase();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.SelectedCells.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedCells[0].Value) && dataGridView1.SelectedCells[0].Value != null)
            {
                textBox3.Text = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                textBox4.Text = dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                s = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
            }

            if (dataGridView1.SelectedRows.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedRows[0].Cells[1].Value) && dataGridView1.SelectedRows[0].Cells[0].Value != null)
            {
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                s = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }

        }

        private void ChangeManager_Load(object sender, EventArgs e)
        {

        }
    }
}
