using System;
using System.Runtime.InteropServices;
namespace HikDeviceApi.Door
{


    /// <summary>
    /// 日 期:2015-08-15
    /// 作 者:痞子少爷
    /// 描 述:海康26系列门禁接口
    /// </summary>
    public static class HikDoorApi
    {

        #region  函数入口
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
         [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_StartRemoteConfig(int lUserID, uint dwCommand, IntPtr lpInBuffer, uint dwInBufferLen, HikDelegate.fRemoteConfigCallback cbStateCallback, IntPtr pUserData);

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
         [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SendRemoteConfig(int lHandle, uint dwDataType, IntPtr pSendBuf, uint dwBufSize);

        /// <summary>
        /// 关闭长连接配置接口所创建的句柄，释放资源。
        /// </summary>
        /// <param name="lHandle">句柄，NET_DVR_StartRemoteConfig的返回值 </param>
        /// <returns>TRUE表示成功，FALSE表示失败.</returns>
         [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRemoteConfig(int lHandle);

        /// <summary>
        /// 门禁控制
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="lGatewayIndex">门禁序号，从1开始，-1表示对所有门进行操作</param>
        /// <param name="dwStaic">命令值：0- 关闭，1- 打开，2- 常开，3- 常关</param>
        /// <returns></returns>
         [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_ControlGateway(int lUserID, int lGatewayIndex, uint dwStaic);

        #endregion

    }
}
