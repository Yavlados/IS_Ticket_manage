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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)//показать таблицу 
        {
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
}

        private void button2_Click(object sender, EventArgs e)//добавить жанр
        {
            var idjanr = Convert.ToInt16(textBox1.Text);
            var namejanr = textBox2.Text;

            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into JANR values (@idjanr, @namejanr)";

            cmd.Parameters.Add("@idjanr", idjanr);
            cmd.Parameters.Add("@namejanr", namejanr);
            
            dataGridView1.Refresh();
            cmd.ExecuteNonQuery();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e) //удаление строки
        {
            var s = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from JANR where idjanr ='" + s + "'";
            cmd.ExecuteNonQuery();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            dataGridView1.Rows.Clear();
        }//обновить таблицу
    }
}
