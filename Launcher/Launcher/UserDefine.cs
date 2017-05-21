
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using System;


namespace Launcher
{
    class UserDefine
    {
        private Launcher _MainForm;
        BaseInfo BI;

        public UserDefine(Launcher main_form, BaseInfo bi)
        {
            _MainForm = main_form;
            BI = bi;
        }

        public bool ReadReg()
        {
            string regSubkey = @"Software\" + Application.ProductName;

            // 서브키를 얻어온다. 없으면 null
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(regSubkey, false);

            if (rk == null)
            {
                DefaultReg();
            }
            else
            {
                string[] WeekKey = { "ReBoot_Mon", "ReBoot_Tue", "ReBoot_Wed", "ReBoot_Thu", "ReBoot_Fri", "ReBoot_Sat", "ReBoot_Sun" };
                string name_key;
                string run_name;
                int i;

                //BI.Server.Port = (int)rk.GetValue("Port", 50001);
                //BI.ReStart_MaxTime = (int)rk.GetValue("ReStartTime", 60);
                BI.AutoRunOn = Convert.ToBoolean(rk.GetValue("AutoRunOn", true));
                BI.TrayIconOn = Convert.ToBoolean(rk.GetValue("TrayIconOn", true));

                BI.ReBootingOn = Convert.ToBoolean(rk.GetValue("ReBootingOn", true));
                for (i = 0; i < 7; i++)
                {
                    BI.Week[i] = Convert.ToBoolean(rk.GetValue(WeekKey[i], true));
                }
                BI.ReBootingTime = rk.GetValue("ReBootingTime", "3:00").ToString();

                for(i = 0; i < 10; i++) 
                {
                    name_key = String.Format("Run{0}_FileName", i);
                    run_name = rk.GetValue(name_key, "").ToString();
                    if (run_name.Length > 0)
                    {
                        //_MainForm.RunMsg_Insert(run_name);
                    }
                }

            }

            return true;
        }

        public bool WriteReg()
        {
            string regSubkey = @"Software\" + Application.ProductName;
            string[] WeekKey = { "ReBoot_Mon", "ReBoot_Tue", "ReBoot_Wed", "ReBoot_Thu", "ReBoot_Fri", "ReBoot_Sat", "ReBoot_Sun" };
            string key_name;

            try
            {
                // 서브키를 얻어온다. 없으면 null
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(regSubkey, true);
                int i;

                // 없으면 서브키를 만든다.
                if (rk == null)
                {
                    // 해당이름으로 서브키 생성
                    rk = Registry.LocalMachine.CreateSubKey(regSubkey);
                }
                //rk.SetValue("Port", BI.Server.Port);
                //rk.SetValue("ReStartTime", BI.ReStart_MaxTime);
                rk.SetValue("AutoRunOn", BI.AutoRunOn);
                rk.SetValue("TrayIconOn", BI.TrayIconOn);
                rk.SetValue("ReBootingOn", BI.ReBootingOn);
                for (i = 0; i < 7; i++)
                {
                    rk.SetValue(WeekKey[i], BI.Week[i]);
                }
                rk.SetValue("ReBootingTime", BI.ReBootingTime);

                for (i = 0; i < 10; i++)
                {
                    key_name = String.Format("Run{0}_FileName", i);

                    //if (i < BI.Exe_Name.Count)
                    //{
                    //    rk.SetValue(key_name, BI.Exe_Name[i].ToString());
                    //}
                    //else
                    //{
                    //    string run_name = rk.GetValue(key_name, "").ToString();
                    //    if(run_name != "") rk.DeleteValue(key_name);
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error WriteReg : {0}", e.Message);
            }
            return true;
        }

        public void DefaultReg()
        {
            //BI.Server.Port = 50001;
            //BI.ReStart_MaxTime = 60;
            BI.AutoRunOn = true;
            BI.TrayIconOn = true;
            BI.ReBootingOn = true;
            for (int i = 0; i < 7; i++) BI.Week[i] = true;
            BI.ReBootingTime = "3:00";

            WriteReg();
        }



    }
}
