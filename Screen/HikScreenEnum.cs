namespace HikDeviceApi.Screen
{
    /// <summary>
    /// 日 期:2016-01-25
    /// 作 者:痞子少爷
    /// 描 述:海康拼接屏控制枚举
    /// </summary>
    public static class HikScreenEnum
    {
        #region 执行命令类型
        /// <summary>
        /// 执行命令类型
        /// </summary>
        public enum DwCommand
        {
            /// <summary>
            ///  获取大屏串口信息,对应结构体 NET_DVR_SERIAL_CONTROL 
            /// </summary>
            NET_DVR_BIGSCREEN_GETSERIA = 1614,
            /// <summary>
            ///  获取用户信息(用户号，从1开始),对于应结构体 NET_DVR_VCS_USER_INFO 
            /// </summary>
            NET_DVR_GET_VCS_USER_CFG = 1623,
            /// <summary>
            ///  获取当前使用串口,对应结构体 NET_DVR_USING_SERIALPORT 
            /// </summary>
            NET_DVR_GET_USING_SERIALPORT = 1742,
            /// <summary>
            ///  获取屏幕信号源参数,对应结构体 NET_DVR_SCREEN_SIGNAL_CFG 
            /// </summary>
            NET_DVR_GET_SCREEN_SIGNAL_CFG = 9037,
            /// <summary>
            ///  获取屏幕拼接,对应结构体 NET_DVR_SCREEN_SPLICE_CFG 
            /// </summary>
            NET_DVR_GET_SCREEN_SPLICE_CFG = 9039,
            /// <summary>
            ///  获取风扇工作方式,对应结构体 NET_DVR_SCREEN_FAN_WORK_MODE_CFG 
            /// </summary>
            NET_DVR_GET_SCREEN_FAN_WORK_MODE = 9040,
            /// <summary>
            ///  获取VGA信号配置,对应结构体 NET_DVR_SCREEN_VGA_CFG 
            /// </summary>
            NET_DVR_GET_VGA_CFG = 9045,
            /// <summary>
            ///  获取屏幕菜单配置,对应结构体 NET_DVR_SCREEN_MENU_CFG 
            /// </summary>
            NET_DVR_GET_SCREEN_MENU_CFG = 9048,
            /// <summary>
            ///  获取显示参数,对应结构体 NET_DVR_SCREEN_DISPLAY_CFG
            /// </summary>
            NET_DVR_GET_SCREEN_DISPLAY_CFG = 9051,
            /// <summary>
            ///  获取画中画参数,对应结构体 NET_DVR_PIP_CFG 
            /// </summary>
            NET_DVR_GET_PIP_CFG = 9061,
            /// <summary>
            ///  获取去雾参数,对应结构体 NET_DVR_DEFOG_LCD 
            /// </summary>
            NET_DVR_GET_DEFOG_LCD = 9074,
            /// <summary>
            ///  获取屏幕位置参数,对应结构体 NET_DVR_SCREEN_POS_CFG 
            /// </summary>
            NET_DVR_GET_SCREEN_POS = 9078,
            /// <summary>
            ///  获取延时开机参数,对应结构体 NET_DVR_DELAY_TIME
            /// </summary>
            NET_DVR_GET_POWERON_DELAY_CFG = 9088,
            /// <summary>
            ///  获取外接矩阵输入输出关联关系,对应结构体 NET_DVR_IO_RELATION_INFO 
            /// </summary>
            NET_DVR_GET_EXTERNAL_MATRIX_RELATION = 9095,
            /// <summary>
            /// 获取网络参数（多路解码器）,通道号无效。对应结构体 NET_DVR_NETCFG_OTHER
            /// </summary>
            NET_DVR_GET_NETCFG_OTHER = 244,
            /// <summary>
            /// 获取大屏拼接参数（64T高清解码器支持），大屏序号，0~n(从能力集获取)。对应结构体 NET_DVR_BIGSCREENCFG
            /// </summary>
            NET_DVR_MATRIX_BIGSCREENCFG_GET = 1140,
            /// <summary>
            /// 获取自动重启参数(DS-65xxD)，通道号无效。对应结构体 NET_DVR_AUTO_REBOOT_CFG
            /// </summary>
            NET_DVR_GET_AUTO_REBOOT_CFG = 1710,
            /// <summary>
            /// 获取设备工作模式，通道号无效。对应结构体 NET_DVR_DEV_WORK_MODE
            /// </summary>
            NET_DVR_GET_DEV_WORK_MODE = 9180,
            /// <summary>
            /// 设置网络参数（多路解码器）,通道号无效。对应结构体 NET_DVR_NETCFG_OTHER
            /// </summary>
            NET_DVR_SET_NETCFG_OTHER = 245,
            /// <summary>
            /// 设置大屏拼接参数（64T高清解码器支持），大屏序号，0~n(从能力集获取)。对应结构体 NET_DVR_BIGSCREENCFG
            /// </summary>
            NET_DVR_MATRIX_BIGSCREENCFG_SET = 1141,
            /// <summary>
            /// 设置自动重启参数(DS-65xxD)，通道号无效。对应结构体 NET_DVR_AUTO_REBOOT_CFG
            /// </summary>
            NET_DVR_SET_AUTO_REBOOT_CFG = 1171,
            /// <summary>
            /// 设置设备工作模式，通道无效。对应结构体 NET_DVR_DEV_WORK_MODE
            /// </summary>
            NET_DVR_SET_DEV_WORK_MODE = 9109,
            /// <summary>
            ///  设置用户参数 无效 NET_DVR_USER_V30 
            /// </summary>
            NET_DVR_SET_USERCFG_V30 = 1007,
            /// <summary>
            ///  场景设置 4字节表示场景号，场景最多为16个，场景号0~15 NET_DVR_LAYOUTCFG 
            /// </summary>
            NET_DVR_SET_LAYOUTCFG = 1606,
            /// <summary>
            ///  场景控制 4字节表示场景号，场景最多为16个，场景号0 ~15 4字节的变量，为1时表示open，2表示close 
            /// </summary>
            NET_DVR_LAYOUTCTRL = 1607,
            /// <summary>
            ///  设置输入信号源 4字节表示信号源序号，信号源序号范围1 ~224 NET_DVR_INPUTSTREAMCFG 
            /// </summary>
            NET_DVR_SET_INPUTSTREAMCFG = 1609,
            /// <summary>
            ///  设置输出参数 集中式多屏控制器4字节保留；分布式多屏控制器表示墙号 NET_DVR_OUTPUTCFG 
            /// </summary>
            NET_DVR_OUTPUT_SET = 1610,
            /// <summary>
            /// 设置虚拟LED参数 虚拟LED序号：集中式多屏控制器4字节表示序号；分布式多屏控制器高2字节表示墙号，低2字节表示序号 NET_DVR_OSDCFG
            /// </summary>
            NET_DVR_SET_OSDCFG = 1612,
            /// <summary>
            ///  设置预案 预案号， 从1开始。分布式多屏控制器，高2字节表示电视墙号，低2字节表示预案号 NET_DVR_PLAN_CFG 
            /// </summary>
            NET_DVR_SET_PLAN = 1616,
            /// <summary>
            ///  设置用户信息配置 用户号，从1开始 NET_DVR_VCS_USER_INFO 
            /// </summary>
            NET_DVR_SET_VCS_USER_CFG = 1624


        }
        #endregion

        #region 远程控制
        /// <summary>
        /// 远程控制
        /// </summary>
        public enum RemoteControl
        {
            /// <summary>
            ///  电视墙关闭所有窗口,输入参数为NULL
            /// </summary>
            NET_DVR_CLOSE_ALL_WND = 9016,
            /// <summary>
            ///  窗口置顶,输入参数为4字节窗口号
            /// </summary>
            NET_DVR_SWITCH_WIN_TOP = 9017,
            /// <summary>
            ///  窗口置底,输入参数为4字节窗口号
            /// </summary>
            NET_DVR_SWITCH_WIN_BOTTOM = 9018,
            /// <summary>
            ///  解码播放远程文件,输入参数为NET_DVR_MATRIX_DEC_REMOTE_PLAY_EX结构体
            /// </summary>
            NET_DVR_DEC_PLAY_REMOTE_FILE = 9032,
            /// <summary>
            /// 获取电视墙中屏幕参数 dwCount个（4字节）屏幕输出号 dwCount个NET_DVR_SINGLEWALLPARAM 
            /// </summary>
            NET_DVR_MATRIX_WALL_GET = 9002,
            /// <summary>
            ///  电视墙中获取窗口参数 dwCount个（4字节）窗口号 dwCount个NET_DVR_WALLWINCFG 
            /// </summary>
            NET_DVR_WALLWIN_GET = 9003,
            /// <summary>
            ///  电视墙中获取场景参数 dwCount个（4字节）场景号 dwCount个NET_DVR_WALLSCENECFG 
            /// </summary>
            NET_DVR_WALLSCENEPARAM_GET = 9007


        }
        #endregion

        #region 信号输入源类型
        /// <summary>
        /// 信号输入源类型
        /// </summary>
        public enum CameraMode
        {
            /// <summary>
            /// 无效
            /// </summary>
            NET_DVR_UNKNOW = 0,
            /// <summary>
            /// BNC输入
            /// </summary>
            NET_DVR_CAM_BNC = 1,
            /// <summary>
            /// VGA输入
            /// </summary>
            NET_DVR_CAM_VGA = 2,
            /// <summary>
            /// DVI输入 
            /// </summary>
            NET_DVR_CAM_DVI = 3,
            /// <summary>
            /// HDMI输入
            /// </summary>
            NET_DVR_CAM_HDMI = 4,
            /// <summary>
            /// IP输入
            /// </summary>
            NET_DVR_CAM_IP = 5,
            /// <summary>
            /// RGB输入
            /// </summary>
            NET_DVR_CAM_RGB = 6,
            /// <summary>
            /// 解码板输入
            /// </summary>
            NET_DVR_CAM_DECODER = 7,
            /// <summary>
            /// 矩阵信号源 
            /// </summary>
            NET_DVR_CAM_MATRIX = 8,
            /// <summary>
            /// YPbPr输入
            /// </summary>
            NET_DVR_CAM_YPBPR = 9,
            /// <summary>
            /// USB输入
            /// </summary>
            NET_DVR_CAM_USB = 10,
            /// <summary>
            /// SDI输入
            /// </summary>
            NET_DVR_CAM_SDI = 11,
            /// <summary>
            /// HDI输入 
            /// </summary>
            NET_DVR_CAM_HDI = 12,
            /// <summary>
            /// DP输入
            /// </summary>
            NET_DVR_CAM_DP = 13,
            /// <summary>
            /// HDTVI输入
            /// </summary>
            NET_DVR_CAM_HDTVI = 14,
            /// <summary>
            /// 拼接信号源输入
            /// </summary>
            NET_DVR_CAM_JOINT = 15,
            /// <summary>
            /// HDBASET输入
            /// </summary>
            NET_DVR_CAM_HDBASET = 16
        }
        #endregion
    }
}
