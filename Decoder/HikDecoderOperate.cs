using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static HikDeviceApi.Decoder.HikDecoderStruct;
using static HikDeviceApi.Decoder.HikDecoderEnum;
using static HikDeviceApi.HikEnum;
using System.IO;
using System.Threading;
using HikDeviceApi.VideoRecorder;
using static HikDeviceApi.VideoRecorder.HikVideoEnum;
using static HikDeviceApi.HikConst;
using static HikDeviceApi.VideoRecorder.HikVideoStruct;
using static HikDeviceApi.HikStruct;

namespace HikDeviceApi.Decoder
{
    /// <summary>
    /// 日 期:2016-06-24
    /// 作 者:痞子少爷
    /// 描 述:海康多路解码器操作代理
    /// </summary>
    public class HikDecoderOperate
    {
        #region 内部成员
        /// <summary>
        /// 录像回放数据回调事件
        /// </summary>
        public event HikVideoDelegate.PLAYDATACALLBACK PlayDataCallBack;
        //private int iTotalDispChanNum = 0;
        ///// <summary>
        ///// 显示通道号
        ///// </summary>
        //public int[] iDispChanNo = new int[64];
        /// <summary>
        /// 发送被动解码数据线程组
        /// </summary>
        List<Thread> threads = new List<Thread>();
        /// <summary>
        /// 回放解码状态事件
        /// </summary>
        public event HikDecoderDelegate.PlayBackStatus PlayStatusCallBack;
        #endregion

        #region 登录解码器
        /// <summary>
        /// 登录解码器
        /// </summary>
        /// <param name="useInfo">设备使用信息</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool LoginDevice(ref DecoderDeviceInfo useInfo)
        {
            //登录设备 Login the device
            HikStruct.NET_DVR_DEVICEINFO_V30 m_struDeviceInfo = new HikStruct.NET_DVR_DEVICEINFO_V30();
            useInfo.DecoderUserId = HikApi.NET_DVR_Login_V30(useInfo.DecoderIp, (int)useInfo.DecoderPoint, useInfo.DecoderUserName, useInfo.DecoderUserPwd, ref m_struDeviceInfo);
            //int x = HikOperate.GetLastError();
            if (useInfo.DecoderUserId == -1)
                return false;
            return true;
        }
        #endregion

        #region 获取显示通道
        /// <summary>
        /// 获取显示通道
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="iDispChanNo">输出显示通道号</param>
        /// <returns>返回显示通道类型</returns>
        public List<string> GetDisplayChannel(int userId,ref int[] iDispChanNo)
        {
            NET_DVR_MATRIX_ABILITY_V41 m_struDecAbility = new NET_DVR_MATRIX_ABILITY_V41();
            int iTotalDispChanNum = 0;
            List<string> channels = new List<string>();
            try
            {
                Int32 nSize = Marshal.SizeOf(m_struDecAbility);
                IntPtr ptrDecAbility = Marshal.AllocHGlobal(nSize);
                Marshal.StructureToPtr(m_struDecAbility, ptrDecAbility, false);
                if (HikApi.NET_DVR_GetDeviceAbility(userId, MATRIXDECODER_ABILITY_V41, IntPtr.Zero, 0, ptrDecAbility, (uint)nSize))
                {
                    m_struDecAbility = (NET_DVR_MATRIX_ABILITY_V41)Marshal.PtrToStructure(ptrDecAbility, typeof(NET_DVR_MATRIX_ABILITY_V41));
                    int i = 0;
                    string str;

                    //显示通道信息 Display channel information
                    for (i = 0; i < m_struDecAbility.struVgaInfo.byChanNums; i++)
                    {
                        str = string.Format("VGA{0}", i + 1);
                        channels.Add(str);
                        iDispChanNo[iTotalDispChanNum] = m_struDecAbility.struVgaInfo.byStartChan + i;
                        iTotalDispChanNum++;
                    }
                    for (i = 0; i < m_struDecAbility.struBncInfo.byChanNums; i++)
                    {
                        str = string.Format("BNC{0}", i + 1);
                        channels.Add(str);
                        iDispChanNo[iTotalDispChanNum] = m_struDecAbility.struBncInfo.byStartChan + i;
                        iTotalDispChanNum++;
                    }
                    for (i = 0; i < m_struDecAbility.struHdmiInfo.byChanNums; i++)
                    {
                        str = string.Format("HDMI{0}", i + 1);
                        channels.Add(str);
                        iDispChanNo[iTotalDispChanNum] = m_struDecAbility.struHdmiInfo.byStartChan + i;
                        iTotalDispChanNum++;
                    }
                    for (i = 0; i < m_struDecAbility.struDviInfo.byChanNums; i++)
                    {
                        str = string.Format("DVI{0}", i + 1);
                        channels.Add(str);
                        iDispChanNo[iTotalDispChanNum] = m_struDecAbility.struDviInfo.byStartChan + i;
                        iTotalDispChanNum++;
                    }
                }
            }
            catch (Exception) { }
            return channels;
        }
        #endregion

        #region 获取解码通道
        /// <summary>
        /// 获取解码通道
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>返回支持的解码通道</returns>
        public List<string> GetDecodeChannel(int userId)
        {
            NET_DVR_MATRIX_ABILITY_V41 m_struDecAbility = new NET_DVR_MATRIX_ABILITY_V41();
            List<string> channels = new List<string>();
            Int32 nSize = Marshal.SizeOf(m_struDecAbility);
            IntPtr ptrDecAbility = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(m_struDecAbility, ptrDecAbility, false);
            if (!HikApi.NET_DVR_GetDeviceAbility(userId, MATRIXDECODER_ABILITY_V41, IntPtr.Zero, 0, ptrDecAbility, (uint)nSize))
            {
                return channels;
            }
            else
            {
                m_struDecAbility = (NET_DVR_MATRIX_ABILITY_V41)Marshal.PtrToStructure(ptrDecAbility, typeof(NET_DVR_MATRIX_ABILITY_V41));
                int i = 0;
                string str;

                //解码通道信息 Decoding channel information
                for (i = 0; i < m_struDecAbility.byDecChanNums; i++)
                {
                    str = string.Format("通道{0}", i + 1);
                    channels.Add(str);
                }
            }
            return channels;
        }
        #endregion

