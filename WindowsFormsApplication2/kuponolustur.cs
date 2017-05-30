using DraggableControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using totofiltreleme;
namespace WindowsFormsApplication2
{
    public partial class kuponolustur : Form
    {
        string filtrefile, ilkfile;
        public static decimal para = 0;
        public kuponolustur()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            initform();
            //totofiltre.touchme = 2hjkjhk;
        }
        public void initform()
        {
            filtrefile = Application.StartupPath + "\\filtreler.txt";
            ilkfile = Application.StartupPath + "\\ilksecim.txt";
            //ilksecim.Draggable(true);
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

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("ttercih.txt");
            foreach (var item in ilksecim.kupondondur())
            {
                file.WriteLine(item);
            }
            file.Close();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter("tfiltreler.txt");
            foreach (var item in rt1.Lines)
            {
                file1.WriteLine(item);
            }
            file1.Close();
        }

        private List<string> zincirolustur(List<string> olumlu, List<string> olumsuz, int[] istenensayi)
        {
            int[] bulcomb = istenensayi;
            Dictionary<int, int> bbb = new Dictionary<int, int>();
            int first = bulcomb[0];
            bbb.Add(first, first);
            for (int i = 1; i < bulcomb.Length; i++)
            {
                if (bulcomb[i] == bulcomb[i - 1] + 1)
                {
                    bbb[first] = bulcomb[i];
                }
                else
                {
                    first = bulcomb[i];
                    bbb.Add(bulcomb[i], bulcomb[i]);
                }
            }
            List<string> sonuc = new List<string>();



            List<int[]> k = totofiltre.listedondur(olumlu.Count, bbb);

            foreach (int[] ty in k)
            {
                List<string> deger = new List<string>();
                for (int i = 0; i < ty.Length; i++)
                {
                    if (ty[i] == 0)
                    {
                        deger.Add(olumlu[i]);
                    }

                    if (ty[i] == 1)
                    {
                        deger.Add(olumsuz[i]);
                    }

                    if (ty[i] == -1)
                    {
                        if (i != 0)
                        {
                            deger.Add("15-1>-1");
                            break;
                        }

                   }

                    if (ty[i] == 2)
                    {
                        deger.Add("15-1>0,1");
                    }

                    
                }

                if (deger != null)
                {
                    sonuc.Add(string.Join(":", deger));

                }



            }








            return sonuc;



        }

        string[] terssayi(int toplam, List<int> a)
        {

            List<int> ters = new List<int>();

            bool ekle = true;
            for (int i = 0; i <= toplam; i++)
            {
                ekle = true;
                for (int k = 0; k < a.Count; k++)
                {
                    if (i == a[k])
                    {
                        ekle = false;
                    }
                }
                if (ekle)
                {
                    ters.Add(i);
                }

            }

            ters.Sort();
            string[] strarray = new string[ters.Count];
            for (int i = 0; i < strarray.Length; i++)
            {
                strarray[i] = ters[i].ToString();
            }

            return strarray;
        }

        private List<string> filtredonustur(string filtretext)
        {

            List<string> olumlu = new List<string>();
            List<string> olumsuz = new List<string>();

            string[] filtrekutu = bol(filtretext, '>');
            string[] parcalar = bol(filtrekutu[0], ',');
            List<int> isteneler = new List<int>();

            if (filtrekutu.Length == 1)
            {
                isteneler.Add(parcalar.Length);
            }

            else
            {
                string[] ist = filtrekutu[1].Split(',');
                for (int i = 0; i < ist.Length; i++)
                {
                    isteneler.Add(int.Parse(ist[i]));
                }

            }

            for (int i = 0; i < parcalar.Length; i++)
            {
                if (parcalar[i][0] == '(' && parcalar[i][parcalar[i].Length - 1] == ')')
                {
                    parcalar[i] = parcalar[i].Substring(1, parcalar[i].Length - 2);
                }
            }


            for (int i = 0; i < parcalar.Length; i++)
            {
                string[] ic = bol(parcalar[i], ':');

                if (ic.Length > 1)
                {
                    string deger = String.Join(",", ic);

                    deger = deger + ">" + ic.Length;
                    olumlu.Add(deger);

                    List<int> k = new List<int>();

                    for (int t = 0; t < ic.Length; t++)
                    {
                        k.Add(t);
                    }

                    olumsuz.Add(deger + ">" + String.Join(",", terssayi(ic.Length, k)));


                    continue;
                }

                ic = bol(parcalar[i], '>');

                if (ic.Length == 2)
                {
                    olumlu.Add(parcalar[i]);

                    string[] k = bol(ic[1], ',');
                    List<int> t = new List<int>();

                    for (int h = 0; h < k.Length; h++)
                    {
                        t.Add(int.Parse(k[h]));
                    }

                    int count = bol(ic[0], ',').Length;
                    olumsuz.Add(ic[0] + ">" + string.Join(",", terssayi(count, t)));
                    continue;
                }
                if (ic.Length == 1)
                {
                    int sayi = bol(ic[0], ',').Length;
                    string deger = parcalar[i] + ">" + sayi.ToString();
                    olumlu.Add(deger);
                    List<int> t = new List<int>();

                    t.Add(sayi);
                    olumsuz.Add(parcalar[i] + ">" + string.Join(",", terssayi(sayi, t)));


                }


            }

            return zincirolustur(olumlu, olumsuz, isteneler.ToArray());



        }


