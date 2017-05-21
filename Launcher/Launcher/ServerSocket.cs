using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;

#if !ASYNCSOCKET_ENABLE
namespace Launcher
{
    class ServerSocket : SocketClass
    {
        // TCP 통신 대기 리스너
        private TcpListener serverListener = null;
        // TCP 통신 대기 쓰레드
        private Thread serverThread;
        // Log 출력
        private Log log = null;
        // 서버 IP
        private string IP;
        // 서버 Port
        private int Port;
        public static Dictionary<string, TcpClient> RemoteClients = new Dictionary<string, TcpClient>();

        private bool ThreadRun = true;

        /*
                [Serializable]
                public class DataPacket
                {
                    public string Name { get; set; }
                    public string Subject { get; set; }
                    public Int32 Grade { get; set; }
                    public string Memo { get; set; }
                    public DateTime SendTime { get; set; }
                }
        */

        /// <summary>
        /// 동기 서버 소켓을 생성 한다.
        /// </summary>
        /// <param name="log">로그</param>
        public ServerSocket(Log lo)
        {
            this.log = lo;
        }
        /// <summary>
        /// 서버 를 동작 시킨다.
        /// </summary>
        /// <param name="ip">서버 동작 IP</param>
        /// <param name="port">서버 동작 Port</param>
        /// <returns>false:서버 Open 실패, true:서버 Open 성공</returns>
        public bool StartServer(string ip, int port)
        {
            bool result = false;

            try
            {
                this.IP = ip;
                this.Port = port;
                ThreadRun = true;
                serverThread = new Thread(new ThreadStart(AcceptThread));
                serverThread.IsBackground = true;
                serverThread.Start();
                result = true;
            }
            catch (Exception err)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, err);
                ErrorOccured(eev);
            }
            return result;
        }
        /// <summary>
        /// 서버 설정 IP 를 가져옵니다.
        /// </summary>
        public string GetIp
        {
            get { return this.IP; }
        }
        /// <summary>
        /// 서버 설정 Port를 가져 옵니다.
        /// </summary>
        public int GetPort
        {
            get { return this.Port; }
        }
        /// <summary>
        /// 서버 동작 을 중지 하고 소켓을 닫습니다.
        /// </summary>
        public void Close()
        {
            ThreadRun = false;
            foreach (var client in RemoteClients.ToList())
            {
                client.Value.Close();
                SocketConnectionEventArgs cev = new SocketConnectionEventArgs(this.id);
                Closed(cev);
            }
            if (serverListener != null)
            {
                serverListener.Stop();
                serverListener = null;
            }

        }
        /// <summary>
        /// 서버 접속 대기 상태 처리
        /// </summary>
        private void AcceptThread()
        {
            try
            {
                serverListener = new TcpListener(IPAddress.Parse(IP), Port);
                serverListener.Start();
                updateStatusInfo(string.Format("{0}:{1} server start:{2}", IP, Port, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                while (ThreadRun)
                {
                    TcpClient client = serverListener.AcceptTcpClient();
                    //string remoteip = client.Client.RemoteEndPoint.ToString().Split(':')[0];
                    string remoteip = client.Client.RemoteEndPoint.ToString();
                    RemoteClients.Add(remoteip, client);

                    Thread clientworker = new Thread(new ParameterizedThreadStart(clientThread));
                    clientworker.IsBackground = true;
                    clientworker.Start(client);

                    // Client를 Accept 했다고 Event를 발생시킨다.
                    SocketAcceptEventArgs aev = new SocketAcceptEventArgs(client);
                    Accepted(aev);
                }
            }
            catch (SocketException se)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, se);
                ErrorOccured(eev);
            }
            catch (Exception ex)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, ex);
                ErrorOccured(eev);
            }
            finally
            {
                if (serverListener != null) serverListener.Stop();
            }

        }
        /// <summary>
        /// 외부 접속 처리 쓰레드
        /// </summary>
        /// <param name="sender"></param>
        private void clientThread(object sender)
        {
            /*
                        string Name = string.Empty;
                        string Subject = string.Empty;
                        Int32 Grade = 0;
                        string Memo = string.Empty;
                        TimeSpan time = TimeSpan.Zero;
            */
            // 1. 데이타 받기
            TcpClient client = sender as TcpClient;
            NetworkStream stream = client.GetStream();

            //            byte[] buffer = new byte[8092];
            StateObject so = new StateObject(client);
            //string data = null;
            int i;

            try
            {
                while (ThreadRun && (i = stream.Read(so.Buffer, 0, so.BufferSize)) != 0)
                {
                    // 수신 이벤트 처리
                    SocketReceiveEventArgs rev = new SocketReceiveEventArgs(id, i, so.Buffer);
                    Received(rev);


/*                  // 수신 확인 테스트용  
                    data = System.Text.Encoding.UTF8.GetString(so.Buffer, 0, i);
                    updateStatusInfo("수신:" + data);
                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.UTF8.GetBytes(data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
 */ 
                }

                /*      // 패킷에 의한 전송
                            if (client.GetStream().CanRead)
                            {
                                IFormatter formatter = new BinaryFormatter();

                                DataPacket packet = new DataPacket();
                                packet = (DataPacket)formatter.Deserialize(stream);

                                Name = packet.Name;
                                Subject = packet.Subject;
                                Grade = packet.Grade;
                                Memo = packet.Memo;
                                SendTime = packet.SendTime;

                                time = DateTime.Now - SendTime;

                                updateStatusInfo(string.Format("[{0}] data received", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                            }
                */
            }
            catch (SocketException se)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, se);
                ErrorOccured(eev);
                //                Debug.WriteLine("ServerSocket->clientThread SocketException" + se.Message);
            }
            catch (Exception err)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, err);
                ErrorOccured(eev);
                //                Debug.WriteLine("ServerSocket->clientThread Exception" + err.Message);
            }
            finally
            {
                try
                {
                    if (stream != null)
                    {
                        stream.Close();
                        stream = null;
                    }
                    if (client != null)
                    {
                        // Client 접속 IP 와 고유번호를 가저온다.
                        string remoteip = client.Client.RemoteEndPoint.ToString();
                        // 해당 IP가 등록 되어 있는지 확인
                        if (RemoteClients.ContainsKey(remoteip))
                        {
                            RemoteClients.Remove(remoteip);
                        }
                        client.Close();
                        client = null;
                    }
                }
                catch (Exception err)
                {
                    SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, err);
                    ErrorOccured(eev);
                    //                    Debug.WriteLine("error:" + err.Message);
                }
            }
            updateStatusInfo(string.Format("[{0}] disconnted", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            // 2. 데이타 표시하기
            /*
                        Invoke((MethodInvoker)delegate
                        {
                            int count = listView1.Items.Count;
                            count++;

                            ListViewItem i = new ListViewItem();
                            i.Text = count.ToString();
                            i.SubItems.Add(Name);
                            i.SubItems.Add(Subject);
                            i.SubItems.Add(Grade.ToString());
                            i.SubItems.Add(Memo);
                            i.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            i.SubItems.Add(string.Format("{0}.{1} seconds", time.Seconds.ToString(), time.Milliseconds.ToString()));
                            listView1.Items.Add(i);

                            listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                        });
            */

        }
        /// <summary>
        /// 서버에 연결된 모든 Client에게 데이터를 송신 한다.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="data_length"></param>
        /// <returns></returns>
        public bool BroadCast(byte[] data, int data_length)
        {
            bool result = false;
            try
            {
                foreach (var client in RemoteClients.ToList())
                {
                    if (client.Value != null)
                    {
                        NetworkStream stream = client.Value.GetStream();
                        if (stream != null)
                        {
                            stream.Write(data, 0, data_length);
                            stream.Flush();
                            result = true;
                        }
                    }
                    SocketSendEventArgs sev = new SocketSendEventArgs(this.id, data_length);
                    Sent(sev);
                }
            }
            catch (Exception err)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, err);
                ErrorOccured(eev);
                //Debug.WriteLine("ServerSocket->BroadCast Error:"+err.Message);
            }
            return result;
        }

        /// <summary>
        /// 작동중인 내부 IP를 가져온다.
        /// </summary>
        /// <returns>내부 IP</returns>
        public static string GetLocalIP()
        {
            string localIP = "127.0.0.1";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        /// <summary>
        /// 이더넷 Mac Address 를 가져온다.
        /// </summary>
        /// <returns>Mac Address</returns>
        public static string GetMACAddress()
        {
            string MacAddress = "";

            System.Net.NetworkInformation.NetworkInterface[] adapters = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (System.Net.NetworkInformation.NetworkInterface adapter in adapters)
            {
                System.Net.NetworkInformation.PhysicalAddress pa = adapter.GetPhysicalAddress();
                if (pa != null && !pa.ToString().Equals(""))
                {
                    MacAddress = pa.ToString();
                    break;
                }
            }

            return MacAddress;
        }

        public static string GetMyHostName()
        {
            return Dns.GetHostName();
        }

        public static IPAddress[] GetSiteDomainIp(string sitedomain)
        {
            return Dns.GetHostAddresses(sitedomain);
        }

        public static string GetMyOutsideIp()
        {
            try
            {
                #region 자신 외부 IP 가져오기 1
                //string whatIsMyIp = "http://www.whatismyip.com/automation/n09230945.asp";
                //WebClient wc = new WebClient();
                //UTF8Encoding utf8 = new UTF8Encoding();
                //string requestHtml = "";

                //requestHtml = utf8.GetString(wc.DownloadData(whatIsMyIp));

                //IPAddress externalIp = null;

                //externalIp = IPAddress.Parse(requestHtml);

                //string WanIp = externalIp.ToString();
                #endregion

                #region 자신 외부 IP 가져오기 2

                Uri siteUri = new Uri("http://www.lgnas.com/");

                //WebRequest wr = WebRequest.Create(siteUri);
                //string se = wr.Method.ToString();

                //string whatIsMyIp = "http://www.lgnas.com/";
                //WebClient client = new WebClient();

                //// Add a user agent header in case the 
                //// requested URI contains a query.

                //client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                //string reply = client.DownloadString(siteUri);

                //Stream data = client.OpenRead(siteUri);
                //StreamReader reader = new StreamReader(data);
                //string s = reader.ReadToEnd();
                //Console.WriteLine(s);
                //data.Close();
                //reader.Close();                
                #endregion

                //URL에서 IP획득
                string searchIpFromUrl = new System.Net.WebClient().DownloadString(("http://checkip.dyndns.org"));
                //                string searchIpFromUrl = new System.Net.WebClient().DownloadString(("http://www.lgnas.com/"));

                //자를부분
                string EtcIpInfo = searchIpFromUrl.Substring(searchIpFromUrl.IndexOf("</body>"), searchIpFromUrl.Length - searchIpFromUrl.IndexOf("</body>"));

                //전체에서 시작점~ 전체길이-앞뒤자를부분
                string serverIp = searchIpFromUrl.Substring(searchIpFromUrl.IndexOf(":") + 1, searchIpFromUrl.Length - searchIpFromUrl.IndexOf(":") - EtcIpInfo.Length - 1).Trim();

                return serverIp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetMyOutsideIp : {0}", ex.Message);
            }
            return null;
        }


        private void updateStatusInfo(string content)
        {
            /*            Action del = delegate ()
                        {
                            listBox1.Items.Add(content);
                        };
                        Invoke(del);
            */
            AddLog(Log.Type.LOG_NOR, content);
            // Debug.WriteLine(content);
        }

        private void AddLog(Log.Type type, string format, params object[] args)
        {
            if (log != null)
            {
                string msg = String.Format(format, args);
                log.Add(type, "ServerSocket", msg);
            }
        }


    }
}
#endif                  // #if !ASYNCSOCKET_ENABLE