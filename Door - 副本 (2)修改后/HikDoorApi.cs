using System;
using System.Runtime.InteropServices;
using static HikDeviceApi.PublicMethod.HikPublicDelegate;

namespace HikDeviceApi.Door
{


    /// <summary>
    /// 版 本:Release
    /// 日 期:2015-08-15
    /// 作 者:逍遥
    /// 描 述:海康26系列门禁接口
    /// </summary>
    public static class HikDoorApi
    {

        #region  函数入口 

        ///// <summary>
        /////  初始化SDK
        ///// </summary>
        ///// <returns>TRUE表示成功，FALSE表示失败。</returns>
        // [DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_Init();
        ///// <summary>
        ///// 释放SDK资源，在程序结束之前调用。
        ///// </summary>
        ///// <returns>TRUE表示成功，FALSE表示失败。</returns>
        // [DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_Cleanup();
        

        ///// <summary>
        ///// 门禁用户注册设备（支持异步登录）。
        ///// </summary>
        ///// <param name="sDVRIP">设备IP地址或是静态域名，字符数不大于128个 </param>
        ///// <param name="wDVRPort">设备端口号</param>
        ///// <param name="sUserName">登录的用户名</param>
        ///// <param name="sPassword">用户密码</param>
        ///// <param name="lpDeviceInfo">设备信息</param>
        ///// <returns>1表示失败，其他值表示返回的用户ID值。该用户ID具有唯一性，后续对设备的操作都需要通过此ID实现。</returns>
        // [DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern int NET_DVR_Login_V30(string sDVRIP, int wDVRPort, string sUserName, string sPassword, ref HikDeviceApi.Door.HikDoorStruct.NET_DVR_DEVICEINFO_V30 lpDeviceInfo);
        
