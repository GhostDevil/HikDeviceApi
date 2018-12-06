using System;
using System.Runtime.InteropServices;
using static HikDeviceApi.PTZ.HikPTZStruct;

namespace HikDeviceApi.PTZ
{
    /// <summary>
    /// 日 期:2016-12-05
    /// 作 者:痞子少爷
    /// 描 述:云台控制接口
    /// </summary>
    public static class HikPTZApi
    {
        #region 云台控制操作
        /// <summary>
        /// 云台控制操作(需先启动图象预览)。
        /// <para>
        /// 对云台实施的每一个动作都需要调用该接口两次，分别是开始和停止控制，由接口中的最后一个参数（dwStop）决定。
        /// 在调用此接口之前需要先开启预览。与设备之间的云台各项操作的命令都对应于设备与云台之间的控制码，设备会根据目前设置的解码器种类和解码器地址向云台发送控制码。
        /// 如果目前设备上设置的解码器与云台设备的不匹配，需要重新配置设备的解码器。如果云台设备所需的解码器设备不支持，则无法用该接口控制。
        /// 云台默认以最大速度动作。
        /// </para>
        /// </summary>
        /// <param name="lRealHandle">NET_DVR_RealPlay或NET_DVR_RealPlay_V30的返回值</param>
        /// <param name="dwPTZCommand">云台控制命令</param>
        /// <param name="dwStop">云台停止动作或开始动作：0－开始，1－停止</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl(Int32 lRealHandle, uint dwPTZCommand, uint dwStop);

        /// <summary>
        /// 云台控制操作(不用启动图象预览)
        /// <para>
        /// 对云台实施的每一个动作都需要调用该接口两次，分别是开始和停止控制，由接口中的最后一个参数（dwStop）决定。在调用此接口之前需要先注册设备。与设备之间的云台各项操作的命令都对应于设备与云台之间的控制码，设备会根据目前设置的解码器种类和解码器地址向云台发送控制码。如果目前设备上设置的解码器与云台设备的不匹配，需要重新配置设备的解码器。如果云台设备所需的解码器设备不支持，则无法用该接口控制。
        /// 云台默认以最大速度动作。
        /// 通过NET_DVR_PTZControl控制云台，设备接收到控制命令后云台进行相应的动作，如果操作失败则返回错误，运行正常则返回成功。而通过NET_DVR_PTZControl_Other控制云台，设备接收到控制命令后直接返回成功
        /// </para>
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lChannel">通道号</param>
        /// <param name="dwPTZCommand">云台控制命令</param>
        /// <param name="dwStop">云台停止动作或开始动作：0－开始，1－停止</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>

        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl_Other(Int32 lUserID, Int32 lChannel, uint dwPTZCommand, uint dwStop);
        /// <summary>
        /// 带速度的云台控制操作(需先启动图象预览)。
        /// <para>
        /// 对云台实施的每一个动作都需要调用该接口两次，分别是开始和停止控制，由接口中的最后一个参数（dwStop）决定。
        /// 在调用此接口之前需要先开启预览。与设备之间的云台各项操作的命令都对应于设备与云台之间的控制码，设备会根据目前设置的解码器种类和解码器地址向云台发送控制码。
        /// 如果目前设备上设置的解码器与云台设备的不匹配，需要重新配置设备的解码器。如果云台设备所需的解码器设备不支持，则无法用该接口控制。 
        /// </para>
        /// </summary>
        /// <param name="lRealHandle">NET_DVR_RealPlay或NET_DVR_RealPlay_V30的返回值 </param>
        /// <param name="dwPTZCommand">云台控制命令</param>
        /// <param name="dwStop">云台停止动作或开始动作：0－开始；1－停止</param>
        /// <param name="dwSpeed">云台控制的速度，用户按不同解码器的速度控制值设置。取值范围[1,7]</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed(int lRealHandle, uint dwPTZCommand, uint dwStop, uint dwSpeed);
        /// <summary>
        /// 带速度的云台控制操作(不用启动图象预览)。
        /// <para>
        /// 对云台实施的每一个动作都需要调用该接口两次，分别是开始和停止控制，由接口中的最后一个参数（dwStop）决定。
        /// 在调用此接口之前需要先注册设备。与设备之间的云台各项操作的命令都对应于设备与云台之间的控制码，设备会根据目前设置的解码器种类和解码器地址向云台发送控制码。
        /// 如果目前设备上设置的解码器与云台设备的不匹配，需要重新配置设备的解码器。如果云台设备所需的解码器设备不支持，则无法用该接口控制。 
        /// 通过NET_DVR_PTZControlWithSpeed控制云台，设备接收到控制命令后云台进行相应的动作，如果操作失败则返回错误，运行正常则返回成功。而通过NET_DVR_PTZControlWithSpeed_Other控制云台，设备接收到控制命令后直接返回成功。
        /// </para>
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lChannel">通道号</param>
        /// <param name="dwPTZCommand">云台控制命令</param>
        /// <param name="dwStop">云台停止动作或开始动作：0－开始；1－停止</param>
        /// <param name="dwSpeed">云台控制的速度，用户按不同解码器的速度控制值设置。取值范围[1,7]</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed_Other(int lUserID, int lChannel, uint dwPTZCommand, uint dwStop, uint dwSpeed);
        #endregion

