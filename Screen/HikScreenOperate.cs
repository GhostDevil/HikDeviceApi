using System;
using static HikDeviceApi.VideoRecorder.HikVideoStruct;
using System.Runtime.InteropServices;
using static HikDeviceApi.VideoRecorder.HikVideoEnum;

using static HikDeviceApi.HikStruct;
using System.Collections.Generic;
using static HikDeviceApi.Screen.HikScreenEnum;

namespace HikDeviceApi.Screen
{
    /// <summary>
    /// 日 期:2016-01-25
    /// 作 者:痞子少爷
    /// 描 述:海康拼接屏控制操作代理
    /// </summary>
    public class HikScreenOperate
    {
        public const int MAX_COUNT= 256;

        #region 登出设备

        /// <summary>
        /// 登出设备
        /// </summary>
        /// <param name="info">所有操作使用的UseInfo对象</param>
        /// <returns>true：操作成功，false：操作失败</returns>
        public bool Logout(ref DvrUseInfo info)
        {
            bool b = false;
            //注销登录 Logout the device
            if (info.UserId >= 0)
            {
                b = HikApi.NET_DVR_Logout(info.UserId);
                info.UserId = -1;
            }
            return b;
        }
        #endregion

        #region 登录设备
        /// <summary>
        /// 登录设备
        /// </summary>
        /// <param name="info">登录设备所需的信息</param>
        /// <returns>true：成功，false：失败</returns>
        public bool Login(ref DvrUseInfo info)
        {
            bool b = false;   
            if (info.UserId <=-1)
            {
                string DVRIPAddress = info.DvrIp; //设备IP地址或者域名
                short DVRPortNumber = short.Parse(info.DvrPoint.ToString());//设备服务端口号
                string DVRUserName = info.UserName;//设备登录用户名
                string DVRPassword =info.UserPwd;//设备登录密码
                NET_DVR_DEVICEINFO_V30 DeviceInfo = new NET_DVR_DEVICEINFO_V30();
                //登录设备 Login the device
                info.UserId = HikApi.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (info.UserId < 0)
                    b= false;
                else
                    b= true;
            }
            return b;
        }
        #endregion

        /// <summary>
        /// 获取屏幕拼接参数
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="config">大屏拼接参数结构体</param>
        /// <param name="screenNo">大屏序号</param>
        /// <returns>成功：true，失败：false</returns>
        public bool GetScreenConfig(int userId, ref HikScreenStruct.NET_DVR_SCREEN_SPLICE_CFG config, int screenNo = 0)
        {
            uint dwSize = (uint)Marshal.SizeOf(config);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(config, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            return HikApi.NET_DVR_GetDVRConfig(userId, (int)9039, screenNo, ptrIpParaCfgV40, dwSize, ref dwReturn);
        }
        /// <summary>
        /// 获取屏幕拼接参数
        /// </summary>
        /// <param name="userId">注册设备的ID</param>
        /// <param name="config">大屏拼接参数结构体</param>
        /// <param name="screenNo">大屏序号</param>
        /// <returns>成功：true，失败：false</returns>
        public bool GetScreenDeviceConfig(int userId, ref HikScreenStruct.NET_DVR_SINGLEWALLPARAM config, int screenNo = 0)
        {
            uint dwSize = 4 + MAX_COUNT * (uint)Marshal.SizeOf(config);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(config, ptrIpParaCfgV40, false);
       
            return HikApi.NET_DVR_GetDeviceConfig(userId, (int)9002, 0xffffffff,IntPtr.Zero, 0,IntPtr.Zero, ptrIpParaCfgV40, dwSize);
        }

        /// <summary>
        /// 远程控制
        /// </summary>
        /// <param name="useInfo">登录设备时的UseInfo对象</param>
        /// <param name="control">控制类型</param>
        /// <param name="lpInBuffer">窗口号或者解码文件信息</param>
        /// <returns>成功：true，失败：false</returns>
        public bool RemoteControl(ref DvrUseInfo useInfo, RemoteControl control, IntPtr lpInBuffer)
        {

            return HikApi.NET_DVR_RemoteControl(useInfo.UserId, (uint)control, lpInBuffer, 0);
        }

    }
}
