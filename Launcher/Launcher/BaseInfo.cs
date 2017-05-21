using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Launcher
{
    class BaseInfo
    {
        public enum ConnectTypeDef
        {
            Server,
            Client,
            Serial
        }

        public class ServerInfo
        {
            public string Ip = "localhost";
            public int Port = 50001;
        }

        public class ClientInfo
        {
            public string Ip = "localhost";
            public int Port = 50001;
        }

        public class SerialInfo
        {
            public string Port = "None";
            public int Baud = 115200;
            public bool DtrRts = false;
        }

        public class CheckItems
        {
            /// <summary>
            /// 통신 연결 타입
            /// </summary>
            public ConnectTypeDef ConnectType;
            /// <summary>
            /// Server 열결 정보
            /// </summary>
            public ServerInfo Server;
            /// <summary>
            /// Client 연결 정보
            /// </summary>
            public ClientInfo Client;
            /// <summary>
            /// Serial 연결 정보
            /// </summary>
            public SerialInfo Serial;
            public int ReStart_MaxTime = 5;             // 재실행 최대 타임 (초)
            public int ReStart_Time;                    // 재실행 타임머
            public byte[] ResetCode;
            public string Exe_Name;
        }

        public Log log = null;

        public List<CheckItems> CheckExe;
        /// <summary>
        /// 수정 및 추가시 선택된 list index
        /// </summary>
        public int SelectIndex = 0;
        public bool AutoRunOn;
        public bool TrayIconOn;
        public bool ReBootingOn;
        public bool[] Week = { true, true, true, true, true, true, true };
        public string ReBootingTime;

        public BaseInfo()
        {
            CheckExe = new List<CheckItems>(10);
        }

        public void ItemAdd(CheckItems data)
        {
            CheckExe.Add(data);
        }

    }
}
