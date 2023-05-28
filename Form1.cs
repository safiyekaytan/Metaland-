using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {/*

        public Form1()
        {
            InitializeComponent();
        }
      /*  void veriGetir()
        {
            dt = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM mazeland.oyuntablo", conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
      */
        private void Form1_Load(object sender, EventArgs e)
        {
          //  veriGetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            String sql = "INSERT INTO oyuntablo(oyunAlanEn, oyunAlanBoy)" +
                "VALUES (@oyunAlanEn, @oyunAlanBoy)";
            cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@alanTur", textBox2.Text);
            cmd.Parameters.AddWithValue("@alanSahip", textBox4.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            veriGetir();
            MessageBox.Show("kayit eklendi");
            */
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            Hide();
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
