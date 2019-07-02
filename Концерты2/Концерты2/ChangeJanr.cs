using System;//ТАБЛИЦА ИЗМЕНЕНИЯ ЖАНРОВ
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
    public partial class ChangeJanr : Form
    {
        string s;
       
        public ChangeJanr()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)//показать таблицу 
        {
            refreshBase();
}
        void refreshBase()
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            var con = new SqlConnection(db.connectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from JANR";
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            dataGridView1.Refresh();
            
            while (rdr.Read())
            {
                dataGridView1.ColumnCount = 2;
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = rdr["idjanr"].ToString();
                dataGridView1[1, i].Value = rdr["namejanr"].ToString();
                i++;
            }
            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)//добавить жанр
        {

            var namejanr = textBox2.Text;

            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd1 = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into JANR values (@namejanr)";

            cmd.Parameters.Add("@namejanr", namejanr);
            cmd.ExecuteNonQuery();
            dataGridView1.Refresh();
            refreshBase();
            con.Close();

        }
    
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.SelectedCells.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedCells[0].Value) && dataGridView1.SelectedCells[0].Value != null)
            {
                textBox1.Text = dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
                s = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
            }

            if (dataGridView1.SelectedRows.Count > 0 && !DBNull.Value.Equals(dataGridView1.SelectedRows[0].Cells[1].Value) && dataGridView1.SelectedRows[0].Cells[1].Value != null)
            {
                textBox1.Text = dataGridView1.SelectedRows[1].Cells[0].Value.ToString();
                 s = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e) //удаление строки
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from JANR where idjanr ='" + s + "'";
            cmd.ExecuteNonQuery();
            //обновление таблицы
            refreshBase();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand fb1 = new SqlCommand("update JANR set namejanr = '" + textBox1.Text + "'where idjanr = '" + s +"'", con);

            try
            {
                con.Open();
                fb1.ExecuteNonQuery();           
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
        
    }//обновить таблицу
    }
