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
using NAudio.CoreAudioApi;

namespace ToggleMuter
{
    public partial class MainForm : Form
    {
        private bool isMuted = false;
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
            appList.Items.Clear();

            // プロセス情報を辞書型に追加
            Dictionary<int, string> processDictionary = new Dictionary<int, string>();
            if (processInfoList != null)
            {
                foreach (var processInfo in processInfoList)
                {
                    // プロセスIDとプロセス名を辞書型に追加
                    processDictionary.Add(processInfo.ProcessId, processInfo.ProcessName);

                    // プロセス名だけをlistBoxに追加
                    appList.Items.Add(processInfo.ProcessName + "(" + processInfo.ProcessId + ")");
                }
            }

            // 選択されたアイテムの変更イベントをハンドル
            appList.SelectedIndexChanged += appList_SelectedIndexChanged;

            // 辞書型をTagプロパティに設定して保持
            appList.Tag = processDictionary;
        }

        private void appList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<int, string> processDictionary = (Dictionary<int, string>)appList.Tag;
            int processId = getSelectedProcessId();
            string processName = processDictionary[processId];

            testlabel.Text = "プロセス名: " + processName + ", プロセスID: " + processId.ToString();
        }

        private void movButton_Click(object sender, EventArgs e)
        {
            Dictionary<int, string> processDictionary = (Dictionary<int, string>)appList.Tag;
            int processId = getSelectedProcessId();
            string processName = processDictionary[processId];
            String listText = processName + "(" + processId + ")";

            if (!IgnoredList.Items.Contains(listText))
            {
                IgnoredList.Items.Add(listText);
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

                // 選択されたアイテムからプロセスIDを取得し、辞書型からプロセス名を取得
                int processId = (int)processDictionary.Keys.ElementAt(selectedIndex);
                return processId;
            }
            return -1;
        }

        private void muteButton_Click(object sender, EventArgs e)
        {
            isMuted = !isMuted;
            muteButton.Text = isMuted ? "ミュート解除" : "ミュート";
            List<int> IgnoredProcessIDs = new List<int>();
            foreach (String listText in IgnoredList.Items)
            {
                int start = listText.IndexOf("(") + 1;
                int end = listText.IndexOf(")");
                String processID = listText.Substring(start, end - start);
                IgnoredProcessIDs.Add(Int32.Parse(processID));
            }

            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            var sessions = defaultDevice.AudioSessionManager.Sessions;
            for (int i = 0; i < sessions.Count; i++)
            {
                var session = sessions[i];
                if (!IgnoredProcessIDs.Contains(Convert.ToInt32(session.GetProcessID)))
                {
                    session.SimpleAudioVolume.Mute = !session.SimpleAudioVolume.Mute;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }

    public class AudioSessionManager
    {
        private MMDeviceEnumerator deviceEnumerator;
        private MMDevice defaultPlaybackDevice;

        public AudioSessionManager()
        {
            deviceEnumerator = new MMDeviceEnumerator();
            defaultPlaybackDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }

        public ProcessInfo[] GetProcessInfoList()
        {
            if (defaultPlaybackDevice != null)
            {
                var sessionManager = defaultPlaybackDevice.AudioSessionManager;
                var sessions = sessionManager.Sessions;

                ProcessInfo[] processInfoList = new ProcessInfo[sessions.Count];
                for (int i = 0; i < sessions.Count; i++)
                {
                    var session = sessions[i];
                    var processId = session.GetProcessID;
                    var process = System.Diagnostics.Process.GetProcessById((int)processId);

                    processInfoList[i] = new ProcessInfo
                    {
                        ProcessId = (int)processId,
                        ProcessName = process.ProcessName
                    };
                }

                return processInfoList;
            }

            return null;
        }
    }
    public class ProcessInfo
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
    }
}
