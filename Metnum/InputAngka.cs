using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metnum
{
    public partial class InputAngka : UserControl
    {
        public InputAngka()
        {
            InitializeComponent();
        }

        int a = 1;

        public TextBox AddNewTextBox()
        {
            TextBox txt = new TextBox();
            this.Controls.Add(txt);
            txt.Top = a * 10;
            txt.Left = 15;
            txt.Text = txt.Name;
            a += 1;
            return txt;
        }
    }
}
