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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "Manager") & (textBox2.Text == "111"))
            {
                Form2 frm2 = new Form2();
                frm2.Show();
            }
            if ((textBox1.Text == "User") & (textBox2.Text == "111"))
            {
                Form4 frm4  = new Form4();
                frm4.Show();
            }
        }
    }
}
