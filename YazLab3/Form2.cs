using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YazLab3
{
    public partial class Form2 : Form
    {
        Thread thread;
        public int frame_sayisi;
        Boolean durdu=false;
        int i;
        int sonOkunanFrame;



        public Form2(int sayi)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            frame_sayisi = sayi;
            /*Image img = Image.FromFile(@"C:\Users\erhan\Desktop\yuv samples\YuvResults\" + 1 + ".bmp");
            pictureBox1.Image = img;
            img.Dispose();*/
            Image img;
            using (var bmpTemp = new Bitmap(@"..\\output\" + 1 + ".bmp"))
            {
                img = new Bitmap(bmpTemp);
            }
            pictureBox1.Image = img;
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            progressBar2.Maximum = sayi;
            progressBar2.Value = 1;
            



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            button1.Enabled = false;
            button2.Enabled = true;

            thread = new Thread(new ThreadStart(oku));
                thread.Start();
         






        }
        public void oku()
        {
            int baslangic=1;

            if (durdu)
            {
                baslangic = sonOkunanFrame;
                durdu = false;
                
            }

            for (i = baslangic; i < frame_sayisi; i++)
            {

                /*Image img = Image.FromFile(@"C:\Users\erhan\Desktop\yuv samples\YuvResults\" + i + ".bmp");
               pictureBox1.Image = img;*/
                Image img;
                using (var bmpTemp = new Bitmap(@"..\\output\" + i + ".bmp"))
                {
                    img = new Bitmap(bmpTemp);
                }
                pictureBox1.Image = img;
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Update();

                progressBar2.Value = i;
                label3.Text = Convert.ToString(i);
                
                System.Threading.Thread.Sleep(trackBar1.Value);

            }
            button1.Enabled = true;
            button2.Enabled = false;




        }

        private void button2_Click(object sender, EventArgs e)
        {
            thread.Abort();
            durdu = true;
            sonOkunanFrame = i;
            button2.Enabled = false;
            button1.Enabled = true;
            
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = Convert.ToString(trackBar1.Value);
        }
    }
}
