using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using static HikDeviceApi.HikConst;
namespace HikDeviceApi.Door
{
    /// <summary>
    /// 日 期:2016-11-15
    /// 作 者:痞子少爷
    /// 描 述:海康26系列门禁控制
    /// </summary>
    public class HikDoorOperate
    {

        #region  委托事件 
        /// <summary>
        /// 门禁操作事件委托
        /// </summary>
        /// <param name="result"></param>
        public delegate void ControlResult(bool result);
        ///// <summary>
        ///// 门禁操作事件
        ///// </summary>
        ////public event ControlResult ResultMsg;
        ///// <summary>
        ///// 登录状态回调
        ///// </summary>
        ////public event HikDoorDelegate.fLoginResultCallBack LoginResult;
        ///// <summary>
        ///// 监听回调
        ///// </summary>
        ////public event HikDelegate.MSGCallBackV31 ListenResult;
        ///// <summary>
        ///// 接收异常消息的回调函数，回调当前异常的相关信息
        ///// </summary>
        ////public event HikDelegate.fExceptionCallBack ExceptionCallBack;
        /// <summary>
        /// 远程配置回调函数
        /// </summary>
        static event HikDelegate.fRemoteConfigCallback RemoteConfigCallback;

        ///// <summary>
        ///// 卡片参数配置事件委托
        ///// </summary>
        ///// <param name="cardInfo"></param>
        //public delegate void CardConfigInfo(HikDoorStruct.NET_DVR_CARD_CFG cardInfo);
        ///// <summary>
        ///// 卡片参数配置
        ///// </summary>
        //public event CardConfigInfo CardInfo;
        /// <summary>
        /// 远程参数配置状态
        /// </summary>
        public event HikDelegate.fRemoteConfigCallback RemoteStatus;
        /// <summary>
        /// 远程参数配置数据
        /// </summary>
        public event HikDelegate.fRemoteConfigCallback RemoteData;
        /// <summary>
        /// 远程参数配置进度
        /// </summary>
        public event HikDelegate.fRemoteConfigCallback RemoteProgress;
        #endregion

