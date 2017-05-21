/* -----------------------------------------------------------------------------------------------------------
    Program : Launcher
 *  Date : 2015.06.01
 *  Program by hansung Byun
 * -----------------------------------------------------------------------------------------------------------
 * Ver      Date
 * -----------------------------------------------------------------------------------------------------------
   1.0.0.1  런쳐 초기 작성
 * ----------------------------------------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Launcher
{
    public partial class Launcher : Form
    {

        const byte Ver_1 = 1;
        const byte Ver_2 = 0;
        const byte Ver_3 = 1;
        const byte Ver_4 = 0;
        private bool StartOn = true;


        public delegate void ResetCount(int no);
        public delegate bool UpdateFile(string copy_name);
        public delegate void UpdateAppendText(string msg);

        private BaseInfo BI = null;
        private UserDefine Def;
        private SocketManager socketManager = null;

        private bool ExitOn = false;
        private bool UpdateOn = false;

        public Launcher()
        {
            InitializeComponent();
            InitializeForm();
            InitRunMsg();

            socketManager = new SocketManager(BI, 0);
            socketManager.OnReceiveMsg += new ReceiveEventHandler(socketManager_OnReceiveMsg);

            Connecting(0, true);
        }

        public void InitializeForm()
        {
            BI = new BaseInfo();

            BI.log = new Log();
            BI.log.pathLog = Directory.GetCurrentDirectory() + "\\Log\\Launcher\\";
            BI.log.InitLog(lvLog);

            Def = new UserDefine(this, BI);
            Def.ReadReg();

            AddLog(Log.Type.LOG_NOR, "▶▶▶▶ Launcher Start Ver:{0}.{1}.{2}.{3} ◀◀◀◀", Ver_1, Ver_2, Ver_3, Ver_4);
        }

        void socketManager_OnReceiveMsg(object sender, SocketReceiveEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                AddLog(Log.Type.LOG_ERR, "socketManager_OnReceiveMsg Error : {0}", ex.Message);
            }
        }


        public bool InitRunMsg()
        {
            try
            {
                if (lvRun_Msg != null)
                {
                    lvRun_Msg.View = View.Details;
                    lvRun_Msg.LabelEdit = true;
                    lvRun_Msg.AllowColumnReorder = true;
                    lvRun_Msg.CheckBoxes = true;
                    lvRun_Msg.FullRowSelect = true;
                    lvRun_Msg.Sorting = SortOrder.None;

                    lvRun_Msg.Columns.Add("실행파일", 150, HorizontalAlignment.Center);
                    lvRun_Msg.Columns.Add("경로", 210, HorizontalAlignment.Left);
                    lvRun_Msg.Columns.Add("카운트", -2, HorizontalAlignment.Left);
                    lvRun_Msg.Columns.Add("재실행", -2, HorizontalAlignment.Left);
                    
                    ImageList imageListLog = new ImageList();
                    imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\ok.bmp"));
                    imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\cancel2.bmp"));

                    lvRun_Msg.SmallImageList = imageListLog;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0} => {1}",
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
                Console.WriteLine(msg);
                return false;
            }

            return true;
        }

        private void Connecting(int index, bool conn_on)
        {
            try
            {
                // 연결
                if (conn_on)
                {
                    bool ret = socketManager.CreateOpen();
                    if (ret == true)
                    {
                        switch (BI.CheckExe[index].ConnectType)
                        {
                            case BaseInfo.ConnectTypeDef.Server: AddLog(Log.Type.LOG_RUN, "Server IP:{0}, Port:{1} 연결 했습니다.", BI.CheckExe[index].Server.Ip, BI.CheckExe[index].Server.Port); break;
                            case BaseInfo.ConnectTypeDef.Client: AddLog(Log.Type.LOG_RUN, "Client IP:{0}, Port:{1} 연결 했습니다.", BI.CheckExe[index].Client.Ip, BI.CheckExe[index].Client.Port); break;
                            case BaseInfo.ConnectTypeDef.Serial: AddLog(Log.Type.LOG_RUN, "Serial Port:{0}, Baud:{1} 연결 했습니다.", BI.CheckExe[index].Serial.Port, BI.CheckExe[index].Serial.Baud); break;
                        }
                    }
                }
                else
                {
                    // 해제
                    switch (BI.CheckExe[index].ConnectType)
                    {
                        case BaseInfo.ConnectTypeDef.Server: AddLog(Log.Type.LOG_RUN, "Server IP:{0}, Port:{1} 연결 해제 했습니다.", BI.CheckExe[index].Server.Ip, BI.CheckExe[index].Server.Port); break;
                        case BaseInfo.ConnectTypeDef.Client: AddLog(Log.Type.LOG_RUN, "Client IP:{0}, Port:{1} 연결 해제 했습니다.", BI.CheckExe[index].Client.Ip, BI.CheckExe[index].Client.Port); break;
                        case BaseInfo.ConnectTypeDef.Serial: AddLog(Log.Type.LOG_RUN, "Serial Port:{0}, Baud:{1} 연결 해제 했습니다.", BI.CheckExe[index].Serial.Port, BI.CheckExe[index].Serial.Baud); break;
                    }
                    socketManager.Close();
                }
            }
            catch (Exception ex)
            {
                AddLog(Log.Type.LOG_ERR, "Connecting Error:{0}", ex.Message);
            }

        }

        private bool ProgramUpdate(string copy_name)
        {
            try
            {
                if (lvRun_Msg.InvokeRequired)
                {
                    lvRun_Msg.Invoke(new UpdateFile(ProgramUpdate), new object[] { copy_name });
                }
                else
                {
                    string exe_name = Path.GetFileName(copy_name);

                    foreach (ListViewItem item in lvRun_Msg.CheckedItems)
                    {
                        string run_name = item.SubItems[0].Text;
                        string run_path = item.SubItems[1].Text;

                        if (String.Compare(run_name, exe_name, true) == 0)
                        {
                            RunProcess run = new RunProcess();
                            string sou_name = Path.GetDirectoryName(run_path) + copy_name;

                            if (File.Exists(sou_name))
                            {
                                UpdateOn = true;
                                // 프로그램이 실행 중이면
                                if (run.IsRun(run_name))
                                {
                                    AddLog(Log.Type.LOG_RUN, "Kill {0} Program", run_name);
                                    run.Kill(Path.GetFileNameWithoutExtension(run_name));
                                    Delay(100);
                                }
                                AddLog(Log.Type.LOG_RUN, "Copy {0} -> {1}", sou_name, run_path);
                                File.Copy(sou_name, run_path, true);
                                Delay(100);
                                AddLog(Log.Type.LOG_RUN, "{0} Program Copy Ok!", run_name);

                                UpdateOn = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog(Log.Type.LOG_ERR, "Error ProgramUpdate Name {0}:{1}", copy_name, ex.Message);
            }
            return true;
        }

        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);
            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }


        public void ResetRestartCount(int no)
        {
            if (lvRun_Msg.InvokeRequired)
            {
                lvRun_Msg.Invoke(new ResetCount(ResetRestartCount), new object[] { no });
            }
            else
            {
                for(int i = 0; i < lvRun_Msg.CheckedItems.Count; i++)
                {
                    lvRun_Msg.CheckedItems[i].SubItems[2].Text = BI.CheckExe[i].ReStart_MaxTime.ToString();
                }
            }
        }

        public void ComMsgAdd(string msg)
        {
            if (lvLog.InvokeRequired)
            {
                lvLog.Invoke(new UpdateAppendText(ComMsgAdd), new object[] { msg });
            }
            else
            {
                AddLog(Log.Type.LOG_NOR, "Server", msg);
            }
        }

        private void AddRunMsg(string filename)
        {
            try
            {
/*                item1 = new ListViewItem(Path.GetFileName(ofdExe_FileOpen.FileName), 0);
                item1.SubItems.Add(ofdExe_FileOpen.FileName, Color.Aqua, Color.Black, new Font("굴림체", 10, FontStyle.Bold));
                item1.SubItems.Add(Def.ReStart_MaxTime.ToString());
                item1.SubItems.Add("0");
                lvRun_Msg.Items.Add(item1);
 */
            }
            catch (Exception ex)
            {
                AddLog(Log.Type.LOG_ERR, "Error OnAsyncServerConnet:{0}", ex.Message);
            }
        }

        private void btRun_Insert_Click(object sender, EventArgs e)
        {
            try
            {
                WatchSetup setup = new WatchSetup(this, BI);

                if (setup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                }

/*
                if (ofdExe_FileOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //string name;
                    //if(ofdExe_FileOpen.FileName.Length > 40)
                    //    name = ofdExe_FileOpen.FileName.Substring(ofdExe_FileOpen.FileName.Length-40);
                    //else
                    //    name = ofdExe_FileOpen.FileName;

                    RunMsg_Insert(ofdExe_FileOpen.FileName);
                    Def.WriteReg();
                }
 */ 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error btRun_Insert_Click {0}", ex.Message);
            }
        }

        public bool RunMsg_Insert(BaseInfo.CheckItems citem)
        {
            ListViewItem item1;
            int items_co = lvRun_Msg.Items.Count;

            item1 = new ListViewItem(Path.GetFileName(citem.Exe_Name), 0);
            item1.SubItems.Add(citem.Exe_Name, Color.Aqua, Color.Black, new Font("굴림체", 10, FontStyle.Bold));
            item1.SubItems.Add(citem.ReStart_MaxTime.ToString());
            item1.SubItems.Add("0");

            lvRun_Msg.Items.Add(item1);
            //lvRun_Msg.Items.Add(Path.GetFileName(name), 0);
            BI.CheckExe.Add(citem);

            lvRun_Msg.Items[items_co].Checked = true;
            if (lvRun_Msg.Items.Count > 0)
            {
                timer_RunCheck.Enabled = true;
            }
            return true;
        }


        private void btRun_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int co = lvRun_Msg.SelectedItems.Count;

                for (int i = 0; i < co; i++)
                {
                    int index = lvRun_Msg.SelectedItems[0].Index;
                    //BI.Exe_Name.RemoveAt(index);
                    lvRun_Msg.SelectedItems[0].Remove();
                }
                lvRun_Msg.Update();
                Def.WriteReg();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error btRun_Delete_Click {0}", ex.Message);
            }
        }

        private void timer_RunCheck_Tick(object sender, EventArgs e)
        {
            try
            {

                if (UpdateOn) return;
                if (BI.ReBootingOn)
                {
                    // Test 용
                    //Def.ReBootingTime = DateTime.Now.ToString("HH:mm");
                    if (BI.Week[(Byte)DateTime.Now.DayOfWeek])
                    {
                        string cur_time = DateTime.Now.ToString("HH:mm");
                        if (cur_time == BI.ReBootingTime)
                        {
                            RunProcess.ComputerRestart();
                            ExitOn = true;
                            Application.Exit();
                        }
                    }
                }

                if (lvRun_Msg.Items.Count > 0)
                {
                    RunProcess run = new RunProcess();

                    if (run.IsRun("WerFault"))
                    {
                        run.Kill("WerFault");
                        // 에러 메시지 창이 뜨면 뭐가 에러 난건지 구분을 못한다. 그래서 다 죽인다.
                        foreach (ListViewItem item in lvRun_Msg.CheckedItems)
                        {
                            string exe_name = item.SubItems[0].Text;
                            run.Kill(Path.GetFileNameWithoutExtension(exe_name));
                        }
                    }

                    for(int i = 0; i < lvRun_Msg.Items.Count; i++)
                    {
                        ListViewItem item = lvRun_Msg.CheckedItems[i];
                        string exe_name = item.SubItems[0].Text;
                        string exe_path = item.SubItems[1].Text;
                        int ReStart_Time = Convert.ToInt32(item.SubItems[2].Text);
                        int ReStart_Co = Convert.ToInt32(item.SubItems[3].Text);

                        if (run.IsRunClass(exe_path))
                        {
                            run.Kill(Path.GetFileNameWithoutExtension(exe_name));
                        }

                        if (run.IsRun(exe_path))
                        {
                            // 실행 중 일때
                            if (ReStart_Time >= 0)
                            {
                                ReStart_Time--;
                                if (ReStart_Time <= 0)
                                {
                                    run.Kill(Path.GetFileNameWithoutExtension(exe_name));
                                    ReStart_Time = BI.CheckExe[i].ReStart_MaxTime;
                                    AddLog(Log.Type.LOG_RUN, "Kill Process : {0}", exe_name);
                                }
                                item.SubItems[2].Text = ReStart_Time.ToString();
                            }
                        }
                        else
                        {
                            // 실행중이 아닐때
                            if (run.Start(exe_path))        // 에러가 아닐때
                            {
                                item.SubItems[2].Text = BI.CheckExe[i].ReStart_MaxTime.ToString();
                                ReStart_Co++;
                                item.SubItems[3].Text = ReStart_Co.ToString();
                                AddLog(Log.Type.LOG_RUN, "Run Process : {0}", exe_name);
                            }
                            else
                            {
                                AddLog(Log.Type.LOG_ERR, "해당파일이 없거나 실행하지 못했습니다.");
                                item.BackColor = Color.Gray;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                AddLog(Log.Type.LOG_ERR, "Error timer_RunCheck_Tick:{0}", ex.Message);
            }
        }

        private void btSet_Setup_Click(object sender, EventArgs e)
        {
            Form_Setup setup = new Form_Setup();
            setup.InitializeForm(BI);

            if (setup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void lvRun_Msg_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!e.Item.Checked)
            {
                e.Item.ImageIndex = 1;
                e.Item.BackColor = Color.White;
                e.Item.ForeColor = Color.Black;
            }
            else
            {
                e.Item.ImageIndex = 0;
                e.Item.BackColor = Color.FromArgb(150, 156, 251, 156);
                e.Item.ForeColor = Color.FromArgb(255, 0, 146, 0);

            }
        }

        // 종료 버튼
        private void btSet_Exit_Click(object sender, EventArgs e)
        {
            ExitOn = true;
            Application.Exit();
        }

        // 트레이아이콘
        private void btSet_TryIcon_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            notifyIcon1.Visible = true;
        }

        // 트레이 아이콘화 일때 더블클릭
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ExitOn)
            {
                if (socketManager != null)
                {
                    socketManager.OnReceiveMsg -= new ReceiveEventHandler(socketManager_OnReceiveMsg);
                    socketManager.Close();
                }
                e.Cancel = true;
                this.Visible = false;
                notifyIcon1.Visible = true;
            }
        }

        private void 설정화면ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitOn = true;
            Application.Exit();
        }

        private void Launcher_Paint(object sender, PaintEventArgs e)
        {
            if (StartOn)
            {
                if (BI.TrayIconOn)
                {
                    this.Visible = false;
                    notifyIcon1.Visible = true;
                }
                StartOn = false;
            }
        }

        private void AddLog(Log.Type type, string format, params object[] args)
        {
            if (BI.log != null)
            {
                BI.log.Add(type, "Main", format, args);
            }
        }
    }
}
