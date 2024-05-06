using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
namespace HafizaOyunu
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();// Iconlari rastgele dagitmak icin olusturuldu 
        List<string> icons = new List<string>()
        {
            "-", "-", "N", "N", ".", ".", "K", "K",
            "%", "%", "V", "V", "w", "w", "s", "s"
        };// İcon olusturulmak icin olusturuldu

        Button first, second;

        public Form1()
        {
            InitializeComponent();

            //Ekran acildiginda kartlari 4 saniye gösterir.
            timer1.Start();
            bilgi.Text = "Oyun Basliyor...";
            timer1.Interval = 4000;
            
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            Button btn;

            //Butonlari okuma islemi
            foreach(Button x in Controls.OfType<Button>())
            {
                btn = x as Button;

                //Butona icon atama
                int rndIndex = rnd.Next(icons.Count);

                btn.Text = icons[rndIndex];

                //Buton renkleri olusturuldu
                btn.ForeColor = Color.Black;

                btn.BackColor = Color.White;

                //Tekrara dusmemek icin listede kullanılan eleman silindi
                icons.RemoveAt(rndIndex);
            }   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer3.Start();
            label2.Text = "Kalan Sure:";
            

            bilgi.Text = "Ayni iki karti bul!!";

            //Baslangıcta kartlar gosterildikten sonra butun kartlari kapatma islemi
            foreach(Button x in Controls.OfType<Button>())
            {
                x.ForeColor = Color.Gray;
                x.BackColor = x.ForeColor;
            }
   
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            bilgi.Text = "Tekrar Dene!!";

            //Yanlis tahmin yapildigi icin kartlar kapatilir
            first.ForeColor = second.ForeColor = Color.Gray;
            first.BackColor = second.BackColor = Color.Gray;
            first = null;
            second = null;

        }

        double sure = 900.0;//Oyun süresi
        
        private void timer3_Tick(object sender, EventArgs e)
        {
            double kalanSure = (sure * 0.1);
            if (sure > 0)
            {
                sure--;
                label1.Text = Convert.ToInt32(kalanSure).ToString();
            }
            else
            {
                timer3.Stop();
                MessageBox.Show("Sure Doldu.Kaybettiniz!!");
                
                Close();
            }
        }

        int sayac = 0; // Butun tahminler bittikten sonra oyun bitirmek icin olusturuldu
        private void button17_Click(object sender, EventArgs e)
        {
            //Tiklanan butonu okur
            Button button = sender as Button;

            if(first == null)
            {
                //Ilk kart acilir
                first = button;
                first.ForeColor = Color.Black;
                first.BackColor = Color.White;
                button.Enabled = false;
                return;

            }
            //Ikinci karti acar
            second = button;
            second.ForeColor = Color.Black;
            second.BackColor = Color.White;
            button.Enabled = false;

            //Kartlarin esitligi sorgulanir
            if(first.Text == second.Text)
            {
                first = null;
                second = null;

                bilgi.Text = "Dogru!!";
                sayac++;
                //Toplam tahmin sayisi 8 dir.
                if(sayac == 8)
                {
                    timer3.Stop();
                    bilgi.Text = "Oyun Bitti!!";
                    MessageBox.Show("Tebrikler Oyunu Kazandiniz");
                    Close();
                }               
            }
            else
            {
                button = first;
                button.Enabled = true;
                button = second;
                button.Enabled = true;
                //Yanlis tahmin durumunda kartlari kapatmak icin kullanilir.
                timer2.Start();
                timer2.Interval = 400;

            }

        }
    }  
}

