using System;

namespace HikDeviceApi.Door
{
    /// <summary>
    /// 版 本:Release
    /// 日 期:2015-08-15
    /// 作 者:逍遥
    /// 描 述:海康26系列门禁接口委托
    /// </summary>
    public class HikDoorDelegate
    {
        #region  回调委托 
        /// <summary>
        /// 登录回调
        /// </summary>
        /// <param name="lUserID">用户ID，NET_DVR_Login_V40的返回值</param>
        /// <param name="dwResult">登录状态：0- 异步登录失败，1- 异步登录成功 </param>
        /// <param name="lpDeviceInfo">设备信息，设备序列号、通道、能力等参数 </param>
        /// <param name="pUser">用户数据</param>
        public delegate void fLoginResultCallBack(ref Int32 lUserID, ref Int32 dwResult, ref HikDoorStruct.NET_DVR_DEVICEINFO_V30 lpDeviceInfo, ref IntPtr pUser);
        /// <summary>
        /// 监听回调
        /// </summary>
        /// <param name="lCommand">上传的消息类型,COMM_ALARM_ACS为门禁主机</param>
        /// <param name="pAlarmer">报警设备信息</param>
        /// <param name="pAlarmInfo">报警信息 </param>
        /// <param name="dwBufLen">报警信息缓存大小</param>
        /// <param name="pUser">用户数据</param>
        public delegate void MSGCallBackV31(Int32 lCommand, ref HikDoorStruct.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);
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

        #endregion
    }
}
