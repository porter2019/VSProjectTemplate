using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Tools
{
    /// <summary>
    /// IO操作
    /// </summary>
    public class IOHelper
    {
        /// <summary>
        /// 写文本到文本中
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="file_name">文件名，不含拓展名</param>
        /// <returns></returns>
        public static void WriteText(string text, string file_name)
        {
            string folder = System.Threading.Thread.GetDomain().BaseDirectory + "config\\";
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            string wl_path = folder + file_name;
            StreamWriter sw = new StreamWriter(wl_path, false, Encoding.UTF8);
            sw.WriteLine(text);
            sw.Close();
        }

        /// <summary>
        /// 读取设置的文本
        /// </summary>
        /// <param name="file_name">文件名，不含拓展名</param>
        /// <returns></returns>
        public static string ReadText(string file_name)
        {
            string folder = System.Threading.Thread.GetDomain().BaseDirectory + "config\\";
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            string wl_path = folder + file_name;
            if (!File.Exists(wl_path)) return "";
            return File.ReadAllText(wl_path).Trim();
        }

        /// <summary>
        /// 判断配置文件是否存在
        /// </summary>
        /// <param name="file_name"></param>
        /// <returns></returns>
        public static bool Exists(string file_name)
        {
            string folder = System.Threading.Thread.GetDomain().BaseDirectory + "config\\";
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            string wl_path = folder + file_name;
            return File.Exists(wl_path);
        }

        /// <summary>
        /// 删除配置文件
        /// </summary>
        /// <param name="file_name"></param>
        /// <returns></returns>
        public static bool Delete(string file_name)
        {
            string folder = System.Threading.Thread.GetDomain().BaseDirectory + "config\\";
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            string wl_path = folder + file_name;
            File.Delete(wl_path);
            return true;
        }
    }
}
