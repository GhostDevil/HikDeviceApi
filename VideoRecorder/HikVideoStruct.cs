using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static HikDeviceApi.VideoRecorder.HikVideoDelegate;
using static HikDeviceApi.HikStruct;
using static HikDeviceApi.HikConst;
namespace HikDeviceApi.VideoRecorder
{
    /// <summary>
    /// 日 期:2015-09-09
    /// 作 者:痞子少爷
    /// 描 述:海康硬盘录像机操作结构
    /// </summary>
    public static class HikVideoStruct
    {
        #region  设备操作参数结构 
        /// <summary>
        /// 设备操作参数结构
        /// </summary>
        [Serializable]
        public class DvrUseInfo
        {
            /// <summary>
            /// 用户登录返回ID
            /// </summary>
            /// <remarks>登陆事件注册返回的ID</remarks>
            private int userId = -1;
            /// <summary>
            /// 远程配置ID
            /// </summary>
            /// <remarks>远程事件注册返回的ID</remarks>
            public int RemoteId { get; set; }
            /// <summary>
            /// 登录用户名
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 登录用户密码
            /// </summary>
            public string UserPwd { get; set; }
            /// <summary>
            /// 登录设备端口
            /// </summary>
            public decimal DvrPoint { get; set; }
            /// <summary>
            /// DVR ip地址
            /// </summary>
            public string DvrIp { get; set; }
            /// <summary>
            /// Dvr名称
            /// </summary>
            public string DvrName { get; set; }
            /// <summary>
            /// Dvr位置
            /// </summary>
            public string DvrPosition { get; set; }
            /// <summary>
            /// 录像下载ID
            /// </summary>
            private int downId = -1;
            /// <summary>
            /// 要下载文件名称（.mp4）
            /// </summary>
            public string DownName { get; set; }
            ///// <summary>
            ///// 报警监听Id
            ///// </summary>
            //public int ListenId { get; set; }
            /// <summary>
            /// 下载文件大小
            /// </summary>
            public long FileSize { get; set; }
            /// <summary>
            /// 下载进度
            /// </summary>
            public int DownProgress { get; set; }
            /// <summary>
            /// 预览ID
            /// </summary>
            public int RealId { get; set; }
            /// <summary>
            /// 回放Id
            /// </summary>
            public int PlaybackId { get; set; }
            /// <summary>
            /// 回放文件名
            /// </summary>
            public string PlaybackName { get; set; }
            /// <summary>
            /// 回放进度
            /// </summary>
            public int PlaybackProgress { get; set; }
            /// <summary>
            /// 抓图保存路径(.bmp)
            /// </summary>
            public string SaveBMPPath { get; set; }
            /// <summary>
            /// 通道信息
            /// </summary>
            public List<ChannelState> Channels { get; set; }

            /// <summary>
            /// 布撤防ID
            /// </summary>
            public int AlarmId { get; set;}

            /// <summary>
            /// 监听端口
            /// </summary>
            public int ListenPort { get; set; }
            /// <summary>
            /// 本机监听Ip
            /// </summary>
            public string ListenIp { get; set; }
            /// <summary>
            /// 用户登录返回ID
            /// </summary>
            public int UserId
            {
                get
                {
                    return userId;
                }

                set
                {
                    userId = value;
                }
            }
            /// <summary>
            /// 录像下载ID
            /// </summary>
            public int DownId
            {
                get
                {
                    return downId;
                }

                set
                {
                    downId = value;
                }
            }
        }
        /// <summary>
        /// 摄像头信息
        /// </summary>
        public class CameraInfo
        {
            /// <summary>
            /// DVR ip地址
            /// </summary>
            public string DvrIp { get; set; }
            /// <summary>
            /// Dvr通道
            /// </summary>
            public int DvrChannel { get; set; }
            /// <summary>
            /// Dvr端口
            /// </summary>
            public int DvrPort { get; set; }
            /// <summary>
            /// Dvr用户名
            /// </summary>
            public string DvrUserName { get; set; }
            /// <summary>
            /// Dvr用户密码
            /// </summary>
            public string DvrUserPwd { get; set; }
            /// <summary>
            /// DVR登录ID
            /// </summary>
            public int DvrUserId { get; set; }
            /// <summary>
            /// DVR设备名称
            /// </summary>
            public string DvrNamer { get; set; }
            /// <summary>
            /// DVR设备地址
            /// </summary>
            public string DvrPosition { get; set; }
            /// <summary>
            /// 摄像头名称
            /// </summary>
            public string CameraNamer { get; set; }
            /// <summary>
            /// 工厂类型
            /// </summary>
            private int factoryType = 0;
            /// <summary>
            /// 工厂类型 0为HIKVision 18为宇视
            /// </summary>
            public int FactoryType { get => factoryType; set => factoryType = value; }

        }
        #endregion

