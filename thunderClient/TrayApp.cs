using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thunderClient
{
    internal class TrayApp : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private System.Windows.Forms.Timer timer;
        private thunderDetector detector;
        private bool thunderNotified = false;

        public TrayApp()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = System.Drawing.SystemIcons.Shield,
                Visible = true,
                ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(),
                Text = "Wayne 雷擊防護程式"
            };
            

            trayIcon.ContextMenuStrip.Items.Add("程式設定", null, OnSettings);
            trayIcon.ContextMenuStrip.Items.Add("離開", null, OnExit);

            detector = new thunderDetector(
                Properties.Settings.Default.ThunderUrl,
                Properties.Settings.Default.AlarmRangeKm,
                Properties.Settings.Default.ThisLon,
                Properties.Settings.Default.ThisLat,
                Properties.Settings.Default.AlarmTimeRangeSec
            );

            timer = new System.Windows.Forms.Timer();
            timer.Interval = Properties.Settings.Default.CheckInterval * 1000; // Check every 20 seconds
            timer.Tick += async (s, e) => await CheckThunder();
            timer.Start();
        }

        public async Task CheckThunder()
        {
            bool thunder = await detector.CheckThunderAsync();
            if (thunder && !thunderNotified)
            {
                thunderNotified = true;
                ShowWarning(Properties.Settings.Default.ActionMode);
            }
            else if (!thunder)
            {
                thunderNotified = false;
            }
        }

        public void ShowWarning(int actionType)
        {
            switch (actionType)
            {
                case 0:
                    if (Properties.Settings.Default.NoticeMode)
                    {
                        MessageBox.Show(
                            "已經偵測到規定範圍內有落雷，請注意安全！",
                            "宜立科技雷擊防護程式",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                    trayIcon.ShowBalloonTip(15000, "落雷警告", "已經偵測到規定範圍內有落雷，請注意安全！", ToolTipIcon.Warning);
                    break;
                case 1:
                    try
                    {
                        System.Diagnostics.Process.Start("shutdown", "/s /f /t 10");
                        if (Properties.Settings.Default.NoticeMode)
                        {
                            MessageBox.Show(
                                "已經偵測到規定範圍內有落雷，系統將在10秒後關機，請盡速儲存資料",
                                "宜立科技雷擊防護程式",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                        trayIcon.ShowBalloonTip(15000, "落雷警告", "已經偵測到規定範圍內有落雷，系統將進行關機！", ToolTipIcon.Warning);
                          
                    }
                    catch (Exception ex)
                    {
                        if (Properties.Settings.Default.NoticeMode)
                        {
                            MessageBox.Show("無法執行關機：" + ex.Message, "宜立科技雷擊防護程式", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        trayIcon.ShowBalloonTip(15000, "錯誤", "無法執行關機：" + ex.Message, ToolTipIcon.Warning);

                    }
                    break;
                case 2:
                    trayIcon.ShowBalloonTip(15000, "落雷警告", "已經偵測到規定範圍內有落雷，請注意安全！", ToolTipIcon.Warning);
                    var toShut = MessageBox.Show(
                            "已經偵測到規定範圍內有落雷，您現在要關機嗎？",
                            "宜立科技雷擊防護程式",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question
                    );

                    if (toShut == DialogResult.OK)
                    {
                        System.Diagnostics.Process.Start("shutdown", "/s /f /t 0");
                    }
                    break;
                case 3:
                    if (Properties.Settings.Default.NoticeMode)
                    {
                        MessageBox.Show(
                            "已經偵測到規定範圍內有落雷，請拔除交流電電源。",
                            "宜立科技雷擊防護程式",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                    trayIcon.ShowBalloonTip(15000, "落雷警告", "已經偵測到規定範圍內有落雷，請拔除交流電電源。", ToolTipIcon.Warning);
                    break;
                case 4:
                    trayIcon.ShowBalloonTip(15000, "落雷警告", "已經偵測到規定範圍內有落雷，已執行應對策略。", ToolTipIcon.Warning);
                    try
                    {
                        System.Diagnostics.Process.Start(Properties.Settings.Default.ActionCmd);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("無法執行對應策略：" + ex.Message, "宜立科技雷擊防護程式", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void OnSettings(object sender, EventArgs e)
        {
            // Open settings window
            SettingsForm settings = new SettingsForm();
            settings.Show();
        }

        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
}
}
