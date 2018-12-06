using System;
using System.Runtime.InteropServices;
using System.Threading;
using static HikDeviceApi.HikConst;
using static HikDeviceApi.HikStruct;

namespace HikDeviceApi.Decoder
{
    /// <summary>
    /// 日 期:2015-07-07
    /// 作 者:痞子少爷
    /// 描 述:海康多路解码器操作结构
    /// </summary>
    public static class HikDecoderStruct
    {
        #region  设备操作参数结构 
        /// <summary>
        /// 设备操作参数结构
        /// </summary>
        [Serializable]
        public class DecoderDeviceInfo
        {
            /// <summary>
            /// 用户ID
            /// </summary>
            /// <remarks>登陆事件注册返回的ID</remarks>
            private int decoderUserId = -1;
            /// <summary>
            /// 用户ID
            /// </summary>
            public int DecoderUserId
            {
                get
                {
                    return decoderUserId;
                }

                set
                {
                    decoderUserId = value;
                }
            }
            /// <summary>
            /// 登录用户名
            /// </summary>
            public string DecoderUserName { get; set; }
            /// <summary>
            /// 登录用户密码
            /// </summary>
            public string DecoderUserPwd { get; set; }
            /// <summary>
            /// 登录设备地址
            /// </summary>
            public string DecoderIp { get; set; }
            /// <summary>
            /// 登录设备端口
            /// </summary>
            public decimal DecoderPoint { get; set; }
            /// <summary>
            /// 解码器设备名称
            /// </summary>
            public string DecoderName { get; set; }
            /// <summary>
            /// 解码器设备位置
            /// </summary>
            public string DecoderPostion { get; set; }
            /// <summary>
            /// 解码器操作信息
            /// </summary>
            public DecoderControlInfo ControlInfo { get; set; }
            /// <summary>
            /// 设备型号
            /// </summary>
            public string DeviceType { get; set; }

        }
        /// <summary>
        /// 解码器操作信息
        /// </summary>
        public class DecoderControlInfo
        {
            /// <summary>
            /// 解码通道
            /// </summary>
            public int DecoderChannel { get; set; }
            /// <summary>
            /// 显示通道
            /// </summary>
            public int DisplayChannel { get; set; }
            /// <summary>
            /// 远程配置ID
            /// </summary>
            /// <remarks>远程事件注册返回的ID</remarks>
            public int RemoteId { get; set; }
            /// <summary>
            /// 语音对讲ID
            /// </summary>
            public int VoiceId { get; set; }
            /// <summary>
            /// 语音转发ID
            /// </summary>
            public int VoiceCom { get; set; }
            /// <summary>
            /// 语音通道ID
            /// </summary>
            public int VoiceChanID { get; set; }
            /// <summary>
            /// 建立透明通道ID
            /// </summary>
            public int SerialID { get; set; }
            /// <summary>
            /// 远程升级ID
            /// </summary>
            public int UpgradeID { get; set; }
            /// <summary>
            /// 被动解码句柄
            /// </summary>
            public int PassiveHandle { get; set; }
            ///// <summary>
            ///// 被动解码发送数据线程
            ///// </summary>
            //public Thread PassiveThread { get; set; }
            /// <summary>
            /// 文件下载ID
            /// </summary>
            public int FileDownId { get; set; }
            /// <summary>
            /// 回放ID
            /// </summary>
            public int PlayBackId { get; set; }
            /// <summary>
            /// 实时预览句柄
            /// </summary>
            public int RealHandle { get; set; }
            /// <summary>
            /// 文件流大小,1-512
            /// </summary>
            public int FileStreamLength { get; set; }
        }
        #endregion

        #region 显示通道画面分割模式
        /// <summary>
        /// 显示通道画面分割模式
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DISPWINDOWMODE
        {
            /// <summary>
            /// 显示通道类型：0-VGA, 1-BNC, 2-HDMI, 3-DVI
            /// </summary>
            public byte byDispChanType;
            /// <summary>
            /// 显示通道序号,从1开始，如果类型是VGA，则表示第几个VGA
            /// </summary>
            public byte byDispChanSeq;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 支持的画面分割模式,VGA支持1、4画面分割，HDMI支持1、4、16画面分割。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WINDOWS_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byDispMode;
        }
        #endregion

