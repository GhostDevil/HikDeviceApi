using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static HikDeviceApi.HikApi;
using static HikDeviceApi.TVWall.HikTVWallStruct;
using static HikDeviceApi.HikConst;
using static HikDeviceApi.HikStruct;
using static HikDeviceApi.TVWall.HikTVWallEnum;
using static HikDeviceApi.Decoder.HikDecoderApi;
using HikDeviceApi.Decoder;
using static HikDeviceApi.VideoRecorder.HikVideoStruct;
namespace HikDeviceApi.TVWall
{
    /// <summary>
    /// 日 期:2017-06-27
    /// 作 者:痞子少爷
    /// 描 述:海康多路解码器接口代理
    /// </summary>
    public static class HikTVWallOperate
    {
        private static bool m_bInitSDK = false;
        private static uint iLastErr = 0;
        private static string strErr;
        /// <summary>
        /// 开窗宽度
        /// </summary>
        private static ushort winBaseX = 1920;
        /// <summary>
        /// 开窗高度
        /// </summary>
        private static ushort winBaseY = 1920;
        /// <summary>
        /// 设置窗口分辨率基数
        /// </summary>
        /// <param name="baseX">X分辨率</param>
        /// <param name="baseY">Y分辨率</param>
        public static void SetWinBase(ushort baseX=1920, ushort baseY = 1920)
        {
            winBaseX = baseX;
            winBaseY = baseY;
        }
        #region 登录设备
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
            return HikOperate.LoginDevice(deviceIp, userName, userPwd, ref userId, port);

        }
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool LoginOutDevice(ref int userId)
        {
            // Logout the device
            if (!HikOperate.LoginOut(ref userId))
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_Logout failed, error code= " + iLastErr; //注销登录失败，输出错误号 Failed to logout and output the error code
                return false;
            }
            return true;
        }
        #endregion

        #region 窗口参数
        /// <summary>
        /// 获取窗口参数
        /// </summary>
        /// <param name="info">窗口操作信息</param>
        /// <param name="struWinParamCfg">输出配置参数</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallGetWinParam(ref WinCtrlInfo info, ref NET_DVR_WALLWINPARAM struWinParamCfg)
        {
            uint m_dwWallNo = info.WallNo;
            uint m_dwRes = 0;
            uint m_dwWinNo = info.WinNo;

            //窗口号(组合)：1字节电视墙号+1字节保留+2字节窗口号
            uint dwWinNum = ((m_dwWallNo & 0xff) << 24) + ((m_dwRes & 0xff) << 16) + (m_dwWinNo & 0xff);

            //窗口相关参数
            struWinParamCfg = new NET_DVR_WALLWINPARAM();
            Int32 nSize = Marshal.SizeOf(struWinParamCfg);
            IntPtr lpOutBuffer = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(struWinParamCfg, lpOutBuffer, false);

            UInt32 dwReturn = 0;
            bool b = NET_DVR_GetDVRConfig(info.UserId, NET_DVR_WALLWINPARAM_GET, (int)dwWinNum, lpOutBuffer, (UInt32)nSize, ref dwReturn);
            if (!b)
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_WALLWINPARAM_GET failed, error code= " + iLastErr;
                //获取窗口相关参数失败，输出错误号 Failed to get the window parameters of device and output the error code
            }
            else
            {
                struWinParamCfg = (NET_DVR_WALLWINPARAM)Marshal.PtrToStructure(lpOutBuffer, typeof(NET_DVR_WALLWINPARAM));
            }
            Marshal.FreeHGlobal(lpOutBuffer);
            return b;
        }
        /// <summary>
        /// 设置窗口分割
        /// </summary>
        /// <param name="info">窗口操作信息</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallSetWinParam(WinCtrlInfo info)
        {
            NET_DVR_WALLWINPARAM m_struWinParamCfg = new NET_DVR_WALLWINPARAM();
            uint m_dwWallNo = info.WallNo;
            uint m_dwRes = 0;
            uint m_dwWinNo = info.WinNo;

            //窗口号(组合)：1字节电视墙号+1字节保留+2字节窗口号
            uint dwWinNum = ((m_dwWallNo & 0xff) << 24) + ((m_dwRes & 0xff) << 16) + (m_dwWinNo & 0xff);

            //窗口相关参数
            Int32 nSize = Marshal.SizeOf(m_struWinParamCfg);
            m_struWinParamCfg.dwSize = (uint)nSize;
            m_struWinParamCfg.byWinMode = byte.Parse(info.SplitNum.ToString());

            IntPtr lpInBuffer = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(m_struWinParamCfg, lpInBuffer, false);
            bool b = NET_DVR_SetDVRConfig(info.UserId, NET_DVR_WALLWINPARAM_SET, (int)dwWinNum, lpInBuffer, (UInt32)nSize);
            if (!b)
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_WALLWINPARAM_SET failed, error code= " + iLastErr;
                //设置窗口相关参数失败，输出错误号 Failed to set the window parameters of device and output the error code
            }
            Marshal.FreeHGlobal(lpInBuffer);
            return b;
        }
        /// <summary>
        /// 获取开窗
        /// </summary>
        /// <param name="info">窗口操作信息</param>
        /// <param name="struWinCfg">窗口信息</param>
        /// <param name="struWinParamCfg">窗口其他信息</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallGetWinCfg(ref WinCtrlInfo info, ref NET_DVR_VIDEOWALLWINDOWPOSITION struWinCfg, ref NET_DVR_WALLWINPARAM struWinParamCfg)
        {
            uint m_dwWallNo = info.WallNo;
            uint m_dwRes = 0;
            uint m_dwWinNo =info.WinNo;

            //窗口号(组合)：1字节电视墙号+1字节保留+2字节窗口号
            uint dwWinNum = ((m_dwWallNo & 0xff) << 24) + ((m_dwRes & 0xff) << 16) + (m_dwWinNo & 0xff);

            IntPtr lpInBuffer = Marshal.AllocHGlobal(4);
            Marshal.StructureToPtr(dwWinNum, lpInBuffer, false);

            //窗口参数
            struWinCfg = new NET_DVR_VIDEOWALLWINDOWPOSITION();
            Int32 nSize = Marshal.SizeOf(struWinCfg);
            IntPtr lpOutBuffer = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(struWinCfg, lpOutBuffer, false);

            //状态参数
            UInt32 dwStatusList = 0;
            IntPtr lpStatusList = Marshal.AllocHGlobal(4);
            Marshal.StructureToPtr(dwStatusList, lpStatusList, false);
            bool b = false;
            if (!NET_DVR_GetDeviceConfig(info.UserId, NET_DVR_GET_VIDEOWALLWINDOWPOSITION, 1, lpInBuffer, 4, lpStatusList, lpOutBuffer, (UInt32)nSize))
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_WALLWIN_GET failed, error code= " + iLastErr;
                //获取窗口参数失败，输出错误号 Failed to set the window parameters of device and output the error code
                return false;
            }
            else
            {
                struWinCfg = (NET_DVR_VIDEOWALLWINDOWPOSITION)Marshal.PtrToStructure(lpOutBuffer, typeof(NET_DVR_VIDEOWALLWINDOWPOSITION));
                //窗口在电视墙上的坐标位置
                b = WallGetWinParam(ref info, ref struWinParamCfg);
            }
            Marshal.FreeHGlobal(lpInBuffer);
            Marshal.FreeHGlobal(lpOutBuffer);
            Marshal.FreeHGlobal(lpStatusList);
            return b;
        }
        /// <summary>
        /// 开窗设置
        /// </summary>
        /// <param name="info">窗口操作信息</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallSetWinCfg(WinCtrlInfo info)
        {
            NET_DVR_VIDEOWALLWINDOWPOSITION m_struWinCfg = new NET_DVR_VIDEOWALLWINDOWPOSITION();

            uint m_dwWallNo = info.WallNo;
            uint m_dwRes = 0;
            uint m_dwWinNo = info.WinNo;

            //窗口号(组合)：1字节电视墙号+1字节保留+2字节窗口号
            uint dwWinNum = ((m_dwWallNo & 0xff) << 24) + ((m_dwRes & 0xff) << 16) + (m_dwWinNo & 0xff);

            ///////////////////////////////////////////////////////////////////////////////
            //输入参数
            NET_DVR_IN_PARAM struInputParam = new NET_DVR_IN_PARAM();
            IntPtr lpBuf = Marshal.AllocHGlobal(4);
            Marshal.StructureToPtr(dwWinNum, lpBuf, false);
            struInputParam.struCondBuf.pBuf = lpBuf; //输入参数缓冲区，开窗的电视墙号，窗口号由设备自动分配
            struInputParam.struCondBuf.nLen = 4;

            //窗口参数，包括使能、位置坐标等
            m_struWinCfg.dwSize = (uint)Marshal.SizeOf(m_struWinCfg);
            m_struWinCfg.byEnable = info.IsEnable;
            m_struWinCfg.struRect.dwXCoordinate =ushort.Parse((info.IndexX*winBaseX).ToString());
            m_struWinCfg.struRect.dwYCoordinate = ushort.Parse((info.IndexY * winBaseY).ToString());
            m_struWinCfg.struRect.dwWidth = winBaseX;
            m_struWinCfg.struRect.dwHeight = winBaseY;

            Int32 nSize = Marshal.SizeOf(m_struWinCfg);
            IntPtr lpInBuf = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(m_struWinCfg, lpInBuf, false);
            struInputParam.struInParamBuf.pBuf = lpInBuf; //输入参数缓冲区，开窗的窗口参数
            struInputParam.struInParamBuf.nLen = (uint)nSize;
            struInputParam.dwRecvTimeOut = 0; //数据接收超时时间，0表示接口默认超时

            /////////////////////////////////////////////////////////////////////////////
            //输出参数
            NET_DVR_OUT_PARAM struOutParam = new NET_DVR_OUT_PARAM();
            //输出参数缓冲区，dwCount个窗口号(组合)，dwCount为0
            IntPtr lpOutBuf = Marshal.AllocHGlobal(4);
            Marshal.StructureToPtr(dwWinNum, lpOutBuf, false);
            struOutParam.struOutBuf.pBuf = lpOutBuf;
            struOutParam.struOutBuf.nLen = 4;

            //状态参数
            UInt32 dwStatusList = 0;
            IntPtr lpStatusList = Marshal.AllocHGlobal(4);
            Marshal.StructureToPtr(dwStatusList, lpStatusList, false);
            bool b = NET_DVR_SetDeviceConfigEx(info.UserId, NET_DVR_SET_VIDEOWALLWINDOWPOSITION, 1, ref struInputParam, ref struOutParam);
            if (!b)
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_SET_VIDEOWALLWINDOWPOSITION failed, error code= " + iLastErr;
                //设置窗口参数失败，输出错误号 Failed to set the window parameters of device and output the error code
            }
            Marshal.FreeHGlobal(lpBuf);
            Marshal.FreeHGlobal(lpInBuf);
            Marshal.FreeHGlobal(lpOutBuf);
            Marshal.FreeHGlobal(lpStatusList);
            return b;
        }
        #endregion

        #region 窗口操作
        /// <summary>
        /// 窗口操作
        /// </summary>
        /// <param name="info">窗口操作信息</param>
        /// <param name="cmd">控制命令</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallWinControl(WinCtrlInfo info, WinControlCmd cmd)
        {
            uint m_dwPlatNum = info.WallNo;
            uint m_dwSubWinNo = info.SubWinNo;
            uint m_dwWinNo = info.WinNo;
            //子窗口号(组号)：1字节电视墙号+1字节子窗口号+2字节窗口号
            uint dwDispChanNum = ((m_dwPlatNum & 0xff) << 24) + ((m_dwSubWinNo & 0xff) << 16) + (m_dwWinNo & 0xff);
            bool b = NET_DVR_MatrixDiaplayControl(info.UserId, dwDispChanNum, (uint)cmd, m_dwSubWinNo);
            if (!b)
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_MatrixDiaplayControl failed, error code= " + iLastErr;
            }
            return b;
        }


        /// <summary>
        /// 窗口置底
        /// </summary>
        /// <param name="info">窗口操作信息</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool SetWinBottom(WinCtrlInfo info)
        {
            return WallSetWinDisplay(info, WinControlCmd.NET_DVR_SWITCH_WIN_BOTTOM);
        }
        /// <summary>
        /// 窗口置顶
        /// </summary>
        /// <param name="info">窗口操作信息</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallSetWinTop(WinCtrlInfo info)
        {
            return WallSetWinDisplay(info, WinControlCmd.NET_DVR_SWITCH_WIN_TOP);
        }
        /// <summary>
        /// 窗口置顶置底
        /// </summary>
        /// <param name="info">窗口操作信息</param>
        /// <param name="cmd">命令类型</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallSetWinDisplay(WinCtrlInfo info, WinControlCmd cmd)
        {
            uint m_dwWallNo = info.WallNo;
            uint m_dwRes = 0;
            uint m_dwWinNo = info.WinNo;

            //窗口号(组合)：1字节电视墙号+1字节保留+2字节窗口号
            uint dwWinNum = ((m_dwWallNo & 0xff) << 24) + ((m_dwRes & 0xff) << 16) + (m_dwWinNo & 0xff);

            //窗口相关参数
            IntPtr lpInBuffer = Marshal.AllocHGlobal(4);
            Marshal.StructureToPtr(dwWinNum, lpInBuffer, false);
            bool b = NET_DVR_RemoteControl(info.UserId, (uint)cmd, lpInBuffer, 4);
            if (!b)
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_SWITCH_WIN_BOTTOM failed, error code= " + iLastErr;
                //窗口置底失败，输出错误号 Failed to set the window down to bottom and output the error code
            }
            Marshal.FreeHGlobal(lpInBuffer);
            return b;
        }
        #endregion

        #region 电视墙输出位置
        /// <summary>
        /// 获取电视墙输出位置
        /// </summary>
        /// <param name="info">窗口操作信息</param>
        /// <param name="struWallPositionCfg">电视墙输出位置参数</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool GetWallDisplayPosition(ref WinCtrlInfo info, ref NET_DVR_VIDEOWALLDISPLAYPOSITION struWallPositionCfg)
        {
            uint m_dwDeviceNo = (info.DeviceNo & 0xff) << 24;
            uint m_dwRes = (0 & 0xff) << 16;
            uint m_dwOutputNo = info.WinNo & 0xff;

            //显示输出号(组合)：1字节设备号+1字节保留+2字节显示输出号
            uint m_dwOutputChan = m_dwDeviceNo + m_dwRes + m_dwOutputNo;

            IntPtr lpInBuffer = Marshal.AllocHGlobal(4);
            Marshal.StructureToPtr(m_dwOutputChan, lpInBuffer, false);

            UInt32 dwStatusList = 0;
            IntPtr lpStatusList = Marshal.AllocHGlobal(4);
            Marshal.StructureToPtr(dwStatusList, lpStatusList, false);

            Int32 dwOutBufferSize = Marshal.SizeOf(struWallPositionCfg);
            IntPtr ptrWallPositionCfg = Marshal.AllocHGlobal(dwOutBufferSize);
            Marshal.StructureToPtr(struWallPositionCfg, ptrWallPositionCfg, false);
            bool b = NET_DVR_GetDeviceConfig(info.UserId, NET_DVR_GET_VIDEOWALLDISPLAYPOSITION, 1, lpInBuffer, 4, lpStatusList, ptrWallPositionCfg, (UInt32)dwOutBufferSize);
            //每次获取一个显示输出口位置参数
            if (!b)
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_GET_VIDEOWALLDISPLAYPOSITION failed, error code= " + iLastErr;
                //获取电视墙屏幕参数失败，输出错误号 Failed to get the wall parameters of device and output the error code
            }
            else
            {
                struWallPositionCfg = (NET_DVR_VIDEOWALLDISPLAYPOSITION)Marshal.PtrToStructure(ptrWallPositionCfg, typeof(NET_DVR_VIDEOWALLDISPLAYPOSITION));
                info.IndexX = (ushort)(struWallPositionCfg.struRectCfg.dwXCoordinate / winBaseX);
                info.IndexY = (ushort)(struWallPositionCfg.struRectCfg.dwYCoordinate / winBaseY);
            }
            Marshal.FreeHGlobal(lpInBuffer);
            Marshal.FreeHGlobal(lpStatusList);
            Marshal.FreeHGlobal(ptrWallPositionCfg);
            return b;

        }
        /// <summary>
        /// 设置输出口位置
        /// </summary>
        /// <param name="info">窗口操作信息</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool SetWallDisplayPosition(WinCtrlInfo info)
        {
            NET_DVR_VIDEOWALLDISPLAYPOSITION m_struWallPositionCfg = new NET_DVR_VIDEOWALLDISPLAYPOSITION();
            uint m_dwDeviceNo = (info.DeviceNo & 0xff) << 24;
            uint m_dwRes = (0 & 0xff) << 16;
            uint m_dwOutputNo = info.OutNo & 0xff;

            //显示输出号(组合)：1字节设备号+1字节保留+2字节显示输出号
            uint m_dwOutputChan = m_dwDeviceNo + m_dwRes + m_dwOutputNo;

            IntPtr lpInBuffer = Marshal.AllocHGlobal(4);
            Marshal.StructureToPtr(m_dwOutputChan, lpInBuffer, false);

            UInt32 dwStatusList = 0;
            IntPtr lpStatusList = Marshal.AllocHGlobal(4);
            Marshal.StructureToPtr(dwStatusList, lpStatusList, false);

            //显示输出口参数

                m_struWallPositionCfg.byEnable = info.IsEnable;
           
            m_struWallPositionCfg.dwVideoWallNo = 1 << 24; //1字节墙号+ 1字节(保留或子窗口)+ 2字节(窗口号)，目前设备只支持一个电视墙
            m_struWallPositionCfg.dwDisplayNo = m_dwOutputChan;
            m_struWallPositionCfg.struRectCfg.dwXCoordinate = (ushort)(info.IndexX * winBaseX);
            m_struWallPositionCfg.struRectCfg.dwYCoordinate = (ushort)(info.IndexY * winBaseY);

            Int32 dwInParamBufferSize = Marshal.SizeOf(m_struWallPositionCfg);
            IntPtr ptrWallPositionCfg = Marshal.AllocHGlobal(dwInParamBufferSize);
            Marshal.StructureToPtr(m_struWallPositionCfg, ptrWallPositionCfg, false);

            bool b = NET_DVR_SetDeviceConfig(info.UserId, NET_DVR_SET_VIDEOWALLDISPLAYPOSITION, 1, lpInBuffer, 4, lpStatusList, ptrWallPositionCfg, (UInt32)dwInParamBufferSize);
            Marshal.FreeHGlobal(lpInBuffer);
            Marshal.FreeHGlobal(lpStatusList);
            Marshal.FreeHGlobal(ptrWallPositionCfg);
            if (b)
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_SET_VIDEOWALLDISPLAYPOSITION failed, error code= " + iLastErr;
                //设置电视墙屏幕参数失败，输出错误号 Failed to set the wall parameters of device and output the error code
                return false;
            }
            else
            {
                return true;
            }

        }
        #endregion

        #region 解码配置
        /// <summary>
        /// 开始主动解码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="cameraInfo">摄像头信息</param>
        /// <param name="info">窗口操作信息</param>
        /// <param name="chanType">通道类型：0-普通通道，1-零通道，2-流ID，3-本地输入源</param>
        /// <param name="transProtocol">传输协议类型0-TCP，1-UDP</param>
        /// <param name="transMode">传输码流模式 0－主码流 1－子码流</param>
        /// <param name="isStreamSever">是否使用流媒体服务</param>
        /// <param name="streamIP">流媒体服务器IP地址或者域名</param>
        /// <param name="streamPort">流媒体服务器端口</param>
        /// <param name="streamProcotol">传输协议类型 0-TCP，1-UDP</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallStartDecodeByIp(int userId, CameraInfo cameraInfo, WinCtrlInfo info, int chanType = 0, int transProtocol = 0, int transMode = 0, bool isStreamSever = false, string streamIP = "", int streamPort = 0, int streamProcotol = 0)
        {
            //子窗口号(组号)：1字节电视墙号 + 1字节子窗口号 + 2字节窗口号
            int decoderChannel = (int)(((info.WallNo & 0xff) << 24) + ((info.SubWinNo & 0xff) << 16) + (info.WinNo & 0xff));
            return (new HikDecoderOperate()).StartDecodeByIp(userId,(uint)decoderChannel, cameraInfo,chanType, transProtocol, transMode,isStreamSever, streamIP, streamPort, streamProcotol);

        }
        /// <summary>
        /// 开始主动解码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="decoderChannel">解码通道</param>
        /// <param name="cameraInfo">摄像头信息</param>
        /// <param name="info">窗口操作信息</param>
        /// <param name="chanType">通道类型：0-普通通道，1-零通道，2-流ID，3-本地输入源</param>
        /// <param name="transProtocol">传输协议类型0-TCP，1-UDP</param>
        /// <param name="transMode">传输码流模式 0－主码流 1－子码流</param>
        /// <param name="isStreamSever">是否使用流媒体服务</param>
        /// <param name="serverIP">流媒体服务器IP地址或者域名</param>
        /// <param name="serverPort">流媒体服务器端口</param>
        /// <param name="serverProcotol">传输协议类型 0-TCP，1-UDP</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallStartDecodeByIp(int userId, uint decoderChannel,CameraInfo cameraInfo, WinCtrlInfo info, int chanType = 0, int transProtocol = 0, int transMode = 0, bool isStreamSever = false, string serverIP = "", int serverPort = 0, int serverProcotol = 0)
        {
            //////子窗口号(组号)：1字节电视墙号 + 1字节子窗口号 + 2字节窗口号
            //int decoderChannel = (int)(((info.WallNo & 0xff) << 24) + ((info.SubWinNo & 0xff) << 16) + (info.WinNo & 0xff));
            return (new HikDecoderOperate()).StartDecodeByIp(userId, (uint)decoderChannel, cameraInfo, chanType, transProtocol, transMode, isStreamSever, serverIP, serverPort, serverProcotol);

        }
        /// <summary>
        /// 停止主动解码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="info">窗口操作信息</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallStopDecode(int userId, WinCtrlInfo info)
        {
            //子窗口号(组号)：1字节电视墙号 + 1字节子窗口号 + 2字节窗口号
            int decoderChannel = (int)(((info.WallNo & 0xff) << 24) + ((info.SubWinNo & 0xff) << 16) + (info.WinNo & 0xff));
            return (new HikDecoderOperate()).StopDecode(userId, (uint)decoderChannel);
        }
        /// <summary>
        /// 停止主动解码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="decoderChannel">解码通道</param>
        /// <param name="info">窗口操作信息</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallStopDecode(int userId, uint decoderChannel ,WinCtrlInfo info)
        {
            ////子窗口号(组号)：1字节电视墙号 + 1字节子窗口号 + 2字节窗口号
            //int decoderChannel = (int)(((info.WallNo & 0xff) << 24) + ((info.SubWinNo & 0xff) << 16) + (info.WinNo & 0xff));
            return (new HikDecoderOperate()).StopDecode(userId, (uint)decoderChannel);
        }

        #endregion

        #region 场景控制
        /// <summary>
        /// 场景控制
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="planNo">场景号</param>
        /// <param name="cmd">控制命令</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool WallPlanCtrl(int userId, uint planNo, PlanCmd cmd)
        {
            NET_DVR_SCENE_CONTROL_INFO struSceneCtrl = new NET_DVR_SCENE_CONTROL_INFO();
            int nSize = Marshal.SizeOf(struSceneCtrl);
            struSceneCtrl.dwSize = (uint)nSize;
            struSceneCtrl.dwCmd = (uint)cmd;

            struSceneCtrl.struVideoWallInfo.dwSize = (uint)Marshal.SizeOf(struSceneCtrl.struVideoWallInfo);
            struSceneCtrl.struVideoWallInfo.dwSceneNo = planNo;
            struSceneCtrl.struVideoWallInfo.dwWindowNo = (1 & 0xff) << 24; //窗口号(组合)：1 字节电视墙号+1 字节保留+2 字节窗口号(保留)，例如：1<<24，场景控制时只需要输入电视墙号

            IntPtr lpInBuffer = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(struSceneCtrl, lpInBuffer, false);
            bool b = NET_DVR_RemoteControl(userId, (uint)WallConfigCmd.NET_DVR_SCENE_CONTROL, lpInBuffer, (uint)nSize);
            if (!b)
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_SCREEN_SCENE_CONTROL failed, error code= " + iLastErr;
            }
            return b;
        }
        #endregion

        #region 获取信号源
        /// <summary>
        /// 获取信号源信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="signalCount">输出信号源数量</param>
        /// <param name="struInputStream">输出信号源信息列表</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool GetSignals(int userId, ref uint signalCount, ref NET_DVR_INPUTSTREAMCFG_V40[] struInputStream)
        {
            NET_DVR_INPUT_SIGNAL_LIST struSignalList = new NET_DVR_INPUT_SIGNAL_LIST();
            struSignalList.dwSize = (uint)Marshal.SizeOf(struSignalList);
            struSignalList.pBuffer = IntPtr.Zero;
            struSignalList.dwBufLen = 0;

            //获取输入信号源个数
            if (!NET_DVR_GetInputSignalList_V40(userId, 0, ref struSignalList))
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_GetInputSignalList_V40 failed, error code= " + iLastErr;
            }
            uint iSignalNum = struSignalList.dwInputSignalNums;
            signalCount = iSignalNum;
            if (iSignalNum <= 0)
            {
                return false;
            }

            //获取信号源列表详细信息
            struInputStream = new NET_DVR_INPUTSTREAMCFG_V40[iSignalNum];
            Int32 nSize = Marshal.SizeOf(typeof(NET_DVR_INPUTSTREAMCFG_V40));
            int iInputStreamSize = nSize * (int)iSignalNum;
            struSignalList.pBuffer = Marshal.AllocHGlobal(iInputStreamSize);
            struSignalList.dwBufLen = (uint)iInputStreamSize;

            //获取输入信号源
            if (!NET_DVR_GetInputSignalList_V40(userId, 0, ref struSignalList))
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_GetInputSignalList_V40 failed, error code= " + iLastErr;
            }
            for (int i = 0; i < iSignalNum; i++)
            {
                struInputStream[i] = (NET_DVR_INPUTSTREAMCFG_V40)Marshal.PtrToStructure((IntPtr)(Int32)(struSignalList.pBuffer + i * nSize), typeof(NET_DVR_INPUTSTREAMCFG_V40));
            }
            return true;
        }
        #endregion

        #region 获取输出口信息
        /// <summary>
        /// 获取输出口信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="info">输出输出口信息列表</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public static bool GetDisplayInfo(int userId, ref List<DisplayInfo> info)
        {
            info = new List<DisplayInfo>();
            /////////////////////////////////////////////////////////////////////////////////////
            //批量获取显示输出口位置配置
            uint uDisplayCount = 0;
            Int32 nWallDisplaySize = Marshal.SizeOf(typeof(NET_DVR_VIDEOWALLDISPLAYPOSITION));
            int outBufSize = 4 + 512 * (int)nWallDisplaySize;
            byte[] pTemp = new byte[outBufSize];
            IntPtr lpOutBuf = Marshal.AllocHGlobal(outBufSize);
            Marshal.Copy(pTemp, 0, lpOutBuf, outBufSize);

            //每次获取一个显示输出口位置参数
            if (!NET_DVR_GetDeviceConfig(userId, NET_DVR_GET_VIDEOWALLDISPLAYPOSITION, 0xffffffff, IntPtr.Zero, 0, IntPtr.Zero, lpOutBuf, (UInt32)outBufSize))
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_GET_VIDEOWALLDISPLAYPOSITION failed, error code= " + iLastErr;
                //获取电视墙屏幕参数失败，输出错误号 Failed to get the wall parameters of device and output the error code
                return false;
            }
            else
            {
                Marshal.Copy(lpOutBuf, pTemp, 0, (int)outBufSize);
                uDisplayCount = ((uint)pTemp[0] & 0xff)
                    | (((uint)pTemp[1] << 8) & 0xff00)
                    | (((uint)pTemp[2] << 16) & 0xff0000)
                    | (((uint)pTemp[3] << 24) & 0xff000000);
            }

            NET_DVR_VIDEOWALLDISPLAYPOSITION[] arrstruWallPos = new NET_DVR_VIDEOWALLDISPLAYPOSITION[uDisplayCount];
            for (int i = 0; i < uDisplayCount; i++)
            {
                arrstruWallPos[i] = new NET_DVR_VIDEOWALLDISPLAYPOSITION();
                arrstruWallPos[i] = (NET_DVR_VIDEOWALLDISPLAYPOSITION)Marshal.PtrToStructure((IntPtr)((Int32)lpOutBuf + i * nWallDisplaySize + 4), typeof(NET_DVR_VIDEOWALLDISPLAYPOSITION));
            }
            Marshal.FreeHGlobal(lpOutBuf);
            //////////////////////////////////////////////
            NET_DVR_DISPLAYCFG m_struDispNoCfg = new NET_DVR_DISPLAYCFG();
            //获取显示输出口号信息
            Int32 nSize = Marshal.SizeOf(m_struDispNoCfg);
            IntPtr lpOutBuffer = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(m_struDispNoCfg, lpOutBuffer, false);

            UInt32 dwReturn = 0;
            if (!NET_DVR_GetDVRConfig(userId, NET_DVR_GET_VIDEOWALLDISPLAYNO, 0, lpOutBuffer, (UInt32)nSize, ref dwReturn))
            {
                iLastErr = NET_DVR_GetLastError();
                strErr = "NET_DVR_WALLWINPARAM_GET failed, error code= " + iLastErr;
                //获取窗口相关参数失败，输出错误号 Failed to get the window parameters of device and output the error code
                return false;
            }
            else
            {
                m_struDispNoCfg = (NET_DVR_DISPLAYCFG)Marshal.PtrToStructure(lpOutBuffer, typeof(NET_DVR_DISPLAYCFG));
            }
            Marshal.FreeHGlobal(lpOutBuffer);
            //////////////////////////////////////////
            //添加显示输出口信息到列表里面
            for (int i = 0; i < uDisplayCount; i++)
            {
                uint dwDisplayNo = m_struDispNoCfg.struDisplayParam[i].dwDisplayNo;

                if (dwDisplayNo > 0)
                {
                    string displayno = Convert.ToString(dwDisplayNo);
                    string displaynoCom = Convert.ToString(dwDisplayNo >> 24) + "_" + Convert.ToString((dwDisplayNo >> 16) & 0xff) + "_" + Convert.ToString(dwDisplayNo & 0xffff); //显示输出口号组合方式显示

                    string DisplayType = "";
                    switch (m_struDispNoCfg.struDisplayParam[i].byDispChanType)
                    {
                        case 1:
                            DisplayType = "BNC";
                            break;
                        case 2:
                            DisplayType = "VGA";
                            break;
                        case 3:
                            DisplayType = "HDMI";
                            break;
                        case 4:
                            DisplayType = "DVI";
                            break;
                        case 0xff:
                            DisplayType = "无效";
                            break;
                        default:
                            DisplayType = "";
                            break;
                    }
                    string enabled = Convert.ToString(arrstruWallPos[i].byEnable);
                    string xCoordinate = Convert.ToString(arrstruWallPos[i].struRectCfg.dwXCoordinate);
                    string yCoordinate = Convert.ToString(arrstruWallPos[i].struRectCfg.dwYCoordinate);
                    info.Add(new DisplayInfo() { Displayno = displayno, DisplaynoCom = displaynoCom, XCoordinate = xCoordinate, YCoordinate = yCoordinate, Enabled = enabled, DisplayType = DisplayType });//获取的显示输出口信息
                }
            }
            return true;
        }
        #endregion

        #region 设备状态
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        public static void GetWinInfo(int userId)
        {
            //NET_DVR_WALLWIN_INFO struWallWinInfo = new NET_DVR_WALLWIN_INFO();
            //struWallWinInfo.dwSize = Marshal.SizeOf(struWallWinInfo);

            NET_DVR_WALLWIN_INFO wincfg = new NET_DVR_WALLWIN_INFO();
            Int32 nSize = Marshal.SizeOf(wincfg);
            IntPtr lpInBuffer = Marshal.AllocHGlobal(nSize);
            wincfg.dwSize = (uint)nSize;
            Marshal.StructureToPtr(wincfg, lpInBuffer, false);
            NET_DVR_WALL_WIN_STATUS[] struWallWinStatus = new NET_DVR_WALL_WIN_STATUS[] { };
            //HikApi.NET_DVR_GetDeviceStatus(userId,WallConfigCmd.NET_DVR_WALLWIN_GET,0, lpInBuffer, (uint)nSize,null,)
        }
        /// <summary>
        /// 获取分屏数量
        /// </summary>
        /// <param name="lUserID">用户id</param>
        /// <param name="dwWinNo">窗口号</param>
        /// <returns>返回分屏数量 -1失败</returns>
        public static int GetSubWinInfo(int lUserID, uint dwWinNo)
        {
            NET_DVR_WALLWINPARAM struWinWallParam = new NET_DVR_WALLWINPARAM();
            Int32 nSize = Marshal.SizeOf(struWinWallParam);
            IntPtr lpOutBuffer = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(struWinWallParam, lpOutBuffer, false);

            UInt32 dwReturn = 0;
            if (!NET_DVR_GetDVRConfig(lUserID, NET_DVR_WALLWINPARAM_GET, (int)dwWinNo, lpOutBuffer, (uint)nSize, ref dwReturn)) 
            {
                return -1;
            }
            return (struWinWallParam.byWinMode == 0) ? 1 : struWinWallParam.byWinMode;
        }
        /// <summary>
        /// 获取所有窗口号
        /// </summary>
        /// <param name="lUserID">用户id</param>
        /// <param name="byWallNo">电视墙号</param>
        /// <param name="dwWinNo">输出分屏数量</param>
        /// <returns>返回窗口信息数组</returns>
        public static NET_DVR_VIDEOWALLWINDOWPOSITION[] GetAllWinInfo(int lUserID, int byWallNo, ref uint[] dwWinNo)
        {
            dwWinNo =new uint[MAX_VM_WIN_NUM * 16];
            int iWinCount = 0;
            int iRet = 0;
            NET_DVR_VIDEOWALLWINDOWPOSITION pTemp = new NET_DVR_VIDEOWALLWINDOWPOSITION();//[MAX_WALL_WIN_COUNT * Marshal.SizeOf(new NET_DVR_VIDEOWALLWINDOWPOSITION())];
            Int32 nSize = 4+Marshal.SizeOf(pTemp);
            IntPtr lpOutBuffer = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(pTemp, lpOutBuffer, false);
            IntPtr pt = new IntPtr();
            uint dwCount = (uint)(4 + MAX_WALL_WIN_COUNT * Marshal.SizeOf(new NET_DVR_VIDEOWALLWINDOWPOSITION()));
            int dwWallNo = byWallNo;
            dwWallNo <<= 24;
            if (!NET_DVR_GetDeviceConfig(lUserID, NET_DVR_GET_VIDEOWALLWINDOWPOSITION, 0xffffffff, (IntPtr)dwWallNo, 4, IntPtr.Zero, lpOutBuffer, (UInt32)nSize))
            {
                iRet = HikOperate.GetLastError();
                return new NET_DVR_VIDEOWALLWINDOWPOSITION[0];
            }
            else
            {
                NET_DVR_VIDEOWALLWINDOWPOSITION lpWinPos1 = (NET_DVR_VIDEOWALLWINDOWPOSITION)Marshal.PtrToStructure(lpOutBuffer, typeof(NET_DVR_VIDEOWALLWINDOWPOSITION));
                NET_DVR_VIDEOWALLWINDOWPOSITION[] lpWinPos=(NET_DVR_VIDEOWALLWINDOWPOSITION[])Marshal.PtrToStructure(pt, typeof(NET_DVR_VIDEOWALLWINDOWPOSITION[]));
                int dwWinCount = lpWinPos.Length;
                 //= pt;
                for (int i = 0; i < dwWinCount; i++)
                {
                    int iSubMode = GetSubWinInfo(lUserID, lpWinPos[i].dwWindowNo);
                    if (iSubMode == -1)
                    {
                        dwWinNo[iWinCount++] = lpWinPos[i].dwWindowNo;
                        continue;
                    }
                    for (int j = 0; j < iSubMode; j++)
                    {
                        dwWinNo[iWinCount++] = (uint)(((j + 1) << 16) + lpWinPos[i].dwWindowNo);
                    };
                }
                iRet = iWinCount;
            }
            return null;
        }
        #endregion


    }
}