        string[] bol(string filtretext, char seperator = ':')
        {

            int numofac = 0;


            int first = 0;

            List<string> bolumler = new List<string>();


            for (int i = 0; i < filtretext.Length; i++)
            {

                if (filtretext[i] == '(')
                {
                    numofac++;

                }
                if (filtretext[i] == ')')
                {
                    numofac--;
                }
                if (filtretext[i] == seperator || i == filtretext.Length - 1)
                {
                    if (numofac == 0)
                    {
                        if (i == filtretext.Length - 1)
                        {
                            if (filtretext.Substring(first, i - first + 1) != "")
                            {
                                bolumler.Add(filtretext.Substring(first, i - first + 1));
                            }

                        }
                        else
                        {
                            if (filtretext.Substring(first, i - first) != "")
                            {
                                bolumler.Add(filtretext.Substring(first, i - first));
                            }

                        }

                        //MessageBox.Show(bolumler[bolumler.Count - 1]);


                        first = i + 1;
                        numofac = 0;

                    }
                }
                if (i == filtretext.Length - 1 && (numofac != 0))
                {
                    throw new Exception("Hatavar " + filtretext);
                }


            }

            return bolumler.ToArray();


        }


        private List<string> zincirparcala(string filtretext = "")
        {

            List<string>[] zincirlisteler;


            string[] fline = bol(filtretext);


            zincirlisteler = new List<string>[fline.Length];

            for (int i = 0; i < fline.Length; i++)
            {
                if (Regex.IsMatch(fline[i], match))
                {
                    zincirlisteler[i] = new List<string>();
                    zincirlisteler[i].Add(fline[i]);
                }
                else
                {
                    if (fline[i][0] == '(' && fline[i][fline[i].Length - 1] == ')')
                    {
                        zincirlisteler[i] = filtredonustur(fline[i].Substring(1, fline[i].Length - 2));
                    }
                    else
                    {
                        zincirlisteler[i] = filtredonustur(fline[i]);
                    }

                }


                
            }




            return birlestir(zincirlisteler, ':');


        }

        private List<string> birlestir(List<string>[] birlesecekler, char neile)
        {


            List<string> liste = new List<string>();

            int carpim = 1;

            for (int i = 0; i < birlesecekler.Length; i++)
            {
                carpim *= birlesecekler[i].Count;
            }


            int[] index = new int[birlesecekler.Length];
            for (int i = 0; i < carpim; i++)
            {


                int kalan = i;


                for (int s = 0; s < birlesecekler.Length; s++)
                {
                    int now = 1;
                    for (int a = s + 1; a < birlesecekler.Length; a++)
                    {
                        now = now * birlesecekler[a].Count;
                    }

                    if (s + 1 != birlesecekler.Length)
                    {
                        index[s] = (kalan) / now;
                        kalan = kalan - index[s] * now;
                    }
                    else
                    {
                        index[s] = kalan;

                    }

                }

                string msg = "";

                for (int g = 0; g < index.Length; g++)
                {
                    msg += index[g].ToString() + "-";
                }

                string sonfiltre = "";

                for (int h = 0; h < birlesecekler.Length; h++)
                {

                    sonfiltre += birlesecekler[h][index[h]];
                    if (h != birlesecekler.Length - 1)
                    {
                        sonfiltre += neile;
                    }

                }

                liste.Add(sonfiltre);


            }

            return liste;

        }


        public string match = @"^(\d+-\d+)(,(\d+-\d+))*(?(>)(>-?\d+(,\d*)*))(:((\d+-\d+)(,(\d+-\d+))*(?(>)(>-?\d+(,\d*)*))))*$";
        private void button3_Click(object sender, EventArgs e)
        {


            totofiltre yenifiltre = new totofiltre(!checkBox1.Checked);
            string cati = string.Join("-", ilksecim.ilksecim());
            yenifiltre.setcati(cati);

            List<string> tamamlisteler = new List<string>();
            List<string> olusacaklisteler = new List<string>();



            for (int i = 0; i < rt1.Lines.Length; i++)
            {
                string line = rt1.Lines[i].Trim();

                if (line != "")
                {
                    olusacaklisteler.Add(rt1.Lines[i]);
                }


            }

            string[] stringSeparators = new string[] { ":15-1>-1:" };
            while (olusacaklisteler.Count != 0)
            {
                //string[] strarray = olusacaklisteler[0].Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);


                //if (strarray.Length>1)
                //{
                //    olusacaklisteler.RemoveAt(0); 
                //}

                if (Regex.IsMatch(olusacaklisteler[0], match))
                {
                    tamamlisteler.Add(olusacaklisteler[0]);
                    olusacaklisteler.RemoveAt(0);
                }
                else
                {
                    foreach (var item in zincirparcala(olusacaklisteler[0]))
                    {
                        olusacaklisteler.Add(item);
                    }
                    olusacaklisteler.RemoveAt(0);
                }


            }


            foreach (var item in tamamlisteler)
            {
                yenifiltre.filtreekle(item);
            }


            yenifiltre.start();
            decimal a = (decimal)yenifiltre.toplampara() * 2;
            decimal fark = a - para;
            para = a;
            label1.Text = "Toplam Para: " + String.Format("{0:C}", a);
            label2.Text = "Toplam Kolon: " + yenifiltre.toplamkolon().ToString();
            label3.Text = "Filtre Farkı: " + String.Format("{0:C}", fark);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            totofiltre yenifiltre = new totofiltre(!checkBox1.Checked);
            string cati = string.Join("-", ilksecim.ilksecim());
            //MessageBox.Show(cati);
            yenifiltre.setcati(cati);
            yenifiltre.filtreekle(rt1.Text);
            yenifiltre.start();
            decimal a = (decimal)yenifiltre.toplampara();
            decimal fark = a - para;
            para = a;
            label1.Text = "Toplam Para: " + String.Format("{0:C}", a);
            label2.Text = "Toplam Kolon: " + yenifiltre.toplamkolon().ToString();
            label3.Text = "Filtre Farkı: " + String.Format("{0:C}", fark);
        }
    }
}
