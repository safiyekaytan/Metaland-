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
    public partial class Form8 : Form
    {
        public List<Label> labeller = new List<Label>();
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            
            labeller.Add(l1); labeller.Add(l2); labeller.Add(l3); labeller.Add(l4); labeller.Add(l5); labeller.Add(l6); labeller.Add(l7);
            labeller.Add(l8); labeller.Add(l9); labeller.Add(l10); labeller.Add(l11); labeller.Add(l12); labeller.Add(l13); labeller.Add(l14);
            labeller.Add(l15); labeller.Add(l16); labeller.Add(l17); labeller.Add(l18); labeller.Add(l19); labeller.Add(l20); labeller.Add(l21);
            labeller.Add(l22); labeller.Add(l23); labeller.Add(l24); labeller.Add(l25); labeller.Add(l26); labeller.Add(l27); labeller.Add(l28);
            labeller.Add(label1);
            labeller.Add(label2);
            labeller.Add(label3);
            labeller.Add(label4);
        }
        

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String girilen = textBox1.Text;
            if(girilen != "12345")
            {
                button1.BackColor = Color.Red;
                foreach (var item in labeller)
                {
                    item.BackColor = Color.Red;
                }
            }
            else if(girilen == "12345")
            {
                button1.BackColor = Color.Green;
                foreach (var item in labeller)
                {
                    item.BackColor = Color.Green;
                }
                MessageBox.Show("Giris Yapiliyor");
                Form3 form3 = new Form3();
                form3.Show();
                Hide();
            }
        }
    }
}
