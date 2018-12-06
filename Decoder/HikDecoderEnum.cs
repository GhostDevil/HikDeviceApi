namespace HikDeviceApi.Decoder
{
    /// <summary>
    /// 日 期:2015-07-07
    /// 作 者:痞子少爷
    /// 描 述:海康多路解码器接口使用枚举
    /// </summary>
    public static class HikDecoderEnum
    {
        #region 显示通道命令
        /// <summary>
        ///  显示通道命令
        /// </summary>
        public enum DisplayChanCmd
        {
            /// <summary>
            ///  显示通道放大某个窗口
            /// </summary>
            DISP_CMD_ENLARGE_WINDOW = 1,
            /// <summary>
            /// 显示通道窗口还原
            /// </summary>
            DISP_CMD_RENEW_WINDOW = 2

        }
        #endregion

        #region 分辨率
        /// <summary>
        /// 分辨率
        /// </summary>
        public enum Resolution
        {
            /// <summary>
            /// 不指定
            /// </summary>
            NOT_AVALIABLE = 0,
            /// <summary>
            /// SVGA_60HZ
            /// </summary>
            SVGA_60HZ = 52505660,
            /// <summary>
            /// SVGA_75HZ
            /// </summary>
            SVGA_75HZ = 52505675,
            /// <summary>
            /// XGA_60HZ
            /// </summary>
            XGA_60HZ = 67207228,
            /// <summary>
            /// XGA_75HZ
            /// </summary>
            XGA_75HZ = 67207243,
            /// <summary>
            /// SXGA_60HZ
            /// </summary>
            SXGA_60HZ = 84017212,
            /// <summary>
            /// SXGA2_60HZ
            /// </summary>
            SXGA2_60HZ = 84009020,
            /// <summary>
            /// 720P_60HZ
            /// </summary>
            _720P_60HZ = 83978300,
            /// <summary>
            /// 720P_50HZ
            /// </summary>
            _720P_50HZ = 83978290,
            /// <summary>
            /// 1080I_60HZ
            /// </summary>
            _1080I_60HZ = 394402876,
            /// <summary>
            /// 1080I_50HZ
            /// </summary>
            _1080I_50HZ = 394402866,
            /// <summary>
            /// 1080P_60HZ
            /// </summary>
            _1080P_60HZ = 125967420,
            /// <summary>
            /// 1080P_50HZ
            /// </summary>
            _1080P_50HZ = 125967410,
            /// <summary>
            /// 1080P_30HZ
            /// </summary>
            _1080P_30HZ = 125967390,
            /// <summary>
            /// 1080P_25HZ
            /// </summary>
            _1080P_25HZ = 125967385,
            /// <summary>
            /// 1080P_24HZ
            /// </summary>
            _1080P_24HZ = 125967384,
            /// <summary>
            /// UXGA_60HZ
            /// </summary>
            UXGA_60HZ = 105011260,
            /// <summary>
            /// UXGA_30HZ
            /// </summary>
            UXGA_30HZ = 105011230,
            /// <summary>
            /// WSXGA_60HZ
            /// </summary>
            WSXGA_60HZ = 110234940,
            /// <summary>
            /// WUXGA_60HZ
            /// </summary>
            WUXGA_60HZ = 125982780,
            /// <summary>
            /// WUXGA_30HZ
            /// </summary>
            WUXGA_30HZ = 125982750,
            /// <summary>
            /// WXGA_60HZ
            /// </summary>
            WXGA_60HZ = 89227324,
            /// <summary>
            /// SXGA_PLUS_60HZ
            /// </summary>
            SXGA_PLUS_60HZ = 91884860
        }
        #endregion

        #region 视频制式
        /// <summary>
        /// 视频制式
        /// </summary>
        public enum VedioStandard
        {
            /// <summary>
            /// NULL
            /// </summary>
            NULL = 0,
            /// <summary>
            /// NTSC
            /// </summary>
            NTSC,
            /// <summary>
            /// PAL
            /// </summary>
            PAL
        }
        #endregion

        #region 显示通道类型
        /// <summary>
        /// 显示通道类型
        /// </summary>
        public enum DisplayChanType
        {
            /// <summary>
            /// BNC
            /// </summary>
            BNC = 0,
            /// <summary>
            /// VGA
            /// </summary>
            VGA,
            /// <summary>
            /// HDMI
            /// </summary>
            HDMI,
            /// <summary>
            /// DVI
            /// </summary>
            DVI,
            /// <summary>
            /// (解码卡服务器DECODER_SERVER专用)
            /// </summary>
            YPbPr
        }
        #endregion

        #region Logo显示
        /// <summary>
        /// Logo显示
        /// </summary>
        public enum LogoCmd
        {
            /// <summary>
            /// 显示LOGO
            /// </summary>
            NET_DVR_SHOWLOGO = 1,
            /// <summary>
            /// 隐藏LOGO
            /// </summary>
            NET_DVR_HIDELOGO = 2,
            /// <summary>
            /// 设置窗口logo参数
            /// </summary>
            NET_DVR_SET_WIN_LOGO_CFG = 9028,
            /// <summary>
            /// 获取窗口logo参数
            /// </summary>
            NET_DVR_GET_WIN_LOGO_CFG = 9027,
            /// <summary>
            /// 设置logo参数
            /// </summary>
            NET_DVR_SET_MATRIX_LOGO_CFG = 9026,
            /// <summary>
            /// 设置logo参数
            /// </summary>
            NET_DVR_GET_MATRIX_LOGO_CFG = 9025,
            /// <summary>
            /// 删除logo
            /// </summary>
            NET_DVR_DELETE_LOGO = 9029,
        }
        #endregion

        #region 远程回放文件控制
        /// <summary>
        /// 远程回放文件控制
        /// </summary>
        public enum RemotePlayControl
        {
            /// <summary>
            ///  开始播放
            /// </summary>
            NET_DVR_PLAYSTART = 1,
            /// <summary>
            /// 停止播放
            /// </summary>
            NET_DVR_PLAYSTOP = 2,
            /// <summary>
            /// 暂停播放
            /// </summary>
            NET_DVR_PLAYPAUSE = 3,
            /// <summary>
            /// 恢复播放
            /// </summary>
            NET_DVR_PLAYRESTART = 4,
            /// <summary>
            /// 快放
            /// </summary>
            NET_DVR_PLAYFAST = 5,
            /// <summary>
            /// 慢放
            /// </summary>
            NET_DVR_PLAYSLOW = 6,
            /// <summary>
            /// 正常速度播放
            /// </summary>
            NET_DVR_PLAYNORMAL = 7,
            /// <summary>
            /// 打开声音
            /// </summary>
            NET_DVR_PLAYSTARTAUDIO = 9,
            /// <summary>
            /// 关闭声音
            /// </summary>
            NET_DVR_PLAYSTOPAUDIO = 10,
            /// <summary>
            /// 改变文件回放的进度
            /// </summary>
            NET_DVR_PLAYSETPOS = 12

        }
        #endregion

        #region 码流类型
        /// <summary>
        /// 解码器码流类型
        /// </summary>
        public enum StreamType
        {
            /// <summary>
            ///  未知编码格式
            /// </summary>
            NET_DVR_ENCODER_UNKOWN = 0,
            /// <summary>
            ///  private 264 
            /// </summary>
            NET_DVR_ENCODER_H264,
            /// <summary>
            /// 标准H264
            /// </summary>
            NET_DVR_ENCODER_S264,

            /// <summary>
            /// MPEG4
            /// </summary>
            NET_DVR_ENCODER_MPEG4,

            /// <summary>
            /// 原始流
            /// </summary>
            NET_DVR_ORIGINALSTREAM,
            /// <summary>
            /// 智能分析联动的报警图片
            /// </summary>
            NET_DVR_PICTURE,

            /// <summary>
            /// MJPEG
            /// </summary>
            NET_DVR_ENCODER_MJPEG,

            /// <summary>
            /// MPEG2
            /// </summary>
            NET_DVR_ECONDER_MPEG2,

            /// <summary>
            /// 标准H265
            /// </summary>
            NET_DVR_ENCODER_S265,
        }
        #endregion

        #region 封装格式
        /// <summary>
        /// 解码器封装格式
        /// </summary>
        public enum PacketType
        {
            /// <summary>
            /// 未知封装格式
            /// </summary>
            NET_DVR_STREAM_TYPE_UNKOWN = 0,
            /// <summary>
            /// 私有自定义封装格式
            /// </summary>
            NET_DVR_STREAM_TYPE_PRIVT = 1,
            /// <summary>
            /// TS封装格式
            /// </summary>
            NET_DVR_STREAM_TYPE_TS =7,
            /// <summary>
            /// PS封装格式
            /// </summary>
            NET_DVR_STREAM_TYPE_PS = 8 ,
            /// <summary>
            /// RTP封装格式
            /// </summary>
            NET_DVR_STREAM_TYPE_RTP = 9,
            /// <summary>
            /// 未封装
            /// </summary>
            NET_DVR_STREAM_TYPE_ORIGIN = 10 

        }
        #endregion

        #region 低帧率
        /// <summary>
        /// 低帧率
        /// </summary>
        public enum FpsDecV
        {
            /// <summary>
            ///   1/2帧
            /// </summary>
            LOW_DEC_FPS_1_2 = 51,
            /// <summary>
            ///  1/4帧
            /// </summary>
            LOW_DEC_FPS_1_4 = 52,
            /// <summary>
            ///  1/8帧
            /// </summary>
            LOW_DEC_FPS_1_8= 53 ,
            /// <summary>
            /// 1/16帧
            /// </summary>
            LOW_DEC_FPS_1_16 =  54 

        }
        #endregion

        #region 解码延时类型
        /// <summary>
        /// 解码延时
        /// </summary>
        public enum DecodeDelay
        {
            默认 = 0,
            实时性好 = 1,
            实时性较好 = 2,
            实时性中_流畅性中 = 3,
            流畅性较好 = 4,
            流畅性好 = 5,
            自动调整 = 0xff

        }
        #endregion

        //#region 远程控制
        ///// <summary>
        ///// 远程控制
        ///// </summary>
        //public enum RemoteControl
        //{
        //    /// <summary>
        //    ///  电视墙关闭所有窗口,输入参数为NULL
        //    /// </summary>
        //    NET_DVR_CLOSE_ALL_WND = 9016,
        //    /// <summary>
        //    ///  窗口置顶,输入参数为4字节窗口号
        //    /// </summary>
        //    NET_DVR_SWITCH_WIN_TOP = 9017,
        //    /// <summary>
        //    ///  窗口置底,输入参数为4字节窗口号
        //    /// </summary>
        //    NET_DVR_SWITCH_WIN_BOTTOM = 9018,
        //    /// <summary>
        //    ///  解码播放远程文件,输入参数为NET_DVR_MATRIX_DEC_REMOTE_PLAY_EX结构体
        //    /// </summary>
        //    NET_DVR_DEC_PLAY_REMOTE_FILE = 9032,
        //    


        //}
        //#endregion

        #region 解码器播放控制状态命令
        /// <summary>
        /// 解码器播放控制状态命令
        /// </summary>
        public enum PassivePlayControl
        {
            /// <summary>
            /// 被动解码暂停（仅对文件流有效）
            /// </summary>
            PASSIVE_DEC_PAUSE = 1,
            /// <summary>
            ///  恢复被动解码（仅对文件流有效）
            /// </summary>
            PASSIVE_DEC_RESUME = 2,
            /// <summary>
            /// 快速被动解码（仅对文件流有效）
            /// </summary>
            PASSIVE_DEC_FAST = 3,
            /// <summary>
            /// 慢速被动解码（仅对文件流有效）
            /// </summary>
            PASSIVE_DEC_SLOW = 4,
            /// <summary>
            /// 正常被动解码（仅对文件流有效）
            /// </summary>
            PASSIVE_DEC_NORMAL = 5,
            /// <summary>
            /// 被动解码单帧播放（保留）
            /// </summary>
            PASSIVE_DEC_ONEBYONE = 6,
            /// <summary>
            /// 音频开启
            /// </summary>
            PASSIVE_DEC_AUDIO_ON = 7,
            /// <summary>
            /// 音频关闭
            /// </summary>
            PASSIVE_DEC_AUDIO_OFF = 8,
            /// <summary>
            /// 清空缓冲区
            /// </summary>
            PASSIVE_DEC_RESETBUFFER = 9

        }
        #endregion

        #region 设备配置命令
        /// <summary>
        /// 设备配置命令
        /// </summary>
        public enum ConfigCommand
        {
            /// <summary>
            ///  获取设备窗口状态 对应结构体NET_DVR_WALLWIN_INFO 
            /// </summary>
            NET_DVR_MATRIX_GETWINSTATUS= 9009,
               /// <summary>
               /// 获取解码通道解码状态 对应结构体NET_DVR_MATRIX_CHAN_STATUS
               /// </summary>
            NET_DVR_GET_DEC_CHAN_STATUS = 9113 ,
                /// <summary>
                ///  获取显示通道状态 对应结构体NET_DVR_DISP_CHAN_STATUS_V41 9114
                /// </summary>
            NET_DVR_GET_DISP_CHAN_STATUS


            ///// <summary>
            ///// 获取网络参数（多路解码器）,通道号无效。对应结构体 NET_DVR_NETCFG_OTHER
            ///// </summary>
            //NET_DVR_GET_NETCFG_OTHER = 244,
            ///// <summary>
            ///// 获取大屏拼接参数（64T高清解码器支持），大屏序号，0~n(从能力集获取)。对应结构体 NET_DVR_BIGSCREENCFG
            ///// </summary>
            //NET_DVR_MATRIX_BIGSCREENCFG_GET = 1140,
            ///// <summary>
            ///// 获取自动重启参数(DS-65xxD)，通道号无效。对应结构体 NET_DVR_AUTO_REBOOT_CFG
            ///// </summary>
            //NET_DVR_GET_AUTO_REBOOT_CFG = 1710,
            ///// <summary>
            ///// 获取设备工作模式，通道号无效。对应结构体 NET_DVR_DEV_WORK_MODE
            ///// </summary>
            //NET_DVR_GET_DEV_WORK_MODE = 9180,
            ///// <summary>
            ///// 设置网络参数（多路解码器）,通道号无效。对应结构体 NET_DVR_NETCFG_OTHER
            ///// </summary>
            //NET_DVR_SET_NETCFG_OTHER = 245,
            ///// <summary>
            ///// 设置大屏拼接参数（64T高清解码器支持），大屏序号，0~n(从能力集获取)。对应结构体 NET_DVR_BIGSCREENCFG
            ///// </summary>
            //NET_DVR_MATRIX_BIGSCREENCFG_SET = 1141,
            ///// <summary>
            ///// 设置自动重启参数(DS-65xxD)，通道号无效。对应结构体 NET_DVR_AUTO_REBOOT_CFG
            ///// </summary>
            //NET_DVR_SET_AUTO_REBOOT_CFG = 1171,
            ///// <summary>
            ///// 设置设备工作模式，通道无效。对应结构体 NET_DVR_DEV_WORK_MODE
            ///// </summary>
            //NET_DVR_SET_DEV_WORK_MODE = 9109
        }
        #endregion

        #region 远程升级的状态
        /// <summary>
        /// 远程升级的状态
        /// </summary>
        public enum UpgradeState
        {
            /// <summary>
            /// 操作失败
            /// </summary>
            操作失败 = -1,
            /// <summary>
            /// 升级成功
            /// </summary>
            升级成功 = 1,
            /// <summary>
            /// 正在升级
            /// </summary>
            正在升级 = 2,
            /// <summary>
            /// 升级失败
            /// </summary>
            升级失败 = 3,
            /// <summary>
            /// 网络断开_状态未知
            /// </summary>
            网络断开_状态未知 = 4,
            /// <summary>
            /// 升级文件语言版本不匹配
            /// </summary>
            升级文件语言版本不匹配 = 5,
            /// <summary>
            /// 升级写flash失败
            /// </summary>
            升级写flash失败 = 6,
            /// <summary>
            /// 升级包类型不匹配
            /// </summary>
            升级包类型不匹配 = 7,
            /// <summary>
            /// 升级包版本不匹配
            /// </summary>
            升级包版本不匹配 = 8

        }
        #endregion

        #region 网络环境级别
        /// <summary>
        /// 网络环境级别
        /// </summary>
        public enum EnvironmentLevel
        {
            /// <summary>
            /// 局域网
            /// </summary>
            LOCAL_AREA_NETWORK = 0,
            /// <summary>
            /// 广域网
            /// </summary>
            WIDE_AREA_NETWORK
        }

        #endregion

        #region 固件升级类型
        /// <summary>
        /// 固件升级类型
        /// </summary>
        public enum ENUM_UPGRADE_TYPE
        {
            /// <summary>
            /// 普通设备升级
            /// </summary>
            ENUM_UPGRADE_DVR = 0,
            /// <summary>
            /// DVR适配器升级
            /// </summary>
            ENUM_UPGRADE_ADAPTER = 1,
            /// <summary>
            /// 智能库升级
            /// </summary>
            ENUM_UPGRADE_VCALIB = 2,
            /// <summary>
            /// 光端机升级
            /// </summary>
            ENUM_UPGRADE_OPTICAL = 3,
            /// <summary>
            /// 门禁系统升级
            /// </summary>
            ENUM_UPGRADE_ACS = 4
        }
        #endregion

        #region 场景控制
        /// <summary>
        /// 场景控制
        /// </summary>
        public enum SceneCmd
        {
            /// <summary>
            /// 场景模式切换（如果要切换的是当前场景，则不进行切换）
            /// </summary>
            PlanChange = 1,
            /// <summary>
            /// 初始化场景（将此场景的配置清空，如果是当前场景，则同时对当前场景进行清屏操作）
            /// </summary>
            PlanClear,
            /// <summary>
            /// 强制切换（无论是否是当前场景，强制切换）
            /// </summary>
            PlanForceChange,
            /// <summary>
            /// 保存场景
            /// </summary>
            PlanSave
        }
        #endregion
    }
}
