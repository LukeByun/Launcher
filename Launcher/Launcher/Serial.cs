using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;

namespace Launcher
{
    class Serial_Port : SerialPort
    {
        SerialPort _serial_port = null;

        public delegate void HandleEvent(Byte[] Recev_Data);                // 델리게이트 선언
        public event EventHandler<SerialDataEventArgs> SerialReceived;      // 이벤트 선언

        /// <summary>
        /// EventArgs used to send bytes recieved on serial port
        /// </summary>
        public class SerialDataEventArgs : EventArgs
        {
            public SerialDataEventArgs(byte[] dataInByteArray)
            {
                Data = dataInByteArray;
            }

            /// <summary>
            /// Byte array containing data from serial port
            /// </summary>
            public byte[] Data;
        }


        public bool IsOpen()
        {
            if (_serial_port == null) return false;
            return _serial_port.IsOpen;
        }


        public bool OpenPort(string port, int baudrate, bool dtrrts)
        {
            bool result = false;
            try
            {
                if (port == null || port.ToUpper() == "NONE" || baudrate < 2400) return result;

                if (_serial_port != null)
                {
                    ClosePort();
                }
                if (_serial_port == null) _serial_port = new SerialPort();
                _serial_port.PortName = port.ToUpper();
                _serial_port.BaudRate = baudrate;
                _serial_port.Parity = Parity.None;
                _serial_port.DataBits = 8;
                _serial_port.StopBits = System.IO.Ports.StopBits.One;
                _serial_port.Handshake = System.IO.Ports.Handshake.None;
                _serial_port.DtrEnable = dtrrts;
                _serial_port.RtsEnable = dtrrts;
                _serial_port.ReadTimeout = 1000;
                _serial_port.WriteTimeout = 1000;

                _serial_port.DataReceived += new SerialDataReceivedEventHandler(_serial_port_DataReceived);

                _serial_port.Open();
                result = _serial_port.IsOpen;
            }
            catch (Exception ex)
            {
                // throw new MyException("SerialPort Open Error", ex);
                Debug.WriteLine("SerialPort Open Error : {0}", ex.Message);
            }
            return result;
        }

        void _serial_port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int rev_length = _serial_port.BytesToRead;
                byte[] data = new byte[rev_length];
                int read_length = _serial_port.Read(data, 0, rev_length);
                if (read_length > 0)
                {
                    if (SerialReceived != null)
                    {
                        SerialReceived(this, new SerialDataEventArgs(data));
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Serial Receive Error : {0}", ex.Message);
            }
        }

        public void Dispose()
        {
            ClosePort();
        }

        /// <summary>
        /// Serial Port 닫기
        /// </summary>
        public void ClosePort()
        {
            try
            {
                if (_serial_port != null && _serial_port.IsOpen)
                {
                    _serial_port.DataReceived -= new SerialDataReceivedEventHandler(_serial_port_DataReceived);
                    _serial_port.Close();
                    _serial_port.Dispose();
                    _serial_port = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SerialPort ClosePort Error : {0}", ex.Message);
            }
        }

        /// <summary>
        /// Serial Data 송신
        /// </summary>
        /// <param name="TxData">송신 byte</param>
        public bool SendData(byte[] TxData)
        {
            bool result = false;
            try
            {
                if (_serial_port.IsOpen)
                {
                    _serial_port.Write(TxData, 0, TxData.Length);
                    result = true;
                }
            }
            catch (InvalidOperationException ie)
            {
                Debug.WriteLine("포트가 닫혀 있음 : {0}", ie.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SerialPort SendData:{0}", ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Serial Data 송신
        /// </summary>
        /// <param name="TxData">송신 byte</param>
        public bool SendData(byte[] TxData, int length)
        {
            bool result = false;
            try
            {
                if (_serial_port.IsOpen)
                {
                    _serial_port.Write(TxData, 0, length);
                    result = true;
                }
            }
            catch (InvalidOperationException ie)
            {
                Debug.WriteLine("포트가 닫혀 있음:{0}", ie.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SerialPort SendData:{0}", ex.Message);
            }
            return result;
        }


    }
}
