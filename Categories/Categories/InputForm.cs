using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Categories
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }

        private void InputForm_Activated(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("catname.txt");
            textBox1.Text = sr.ReadLine();
            sr.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                StreamWriter sw = new StreamWriter("catname.txt");
                sw.WriteLine(textBox1.Text);
                sw.Close();
                Close();
            }
            //else if (e.KeyChar == 27 && textBox1.Text == "NONAME")
            //{
            //    StreamWriter sw = new StreamWriter("catname.txt");
            //    sw.WriteLine("");
            //    sw.Close();
            //    Close();
            //}
        }

        private void InputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing && textBox1.Text == "NONAME")
            //{
            //    StreamWriter sw = new StreamWriter("catname.txt");
            //    sw.WriteLine("");
            //    sw.Close();
            //    Close();
            //}
        }
    }
}