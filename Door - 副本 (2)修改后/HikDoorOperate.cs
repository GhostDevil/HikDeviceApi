using System;
using System.Text;
using System.Runtime.InteropServices;
using HikDeviceApi.PublicMethod;
using static HikDeviceApi.PublicMethod.HikPublicDelegate;
using static HikDeviceApi.PublicMethod.HikPublicStruct;
namespace HikDeviceApi.Door
{
    /// <summary>
    /// 版 本:Release
    /// 日 期:2015-08-15
    /// 作 者:逍遥
    /// 描 述:海康26系列门禁控制
    /// </summary>
    public class HikDoorOperate
    {

        #region  全局变量 
        ///// <summary>
        ///// 设备信息
        ///// </summary>
        //public HikDoorStruct.NET_DVR_DEVICEINFO_V40 deviceInfoV40;//设备信息
        /// <summary>
        /// 设备参数
        /// </summary>
        public NET_DVR_DEVICECFG_V40 deviceCfg;//设备参数
        ///// <summary>
        ///// 登录用户信息V40
        ///// </summary>
        //public HikDoorStruct.NET_DVR_USER_LOGIN_INFO loginInfo;
        /// <summary>
        /// 登录设备V30
        /// </summary>
        public NET_DVR_DEVICEINFO_V30 deviceInfoV30;
        /// <summary>
        /// 登录设备V40
        /// </summary>
        //public HikDoorStruct.NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40;
        /// <summary>
        /// 时间信息
        /// </summary>
        public NET_DVR_TIME deviceTime;//时间信息
        /// <summary>
        /// 门参数
        /// </summary>
        public HikDoorStruct.NET_DVR_DOOR_CFG doorStatus;//门参数
        /// <summary>
        /// 门禁主机工作状态
        /// </summary>
        public HikDoorStruct.NET_DVR_ACS_WORK_STATUS controllerStatus;//门禁主机工作状态
        /// <summary>
        /// 读卡器配置
        /// </summary>
        public HikDoorStruct.NET_DVR_CARD_READER_CFG cardReaderConfig;//读卡器配置
        /// <summary>
        /// 获取卡参数，发送数据结构对象
        /// </summary>
        public HikDoorStruct.NET_DVR_CARD_CFG_SEND_DATA cardCfgSendData;
        /// <summary>
        /// 卡参数配置条件
        /// </summary>
        public HikDoorStruct.NET_DVR_CARD_CFG_COND cardCfgCond;
        /// <summary>
        /// 卡参数配置结构
        /// </summary>
        public HikDoorStruct.NET_DVR_CARD_CFG cardConfig;
        #endregion

        #region  委托事件 
        /// <summary>
        /// 门禁操作事件委托
        /// </summary>
        /// <param name="result"></param>
        public delegate void ControlResult(bool result);
        /// <summary>
        /// 门禁操作事件
        /// </summary>
        public event ControlResult ResultMsg;
        /// <summary>
        /// 登录状态回调
        /// </summary>
        public event fLoginResultCallBack LoginResult;
        /// <summary>
        /// 监听回调
        /// </summary>
        public event MSGCallBackV31 ListenResult;
        /// <summary>
        /// 接收异常消息的回调函数，回调当前异常的相关信息
        /// </summary>
        public event fExceptionCallBack ExceptionCallBack;
        /// <summary>
        /// 远程配置回调函数
        /// </summary>
        event fRemoteConfigCallback RemoteConfigCallback;

        /// <summary>
        /// 卡片参数配置事件委托
        /// </summary>
        /// <param name="cardInfo"></param>
        public delegate void CardConfigInfo(HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_CFG cardInfo);
        /// <summary>
        /// 卡片参数配置
        /// </summary>
        public event CardConfigInfo CardInfo;
        #endregion

        #region  登录设备 
        /// <summary>
        /// 登录设备
        /// </summary>
        ///<param name="info">设备使用参数结构</param>
        ///<param name="isReConnect">是否执行重连设置</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool LoginV30(ref UseInfo info, bool isReConnect = true)
        {
            try
            {
                //userName = userId;
                //pwd = userPwd;
                //deviceIp = ipAddress;
                //devicePoint = port;

                //door.LoginResult += Door_LoginResult;
                //loginInfo = new HikDoorStruct.NET_DVR_USER_LOGIN_INFO()
                //{
                //    bUseAsynLogin = true,
                //    wPort = uint.Parse(devicePoint)

                //};

                //IntPtr pUser = new IntPtr();//用户数据
                //loginInfo.pUser = pUser;

                //byte[] address = Encoding.UTF8.GetBytes(deviceIp);
                //loginInfo.sDeviceAddress = new byte[64];
                //address.CopyTo(loginInfo.sDeviceAddress, 0);

                //byte[] byRes1 = Encoding.UTF8.GetBytes("0");
                //loginInfo.byRes1 = new byte[2];
                //byRes1.CopyTo(loginInfo.byRes1, 0);

                //byte[] byRes2 = Encoding.UTF8.GetBytes("0");
                //loginInfo.byRes2 = new byte[128];
                //byRes1.CopyTo(loginInfo.byRes2, 0);

                //byte[] byRes = Encoding.UTF8.GetBytes(userName);
                //loginInfo.sUserName = new byte[32];
                //byRes.CopyTo(loginInfo.sUserName, 0);

                //byte[] pd = Encoding.UTF8.GetBytes(pwd);
                //loginInfo.sPassword = new byte[16];
                //pd.CopyTo(loginInfo.sPassword, 0);


                //deviceInfoV40 = new HikDoorStruct.NET_DVR_DEVICEINFO_V40();
                //deviceInfoV40.struDeviceV30 = new HikDoorStruct.NET_DVR_DEVICEINFO_V30();//door.Login(loginInfo, ref deviceInfoV40)==-1)

                //userName = userId;
                //pwd = userPwd;
                //deviceIp = ipAddress;
                //devicePoint = port;
                info.UserId = HikPublicApi.NET_DVR_Login_V30(info.LoginDeviceIp, info.LoginDevicePoint, info.LoginUserName, info.LoginUserPwd, ref deviceInfoV30);

                if (info.UserId == -1)
                {
                    return false;
                }
                else
                {
                    if (isReConnect)
                    {
                        HikPublicApi.NET_DVR_SetConnectTime(info.WaitTime, info.TryTimes);
                        HikPublicApi.NET_DVR_SetReconnect(info.Interval, info.EnableRecon);
                    }

                    return true;
                }

            }
            catch (Exception) { return false; }
        }
        #endregion

        #region  登出设备 
        /// <summary>
        /// 登出设备
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <returns>true:成功，false:失败</returns>
        public bool LoginOut(ref UseInfo info)
        {
            bool b= HikPublicApi.NET_DVR_Logout(info.UserId);
            if(b)
                info.UserId = -1;
            return b;
        }
        #endregion

