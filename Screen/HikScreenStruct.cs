using System.Runtime.InteropServices;
using static HikDeviceApi.HikStruct;

namespace HikDeviceApi.Screen
{
    /// <summary>
    /// 日 期:2016-01-25
    /// 作 者:痞子少爷
    /// 描 述:海康拼接屏控制结构体
    /// </summary>
    public static class HikScreenStruct
    {
        #region  设备操作参数结构 
        /// <summary>
        /// 设备操作参数结构
        /// </summary>
        public struct UseInfo
        {


        }
        #endregion

        #region 屏幕拼接参数结构体
        /// <summary>
        /// 屏幕拼接参数结构体
        /// </summary>
        public struct NET_DVR_SCREEN_SPLICE_CFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 自拼接屏号，0就是没有拼接，其他值表示自拼接屏的屏号
            /// </summary>
            public byte bySpliceIndex;
            /// <summary>
            /// 屏幕在自拼接屏中的行位置
            /// </summary>
            public byte bySpliceX;
            /// <summary>
            /// 屏幕在自拼接屏中的列位置
            /// </summary>
            public byte bySpliceY;
            /// <summary>
            /// 拼接规模宽，以屏幕宽为单位
            /// </summary>
            public byte byWidth;
            /// <summary>
            /// 拼接规模高，以屏幕高为单位
            /// </summary>
            public byte byHeight;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region 电视墙中屏幕参数结构体
        /// <summary>
        /// 电视墙中屏幕参数结构体
        /// </summary>
        public struct NET_DVR_SINGLEWALLPARAM
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 是否使能：0- 否，1- 是
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 电视墙屏幕号
            /// </summary>
            public uint dwWallNum;
            /// <summary>
            /// 屏幕区域，坐标必须为基准坐标（128×128）的整数倍。宽度和高度值不用设置，即为基准值 
            /// </summary>
            public NET_DVR_RECTCFG struRectCfg;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 电视墙中窗口参数结构体

