using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToggreMuter
{
    public partial class HotKeySettingForm : Form
    {
        private List<Keys> hotkeyKeys = new List<Keys>();

        int keyCount = 0;
        public HotKeySettingForm()
        {
            InitializeComponent();
        }

        public Keys SelectedHotkey { get; private set; }
        public List<Keys> HotkeyKeys { get => hotkeyKeys; set => hotkeyKeys = value; }
        public int KeyCount { get => keyCount; set => keyCount = value; }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            // OKボタンがクリックされたときにフォームを閉じる
            DialogResult = DialogResult.OK;
            Close();
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
                if(!HotkeyKeys.Contains(e.KeyCode))
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