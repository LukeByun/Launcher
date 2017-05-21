namespace Launcher
{
    partial class WatchSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSetup_Serial = new System.Windows.Forms.RadioButton();
            this.rbSetup_Client = new System.Windows.Forms.RadioButton();
            this.rbSetup_Server = new System.Windows.Forms.RadioButton();
            this.plSetup_Serial = new System.Windows.Forms.Panel();
            this.cbSetup_DtrRts = new System.Windows.Forms.CheckBox();
            this.cbSetup_SerialBaud = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSetup_SerialPort = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.plSetup_TcpIp = new System.Windows.Forms.Panel();
            this.tbSetup_IP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSetup_Port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSetup_ResetCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSetup_ReStartTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btSetup_ExeOpen = new System.Windows.Forms.Button();
            this.tbSetup_ExeName = new System.Windows.Forms.TextBox();
            this.btSetup_Close = new System.Windows.Forms.Button();
            this.btSetup_Ok = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.plSetup_Serial.SuspendLayout();
            this.plSetup_TcpIp.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSetup_Serial);
            this.groupBox1.Controls.Add(this.rbSetup_Client);
            this.groupBox1.Controls.Add(this.rbSetup_Server);
            this.groupBox1.Controls.Add(this.plSetup_Serial);
            this.groupBox1.Controls.Add(this.plSetup_TcpIp);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 65);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "통신설정";
            // 
            // rbSetup_Serial
            // 
            this.rbSetup_Serial.AutoSize = true;
            this.rbSetup_Serial.Location = new System.Drawing.Point(135, 16);
            this.rbSetup_Serial.Name = "rbSetup_Serial";
            this.rbSetup_Serial.Size = new System.Drawing.Size(55, 16);
            this.rbSetup_Serial.TabIndex = 6;
            this.rbSetup_Serial.TabStop = true;
            this.rbSetup_Serial.Text = "Serial";
            this.rbSetup_Serial.UseVisualStyleBackColor = true;
            this.rbSetup_Serial.CheckedChanged += new System.EventHandler(this.rbSetup_Serial_CheckedChanged);
            // 
            // rbSetup_Client
            // 
            this.rbSetup_Client.AutoSize = true;
            this.rbSetup_Client.Location = new System.Drawing.Point(71, 17);
            this.rbSetup_Client.Name = "rbSetup_Client";
            this.rbSetup_Client.Size = new System.Drawing.Size(55, 16);
            this.rbSetup_Client.TabIndex = 5;
            this.rbSetup_Client.TabStop = true;
            this.rbSetup_Client.Text = "Client";
            this.rbSetup_Client.UseVisualStyleBackColor = true;
            this.rbSetup_Client.CheckedChanged += new System.EventHandler(this.rbSetup_Client_CheckedChanged);
            // 
            // rbSetup_Server
            // 
            this.rbSetup_Server.AutoSize = true;
            this.rbSetup_Server.Location = new System.Drawing.Point(4, 17);
            this.rbSetup_Server.Name = "rbSetup_Server";
            this.rbSetup_Server.Size = new System.Drawing.Size(59, 16);
            this.rbSetup_Server.TabIndex = 4;
            this.rbSetup_Server.TabStop = true;
            this.rbSetup_Server.Text = "Server";
            this.rbSetup_Server.UseVisualStyleBackColor = true;
            this.rbSetup_Server.CheckedChanged += new System.EventHandler(this.rbSetup_Server_CheckedChanged);
            // 
            // plSetup_Serial
            // 
            this.plSetup_Serial.Controls.Add(this.cbSetup_DtrRts);
            this.plSetup_Serial.Controls.Add(this.cbSetup_SerialBaud);
            this.plSetup_Serial.Controls.Add(this.label7);
            this.plSetup_Serial.Controls.Add(this.cbSetup_SerialPort);
            this.plSetup_Serial.Controls.Add(this.label6);
            this.plSetup_Serial.Location = new System.Drawing.Point(3, 38);
            this.plSetup_Serial.Name = "plSetup_Serial";
            this.plSetup_Serial.Size = new System.Drawing.Size(260, 23);
            this.plSetup_Serial.TabIndex = 3;
            // 
            // cbSetup_DtrRts
            // 
            this.cbSetup_DtrRts.AutoSize = true;
            this.cbSetup_DtrRts.Location = new System.Drawing.Point(208, 5);
            this.cbSetup_DtrRts.Name = "cbSetup_DtrRts";
            this.cbSetup_DtrRts.Size = new System.Drawing.Size(52, 16);
            this.cbSetup_DtrRts.TabIndex = 4;
            this.cbSetup_DtrRts.Text = "dtrrts";
            this.cbSetup_DtrRts.UseVisualStyleBackColor = true;
            // 
            // cbSetup_SerialBaud
            // 
            this.cbSetup_SerialBaud.FormattingEnabled = true;
            this.cbSetup_SerialBaud.Location = new System.Drawing.Point(136, 2);
            this.cbSetup_SerialBaud.Name = "cbSetup_SerialBaud";
            this.cbSetup_SerialBaud.Size = new System.Drawing.Size(69, 20);
            this.cbSetup_SerialBaud.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Baud";
            // 
            // cbSetup_SerialPort
            // 
            this.cbSetup_SerialPort.FormattingEnabled = true;
            this.cbSetup_SerialPort.Location = new System.Drawing.Point(36, 1);
            this.cbSetup_SerialPort.Name = "cbSetup_SerialPort";
            this.cbSetup_SerialPort.Size = new System.Drawing.Size(61, 20);
            this.cbSetup_SerialPort.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "Port";
            // 
            // plSetup_TcpIp
            // 
            this.plSetup_TcpIp.Controls.Add(this.tbSetup_IP);
            this.plSetup_TcpIp.Controls.Add(this.label5);
            this.plSetup_TcpIp.Controls.Add(this.tbSetup_Port);
            this.plSetup_TcpIp.Controls.Add(this.label1);
            this.plSetup_TcpIp.Location = new System.Drawing.Point(3, 37);
            this.plSetup_TcpIp.Name = "plSetup_TcpIp";
            this.plSetup_TcpIp.Size = new System.Drawing.Size(254, 26);
            this.plSetup_TcpIp.TabIndex = 2;
            // 
            // tbSetup_IP
            // 
            this.tbSetup_IP.Location = new System.Drawing.Point(24, 3);
            this.tbSetup_IP.Name = "tbSetup_IP";
            this.tbSetup_IP.Size = new System.Drawing.Size(131, 21);
            this.tbSetup_IP.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "IP";
            // 
            // tbSetup_Port
            // 
            this.tbSetup_Port.Location = new System.Drawing.Point(190, 3);
            this.tbSetup_Port.Name = "tbSetup_Port";
            this.tbSetup_Port.Size = new System.Drawing.Size(60, 21);
            this.tbSetup_Port.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "포트";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Reset Code";
            // 
            // tbSetup_ResetCode
            // 
            this.tbSetup_ResetCode.Location = new System.Drawing.Point(79, 68);
            this.tbSetup_ResetCode.Name = "tbSetup_ResetCode";
            this.tbSetup_ResetCode.Size = new System.Drawing.Size(190, 21);
            this.tbSetup_ResetCode.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "초";
            // 
            // tbSetup_ReStartTime
            // 
            this.tbSetup_ReStartTime.Location = new System.Drawing.Point(102, 91);
            this.tbSetup_ReStartTime.Name = "tbSetup_ReStartTime";
            this.tbSetup_ReStartTime.Size = new System.Drawing.Size(148, 21);
            this.tbSetup_ReStartTime.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "재실행 대기시간";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "감시대상";
            // 
            // btSetup_ExeOpen
            // 
            this.btSetup_ExeOpen.Location = new System.Drawing.Point(209, 114);
            this.btSetup_ExeOpen.Name = "btSetup_ExeOpen";
            this.btSetup_ExeOpen.Size = new System.Drawing.Size(57, 22);
            this.btSetup_ExeOpen.TabIndex = 12;
            this.btSetup_ExeOpen.Text = "열기";
            this.btSetup_ExeOpen.UseVisualStyleBackColor = true;
            this.btSetup_ExeOpen.Click += new System.EventHandler(this.btSetup_ExeOpen_Click);
            // 
            // tbSetup_ExeName
            // 
            this.tbSetup_ExeName.Location = new System.Drawing.Point(61, 114);
            this.tbSetup_ExeName.Name = "tbSetup_ExeName";
            this.tbSetup_ExeName.Size = new System.Drawing.Size(147, 21);
            this.tbSetup_ExeName.TabIndex = 13;
            // 
            // btSetup_Close
            // 
            this.btSetup_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSetup_Close.Location = new System.Drawing.Point(196, 137);
            this.btSetup_Close.Name = "btSetup_Close";
            this.btSetup_Close.Size = new System.Drawing.Size(75, 23);
            this.btSetup_Close.TabIndex = 14;
            this.btSetup_Close.Text = "닫기";
            this.btSetup_Close.UseVisualStyleBackColor = true;
            // 
            // btSetup_Ok
            // 
            this.btSetup_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btSetup_Ok.Location = new System.Drawing.Point(121, 137);
            this.btSetup_Ok.Name = "btSetup_Ok";
            this.btSetup_Ok.Size = new System.Drawing.Size(75, 23);
            this.btSetup_Ok.TabIndex = 15;
            this.btSetup_Ok.Text = "확인";
            this.btSetup_Ok.UseVisualStyleBackColor = true;
            this.btSetup_Ok.Click += new System.EventHandler(this.btSetup_Ok_Click);
            // 
            // WatchSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 161);
            this.Controls.Add(this.btSetup_Ok);
            this.Controls.Add(this.btSetup_Close);
            this.Controls.Add(this.tbSetup_ExeName);
            this.Controls.Add(this.btSetup_ExeOpen);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbSetup_ReStartTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSetup_ResetCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Name = "WatchSetup";
            this.Text = "WatchSetup";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.plSetup_Serial.ResumeLayout(false);
            this.plSetup_Serial.PerformLayout();
            this.plSetup_TcpIp.ResumeLayout(false);
            this.plSetup_TcpIp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSetup_Serial;
        private System.Windows.Forms.RadioButton rbSetup_Client;
        private System.Windows.Forms.RadioButton rbSetup_Server;
        private System.Windows.Forms.Panel plSetup_Serial;
        private System.Windows.Forms.CheckBox cbSetup_DtrRts;
        private System.Windows.Forms.ComboBox cbSetup_SerialBaud;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbSetup_SerialPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel plSetup_TcpIp;
        private System.Windows.Forms.TextBox tbSetup_IP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSetup_Port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSetup_ResetCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSetup_ReStartTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btSetup_ExeOpen;
        private System.Windows.Forms.TextBox tbSetup_ExeName;
        private System.Windows.Forms.Button btSetup_Close;
        private System.Windows.Forms.Button btSetup_Ok;
    }
}