        #region 报警事件信息
        /// <summary>
        /// 报警事件信息
        /// </summary>
        public struct AlarmInof
        {
            /// <summary>
            /// 设备IP地址
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 事件名称
            /// </summary>
            public string EventName { get; set; }
            /// <summary>
            /// 事件通道（dvr通道或者硬盘号）
            /// </summary>
            public string EventChannel { get; set; }
            /// <summary>
            /// 事件时间
            /// </summary>
            public DateTime EventTime { get; set; }
            /// <summary>
            /// 报警输入口
            /// </summary>
            public string AlaarmInChannel { get; set; }
            ///// <summary>
            ///// 硬盘号
            ///// </summary>
            //public string EventDiskNumber { get; set; }
        }
        #endregion

        #region  回放文件
        /// <summary>
        /// 回放文件属性
        /// </summary>
        public struct BackFile
        {
            /// <summary>
            /// 文件名称
            /// </summary>
            public string FileName { get; set; }
            /// <summary>
            /// 回放开始时间
            /// </summary>
            public DateTime StartTime { get; set; }
            /// <summary>
            /// 回放结束时间
            /// </summary>
            public DateTime EndTime { get; set; }
            /// <summary>
            /// 文件的大小
            /// </summary>
            public long FileSize;
            /// <summary>
            /// 卡号
            /// </summary>
            public string CardNum;
            /// <summary>
            /// 文件是否被锁定，1- 文件已锁定；0-文件未锁定
            /// </summary>
            public int Locked;

            /// <summary>
            /// 文件类型：0-定时录像，1-移动侦测，2-报警触发，3-报警|移动侦测，4-报警和移动侦测，5-命令触发，6-手动录像，7-震动报警，8-环境报警，9-智能报警，10-PIR报警，11-无线报警，12-呼救报警，13-移动侦测、PIR、无线、呼救等所有报警类型的"或"，14-智能交通事件，15-越界侦测，16-区域入侵，17-声音异常，18-场景变更侦测  
            /// </summary>
            public int FileType;
        }
        #endregion

        #region 通道状态
        /// <summary>
        /// 通道状态
        /// </summary>
        public struct ChannelState
        {
            /// <summary>
            /// 通道号
            /// </summary>
            public int Num { get; set; }
            /// <summary>
            /// 通道是否处于开启状态（模拟通道）
            /// </summary>
            public bool IsEnabled { get; set; }
            /// <summary>
            /// 通道是否处于在线状态（数字通道）
            /// </summary>
            public bool IsOnline { get; set; }
            /// <summary>
            /// 是否是数字通道
            /// </summary>
           public bool IsIpChannel { get; set; }
            /// <summary>
            /// 是否空闲
            /// </summary>
            public bool IsNull { get; set; }
        }
        #endregion

        #region 录像回放结构体
        /// <summary>
        /// 录像回放结构体
        /// </summary>
        /// <remarks>Linux 64位系统不支持软解码功能，因此需要将窗口句柄传空，设置回调函数，只取流不解码显示。</remarks>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VOD_PARA
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 流ID信息
            /// </summary>
            public NET_DVR_STREAM_INFO struIDInfo;
            /// <summary>
            /// 回放开始时间
            /// </summary>
            public NET_DVR_TIME struBeginTime;
            /// <summary>
            /// 回放结束时间
            /// </summary>
            public NET_DVR_TIME struEndTime;
            /// <summary>
            /// 回放的窗口句柄，若置为空，SDK仍能收到码流数据，但不解码显示.
            /// </summary>
            public IntPtr hWnd;
            /// <summary>
            /// 是否抽帧：0- 不抽帧，1- 抽帧
            /// </summary>
            public byte byDrawFrame;
            /// <summary>
            /// 0-普通录像卷，1-存档卷，适用于CVR设备，普通卷用于通道录像，存档卷用于备份录像
            /// </summary>
            public byte byVolumeType;
            /// <summary>
            /// 存档卷号
            /// </summary>
            public byte byVolumeNum;
            /// <summary>
            /// 保留
            /// </summary>
            public byte byRes1;
            /// <summary>
            /// 存档卷上的录像文件索引，搜索存档卷录像时返回的值.
            /// </summary>
            public uint dwFileIndex;

