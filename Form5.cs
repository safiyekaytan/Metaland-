using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp6
{
    public partial class Form5 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=mazeland;Uid=root;Pwd='Pwd'");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adapter;
        DataTable dt;
        public String kayitliAd;
        public String kayitliSoyad;
        public String kayitliSifre;
        public bool girisKontrol = false;
        public int kayitliId;
        public Form5()
        {
            InitializeComponent();
        }
        void veriGetir()
        {
            dt = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM mazeland.kullanicitablo, mazeland.oyuntablo", conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            veriGetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Oyun Kurallari\nBaslangicta yoneticinin size verdigi yemek, esya ve para miktarina sahipsiniz.\n" +
                "Yoneticinin belirledigi oranlarda gunluk olarak belirli miktarda paranizda, yemeginizde ve esyanizda azaltmalar yapilacaktir. \n" +
                "Sag ust kısımda verileriniz gorulmektedir. Eger yemeginiz 0' in altina duserse kullanici olur ve oyun kaybedilir. \n" +
                "Eger para miktariniz 0' in altina duserse iflas edersiniz. \n" +
                "Eger esya miktariniz 0' in altina duserse oyuna devam edebilirsiniz ama en kisa surede bu veriyi yukseltmeye calisin.\n" +
                "Yeterli bakiyeniz varsa arsa satin alabilirsiniz ve belirlenen ucreti odeyerek arsayi 'Magaza', 'Market' ve 'Emlak' adli isletmelere donusturebilirsiniz.\n" +
                "Mevcut isletmelerden istediginizi secerek ise girebilirsiniz.\n" +
                "Her oyuncunun yalnizca bir isletmede calisma hakki vardir.\n" +
                "Eger calistiginiz isletme magaza ise calistiginiz sure boyunca esyaniz azalmaz.\n" +
                "Eger calistiginiz isletme market ise calistiginiz sure boyunca yemek azalmaz.\n" +
                "Eger calistiginiz isletme emlak ise calistiginiz sure boyunca para azalmaz.\n" +
                "Calistiginiz isletmede belirlenen fiyata gore gunluk maasinizi alirsiniz.\n" +
                "Kirala butonuna basarak istediginiz isletmeyi kiralayabilirsiniz ve kiraladiginiz.\n" +
                "Tarih Degistir butonuna tiklayarak guncel tarihi ileri bir tarihe atabilirsiniz ve buna bagli olarak para, yemek ve esya degerleriniz degisebilir.\n" +
                "Kareli alanda mevcut isletmeler ve arsalar gorunmektedir.\n" +
                "Satin alinana kadar tum arsalar yoneticiye aittir ve yoneticinin baslangicta bir magaza turunde, bir emlak turunde ve bir market turunde isletmesi vardir.\n" +
                "Alim - satim islemlerinizi kareli alanda gormek isterseniz sayfayi acip kapatmanizi oneririz.\n" +
                "Keyifli Oyunlar !"
                );


            String sql = "INSERT INTO kullanicitablo(kullaniciAd, kullaniciSoyad, kullaniciSifre, kullaniciYemekMiktar, kullaniciEsyaMiktar, kullaniciParaMiktar)" +
                            "VALUES (@kullaniciAd, @kullaniciSoyad, @kullaniciSifre, @kullaniciYemekMiktar, @kullaniciEsyaMiktar, @kullaniciParaMiktar)";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@kullaniciAd", textBox1.Text);
            cmd.Parameters.AddWithValue("@kullaniciSoyad", textBox2.Text);
            cmd.Parameters.AddWithValue("@kullaniciSifre", textBox3.Text);

            cmd.Parameters.AddWithValue("@kullaniciYemekMiktar", Convert.ToInt32(dataGridView1.Rows[0].Cells[16].Value.ToString()));
            cmd.Parameters.AddWithValue("@kullaniciEsyaMiktar", Convert.ToInt32(dataGridView1.Rows[0].Cells[15].Value.ToString()));
            cmd.Parameters.AddWithValue("@kullaniciParaMiktar", Convert.ToInt32(dataGridView1.Rows[0].Cells[14].Value.ToString()));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            veriGetir();
            button1.BackColor = Color.White;
            MessageBox.Show("kayit eklendi");
            Form2 form2 = new Form2();
            form2.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            kayitliAd = textBox1.Text;
            kayitliSoyad = textBox2.Text;
            kayitliSifre = textBox3.Text;
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value.ToString() == kayitliAd)
                {
                    if(dataGridView1.Rows[i].Cells[2].Value.ToString() == kayitliSoyad)
                    {
                        if(dataGridView1.Rows[i].Cells[3].Value.ToString() == kayitliSifre)
                        {
                            button2.BackColor = Color.White;
                            MessageBox.Show("kullanici bulundu, kullanici No : " + (i + 1));
                            girisKontrol = true;
                            kayitliId = i + 1;
                            Form7 form7 = new Form7();
                            form7.kayitliId = kayitliId;
                            form7.Show();
                            Hide();
                        }
                    }
                }
            }
            if(girisKontrol == false)
            {
                button2.BackColor = Color.DarkGreen;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
