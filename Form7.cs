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
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Bcpg;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace WindowsFormsApp6
{
    public partial class Form7 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=mazeland;Uid=root;Pwd='Pwd'");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adapter;
        MySqlDataAdapter adapter2;
        MySqlDataAdapter adapter3;
        MySqlDataAdapter adapter4;
        MySqlDataAdapter adapter5;
        MySqlDataAdapter adapter6;
        MySqlDataAdapter adapter7;
        DataTable dt;
        DataTable dt2;
        DataTable dt3;
        DataTable dt4;
        DataTable dt5;
        DataTable dt6;
        DataTable dt7;
        public int kayitliId;
        public List<Label> area = new List<Label>();
        public int magaza = 100;
        public int market = 85;
        public int emlak = 110;
        public int arsa = 200;
        public int[,] oyunAlan;
        public int en, boy;
        public int x, y;
        public int yerKontrol = 0;
        public String isletmeTur;
        public int maliyet;
        public String isim;
        public int money;
        public int kiralaButton;
        public int gunFark;
        public int gunlukEsyaGider;
        public int gunlukParaGider;
        public int gunlukYiyecekGider;
        public int food;
        public int goods;
        public String sahteTarih;
        public int isletmeyeDonusturKontrol = 0;
        public int ilkArsaKontrol = 0;
        public String kiralanacakTur;
        public String[] kiraArray;
        public List<Label> kiraLabel = new List<Label>();
        public List<Label> isLabel = new List<Label>();
        public DateTime kiraBitis = new DateTime();
        public int satirSay;
        public int kiralamaSure;
        public int iseGirKontrol = 0;
        public DateTime calismaBitis = new DateTime();
        public String iseGirTur;
        public String isTur;
        public int isiVarMi = 0;
        public int olduMu = 0;
        public Form7()
        {
            InitializeComponent();
        }
        void veriGetir()
        {
            dt = new DataTable();
            dt2 = new DataTable();
            dt3 = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM mazeland.kullanicitablo, mazeland.oyuntablo WHERE kullaniciNo = " + kayitliId , conn);
            adapter2 = new MySqlDataAdapter("SELECT * FROM mazeland.alantablo", conn);
            adapter3 = new MySqlDataAdapter("SELECT * FROM mazeland.isletmetablo", conn);
            adapter2.Fill(dt2);
            adapter.Fill(dt);
            adapter3.Fill(dt3);
            dataGridView2.DataSource = dt2;
            dataGridView1.DataSource = dt;
            dataGridView3.DataSource = dt3;
            conn.Close();
        }
        void kiralamaVeriGetir(String kira)
        {
            dt4 = new DataTable();
            conn.Open();
            adapter4 = new MySqlDataAdapter("SELECT * FROM mazeland.alantablo a, mazeland.isletmetablo i WHERE a.alanSahip = i.isletmeSahip AND a.alanTur = i.isletmeTur AND a.alanTur = '" + kira + "'", conn);
            adapter4.Fill(dt4);
            dataGridView4.DataSource = dt4;
            conn.Close();
        }

        void mevcutCalisanSayi(String isletmeSahip, String isletmeTur)
        {
            dt5 = new DataTable();
            conn.Open();
            adapter5 = new MySqlDataAdapter("SELECT isletmeCalisanSayi, isletmeId FROM mazeland.isletmetablo WHERE isletmeSahip = '" +isletmeSahip +  "' AND isletmeTur = '" + isletmeTur + "'", conn);
            adapter5.Fill(dt5);
            dataGridView5.DataSource = dt5;
            conn.Close();
        }

        void calismaTablo(int kullaniciNo)
        {
            dt6 = new DataTable();
            conn.Open();
            adapter6 = new MySqlDataAdapter("SELECT COUNT(*) FROM mazeland.calismatablo WHERE kullaniciNo = " + kullaniciNo, conn);
            adapter6.Fill(dt6);
            dataGridView6.DataSource = dt6;
            conn.Close();
        }

        void isTablo(int kullaniciNo)
        {
            dt7 = new DataTable();
            conn.Open();
            adapter7 = new MySqlDataAdapter("SELECT * FROM mazeland.calismatablo WHERE kullaniciNo = " + kullaniciNo, conn);
            adapter7.Fill(dt7);
            dataGridView7.DataSource = dt7;
            conn.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            

            kiraLabel.Add(label10);

            kiraLabel.Add(label11);
            kiraLabel.Add(label12);

            kiraLabel.Add(label13);
            kiraLabel.Add(label14);
            kiraLabel.Add(label15);
            kiraLabel.Add(label16);

            isLabel.Add(label10);
            isLabel.Add(label13);
            isLabel.Add(label14);
            isLabel.Add(label15);
            isLabel.Add(label16);

            isLabel.Add(label22);
            isLabel.Add(label23);


            veriGetir();
            
            label1.Text = "Yemek = " + dataGridView1.Rows[0].Cells[4].Value.ToString();
            label2.Text = "Esya = " + dataGridView1.Rows[0].Cells[5].Value.ToString();
            label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();

            int IdKullanici = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value.ToString());

            isTablo(IdKullanici);

            calismaTablo(IdKullanici);
            if(Convert.ToInt32(dataGridView6.Rows[0].Cells[0].Value.ToString()) == 0)
            {
                //işi yoksa
                label17.Text = dataGridView1.Rows[0].Cells[1].Value.ToString() + " " + dataGridView1.Rows[0].Cells[2].Value.ToString() + " (calismiyor )";
            }
            else
            {
                label17.Text = dataGridView1.Rows[0].Cells[1].Value.ToString() + " " + dataGridView1.Rows[0].Cells[2].Value.ToString() + " " + dataGridView7.Rows[0].Cells[7].Value.ToString() + " da calismaktadir.";
            }
            

            tariheBak();
            en = Convert.ToInt32(dataGridView1.Rows[0].Cells[12].Value);
            boy = Convert.ToInt32(dataGridView1.Rows[0].Cells[13].Value);
            oyunAlan = new int[en, boy]; //oyun alanı matris şeklinde oluşturuldu
            //işletmeye dönüşmüş alanlar magaza ise "2" degerini alır
            //işletmeye dönüşmüş alanlar market ise "3" degerini alır
            //işletmeye dönüşmüş alanlar emlak ise "4" degerini alır
            //arsalar "1" değerini alır
            //eger alan boşsa "0" degerini alır

        //    oyunAlan[0, 0] = 3; //yoneticinin marketi alanid= 1 i= alanid - 1
        //    oyunAlan[0, 1] = 2; //yoneticinin magazası
          //  oyunAlan[0, 2] = 4;  //yonetiicin emlagı
            
            for(int i = 0; i < dataGridView2.RowCount - 1; i++)  //alantablosunun tüm satırlarını tek tek al
            {
                if(Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()) <= en)
                {
                    //alanıd 0-7 aralıgındaysa
                    //ilk satıra yerlestir
                    //mesela i = 3 olsun alanid = 4
                    //alanın tipi neyse ona göre yerleştir
                    if(dataGridView2.Rows[i].Cells[2].Value.ToString() == "Arsa")
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
                        label.BackColor = Color.Black;
                        Controls.Add(label);
                        area.Add(label);
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
                        label.BackColor = Color.Black;
                        Controls.Add(label);
                        area.Add(label);
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
                        label.BackColor = Color.Black;
                        Controls.Add(label);
                        area.Add(label);
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
                        label.BackColor = Color.Black;
                        Controls.Add(label);
                        area.Add(label);
                    }
                    else
                    {
                        Label label = new Label();
                        label.Location = new Point(i * 60 + 250, j * 60 + 100);
                        label.AutoSize = false;
                        label.Size = new Size(55, 55);
                        label.Font = new Font("Microsoft YaHei UI", 8.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
                        label.ForeColor = Color.DarkSlateBlue;
                        label.BackColor = Color.DarkSlateBlue;
                        Controls.Add(label);
                        area.Add(label);
                    }
                }
            }
            olmek();
            if (olmek() == 1)
            {
                MessageBox.Show("kapatiliyor...");
                Application.Exit();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //İSE GİRMEK İÇİN İSLETME SEÇ
            //SEÇTİĞİN İSLETMEDE ÇALIŞMA SÜRESİ SEÇ
            //O SÜRE BOYUNCA GÜNLÜK MAAŞ AL
            //İSLETME TÜRÜNE GÖRE YEMEK VS SABİT TUT
            //HER KULLANICI SADECE 1 İSLETMEDE ÇALIŞABİLİR
            iseGirKontrol = 1;
         //   BURADA KALDIN BURDAN DEVAM
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label5.BackColor = Color.Black;
            label6.BackColor = Color.Black;
            label7.BackColor = Color.Black;

            label5.ForeColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            label6.ForeColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            label7.ForeColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
        }

        private void label5_Click(object sender, EventArgs e)
        {
            isletmeTur = "Magaza";
            kiralanacakTur = "Magaza";
            if(isletmeyeDonusturKontrol == 1)
            {
                isletmeyeDonustur(isletmeTur);
                ilkArsaKontrol = 0;
                isletmeyeDonusturKontrol = 0;
            }
            
            if(kiralaButton == 1)
            {
                foreach (var item in kiraLabel)
                {
                    item.Visible = true;
                }
                button7.Visible = true;
                numericUpDown1.Visible = true;
                numericUpDown2.Visible = true;
                kiralamaVeriGetir(kiralanacakTur);
                satirSay = dataGridView4.RowCount;
                String kullaniciAd = dataGridView1.Rows[0].Cells[1].Value.ToString();
                kiraArray = new String[satirSay - 1];

                for (int i = 0; i < satirSay - 1; i++)
                {
                    kiraArray[i] = (i + 1) + ")  Isletme Sahibi :  " + dataGridView4.Rows[i].Cells[1].Value.ToString() + "  Isletme Turu : " + dataGridView4.Rows[i].Cells[2].Value.ToString() + "  Kira Ucreti : " + dataGridView4.Rows[i].Cells[13].Value.ToString() + "\n";
                }

                var aaa = string.Concat(kiraArray);

                label10.Text = "Kiralama Tablosu\n" + aaa;
                //labelde kiralanabilecek ilgili isletme türleri listelendi
            }
            if(iseGirKontrol == 1)
            {
                //bir magazada ise gir

                foreach (var item in isLabel)
                {
                    item.Visible = true;
                }
                button8.Visible = true;
                numericUpDown3.Visible = true;
                numericUpDown4.Visible = true;
                kiralamaVeriGetir(kiralanacakTur);
                satirSay = dataGridView4.RowCount;
                String kullaniciAd = dataGridView1.Rows[0].Cells[1].Value.ToString();
                kiraArray = new String[satirSay - 1];

                for (int i = 0; i < satirSay - 1; i++)
                {
                    kiraArray[i] = (i + 1) + ")  Isletme Sahibi :  " + dataGridView4.Rows[i].Cells[1].Value.ToString() + "  Isletme Turu : " + dataGridView4.Rows[i].Cells[2].Value.ToString() + "  Gunluk Maas : " + dataGridView4.Rows[i].Cells[6].Value.ToString() + "\n";
                }

                var aaa = string.Concat(kiraArray);

                label10.Text = "Isletmeler Tablosu\n" + aaa;
                //labelde ise girebilecekler ilgili isletme türleri listelendi
                isTur = "Magaza";
                iseGirKontrol = 0;
            }
        }

        void arsaSatinAl()
        {


            money = Convert.ToInt32(dataGridView1.Rows[0].Cells[6].Value.ToString()); //mevcut para
            isim = dataGridView1.Rows[0].Cells[1].Value.ToString(); //mevcut para

            //paran yetiyor mu
            if (money < arsa)
            {
                MessageBox.Show("Yetersiz Bakiye");
            }

            else
            {
                for(int i = 0; i < en; i++)
                {
                    for(int j = 0; j < boy; j++)
                    {
                        if(yerKontrol == 0)
                        {
                            if (oyunAlan[i, j] != 1 && oyunAlan[i, j] != 2 && oyunAlan[i, j] != 3 && oyunAlan[i, j] != 4)
                            {
                                x = i;
                                y = j;
                                oyunAlan[x, y] = 1; //artık burada bir arsa var
                                yerKontrol++;

                            }
                        }
                        
                    }
                }
                
                //arsa aldı alanla ekle
                money -= arsa; //parayı azalttın, parayı güncelle

                //ARSAYI SATIN ALDI, ALANLAR TABLOSUNA EKLEDİ
                String ekle = "INSERT INTO alantablo(alanSahip, alanTur) VALUES (@alanSahip, @alanTur)";
                cmd = new MySqlCommand(ekle, conn);
                cmd.Parameters.AddWithValue("@alanSahip", isim);
                cmd.Parameters.AddWithValue("@alanTur", "Arsa");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                veriGetir();
                MessageBox.Show("Arsa Satin Alindi");



                //PARAYI GUNCELLE

                String guncelle = "UPDATE kullanicitablo SET kullaniciParaMiktar = @kullaniciParaMiktar WHERE kullaniciAd = @kullaniciAd";
                cmd = new MySqlCommand(guncelle, conn);
                cmd.Parameters.AddWithValue("@kullaniciParaMiktar", money);
                cmd.Parameters.AddWithValue("@kullaniciAd", isim);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                veriGetir();
                label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                MessageBox.Show("para guncellendi");


            }

        }

        void isletmeyeDonustur(String isletmeninTuru)
        {
            //isletmeye donusmemis arsası var mı bak //ok
            //istediği isletmeye parası yetiyor mu bak //OK
            //isletme tablosuna kayıtları yap //OK
            

            //İSLETMENİN TİPİNE GÖRE MALİYET ÇIKARDIN.
            if(isletmeninTuru == "Magaza")
            {
                maliyet = magaza;
            }

            else if(isletmeninTuru == "Market")
            {
                maliyet = market;
            }

            else if (isletmeninTuru == "Emlak")
            {
                maliyet = emlak;
            }


            int yer = 0;

            //PARASINA VE İSMİNE BAK
            money = Convert.ToInt32(dataGridView1.Rows[0].Cells[6].Value.ToString()); //mevcut para
            isim = dataGridView1.Rows[0].Cells[1].Value.ToString();

            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                int id = Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString()); //alanıd
                String ad = dataGridView2.Rows[i].Cells[1].Value.ToString(); //alansahip
                String tur = dataGridView2.Rows[i].Cells[2].Value.ToString(); //alantur
                if (ad == dataGridView1.Rows[0].Cells[1].Value.ToString()) //isim kullanicinin adıysa
                {
                    if(ilkArsaKontrol == 0)
                    {
                        if (tur == "Arsa") //arsası var mı
                        {
                            yer = i + 1;
                            if (money > maliyet) //parası yetiyo mu
                            {
                                ilkArsaKontrol = 1;
                                money -= maliyet;

                                //PARAYI GUNCELLE
                                String guncelle = "UPDATE kullanicitablo SET kullaniciParaMiktar = @kullaniciParaMiktar WHERE kullaniciAd = @kullaniciAd";
                                cmd = new MySqlCommand(guncelle, conn);
                                cmd.Parameters.AddWithValue("@kullaniciParaMiktar", money);
                                cmd.Parameters.AddWithValue("@kullaniciAd", ad);
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                veriGetir();
                                label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                                MessageBox.Show("para guncellendi");


                                //ARSAYI İSLETMEYE DONUSTURDU, ISLETME TABLOSUNA KAYDINI YAP
                                String ekle = "INSERT INTO isletmetablo(isletmeTur, yoneticiIsletmeUcret, kullaniciIsletmeUcret, isletmeSabitGelirMiktar, isletmeSabitGelirOran, isletmeSeviye, isletmeKapasite, isletmeCalisanSayi, isletmeFiyat, kiralikIsletmeFiyat, isletmeSahip) VALUES (@isletmeTur, @yoneticiIsletmeUcret, @kullaniciIsletmeUcret, @isletmeSabitGelirMiktar, @isletmeSabitGelirOran, @isletmeSeviye, @isletmeKapasite, @isletmeCalisanSayi, @isletmeFiyat, @kiralikIsletmeFiyat, @isletmeSahip)";
                                cmd = new MySqlCommand(ekle, conn);
                                cmd.Parameters.AddWithValue("@isletmeTur", isletmeninTuru);
                                cmd.Parameters.AddWithValue("@yoneticiIsletmeUcret", 400);
                                cmd.Parameters.AddWithValue("@kullaniciIsletmeUcret", 50);
                                cmd.Parameters.AddWithValue("@isletmeSabitGelirMiktar", 150);
                                cmd.Parameters.AddWithValue("@isletmeSabitGelirOran", 10);
                                cmd.Parameters.AddWithValue("@isletmeSeviye", 1);
                                cmd.Parameters.AddWithValue("@isletmeKapasite", 3);
                                cmd.Parameters.AddWithValue("@isletmeCalisanSayi", 0);
                                cmd.Parameters.AddWithValue("@isletmeFiyat", 600);
                                cmd.Parameters.AddWithValue("@kiralikIsletmeFiyat", 200);
                                cmd.Parameters.AddWithValue("@isletmeSahip", isim);
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                veriGetir();
                                MessageBox.Show("Arsaniz" + isletmeninTuru + " e Donusturuldu");

                                //ALANTABLOSUNDA ARSA YAZISINI İSLETME DİYE DEĞİSTİR.

                                String alandanGuncelle = "UPDATE alantablo SET alanTur=@alanTur WHERE alanId = @alanId";
                                cmd = new MySqlCommand(alandanGuncelle, conn);
                                cmd.Parameters.AddWithValue("@alanId", yer);
                                cmd.Parameters.AddWithValue("@alanTur", isletmeninTuru);
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                veriGetir();
                                MessageBox.Show("alan tablosundaki arsa yazısı degistirildi");


                                //İLGİLİ İSLETME TABLOSUNA KAYİT YAP

                                if (isletmeninTuru == "Magaza")
                                {
                                    //magaza tablosuna kayıt yap
                                    String magazayaEkle = "INSERT INTO magazatablo(magazaEsyaUcret, magazaSahip) VALUES (@magazaEsyaUcret, @magazaSahip)";
                                    cmd = new MySqlCommand(magazayaEkle, conn);
                                    cmd.Parameters.AddWithValue("@magazaEsyaUcret", 40);
                                    cmd.Parameters.AddWithValue("@magazaSahip", ad);
                                    conn.Open();
                                    cmd.ExecuteNonQuery();
                                    conn.Close();
                                    veriGetir();
                                    MessageBox.Show("Magaza Tablosuna Kayit Yapildi");
                                }

                                else if (isletmeninTuru == "Market")
                                {
                                    //market tablosuna kayıt yap
                                    String marketeEkle = "INSERT INTO markettablo(marketYiyecekUcret, marketSahip) VALUES (@marketYiyecekUcret, @marketSahip)";
                                    cmd = new MySqlCommand(marketeEkle, conn);
                                    cmd.Parameters.AddWithValue("@marketYiyecekUcret", 35);
                                    cmd.Parameters.AddWithValue("@marketSahip", ad);
                                    conn.Open();
                                    cmd.ExecuteNonQuery();
                                    conn.Close();
                                    veriGetir();
                                    MessageBox.Show("Market Tablosuna Kayit Yapildi");
                                }

                                else if (isletmeninTuru == "Emlak")
                                {
                                    //market tablosuna kayıt yap
                                    String emlakEkle = "INSERT INTO emlaktablo(emlakKomisyon, emlakIslemi, emlakSahip) VALUES (@emlakKomisyon, @emlakIslemi, @emlakSahip)";
                                    cmd = new MySqlCommand(emlakEkle, conn);
                                    cmd.Parameters.AddWithValue("@emlakKomisyon", 85);
                                    cmd.Parameters.AddWithValue("@emlakIslemi", "Emlak Islemleri Burada Gorunur");
                                    cmd.Parameters.AddWithValue("@emlakSahip", ad);
                                    conn.Open();
                                    cmd.ExecuteNonQuery();
                                    conn.Close();
                                    veriGetir();
                                    MessageBox.Show("Emlak Tablosuna Kayit Yapildi");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Yetersiz Bakiye");
                            }
                        }
                    }
                    
                }
            }
        }

        void kirala()
        {
            //KİRALAYACAK PARASI VAR MI?
            money = Convert.ToInt32(dataGridView1.Rows[0].Cells[6].Value.ToString());
            
            satirSay = dataGridView4.RowCount;
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = satirSay - 1;
            kiralamaSure = Convert.ToInt32(numericUpDown2.Value);
            kiraBitis = DateTime.Now.AddDays(kiralamaSure);

            int indis = Convert.ToInt32(numericUpDown1.Value); //mesela 1i seçtiyse sıfırıncı indisi al
            int kiraParasi = Convert.ToInt32(dataGridView4.Rows[indis - 1].Cells[6].Value.ToString());
            String isletmeSahibi = dataGridView4.Rows[indis - 1].Cells[1].Value.ToString();
            String kullaniciAd = dataGridView1.Rows[0].Cells[1].Value.ToString();
            //  MessageBox.Show(kiraArray[indis - 1]);
            int gunlukGelir = 5;


            //KİRALAMAK İSTEDİGİNİN KAYDINI KİRA TABLOSUNA YAP
            String kiraEkle = "INSERT INTO kiratablo(kiralananTur, isletmeSahip, kiralayanKisi, kiraSure, kiraBitisTarih, gunlukGelir) VALUES (@kiralananTur, @isletmeSahip, @kiralayanKisi, @kiraSure, @kiraBitisTarih, @gunlukGelir)";
            cmd = new MySqlCommand(kiraEkle, conn);
            cmd.Parameters.AddWithValue("@kiralananTur", isletmeTur);
            cmd.Parameters.AddWithValue("@isletmeSahip", isletmeSahibi);
            cmd.Parameters.AddWithValue("@kiralayanKisi", kullaniciAd);
            cmd.Parameters.AddWithValue("@kiraSure", kiralamaSure); //BURADA KALDIN, KİRA SÜRESİNİ TEXTBOXLA AL
            cmd.Parameters.AddWithValue("@kiraBitisTarih", kiraBitis);
            cmd.Parameters.AddWithValue("@gunlukGelir", gunlukGelir);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            veriGetir();
            MessageBox.Show("Kira tablosuna kayit yapildi");



        }


        private void label6_Click(object sender, EventArgs e)
        {
            isletmeTur = "Market";
            kiralanacakTur = "Market";
            if (isletmeyeDonusturKontrol == 1)
            {
                isletmeyeDonustur(isletmeTur);
                ilkArsaKontrol = 0;
                isletmeyeDonusturKontrol = 0;
            }

            if (kiralaButton == 1)
            {
                foreach (var item in kiraLabel)
                {
                    item.Visible = true;
                }
                button7.Visible = true;
                numericUpDown1.Visible = true;
                numericUpDown2.Visible = true;
                kiralamaVeriGetir(kiralanacakTur);
                satirSay = dataGridView4.RowCount;
                String kullaniciAd = dataGridView1.Rows[0].Cells[1].Value.ToString();
                kiraArray = new String[satirSay - 1];

                for (int i = 0; i < satirSay - 1; i++)
                {
                    kiraArray[i] = (i + 1) + ")  Isletme Sahibi :  " + dataGridView4.Rows[i].Cells[1].Value.ToString() + "  Isletme Turu : " + dataGridView4.Rows[i].Cells[2].Value.ToString() + "  Kira Ucreti : " + dataGridView4.Rows[i].Cells[13].Value.ToString() + "\n";
                }

                var aaa = string.Concat(kiraArray);

                label10.Text = "Kiralama Tablosu\n" + aaa;
                //labelde kiralanabilecek ilgili isletme türleri listelendi

            }
            if (iseGirKontrol == 1)
            {
                foreach (var item in isLabel)
                {
                    item.Visible = true;
                }
                button8.Visible = true;
                numericUpDown3.Visible = true;
                numericUpDown4.Visible = true;
                kiralamaVeriGetir(kiralanacakTur);
                satirSay = dataGridView4.RowCount;
                String kullaniciAd = dataGridView1.Rows[0].Cells[1].Value.ToString();
                kiraArray = new String[satirSay - 1];

                for (int i = 0; i < satirSay - 1; i++)
                {
                    kiraArray[i] = (i + 1) + ")  Isletme Sahibi :  " + dataGridView4.Rows[i].Cells[1].Value.ToString() + "  Isletme Turu : " + dataGridView4.Rows[i].Cells[2].Value.ToString() + "  Gunluk Maas : " + dataGridView4.Rows[i].Cells[6].Value.ToString() + "\n";
                }

                var aaa = string.Concat(kiraArray);

                label10.Text = "İsletmeler Tablosu\n" + aaa;
                //labelde ise girebilecekler ilgili isletme türleri listelendi
                isTur = "Market";
                iseGirKontrol = 0;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            isletmeTur = "Emlak";
            kiralanacakTur = "Emlak";
            if (isletmeyeDonusturKontrol == 1)
            {
                isletmeyeDonustur(isletmeTur);
                ilkArsaKontrol = 0;
                isletmeyeDonusturKontrol = 0;
            }

            if (kiralaButton == 1)
            {
                foreach (var item in kiraLabel)
                {
                    item.Visible = true;
                }
                button7.Visible = true;
                numericUpDown1.Visible = true;
                numericUpDown2.Visible = true;
                kiralamaVeriGetir(kiralanacakTur);
                satirSay = dataGridView4.RowCount;
                String kullaniciAd = dataGridView1.Rows[0].Cells[1].Value.ToString();
                kiraArray = new String[satirSay - 1];

                for (int i = 0; i < satirSay - 1; i++)
                {
                    kiraArray[i] = (i + 1) + ")  Isletme Sahibi :  " + dataGridView4.Rows[i].Cells[1].Value.ToString() + "  Isletme Turu : " + dataGridView4.Rows[i].Cells[2].Value.ToString() + "  Kira Ucreti : " + dataGridView4.Rows[i].Cells[13].Value.ToString() + "\n -------------------------\n";
                }

                var aaa = string.Concat(kiraArray);

                label10.Text = "Kiralama Tablosu\n" + aaa;
                //labelde kiralanabilecek ilgili isletme türleri listelendi
            }
            if (iseGirKontrol == 1)
            {
                foreach (var item in isLabel)
                {
                    item.Visible = true;
                }
                button8.Visible = true;
                numericUpDown3.Visible = true;
                numericUpDown4.Visible = true;
                kiralamaVeriGetir(kiralanacakTur);
                satirSay = dataGridView4.RowCount;
                String kullaniciAd = dataGridView1.Rows[0].Cells[1].Value.ToString();
                kiraArray = new String[satirSay - 1];

                for (int i = 0; i < satirSay - 1; i++)
                {
                    kiraArray[i] = (i + 1) + ")  Isletme Sahibi :  " + dataGridView4.Rows[i].Cells[1].Value.ToString() + "  Isletme Turu : " + dataGridView4.Rows[i].Cells[2].Value.ToString() + "  Gunluk Maas : " + dataGridView4.Rows[i].Cells[6].Value.ToString() + "\n";
                }

                var aaa = string.Concat(kiraArray);

                label10.Text = "Isletmeler Tablosu\n" + aaa;
                //labelde ise girebilecekler ilgili isletme türleri listelendi
                isTur = "Emlak";
                iseGirKontrol = 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //bir isletme kiralanabilir
            //kiralancak isletme tipini seç
            //eğer kirala butonuna tıklarsa kirala = 1 olsun
            //kirala = 1 iken isletmelerden birine tıklanırsada oradaki isletme sahipleri ve isletmelerin fiyatları listelensin
            //istedigini seç
            //kiralama süreni de gir.
            //kira tablona eklersin
            kiralaButton = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //sahte bir tarih üret
            //kullanici tablosundaki tarihi al
            //aradaki gün sayisina göre islemlerini yap
            textBox1.Visible = true;
            label9.Visible = true;
            button6.Visible = true;
            
            
        }

        private void label8_Click(object sender, EventArgs e)
        {
            arsaSatinAl();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isletmeyeDonusturKontrol = 1;
            label5.BackColor = Color.BlueViolet;
            label6.BackColor = Color.BlueViolet;
            label7.BackColor = Color.BlueViolet;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sahteTarih = textBox1.Text;
            sahteTariheBak(sahteTarih);
            textBox1.Visible = false;
            label9.Visible = false;
            button6.Visible = false;

            if(isiVarMi == 1)
            {
                String isinTuru = dataGridView7.Rows[0].Cells[7].Value.ToString();
                //sahte tariih olusturuldu, iste calismak fonksiyonunu tekrar çalıştır.
                isteCalismak(isinTuru);
            }
            
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            kirala();
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            button7.Visible = false;
            foreach (var item in kiraLabel)
            {
                item.Visible = false;
            }

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        void tariheBak()
        {
            DateTime dt = DateTime.Now;
            String bugununTarih = dt.ToString();
            String kullaniciBasTarih = dataGridView1.Rows[0].Cells[7].Value.ToString();
            MessageBox.Show(bugununTarih);
            MessageBox.Show(kullaniciBasTarih);
            //   5/19/2023 6:08:47 PM
            // ay, gun, yıl saat
            string[] tarihParcalaBugun = bugununTarih.Split('/');
            string[] tarihParcalaBas = kullaniciBasTarih.Split('/');
            if (tarihParcalaBugun[0] == tarihParcalaBas[0]) //aynı aysa
            {
                //eger aynı aydalarsa
                //gunlerinin farkını hesapla
                gunFark = Convert.ToInt32(tarihParcalaBugun[1]) - Convert.ToInt32(tarihParcalaBas[1]);


                gunlukEsyaGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[18].Value.ToString());
                gunlukParaGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[19].Value.ToString());
                gunlukYiyecekGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[17].Value.ToString());

                money = Convert.ToInt32(dataGridView1.Rows[0].Cells[6].Value.ToString()); //mevcut para

                goods = Convert.ToInt32(dataGridView1.Rows[0].Cells[5].Value.ToString()); //mevcut esya

                food = Convert.ToInt32(dataGridView1.Rows[0].Cells[4].Value.ToString()); //mevcut yiyecek

                isim = dataGridView1.Rows[0].Cells[1].Value.ToString(); //mevcut yiyecek
                //gun farkının gunluk gideri kadar eksilt

                money -= gunFark * gunlukParaGider;
                goods -= gunFark * gunlukEsyaGider;
                food -= gunFark * gunlukYiyecekGider;

                //PARAYI, ESYAYI, YEMEĞİ GUNCELLE, guncel tarihide guncelle
                String guncelle = "UPDATE kullanicitablo SET kullaniciParaMiktar = @kullaniciParaMiktar, kullaniciEsyaMiktar = @kullaniciEsyaMiktar, kullaniciYemekMiktar = @kullaniciYemekMiktar, guncelTarih = @guncelTarih WHERE kullaniciAd = @kullaniciAd";
                cmd = new MySqlCommand(guncelle, conn);
                cmd.Parameters.AddWithValue("@kullaniciParaMiktar", money);
                cmd.Parameters.AddWithValue("@kullaniciEsyaMiktar", goods);
                cmd.Parameters.AddWithValue("@kullaniciYemekMiktar", food);
                cmd.Parameters.AddWithValue("@guncelTarih", dt);
                cmd.Parameters.AddWithValue("@kullaniciAd", isim);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                veriGetir();
                label1.Text = "Yemek = " + dataGridView1.Rows[0].Cells[4].Value.ToString();
                label2.Text = "Esya = " + dataGridView1.Rows[0].Cells[5].Value.ToString();
                label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                MessageBox.Show("para, esya ve yiyecek gun farkına gore guncellendi");
                olmek();
                if (olmek() == 1)
                {
                    MessageBox.Show("kapatiliyor...");
                    Application.Exit();
                }
            }
            else if(Convert.ToInt32(tarihParcalaBugun[0]) > Convert.ToInt32(tarihParcalaBas[0])) //farklı aysa
            {
                int ayFarki = Convert.ToInt32(tarihParcalaBugun[0]) - Convert.ToInt32(tarihParcalaBas[0]);  // 2 ay farkli
                gunFark = 0;
                if(ayFarki == 1)
                {
                    gunFark += 30 - Convert.ToInt32(tarihParcalaBas[1]); //onceki ayın artık gununu ekledin
                    gunFark += Convert.ToInt32(tarihParcalaBugun[1]); //bu ayın gun sayısını ekledin
                }
                else if(ayFarki > 1)
                {
                    gunFark += 30 - Convert.ToInt32(tarihParcalaBas[1]); //onceki ayın artık gununu ekledin
                    gunFark += Convert.ToInt32(tarihParcalaBugun[1]); //bu ayın gun sayısını ekledin

                    gunFark += (ayFarki - 1) * 30; //aradak ay farkinin 1 eksiği kadar 30 gun
                }


                gunlukEsyaGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[18].Value.ToString());
                gunlukParaGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[19].Value.ToString());
                gunlukYiyecekGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[17].Value.ToString());

                money = Convert.ToInt32(dataGridView1.Rows[0].Cells[6].Value.ToString()); //mevcut para

                goods = Convert.ToInt32(dataGridView1.Rows[0].Cells[5].Value.ToString()); //mevcut esya

                food = Convert.ToInt32(dataGridView1.Rows[0].Cells[4].Value.ToString()); //mevcut yiyecek

                isim = dataGridView1.Rows[0].Cells[1].Value.ToString(); //mevcut yiyecek
                //gun farkının gunluk gideri kadar eksilt

                money -= gunFark * gunlukParaGider;
                goods -= gunFark * gunlukEsyaGider;
                food -= gunFark * gunlukYiyecekGider;

                //PARAYI, ESYAYI, YEMEĞİ GUNCELLE, guncel tarihide guncelle
                String guncelle = "UPDATE kullanicitablo SET kullaniciParaMiktar = @kullaniciParaMiktar, kullaniciEsyaMiktar = @kullaniciEsyaMiktar, kullaniciYemekMiktar = @kullaniciYemekMiktar, guncelTarih = @guncelTarih WHERE kullaniciAd = @kullaniciAd";
                cmd = new MySqlCommand(guncelle, conn);
                cmd.Parameters.AddWithValue("@kullaniciParaMiktar", money);
                cmd.Parameters.AddWithValue("@kullaniciEsyaMiktar", goods);
                cmd.Parameters.AddWithValue("@kullaniciYemekMiktar", food);
                cmd.Parameters.AddWithValue("@guncelTarih", dt);
                cmd.Parameters.AddWithValue("@kullaniciAd", isim);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                veriGetir();
                label1.Text = "Yemek = " + dataGridView1.Rows[0].Cells[4].Value.ToString();
                label2.Text = "Esya = " + dataGridView1.Rows[0].Cells[5].Value.ToString();
                label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                MessageBox.Show("para, esya ve yiyecek gun farkına gore guncellendi");
                olmek();
                if (olmek() == 1)
                {
                    MessageBox.Show("kapatiliyor...");
                    Application.Exit();
                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            iseBasla(isTur);
            numericUpDown3.Visible = false;
            numericUpDown4.Visible = false;
            button8.Visible = false;
            foreach (var item in isLabel)
            {
                item.Visible = false;
            }
            
            isteCalismak(isTur);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            if (tarihParcalaBugun[0] == tarihParcalaBas[0])
            {
                //eger aynı aydalarsa
                //gunlerinin farkını hesapla
                gunFark = Convert.ToInt32(tarihParcalaBugun[1]) - Convert.ToInt32(tarihParcalaBas[1]);

                gunlukEsyaGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[18].Value.ToString());
                gunlukParaGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[19].Value.ToString());
                gunlukYiyecekGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[17].Value.ToString());

                money = Convert.ToInt32(dataGridView1.Rows[0].Cells[6].Value.ToString()); //mevcut para

                goods = Convert.ToInt32(dataGridView1.Rows[0].Cells[5].Value.ToString()); //mevcut esya

                food = Convert.ToInt32(dataGridView1.Rows[0].Cells[4].Value.ToString()); //mevcut yiyecek

                isim = dataGridView1.Rows[0].Cells[1].Value.ToString(); //mevcut yiyecek
                //gun farkının gunluk gideri kadar eksilt

                money -= gunFark * gunlukParaGider;
                goods -= gunFark * gunlukEsyaGider;
                food -= gunFark * gunlukYiyecekGider;

                //PARAYI, ESYAYI, YEMEĞİ GUNCELLE
                String guncelle = "UPDATE kullanicitablo SET kullaniciParaMiktar = @kullaniciParaMiktar, kullaniciEsyaMiktar = @kullaniciEsyaMiktar, kullaniciYemekMiktar = @kullaniciYemekMiktar, guncelTarih = @guncelTarih WHERE kullaniciAd = @kullaniciAd";
                cmd = new MySqlCommand(guncelle, conn);
                cmd.Parameters.AddWithValue("@kullaniciParaMiktar", money);
                cmd.Parameters.AddWithValue("@kullaniciEsyaMiktar", goods);
                cmd.Parameters.AddWithValue("@kullaniciYemekMiktar", food);
                cmd.Parameters.AddWithValue("@kullaniciAd", isim);
                cmd.Parameters.AddWithValue("@guncelTarih", Convert.ToDateTime(fakeTarih));
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                veriGetir();
                label1.Text = "Yemek = " + dataGridView1.Rows[0].Cells[4].Value.ToString();
                label2.Text = "Esya = " + dataGridView1.Rows[0].Cells[5].Value.ToString();
                label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                MessageBox.Show("para, esya ve yiyecek gun farkına gore guncellendi");
                olmek();
                if (olmek() == 1)
                {
                    MessageBox.Show("kapatiliyor...");
                    Application.Exit();
                }
            }

            else if (Convert.ToInt32(tarihParcalaBugun[0]) > Convert.ToInt32(tarihParcalaBas[0])) //farklı aysa
            {
                int ayFarki = Convert.ToInt32(tarihParcalaBugun[0]) - Convert.ToInt32(tarihParcalaBas[0]);  // 2 ay farkli
                gunFark = 0;
                if (ayFarki == 1)
                {
                    gunFark += 30 - Convert.ToInt32(tarihParcalaBas[1]); //onceki ayın artık gununu ekledin
                    gunFark += Convert.ToInt32(tarihParcalaBugun[1]); //bu ayın gun sayısını ekledin
                }
                else if (ayFarki > 1)
                {
                    gunFark += 30 - Convert.ToInt32(tarihParcalaBas[1]); //onceki ayın artık gununu ekledin
                    gunFark += Convert.ToInt32(tarihParcalaBugun[1]); //bu ayın gun sayısını ekledin
                    gunFark += (ayFarki - 1) * 30; //aradak ay farkinin 1 eksiği kadar 30 gun
                }

                gunlukEsyaGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[18].Value.ToString());
                gunlukParaGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[19].Value.ToString());
                gunlukYiyecekGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[17].Value.ToString());

                money = Convert.ToInt32(dataGridView1.Rows[0].Cells[6].Value.ToString()); //mevcut para

                goods = Convert.ToInt32(dataGridView1.Rows[0].Cells[5].Value.ToString()); //mevcut esya

                food = Convert.ToInt32(dataGridView1.Rows[0].Cells[4].Value.ToString()); //mevcut yiyecek

                isim = dataGridView1.Rows[0].Cells[1].Value.ToString(); //mevcut yiyecek
                //gun farkının gunluk gideri kadar eksilt

                money -= gunFark * gunlukParaGider;
                goods -= gunFark * gunlukEsyaGider;
                food -= gunFark * gunlukYiyecekGider;

                //PARAYI, ESYAYI, YEMEĞİ GUNCELLE
                String guncelle = "UPDATE kullanicitablo SET kullaniciParaMiktar = @kullaniciParaMiktar, kullaniciEsyaMiktar = @kullaniciEsyaMiktar, kullaniciYemekMiktar = @kullaniciYemekMiktar, guncelTarih = @guncelTarih WHERE kullaniciAd = @kullaniciAd";
                cmd = new MySqlCommand(guncelle, conn);
                cmd.Parameters.AddWithValue("@kullaniciParaMiktar", money);
                cmd.Parameters.AddWithValue("@kullaniciEsyaMiktar", goods);
                cmd.Parameters.AddWithValue("@kullaniciYemekMiktar", food);
                cmd.Parameters.AddWithValue("@kullaniciAd", isim);
                cmd.Parameters.AddWithValue("@guncelTarih", Convert.ToDateTime(fakeTarih));
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                veriGetir();
                label1.Text = "Yemek = " + dataGridView1.Rows[0].Cells[4].Value.ToString();
                label2.Text = "Esya = " + dataGridView1.Rows[0].Cells[5].Value.ToString();
                label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                MessageBox.Show("para, esya ve yiyecek gun farkına gore guncellendi");
                olmek();
                if (olmek() == 1)
                {
                    MessageBox.Show("kapatiliyor...");
                    Application.Exit();
                }

            }


            }

        void iseBasla(String girilenIs)
        {


            int kacinci = Convert.ToInt32(numericUpDown3.Value);
            int indis = Convert.ToInt32(numericUpDown3.Value); //mesela 1i seçtiyse sıfırıncı indisi al

            String isletmeSahibi = dataGridView4.Rows[indis - 1].Cells[1].Value.ToString();
            int isletmeIdd = Convert.ToInt32(dataGridView4.Rows[indis - 1].Cells[3].Value.ToString());
            String kullaniciAd = dataGridView1.Rows[0].Cells[1].Value.ToString();

            //       mevcutCalisanSayi(isletmeSahibi, girilenIs);
            //EĞER HİÇBİR İŞİ YOKSA KULLANICININ İŞE BAŞLAYABİLİR
            int kullaniciNosu = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value.ToString());

            calismaTablo(kullaniciNosu);
            MessageBox.Show(dataGridView6.Rows[0].Cells[0].Value.ToString());

            if (Convert.ToInt32(dataGridView6.Rows[0].Cells[0].Value.ToString()) == 0)
            {
                mevcutCalisanSayi(isletmeSahibi, girilenIs);

                int kullaniciCalSayi = Convert.ToInt32(numericUpDown4.Value);
                calismaBitis = DateTime.Now.AddDays(kullaniciCalSayi);



                int gunlukUcret = Convert.ToInt32(dataGridView4.Rows[kacinci - 1].Cells[6].Value.ToString());



                //calisma tablosuna kayıt yap        // ok

                //isletmetablosuna ilgili isletme ile ilgili çalışan sayısını değiştir
                //kullanıcıya çalıştığı gün süresince her gün belirli miktarda para ver
                //kullanıcıya çalıştığı gün süresince çalıştığı işletmenin ürününü azaltma

                //ÇALIŞMA TABLOSUNA İŞİN KAYDI YAPILDI
                String calismaEkle = "INSERT INTO calismatablo(kullaniciNo, kullaniciCalBitTarih, kullaniciCalGunSayi, kullaniciCalSaat, gunlukUcret, isTur, isletmeId) VALUES (@kullaniciNo, @kullaniciCalBitTarih, @kullaniciCalGunSayi, @kullaniciCalSaat, @gunlukUcret, @isTur, @isletmeId)";
                cmd = new MySqlCommand(calismaEkle, conn);
                cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciNosu);
                cmd.Parameters.AddWithValue("@kullaniciCalBitTarih", calismaBitis);
                cmd.Parameters.AddWithValue("@kullaniciCalGunSayi", kullaniciCalSayi);
                cmd.Parameters.AddWithValue("@kullaniciCalSaat", 8);
                cmd.Parameters.AddWithValue("@gunlukUcret", gunlukUcret);
                cmd.Parameters.AddWithValue("@isTur", girilenIs);
                cmd.Parameters.AddWithValue("@isletmeId", isletmeIdd);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                kiralamaVeriGetir("Magaza");
                MessageBox.Show("calisma tablosuna kayit yapildi");

                isiVarMi = 1;


                int calisanSay = Convert.ToInt32(dataGridView5.Rows[0].Cells[0].Value.ToString()); //mevcut çalışan sayısına bak
                calisanSay++; //bir arttır
                int isletmeId = Convert.ToInt32(dataGridView5.Rows[0].Cells[1].Value.ToString());


                //İŞLETME TABLOSUNDAKİ ÇALIŞAN SAYISINI DEĞİŞTİR
                String isletmeGuncelle = "UPDATE isletmetablo SET isletmeCalisanSayi = @isletmeCalisanSayi WHERE isletmeId = @isletmeId";
                cmd = new MySqlCommand(isletmeGuncelle, conn);
                cmd.Parameters.AddWithValue("@isletmeCalisanSayi", calisanSay);
                cmd.Parameters.AddWithValue("@isletmeId", isletmeId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                veriGetir();
                label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                MessageBox.Show("para guncellendi");

                


            }
            else
            {
                MessageBox.Show("kullanicinin zaten bir isi var, iki iste birden calisamaz!!!!!!");
            }


        }

        void isteCalismak(String isTur)
        {

            int kullaniciId = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value.ToString());
            int paraMiktar = Convert.ToInt32(dataGridView1.Rows[0].Cells[6].Value.ToString());  //mevcut para miktari
            isTablo(kullaniciId);

            if (isTur == "Magaza")
            {
                int gunlukEsyaGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[18].Value.ToString()); //gunlujk esya gideri
                int esyaMiktar = Convert.ToInt32(dataGridView1.Rows[0].Cells[5].Value.ToString());  //mevcut esya miktari
                

                DateTime basTarih = Convert.ToDateTime(dataGridView7.Rows[0].Cells[2].Value.ToString());
                DateTime bitTarih = Convert.ToDateTime(dataGridView7.Rows[0].Cells[3].Value.ToString());

                DateTime guncelTarih = Convert.ToDateTime(dataGridView1.Rows[0].Cells[7].Value.ToString());
                //kullanicinin guncel tarihini aldın

                if (bitTarih > guncelTarih) //calisma süresi bitmediyse
                {
                    string[] tarihParcala = guncelTarih.ToString().Split('/');
                    string[] tarihParcala2 = basTarih.ToString().Split('/');
                    int gunlukMaasi = Convert.ToInt32(dataGridView7.Rows[0].Cells[6].Value.ToString());

                    if (tarihParcala[0] == tarihParcala2[0]) //eğer aynı aysa
                    {
                        int fark = Convert.ToInt32(tarihParcala[1]) - Convert.ToInt32(tarihParcala2[1]); //gun farkı (AYNI AYSA)
                        
                        paraMiktar += gunlukMaasi * fark;
                        esyaMiktar += gunlukEsyaGider * fark;

                        String guncelle = "UPDATE kullanicitablo SET kullaniciEsyaMiktar = @kullaniciEsyaMiktar, kullaniciParaMiktar = @kullaniciParaMiktar WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelle, conn);
                        cmd.Parameters.AddWithValue("@kullaniciEsyaMiktar", esyaMiktar);
                        cmd.Parameters.AddWithValue("@kullaniciParaMiktar", paraMiktar);
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        //KULLANİCİ GÜN SAYİ BİR AZALDI, SIFIRLANANA KADAR DEVAM
                        String guncelleGun = "UPDATE calismatablo SET kullaniciCalBasTarih = @kullaniciCalBasTarih WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelleGun, conn);
                        cmd.Parameters.AddWithValue("@kullaniciCalBasTarih", guncelTarih); //cal bas tarihi bugunle değiştir
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        label1.Text = "Yemek = " + dataGridView1.Rows[0].Cells[4].Value.ToString();
                        label2.Text = "Esya = " + dataGridView1.Rows[0].Cells[5].Value.ToString();
                        label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                        MessageBox.Show("Magazada calistigi icin esya miktari sabit kaldi, GUNLUK MAAS VERILDI");
                    }

                    else if (Convert.ToInt32(tarihParcala[0]) > Convert.ToInt32(tarihParcala2[0])) //farkli aysa
                    {

                        int ayFarki = Convert.ToInt32(tarihParcala[0]) - Convert.ToInt32(tarihParcala2[0]);  // 2 ay farkli
                        gunFark = 0;
                        if (ayFarki == 1)
                        {
                            gunFark += 30 - Convert.ToInt32(tarihParcala2[1]); //onceki ayın artık gununu ekledin
                            gunFark += Convert.ToInt32(tarihParcala[1]); //bu ayın gun sayısını ekledin
                        }
                        else if (ayFarki > 1)
                        {
                            gunFark += 30 - Convert.ToInt32(tarihParcala2[1]); //onceki ayın artık gununu ekledin
                            gunFark += Convert.ToInt32(tarihParcala[1]); //bu ayın gun sayısını ekledin

                            gunFark += (ayFarki - 1) * 30; //aradak ay farkinin 1 eksiği kadar 30 gun
                        }

                        paraMiktar += gunlukMaasi * gunFark;
                        esyaMiktar += gunlukEsyaGider * gunFark;

                        String guncelle = "UPDATE kullanicitablo SET kullaniciEsyaMiktar = @kullaniciEsyaMiktar, kullaniciParaMiktar = @kullaniciParaMiktar WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelle, conn);
                        cmd.Parameters.AddWithValue("@kullaniciEsyaMiktar", esyaMiktar);
                        cmd.Parameters.AddWithValue("@kullaniciParaMiktar", paraMiktar);
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        //KULLANİCİ GÜN SAYİ BİR AZALDI, SIFIRLANANA KADAR DEVAM
                        String guncelleGun = "UPDATE calismatablo SET kullaniciCalBasTarih = @kullaniciCalBasTarih WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelleGun, conn);
                        cmd.Parameters.AddWithValue("@kullaniciCalBasTarih", guncelTarih); //cal bas tarihi bugunle değiştir
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        label1.Text = "Yemek = " + dataGridView1.Rows[0].Cells[4].Value.ToString();
                        label2.Text = "Esya = " + dataGridView1.Rows[0].Cells[5].Value.ToString();
                        label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                        MessageBox.Show("Magazada calistigi icin esya miktari sabit kaldi, GUNLUK MAAS VERILDI");
                    }
                    
                }
            }

            else if (isTur == "Market")
            {
                int gunlukYemekGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[17].Value.ToString()); //gunlujk yiyecek gideri
                int yemekMiktar = Convert.ToInt32(dataGridView1.Rows[0].Cells[4].Value.ToString());  //mevcut yemek miktari


                DateTime basTarih = Convert.ToDateTime(dataGridView7.Rows[0].Cells[2].Value.ToString());
                DateTime bitTarih = Convert.ToDateTime(dataGridView7.Rows[0].Cells[3].Value.ToString());

                DateTime guncelTarih = Convert.ToDateTime(dataGridView1.Rows[0].Cells[7].Value.ToString());
                //kullanicinin guncel tarihini aldın

                 if (bitTarih > guncelTarih) //calisma süresi bitmediyse
                 {
                    string[] tarihParcala = guncelTarih.ToString().Split('/');
                    string[] tarihParcala2 = basTarih.ToString().Split('/');
                    int gunlukMaasi = Convert.ToInt32(dataGridView7.Rows[0].Cells[6].Value.ToString());

                    if (tarihParcala[0] == tarihParcala2[0]) // eger aynı aysa
                    {

                        int fark = Convert.ToInt32(tarihParcala[1]) - Convert.ToInt32(tarihParcala2[1]); //gun farkı (AYNI AYSA)

                        paraMiktar += gunlukMaasi * fark;
                        yemekMiktar += gunlukYemekGider * fark;

                        String guncelle = "UPDATE kullanicitablo SET kullaniciYemekMiktar = @kullaniciYemekMiktar, kullaniciParaMiktar = @kullaniciParaMiktar WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelle, conn);
                        cmd.Parameters.AddWithValue("@kullaniciYemekMiktar", yemekMiktar);
                        cmd.Parameters.AddWithValue("@kullaniciParaMiktar", paraMiktar);
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        //KULLANİCİ GÜN SAYİ BİR AZALDI, SIFIRLANANA KADAR DEVAM
                        String guncelleGun = "UPDATE calismatablo SET kullaniciCalBasTarih = @kullaniciCalBasTarih WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelleGun, conn);
                        cmd.Parameters.AddWithValue("@kullaniciCalBasTarih", guncelTarih); //cal bas tarihi bugunle değiştir
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        label1.Text = "Yemek = " + dataGridView1.Rows[0].Cells[4].Value.ToString();
                        label2.Text = "Esya = " + dataGridView1.Rows[0].Cells[5].Value.ToString();
                        label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                        MessageBox.Show("Markette calistigi icin yemek miktari sabit kaldi, GUNLUK MAAS VERILDI");
                    }

                    else if (Convert.ToInt32(tarihParcala[0]) > Convert.ToInt32(tarihParcala2[0])) //farkli aysa
                    {

                        int ayFarki = Convert.ToInt32(tarihParcala[0]) - Convert.ToInt32(tarihParcala2[0]);  // 2 ay farkli
                        gunFark = 0;
                        if (ayFarki == 1)
                        {
                            gunFark += 30 - Convert.ToInt32(tarihParcala2[1]); //onceki ayın artık gununu ekledin
                            gunFark += Convert.ToInt32(tarihParcala[1]); //bu ayın gun sayısını ekledin
                        }
                        else if (ayFarki > 1)
                        {
                            gunFark += 30 - Convert.ToInt32(tarihParcala2[1]); //onceki ayın artık gununu ekledin
                            gunFark += Convert.ToInt32(tarihParcala[1]); //bu ayın gun sayısını ekledin

                            gunFark += (ayFarki - 1) * 30; //aradak ay farkinin 1 eksiği kadar 30 gun
                        }

                        paraMiktar += gunlukMaasi * gunFark;
                        yemekMiktar += gunlukYemekGider * gunFark;

                        String guncelle = "UPDATE kullanicitablo SET kullaniciYemekMiktar = @kullaniciYemekMiktar, kullaniciParaMiktar = @kullaniciParaMiktar WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelle, conn);
                        cmd.Parameters.AddWithValue("@kullaniciYemekMiktar", yemekMiktar);
                        cmd.Parameters.AddWithValue("@kullaniciParaMiktar", paraMiktar);
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        //KULLANİCİ GÜN SAYİ BİR AZALDI, SIFIRLANANA KADAR DEVAM
                        String guncelleGun = "UPDATE calismatablo SET kullaniciCalBasTarih = @kullaniciCalBasTarih WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelleGun, conn);
                        cmd.Parameters.AddWithValue("@kullaniciCalBasTarih", guncelTarih); //cal bas tarihi bugunle değiştir
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        label1.Text = "Yemek = " + dataGridView1.Rows[0].Cells[4].Value.ToString();
                        label2.Text = "Esya = " + dataGridView1.Rows[0].Cells[5].Value.ToString();
                        label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                        MessageBox.Show("Markette calistigi icin yemek miktari sabit kaldi, GUNLUK MAAS VERILDI");


                    }

                    }
            }

            else if (isTur == "Emlak")
            {
                int gunlukParaGider = Convert.ToInt32(dataGridView1.Rows[0].Cells[19].Value.ToString()); //gunlujk yiyecek gideri

                DateTime basTarih = Convert.ToDateTime(dataGridView7.Rows[0].Cells[2].Value.ToString());
                DateTime bitTarih = Convert.ToDateTime(dataGridView7.Rows[0].Cells[3].Value.ToString());

                DateTime guncelTarih = Convert.ToDateTime(dataGridView1.Rows[0].Cells[7].Value.ToString());
                //kullanicinin guncel tarihini aldın

                if (bitTarih > guncelTarih) //calisma süresi bitmediyse
                {
                    string[] tarihParcala = guncelTarih.ToString().Split('/');
                    string[] tarihParcala2 = basTarih.ToString().Split('/');
                    int gunlukMaasi = Convert.ToInt32(dataGridView7.Rows[0].Cells[6].Value.ToString());

                    if (tarihParcala[0] == tarihParcala2[0]) //eger aynı aysa
                    {
                        int fark = Convert.ToInt32(tarihParcala[1]) - Convert.ToInt32(tarihParcala2[1]); //gun farkı (AYNI AYSA)

                        paraMiktar += gunlukMaasi * fark;

                        String guncelle = "UPDATE kullanicitablo SET kullaniciParaMiktar = @kullaniciParaMiktar WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelle, conn);
                        cmd.Parameters.AddWithValue("@kullaniciParaMiktar", paraMiktar);
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        //KULLANİCİ GÜN SAYİ BİR AZALDI, SIFIRLANANA KADAR DEVAM
                        String guncelleGun = "UPDATE calismatablo SET kullaniciCalBasTarih = @kullaniciCalBasTarih WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelleGun, conn);
                        cmd.Parameters.AddWithValue("@kullaniciCalBasTarih", guncelTarih); //cal bas tarihi bugunle değiştir
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        label1.Text = "Yemek = " + dataGridView1.Rows[0].Cells[4].Value.ToString();
                        label2.Text = "Esya = " + dataGridView1.Rows[0].Cells[5].Value.ToString();
                        label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();
                    }

                    else if (Convert.ToInt32(tarihParcala[0]) > Convert.ToInt32(tarihParcala2[0])) //farkli aysa
                    {

                        int ayFarki = Convert.ToInt32(tarihParcala[0]) - Convert.ToInt32(tarihParcala2[0]);  // 2 ay farkli
                        gunFark = 0;
                        if (ayFarki == 1)
                        {
                            gunFark += 30 - Convert.ToInt32(tarihParcala2[1]); //onceki ayın artık gununu ekledin
                            gunFark += Convert.ToInt32(tarihParcala[1]); //bu ayın gun sayısını ekledin
                        }
                        else if (ayFarki > 1)
                        {
                            gunFark += 30 - Convert.ToInt32(tarihParcala2[1]); //onceki ayın artık gununu ekledin
                            gunFark += Convert.ToInt32(tarihParcala[1]); //bu ayın gun sayısını ekledin

                            gunFark += (ayFarki - 1) * 30; //aradak ay farkinin 1 eksiği kadar 30 gun
                        }

                        paraMiktar += gunlukMaasi * gunFark;

                        String guncelle = "UPDATE kullanicitablo SET kullaniciParaMiktar = @kullaniciParaMiktar WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelle, conn);
                        cmd.Parameters.AddWithValue("@kullaniciParaMiktar", paraMiktar);
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        //KULLANİCİ GÜN SAYİ BİR AZALDI, SIFIRLANANA KADAR DEVAM
                        String guncelleGun = "UPDATE calismatablo SET kullaniciCalBasTarih = @kullaniciCalBasTarih WHERE kullaniciNo = @kullaniciNo";
                        cmd = new MySqlCommand(guncelleGun, conn);
                        cmd.Parameters.AddWithValue("@kullaniciCalBasTarih", guncelTarih); //cal bas tarihi bugunle değiştir
                        cmd.Parameters.AddWithValue("@kullaniciNo", kullaniciId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        veriGetir();

                        label1.Text = "Yemek = " + dataGridView1.Rows[0].Cells[4].Value.ToString();
                        label2.Text = "Esya = " + dataGridView1.Rows[0].Cells[5].Value.ToString();
                        label3.Text = "Para = " + dataGridView1.Rows[0].Cells[6].Value.ToString();


                    }


                    }
            }

            else
            {
                MessageBox.Show("calisma sureniz sona ermistir!!!!");

                int isletmeId = Convert.ToInt32(dataGridView7.Rows[0].Cells[8].Value.ToString());
                int isletmeCalisanlari = Convert.ToInt32(dataGridView4.Rows[0].Cells[11].Value.ToString());
                //ISLETME ÇALIŞAN SAYİSİNİ AZALT
                String guncelleCalisan = "UPDATE isletmetablo SET isletmeCalisanSayi = @isletmeCalisanSayi WHERE isletmeId = @isletmeId";
                cmd = new MySqlCommand(guncelleCalisan, conn);
                cmd.Parameters.AddWithValue("@isletmeCalisanSayi", isletmeCalisanlari - 1);
                cmd.Parameters.AddWithValue("@isletmeId", isletmeId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                veriGetir();
                MessageBox.Show("isten ayrildigin icin isletme calisan sayisi azaldi");
            }
           

        }

        int olmek()
        {
            if(Convert.ToInt32(dataGridView1.Rows[0].Cells[4].Value.ToString()) <= 0)
            {
                Label labelBit = new Label();
                labelBit.AutoSize = false;
                labelBit.Size = new Size(1942, 1102);
                labelBit.Location = new Point(0, 0);
                labelBit.BackColor = Color.Black;
                labelBit.ForeColor = Color.White;
                labelBit.Font = new Font("Microsoft YaHei UI", 30F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
                labelBit.Text = "Yemek Bitti\n ÖLDÜN\n";
                labelBit.TextAlign = ContentAlignment.MiddleCenter;
                Controls.Add(labelBit);

                olduMu = 1;
                
            }
            return olduMu;
        }
       
    }
       
}