        #region 注册接收异常、重连等消息的窗口句柄或回调函数。 
        /// <summary>
        /// 注册接收异常、重连等消息的窗口句柄或回调函数。
        /// </summary>
        /// <param name="nMessage">消息</param>
        /// <param name="hWnd">接收异常信息消息的窗口句柄</param>
        /// <param name="pUser">用户数据</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        public bool SetExceptionCallBack_V30(int nMessage, IntPtr hWnd, IntPtr pUser)
        {
            if (ExceptionCallBack == null)
                return false;
            return HikPublicApi.NET_DVR_SetExceptionCallBack_V30(nMessage, hWnd, ExceptionCallBack, pUser);
        }
        #endregion

        #region 注册回调函数，接收设备报警消息等。 
        /// <summary>
        /// 注册回调函数，接收设备报警消息等。
        /// </summary>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        /// <remarks>只需设置一次回调函数即可，无需调用多次</remarks>
        public bool SetDVRMessageCallBack_V31()
        {
            if (ListenResult == null)
                return false;
            return HikPublicApi.NET_DVR_SetDVRMessageCallBack_V31(ListenResult, IntPtr.Zero);
        }
        #endregion

        #region  设置接收超时时间 
        /// <summary>
        /// 设置接收超时时间
        /// </summary>
        /// <param name="nRecvTimeOut">接收超时时间，单位毫秒，默认为5000，最小为1000毫秒。</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        public bool SetRecvTimeOut(uint nRecvTimeOut = 5000)
        {
            return HikPublicApi.NET_DVR_SetRecvTimeOut(nRecvTimeOut);
        }
        #endregion