            /// <summary>
            /// 回放音频文件：0- 不回放音频文件，1- 回放音频文件，该功能需要设备支持，启动音频回放后只回放音频文件 .
            /// </summary>
            public byte byAudioFile;

            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 流ID信息结构体
        /// <summary>
        /// 流ID信息结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_INFO
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 流ID，为字母、数字和"_"的组合。全部为0时，无效。
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byID;
            /// <summary>
            /// 关联的设备通道。等于0xffffffff时，如果是设置流的来源信息(NET_DVR_SET_STREAM_SRC_INFO)，表示不关联；如果是作为其他如NET_DVR_SET_STREAM_RECORD_INFO、NET_DVR_SET_STREAM_RECORD_STATUS、NET_DVR_SET_MONITOR_VQDCFG等配置时的输入条件参数时，表示无效。 
            /// </summary>
            public uint dwChannel;
            /// <summary>
            /// 保留，置为0。
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 初始化参数
            /// </summary>
            public void init()
            {
                byID = new byte[STREAM_ID_LEN];
                byRes = new byte[32];
            }
        }
        #endregion

        #region 回放或者下载信息结构体
        /// <summary>
        /// 回放或者下载信息结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PLAYCOND
        {
            /// <summary>
            /// 通道号
            /// </summary>
            public uint dwChannel;
            /// <summary>
            /// 开始时间
            /// </summary>
            public NET_DVR_TIME struStartTime;
            /// <summary>
            /// 结束时间
            /// </summary>
            public NET_DVR_TIME struStopTime;
            /// <summary>
            /// 码流类型：0- 主码流，1- 子码流，2- 码流三 
            /// </summary>
            public byte byStreamType;
            /// <summary>
            /// 是否抽帧：0- 不抽帧，1- 抽帧 
            /// </summary>
            public byte byDrawFrame;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byStreamID;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 63, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region ATM文件查找条件结构体
        /// <summary>
        /// ATM文件查找条件结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ATMFINDINFO
        {
            /// <summary>
            /// 交易类型：0- 全部，1- 查询，2- 取款，3- 存款，4- 修改密码，5- 转账，6- 无卡查询，7- 无卡存款，8- 吞钞，9- 吞卡，10- 自定义 
            /// </summary>
            public byte byTransactionType;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;    //保留
            /// <summary>
            /// 交易金额
            /// </summary>
            public uint dwTransationAmount;     //交易金额 ;
        }
        #endregion

        #region 智能查找条件联合体
        /// <summary>
        /// 智能查找条件联合体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct NET_DVR_SPECIAL_FINDINFO_UNION
        {
            /// <summary>
            /// 联合体大小，8字节
            /// </summary>
            [FieldOffsetAttribute(0)]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byLenth;
            /// <summary>
            /// ATM查询条件 
            /// </summary>
            [FieldOffsetAttribute(0)]
            public NET_DVR_ATMFINDINFO struATMFindInfo;	       //ATM查询
        }
        #endregion

        #region 文件查找条件结构体
        /// <summary>
        /// 文件查找条件结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FILECOND_V40
        {
            /// <summary>
            /// 通道号
            /// </summary>
            public Int32 lChannel;
            /// <summary>
            /// 录象文件类型（根据dwUseCardNo参数是否带卡号查找分为两类）
            /// </summary>
            public uint dwFileType;
            /// <summary>
            /// 是否锁定：0-未锁定文件，1-锁定文件，0xff表示所有文件（包括锁定和未锁定）
            /// </summary>
            public uint dwIsLocked;
            /// <summary>
            /// 是否带ATM信息进行查询：0-不带ATM信息，1-按交易卡号查询，2-按交易类型查询，3-按交易金额查询，4-按卡号、交易类型及交易金额的组合查询，5-按课程名称查找（此时卡号表示课程名称）
            /// </summary>
            public uint dwUseCardNo;
            /// <summary>
            /// dwUseCardNo为1、4时表示卡号，有效字符个数为20；dwUseCardNo为5时表示课程名称
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CARDNUM_LEN_OUT, ArraySubType = UnmanagedType.I1)]
            public byte[] sCardNumber;
            /// <summary>
            /// 开始时间
            /// </summary>
            public NET_DVR_TIME struStartTime;
            /// <summary>
            /// 结束时间
            /// </summary>
            public NET_DVR_TIME struStopTime;
            /// <summary>
            /// 是否抽帧：0- 不抽帧，1- 抽帧 
            /// </summary>
            public byte byDrawFrame;
            /// <summary>
            /// 0- 查询普通卷，1- 查询存档卷(适用于CVR设备，普通卷用于通道录像，存档卷用于备份录像))，2- 查询N+1录像文件。
            /// </summary>
            public byte byFindType;
            /// <summary>
            /// 0- 普通查询，1- 快速（日历）查询，快速查询速度快于普通查询但是相关的录像信息减少（比如没有文件名、文件类型等）
            /// </summary>
            public byte byQuickSearch;
            /// <summary>
            /// 专有查询类型：0-无效，1-带ATM信息的查询
            /// </summary>
            public byte bySpecialFindInfoType;
            /// <summary>
            /// 存档卷号，适用于CVR设备 
            /// </summary>
            public uint dwVolumeNum;
            /// <summary>
            /// 工作机GUID，通过获取N+1设备信息（NET_DVR_WORKING_DEVICE_INFO）得到，byFindType为2时有效。
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = GUID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byWorkingDeviceGUID;
            /// <summary>
            /// 专有查询条件联合体 
            /// </summary>
            public NET_DVR_SPECIAL_FINDINFO_UNION uSpecialFindInfo;
            /// <summary>
            /// 码流类型：0- 无意义，1- 主码流，2- 子码流，3- 码流三，0xff- 全部
            /// </summary>
            public byte byStreamType;
            /// <summary>
            /// 查找音频文件：0- 不搜索音频文件，1- 搜索音频文件，该功能需要设备支持，启动音频搜索后只搜索音频文件
            /// </summary>
            public byte byAudioFile;

            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;    //保留
        }
        #endregion

        #region 录像文件信息结构体
        /// <summary>
        /// 录像文件信息结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FIND_DATA
        {
            /// <summary>
            /// 文件名 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;
            /// <summary>
            /// 文件的开始时间
            /// </summary>
            public NET_DVR_TIME struStartTime;
            /// <summary>
            /// 文件的结束时间
            /// </summary>
            public NET_DVR_TIME struStopTime;
            /// <summary>
            /// 文件大小
            /// </summary>
            public uint dwFileSize;
        }
        #endregion

        #region 录像文件信息结构体V30
        /// <summary>
        /// 录像文件信息结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FINDDATA_V30
        {
            /// <summary>
            /// 文件名
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;
            /// <summary>
            /// 文件的开始时间
            /// </summary>
            public NET_DVR_TIME struStartTime;
            /// <summary>
            /// 文件的结束时间
            /// </summary>
            public NET_DVR_TIME struStopTime;
            /// <summary>
            /// 文件的大小
            /// </summary>
            public uint dwFileSize;
            /// <summary>
            /// 卡号
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
            /// <summary>
            /// 文件是否被锁定，1- 文件已锁定；0-文件未锁定
            /// </summary>
            public byte byLocked;
            /// <summary>
            /// 文件类型：0-定时录像，1-移动侦测，2-报警触发，3-报警|移动侦测，4-报警和移动侦测，5-命令触发，6-手动录像，7-震动报警，8-环境报警，9-智能报警，10-PIR报警，11-无线报警，12-呼救报警，13-移动侦测、PIR、无线、呼救等所有报警类型的"或"，14-智能交通事件，15-越界侦测，16-区域入侵，17-声音异常，18-场景变更侦测  
            /// </summary>
            public byte byFileType;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region 文件查找结果信息结构体V40
        /// <summary>
        /// 文件查找结果信息结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FINDDATA_V40
        {
            /// <summary>
            /// 文件名，日历查询时无效
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;
            /// <summary>
            /// 文件的开始时间
            /// </summary>
            public NET_DVR_TIME struStartTime;
            /// <summary>
            /// 文件的结束时间
            /// </summary>
            public NET_DVR_TIME struStopTime;
            /// <summary>
            /// 文件的大小
            /// </summary>
            public uint dwFileSize;
            /// <summary>
            /// 卡号 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
            /// <summary>
            /// 文件是否被锁定，1－文件已锁定；0－文件未锁定
            /// </summary>
            public byte byLocked;
            /// <summary>
            /// 文件类型（日历查询时无效）： 
            ///0-定时录像，1-移动侦测，2-报警触发，3-报警|移动侦测，4-报警|移动侦测，5-命令触发，6-手动录像， 
            ///7-震动报警，8-环境报警，9-智能报警，10-PIR报警，11-无线报警，12-呼救报警， 
            ///13-移动侦测、PIR、无线、呼救等所有报警类型的"或"，14-智能交通事件，15-越界侦测， 
            ///16-区域入侵侦测，17-音频异常侦测，18-场景变更侦测，19-智能侦测，20-人脸侦测 
            ///21-信号量，22-回传，23-回迁录像，24-遮挡，26-进入区域侦测，27-离开区域侦测， 
            ///28-徘徊侦测，29-人员聚集侦测，30-快速运动侦测，31-停车侦测，32-物品遗留侦测，33-物品拿取侦测
            /// </summary>
            public byte byFileType;
            /// <summary>
            /// 0- 普通查询结果，1- 快速（日历）查询结果 
            /// </summary>
            public byte byQuickSearch;
            /// <summary>
            /// 保留 
            /// </summary>
            public byte byRes;
            /// <summary>
            /// 文件索引号
            /// </summary>
            public uint dwFileIndex;
            /// <summary>
            /// 码流类型：0- 主码流，1- 子码流，2- 码流三 
            /// </summary>
            public byte byStreamType;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }
        #endregion

        #region 录象文件参数(带卡号)
        /// <summary>
        /// 录象文件参数(带卡号)
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FINDDATA_CARD
        {
            /// <summary>
            /// 文件名
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;
            /// <summary>
            /// 文件的开始时间
            /// </summary>
            public NET_DVR_TIME struStartTime;
            /// <summary>
            /// 文件的结束时间
            /// </summary>
            public NET_DVR_TIME struStopTime;
            /// <summary>
            /// 文件的大小
            /// </summary>
            public uint dwFileSize;
            /// <summary>
            /// 卡号
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
        }
        #endregion

        #region 录象文件查找条件结构
        /// <summary>
        /// 录象文件查找条件结构
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FILECOND
        {
            /// <summary>
            /// 通道号 
            /// </summary>
            public int lChannel;
            /// <summary>
            /// 录象文件类型（根据dwUseCardNo参数是否带卡号查找分为两类），0xff－全部。
            /// </summary>
            public uint dwFileType;
            /// <summary>
            /// 是否锁定：0-未锁定文件，1-锁定文件，0xff表示所有文件（包括锁定和未锁定）
            /// </summary>
            public uint dwIsLocked;
            /// <summary>
            /// 是否带卡号查找
            /// </summary>
            public uint dwUseCardNo;
            /// <summary>
            /// 卡号
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] sCardNumber;
            /// <summary>
            /// 开始时间
            /// </summary>
            public NET_DVR_TIME struStartTime;
            /// <summary>
            /// 结束时间
            /// </summary>
            public NET_DVR_TIME struStopTime;
        }
        #endregion

        #region 设备工作状态信息结构体
        /// <summary>
        /// 设备工作状态信息结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE
        {
            /// <summary>
            /// 设备的状态：0－正常；1－CPU占用率太高，超过85%；2－硬件错误，例如串口异常
            /// </summary>
            public uint dwDeviceStatic;
            /// <summary>
            /// 硬盘状态 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            /// <summary>
            /// 通道状态 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE[] struChanStatic;
            /// <summary>
            /// 报警输入口的状态：0-没有报警；1-有报警 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatic;
            /// <summary>
            /// 报警输出口的状态：0-没有输出，1-有报警输出
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutStatic;
            /// <summary>
            /// 本地显示状态：0-正常，1-不正常
            /// </summary>
            public uint dwLocalDisplay;
            /// <summary>
            /// 初始化参数
            /// </summary>
            public void Init()
            {
                struHardDiskStatic = new NET_DVR_DISKSTATE[MAX_DISKNUM];
                struChanStatic = new NET_DVR_CHANNELSTATE[MAX_CHANNUM];
                byAlarmInStatic = new byte[MAX_ALARMIN];
                byAlarmOutStatic = new byte[MAX_ALARMOUT];
            }
        }
        #endregion

        #region 工作状态巡检参数结构体
        /// <summary>
        /// 工作状态巡检参数结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CHECK_DEV_STATE
        {
            /// <summary>
            /// 定时检测设备工作状态，单位：ms，0表示使用默认值(30000)，最小值为1000 
            /// </summary>
            public uint dwTimeout;
            /// <summary>
            /// 设备状态信息回调函数
            /// </summary>
            public DEV_WORK_STATE_CB fnStateCB;
            /// <summary>
            /// 用户数据 
            /// </summary>
            public IntPtr pUserData;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 60, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }
        #endregion

        #region 硬盘状态
        /// <summary>
        /// 硬盘状态
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISKSTATE
        {
            /// <summary>
            /// 硬盘容量，单位：MB 
            /// </summary>
            public uint dwVolume;
            /// <summary>
            /// 硬盘剩余空间，单位：MB 
            /// </summary>
            public uint dwFreeSpace;
            /// <summary>
            /// 硬盘的状态：0- 活动，1- 休眠，2- 异常，3- 休眠硬盘出错，4- 未格式化，5- 未连接状态(网络硬盘)，6- 硬盘正在格式化 
            /// </summary>
            public uint dwHardDiskStatic;
        }
        #endregion

        #region 通道状态

        /// <summary>
        /// 通道状态
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CHANNELSTATE
        {
            /// <summary>
            /// 通道是否在录像：0－不录像；1－录像 
            /// </summary>
            public byte byRecordStatic;
            /// <summary>
            /// 连接的信号状态,0-正常,1-信号丢失
            /// </summary>
            public byte bySignalStatic;
            /// <summary>
            /// 通道硬件状态：0－正常，1－异常（例如DSP异常） 
            /// </summary>
            public byte byHardwareStatic;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            public byte reservedData;
            /// <summary>
            /// 实际码率
            /// </summary>
            public uint dwBitRate;
            /// <summary>
            /// 客户端连接的个数
            /// </summary>
            public uint dwLinkNum;
            /// <summary>
            /// 客户端的IP地址(这里为网络字节序)
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LINK, ArraySubType = UnmanagedType.U4)]
            public uint[] dwClientIP;
        }
        #endregion

        #region 通道状态(9000扩展)
        /// <summary>
        /// 通道状态(9000扩展)
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CHANNELSTATE_V30
        {
            /// <summary>
            /// 通道是否在录像,0-不录像,1-录像
            /// </summary>
            public byte byRecordStatic;
            /// <summary>
            /// 连接的信号状态,0-正常,1-信号丢失
            /// </summary>
            public byte bySignalStatic;
            /// <summary>
            /// 通道硬件状态,0-正常,1-异常,例如DSP死掉
            /// </summary>
            public byte byHardwareStatic;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            public byte byRes1;
            /// <summary>
            /// 实际码率
            /// </summary>
            public uint dwBitRate;
            /// <summary>
            /// 客户端连接的个数
            /// </summary>
            public uint dwLinkNum;
            /// <summary>
            /// 客户端的IP地址
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LINK, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPADDR[] struClientIP;
            /// <summary>
            /// 如果该通道为IP接入，那么表示IP接入当前的连接数
            /// </summary>
            public uint dwIPLinkNum;
            /// <summary>
            /// 是否超出了单路6路连接数 0 - 未超出, 1-超出
            /// </summary>
            public byte byExceedMaxLink;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 当前的通道号，0xffffffff表示当前及后续通道信息无效
            /// </summary>
            public uint dwChannelNo;
            /// <summary>
            /// 所有客户端和该通道连接的实际码率之和
            /// </summary>
            public uint dwAllBitRate;
            //public void Init()
            //{
            //    struClientIP = new NET_DVR_IPADDR[MAX_LINK];

            //    for (int i = 0; i < MAX_LINK; i++)
            //    {
            //        struClientIP[i].Init();
            //    }
            //    byRes = new byte[12];
            //}
        }
        #endregion

        #region 设备工作状态扩展结构体
        /// <summary>
        /// 设备工作状态扩展结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE_V40
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 设备的状态：0－正常；1－CPU占用率太高，超过85%；2－硬件错误，例如串口异常
            /// </summary>
            public uint dwDeviceStatic;
            /// <summary>
            /// 硬盘状态,一次最多只能获取33个硬盘信息
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            /// <summary>
            /// 通道的状态，从前往后顺序排列
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V40, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE_V30[] struChanStatic;
            /// <summary>
            /// 有报警的报警输入口，按值表示，按下标值顺序排列，值为0xffffffff时当前及后续值无效
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHasAlarmInStatic;
            /// <summary>
            /// 有报警输出的报警输出口，按值表示，按下标值顺序排列，值为0xffffffff时当前及后续值无效
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHasAlarmOutStatic;
            /// <summary>
            /// 本地显示状态,0-正常,1-不正常
            /// </summary>
            public uint dwLocalDisplay;
            /// <summary>
            /// 表示语音通道的状态，第0位表示第1个语音通道，第1位表示第2个语音通道，数组元素值：0-未使用，1-使用中，0xff无效 
            /// </summary>	
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAudioChanStatus;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 126, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region 工作状态(9000扩展)
        /// <summary>
        /// 工作状态(9000扩展)
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE_V30
        {
            /// <summary>
            /// 设备的状态,0-正常,1-CPU占用率太高,超过85%,2-硬件错误,例如串口死掉
            /// </summary>
            public uint dwDeviceStatic;
            /// <summary>
            /// 硬盘状态 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            /// <summary>
            /// 通道状态
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE_V30[] struChanStatic;
            /// <summary>
            /// 报警输入口的状态：0-没有报警；1-有报警
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatic;
            /// <summary>
            /// 报警输出口的状态：0-没有输出，1-有报警输出 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutStatic;
            /// <summary>
            /// 本地显示状态：0-正常，1-不正常
            /// </summary>
            public uint dwLocalDisplay;
            /// <summary>
            /// 表示语音通道的状态：0-未使用，1-使用中，0xff无效 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAudioChanStatus;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                struHardDiskStatic = new NET_DVR_DISKSTATE[MAX_DISKNUM_V30];
                struChanStatic = new NET_DVR_CHANNELSTATE_V30[MAX_CHANNUM_V30];
                //for (int i = 0; i < MAX_CHANNUM_V30; i++)
                //{
                //    struChanStatic[i].Init();
                //}
                byAlarmInStatic = new byte[MAX_ALARMOUT_V30];
                byAlarmOutStatic = new byte[MAX_ALARMOUT_V30];
                byAudioChanStatus = new byte[MAX_AUDIO_V30];
                byRes = new byte[10];
            }
        }
        #endregion

       

        //#region  IP设备资源及IP通道资源配置结构体 
        ///// <summary>
        ///// IP设备资源及IP通道资源配置结构体
        ///// </summary>
        //public struct NET_DVR_IPPARACFG_V40
        //{
        //    /// <summary>
        //    /// 结构大小
        //    /// </summary>
        //    public uint dwSize;
        //    /// <summary>
        //    /// 设备支持的总组数（只读）。若设备支持的组数大于1，NET_DVR_GetDVRConfig（或者NET_DVR_SetDVRConfig）获取（或设置）相关通道参数需要按照组数调用多次命令分别获取（或设置）各组通道参数，此时接口中lChannel对应组号。 
        //    /// </summary>
        //    public uint dwGroupNum;
        //    /// <summary>
        //    /// 最大模拟通道个数（只读）
        //    /// </summary>
        //    public uint dwAChanNum;
        //    /// <summary>
        //    /// 数字通道个数（只读） 
        //    /// </summary>
        //    public uint dwDChanNum;
        //    /// <summary>
        //    /// 起始数字通道（只读） 
        //    /// </summary>
        //    public uint dwStartDChan;
        //    /// <summary>
        //    /// 模拟通道资源是否启用，数组下标与通道号一一对应，取值：0-禁用，1-启用。
        //    /// 例如：byAnalogChanEnable[i]=1表示通道(i+1)启用
        //    /// </summary>
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
        //    public byte[] byAnalogChanEnable;
        //    /// <summary>
        //    /// IP设备信息，下标0对应设备IP ID为1 
        //    /// </summary>
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE_V40, ArraySubType = UnmanagedType.Struct)]
        //    public NET_DVR_IPDEVINFO_V31[] struIPDevInfo;
        //    /// <summary>
        //    /// 取流模式
        //    /// </summary>
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.Struct)]
        //    public NET_DVR_STREAM_MODE[] struStreamMode;
        //    /// <summary>
        //    /// 保留，置为0
        //    /// </summary>
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
        //    public byte[] byRes2;
        //}
        //#endregion

        #region  IP设备信息结构体 
        /// <summary>
        /// IP设备信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPDEVINFO_V31
        {/// <summary>
         /// 该IP设备是否有效
         /// </summary>
            public byte byEnable;//该IP设备是否有效
            /// <summary>
            /// 协议类型(默认为私有协议)，0- 私有协议，1- 松下协议，2- 索尼，更多协议通过NET_DVR_GetIPCProtoList获取。 
            /// </summary>
            public byte byProType;
            /// <summary>
            /// 0-不支持快速添加；1-使用快速添加 
            /// </summary>
            public byte byEnableQuickAdd;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            public byte byRes1;//保留字段，置0
            /// <summary>
            /// 用户名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sUserName;//用户名
            /// <summary>
            /// 密码
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPassword;//密码
            /// <summary>
            /// 设备域名 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string byDomain;//设备域名
            /// <summary>
            /// ip地址
            /// </summary>
            public NET_DVR_IPADDR struIP;//IP地址
            /// <summary>
            /// 端口号
            /// </summary>
            public ushort wDVRPort;// 端口号
            /// <summary>
            /// 保留字段，置0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//保留字段，置0
            /// <summary>
            /// 初始化参数
            /// </summary>
            public void Init()
            {
                byRes2 = new byte[34];
            }
        }
        #endregion

        //#region  取流方式配置结构体 
        ///// <summary>
        ///// 取流方式配置结构体
        ///// </summary>
        //[StructLayout(LayoutKind.Sequential)]
        //public struct NET_DVR_STREAM_MODE
        //{
        //    /// <summary>
        //    /// 取流方式：
        //    ///0- 直接从设备取流，对应联合体中结构NET_DVR_IPCHANINFO；
        //    ///1- 从流媒体取流，对应联合体中结构NET_DVR_IPSERVER_STREAM；
        //    ///2- 通过IPServer获得IP地址后取流，对应联合体中结构NET_DVR_PU_STREAM_CFG；
        //    ///3- 通过IPServer找到设备，再通过流媒体取设备的流，对应联合体中结构NET_DVR_DDNS_STREAM_CFG；
        //    ///4- 通过流媒体由URL去取流，对应联合体中结构NET_DVR_PU_STREAM_URL；
        //    ///5- 通过hiDDNS域名连接设备然后从设备取流，对应联合体中结构NET_DVR_HKDDNS_STREAM；
        //    ///6- 直接从设备取流(扩展)，对应联合体中结构NET_DVR_IPCHANINFO_V40
        //    /// </summary>
        //    public byte byGetStreamType;
        //    /// <summary>
        //    /// 保留，置为0 
        //    /// </summary>
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
        //    public byte[] byRes;
        //    /// <summary>
        //    /// 不同取流方式联合体 
        //    /// </summary>
        //    public NET_DVR_GET_STREAM_UNION uGetStream;
        //    /// <summary>
        //    /// 初始化参数
        //    /// </summary>
        //    public void Init()
        //    {
        //        byGetStreamType = 0;
        //        byRes = new byte[3];
        //        //uGetStream.Init();
        //    }
        //}
        //#endregion

        //#region  取流方式联合体 
        ///// <summary>
        ///// 取流方式联合体
        ///// </summary>
        //[StructLayout(LayoutKind.Explicit)]
        //public struct NET_DVR_GET_STREAM_UNION
        //{
        //    [FieldOffset(0)]
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 492, ArraySubType = UnmanagedType.I1)]
        //    public byte[] byUnion;
        //    //public void Init()
        //    //{
        //    //    byUnion = new byte[492];
        //    //}
        //}
        //#endregion

    
    }
}
