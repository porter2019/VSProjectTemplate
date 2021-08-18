using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    /// <summary>
    /// 拓展类
    /// </summary>
    public static class ExtensionsHelper
    {
        #region Object

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue == null) return 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static long ObjToLong(this object thisValue)
        {
            long reval = 0;
            if (thisValue == null) return 0;
            if (thisValue != null && thisValue != DBNull.Value && long.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue, int errorValue)
        {
            int reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static long ObjToLong(this object thisValue, int errorValue)
        {
            long reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && long.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static double ObjToDouble(this object thisValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static double ObjToDouble(this object thisValue, double errorValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string ObjToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static string ObjToString(this object thisValue, string errorValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return errorValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static Decimal ObjToDecimal(this object thisValue)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static Decimal ObjToDecimal(this object thisValue, decimal errorValue)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }



        /// <summary>
        /// 转化为小数
        /// </summary>
        /// <param name="thisValue">被转化值</param>
        /// <param name="digits">小数位数(默认两位)</param>
        /// <returns></returns>
        public static string ObjToDecimalString(this object thisValue, int digits = 2)
        {
            Decimal reval = 0;
            string p = "0.";

            for (int i = 0; i < digits; i++)
            {
                p += "0";
            }

            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval.ToString(p);
            }
            return reval.ToString(p);
        }

        /// <summary>
        /// 转化为小数
        /// </summary>
        /// <param name="thisValue">被转化值</param>
        /// <param name="errorValue">转化失败，返回默认值</param>
        /// <param name="digits">小数位数(默认两位)</param>
        /// <returns></returns>
        public static string ObjToDecimalString(this object thisValue, string errorValue, int digits = 2)
        {
            Decimal reval = 0;
            string p = "0.";

            for (int i = 0; i < digits; i++)
            {
                p += "0";
            }

            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval.ToString(p);
            }
            return errorValue;
        }

        /// <summary>
        /// 转化为小数(千分位格式)
        /// </summary>
        /// <param name="thisValue">被转化值</param>
        /// <param name="digits">小数位数(默认两位)</param>
        /// <returns></returns>
        public static string ObjToThousString(this object thisValue, int digits = 2)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return String.Format("{0:N" + digits + "}", reval);
            }
            return String.Format("{0:N" + digits + "}", 0);
        }

        /// <summary>
        /// 转化为小数(千分位格式)
        /// </summary>
        /// <param name="thisValue">被转化值</param>
        /// <param name="errorValue">转化失败，返回默认值</param>
        /// <param name="digits">小数位数(默认两位)</param>
        /// <returns></returns>
        public static string ObjToThousString(this object thisValue, string errorValue, int digits = 2)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return String.Format("{0:N" + digits + "}", reval);
            }
            return errorValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static DateTime ObjToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                reval = Convert.ToDateTime(thisValue);
            }
            return reval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static DateTime ObjToDate(this object thisValue, DateTime errorValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool ObjToBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        /// <summary>
        /// 获取程序的运行目录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetProgramDirectory(this object obj)
        {
            return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        }

        #endregion

        #region String

        /// <summary>
        /// 正则表达式匹配截取字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regexExpression">正则表达式</param>
        /// <remarks>比如从"AAhelloBB"中取出"hello"，正则写为：AA(\\S*)BB</remarks>
        /// <returns></returns>
        public static string RegexMatchValue(this string s, string regexExpression)
        {
            if (regexExpression.IsNull()) return string.Empty;
            Regex regex = new Regex(regexExpression, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            return regex.IsMatch(s) ? regex.Match(s).Groups[1].ToString() : "";
        }

        /// <summary>
        /// 判断字符串是否为Null、空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNull(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 判断字符串是否不为Null、空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotNull(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 与字符串进行比较，忽略大小写
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string s, string value)
        {
            return s.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 首字母转小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstCharToLower(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string str = s.First().ToString().ToLower() + s.Substring(1);
            return str;
        }

        /// <summary>
        /// 首字母转大写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string str = s.First().ToString().ToUpper() + s.Substring(1);
            return str;
        }

        /// <summary>
        /// 字符串删除一个元素，一般用;或,分隔
        /// </summary>
        /// <param name="str"></param>
        /// <param name="element"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string RemoveElement(this string str, string element, string separator)
        {
            List<string> strList = new List<string>(str.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries));

            if (!String.IsNullOrEmpty(element))
            {
                foreach (var ele in element.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries))
                {
                    strList.RemoveAll(s => s.Equals(ele));
                }
            }

            return String.Join(separator, strList);
        }

        /// <summary>
        /// 字符串添加一个元素，一般用;或,分隔
        /// </summary>
        /// <param name="str"></param>
        /// <param name="element"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string AddElement(this string str, string element, string separator)
        {
            List<string> strList = new List<string>(str.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries));

            if (!String.IsNullOrEmpty(element))
            {
                foreach (var ele in element.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!strList.Contains(ele))
                    {
                        strList.Add(ele);
                    }
                }
            }

            return String.Join(separator, strList);
        }

        /// <summary>
        /// 过滤不安全SQL关键字，包括（'|and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterSql(this string str)
        {
            string str2 = str;

            if (!String.IsNullOrEmpty(str2))
            {
                str2.Replace("'", "''");

                string words = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join";
                foreach (string w in words.Split('|'))
                {
                    if (str2.ToLower().IndexOf(w + " ") > -1)
                    {
                        str2 = new Regex(w + " ", RegexOptions.IgnoreCase).Replace(str2, "");
                    }
                    if (str2.ToLower().IndexOf(" " + w) > -1)
                    {
                        str2 = new Regex(" " + w, RegexOptions.IgnoreCase).Replace(str2, "");
                    }
                }
            }

            return str2;
        }

        /// <summary>
        /// 过滤不安全SQL关键字，仅（exec|insert|delete|update|master|truncate)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterSql2(this string str)
        {
            string str2 = str;

            if (!String.IsNullOrEmpty(str2))
            {
                //str2.Replace("'", "''");

                string words = "exec|insert|delete|update|truncate";
                foreach (string w in words.Split('|'))
                {
                    //update
                    str2 = new Regex("[\\s|(|;]" + w + "\\s", RegexOptions.IgnoreCase).Replace(str2, "");
                    //if (str2.ToLower().IndexOf(w + " ") > -1) {
                    //	str2 = new Regex(w + " ", RegexOptions.IgnoreCase).Replace(str2, "");
                    //}
                    //if (str2.ToLower().IndexOf(" " + w) > -1) {
                    //	str2 = new Regex(" " + w, RegexOptions.IgnoreCase).Replace(str2, "");
                    //}
                }
            }

            return str2;
        }

        /// <summary>
        /// 按字节数截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStringByte(this string str, int start, int length)
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(str))
            {
                int intLen = 0;

                ASCIIEncoding ascii = new ASCIIEncoding();
                byte[] bytes = ascii.GetBytes(str);
                for (int i = 0; i < bytes.Length; i++)
                {
                    if ((int)bytes[i] == 63)
                    {
                        intLen += 2;
                    }
                    else
                    {
                        intLen += 1;
                    }

                    if (intLen > length)
                    {
                        sb.Append(str.Substring(0, i));
                        break;
                    }
                }

            }

            return sb.ToString();
        }

        /// <summary>
        /// 返回字符串字节数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ByteLength(this string str)
        {
            int intLen = 0;

            if (!String.IsNullOrEmpty(str))
            {
                intLen = Encoding.Default.GetBytes(str).Length;
            }

            return intLen;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="splitStr">分隔符</param>
        /// <returns></returns>
        public static string[] SplitString(this string sourceStr, string splitStr)
        {
            if (string.IsNullOrEmpty(sourceStr) || string.IsNullOrEmpty(splitStr))
                return new string[0] { };

            if (sourceStr.IndexOf(splitStr) == -1)
                return new string[] { sourceStr };

            if (splitStr.Length == 1)
                return sourceStr.Split(splitStr[0], StringSplitOptions.RemoveEmptyEntries);
            else
                return Regex.Split(sourceStr, Regex.Escape(splitStr), RegexOptions.IgnoreCase);

        }

        /// <summary>
        ///  使用“;”号分割字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <returns></returns>
        public static string[] SplitWithSemicolon(this string sourceStr)
        {
            return SplitString(sourceStr, ";");
        }

        /// <summary>
        ///  使用“,”号分割字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <returns></returns>
        public static string[] SplitWithComma(this string sourceStr)
        {
            return SplitString(sourceStr, ",");
        }

        /// <summary>
        /// 将没有分隔符的时间字符串格式化为时间
        /// </summary>
        /// <param name="sourceStr">20210125193055/20210125/193055</param>
        /// <param name="errorValue">转换失败返回的默认值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string sourceStr, DateTime errorValue)
        {
            if (sourceStr.Length == 6)
            {
                //193055  只有时间
                return (DateTime.Now.ToString("yyyy-MM-dd") + " " + sourceStr.Insert(2, ":").Insert(5, ":")).ObjToDate();
            }
            else if (sourceStr.Length == 8)
            {
                //20210125
                return sourceStr.Insert(4, "-").Insert(7, "-").ObjToDate();
            }
            else if (sourceStr.Length == 14)
            {
                //20210125193055
                return sourceStr.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":").ObjToDate();
            }
            else
            {
                var dt = sourceStr.ObjToDate();
                if (dt.IsDaylightSavingTime()) return dt;
                else return errorValue;
            }
        }

        #endregion

        #region Decimal

        /// <summary>
        /// 将数值向上取整
        /// </summary>
        /// <param name="toRound">原数值</param>
        /// <param name="multiple">倍数(基数10)</param>
        /// <returns></returns>
        public static decimal ToRoundUp(this decimal toRound, int multiple)
        {
            var num = 10 * multiple;
            return (num - toRound % num) + toRound;
        }

        /// <summary>
        /// 将数值向下取整
        /// </summary>
        /// <param name="toRound">原数值</param>
        /// <param name="multiple">倍数(基数10)</param>
        /// <returns></returns>
        public static decimal ToRoundDown(this decimal toRound, int multiple)
        {
            var num = 10 * multiple;
            return toRound - toRound % num;
        }

        #endregion

        #region Int32

        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this int timeStamp)
        {
            var startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return startTime.AddSeconds(timeStamp);
        }

        #endregion

        #region long

        /// <summary>
        /// 时间戳转换为日期（时间戳单位毫秒）
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this long timeStamp)
        {
            var startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return startTime.AddMilliseconds(timeStamp);
        }

        #endregion

        #region TimeSpan

        /// <summary>
        /// 将格式化为：d天HH小时mm分钟
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static string Format(this TimeSpan ts)
        {
            StringBuilder sb = new StringBuilder();

            if (ts.Days > 0)
            {
                sb.AppendFormat("{0}天", ts.Days);
            }
            if (ts.Hours > 0)
            {
                sb.AppendFormat("{0}小时", ts.Hours);
            }
            if (ts.Minutes > 0)
            {
                sb.AppendFormat("{0}分钟", ts.Minutes);
            }
            if (ts.Days == 0 && ts.Hours == 0 && ts.Minutes == 0)
            {
                sb.AppendFormat("<1分钟");
            }

            return sb.ToString();
        }

        /// <summary>
        /// TimeSpan 转DateTime
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this TimeSpan ts)
        {
            var startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return startTime.Add(ts);
        }

        #endregion

        #region DateTime

        /// <summary>
		/// 日期转换为短日期格式(yyyy-MM-dd)
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static DateTime ToShortDate(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }

        /// <summary>
		/// 返回该日期所在周的第一天（周一）
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static DateTime WeekBegin(this DateTime dt)
        {
            int padding = (dt.DayOfWeek == DayOfWeek.Sunday ? 7 : Convert.ToInt32(dt.DayOfWeek)) - 1;
            return dt.AddDays(0 - padding);
        }

        /// <summary>
        /// 返回该日期所在周的最后一天（周日）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime WeekEnd(this DateTime dt)
        {
            int padding = 7 - (dt.DayOfWeek == DayOfWeek.Sunday ? 7 : Convert.ToInt32(dt.DayOfWeek));
            return dt.AddDays(padding);
        }

        /// <summary>
        /// 返回该日期的周数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime dt)
        {
            CultureInfo ci = new CultureInfo("zh-CN");
            return ci.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>
        /// 返回该日期的季度(1~4)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int QuarterOfYear(this DateTime dt)
        {
            return (dt.Month - 1) / 3 + 1;
        }

        /// <summary>
        /// 返回该日期所在季度的第一天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime QuarterBegin(this DateTime dt)
        {
            int paddingMonths = (dt.Month - 1) % 3;
            int paddingDays = dt.Day - 1;
            return dt.AddDays(0 - paddingDays).AddMonths(0 - paddingMonths);
        }

        /// <summary>
        /// 返回该日期所在季度的最后一天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime QuarterEnd(this DateTime dt)
        {
            return QuarterBegin(dt).AddMonths(3).AddDays(-1);
        }

        /// <summary>
        /// 转时间戳(毫秒)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToTimeStampMilliSecond(this DateTime dt)
        {
            var startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Utc);
            return (new DateTimeOffset(dt).UtcTicks - startTime.Ticks) / 10000;
        }

        /// <summary>
        /// 转时间戳(精度:秒)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int ToTimeStamp(this DateTime dt)
        {
            var startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Utc);
            return (int)((new DateTimeOffset(dt).UtcTicks - startTime.Ticks) / 10000 / 1000);
        }

        /// <summary>
        /// 转为Cron表达式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToCron(this DateTime dt)
        {
            return dt.ToString("ss mm HH dd MM ? yyyy");
        }


        #endregion

        #region StringBuilder

        /// <summary>
        /// StringBuilder删除第一位字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static StringBuilder RemoveFristChar(this StringBuilder str)
        {
            if (str.Length > 0)
            {
                return str.Remove(0, 1);
            }
            return str;
        }

        /// <summary>
        /// StringBuilder删除最后一位字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static StringBuilder RemoveLastChar(this StringBuilder str)
        {
            if (str.Length > 0)
            {
                return str.Remove(str.Length - 1, 1);
            }
            return str;
        }

        #endregion

        #region List<T>

        /// <summary>
        /// 仿照JavaScript的join方法
        /// </summary>
        /// <param name="list"></param>
        /// <param name="separator">分隔符，默认','</param>
        /// <returns></returns>
        public static string Join(this List<string> list, char separator = ',')
        {
            if (list == null) return "";
            if (list.Count == 0) return "";
            return string.Join(separator, list);
        }

        /// <summary>
        /// 仿照JavaScript的join方法
        /// </summary>
        /// <param name="list"></param>
        /// <param name="separator">分隔符，默认','</param>
        /// <returns></returns>
        public static string Join(this List<int> list, char separator = ',')
        {
            if (list == null) return "";
            if (list.Count == 0) return "";
            return string.Join(separator, list);
        }

        #endregion

        #region HTML

        /// <summary>
        /// 去掉HTML标记
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StripHtml(this string str)
        {
            string str2 = str;

            if (!String.IsNullOrEmpty(str2))
            {
                Regex regex = new Regex(@"<[^>]+>|</[^>]+>|&nbsp;");
                str2 = regex.Replace(str2, "");
            }

            return str2;
        }

        /// <summary>
        /// 获取html中纯文本
        /// </summary>
        /// <param name="html">html</param>
        /// <returns></returns>
        public static string GetHtmlText(this string html)
        {
            if (html.IsNull()) return "";
            html = Regex.Replace(html, @"<\/*[^<>]*>", "", RegexOptions.IgnoreCase);
            html = html.Replace("\r\n", "").Replace("\r", "").Replace("&nbsp;", "").Replace(" ", "");
            return html.UnTransferred();
        }

        /// <summary>
        /// 普通字符变换成转义字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string Transferred(this string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("'", "&apos;");
            str = str.Replace("\"", "&quot;");
            return str;
        }

        /// <summary>
        /// 转义字符变换成普通字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string UnTransferred(this string str)
        {
            str = str.Replace("&lt;", "<");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&apos;", "'");
            str = str.Replace("&quot;", "\"");
            str = str.Replace("&amp;", "&");
            str = str.Replace("&ldquo;", "“");
            str = str.Replace("&rdquo;", "”");

            return str;
        }

        #endregion
    }
}
