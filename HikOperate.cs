using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static HikDeviceApi.HikDelegate;
using static HikDeviceApi.HikEnum;
using static HikDeviceApi.HikStruct;

namespace HikDeviceApi
{
    /// <summary>
    /// 日 期:2015-11-10
    /// 作 者:痞子少爷
    /// 描 述:海康设备公用接口代理
    /// </summary>
    public static class HikOperate
    {
        #region 初始化SDK
        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <returns>true：成功，false：失败</returns>
        public static bool InitSDK()
        {
            return HikApi.NET_DVR_Init();
        }
        #endregion

        #region 反初始化
        /// <summary>
        /// 反初始化SDk
        /// </summary>
        /// <returns>true：成功，false：失败</returns>
        public static bool CleanupSDk()
        {
            try
            {
                return HikApi.NET_DVR_Cleanup();
            }
            catch (Exception) { }
            return false;
        }
        #endregion

        #region  登录登出
        /// <summary>
        /// 登录设备
        /// </summary>
        /// <param name="deviceIp">设备IP</param>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">用户密码</param>
        /// <param name="deviceInfo">设备参数结构体（输出）</param>
        /// <param name="userId">用户ID（输出）</param>
        /// <param name="port">通讯端口</param>
        /// <returns>成功返回true，否则失败</returns>
        public static bool LoginBySynLogin_V40(string deviceIp,string userName,string userPwd, ref NET_DVR_DEVICEINFO_V40 deviceInfo,ref int userId,uint port=8000)
        {
            try
            {
                NET_DVR_USER_LOGIN_INFO loginInfo = new NET_DVR_USER_LOGIN_INFO();
                loginInfo.bUseAsynLogin = false;
                loginInfo.sUserName = userName;
                loginInfo.sPassword = userPwd;
                loginInfo.wPort = port;
                loginInfo.pUser = IntPtr.Zero;
                loginInfo.sDeviceAddress = deviceIp;
                int x = HikApi.NET_DVR_Login_V40(loginInfo, ref deviceInfo);
                if (x == -1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        ///  登录设备
        /// </summary>
        /// <param name="deviceIp">设备ip</param>
        /// <param name="userName">用户账号</param>
        /// <param name="userPwd">用户密码</param>
        /// <param name="userId">输出用户id</param>
        /// <param name="port">通讯端口</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool LoginDevice(string deviceIp, string userName, string userPwd, ref int userId, short port = 8000)
        {
            if (deviceIp == "" || port < 0 || userName == "" || userPwd == "")
            {
                return false;
            }
            string DVRIPAddress = deviceIp;
            Int16 DVRPortNumber = port;
            string DVRUserName = userName;
            string DVRPassword = userPwd;

            //登录设备 Login the device
            NET_DVR_DEVICEINFO_V30 m_struDeviceInfo = new NET_DVR_DEVICEINFO_V30();
            userId = HikApi.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref m_struDeviceInfo);
            if (userId == -1)
            {
                //iLastErr = NET_DVR_GetLastError();
                //strErr = "NET_DVR_Login_V30 failed, error code= " + iLastErr;
                //登录失败，输出错误号 Failed to login and output the error code
                return false;
            }
            return true;

        }
        /// <summary>
        /// 登出设备
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>true:成功，false:失败</returns>
        public static bool LoginOut(ref int userId)
        {
            bool b = HikApi.NET_DVR_Logout(userId);
            if(b) userId = -1;
            return b;
        }
        #endregion

        #region 设置重连 
        /// <summary>
        /// 设置连接时间
        /// </summary>
        /// <param name="dwWaitTime">超时时间，单位毫秒，取值范围[300,75000]，实际最大超时时间因系统的connect超时时间而不同</param>
        /// <param name="dwTryTimes">连接尝试次数（保留）</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        public static bool SetConnectTime(uint dwWaitTime = 1000, uint dwTryTimes = 5)
        {
            return HikApi.NET_DVR_SetConnectTime(dwWaitTime, dwTryTimes);
        }
        /// <summary>
        /// 设置重连时间
        /// </summary>
        /// <param name="dwInterval">重连间隔，单位:毫秒</param>
        /// <param name="bEnableRecon">是否重连，0-不重连，1-重连，参数默认值为1</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        public static bool SetReconnectTime(uint dwInterval = 3000, int bEnableRecon = 1)
        {
            return HikApi.NET_DVR_SetReconnect(dwInterval, bEnableRecon);
        }
        #endregion

        #region  设置接收超时时间 
        /// <summary>
        /// 设置接收超时时间
        /// </summary>
        /// <param name="nRecvTimeOut">接收超时时间，单位毫秒，默认为5000，最小为1000毫秒。</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        public static bool SetRecvTimeOut(uint nRecvTimeOut = 5000)
        {
            return HikApi.NET_DVR_SetRecvTimeOut(nRecvTimeOut);
        }
        #endregion

        #region 使用日志保存
        /// <summary>
        /// 使用日志保存
        /// </summary>
        /// <param name="strLogDir">日志文件的路径，windows默认值为"C:\\SdkLog\\"；linux默认值"/home/sdklog/"</param>
        /// <param name="bLogEnable">志的等级（默认为0）：0-表示关闭日志，1-表示只输出ERROR错误日志，2-输出ERROR错误信息和DEBUG调试信息，3-输出ERROR错误信息、DEBUG调试信息和INFO普通信息等所有信息 </param>
        /// <param name="bAutoDel">是否删除超出的文件数，默认值为TRUE </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public static bool UseLog(string strLogDir= "C:\\SdkLog\\", int bLogEnable = 1, bool bAutoDel = true)
        {
            return HikApi.NET_DVR_SetLogToFile(bLogEnable, strLogDir, bAutoDel);
        }
        #endregion

        #region 获取最后的错误码 
        /// <summary>
        /// 获取最后的错误码
        /// </summary>
        /// <returns>返回错误码</returns>
        public static int GetLastError()
        {
            return (int)HikApi.NET_DVR_GetLastError();
        }
        /// <summary>
        /// 获取最后的错误信息
        /// </summary>
        /// <param name="pErrorNo"> 错误码数值</param>
        /// <returns>返回错误码</returns>
        public static string GetErrorMsg(ref int pErrorNo)
        {
            return HikApi.NET_DVR_GetErrorMsg(ref pErrorNo);
        }
        #endregion

        #region  设备时间 
        /// <summary>
        /// 获取设备时间
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回设备时间字符串</returns>
        /// <remarks>获取失败返回空</remarks>
        public static string GetDeviceTime(int userId)
        {
            NET_DVR_TIME deviceTime = new NET_DVR_TIME();
            uint dwSize = (uint)Marshal.SizeOf(deviceTime);

            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(deviceTime, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(userId, (uint)DwCommand.NET_DVR_GET_TIMECFG, 0, ptrIpParaCfgV40, dwSize, ref dwReturn);
            if (b)
            {
                deviceTime = (NET_DVR_TIME)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(NET_DVR_TIME));
                return string.Format("{0}-{1}-{2} {3}:{4}:{5}", deviceTime.dwYear, deviceTime.dwMonth, deviceTime.dwDay, deviceTime.dwHour, deviceTime.dwMinute, deviceTime.dwSecond);
            }
            else
            {
                return "";
            }
        }
        
        /// <summary>
        /// 设置设备时间
        /// </summary>
        /// <param name="userId">登陆设备时的用户ID</param>
        /// <param name="time">时间</param>
        /// <returns>成功返回true，否则失败</returns>
        public static bool SetDeviceDate(int userId, DateTime time)
        {

            NET_DVR_TIME deviceTime = new NET_DVR_TIME { dwDay = (uint)time.Day, dwHour = (uint)time.Hour, dwMinute = (uint)time.Minute, dwSecond = (uint)time.Second, dwMonth = (uint)time.Month, dwYear = (uint)time.Year };
            uint dwSize = (uint)Marshal.SizeOf(deviceTime);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(deviceTime, ptrIpParaCfgV40, false);
            return HikApi.NET_DVR_SetDVRConfig(userId, (uint)DwCommand.NET_DVR_SET_TIMECFG, 0, ptrIpParaCfgV40, dwSize);
        }
        #endregion

        #region 获取第一张网卡ip
        /// <summary>
        /// 获取第一张网卡的IP
        /// </summary>
        /// <returns>返回ip地址</returns>
        public static string GetFirstIp()
        {
            byte[] strIP = new byte[16 * 16];
            uint dwValidNum = 0;
            Boolean bEnableBind = false;

            //获取本地PC网卡IP信息
            if (HikApi.NET_DVR_GetLocalIP(strIP, ref dwValidNum, ref bEnableBind))
            {
                if (dwValidNum > 0)
                {
                    //取第一张网卡的IP地址为默认监听端口
                    return System.Text.Encoding.UTF8.GetString(strIP, 0, 16);
                }

            }
            return "";
        }
        #endregion

        #region 设置设备的配置信息 
        /// <summary>
        /// 设置设备的配置信息
        /// </summary>
        /// <param name="userId">登录设备的ID</param>
        /// <param name="dwCommand">设备配置命令 获取设备参数(扩展)-1100,获取时间参数-118</param>
        /// <param name="lChannel">通道号，不同的命令对应不同的取值，如果该参数无效则置为0xFFFFFFFF即可</param>
        /// <param name="lpInBuffer">接收数据的缓冲指针 </param>
        /// <param name="dwInBufferSize">接收数据的缓冲长度(以字节为单位)，不能为0</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        public static bool SetDVRConfig(int userId, uint dwCommand, int lChannel, IntPtr lpInBuffer, uint dwInBufferSize)
        {
            return HikApi.NET_DVR_SetDVRConfig(userId, dwCommand, lChannel, lpInBuffer, dwInBufferSize);
        }
        #endregion

        #region 获取设备的配置信息 
        /// <summary>
        /// 获取设备的配置信息
        /// </summary>
        /// <param name="userId">登录设备的ID</param>
        /// <param name="dwCommand">设备配置命令 获取设备参数(扩展)-1100,获取时间参数-118</param>
        /// <param name="lChannel">通道号，不同的命令对应不同的取值，如果该参数无效则置为0xFFFFFFFF即可</param>
        /// <param name="lpOutBuffer">接收数据的缓冲指针 </param>
        /// <param name="dwOutBufferSize">接收数据的缓冲长度(以字节为单位)，不能为0</param>
        /// <param name="lpBytesReturned">实际收到的数据长度指针，不能为NULL </param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        public static bool GetDVRConfig(int userId, uint dwCommand, int lChannel, IntPtr lpOutBuffer, uint dwOutBufferSize, ref uint lpBytesReturned)
        {
            return HikApi.NET_DVR_GetDVRConfig(userId, dwCommand, lChannel, lpOutBuffer, dwOutBufferSize, ref lpBytesReturned);
        }
        #endregion

        #region  重启设备 
        /// <summary>
        /// 重启设备
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>成功返回true，否则失败</returns>
        public static bool ReStartPower(int userId)
        {
            return HikApi.NET_DVR_RebootDVR(userId);
        }
        #endregion
      
        #region 设备状态
        
        /// <summary>
        /// 检查设备是否在线
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>接口返回TRUE表示在线，FALSE表示与设备通信失败或者返回错误状态</returns>
        public static bool CheckIsOnline(int userId)
        {
            return HikApi.NET_DVR_RemoteControl(userId, (uint)RemoteCommand.NET_DVR_CHECK_USER_STATUS, IntPtr.Zero, 0);
        }

        #endregion

        #region 注册回调函数
        /// <summary>
        /// 注册回调函数，接收设备报警消息等。
        /// </summary>
        /// <param name="alarmCallBack">回调函数委托</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public static bool SetDVRMessageCallBack_V30(MSGCallBack alarmCallBack)
        {
            if (alarmCallBack == null)
                return false;
            return HikApi.NET_DVR_SetDVRMessageCallBack_V30(alarmCallBack, IntPtr.Zero);
        }
        /// <summary>
        /// 注册回调函数，接收设备报警消息等。
        /// </summary>
        /// <param name="alarmCallBackV31">回调函数委托</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public static bool SetDVRMessageCallBack_V31(MSGCallBack_V31 alarmCallBackV31)
        {
            if (alarmCallBackV31 == null)
                return false;
            return HikApi.NET_DVR_SetDVRMessageCallBack_V31(alarmCallBackV31, IntPtr.Zero);
        }
        /// <summary>
        /// 注册回调函数，接收设备报警消息等。
        /// </summary>
        /// <param name="index">回调函数索引，取值范围：[0,15] </param>
        /// <param name="alarmCallBack">回调函数委托</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public static bool SetDVRMessageCallBack_V50(int index,MSGCallBack alarmCallBack)
        {
            if (alarmCallBack == null)
                return false;
            return HikApi.NET_DVR_SetDVRMessageCallBack_V50(index,alarmCallBack, IntPtr.Zero);
        }
        /// <summary>
        /// 注册接收异常、重连等消息的窗口句柄或回调函数。
        /// </summary>
        /// <param name="nMessage">消息</param>
        /// <param name="hWnd">接收异常信息消息的窗口句柄</param>
        /// <param name="pUser">用户数据</param>
        /// <param name="exceptionCallBack">回调函数委托</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        public static bool SetExceptionCallBack_V30(int nMessage, IntPtr hWnd, IntPtr pUser, fExceptionCallBack exceptionCallBack)
        {
            if (exceptionCallBack == null)
                return false;
            return HikApi.NET_DVR_SetExceptionCallBack_V30(nMessage, hWnd, exceptionCallBack, pUser);
        }

        #endregion

        #region  建立报警上传通道 
        /// <summary>
        /// 建立报警上传通道
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="closeAlarmId">返回撤销报警上传通道ID</param>
        /// <param name="level">布防优先级：0- 一等级（高），1- 二等级（中），2- 三等级（低，保留）</param>
        /// <returns>成功返回true，否则失败</returns>
        /// <remarks>执行前请先调用SetDVRMessageCallBackV31函数</remarks>
        public static bool SetupAlarmChan_V41(int userId, ref int closeAlarmId, byte level = 1)
        {

            NET_DVR_SETUPALARM_PARAM struAlarmParam = new NET_DVR_SETUPALARM_PARAM();
            struAlarmParam.dwSize = (uint)Marshal.SizeOf(struAlarmParam);
            struAlarmParam.byAlarmInfoType = 1;//1智能交通设备有效
            struAlarmParam.byLevel = level; //0- 一级布防,1- 二级布防
            closeAlarmId = HikApi.NET_DVR_SetupAlarmChan_V41(userId, ref struAlarmParam);
            if (closeAlarmId < 0)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 关闭报警上传通道
        /// </summary>
        /// <param name="setupAlarmId">报警上传通道ID</param>
        /// <returns>true:成功，false:失败</returns>
        public static bool CloseAlarmChan_V30(int setupAlarmId)
        {
            try
            {
                bool b = HikApi.NET_DVR_CloseAlarmChan_V30(setupAlarmId);
                return b;
            }
            catch (Exception) { return false; }
        }
        #endregion

        /// <summary>
        /// 获取设备支持协议列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="list">输出协议列表</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public static  bool GetIPCList(int userId,ref  NET_DVR_IPC_PROTO_LIST  list)
        {
            return HikApi.NET_DVR_GetIPCProtoList(userId, ref list);
        }
    }
}
