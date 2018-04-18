using System;
using System.Windows.Forms;

namespace Braz
{
    class LinkProv
    {
        
        private string link;
        WebBrowser wb = new WebBrowser();

        public LinkProv() => wb.ScriptErrorsSuppressed = true;

        private void Wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //MessageBox.Show(wb.DocumentTitle);
            if (wb.DocumentTitle.StartsWith("Error"))
            {
                link = "invalid";
                return;
            }
            else if (wb.DocumentTitle.StartsWith("Navigation")){
                link = "gg internet";
                return;
            }
            else if (wb.DocumentTitle.Contains("cloud"))
            {
                link = "ddos";
                return;
            }
            foreach (HtmlElement hm in wb.Document.GetElementsByTagName("a"))
            {
                if (hm.GetAttribute("title") == "" && hm.GetAttribute("className") == "" &&
                    hm.GetAttribute("target") == "" && (hm.GetAttribute("href").Length > (Form1.to_is_live?25:45)) )
                {
                    link=hm.GetAttribute("href");
                    break;
                }
            }
          
        }

        public string Give_link(string name_of_stuff)
        {
            if(!Form1.to_is_live)
                wb.Navigate("https://1337x1.unblocked.lol/search/" + name_of_stuff + "/1/");
            else
                wb.Navigate("https://1337x.to/search/" + name_of_stuff + "/1/");
            wb.DocumentCompleted += Wb_DocumentCompleted;
            while (wb.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
            }
            //MessageBox.Show(link);
            return link;

        }
       
    }
}
