using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Концерты2
{
    public partial class Autorizacia : Form
    {
        public Autorizacia()
        {
            InitializeComponent();
        }
       

        private void button1_Click(object sender, EventArgs e)
        {      
            if ((textBox1.Text == "Manager") & (textBox2.Text == "1"))
            {
                MenuMANAGER frm2 = new MenuMANAGER();
                frm2.Show();
                this.Hide();
            }
            if ((textBox1.Text == "User") & (textBox2.Text == "1"))
            {
                MenuUSER frm4  = new MenuUSER();
                frm4.Show();
                this.Hide();
            }
        }

        private void Autorizacia_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            ORazrabotchike sxm = new ORazrabotchike();
            sxm.Show();
        }
    }
}
