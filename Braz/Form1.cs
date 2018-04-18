using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.Event;   
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using CefSharp.Handler;

namespace Braz
{
    public partial class Form1 : Form
    {
        public static bool to_is_live = true;
        public static string Page_link;
        public string Magnet_link { get; private set; }
        ChromiumWebBrowser myBrowser = new ChromiumWebBrowser();
        

        public Form1()
        {
            InitializeComponent();
            MessageBoxManager.OK = "Alright";
            MessageBoxManager.Yes = "Yep!";
            MessageBoxManager.Ignore = "Try Proxy";
            MessageBoxManager.No = "Nope";
            MessageBoxManager.Register();
            myBrowser.Dock = DockStyle.Bottom;
            myBrowser.Width = 1042;
            myBrowser.Height = 442;
            this.Controls.Add(myBrowser);
            myBrowser.Focus();
            myBrowser.Load("https://www.brazzers.com/home/");
            myBrowser.AddressChanged += MyBrowser_AddressChanged;

        }
        
        private void MyBrowser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            //MessageBox.Show("s");
            
            if (e.Address.ToString().Contains("www.brazzers.com/scenes/view/id/"))
            {
                string[] s = e.Address.ToString().Split('/');
                //alert("s");
                textBox1.Text = s[7];
            }
        }

       
        
        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            LinkProv lp = new LinkProv();
            Page_link = lp.Give_link(textBox1.Text);
            if (Page_link == "invalid")
            {
                MessageBox.Show("No results. Please try a different string.", "Try again", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Hand);
                button1.Enabled = true;
                return;
            }
            else if (Page_link == "gg internet")
            {
                var result = MessageBox.Show("Please check your internet connection and try again.", "Try again", buttons: MessageBoxButtons.AbortRetryIgnore, icon: MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    Button1_Click(sender, e);
                }
                else if (result == DialogResult.Abort)
                {
                    this.Close();
                }
                else if (result == DialogResult.Ignore)
                {
                     to_is_live = false;
                     Button1_Click(sender, e);  
                }
                button1.Enabled = true;
                return;
            }
            else if (Page_link == "ddos")
            {
                var result = MessageBox.Show("CloudFlare DDOS protection. Try with proxy site?.", "Human Verification",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    to_is_live = false;
                    Button1_Click(sender, e);
                }
                button1.Enabled = true;
                return;
            }
            //MessageBox.Show(Page_link);
            Magnet_gen mg = new Magnet_gen();
            Magnet_link = mg.Give_magnet(Page_link);
            if (Magnet_link == null)
            {
                var result = MessageBox.Show("The site seems down. Try again with proxy site?", "Internal Error", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    to_is_live = false;
                    Button1_Click(sender, e);
                }
                button1.Enabled = true;
                return;
            }
            Clipboard.SetText(Magnet_link);
            MessageBox.Show("Copied to clipboard. Ctrl + V to paste.", "Copied");

            //MessageBox.Show(Magnet_link);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 3)
            {
                button1.Enabled = true;
            }
            if (textBox1.Text.Length <= 3)
            {
                button1.Enabled = false;
            }
        }
    }
}