        /// <summary>
        /// 启动远程配置
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwCommand">配置命令，不同的功能对应不同的命令号(dwCommand)，lpInBuffer等参数也对应不同的内容</param>
        /// <param name="lpInBuffer">输入参数，具体内容跟配置命令相关</param>
        /// <param name="dwInBufferLen">输入缓冲的大小</param>
        /// <param name="cbStateCallback">状态回调函数</param>
        /// <param name="pUserData">用户数据</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_SendRemoteConfig等接口的句柄</returns>
        /// <remarks>
        /// NET_DVR_GET_CARD_CFG获取卡参数时，在调用该接口启动长连接远程配置后，还需要调用NET_DVR_SendRemoteConfig发送查找条件数据(获取所有卡参数时不需要调用该发送接口)，查找结果在NET_DVR_StartRemoteConfig设置的回调函数中返回。 
        /// NET_DVR_SET_CARD_CFG设置卡参数时，在调用该接口启动长连接远程配置后，通过调用NET_DVR_SendRemoteConfig向设备下发卡参数信息。
        /// </remarks>
         [DllImport(@"HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_StartRemoteConfig(int lUserID, uint dwCommand, IntPtr lpInBuffer, uint dwInBufferLen, fRemoteConfigCallback cbStateCallback, IntPtr pUserData);

        /// <summary>
        /// 发送长连接数据
        /// </summary>
        /// <param name="lHandle">长连接句柄，NET_DVR_StartRemoteConfig的返回值</param>
        /// <param name="dwDataType"> 数据类型，跟长连接接口NET_DVR_StartRemoteConfig的命令参数（dwCommand）有关</param>
        /// <param name="pSendBuf">保存发送数据的缓冲区，与dwDataType有关</param>
        /// <param name="dwBufSize">发送数据的长度</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>
        /// NET_DVR_GET_CARD_CFG获取卡参数时，pSendBuf为查找条件，查找到的卡参数信息在NET_DVR_StartRemoteConfig设置的回调函数中返回。 
        /// NET_DVR_SET_CARD_CFG设置卡参数时，pSendBuf为下发的卡参数信息，必须保证卡号是从小到大递增的（可以不连续）而且卡号的整型值不能重复（比如不能同时含有1和01两种卡号），否则将返回失败。 
        /// </remarks>
         [DllImport(@"HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SendRemoteConfig(int lHandle, uint dwDataType, IntPtr pSendBuf, uint dwBufSize);

        /// <summary>
        /// 关闭长连接配置接口所创建的句柄，释放资源。
        /// </summary>
        /// <param name="lHandle">句柄，NET_DVR_StartRemoteConfig的返回值 </param>
        /// <returns>TRUE表示成功，FALSE表示失败.</returns>
         [DllImport(@"HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRemoteConfig(int lHandle);


        ///// <summary>
        ///// 获取错误码
        ///// </summary>
        ///// <returns>返回错误码</returns>
        // [DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern int NET_DVR_GetLastError();

        ///// <summary>
        ///// 用户注销
        ///// </summary>
        ///// <param name="lUserID">用户ID号，NET_DVR_Login_V40等登录接口的返回值</param>
        ///// <returns>TRUE表示成功，FALSE表示失败</returns>
        // [DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_Logout(int lUserID);

        /// <summary>
        /// 门禁控制
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="lGatewayIndex">门禁序号，从1开始，-1表示对所有门进行操作</param>
        /// <param name="dwStaic">命令值：0- 关闭，1- 打开，2- 常开，3- 常关</param>
        /// <returns></returns>
         [DllImport(@"HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_ControlGateway(int lUserID, int lGatewayIndex, uint dwStaic);

        
        ///// <summary>
        ///// 启动监听，接收设备主动上传的报警等信息（支持多线程）
        ///// </summary>
        ///// <param name="sLocalIP">PC机本地IP地址，可以置为NULL </param>
        ///// <param name="wLocalPort">PC本地监听端口号。由用户设置，必须和设备端设置的一致</param>
        ///// <param name="DataCallback">回调函数</param>
        ///// <param name="pUserData">用户数据 </param>
        ///// <returns>-1表示失败，其他值作为NET_DVR_StopListen_V30函数的句柄参数</returns>
        ///// <remarks>必须将设备的网络配置中的“远程管理主机地址”或者“远程报警主机地址”设置成PC机的IP地址（与接口中的sLocalIP参数一致），“远程管理主机端口号”或者“远程报警主机端口号”设置成PC机的监听端口号（与接口中的wLocalPort参数一致）。</remarks>
        // [DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern int NET_DVR_StartListen_V30(string sLocalIP, int wLocalPort, HikDeviceApi.Door.HikDoorDelegate.MSGCallBackV31 DataCallback, IntPtr pUserData);

        ///// <summary>
        ///// 停止监听（支持多线程）
        ///// </summary>
        ///// <param name="lListenHandle">监听句柄，NET_DVR_StartListen_V30的返回值 </param>
        ///// <returns>TRUE表示成功，FALSE表示失败。</returns>
        // [DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_StopListen_V30(int lListenHandle);

        ///// <summary>
        ///// 注册回调函数，接收设备报警消息等
        ///// </summary>
        ///// <param name="fMessageCallBack">回调函数</param>
        ///// <param name="pUser">用户数据</param>
        ///// <returns>TRUE表示成功，FALSE表示失败</returns>
        ///// <remarks>只需要设置一次回调函数即可，无需调用多次</remarks>
        //[DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_SetDVRMessageCallBack_V31(HikDeviceApi.Door.HikDoorDelegate.MSGCallBackV31 fMessageCallBack, IntPtr pUser);
        ///// <summary>
        ///// 建立报警上传通道，获取报警等信息。
        ///// </summary>
        ///// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        ///// <returns>-1表示失败，其他值作为NET_DVR_CloseAlarmChan_V30函数的句柄参数</returns>
        //[DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern int NET_DVR_SetupAlarmChan_V30(int lUserID);
        ///// <summary>
        ///// 撤销报警上传通道
        ///// </summary>
        ///// <param name="lAlarmHandle">NET_DVR_SetupAlarmChan_V30或者NET_DVR_SetupAlarmChan_V41的返回值</param>
        ///// <returns>TRUE表示成功，FALSE表示失败</returns>
        //[DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_CloseAlarmChan_V30(int lAlarmHandle);
        ///// <summary>
        ///// 注册接收异常、重连等消息的窗口句柄或回调函数。
        ///// </summary>
        ///// <param name="nMessage">消息</param>
        ///// <param name="hWnd">接收异常信息消息的窗口句柄</param>
        ///// <param name="cbExceptionCallBack">接收异常消息的回调函数</param>
        ///// <param name="pUser">用户数据</param>
        ///// <returns>TRUE表示成功，FALSE表示失败</returns>
        //[DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_SetExceptionCallBack_V30(int nMessage, IntPtr hWnd, HikDeviceApi.Door.HikDoorDelegate.fExceptionCallBack cbExceptionCallBack, IntPtr pUser);
        ///// <summary>
        ///// 获取设备的配置信息
        ///// </summary>
        ///// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        ///// <param name="dwCommand">设备配置命令 获取设备参数(扩展)-1100,获取时间参数-118</param>
        ///// <param name="lChannel">通道号，不同的命令对应不同的取值，如果该参数无效则置为0xFFFFFFFF即可</param>
        ///// <param name="lpOutBuffer">接收数据的缓冲指针 </param>
        ///// <param name="dwOutBufferSize">接收数据的缓冲长度(以字节为单位)，不能为0</param>
        ///// <param name="lpBytesReturned">实际收到的数据长度指针，不能为NULL </param>
        ///// <returns>TRUE表示成功，FALSE表示失败</returns>
        //[DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_GetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpOutBuffer, uint dwOutBufferSize, ref uint lpBytesReturned);

        ///// <summary>
        ///// 设置设备的配置信息
        ///// </summary>
        ///// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        ///// <param name="dwCommand">设备配置命令 获取设备参数(扩展)-1100,获取时间参数-118</param>
        ///// <param name="lChannel">通道号，不同的命令对应不同的取值，如果该参数无效则置为0xFFFFFFFF即可</param>
        ///// <param name="lpInBuffer">接收数据的缓冲指针 </param>
        ///// <param name="dwInBufferSize">接收数据的缓冲长度(以字节为单位)，不能为0</param>
        ///// <returns>TRUE表示成功，FALSE表示失败</returns>
        //[DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_SetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpInBuffer, uint dwInBufferSize);

        ///// <summary>
        ///// 设置连接时间
        ///// </summary>
        ///// <param name="dwWaitTime">超时时间，单位毫秒，取值范围[300,75000]，实际最大超时时间因系统的connect超时时间而不同</param>
        ///// <param name="dwTryTimes">连接尝试次数（保留）</param>
        ///// <returns>TRUE表示成功，FALSE表示失败</returns>
        //[DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_SetConnectTime(uint dwWaitTime, uint dwTryTimes);

        ///// <summary>
        ///// 设置重连时间
        ///// </summary>
        ///// <param name="dwInterval">重连间隔，单位:毫秒</param>
        ///// <param name="bEnableRecon">是否重连，0-不重连，1-重连，参数默认值为1</param>
        ///// <returns>TRUE表示成功，FALSE表示失败</returns>
        //[DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_SetReconnect(uint dwInterval, int bEnableRecon);
       

        ///// <summary>
        ///// 重启设备
        ///// </summary>
        ///// <param name="lUserID">用户ID号，NET_DVR_Login或NET_DVR_Login_V30的返回值 </param>
        ///// <returns>TRUE表示成功，FALSE表示失败</returns>
        //[DllImport(@"HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_RebootDVR(int lUserID);


        #endregion

    }
}
