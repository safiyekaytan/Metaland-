using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            Form8 form8 = new Form8();
            form8.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            Form5 form5 = new Form5();
            form5.Show();
            Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label98_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }
    }
}
