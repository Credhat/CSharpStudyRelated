# File Explore Operation

### 1. 通过文件夹浏览器浏览文件夹

```Csharp
///<summary>打开filePath的文件夹</summary>
System.Diagnostics.Process.Start("explorer.exe",filePath);
```

### 访问网络共享文件夹

```Csharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Management;//References也要引用，在.NET里
using System.IO;

using System.Diagnostics;


namespace @in
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            bool status = false;
            string filepath = "";


            //连接共享文件夹
            //status = connectState(@"\\dechang.kin.net", @"kin\wanghaidong", "123456");
            //直接用IP也可以
            //status = connectState(@"\\172.18.20.121", @"kin\wanghaidong", "123456");
            //本机
            status = connectState(@"\\puter.kin.net", @"kin\wanghaidong", "123456");
            if (status)
            {
                //共享文件夹的目录
                //DirectoryInfo theFolder = new DirectoryInfo(@"dechang.kin.net\kin");
                //相对共享文件夹的路径，这个路径一般都是映射
                filepath = @"I:\kin\";
                //本机
                filepath = @"H:\";
                //获取保存文件的路径
                string filename = filepath;// theFolder.ToString() + fielpath;
                //执行方法，把本地D盘的1文件复制到服务器上，并命名2
                //Transport(@"D:\1.txt", filename, "2.txt");
                //打开路径（文件夹）
                System.Diagnostics.Process.Start(filepath);
            }
            else
            {
                MessageBox.Show("未能连接！");
            }
        }


        public static bool connectState(string path)
        {
            return connectState(path, "", "");
        }
        /// <summary>
        /// 连接远程共享文件夹
        /// </summary>
        /// <param name="path">远程共享文件夹的路径</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public static bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = "net use " + path + " " + passWord + " /user:" + userName;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }

        /// <summary>
        /// 向远程文件夹保存本地内容，或者从远程文件夹下载文件到本地
        /// </summary>
        /// <param name="src">要保存的文件的路径，如果保存文件到共享文件夹，这个路径就是本地文件路径如：@"D:\1.avi"</param>
        /// <param name="dst">保存文件的路径，不含名称及扩展名</param>
        /// <param name="fileName">保存文件的名称以及扩展名</param>
        public static void Transport(string src, string dst, string fileName)
        {
            FileStream inFileStream = new FileStream(src, FileMode.Open);
            if (!Directory.Exists(dst))
            {
                Directory.CreateDirectory(dst);
            }
            dst = dst + fileName;
            FileStream outFileStream = new FileStream(dst, FileMode.OpenOrCreate);

            byte[] buf = new byte[inFileStream.Length];
            int byteCount;
            while ((byteCount = inFileStream.Read(buf, 0, buf.Length)) > 0)
            {
                outFileStream.Write(buf, 0, byteCount);
            }

            inFileStream.Flush();
            inFileStream.Close()

            outFileStream.Flush();
            outFileStream.Close();


        }
    }
}
```
