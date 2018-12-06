using System;
using static HikDeviceApi.HikStruct;

namespace HikDeviceApi
{
    /// <summary>
    /// 日 期:2015-11-05
    /// 作 者:痞子少爷
    /// 描 述:海康设备接口委托
    /// </summary>
    public static class HikDelegate
    {
        /// <summary>
        /// 监听回调
        /// </summary>
        /// <param name="lCommand">上传的消息类型，不同的报警信息对应不同的类型，通过类型区分是什么报警信息</param>
        /// <param name="pAlarmer">报警设备信息，包括设备序列号、IP地址、登录IUserID句柄等</param>
        /// <param name="pAlarmInfo">报警信息，通过lCommand值判断pAlarmer对应的结构体 </param>
        /// <param name="dwBufLen">报警信息缓存大小</param>
        /// <param name="pUser">用户数据</param>
        public delegate bool MSGCallBack_V31(int lCommand, ref NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);
        /// <summary>
        /// 监听回调
        /// </summary>
        /// <param name="lCommand">上传的消息类型，不同的报警信息对应不同的类型，通过类型区分是什么报警信息</param>
        /// <param name="pAlarmer">报警设备信息，包括设备序列号、IP地址、登录IUserID句柄等</param>
        /// <param name="pAlarmInfo">报警信息，通过lCommand值判断pAlarmer对应的结构体 </param>
        /// <param name="dwBufLen">报警信息缓存大小</param>
        /// <param name="pUser">用户数据</param>
        public delegate void MSGCallBack(int lCommand, ref NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);
        ///// <summary>
        ///// 监听回调
        ///// </summary>
        ///// <param name="lCommand">上传的消息类型，不同的报警信息对应不同的类型，通过类型区分是什么报警信息</param>
        ///// <param name="pAlarmer">报警设备信息，包括设备序列号、IP地址、登录IUserID句柄等</param>
        ///// <param name="pAlarmInfo">报警信息，通过lCommand值判断pAlarmer对应的结构体 </param>
        ///// <param name="dwBufLen">报警信息缓存大小</param>
        ///// <param name="pUser">用户数据</param>
        //public delegate void MSGCallBack(int lCommand, ref Door.HikDoorStruct.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);
        /// <summary>
        /// 监听回调
        /// </summary>
        /// <param name="lCommand">上传的消息类型</param>
        /// <param name="sDVRIP">设备IP地址</param>
        /// <param name="pBuf">报警信息</param>
        /// <param name="dwBufLen">报警信息缓存大小</param>
        /// <param name="dwLinkDVRPort">与设备间连接的设备端口号</param>
        public delegate void fMessCallBack_NEW(int lCommand,string sDVRIP, IntPtr pBuf,uint dwBufLen,int dwLinkDVRPort);

        /// <summary>
        /// 登录回调
        /// </summary>
        /// <param name="lUserID">用户ID，NET_DVR_Login_V40的返回值</param>
        /// <param name="dwResult">登录状态：0- 异步登录失败，1- 异步登录成功 </param>
        /// <param name="lpDeviceInfo">设备信息，设备序列号、通道、能力等参数 </param>
        /// <param name="pUser">用户数据</param>
        public delegate void fLoginResultCallBack(ref Int32 lUserID, ref Int32 dwResult, ref NET_DVR_DEVICEINFO_V30 lpDeviceInfo, ref IntPtr pUser);
        /// <summary>
        /// 接收异常消息的回调函数，回调当前异常的相关信息
        /// </summary>
        /// <param name="dwType">异常或重连等消息的类型</param>
        /// <param name="lUserID">登录ID </param>
        /// <param name="lHandle">出现异常的相应类型的句柄</param>
        /// <param name="pUser">用户数据</param>
        public delegate void fExceptionCallBack(int dwType, Int32 lUserID, Int32 lHandle, IntPtr pUser);
        /// <summary>
        /// 远程配置回调函数
        /// </summary>
        /// <param name="dwType">配置状态</param>
        /// <param name="lpBuffer">回调状态值</param>
        /// <param name="dwBufLen">回调进度值(暂不支持)</param>
        /// <param name="pUserData">回调数据内容</param>
        public delegate void fRemoteConfigCallback(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData);
      
        /// <summary>
        /// 网络流量检测回调函数
        /// </summary>
        /// <param name="lFlowHandle">当前的检测句柄</param>
        /// <param name="pFlowInfo">网络流量检测结果</param>
        /// <param name="pUser">用户数据</param>
        public delegate void FLOWTESTCALLBACK(int lFlowHandle, NET_DVR_FLOW_INFO pFlowInfo,IntPtr pUser);

    }
}
