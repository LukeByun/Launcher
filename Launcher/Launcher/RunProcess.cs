using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;

namespace Launcher
{
    /// <summary>
    /// 실행중인 프로세서를 찾거나 감시한다.
    /// </summary>

    class RunProcess
    {

        /// <summary>
        /// 윈도우 타이틀 이름으로 프로세서가 동작 중인지를 확인 한다.
        /// </summary>
        /// <param name="name">윈도우 타이틀 이름 (확장자 빼고)</param>
        /// <returns>true:동작중, false:실행상태 아님</returns>
        public bool IsRun(string name)
        {
            bool result = false;

            // 이름으로 프로세스 찾기
            string run_name = Path.GetFileNameWithoutExtension(name);
            Process[] procs = Process.GetProcessesByName(run_name);
            if (procs.Length > 0) result = true;
            
            
            // 실행중인 모든 프로세서 검사
            //foreach (Process pro in Process.GetProcesses())
            //{
            //    Console.WriteLine("Run Process : {0}", pro.ToString());
            //}


            #region 검색된 프로세서의 상세 정보
            //foreach (Process proc in procs)
            //{

            //    // 연결된 프로세스의 기본 우선 순위를 가져옵니다.
            //    Console.WriteLine("BasePriority : {0}", proc.BasePriority);
            //    // 프로세스가 종료될 때 Exited 이벤트를 발생시켜야 하는지 여부를 나타내는 값을 가져오거나 설정합니다.
            //    Console.WriteLine("EnableRaisingEvents : {0}", proc.EnableRaisingEvents);
            //    // 연결된 프로세스가 종료될 때 연결된 프로세스에서 지정한 값을 가져옵니다. 
            //    // Console.WriteLine("ExitCode : {0}", proc.ExitCode);
            //    // 연결된 프로세스가 종료된 시간을 가져옵니다. 
            //    // Console.WriteLine("ExitTime : {0}", proc.ExitTime);
            //    // 관련된 프로세스의 기본 핸들을 반환합니다. 
            //    Console.WriteLine("Handle : {0}", proc.Handle);
            //    // 프로세스에서 연 핸들 수를 가져옵니다. 
            //    Console.WriteLine("HandleCount : {0}", proc.HandleCount);
            //    // 연결된 프로세스가 종료되었는지 여부를 나타내는 값을 가져옵니다. 
            //    Console.WriteLine("HasExited : {0}", proc.HasExited);
            //    // 연결된 프로세스의 고유 식별자를 가져옵니다. 
            //    Console.WriteLine("Id : {0}", proc.Id);
            //    // 연결된 프로세스가 실행 중인 컴퓨터 이름을 가져옵니다. 
            //    Console.WriteLine("MachineName : {0}", proc.MachineName);
            //    // 연결된 프로세스의 주 모듈을 가져옵니다. 
            //    Console.WriteLine("MainModule : {0}", proc.MainModule);
            //    // 연결된 프로세스의 주 창에 대한 창 핸들을 가져옵니다. 
            //    Console.WriteLine("MainWindowHandle : {0}", proc.MainWindowHandle);
            //    // 프로세스의 주 창에 대한 캡션을 가져옵니다.
            //    Console.WriteLine("MainWindowTitle : {0}", proc.MainWindowTitle);
            //    // 연결된 프로세스에 대해 허용되는 작업 집합의 최대 크기를 가져오거나 설정합니다. 
            //    Console.WriteLine("MaxWorkingSet : {0}", proc.MaxWorkingSet);
            //    // 연결된 프로세스에 대해 허용되는 작업 집합의 최소 크기를 가져오거나 설정합니다. 
            //    Console.WriteLine("MinWorkingSet : {0}", proc.MinWorkingSet);
            //    // 연결된 프로세스에 의해 로드된 모듈을 가져옵니다. 
            //    Console.WriteLine("Modules : {0}", proc.Modules);
            //    // 이 프로세스에 할당된 페이지되지 않은 시스템 메모리 크기를 가져옵니다. 
            //    Console.WriteLine("NonpagedSystemMemorySize : {0}", proc.NonpagedSystemMemorySize);
            //    // 페이징된 메모리의 크기를 가져옵니다. 
            //    Console.WriteLine("PagedMemorySize : {0}", proc.PagedMemorySize);
            //    // 페이징된 시스템 메모리의 크기를 가져옵니다. 
            //    Console.WriteLine("PagedSystemMemorySize : {0}", proc.PagedSystemMemorySize);
            //    // 페이징된 메모리의 최대 크기를 가져옵니다. 
            //    Console.WriteLine("PeakPagedMemorySize : {0}", proc.PeakPagedMemorySize);
            //    // 가상 메모리의 최대 크기를 가져옵니다. 
            //    Console.WriteLine("PeakVirtualMemorySize : {0}", proc.PeakVirtualMemorySize);
            //    // 작업 집합의 최대 크기를 가져옵니다. 
            //    Console.WriteLine("PeakWorkingSet : {0}", proc.PeakWorkingSet);
            //    // 포커스가 주 창에 있을 때 운영 체제가 연결된 프로세스의 우선 순위를 일시적으로 높일 것인지 여부를 나타내는 값을 가져오거나 설정합니다. 
            //    Console.WriteLine("PriorityBoostEnabled : {0}", proc.PriorityBoostEnabled);
            //    // 연결된 프로세스에 대한 전체 우선 순위 범주를 가져오거나 설정합니다. 
            //    Console.WriteLine("PriorityClass : {0}", proc.PriorityClass);
            //    // 전용 메모리의 크기를 가져옵니다. 
            //    Console.WriteLine("PrivateMemorySize : {0}", proc.PrivateMemorySize);
            //    // 해당 프로세스의 시스템 프로세서 시간을 가져옵니다. 
            //    Console.WriteLine("PrivilegedProcessorTime : {0}", proc.PrivilegedProcessorTime);
            //    // 프로세스의 이름을 가져옵니다. 
            //    Console.WriteLine("ProcessName : {0}", proc.ProcessName);
            //    // 해당 프로세스에 포함된 스레드의 실행을 예약할 수 있는 프로세서를 가져오거나 설정합니다. 
            //    Console.WriteLine("ProcessorAffinity : {0}", proc.ProcessorAffinity);
            //    // 프로세스의 사용자 인터페이스가 응답하는지 여부를 나타내는 값을 가져옵니다. 
            //    Console.WriteLine("Responding : {0}", proc.Responding);
            //    // (Component에서 상속) Component의 ISite를 가져오거나 설정합니다. 
            //    Console.WriteLine("Site : {0}", proc.Site);
            //    // 응용 프로그램에서 나오는 오류 출력을 읽는 StreamReader를 가져옵니다. 
            //    // Console.WriteLine("StandardError : {0}", proc.StandardError);
            //    // 응용 프로그램에서 프로세스에 입력을 쓸 수 있는 StreamWriter를 가져옵니다. 
            //    // Console.WriteLine("StandardInput : {0}", proc.StandardInput);
            //    // 응용 프로그램에서 프로세스의 출력을 읽을 수 있는 StreamReader를 가져옵니다. 
            //    // Console.WriteLine("StandardOutput : {0}", proc.StandardOutput);
            //    // Process의 Start 메서드에 전달할 속성을 가져오거나 설정합니다. 
            //    Console.WriteLine("StartInfo : {0}", proc.StartInfo);
            //    // 연결된 프로세스가 시작된 시간을 가져옵니다. 
            //    Console.WriteLine("StartTime : {0}", proc.StartTime);
            //    // 프로세스 종료 이벤트의 결과로 발생하는 이벤트 처리기 호출을 마샬링하는 데 사용되는 개체를 가져오거나 설정합니다. 
            //    Console.WriteLine("SynchronizingObject : {0}", proc.SynchronizingObject);
            //    // 연결된 프로세스에서 실행 중인 스레드를 가져오거나 설정합니다. 
            //    Console.WriteLine("Threads : {0}", proc.Threads);
            //    // 해당 프로세스의 총 프로세서 시간을 가져옵니다. 
            //    Console.WriteLine("TotalProcessorTime : {0}", proc.TotalProcessorTime);
            //    // 해당 프로세스의 사용자 프로세서 시간을 가져옵니다. 
            //    Console.WriteLine("UserProcessorTime : {0}", proc.UserProcessorTime);
            //    // 프로세스의 가상 메모리 크기를 가져옵니다. 
            //    Console.WriteLine("VirtualMemorySize : {0}", proc.VirtualMemorySize);
            //    // 연결된 프로세스의 실제 메모리 사용량을 가져옵니다. 
            //    Console.WriteLine("WorkingSet : {0}", proc.WorkingSet);
            //    result = true;
            //}

            #endregion

            return result;
        }

