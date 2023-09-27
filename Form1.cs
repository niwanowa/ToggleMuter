using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ToggleMuter
{
    public partial class MainForm : Form
    {
        //タスクバーにアイコンを表示するためのクラス
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETICON = 0x80;
        private const int ICON_SMALL = 0;
        private const int ICON_BIG = 1;

        //ミュート設定フラグ
        //true:ミュート中 false:ミュート解除中
        private bool isMuted = false;

        //ホットキーが押されると発火するイベント
        private HotKey hotKey = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //ListBoxに実行中のプロセス読み込み
            LoadAppList();

            // TimerのTickイベントハンドラを設定
            timer1.Tick += Timer_Tick;
            // Timerを開始
            timer1.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //ListBoxに実行中のプロセス読み込み
            LoadAppList();
        }

        private void LoadAppList()
        {
            // AudioSessionManagerをインスタンス化してプロセス情報を取得
            AudioSessionManager audioSessionManager = new AudioSessionManager();
            ProcessInfo[] processInfoList = audioSessionManager.GetProcessInfoList();

            //選択されたアイテムを保持
            int selectedIndex = appList.SelectedIndex;
            appList.Items.Clear();

            // プロセス情報を辞書型に追加
            Dictionary<int, string> processDictionary = new Dictionary<int, string>();
            if (processInfoList != null)
            {
                foreach (ProcessInfo info in processInfoList)
                {
                    // プロセスIDとプロセス名を辞書型に追加
                    processDictionary.Add(info.ProcessId, info.ProcessName);

                    // プロセス名だけlistBoxに追加
                    _ = appList.Items.Add(info.ProcessName + "(" + info.ProcessId + ")");
                }
            }

            RemoveMissingItems(appList, IgnoredList);

            // 選択されたアイテムを復元
            if (selectedIndex >= 0 && selectedIndex < appList.Items.Count)
            {
                appList.SelectedIndex = selectedIndex;
            }

            // 選択されたアイテムの変更イベントをハンドル
            appList.SelectedIndexChanged += appList_SelectedIndexChanged;

            // 辞書型をTagプロパティに設定して保持
            appList.Tag = processDictionary;
        }

        private void appList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //do nothing
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            moveProcess();
        }

        private void moveProcess()
        {
            Dictionary<int, string> processDictionary = (Dictionary<int, string>)appList.Tag;
            int processId = getSelectedProcessId();
            string processName = processDictionary[processId];
            string listText = processName + "(" + processId + ")";

            if (!IgnoredList.Items.Contains(listText))
            {
                _ = IgnoredList.Items.Add(listText);
            }
            else
            {
                IgnoredList.Items.Remove(listText);
            }
        }

        private int getSelectedProcessId()
        {
            if (appList.SelectedItems.Count > 0)
            {
                int selectedIndex = appList.SelectedIndex;
                Dictionary<int, string> processDictionary = (Dictionary<int, string>)appList.Tag;

                // 選択されたアイテムからプロセスIDを取得し、辞書からプロセス名を取得
                int processId = processDictionary.Keys.ElementAt(selectedIndex);
                return processId;
            }
            return -1;
        }

        private void muteButton_Click(object sender, EventArgs e)
        {
            execMute(null, null);
        }

        private void execMute(object sender, EventArgs e)
        {
            isMuted = !isMuted;
            muteButton.Text = isMuted ? "ミュート解除" : "ミュート";
            List<int> IgnoredProcessIDs = new List<int>();
            foreach (string listText in IgnoredList.Items)
            {
                int start = listText.IndexOf("(") + 1;
                int end = listText.IndexOf(")");
                string processID = listText.Substring(start, end - start);
                IgnoredProcessIDs.Add(int.Parse(processID));
            }

            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            SessionCollection sessions = defaultDevice.AudioSessionManager.Sessions;
            for (int i = 0; i < sessions.Count; i++)
            {
                AudioSessionControl session = sessions[i];
                if (!IgnoredProcessIDs.Contains(Convert.ToInt32(session.GetProcessID)))
                {
                    session.SimpleAudioVolume.Mute = isMuted;
                }
            }

            //タスクバーのアイコンを更新
            Icon appIcon = isMuted ? Properties.Resources.icon_volume_x : Properties.Resources.icon_volume;
            _ = SendMessage(Handle, WM_SETICON, (IntPtr)ICON_SMALL, appIcon.Handle);
            _ = SendMessage(Handle, WM_SETICON, (IntPtr)ICON_BIG, appIcon.Handle);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // do nothing
        }

        private void buttonSettingHotKey_Click(object sender, EventArgs e)
        {
            //ホットキーを設定するための新しいフォームを表示
            HotKeySettingForm hotKeySettingForm = new HotKeySettingForm();
            _ = hotKeySettingForm.ShowDialog();

            //フォームが閉じられた後にホットキーの設定を行う
            List<Keys> hotkeyKeys = new List<Keys>(hotKeySettingForm.GetHotkeyKeys());

            //ホットキーが設定されていない場合は何もしない
            if (hotkeyKeys.Count == 0)
            {
                return;
            }

            //ホットキーの設定をラベルに表示
            testlabel.Text = "ホットキー設定：" + string.Join(" + ", hotkeyKeys);

            int modKey = 0x0000;
            if (hotkeyKeys.Contains(Keys.ControlKey))
            {
                modKey |= 0x0002;
                _ = hotkeyKeys.Remove(Keys.ControlKey);
            }
            if (hotkeyKeys.Contains(Keys.Menu))
            {
                modKey |= 0x0001;
                _ = hotkeyKeys.Remove(Keys.Menu);
            }
            if (hotkeyKeys.Contains(Keys.ShiftKey))
            {
                modKey |= 0x0004;
                _ = hotkeyKeys.Remove(Keys.ShiftKey);
            }

            //ホットキーを設定する。
            hotKey = new HotKey(modKey, hotkeyKeys[0]);
            hotKey.HotKeyPush += new EventHandler(execMute);
        }

        public static void RemoveMissingItems(ListBox listBoxA, ListBox listBoxB)
        {
            for (int i = listBoxB.Items.Count - 1; i >= 0; i--)
            {
                string itemB = listBoxB.Items[i].ToString();
                if (!listBoxA.Items.Contains(itemB))
                {
                    listBoxB.Items.RemoveAt(i);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            hotKey?.Dispose();
        }

        private void PressSettingHotKey(object sender, PreviewKeyDownEventArgs e)
        {
            //spaceキーが押されたら、moveProcessを実行
            if (e.KeyCode == Keys.Space)
            {
                moveProcess();
            }
        }


    }

    public class AudioSessionManager
    {
        private readonly MMDevice defaultPlaybackDevice;

        public AudioSessionManager()
        {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            defaultPlaybackDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }

        public ProcessInfo[] GetProcessInfoList()
        {
            if (defaultPlaybackDevice != null)
            {
                NAudio.CoreAudioApi.AudioSessionManager sessionManager = defaultPlaybackDevice.AudioSessionManager;
                SessionCollection sessions = sessionManager.Sessions;

                ProcessInfo[] processInfoList = new ProcessInfo[sessions.Count];
                for (int i = 0; i < sessions.Count; i++)
                {
                    AudioSessionControl session = sessions[i];
                    uint processId = session.GetProcessID;
                    System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessById((int)processId);

                    processInfoList[i] = new ProcessInfo
                    {
                        ProcessId = (int)processId,
                        ProcessName = process.ProcessName
                    };
                }

                return processInfoList;
            }

            return new ProcessInfo[1];
        }
    }
    public class ProcessInfo
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
    }
}
