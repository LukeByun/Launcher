namespace Launcher
{
    partial class Launcher
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvRun_Msg = new System.Windows.Forms.ListView();
            this.btRun_Delete = new System.Windows.Forms.Button();
            this.btRun_Insert = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvLog = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btSet_Setup = new System.Windows.Forms.Button();
            this.btSet_Exit = new System.Windows.Forms.Button();
            this.btSet_TryIcon = new System.Windows.Forms.Button();
            this.timer_RunCheck = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.설정화면ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvRun_Msg);
            this.groupBox1.Controls.Add(this.btRun_Delete);
            this.groupBox1.Controls.Add(this.btRun_Insert);
            this.groupBox1.Location = new System.Drawing.Point(0, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 204);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "실행 파일";
            // 
            // lvRun_Msg
            // 
            this.lvRun_Msg.Location = new System.Drawing.Point(8, 18);
            this.lvRun_Msg.Name = "lvRun_Msg";
            this.lvRun_Msg.Size = new System.Drawing.Size(461, 177);
            this.lvRun_Msg.TabIndex = 3;
            this.lvRun_Msg.UseCompatibleStateImageBehavior = false;
            this.lvRun_Msg.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvRun_Msg_ItemChecked);
            // 
            // btRun_Delete
            // 
            this.btRun_Delete.Location = new System.Drawing.Point(478, 45);
            this.btRun_Delete.Name = "btRun_Delete";
            this.btRun_Delete.Size = new System.Drawing.Size(110, 23);
            this.btRun_Delete.TabIndex = 2;
            this.btRun_Delete.Text = "삭제";
            this.btRun_Delete.UseVisualStyleBackColor = true;
            this.btRun_Delete.Click += new System.EventHandler(this.btRun_Delete_Click);
            // 
            // btRun_Insert
            // 
            this.btRun_Insert.Location = new System.Drawing.Point(478, 18);
            this.btRun_Insert.Name = "btRun_Insert";
            this.btRun_Insert.Size = new System.Drawing.Size(110, 23);
            this.btRun_Insert.TabIndex = 1;
            this.btRun_Insert.Text = "추가";
            this.btRun_Insert.UseVisualStyleBackColor = true;
            this.btRun_Insert.Click += new System.EventHandler(this.btRun_Insert_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvLog);
            this.groupBox2.Location = new System.Drawing.Point(0, 209);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(595, 161);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // lvLog
            // 
            this.lvLog.Location = new System.Drawing.Point(10, 19);
            this.lvLog.Name = "lvLog";
            this.lvLog.Size = new System.Drawing.Size(577, 133);
            this.lvLog.TabIndex = 0;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btSet_Setup);
            this.groupBox3.Controls.Add(this.btSet_Exit);
            this.groupBox3.Controls.Add(this.btSet_TryIcon);
            this.groupBox3.Location = new System.Drawing.Point(0, 375);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(595, 73);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "설정";
            // 
            // btSet_Setup
            // 
            this.btSet_Setup.Location = new System.Drawing.Point(293, 20);
            this.btSet_Setup.Name = "btSet_Setup";
            this.btSet_Setup.Size = new System.Drawing.Size(103, 43);
            this.btSet_Setup.TabIndex = 2;
            this.btSet_Setup.Text = "설정";
            this.btSet_Setup.UseVisualStyleBackColor = true;
            this.btSet_Setup.Click += new System.EventHandler(this.btSet_Setup_Click);
            // 
            // btSet_Exit
            // 
            this.btSet_Exit.Location = new System.Drawing.Point(401, 19);
            this.btSet_Exit.Name = "btSet_Exit";
            this.btSet_Exit.Size = new System.Drawing.Size(64, 45);
            this.btSet_Exit.TabIndex = 1;
            this.btSet_Exit.Text = "종료";
            this.btSet_Exit.UseVisualStyleBackColor = true;
            this.btSet_Exit.Click += new System.EventHandler(this.btSet_Exit_Click);
            // 
            // btSet_TryIcon
            // 
            this.btSet_TryIcon.Location = new System.Drawing.Point(469, 18);
            this.btSet_TryIcon.Name = "btSet_TryIcon";
            this.btSet_TryIcon.Size = new System.Drawing.Size(118, 46);
            this.btSet_TryIcon.TabIndex = 0;
            this.btSet_TryIcon.Text = "감추기";
            this.btSet_TryIcon.UseVisualStyleBackColor = true;
            this.btSet_TryIcon.Click += new System.EventHandler(this.btSet_TryIcon_Click);
            // 
            // timer_RunCheck
            // 
            this.timer_RunCheck.Interval = 1000;
            this.timer_RunCheck.Tick += new System.EventHandler(this.timer_RunCheck_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "메롱";
            this.notifyIcon1.BalloonTipTitle = "더블클릭";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Launcher";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.설정화면ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 48);
            // 
            // 설정화면ToolStripMenuItem
            // 
            this.설정화면ToolStripMenuItem.Name = "설정화면ToolStripMenuItem";
            this.설정화면ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.설정화면ToolStripMenuItem.Text = "설정화면";
            this.설정화면ToolStripMenuItem.Click += new System.EventHandler(this.설정화면ToolStripMenuItem_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 448);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Launcher";
            this.Text = "Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Launcher_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Launcher_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btRun_Delete;
        private System.Windows.Forms.Button btRun_Insert;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btSet_Setup;
        private System.Windows.Forms.Button btSet_Exit;
        private System.Windows.Forms.Button btSet_TryIcon;
        private System.Windows.Forms.ListView lvRun_Msg;
        private System.Windows.Forms.Timer timer_RunCheck;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 설정화면ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
    }
}

