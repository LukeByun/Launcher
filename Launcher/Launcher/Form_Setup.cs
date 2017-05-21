using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Launcher
{
    partial class Form_Setup : Form
    {
        private BaseInfo BI = null;

        public Form_Setup()
        {
            InitializeComponent();
        }

        public void InitializeForm(BaseInfo bi)
        {
            BI = bi;
            RunProcess run = new RunProcess();

            cbSetup_Rebooting.Checked = BI.ReBootingOn;
            cbSetup_Mon.Checked = BI.Week[0];
            cbSetup_Tue.Checked = BI.Week[1];
            cbSetup_Wed.Checked = BI.Week[2];
            cbSetup_Thu.Checked = BI.Week[3];
            cbSetup_Fri.Checked = BI.Week[4];
            cbSetup_Sat.Checked = BI.Week[5];
            cbSetup_Sun.Checked = BI.Week[6];
            dtpSetup_RebootTime.Text = BI.ReBootingTime;

            if(run.IsAutoRunProgram())
                cbSetup_AutoRun.Checked = true;
            else
                cbSetup_AutoRun.Checked = false;
            cbSetup_TrayIcon.Checked = BI.TrayIconOn;
        }



        private void btOk_Click(object sender, EventArgs e)
        {
            RunProcess run = new RunProcess();

            BI.ReBootingOn = cbSetup_Rebooting.Checked;
            BI.Week[0] = cbSetup_Mon.Checked;
            BI.Week[1] = cbSetup_Tue.Checked;
            BI.Week[2] = cbSetup_Wed.Checked;
            BI.Week[3] = cbSetup_Thu.Checked;
            BI.Week[4] = cbSetup_Fri.Checked;
            BI.Week[5] = cbSetup_Sat.Checked;
            BI.Week[6] = cbSetup_Sun.Checked;
            dtpSetup_RebootTime.Format = DateTimePickerFormat.Custom;
            dtpSetup_RebootTime.CustomFormat = "HH:mm";
            BI.ReBootingTime = dtpSetup_RebootTime.Text;

            BI.AutoRunOn = cbSetup_AutoRun.Checked;
            if (BI.AutoRunOn) run.SetAutoRunProgram(true);
            else run.SetAutoRunProgram(false);

            BI.TrayIconOn = cbSetup_TrayIcon.Checked;

            //Launcher.Def.WriteReg();
            this.Close();
        }

    }
}
