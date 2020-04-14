using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yilan_oyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

        }





       yilan yilanimiz;
        Yon yonumuz;

        PictureBox[] pb_yilanparcalari;
        bool yem_varmi = false;
        Random rasg = new Random();
        PictureBox pb_yem;
        int skor = 0;


        private void Form1_Load(object sender, EventArgs e)
        {

            Yeni_oyun();
        }
        private void Yeni_oyun()
        {
            yilanimiz = new yilan();
            yonumuz = new Yon(-10, 0);

            pb_yilanparcalari = new PictureBox[0];
            for (int i = 0; i < 3; i++)
            {
                Array.Resize(ref pb_yilanparcalari, pb_yilanparcalari.Length + 1);
                pb_yilanparcalari[i] = Pb_ekle();
            }
            timer1.Start();
            btn_rest.Enabled = false;
        }


        private PictureBox Pb_ekle()
             
        {
            
            PictureBox pb = new PictureBox();
            pb.Size = new Size(10, 10);
            pb.BackColor = Color.GreenYellow;
            pb.Location = yilanimiz.GetPos(pb_yilanparcalari.Length-1);
            panel1.Controls.Add(pb);
            return(pb);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void pb_guncelle()
        {
            for (int i = 0; i < pb_yilanparcalari.Length; i++)
            {
                pb_yilanparcalari[i].Location = yilanimiz.GetPos(i);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            { if (yonumuz._y != 10)
                { 
                    yonumuz = new Yon(0, -10); 
                }
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                if (yonumuz._y != -10)
                {
                    yonumuz = new Yon(0, +10);
                }
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (yonumuz._x != 10)
                {
                    yonumuz = new Yon(-10, 0);
                }
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if(yonumuz._x != -10){
                    yonumuz = new Yon(+10, 0); }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Skor :" + skor.ToString();
            yilanimiz.İlerle(yonumuz);
            pb_guncelle();
            Yem_olustur();
            Yem_yedi_mi();
            yilan_kendine_carpti();
            Duvarlara_carpma();
           
        }
        public void Yem_olustur()
        {
            if (!yem_varmi)
            {
                PictureBox pb = new PictureBox();
                pb.BackColor = Color.Red;
                pb.Size = new Size(20, 20);
                pb.Location = new Point(rasg.Next(panel1.Width / 10) * 10, rasg.Next(panel1.Height / 10) * 10);
                yem_varmi = true;
                pb_yem = pb;
                panel1.Controls.Add(pb);
            }

        }
        public void Yem_yedi_mi()
        {
            if (yilanimiz.GetPos(0) == pb_yem.Location)
            {
                yilanimiz.Buyu();
                Array.Resize(ref pb_yilanparcalari, pb_yilanparcalari.Length + 1);
                pb_yilanparcalari[pb_yilanparcalari.Length - 1] = Pb_ekle();
                yem_varmi = false;
                panel1.Controls.Remove(pb_yem);
                skor += 10;
            }
        }
        public void yilan_kendine_carpti()
        {
            for (int i = 1; i < yilanimiz.Yilan_buyuklugu; i++)
            {
                if (yilanimiz.GetPos(0) == yilanimiz.GetPos(i))
                {
                    yeniden_basla();
                }
            }
            
        }
        public void Duvarlara_carpma()
        {
            Point p = yilanimiz.GetPos(0);
            if(p.X<0||p.X>panel1.Width-10 || p.Y<0 || p.Y > panel1.Height - 10)
            {
                yeniden_basla();
            }
        }
        private void yeniden_basla()
        {
            timer1.Stop();
            btn_rest.Enabled = true;
            MessageBox.Show("Kaybettin Skorun:" + skor.ToString());
        }

        private void btn_rest_Click(object sender, EventArgs e)
        {

            skor = 0;
            panel1.Controls.Clear();
            Yeni_oyun();
             panel1.Controls.Add(pb_yem);
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    }
    

