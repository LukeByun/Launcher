using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Launcher
{
    partial class WatchSetup : Form
    {
        private Launcher _MainForm;
        BaseInfo BI;
        BaseInfo.CheckItems CheckItem;

        public WatchSetup(Launcher main_form, BaseInfo ini)
        {
            _MainForm = main_form;
            BI = ini;
            InitializeComponent();
            InitializeForm();
            CheckItem = new BaseInfo.CheckItems();
        }

        public void InitializeForm()
        {
            string[] baudrate = { "2400", "4800", "9600", "19200", "38400", "57600", "115200" };

            try
            {
                cbSetup_SerialPort.Items.Add("None");
                foreach (string s in Serial_Port.GetPortNames())
                {
                    cbSetup_SerialPort.Items.Add(s);
                }
                cbSetup_SerialPort.SelectedIndex = 0;

                foreach (string s in baudrate)
                {
                    cbSetup_SerialBaud.Items.Add(s);
                }
                cbSetup_SerialBaud.SelectedIndex = 6;

                rbSetup_Server.Checked = true;
                Display(0);

            }
            catch (Exception ex)
            {
                AddLog(Log.Type.LOG_ERR, "InitializeForm Error : {0}", ex.Message);
            }
        }

        private bool Display(int index)
        {
            bool result = false;

            try
            {
                if (BI.CheckExe == null) return false;
                if (BI.SelectIndex == BI.CheckExe.Count)
                {
                    SetDefaultView();
                }
                else
                {
                    switch (BI.CheckExe[index].ConnectType)
                    {
                        default:
                        case BaseInfo.ConnectTypeDef.Server: 
                            rbSetup_Server.Checked = true; 
                            tbSetup_IP.Text = BI.CheckExe[index].Server.Ip;
                            tbSetup_Port.Text = BI.CheckExe[index].Server.Port.ToString();
                            break;
                        case BaseInfo.ConnectTypeDef.Client: 
                            rbSetup_Client.Checked = true; 
                            tbSetup_IP.Text = BI.CheckExe[index].Client.Ip;
                            tbSetup_Port.Text = BI.CheckExe[index].Client.Port.ToString();
                            break;
                        case BaseInfo.ConnectTypeDef.Serial: 
                            rbSetup_Serial.Checked = true; 
                            cbSetup_SerialPort.Text = BI.CheckExe[index].Serial.Port;
                            cbSetup_SerialBaud.Text = BI.CheckExe[index].Serial.Baud.ToString();
                            cbSetup_DtrRts.Checked = BI.CheckExe[index].Serial.DtrRts;
                            break;
                    }
                    ViewConnectType(BI.CheckExe[index].ConnectType);

                    tbSetup_ResetCode.Text =  BI.CheckExe[index].ResetCode.ToString();
                    tbSetup_ReStartTime.Text = BI.CheckExe[index].ReStart_MaxTime.ToString();

                }

            }
            catch (Exception ex)
            {
                AddLog(Log.Type.LOG_ERR, "Display Error : {0}", ex.Message);
            }
            return result;
        }

        private void ViewConnectType(BaseInfo.ConnectTypeDef conn)
        {
            switch (conn)
            {
                default:
                case BaseInfo.ConnectTypeDef.Server:
                case BaseInfo.ConnectTypeDef.Client:
                    plSetup_TcpIp.Visible = true;
                    plSetup_Serial.Visible = false;
                    break;
                case BaseInfo.ConnectTypeDef.Serial:
                    plSetup_TcpIp.Visible = false;
                    plSetup_Serial.Visible = true;
                    break;
            }
        }

        private void rbSetup_Server_CheckedChanged(object sender, EventArgs e)
        {
            if (BI.CheckExe.Count > 0)
            {
                BI.CheckExe[BI.SelectIndex].ConnectType = BaseInfo.ConnectTypeDef.Server;
                ViewConnectType(BI.CheckExe[BI.SelectIndex].ConnectType);
            }
            else ViewConnectType(BaseInfo.ConnectTypeDef.Server);
        }

        private void rbSetup_Client_CheckedChanged(object sender, EventArgs e)
        {
            if (BI.CheckExe.Count > 0)
            {
                BI.CheckExe[BI.SelectIndex].ConnectType = BaseInfo.ConnectTypeDef.Client;
                ViewConnectType(BI.CheckExe[BI.SelectIndex].ConnectType);
            }
            else ViewConnectType(BaseInfo.ConnectTypeDef.Client);
        }

        private void rbSetup_Serial_CheckedChanged(object sender, EventArgs e)
        {
            if (BI.CheckExe.Count > 0)
            {
                BI.CheckExe[BI.SelectIndex].ConnectType = BaseInfo.ConnectTypeDef.Serial;
                ViewConnectType(BI.CheckExe[BI.SelectIndex].ConnectType);
            }
            else ViewConnectType(BaseInfo.ConnectTypeDef.Serial);
        }

        private void AddLog(Log.Type type, string format, params object[] args)
        {
            if (BI.log != null)
            {
                BI.log.Add(type, "WatchSetup", format, args);
            }
        }

        private void SetDefaultView()
        {
            rbSetup_Server.Checked = true;
            tbSetup_IP.Text = "localhost";
            tbSetup_Port.Text = "50001";
            tbSetup_ResetCode.Text = "LC";
            tbSetup_ReStartTime.Text = "60";
        }

        // Setup Exe file open
        private void btSetup_ExeOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ofd.Filter = "Exe|*.exe|All File (*.*)|*.*";
            ofd.Title = "실행 파일 열기";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CheckItem.Exe_Name = ofd.FileName;
                tbSetup_ExeName.Text = Path.GetFileName(CheckItem.Exe_Name);
                //string name;
                //if(ofdExe_FileOpen.FileName.Length > 40)
                //    name = ofdExe_FileOpen.FileName.Substring(ofdExe_FileOpen.FileName.Length-40);
                //else
                //    name = ofdExe_FileOpen.FileName;

                //RunMsg_Insert(ofdExe_FileOpen.FileName);
                //Def.WriteReg();
            }

        }

        private void btSetup_Ok_Click(object sender, EventArgs e)
        {
            if (rbSetup_Server.Checked == true)
            {
                CheckItem.ConnectType = BaseInfo.ConnectTypeDef.Server;
                CheckItem.Server = new BaseInfo.ServerInfo();
                CheckItem.Server.Ip = tbSetup_IP.Text;
                CheckItem.Server.Port = Convert.ToInt32(tbSetup_Port.Text);
            }
            else if (rbSetup_Client.Checked == true)
            {
                CheckItem.ConnectType = BaseInfo.ConnectTypeDef.Client;
                CheckItem.Client = new BaseInfo.ClientInfo();
                CheckItem.Client.Ip = tbSetup_IP.Text;
                CheckItem.Client.Port = Convert.ToInt32(tbSetup_Port.Text);
            }
            else
            {
                CheckItem.ConnectType = BaseInfo.ConnectTypeDef.Serial;
                CheckItem.Serial = new BaseInfo.SerialInfo();
                CheckItem.Serial.Port = cbSetup_SerialPort.Text;
                CheckItem.Serial.Baud = Convert.ToInt32(cbSetup_SerialBaud.Text);
                CheckItem.Serial.DtrRts = cbSetup_DtrRts.Checked;
            }
            CheckItem.ResetCode = Encoding.UTF8.GetBytes(tbSetup_ResetCode.Text);
            CheckItem.ReStart_MaxTime = Convert.ToInt32(tbSetup_ReStartTime.Text);

            _MainForm.RunMsg_Insert(CheckItem);
        }

    }
}
