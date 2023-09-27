using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ToggleMuter
{
    public partial class HotKeySettingForm : Form
    {
        public HotKeySettingForm()
        {
            InitializeComponent();
        }

        public Keys SelectedHotkey { get; private set; }
        public List<Keys> HotkeyKeys { get; set; } = new List<Keys>();
        public int KeyCount { get; set; } = 0;

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            // ホットキーが設定されていない場合はエラーを表示
            if (HotkeyKeys.Count == 0)
            {
                _ = MessageBox.Show("ホットキーが設定されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // OKボタンがクリックされたときにフォームを閉じる
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void HotKeySettingForm_KeyDown(object sender, KeyEventArgs e)
        {
            //　押下されたキーが一つの場合、リストをクリアして押下されたキーを追加
            if (KeyCount == 0)
            {
                HotkeyKeys.Clear();
                HotkeyKeys.Add(e.KeyCode);
                KeyCount++;
            }
            else
            {
                if (!HotkeyKeys.Contains(e.KeyCode))
                {
                    HotkeyKeys.Add(e.KeyCode);
                    KeyCount++;
                }
            }

            // ホットキーのリストをラベルに表示
            labelHotKey.Text = string.Join(" + ", HotkeyKeys);
        }

        private void HotKeySettingForm_KeyUp(object sender, KeyEventArgs e)
        {
            KeyCount--;
        }

        public List<Keys> GetHotkeyKeys()
        {
            return HotkeyKeys;
        }


    }
}