        public bool IsRunClass(string name)
        {
            var handle = IntPtr.Zero;
            StringBuilder sb = new StringBuilder();
            string run_name = Path.GetFileNameWithoutExtension(name);

            do
            {
                handle = Win32API.FindWindowEx(IntPtr.Zero, handle, "#32770", null);

                if (handle != IntPtr.Zero)
                {
                    Win32API.GetWindowText(handle.ToInt32(), sb, 100);
                    if (sb.ToString() == run_name)
                    {
                        Win32API.SendMessage(handle.ToInt32(), Win32API.WM_CLOSE, 0, 0);
                        return true;
                    }

//                    Console.WriteLine("Found handle: {0:X}, Title:{1}", handle.ToInt64(), sb.ToString());
                }
            } while (handle != IntPtr.Zero);

            return false;
        }



        /// <summary>
        /// 경로포함 실행파일 명으로 프로그램을 동작 시킨다.
        /// </summary>
        /// <param name="name"></param>
        public bool Start(string name)
        {
            bool result = true;

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = Path.GetFileName(name);
                psi.WorkingDirectory = Path.GetDirectoryName(name);
                psi.Arguments = "";

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Process.Start : {0}", ex.Message);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 동작중인 실행프로그램을 죽인다.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Kill(string name)
        {
            bool result = false;

            try
            {
                string run_name = Path.GetFileName(name);
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.ProcessName.StartsWith(run_name))
                    {
                        process.Kill();
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Process.Kill : {0}", ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 컴퓨터 를 강제 리부팅 시킨다.
        /// </summary>
        public static void ComputerRestart()
        {

#if false
            // 강제 종료
            // System.Diagnostics.Process.Start("shutdown.exe", "-s -f");
            // 종료 카운트다운 때 아래 명령을 날리면 종료가 취소됨
            // System.Diagnostics.Process.Start("shutdown.exe", "-a");
            // 재시작
            System.Diagnostics.Process.Start("shutdown.exe", "-r");
            // 로그오프
            // System.Diagnostics.Process.Start("shutdown.exe", "-l");
            
#else

            Win32API.DoExitWin(Win32API.EWX_REBOOT);               

#endif

        }

        /// <summary>
        /// 윈도우 레지스트리 에 부팅시 자동실행 목록을 설정 한다.
        /// </summary>
        /// <param name="type">false:자동실행 기능 Off, true:자동실행 기능 On</param>
        public void SetAutoRunProgram(bool type)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            string exe_name = Application.ExecutablePath.ToString();
            exe_name = exe_name.Replace('/', '\\');
            string key_name = Path.GetFileNameWithoutExtension(exe_name);

            if (type)
            {
                // 레지스트리 등록 할때
                if (registryKey.GetValue(key_name) == null)
                {
                    registryKey.SetValue(key_name, exe_name);
                }
            }
            else
            {
                //레지스트리 삭제 할때
                if (registryKey.GetValue(key_name) != null)
                {
                    registryKey.DeleteValue(key_name, false);
                }
            }
        }

        /// <summary>
        /// 윈도우 부팅시 자동실행 여부를 설정 한다.
        /// </summary>
        /// <param name="type">false:자동실행 기능 Off, true:자동실행 기능 On</param>
        /// <param name="key">자동실행 Key값(보통 실행명)</param>
        /// <param name="exe_name">실행될 경로포함 파일명</param>
        public void SetAutoRunProgram(bool type, string key, string exe_name)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (type)
            {
                // 레지스트리 등록 할때
                if (registryKey.GetValue(key) == null)
                {
                    registryKey.SetValue(key, exe_name);
                }
            }
            else
            {
                //레지스트리 삭제 할때
                if (registryKey.GetValue(key) != null)
                {
                    registryKey.DeleteValue(key, false);
                }
            }
        }

        /// <summary>
        /// 자동실행 기능 실행 여부를 레지스트리 에서 가져온다.
        /// </summary>
        /// <returns>false:자동실행 Off, true:자동실행 On</returns>
        public bool IsAutoRunProgram()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            string exe_name = Application.ExecutablePath.ToString();
            string key_name = Path.GetFileNameWithoutExtension(exe_name);

            if (registryKey.GetValue(key_name) != null) return true;
            return false;
        }

    }
}
