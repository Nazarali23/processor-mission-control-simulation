using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace işlemci_kontrol
{
    public partial class Form1 : Form
    {
        Kuyruk kuyruk1 = new Kuyruk();
        Kuyruk kuyruk2 = new Kuyruk();
        Kuyruk kuyruk3 = new Kuyruk();
        Kuyruk_islem kuyruk_İslem = new Kuyruk_islem();
        Yigin yigin = new Yigin();
        public Form1()
        {
            InitializeComponent();
            panel4.Location = new Point((ClientSize.Width - panel4.Width) / 2,
                                       (ClientSize.Height - panel4.Height) / 2);
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                kuyruk1.enqueue("Proses_1 - ");
                kuyruk1.yazdir(textBox1);
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timer2.Enabled == true)
            {
                kuyruk2.enqueue("Proses_2 - ");
                kuyruk2.yazdir(textBox2);
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (timer3.Enabled == true)
            {
                kuyruk3.enqueue("Proses_3 - ");
                kuyruk3.yazdir(textBox3);
            }
        }
        private void islemci_hiz_Tick(object sender, EventArgs e)
        {
            if (islemci_hiz.Enabled == true)
            {
                if (kuyruk_İslem.bas_k != null)
                {
                    ayristirmaVeYiginaEkleme();
                    yigin.yazdir(textBox5, textBox6, textBox7, checkBox1, checkBox2, checkBox3);
                }
            }
        }
        private void baslat_Click(object sender, EventArgs e)
        {
            islemci_hiz.Enabled = !islemci_hiz.Enabled;
            if (baslat.BackColor == Color.LawnGreen)
            {
                baslat.BackColor = Color.Red;
                baslat.Text = "İŞLEMCİ KAPALI";
                if (timer1.Interval > 200) { timer1.Interval -= 200; }
                if (timer2.Interval > 200) { timer2.Interval -= 200; }
                if (timer3.Interval > 200) { timer3.Interval -= 200; }
            }
            else
            {
                baslat.BackColor = Color.LawnGreen;
                baslat.Text = "İŞLEMCİ AÇIK";
                timer.Interval -= 200;
                timer1.Interval += 200;
                timer2.Interval += 200;
                timer3.Interval += 200;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            timer2.Start();
            timer2.Enabled = true;
            timer3.Start();
            timer3.Enabled = true;
            timer.Start();
            timer.Enabled = true;
        }
        private void p1_hız_bar_Scroll(object sender, EventArgs e)
        {
            if (p1_hız_bar.Value > 0)
            {
                timer1.Start();
                timer1.Enabled = true;
                timer1.Interval = 1300;
                timer1.Interval -= p1_hız_bar.Value * 100;
            }
            else
            {
                timer1.Enabled = false;
                timer1.Stop();
            }
        }
        private void p2_hız_bar_Scroll(object sender, EventArgs e)
        {
            if (p2_hız_bar.Value > 0)
            {
                timer2.Start();
                timer2.Enabled = true;
                timer2.Interval = 1300;
                timer2.Interval -= p2_hız_bar.Value * 100;
            }
            else
            {
                timer2.Enabled = false;
                timer2.Stop();
            }
        }
        private void p3_hız_bar_Scroll(object sender, EventArgs e)
        {
            if (p3_hız_bar.Value > 0)
            {
                timer3.Start();
                timer3.Enabled = true;
                timer3.Interval = 1300;
                timer3.Interval -= p3_hız_bar.Value * 100;
            }
            else
            {
                timer3.Enabled = false;
                timer3.Stop();
            }
        }
        private void islemci_hiz_bar_Scroll(object sender, EventArgs e)
        {
            islemci_hiz.Interval = 110;
            islemci_hiz.Interval -= islemci_hiz_bar.Value * 10;
        }
        private async void timer_Tick(object sender, EventArgs e)
        {
            int t1 = 1, t2 = 1, t3 = 1, i = 0;
            if (timer1.Enabled == true) { t1 = timer1.Interval; i++; }
            if (timer2.Enabled == true) { t2 = timer2.Interval; i++; }
            if (timer3.Enabled == true) { t3 = timer3.Interval; i++; }
            if(i == 0) { i=1; }
            timer.Interval = (t1 + t2 + t3) / i + 300;
            islem_onceligi();
            kuyruk_İslem.yazdir(textBox4);
            kuyruk1.yazdir(textBox1);
            kuyruk2.yazdir(textBox2);
            kuyruk3.yazdir(textBox3);
            if (textBox4.TextLength > 1000) 
            { textBox4.BackColor=Color.Orange; await Task.Delay(400) ; textBox4.BackColor = Color.Red; }
            else { textBox4.BackColor=Color.LightGreen; }
            if (textBox1.TextLength > 500) { textBox1.BackColor = Color.LightYellow; textBox1.ForeColor = Color.Red; }
            else { textBox1.BackColor = Color.Black; textBox1.ForeColor = Color.LimeGreen; }
            if (textBox2.TextLength > 500) { textBox2.BackColor = Color.LightYellow; textBox2.ForeColor = Color.Red; }
            else { textBox2.BackColor = Color.Black; textBox2.ForeColor = Color.LimeGreen; }
            if (textBox3.TextLength > 500) { textBox3.BackColor = Color.LightYellow; textBox3.ForeColor = Color.Red; }
            else { textBox3.BackColor = Color.Black; textBox3.ForeColor = Color.LimeGreen; }
        }
        public void islem_onceligi()
        {
            int s1 = 0, s2 = 0, s3 = 0;
            bool b1 = kuyruk1.kontrol();
            bool b2 = kuyruk2.kontrol();
            bool b3 = kuyruk3.kontrol();
            if (b1 == true) { s1 = kuyruk1.dequeue(); }
            if (b2 == true) { s2 = kuyruk2.dequeue(); }
            if (b3 == true) { s3 = kuyruk3.dequeue(); }
            if (s1 > -1 || s2 > -1 || s3 > -1)
            {
                int enBuyuk = Math.Max(s1, Math.Max(s2, s3));
                if (enBuyuk == s1)
                {
                    if (s1 > -1) { kuyruk_İslem.enqueue(" P_1-" + s1 + "--> "); }
                    if (s2 > s3)
                    {
                        if (s2 > -1) { kuyruk_İslem.enqueue(" P_2-" + s2 + "--> "); }
                        if (s3 > -1) { kuyruk_İslem.enqueue(" P_3-" + s3 + "--> "); }
                    }
                    else
                    {
                        if (s3 > -1) { kuyruk_İslem.enqueue(" P_3-" + s3 + "--> "); }
                        if (s2 > -1) { kuyruk_İslem.enqueue(" P_2-" + s2 + "--> "); }
                    }
                }
                else if (enBuyuk == s2)
                {
                    if (s2 > -1) { kuyruk_İslem.enqueue(" P_2-" + s2 + "--> "); }
                    if (s1 > s3)
                    {
                        if (s1 > -1) { kuyruk_İslem.enqueue(" P_1-" + s1 + "--> "); }
                        if (s3 > -1) { kuyruk_İslem.enqueue(" P_3-" + s3 + "--> "); }
                    }
                    else
                    {
                        if (s3 > -1) { kuyruk_İslem.enqueue(" P_3-" + s3 + "--> "); }
                        if (s1 > -1) { kuyruk_İslem.enqueue(" P_1-" + s1 + "--> "); }
                    }
                }
                else if (enBuyuk == s3)
                {
                    if (s3 > -1) { kuyruk_İslem.enqueue(" P_3-" + s3 + "--> "); }
                    if (s1 > s2)
                    {
                        if (s1 > -1) { kuyruk_İslem.enqueue(" P_1-" + s1 + "--> "); }
                        if (s2 > -1) { kuyruk_İslem.enqueue(" P_2-" + s2 + "--> "); }
                    }
                    else
                    {
                        if (s2 > -1) { kuyruk_İslem.enqueue(" P_2-" + s2 + "--> "); }
                        if (s1 > -1) { kuyruk_İslem.enqueue(" P_1-" + s1 + "--> "); }
                    }
                }
            }
        }
        public void ayristirmaVeYiginaEkleme()
        {
            string m = kuyruk_İslem.dequeue();
            if (m != null)
            {
                string desen = @"P_(\d)-"; // P_ ile başlayan ve bir veya daha fazla rakam içeren kısmı yakalar
                Match match = Regex.Match(m, desen);
                if (match.Success)
                {
                    int s = int.Parse(match.Groups[1].Value);
                    string value = m;
                    switch (s)
                    {
                        case 1:
                            yigin.push(value, null, null);
                            break;
                        case 2:
                            yigin.push(null, value, null);
                            break;
                        case 3:
                            yigin.push(null, null, value);
                            break;
                    }
                }
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
