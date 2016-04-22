using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastQueryStock.Controls
{
    public class NotificationTip
    {
        static NotifyIcon notifyIcon = new NotifyIcon();
        static NotificationTip()
        {
            notifyIcon.Visible = true;
            notifyIcon.Icon = new System.Drawing.Icon(Path.GetFullPath(@"Images\stockicon.ico"));
            notifyIcon.Text = "TWSE Stock";
        }


        public static void Show(string balloonTitle, string balloonText)
        {
            try
            {
                notifyIcon.ShowBalloonTip(20000, balloonTitle, balloonText, ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