        #region 透明云台控制操作
        /// <summary>
        /// 透明云台操作，需先启动预览。
        /// <para>
        /// 使用该接口能直接通过设备将云台控制码信息直接传输给云台设备，而无需配置解码器。
        /// </para>
        /// </summary>
        /// <param name="lRealHandle">NET_DVR_RealPlay或NET_DVR_RealPlay_V30的返回值</param>
        /// <param name="pPTZCodeBuf">存放云台控制码缓冲区的指针</param>
        /// <param name="dwBufSize">云台控制码的长度</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ(Int32 lRealHandle, string pPTZCodeBuf, uint dwBufSize);
        /// <summary>
        /// 透明云台操作
        /// <para>
        /// 使用该接口能直接通过设备将云台控制码信息直接传输给云台设备，而无需配置解码器。
        /// </para>
        /// </summary>
        /// <param name="lUserID"> NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lChannel"> 通道号 </param>
        /// <param name="pPTZCodeBuf">存放云台控制码缓冲区的指针</param>
        /// <param name="dwBufSize">云台控制码的长度</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>使用该接口能直接通过设备将云台控制码信息直接传输给云台设备，而无需配置解码器。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ_Other(int lUserID, int lChannel, string pPTZCodeBuf, uint dwBufSize);
        #endregion

        #region 云台预置点操作
        /// <summary>
        /// 云台预置点操作（需先启动预览）。
        /// <para>
        /// 与设备之间的云台各项操作的命令都对应于设备与云台之间的控制码，设备会根据目前设置的解码器种类和解码器地址向云台发送控制码。
        /// 如果目前设备上设置的解码器与云台设备的不匹配，需要重新配置设备的解码器。如果云台设备所需的解码器设备不支持，则无法用该接口控制。
        /// </para>
        /// </summary>
        /// <param name="lRealHandle">NET_DVR_RealPlay或NET_DVR_RealPlay_V30的返回值 </param>
        /// <param name="dwPTZPresetCmd">操作云台预置点命令</param>
        /// <param name="dwPresetIndex">预置点的序号（从1开始），最多支持300个预置点 </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);
        /// <summary>
        /// 云台预置点操作
        /// <para>
        /// 与设备之间的云台各项操作的命令都对应于设备与云台之间的控制码，设备会根据目前设置的解码器种类和解码器地址向云台发送控制码。如果目前设备上设置的解码器与云台设备的不匹配，需要重新配置设备的解码器。
        /// 如果云台设备所需的解码器设备不支持，则无法用该接口控制。 
        /// 通过NET_DVR_PTZPreset控制云台，设备接收到控制命令后云台进行相应的动作，如果操作失败则返回错误，运行正常才返回成功。而通过NET_DVR_PTZPreset_Other控制云台，设备接收到控制命令后直接返回成功。
        /// </para>
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="lChannel">通道号</param>
        /// <param name="dwPTZPresetCmd">操作云台预置点命令</param>
        /// <param name="dwPresetIndex">预置点的序号（从1开始），最多支持300个预置点</param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset_Other(int lUserID, int lChannel, uint dwPTZPresetCmd, uint dwPresetIndex);
        #endregion

