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
    public partial class MenuMANAGER : Form
    {
        public MenuMANAGER()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)//Редактирование жанров
        {
            ChangeJanr frm3 = new ChangeJanr();
            frm3.Show();
        }
        private void button2_Click(object sender, EventArgs e)//Редактирование менеджеров
        {
            ChangeManager frm5 = new ChangeManager();
            frm5.Show();
        }

        private void button3_Click(object sender, EventArgs e)//редактирование актеров
        {
            ChangeArtist frm6 = new ChangeArtist();
            frm6.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeCity frm7 = new ChangeCity();
            frm7.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeKP frm8 = new ChangeKP();
            frm8.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeDogovor frm9 = new ChangeDogovor();
            frm9.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeVistuplenie frm10 = new ChangeVistuplenie();
            frm10.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChangeBilet frm11 = new ChangeBilet();
            frm11.Show();
        }
    }
}
