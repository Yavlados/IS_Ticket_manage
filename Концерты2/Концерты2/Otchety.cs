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
    public partial class Otchety : Form
    {
        public Otchety()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "drop view kFS";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "create view kFS as select fioartist, namejanr, namevist, datavist, timevist, adreskp from dbo.ARTIST left join JANR on idjartist=idjanr left join DOGOVOR on innartdogovor=innartist left join  VISTUPLENIE on idvist=idvistupdogovor left join KP on idkpvist = idkp ";

            cmd.ExecuteNonQuery();

            cmd.CommandText = "select * from kFS";
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            while (rdr.Read())
            {

                dataGridView1.ColumnCount = 6;
                dataGridView1.Rows.Add();

                dataGridView1[0, i].Value = rdr["fioartist"].ToString();
                dataGridView1[1, i].Value = rdr["namejanr"].ToString();
                dataGridView1[2, i].Value = rdr["namevist"].ToString();
                dataGridView1[3, i].Value = rdr["datavist"].ToString();
                dataGridView1[4, i].Value = rdr["timevist"].ToString();
                dataGridView1[5, i].Value = rdr["adreskp"].ToString();
                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "drop view kpotchet";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "create view kpotchet as select typekp, graphickp, adreskp, namevist, datavist, timevist, namecountry, namegcountry from KP left join VISTUPLENIE on idkpvist=idkp left join COUNTRY on  idgkp=idgcountry	";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "select * from kpotchet";
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            while (rdr.Read())
            {

                dataGridView1.ColumnCount = 8;
                dataGridView1.Rows.Add();

                dataGridView1[0, i].Value = rdr["typekp"].ToString();
                dataGridView1[1, i].Value = rdr["graphickp"].ToString();
                dataGridView1[2, i].Value = rdr["adreskp"].ToString();
                dataGridView1[3, i].Value = rdr["namevist"].ToString();
                dataGridView1[4, i].Value = rdr["datavist"].ToString();
                dataGridView1[5, i].Value = rdr["timevist"].ToString();
                dataGridView1[6, i].Value = rdr["namecountry"].ToString();
                dataGridView1[7, i].Value = rdr["namegcountry"].ToString();
                i++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "drop view biletotchet";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "create view biletotchet as select typebil, ostbil, namevist, datavist,	timevist, adreskp, namegcountry, namecountry, pricebil  from BILET  left join   VISTUPLENIE on idvistbilet=idvist  left join  KP on idkpvist = idkp  left join  COUNTRY on idgkp = idgcountry"; 
            cmd.ExecuteNonQuery();

            cmd.CommandText = "select * from biletotchet";
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            while (rdr.Read())
            {

                dataGridView1.ColumnCount = 9;
                dataGridView1.Rows.Add();

                dataGridView1[0, i].Value = rdr["typebil"].ToString();
                dataGridView1[1, i].Value = rdr["ostbil"].ToString();
                dataGridView1[2, i].Value = rdr["namevist"].ToString();
                dataGridView1[3, i].Value = rdr["datavist"].ToString();
                dataGridView1[4, i].Value = rdr["timevist"].ToString();
                dataGridView1[5, i].Value = rdr["adreskp"].ToString();
                dataGridView1[6, i].Value = rdr["namegcountry"].ToString();
                dataGridView1[7, i].Value = rdr["namecountry"].ToString();
                dataGridView1[8, i].Value = rdr["pricebil"].ToString();
                i++;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "drop view countryotchet";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "create view  countryotchet as select namecountry, namegcountry, typekp, adreskp, namevist, datavist, timevist  from COUNTRY  left join KP on idgcountry=idgkp left join VISTUPLENIE on idkpvist=idkp";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "select * from countryotchet";
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            while (rdr.Read())
            {

                dataGridView1.ColumnCount = 7;
                dataGridView1.Rows.Add();

                dataGridView1[0, i].Value = rdr["namecountry"].ToString();
                dataGridView1[1, i].Value = rdr["namegcountry"].ToString();
                dataGridView1[2, i].Value = rdr["typekp"].ToString();
                dataGridView1[3, i].Value = rdr["adreskp"].ToString();
                dataGridView1[4, i].Value = rdr["namevist"].ToString();
                dataGridView1[5, i].Value = rdr["datavist"].ToString();
                dataGridView1[6, i].Value = rdr["timevist"].ToString();
                i++;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            SqlConnection con = new SqlConnection(db.connectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "drop view managerotchet";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "create view managerotchet as select fiomanager, fioartist, namejanr  from MANAGER  left join  ARTIST on innmanager=innmartist  left join JANR on idjanr=idjartist";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "select * from managerotchet";
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            while (rdr.Read())
            {

                dataGridView1.ColumnCount = 3;
                dataGridView1.Rows.Add();

                dataGridView1[0, i].Value = rdr["fiomanager"].ToString();
                dataGridView1[1, i].Value = rdr["fioartist"].ToString();
                dataGridView1[2, i].Value = rdr["namejanr"].ToString();
                i++;
            }
        }
    }
}