        #region 远程回放文件的参数结构体
        /// <summary>
        /// 远程回放文件的参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 远程设备IP地址 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/* DVR IP地址 */
            /// <summary>
            /// 远程设备端口 
            /// </summary>
            public ushort wDVRPort;/* 端口号 */
            /// <summary>
            /// 通道号 
            /// </summary>
            public byte byChannel;/* 通道号 */
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            public byte byReserve;
            /// <summary>
            /// 用户名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]//, ArraySubType = UnmanagedType.I1
            public string sUserName;/* 用户名 */
            /// <summary>
            /// 密码
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]//, ArraySubType = UnmanagedType.I1
            public string sPassword;/* 密码 */
            /// <summary>
            /// 播放类型：0－按文件；1－按时间 
            /// </summary>
            public uint dwPlayMode;/* 0－按文件 1－按时间*/
            /// <summary>
            /// 文件开始时间
            /// </summary>
            public HikStruct.NET_DVR_TIME StartTime;
            /// <summary>
            /// 文件结束时间
            /// </summary>
            public HikStruct.NET_DVR_TIME StopTime;
            /// <summary>
            /// 保存的文件路径名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;
        }
        #endregion

        #region 回放状态信息结构体
        /// <summary>
        /// 回放状态信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 当前播放的媒体文件长度
            /// </summary>
            public uint dwCurMediaFileLen;
            /// <summary>
            /// 当前播放文件的播放位置
            /// </summary>
            public uint dwCurMediaFilePosition;
            /// <summary>
            /// 当前播放文件的总时间
            /// </summary>
            public uint dwCurMediaFileDuration;
            /// <summary>
            /// 当前已经播放的时间
            /// </summary>
            public uint dwCurPlayTime;
            /// <summary>
            /// 当前播放文件的总帧数
            /// </summary>
            public uint dwCurMediaFIleFrames;
            /// <summary>
            /// 当前传输的数据类型，19-文件头，20-流数据， 21-播放结束标志
            /// </summary>
            public uint dwCurDataType;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 72, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }
        #endregion

        #region 被动解码控制参数
        /// <summary>
        /// 被动解码控制参数
        /// </summary>
        /// <remarks>该结构中的第三个参数是否需要输入数值与控制命令有关</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PASSIVEDECODE_CONTROL
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 解码器播放控制状态命令
            /// </summary>
            public uint dwPlayCmd;
            /// <summary>
            /// 被动解码控制参数
            /// </summary>
            public uint dwCmdParam;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region 透明通道参数结构体
        /// <summary>
        /// 透明通道参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_TRAN_CHAN_CONFIG_V30
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 设置哪路232透明通道是全双工的 取值1到64
            /// </summary>
            public byte by232IsDualChan;
            /// <summary>
            ///  设置哪路485透明通道是全双工的 取值1到64
            /// </summary>
            public byte by485IsDualChan;
            /// <summary>
            ///  保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] vyRes;
            /// <summary>
            /// 透明通道参数结构体，同时支持建立64个透明通道
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SERIAL_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_TRAN_CHAN_INFO[] struTranInfo;
        }
        #endregion

        #region 透明通道参数子结构体
        /// <summary>
        /// 透明通道参数子结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_TRAN_CHAN_INFO
        {
            /// <summary>
            /// 当前透明通道是否打开：0－关闭；1－打开。多路解码器本地有1个485串口、1个232串口都可以作为透明通道，设备号分配如下：0－RS485；1－RS232 
            /// </summary>
            public byte byTranChanEnable;/* 当前透明通道是否打开 0：关闭 1：打开 */

            /// <summary>
            /// 多路解码器本地有1个485串口，1个232串口都可以作为透明通道,设备号分配如下:0－RS485；1－RS232 
            /// </summary>
            public byte byLocalSerialDevice;
            /// <summary>
            /// 远程串口输出两个：一个RS232，一个RS485。1表示232串口；2表示485串口
            /// </summary>
            public byte byRemoteSerialDevice;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            public byte res1;
            /// <summary>
            /// 远程设备IP地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteDevIP;
            /// <summary>
            /// 远程设备端口
            /// </summary>
            public ushort wRemoteDevPort;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] res2;
            /// <summary>
            /// 串口配置参数
            /// </summary>
            public TTY_CONFIG RemoteSerialDevCfg;
        }
        #endregion

        #region 串口参数结构体
        /// <summary>
        /// 串口参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TTY_CONFIG
        {
            /// <summary>
            /// 波特率
            /// </summary>
            public byte baudrate;
            /// <summary>
            /// 数据位
            /// </summary>
            public byte databits;
            /// <summary>
            /// 停止位
            /// </summary>
            public byte stopbits;
            /// <summary>
            /// 奇偶校验位
            /// </summary>
            public byte parity;
            /// <summary>
            /// 流控
            /// </summary>
            public byte flowcontrol;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }
        #endregion

        #region 远程文件解码回放参数结构体
        /// <summary>
        /// 远程文件解码回放参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY_EX
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            uint dwSize;
            /// <summary>
            /// 解码通道号
            /// </summary>
            uint dwDecChannel;
            /// <summary>
            /// 设备地址类型：0- IP，1- DDNS域名
            /// </summary>
            byte byAddressType;
            /// <summary>
            /// 通道类型：0- 普通通道，1- 零通道，2- 流ID
            /// </summary>
            byte byChannelType;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            byte[] byres;
            /// <summary>
            /// 用户名 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            byte[] sUserName;
            /// <summary>
            /// 密码
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            byte[] sPassword;
            /// <summary>
            /// 设备通道号
            /// </summary>
            uint dwChannel;
            /// <summary>
            /// 流ID,此参数在通道类型为流ID时有效
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            byte[] byStreamId;
            /// <summary>
            /// 0－按文件 1－按时间
            /// </summary>
            uint dwPlayMode;
            /// <summary>
            /// 设备地址联合体
            /// </summary>
            HikStruct.Addr addr;
            /// <summary>
            /// 回放参数联合体 
            /// </summary>
            PlayBackInfo playBackInfo;

        }
        #endregion

        #region 为回放参数联合体 
        /// <summary>
        /// 为回放参数联合体 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PlayBackInfo
        {
            /// <summary>
            /// 联合体大小为128字节 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            byte[] byRes;
            /// <summary>
            /// 按时间回放时文件信息 
            /// </summary>
            NET_DVR_PLAY_BACK_BY_TIME struPlayBackByTime;
            /// <summary>
            /// 按文件名回放时文件信息
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            byte[] sFileName;
        }
        #endregion

        #region 回放时间信息结构体
        /// <summary>
        /// 回放时间信息结构体
        /// </summary>
        public struct NET_DVR_PLAY_BACK_BY_TIME
        {
            /// <summary>
            /// 回放开始时间
            /// </summary>
            HikStruct.NET_DVR_TIME StartTime;
            /// <summary>
            /// 回放结束时间
            /// </summary>
            HikStruct.NET_DVR_TIME StopTime;
        }
        #endregion

        #region 解码器能力集结构体
        /// <summary>
        /// 解码器能力集结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_ABILITY_V41
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// DSP个数
            /// </summary>
            public byte byDspNums;
            /// <summary>
            /// 解码通道数
            /// </summary>
            public byte byDecChanNums;
            /// <summary>
            /// 起始解码通道
            /// </summary>
            public byte byStartChan;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// VGA显示通道信息
            /// </summary>
            public NET_DVR_DISPINFO struVgaInfo;
            /// <summary>
            /// BNC显示通道信息
            /// </summary>
            public NET_DVR_DISPINFO struBncInfo;
            /// <summary>
            /// HDMI显示通道信息
            /// </summary>
            public NET_DVR_DISPINFO struHdmiInfo;
            /// <summary>
            /// DVI显示通道信息
            /// </summary>
            public NET_DVR_DISPINFO struDviInfo;
            /// <summary>
            /// 显示通道画面分割模式
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DISPNUM_V41, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISPWINDOWMODE[] struDispMode;
            /// <summary>
            /// 大屏拼接信息 
            /// </summary>
            public NET_DVR_SCREENINFO struBigScreenInfo;
            /// <summary>
            /// 是否支持自动重启，0-不支持，1-支持
            /// </summary>
            public byte bySupportAutoReboot;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 119, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 显示通道信息
        /// <summary>
        /// 显示通道信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DISPINFO
        {
            /// <summary>
            /// 通道个数，该参数在大屏中无效 
            /// </summary>
            public byte byChanNums;
            /// <summary>
            /// 起始通道号，该参数在大屏中无效
            /// </summary>
            public byte byStartChan;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 支持的分辨率
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SUPPORT_RES, ArraySubType = UnmanagedType.U1)]
            public uint[] dwSupportResolution;
        }
        #endregion

        //#region 协议参数的结构体
        ///// <summary>
        ///// 协议参数的结构体
        ///// </summary>
        //[StructLayoutAttribute(LayoutKind.Sequential)]
        //public struct NET_DVR_PROTO_TYPE
        //{
        //    /// <summary>
        //    /// 协议值 
        //    /// </summary>
        //    public uint dwType;
        //    /// <summary>
        //    /// 协议描述
        //    /// </summary>
        //    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DESC_LEN, ArraySubType = UnmanagedType.I1)]
        //    public byte[] byDescribe;
        //}
        //#endregion

        #region 大屏拼接信息结构体
        /// <summary>
        /// 大屏拼接信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SCREENINFO
        {
            /// <summary>
            /// 最多大屏拼接数量
            /// </summary>
            public byte bySupportBigScreenNums;
            /// <summary>
            /// 大屏拼接起始序号
            /// </summary>
            public byte byStartBigScreenNum;
            /// <summary>
            /// 水平方向屏幕个数
            /// </summary>
            public byte byMaxScreenX;
            /// <summary>
            /// 垂直方向屏幕个数。大屏拼接模式：byMaxScreenX*byMaxScreenY，例如3*3，共有9块屏幕组成了一个大屏 
            /// </summary>
            public byte byMaxScreenY;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region  解码通道信息结构体 
        /// <summary>
        /// 解码通道信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_CHAN_INFO_V41
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 取流模式：0- 无效，1- 通过IP或域名取流，2- 通过URL取流，3- 通过动态域名解析向设备取流
            /// </summary>
            public byte byStreamMode;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 取流信息 
            /// </summary>
            public NET_DVR_DEC_STREAM_MODE uDecStreamMode;
            /// <summary>
            /// 解码状态：0-动态解码，1－循环解码，2－按时间回放，3－按文件回放
            /// </summary>
            public uint dwPlayMode;
            /// <summary>
            /// 按时间回放开始时间 
            /// </summary>
            public HikStruct.NET_DVR_TIME StartTime;
            /// <summary>
            /// 按时间回放结束时间
            /// </summary>
            public HikStruct.NET_DVR_TIME StopTime;
            /// <summary>
            /// 按文件回放文件名 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;
            /// <summary>
            /// 取流模式:1-主动，2-被动
            /// </summary>
            public uint dwGetStreamMode;
            /// <summary>
            /// 被动解码信息
            /// </summary>
            public NET_DVR_MATRIX_PASSIVEMODE struPassiveMode;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 被动解码参数结构体 
        /// <summary>
        /// 被动解码参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_PASSIVEMODE
        {
            /// <summary>
            /// 传输协议：0-TCP，1-UDP，2-MCAST 
            /// </summary>
            public ushort wTransProtol;
            /// <summary>
            /// TCP或者UDP端口，TCP时端口默认是8000，不同的解码通道UDP端口号需设置为不同的值 
            /// </summary>
            public ushort wPassivePort;
            /// <summary>
            /// TCP/UDP时无效，MCAST时为多播地址(暂时不支持多播，保留)
            /// </summary>
            public HikStruct.NET_DVR_IPADDR struMcastIP;
            /// <summary>
            /// 数据类型: 1-实时流, 2-文件流 
            /// </summary>
            public byte byStreamType;
            /// <summary>
            /// 重连标志：0- 新连接，1- 重连 
            /// </summary>
            public byte byReconnectFlag;
            /// <summary>
            /// 实时流模式：1- 客户端直接发流，不发送交互头；0- 需要发送交互头 
            /// </summary>
            public byte byRequestType;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region  取流模式配置联合体 
        /// <summary>
        /// 取流模式配置联合体
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct NET_DVR_DEC_STREAM_MODE
        {
            /// <summary>
            /// 联合体大小
            /// </summary>
            [FieldOffset(0)]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 300, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            //HikStruct.NET_DVR_DEC_STREAM_DEV_EX struDecStreamDev;
            //HikStruct.NET_DVR_PU_STREAM_URL struUrlInfo;
            //HikStruct.NET_DVR_DEC_DDNS_DEV struDdnsDecInfo;
            /// <summary>
            /// 初始化联合体大小
            /// </summary>
            public void Init()
            {
                byRes = new byte[300];
            }
        }
        #endregion

        #region  动态解码参数结构体 
        /// <summary>
        /// 动态解码参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PU_STREAM_CFG_V41
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 取流模式：0- 无效，1- 通过IP或域名取流，2- 通过URL取流，3- 通过动态域名解析向设备取流
            /// </summary>
            public byte byStreamMode;//取流模式：0- 无效，1- 通过IP或域名取流，2- 通过URL取流，3- 通过动态域名解析向设备取流
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 取流配置信息
            /// </summary>
            public NET_DVR_DEC_STREAM_MODE uDecStreamMode;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 显示通道配置结构体
        /// <summary>
        /// 显示通道配置结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_VOUTCFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 音频是否开启：0- 关闭，1- 开启 
            /// </summary>
            public byte byAudio;
            /// <summary>
            /// 音频开启子窗口 
            /// </summary>
            public byte byAudioWindowIdx;
            /// <summary>
            /// 显示通道类型：0-BNC，1-VGA，2-HDMI，3-DVI，4-YPbPr(解码卡服务器DECODER_SERVER专用)
            /// </summary>
            public byte byDispChanType;
            /// <summary>
            /// 视频制式：1- NTSC，2- PAL，0- NULL 
            /// </summary>
            public byte byVedioFormat;
            /// <summary>
            /// 分辨率
            /// </summary>
            public uint dwResolution;
            /// <summary>
            /// 画面分隔模式，能力集获取
            /// </summary>
            public uint dwWindowMode;
            /// <summary>
            /// 各个子窗口关联的解码通道,设备支持解码资源自动分配时此参数不用填充
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WINDOWS_V41, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecChan;
            /// <summary>
            /// 是否处于放大状态，0：不放大，1：放大。获取时有效，设置通过接口NET_DVR_MatrixDiaplayControl实现
            /// </summary>
            public byte byEnlargeStatus;
            /// <summary>
            /// 放大的子窗口号（获取时有效）
            /// </summary>
            public byte byEnlargeSubWindowIndex;
            /// <summary>
            /// 显示模式，0---真实显示，1---缩放显示( 针对BNC )
            /// </summary>
            public byte byScale;
            /// <summary>
            /// 区分共用体,0-视频综合平台内部解码器显示通道配置，1-其他解码器显示通道配置
            /// </summary>
            public byte byUnionType;
            /// <summary>
            /// 显示通道配置联合体
            /// </summary>
            public NET_DVR_VIDEO_PLATFORM struDiff;
            /// <summary>
            /// 显示输出号，此参数在全部获取时有效
            /// </summary>
            public uint dwDispChanNum;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 76, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 显示通道配置联合体
        /// <summary>
        /// 显示通道配置联合体
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct NET_DVR_VIDEO_PLATFORM
        {
            /// <summary>
            /// 保留 
            /// </summary>
            [FieldOffsetAttribute(0)]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 160, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 视频综合平台内部解码器显示通道配置
            /// </summary>
            [FieldOffsetAttribute(0)]
            public VideoPlatform struVideoPlatform;
            /// <summary>
            /// 其他解码器显示通道配置
            /// </summary>
            [FieldOffsetAttribute(0)]
            public NotVideoPlatform struNotVideoPlatform;
        }
        #endregion

        #region 视频综合平台内部解码器显示通道配置
        /// <summary>
        /// 视频综合平台内部解码器显示通道配置
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct VideoPlatform
        {
            /// <summary>
            /// 各个子窗口对应解码通道所对应的解码子系统的槽位号(对于视频综合平台中解码子系统有效) 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WINDOWS_V41, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecoderId;
            /// <summary>
            /// 显示窗口所解视频分辨率：1- D1，2- 720P，3- 1080P，设备端需要根据此分辨率进行解码通道的分配。如1分屏配置成1080P，则设备会把4个解码通道都分配给此显示窗口。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WINDOWS_V41, ArraySubType = UnmanagedType.I1)]
            public byte[] byDecResolution;
            /// <summary>
            /// 显示通道在电视墙中位置
            /// </summary>
            public NET_DVR_RECTCFG struPosition;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region 其他解码器显示通道配置
        /// <summary>
        /// 其他解码器显示通道配置
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NotVideoPlatform
        {
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 160, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region 解码通道状态结构体
        /// <summary>
        /// 解码通道状态结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_CHAN_STATUS
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 解码通道状态 0－休眠 1－正在连接 2－已连接 3-正在解码
            /// </summary>
            public uint dwIsLinked;
            /// <summary>
            /// 码流传输速率，单位kb/s
            /// </summary>
            public uint dwStreamCpRate;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string cRes;
        }
        #endregion

        

        #region 上传的LOGO的结构体
        /// <summary>
        /// 上传的LOGO的结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DISP_LOGOCFG
        {
            /// <summary>
            /// 图片显示区域X坐标
            /// </summary>
            public uint dwCorordinateX;
            /// <summary>
            /// 图片显示区域Y坐标
            /// </summary>
            public uint dwCorordinateY;
            /// <summary>
            /// 图片宽
            /// </summary>
            public ushort wPicWidth;
            /// <summary>
            /// 图片高
            /// </summary>
            public ushort wPicHeight;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 是否闪烁 1-闪烁，0-不闪烁
            /// </summary>
            public byte byFlash;
            /// <summary>
            /// 是否半透明 1-半透明，0-不半透明
            /// </summary>
            public byte byTranslucent;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            /// <summary>
            /// LOGO的大小，包括BMP的文件头(要求：BMP图片，位深24，长宽是32的倍数，最大是128*128)
            /// </summary>
            /// <remarks>
            /// 解码器支持的LOGO图片要求：BMP图片，位深24，长宽是32的倍数，最大是128*128（即大小不能超过100K）。
            ///</remarks>
            public uint dwLogoSize;//LOGO大小，包括BMP的文件头
        }
        /// <summary>
        /// 窗口LOGO的结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WIN_LOGO_CFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// logo号
            /// </summary>
            public uint dwLogoNo;
            /// <summary>
            /// 是否显示 1显示 0隐藏
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 图片显示区域X坐标
            /// </summary>
            public uint dwCorordinateX;
            /// <summary>
            /// 图片显示区域Y坐标
            /// </summary>
            public uint dwCorordinateY;
            ///// <summary>
            ///// 图片宽
            ///// </summary>
            //public ushort wPicWidth;
            ///// <summary>
            ///// 图片高
            ///// </summary>
            //public ushort wPicHeight;
            
            /// <summary>
            /// 是否闪烁 1-闪烁，0-不闪烁
            /// </summary>
            public byte byFlash;
            /// <summary>
            /// 是否半透明 1-半透明，0-不半透明
            /// </summary>
            public byte byTranslucent;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        /// <summary>
        /// LOGO参数
        /// </summary>
        public struct NET_DVR_MATRIX_LOGO_CFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// LOGO 是否存在 0-不存在1-存在 用于获取
            /// </summary>
            public byte byExist;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// LOGO的大小，包括BMP的文件头(要求：BMP图片，位深24，长宽是32的倍数，最大是128*128)
            /// </summary>
            /// <remarks>
            /// 解码器支持的LOGO图片要求：BMP图片，位深24，长宽是32的倍数，最大是128*128（即大小不能超过100K）。
            ///</remarks>
            public uint dwLogoSize;
            /// <summary>
            /// LOGO名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string dwLogoName;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// LOGO上传参数
        /// </summary>
        public struct NET_DVR_MATRIX_LOGO_INFO
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;//LOGO大小，包括BMP的文件头
            /// <summary>
            /// LOGO的大小，包括BMP的文件头(要求：BMP图片，位深24，长宽是32的倍数，最大是128*128)
            /// </summary>
            /// <remarks>
            /// 解码器支持的LOGO图片要求：BMP图片，位深24，长宽是32的倍数，最大是128*128（即大小不能超过100K）。
            ///</remarks>
            public uint dwLogoSize;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 设备状态
        /// <summary>
        /// 解码设备状态参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DECODER_WORK_STATUS_V41
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public int dwSize;
            /// <summary>
            /// 解码通道状态
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DECODECHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_STATUS[] struDecChanStatus;
            /// <summary>
            /// 显示通道状态
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DISPNUM_V41, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISP_CHAN_STATUS_V41[] struDispChanStatus;
            /// <summary>
            /// 报警输入状态
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMIN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatus;
            /// <summary>
            /// 报警输出状态 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byAalarmOutStatus;
            /// <summary>
            /// 语音对讲状态 
            /// </summary>
            public byte byAudioInChanStatus;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 127, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 解码设备状态参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DECODER_WORK_STATUS
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public int dwSize;
            /// <summary>
            /// 解码通道状态 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DECODECHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_STATUS[] struDecChanStatus;
            /// <summary>
            /// 显示通道状态 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DISPCHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISP_CHAN_STATUS[] struDispChanStatus;
            /// <summary>
            /// 报警输入状态
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMIN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatus;
            /// <summary>
            /// 报警输出状态 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byAalarmOutStatus;
            /// <summary>
            /// 语音对讲状态
            /// </summary>
            public byte byAudioInChanStatus;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 127, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 显示通道状态结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DISP_CHAN_STATUS
        {
            /// <summary>
            /// 显示状态：0-未显示，1-启动显示
            /// </summary>
            public byte byDispStatus;
            /// <summary>
            /// 显示接口，0-BNC输出，1-VGA输出，2-HDMI输出，3-DVI输出 
            /// </summary>
            public byte byBVGA;
            /// <summary>
            /// 视频制式
            /// </summary>
            public byte byVideoFormat;
            /// <summary>
            /// 当前画面模式
            /// </summary>
            public byte byWindowMode;
            /// <summary>
            /// 各个子窗口关联的解码通道 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WINDOWS, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecChan;
            /// <summary>
            /// 每个子画面的显示帧率
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NET_DVR_MAX_DISPREGION, ArraySubType = UnmanagedType.I1)]
            public byte[] byFpsDisp;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
    }
    
        /// <summary>
        /// 解码通道状态结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_CHAN_STATUS
        {

            /// <summary>
            /// 当前状态：0-未启动，1-启动解码
            /// </summary>
            public byte byDecodeStatus;
            /// <summary>
            /// 码流类型
            /// </summary>
            public byte byStreamType;
            /// <summary>
            /// 封装格式
            /// </summary>
            public byte byPacketType;
            /// <summary>
            /// 接收缓冲使用率 
            /// </summary>
            public byte byRecvBufUsage;
            /// <summary>
            /// 解码缓冲使用率
            /// </summary>
            public byte byDecBufUsage;
            /// <summary>
            /// 视频解码帧率，大于1的数值即代表帧率值，对于小于1的低帧率见枚举FpsDecV
            /// </summary>
            public byte byFpsDecV;
            /// <summary>
            /// 音频解码帧率，其值的含义和视频解码帧率相同
            /// </summary>
            public byte byFpsDecA;
            /// <summary>
            /// DSP CPU利用率 
            /// </summary>
            public byte byCpuLoad;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 解码的视频帧 
            /// </summary>
            public uint dwDecodedV;
            /// <summary>
            /// 解码的音频帧
            /// </summary>
            public uint dwDecodedA;
            /// <summary>
            /// 解码器当前的图像的宽
            /// </summary>
            public ushort wImgW;
            /// <summary>
            /// 解码器当前的图像的高
            /// </summary>
            public ushort wImgH;
            /// <summary>
            /// 视频制式
            /// </summary>
            public byte byVideoFormat;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            /// <summary>
            /// 解码通道号，获取全部解码通道状态时有效，设置时可填0 
            /// </summary>
            public uint dwDecChan;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }
        /// <summary>
        /// 显示通道状态结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DISP_CHAN_STATUS_V41
        {
            /// <summary>
            /// 显示状态：0-未显示，1-启动显示 
            /// </summary>
            public byte byDispStatus;
            /// <summary>
            /// 显示接口，0-BNC输出，1-VGA输出，2-HDMI输出，3-DVI输出，0xff-无效(255)
            /// </summary>
            public byte byBVGA;
            /// <summary>
            /// 视频制式 1:NTSC,2:PAL,0-NON
            /// </summary>
            public byte byVideoFormat;
            /// <summary>
            /// 当前画面模式 
            /// </summary>
            public byte byWindowMode;
            /// <summary>
            /// 各个子窗口关联的解码通道
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WINDOWS_V41, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecChan;
            /// <summary>
            /// 每个子画面的显示帧率
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WINDOWS_V41, ArraySubType = UnmanagedType.I1)]
            public byte[] byFpsDisp;
            /// <summary>
            /// 屏幕模式：0- 普通，1- 大屏
            /// </summary>
            public byte byScreenMode;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 显示通道号，获取全部显示通道状态时有效，设置时可填0 
            /// </summary>
            public uint dwDispChan;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
       

        /// <summary>
        /// 解码通道控制参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DECCHAN_CONTROL
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public int dwSize;
            /// <summary>
            /// 解码通道显示缩放控制，1-缩放显示，0-真实显示
            /// </summary>
            public byte byDecChanScaleStatus;
            /// <summary>
            /// 解码延时：0- 默认，1- 实时性好；2- 实时性较好；3- 实时性中，流畅性中；4- 流畅性较好；5- 流畅性好；0xff- 自动调整
            /// </summary>
            public byte byDecodeDelay;
            /// <summary>
            /// 畅显使能：0- 关，1- 开
            /// </summary>
            public byte byEnableSpartan;
            /// <summary>
            /// 低照度：0- 关，1~8代表低照度等级，等级越高强度越大
            /// </summary>
            public byte byLowLight;
            /// <summary>
            /// 3D降噪：0- 关，1- 开，2- 自动
            /// </summary>
            public byte byNoiseReduction;
            /// <summary>
            /// 透雾：0- 关，1~7代表透雾等级，等级越高强度越大
            /// </summary>
            public byte byDefog;
            /// <summary>
            /// 是否启用智能解码：0- 不启用，非0- 启用 
            /// </summary>
            public byte byEnableVcaDec;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            public byte byRes1;
            /// <summary>
            /// 所有子窗口一起操作的类型，设置时有效，按位表示，值：0- 不设置，1- 设置 
            /// dwAllCtrlType  0x01：表示开启关闭智能解码
            /// </summary>
            public uint dwAllCtrlType;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        #endregion

    }
}
