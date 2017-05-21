using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

#if !ASYNCSOCKET_ENABLE
namespace Launcher
{
    class ClientSocket : SocketClass
    {
        private TcpClient client = null;
        private NetworkStream nStream = null;
        private Log log = null;
        private bool ThreadRun = true;

        public ClientSocket(int id, Log lo)
        {
            this.id = id;
            this.log = lo;
        }

        public ClientSocket(int id, TcpClient conn)
        {
            this.id = id;
            this.client = conn;
        }

        public TcpClient Connection
        {
            get { return this.client; }
            set { this.client = value; }
        }

        public bool Connect(string Ip, int port)
        {
            bool result = false;

            try
            {
                client = new TcpClient(Ip, port);
                if (client != null && client.Connected)
                {
                    ThreadRun = true;
                    nStream = client.GetStream();
                    //                rStream = new StreamReader(nStream, Encoding.UTF8);     // Get Message
                    //                wStream = new StreamWriter(nStream, Encoding.UTF8);     // Get Message

                    Thread c_thread = new Thread(new ParameterizedThreadStart(ClientThread));
                    if (c_thread != null)
                    {
                        c_thread.Start(client);
                        // 연결 성공 이벤트를 날린다.
                        SocketConnectionEventArgs cev = new SocketConnectionEventArgs(this.id);
                        Connected(cev);
                        result = true;
                    }
                }
            }
            catch (SocketException se)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, se);
                ErrorOccured(eev);
            }
            catch (Exception err)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, err);
                ErrorOccured(eev);
            }

            return result;
        }

        public void Close()
        {
            ThreadRun = false;

            if (nStream != null)
            {
                nStream.Close();
                nStream = null;
            }
            if (client != null)
            {
                client.Close();
                client = null;
            }

        }

        public Boolean Connected()
        {
            if (client != null)
            {
                return client.Connected;
            }
            return false;
        }


        private void ClientThread(object sender)
        {
            TcpClient client = sender as TcpClient;
            StateObject so = new StateObject(client);
            int i;

            try
            {
                while (ThreadRun && (i = nStream.Read(so.Buffer, 0, so.BufferSize)) != 0)
                {
                    SocketReceiveEventArgs rev = new SocketReceiveEventArgs(id, i, so.Buffer);
                    Received(rev);

//                    string data = System.Text.Encoding.UTF8.GetString(so.Buffer, 0, i);
//                    Debug.WriteLine("Client Read:" + data);
                }

            }
            catch (SocketException se)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, se);
                ErrorOccured(eev);
            }
            catch (Exception err)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, err);
                ErrorOccured(eev);
            }
            finally
            {
                if (nStream != null)
                {
                    nStream.Close();
                    nStream = null;
                }
                if (client != null)
                {
                    client.Close();
                }
            }
        }

        public bool Send(byte[] data, int data_size)
        {
            bool result = false;

            try
            {
                if (nStream != null && client.Connected)
                {
                    nStream.Write(data, 0, data_size);
                    nStream.Flush();

                    SocketSendEventArgs sev = new SocketSendEventArgs(this.id, data_size);
                    Sent(sev);

                    result = true;
                }
            }
            catch (Exception err)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, err);
                ErrorOccured(eev);
            }
            return result;
        }

        public bool SendString(String str)
        {
            bool result = false;

            try
            {
                if (client != null && client.Connected)
                {
                    // 메시지 보내기
                    byte[] msg = System.Text.Encoding.UTF8.GetBytes(str);
                    nStream.Write(msg, 0, msg.Length);
                    nStream.Flush();

                    SocketSendEventArgs sev = new SocketSendEventArgs(this.id, msg.Length);
                    Sent(sev);

                    result = true;
                }
            }
            catch (Exception err)
            {
                SocketErrorEventArgs eev = new SocketErrorEventArgs(this.id, err);
                ErrorOccured(eev);
            }
            return result;
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
#endif          // #if !ASYNCSOCKET_ENABLE