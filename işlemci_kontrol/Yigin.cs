using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace işlemci_kontrol
{
    public class RandomNumberGenerator
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber()
        {
            lock (syncLock)
            {
                return random.Next(0,6);
            }
        }
    }
    public class Kuyruk
    {
        public Node bas_k;
        public Node son_k;
        public Kuyruk() 
        {
            bas_k=null;
            son_k=null;
        }

        public int enqueue(string a)
        {
            int s = RandomNumberGenerator.GetRandomNumber();
            string p= a+s.ToString();
            Node n = new Node(p);
            if (bas_k == null)
            {
                bas_k = son_k = n;
            } 
            else
            {
                son_k.sonraki = n;    
                n.onceki = son_k;   
                son_k = n;
            }
            return s;
        }
        public int dequeue()
        {
            int s = -1;
            if (bas_k != null)
            {
                if (bas_k.sonraki == null)
                {
                    string metin = bas_k.p;
                    string pattern = @"- (\d+)";    // Düzenli ifade: '-' karakterinden sonra bir veya daha fazla sayı
                    Match match = Regex.Match(metin, pattern);
                    if (match.Success)
                    {
                        s = int.Parse(match.Groups[1].Value);
                    }
                    bas_k = null;
                }
                else
                {
                    Node temp = bas_k;
                    string metin = temp.p;
                    string pattern = @"- (\d+)";   // Düzenli ifade: '-' karakterinden sonra bir veya daha fazla sayı
                    Match match = Regex.Match(metin, pattern);
                    if (match.Success)
                    {
                        s = int.Parse(match.Groups[1].Value);
                    }
                    bas_k = temp.sonraki;     
                    if (bas_k != null)
                    {
                        bas_k.onceki = null;    
                    }
                }
            }
            return s;
        }
        public bool kontrol()
        {
            if(son_k != null)
            {
                return true;
            }
            return false;
        }
        public void yazdir(TextBox t)
        {
            t.Clear();
            Node temp = bas_k;
            string a = "";
            if (bas_k != null)
            {
                while (temp != null)
                {
                    a += temp.p + "\n";
                    temp = temp.sonraki;
                }
                t.Text = a;
            }
        }
    }
    public class Kuyruk_islem
    {
        public Node bas_k;
        public Node son_k;
        public Kuyruk_islem()
        {
            bas_k = null;
            son_k = null;
        }
        public void enqueue(string p)
        {
            Node n = new Node(p);
            if (bas_k == null)
            {
                bas_k = son_k = n;
            }
            else
            {
                son_k.sonraki = n;
                n.onceki = son_k;
                son_k = n;
            }
        }
        public string dequeue()
        {
            string veri = null;
            if (bas_k != null)
            {
                if (bas_k == son_k)
                {
                    veri = bas_k.p;
                    bas_k = null;
                    son_k = null;
                    return veri;
                }
                else
                {
                    Node temp = bas_k;
                    veri = temp.p;
                    bas_k = temp.sonraki;      
                    if (bas_k != null)
                    {
                        bas_k.onceki = null;   
                    }
                    return veri;
                }
            }
            return veri;
        }
        public void yazdir(TextBox t)
        {
            t.Clear();
            Node temp = bas_k;
            string a = "";
            if (bas_k != null)
            {
                while (temp != null)
                {
                    a += temp.p + "\n";
                    temp = temp.sonraki;
                }
                t.Text = a;
            }
        }
    }
    public class Yigin
    {
        public Node bas_y;
        public Node son_y;
        public Yigin()
        {
            bas_y = null;
            son_y = null;
        }
        public void push(string p1, string p2, string p3)
        {
            Node n = new Node(p1, p2, p3);
            if (bas_y == null)
            {
                bas_y = son_y = n;
            }
            else
            {
                n.sonraki = bas_y;
                bas_y = n;
            }
        }
        public void yazdir(TextBox t1, TextBox t2, TextBox t3, CheckBox c1, CheckBox c2, CheckBox c3)
        {
            t1.Clear();
            t2.Clear();
            t3.Clear();
            Node temp = bas_y;
            if (bas_y != null)
            {
                while (temp != null)
                {
                    string satir1 = "";
                    string satir2 = "";
                    string satir3 = "";
                    if (c1.Checked && c2.Checked == false && c3.Checked == false)
                    {
                        satir1 += temp.p1 + "\n";
                        t1.AppendText(satir1);
                    }
                    else if (c2.Checked && c1.Checked == false && c3.Checked == false)
                    {
                        satir2 += temp.p2 + "\n";
                        t2.AppendText(satir2);
                    }
                    else if (c3.Checked && c2.Checked == false && c1.Checked == false)
                    {
                        satir3 += temp.p3 + "\n";
                        t3.AppendText(satir3);
                    }
                    else if (c1.Checked && c2.Checked && c3.Checked == false)
                    {
                        satir1 += temp.p1 + "\n";
                        satir2 += temp.p2 + "\n";
                        t1.AppendText(satir1);
                        t2.AppendText(satir2);
                    }
                    else if (c1.Checked && c3.Checked && c2.Checked == false)
                    {
                        satir1 += temp.p1 + "\n";
                        satir3 += temp.p3 + "\n";
                        t1.AppendText(satir1);
                        t3.AppendText(satir3);
                    }
                    else if (c2.Checked && c3.Checked && c1.Checked == false)
                    {
                        satir2 += temp.p2 + "\n";
                        satir3 += temp.p3 + "\n";
                        t2.AppendText(satir2);
                        t3.AppendText(satir3);
                    }
                    else if (c2.Checked && c3.Checked && c1.Checked)
                    {
                        satir1 += temp.p1 + "\n";
                        satir2 += temp.p2 + "\n";
                        satir3 += temp.p3 + "\n";
                        t1.AppendText(satir1);
                        t2.AppendText(satir2);
                        t3.AppendText(satir3);
                    }
                    temp = temp.sonraki;
                }
            }
        }
    }
}
