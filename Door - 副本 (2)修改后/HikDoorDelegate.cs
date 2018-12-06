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
        /// 监听回调
        /// </summary>
        /// <param name="lCommand">上传的消息类型,COMM_ALARM_ACS为门禁主机</param>
        /// <param name="pAlarmer">报警设备信息</param>
        /// <param name="pAlarmInfo">报警信息 </param>
        /// <param name="dwBufLen">报警信息缓存大小</param>
        /// <param name="pUser">用户数据</param>
        //public delegate void MSGCallBackV31(Int32 lCommand, ref HikDoorStruct.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);
        

        #endregion
    }
}
