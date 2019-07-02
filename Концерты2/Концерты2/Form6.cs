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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)//вызов всех таблиц
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
            rdr.Close();
            cmd.CommandText = "select * from JANR";
            SqlDataReader ror = cmd.ExecuteReader();
            i = 0;
            dataGridView3.Refresh();
            while (ror.Read())
            {
                dataGridView3.ColumnCount = 2;
                dataGridView3.Rows.Add();
                dataGridView3[0, i].Value = ror["idjanr"].ToString();
                dataGridView3[1, i].Value = ror["namejanr"].ToString();
                i++;
            }
            ror.Close();
            cmd.CommandText = "select * from ARTIST";
            SqlDataReader rar = cmd.ExecuteReader();
            i = 0;
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

        private void button2_Click(object sender, EventArgs e) //удаление строки
        {
            var d = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from ARTIST where innartist ='" + d + "'";
            cmd.ExecuteNonQuery();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.Refresh();
            dataGridView2.Rows.Clear();
        }//обновить таблицу
        private void button4_Click(object sender, EventArgs e)
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
        }

       
    }
}
