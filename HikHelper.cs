using System;
using System.Text;

namespace HikDeviceApi
{
    /// <summary>
    /// 版 本:Release
    /// 日 期:2015-08-15
    /// 作 者:痞子少爷
    /// 描 述:海康设备操作辅助方法
    /// </summary>
    public class HikHelper
    {
        /// <summary>
        /// 从字符串获得指定长度的byte数组
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="Length">返回长度</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public byte[] GetByteFromString(string s, int Length, Encoding encoding)
        {
            byte[] temp = encoding.GetBytes(s);
            byte[] ret = new byte[Length];
            if (temp.Length > Length)
                Array.Copy(temp, ret, Length);
            else
                Array.Copy(temp, ret, temp.Length);
            ret[Length - 1] = 0;
            return ret;
        }
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {
                return "0";
            }
        }
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="leng">转换的长度</param>
        /// <returns>返回16进制字符串，</returns>
        public string ByteToHexStr(byte[] bytes, int leng)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < leng; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
    }
}
