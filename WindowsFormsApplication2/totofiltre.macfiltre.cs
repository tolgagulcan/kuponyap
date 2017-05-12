using System;
using System.Collections.Generic;
using System.Linq;
namespace totofiltreleme
{
    partial class totofiltre
    {
        private class macfiltre
        {
            private Dictionary<int, sonuc> etkilenenmaclar = null;
            int[] sayi; int istenenmax; int istenenmin;
            private macfiltre subfiltre = null;
            private macfiltre tersfiltre = null;
            private macfiltre ters()
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
                tersfiltre = new macfiltre(etkilenenmaclar, a1);
                return tersfiltre;
            }
            public macfiltre(string filtretext)
            {
                string[] fline = filtretext.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                string line = fline[0];
                string[] macvesayi = line.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                string[] wntints;
                int lenmaclar = macvesayi[0].Split(',').Length;
                if (macvesayi.Length != 1)
                {
                    wntints = macvesayi[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    wntints = new string[lenmaclar];
                    for (int i = 0; i < lenmaclar; i++)
                    {
                        wntints[i] = i.ToString();
                    }
;
                }
                int[] wnt = new int[wntints.Length];
                for (int i = 0; i < wntints.Length; i++)
                {
                    wnt[i] = int.Parse(wntints[i]);
                }
                etkilenenmaclar = dicolustur(macvesayi[0]);
                sayi = wnt.OrderBy(i => i).ToArray(); istenenmax = sayi.Max(); istenenmin = sayi.Min();
                string strtosub = "";
                if (fline.Length > 1)
                {
                    for (int i = 1; i < fline.Length; i++)
                    {
                        strtosub += fline[i]+  ":";
                    }
                    subfiltre = new macfiltre(strtosub);
                }
            }
            public macfiltre(Dictionary<int, sonuc> etk, int[] say,macfiltre sub=null)
            {
                etkilenenmaclar = etk;
                sayi = say.OrderBy(i => i).ToArray(); istenenmax = sayi.Max(); istenenmin = sayi.Min();
                subfiltre = sub;
            }
            public void broke(liste a)
            {
                bool uygun = uygunmu(a);
                if (a.dead==true)
                {
                    return;
                }
                if (uygun==false && subfiltre==null)
                {
                    a.dead = true;
                    return;
                }
                if (uygun == false && subfiltre != null)
                {
                    return;
                }
                if (uygun == true && (a.dallarf!=null || a.dallart!=null))
                {
                    if (a.dallarf!=null)
                    {
                        foreach (var item in a.dallarf)
                        {
                            broke(item);
                        }
                    }
                    if (a.dallart != null)
                    {
                        foreach (var item in a.dallart)
                        {
                            broke(item);
                        }
                    }
                    return;
                }
                if (subfiltre==null)
                {
                    parcala(a);
                }
                else
                {
                    if (ters()!=null)
                    {
                        ters().parcala(a);
                    }
                    parcala(a,true);
                    if (a.dallarf!=null)
                    {
                        foreach (var item in a.dallarf)
                        {
                            subfiltre.broke(item);
                        }
                    }
                    else
                    {
                        subfiltre.broke(a);
                    }
                }
            }
            private bool uygunmu(liste a)
            {
                int cikar = 0;
                int pass = 0;
                int parcala = 0;
                foreach (var item in etkilenenmaclar)
                {
                    int deger = nlookup[(int)a.cati[item.Key - 1] - 1, (int)item.Value - 1];
                    if (deger==0 )
                    {
                        cikar++;
                    }
                    if (deger == 1)
                    {
                        pass++;
                    }
                    if (deger == 2)
                    {
                        parcala++;
                    }
                }
                for (int i = 0;i< sayi.Length; i++) {
                    if (sayi[i]>=cikar && sayi[i]<=(cikar+parcala))
                    {
                        if (subfiltre != null && subfiltre.subfiltre != null)
                        {
                            return subfiltre.uygunmu(a);
                        }
                        return true;
                    }
                }
                
                return false;
            }
            private void parcala(liste a,bool df=false)
            {
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
                if (etkilenenler.Count == 0 || bulunacaklar.Count == 0)
                {
                    return;
                }
                List<liste> hangidallar;
                if (df)
                {
                    if (a.dallarf == null)
                    {
                        a.dallarf = new List<liste>();
                    }
                    hangidallar = a.dallarf;
                }
                else
                {
                    if (a.dallart == null)
                    {
                        a.dallart = new List<liste>();
                    }
                    hangidallar = a.dallart;
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

                Dictionary<int, int> bbb1 = new Dictionary<int, int>();
                foreach (var item in bulcomb)
                {
                    bbb1.Add(item, item);
                }

                bool tekli = false;

                Dictionary<int, int> secenek;

                secenek = (tekli) ? bbb1 : bbb;

                foreach (var item in secenek)
                {
                    List<int[]> k = totofiltre.listedondur(etkilenenler.Count, item.Key, item.Value);
                    for (int i = 0; i < k.Count; i++)
                    {
                        hangidallar.Add(new liste());
                        hangidallar[hangidallar.Count - 1].cati = new sonuc[15];
                        a.cati.CopyTo(hangidallar[hangidallar.Count - 1].cati, 0);
                        for (int s = 0; s < etkilenenler.Count; s++)
                        {
                           hangidallar[hangidallar.Count - 1].cati[etkilenenler[s] - 1] = degerler[s, k[i][s]];
                        }
                    }
                }
            }
        }
    }
}
