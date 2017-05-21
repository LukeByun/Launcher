using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace Launcher
{
    public class Log : Form
    {
        public enum Type
        {
            LOG_NOR = 0x00,
            LOG_RUN,
            LOG_ERR,
            LOG_INFO,
        };

        public delegate void UpdateAppendLog(Type lt, string type, string format, params object[] args);
        /// <summary>
        /// 로그파일 기록 유무 (false:기록안함, true:기록)
        /// </summary>
        private bool WriteLogOn;
        /// <summary>
        /// Raw 파일 기록 유무 (false:기록안함, true:기록)
        /// </summary>
        private bool WriteRawOn;
        /// <summary>
        /// 로그파일 이 기록될 폴더경로
        /// </summary>
        public string pathLog { get; set; }
        /// <summary>
        /// listview 표시 되는 최대 row 개수(기본 1000개)
        /// </summary>
        public int row_limit;
        /// <summary>
        /// 로그 보관일수 기본:-30일 (음수만 사용 예.keep_file = -10 : 로그파일 10일 치만 보관)
        /// </summary>
        public int keep_file;
        /// <summary>
        /// 로그 파일 확장자 변경 기본 ".log"
        /// </summary>
        public string LogExtension { get { return this.logName_Extension; } set { this.logName_Extension = value; } }
        /// <summary>
        /// RAW 파일 확장자 변경 기본 ".log"
        /// </summary>
        public string RawExtension { get { return this.rawName_Extension; } set { this.rawName_Extension = value; } }

        private DateTime back_day;                      // 보관일수 계산용 날짜
        private string logName_Extension;               // 로그파일 확장자
        private string rawName_Extension;               // Raw 파일 확장자
        private ListView _listLog { get; set; }         // 리스트 콤퍼넌트
        private ImageList imageListLog;
        private static readonly object thisLock = new object();

        public void SetWriteLogOn(bool f) { WriteLogOn = f; }
        public bool GetWriteLogOn() { return WriteLogOn; }
        public void SetWriteRawOn(bool f) { WriteRawOn = f; }
        public bool GetWriteRawOn() { return WriteRawOn; }

        /// <summary>
        /// ListView 가 폼에 생성 되어 있지 않은 경우
        /// </summary>
        public Log()
        {
            _listLog = null;
            pathLog = Directory.GetCurrentDirectory() + "\\Log\\";
            logName_Extension = ".log";
            rawName_Extension = ".log";
            row_limit = 1000;
            keep_file = -30;
            WriteLogOn = true;
            WriteRawOn = false;
        }

        /// <summary>
        /// ListView 가 폼에 생성 되어 있는 경우
        /// </summary>
        /// <param name="log">ListView</param>
        public Log(ListView log)
        {
            if (log != null)
            {
                _listLog = log;
                _listLog.View = View.Details;
                _listLog.LabelEdit = true;
                _listLog.AllowColumnReorder = true;
                _listLog.CheckBoxes = false;
                _listLog.FullRowSelect = true;
                _listLog.Sorting = SortOrder.None;

                _listLog.Columns.Add("Date", -2, HorizontalAlignment.Left);
                _listLog.Columns.Add("Type", -2, HorizontalAlignment.Left);
                _listLog.Columns.Add("Position", -2, HorizontalAlignment.Left);
                _listLog.Columns.Add("Log", -2, HorizontalAlignment.Left);

                imageListLog = new ImageList();
                imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\편집.bmp"));
                imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\accept.bmp"));
                imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\delete.bmp"));
                imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\exclamation.bmp"));

                _listLog.SmallImageList = imageListLog;

            }
            else
            {
                _listLog = null;
            }
            pathLog = Directory.GetCurrentDirectory() + "\\Log\\";
            logName_Extension = ".log";
            rawName_Extension = ".log";
            row_limit = 1000;
            keep_file = -30;
            WriteLogOn = true;
            WriteRawOn = false;

            WriteStartLog();
        }

        /// <summary>
        /// ListView 를 해당위치에 생성 시킨다.
        /// </summary>
        /// <param name="x">ListView Start X</param>
        /// <param name="y">ListView Start Y</param>
        /// <param name="width">ListView Width</param>
        /// <param name="height">ListView Height</param>
        public Log(int x, int y, int width, int height)
        {
            try
            {
                _listLog = new ListView();
                _listLog.Bounds = new Rectangle(new Point(x, y), new Size(width, height));

                _listLog.View = View.Details;
                _listLog.LabelEdit = true;
                _listLog.AllowColumnReorder = true;
                _listLog.CheckBoxes = false;
                _listLog.FullRowSelect = true;
                _listLog.Sorting = SortOrder.None;

                _listLog.Columns.Add("Date", -2, HorizontalAlignment.Left);
                _listLog.Columns.Add("Type", -2, HorizontalAlignment.Left);
                _listLog.Columns.Add("Position", -2, HorizontalAlignment.Left);
                _listLog.Columns.Add("Log", -2, HorizontalAlignment.Left);

                imageListLog = new ImageList();
                imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\편집.bmp"));
                imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\accept.bmp"));
                imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\delete.bmp"));
                imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\exclamation.bmp"));

                _listLog.SmallImageList = imageListLog;

                pathLog = Directory.GetCurrentDirectory() + "\\Log\\";
                logName_Extension = ".log";
                rawName_Extension = ".log";
                row_limit = 1000;
                keep_file = -30;
                WriteLogOn = true;
                WriteRawOn = false;

                WriteStartLog();
                //                    this.Controls.Add(_listLog);
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0} => {1}",
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString()); ;
                Debug.Write(msg);
            }
        }

        /// <summary>
        /// 폼에 생성된 ListView를 연결 하여 초기화
        /// </summary>
        /// <param name="log">ListView</param>
        /// <returns>예러:false, 정상:true</returns>
        public bool InitLog(ListView log)
        {
            try
            {
                if (log != null)
                {
                    _listLog = log;
                    _listLog.View = View.Details;
                    _listLog.LabelEdit = true;
                    _listLog.AllowColumnReorder = true;
                    _listLog.CheckBoxes = false;
                    _listLog.FullRowSelect = true;
                    _listLog.Sorting = SortOrder.None;

                    _listLog.Columns.Add("Date", -2, HorizontalAlignment.Left);
                    _listLog.Columns.Add("Type", -2, HorizontalAlignment.Left);
                    _listLog.Columns.Add("Position", -2, HorizontalAlignment.Left);
                    _listLog.Columns.Add("Log", -2, HorizontalAlignment.Left);

                    imageListLog = new ImageList();
                    imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\편집.bmp"));
                    imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\accept.bmp"));
                    imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\delete.bmp"));
                    imageListLog.Images.Add(Bitmap.FromFile("..\\..\\Res\\exclamation.bmp"));

                    _listLog.SmallImageList = imageListLog;

                    WriteStartLog();
                }
                else
                {
                    _listLog = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0} => {1}",
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString()); ;
                Debug.Write(msg);
                return false;
            }

            return true;
        }

        public void WriteStartLog()
        {
            if (WriteLogOn)
            {
                string write_msg = "\r\n"
                                 + "----------------------------------------------------------------------------------------------\r\n"
                                 + "---                                    LOG Start                                           ---\r\n"
                                 + "----------------------------------------------------------------------------------------------\r\n";
                Write_LogFile(write_msg);
            }
        }

        /// <summary>
        /// Log 내용을 추가한다.
        /// </summary>
        /// <param name="lt">Log Type (LOG_NOR, LOG_RUN, LOG_ERR, LOG_INFO)</param>
        /// <param name="type">Log 출력 접두어</param>
        /// <param name="format">Log 출력 포맷</param>
        /// <param name="args">Log 데이터</param>
        public void Add(Type lt, string type, string format, params object[] args)
        {
            string log_date, log_pos, log_msg, write_msg;
            StackTrace st = new StackTrace(true);

            try
            {
                log_date = String.Format(DateTime.Now.ToString("HH:mm:ss.fff"));
                log_pos = String.Format("{0}:{1}", st.GetFrame(2).GetMethod().Name,
                    st.GetFrame(2).GetFileLineNumber());

                log_msg = String.Format(format, args);

                if (_listLog != null)
                {
                    ListViewItem item1 = new ListViewItem(log_date, (byte)lt);
                    item1.SubItems.Add(type);
                    item1.SubItems.Add(log_pos);
                    item1.SubItems.Add(log_msg);

                    if (_listLog.InvokeRequired)
                    {
                        _listLog.Invoke(new UpdateAppendLog(Add), new object[] { lt, type, format, args });
                    }
                    else
                    {
                        switch (lt)
                        {
                            case Type.LOG_NOR: item1.ForeColor = Color.Black; item1.BackColor = Color.White; break;
                            case Type.LOG_RUN: item1.ForeColor = Color.Blue; item1.BackColor = Color.LightCyan; break;
                            case Type.LOG_ERR: item1.ForeColor = Color.Red; item1.BackColor = Color.Pink; break;
                            case Type.LOG_INFO: item1.ForeColor = Color.Green; item1.BackColor = Color.LightGreen; break;
                        }

                        if (_listLog.Items.Count >= row_limit)
                        {
                            CheckItemLimit();
                        }
                        int co = _listLog.Items.Count;
                        lock (thisLock)
                        {
                            _listLog.Items.Add(item1);
                            _listLog.EnsureVisible(_listLog.Items.Count - 1);
                        }
                    }
                }
                if (lt == Type.LOG_NOR) write_msg = "Nor:" + "[" + log_date + ", Type:" + type + ", Pos:" + log_pos + "] : " + log_msg;
                else if (lt == Type.LOG_RUN) write_msg = "Run:" + "[" + log_date + ", Type:" + type + ", Pos:" + log_pos + "] : " + log_msg;
                else if (lt == Type.LOG_ERR) write_msg = "Err:" + "[" + log_date + ", Type:" + type + ", Pos:" + log_pos + "] : " + log_msg;
                else if (lt == Type.LOG_INFO) write_msg = "Inf:" + "[" + log_date + ", Type:" + type + ", Pos:" + log_pos + "] : " + log_msg;
                else write_msg = "Etc:" + "[" + log_date + ", Type:" + type + ", Pos:" + log_pos + "] : " + log_msg;

                if (WriteLogOn) Write_LogFile(write_msg);

            }
            catch (Exception ex)
            {
                string msg = string.Format("{0} => {1}",
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString()); ;
                Debug.Write(msg);
            }

        }

        /// <summary>
        /// ListView Log 에 최대라인을 검사 하여 예전에 들어온 Log부터 삭제한다.
        /// </summary>
        /// <returns>삭제된 Log 개수</returns>
        public int CheckItemLimit()
        {
            int row = 0;
            int i = 0;

            try
            {
                if (_listLog != null && row >= row_limit)
                {
                    row = _listLog.Items.Count;

                    if (row >= 1000)
                    {
                        for (i = 0; i < 500; i++)
                            _listLog.Items.RemoveAt(0);
                    }
                    else if (row >= 100)
                    {
                        for (i = 0; i < 100; i++)
                            _listLog.Items.RemoveAt(0);
                    }
                    else
                    {
                        for (i = 0; i < 10; i++)
                        {
                            _listLog.Items.RemoveAt(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error[Log.CheckItemLimit] : {0} => {1}",
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString()); ;
                Debug.Write(msg);
            }

            return i;
        }

        /// <summary>
        /// Log file 이 보관일수 보다 크면 삭제한다. (back_day)
        /// </summary>
        /// <returns>삭제된 Log File 개수</returns>
        public int DeleteOldFile()
        {
            int del_co = 0;

            try
            {
                TimeSpan interval = DateTime.Now - back_day;

                if (interval.Days > 0)
                {
                    String st = DateTime.Today.AddDays(keep_file).ToString("yyyy_MM_dd") + logName_Extension;

                    string[] files = Directory.GetFiles(pathLog, "*" + logName_Extension);
                    int total_files = files.Length;
                    String cmp_name, raw_name;

                    foreach (string file in files)
                    {
                        cmp_name = Path.GetFileName(file);
                        raw_name = Path.GetFileNameWithoutExtension(file) + rawName_Extension;
                        if (st.CompareTo(cmp_name) > 0)
                        {
                            //                        Directory.Delete(file, true);
                            File.Delete(file);
                            if (logName_Extension != rawName_Extension)
                            {
                                File.Delete(raw_name);
                            }
                            del_co++;
                        }
                    }
                    back_day = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error[Log.DeleteOldFile] : {0} => {1}",
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString()); ;
                Debug.Write(msg);
            }
            return del_co;
        }

        /// <summary>
        /// 현재 날짜 Log File에 Log를 기록 한다.
        /// </summary>
        /// <param name="lt">Log Type (LOG_NOR, LOG_RUN, LOG_ERR, LOG_INFO)</param>
        /// <param name="type">Log 출력 접두어</param>
        /// <param name="format">Log 출력 포맷</param>
        /// <param name="args">Log 데이터</param>
        public void FileAdd(Byte lt, string type, string format, params object[] args)
        {
            // Log 기록 On 이 아니면 리턴.
            if (!WriteLogOn) return;
            string log_date, log_pos, log_msg, write_msg;
            StackTrace st = new StackTrace(true);

            try
            {
                log_date = String.Format(DateTime.Now.ToString("HH:mm:ss.fff"));
                log_pos = String.Format("{0}:{1}", st.GetFrame(1).GetMethod().Name,
                    st.GetFrame(1).GetFileLineNumber());
                log_msg = String.Format(format, args);

                if (lt == 0) write_msg = "Nor:" + "[" + log_date + ", Type:" + type + ", Pos:" + log_pos + "] : " + log_msg;
                else if (lt == 1) write_msg = "Run:" + "[" + log_date + ", Type:" + type + ", Pos:" + log_pos + "] : " + log_msg;
                else if (lt == 2) write_msg = "Err:" + "[" + log_date + ", Type:" + type + ", Pos:" + log_pos + "] : " + log_msg;
                else if (lt == 3) write_msg = "Inf:" + "[" + log_date + ", Type:" + type + ", Pos:" + log_pos + "] : " + log_msg;
                else write_msg = "Etc:" + "[" + log_date + ", Type:" + type + ", Pos:" + log_pos + "] : " + log_msg;

                Write_LogFile(write_msg);

            }
            catch (Exception ex)
            {
                string msg = string.Format("Error[Log.FileAdd] : {0} => {1}",
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString()); ;
                Debug.Write(msg);
            }

        }

        /// <summary>
        /// 현재 날짜 Log File에 Binary Data 버퍼 내용을 Text 형식으로 저장 한다.
        /// </summary>
        /// <param name="type">Log 접두어</param>
        /// <param name="buff">Byte 버퍼</param>
        /// <param name="size">버퍼 사이즈</param>
        public void Raw(string type, Byte[] buff, int size)
        {

            // Raw 기록이 On 이 아니면 리턴.
            if (!WriteRawOn) return;
            StringBuilder hex_msg = new StringBuilder();
            string wr_msg;
            int bc = 0, i = 0;

            try
            {
                wr_msg = String.Format(DateTime.Now.ToString("[HH:mm:ss.fff]"));
                Write_RawFile("\r\n===============================================================================");
                Write_RawFile(type + wr_msg + " Length:" + size.ToString());
                Write_RawFile("-------------------------------------------------------------------------------");

                while (bc < size)
                {
                    hex_msg.Clear();
                    hex_msg.Append(String.Format("{0:x4} : ", bc));
                    for (i = 0; i < 16; i++)
                    {
                        if (bc + i < size)
                            hex_msg.Append(String.Format("{0:x2} ", buff[bc + i]));
                        else
                            hex_msg.Append("   ");
                    }

                    hex_msg.Append("     ");
                    for (i = 0; i < 16; i++)
                    {
                        if (bc + i < size)
                            hex_msg.Append(Convert.ToChar(buff[bc + i] >= 0x20 ? buff[bc + i] : 0x20));
                        else
                            hex_msg.Append(" ");
                    }
                    //                    hex_msg.Append("\r\n");
                    bc += i;
                    wr_msg = hex_msg.ToString();
                    Write_RawFile(wr_msg);
                }
                Write_RawFile("===============================================================================\r\n");

            }
            catch (Exception ex)
            {
                string msg = string.Format("{0} => {1}",
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString()); ;
                Debug.Write(msg);
            }
        }

        /// <summary>
        /// 현재 날짜의 로그 파일에 해당 메시지 기록
        /// </summary>
        /// <param name="msg">기록할 메시지</param>
        private void Write_LogFile(string msg)
        {
            // WriteLogOn false 이면 log file을 생성 하지 않는다.
            if (!WriteLogOn) return;

            FileInfo fileinfo = new FileInfo(pathLog);

            try
            {
                string str_path = pathLog + string.Format("{0}_{1:00}_{2:00}{3}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, logName_Extension);
                string strDir = Path.GetDirectoryName(pathLog);
                DirectoryInfo diDir = new DirectoryInfo(strDir);

                DeleteOldFile();

                if (!diDir.Exists)
                {
                    diDir.Create();
                    diDir = new DirectoryInfo(strDir);  // 아래에 있는 if (diDir.Exists)은 Directory 생성전 상태를 나타내므로 다시 DirectoryInfo object를 생성.
                }

                lock (thisLock)
                {
                    FileStream fw_stream = File.Open(str_path, FileMode.Append);
                    StreamWriter sw = new StreamWriter(fw_stream);
                    // log msg
                    sw.WriteLine(msg);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                string str = string.Format("Error : {0} => {1}",
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString()); ;
                Debug.Write(str);
            }
        }

        /// <summary>
        /// 현재 날짜의 RAW 파일에 해당 메시지 기록
        /// </summary>
        /// <param name="msg">기록할 메시지</param>
        private void Write_RawFile(string msg)
        {
            // WriteRawOn false 이면 log file을 생성 하지 않는다.
            if (!WriteRawOn) return;

            FileInfo fileinfo = new FileInfo(pathLog);

            try
            {
                string str_path = pathLog + string.Format("{0}_{1:00}_{2:00}{3}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, rawName_Extension);
                string strDir = Path.GetDirectoryName(pathLog);
                DirectoryInfo diDir = new DirectoryInfo(strDir);

                DeleteOldFile();

                if (!diDir.Exists)
                {
                    diDir.Create();
                    diDir = new DirectoryInfo(strDir);  // 아래에 있는 if (diDir.Exists)은 Directory 생성전 상태를 나타내므로 다시 DirectoryInfo object를 생성.
                }

                lock (thisLock)
                {
                    FileStream fw_stream = File.Open(str_path, FileMode.Append);
                    StreamWriter sw = new StreamWriter(fw_stream);
                    // log msg
                    sw.WriteLine(msg);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                string str = string.Format("Error : {0} => {1}",
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString()); ;
                Debug.Write(str);
            }
        }
    }
}