        /// <summary>
        /// 电视墙中窗口参数结构体
        /// </summary>
        public struct NET_DVR_WALLWINCFG
        {
            /// <summary>
            /// 结构体大小
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
            /// 窗口相对应的图层号
            /// </summary>
            public uint dwLayerIndex;
            /// <summary>
            /// 窗口号
            /// </summary>
            public uint dwWinNum;
            /// <summary>
            /// 目的窗口（相对显示墙）
            /// </summary>
            public NET_DVR_RECTCFG struWinPosition;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 场景信息结构体
        /// <summary>
        /// 场景信息结构体
        /// </summary>
        /// <remarks>
        /// 对于LCD拼接屏，场景包括信源类型、拼接、边缘屏蔽、画中画、去雾，切换或保存场景时需要对每块屏发送命令。
        /// </remarks>
        public struct NET_DVR_WALLSCENECFG
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 场景名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sSceneName;
            /// <summary>
            /// 场景是否有效：0- 无效，1- 有效
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 场景号，只能获取。获取所有场景时该参数有效。
            /// </summary>
            public byte bySceneIndex;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 78, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region 大屏拼接结构体
        /// <summary>
        /// 大屏拼接参数结构体
        /// </summary>
        public struct NET_DVR_BIGSCREENCFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 大屏拼接使能：0- 不使能，1- 使能
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 大屏拼接模式（byModeX*byModeY）：水平方向屏幕个数 
            /// </summary>
            public byte byModeX;
            /// <summary>
            /// 大屏拼接模式（byModeX*byModeY）：垂直方向屏幕个数 
            /// </summary>
            public byte byModeY;
            /// <summary>
            /// 主屏槽位号 
            /// </summary>
            public byte byMainDecodeSystem;
            /// <summary>
            /// 主屏所用显示通道号
            /// </summary>
            public byte byMainDecoderDispChan;
            /// <summary>
            /// 视频制式（大屏每个子屏制式相同）：1- NTSC，2- PAL 
            /// </summary>
            public byte byVideoStandard;
            /// <summary>
            /// 保留 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 屏幕分辨率，大屏每个子屏分辨率相同，一个大屏可能有几个显示通道共同输出，这时就会把相关显示通道都设为该分辨率 
            /// </summary>
            public uint dwResolution;
            /// <summary>
            /// 大屏拼接单个子屏幕信息结构体
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BIGSCREENNUM, ArraySubType = UnmanagedType.I1)]
            public NET_DVR_SINGLESCREENCFG[] struFollowSingleScreen;
            /// <summary>
            /// 大屏在电视墙中起始X坐标，对于解码器该参数无效 
            /// </summary>
            public int wBigScreenX;
            /// <summary>
            /// 大屏在电视墙中起始Y坐标，对于解码器该参数无效 
            /// </summary>
            public int wBigScreenY;
            /// <summary>
            /// 保留 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 大屏拼接子屏幕信息结构体
        /// <summary>
        /// 大屏拼接子屏幕信息结构体
        /// </summary>
        public struct NET_DVR_SINGLESCREENCFG
        {
            /// <summary>
            /// 屏幕序号，0xff表示此屏幕不用 
            /// </summary>
            public byte byScreenSeq;
            /// <summary>
            /// 解码子系统槽位号
            /// </summary>
            public byte bySubSystemNum;
            /// <summary>
            /// 解码子系统上对应显示通道号
            /// </summary>
            public byte byDispNum;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region 信号源列表
        /// <summary>
        /// 信号源列表
        /// </summary>
        public struct NET_DVR_INPUTSTREAM_LIST
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 输入流信息:0- (INPUT_UNKNOWN)未知输入信息，1- (INPUT_VIDEO)视频信号，2- (INPUT_RGB)计算机信号，3- (INPUT_SCREENCAPTURE)网络抓屏
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CAM_COUNT, ArraySubType = UnmanagedType.AsAny)]
            public NET_DVR_INPUTSTREAMCFG[] struInputStreamInfo;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
    }
        #endregion

        #region 输入流参数结构体
        /// <summary>
        /// 输入流参数结构体
        /// </summary>
        public struct NET_DVR_INPUTSTREAMCFG
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 是否有效 
            /// </summary>
            public byte byValid;
            /// <summary>
            /// 信号输入源类型(参见CameraMode)
            /// </summary>
            public byte byCamMode;
            /// <summary>
            /// 信号源序号：0~224 
            /// </summary>
            public int wInputNo;
            /// <summary>
            /// 信号输入源名称 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sCamName;
            /// <summary>
            /// 视频参数 
            /// </summary>
            public NET_DVR_VIDEOEFFECT struVideoEffect;
            /// <summary>
            /// 输入流参数，ip输入时使用
            /// </summary>
            public NET_DVR_PU_STREAM_CFG struPuStream;
            /// <summary>
            /// 信号源所在的板卡号，只能获取
            /// </summary>
            public int wBoardNum;
            /// <summary>
            /// 信号源在板卡上的位置，只能获取 
            /// </summary>
            public int wInputIdxOnBoard;
            /// <summary>
            /// 分辨率X*Y
            /// </summary>
            public int wResolutionX;
            /// <summary>
            /// 分辨率X*Y
            /// </summary>
            public int wResolutionY;
            /// <summary>
            /// 视频制式，0-无，1-NTSC，2-PAL 
            /// </summary>
            public byte byVideoFormat;
            /// <summary>
            /// 网络信号源的分辨率：1- CIF，2- 4CIF，3- 720P，4- 1080P，5- 500万像素，在添加网络信号源时传给设备，设备根据这个分辨率来分配解码资源 
            /// </summary>
            public byte byNetSignalResolution;
            /// <summary>
            /// 网络信号源分组的组名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sGroupName;
            /// <summary>
            /// 关联矩阵：0-不关联，1-关联，当输入信号源为NET_DVR_CAM_BNC、NET_DVR_CAM_VGA、NET_DVR_CAM_DVI、NET_DVR_CAM_HDMI中的一种时，该参数有效。
            /// </summary>
            public byte byJointMatrix;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            public byte byRes;
    }
        #endregion

        #region 视频参数结构体
        /// <summary>
        /// 视频参数结构体
        /// </summary>
        public struct NET_DVR_VIDEOEFFECT
        {
            /// <summary>
            /// 亮度，取值范围[0,100] 
            /// </summary>
            public byte byBrightnessLevel;
            /// <summary>
            /// 对比度，取值范围[0,100]
            /// </summary>
            public byte byContrastLevel;
            /// <summary>
            /// 锐度，取值范围[0,100]
            /// </summary>
            public byte bySharpnessLevel;
            /// <summary>
            /// 饱和度，取值范围[0,100]
            /// </summary>
            public byte bySaturationLevel;
            /// <summary>
            /// 色度，取值范围[0,100]，保留 
            /// </summary>
            public byte byHueLevel;
            /// <summary>
            /// 使能，按位表示。bit0-SMART IR(防过曝)，bit1-低照度，bit2-强光抑制使能，值：0-否，1-是
            /// 例如byEnableFunc 0x2==1表示使能低照度功能； bit3-锐度类型，值：0-自动，1-手动。
            /// </summary>
            public byte byEnableFunc;
            /// <summary>
            /// 强光抑制等级，取值范围：[1,3] 
            /// </summary>
            public byte byLightInhibitLevel;
            /// <summary>
            /// 灰度值域:0-[0,255]，1-[16,235]
            /// </summary>
            public byte byGrayLevel;
        }
        #endregion

        #region 窗口信息结构体
        /// <summary>
        /// 窗口信息结构体
        /// </summary>

        public struct NET_DVR_SCREEN_WINCFG
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 是否有效：0－无效，1－有效
            /// </summary>
            public byte byVaild;
            /// <summary>
            /// 信号输入源类型
            /// </summary>
            public byte byInputType;
            /// <summary>
            /// 输入源索引
            /// </summary>
            public int wInputIdx;
            /// <summary>
            /// 图层，0为最底层，只能获取
            /// </summary>
            public uint dwLayerIdx;
            /// <summary>
            /// 目的窗口(相对显示墙)
            /// </summary>
            public NET_DVR_RECTCFG struWin;
            /// <summary>
            /// 窗口号
            /// </summary>
            public byte byWndIndex;
            /// <summary>
            /// 是否带背景：0- 无，1- 带背景，2- 不带背景 
            /// </summary>
            public byte byCBD;
            /// <summary>
            /// 是否子窗口: 0-不是，1-是
            /// </summary>
            public byte bySubWnd;
            /// <summary>
            /// 保留参数
            /// </summary>
            public byte byRes1;
            /// <summary>
            /// 设备序号 
            /// </summary>
            public uint dwDeviceIndex;
            /// <summary>
            /// 保留参数
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 场景参数设置
        /// <summary>
        /// 场景参数设置
        /// </summary>
        public struct NET_DVR_LAYOUT_LIST
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 所有场景名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_LAYOUT_COUNT, ArraySubType = UnmanagedType.AsAny)]
            public NET_DVR_LAYOUTCFG[] struLayoutInfo;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
    }
        #endregion

        #region 场景参数设置
        /// <summary>
        /// 场景参数设置
        /// </summary>
        public struct NET_DVR_LAYOUTCFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 是否有效：0- 否，1- 是
            /// </summary>
            public byte byValid;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 场景名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string byLayoutName;
            /// <summary>
            /// 场景内窗口参数 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WIN_COUNT, ArraySubType = UnmanagedType.AsAny)]
            public NET_DVR_SCREEN_WINCFG[] struWinCfg;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte byRes2;
        }
        #endregion

        #region 常量

        /// <summary>
        /// 最大大屏数量
        /// </summary>
        public const int MAX_BIGSCREENNUM = 100;
        public const int NAME_LEN = 32;
        public const int MAX_CAM_COUNT = 224;
        /// <summary>
        /// 场景数量
        /// </summary>
        public const int MAX_LAYOUT_COUNT = 16;
        /// <summary>
        /// 最大窗口数量
        /// </summary>
        public const int MAX_WIN_COUNT = 224;
        #endregion
    }
}