        #region 获取解码器能力集
        /// <summary>
        /// 获取解码器能力集
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>返回支持的解码器能力集</returns>
        public NET_DVR_MATRIX_ABILITY_V41 GetDecodeChannelInfo(int userId)
        {
            NET_DVR_MATRIX_ABILITY_V41 m_struDecAbility = new NET_DVR_MATRIX_ABILITY_V41();
            List<string> channels = new List<string>();
            Int32 nSize = Marshal.SizeOf(m_struDecAbility);
            IntPtr ptrDecAbility = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(m_struDecAbility, ptrDecAbility, false);
            HikApi.NET_DVR_GetDeviceAbility(userId, MATRIXDECODER_ABILITY_V41, IntPtr.Zero, 0, ptrDecAbility, (uint)nSize);

            m_struDecAbility = (NET_DVR_MATRIX_ABILITY_V41)Marshal.PtrToStructure(ptrDecAbility, typeof(NET_DVR_MATRIX_ABILITY_V41));

            return m_struDecAbility;
        }
        #endregion

        #region 显示通道
        /// <summary>
        /// 获取器解码通道配置
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="channel">解码通道号</param>
        /// <param name="decchan">解码通道控制参数，包括解码通道显示缩放控制和解码延时控制</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public bool GetDecChanCfg(int userId, int channel, ref NET_DVR_MATRIX_DECCHAN_CONTROL decchan)
        {
            return HikDecoderApi.NET_DVR_MatrixGetDecChanCfg(userId, (uint)channel, ref decchan);

        }

        /// <summary>
        /// 获取解码器显示通道配置
        /// </summary>
        /// <param name="userId">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dispChanNum">显示通道，从能力集获取</param>
        /// <param name="lpDisplayCfg">显示通道配置参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public bool GetDecDisplayCfg(int userId, uint dispChanNum, ref NET_DVR_MATRIX_VOUTCFG lpDisplayCfg)
        {
            return HikDecoderApi.NET_DVR_MatrixGetDisplayCfg_V41(userId, (uint)dispChanNum, ref lpDisplayCfg);

        }
        #endregion

        /// <summary>
        /// 获取解码器设备状态
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="status">设备状态参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public bool GetDecoderStatus(int userId, ref NET_DVR_DECODER_WORK_STATUS_V41 status)
        {
            return HikDecoderApi.NET_DVR_MatrixGetDeviceStatus_V41(userId, ref status);
        }

        #region 登出设备
        /// <summary>
        /// 登出设备
        /// </summary>
        /// <param name="useInfo">登陆设备返回的UseInfo对象</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool OutLoginDevice(ref DecoderDeviceInfo useInfo)
        {
            //注销登录 Logout the device
            bool b = HikApi.NET_DVR_Logout(useInfo.DecoderUserId);
            useInfo.DecoderUserId = -1;
            return b;

        }
        #endregion

        #region 开始主动解码
        /// <summary>
        /// 开始主动解码 通过IP地址或者域名从设备或者流媒体服务器取流
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="decoderChannel">解码通道</param>
        /// <param name="cameraInfo">摄像头信息</param>
        /// <param name="chanType">通道类型：0-普通通道，1-零通道，2-流ID，3-本地输入源</param>
        /// <param name="transProtocol">传输协议类型0-TCP，1-UDP</param>
        /// <param name="transMode">传输码流模式 0－主码流 1－子码流</param>
        /// <param name="isUseStreamSever">是否使用流媒体服务</param>
        /// <param name="streamIP">流媒体服务器IP地址或者域名</param>
        /// <param name="streamPort">流媒体服务器端口</param>
        /// <param name="streamProcotol">传输协议类型 0-TCP，1-UDP</param>
        /// <returns>true：成功，false：失败</returns>
        public bool StartDecodeByIp(int userId, uint decoderChannel, CameraInfo cameraInfo, int chanType = 0, int transProtocol = 0, int transMode = 0, bool isUseStreamSever = false, string streamIP = "", int streamPort = 554, int streamProcotol = 0)
        {
            try
            {
                //HikStruct.NET_DVR_IPC_PROTO_LIST m_struProtoList = new HikStruct.NET_DVR_IPC_PROTO_LIST();
                NET_DVR_DEC_STREAM_DEV_EX m_struStreamDev = new HikStruct.NET_DVR_DEC_STREAM_DEV_EX();
                //if (!HikPublicApi.NET_DVR_GetIPCProtoList(useInfo.DecoderUserId, ref m_struProtoList))
                //    return false;
                NET_DVR_PU_STREAM_CFG_V41 m_struStreamCfgV41 = new NET_DVR_PU_STREAM_CFG_V41();
                m_struStreamCfgV41.dwSize = (uint)Marshal.SizeOf(m_struStreamCfgV41);
                m_struStreamCfgV41.byStreamMode = 1;

                //通过IP地址或者域名从设备或者流媒体服务器取流
                if (m_struStreamCfgV41.byStreamMode == 1)
                {
                    m_struStreamDev.struDevChanInfo.byChanType = (byte)chanType;
                    if (m_struStreamDev.struDevChanInfo.byChanType == 2)
                    {
                        m_struStreamDev.struDevChanInfo.byStreamId = cameraInfo.DvrChannel.ToString();
                    }
                    else
                    {
                        m_struStreamDev.struDevChanInfo.dwChannel = uint.Parse(cameraInfo.DvrChannel.ToString());
                        m_struStreamDev.struDevChanInfo.byChannel = byte.Parse(cameraInfo.DvrChannel.ToString());

                    }
                    m_struStreamDev.struDevChanInfo.byAddress = cameraInfo.DvrIp;
                    m_struStreamDev.struDevChanInfo.wDVRPort = (ushort)cameraInfo.DvrPort;
                    m_struStreamDev.struDevChanInfo.byTransProtocol = (byte)transProtocol;
                    m_struStreamDev.struDevChanInfo.byTransMode = (byte)transMode;
                    m_struStreamDev.struDevChanInfo.byFactoryType = (byte)cameraInfo.FactoryType;//(byte)m_struProtoList.struProto[factoryType].dwType;
                    m_struStreamDev.struDevChanInfo.sUserName = cameraInfo.DvrUserName;
                    m_struStreamDev.struDevChanInfo.sPassword = cameraInfo.DvrUserPwd;
                    if (isUseStreamSever)
                    {
                        m_struStreamDev.struStreamMediaSvrCfg.byValid = 1;
                        m_struStreamDev.struStreamMediaSvrCfg.byAddress = streamIP;
                        m_struStreamDev.struStreamMediaSvrCfg.wDevPort = ushort.Parse(streamPort.ToString());
                        m_struStreamDev.struStreamMediaSvrCfg.byTransmitType = (byte)streamProcotol;
                    }
                    else
                    {
                        m_struStreamDev.struStreamMediaSvrCfg.byValid = 0;
                    }

                    uint dwUnionSize = (uint)Marshal.SizeOf(m_struStreamCfgV41.uDecStreamMode);
                    IntPtr ptrStreamUnion = Marshal.AllocHGlobal((Int32)dwUnionSize);
                    Marshal.StructureToPtr(m_struStreamDev, ptrStreamUnion, false);
                    m_struStreamCfgV41.uDecStreamMode = (NET_DVR_DEC_STREAM_MODE)Marshal.PtrToStructure(ptrStreamUnion, typeof(HikDeviceApi.Decoder.HikDecoderStruct.NET_DVR_DEC_STREAM_MODE));
                    Marshal.FreeHGlobal(ptrStreamUnion);
                }

                if (!HikDecoderApi.NET_DVR_MatrixStartDynamic_V41(userId, decoderChannel, ref m_struStreamCfgV41))
                {
                    int x = HikOperate.GetLastError();
                    return false;
                }
            }
            catch (Exception) { return false; }
            return true;
        }
        ///// <summary>
        ///// 开始主动解码
        ///// </summary>
        ///// <param name="userId">用户id</param>
        ///// <param name="decoderChannel">解码通道</param>
        ///// <param name="cameraInfo">摄像头信息</param>
        ///// <param name="chanType">通道类型：0-普通通道，1-零通道，2-流ID，3-本地输入源</param>
        ///// <param name="transProtocol">传输协议类型0-TCP，1-UDP</param>
        ///// <param name="transMode">传输码流模式 0－主码流 1－子码流</param>
        ///// <param name="isStreamSever">是否使用流媒体服务</param>
        ///// <param name="streamIP">流媒体服务器IP地址或者域名</param>
        ///// <param name="streamPort">流媒体服务器端口</param>
        ///// <param name="streamProcotol">传输协议类型 0-TCP，1-UDP</param>
        ///// <returns>true：成功，false：失败</returns>
        //public bool StartDecodeByDDNS(int userId, uint decoderChannel, CameraInfo cameraInfo, int chanType = 0, int transProtocol = 0, int transMode = 0, bool isStreamSever = false, string streamIP = "", int streamPort = 0, int streamProcotol = 0)
        //{
        //    try
        //    {
        //        NET_DVR_DEC_DDNS_DEV m_struStreamDDNS = new NET_DVR_DEC_DDNS_DEV();
        //        //HikStruct.NET_DVR_IPC_PROTO_LIST m_struProtoList = new HikStruct.NET_DVR_IPC_PROTO_LIST();
        //        HikStruct.NET_DVR_DEC_STREAM_DEV_EX m_struStreamDev = new HikStruct.NET_DVR_DEC_STREAM_DEV_EX();
        //        //if (!HikPublicApi.NET_DVR_GetIPCProtoList(useInfo.DecoderUserId, ref m_struProtoList))
        //        //    return false;
        //        NET_DVR_PU_STREAM_CFG_V41 m_struStreamCfgV41 = new NET_DVR_PU_STREAM_CFG_V41();
        //        m_struStreamCfgV41.dwSize = (uint)Marshal.SizeOf(m_struStreamCfgV41);
        //        m_struStreamCfgV41.byStreamMode = 3;
        //        ////通过URL从设备或者流媒体服务器取流
        //        //if (m_struStreamCfgV41.byStreamMode == 2)
        //        //{
        //        //    m_struStreamURL.byEnable = 1;
        //        //    m_struStreamURL.strURL = textBoxURL.Text;

