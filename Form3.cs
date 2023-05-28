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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp6
{
    public partial class Form3 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=mazeland;Uid=root;Pwd='Pwd'");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adapter;
        DataTable dt;
        public Form3()
        {
            InitializeComponent();
        }

        void veriGetir()
        {
            dt = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM mazeland.oyuntablo", conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            String sql = "INSERT INTO oyuntablo(oyunAlanEn, oyunAlanBoy, basParaMiktar, basEsyaMiktar, basYiyecekMiktar, gunlukYiyecekGider, gunlukEsyaGider, gunlukParaGider)" +
                "VALUES (@oyunAlanEn, @oyunAlanBoy, @basParaMiktar, @basEsyaMiktar, @basYiyecekMiktar, @gunlukYiyecekGider, @gunlukEsyaGider, @gunlukParaGider)";
            
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@oyunAlanEn", textBox1.Text);
            cmd.Parameters.AddWithValue("@oyunAlanBoy", textBox2.Text);
            cmd.Parameters.AddWithValue("@basParaMiktar", textBox3.Text);
            cmd.Parameters.AddWithValue("@basEsyaMiktar", textBox4.Text);
            cmd.Parameters.AddWithValue("@basYiyecekMiktar", textBox5.Text);
            cmd.Parameters.AddWithValue("@gunlukYiyecekGider", textBox6.Text);
            cmd.Parameters.AddWithValue("@gunlukEsyaGider", textBox7.Text);
            cmd.Parameters.AddWithValue("@gunlukParaGider", textBox8.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            veriGetir();
            MessageBox.Show("kayit eklendi");

            Form4 form4 = new Form4();
            form4.enn = textBox1.Text;
            form4.boyy = textBox2.Text;
            form4.basPara = Convert.ToInt32(textBox3.Text);
            form4.basEsya = Convert.ToInt32(textBox4.Text);
            form4.basYiyecek = Convert.ToInt32(textBox5.Text);

            form4.Show();
            Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            veriGetir();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            Hide();
        }
    }
}
