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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)//Редактирование жанров
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }
        private void button2_Click(object sender, EventArgs e)//Редактирование менеджеров
        {
            Form5 frm5 = new Form5();
            frm5.Show();
        }

        private void button3_Click(object sender, EventArgs e)//редактирование актеров
        {
            Form6 frm6 = new Form6();
            frm6.Show();
        }
    }
}
