using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DraggableControls;
namespace WindowsFormsApplication2
{
    public partial class UserControl1 : UserControl
    {
        public int deger;
        public ComboBox[,] ls = new ComboBox[15,2];
        public UserControl1()
        {
            InitializeComponent();
            ls[0, 0] = m1; ls[0, 1] = d1;
            ls[1, 0] = m2; ls[1, 1] = d2;
            ls[2, 0] = m3; ls[2, 1] = d3;
            ls[3, 0] = m4; ls[3, 1] = d4;
            ls[4, 0] = m5; ls[4, 1] = d5;
            ls[5, 0] = m6; ls[5, 1] = d6;
            ls[6, 0] = m7; ls[6, 1] = d7;
            ls[7, 0] = m8; ls[7, 1] = d8;
            ls[8, 0] = m9; ls[8, 1] = d9;
            ls[9, 0] = m10; ls[9, 1] = d10;
            ls[10, 0] = m11; ls[10, 1] = d11;
            ls[11, 0] = m12; ls[11, 1] = d12;
            ls[12, 0] = m13; ls[12, 1] = d13;
            ls[13, 0] = m14; ls[13, 1] = d14;
            ls[14, 0] = m15; ls[14, 1] = d15;
        }
        public string[] ilksecim() {
            string[] tercih = new string[15];
            for (int i = 0; i < 15; i++)
            {
                string k = "";
                k = ls[i, 1].Text.IndexOf("1") != -1 ? k + "1" : k;
                k = ls[i, 1].Text.IndexOf("0") != -1 ? k + "0" : k;
                k = ls[i, 1].Text.IndexOf("2") != -1 ? k + "2" : k;
                tercih[i] = k;
            }
            return tercih;
        }
        private void m2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void d1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.Draggable(true);
            for (int i = 0; i < 15; i++)
            {
                ls[i, 1].Text = "102";
            }
        }
        public List<string> kupondondur() {
            List<string> a = new List<string>();
            for (int i = 0; i < 15; i++)
            {
                a.Add(ls[i, 1].Text);
            }
            return a;
        }
        public void kupondoldur(List<string> a)
        {
            for (int i = 0; i < 15; i++)
            {
                ls[i, 1].Text=a[i];
            }
        }
    }
}
