using System;
using System.Windows.Forms;

namespace Braz
{
    public partial class Form1 : Form
    {
        
        public static string Page_link { get; set; }
        public string Magnet_link { get; private set; }

        public Form1()
        {
            InitializeComponent();
            textBox1.Select();
           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            LinkProv lp = new LinkProv();
            Page_link=lp.Give_link(textBox1.Text);
            if (Page_link == "invalid")
            {
                MessageBox.Show("No results. Please try a different string.","Try again", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Hand);
                return;
            }
            else if (Page_link == "gg internet")
            {
                var result=MessageBox.Show("Please check your internet connection and try again.", "Try again", buttons: MessageBoxButtons.AbortRetryIgnore, icon: MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    Button1_Click(sender, e);
                }
                else if (result == DialogResult.Abort)
                {
                    this.Close();
                }
                return;
            }
            Magnet_gen mg = new Magnet_gen();
            Magnet_link = mg.Give_magnet(Page_link);
            if (Magnet_link == null)
            {
                MessageBox.Show("The site seems down. Try again later.", "Internal Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Clipboard.SetText(Magnet_link);
                MessageBox.Show("Copied to clipboard. Ctrl + V to paste.", "Copied");
            }
            //MessageBox.Show(Magnet_link);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 3)
            {
                button1.Enabled = true;
            }
        }


    }
}
