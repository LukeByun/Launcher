using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Launcher
{
    ///
    /// delegate 정의
    /// 
    public delegate void SocketErrorEventHandler(object sender, SocketErrorEventArgs e);
    public delegate void SocketConnectEventHandler(object sender, SocketConnectionEventArgs e);
    public delegate void SocketCloseEventHandler(object sender, SocketConnectionEventArgs e);
    public delegate void SocketSendEventHandler(object sender, SocketSendEventArgs e);
    public delegate void SocketReceiveEventHandler(object sender, SocketReceiveEventArgs e);
    public delegate void SocketAcceptEventHandler(object sender, SocketAcceptEventArgs e);

#if ASYNCSOCKET_ENABLE
    public class StateObject
    {
        private const int BUFFER_SIZE = 327680;

        private Socket worker;
        private byte[] buffer;

        public StateObject(Socket worker)
        {
            this.worker = worker;
            this.buffer = new byte[BUFFER_SIZE];
        }

        public Socket Worker
        {
            get { return this.worker; }
            set { this.worker = value; }
        }

        public byte[] Buffer
        {
            get { return this.buffer; }
            set { this.buffer = value; }
        }

        public int BufferSize
        {
            get { return BUFFER_SIZE; }
        }
    } // end of class StateObject

    /// <summary>
    /// 비동기 서버의 Accept 이벤트를 위한 Argument Class
    /// </summary>
    public class SocketAcceptEventArgs : EventArgs
    {
        private readonly Socket conn;

        public SocketAcceptEventArgs(Socket conn)
        {
            this.conn = conn;
        }

        public Socket Worker
        {
            get { return this.conn; }
        }
    }

#else

    /// <summary>
    /// 수신 버퍼를 생성
    /// </summary>
    public class StateObject
    {
        public const int BUFFER_SIZE = 327680;

        private TcpClient worker;
        private byte[] buffer;

        public StateObject(TcpClient worker)
        {
            this.worker = worker;
            this.buffer = new byte[BUFFER_SIZE];
        }

        public TcpClient Worker
        {
            get { return this.worker; }
            set { this.worker = value; }
        }

        public byte[] Buffer
        {
            get { return this.buffer; }
            set { this.buffer = value; }
        }

        public int BufferSize
        {
            get { return BUFFER_SIZE; }
        }
    } // end of class StateObject

    /// <summary>
    /// 동기 서버의 Accept 이벤트를 위한 Argument Class
    /// </summary>
    public class SocketAcceptEventArgs : EventArgs
    {
        private readonly TcpClient conn;

        public SocketAcceptEventArgs(TcpClient conn)
        {
            this.conn = conn;
        }

        public TcpClient Worker
        {
            get { return this.conn; }
        }
    }

#endif                  // ASYNCSOCKET_ENABLE

    /// <summary>
    /// 소켓에서 발생한 에러 처리를 위한 이벤트 Argument Class
    /// </summary>
    public class SocketErrorEventArgs : EventArgs
    {
        private readonly Exception exception;
        private readonly int id = 0;

        public SocketErrorEventArgs(int id, Exception exception)
        {
            this.id = id;
            this.exception = exception;
        }

        public Exception SocketException
        {
            get { return this.exception; }
        }

        public int ID
        {
            get { return this.id; }
        }
    }

    /// <summary>
    /// 소켓의 연결 및 연결해제 이벤트 처리를 위한 Argument Class
    /// </summary>
    public class SocketConnectionEventArgs : EventArgs
    {
        private readonly int id = 0;

        public SocketConnectionEventArgs(int id)
        {
            this.id = id;
        }

        public int ID
        {
            get { return this.id; }
        }
    }

    /// <summary>
    /// 소캣의 데이터 전송 이벤트 처리를 위한 Argument Class
    /// </summary>
    public class SocketSendEventArgs : EventArgs
    {
        private readonly int id = 0;
        private readonly int sendBytes;

        public SocketSendEventArgs(int id, int sendBytes)
        {
            this.id = id;
            this.sendBytes = sendBytes;
        }

        public int SendBytes
        {
            get { return this.sendBytes; }
        }

        public int ID
        {
            get { return this.id; }
        }
    }

    /// <summary>
    /// 소켓의 데이터 수신 이벤트 처리를 위한 Argument Class
    /// </summary>
    public class SocketReceiveEventArgs : EventArgs
    {
        private readonly int id = 0;
        private readonly int receiveBytes;
        private readonly byte[] receiveData;

        public SocketReceiveEventArgs(int id, int receiveBytes, byte[] receiveData)
        {
            this.id = id;
            this.receiveBytes = receiveBytes;
            this.receiveData = receiveData;
        }

        public int ReceiveBytes
        {
            get { return this.receiveBytes; }
        }

        public byte[] ReceiveData
        {
            get { return this.receiveData; }
        }

        public int ID
        {
            get { return this.id; }
        }
    }

    public class SocketClass
    {
        protected int id;

        // Event Handler
        public event SocketErrorEventHandler OnError;
        public event SocketConnectEventHandler OnConnet;
        public event SocketCloseEventHandler OnClose;
        public event SocketSendEventHandler OnSend;
        public event SocketReceiveEventHandler OnReceive;
        public event SocketAcceptEventHandler OnAccept;

        public SocketClass()
        {
            this.id = -1;
        }
        /// <summary>
        /// 소켓을 ID로 보관한다.
        /// </summary>
        /// <param name="id"></param>
        public SocketClass(int id)
        {
            this.id = id;
        }
        /// <summary>
        /// 소켓 생성 고유ID 를 가져온다.
        /// </summary>
        public int ID
        {
            get { return this.id; }
        }

        protected virtual void ErrorOccured(SocketErrorEventArgs e)
        {
            SocketErrorEventHandler handler = OnError;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void Connected(SocketConnectionEventArgs e)
        {
            SocketConnectEventHandler handler = OnConnet;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void Closed(SocketConnectionEventArgs e)
        {
            SocketCloseEventHandler handler = OnClose;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void Sent(SocketSendEventArgs e)
        {
            SocketSendEventHandler handler = OnSend;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void Received(SocketReceiveEventArgs e)
        {
            SocketReceiveEventHandler handler = OnReceive;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void Accepted(SocketAcceptEventArgs e)
        {
            SocketAcceptEventHandler handler = OnAccept;

            if (handler != null)
                handler(this, e);
        }
    }
}
