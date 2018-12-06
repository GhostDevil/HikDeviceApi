using System.Runtime.InteropServices;
using static HikDeviceApi.HikStruct;
using static HikDeviceApi.HikConst;
using System;

namespace HikDeviceApi.TVWall
{
    /// <summary>
    /// 日 期:2017-06-27
    /// 作 者:痞子少爷
    /// 描 述:海康多路解码器接口结构
    /// </summary>
    public class HikTVWallStruct
    {
        /// <summary>
        /// 窗口操作信息
        /// </summary>
        public struct WinCtrlInfo
        {
            /// <summary>
            /// 用户id
            /// </summary>
            public int UserId { get; set; }
            /// <summary>
            /// 电视墙号
            /// </summary>
            public uint WallNo { get; set; }
            /// <summary>
            /// 窗口号
            /// </summary>
            public uint WinNo { get; set; }
            /// <summary>
            /// 子窗口号
            /// </summary>
            public uint SubWinNo { get; set; }
            /// <summary>
            /// 设备号
            /// </summary>
            public uint DeviceNo { get; set; }
            /// <summary>
            /// 输出口
            /// </summary>
            public uint OutNo { get; set; }
            /// <summary>
            /// 是否使能0- 否，1- 是
            /// </summary>
            public byte IsEnable { get; set; }
            /// <summary>
            /// 窗口列坐标
            /// </summary>
            public ushort IndexX { get; set; }
            /// <summary>
            /// 窗口行坐标
            /// </summary>
            public ushort IndexY { get; set; }
            /// <summary>
            /// 分割子窗口数
            /// </summary>
            public int SplitNum { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CHAN_RELATION_RESOURCE
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 显示通道号（1字节设备号+1字节保留+2字节显示通道号）
            /// </summary>
            public uint dwDisplayChan;
            /// <summary>
            /// 是否关联子窗口音频
            /// </summary>
            public byte byRelateAudio;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 关联音频的子窗口号（1字节电视墙号+1字节子窗口号+2字节窗口号）
            /// </summary>
            public uint dwSubWinNo;
            /// <summary>
            /// 编码通道号，获取全部时有效
            /// </summary>
            public uint dwChannel;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        /// <summary>
        /// 窗口信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WALLWINCFG
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 是否使能：0- 否，1- 是
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 窗口号
            /// </summary>
            public uint dwWinNum;
            /// <summary>
            /// 窗口相对应的图层号
            /// </summary>
            public uint dwLayerIndex;
            /// <summary>
            /// 目的窗口(相对显示墙)
            /// </summary>
            public NET_DVR_RECTCFG struWinPosition;
            /// <summary>
            /// 分布式大屏控制器设备序号
            /// </summary>
            public uint dwDeviceIndex;
            /// <summary>
            /// 输入信号源
            /// </summary>
            public ushort wInputIndex;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /// <summary>
        /// 窗口相关参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WALLWINPARAM
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 使能透明度，0-关，非0-开	
            /// </summary>
            public byte byTransparency;
            /// <summary>
            /// 窗口分屏模式（..25 36）,同时最多25+36
            /// </summary>
            public byte byWinMode;
            /// <summary>
            /// 畅显使能，0-关，1-开
            /// </summary>
            public byte byEnableSpartan;
            /// <summary>
            /// 为窗口分配的解码资源，1-D1,2-720P,3-1080P
            /// </summary>
            public byte byDecResource;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        /// <summary>
        /// 电视墙输出位置配置
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOWALLDISPLAYPOSITION
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 使能：0- 禁用，1- 启用
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 电视墙号(组合)
            /// </summary>
            public uint dwVideoWallNo;
            /// <summary>
            /// 显示输出号(组合)，批量获取全部时有效
            /// </summary>
            public uint dwDisplayNo;
            /// <summary>
            /// 位置坐标，须为基准坐标（通过能力集获取）的整数倍，宽度和高度值不用设置，即为基准值
            /// </summary>
            public NET_DVR_RECTCFG_EX struRectCfg;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /// <summary>
        /// 显示输出参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DISPLAYPARAM
        {
            /// <summary>
            /// 显示输出号
            /// </summary>
            public uint dwDisplayNo;
            /// <summary>
            /// 输出连接模式,1-BNC，2-VGA，3-HDMI，4-DVI，5-SDI, 6-FIBER ，7-RGB, 8-YPrPb, 9-VGA/HDMI/DVI自适应，10-3GSDI,11-VGA/DVI自适应，12-HDBaseT, 0xff-无效
            /// </summary>
            public byte byDispChanType;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /// <summary>
        /// 显示输出参数配置
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DISPLAYCFG
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 显示输出参数
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DISPLAY_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISPLAYPARAM[] struDisplayParam;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 窗口信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOWALLWINDOWPOSITION
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 窗口使能,0-不使能，1-使能 
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 窗口操作模式，0-统一坐标，1-分辨率坐标
            /// </summary>
            public byte byWndOperateMode;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 窗口号
            /// </summary>
            public uint dwWindowNo;
            /// <summary>
            /// 窗口相对应的图层号，图层号到最大即置顶，置顶操作
            /// </summary>
            public uint dwLayerIndex;
            /// <summary>
            /// 目的窗口统一坐标(相对显示墙)，获取或按统一坐标设置时有效
            /// </summary>
            public NET_DVR_RECTCFG_EX struRect;
            /// <summary>
            /// 目的窗口分辨率坐标，获取或按分辨率坐标设置有效
            /// </summary>
            public NET_DVR_RECTCFG_EX struResolution;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        /// <summary>
        /// 电视墙窗口信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_WALLWIN_INFO
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 窗口号(组合)
            /// </summary>
            public uint dwWinNum;
            /// <summary>
            /// 子窗口号(组合)
            /// </summary>
            public uint dwSubWinNum;
            /// <summary>
            /// 电视墙号(组合)
            /// </summary>
            public uint dwWallNo;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 电视墙信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_VIDEO_WALL_INFO
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 窗口号(组合)：1 字节电视墙号+1 字节保留+2 字节窗口号(保留)
            /// </summary>
            public uint dwWindowNo;
            /// <summary>
            /// 场景号
            /// </summary>
            public uint dwSceneNo;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 场景控制参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_SCENE_CONTROL_INFO
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 电视墙信息
            /// </summary>
            public NET_DVR_VIDEO_WALL_INFO struVideoWallInfo;
            /// <summary>
            /// 场景控制命令：1-场景模式切换（如果要切换的是当前场景，则不进行切换），2-初始化场景（将此场景的配置清空，如果是当前场景，则同时对当前场景进行清屏操作），3-强制切换（无论是否是当前场景，强制切换），4-保存当前模式到某场景
            /// </summary>
            public uint dwCmd;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        /// <summary>
        /// 电视墙显示输出参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WALLOUTPUTPARAM
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 输出分辨率 LED分辨率（wLEDWidth和wLEDHeight）和EDID分辨率没有设置和启用时有效
            /// </summary>
            public uint dwResolution;
            /// <summary>
            /// 视频参数 保留
            /// </summary>
            public NET_DVR_VIDEOEFFECT struRes;
            /// <summary>
            /// 视频制式 0保留 1n 2p
            /// </summary>
            public byte byVideoFormat;
            /// <summary>
            /// 输出连接方式只能获取 1BNC 2VGA 3HDMI 4DVI 5SDI 6FIBER 7RGB 8YprPb  9VGA HDMI DVI 自适应 10 3GSDI  11 VGA DVI自适应 0xff无效
            /// </summary>
            public byte byDisplayMode;
            /// <summary>
            /// 背景颜色
            /// </summary>
            public byte byBackgroundColor;
            /// <summary>
            /// 是否启用EDID分辨率 如果设置led分辨率 则无效
            /// </summary>
            public byte byUseEDIDResolution;
            /// <summary>
            /// 自定义输出分辨率宽  为 0 代表连接的时lcd屏 否则led屏
            /// </summary>
            public uint wLEDWidth;
            /// <summary>
            /// 自定义输出分辨率高  为 0 代表连接的时lcd屏 否则led屏
            /// </summary>
            public uint wLEDHeight;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

        }
            /// <summary>
            /// 窗口状态结构体
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WALL_WIN_STATUS
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint dwSize;
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
            /// 视频解码帧率 
            /// </summary>
            public byte byFpsDecV;
            /// <summary>
            /// 音频解码帧率
            /// </summary>
            public byte byFpsDecA;
            ///// <summary>
            ///// DSP CPU利用率 
            ///// </summary>
            //public byte byCpuLoad;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
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
            /// 码流源 1网络 2综合平台内部编码子系统 0xff无效
            /// </summary>
            public byte byStreamMode;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            
        }
        /// <summary>
        /// 显示输出口信息
        /// </summary>
        public struct DisplayInfo
        {
            /// <summary>
            /// 输出口号
            /// </summary>
            public string Displayno { get; set; }
            /// <summary>
            /// 组号方式
            /// </summary>
            public string DisplaynoCom { get; set; }
            /// <summary>
            /// X坐标
            /// </summary>
            public string XCoordinate { get; set; }
            /// <summary>
            /// Y坐标
            /// </summary>
            public string YCoordinate { get; set; }
            /// <summary>
            /// 是否使能
            /// </summary>
            public string Enabled { get; set; }
            /// <summary>
            /// 连接模式
            /// </summary>
            public string DisplayType { get; set; }
        }
    }
}
