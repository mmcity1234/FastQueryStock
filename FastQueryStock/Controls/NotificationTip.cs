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
        public static void Show(string text, string balloonTitle, string balloonText)
        {
            try
            {
                notifyIcon.Visible = true;
                notifyIcon.Icon = new System.Drawing.Icon(Path.GetFullPath(@"Images\stockicon.ico"));
                notifyIcon.Text = text;
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.BalloonTipTitle = balloonTitle;
                notifyIcon.BalloonTipText = balloonText;
                notifyIcon.ShowBalloonTip(3000);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