        //        //    uint dwUnionSize = (uint)Marshal.SizeOf(m_struStreamCfgV41.uDecStreamMode);
        //        //    IntPtr ptrStreamUnion = Marshal.AllocHGlobal((Int32)dwUnionSize);
        //        //    Marshal.StructureToPtr(m_struStreamURL, ptrStreamUnion, false);
        //        //    m_struStreamCfgV41.uDecStreamMode = (CHCNetSDK.NET_DVR_DEC_STREAM_MODE)Marshal.PtrToStructure(ptrStreamUnion, typeof(CHCNetSDK.NET_DVR_DEC_STREAM_MODE));
        //        //    Marshal.FreeHGlobal(ptrStreamUnion);
        //        //}

        //        //通过动态域名解析从设备或者流媒体服务器取流
        //        if (m_struStreamCfgV41.byStreamMode == 3)
        //        {
        //            m_struStreamDDNS.struDdnsInfo.byChanType = (byte)chanType;
        //            if (m_struStreamDDNS.struDdnsInfo.byChanType == 2)
        //            {
        //                m_struStreamDDNS.struDdnsInfo.byStreamId = cameraInfo.DvrChannel.ToString();
        //            }
        //            else
        //            {
        //                m_struStreamDDNS.struDdnsInfo.dwChannel = uint.Parse(cameraInfo.DvrChannel.ToString()); ;

        //            }
        //            m_struStreamDev.struDevChanInfo.byAddress = cameraInfo.DvrIp;
        //            m_struStreamDev.struDevChanInfo.wDVRPort = (ushort)cameraInfo.DvrPort;
        //            m_struStreamDev.struDevChanInfo.byTransProtocol = (byte)transProtocol;
        //            m_struStreamDev.struDevChanInfo.byTransMode = (byte)transMode;
        //            m_struStreamDev.struDevChanInfo.byFactoryType = (byte)cameraInfo.FactoryType;//(byte)m_struProtoList.struProto[factoryType].dwType;
        //            m_struStreamDev.struDevChanInfo.sUserName = cameraInfo.DvrUserName;
        //            m_struStreamDev.struDevChanInfo.sPassword = cameraInfo.DvrUserPwd;

        //            m_struStreamDDNS.struDdnsInfo.byDevAddress = textBoxDevDomain.Text;
        //            m_struStreamDDNS.struDdnsInfo.byTransProtocol = (byte)comboBoxTransType_D.SelectedIndex;
        //            m_struStreamDDNS.struDdnsInfo.byTransMode = (byte)comboBoxStreamType_D.SelectedIndex;
        //            m_struStreamDDNS.struDdnsInfo.byDdnsType = 4;
        //            m_struStreamDDNS.struDdnsInfo.byDdnsAddress = textBoxDDNSAddr.Text;
        //            m_struStreamDDNS.struDdnsInfo.wDdnsPort = ushort.Parse(textBoxDDNSPort.Text);
        //            m_struStreamDDNS.struDdnsInfo.byFactoryType = (byte)cameraInfo.FactoryType;
        //            m_struStreamDDNS.struDdnsInfo.sUserName = cameraInfo.DvrUserName;
        //            m_struStreamDDNS.struDdnsInfo.sPassword = cameraInfo.DvrUserPwd;
        //            m_struStreamDDNS.struDdnsInfo.wDevPort = ushort.Parse(textBoxDevPort_D.Text);