        #region  登录设备 
        /// <summary>
        /// 登录设备
        /// </summary>
        ///<param name="info">设备使用参数结构</param>
        ///<param name="deviceInfoV30">设备参数结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool LoginDoor_V30(ref DoorUseInfo info,ref HikStruct.NET_DVR_DEVICEINFO_V30 deviceInfoV30)
        {
            try
            {
                if(RemoteConfigCallback == null)
                    RemoteConfigCallback += Door_RemoteConfigCallback;  
                info.UserId = HikApi.NET_DVR_Login_V30(info.DeviceIp, (int)info.DevicePoint, info.UserName, info.UserPwd, ref deviceInfoV30);
                if (info.UserId == -1)
                {
                    return false;
                }
                else
                {
                    //if (isReConnect)
                    //{
                    //    HikApi.NET_DVR_SetConnectTime(info.WaitTime, info.TryTimes);
                    //    HikApi.NET_DVR_SetReconnect(info.Interval, info.EnableRecon);
                    //}

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
        public bool LoginOut(ref DoorUseInfo info)
        {
            bool b= HikApi.NET_DVR_Logout(info.UserId);
            //if(b)
            info.UserId = -1;
            return b;
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
        public bool ControlGateway(DoorUseInfo info, int doorNo, HikDoorEnum.DoorControl con)
        {
            return HikDoorApi.NET_DVR_ControlGateway(info.UserId, doorNo, (uint)con);
        }


        #endregion

        #region  获取主机工作状态 
        /// <summary>
        /// 获取主机工作状态
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="doorStatus">1-休眠 2-常开 3-常闭 4-普通</param>
        /// <param name="lockStatus">1-休眠 2-刷卡+密码 3-刷卡</param>
        /// <param name="status">主机工作状态结构体（输出）</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool GetDeviceWorkStatus(int userId, ref string[] doorStatus, ref string[] lockStatus,ref HikDoorStruct.NET_DVR_ACS_WORK_STATUS status)
        {
           
            uint dwSize = (uint)Marshal.SizeOf(status);
            IntPtr deviceInfo = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(status, deviceInfo, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(userId, (uint)HikDoorEnum.ConfigCommand.NET_DVR_GET_ACS_WORK_STATUS, 0, deviceInfo, dwSize, ref dwReturn);
            
            lockStatus = new string[4];
            doorStatus = new string[2];
            if (b)
            {
                status = (HikDoorStruct.NET_DVR_ACS_WORK_STATUS)Marshal.PtrToStructure(deviceInfo, typeof(HikDoorStruct.NET_DVR_ACS_WORK_STATUS));
                for (int j = 0; j <   2  ; j++)
                {
                    doorStatus[j] = Enum.GetName(typeof(HikDoorEnum.DoorStatus), status.byDoorStatus[j]);
                }
                for (int i = 0; i < 4; i++)
                {
                    lockStatus[i]=Enum.GetName(typeof(HikDoorEnum.CardReadVerifyMode), status.byCardReaderVerifyMode[i]);
                }
               
                //switch (ConvertBase(byteToHexStr(status.byDoorStatus, 1), 16, 10))
                //{
                //    case "1":
                //        ds = "休眠状态";
                //        break;
                //    case "2":
                //        ds = "常开状态";
                //        break;
                //    case "3":
                //        ds = "常闭状态";
                //        break;
                //    case "4":
                //        ds = "普通状态";
                //        break;
                //}

                //switch (ConvertBase(byteToHexStr(status.byCardReaderVerifyMode, 1), 16, 10))
                //{
                //    //0- 无效，1- 休眠，2- 刷卡+密码，3- 刷卡，4- 刷卡或密码，5- 指纹，6- 指纹加密码，7- 指纹或刷卡，8- 指纹加刷卡，9- 指纹加刷卡加密码
                //    case "1":
                //        ls = "休眠";
                //        break;
                //    case "2":
                //        ls = "刷卡+密码";
                //        break;
                //    case "3":
                //        ls = "刷卡";
                //        break;
                //    case "1":
                //        ls = "休眠";
                //        break;
                //    case "2":
                //        ls = "刷卡+密码";
                //        break;
                //    case "3":
                //        ls = "刷卡";
                //        break;
                //    case "1":
                //        ls = "休眠";
                //        break;
                //    case "2":
                //        ls = "刷卡+密码";
                //        break;
                //    case "3":
                //        ls = "刷卡";
                //        break;

                //}
                //skinWaterTextBox1.AppendText(string.Format("\r\n\r\n状态:门锁状态--{0}，门状态--{1}，门磁状态--{2}，读卡器状态--{3}，读卡器防拆--{4}，读卡器验证方式--{5}，已添加卡数量--{6}",
                //     door.ConvertBase(door.byteToHexStr(controllerStatus.byDoorLockStatus, 1), 16, 10) == "0" ? "关" : "开",
                //     doorStatus,
                //     door.ConvertBase(door.byteToHexStr(controllerStatus.byMagneticStatus, 1), 16, 10) == "0" ? "闭合" : "开启",
                //     door.ConvertBase(door.byteToHexStr(controllerStatus.byCardReaderOnlineStatus, 1), 16, 10) == "0" ? "不在线" : "在线",
                //     door.ConvertBase(door.byteToHexStr(controllerStatus.byCardReaderAntiDismantleStatus, 1), 16, 10) == "0" ? "关闭" : "开启",
                //     lockStatus,
                //     controllerStatus.dwCardNum.ToString()));
            }
            //else
            //{

            //    // int iLastErr = GetLastError();
            //    //skinWaterTextBox1.AppendText("\r\n\r\n查询失败");
            //}
            return b;
        }
        #endregion

        #region  设置报警上传通道 
        /// <summary>
        /// 设置报警上传通道
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象</param>
        /// <returns>成功返回true，否则失败</returns>
        /// <remarks>执行前请先调用SetDVRMessageCallBack_V31函数</remarks>
        public bool SetupAlarmChan_V41(ref DoorUseInfo info)
        {
            //if (ListenResult == null)
            //    return false;
            //bool bb = HikDoorApi.NET_DVR_SetDVRMessageCallBack_V31(ListenResult, IntPtr.Zero);

            HikDeviceApi.HikStruct.NET_DVR_SETUPALARM_PARAM struAlarmParam = new HikDeviceApi.HikStruct.NET_DVR_SETUPALARM_PARAM();
            struAlarmParam.dwSize = (uint)Marshal.SizeOf(struAlarmParam);
            struAlarmParam.byAlarmInfoType = 1;//1智能交通设备有效
           // info.AlarmId = HikApi.NET_DVR_SetupAlarmChan_V30(info.UserId);

            info.AlarmId = HikApi.NET_DVR_SetupAlarmChan_V41(info.UserId,ref struAlarmParam);
            if (info.AlarmId < 0)//door.SetupAlarmChan_V30()
            {
                int iLastErr = HikOperate.GetLastError();
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
        /// 关闭报警上传通道
        /// </summary>
        /// <param name="info">监视门禁动态返回的UseInfo对象</param>
        /// <returns>true:成功，false:失败</returns>
        public bool CloseAlarmChanV30(ref DoorUseInfo info)
        {
            try
            {
                bool b = HikApi.NET_DVR_CloseAlarmChan_V30(info.AlarmId);
                if (b)
                {
                    info.AlarmId = -1;
                }
                return b;
            }
            catch (Exception) { return false; }
        }
        #endregion

        #region  监视门禁动态

        /// <summary>
        /// 监视门禁动态，接收设备主动上传的报警等信息（支持多线程）。
        /// </summary>
        /// <param name="listenResult">报警消息回调</param>
        /// <param name="listenId">返回监听Id</param>
        /// <param name="localhostIP">本机IP地址</param>
        /// <param name="listenPort">监听端口</param>
        /// <returns>成功返回true，否则失败</returns>
        /// <remarks>执行前请先调用SetDVRMessageCallBack_V31函数。必须将设备的网络配置中的“远程管理主机地址”或者“远程报警主机地址”设置成PC机的IP地址（与接口中的sLocalIP参数一致），“远程管理主机端口号”或者“远程报警主机端口号”设置成PC机的监听端口号（与接口中的wLocalPort参数一致）。</remarks>
        public bool StartListen_V30(HikDelegate.MSGCallBack listenResult,ref int listenId, string localhostIP, int listenPort = 7200)
        {
            //if (ListenResult == null)
            //    return false;
            //bool bb = HikDoorApi.NET_DVR_SetDVRMessageCallBack_V31(ListenResult,IntPtr.Zero);
            HikDeviceApi.HikStruct.NET_DVR_SETUPALARM_PARAM struAlarmParam = new HikDeviceApi.HikStruct.NET_DVR_SETUPALARM_PARAM();
            struAlarmParam.dwSize = (uint)Marshal.SizeOf(struAlarmParam);
            struAlarmParam.byAlarmInfoType = 1;//1智能交通设备有效
            listenId = HikApi.NET_DVR_StartListen_V30(localhostIP, (short)listenPort, listenResult, IntPtr.Zero);
            if (listenId == -1)
            {
                //int x = GetlastErrorNo();
                return false;
            }
            return true;
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
                bool b= HikApi.NET_DVR_StopListen_V30(listenId);
                if (b)
                {
                    listenId = -1;
                }
                return b;
            }
            catch (Exception) { return false; }
        }
        #endregion

        #region  查询门禁信息 
        /// <summary>
        /// 查询门禁信息
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="chanel">门禁编号</param>
        /// <param name="doorStatus">门禁信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetDoorInfo(DoorUseInfo info, int chanel,ref HikDoorStruct.NET_DVR_DOOR_CFG doorStatus)
        {
            uint dwSize = (uint)Marshal.SizeOf(doorStatus);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(doorStatus, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(info.UserId, (uint)HikDoorEnum.ConfigCommand.NET_DVR_GET_DOOR_CFG, chanel, ptrIpParaCfgV40, dwSize, ref dwReturn);
            if (b)
            {
                doorStatus = (HikDoorStruct.NET_DVR_DOOR_CFG)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_DOOR_CFG));
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region  查询计划
        /// <summary>
        /// 获取读卡器验证计划
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="chanel">设备通道号</param>
        /// <param name="plan">计划参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetCardReadValidPlan(int userId, int chanel, ref HikDoorStruct.NET_DVR_CARD_READER_PLAN plan)
        {
            uint dwSize = (uint)Marshal.SizeOf(plan);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((int)dwSize);
            Marshal.StructureToPtr(plan, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(userId, (uint)HikDoorEnum.ConfigCommand.NET_DVR_GET_CARD_READER_PLAN, chanel, ptrIpParaCfgV40, dwSize,ref dwReturn);
            if (b)
            {
                plan = (HikDoorStruct.NET_DVR_CARD_READER_PLAN)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_CARD_READER_PLAN));
                Marshal.FreeHGlobal(ptrIpParaCfgV40);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取周计划
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="chanel">编号</param>
        /// <param name="cmd">获取类型</param>
        /// <param name="weekPlan">计划参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetWeekPlanInfo(int userId, int chanel, HikDoorEnum.ConfigCommand cmd,ref HikDoorStruct.NET_DVR_WEEK_PLAN_CFG weekPlan)
        {
            uint dwSize = (uint)Marshal.SizeOf(weekPlan);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((int)dwSize);
            Marshal.StructureToPtr(weekPlan, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(userId, (uint)cmd, chanel, ptrIpParaCfgV40, dwSize, ref dwReturn);
            if (b)
            {
                weekPlan = (HikDoorStruct.NET_DVR_WEEK_PLAN_CFG)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_WEEK_PLAN_CFG));
                Marshal.FreeHGlobal(ptrIpParaCfgV40);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取假日计划
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="chanel">编号</param>
        /// <param name="cmd">获取类型</param>
        /// <param name="holidayPlan">假日计划参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetHolidayPlanInfo(int userId, int chanel, HikDoorEnum.ConfigCommand cmd, ref HikDoorStruct.NET_DVR_HOLIDAY_PLAN_CFG holidayPlan)
        {
            uint dwSize = (uint)Marshal.SizeOf(holidayPlan);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(holidayPlan, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(userId, (uint)cmd, chanel, ptrIpParaCfgV40, dwSize, ref dwReturn);
            if (b)
            {
                holidayPlan = (HikDoorStruct.NET_DVR_HOLIDAY_PLAN_CFG)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_HOLIDAY_PLAN_CFG));
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 设置计划
        /// <summary>
        /// 设置读卡器周计划
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="chanel">设备通道号</param>
        /// <param name="plan">读卡器计划参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetCardReadValidPlan(int userId, int chanel, ref HikDoorStruct.NET_DVR_CARD_READER_PLAN plan)
        {
            uint dwSize = (uint)Marshal.SizeOf(plan);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((int)dwSize);
            Marshal.StructureToPtr(plan, ptrIpParaCfgV40, false);
            bool b = HikApi.NET_DVR_SetDVRConfig(userId, (uint)HikDoorEnum.ConfigCommand.NET_DVR_SET_CARD_READER_PLAN, chanel, ptrIpParaCfgV40, dwSize);
            if (b)
            {
                plan = (HikDoorStruct.NET_DVR_CARD_READER_PLAN)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_CARD_READER_PLAN));
                Marshal.FreeHGlobal(ptrIpParaCfgV40);
                return true;
            }
            else
            {
                return false;
            }
        }
        ///// <summary>
        ///// 设置卡权限周计划
        ///// </summary>
        ///// <param name="userId">用户Id</param>
        ///// <param name="chanel">设备通道号</param>
        ///// <param name="plan">读卡器计划参数信息结构体（输出）</param>
        ///// <returns>成功返回true，否则失败</returns>
        //public bool SetCardValidPlan(int userId, int chanel, ref HikDoorStruct.NET_DVR_CARD_READER_PLAN plan)
        //{
        //    uint dwSize = (uint)Marshal.SizeOf(plan);
        //    IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((int)dwSize);
        //    Marshal.StructureToPtr(plan, ptrIpParaCfgV40, false);
        //    bool b = HikApi.NET_DVR_SetDVRConfig(userId, (uint)HikDoorEnum.ConfigCommand.NET_DVR_SET_CARD_RIGHT_WEEK_PLAN, chanel, ptrIpParaCfgV40, dwSize);
        //    if (b)
        //    {
        //        plan = (HikDoorStruct.NET_DVR_CARD_READER_PLAN)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_CARD_READER_PLAN));
        //        Marshal.FreeHGlobal(ptrIpParaCfgV40);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        ///// <summary>
        ///// 设置门权限周计划
        ///// </summary>
        ///// <param name="userId">用户Id</param>
        ///// <param name="chanel">设备通道号</param>
        ///// <param name="plan">读卡器计划参数信息结构体（输出）</param>
        ///// <returns>成功返回true，否则失败</returns>
        //public bool SetDoorValidPlan(int userId, int chanel, ref HikDoorStruct.NET_DVR_CARD_READER_PLAN plan)
        //{
        //    uint dwSize = (uint)Marshal.SizeOf(plan);
        //    IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((int)dwSize);
        //    Marshal.StructureToPtr(plan, ptrIpParaCfgV40, false);
        //    bool b = HikApi.NET_DVR_SetDVRConfig(userId, (uint)HikDoorEnum.ConfigCommand.NET_DVR_SET_CARD_RIGHT_WEEK_PLAN, chanel, ptrIpParaCfgV40, dwSize);
        //    if (b)
        //    {
        //        plan = (HikDoorStruct.NET_DVR_CARD_READER_PLAN)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_CARD_READER_PLAN));
        //        Marshal.FreeHGlobal(ptrIpParaCfgV40);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        /// <summary>
        /// 设置周计划
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="chanel">设备通道号</param>
        /// <param name="cmd">设置类型</param>
        /// <param name="weekPlan">周计划参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetWeekPlanInfo(int userId, int chanel, HikDoorEnum.ConfigCommand cmd, ref HikDoorStruct.NET_DVR_WEEK_PLAN_CFG weekPlan)
        {
            uint dwSize = (uint)Marshal.SizeOf(weekPlan);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((int)dwSize);
            Marshal.StructureToPtr(weekPlan, ptrIpParaCfgV40, false);
            bool b = HikApi.NET_DVR_SetDVRConfig(userId, (uint)cmd, chanel, ptrIpParaCfgV40, dwSize);
            int x = HikOperate.GetLastError();
            if (b)
            {
                weekPlan = (HikDoorStruct.NET_DVR_WEEK_PLAN_CFG)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_WEEK_PLAN_CFG));
                Marshal.FreeHGlobal(ptrIpParaCfgV40);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置假日计划
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="chanel">编号</param>
        /// <param name="cmd">获取类型</param>
        /// <param name="weekPlan">假日计划参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetHolidayPlanInfo(int userId, int chanel, HikDoorEnum.ConfigCommand cmd, ref HikDoorStruct.NET_DVR_HOLIDAY_PLAN_CFG weekPlan)
        {
            uint dwSize = (uint)Marshal.SizeOf(weekPlan);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(weekPlan, ptrIpParaCfgV40, false);
            bool b = HikApi.NET_DVR_SetDVRConfig(userId, (uint)cmd, chanel, ptrIpParaCfgV40, dwSize);
            if (b)
            {
                weekPlan = (HikDoorStruct.NET_DVR_HOLIDAY_PLAN_CFG)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_HOLIDAY_PLAN_CFG));
                Marshal.FreeHGlobal(ptrIpParaCfgV40);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 计划模板
        /// <summary>
        /// 获取计划模板参数
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="chanel">编号</param>
        /// <param name="cmd">获取类型</param>
        /// <param name="weekPlan">计划参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetPlanTemplateInfo(DoorUseInfo info, int chanel, HikDoorEnum.ConfigCommand cmd,ref HikDoorStruct.NET_DVR_PLAN_TEMPLATE weekPlan)
        {
            uint dwSize = (uint)Marshal.SizeOf(weekPlan);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(weekPlan, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(info.UserId, (uint)cmd, chanel, ptrIpParaCfgV40, dwSize, ref dwReturn);
            if (b)
            {
                weekPlan = (HikDoorStruct.NET_DVR_PLAN_TEMPLATE)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_PLAN_TEMPLATE));
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置计划模板参数
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="chanel">编号</param>
        /// <param name="cmd">设置类型</param>
        /// <param name="weekPlan">计划参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetPlanTemplateInfo(DoorUseInfo info, int chanel, HikDoorEnum.ConfigCommand cmd, ref HikDoorStruct.NET_DVR_PLAN_TEMPLATE weekPlan)
        {
            uint dwSize = (uint)Marshal.SizeOf(weekPlan);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(weekPlan, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(info.UserId, (uint)cmd, chanel, ptrIpParaCfgV40, dwSize, ref dwReturn);
            if (b)
            {
                weekPlan = (HikDoorStruct.NET_DVR_PLAN_TEMPLATE)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_PLAN_TEMPLATE));
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 假日组参数

        /// <summary>
        /// 获取周计划
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="chanel">编号</param>
        /// <param name="cmd">获取类型</param>
        /// <param name="holidayGroup">假日组参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetGroupInfo(DoorUseInfo info, int chanel, HikDoorEnum.ConfigCommand cmd, ref HikDoorStruct.NET_DVR_HOLIDAY_GROUP_CFG holidayGroup)
        {
            uint dwSize = (uint)Marshal.SizeOf(holidayGroup);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(holidayGroup, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(info.UserId, (uint)cmd, chanel, ptrIpParaCfgV40, dwSize, ref dwReturn);
            if (b)
            {
                holidayGroup = (HikDoorStruct.NET_DVR_HOLIDAY_GROUP_CFG)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_HOLIDAY_GROUP_CFG));
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置假日组参数
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="chanel">编号</param>
        /// <param name="cmd">设置类型</param>
        /// <param name="holidayGroup">假日组参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetGroupInfo(DoorUseInfo info, int chanel, HikDoorEnum.ConfigCommand cmd, ref HikDoorStruct.NET_DVR_HOLIDAY_GROUP_CFG holidayGroup)
        {
            uint dwSize = (uint)Marshal.SizeOf(holidayGroup);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(holidayGroup, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(info.UserId, (uint)cmd, chanel, ptrIpParaCfgV40, dwSize, ref dwReturn);
            if (b)
            {
                holidayGroup = (HikDoorStruct.NET_DVR_HOLIDAY_GROUP_CFG)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_HOLIDAY_GROUP_CFG));
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 获取设备信息 
        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="deviceCfg">设备信息结构体（输出）</param>
        /// <returns>返回产品型号以及名称</returns>
        /// <remarks>返回字符串：设备名称：{0},设备型号:{1} 或者 “查询失败”</remarks>
        public string GetDeviceInfo(DoorUseInfo info,ref HikStruct.NET_DVR_DEVICECFG_V40 deviceCfg)
        {
            uint dwSize = (uint)Marshal.SizeOf(deviceCfg);
            IntPtr deviceInfo = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(deviceCfg, deviceInfo, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(info.UserId, (uint)HikDoorEnum.ConfigCommand.NET_DVR_GET_DEVICECFG_V40, 0, deviceInfo, dwSize, ref dwReturn);
            if (b)
            {
                deviceCfg = (HikStruct.NET_DVR_DEVICECFG_V40)Marshal.PtrToStructure(deviceInfo, typeof(HikStruct.NET_DVR_DEVICECFG_V40));
                string name = Encoding.UTF8.GetString(deviceCfg.sDVRName).TrimEnd('\0');
                string type = Encoding.UTF8.GetString(deviceCfg.byDevTypeName).TrimEnd('\0');
                return string.Format("设备名称：{0},设备型号:{1}", name, type);

            }
            else
            {
                return "查询失败";
            }
        }
        #endregion

        #region  启动远程获取配置 
        /// <summary>
        /// 长连接配置ID
        /// </summary>
        static int remoteID = -1;
        /// <summary>
        /// 启动远程配置（设置、获取卡片使用）
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="type">操作类型，设置或者获取</param>
        /// <param name="cardNum">获取卡数量，默认获取所有</param>
        /// <param name="checkCardNo">设备是否进行卡号校验：0- 不校验，1- 校验 </param>
        /// <returns>成功返回true，否则false</returns>
        /// <remarks>
        /// NET_DVR_GET_CARD_CFG获取卡参数时，在调用该接口启动长连接远程配置后，还需要调用NET_DVR_SendRemoteConfig发送查找条件数据(获取所有卡参数时不需要调用该发送接口)，查找结果在NET_DVR_StartRemoteConfig设置的回调函数中返回。 
        /// NET_DVR_SET_CARD_CFG设置卡参数时，在调用该接口启动长连接远程配置后，通过调用NET_DVR_SendRemoteConfig向设备下发卡参数信息。
        /// </remarks>
        public bool StartRemoteConfig(ref DoorUseInfo info, HikDoorEnum.ConfigCommand type = HikDoorEnum.ConfigCommand.NET_DVR_GET_CARD_CFG, uint cardNum = 0xffffffff, uint checkCardNo= 1)
        {
            //if (remoteID != -1)
            //    StopRemoteConfig();
            HikDoorStruct.NET_DVR_CARD_CFG_COND cardCfgCond = new HikDoorStruct.NET_DVR_CARD_CFG_COND();
            cardCfgCond.byCheckCardNo = byte.Parse(checkCardNo.ToString());
            cardCfgCond.dwCardNum = cardNum; //0xffffffff;
            cardCfgCond.dwSize = (uint)Marshal.SizeOf(cardCfgCond);
            Int32 dwSize = Marshal.SizeOf(cardCfgCond);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal(dwSize);
            Marshal.StructureToPtr(cardCfgCond, ptrIpParaCfgV40, false);
            //SetCardInfo inf=new SetCardInfo() {  CardNo=cardNum, DeviceIp=info.DeviceIp, DeviceName=info.DeviceName, DevicePoint=info.DevicePoint, DevicePosition=info.DevicePosition, UserName=info.UserName, UserPwd=info.UserPwd}
            //Int32 dwSize2 = Marshal.SizeOf(info);
            //IntPtr uinfo = Marshal.AllocHGlobal(dwSize2);
            //Marshal.StructureToPtr(info, uinfo, false);
            //if (RemoteConfigCallback == null)
            //    RemoteConfigCallback += Door_RemoteConfigCallback;
            info.RemoteId = HikDoorApi.NET_DVR_StartRemoteConfig(info.UserId, (uint)type, ptrIpParaCfgV40, (uint)dwSize, RemoteConfigCallback, IntPtr.Zero);
            //int x = HikOperate.GetLastError();
            if (info.RemoteId < 0)
                return false;
            else
            {
                remoteID = info.RemoteId;
                return true;
            }

        }

        /// <summary>
        /// 启动远程配置
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="type">配置命令，不同的功能对应不同的命令号(dwCommand)，lpInBuffer等参数也对应不同的内容</param>
        /// <param name="lpInBuffer">输入参数，具体内容跟配置命令相关</param>
        /// <param name="dwInBufferLen">输入缓冲的大小</param>
        /// <param name="pUserData">用户数据</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_SendRemoteConfig等接口的句柄</returns>
        /// <remarks>
        /// NET_DVR_GET_CARD_CFG获取卡参数时，在调用该接口启动长连接远程配置后，还需要调用NET_DVR_SendRemoteConfig发送查找条件数据(获取所有卡参数时不需要调用该发送接口)，查找结果在NET_DVR_StartRemoteConfig设置的回调函数中返回。 
        /// NET_DVR_SET_CARD_CFG设置卡参数时，在调用该接口启动长连接远程配置后，通过调用NET_DVR_SendRemoteConfig向设备下发卡参数信息。
        /// </remarks>
        public int StartRemoteConfig(ref DoorUseInfo info, HikDoorEnum.ConfigCommand type, IntPtr lpInBuffer, uint dwInBufferLen, IntPtr pUserData)
        {
            //if (remoteID != -1)
            //    StopRemoteConfig();
            //if (RemoteConfigCallback == null)
            //    RemoteConfigCallback += Door_RemoteConfigCallback;
            //Int32 dwSize = Marshal.SizeOf(info);
            //IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal(dwSize);
            //Marshal.StructureToPtr(info, ptrIpParaCfgV40, false);
            info.RemoteId = HikDoorApi.NET_DVR_StartRemoteConfig(info.UserId, (uint)type, lpInBuffer, dwInBufferLen, RemoteConfigCallback, IntPtr.Zero);
            remoteID = info.RemoteId;
            return info.RemoteId;
        }

        private void Door_RemoteConfigCallback(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData)
        {
            List<object> data = new List<object>() { dwType, lpBuffer, dwBufLen, pUserData };
            //(new Thread(new ParameterizedThreadStart(CardConfigCallback)) { IsBackground = true }).Start(data);
            CardConfigCallback(data);
        }

        private void CardConfigCallback(object obj)
        {
            List<object> data = obj as List<object>;
            uint dwType = (uint)data[0];
            IntPtr lpBuffer = (IntPtr)data[1];
            uint dwBufLen = (uint)data[2];
            IntPtr pUserData = (IntPtr)data[3];

            switch ((HikDoorEnum.RemoteSetState)dwType)
            {
                case HikDoorEnum.RemoteSetState.NET_SDK_CALLBACK_TYPE_STATUS:
                    //byte[] byStatus = new byte[4];
                    //Marshal.Copy(lpBuffer, byStatus, 0, 4);
                    //int dwStatus = BitConverter.ToInt32(byStatus, 0);
                    //if (dwStatus == (int)HikDoorEnum.DwStatus.NET_SDK_CALLBACK_STATUS_SUCCESS)
                    // StopRemoteConfig(remoteID);
                    RemoteStatus?.Invoke(dwType, lpBuffer, dwBufLen, pUserData);
                    break;
                case HikDoorEnum.RemoteSetState.NET_SDK_CALLBACK_TYPE_PROGRESS:
                    RemoteProgress?.Invoke(dwType, lpBuffer, dwBufLen, pUserData);
                    break;
                case HikDoorEnum.RemoteSetState.NET_SDK_CALLBACK_TYPE_DATA:
                    RemoteData?.Invoke(dwType, lpBuffer, dwBufLen, pUserData);
                    break;
                default:
                    break;
            }

        }
        #endregion

        #region  关闭长连接配置接口所创建的句柄，释放资源。 
        /// <summary>
        /// 关闭长连接配置接口所创建的句柄，释放资源。
        /// </summary>
        /// <returns>TRUE表示成功，FALSE表示失败.</returns>
        public bool StopRemoteConfig()
        {

            bool b = HikDoorApi.NET_DVR_StopRemoteConfig(remoteID);
            if (b)
            {
                remoteID = -1;
                //RemoteConfigCallback -= Door_RemoteConfigCallback;
                //RemoteConfigCallback =null;
            }
            return b;
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
        public bool SendRemoteConfig(DoorUseInfo info, uint dwDataType, IntPtr pSendBuf, uint dwBufSize)
        {
            return HikDoorApi.NET_DVR_SendRemoteConfig(info.RemoteId, dwDataType, pSendBuf, dwBufSize);
        }
        #endregion

        #region  查询卡片信息 
        /// <summary>
        /// 获取卡片信息
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="cardNum">要查询的卡号 </param>
        /// <param name="cardCfgSendData">卡片信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetCardConfig(DoorUseInfo info, string cardNum,ref HikDoorStruct.NET_DVR_CARD_CFG_SEND_DATA cardCfgSendData)
        {
            cardCfgSendData = new HikDoorStruct.NET_DVR_CARD_CFG_SEND_DATA();
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

            if (HikDoorApi.NET_DVR_SendRemoteConfig(info.RemoteId, (uint)HikDoorEnum.ConfigCommand.ENUM_ACS_SEND_DATA, ptrIpParaCfgV40, dwSize))
            {
                cardCfgSendData = (HikDoorStruct.NET_DVR_CARD_CFG_SEND_DATA)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_CARD_CFG_SEND_DATA));
                return true;
            }
            else
            {
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
        /// <param name="doorRight">门权限，按位表示，从低位到高位表示对门1~N是否有权限，值：0- 无权限，1- 有权限</param>
        /// <param name="cardValid">卡是否有效：0- 无效，1- 有效（用于删除卡，设置时置为0进行删除，获取时此字段始终为1）</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="struValid">是否启用该有效期：0- 不启用，1- 启用 </param>
        /// <param name="leaderCard">是否为首卡：1- 是，0- 否 </param>
        /// <param name="maxSwipeTime">最大刷卡次数，0 为不限制</param>
        /// <param name="swipeTime">已刷卡次数</param>
        /// <param name="belongGroup">所属群组</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetCardConfig(DoorUseInfo info, string cardNo, string cardPassWord, DateTime startDate, DateTime endDate,uint doorRight, uint cardValid = 1, HikDoorEnum.CardType cardType = HikDoorEnum.CardType.普通卡, uint struValid = 1, uint leaderCard = 0,uint maxSwipeTime=0,uint swipeTime=0,uint belongGroup=0)
        { 
            HikDoorStruct.NET_DVR_CARD_CFG cardConfig = new HikDoorStruct.NET_DVR_CARD_CFG()
            {
                dwModifyParamType =
                (uint)HikDoorEnum.ModifyParamType.CARD_PARAM_PASSWORD |
                (uint)HikDoorEnum.ModifyParamType.CARD_PARAM_CARD_TYPE |
                (uint)HikDoorEnum.ModifyParamType.CARD_PARAM_CARD_VALID |
                (uint)HikDoorEnum.ModifyParamType.CARD_PARAM_LEADER_CARD |
                (uint)HikDoorEnum.ModifyParamType.CARD_PARAM_VALID |
                (uint)HikDoorEnum.ModifyParamType.CARD_PARAM_SWIPE_NUM |
                (uint)HikDoorEnum.ModifyParamType.CARD_PARAM_SWIPED_NUM |
                (uint)HikDoorEnum.ModifyParamType.CARD_PARAM_DOOR_RIGHT |
                (uint)HikDoorEnum.ModifyParamType.CARD_PARAM_RIGHT_PLAN |
                (uint)HikDoorEnum.ModifyParamType.CARD_PARAM_GROUP /*&*/
                /*(uint)HikDoorEnum.ModifyParamType.CARD_PARAM_CARD_NO*/,
                byCardPassword = cardPassWord,
                byCardType = byte.Parse(((int)cardType).ToString()),
                dwMaxSwipeTime = maxSwipeTime,
                byCardNo = cardNo,
                byCardValid = byte.Parse(cardValid.ToString()),
                byLeaderCard = byte.Parse(leaderCard.ToString()),
                dwBelongGroup = belongGroup,
                dwDoorRight = doorRight,//doorRight,
                dwSwipeTime = swipeTime
            };
            byte[] plan = new byte[MAX_DOOR_NUM * MAX_CARD_RIGHT_PLAN_NUM];//Encoding.UTF8.GetBytes("1");
            if (doorRight == 3)
            {
                plan[0] = 1;
                plan[4] = 1;
            }
            else if(doorRight == 1)
                plan[0] = 1;
            else if (doorRight == 2)
                plan[4] = 1;
            cardConfig.byCardRightPlan = plan;
            byte[] byRes3 = Encoding.UTF8.GetBytes("0");
            cardConfig.byRes2 = new byte[24];
            byRes3.CopyTo(cardConfig.byRes2, 0);
            cardConfig.byRes1 = byte.Parse("0");

            //有效期
            //HikDoorStruct.NET_DVR_VALID_PERIOD_CFG cfg = new HikDoorStruct.NET_DVR_VALID_PERIOD_CFG();
            //cfg.byEnable = struValid ? byte.Parse("1") : byte.Parse("0");
            //cfg.struBeginTime = new HikDoorStruct.NET_DVR_TIME_EX() { wYear = (ushort)startDate.Year, byMonth = byte.Parse(startDate.Month.ToString()), byDay = byte.Parse(startDate.Day.ToString()), byHour = byte.Parse(startDate.Hour.ToString()), byMinute = byte.Parse(startDate.Minute.ToString()), bySecond = byte.Parse(startDate.Second.ToString()) };
            //cfg.struEndTime = new HikDoorStruct.NET_DVR_TIME_EX() { wYear = (ushort)endDate.Year, byMonth = byte.Parse(endDate.Month.ToString()), byDay = byte.Parse(endDate.Day.ToString()), byHour = byte.Parse(endDate.Hour.ToString()), byMinute = byte.Parse(endDate.Minute.ToString()), bySecond = byte.Parse(endDate.Second.ToString()) };

            //cfg.byRes1 = new byte[3];
            //cfg.byRes2 = new byte[32];
            //cardConfig.struValid = cfg;
            cardConfig.struValid.byEnable = byte.Parse(struValid.ToString());
            cardConfig.struValid.struBeginTime = new HikDoorStruct.NET_DVR_TIME_EX() { wYear = (ushort)startDate.Year, byMonth = byte.Parse(startDate.Month.ToString()), byDay = byte.Parse(startDate.Day.ToString()), byHour = byte.Parse(startDate.Hour.ToString()), byMinute = byte.Parse(startDate.Minute.ToString()), bySecond = byte.Parse(startDate.Second.ToString()) };
            cardConfig.struValid.struEndTime = new HikDoorStruct.NET_DVR_TIME_EX() { wYear = (ushort)endDate.Year, byMonth = byte.Parse(endDate.Month.ToString()), byDay = byte.Parse(endDate.Day.ToString()), byHour = byte.Parse(endDate.Hour.ToString()), byMinute = byte.Parse(endDate.Minute.ToString()), bySecond = byte.Parse(endDate.Second.ToString()) };

            cardConfig.struValid.byRes1 = new byte[3];
            cardConfig.struValid.byRes2 = new byte[32];

            Int32 nSize = Marshal.SizeOf(cardConfig);
            cardConfig.dwSize = (uint)nSize;
            uint dwSize = (uint)Marshal.SizeOf(cardConfig);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(cardConfig, ptrIpParaCfgV40, false);

            if (HikDoorApi.NET_DVR_SendRemoteConfig(info.RemoteId, (uint)HikDoorEnum.ConfigCommand.ENUM_ACS_SEND_DATA, ptrIpParaCfgV40, dwSize))
            {
                cardConfig = (HikDoorStruct.NET_DVR_CARD_CFG)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HikDoorStruct.NET_DVR_CARD_CFG));
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region  获取读卡器参数 
        /// <summary>
        /// 获取读卡器参数
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="deviceNo">读卡器编号</param>
        /// <param name="cardReaderConfig">读卡器参数结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetCardReaderInfo(DoorUseInfo info, int deviceNo, ref HikDoorStruct.NET_DVR_CARD_READER_CFG cardReaderConfig)
        {
            uint dwSize = (uint)Marshal.SizeOf(cardReaderConfig);
            IntPtr deviceInfo = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(cardReaderConfig, deviceInfo, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(info.UserId, (uint)HikDoorEnum.ConfigCommand.NET_DVR_GET_CARD_READER_CFG, deviceNo, deviceInfo, dwSize, ref dwReturn);
            if (b)
            {
                cardReaderConfig = (HikDoorStruct.NET_DVR_CARD_READER_CFG)Marshal.PtrToStructure(deviceInfo, typeof(HikDoorStruct.NET_DVR_CARD_READER_CFG));
                //string type = "";
                //switch (cardReaderConfig.byCardReaderType.ToString())
                //{
                //    case "1":
                //        type = HikDoorEnum.CardReaderType.DS_K110XM_MK_C_CK.ToString();
                //        break;
                //    case "2":
                //        type = HikDoorEnum.CardReaderType.DS_K182AM_AMP.ToString();
                //        break;
                //    case "3":
                //        type = HikDoorEnum.CardReaderType.DS_K182BM_BMP.ToString();
                //        break;
                //    case "4":
                //        type = HikDoorEnum.CardReaderType.DS_K192AM_AMP.ToString();
                //        break;
                //    case "5":
                //        type = HikDoorEnum.CardReaderType.DS_K192BM_BMP.ToString();
                //        break;
                //    default:
                //        break;
                //}
                //config = cardReaderConfig;
                return true;

            }
            else
            {
                //int iLastErr = GetlastErrorNo();
                //config = cardReaderConfig;
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
        /// <param name="enableFailAlarm">是否启用读卡失败超次报警：0- 不启用，1- 启用</param>
        /// <param name="enableTamperCheck">是否启用防拆检测：0- 不启用，1- 启用</param>
        /// <param name="maxReadCardFailNum">最大读卡失败次数</param>
        /// <param name="offlineCheckTime">防拆检测时间 0--255 s</param>
        /// <param name="pressTimeout">按键超时时间1--255 s</param>
        /// <param name="swipeInterval">重复刷卡间隔 单位秒</param>
        /// <param name="errorLedPolarity">错误led极性 0-阴极 1-阳极</param>
        /// <param name="okLedPolarity">正常led极性 0-阴极 1-阳极</param>
        /// <param name="buzzerPolarity">蜂鸣器极性：0- 阴极，1- 阳极</param>
        /// <param name="fingerPrintCheckLevel">指纹识别等级：1- 1/10误认率，2- 1/100误认率，3- 1/1000误认率，4- 1/10000误认率，5- 1/100000误认率，6- 1/1000000误认率，7- 1/10000000误认率，8- 1/100000000误认率，9- 3/100误认率，10- 3/1000误认率，11- 3/10000误认率，12- 3/100000误认率，13- 3/1000000误认率，14- 3/10000000误认率，15- 3/100000000误认率，16- Automatic Normal，17- Automatic Secure，18- Automatic More Secure </param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetCardReaderInfo(DoorUseInfo info, int deviceNo, HikDoorEnum.CardReaderType type, uint enableFailAlarm = 1, uint enableTamperCheck = 1, uint maxReadCardFailNum = 5, uint offlineCheckTime = 5, uint pressTimeout = 5, uint swipeInterval = 0, uint errorLedPolarity = 1, uint okLedPolarity = 1,uint buzzerPolarity=1,uint fingerPrintCheckLevel=1)
        {
            HikDoorStruct.NET_DVR_CARD_READER_CFG cardReaderConfig = new HikDoorStruct.NET_DVR_CARD_READER_CFG()
            {
                byEnable = byte.Parse("1"),
                byCardReaderType = byte.Parse(((int)type).ToString()),
                byEnableFailAlarm = byte.Parse(enableFailAlarm.ToString()),
                byEnableTamperCheck = byte.Parse(enableTamperCheck.ToString()),
                byMaxReadCardFailNum = byte.Parse(maxReadCardFailNum.ToString()),
                byOfflineCheckTime = byte.Parse(offlineCheckTime.ToString()),
                byPressTimeout = byte.Parse(pressTimeout.ToString()),
                bySwipeInterval = byte.Parse(swipeInterval.ToString()),
                byErrorLedPolarity = byte.Parse(errorLedPolarity.ToString()),
                byOkLedPolarity = byte.Parse(okLedPolarity.ToString()),
                //byFingerPrintCheckLevel = byte.Parse(fingerPrintCheckLevel.ToString()),
                byBuzzerPolarity = byte.Parse(buzzerPolarity.ToString())
            };

            byte[] byRes = Encoding.UTF8.GetBytes("0");
            cardReaderConfig.byRes = new byte[25];
            byRes.CopyTo(cardReaderConfig.byRes, 0);

            Int32 nSize = Marshal.SizeOf(cardReaderConfig);
            cardReaderConfig.dwSize = (uint)nSize;
            IntPtr ptrTimeCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(cardReaderConfig, ptrTimeCfg, false);

            return HikApi.NET_DVR_SetDVRConfig(info.UserId, (uint)HikDoorEnum.ConfigCommand.NET_DVR_SET_CARD_READER_CFG, deviceNo, ptrTimeCfg, (uint)nSize);

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
        /// <param name="isEnableDoorLock">是否启用闭门回锁：0- 否，1- 是</param>
        /// <param name="isEnableLeaderCard">是否启用首卡常开功能：0- 否，1- 是 </param>
        /// <param name="isOpenButton">开门按钮类型：0- 常闭，1- 常开</param>
        /// <param name="isOpenMagnetic">门磁类型：0- 常闭，1- 常开</param>
        /// <param name="alarmTimeout">门禁检测超时时间 0--255s 0表示不报警</param>
        /// <param name="openTime">开门持续时间 1~255s</param>
        /// <param name="disabledOpenTime">残疾人开门持续时间 1--255s</param>
        /// <param name="leaderCardOpenTime">首卡常开持续时间 1--1440min</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetDoorInfo(DoorUseInfo info, int doorNo, string doorName, uint stressPassword, uint supperPassword, uint isEnableDoorLock = 1, uint isEnableLeaderCard = 0, uint isOpenButton = 0, uint isOpenMagnetic = 0, uint alarmTimeout = 10, uint openTime = 9, uint disabledOpenTime = 20, uint leaderCardOpenTime = 30)
        {
            HikDoorStruct.NET_DVR_DOOR_CFG doorStatus = new HikDoorStruct.NET_DVR_DOOR_CFG()
            {
                byDoorName = doorName,
                byEnableDoorLock = byte.Parse(isEnableDoorLock.ToString()),
                byEnableLeaderCard = byte.Parse(isEnableLeaderCard.ToString()),
                byMagneticAlarmTimeout = byte.Parse(alarmTimeout.ToString()),
                byMagneticType = byte.Parse(isOpenMagnetic.ToString()),
                byOpenButtonType = byte.Parse(isOpenButton.ToString()) ,
                byOpenDuration = byte.Parse(openTime.ToString()),
                byStressPassword = stressPassword.ToString(),
                bySuperPassword = supperPassword.ToString(),
                dwLeaderCardOpenDuration = (uint)leaderCardOpenTime,
                byDisabledOpenDuration = byte.Parse(disabledOpenTime.ToString()),
                byRes1 = byte.Parse("0"),
                byRes2 = Encoding.UTF8.GetBytes("0")
            };

            byte[] byRes2 = Encoding.UTF8.GetBytes("0");
            doorStatus.byRes2 = new byte[64];
            byRes2.CopyTo(doorStatus.byRes2, 0);

            Int32 nSize = Marshal.SizeOf(doorStatus);
            doorStatus.dwSize = (uint)nSize;
            IntPtr ptrTimeCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(doorStatus, ptrTimeCfg, false);

            return HikApi.NET_DVR_SetDVRConfig(info.UserId, (uint)HikDoorEnum.ConfigCommand.NET_DVR_SET_DOOR_CFG, doorNo, ptrTimeCfg, (uint)nSize);

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
        /// 设置卡信息
        /// </summary>
        public struct SetCardInfo
        {
            /// <summary>
            /// 登录用户名
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 登录用户密码
            /// </summary>
            public string UserPwd { get; set; }
            /// <summary>
            /// 登录设备地址
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 登录设备端口
            /// </summary>
            public decimal DevicePoint { get; set; }
            /// <summary>
            /// 设备名称
            /// </summary>
            public string DeviceName { get; set; }
            /// <summary>
            /// 设备地址
            /// </summary>
            public string DevicePosition { get; set; }
            /// <summary>
            /// 卡号
            /// </summary>
            public uint CardNo { get; set; }
            /// <summary>
            /// 门权限  1门禁1有权限，2门禁2有权限，3门禁1和2均有权限 
            /// </summary>
            public string DoorRight { get; set; }
            /// <summary>
            /// 设置状态
            /// </summary>
            public bool SetState { get; set; }
        }
        /// <summary>
        /// 设备操作参数结构
        /// </summary>
        [Serializable]
        public class DoorUseInfo
        {
            /// <summary>
            /// 用户登录返回ID
            /// </summary>
            /// <remarks>登陆事件注册返回的ID</remarks>
            private int userId = -1;
            ///// <summary>
            ///// 监听事件返回ID
            ///// </summary>
            ///// <remarks>监听事件注册返回的ID</remarks>
            //public int ListenId { get; set; }
            /// <summary>
            /// 报警事件返回ID
            /// </summary>
            /// <remarks>报警事件注册返回的ID</remarks>
            public int AlarmId { get; set; }
            /// <summary>
            /// 远程配置id
            /// </summary>
            /// <remarks>远程事件注册返回的ID</remarks>
            public int RemoteId { get; set; }
            /// <summary>
            /// 登录用户名
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 登录用户密码
            /// </summary>
            public string UserPwd { get; set; }
            /// <summary>
            /// 登录设备地址
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 登录设备端口
            /// </summary>
            public decimal DevicePoint { get; set; }
            /// <summary>
            /// 设备名称
            /// </summary>
            public string DeviceName { get; set; }
            /// <summary>
            /// 设备地址
            /// </summary>
            public string DevicePosition { get; set; }
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
            /// <summary>
            /// 用户登录返回ID
            /// </summary>
            public int UserId
            {
                get
                {
                    return userId;
                }

                set
                {
                    userId = value;
                }
            }
        }
        #endregion
    }
}
