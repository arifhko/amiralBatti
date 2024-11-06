using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asdasdas
{
    public partial class Form1 : Form
    {
        SoundPlayer sPlayer = new SoundPlayer(Resource2.heavy_cineamtic_hit_166888__1_);
        private List<string> basariliHamle = new List<string>();
        private Timer timer;
        private Button[,] butonlar;
        private int[,] tahta;
        private int kalanGemiler;
        private int adimlar;
        private DateTime baslangicVakti;
        int sayac = 1;
        private Dictionary<Button, PictureBox> alevPictureBoxes;

        public Form1()
        {
            InitializeTimer();
            InitializeComponent();
            InitializeGame();
            alevPictureBoxes = new Dictionary<Button, PictureBox>();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializeGame()
        {
            butonlar = new Button[15, 15];
            tahta = new int[15, 15];
            kalanGemiler = 20;
            adimlar = 0;
            baslangicVakti = DateTime.Now;

            for (int i = 0; i < 15; i++)
            {
                Label yeniLabel = new Label();
                yeniLabel.Size = new Size(60, 60);
                yeniLabel.Text = "" + (i + 1);
                yeniLabel.TextAlign = ContentAlignment.MiddleCenter;
                yeniLabel.Name = "label" + (i + 1);
                yeniLabel.Font = new System.Drawing.Font("Bold", 16, System.Drawing.FontStyle.Bold);
                yeniLabel.Location = new System.Drawing.Point(0, 60 + i * 60);
                this.Controls.Add(yeniLabel);
            }



            for (int i = 0; i < 15; i++)
            {
                Label yeniLabel = new Label();
                yeniLabel.Size = new Size(60, 60);
                yeniLabel.Text = "" + (i + 1);
                yeniLabel.TextAlign = ContentAlignment.MiddleCenter;
                yeniLabel.Name = "label" + (i + 16);
                yeniLabel.Font = new System.Drawing.Font("Bold", 16, System.Drawing.FontStyle.Bold);
                yeniLabel.Location = new System.Drawing.Point(60 + i * 60, 0);
                this.Controls.Add(yeniLabel);
            }


            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    butonlar[i, j] = new Button();
                    butonlar[i, j].Size = new Size(60, 60);
                    butonlar[i, j].Location = new Point(i * 60 + 60, j * 60 + 60);
                    butonlar[i, j].Tag = new Point(i, j);
                    butonlar[i, j].Click += Button_Click;
                    Controls.Add(butonlar[i, j]);
                }
            }


            Label sayac = new Label();
            sayac.Size = new Size(70, 28);
            sayac.Text = adimlar.ToString();
            sayac.Name = "label" + 31;
            sayac.TextAlign = ContentAlignment.BottomCenter;
            sayac.Location = new System.Drawing.Point(1530, 198);
            sayac.Font = new System.Drawing.Font("Bold", 20, System.Drawing.FontStyle.Bold);
            this.Controls.Add(sayac);

            Label isim = new Label();
            isim.Size = new Size(370, 25);
            isim.Name = "label" + 32;
            isim.Text = "HAMLE SAYISI:";
            isim.TextAlign = ContentAlignment.BottomCenter;
            isim.Font = new System.Drawing.Font("Bold", 18, System.Drawing.FontStyle.Bold);
            isim.Location = new System.Drawing.Point(1250, 200);
            this.Controls.Add(isim);

            Label sure = new Label();
            sure.Size = new Size(150, 25);
            sure.Name = "label" + 33;
            sure.Text = "SANİYE:";
            sure.TextAlign = ContentAlignment.BottomCenter;
            sure.Font = new System.Drawing.Font("Bold", 18, System.Drawing.FontStyle.Bold);
            sure.Location = new System.Drawing.Point(1320, 300);
            this.Controls.Add(sure);

            Label saniye = new Label();
            saniye.Size = new Size(270, 58);
            saniye.Text = "0";
            saniye.Name = "label" + 34;
            saniye.TextAlign = ContentAlignment.MiddleCenter;
            saniye.Location = new System.Drawing.Point(1420, 285);
            saniye.Font = new System.Drawing.Font("Bold", 18, System.Drawing.FontStyle.Bold);
            this.Controls.Add(saniye);

            Label labelX = new Label();
            labelX.Size = new Size(19, 19);
            labelX.Text = "X";
            labelX.Name = "label" + 36;
            labelX.Font = new System.Drawing.Font("Bold", 12, System.Drawing.FontStyle.Bold);
            labelX.Location = new System.Drawing.Point(44, 21);
            this.Controls.Add(labelX);

            Label labelY = new Label();
            labelY.Size = new Size(19, 19);
            labelY.Text = "Y";
            labelY.Name = "label" + 37;
            labelY.Font = new System.Drawing.Font("Bold", 12, System.Drawing.FontStyle.Bold);
            labelY.Location = new System.Drawing.Point(21, 44);
            this.Controls.Add(labelY);

            gemileriYerlestir();
        }

        private void gemileriYerlestir()
        {
            Random random = new Random();

            for (int gemiBoyutu = 4; gemiBoyutu >= 1; gemiBoyutu--)
            {
                for (int gemiSayaci = 0; gemiSayaci < 5 - gemiBoyutu; gemiSayaci++)
                {
                    bool gemiYerlestiMi = false;

                    while (!gemiYerlestiMi)
                    {
                        int x = random.Next(15);
                        int y = random.Next(15);
                        bool dikeyMi = random.Next(2) == 0;

                        if (yerlesebiliyorMu(x, y, gemiBoyutu, dikeyMi))
                        {
                            gemiYerlestir(x, y, gemiBoyutu, dikeyMi);
                            gemiYerlestiMi = true;
                        }
                    }

                    if (gemiYerlestiMi)
                    {
                        sayac++;
                    }
                }
            }
        }

        private bool yerlesebiliyorMu(int x, int y, int gemiBoyutu, bool dikeyMi)
        {
            if (dikeyMi)
            {
                if (x + gemiBoyutu > 15)
                    return false;

                for (int i = y - 1; i < y + 2; i++)
                {
                    for (int j = x - 1; j <= x + gemiBoyutu; j++)
                    {
                        if (j >= 0 && i >= 0 && j < 15 && i < 15)
                        {
                            if (tahta[j, i] > 0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {
                if (y + gemiBoyutu > 15)
                    return false;

                for (int i = x - 1; i < x + 2; i++)
                {
                    for (int j = y - 1; j <= y + gemiBoyutu; j++)
                    {
                        if (j >= 0 && i >= 0 && j < 15 && i < 15)
                        {
                            if (tahta[i, j] != 0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void gemiYerlestir(int x, int y, int gemiBoyutu, bool dikeyMi)
        {
            for (int i = 0; i < gemiBoyutu; i++)
            {
                if (dikeyMi)
                {
                    tahta[x + i, y] = sayac;
                }
                else
                {
                    tahta[x, y + i] = sayac;
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button tiklananButon = (Button)sender;
            Point location = (Point)tiklananButon.Tag;

            if (tahta[location.X, location.Y] > 0)
            {
                sPlayer.Play();
                tiklananButon.Enabled = false;
                AlevEfekti(tiklananButon);
                vurulduktanSonraGuncelle(location.X, location.Y, tahta[location.X, location.Y]);
                butonlarıKapat(location.X, location.Y, tahta[location.X, location.Y]);
                tahta[location.X, location.Y] = 0;
                kalanGemiler--;
                adimlar++;
                oyunuDenetle();
                basariliHamleEkle(location.X + 1, location.Y + 1);
            }
            else
            {
                tiklananButon.BackColor = Color.LightSkyBlue;
                butonlar[location.X,location.Y].Enabled = false;
                adimlar++;
            }
            SayacLabeliniGuncelle();
        }
        private void AlevEfekti(Button button)
        {
            PictureBox alevPictureBox = new PictureBox();
            alevPictureBox.Image = Resource1.patlama_hareketli_resim_0007;
            alevPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            alevPictureBox.Size = button.Size;
            alevPictureBox.Location = button.Location;

            this.Controls.Add(alevPictureBox);
            alevPictureBox.BringToFront();

            alevPictureBoxes.Add(button, alevPictureBox);


            Task.Run(async () =>
            {
                while (button.BackColor == Color.Red)
                {
                    await Task.Delay(50);

                    alevPictureBox.BringToFront();
                }
            });
        }
        private void basariliHamleEkle(int x, int y)
        {
            string move = "X" + x + "\tY" + y;
            basariliHamle.Add(move);
            listboxGuncelle();
        }

        private void listboxGuncelle()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("BAŞARILI HAMLELER:");

            foreach (string move in basariliHamle)
            {
                listBox1.Items.Add(move);
            }
        }

        private void SayacLabeliniGuncelle()
        {
            Label sayacLabeli = (Label)this.Controls.Find("label31", true).FirstOrDefault();

            if (sayacLabeli != null)
            {
                sayacLabeli.Text = adimlar.ToString();

            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan gecenVakit = DateTime.Now - baslangicVakti;
            Label saniyeLabel = (Label)this.Controls.Find("label34", true).FirstOrDefault();

            if (saniyeLabel != null)
            {
                saniyeLabel.Text = gecenVakit.TotalSeconds.ToString("F2");
            }
            if (kalanGemiler == 0)
            {
                timer.Stop();
            }
        }

        private void vurulduktanSonraGuncelle(int x, int y, int deger)
        {
            for (int i = Math.Max(0, x - 1); i <= Math.Min(14, x + 1); i++)
            {
                for (int j = Math.Max(0, y - 1); j <= Math.Min(14, y + 1); j++)
                {
                    if (tahta[i, j] == 0)
                    {
                        tahta[i, j] = -deger;
                    }
                }
            }
        }

        private void butonlarıKapat(int x, int y, int deger)
        {
            int fontSize = 37;
            butonlar[x, y].Text = "X";
            butonlar[x, y].Font = new Font(butonlar[x, y].Font.FontFamily, fontSize);
            deger = -deger;

            bool gemiBattiMi = true;

            for (int i = Math.Max(0, x - 1); i <= Math.Min(14, x + 1); i++)
            {
                for (int j = Math.Max(0, y - 1); j <= Math.Min(14, y + 1); j++)
                {
                    if (tahta[i, j] > 0 && butonlar[i, j].Enabled == true)
                    {
                        gemiBattiMi = false;
                        break;
                    }
                }
                if (!gemiBattiMi)
                    break;
            }

            if (gemiBattiMi)
            {
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (tahta[i, j] == deger)
                        {
                            butonlar[i, j].Enabled = false;
                            butonlar[i, j].BackColor = Color.Gray;
                        }
                    }
                }
            }
        }

        private void oyunuDenetle()
        {
            if (kalanGemiler == 0)
            {
                oyunuBitir();
            }
        }

        private int puan(int adimlar)
        {
            TimeSpan gecenVakit = DateTime.Now - baslangicVakti;
            int puan = 30000;
            puan -= adimlar * 100;
            int saniye = (int)Math.Round(gecenVakit.TotalSeconds);

            puan -= saniye * 20;
            if (puan < 0)
            {
                puan = 0;
            }
            return puan;
        }

        private void oyunuBitir()
        {
            int x;

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    butonlar[i, j].Enabled = false;
                }
            }
            puan(adimlar);
            x = puan(adimlar);
            Label puan1 = new Label();
            puan1.Size = new Size(270, 58);
            puan1.Text = "PUAN:" + x;
            puan1.Name = "label" + 35;
            puan1.TextAlign = ContentAlignment.MiddleCenter;
            puan1.Location = new System.Drawing.Point(1280, 385);
            puan1.Font = new System.Drawing.Font("Bold", 18, System.Drawing.FontStyle.Bold);
            this.Controls.Add(puan1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.AutoSize = true;
            button1.Text = "Yeniden Başla";
            listBox1.Items.Insert(0, "BAŞARILI HAMLELER:");
            listBox1.Font = new Font("Bold", 13);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}