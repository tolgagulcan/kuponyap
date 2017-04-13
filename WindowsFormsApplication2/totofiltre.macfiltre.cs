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
            private olmak olsunmu = olmak.olsun;
            int[] sayi; int istenenmax; int istenenmin;
            private macfiltre subfiltre = null;
            private macfiltre parentfiltre = null;
            private macfiltre tersfiltre = null;
            private macfiltre dipfiltre = null;
            private macfiltre deep()
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
                tersfiltre = new macfiltre(etkilenenmaclar, a1, olmak.olsun);
                return tersfiltre;
            }

            public macfiltre(string filtretext, macfiltre parent = null)
            {

                parentfiltre = parent;

                string[] fline = filtretext.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                string line = fline[0];

                olmak sl = olmak.olsun;

                if (line.IndexOf("f") != -1)
                {
                    sl = olmak.olmasin;
                    line = line.Replace("f", "");
                }

                string[] macvesayi = line.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                


                string[] wntints = macvesayi[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                int[] wnt = new int[wntints.Length];
                for (int i = 0; i < wntints.Length; i++)
                {
                    wnt[i] = int.Parse(wntints[i]);
                }
                initmacfiltre(dicolustur(macvesayi[0]), wnt, sl);
                string strtosub = "";
                if (fline.Length>1)
                {
                    for (int i = 1; i < fline.Length; i++)
                    {
                        strtosub += fline[i];
                    }

                    subfiltre = new macfiltre(strtosub,this);
                }


            }

            private void initmacfiltre(Dictionary<int, sonuc> k, int[] tane, olmak ols = olmak.olsun)
            {

                if (tane.Length == 0 || tane.Length == (k.Count + 1))
                {
                    throw new Exception("wrong number of wanted numbers");
                }
                etkilenenmaclar = k;
                olsunmu = ols;
                sayi = tane.OrderBy(i => i).ToArray(); istenenmax = sayi.Max(); istenenmin = sayi.Min(); ;

            }

            public macfiltre(Dictionary<int, sonuc> k, int[] tane, olmak ols = olmak.olsun)
            {
                initmacfiltre(k, tane, ols);
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

            private bool uygunmutoptan(liste a)
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
            private bool uygunmu(liste a, bool forcedead = true)
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
                    if (forcedead == true)
                    {
                        a.dead = true;
                    }

                    return false;
                }
                return dondur;
            }
            private bool parcalatoptan(liste a)
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
                                if (s.ters().uygunmu(item, false))
                                {
                                    s.ters().parcala(item, false);
                                }

                                if (s.uygunmu(item, false))
                                {
                                    k = s.parcala(item, false);
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
            private List<liste> parcala(liste a, bool forcedead = true)
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
                    if (forcedead == true)
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
                    List<int[]> k = totofiltre.listedondur(etkilenenler.Count, item.Key, item.Value);
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

    }



}
