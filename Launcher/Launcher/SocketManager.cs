using System;

namespace Launcher
{
    public delegate void ReceiveEventHandler(object sender, SocketReceiveEventArgs e);

    class SocketManager
    {
        public event ReceiveEventHandler OnReceiveMsg;

        private Log log = null;
        private BaseInfo.CheckItems ConnInfo = null;
        ServerSocket Server = null;
        ClientSocket Client = null;
        Serial_Port Serial = null;

        public SocketManager(BaseInfo bi, int index)
        {
            this.log = bi.log;
            if (bi.CheckExe != null && bi.CheckExe.Count > 0 && index < bi.CheckExe.Count)
                ConnInfo = bi.CheckExe[index];
        }

        public bool CreateOpen()
        {
            bool result = false;

            try
            {
                if (ConnInfo != null)
                {
                    switch (ConnInfo.ConnectType)
                    {
                        case BaseInfo.ConnectTypeDef.Server:
                            result = InitializeServer();
                            break;
                        case BaseInfo.ConnectTypeDef.Client:
                            result = InitializeClient();
                            break;
                        case BaseInfo.ConnectTypeDef.Serial:
                            result = InitializeSerial();
                            break;
                        default:
                            AddLog(Log.Type.LOG_ERR, "선택된 통신 연결 타입이 없습니다.");
                            break;
                    }
                }
            }
            catch (Exception err)
            {
                AddLog(Log.Type.LOG_ERR, "CreateOpen Error:" + err.Message);
            }
            return result;
        }

        /// <summary>
        /// 동기통신 소켓을 닫는다.
        /// </summary>
        public bool Close()
        {
            bool result = false;

            try
            {
                if (ConnInfo == null) return false;
                switch (ConnInfo.ConnectType)
                {
                    case BaseInfo.ConnectTypeDef.Server:
                        if (Server != null)
                        {
                            Server.OnReceive -= new SocketReceiveEventHandler(Server_OnReceive);
                            Server.Close();
                            result = true;
                        }
                        break;
                    case BaseInfo.ConnectTypeDef.Client:
                        if (Client != null)
                        {
                            Client.OnConnet -= new SocketConnectEventHandler(OnClientConnet);
                            Client.OnClose -= new SocketCloseEventHandler(OnClientClose);
                            Client.OnReceive -= new SocketReceiveEventHandler(OnClientReceive);
                            Client.OnSend -= new SocketSendEventHandler(OnClientSend);
                            Client.OnError -= new SocketErrorEventHandler(OnClientError);

                            Client.Close();
                            result = true;
                        }
                        break;
                    case BaseInfo.ConnectTypeDef.Serial:
                        if (Serial != null)
                        {
                            Serial.SerialReceived -= new EventHandler<Serial_Port.SerialDataEventArgs>(SerialReceived);
                            Serial.ClosePort();
                            result = true;
                        }
                        break;
                }
            }
            catch (Exception err)
            {
                AddLog(Log.Type.LOG_ERR, "Close Error:" + err.Message);
            }
            return result;
        }

        /// <summary>
        /// Server Socket 생성 및 동작
        /// </summary>
        private bool InitializeServer()
        {
            bool result = false;

            try
            {
                if (ConnInfo == null) return false;
                Server = new ServerSocket(log);
                if (Server != null)
                {
                    result = Server.StartServer(ConnInfo.Server.Ip, ConnInfo.Server.Port);
                    Server.OnReceive += new SocketReceiveEventHandler(Server_OnReceive);
                }
            }
            catch (Exception err)
            {
                AddLog(Log.Type.LOG_ERR, "InitializeServer Error:{0}", err.Message);
            }
            return result;
        }

