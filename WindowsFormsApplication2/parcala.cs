using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class kuponolustur : Form
    {
        //çıkar=0, passgeç=1, parçala=2
        public static byte[,] nlookup = new byte[7, 7] {
                                        { 0,1,0,1,0,1,0 },
                                        { 1,0,0,1,1,0,0 },
                                        { 1,1,1,0,0,0,0 },
                                        { 2,2,0,1,2,2,0 },
                                        { 2,1,2,2,0,2,0 },
                                        { 1,2,2,2,2,0,0 },
                                        { 2,2,2,2,2,2,0 },
        };


        public class liste
        {
            public sonuc[] cati;
            public liste[] dallar = null;
            public int boyut()
            {

                int toplam = 0;
                if (dallar == null)
                {
                    return kuponboyut(cati);
                }
                else
                {

                    foreach (var item in dallar)
                    {
                        toplam = toplam + kuponboyut(item.cati);
                    }

                    return toplam;
                }



            }

            public int toplamkolon()
            {


                int toplam = 0;
                if (dallar == null)
                {
                    return 1;
                }
                else
                {

                    foreach (var item in dallar)
                    {
                        toplam = toplam + item.toplamkolon();
                    }

                    return toplam;
                }
            }



        }

        public static int kuponboyut(sonuc[] k)
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

        sonuc[] startup = new sonuc[15];



    }
}