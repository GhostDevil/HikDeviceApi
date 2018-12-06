namespace HikDeviceApi.PTZ
{
    /// <summary>
    /// 日 期:2016-12-05
    /// 作 者:痞子少爷
    /// 描 述:云台控制操作枚举
    /// </summary>
    public static class HikPTZEnum
    {
        #region 云台控制命令
        ///// <summary>
        ///// 云台控制命令
        ///// </summary>
        //public enum PTZControlCmd
        //{
        //    /// <summary>
        //    /// 接通灯光电源
        //    /// </summary>
        //    LIGHT_PWRON = 2,
        //    /// <summary>
        //    /// 接通雨刷开关
        //    /// </summary>
        //    WIPER_PWRON = 3,
        //    /// <summary>
        //    /// 接通风扇开关
        //    /// </summary>
        //    FAN_PWRON = 4,
        //    /// <summary>
        //    /// 接通加热器开关
        //    /// </summary>
        //    HEATER_PWRON = 5,
        //    /// <summary>
        //    /// 接通辅助设备开关
        //    /// </summary>
        //    AUX_PWRON1 = 6,
        //    /// <summary>
        //    /// 接通辅助设备开关
        //    /// </summary>
        //    AUX_PWRON2 = 7,
        //    /// <summary>
        //    /// 设置预置点
        //    /// </summary>
        //    SET_PRESET = 8,
        //    /// <summary>
        //    /// 清除预置点
        //    /// </summary>
        //    CLE_PRESET = 9,
        //    /// <summary>
        //    /// 焦距以速度SS变大(倍率变大)
        //    /// </summary>
        //    ZOOM_IN = 11,
        //    /// <summary>
        //    /// 焦距以速度SS变小(倍率变小)
        //    /// </summary>
        //    ZOOM_OUT = 12,
        //    /// <summary>
        //    /// 焦点以速度SS前调
        //    /// </summary>
        //    FOCUS_NEAR = 13,
        //    /// <summary>
        //    /// 焦点以速度SS后调
        //    /// </summary>
        //    FOCUS_FAR = 14,
        //    /// <summary>
        //    /// 光圈以速度SS扩大
        //    /// </summary>
        //    IRIS_OPEN = 15,
        //    /// <summary>
        //    /// 光圈以速度SS缩小
        //    /// </summary>
        //    IRIS_CLOSE = 16,
        //    /// <summary>
        //    /// 云台以SS的速度上仰
        //    /// </summary>
        //    TILT_UP = 21,
        //    /// <summary>
        //    /// 云台以SS的速度下俯
        //    /// </summary>
        //    TILT_DOWN = 22,
        //    /// <summary>
        //    /// 云台以SS的速度左转
        //    /// </summary>
        //    PAN_LEFT = 23,
        //    /// <summary>
        //    /// 云台以SS的速度右转
        //    /// </summary>
        //    PAN_RIGHT = 24,
        //    /// <summary>
        //    /// 云台以SS的速度上仰和左转
        //    /// </summary>
        //    UP_LEFT = 25,
        //    /// <summary>
        //    /// 云台以SS的速度上仰和右转
        //    /// </summary>
        //    UP_RIGHT = 26,
        //    /// <summary>
        //    /// 云台以SS的速度下俯和左转
        //    /// </summary>
        //    DOWN_LEFT = 27,
        //    /// <summary>
        //    /// 云台以SS的速度下俯和右转
        //    /// </summary>
        //    DOWN_RIGHT = 28,
        //    /// <summary>
        //    /// 云台以SS的速度左右自动扫描
        //    /// </summary>
        //    PAN_AUTO = 29,
        //    /// <summary>
        //    /// 将预置点加入巡航序列
        //    /// </summary>
        //    FILL_PRE_SEQ = 30,
        //    /// <summary>
        //    /// 设置巡航点停顿时间
        //    /// </summary>
        //    SET_SEQ_DWELL = 31,
        //    /// <summary>
        //    /// 设置巡航速度
        //    /// </summary>
        //    SET_SEQ_SPEED = 32,
        //    /// <summary>
        //    /// 将预置点从巡航序列中删除
        //    /// </summary>
        //    CLE_PRE_SEQ = 33,
        //    /// <summary>
        //    /// 开始记录轨迹
        //    /// </summary>
        //    STA_MEM_CRUISE = 34,
        //    /// <summary>
        //    /// 停止记录轨迹
        //    /// </summary>
        //    STO_MEM_CRUISE = 35,
        //    /// <summary>
        //    /// 开始轨迹
        //    /// </summary>
        //    RUN_CRUISE = 36,
        //    /// <summary>
        //    /// 开始巡航
        //    /// </summary>
        //    RUN_SEQ = 37,
        //    /// <summary>
        //    /// 停止巡航
        //    /// </summary>
        //    STOP_SEQ = 38,
        //    /// <summary>
        //    /// 快球转到预置点
        //    /// </summary>
        //    GOTO_PRESET = 39
        //}
        #endregion

        #region 云台控制命令
        /// <summary>
        /// 云台控制命令
        /// </summary>
        public enum PTZControlCmd
        {
            /// <summary>
            /// 接通灯光电源
            /// </summary>
            LIGHT_PWRON = 2,
            /// <summary>
            /// 接通雨刷开关
            /// </summary>
            WIPER_PWRON = 3,
            /// <summary>
            /// 接通风扇开关
            /// </summary>
            FAN_PWRON = 4,
            /// <summary>
            /// 接通加热器开关
            /// </summary>
            HEATER_PWRON = 5,
            /// <summary>
            /// 接通辅助设备开关
            /// </summary>
            AUX_PWRON1 = 6,
            /// <summary>
            /// 接通辅助设备开关
            /// </summary>
            AUX_PWRON2 = 7,
            /// <summary>
            /// 焦距变大(倍率变大) 
            /// </summary>
            ZOOM_IN = 11,
            /// <summary>
            /// 焦距变小(倍率变小)
            /// </summary>
            ZOOM_OUT = 12,
            /// <summary>
            /// 焦点前调 
            /// </summary>
            FOCUS_NEAR = 13,
            /// <summary>
            /// 焦点后调
            /// </summary>
            FOCUS_FAR = 14,
            /// <summary>
            /// 光圈扩大
            /// </summary>
            IRIS_OPEN = 15,
            /// <summary>
            /// 光圈缩小
            /// </summary>
            IRIS_CLOSE = 16,
            /// <summary>
            /// 云台上仰
            /// </summary>
            TILT_UP = 21,
            /// <summary>
            /// 云台下俯
            /// </summary>
            TILT_DOWN = 22,
            /// <summary>
            /// 云台左转
            /// </summary>
            PAN_LEFT = 23,
            /// <summary>
            /// 云台右转
            /// </summary>
            PAN_RIGHT = 24,
            /// <summary>
            /// 云台上仰和左转
            /// </summary>
            UP_LEFT = 25,
            /// <summary>
            /// 云台上仰和右转
            /// </summary>
            UP_RIGHT = 26,
            /// <summary>
            /// 云台下俯和左转
            /// </summary>
            DOWN_LEFT = 27,
            /// <summary>
            /// 云台下俯和右转
            /// </summary>
            DOWN_RIGHT = 28,
            /// <summary>
            /// 云台左右自动扫描
            /// </summary>         
            PAN_AUTO = 29,
            /// <summary>
            /// 云台下俯和焦距变大(倍率变大) 
            /// </summary>
            TILT_DOWN_ZOOM_IN = 58,
            /// <summary>
            /// 云台下俯和焦距变小(倍率变小) 
            /// </summary>
            TILT_DOWN_ZOOM_OUT = 59,
            /// <summary>
            /// 云台左转和焦距变大(倍率变大) 
            /// </summary>
            PAN_LEFT_ZOOM_IN = 60,
            /// <summary>
            /// 云台左转和焦距变小(倍率变小) 
            /// </summary>
            PAN_LEFT_ZOOM_OUT = 61,
            /// <summary>
            /// 云台右转和焦距变大(倍率变大) 
            /// </summary>
            PAN_RIGHT_ZOOM_IN = 62,
            /// <summary>
            /// 云台右转和焦距变小(倍率变小)
            /// </summary>
            PAN_RIGHT_ZOOM_OUT = 63,
            /// <summary>
            ///云台上仰和左转和焦距变大(倍率变大)  
            /// </summary>
            UP_LEFT_ZOOM_IN = 64,
            /// <summary>
            /// 云台上仰和左转和焦距变小(倍率变小) 
            /// </summary>
            UP_LEFT_ZOOM_OUT = 65,
            /// <summary>
            /// 云台上仰和右转和焦距变大(倍率变大)
            /// </summary>
            UP_RIGHT_ZOOM_IN = 66,
            /// <summary>
            /// 云台上仰和右转和焦距变小(倍率变小)
            /// </summary>
            UP_RIGHT_ZOOM_OUT = 67,
            /// <summary>
            /// 云台下俯和左转和焦距变大(倍率变大)
            /// </summary>
            DOWN_LEFT_ZOOM_IN = 68,
            /// <summary>
            /// 云台下俯和左转和焦距变小(倍率变小)
            /// </summary>
            DOWN_LEFT_ZOOM_OUT = 69,
            /// <summary>
            /// 云台下俯和右转和焦距变大(倍率变大)
            /// </summary>
            DOWN_RIGHT_ZOOM_IN = 70,
            /// <summary>
            /// 云台下俯和右转和焦距变小(倍率变小) 
            /// </summary>
            DOWN_RIGHT_ZOOM_OUT = 71,
            /// <summary>
            /// 云台上仰和焦距变大(倍率变大)
            /// </summary>
            TILT_UP_ZOOM_IN = 72,
            /// <summary>
            /// 云台上仰和焦距变小(倍率变小) 
            /// </summary>
            TILT_UP_ZOOM_OUT = 73

        }
        #endregion

        #region 操作云台预置点命令

        /// <summary>
        /// 操作云台预置点命令
        /// </summary>
        public enum PTZPresetCmd
        {
            /// <summary>
            /// 设置预置点
            /// </summary>
            SET_PRESET = 8,
            /// <summary>
            /// 清除预置点
            /// </summary>
            CLE_PRESET = 9,
            /// <summary>
            /// 转到预置点
            /// </summary>
            GOTO_PRESET = 39

        }
        #endregion

        #region 操作云台巡航命令
        /// <summary>
        /// 操作云台巡航命令
        /// </summary>
        public enum PTZCruiseCmd
        {
            /// <summary>
            /// 将预置点加入巡航序列
            /// </summary>
            FILL_PRE_SEQ = 30,
            /// <summary>
            /// 设置巡航点停顿时间
            /// </summary>
            SET_SEQ_DWELL = 31,
            /// <summary>
            /// 设置巡航速度
            /// </summary>
            SET_SEQ_SPEED = 32,
            /// <summary>
            /// 将预置点从巡航序列中删除
            /// </summary>
            CLE_PRE_SEQ = 33,
            /// <summary>
            /// 开始巡航
            /// </summary>
            RUN_SEQ = 37,
            /// <summary>
            /// 停止巡航
            /// </summary>
            STOP_SEQ = 38

        }
        #endregion

        #region 操作云台轨迹命令
        /// <summary>
        /// 操作云台轨迹命令
        /// </summary>
        public enum PTZTrackCmd
        {
            /// <summary>
            /// 开始记录轨迹
            /// </summary>
            STA_MEM_CRUISE = 34,
            /// <summary>
            /// 停止记录轨迹
            /// </summary>
            STO_MEM_CRUISE = 35,
            /// <summary>
            /// 开始轨迹
            /// </summary>
            RUN_CRUISE = 36

        }
        #endregion

        #region 云台远程控制命令

        /// <summary>
        /// 云台远程控制命令
        /// </summary>
        public enum PTZRemoteCmd
        {
            /// <summary>
            /// 限位参数控制 NET_DVR_PTZ_LIMITCTRL
            /// </summary>
            NET_DVR_PTZLIMIT_CTRL = 3278,
            /// <summary>
            /// 清除云台配置信息 NET_DVR_CLEARCTRL
            /// </summary>
            NET_DVR_CLEARCTRL = 3279,
            /// <summary>
            /// 零方位角控制 NET_DVR_INITIALPOSITIONCTRL
            /// </summary>
            NET_DVR_PTZ_INITIALPOSITIONCTRL = 3283,
            /// <summary>
            /// 设置跟踪倍率 NET_DVR_ZOOMRATIOCTRL
            /// </summary>
            NET_DVR_PTZ_ZOOMRATIOCTRL = 3289,
            /// <summary>
            /// 快球云台花样扫描 NET_DVR_PTZ_PATTERN
            /// </summary>
            NET_DVR_CONTROL_PTZ_PATTERN = 3313,
            /// <summary>
            ///  手动跟踪定位 NET_DVR_PTZ_MANUALTRACE
            /// </summary>
            NET_DVR_CONTROL_PTZ_MANUALTRACE = 3316,
            /// <summary>
            /// 3维带速度的云台控制 NET_DVR_PTZ_3D_SPEED_CONTROL
            /// </summary>
            NET_DVR_PTZ_3D_SPEED = 1765,
            /// <summary>
            /// 清空前端参数 NET_DVR_IPC_PARAM_TYPE
            /// </summary>
            NET_DVR_CLEAR_IPC_PARAM = 3230

        }
        #endregion


    }
}