        //            if (isStreamSever)
        //            {
        //                m_struStreamDDNS.struMediaServer.byValid = 1;
        //                m_struStreamDDNS.struMediaServer.byAddress = streamIP;
        //                m_struStreamDDNS.struMediaServer.wDevPort = ushort.Parse(streamPort.ToString());
        //                m_struStreamDDNS.struMediaServer.byTransmitType = (byte)streamProcotol;
        //            }
        //            else
        //            {
        //                m_struStreamDDNS.struMediaServer.byValid = 0;
        //            }

        //            uint dwUnionSize = (uint)Marshal.SizeOf(m_struStreamCfgV41.uDecStreamMode);
        //            IntPtr ptrStreamUnion = Marshal.AllocHGlobal((Int32)dwUnionSize);
        //            Marshal.StructureToPtr(m_struStreamDDNS, ptrStreamUnion, false);
        //            m_struStreamCfgV41.uDecStreamMode = (NET_DVR_DEC_STREAM_MODE)Marshal.PtrToStructure(ptrStreamUnion, typeof(NET_DVR_DEC_STREAM_MODE));
        //            Marshal.FreeHGlobal(ptrStreamUnion);
        //        }

        //        if (!HikDecoderApi.NET_DVR_MatrixStartDynamic_V41(userId, decoderChannel, ref m_struStreamCfgV41))
        //        {
        //            int x = HikOperate.GetLastError();
        //            return false;
        //        }
        //    }
        //    catch (Exception) { return false; }
        //    return true;
        //}
        #endregion

        #region 停止主动解码
        /// <summary>
        /// 停止主动解码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="decoderChannel">解码通道</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool StopDecode(int userId, uint decoderChannel)
        {
            if (!HikDecoderApi.NET_DVR_MatrixStopDynamic(userId, decoderChannel))
            {
                //uint iLastErr = HikDecoderApi.NET_DVR_GetLastError();
                return false;
            }
            return true;
        }
        #endregion

        #region 开始被动解码
        /// <summary>
        /// 开始被动解码
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象，需要参数：解码通道</param>
        /// <param name="type">通讯方式</param>
        /// <param name="port">TCP或者UDP端口，TCP时端口默认是8000，不同的解码通道UDP端口号需设置为不同的值 </param>
        /// <param name="stream">数据类型</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool StartPassiveDecode(ref DecoderDeviceInfo info, TransProtol type = TransProtol.TCP, int port = 8000, StreamDataType stream = StreamDataType.FileStream)
        {
            NET_DVR_MATRIX_PASSIVEMODE m_struPassivePara = new NET_DVR_MATRIX_PASSIVEMODE() { wTransProtol = (ushort)type /*传输协议：0-TCP，1-UDP，2-MCAST*/, wPassivePort = ushort.Parse(port.ToString()) /*TCP或者UDP端口*/, byStreamType = (byte)(stream) /*数据播放模式: 1-实时流, 2-文件流*/ };
            info.ControlInfo.PassiveHandle = HikDecoderApi.NET_DVR_MatrixStartPassiveDecode(info.DecoderUserId, (uint)info.ControlInfo.DecoderChannel, ref m_struPassivePara);
            PlayDataCallBack += DataCallBack;

            if (info.ControlInfo.PassiveHandle == -1)
                return false;
            else
                return true;
        }
        #endregion

        #region 停止被动解码
        /// <summary>
        /// 停止被动解码
        /// </summary>
        /// <param name="passiveId">被动解码Id</param>
        /// <param name="dvrUserId">Dvr用户Id</param>
        /// <param name="type">数据类型</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool StopPassiveDecode(int passiveId, int dvrUserId = -1, StreamDataType type = StreamDataType.FileStream)
        {
            switch (type)
            {
                case StreamDataType.RealTimeStream:
                    //停止被动解码 Stop the passive decoding
                    if (!HikDecoderApi.NET_DVR_MatrixStopPassiveDecode(passiveId))
                    {

                        return false;
                    }

                    return true;
                case StreamDataType.FileStream:
                    if (threads.Count > 0)
                        threads.Find(o => o.Name == dvrUserId.ToString()).Abort();
                    //停止被动解码 Stop the passive decoding
                    if (!HikDecoderApi.NET_DVR_MatrixStopPassiveDecode(passiveId))
                    {
                        if (!HikDecoderApi.NET_DVR_MatrixStopPassiveDecode(passiveId))
                            return false;


                    }
                    return true;
            }
            return false;
        }
        #endregion

