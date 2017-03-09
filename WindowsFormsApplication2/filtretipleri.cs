using System;
using System.Collections.Generic;
using System.Linq;
using static WindowsFormsApplication2.tanimlamalar;
namespace WindowsFormsApplication2
{
    public class macfiltre
    {
        public Dictionary<int, sonuc> etkilenenmaclar = null;
        public olmak olsunmu = olmak.olsun;
        int[] sayi; int istenenmax; int istenenmin;
        public macfiltre subfiltre = null;
        public macfiltre parentfiltre = null;
        public macfiltre tersfiltre = null;
        public macfiltre dipfiltre = null;
        public macfiltre deep()
        {
            if (dipfiltre != null)
            {
                return dipfiltre;
            }
            if (subfiltre == null)
            {
                return dipfiltre = this;
            }
            else
            {
                dipfiltre = subfiltre.deep();
            }
            return dipfiltre;
        }
        public macfiltre ters()
        {
            if (tersfiltre != null)
            {
                return tersfiltre;
            }
            int[] a1;
            int nerde = 0;
            if (sayi.Length >= (etkilenenmaclar.Count + 1))
            {
                return null;
            }
            a1 = new int[etkilenenmaclar.Count + 1 - sayi.Length];
            for (int i = 0; i <= etkilenenmaclar.Count; i++)
            {
                bool ekle = true;
                for (int s = 0; s < sayi.Length; s++)
                {
                    if (i == sayi[s])
                    {
                        ekle = false;
                    }
                }
                if (ekle)
                {
                    a1[nerde] = i;
                    nerde++;
                }
            }
            tersfiltre = new macfiltre(etkilenenmaclar, a1, olmak.olsun);
            return tersfiltre;
        }
        public macfiltre(Dictionary<int, sonuc> k, int[] tane, olmak ols = olmak.olsun)
        {
            if (tane.Length == 0 || tane.Length == (k.Count + 1))
            {
                throw new Exception("wrong number of wanted numbers");
            }
            etkilenenmaclar = k;
            olsunmu = ols;
            sayi = tane.OrderBy(i => i).ToArray(); istenenmax = sayi.Max(); istenenmin = sayi.Min(); ;
        }
        public bool broke(liste a)
        {
            if (olsunmu == olmak.olmasin)
            {
                if (ters() != null)
                {
                    ters().broke(a);
                }
                else
                {
                    a.dead = true;
                }
                return true;
            }
            if (a.dead)
            {
                return false;
            }
            if (uygunmutoptan(a))
            {
                if (a.dallar == null)
                {
                    parcalatoptan(a);
                }
                else
                {
                    if (a.dallar.Count != 0)
                    {
                        foreach (var item in a.dallar)
                        {
                            broke(item);
                        }
                    }
                }
            }
            return true;
        }

