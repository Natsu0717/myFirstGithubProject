using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CMD_CURL_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入要执行的命令:");
            string strInput = Console.ReadLine();
            string strOuput = "";
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "cmd.exe";
                //是否使用操作系统shell启动
                p.StartInfo.UseShellExecute = false;
                // 接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardInput = true;
                //输出信息
                p.StartInfo.RedirectStandardOutput = true;
                // 输出错误
                p.StartInfo.RedirectStandardError = true;
                //不显示程序窗口
                p.StartInfo.CreateNoWindow = true;
                //启动程序
                p.Start();

                //向cmd窗口发送输入信息
                p.StandardInput.WriteLine(strInput + "&&exit");

                /// 多命令请使用批处理命令连接符：
                /// <![CDATA[
                /// &:同时执行两个命令
                /// |:将上一个命令的输出,作为下一个命令的输入
                /// &&：当&&前的命令成功时,才执行&&后的命令
                /// ||：当||前的命令失败时,才执行||后的命令]]>

                p.StandardInput.AutoFlush = true;

                //获取输出信息
                strOuput = p.StandardOutput.ReadToEnd();
                //等待程序执行完退出进程
                p.WaitForExit();
                p.Close();
            }
            //设置要启动的应用程序
            Console.WriteLine(strOuput);
            Console.ReadKey();
        }
    }
}
