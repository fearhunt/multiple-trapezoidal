using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metnum
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            btHitung.Enabled = false;
            panel3.Visible = false;
        }

        public int a, b, pangkat, range; // a dan b sebagai batas fungsi. pangkat sebagai pangkat tertinggi di f(x). d sebagai indeks di btHitung
        public double h;

        public void btAct_Click(object sender, EventArgs e)
        {
            if (tbA.Text == "" || tbB.Text == "" || tbH.Text == "" || tbExp.Text == "")
            {
                MessageBox.Show("Semua data batas harus dimasukkan!", "Error (Empty Data)");
            }
            else
            {
                a = Convert.ToInt32(tbA.Text);
                b = Convert.ToInt32(tbB.Text);
                h = Double.Parse(tbH.Text);
                pangkat = Convert.ToInt32(tbExp.Text); 

                if (a < 0 || b <= 0 || h <= 0 || pangkat <= 0)
                {
                    MessageBox.Show("Minimal nilai data a = 0; b > 0; h > 0; dan pangkat > 0", "Error (Wrong Data)");
                    tbA.Text = ""; tbB.Text = ""; tbH.Text = ""; tbExp.Text = "";
                    a = 0; b = 0; h = 0; pangkat = 0;
                }
                else if (a >= b)
                {
                    MessageBox.Show("Batas a harus lebih kecil dari batas b (a < b)", "Error (Wrong Data)");
                    tbA.Text = ""; tbB.Text = "";
                    a = 0; b = 0;
                }
                else
                {
                    range = b - a;

                    if (range % h > 0)
                    {
                        MessageBox.Show("Batas h tidak memungkinkan untuk digunakan", "Error (Range)");
                        tbH.Text = "";
                        h = 0.0;
                    }
                    else
                    {
                        function.Text = Function();
                        MessageBox.Show("Data batas fungsi sudah berhasil dimasukkan \nSilakan untuk mengisi koefisien variabel di bagian kanan aplikasi", "Sukses"); //sepertinya cukup run method saja
                        btAct.Enabled = false;
                        tbA.Enabled = false;
                        tbB.Enabled = false;
                        tbH.Enabled = false;
                        tbExp.Enabled = false;
                        btHitung.Enabled = true;
                        AddCoef(pangkat);
                    }
                }
            }
        }

        private void btHitung_Click(object sender, EventArgs e)
        {
            int[] koef = new int[pangkat + 1];
            int i;
            string str;

            for (i = 0; i <= pangkat; i++)
            {
                str = ((TextBox)panel2.Controls["tbData" + (i)]).Text;

                if (str == "")
                {
                    MessageBox.Show("Setiap data harus dimasukkan!", "Error (Empty Data");
                    break;
                }
                else
                    koef[i] = Convert.ToInt32(str);
            }

            if (i == pangkat + 1)
            {
                Count(koef);
                function.Text = Function(koef);
                btHitung.Enabled = false;
            }
        }

        int topLoc = 1; // untuk indeks posisi label dan textBox

        private void AddCoef(int pangkat)
        {
            for (int i = 0; i <= pangkat; i++) // sekalian buat textbox konstanta
            {
                AddNewLabel(i);
                AddNewTextBox(i);
            }
            topLoc = 1; // inisiasi ulang. Jaga-jaga kalau mau dipakai lagi
        }

        private Label AddNewLabel(int i)
        {
            char char_temp = (char)(65 + i);
            Label lbl = new Label();
            this.panel2.Controls.Add(lbl);
            lbl.Top = topLoc * 10;
            lbl.Left = 5;
            lbl.AutoSize = true;
            lbl.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lbl.Size = new Size(157, 19);
            //this.label5.TabIndex = 8;
            lbl.Text = char_temp + " :";
            return lbl;
        }

        private TextBox AddNewTextBox(int i)
        {
            TextBox txt = new TextBox();
            this.panel2.Controls.Add(txt);
            txt.Top = topLoc * 10;
            txt.Left = 30;
            txt.Name = "tbData" + i;
            //txt.Text = txt.Name;
            topLoc += 3;
            return txt;
        }
        //this.Controls["label" + (i + 1).ToString()].Text = name[i];

        public string Function()
        {
            char char_temp;

            function.Text = "";
            string Temp=""; 

            for (int i = pangkat; i > 0; i--)
            {
                char_temp = (char)(65 + pangkat - i);
                string temp = char_temp + "x^" + i + " + ";

                Temp += temp;
            }
            char_temp = (char)(65 + pangkat);
            Temp += char_temp;
            return Temp;
        }

        public string Function(int[] koef)
        {
            string char_temp;

            function.Text = "";
            string Temp = "";

            for (int i = pangkat; i > 0; i--)
            {
                if (koef[pangkat - i] == 0)
                    continue;
                else
                {
                    char_temp = Convert.ToString(koef[pangkat - i]);
                    string temp = char_temp + "x^" + i + " + ";

                    Temp += temp;
                }
            }
            char_temp = Convert.ToString(koef[pangkat]);
            Temp += char_temp;
            return Temp;
        }

        public void Count(int[] koef)
        {
            double I = (h / 2.0), num1, num2, sum_temp = 0;

            for (double i = a; i < b; i+=h)
            {
                num1 = 0; num2 = 0;
                for (int j = pangkat; j >= 0; j--)
                {
                    num1 += koef[pangkat - j] * Math.Pow(i, j);
                    num2 += koef[pangkat - j] * Math.Pow(i+h, j);
                }
                sum_temp += num1 + num2;
            }
            
            I *= sum_temp;
            panel3.Visible = true;
            lblHasil.Text = Convert.ToString(I);
        }
    }
}
