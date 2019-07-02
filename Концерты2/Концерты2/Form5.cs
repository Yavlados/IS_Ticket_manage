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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
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

        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();
        }//обновить таблицу

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
        }

    }
}
