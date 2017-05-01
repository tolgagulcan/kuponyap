using DraggableControls;
using System;
using System.Collections.Generic;
using System.IO;
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
            //totofiltre.touchme = 2;
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

        private void button1_Click(object sender, EventArgs e)
        {
            totofiltre yenifiltre = new totofiltre();
            string cati = string.Join("-", ilksecim.ilksecim());
            //MessageBox.Show(cati);
            yenifiltre.setcati(cati);
            yenifiltre.filtreekle(rt1.Text);
            yenifiltre.start();
            decimal a = (decimal)yenifiltre.toplampara();
            decimal fark = a - para;
            para = a;
            label1.Text = "Toplam Para: "+String.Format("{0:C}",a);
            label2.Text = "Toplam Kolon: " + yenifiltre.toplamkolon().ToString();
            label3.Text = "Filtre Farkı: "+String.Format("{0:C}", fark);
        }
    }
}
