using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YazLab3
{
    public partial class Form1 : Form
    {
        public String dosyaYolu;
        public int rWidth;
        public int rHeight;
        public byte[] arr;
        public FileStream fs;
        public Bitmap bm;
        public int[,] y;


       

        public Form1()
        {
            InitializeComponent();
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        //Okunacak Dosyayı seçme ve uzantı kontrolü, uzantısı .yuv olmalı.Eğer dosya geçerli ise path değişkene atanır.
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileInfo uzanti = new System.IO.FileInfo(openFileDialog1.FileName);
                Console.WriteLine(uzanti.Extension);

                if (uzanti.Extension == ".yuv") 
                {
                    textBox1.Text = openFileDialog1.FileName;
                    dosyaYolu = openFileDialog1.FileName;
                    button2.Enabled = true;
                }
                else{
                    MessageBox.Show(".yuv uzantılı dosya seçiniz. !!!");

                }
            }

        }
        // Render parse butonudur verilen dosya yolu ile dosyayı okur file stream olarak okunan veriyi byte array e çevirir
        // daha sonra resimlerin oluşturulacağı hedef klasör temizlenir.
        private void button2_Click(object sender, EventArgs e)
        {
            String[] Size = comboBox2.SelectedItem.ToString().Split(' ');
            rWidth = Convert.ToInt32(Size[0]);
            rHeight= Convert.ToInt32(Size[2]);
            Color c = new Color();
            bm = new Bitmap(rWidth, rHeight);

            fs = new FileStream(dosyaYolu,FileMode.Open);
            Console.WriteLine(fs.Length);

            arr = new byte[fs.Length];
            fs.Read(arr, 0, arr.Length);

            fs.Flush();
            fs.Close();
            
           


            //---------------------- FRAMELERİN KAYIT EDİLECEĞİ KLASÖRÜ TEMİZLER --------------------------------------

            System.IO.DirectoryInfo di = new DirectoryInfo(@"..\\output\");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
                
                
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
                
                
            }
            
            
            //----------------------------------------------------------------------------------------------------------

            // Seçilen formata göre byte arrayden okuma işlemi yapılır okuma işleminde y bileşenler her bir framein boyutunda tek boyutlu
            // bir dizi içinde saklanır byte array her frame için yyyy..uuuu..vvvv şeklinde olduğundan frame boyutu kadar y bilgisi okunur
            //daha sonra formata göre u ve v bileşenleri atlanıp diğer framein y bileşenleri okunur.
            int t = 0, a = 1;

            if (comboBox1.SelectedItem == "YUV (4:2:0)")
            {
                t = 0; a = 1;
                while (t < arr.Length)
                {
                    y = new int[rHeight, rWidth];

                    for (int i = 0; i < rHeight; i++)
                    {
                        for (int j = 0; j < rWidth; j++)
                        {
                            try
                            {

                                y[i, j] = Convert.ToInt32(arr[t]);
                                c = Color.FromArgb(255, y[i, j], y[i, j], y[i, j]);
                                bm.SetPixel(j, i, c);
                                t++;
                            }
                            catch
                            {
                                MessageBox.Show("Hatalı Boyut Bilgisi Girdiniz !!!");
                                return;
                            }

                        }
                    }
                    bm.Save(@"..\\output\" + a + ".bmp", ImageFormat.Bmp);
                    t = t + rHeight * rWidth / 2;    // sadece y bileşeni okumak için formata göre u ve v bileşenleri atlandı.
                    a++;
                }
            }


            else if (comboBox1.SelectedItem == "YUV (4:4:4)")
            {
                t = 0; a = 1;
                while (t < arr.Length)
                {
                    y = new int[rHeight, rWidth];

                    for (int i = 0; i < rHeight; i++)
                    {
                        for (int j = 0; j < rWidth; j++)
                        {
                            try
                            {

                                y[i, j] = Convert.ToInt32(arr[t]);
                                c = Color.FromArgb(255, y[i, j], y[i, j], y[i, j]);
                                bm.SetPixel(j, i, c);
                                t++;
                            }
                            catch
                            {
                                MessageBox.Show("Hatalı Boyut Bilgisi Girdiniz !!!");
                                return;
                            }
                        }
                    }
                    bm.Save(@"..\\output\" + a + ".bmp", ImageFormat.Bmp);      
                    t = t + rHeight * rWidth*2;
                    a++;
                }
            }



            else if (comboBox1.SelectedItem == "YUV (4:2:2)")
            {
                t = 0; a = 1;
                while (t < arr.Length)
                {
                    y = new int[rHeight, rWidth];

                    for (int i = 0; i < rHeight; i++)
                    {
                        for (int j = 0; j < rWidth; j++)
                        {
                            try
                            {

                                y[i, j] = Convert.ToInt32(arr[t]);
                                c = Color.FromArgb(255, y[i, j], y[i, j], y[i, j]);
                                bm.SetPixel(j, i, c);
                                t++;
                            }
                            catch
                            {
                                MessageBox.Show("Hatalı Boyut Bilgisi Girdiniz !!!");
                                return;
                            }
                        }
                    }
                    bm.Save(@"..\\output\" + a + ".bmp", ImageFormat.Bmp);       
                    t = t + rHeight * rWidth;
                    a++;
                }
            }


            else
            {
                MessageBox.Show("Geçersiz Format !!!");

            }
            
          
            new Form2(a).Show();
            
            }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
