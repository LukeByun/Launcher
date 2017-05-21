namespace Launcher
{
    partial class Form_Setup
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbSetup_TrayIcon = new System.Windows.Forms.CheckBox();
            this.cbSetup_AutoRun = new System.Windows.Forms.CheckBox();
            this.btOk = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtpSetup_RebootTime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSetup_Sun = new System.Windows.Forms.CheckBox();
            this.cbSetup_Sat = new System.Windows.Forms.CheckBox();
            this.cbSetup_Fri = new System.Windows.Forms.CheckBox();
            this.cbSetup_Thu = new System.Windows.Forms.CheckBox();
            this.cbSetup_Wed = new System.Windows.Forms.CheckBox();
            this.cbSetup_Tue = new System.Windows.Forms.CheckBox();
            this.cbSetup_Mon = new System.Windows.Forms.CheckBox();
            this.cbSetup_Rebooting = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbSetup_TrayIcon);
            this.groupBox2.Controls.Add(this.cbSetup_AutoRun);
            this.groupBox2.Location = new System.Drawing.Point(2, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 37);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "시작설정";
            // 
            // cbSetup_TrayIcon
            // 
            this.cbSetup_TrayIcon.AutoSize = true;
            this.cbSetup_TrayIcon.Location = new System.Drawing.Point(107, 16);
            this.cbSetup_TrayIcon.Name = "cbSetup_TrayIcon";
            this.cbSetup_TrayIcon.Size = new System.Drawing.Size(100, 16);
            this.cbSetup_TrayIcon.TabIndex = 1;
            this.cbSetup_TrayIcon.Text = "시작시 아이콘";
            this.cbSetup_TrayIcon.UseVisualStyleBackColor = true;
            // 
            // cbSetup_AutoRun
            // 
            this.cbSetup_AutoRun.AutoSize = true;
            this.cbSetup_AutoRun.Location = new System.Drawing.Point(12, 17);
            this.cbSetup_AutoRun.Name = "cbSetup_AutoRun";
            this.cbSetup_AutoRun.Size = new System.Drawing.Size(72, 16);
            this.cbSetup_AutoRun.TabIndex = 0;
            this.cbSetup_AutoRun.Text = "자동실행";
            this.cbSetup_AutoRun.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(163, 153);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(105, 23);
            this.btOk.TabIndex = 2;
            this.btOk.Text = "확인";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(271, 106);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "환경설정";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpSetup_RebootTime);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.cbSetup_Sun);
            this.groupBox4.Controls.Add(this.cbSetup_Sat);
            this.groupBox4.Controls.Add(this.cbSetup_Fri);
            this.groupBox4.Controls.Add(this.cbSetup_Thu);
            this.groupBox4.Controls.Add(this.cbSetup_Wed);
            this.groupBox4.Controls.Add(this.cbSetup_Tue);
            this.groupBox4.Controls.Add(this.cbSetup_Mon);
            this.groupBox4.Controls.Add(this.cbSetup_Rebooting);
            this.groupBox4.Location = new System.Drawing.Point(5, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(261, 84);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "컴퓨터 리부팅 설정";
            // 
            // dtpSetup_RebootTime
            // 
            this.dtpSetup_RebootTime.CustomFormat = "hh:mm";
            this.dtpSetup_RebootTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpSetup_RebootTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpSetup_RebootTime.Location = new System.Drawing.Point(50, 58);
            this.dtpSetup_RebootTime.Name = "dtpSetup_RebootTime";
            this.dtpSetup_RebootTime.Size = new System.Drawing.Size(97, 21);
            this.dtpSetup_RebootTime.TabIndex = 9;
            this.dtpSetup_RebootTime.Value = new System.DateTime(2015, 4, 18, 11, 22, 50, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "시간";
            // 
            // cbSetup_Sun
            // 
            this.cbSetup_Sun.AutoSize = true;
            this.cbSetup_Sun.Location = new System.Drawing.Point(6, 37);
            this.cbSetup_Sun.Name = "cbSetup_Sun";
            this.cbSetup_Sun.Size = new System.Drawing.Size(36, 16);
            this.cbSetup_Sun.TabIndex = 7;
            this.cbSetup_Sun.Text = "일";
            this.cbSetup_Sun.UseVisualStyleBackColor = true;
            // 
            // cbSetup_Sat
            // 
            this.cbSetup_Sat.AutoSize = true;
            this.cbSetup_Sat.Location = new System.Drawing.Point(223, 37);
            this.cbSetup_Sat.Name = "cbSetup_Sat";
            this.cbSetup_Sat.Size = new System.Drawing.Size(36, 16);
            this.cbSetup_Sat.TabIndex = 6;
            this.cbSetup_Sat.Text = "토";
            this.cbSetup_Sat.UseVisualStyleBackColor = true;
            // 
            // cbSetup_Fri
            // 
            this.cbSetup_Fri.AutoSize = true;
            this.cbSetup_Fri.Location = new System.Drawing.Point(187, 37);
            this.cbSetup_Fri.Name = "cbSetup_Fri";
            this.cbSetup_Fri.Size = new System.Drawing.Size(36, 16);
            this.cbSetup_Fri.TabIndex = 5;
            this.cbSetup_Fri.Text = "금";
            this.cbSetup_Fri.UseVisualStyleBackColor = true;
            // 
            // cbSetup_Thu
            // 
            this.cbSetup_Thu.AutoSize = true;
            this.cbSetup_Thu.Location = new System.Drawing.Point(151, 37);
            this.cbSetup_Thu.Name = "cbSetup_Thu";
            this.cbSetup_Thu.Size = new System.Drawing.Size(36, 16);
            this.cbSetup_Thu.TabIndex = 4;
            this.cbSetup_Thu.Text = "목";
            this.cbSetup_Thu.UseVisualStyleBackColor = true;
            // 
            // cbSetup_Wed
            // 
            this.cbSetup_Wed.AutoSize = true;
            this.cbSetup_Wed.Location = new System.Drawing.Point(115, 37);
            this.cbSetup_Wed.Name = "cbSetup_Wed";
            this.cbSetup_Wed.Size = new System.Drawing.Size(36, 16);
            this.cbSetup_Wed.TabIndex = 3;
            this.cbSetup_Wed.Text = "수";
            this.cbSetup_Wed.UseVisualStyleBackColor = true;
            // 
            // cbSetup_Tue
            // 
            this.cbSetup_Tue.AutoSize = true;
            this.cbSetup_Tue.Location = new System.Drawing.Point(79, 37);
            this.cbSetup_Tue.Name = "cbSetup_Tue";
            this.cbSetup_Tue.Size = new System.Drawing.Size(36, 16);
            this.cbSetup_Tue.TabIndex = 2;
            this.cbSetup_Tue.Text = "화";
            this.cbSetup_Tue.UseVisualStyleBackColor = true;
            // 
            // cbSetup_Mon
            // 
            this.cbSetup_Mon.AutoSize = true;
            this.cbSetup_Mon.Location = new System.Drawing.Point(43, 37);
            this.cbSetup_Mon.Name = "cbSetup_Mon";
            this.cbSetup_Mon.Size = new System.Drawing.Size(36, 16);
            this.cbSetup_Mon.TabIndex = 1;
            this.cbSetup_Mon.Text = "월";
            this.cbSetup_Mon.UseVisualStyleBackColor = true;
            // 
            // cbSetup_Rebooting
            // 
            this.cbSetup_Rebooting.AutoSize = true;
            this.cbSetup_Rebooting.Location = new System.Drawing.Point(7, 17);
            this.cbSetup_Rebooting.Name = "cbSetup_Rebooting";
            this.cbSetup_Rebooting.Size = new System.Drawing.Size(82, 16);
            this.cbSetup_Rebooting.TabIndex = 0;
            this.cbSetup_Rebooting.Text = "리부팅 ON";
            this.cbSetup_Rebooting.UseVisualStyleBackColor = true;
            // 
            // Form_Setup
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 179);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_Setup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "설정";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbSetup_TrayIcon;
        private System.Windows.Forms.CheckBox cbSetup_AutoRun;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpSetup_RebootTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbSetup_Sun;
        private System.Windows.Forms.CheckBox cbSetup_Sat;
        private System.Windows.Forms.CheckBox cbSetup_Fri;
        private System.Windows.Forms.CheckBox cbSetup_Thu;
        private System.Windows.Forms.CheckBox cbSetup_Wed;
        private System.Windows.Forms.CheckBox cbSetup_Tue;
        private System.Windows.Forms.CheckBox cbSetup_Mon;
        private System.Windows.Forms.CheckBox cbSetup_Rebooting;
    }
}