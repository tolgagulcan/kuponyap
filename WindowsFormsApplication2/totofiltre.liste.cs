using System.Collections.Generic;
namespace totofiltreleme
{
    partial class totofiltre
    {
        private class liste
        {
            public bool dead = false;
            public sonuc[] cati;
            public List<liste> dallart = null;
            public List<liste> dallarf = null;
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
                        toplam += item.boyut();
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
                            toplam += item.toplamkolon();
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
                        case sonuc.m10:
                        case sonuc.m12:
                        case sonuc.m02:
                            carpim = 2;
                            break;
                        case sonuc.m102:
                            carpim = 3;
                            break;
                    }
                    boyut *= carpim;
                }
                return boyut;
            }
        }
    }
}
