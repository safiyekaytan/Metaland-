using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace WindowsFormsApp6
{
    public partial class Form4 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=mazeland;Uid=root;Pwd='Pwd'");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adapter;
        MySqlDataAdapter adapter2;
        DataTable dt;
        DataTable dt2;

        public String enn;
        public String boyy;
        public int basPara;
        public int basEsya;
        public int basYiyecek;
        DateTime yeniTarih = new DateTime();
        public int dataRowCount;
        public String anlikSaat;
        public int gunFark;
        public String[] kullanicilar;
        public String kullaniciTablo;
        public int en;
        public int boy;
        public int[,] oyunAlan;
        public int money;
        public int goods;
        public int food;
        public int gunlukEsyaGider;
        public int gunlukParaGider;
        public int gunlukYiyecekGider;
        public String isim;

        public Form4()
        {
            InitializeComponent();
        }

        void veriGetir(String sorgu)
        {
            dt = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter(sorgu, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        void veriGetirAlan(String sorgu)
        {
            dt2 = new DataTable();
            conn.Open();
            adapter2 = new MySqlDataAdapter(sorgu, conn);
            adapter2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            conn.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            veriGetir("SELECT * FROM mazeland.kullanicitablo, mazeland.oyuntablo");

            veriGetirAlan("SELECT * FROM mazeland.alantablo");

            en = Convert.ToInt32(enn);
            boy = Convert.ToInt32(boyy);

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            kullanicilar = new String[dataGridView1.RowCount];
            veriGetir("SELECT k.kullaniciNo, k.kullaniciAd, k.kullaniciSoyad, k.kullaniciSifre, k.kullaniciYemekMiktar, k.kullaniciEsyaMiktar, k.kullaniciParaMiktar, k.guncelTarih, o.oyunId, o.oyunBasTarih, o.oyunAlanEn, o.oyunAlanBoy FROM mazeland.kullanicitablo k, mazeland.oyuntablo o");
            dataGridView1.Visible = true;
            label2.Visible = true;
            button4.Visible = true;
            
            for(int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                kullanicilar[i] = (i + 1) + ")  " + dataGridView1.Rows[i].Cells[1].Value.ToString() + " " + dataGridView1.Rows[i].Cells[2].Value.ToString() + "     yemek: " + dataGridView1.Rows[i].Cells[4].Value.ToString() + "     esya: " + dataGridView1.Rows[i].Cells[5].Value.ToString() + "     para: " + dataGridView1.Rows[i].Cells[6].Value.ToString() + ".\n";

                kullaniciTablo += kullanicilar[i];
            }
            label2.Text = kullaniciTablo;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            button4.Visible = false;
            dataGridView1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.ForeColor = Color.SlateBlue;
            label3.ForeColor = Color.SlateBlue;
            label4.BackColor = Color.SlateBlue;
            label5.BackColor = Color.SlateBlue;
            label6.BackColor = Color.SlateBlue;
            veriGetirAlan("SELECT * FROM mazeland.alantablo");

            en = Convert.ToInt32(dataGridView1.Rows[0].Cells[12].Value);
            boy = Convert.ToInt32(dataGridView1.Rows[0].Cells[13].Value);
            oyunAlan = new int[en, boy];

            for (int i = 0; i < dataGridView2.RowCount - 1; i++)  //alantablosunun tüm satırlarını tek tek al
            {
                if (Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) <= en)
                {
                    //alanıd 0-7 aralıgındaysa
                    //ilk satıra yerlestir
                    //mesela i = 3 olsun alanid = 4
                    //alanın tipi neyse ona göre yerleştir
                    if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Arsa")
                    {
                        oyunAlan[0, i] = 1;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Emlak")
                    {
                        oyunAlan[0, i] = 4;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Magaza")
                    {
                        oyunAlan[0, i] = 2;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Market")
                    {
                        oyunAlan[0, i] = 3;
                    }


                }
                else if (Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) >= en + 1 && Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) <= en * 2)
                {

                    //alanid = 9
                    //i = 8 olue
                    //0. yere yerlestir

                    if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Arsa")
                    {
                        oyunAlan[1, i - en] = 1;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Emlak")
                    {
                        oyunAlan[1, i - en] = 4;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Magaza")
                    {
                        oyunAlan[1, i - en] = 2;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Market")
                    {
                        oyunAlan[1, i - en] = 3;
                    }
                }
                else if (Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) >= (en * 2) + 1 && Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) <= en * 3)
                {
                    // alanıd 17
                    //i =16 
                    //0. yere yerlestir
                    //alanid = 19
                    //i=18
                    //18 - 16= 2

                    if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Arsa")
                    {
                        oyunAlan[2, i - (en * 2)] = 1;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Emlak")
                    {
                        oyunAlan[2, i - (en * 2)] = 4;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Magaza")
                    {
                        oyunAlan[2, i - (en * 2)] = 2;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Market")
                    {
                        oyunAlan[2, i - (en * 2)] = 3;
                    }
                }
                else if (Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) >= (en * 3) + 1 && Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) <= en * 4)
                {
                    //alanid =25
                    //i=24
                    //0.yere yerlestir
                    //i - en *3
                    if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Arsa")
                    {
                        oyunAlan[3, i - (en * 3)] = 1;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Emlak")
                    {
                        oyunAlan[3, i - (en * 3)] = 4;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Magaza")
                    {
                        oyunAlan[3, i - (en * 3)] = 2;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Market")
                    {
                        oyunAlan[3, i - (en * 3)] = 3;
                    }

                }
                else if (Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) >= (en * 4) + 1 && Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) <= en * 5)
                {
                    if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Arsa")
                    {
                        oyunAlan[4, i - (en * 4)] = 1;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Emlak")
                    {
                        oyunAlan[4, i - (en * 4)] = 4;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Magaza")
                    {
                        oyunAlan[4, i - (en * 4)] = 2;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Market")
                    {
                        oyunAlan[4, i - (en * 4)] = 3;
                    }
                }
                else if (Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) >= (en * 5) + 1 && Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) <= en * 6)
                {
                    if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Arsa")
                    {
                        oyunAlan[5, i - (en * 5)] = 1;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Emlak")
                    {
                        oyunAlan[5, i - (en * 5)] = 4;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Magaza")
                    {
                        oyunAlan[5, i - (en * 5)] = 2;
                    }
                    else if (dataGridView2.Rows[i].Cells[2].Value.ToString() == "Market")
                    {
                        oyunAlan[5, i - (en * 5)] = 3;
                    }
                }



            }

            for (int i = 0; i < en; i++)
            {
                for (int j = 0; j < boy; j++)
                {
                    if (oyunAlan[i, j] == 3)
                    {
                        //burası market

                        Label label = new Label();
                        label.Location = new Point(i * 60 + 250, j * 60 + 100);
                        label.AutoSize = false;
                        label.Text = "Market";
                        label.ForeColor = Color.White;
                        label.Font = new Font("Microsoft YaHei UI", 8.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
                        label.Size = new Size(55, 55);
                        label.BackColor = Color.DarkSlateBlue;
                        Controls.Add(label);
                    }
                    else if (oyunAlan[i, j] == 2)
                    {

                        //burası magaza 

                        Label label = new Label();
                        label.Location = new Point(i * 60 + 250, j * 60 + 100);
                        label.AutoSize = false;
                        label.Text = "Magaza";
                        label.ForeColor = Color.White;
                        label.Font = new Font("Microsoft YaHei UI", 8.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
                        label.Size = new Size(55, 55);
                        label.BackColor = Color.DarkSlateBlue;
                        Controls.Add(label);
                    }
                    else if (oyunAlan[i, j] == 4)
                    {
                        //burası emlak

                        Label label = new Label();
                        label.Location = new Point(i * 60 + 250, j * 60 + 100);
                        label.AutoSize = false;
                        label.Text = "Emlak";
                        label.Font = new Font("Microsoft YaHei UI", 8.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
                        label.ForeColor = Color.White;
                        label.Size = new Size(55, 55);
                        label.BackColor = Color.DarkSlateBlue;
                        Controls.Add(label);
                    }
                    else if (oyunAlan[i, j] == 1)
                    {
                        //burası arsa

                        Label label = new Label();
                        label.Location = new Point(i * 60 + 250, j * 60 + 100);
                        label.AutoSize = false;
                        label.Text = "Arsa";
                        label.Font = new Font("Microsoft YaHei UI", 8.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
                        label.ForeColor = Color.White;
                        label.Size = new Size(55, 55);
                        label.BackColor = Color.DarkSlateBlue;
                        Controls.Add(label);
                    }
                    else
                    {
                        Label label = new Label();
                        label.Location = new Point(i * 60 + 250, j * 60 + 100);
                        label.AutoSize = false;
                        label.Size = new Size(55, 55);
                        label.Font = new Font("Microsoft YaHei UI", 8.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
                        label.ForeColor = Color.SlateBlue;
                        label.BackColor = Color.SlateBlue;
                        Controls.Add(label);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox1.Visible = true;
            button6.Visible = true;
        }
        void sahteTariheBak(String fakeTarih)
        {

            String kullaniciBasTarih = dataGridView1.Rows[0].Cells[7].Value.ToString();
            MessageBox.Show(fakeTarih);
            MessageBox.Show(kullaniciBasTarih);
            //   5/19/2023 6:08:47 PM
            // ay, gun, yıl saat
            string[] tarihParcalaBugun = fakeTarih.Split('/');
            string[] tarihParcalaBas = kullaniciBasTarih.Split('/');


                gunFark = Convert.ToInt32(tarihParcalaBugun[1]) - Convert.ToInt32(tarihParcalaBas[1]);

                //PARAYI, ESYAYI, YEMEĞİ GUNCELLE
                String guncelle = "UPDATE kullanicitablo SET guncelTarih = @guncelTarih";
                cmd = new MySqlCommand(guncelle, conn);
                cmd.Parameters.AddWithValue("@guncelTarih", Convert.ToDateTime(fakeTarih));
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                veriGetir("SELECT * FROM mazeland.kullanicitablo, mazeland.oyuntablo");
                MessageBox.Show(" gun farkına gore guncellendi");
            



        }

        private void button6_Click(object sender, EventArgs e)
        {
            sahteTariheBak(textBox1.Text);
            label9.Visible = false;
            textBox1 .Visible = false;
            button6.Visible = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