        #region 获取错误码 
        /// <summary>
        /// 获取错误码
        /// </summary>
        /// <returns>返回错误码</returns>
        public int GetLastError()
        {
            return (int)HikPublicApi.NET_DVR_GetLastError();
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
        public bool SetDVRConfig(int userId,uint dwCommand, int lChannel, IntPtr lpInBuffer, uint dwInBufferSize)
        {
            return HikPublicApi.NET_DVR_SetDVRConfig(userId, dwCommand, lChannel, lpInBuffer, dwInBufferSize);
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
        public bool GetDVRConfig(int userId, uint dwCommand, int lChannel, IntPtr lpOutBuffer, uint dwOutBufferSize, ref uint lpBytesReturned)
        {
            return HikPublicApi.NET_DVR_GetDVRConfig(userId, dwCommand, lChannel, lpOutBuffer, dwOutBufferSize, ref lpBytesReturned);
        }
        #endregion

        #region 设置重连 
        /// <summary>
        /// 设置连接时间
        /// </summary>
        /// <param name="dwWaitTime">超时时间，单位毫秒，取值范围[300,75000]，实际最大超时时间因系统的connect超时时间而不同</param>
        /// <param name="dwTryTimes">连接尝试次数（保留）</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        public bool SetConnectTime(uint dwWaitTime = 2000, uint dwTryTimes = 3)
        {
            return HikPublicApi.NET_DVR_SetConnectTime(dwWaitTime, dwTryTimes);
        }
        /// <summary>
        /// 设置重连时间
        /// </summary>
        /// <param name="dwInterval">重连间隔，单位:毫秒</param>
        /// <param name="bEnableRecon">是否重连，0-不重连，1-重连，参数默认值为1</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        public bool SetReconnectTime(uint dwInterval = 5000, int bEnableRecon = 1)
        {
            return HikPublicApi.NET_DVR_SetReconnect(dwInterval, bEnableRecon);
        }
        #endregion

        #region  初始化SDK 
        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public static bool Init()
        {
            return HikPublicApi.NET_DVR_Init();
        }
        #endregion

        #region  释放SDK资源 
        /// <summary>
        /// 释放SDK资源
        /// </summary>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>在程序结束之前调用</remarks>
        public static bool Cleanup()
        {
            return HikPublicApi.NET_DVR_Cleanup();
        }
        #endregion

        #region  控制门禁 
        /// <summary>
        /// 控制门禁
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象</param>
        /// <param name="doorNo">门禁序号，从1开始，-1表示对所有门进行操作</param>
        /// <param name="con">控制类型</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool ControlGateway(UseInfo info, int doorNo, HikDeviceApi.Door.HikDoorEnum.DoorControl con)
        {
            return HikDoorApi.NET_DVR_ControlGateway(info.UserId, doorNo, (uint)con);
        }


        #endregion

        #region  获取主机工作状态 
        /// <summary>
        /// 获取主机工作状态
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象</param>
        /// <param name="doorStatus">1-休眠 2-常开 3-常闭 4-普通</param>
        /// <param name="lockStatus">1-休眠 2-刷卡+密码 3-刷卡</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool GetDeviceWorkStatus(UseInfo info, ref string doorStatus, ref string lockStatus)
        {
            uint dwSize = (uint)Marshal.SizeOf(controllerStatus);
            IntPtr deviceInfo = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(controllerStatus, deviceInfo, false);
            uint dwReturn = 0;
            bool b = HikPublicApi.NET_DVR_GetDVRConfig(info.UserId, (uint)HikDeviceApi.Door.HikDoorEnum.DwCommand.NET_DVR_GET_ACS_WORK_STATUS, 0, deviceInfo, dwSize, ref dwReturn);
            string ds = "";
            string ls = "";
            if (b)
            {
                controllerStatus = (HikDeviceApi.Door.HikDoorStruct.NET_DVR_ACS_WORK_STATUS)Marshal.PtrToStructure(deviceInfo, typeof(HikDeviceApi.Door.HikDoorStruct.NET_DVR_ACS_WORK_STATUS));


                switch (ConvertBase(byteToHexStr(controllerStatus.byDoorStatus, 1), 16, 10))
                {
                    case "1":
                        ds = "休眠状态";
                        break;
                    case "2":
                        ds = "常开状态";
                        break;
                    case "3":
                        ds = "常闭状态";
                        break;
                    case "4":
                        ds = "普通状态";
                        break;
                }

                switch (ConvertBase(byteToHexStr(controllerStatus.byCardReaderVerifyMode, 1), 16, 10))
                {
                    case "1":
                        ls = "休眠";
                        break;
                    case "2":
                        ls = "刷卡+密码";
                        break;
                    case "3":
                        ls = "刷卡";
                        break;

                }
                //skinWaterTextBox1.AppendText(string.Format("\r\n\r\n状态:门锁状态--{0}，门状态--{1}，门磁状态--{2}，读卡器状态--{3}，读卡器防拆--{4}，读卡器验证方式--{5}，已添加卡数量--{6}",
                //     door.ConvertBase(door.byteToHexStr(controllerStatus.byDoorLockStatus, 1), 16, 10) == "0" ? "关" : "开",
                //     doorStatus,
                //     door.ConvertBase(door.byteToHexStr(controllerStatus.byMagneticStatus, 1), 16, 10) == "0" ? "闭合" : "开启",
                //     door.ConvertBase(door.byteToHexStr(controllerStatus.byCardReaderOnlineStatus, 1), 16, 10) == "0" ? "不在线" : "在线",
                //     door.ConvertBase(door.byteToHexStr(controllerStatus.byCardReaderAntiDismantleStatus, 1), 16, 10) == "0" ? "关闭" : "开启",
                //     lockStatus,
                //     controllerStatus.dwCardNum.ToString()));
            }
            else
            {

               // int iLastErr = GetLastError();
                //skinWaterTextBox1.AppendText("\r\n\r\n查询失败");
            }
            doorStatus = ds;
            lockStatus = ls;
            return b;
        }
        #endregion

        #region  监视门禁动态 
        /// <summary>
        /// 监视门禁动态
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象</param>
        /// <returns>成功返回true，否则失败</returns>
        /// <remarks>执行前请先调用SetDVRMessageCallBack_V31函数</remarks>
        public bool StartWatchDoorV30(ref UseInfo info)
        {
            //if (ListenResult == null)
            //    return false;
            //bool bb = HikDoorApi.NET_DVR_SetDVRMessageCallBack_V31(ListenResult, IntPtr.Zero);

            NET_DVR_SETUPALARM_PARAM struAlarmParam = new NET_DVR_SETUPALARM_PARAM();
            struAlarmParam.dwSize = (uint)Marshal.SizeOf(struAlarmParam);
            struAlarmParam.byAlarmInfoType = 1;//1智能交通设备有效
            info.AlarmId = HikPublicApi.NET_DVR_SetupAlarmChan_V30(info.UserId);
            if (info.AlarmId < 0)//door.SetupAlarmChan_V30()
            {
                //int iLastErr = GetLastError();
                return false;
            }
            //if (!door.SetupAlarmChan_V30())
            //{
            //    int iLastErr = door.GetLastError();
            //    return false;
            //}
            //int b = door.StartListenV30(localhostIP, short.Parse(devicePoint));
            //if (b == -1)
            //{
            //    int x = GetlastErrorNo();
            //    return false;
            //}
            return true;
        }
        ///// <summary>
        ///// 监视们门禁动态
        ///// </summary>
        ///// <param name="info">登录设备时的UseInfo对象</param>
        ///// <returns>成功返回true，否则失败</returns>
        ///// <remarks>执行前请先调用SetDVRMessageCallBack_V31函数</remarks>
        //public bool StartWatchDoorV30(ref UseInfo info)
        //{
        //    //if (ListenResult == null)
        //    //    return false;
        //    //bool bb = HikDoorApi.NET_DVR_SetDVRMessageCallBack_V31(ListenResult, IntPtr.Zero);

        //    HikDoorStruct.NET_DVR_SETUPALARM_PARAM struAlarmParam = new HikDoorStruct.NET_DVR_SETUPALARM_PARAM();
        //    struAlarmParam.dwSize = (uint)Marshal.SizeOf(struAlarmParam);
        //    struAlarmParam.byAlarmInfoType = 1;//1智能交通设备有效
        //    info.AlarmId = HikDoorApi.NET_DVR_SetupAlarmChan_V30(info.UserId);
        //    if (info.AlarmId == -1)
        //    {
        //        int iLastErr = GetLastError();
        //        return false;
        //    }
        //    return true;
        //}
        /// <summary>
        /// 监视门禁动态，接收设备主动上传的报警等信息（支持多线程）。
        /// </summary>
        /// <param name="listenId">返回监听Id</param>
        /// <param name="localhostIP">本机IP地址</param>
        /// <param name="listenPort">监听端口</param>
        /// <returns>成功返回true，否则失败</returns>
        /// <remarks>执行前请先调用SetDVRMessageCallBack_V31函数。必须将设备的网络配置中的“远程管理主机地址”或者“远程报警主机地址”设置成PC机的IP地址（与接口中的sLocalIP参数一致），“远程管理主机端口号”或者“远程报警主机端口号”设置成PC机的监听端口号（与接口中的wLocalPort参数一致）。</remarks>
        public bool StartListenV30(ref int listenId, string localhostIP,int listenPort=7200)
        {
            //if (ListenResult == null)
            //    return false;
            //bool bb = HikDoorApi.NET_DVR_SetDVRMessageCallBack_V31(ListenResult,IntPtr.Zero);
            NET_DVR_SETUPALARM_PARAM struAlarmParam = new NET_DVR_SETUPALARM_PARAM();
            struAlarmParam.dwSize = (uint)Marshal.SizeOf(struAlarmParam);
            struAlarmParam.byAlarmInfoType = 1;//1智能交通设备有效
            listenId = HikPublicApi.NET_DVR_StartListen_V30(localhostIP, (short)listenPort, ListenResult, IntPtr.Zero);
            if (listenId == -1)
            {
                //int x = GetlastErrorNo();
                return false;
            }
            return true;
        }
        #endregion

        #region  结束门禁监视 
        /// <summary>
        /// 结束门禁监视
        /// </summary>
        /// <param name="info">监视门禁动态返回的UseInfo对象</param>
        /// <returns>true:成功，false:失败</returns>
        public bool EndWatchDoorV30(ref UseInfo info)
        {
            try
            {
                bool b= HikPublicApi.NET_DVR_CloseAlarmChan_V30(info.AlarmId);
                if (b)
                {
                    info.AlarmId = -1;
                    ListenResult = null;
                }
                return b;
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        /// 结束门禁监视
        /// </summary>
        /// <param name="listenId">监听设备ID</param>
        /// <returns>true:成功，false:失败</returns> 
        public bool StopListenV30(ref int listenId)
        {
            try
            {
                bool b= HikPublicApi.NET_DVR_StopListen_V30(listenId);
                if (b)
                {
                    listenId = -1;
                    ListenResult = null;
                }
                return b;
            }
            catch (Exception) { return false; }
        }
        #endregion

        #region  回执监听 
        ////InfoStruct.DoorActualStaus actualStaus;
        //private void Door_ListenResult(int lCommand, ref HikDoorStruct.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        //{
        //    try
        //    {
        //        // HikDoorStruct.NET_DVR_ALARMER alarmer = pAlarmer;
        //        if (lCommand == 0x5002)
        //        {
        //            HikDoorStruct.NET_DVR_ACS_ALARM_INFO struAlarmInfoV30 = new HikDoorStruct.NET_DVR_ACS_ALARM_INFO();
        //            struAlarmInfoV30 = (HikDoorStruct.NET_DVR_ACS_ALARM_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(HikDoorStruct.NET_DVR_ACS_ALARM_INFO));

        //            //string name = "";
        //            //if (GetDoorInfo(struAlarmInfoV30.struAcsEventInfo.dwDoorNo))
        //            //    if (doorStatus.byDoorName != null)
        //            //        name = Encoding.UTF8.GetString(doorStatus.byDoorName).Trim('\0');
        //            if (ListenResult != null)
        //                ListenResult(lCommand, ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
        //            return;
        //            #region [数据处理]
        //            // if (struAlarmInfoV30.dwMajor == (int)HikDoorEnum.MajorType.MAJOR_EVENT)
        //            // {
        //            //     if (struAlarmInfoV30.dwMinor != (int)HikDoorEnum.MajorEvent.门锁打开 && struAlarmInfoV30.dwMinor != (int)HikDoorEnum.MajorEvent.门锁关闭)
        //            //     {
        //            //         string msg = string.Format("\r\n\r\n事件类型--{0}  事件时间--{1}  门禁编号--{2}  读卡器编号--{3}  卡号--{4}  卡片类型--{5}  操作类型--{6}",
        //            //         Enum.GetName(typeof(HikDoorEnum.MajorEvent), struAlarmInfoV30.dwMinor),
        //            //     string.Format("{0}-{1}-{2}  {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //            //     struAlarmInfoV30.struAcsEventInfo.dwDoorNo,
        //            //     struAlarmInfoV30.struAcsEventInfo.dwCardReaderNo,
        //            //     Encoding.UTF8.GetString(struAlarmInfoV30.struAcsEventInfo.byCardNo).Trim('\0'),
        //            //     Enum.GetName(typeof(HikDoorApi.CardType), struAlarmInfoV30.struAcsEventInfo.byCardType),
        //            //     "手动操作"
        //            //         );
        //            //     }
        //            //     else
        //            //     {
        //            //         string msg = string.Format("\r\n\r\n事件类型--{0}  事件时间--{1}  门禁编号--{2}",
        //            //         Enum.GetName(typeof(HikDoorEnum.MajorEvent), struAlarmInfoV30.dwMinor),
        //            //     string.Format("{0}-{1}-{2}  {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //            //     struAlarmInfoV30.struAcsEventInfo.dwDoorNo
        //            //         );

        //            //     }

        //            //     actualStaus = new InfoStruct.DoorActualStaus()
        //            //     {
        //            //         // string.Format("\r\n\r\n事件类型--{0}  事件时间--{1}  门禁编号--{2}  读卡器编号--{3}  卡号--{4}  卡片类型--{5}  操作类型--{6}",
        //            //         brushCardTime = string.Format("{0}-{1}-{2}  {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //            //         cardIdentify = long.Parse(Encoding.UTF8.GetString(struAlarmInfoV30.struAcsEventInfo.byCardNo).Trim('\0')),
        //            //         cusNumber = "1",
        //            //         inOutFlag = struAlarmInfoV30.struAcsEventInfo.dwCardReaderNo,
        //            //         msgType = InfoStruct.doorActualStaus,
        //            //         status = struAlarmInfoV30.dwMinor,
        //            //         remark = Enum.GetName(typeof(HikDoorEnum.MajorEvent), struAlarmInfoV30.dwMinor),
        //            //         address = Encoding.UTF8.GetString(doorStatus.byDoorName).Trim('\0'),
        //            //         doorControlIdentify = struAlarmInfoV30.struAcsEventInfo.dwCardReaderNo,
        //            //         doorIdentify = struAlarmInfoV30.struAcsEventInfo.dwDoorNo
        //            //     };
        //            //     bool b = msg.SendMessge(GhostHelper.JSONHelper.ObjectToJSON(actualStaus));
        //            // }
        //            // else if (struAlarmInfoV30.dwMajor == (int)HikDoorEnum.MajorType.MAJOR_OPERATION)
        //            // {
        //            //     if (struAlarmInfoV30.dwMinor == (int)HikDoorEnum.MajorOperation.远程关门 && struAlarmInfoV30.dwMinor != (int)HikDoorEnum.MajorOperation.远程开门 && struAlarmInfoV30.dwMinor != (int)HikDoorEnum.MajorOperation.远程常开 && struAlarmInfoV30.dwMinor != (int)HikDoorEnum.MajorOperation.远程常关)
        //            //     {
        //            //         string msg = string.Format("\r\n\r\n事件类型--{0}  事件时间--{1}  门禁编号--{2}操作类型--{3}",
        //            //         Enum.GetName(typeof(HikDoorEnum.MajorOperation), struAlarmInfoV30.dwMinor),
        //            //     string.Format("{0}-{1}-{2}  {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //            //     struAlarmInfoV30.struAcsEventInfo.dwDoorNo,
        //            //     "远程操作"

        //            //     );
        //            //     }
        //            //     else
        //            //     {
        //            //         string msg = string.Format("\r\n\r\n事件类型--{0}  事件时间--{1}  门禁编号--{2} ",
        //            //         Enum.GetName(typeof(HikDoorEnum.MajorOperation), struAlarmInfoV30.dwMinor),
        //            //     string.Format("{0}-{1}-{2}  {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //            //     struAlarmInfoV30.struAcsEventInfo.dwDoorNo
        //            //         );
        //            //     }
        //            // }
        //            // else if (struAlarmInfoV30.dwMajor == (int)HikDoorEnum.MajorType.MAJOR_EXCEPTION)
        //            // {

        //            //     string msg= string.Format("\r\n\r\n事件类型--{0}  事件时间--{1}  门禁编号--{2}  操作类型--{3}",
        //            //     Enum.GetName(typeof(HikDoorEnum.MajorException), struAlarmInfoV30.dwMinor),
        //            //string.Format("{0}-{1}-{2}  {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //            //struAlarmInfoV30.struAcsEventInfo.dwDoorNo,
        //            //"远程操作"
        //            //    );

        //            // }
        //            // else if (struAlarmInfoV30.dwMajor == (int)HikDoorEnum.MajorType.MAJOR_ALARM)
        //            // {
        //            //     string msg = string.Format("\r\n\r\n事件类型--{0}  事件时间--{1}  门禁编号--{2}  读卡器编号--{3}  卡号--{4}  卡片类型--{5}  操作类型--{6}",
        //            //     Enum.GetName(typeof(HikDoorEnum.MajorAlarm), struAlarmInfoV30.dwMinor),
        //            //     string.Format("{0}-{1}-{2}  {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //            //     struAlarmInfoV30.struAcsEventInfo.dwDoorNo,
        //            //     struAlarmInfoV30.struAcsEventInfo.dwCardReaderNo,
        //            //     Encoding.UTF8.GetString(struAlarmInfoV30.struAcsEventInfo.byCardNo).Trim('\0'),
        //            //     Enum.GetName(typeof(HikDoorApi.CardType), struAlarmInfoV30.struAcsEventInfo.byCardType),
        //            //     "手动操作"
        //            //         );
        //            // }
        //            #endregion
        //        }
        //    }
        //    catch { }
        //}
        #endregion

        #region  查询门禁信息 
        /// <summary>
        /// 查询门禁信息
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="chanel">门禁编号</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetDoorInfo(UseInfo info, int chanel)
        {

            uint dwSize = (uint)Marshal.SizeOf(doorStatus);

            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(doorStatus, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikPublicApi.NET_DVR_GetDVRConfig(info.UserId, (uint)HikDeviceApi.Door.HikDoorEnum.DwCommand.NET_DVR_GET_DOOR_CFG, chanel, ptrIpParaCfgV40, dwSize, ref dwReturn);
            if (b)
            {
                doorStatus = (HikDeviceApi.Door.HikDoorStruct.NET_DVR_DOOR_CFG)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDeviceApi.Door.HikDoorStruct.NET_DVR_DOOR_CFG));
                return true;
                //#region [数据处理]
                //string msg = string.Format("\r\n\r\n状态:  名称--{0}  通道--{1}  门磁类型--{2}  开门按钮类型--{3}  开门持续时间--{4}  胁迫密码--{5}  超级密码--{6}  闭门回锁--{7}  首卡常开--{8}",
                //Encoding.UTF8.GetString(doorStatus.byDoorName).Trim('\0'),
                //chanel,
                //doorStatus.byMagneticType.ToString() == "0" ? "常闭" : "常开",
                //doorStatus.byOpenButtonType.ToString() == "0" ? "常闭" : "常开",
                //doorStatus.byOpenDuration.ToString(),
                //Encoding.UTF8.GetString(doorStatus.byStressPassword).Trim('\0'),
                //Encoding.UTF8.GetString(doorStatus.bySuperPassword).Trim('\0'),
                //doorStatus.byEnableDoorLock.ToString() == "0" ? "否" : "是",
                //doorStatus.byEnableLeaderCard.ToString() == "0" ? "否" : "是");
                //#endregion
            }
            else
            {
                return false;
                //int iLastErr = GetLastError();
                //string msg = "\r\n\r\n状态获取失败";

            }
        }
        #endregion

        #region  获取设备时间 
        /// <summary>
        /// 获取设备时间
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <returns>返回设备时间字符串</returns>
        /// <remarks>获取失败返回空</remarks>
        public string GetDeviceTime(UseInfo info)
        {
            
            uint dwSize = (uint)Marshal.SizeOf(deviceTime);

            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(deviceTime, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikPublicApi.NET_DVR_GetDVRConfig(info.UserId, (uint)HikDeviceApi.Door.HikDoorEnum.DwCommand.NET_DVR_GET_TIMECFG, 0, ptrIpParaCfgV40, dwSize, ref dwReturn);
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
        #endregion

        #region  设置设备时间 
        /// <summary>
        /// 设置设备时间
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="time">时间</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetDeviceDate(UseInfo info, DateTime time)
        {

            deviceTime.dwDay = (uint)time.Day;
            deviceTime.dwHour = (uint)time.Hour;
            deviceTime.dwMinute = (uint)time.Minute;
            deviceTime.dwSecond = (uint)time.Second;
            deviceTime.dwMonth = (uint)time.Month;
            deviceTime.dwYear = (uint)time.Year;

            uint dwSize = (uint)Marshal.SizeOf(deviceTime);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(deviceTime, ptrIpParaCfgV40, false);
            return HikPublicApi.NET_DVR_SetDVRConfig(info.UserId, (uint)HikDeviceApi.Door.HikDoorEnum.DwCommand.NET_DVR_SET_TIMECFG, 0, ptrIpParaCfgV40, dwSize);
        }
        #endregion

        #region 获取设备信息 
        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <returns>返回产品型号以及名称</returns>
        /// <remarks>返回字符串：设备名称：{0},设备型号:{1} 或者 “查询失败”</remarks>
        public string GetDeviceInfo(UseInfo info)
        {
            uint dwSize = (uint)Marshal.SizeOf(deviceCfg);
            IntPtr deviceInfo = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(deviceCfg, deviceInfo, false);
            uint dwReturn = 0;
            bool b = HikPublicApi.NET_DVR_GetDVRConfig(info.UserId, (uint)HikDeviceApi.Door.HikDoorEnum.DwCommand.NET_DVR_GET_DEVICECFG_V40, 0, deviceInfo, dwSize, ref dwReturn);
            if (b)
            {
                deviceCfg = (NET_DVR_DEVICECFG_V40)Marshal.PtrToStructure(deviceInfo, typeof(NET_DVR_DEVICECFG_V40));
                string name = Encoding.UTF8.GetString(deviceCfg.sDVRName).TrimEnd('\0');
                string type = Encoding.UTF8.GetString(deviceCfg.byDevTypeName).TrimEnd('\0');
                return string.Format("设备名称：{0},设备型号:{1}", name, type);

            }
            else
            {
                //int iLastErr = GetLastError();
                return "查询失败";
            }
        }
        #endregion

        #region  重启设备 
        /// <summary>
        /// 重启设备
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool ReStartPower(UseInfo info)
        {
            return HikPublicApi.NET_DVR_RebootDVR(info.UserId);
        }
        #endregion

        #region  启动远程获取配置 
        /// <summary>
        /// 启动远程获取配置
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="type">操作类型，设置或者获取</param>
        /// <param name="cardNum">获取卡数量，默认获取所有</param>
        /// <returns>成功返回true，否则false</returns>
        /// <remarks>调用前先注册RemoteConfigCallback事件</remarks>
        public bool StartRemoteConfig(ref UseInfo info, HikDeviceApi.Door.HikDoorEnum.DwCommand type = HikDeviceApi.Door.HikDoorEnum.DwCommand.NET_DVR_GET_CARD_CFG, uint cardNum = 0xffffffff)
        {
            cardCfgCond = new HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_CFG_COND();
            cardCfgCond.dwSize = (uint)Marshal.SizeOf(cardCfgCond);
            cardCfgCond.dwCardNum = cardNum; //0xffffffff;

            Int32 dwSize = Marshal.SizeOf(cardCfgCond);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal(dwSize);
            Marshal.StructureToPtr(cardCfgCond, ptrIpParaCfgV40, false);


            if (RemoteConfigCallback == null)
                return false;
            info.RemoteId = HikDeviceApi.Door.HikDoorApi.NET_DVR_StartRemoteConfig(info.UserId, (uint)type, ptrIpParaCfgV40, (uint)dwSize, RemoteConfigCallback, new IntPtr(0));
            if (info.RemoteId < 0)
                return false;
            else
            {
                //RemoteConfigCallback += Door_RemoteConfigCallback;
                return true;
            }

        }

        /// <summary>
        /// 启动远程配置
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="dwCommand">配置命令，不同的功能对应不同的命令号(dwCommand)，lpInBuffer等参数也对应不同的内容</param>
        /// <param name="lpInBuffer">输入参数，具体内容跟配置命令相关</param>
        /// <param name="dwInBufferLen">输入缓冲的大小</param>
        /// <param name="pUserData">用户数据</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_SendRemoteConfig等接口的句柄</returns>
        /// <remarks>
        /// NET_DVR_GET_CARD_CFG获取卡参数时，在调用该接口启动长连接远程配置后，还需要调用NET_DVR_SendRemoteConfig发送查找条件数据(获取所有卡参数时不需要调用该发送接口)，查找结果在NET_DVR_StartRemoteConfig设置的回调函数中返回。 
        /// NET_DVR_SET_CARD_CFG设置卡参数时，在调用该接口启动长连接远程配置后，通过调用NET_DVR_SendRemoteConfig向设备下发卡参数信息。
        /// </remarks>
        public int StartRemoteConfig(ref UseInfo info, uint dwCommand, IntPtr lpInBuffer, uint dwInBufferLen, IntPtr pUserData)
        {
            info.RemoteId = HikDeviceApi.Door.HikDoorApi.NET_DVR_StartRemoteConfig(info.UserId, dwCommand, lpInBuffer, dwInBufferLen, RemoteConfigCallback, pUserData);
            return info.RemoteId;
        }

        private void Door_RemoteConfigCallback(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData)
        {
            if (CardInfo != null)
            {
                if (pUserData == null)
                    return;
                switch ((HikDeviceApi.Door.HikDoorEnum.DwState)dwType)//(HikDoorApi.DwState)Enum.Parse(typeof(HikDoorApi.DwState), dwType.ToString())
                {
                    case HikDeviceApi.Door.HikDoorEnum.DwState.NET_SDK_CALLBACK_TYPE_STATUS:
                        byte[] byStatus = new byte[4];
                        Marshal.Copy(lpBuffer, byStatus, 0, 4);
                        int dwStatus = BitConverter.ToInt32(byStatus, 0);
                        //if (dwStatus == (int)HikDoorApi.DwStatus.NET_SDK_CALLBACK_STATUS_SUCCESS)
                            //StopRemoteConfig();
                        break;
                    case HikDeviceApi.Door.HikDoorEnum.DwState.NET_SDK_CALLBACK_TYPE_PROGRESS:
                        break;
                    case HikDeviceApi.Door.HikDoorEnum.DwState.NET_SDK_CALLBACK_TYPE_DATA:
                        cardConfig = (HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_CFG)Marshal.PtrToStructure(lpBuffer, typeof(HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_CFG));
                        if (CardInfo != null)
                            CardInfo(cardConfig);
                        break;
                }

            }
        }
        #endregion

        #region  发送长连接数据 
        /// <summary>
        /// 发送长连接数据
        /// </summary>
        /// <param name="info">启动长连接时的UseInfo对象</param>
        /// <param name="dwDataType"> 数据类型，跟长连接接口NET_DVR_StartRemoteConfig的命令参数（dwCommand）有关</param>
        /// <param name="pSendBuf">保存发送数据的缓冲区，与dwDataType有关</param>
        /// <param name="dwBufSize">发送数据的长度</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>
        /// NET_DVR_GET_CARD_CFG获取卡参数时，pSendBuf为查找条件，查找到的卡参数信息在NET_DVR_StartRemoteConfig设置的回调函数中返回。 
        /// NET_DVR_SET_CARD_CFG设置卡参数时，pSendBuf为下发的卡参数信息，必须保证卡号是从小到大递增的（可以不连续）而且卡号的整型值不能重复（比如不能同时含有1和01两种卡号），否则将返回失败。 
        /// dwCommand:2116,dwDataType:0x3(门禁主机数据类型 pSendBuf对应结构体 NET_DVR_CARD_CFG_SEND_DATA )
        /// dwCommand:2117,dwDataType:0x3(门禁主机数据类型 pSendBuf对应结构体 NET_DVR_CARD_CFG )
        /// </remarks>
        public bool SendRemoteConfig(UseInfo info, uint dwDataType, IntPtr pSendBuf, uint dwBufSize)
        {
            return HikDeviceApi.Door.HikDoorApi.NET_DVR_SendRemoteConfig(info.RemoteId, dwDataType, pSendBuf, dwBufSize);
        }
        #endregion

        #region  查询卡片信息 
        /// <summary>
        /// 获取卡片信息
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="cardNum">要查询的卡号 </param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetCardConfig(UseInfo info, string cardNum)
        {
            cardCfgSendData = new HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_CFG_SEND_DATA();
            byte[] byRes = Encoding.UTF8.GetBytes("0");
            cardCfgSendData.byRes = new byte[16];
            byRes.CopyTo(cardCfgSendData.byRes, 0);

            //byte[] carNo = Encoding.UTF8.GetBytes(cardNum);
            //cardCfgSendData.byCardNo = new byte[32];
            //carNo.CopyTo(cardCfgSendData.byCardNo, 0);
            cardCfgSendData.byCardNo = cardNum;

            Int32 nSize = Marshal.SizeOf(cardCfgSendData);
            cardCfgSendData.dwSize = (uint)nSize;

            uint dwSize = (uint)Marshal.SizeOf(cardCfgSendData);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(cardCfgSendData, ptrIpParaCfgV40, false);

            if (HikDeviceApi.Door.HikDoorApi.NET_DVR_SendRemoteConfig(info.RemoteId, (uint)HikDeviceApi.Door.HikDoorEnum.DwCommand.ENUM_ACS_SEND_DATA, ptrIpParaCfgV40, dwSize))
            {
                cardCfgSendData = (HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_CFG_SEND_DATA)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_CFG_SEND_DATA));
                return true;
            }
            else
            {
                int error = GetLastError();
                return false;
            }
        }
        #endregion

        #region  设置卡参数 
        /// <summary>
        /// 设置卡参数
        /// </summary>
        /// <param name="info">启动远程配置时的UseInfo对象</param>
        /// <param name="cardNo">卡号</param>
        /// <param name="cardPassWord">卡密码</param>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">截至日期</param>
        /// <param name="cardValid">是否有效</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="struValid">是否启用有效期</param>
        /// <param name="leaderCard">是否为首卡</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetCardConfig(UseInfo info, string cardNo, string cardPassWord, DateTime startDate, DateTime endDate, bool cardValid = true, HikDeviceApi.Door.HikDoorEnum.CardType cardType = HikDeviceApi.Door.HikDoorEnum.CardType.普通卡, bool struValid = true, bool leaderCard = false)
        {

            cardConfig = new HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_CFG();
            cardConfig.dwModifyParamType = (uint)HikDeviceApi.Door.HikDoorEnum.ModifyParamType.CARD_PARAM_VALID;
            //(uint)HikDoorApi.ModifyParamType.CARD_PARAM_PASSWORD |
            //(uint)HikDoorApi.ModifyParamType.CARD_PARAM_CARD_TYPE |
            //(uint)HikDoorApi.ModifyParamType.CARD_PARAM_CARD_VALID |
            //(uint)HikDoorApi.ModifyParamType.CARD_PARAM_LEADER_CARD |
            //(uint)HikDoorApi.ModifyParamType.CARD_PARAM_VALID |
            //(uint)HikDoorApi.ModifyParamType.CARD_PARAM_SWIPE_NUM
            //;
            cardConfig.byCardPassword = cardPassWord;
            cardConfig.byCardType = byte.Parse(((int)cardType).ToString());
            cardConfig.dwMaxSwipeTime = 0;
            cardConfig.byCardNo = cardNo;
            cardConfig.byCardValid = cardValid ? byte.Parse("1") : byte.Parse("0");
            cardConfig.byLeaderCard = leaderCard ? byte.Parse("1") : byte.Parse("0");
            byte[] byRes3 = Encoding.UTF8.GetBytes("0");
            cardConfig.byRes2 = new byte[24];
            byRes3.CopyTo(cardConfig.byRes2, 0);
            cardConfig.byRes1 = byte.Parse("0");

            cardConfig.struValid.byEnable = struValid ? byte.Parse("1") : byte.Parse("0");
            cardConfig.struValid.struBeginTime = new NET_DVR_TIME_EX() { wYear = (ushort)startDate.Year, byMonth = byte.Parse(startDate.Month.ToString()), byDay = byte.Parse(startDate.Day.ToString()), byHour = byte.Parse(startDate.Hour.ToString()), byMinute = byte.Parse(startDate.Minute.ToString()), bySecond = byte.Parse(startDate.Second.ToString()) };
            cardConfig.struValid.struEndTime = new NET_DVR_TIME_EX() { wYear = (ushort)endDate.Year, byMonth = byte.Parse(endDate.Month.ToString()), byDay = byte.Parse(endDate.Day.ToString()), byHour = byte.Parse(endDate.Hour.ToString()), byMinute = byte.Parse(endDate.Minute.ToString()), bySecond = byte.Parse(endDate.Second.ToString()) };
            byte[] byRes = Encoding.UTF8.GetBytes("0");
            cardConfig.struValid.byRes1 = new byte[3];
            byRes.CopyTo(cardConfig.struValid.byRes1, 0);
            byte[] byRes2 = Encoding.UTF8.GetBytes("0");
            cardConfig.struValid.byRes2 = new byte[32];
            byRes2.CopyTo(cardConfig.struValid.byRes1, 0);

            Int32 nSize = Marshal.SizeOf(cardConfig);
            cardConfig.dwSize = (uint)nSize;

            uint dwSize = (uint)Marshal.SizeOf(cardConfig);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(cardConfig, ptrIpParaCfgV40, false);

            if (HikDeviceApi.Door.HikDoorApi.NET_DVR_SendRemoteConfig(info.RemoteId, (uint)HikDeviceApi.Door.HikDoorEnum.DwCommand.NET_DVR_SET_CARD_CFG, ptrIpParaCfgV40, dwSize))
            {
                cardConfig = (HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_CFG)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_CFG));
                return true;
            }
            else
            {
                //int error = GetLastError();
                return false;
            }
        }

        #endregion

        #region  关闭长连接配置接口所创建的句柄，释放资源。 
        /// <summary>
        /// 关闭长连接配置接口所创建的句柄，释放资源。
        /// </summary>
        /// <param name="info">启动远程配置时的UseInfo对象</param>
        /// <returns>TRUE表示成功，FALSE表示失败.</returns>
        public bool StopRemoteConfig(ref UseInfo info)
        {

            bool b=HikDeviceApi.Door.HikDoorApi.NET_DVR_StopRemoteConfig(info.RemoteId);
            if (b)
                info.RemoteId = -1;
            return b;
        }
        #endregion

        #region  获取读卡器参数 
        /// <summary>
        /// 获取读卡器参数
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="deviceNo">读卡器编号</param>
        /// <param name="config">读卡器参数结构输出对象</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetCardReaderInfo(UseInfo info, int deviceNo, out HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_READER_CFG config)
        {
            uint dwSize = (uint)Marshal.SizeOf(cardReaderConfig);
            IntPtr deviceInfo = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(cardReaderConfig, deviceInfo, false);
            uint dwReturn = 0;
            bool b = HikPublicApi.NET_DVR_GetDVRConfig(info.UserId, (uint)HikDeviceApi.Door.HikDoorEnum.DwCommand.NET_DVR_GET_CARD_READER_CFG, deviceNo, deviceInfo, dwSize, ref dwReturn);
            if (b)
            {
                cardReaderConfig = (HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_READER_CFG)Marshal.PtrToStructure(deviceInfo, typeof(HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_READER_CFG));
                string type = "";
                switch (cardReaderConfig.byCardReaderType.ToString())
                {
                    case "1":
                        type = HikDeviceApi.Door.HikDoorEnum.CardReaderType.DS_K110XM_MK_C_CK.ToString();
                        break;
                    case "2":
                        type = HikDeviceApi.Door.HikDoorEnum.CardReaderType.DS_K182AM_AMP.ToString();
                        break;
                    case "3":
                        type = HikDeviceApi.Door.HikDoorEnum.CardReaderType.DS_K182BM_BMP.ToString();
                        break;
                    case "4":
                        type = HikDeviceApi.Door.HikDoorEnum.CardReaderType.DS_K192AM_AMP.ToString();
                        break;
                    case "5":
                        type = HikDeviceApi.Door.HikDoorEnum.CardReaderType.DS_K192BM_BMP.ToString();
                        break;
                    default:
                        break;
                }
                config = cardReaderConfig;
                return true;

            }
            else
            {
                //int iLastErr = GetlastErrorNo();
                config = cardReaderConfig;
                return false;
            }
        }
        #endregion

        #region  设置读卡器参数 
        /// <summary>
        /// 设置读卡器参数
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="deviceNo">读卡器编号</param>
        /// <param name="type">读卡器类型</param>
        /// <param name="enableFailAlarm">是否启用读卡失败超次报警</param>
        /// <param name="enableTamperCheck">是否启用防拆检测</param>
        /// <param name="maxReadCardFailNum">最大读卡失败次数</param>
        /// <param name="offlineCheckTime">防拆检测时间 0--255 s</param>
        /// <param name="pressTimeout">按键超时时间1--255 s</param>
        /// <param name="swipeInterval">重复刷卡间隔 单位秒</param>
        /// <param name="errorLedPolarity">错误led极性 0-阴极 1-阳极</param>
        /// <param name="okLedPolarity">正常led极性 0-阴极 1-阳极</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetCardReaderInfo(UseInfo info, int deviceNo, HikDeviceApi.Door.HikDoorEnum.CardReaderType type, bool enableFailAlarm = true, bool enableTamperCheck = true, int maxReadCardFailNum = 5, int offlineCheckTime = 5, int pressTimeout = 5, int swipeInterval = 0, int errorLedPolarity = 1, int okLedPolarity = 1)
        {
            cardReaderConfig = new HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_READER_CFG()
            {
                byEnable = byte.Parse("1"),
                byCardReaderType = byte.Parse(((int)type).ToString()),
                byEnableFailAlarm = byte.Parse(enableFailAlarm ? "1" : "0"),
                byEnableTamperCheck = byte.Parse(enableTamperCheck ? "1" : "0"),
                byMaxReadCardFailNum = byte.Parse(maxReadCardFailNum.ToString()),
                byOfflineCheckTime = byte.Parse(offlineCheckTime.ToString()),
                byPressTimeout = byte.Parse(pressTimeout.ToString()),
                bySwipeInterval = byte.Parse(swipeInterval.ToString()),
                byErrorLedPolarity = byte.Parse(errorLedPolarity.ToString()),
                byOkLedPolarity = byte.Parse(okLedPolarity.ToString())
            };

            byte[] byRes = Encoding.UTF8.GetBytes("0");
            cardReaderConfig.byRes = new byte[25];
            byRes.CopyTo(cardReaderConfig.byRes, 0);

            Int32 nSize = Marshal.SizeOf(cardReaderConfig);
            cardReaderConfig.dwSize = (uint)nSize;
            IntPtr ptrTimeCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(cardReaderConfig, ptrTimeCfg, false);

            return HikPublicApi.NET_DVR_SetDVRConfig(info.UserId, (uint)HikDeviceApi.Door.HikDoorEnum.DwCommand.NET_DVR_SET_CARD_READER_CFG, deviceNo, ptrTimeCfg, (uint)nSize);

        }
        #endregion

        #region  设置门禁参数 
        /// <summary>
        /// 设置门禁参数
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="doorNo">门禁编号</param>
        /// <param name="doorName">门禁名称</param>
        /// <param name="stressPassword">胁迫密码 不大于8位</param>
        /// <param name="supperPassword">超级密码 不大于8位</param>
        /// <param name="isEnableDoorLock">是否启用闭门回锁</param>
        /// <param name="isEnableLeaderCard">是否启用首卡常开</param>
        /// <param name="isOpenButton">开门按钮是否常开</param>
        /// <param name="isOpenMagnetic">门磁是否常开</param>
        /// <param name="alarmTimeout">门禁检测超时时间 0--255s 0表示不报警</param>
        /// <param name="openTime">开门持续时间 1~255s</param>
        /// <param name="disabledOpenTime">残疾人开门持续时间 1--255s</param>
        /// <param name="leaderCardOpenTime">首卡常开持续时间 1--1440min</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetDoorInfo(UseInfo info, int doorNo, string doorName, int stressPassword, int supperPassword, bool isEnableDoorLock = true, bool isEnableLeaderCard = false, bool isOpenButton = false, bool isOpenMagnetic = false, int alarmTimeout = 10, int openTime = 9, int disabledOpenTime = 20, int leaderCardOpenTime = 30)
        {
            doorStatus = new HikDeviceApi.Door.HikDoorStruct.NET_DVR_DOOR_CFG();
            byte[] byName = Encoding.UTF8.GetBytes(doorName);
            doorStatus.byDoorName = new byte[32];
            byName.CopyTo(doorStatus.byDoorName, 0);

            doorStatus.byEnableDoorLock = isEnableDoorLock ? byte.Parse("1") : byte.Parse("0");
            doorStatus.byEnableLeaderCard = isEnableLeaderCard ? byte.Parse("1") : byte.Parse("0");
            doorStatus.byMagneticAlarmTimeout = byte.Parse(alarmTimeout.ToString());
            doorStatus.byMagneticType = isOpenMagnetic ? byte.Parse("1") : byte.Parse("0");
            doorStatus.byOpenButtonType = isEnableLeaderCard ? byte.Parse("1") : byte.Parse("0");
            doorStatus.byOpenDuration = byte.Parse(openTime.ToString());

            byte[] byStressPassword = Encoding.UTF8.GetBytes(stressPassword.ToString());
            doorStatus.byStressPassword = new byte[8];
            byStressPassword.CopyTo(doorStatus.byStressPassword, 0);

            byte[] bySuperPassword = Encoding.UTF8.GetBytes(supperPassword.ToString());
            doorStatus.bySuperPassword = new byte[8];
            bySuperPassword.CopyTo(doorStatus.bySuperPassword, 0);

            doorStatus.dwLeaderCardOpenDuration = (uint)leaderCardOpenTime;
            doorStatus.byDisabledOpenDuration = byte.Parse(disabledOpenTime.ToString());
            doorStatus.byRes1 = byte.Parse("0");
            doorStatus.byRes2 = Encoding.UTF8.GetBytes("0");

            byte[] byRes2 = Encoding.UTF8.GetBytes("0");
            doorStatus.byRes2 = new byte[64];
            byRes2.CopyTo(doorStatus.byRes2, 0);

            Int32 nSize = Marshal.SizeOf(doorStatus);
            doorStatus.dwSize = (uint)nSize;
            IntPtr ptrTimeCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(doorStatus, ptrTimeCfg, false);

            return HikPublicApi.NET_DVR_SetDVRConfig(info.UserId, (uint)HikDeviceApi.Door.HikDoorEnum.DwCommand.NET_DVR_SET_DOOR_CFG, doorNo, ptrTimeCfg, (uint)nSize);

        }
        #endregion

        #region  获取最后一次错误码 
        /// <summary>
        /// 获取最后一次错误码
        /// </summary>
        /// <returns>错误码</returns>
        public int GetlastErrorNo()
        {
            return (int)HikPublicApi.NET_DVR_GetLastError();
        }
        #endregion

        #region  数据转换 
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
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string byteToHexStr(byte[] bytes, int leng)
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
        #endregion

        #region  设备操作参数结构 
        /// <summary>
        /// 设备操作参数结构
        /// </summary>
        public struct UseInfo
        {
            /// <summary>
            /// 用户登录返回ID
            /// </summary>
            /// <remarks>登陆事件注册返回的ID</remarks>
            public Int32 UserId { get; set; }
            ///// <summary>
            ///// 监听事件返回ID
            ///// </summary>
            ///// <remarks>监听事件注册返回的ID</remarks>
            //public int ListenId { get; set; }
            /// <summary>
            /// 报警事件返回ID
            /// </summary>
            /// <remarks>报警事件注册返回的ID</remarks>
            public Int32 AlarmId { get; set; }
            /// <summary>
            /// 远程配置id
            /// </summary>
            /// <remarks>远程事件注册返回的ID</remarks>
            public int RemoteId { get; set; }
            /// <summary>
            /// 登录用户名
            /// </summary>
            public string LoginUserName { get; set; }
            /// <summary>
            /// 登录用户密码
            /// </summary>
            public string LoginUserPwd { get; set; }
            /// <summary>
            /// 登录设备地址
            /// </summary>
            public string LoginDeviceIp { get; set; }
            /// <summary>
            /// 登录设备端口
            /// </summary>
            public int LoginDevicePoint { get; set; }
            /// <summary>
            /// 超时时间，单位毫秒.
            /// </summary>
            /// <remarks>取值范围[300,75000]，实际最大超时时间因系统的connect超时时间而不同.</remarks>
            public uint WaitTime { get; set; }
            /// <summary>
            /// 连接尝试次数（保留）
            /// </summary>
            public uint TryTimes { get; set; }
            /// <summary>
            /// 重连间隔，单位:毫秒
            /// </summary>
            public uint Interval { get; set; }
            /// <summary>
            /// 是否重连 0-不重连，1-重连
            /// </summary> 
            /// <remarks> 0-不重连，1-重连</remarks>
            public int EnableRecon { get; set; }
        }
        #endregion
    }
}
