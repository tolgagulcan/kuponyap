using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DraggableControls;
using System.IO;
using System.Linq;
using System.Diagnostics;
using WindowsFormsApplication2;
using static WindowsFormsApplication2.tanimlamalar;
namespace WindowsFormsApplication2
{
    public partial class kuponolustur : Form
    {
        sonuc[] startup = new sonuc[15];
        sonuc[] kupon = new sonuc[15] { sonuc.m0, sonuc.m0, sonuc.m0, sonuc.m0,
            sonuc.m0, sonuc.m0, sonuc.m0, sonuc.m0, sonuc.m0, sonuc.m0, sonuc.m0,
            sonuc.m0, sonuc.m0, sonuc.m0, sonuc.m0 };
        public List<sonuc[]> kuponlar;
        List<macfiltre> fl = new List<macfiltre>();
        string filtrefile, ilkfile;
        public kuponolustur()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            initform();
            // doldur();
        }
        public void initform()
        {
            filtrefile = Application.StartupPath + "\\filtreler.txt";
            ilkfile = Application.StartupPath + "\\ilksecim.txt";
            ilksecim.Draggable(true);
            kuponlar = new List<sonuc[]>();
            //Bu kod Usercontrol da 1 den 15 e kadar doldurur ve disabled yapar
            for (int i = 0; i < 15; i++)
            {
                ilksecim.ls[i, 0].Text = (i + 1).ToString(); ilksecim.ls[i, 0].Enabled = false;
            }
            //
            //Bu kod form kapanmadan önce kaydettiği filtre kutusunu yeniden doldurur.
            string line;
            if (File.Exists(filtrefile))
            {
                int counter = 0;
                System.IO.StreamReader file = new System.IO.StreamReader(filtrefile);
                while ((line = file.ReadLine()) != null)
                {
                    rt1.AppendText(line + Environment.NewLine);
                    counter++;
                }
                file.Close();
                rt1.Text = rt1.Text.Trim();
            }
            //
            if (File.Exists(ilkfile))
            {
                List<string> kk = new List<string>();
                System.IO.StreamReader file1 = new System.IO.StreamReader(ilkfile);
                for (int i = 0; i < 15; i++)
                {
                    line = file1.ReadLine();
                    kk.Add(line);
                }
                ilksecim.kupondoldur(kk);
                file1.Close();
            }
        }
        private void ilksecim_Load(object sender, EventArgs e)
        {
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            UserControl1 a = new UserControl1();
            this.Controls.Add(a);
        }
        public macfiltre filtreolustur(string line)
        {
            string[] fline = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            macfiltre[] k = new macfiltre[fline.Length];
            for (int i = 0; i < fline.Length; i++)
            {
                k[i] = parcafiltreolustur(fline[i]);
            }
            for (int i = 1; i < fline.Length; i++)
            {
                k[i - 1].subfiltre = k[i];
            }
            for (int i = fline.Length-1; i >=1; i--)
            {
                k[i].parentfiltre = k[i-1];
            }
            return k[0];
        }
        public macfiltre parcafiltreolustur(string line)
        {
            olmak sl=olmak.olsun;
            if (line.IndexOf("f")!=-1)
            {
                sl = olmak.olmasin;
                line=line.Replace("f", "");
            }
            string[] fline = line.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
            macfiltre a = null;
            if (fline.Length == 1)
            {
                var s = dicolustur(fline[0]);
                a = new macfiltre(s, Enumerable.Range(0, s.Count).ToArray());
            }
            if (fline.Length == 2)
            {
                string[] wntints = fline[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                int[] wnt = new int[wntints.Length];
                for (int i = 0; i < wntints.Length; i++)
                {
                    wnt[i] = int.Parse(wntints[i]);
                }
                a = new macfiltre(dicolustur(fline[0]), wnt,sl);
            }
            return a;
        }
        public Dictionary<int, sonuc> dicolustur(string line)
        {
            Dictionary<int, sonuc> g = new Dictionary<int, sonuc>();
            string[] fline = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < fline.Length; i++)
            {
                int konum = fline[i].IndexOf('-');
                int l = fline[i].Length;
                string min = fline[i].Substring(0, konum);
                string max = fline[i].Substring(konum + 1, l - konum - 1);
                max = (max == "01" ? "10" : max);
                max = (max == "21" ? "12" : max);
                max = (max == "02" ? "20" : max);
                int mn = int.Parse(min);
                sonuc ax = (sonuc)Enum.Parse(typeof(sonuc), "m" + max);
                g.Add(mn, ax);
            }
            return g;
        }
        private void ilksecim_Load_1(object sender, EventArgs e)
        {
        }
        private void kuponolustur_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(ilkfile);
            foreach (var item in ilksecim.kupondondur())
            {
                file.WriteLine(item);
            }
            file.Close();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(filtrefile);
            foreach (var item in rt1.Lines)
            {
                file1.WriteLine(item);
            }
            file1.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            kuponlar.Clear();
            fl.Clear();
            string[] tercih = ilksecim.ilksecim();
            sonuc[] ilktercih = new sonuc[15];
            for (int i = 0; i < 15; i++)
            {
                ilktercih[i] = ((sonuc)Enum.Parse(typeof(sonuc), "m" + tercih[i])) ^ sonuc.m102;
            }
            for (int i = 0; i < rt1.Lines.Length; i++)
            {
                string[] fline = rt1.Lines[i].Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var mline in fline)
                {
                    fl.Add(filtreolustur(mline));
                }
            }
            for (int i = 0; i < 15; i++)
            {
                startup[i] = (sonuc)Enum.Parse(typeof(sonuc), "m" + tercih[i]);
            }
            liste k = new liste();
            k.cati = startup;
            k.dallar = null;
            // MessageBox.Show(((int)sonuc.m2).ToString());
            //return;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (var item in fl)
            {
                    item.broke(k);
            }
            sw.Stop();
            MessageBox.Show("Toplam Süre (MSN): "+((float)sw.ElapsedMilliseconds).ToString());
            List<sonuc[]> y = new List<sonuc[]>();
            k.kolanlar(y);
            MessageBox.Show("Toplam Kolon Sayısı=" + k.boyut().ToString() + "- Toplam Kupon Sayısı=" + k.toplamkolon().ToString());
            if (y.Count < 50000)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(Application.StartupPath + "\\" + "test.txt");
                System.IO.StreamWriter file1 = new System.IO.StreamWriter(Application.StartupPath + "\\" + "test1.txt");
                string str = "";
                foreach (var item in y)
                {
                    str = string.Join("-", item);
                    str = str.Replace("m", "");
                    str = str.Replace("-01", "-10");
                    file.WriteLine(str);
                }
                foreach (var item in y)
                {
                    string ka = "";
                    int t = 1;
                    foreach (var sss in item)
                    {
                        ka = ka + t.ToString() + "-" + sss.ToString() + ",";
                        ka = ka.Replace("m", "");
                        t++;
                    }
                    file1.WriteLine(ka);
                }
                file1.Close();
                file.Close(); ;
            }
        }
    }
}