        public bool uygunmutoptan(liste a)
        {
            if (subfiltre == null)
            {
                return uygunmu(a);
            }
            else
            {
                if (parentfiltre != null)
                {
                    return subfiltre.uygunmu(a) & uygunmu(a);
                }
                else
                {
                    return subfiltre.uygunmu(a);
                }

            }



        }
        public bool uygunmu(liste a,bool forcedead=true)
        {
            macfiltre dfg = this;
            int uygun = 0;
            bool dondur = true;
            foreach (var item in etkilenenmaclar)
            {
                if (nlookup[(int)a.cati[item.Key - 1] - 1, (int)item.Value - 1] != 1)
                {
                    uygun++;
                }
            }
            if (uygun < istenenmin)
            {
                if (forcedead==true)
                {
                    a.dead = true;
                }
                
                return false;
            }
            return dondur;
        }
        public bool parcalatoptan(liste a)
        {
            if (subfiltre == null)
            {
                parcala(a);
                return true;
            }
            else
            {
                List<liste> k;
                macfiltre s = deep();
                List<liste> donentoplam = new List<liste>();
                donentoplam.Add(a);


                do
                {
                    List<liste> yedek = new List<liste>();
                    k = null;


                    foreach (var item in donentoplam)
                    {

                        if (s != this)
                        {
                            if (s.ters().uygunmu(item,false))
                            {
                                s.ters().parcala(item,false);
                            }

                            if (s.uygunmu(item,false))
                            {
                                k = s.parcala(item,false);
                            }

                        }
                        else
                        {
                            if (s.uygunmu(item))
                            {
                                s.parcala(item);
                            }
                        }



                        if (k != null)
                        {
                            foreach (var st in k)
                            {
                                yedek.Add(st);
                            }
                            
                        }
                    }

                    donentoplam = yedek;
                } while ((s = s.parentfiltre) != null);
            }
            return true;
        }
        public List<liste> parcala(liste a, bool forcedead = true)
        {
            List<liste> olusturulanlar = new List<liste>();
            List<int> etkilenenler = new List<int>();
            List<sonuc> olsun = new List<sonuc>();
            List<sonuc> olmasin = new List<sonuc>();
            List<sonuc> both = new List<sonuc>();
            int halihazir = 0;
            foreach (var item in etkilenenmaclar)
            {
                int sonuc = nlookup[(int)a.cati[item.Key - 1] - 1, (int)item.Value - 1];
                if (sonuc == 2)
                {
                    etkilenenler.Add(item.Key);
                    both.Add(a.cati[item.Key - 1]);
                    sonuc deger = a.cati[item.Key - 1] & item.Value;
                    olsun.Add(deger);
                    olmasin.Add(a.cati[item.Key - 1] ^ deger);
                }
                else if (sonuc == 0)
                {
                    halihazir++;
                }
            }
            List<int> bulunacaklar = new List<int>();
            foreach (var item in sayi)
            {
                if (item - halihazir <= etkilenenler.Count && item - halihazir >= 0)
                {
                    bulunacaklar.Add(item - halihazir);
                }
            }
            if (halihazir > istenenmax)
            {
                if (forcedead==true)
                {
                      a.dead = true;
                }

                olusturulanlar.Add(a);

                return olusturulanlar;
            }
            if (etkilenenler.Count == 0)
            {
                olusturulanlar.Add(a);

                return olusturulanlar;
               
            }
            if (bulunacaklar.Count == 0)
            {
                
                olusturulanlar.Add(a);

                return olusturulanlar;
            }
            sonuc[,] degerler = new sonuc[olsun.Count, 3];
            for (int i = 0; i < olsun.Count; i++)
            {
                degerler[i, 0] = olsun[i];
                degerler[i, 1] = olmasin[i];
                degerler[i, 2] = both[i];
            }
            int[] bulcomb = bulunacaklar.ToArray();
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
         
         
            if (a.dallar == null)
            {
                a.dallar = new List<liste>();
                
            }
            foreach (var item in bbb)
            {
                List<int[]> k = buyuklisteler.listedondur(etkilenenler.Count, item.Key, item.Value);
                for (int i = 0; i < k.Count; i++)
                {
                    a.dallar.Add(new liste());
                    olusturulanlar.Add(a.dallar[a.dallar.Count - 1]);

                    a.dallar[a.dallar.Count - 1].cati = new sonuc[15];
                    a.cati.CopyTo(a.dallar[a.dallar.Count - 1].cati, 0);
                    for (int s = 0; s < etkilenenler.Count; s++)
                    {
                        a.dallar[a.dallar.Count - 1].cati[etkilenenler[s] - 1] = degerler[s, k[i][s]];
                    }
                }
            }
            
            a.parcalayan = this;
            return olusturulanlar;
        }
    }
    public static class tanimlamalar
    {
        //parçalama
        //çıkar=0, passgeç=1, parçala=2
        public static int[,] nlookup = new int[7, 7] {
                                        { 0,1,0,1,0,1,0 },
                                        { 1,0,0,1,1,0,0 },
                                        { 2,2,0,1,2,2,0 },
                                        { 1,1,1,0,0,0,0 },
                                        { 2,1,2,2,0,2,0 },
                                        { 1,2,2,2,2,0,0 },
                                        { 2,2,2,2,2,2,0 },};
        public class liste
        {
            public bool dead = false;
            public liste parent = null;
            public sonuc[] cati;
            public macfiltre parcalayan = null;
            public List<liste> dallar = null;

            public int boyut()
            {
                int toplam = 0;
                if (dead)
                {
                    return 0;
                }
                if (dallar == null)
                {
                    return kuponboyut(cati);
                }
                else
                {
                    foreach (var item in dallar)
                    {
                        toplam = toplam + item.boyut();
                    }
                    return toplam;
                }
            }
            public void kolanlar(List<sonuc[]> k)
            {
                if (dead)
                {
                    return;
                }
                if (dallar == null)
                {
                    k.Add(cati);
                }
                else
                {
                    foreach (var item in dallar)
                    {
                        item.kolanlar(k);
                    }
                }
            }
            public int toplamkolon()
            {
                int toplam = 0;
                if (dead)
                {
                    return 0;
                }
                if (dallar == null)
                {
                    return 1;
                }
                else
                {
                    foreach (var item in dallar)
                    {
                        if (item.dead == false)
                        {
                            toplam = toplam + item.toplamkolon();
                        }
                    }
                    return toplam;
                }
            }
            public int kuponboyut(sonuc[] k)
            {
                int boyut = 1;
                int carpim = 1;
                for (int i = 0; i < k.Length; i++)
                {
                    switch (k[i])
                    {
                        // The following switch section causes an error.  
                        case sonuc.m0:
                        case sonuc.m1:
                        case sonuc.m2:
                            carpim = 1;
                            break;
                        case sonuc.m01:
                        case sonuc.m12:
                        case sonuc.m20:
                            carpim = 2;
                            break;
                        case sonuc.m012:
                            carpim = 3;
                            break;
                    }
                    boyut = boyut * carpim;
                }
                return boyut;
            }
        }
        public class listesayili
        {
            public List<liste> donen;
            public int donenmin;
            public int donenmax;

            public listesayili(List<liste> donenk,int min,int max) {
                donen = donenk;donenmin = min;donenmax = max;
            }
        }
        [Flags]
        public enum sonuc : byte
        {
            bos = 0x0,
            m1 = 0x1,
            m0 = 0x2,
            m2 = 0x4,
            m10 = (m1 | m0), m01 = m1 | m0,
            m20 = m2 | m0, m02 = m2 | m0,
            m12 = m1 | m2, m21 = m1 | m2,
            m102 = m1 | m0 | m2,
            m120 = m1 | m0 | m2,
            m210 = m1 | m0 | m2,
            m201 = m1 | m0 | m2,
            m012 = m1 | m0 | m2,
            m021 = m1 | m0 | m2,
        }
        [Flags]
        public enum olmak : byte
        {
            hic = 0x0,
            olsun = 0x1,
            olmasin = 0x2,
            ikisi = olsun | olmasin
        }
    }
}