        void Server_OnReceive(object sender, SocketReceiveEventArgs e)
        {
            AddLog(Log.Type.LOG_NOR, "Server Receive Id:{0}, Length:{1}", e.ID, e.ReceiveBytes);
            ReceiveProcess(e.ReceiveData, e.ReceiveBytes);

            ReceiveEventHandler handler = OnReceiveMsg;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Client Socket 생성 및 동작
        /// </summary>
        private bool InitializeClient()
        {
            bool result = false;

            try
            {
                if (ConnInfo == null) return false;
                Client = new ClientSocket(0, log);
                if (Client != null)
                {
                    Client.OnConnet += new SocketConnectEventHandler(OnClientConnet);
                    Client.OnClose += new SocketCloseEventHandler(OnClientClose);
                    Client.OnReceive += new SocketReceiveEventHandler(OnClientReceive);
                    Client.OnSend += new SocketSendEventHandler(OnClientSend);
                    Client.OnError += new SocketErrorEventHandler(OnClientError);

                    result = Client.Connect(ConnInfo.Client.Ip, ConnInfo.Client.Port);
                }
            }
            catch (Exception err)
            {
                AddLog(Log.Type.LOG_ERR, "Error InitializeClient:{0}", err.Message);
            }
            return result;
        }

        private void OnClientConnet(object sender, SocketConnectionEventArgs e)
        {
            if (ConnInfo == null) return;
            AddLog(Log.Type.LOG_INFO, "PC -> HOST: Connected ID: " + e.ID.ToString()
                + ", IP:" + ConnInfo.Client.Ip
                + ", Port:" + ConnInfo.Client.Port.ToString()
            );
        }

        private void OnClientClose(object sender, SocketConnectionEventArgs e)
        {
            if (ConnInfo == null) return;
            AddLog(Log.Type.LOG_INFO, "PC -> HOST: DisConnect ID: " + e.ID.ToString()
                + ", IP:" + ConnInfo.Client.Ip
                + ", Port:" + ConnInfo.Client.Port.ToString()
            );
        }

        private void OnClientSend(object sender, SocketSendEventArgs e)
        {
            //AddLog(Log.Type.LOG_INFO, "PC -> HOST: Send ID: " + e.ID.ToString() + " Bytes sent: " + e.SendBytes.ToString());
        }

        private void OnClientReceive(object sender, SocketReceiveEventArgs e)
        {
            ReceiveProcess(e.ReceiveData, e.ReceiveBytes);
            
            ReceiveEventHandler handler = OnReceiveMsg;
            
            if (handler != null)
                handler(this, e);
        }

        private void OnClientError(object sender, SocketErrorEventArgs e)
        {
            AddLog(Log.Type.LOG_ERR, "HOST -> PC: Error ID: " + e.ID.ToString() + " Error Message: " + e.SocketException.ToString());
        }


        /// <summary>
        /// Serial 생성 및 동작
        /// </summary>
        private bool InitializeSerial()
        {
            bool result = false;

            try
            {
                if (ConnInfo == null) return false;
                if (Serial == null)
                {
                    Serial = new Serial_Port();
                }

                result = Serial.OpenPort(ConnInfo.Serial.Port, ConnInfo.Serial.Baud, ConnInfo.Serial.DtrRts);
                Serial.SerialReceived += new EventHandler<Serial_Port.SerialDataEventArgs>(SerialReceived);
            }
            catch (Exception err)
            {
                AddLog(Log.Type.LOG_ERR, "InitializeSerial Error:{0}", err.Message);
            }
            return result;
        }

        void SerialReceived(object sender, Serial_Port.SerialDataEventArgs e)
        {
            try
            {
                ReceiveProcess(e.Data, e.Data.Length);

                SocketReceiveEventArgs rev = new SocketReceiveEventArgs(0, e.Data.Length, e.Data);
                ReceiveEventHandler handler = OnReceiveMsg;

                if (handler != null)
                    handler(this, rev);
            }
            catch (Exception err)
            {
                AddLog(Log.Type.LOG_ERR, "SerialRecieved Error:{0}", err.Message);
            }
        }

        /// <summary>
        /// 데이터 를 송신한다.
        /// </summary>
        /// <param name="send_data">송신버퍼</param>
        /// <param name="send_size">송신데이터 사이즈</param>
        /// <returns></returns>
        public Boolean Send(byte[] send_data, int send_size)
        {
            bool result = false;
            try
            {
                if (ConnInfo == null) return false;
                log.Raw("Send", send_data, send_size);
                switch (ConnInfo.ConnectType)
                {
                    case BaseInfo.ConnectTypeDef.Server:
                        if (Server != null)
                        {
                            result = Server.BroadCast(send_data, send_size);
                        }
                        break;
                    case BaseInfo.ConnectTypeDef.Client:
                        if (Client != null)
                        {
                            result = Client.Send(send_data, send_size);
                        }
                        break;
                    case BaseInfo.ConnectTypeDef.Serial:
                        if (Serial != null && Serial.IsOpen())
                        {
                            Serial.SendData(send_data);
                        }
                        break;
                }

            }
            catch (Exception err)
            {
                AddLog(Log.Type.LOG_ERR, "Send Error:{0}", err.Message);
            }
            return result;
        }



        /// <summary>
        /// 수신 처리 루틴 (수신데이터 기록 및 프로토콜 관리자로 수신 데이터 전송)
        /// </summary>
        /// <param name="rev_data">수신버퍼</param>
        /// <param name="rev_size">수신데이터 사이즈</param>
        public void ReceiveProcess(byte[] rev_data, int rev_size)
        {

            AddLog(Log.Type.LOG_NOR, "Receive Data {0}Byte", rev_size);
            // 통신 수신데이터를 log 파일에 기록한다.
            log.Raw("Receive", rev_data, rev_size);
            //if (BI.ReceiveBinaryWriteOn)
            //{
            //    CFileControl.Write_AppendBinaryFile(BI.BinaryWrite_Filename, rev_data, rev_size);
            //}

        }


        private void AddLog(Log.Type type, string format, params object[] args)
        {
            if (log != null)
            {
                log.Add(type, "SocketManager", format, args);
            }
        }

        
    }
}
