using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace totofiltreleme
{
    partial class totofiltre
    {
        public static int touchme = 0;
        private liste cati = null;
        private List<macfiltre> filtrekutusu=new List<macfiltre>(50);
        private string catitostr="";
        private string filtrelertostr="";

        public void setcati(string catistr) {
            catitostr = catistr;
            catistr=catistr.Replace("01", "10").Replace("20", "02").Replace("21", "12").Replace("120", "102").Replace("012", "102").Replace("021", "102").Replace("210", "102").Replace("201", "102");

            string[] catiarray = catistr.Split('-');
            sonuc[] sonucarray = new sonuc[15];
            

            for (int i = 0; i < 15; i++)
            {
               
                sonucarray[i] = (sonuc)Enum.Parse(typeof(sonuc), "m" + catiarray[i]);
            }

            cati = new liste();cati.cati = sonucarray;

            
        }

        public string getcati()
        {

            return catitostr;
        }

        public void filtreekle(string filtreler) {
            filtreler = filtreler.Replace(Environment.NewLine, "&").Replace("\n","&");

            filtrelertostr = filtreler;
            filtrekutusu.Clear();

            string[] fline = filtreler.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var mline in fline)
            {
                filtrekutusu.Add(new macfiltre(mline));
            }
        }
        public string filtreeal() {
            return filtrelertostr;
        }


        private static Dictionary<int, sonuc> dicolustur(string line)
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

        public void start() {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (var item in filtrekutusu)
            {
                item.broke(cati);
            }
            sw.Stop();
            MessageBox.Show("Toplam Süre (MSN): " + ((float)sw.ElapsedMilliseconds).ToString());
            List<sonuc[]> y = new List<sonuc[]>();
            cati.kolanlar(y);
            MessageBox.Show("Toplam Kolon Sayısı=" + cati.boyut().ToString() + "- Toplam Kupon Sayısı=" + cati.toplamkolon().ToString());
            if (y.Count < 50000)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(Application.StartupPath + "\\" + "test.txt");
                
                string str = "";
                foreach (var item in y)
                {
                    str = string.Join("-", item);
                    str = str.Replace("m", "");
                    str = str.Replace("-01", "-10");
                    file.WriteLine(str);
                }

                file.Close(); ;
            }

        }

        private static List<int[]> listedondur(int kactane, int min, int max)
        {
            var hangiliste = listeler[kactane - 1][arrayindex(kactane, min, max)];
            if (hangiliste != null)
            {
                return hangiliste;
            }

            int sol = 0;int sag = kactane;int level = kactane;
            int[] kupon = new int[kactane];

            for (int i = 0; i < kactane; i++)
            {
                kupon[i] = 2;
            }

            hangiliste = (listeler[kactane - 1][arrayindex(kactane, min, max)] = new List<int[]>());

            olustur(kactane, (min > max) ? max : min, (min > max) ? min : max, sol, sag, level, kupon, hangiliste);

            return hangiliste;
        }
        private static void olustur(int sayi, int min = 0, int max = 15, int sol = 0, int sag = 15, int level = 15, int[] kupon = null, List<int[]> kuponlar = null)
        {
            if (min <= sol && max >= sag)
            {
                kuponlar.Add(kupon);
                return;
               
            }
            else if ((min < sol && max < sol) | (min > sag && max > sag) | (min < sol && max > sag) | (min > sag && max < sol))
            {
                return;
            }
            else
            {
                int[] p1 = new int[sayi]; kupon.CopyTo(p1, 0); p1[sayi - level] = 1;
                int[] p2 = new int[sayi]; kupon.CopyTo(p2, 0); p2[sayi - level] = 0;
                olustur(sayi, min, max, sol, sag - 1, level - 1, p1, kuponlar);
                olustur(sayi, min, max, sol + 1, sag, level - 1, p2, kuponlar);
            }
        }
        private static List<int[]>[][] listeler = new List<int[]>[15][];
        static totofiltre()
        {
            listeler[0] = new List<int[]>[3];
            listeler[1] = new List<int[]>[6];
            listeler[2] = new List<int[]>[10];
            listeler[3] = new List<int[]>[15];
            listeler[4] = new List<int[]>[21];
            listeler[5] = new List<int[]>[28];
            listeler[6] = new List<int[]>[36];
            listeler[7] = new List<int[]>[45];
            listeler[8] = new List<int[]>[55];
            listeler[9] = new List<int[]>[66];
            listeler[10] = new List<int[]>[78];
            listeler[11] = new List<int[]>[91];
            listeler[12] = new List<int[]>[105];
            listeler[13] = new List<int[]>[120];
            listeler[14] = new List<int[]>[136];

            
            for (int i = 1; i <= 15; i++)
            {
                for (int a = 0; a <= i; a++)
                {
                    for (int k = a; k <= i; k++)
                    {
                        listedondur(i, a, k);
                    }
                }
            }

    
        }
        private static int arrayindex(int sayi, int min, int max)
        {
            if (min == max)
            {
                return min;
            }
            else
            {
                int k = 0;
                for (int i = sayi + 1; i >= sayi - min + 1; i--)
                {
                    k = k + i;
                }
                return (k + max - min - 1);
            }
        }
        //parçalama
        //çıkar=0, passgeç=1, parçala=2
        private static int[,] nlookup = new int[7, 7] {
                                        { 0,1,0,1,0,1,0 },
                                        { 1,0,0,1,1,0,0 },
                                        { 2,2,0,1,2,2,0 },
                                        { 1,1,1,0,0,0,0 },
                                        { 2,1,2,2,0,2,0 },
                                        { 1,2,2,2,2,0,0 },
                                        { 2,2,2,2,2,2,0 },};
        [Flags]
        private enum sonuc : byte
        {
            bos = 0x0,
            m1 = 0x1,
            m0 = 0x2,
            m2 = 0x4,
            m10 = m1 | m0,
            m02 = m2 | m0,
            m12 = m1 | m2,
            m102 = m1 | m0 | m2,
            
        }
        
        private enum olmak : byte
        {
            hic = 0x0,
            olsun = 0x1,
            olmasin = 0x2,
            ikisi = olsun | olmasin
        }

    }

    
  
}
