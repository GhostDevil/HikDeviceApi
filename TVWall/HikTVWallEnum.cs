using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikDeviceApi.TVWall
{
    /// <summary>
    /// 日 期:2017-06-27
    /// 作 者:痞子少爷
    /// 描 述:海康多路解码器接口枚举
    /// </summary>
    public class HikTVWallEnum
    {
        /// <summary>
        /// 电视墙配置命令
        /// </summary>
        public enum WallConfigCmd
        {
            /// <summary>
            /// 设置电视墙窗口参数  
            /// </summary>
            NET_DVR_SET_VIDEOWALLWINDOWPOSITION = 1736,  
            /// <summary>
            ///  获取电视墙中的窗口 dwCount个NET_DVR_WALL_INDEX dwCount个NET_DVR_WALLWINCFG 
            /// </summary>
            NET_DVR_GET_WALL_WINDOW_V41 = 9020 ,
            /// <summary>
            ///  获取场景参数 dwCount个NET_DVR_WALL_INDEX dwCount个NET_DVR_WALLSCENECFG 
            /// </summary>
            NET_DVR_GET_WALL_SCENE_PARAM_V41 = 9023 ,
            /// <summary>
            ///  设置电视墙中的窗口 dwCount个NET_DVR_WALL_INDEX dwCount个NET_DVR_WALLWINCFG 
            /// </summary>
            NET_DVR_SET_WALL_WINDOW_V41 = 9021,
            /// <summary>
            /// 设置场景参数 dwCount个NET_DVR_WALL_INDEX dwCount个NET_DVR_WALLSCENECFG 
            /// </summary>
            NET_DVR_SET_WALL_SCENE_PARAM_V41 = 9024 ,
            /// <summary>
            /// 场景控制
            /// </summary>
            NET_DVR_SCENE_CONTROL = 1744,
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
            NET_DVR_WALLSCENEPARAM_GET = 9007,
            /// <summary>
            ///  电视墙中获取窗口解码状态 dwCount个（4字节）场景号 dwCount个NET_DVR_WALLWIN_INFO 
            /// </summary>
            NET_DVR_MATRIX_GETWINSTATUS = 9009,
        }
        /// <summary>
        /// 窗口控制
        /// </summary>
        public enum WinControlCmd
        {
            /// <summary>
            /// 显示通道放大某个窗口
            /// </summary>
            DISP_CMD_ENLARGE_WINDOW = 1,
            /// <summary>
            /// 显示通道窗口还原
            /// </summary>
            DISP_CMD_RENEW_WINDOW = 2,
            /// <summary>
            /// 窗口置顶
            /// </summary>
            NET_DVR_SWITCH_WIN_TOP = 9017,
            /// <summary>
            /// 窗口置底
            /// </summary>
            NET_DVR_SWITCH_WIN_BOTTOM = 9018
        }
        /// <summary>
        /// 场景控制
        /// </summary>
        public enum PlanCmd
        {
            /// <summary>
            /// 场景模式切换_如果要切换的是当前场景则不进行切换
            /// </summary>
            ModelChange = 1,
            /// <summary>
            /// 初始化场景_将此场景的配置清空_如果是当前场景则同时对当前场景进行清屏操作
            /// </summary>
            Initialize,
            /// <summary>
            /// 强制切换_无论是否是当前场景_强制切换
            /// </summary>
            ForcedSwitch,
            /// <summary>
            /// 保存当前模式到某场景
            /// </summary>
            SaveToPlan

        }
        /// <summary>
        /// 视频制式
        /// </summary>
        public enum WallDisplayMode
        {
            BNC=1,
            VGA ,
            HDMI,
            DVI ,
            SDI ,
            FIBER,
            RGB,
            YprPb, 
            VGA_HDMI_DVI自适应,
            GSDI,
            VGA_DVI自适应,
            无效=0xff
        }
        public enum WallBackgroundColor
        {
            不支持=0,
            红 = 1,
            绿,
            蓝,
            黄,
            紫,
            青,
            黑,
            白
        }
    }
}