        #region 发送被动解码数据（回放流）
        /// <summary>
        /// 发送被动解码数据（回放流）
        /// </summary>
        /// <param name="dvrUserId">DVR用户ID</param>
        /// <param name="dvrChannel">DVR通道</param>
        /// <param name="passiveId">被动解码ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool SendDataByPlayBack(int dvrUserId, int dvrChannel, int passiveId, DateTime startTime, DateTime endTime)
        {
            if (dvrUserId <= -1)
            {
                return false;
            }
            else
            {
                HikStruct.NET_DVR_TIME ss = new HikStruct.NET_DVR_TIME() { dwYear = (uint)startTime.Year, dwMonth = (uint)startTime.Month, dwDay = (uint)startTime.Day, dwHour = (uint)startTime.Hour, dwMinute = (uint)startTime.Minute, dwSecond = (uint)startTime.Second };
                HikStruct.NET_DVR_TIME sss = new HikStruct.NET_DVR_TIME() { dwYear = (uint)endTime.Year, dwMonth = (uint)endTime.Month, dwDay = (uint)endTime.Day, dwHour = (uint)endTime.Hour, dwMinute = (uint)endTime.Minute, dwSecond = (uint)endTime.Second };

                int x = HikVideoApi.NET_DVR_PlayBackByTime(dvrUserId, dvrChannel, ref ss, ref sss, IntPtr.Zero);
                HikVideoApi.NET_DVR_SetPlayDataCallBack(x, PlayDataCallBack, (uint)passiveId);
                //passiveNum = passiveId;
                uint iOutValue = 0;
                if (HikVideoApi.NET_DVR_PlayBackControl_V40(x, (int)PlayBackControlCmd.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
                {
                    List<object> obj = new List<object>() { x, passiveId, dvrUserId };

                    (new Thread(new ParameterizedThreadStart(GetPlayBackProgress))).Start(obj);
                }
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 获取回放进度
        /// </summary>
        /// <param name="obj"></param>
        private void GetPlayBackProgress(object obj)
        {
            List<object> objs = obj as List<object>;
            uint iOutValue = 0;
            int iPos = 0;
            int m_lPlayHandle = (int)objs[0];

            while (true)
            {
                IntPtr lpOutBuffer = Marshal.AllocHGlobal(4);
                //获取回放进度
                HikVideoApi.NET_DVR_PlayBackControl_V40(m_lPlayHandle, (int)PlayBackControlCmd.NET_DVR_PLAYGETPOS, IntPtr.Zero, 0, lpOutBuffer, ref iOutValue);

                iPos = (int)Marshal.PtrToStructure(lpOutBuffer, typeof(int));

                if (iPos == 100)  //回放结束
                {
                    if (HikVideoApi.NET_DVR_StopPlayBack(m_lPlayHandle))
                    {
                        break;
                    }
                    StopPassiveDecode((int)objs[1], dvrUserId: (int)objs[2], type: StreamDataType.RealTimeStream);
                    m_lPlayHandle = -1;
                }
                if (iPos == 200) //网络异常，回放失败
                {
                    break;
                }
                Marshal.FreeHGlobal(lpOutBuffer);
                Thread.Sleep(50);
            }


        }
        /// <summary>
        /// 录像数据回调函数 
        /// </summary>
        /// <param name="lPlayHandle">当前的录像播放句柄</param>
        /// <param name="dwDataType">数据类型</param>
        /// <param name="pBuffer">存放数据的缓冲区指针</param>
        /// <param name="dwBufSize">缓冲区大小</param>
        /// <param name="dwUser">用户数据</param>
        private void DataCallBack(int lPlayHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser)
        {

            //此处代码仅供参考，实际应用时回调中获取实时流数据后需要在回调外面处理或处理数据
            sendData.Enqueue(new CallBackData() { lPlayHandle = lPlayHandle, dwBufSize = dwBufSize, dwDataType = dwDataType, dwUser = dwUser, pBuffer = pBuffer });
            //if (!HikDecoderApi.NET_DVR_MatrixSendData((int)dwUser, pBuffer, dwBufSize))
            //{
            //    //发送失败 Failed to send data to the decoder
            //    HikDecoderApi.NET_DVR_MatrixSendData((int)dwUser, pBuffer, dwBufSize);
            //}
        }
        private class CallBackData
        {
            public int lPlayHandle;
            public uint dwDataType;
            public IntPtr pBuffer;
            public uint dwBufSize;
            public uint dwUser;
        }
        Queue<CallBackData> sendData = new Queue<CallBackData>();
        private void SendCallBackData()
        {
            while (true)
            {
                if (sendData.Count > 0)
                {
                    CallBackData data = sendData.Dequeue();
                    // List<object> objs = obj as List<object>;
                    uint dwUser = data.dwUser;
                    IntPtr pBuffer = data.pBuffer;
                    uint dwBufSize = data.dwBufSize;
                    if (!HikDecoderApi.NET_DVR_MatrixSendData((int)dwUser, pBuffer, dwBufSize))
                    {
                        //发送失败 Failed to send data to the decoder
                        HikDecoderApi.NET_DVR_MatrixSendData((int)dwUser, pBuffer, dwBufSize);
                    }
                }
                Thread.Sleep(50);
            }

        }
        #endregion

        #region 发送被动解码数据（文件流）
        /// <summary>
        /// 发送被动解码数据（文件流）
        /// </summary>
        /// <param name="info">执行被动解码时的UseInfo对象,需要DVR信息</param>
        /// <param name="cameraInfo">摄像头信息</param>
        /// <param name="filePath">文件地址</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool SendPassiveDataByFile(ref DecoderDeviceInfo info, CameraInfo cameraInfo, string filePath)
        {
            List<object> ob = new List<object>();
            ob.Add(info);
            FileStream hFileHandle = new FileStream(filePath, FileMode.Open, FileAccess.Read); //打开文件 Open the file         
            ob.Add(hFileHandle);
            ob.Add(filePath);
            Thread th = new Thread(new ParameterizedThreadStart(FileThreadTask)) { IsBackground = true, Name = cameraInfo.DvrUserId.ToString() };
            threads.Add(th);
            th.Start(ob);
            if (info.ControlInfo.FileStreamLength < 1 || info.ControlInfo.FileStreamLength > 512)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送文件流
        /// </summary>
        /// <param name="lHandle"></param>
        private void FileThreadTask(object lHandle)
        {
            //该线程数据处理方式仅供参考，实际使用时请自行优化

            List<object> obj = lHandle as List<object>;
            DecoderDeviceInfo info = obj[0] as DecoderDeviceInfo;
            FileStream fs = obj[1] as FileStream;
            string path = obj[2].ToString();
            long left = fs.Length;
            byte[] tmpFile = new byte[info.ControlInfo.FileStreamLength * 1024];
            int maxLength = tmpFile.Length;
            int istart = 0;
            int iRealSize = 0;

            while (left > 0)
            {
                try
                {
                    fs.Position = istart;
                    iRealSize = 0;
                    if (left < maxLength)
                        iRealSize = fs.Read(tmpFile, 0, Convert.ToInt32(left));
                    else
                        iRealSize = fs.Read(tmpFile, 0, maxLength);

                    if (iRealSize == 0)
                        break;

                    istart += iRealSize;
                    left -= iRealSize;
                    //将读取到的文件数据发送给解码器 
                    IntPtr pBuffer = Marshal.AllocHGlobal((Int32)iRealSize);
                    Marshal.Copy(tmpFile, 0, pBuffer, iRealSize);
                    if (!HikDecoderApi.NET_DVR_MatrixSendData(info.ControlInfo.PassiveHandle, pBuffer, (uint)iRealSize))
                    {
                        //while (!HikDecoderApi.NET_DVR_MatrixSendData(info.PassiveHandle, pBuffer, (uint)iRealSize))
                        //{
                        //    break;
                        //}
                    }
                    Marshal.FreeHGlobal(pBuffer);
                    Thread.Sleep(10);
                }
                catch (Exception) { }
            }
            try
            {
                fs.Close();
            }
            catch (Exception) { }
        }
        #endregion

        #region 预览回调
        /// <summary>
        /// 预览回调
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwDataType"></param>
        /// <param name="pBuffer"></param>
        /// <param name="dwBufSize"></param>
        /// <param name="pUser"></param>
        private void RealDataCallBack(int lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, IntPtr pUser)
        {
            //此处代码仅供参考，实际应用时回调中获取实时流数据后需要在回调外面处理或处理数据
            //if (!HikDecoderApi.NET_DVR_MatrixSendData(lPassiveHandle, pBuffer, dwBufSize))
            //{
            //    //发送失败 Failed to send data to the decoder
            //}
        }

        #endregion

        #region 远程回放控制
        /// <summary>
        /// 设置解码设备远程回放文件配置（按时间）
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象，需要Dvr信息</param>
        /// <param name="cameraInfo">摄像头信息</param>
        /// <param name="startTime">回放开始时间</param>
        /// <param name="endTime">回放结束时间</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool SetRemotePlay(ref DecoderDeviceInfo info, CameraInfo cameraInfo, DateTime startTime, DateTime endTime)
        {
            HikDecoderStruct.NET_DVR_MATRIX_DEC_REMOTE_PLAY remote = new NET_DVR_MATRIX_DEC_REMOTE_PLAY() { dwPlayMode = 1, sDVRIP = cameraInfo.DvrIp, wDVRPort = (ushort)cameraInfo.DvrPort, byChannel = byte.Parse(cameraInfo.DvrChannel.ToString()), /*remote.sUserName = Encoding.UTF8.GetBytes(info.DvrUserName);*/sUserName = cameraInfo.DvrUserName, sPassword = cameraInfo.DvrUserPwd, StartTime = new HikStruct.NET_DVR_TIME() { dwYear = (uint)startTime.Year, dwMonth = (uint)startTime.Month, dwDay = (uint)startTime.Day, dwHour = (uint)startTime.Hour, dwMinute = (uint)startTime.Minute, dwSecond = (uint)startTime.Second }, StopTime = new HikStruct.NET_DVR_TIME() { dwYear = (uint)endTime.Year, dwMonth = (uint)endTime.Month, dwDay = (uint)endTime.Day, dwHour = (uint)endTime.Hour, dwMinute = (uint)endTime.Minute, dwSecond = (uint)endTime.Second }, byReserve = 0 };
            uint dwSize = (uint)Marshal.SizeOf(remote);
            IntPtr deviceInfo = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(remote, deviceInfo, false);
            remote.dwSize = dwSize;
            return HikDecoderApi.NET_DVR_MatrixSetRemotePlay(info.DecoderUserId, (uint)info.ControlInfo.DecoderChannel, ref remote);
        }
        /// <summary>
        /// 设置解码设备远程回放文件配置（按文件）
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象，需要Dvr信息</param>
        /// <param name="cameraInfo">摄像头信息</param>
        /// <param name="filePath">回放文件地址</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool SetRemotePlay(ref DecoderDeviceInfo info, CameraInfo cameraInfo, string filePath)
        {
            HikDecoderStruct.NET_DVR_MATRIX_DEC_REMOTE_PLAY remote = new NET_DVR_MATRIX_DEC_REMOTE_PLAY() { dwPlayMode = 0, sFileName = filePath, sDVRIP = cameraInfo.DvrIp, wDVRPort = (ushort)cameraInfo.DvrPort, /*remote.sUserName = Encoding.UTF8.GetBytes(info.DvrUserName);*/sUserName = cameraInfo.DvrUserName, sPassword = cameraInfo.DvrUserPwd, /*remote.StartTime = new NET_DVR_TIME() { dwYear = (uint)startTime.Year, dwMonth = (uint)startTime.Month, dwDay = (uint)startTime.Day, dwHour = (uint)startTime.Hour, dwMinute = (uint)startTime.Minute, dwSecond = (uint)startTime.Second };*/ /*remote.StopTime = new NET_DVR_TIME() { dwYear = (uint)endTime.Year, dwMonth = (uint)endTime.Month, dwDay = (uint)endTime.Day, dwHour = (uint)endTime.Hour, dwMinute = (uint)endTime.Minute, dwSecond = (uint)endTime.Second };*/byReserve = 0 };
            uint dwSize = (uint)Marshal.SizeOf(remote);
            IntPtr deviceInfo = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(remote, deviceInfo, false);
            remote.dwSize = dwSize;
            return HikDecoderApi.NET_DVR_MatrixSetRemotePlay(info.DecoderUserId, (uint)info.ControlInfo.DecoderChannel, ref remote);
        }
        /// <summary>
        /// 远程回放文件控制
        /// </summary>
        /// <param name="info">设置解码设备远程回放时的UseInfo对象，需要解码信息</param>
        /// <param name="control">回放控制命令</param>
        /// <param name="dwInValue">设置参数</param>
        /// <param name="lpOutValue">获取到的参数指针</param>
        /// <param name="isGetPlayStatus">是否获取回放状态,使用时需要初始化PlayStatusCallBack事件,开始播放时参数有效</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>
        /// 接口中的dwInValue和lpOutValue参数根据不同的命令号决定是否输入和输出，如当进行NET_DVR_PLAYSETPOS命令操作时，需要对dwInValue参数进行赋值。
        /// </remarks>
        public bool PlayBackControl(ref DecoderDeviceInfo info, RemotePlayControl control, ref uint lpOutValue, int dwInValue = 0, bool isGetPlayStatus = false)
        {
            bool b = HikDecoderApi.NET_DVR_MatrixSetRemotePlayControl(info.DecoderUserId, (uint)info.ControlInfo.DecoderChannel, (uint)control, (uint)dwInValue, ref lpOutValue);
            if (isGetPlayStatus && control == RemotePlayControl.NET_DVR_PLAYSTART)
                (new Thread(new ParameterizedThreadStart(GetPlayBackStatus)) { IsBackground = true }).Start(info);
            return b;
        }

        private void GetPlayBackStatus(object obj)
        {
            DecoderDeviceInfo info = (DecoderDeviceInfo)obj;
            NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS status = new NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS();
            while (true)
            {
                if (GetRemotePlayStatus(info.DecoderUserId,(uint)info.ControlInfo.DecoderChannel, ref status))
                {
                    if (PlayStatusCallBack != null)
                        PlayStatusCallBack(status, info.DecoderIp, info.ControlInfo.DecoderChannel);
                    else
                        break;
                }
                if (status.dwCurMediaFileDuration == status.dwCurPlayTime || status.dwCurDataType == 21)
                    break;
            }


        }

        /// <summary>
        /// 获取回放状态
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="decoderChannel">解码通道</param>
        /// <param name="status">回放状态参数结构</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public bool GetRemotePlayStatus(int userId, uint decoderChannel, ref NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS status)
        {
            return HikDecoderApi.NET_DVR_MatrixGetRemotePlayStatus(userId, decoderChannel, ref status);
        }
        #endregion

        #region 解码器获取显示通道配置
        /// <summary>
        /// 解码器获取显示通道配置
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="decoderChannel">解码通道</param>
        /// <param name="m_struDisplayCfg">显示通道配置信息结构体（输出）</param>
        /// <returns>true:成功，false:失败</returns>
        public bool GetDisplayConfig(int userId, uint decoderChannel, ref NET_DVR_MATRIX_VOUTCFG m_struDisplayCfg)
        {
            return HikDecoderApi.NET_DVR_MatrixGetDisplayCfg_V41(userId, decoderChannel, ref m_struDisplayCfg);

        }
        #endregion

        #region 解码器显示通道配置
        /// <summary>
        /// 解码器显示通道配置
        /// </summary>
        /// <param name="userId">用户的id</param>
        /// <param name="decoderChannel">解码通道</param>
        /// <param name="winMode">画面分隔模式</param>
        /// <param name="chanType">通道类型</param>
        /// <param name="subWindowNo">子窗口号，nulll为不设置</param>
        /// <param name="subWindowChannel">子窗口关联解码通道号，null为不设置</param>
        /// <param name="videoForm">视频制式</param>
        /// <param name="res">分辨率</param>
        /// <param name="scaleMode">显示模式 0---真实显示，1---缩放显示  ( 针对BNC )</param>
        /// <param name="isOpenAudio">是否开启音频</param>
        /// <param name="audioWindosNo">音频子窗口</param>
        /// <returns>true:成功，false:失败</returns>
        public bool SetDisplayConfig(int userId, uint decoderChannel, int winMode, DisplayChanType chanType, int[] subWindowNo = null, int[] subWindowChannel = null, VedioStandard videoForm = VedioStandard.PAL, Resolution res = Resolution._1080P_60HZ, int scaleMode = 0, bool isOpenAudio = false, int audioWindosNo = 1)
        {
            NET_DVR_MATRIX_VOUTCFG m_struDisplayCfg = new NET_DVR_MATRIX_VOUTCFG();
            if (!GetDisplayConfig(userId, decoderChannel,ref m_struDisplayCfg))
            {
                int x = HikOperate.GetLastError();
                return false;
            }
            m_struDisplayCfg.byDispChanType = (byte)((int)chanType);
            //画面分隔
            m_struDisplayCfg.dwWindowMode = uint.Parse(winMode.ToString());
            if (subWindowChannel != null && subWindowNo != null)//子窗口设置
            {
                if (subWindowChannel.Length == 0 || subWindowNo.Length == 0)
                    return false;
                int x = 0;
                m_struDisplayCfg.byJoinDecChan = new byte[m_struDisplayCfg.byJoinDecChan.Length];
                foreach (int item in subWindowNo)
                {
                    m_struDisplayCfg.byJoinDecChan[item - 1] = (byte)(subWindowChannel[x]);
                    x++;
                }

            }

            //音频设置
            if (!isOpenAudio)
            {
                m_struDisplayCfg.byAudio = 0;
            }
            else
            {
                m_struDisplayCfg.byAudio = 1;
                m_struDisplayCfg.byAudioWindowIdx = (byte)(audioWindosNo);
            }
            m_struDisplayCfg.byVedioFormat = (byte)((int)videoForm);
            m_struDisplayCfg.dwResolution = (uint)res;
            m_struDisplayCfg.byScale = (byte)scaleMode;
            m_struDisplayCfg.byJoinDecChan = new byte[m_struDisplayCfg.byJoinDecChan.Length];
            if (subWindowChannel != null && subWindowNo != null)//子窗口设置
            {
                if (subWindowChannel.Length == 0 || subWindowNo.Length == 0)
                    return false;
                int x = 0;

                foreach (int item in subWindowNo)
                {
                    m_struDisplayCfg.byJoinDecChan[item - 1] = (byte)(subWindowChannel[x]);
                    x++;
                }

            }
            if (HikDecoderApi.NET_DVR_MatrixSetDisplayCfg_V41(userId, decoderChannel, ref m_struDisplayCfg))
            {
                //struDisplayCfg = m_struDisplayCfg;
                return true;
            }
            else
                return false;
        }
        #endregion

        #region 窗口大小改变
        /// <summary>
        /// 窗口大小改变
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="decoderChannel">解码通道</param>
        /// <param name="subWinNo">子窗口号</param>
        /// <param name="showType">1-显示通道放大某个窗口，2-显示通道窗口还原</param>
        /// <returns>true:成功，false:失败</returns>
        public bool WindowChange(int userId, uint decoderChannel, int subWinNo, int showType)
        {
            if (HikDecoderApi.NET_DVR_MatrixDiaplayControl(userId, decoderChannel, (uint)showType, (uint)subWinNo))
                return true;
            else
                return false;
        }
        #endregion

        #region 场景切换控制
        /// <summary>
        /// 场景切换控制
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="sceneNum">场景编号</param>
        /// <param name="scenecmd">场景操作命令</param>
        /// <returns>true：成功，false：失败</returns>
        public bool GetCurrentSceneMode(int userId, int sceneNum, SceneCmd scenecmd)
        {
            return HikDecoderApi.NET_DVR_MatrixSceneControl(userId, (uint)sceneNum, (uint)scenecmd, 0);
        }
        #endregion

        #region 获取当前正在使用的场景模式
        /// <summary>
        /// 获取当前正在使用的场景模式
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="sceneNum">场景编号</param>
        /// <returns>true：成功，false：失败</returns>
        public bool GetCurrentSceneMode(int userId, uint sceneNum)
        {
            return HikDecoderApi.NET_DVR_MatrixGetCurrentSceneMode(userId, ref sceneNum);
        }
        #endregion

        #region LOGO控制
        /// <summary>
        /// 上传LOGO。Logo要求bmp格式，24位图，支持分辨率：32*32 ~ 256*128。
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="decoderChannel">解码通道号</param>
        /// <param name="corordinateX">显示x坐标</param>
        /// <param name="corordinateY">显示y坐标</param>
        /// <param name="width">logo 宽度</param>
        /// <param name="height">logo 高度</param>
        /// <param name="filePath">logo 文件路径</param>
        /// <param name="isFlash">是否闪烁 1-闪烁，0-不闪烁</param>
        /// <param name="isTranslucent">是否半透明 1-半透明，0-不半透明</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public bool UploadLogo(int userId, uint decoderChannel, uint corordinateX, uint corordinateY, ushort width, ushort height, string filePath, int isFlash = 0, int isTranslucent = 1)
        {
            // Image img= Image.FromFile(filePath);
            uint logoSize = 0;
            byte[] byts;
            if (File.Exists(filePath))
            {
                byts = File.ReadAllBytes(filePath);
                logoSize = (uint)byts.Length;
            }
            else
                return false;
            NET_DVR_DISP_LOGOCFG cfg = new NET_DVR_DISP_LOGOCFG() { byFlash = (byte)isFlash, byTranslucent = (byte)isTranslucent, dwCorordinateX = corordinateX, dwCorordinateY = corordinateY, wPicHeight = height, wPicWidth = width, dwLogoSize = logoSize, byRes1 = new byte[4], byRes2 = new byte[6] };
            //// int nSize = Marshal.SizeOf(img);
            // IntPtr ptrDecAbility = Marshal.AllocHGlobal(byts.Length);
            // Marshal.StructureToPtr(img, ptrDecAbility, false);

            bool b = HikDecoderApi.NET_DVR_UploadLogo(userId, decoderChannel, ref cfg, byts);
            //Marshal.FreeHGlobal(ptrDecAbility);
            //int x = HikOperate.GetLastError();
            return b;
        }
        /// <summary>
        /// 上传LOGO。Logo要求bmp格式，24位图，支持分辨率：32*32 ~ 256*128。
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="logoNo">logo序号</param>
        /// <param name="filePath">logo 文件路径</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public bool UploadLogoNew(int userId, string filePath,uint logoNo=1)
        {
            uint logoSize = 0;
            byte[] byts;
            if (File.Exists(filePath))
            {
                byts = File.ReadAllBytes(filePath);
                logoSize = (uint)byts.Length;
            }
            else
                return false;
            NET_DVR_MATRIX_LOGO_INFO cfg = new NET_DVR_MATRIX_LOGO_INFO() {  dwLogoSize = logoSize,byRes2 = new byte[32] };
            uint dwSize = (uint)Marshal.SizeOf(cfg);
            cfg.dwSize = dwSize;
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(cfg, ptrIpParaCfgV40, false);
            bool b = HikDecoderApi.NET_DVR_UploadLogo_NEW(userId, logoNo, ref cfg, byts);
            int x = HikOperate.GetLastError();
            return b;
        }
        /// <summary>
        /// 控制logo
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="logoNo">logo序号</param>
        /// <param name="channel"></param>
        /// <param name="corordinateX">显示x坐标</param>
        /// <param name="corordinateY">显示y坐标</param>
        /// <param name="logoControl">logo操作</param>
        /// <param name="logoSwith">开关命令，1-显示 0-隐藏</param>
        /// <param name="isFlash">是否闪烁 1-闪烁，0-不闪烁</param>
        /// <param name="isTranslucent">是否半透明 1-半透明，0-不半透明</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public bool ControlLogo(int userId,  uint channel, uint corordinateX, uint corordinateY,LogoCmd logoControl,int logoSwith, int isFlash = 0, int isTranslucent = 1, uint logoNo=1)
        {
            NET_DVR_WIN_LOGO_CFG cfg = new NET_DVR_WIN_LOGO_CFG() { byEnable = (byte)logoSwith, byFlash = (byte)isFlash, byTranslucent = (byte)isTranslucent, dwCorordinateX = corordinateX, dwCorordinateY = corordinateY, dwLogoNo = logoNo,byRes1 = new byte[3], byRes2 = new byte[34] };
            uint dwSize = (uint)Marshal.SizeOf(cfg);
            cfg.dwSize = dwSize;
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(cfg, ptrIpParaCfgV40, false);
            bool b=HikApi.NET_DVR_SetDVRConfig(userId, (uint)logoControl, (int)channel, ptrIpParaCfgV40, dwSize);
            return b;
        }
        /// <summary>
        /// 获取窗口logo参数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="channel">通道号</param>
        /// <returns>返回logo参数结构体</returns>
        public NET_DVR_WIN_LOGO_CFG GetLogoConfig(int userId,uint channel)
        {
            NET_DVR_WIN_LOGO_CFG net_dvr_win_logo_cfg = new NET_DVR_WIN_LOGO_CFG();
            uint dwSize = (uint)Marshal.SizeOf(net_dvr_win_logo_cfg);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(net_dvr_win_logo_cfg, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            bool b = HikApi.NET_DVR_GetDVRConfig(userId, (uint)LogoCmd.NET_DVR_GET_WIN_LOGO_CFG, (int)channel, ptrIpParaCfgV40, dwSize, ref dwReturn);
            net_dvr_win_logo_cfg = (NET_DVR_WIN_LOGO_CFG)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(NET_DVR_WIN_LOGO_CFG));
            return net_dvr_win_logo_cfg;
        }
        /// <summary>
        /// LOGO显示控制
        /// </summary>
        /// <param name="userId">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="decoderChannel">解码通道号</param>
        /// <param name="logoSwith">开关命令，1-显示 2-隐藏</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        public bool LogoSwitch(int userId, uint decoderChannel, uint logoSwith = 1)
        {
            return HikDecoderApi.NET_DVR_LogoSwitch(userId, decoderChannel, logoSwith);
        }
        #endregion
        ///// <summary>
        ///// 远程控制
        ///// </summary>
        ///// <param name="useInfo">登录设备时的UseInfo对象</param>
        ///// <param name="control">控制类型</param>
        ///// <param name="lpInBuffer">窗口号或者解码文件信息</param>
        ///// <returns></returns>
        //public bool RemoteControl(ref UseInfo useInfo, RemoteControl control,IntPtr lpInBuffer)
        //{

        //    return HikPublicApi.NET_DVR_RemoteControl(useInfo.UserId, (uint)control, lpInBuffer, 0);
        //}
        ///// <summary>
        ///// 获取大屏拼接参数
        ///// </summary>
        ///// <param name="useInfo">登陆设备时的UseInfo对象</param>
        ///// <param name="config">大屏拼接参数结构体</param>
        ///// <param name="screenNo">大屏序号</param>
        ///// <returns>成功：true，失败：false</returns>
        //public bool GetScreenConfig(UseInfo useInfo,ref HikDecoderStruct.NET_DVR_BIGSCREENCFG config,int screenNo=0)
        //{
        //    uint dwSize = (uint)Marshal.SizeOf(config);
        //    IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
        //    Marshal.StructureToPtr(config, ptrIpParaCfgV40, false);
        //    uint dwReturn = 0;
        //    return HikPublicApi.NET_DVR_GetDVRConfig(useInfo.UserId, (int)HikDecoderEnum.DwCommand.NET_DVR_MATRIX_BIGSCREENCFG_GET, screenNo, ptrIpParaCfgV40, dwSize,ref dwReturn);
        //}

        //#region 获取显示通道
        ///// <summary>
        ///// 获取显示通道
        ///// </summary>
        ///// <param name="useInfo">登陆设备返回的UseInfo对象</param>
        ///// <returns>返回显示通道类型</returns>
        //public List<string> GetScreenAbility(UseInfo useInfo)
        //{
        //    string m_struDecAbility = "";
        //    try
        //    {

        //        IntPtr ptrDecAbility =new IntPtr(,);
        //      //  Marshal.StructureToPtr(m_struDecAbility, ptrDecAbility, false);
        //        if (!HikPublicApi.NET_DVR_GetDeviceAbility(useInfo.UserId, 0x212, IntPtr.Zero, 0, ptrDecAbility, (uint)6000))
        //        {

        //        }

        //    }
        //    catch (Exception) { }
        //    return null;
        //}
        //#endregion
    }
}