        #region 云台巡航操作
        /// <summary>
        /// 获取IP快球云台巡航路径。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="lChannel">通道号</param>
        /// <param name="lCruiseRoute">巡航路径号</param>
        /// <param name="lpCruiseRet">巡航轨迹</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZCruise(int lUserID, int lChannel, int lCruiseRoute, ref NET_DVR_CRUISE_RET lpCruiseRet);
        /// <summary>
        /// 云台巡航操作，需先启动预览。
        /// <para>与设备之间的云台各项操作的命令都对应于设备与云台之间的控制码，设备会根据目前设置的解码器种类和解码器地址向云台发送控制码。</para>
        /// <para>如果目前设备上设置的解码器与云台设备的不匹配，需要重新配置设备的解码器。如果云台设备所需的解码器设备不支持，则无法用该接口控制。 </para>
        /// </summary>
        /// <param name="lRealHandle">NET_DVR_RealPlay或NET_DVR_RealPlay_V30的返回值</param>
        /// <param name="dwPTZCruiseCmd">操作云台巡航命令</param>
        /// <param name="byCruiseRoute">巡航路径，最多支持32条路径（序号从1开始）</param>
        /// <param name="byCruisePoint">巡航点，最多支持32个点（序号从1开始）</param>
        /// <param name="wInput">不同巡航命令时的值不同，预置点(最大300)、时间(最大255)、速度(最大40) </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise(int lRealHandle, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);
        /// <summary>
        /// 云台巡航操作
        /// <para>
        /// 与设备之间的云台各项操作的命令都对应于设备与云台之间的控制码，设备会根据目前设置的解码器种类和解码器地址向云台发送控制码。</para>
        /// <para>如果目前设备上设置的解码器与云台设备的不匹配，需要重新配置设备的解码器。</para>
        /// <para>如果云台设备所需的解码器设备不支持，则无法用该接口控制。 </para>
        /// <para>通过NET_DVR_PTZCruise控制云台，设备接收到控制命令后云台进行相应的动作，如果操作失败则返回错误，运行正常则返回成功。而通过NET_DVR_PTZCruise_Other控制云台，设备接收到控制命令后直接返回成功。</para>
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lChannel">通道号</param>
        /// <param name="dwPTZCruiseCmd">操作云台巡航命令</param>
        /// <param name="byCruiseRoute">巡航路径，最多支持32条路径（序号从1开始）</param>
        /// <param name="byCruisePoint">巡航点，最多支持32个点（序号从1开始）</param>
        /// <param name="wInput">不同巡航命令时的值不同，预置点(最大255)、时间(最大255)、速度(最大40)</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise_Other(int lUserID, int lChannel, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);
        #endregion

        #region 云台轨迹操作 
        /// <summary>
        /// 云台轨迹操作，需先启动预览。
        /// <para>
        /// 与设备之间的云台各项操作的命令都对应于设备与云台之间的控制码，设备会根据目前设置的解码器种类和解码器地址向云台发送控制码。
        /// 如果目前设备上设置的解码器与云台设备的不匹配，需要重新配置设备的解码器。如果云台设备所需的解码器设备不支持，则无法用该接口控制。
        /// </para>
        /// </summary>
        /// <param name="lRealHandle">NET_DVR_RealPlay或NET_DVR_RealPlay_V30的返回值 </param>
        /// <param name="dwPTZTrackCmd">操作云台轨迹命令</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack(int lRealHandle, uint dwPTZTrackCmd);
        /// <summary>
        /// 云台轨迹操作
        /// <para>
        /// 与设备之间的云台各项操作的命令都对应于设备与云台之间的控制码，设备会根据目前设置的解码器种类和解码器地址向云台发送控制码。如果目前设备上设置的解码器与云台设备的不匹配，需要重新配置设备的解码器。
        /// 如果云台设备所需的解码器设备不支持，则无法用该接口控制。
        /// 通过NET_DVR_PTZTrack控制云台，设备接收到控制命令后云台进行相应的动作，如果操作失败则返回错误，运行正常才返回成功。而通过NET_DVR_PTZTrack_Other控制云台，设备接收到控制命令后直接返回成功。
        /// </para>
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lChannel">通道号</param>
        /// <param name="dwPTZTrackCmd">操作云台轨迹命令</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack_Other(int lUserID, int lChannel, uint dwPTZTrackCmd);
        #endregion

        #region 云台区域缩放控制操作
        /// <summary>
        /// 云台图象区域选择放大或缩小
        /// <para>
        /// 该接口实现3D定位功能，需要前端设备的支持。
        /// </para>
        /// </summary>
        /// <param name="lRealHandle">NET_DVR_RealPlay或者NET_DVR_RealPlay_V30的返回值 </param>
        /// <param name="pStruPointFrame">云台图像区域位置信息</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZSelZoomIn(int lRealHandle,NET_DVR_POINT_FRAME pStruPointFrame);
        /// <summary>
        /// 云台图象区域选择放大或缩小
        /// <para>
        /// 该接口实现3D定位功能，需要前端设备的支持。
        /// </para>
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        /// <param name="lChannel">通道号</param>
        /// <param name="pStruPointFrame">云台图像区域位置信息</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZSelZoomIn_EX(int lUserID,int lChannel,NET_DVR_POINT_FRAME pStruPointFrame);

        #endregion

        #region 其他操作
        /// <summary>
        /// 辅助聚焦
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lChannel">通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_FocusOnePush(int lUserID,int lChannel);
        /// <summary>
        /// 恢复镜头电机默认位置
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lChannel">通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_ResetLens( int lUserID,int lChannel);

        /// <summary>
        /// 获取设备支持的云台协议
        /// <para>
        /// 在早前的设备中规定了一系列云台协议，但在后期的设备（如DS-90xx、DS-91xx、DS-81xx等）仅保留一部分常用的协议，所以在配置前端云台协议时必须调用该接口获取当前设备支持的云台协议。
        /// </para>
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="pPtzcfg">设备的云台协议结构NET_DVR_PTZCFG</param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZProtocol(int lUserID,IntPtr pPtzcfg);
        #endregion
    }
}
