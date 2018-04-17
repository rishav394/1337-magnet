using System;
using System.Windows.Forms;

namespace Braz
{
    class Magnet_gen
    {
        WebBrowser wb = new WebBrowser();
        private string link;

        public Magnet_gen() => wb.ScriptErrorsSuppressed = true;

        public string Give_magnet(string address)
        {

            //MessageBox.Show(address);
            wb.Navigate(address);
            wb.DocumentCompleted += Wb_DocumentCompleted;
            while (wb.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
            }
            return link;
            
        }

        private void Wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            foreach (HtmlElement hm in wb.Document.GetElementsByTagName("a"))
            {
                if(hm.GetAttribute("href").Length > 200)
                {
                    link = hm.GetAttribute("href");
                    //MessageBox.Show(link);
                    break;
                }
            }
        }
    }
}
