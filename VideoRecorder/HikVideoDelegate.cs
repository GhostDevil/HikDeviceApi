using System;
using static HikDeviceApi.VideoRecorder.HikVideoStruct;
using static HikDeviceApi.HikStruct;
namespace HikDeviceApi.VideoRecorder
{
    /// <summary>
    /// 日 期:2015-09-09
    /// 作 者:痞子少爷
    /// 描 述:海康硬盘录像机接口委托
    /// </summary>
    public static class HikVideoDelegate
    {
        /// <summary>
        /// 预览回调
        /// </summary>
        /// <param name="lRealHandle">当前的预览句柄</param>
        /// <param name="dwDataType">数据类型</param>
        /// <param name="pBuffer">存放数据的缓冲区指针</param>
        /// <param name="dwBufSize">缓冲区大小</param>
        /// <param name="pUser">用户数据</param>
        public delegate void REALDATACALLBACK(Int32 lRealHandle, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser);


        /// <summary>
        /// 监听回调处理
        /// </summary>
        /// <param name="pAlarmer">报警设备信息，包括设备序列号、IP地址、登录IUserID句柄等</param>
        /// <param name="pAlarmInfo">报警信息，通过lCommand值判断pAlarmer对应的结构体 </param>
        /// <param name="dwBufLen">报警信息缓存大小</param>
        /// <param name="pUser">用户数据</param>
        public delegate void MSGCallBackHandel(ref NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);
        ///// <summary>
        ///// 监听回调处理
        ///// </summary>
        ///// <param name="pAlarmer">报警设备信息，包括设备序列号、IP地址、登录IUserID句柄等</param>
        ///// <param name="pAlarmInfo">报警信息，通过lCommand值判断pAlarmer对应的结构体 </param>
        ///// <param name="dwBufLen">报警信息缓存大小</param>
        ///// <param name="pUser">用户数据</param>
        //public delegate void MSGCallBackHandel(ref NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);
        /// <summary>
        /// 录像数据回调函数 
        /// </summary>
        /// <param name="lPlayHandle">当前的录像播放句柄</param>
        /// <param name="dwDataType">数据类型</param>
        /// <param name="pBuffer">存放数据的缓冲区指针</param>
        /// <param name="dwBufSize">缓冲区大小</param>
        /// <param name="dwUser">用户数据</param>
        public delegate void PLAYDATACALLBACK(int lPlayHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser);
        /// <summary>
        /// 设备状态信息回调函数
        /// </summary>
        /// <param name="pUserdata">用户数据</param>
        /// <param name="lUserID">用户ID，NET_DVR_Login_V40的返回值</param>
        /// <param name="lpWorkState">NET_DVR_WORKSTATE_V40 设备状态信息。当获取失败时，lpWorkState为NULL，用户可以直接在回调函数中调用NET_DVR_GetLastError获取错误号</param>
        public delegate void DEV_WORK_STATE_CB(IntPtr pUserdata, int lUserID, NET_DVR_WORKSTATE_V40 lpWorkState);//NET_DVR_WORKSTATE_V40//DEV_WORK_STATE_CB
    }
}
