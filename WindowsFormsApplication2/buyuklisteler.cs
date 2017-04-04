using System.Collections.Generic;
namespace WindowsFormsApplication2
{
    public static class buyuklisteler
    {
        public static int[] katlar = new int[16] { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768 };
        public static List<int[]> listedondur(int kactane,int min,int max)
        {
            if (listeler[kactane-1][arrayindex(kactane,min,max)]!=null)
            {
                return listeler[kactane - 1][arrayindex(kactane, min, max)];
            }
            int sol = 0;
            int sag = kactane;
            int level = kactane;
            int[] kupon = new int[kactane];
            for (int i = 0; i < kactane; i++)
            {
                kupon[i] = 2;
            }
            listeler[kactane - 1][arrayindex(kactane, min, max)] = new List<int[]>();
            int a = toplam(kactane, (min > max) ? max : min, (min > max) ? min : max, sol, sag, level, kupon, listeler[kactane - 1][arrayindex(kactane, min, max)]);
            return listeler[kactane - 1][arrayindex(kactane, min, max)];
        }
        static int toplam(int sayi, int min = 0, int max = 15, int sol = 0, int sag = 15, int level = 15, int[] kupon = null, List<int[]> kuponlar = null)
        {
            if (min <= sol && max >= sag)
            {
                kuponlar.Add(kupon);
                return 1;
                //return katlar[level];
            }
            else if ((min < sol && max < sol) | (min > sag && max > sag) | (min < sol && max > sag) | (min > sag && max < sol))
            {
                return 0;
            }
            else
            {
                int[] p1 = new int[sayi]; kupon.CopyTo(p1, 0); p1[sayi - level] = 1;
                int[] p2 = new int[sayi]; kupon.CopyTo(p2, 0); p2[sayi - level] = 0;
                return toplam(sayi, min, max, sol, sag - 1, level - 1, p1, kuponlar) + toplam(sayi, min, max, sol + 1, sag, level - 1, p2, kuponlar);
            }
        }
        public static List<int[]>[][] listeler = new List<int[]>[15][];
        static buyuklisteler()
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


            for (int i = 15; i <= 15; i++)
            {
                for (int a = 0; a <= i; a++)
                {
                    for (int k = a; k <= i; k++)
                    {
                        //System.Windows.Forms.MessageBox.Show(i.ToString()+"-"+a.ToString()+"-"+k.ToString());
                        listedondur(i, a, k);
                    }
                }
            }
        }
        public static int arrayindex(int sayi, int min, int max)
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
    }
}
