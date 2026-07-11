using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thunderClient
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // 載入設定
            this.Icon = System.Drawing.SystemIcons.Shield;
            txtUrl.Text = (String)Properties.Settings.Default.ThunderUrl;
            numRange.Value = (decimal)Properties.Settings.Default.AlarmRangeKm;
            numLon.Value = (decimal)Properties.Settings.Default.ThisLon;
            numLat.Value = (decimal)Properties.Settings.Default.ThisLat;
            numTime.Value = (decimal)Properties.Settings.Default.AlarmTimeRangeSec;
            cmbAction.SelectedIndex = Properties.Settings.Default.ActionMode;
            txtCmd.Text = Properties.Settings.Default.ActionCmd;
            numCk.Value = (decimal)Properties.Settings.Default.CheckInterval;
            ckbNotice.Checked = Properties.Settings.Default.NoticeMode;
            // 檢查 Startup
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            if (key.GetValue("thunderClient") != null)
            {
                if (key.GetValue("thunderClient").ToString() == Application.ExecutablePath)
                {
                    ckbStartup.Checked = true;
                }
                else
                {
                    MessageBox.Show("檢測到開機啟動的程式路徑與目前程式路徑不符，程式將會自動修正位置。", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    key.SetValue("thunderClient", Application.ExecutablePath);
                    ckbStartup.Checked = true;
                }
            }
            else
            {
                ckbStartup.Checked = false;
            }
            

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // 儲存設定
            if (await TestUrl(txtUrl.Text))
            {
                Properties.Settings.Default.ThunderUrl = txtUrl.Text;
            }
            else
            {
                MessageBox.Show("URL 無效，請檢查輸入的網址是否正確。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if ((int)numCk.Value < 20)
                {
                    MessageBox.Show(
                        "通常中央氣象局雷擊資料每五分鐘更新一次，檢查時間極短會導致電腦資源的浪費以及提早到達 API 使用限制。", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning
                    );
                }
                Properties.Settings.Default.CheckInterval = (int)numCk.Value;
                
            }
            catch
            {
                MessageBox.Show("檢查間隔時間太長", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
                
            try
            {
                Properties.Settings.Default.AlarmRangeKm = (double)numRange.Value;
                if ((double)numRange.Value < 2.0)
                {
                    MessageBox.Show(
                        "設定的偵測範圍可能太小。", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning
                    );
                }
            }
            catch
            {
                MessageBox.Show("防護偵測範圍太大", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                Properties.Settings.Default.ThisLon = (double)numLon.Value;
                Properties.Settings.Default.ThisLat = (double)numLat.Value;

            }
            catch
            {
                MessageBox.Show("經緯度輸入錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            try
            {
                Properties.Settings.Default.AlarmTimeRangeSec = (double)numTime.Value;
            }
            catch
            {
                MessageBox.Show("防護偵測時間太長", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (cmbAction.SelectedIndex == 4)
            {
                if (!CheckExecutablePath(txtCmd.Text))
                {
                    MessageBox.Show("請輸入有效的可執行檔路徑", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Properties.Settings.Default.ActionMode = cmbAction.SelectedIndex;
                    Properties.Settings.Default.ActionCmd = txtCmd.Text;
                }
                
            }
            else
            {
                Properties.Settings.Default.ActionMode = cmbAction.SelectedIndex;
            }
            Properties.Settings.Default.NoticeMode = ckbNotice.Checked;
            Properties.Settings.Default.Save();
            MessageBox.Show("設定已儲存", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ckbStartup_Changed(object sender, EventArgs e)
        {
            // 設定開機啟動
            if (ckbStartup.Checked)
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                if (key.GetValue("thunderClient") != null)
                {
                    if (key.GetValue("thunderClient").ToString() != Application.ExecutablePath)
                    {
                        MessageBox.Show(
                            "檢測到開機啟動的程式路徑與目前程式路徑不符，程式將會自動修正位置。",
                            "提醒",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        key.SetValue("thunderClient", Application.ExecutablePath);
                        ckbStartup.Checked = true;
                        return;
                    }
                }
                else
                {
                    var startupCheck = MessageBox.Show(
                        "請先將程式移到您想固定存放的資料夾，再開啟此功能。如果事後移動程式的位置，自動啟動將會失效。",
                        "提醒",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );
                        if (startupCheck == DialogResult.No)
                        {
                            ckbStartup.Checked = false;
                            return;
                        }
                        else
                        {
                            key.SetValue("thunderClient", Application.ExecutablePath);
                        }
                }
            }
            else
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                if (key.GetValue("thunderClient") != null)
                {
                    key.DeleteValue("thunderClient", false);
                }
            }
        }

        private async Task<bool> TestUrl(string url, int timeout = 3000)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(timeout);
                    var request = new HttpRequestMessage(HttpMethod.Head, url);

                    HttpResponseMessage response = await client.SendAsync(request);

                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool CheckExecutablePath(string filepath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filepath))
                    return false;

                // 檢查檔案是否存在
                if (!File.Exists(filepath))
                    return false;

                // 檢查副檔名
                string ext = Path.GetExtension(filepath).ToLower();

                if (ext != ".exe" && ext != ".bat" && ext != ".cmd")
                    return false;

                // 檢查是否可讀取
                using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
                {
                    // 如果能成功開啟，就代表可存取
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
