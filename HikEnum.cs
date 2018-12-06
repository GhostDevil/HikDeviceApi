namespace HikDeviceApi
{
    /// <summary>
    /// 日 期:2017-11-25
    /// 作 者:痞子少爷
    /// 描 述:海康设备公用接口枚举
    /// </summary>
    public static class HikEnum
    {
        #region
        /// <summary>
        /// 上传的报警信息类型
        /// </summary>
        public enum DvrAlarmType
        {
            信号量报警 = 0,
            硬盘满,
            信号丢失,
            移动侦测,
            硬盘未格式化,
            读写硬盘出错,
            遮挡报警,
            制式不匹配,
            非法访问,
            视频信号异常,
            录像_抓图异常,
            智能场景变化,
            阵列异常,
            前端_录像分辨率不匹配,
            智能侦测 = 15,
            POE供电异常 = 16,
            闪光灯异常 = 17,
            磁盘满负荷异常报警 = 18,
            音频丢失 = 19,
            脉冲报警 = 23,
            人脸库硬盘异常 = 24,
            人脸库变更 = 25,
            人脸库图片变更 = 26
        }
        #endregion

        #region 行为分析

        /// <summary>
        /// 行为分析事件类型
        /// </summary>
        public enum VCA_EVENT_TYPE : uint
        {
            /// <summary>
            /// 穿越警戒面
            /// </summary>
            VCA_TRAVERSE_PLANE = 0x1,
            /// <summary>
            /// 目标进入区域,支持区域规则
            /// </summary>
            VCA_ENTER_AREA = 0x2,
            /// <summary>
            /// 目标离开区域,支持区域规则
            /// </summary>
            VCA_EXIT_AREA = 0x4,
            /// <summary>
            /// 周界入侵,支持区域规则
            /// </summary>
            VCA_INTRUSION = 0x8,
            /// <summary>
            /// 徘徊,支持区域规则
            /// </summary>
            VCA_LOITER = 0x10,
            /// <summary>
            /// 物品遗留拿取,支持区域规则
            /// </summary>
            VCA_LEFT_TAKE = 0x20,
            /// <summary>
            /// 停车,支持区域规则
            /// </summary>
            VCA_PARKING = 0x40,
            /// <summary>
            /// 快速移动,支持区域规则
            /// </summary>
            VCA_RUN = 0x80,
            /// <summary>
            /// 区域内人员聚集,支持区域规则
            /// </summary>
            VCA_HIGH_DENSITY = 0x100,
            /// <summary>
            /// 剧烈运动检测
            /// </summary>
            VCA_VIOLENT_MOTION = 0x200,
            /// <summary>
            /// 攀高检测
            /// </summary>
            VCA_REACH_HIGHT = 0x400,
            /// <summary>
            /// 起身检测
            /// </summary>
            VCA_GET_UP = 0x800,
            /// <summary>
            /// 物品遗留
            /// </summary>
            VCA_LEFT = 0x1000,
            /// <summary>
            /// 物品拿取
            /// </summary>
            VCA_TAKE = 0x2000,
            /// <summary>
            /// 离岗
            /// </summary>
            VCA_LEAVE_POSITION = 0x4000,
            /// <summary>
            /// 尾随
            /// </summary>
            VCA_TRAIL = 0x8000,
            /// <summary>
            /// 重点人员起身检测
            /// </summary>
            VCA_KEY_PERSON_GET_UP = 0x10000,
            /// <summary>
            /// 倒地检测
            /// </summary>
            VCA_FALL_DOWN = 0x80000,
            /// <summary>
            /// 声强突变检测
            /// </summary>
            VCA_AUDIO_ABNORMAL = 0x100000,
            /// <summary>
            /// 折线攀高
            /// </summary>
            VCA_ADV_REACH_HEIGHT = 0x200000,
            /// <summary>
            /// 如厕超时
            /// </summary>
            VCA_TOILET_TARRY = 0x400000,
            /// <summary>
            /// 放风场滞留
            /// </summary>
            VCA_YARD_TARRY = 0x800000,
            /// <summary>
            /// 折线警戒面
            /// </summary>
            VCA_ADV_TRAVERSE_PLANE = 0x1000000,
            /// <summary>
            /// 人靠近ATM 只在ATM_PANEL模式下支持
            /// </summary>
            VCA_HUMAN_ENTER = 0x10000000,
            /// <summary>
            /// 操作超时  只在ATM_PANEL模式下支持
            /// </summary>
            VCA_OVER_TIME = 0x20000000,
            /// <summary>
            /// 贴纸条,支持区域规则
            /// </summary>
            VCA_STICK_UP = 0x40000000,
            /// <summary>
            /// 安装读卡器,支持区域规则
            /// </summary>
            VCA_INSTALL_SCANNER = 0x80000000
        }

        /// <summary>
        /// 行为分析事件类型扩展
        /// </summary>
        public enum VCA_RULE_EVENT_TYPE_EX : ushort
        {
            /// <summary>
            /// 穿越警戒面
            /// </summary>
            ENUM_VCA_EVENT_TRAVERSE_PLANE = 1,
            /// <summary>
            /// 目标进入区域,支持区域规则
            /// </summary>
            ENUM_VCA_EVENT_ENTER_AREA = 2,
            /// <summary>
            /// 目标离开区域,支持区域规则
            /// </summary>
            ENUM_VCA_EVENT_EXIT_AREA = 3,
            /// <summary>
            /// 周界入侵,支持区域规则
            /// </summary>
            ENUM_VCA_EVENT_INTRUSION = 4,
            /// <summary>
            /// 徘徊,支持区域规则
            /// </summary>
            ENUM_VCA_EVENT_LOITER = 5,
            /// <summary>
            /// 物品遗留拿取,支持区域规则
            /// </summary>
            ENUM_VCA_EVENT_LEFT_TAKE = 6,
            /// <summary>
            /// 停车,支持区域规则
            /// </summary>
            ENUM_VCA_EVENT_PARKING = 7,
            /// <summary>
            /// 快速移动,支持区域规则
            /// </summary>
            ENUM_VCA_EVENT_RUN = 8,
            /// <summary>
            /// 区域内人员聚集,支持区域规则
            /// </summary>
            ENUM_VCA_EVENT_HIGH_DENSITY = 9,
            /// <summary>
            /// 剧烈运动检测
            /// </summary>
            ENUM_VCA_EVENT_VIOLENT_MOTION = 10,
            /// <summary>
            /// 攀高检测
            /// </summary>
            ENUM_VCA_EVENT_REACH_HIGHT = 11,
            /// <summary>
            /// 起身检测
            /// </summary>
            ENUM_VCA_EVENT_GET_UP = 12,
            /// <summary>
            /// 物品遗留
            /// </summary>
            ENUM_VCA_EVENT_LEFT = 13,
            /// <summary>
            /// 物品拿取
            /// </summary>
            ENUM_VCA_EVENT_TAKE = 14,
            /// <summary>
            /// 离岗
            /// </summary>
            ENUM_VCA_EVENT_LEAVE_POSITION = 15,
            /// <summary>
            /// 尾随
            /// </summary>
            ENUM_VCA_EVENT_TRAIL = 16,
            /// <summary>
            /// 重点人员起身检测
            /// </summary>
            ENUM_VCA_EVENT_KEY_PERSON_GET_UP = 17,
            /// <summary>
            /// 倒地检测
            /// </summary>
            ENUM_VCA_EVENT_FALL_DOWN = 20,
            /// <summary>
            /// 声强突变检测
            /// </summary>
            ENUM_VCA_EVENT_AUDIO_ABNORMAL = 21,
            /// <summary>
            /// 折线攀高
            /// </summary>
            ENUM_VCA_EVENT_ADV_REACH_HEIGHT = 22,
            /// <summary>
            /// 厕如超时
            /// </summary>
            ENUM_VCA_EVENT_TOILET_TARRY = 23,
            /// <summary>
            /// 放风场滞留
            /// </summary>
            ENUM_VCA_EVENT_YARD_TARRY = 24,
            /// <summary>
            /// 折线警戒面
            /// </summary>
            ENUM_VCA_EVENT_ADV_TRAVERSE_PLANE = 25,
            /// <summary>
            /// 人靠近ATM,只在ATM_PANEL模式下支持  
            /// </summary>
            ENUM_VCA_EVENT_HUMAN_ENTER = 29,
            /// <summary>
            /// 操作超时,只在ATM_PANEL模式下支持
            /// </summary>
            ENUM_VCA_EVENT_OVER_TIME = 30,
            /// <summary>
            /// 贴纸条,支持区域规则
            /// </summary>
            ENUM_VCA_EVENT_STICK_UP = 31,
            /// <summary>
            /// 安装读卡器,支持区域规则
            /// </summary>
            ENUM_VCA_EVENT_INSTALL_SCANNER = 32
        }

        /// <summary>
        /// 警戒面穿越方向类型
        /// </summary>
        public enum VCA_CROSS_DIRECTION
        {
            /// <summary>
            /// 双向
            /// </summary>
            VCA_BOTH_DIRECTION,
            /// <summary>
            /// 由左至右
            /// </summary>
            VCA_LEFT_GO_RIGHT,
            /// <summary>
            /// 由右至左
            /// </summary>
            VCA_RIGHT_GO_LEFT,
        }
        #endregion

        #region lCommand消息类型
        /// <summary>
        /// lCommand消息类型
        /// </summary>
        public enum LCommandMsgType
        {
            #region 智能报警
            /// <summary>
            /// 行为分析信息 NET_VCA_RULE_ALARM
            /// </summary>
            COMM_ALARM_RULE = 0x1102,
            /// <summary>
            /// 客流量统计报警信息 NET_DVR_PDC_ALRAM_INFO
            /// </summary>
            COMM_ALARM_PDC = 0x1103,
            /// <summary>
            ///  事件数据信息 NET_DVR_RULE_INFO_ALARM
            /// </summary>
            COMM_RULE_INFO_UPLOAD = 0x1107,
            /// <summary>
            /// 人脸检测识别报警信息 NET_DVR_FACEDETECT_ALARM
            /// </summary>
            COMM_ALARM_FACE = 0x1106,
            /// <summary>
            ///  人脸抓拍结果信息 NET_VCA_FACESNAP_RESULT
            /// </summary>
            COMM_UPLOAD_FACESNAP_RESULT = 0x1112,
            /// <summary>
            /// 人脸抓拍人员统计信息 NET_DVR_FACECAPTURE_STATISTICS_RESULT
            /// </summary>
            COMM_FACECAPTURE_STATISTICS_RESULT = 0x112a,
            /// <summary>
            ///  人脸黑名单比对结果信息 NET_VCA_FACESNAP_MATCH_ALARM
            /// </summary>
            COMM_SNAP_MATCH_ALARM = 0x2902,
            /// <summary>
            ///  人脸比对报警（Json数据透传方式） NET_VCA_FACESNAP_RAWDATA_ALARM
            /// </summary>
            COMM_FACESNAP_RAWDATA_ALARM = 0x6015,
            /// <summary>
            /// 人脸侦测报警信息 NET_DVR_FACE_DETECTION
            /// </summary>
            COMM_ALARM_FACE_DETECTION = 0x4010,
            /// <summary>
            ///  教师离开讲台报警 NET_DVR_TARGET_LEFT_REGION_ALARM
            /// </summary>
            COMM_ALARM_TARGET_LEFT_REGION = 0x4011,
            /// <summary>
            ///  人员侦测信息 NET_DVR_PEOPLE_DETECTION_RESULT
            /// </summary>
            COMM_PEOPLE_DETECTION_UPLOAD = 0x4014,
            /// <summary>
            ///  智能检测通用报警(人体目标识别报警等，Json数据结构) EVENT_JSON
            /// </summary>
            COMM_VCA_ALARM = 0x4521,
            /// <summary>
            ///  VQD报警信息 NET_DVR_VQD_ALARM
            /// </summary>
            COMM_ALARM_VQD_EX = 0x1116,
            /// <summary>
            ///  诊断服务器VQD报警信息 NET_DVR_DIAGNOSIS_UPLOAD
            /// </summary>
            COMM_DIAGNOSIS_UPLOAD =0x5100,
            /// <summary>
            ///  VQD诊断报警信息 NET_DVR_VQD_DIAGNOSE_INFO
            /// </summary>
            COMM_ALARM_VQD = 0x6000,
            /// <summary>
            /// 场景变更报警信息 NET_DVR_SCENECHANGE_DETECTION_RESULT
            /// </summary>
            COMM_SCENECHANGE_DETECTION_UPLOAD = 0x1130,
            /// <summary>
            ///  压线报警信息 NET_DVR_CROSSLINE_ALARM
            /// </summary>
            COMM_CROSSLINE_ALARM = 0x1131,
            /// <summary>
            /// 声音报警信息 NET_DVR_AUDIOEXCEPTION_ALARM
            /// </summary>
            COMM_ALARM_AUDIOEXCEPTION = 0x1150,
            /// <summary>
            ///  虚焦报警信息 NET_DVR_DEFOCUS_ALARM
            /// </summary>
            COMM_ALARM_DEFOCUS = 0x1151,
            /// <summary>
            /// 开关灯检测报警信息 NET_DVR_SWITCH_LAMP_ALARM
            /// </summary>
            COMM_SWITCH_LAMP_ALARM = 0x6002,
            /// <summary>
            ///  热度图报警信息 NET_DVR_HEATMAP_RESULT
            /// </summary>
            COMM_UPLOAD_HEATMAP_RESULT = 0x4008,
            /// <summary>
            ///  火点检测报警信息 NET_DVR_FIREDETECTION_ALARM
            /// </summary>
            COMM_FIREDETECTION_ALARM = 0x4991,
            /// <summary>
            ///  温差报警信息 NET_DVR_THERMOMETRY_DIFF_ALARM
            /// </summary>
            COMM_THERMOMETRY_DIFF_ALARM = 0x5211,
            /// <summary>
            ///  温度报警信息 NET_DVR_THERMOMETRY_ALARM
            /// </summary>
            COMM_THERMOMETRY_ALARM = 0x5212,
            ///// <summary>
            /////  船只检测报警信息 NET_DVR_SHIPSDETECTION_ALARM
            ///// </summary>
            //COMM_ALARM_SHIPSDETECTION = 0x4521,
            #endregion

            #region 报警主机
            /// <summary>
            ///  网络报警主机报警信息 NET_DVR_ALARMHOST_ALARMINFO
            /// </summary>
            COMM_ALARM_ALARMHOST = 0x1105,
            /// <summary>
            ///  模拟量数据实时信息 NET_DVR_SENSOR_ALARM
            /// </summary>
            COMM_SENSOR_VALUE_UPLOAD = 0x1120,
            /// <summary>
            ///  模拟量报警信息 NET_DVR_SENSOR_ALARM
            /// </summary>
            COMM_SENSOR_ALARM = 0x1121,
            /// <summary>
            ///  开关量报警信息 NET_DVR_SWITCH_ALARM
            /// </summary>
            COMM_SWITCH_ALARM = 0x1122,
            /// <summary>
            ///  故障报警信息 NET_DVR_ALARMHOST_EXCEPTION_ALARM
            /// </summary>
            COMM_ALARMHOST_EXCEPTION = 0x1123,
            /// <summary>
            ///  防护舱状态信息 NET_DVR_ALARMHOST_SAFETYCABINSTATE
            /// </summary>
            COMM_ALARMHOST_SAFETYCABINSTATE = 0x1125,
            /// <summary>
            /// 报警输出口或警号状态信息 NET_DVR_ALARMHOST_ALARMOUTSTATUS
            /// </summary>
            COMM_ALARMHOST_ALARMOUTSTATUS = 0x1126,
            /// <summary>
            /// 报警主机CID报告报警上传 NET_DVR_CID_ALARM
            /// </summary>
            COMM_ALARMHOST_CID_ALARM = 0x1127,
            /// <summary>
            ///  报警主机外接设备报警信息 NET_DVR_485_EXTERNAL_DEVICE_ALARMINFO
            /// </summary>
            COMM_ALARMHOST_EXTERNAL_DEVICE_ALARM = 0x1128,
            /// <summary>
            ///  报警数据信息 NET_DVR_ALARMHOST_DATA_UPLOAD
            /// </summary>
            COMM_ALARMHOST_DATA_UPLOAD = 0x1129,

            #endregion

            #region 智能交通
            /// <summary>
            ///  交通事件报警信息 NET_DVR_AID_ALARM
            /// </summary>
            COMM_ALARM_AID = 0x1110,
            /// <summary>
            ///  交通参数统计报警信息 NET_DVR_TPS_ALARM
            /// </summary>
            COMM_ALARM_TPS = 0x1111,
            /// <summary>
            ///  交通取证报警信息 NET_DVR_TFS_ALARM
            /// </summary>
            COMM_ALARM_TFS = 0x1113,
            /// <summary>
            /// 交通参数统计报警信息(扩展) NET_DVR_TPS_ALARM_V41
            /// </summary>
            COMM_ALARM_TPS_V41 = 0x1114,
            /// <summary>
            ///  交通事件报警信息扩展 NET_DVR_AID_ALARM_V41
            /// </summary>
            COMM_ALARM_AID_V41 = 0x1115,
            /// <summary>
            ///  交通抓拍结果 NET_DVR_PLATE_RESULT
            /// </summary>
            COMM_UPLOAD_PLATE_RESULT = 0x2800,
            /// <summary>
            ///  实时状态检测结果 NET_ITC_STATUS_DETECT_RESULT
            /// </summary>
            COMM_ITC_STATUS_DETECT_RESULT =0x2810,
            /// <summary>
            ///  交通抓拍结果(新报警信息) NET_ITS_PLATE_RESULT
            /// </summary>
            COMM_ITS_PLATE_RESULT = 0x3050,
            /// <summary>
            ///  交通统计数据上传 NET_ITS_TRAFFIC_COLLECT
            /// </summary>
            COMM_ITS_TRAFFIC_COLLECT = 0x3051,
            /// <summary>
            ///  出入口车辆抓拍数据 NET_ITS_GATE_VEHICLE
            /// </summary>
            COMM_ITS_GATE_VEHICLE =0x3052,
            /// <summary>
            ///  出入口人脸抓拍数据 NET_ITS_GATE_FACE
            /// </summary>
            COMM_ITS_GATE_FACE =0x3053,
            /// <summary>
            ///  出入口过车收费明细 NET_ITS_PASSVEHICLE_COST_ITEM
            /// </summary>
            COMM_ITS_GATE_COSTITEM =0x3054,
            /// <summary>
            ///  出入口交接班数据 NET_ITS_HANDOVER_INFO
            /// </summary>
            COMM_ITS_GATE_HANDOVER =0x3055,
            /// <summary>
            ///  停车场数据上传 NET_ITS_PARK_VEHICLE
            /// </summary>
            COMM_ITS_PARK_VEHICLE = 0x3056,
            /// <summary>
            ///  车辆黑名单报警上传 NET_ITS_ECT_BLACKLIST
            /// </summary>
            COMM_ITS_BLACKLIST_ALARM = 0x3057,
            /// <summary>
            ///  车辆黑白名单数据需要同步报警上传 NET_DVR_VEHICLE_CONTROL_LIST_DSALARM
            /// </summary>
            COMM_VEHICLE_CONTROL_LIST_DSALARM = 0x3058,
            /// <summary>
            /// 黑白名单车辆报警上传 NET_DVR_VEHICLE_CONTROL_ALARM
            /// </summary>
            COMM_VEHICLE_CONTROL_ALARM = 0x3059,
            /// <summary>
            ///  消防报警上传 NET_DVR_FIRE_ALARM
            /// </summary>
            COMM_FIRE_ALARM = 0x3060,
            /// <summary>
            ///  车辆二次识别结果上传 NET_DVR_VEHICLE_RECOG_RESULT
            /// </summary>
            COMM_VEHICLE_RECOG_RESULT = 0x3062,
            /// <summary>
            ///  传感器上传信息 NET_DVR_SENSOR_INFO_UPLOAD
            /// </summary>
            COMM_ALARM_SENSORINFO_UPLOAD = 0x3077,
            /// <summary>
            ///  抓拍图片上传 NET_DVR_CAPTURE_UPLOAD
            /// </summary>
            COMM_ALARM_CAPTURE_UPLOAD = 0x3078,
            /// <summary>
            ///  雷达报警上传 NET_DVR_ALARM_RADARINFO
            /// </summary>
            COMM_ITS_RADARINFO = 0x3079,
            /// <summary>
            ///  信号灯异常检测上传 NET_DVR_SIGNALLAMP_DETCFG
            /// </summary>
            COMM_SIGNAL_LAMP_ABNORMAL = 0x3080,
            /// <summary>
            ///  TPS实时过车数据上传 NET_DVR_TPS_REAL_TIME_INFO
            /// </summary>
            COMM_ALARM_TPS_REAL_TIME = 0x3081,
            /// <summary>
            ///  TPS统计过车数据上传 NET_DVR_TPS_STATISTICS_INFO
            /// </summary>
            COMM_ALARM_TPS_STATISTICS = 0x3082,
            /// <summary>
            ///  路口设备异常报警信息 NET_ITS_ROADINFO
            /// </summary>
            COMM_ITS_ROAD_EXCEPTION = 0x4500,
            /// <summary>
            ///  指示灯外控报警信息 NET_DVR_EXTERNAL_CONTROL_ALARM
            /// </summary>
            COMM_ITS_EXTERNAL_CONTROL_ALARM = 0x4520,
            /// <summary>
            ///  出入口控制机数据 NET_DVR_GATE_ALARMINFO
            /// </summary>
            COMM_ITS_GATE_ALARMINFO = 0x3061,
            /// <summary>
            ///  出入口付费信息 NET_DVR_GATE_CHARGEINFO
            /// </summary>
            COMM_GATE_CHARGEINFO_UPLOAD = 0x3064,
            /// <summary>
            ///  出入口控制器TME车辆抓拍信息 NET_DVR_TME_VEHICLE_RESULT
            /// </summary>
            COMM_TME_VEHICLE_INDENTIFICATION = 0x3065,
            /// <summary>
            ///  出入口卡片信息 NET_DVR_GATE_CARDINFO
            /// </summary>
            COMM_GATE_CARDINFO_UPLOAD = 0x3066,
            #endregion

            #region 其他设备报警

            /// <summary>
            ///  移动侦测、视频丢失、遮挡、IO信号量等报警信息(V3.0以下版本支持的设备) NET_DVR_ALARMINFO
            /// </summary>
            COMM_ALARM = 0x1100,
            /// <summary>
            ///  移动侦测、视频丢失、遮挡、IO信号量等报警信息(V3.0以上版本支持的设备) NET_DVR_ALARMINFO_V30
            /// </summary>
            COMM_ALARM_V30 = 0x4000,
            /// <summary>
            ///  移动侦测、视频丢失、遮挡、IO信号量等报警信息，报警数据为可变长 NET_DVR_ALARMINFO_V40
            /// </summary>
            COMM_ALARM_V40 = 0x4007,
            /// <summary>
            ///  混合型DVR、NVR等在IPC接入配置改变时的报警信息 NET_DVR_IPALARMINFO
            /// </summary>
            COMM_IPCCFG = 0x4001,
            /// <summary>
            ///  混合型DVR、NVR等在IPC接入配置改变时的报警信息（扩展） NET_DVR_IPALARMINFO_V31
            /// </summary>
            COMM_IPCCFG_V31 = 0x4002,
            /// <summary>
            ///  PIR报警、无线报警、呼救报警信息 NET_IPC_AUXALARM_RESULT
            /// </summary>
            COMM_IPC_AUXALARM_RESULT = 0x2820,
            /// <summary>
            ///  CVR设备报警信息，由于通道值大于256而扩展 NET_DVR_ALARMINFO_DEV
            /// </summary>
            COMM_ALARM_DEVICE = 0x4004,
            /// <summary>
            /// CVR设备报警信息扩展(增加报警信息子结构) NET_DVR_ALARMINFO_DEV_V40
            /// </summary>
            COMM_ALARM_DEVICE_V40 = 0x4009,
            /// <summary>
            /// CVR外部报警信息 NET_DVR_CVR_ALARM
            /// </summary>
            COMM_ALARM_CVR = 0x4005,
            /// <summary>
            ///  ATM DVR交易信息 NET_DVR_TRADEINFO
            /// </summary>
            COMM_TRADEINFO = 0x1500,
            /// <summary>
            /// 热备异常报警（N+1模式异常报警）信息 NET_DVR_ALARM_HOT_SPARE
            /// </summary>
            COMM_ALARM_HOT_SPARE = 0x4006,
            /// <summary>
            ///  按钮按下报警信息(IP可视对讲主机) NET_BUTTON_DOWN_EXCEPTION_ALARM
            /// </summary>
            COMM_ALARM_BUTTON_DOWN_EXCEPTION = 0x1152,
            /// <summary>
            ///  门禁主机报警信息 NET_DVR_ACS_ALARM_INFO
            /// </summary>
            COMM_ALARM_ACS =0x5002,
            /// <summary>
            ///  多屏控制器上传的报警信息 NET_DVR_SCREENALARMCFG
            /// </summary>
            COMM_SCREEN_ALARM = 0x5000,
            /// <summary>
            ///  LCD屏幕报警信息 NET_DVR_LCD_ALARM
            /// </summary>
            COMM_ALARM_LCD = 0x5011,
            /// <summary>
            ///  可视对讲事件记录信息 NET_DVR_VIDEO_INTERCOM_EVENT
            /// </summary>
            COMM_UPLOAD_VIDEO_INTERCOM_EVENT = 0x1132,
            /// <summary>
            ///  可视对讲报警信息 NET_DVR_VIDEO_INTERCOM_ALARM
            /// </summary>
            COMM_ALARM_VIDEO_INTERCOM = 0x1133,
            /// <summary>
            ///  解码器智能解码报警信息 NET_DVR_DEC_VCA_ALARM
            /// </summary>
            COMM_ALARM_DEC_VCA = 0x5010,
            /// <summary>
            ///  GIS信息 NET_DVR_GIS_UPLOADINFO
            /// </summary>
            COMM_GISINFO_UPLOAD = 0x4012,
            /// <summary>
            ///  防破坏报警信息 NET_DVR_VANDALPROOF_ALARM
            /// </summary>
            COMM_VANDALPROOF_ALARM = 0x4013,
            /// <summary>
            /// 存储智能检测报警信息 NET_DVR_STORAGE_DETECTION_ALARM
            /// </summary>
            COMM_ALARM_STORAGE_DETECTION = 0x4015,
            /// <summary>
            ///  会议终端会议呼叫报警信息 NET_DVR_CONFERENCE_CALL_ALARM
            /// </summary>
            COMM_CONFERENCE_CALL_ALARM = 0x5012,
            /// <summary>
            ///  GPS报警信息 NET_DVR_GPSALARMINFO
            /// </summary>
            COMM_ALARM_ALARMGPS = 0x1202,
            /// <summary>
            ///  交换机报警信息 NET_DVR_SWITCH_CONVERT_ALARM
            /// </summary>
            COMM_ALARM_SWITCH_CONVERT = 0x5004,
            /// <summary>
            ///  审讯主机报警信息 NET_DVR_INQUEST_ALARM
            /// </summary>
            COMM_INQUEST_ALARM = 0x6005,
            /// <summary>
            ///  鹰眼全景联动到位事件信息 NET_DVR_PANORAMIC_LINKAGE
            /// </summary>
            COMM_PANORAMIC_LINKAGE_ALARM = 0x5213,


            #endregion

        }
        #endregion

        #region 传输方式
        /// <summary>
        /// 传输方式
        /// </summary>
        public enum TransProtol
        {

            /// <summary>
            /// TCP
            /// </summary>
            TCP = 0,
            /// <summary>
            /// UDP
            /// </summary>
            UDP,
            /// <summary>
            /// MCAST
            /// </summary>
            MCAST
        }
        #endregion

        #region 报警类型

        /// <summary>
        /// 报警类型
        /// </summary>
        public enum AlarmType
        {
            信号量报警 = 0,
            硬盘满,
            信号丢失,
            移动侦测,
            硬盘未格式化,
            读写硬盘出错,
            遮挡报警,
            制式不匹配,
            非法访问,
            视频信号异常,
            录像_抓图异常,
            智能场景变化,
            阵列异常,
            前端_录像分辨率不匹配,
            智能侦测,
            POE供电异常,
            闪光灯异常
        }
        #endregion

        #region 数据类型
        /// <summary>
        /// 数据类型
        /// </summary>
        public enum StreamDataType
        {
            /// <summary>
            /// 实时流
            /// </summary>
            RealTimeStream = 1,
            /// <summary>
            /// 文件流
            /// </summary>
            FileStream = 2
        }
        #endregion

        #region 码流类型
        /// <summary>
        /// 码流类型
        /// </summary>
        public enum StreamType
        {
            /// <summary>
            /// 主码流
            /// </summary>
            主码流 = 0,
            /// <summary>
            /// 子码流
            /// </summary>
            子码流,
            /// <summary>
            /// 码流3
            /// </summary>
            码流3,
            /// <summary>
            /// 码流4
            /// </summary>
            码流4
        }
        #endregion

        #region 连接方式
        /// <summary>
        /// 连接方式
        /// </summary>
        public enum ConnetType
        {
            /// <summary>
            /// TCP连接
            /// </summary>
            TCP = 0,
            /// <summary>
            /// DUP连接
            /// </summary>
            UDP,
            /// <summary>
            /// 多播方式
            /// </summary>
            Multicast,
            /// <summary>
            /// RTP连接
            /// </summary>
            RTP,
            /// <summary>
            /// RTP_RTSP
            /// </summary>
            RTP_RTSP,
            /// <summary>
            /// RSTP_HTTP
            /// </summary>
            RSTP_HTTP
        }
        #endregion

        #region 远程控制命令
        /// <summary>
        /// 远程控制命令
        /// </summary>
        public enum RemoteCommand
        {
            /// <summary>
            /// 检测设备是否在线
            /// </summary>
            NET_DVR_CHECK_USER_STATUS = 20005,
            /// <summary>
            /// 获取设备工作状态
            /// </summary>
            NET_DVR_GET_WORK_STATUS = 6189,
            /// <summary>
            /// 快球恢复前端默认参数,对应结构 NET_DVR_REMOTECONTROL_STUDY_PARAM
            /// </summary>
            NET_DVR_CONTROL_RESTORE_SUPPORT = 3311,
            /// <summary>
            /// 快球机芯重启,对应结构 NET_DVR_REMOTECONTROL_STUDY_PARAM
            /// </summary>
            NET_DVR_CONTROL_RESTART_SUPPORT = 3312,
            /// <summary>
            /// 设置完全恢复出厂值,对应结构 NET_DVR_COMPLETE_RESTORE_INFO
            /// </summary>
            NET_DVR_COMPLETE_RESTORE_CTRL = 3420,
            /// <summary>
            /// 重启CVR服务
            /// </summary>
            NET_DVR_RESTART_SERVICE = 6213,
            /// <summary>
            ///  加载磁盘,对应结构  NET_DVR_MOUNT_DISK_PARAM
            /// </summary>
            NET_DVR_MOUNT_DISK = 6015,
            /// <summary>
            ///  卸载磁盘,对应结构  NET_DVR_MOUNT_DISK_PARAM
            /// </summary>
            NET_DVR_UNMOUNT_DISK = 6016,
            /// <summary>
            ///  删除无效磁盘,对应结构  NET_DVR_INVALID_DISK_PARAM
            /// </summary>
            NET_DVR_DEL_INVALID_DISK = 6107,
            /// <summary>
            ///  清空门禁主机参数,对应结构  NET_DVR_ACS_PARAM_TYPE
            /// </summary>
            NET_DVR_CLEAR_ACS_PARAM = 2118,
            /// <summary>
            ///   删除门禁指纹参数,对应结构  NET_DVR_FINGER_PRINT_INFO_CTRL
            /// </summary> 
            NET_DVR_DEL_FINGERPRINT_CFG = 2152



        }
        #endregion

        #region 控制命令
        /// <summary>
        /// dwCommand宏定义
        /// </summary>
        public enum DwCommand
        {

            /// <summary>
            /// 获取时间参数，对应NET_DVR_TIME结构体
            /// </summary>
            NET_DVR_GET_TIMECFG = 118,
            /// <summary>
            /// 设置时间参数，对应NET_DVR_TIME结构体
            /// </summary>
            NET_DVR_SET_TIMECFG = 119,
            /// <summary>
            /// 获取设备参数(扩展)，对应NET_DVR_DEVICECFG_V40结构体
            /// </summary>
            NET_DVR_GET_DEVICECFG_V40 = 1100

        }
        #endregion

        #region SDK本地配置
        /// <summary>
        /// 设置SDK本地参数
        /// </summary>
        public enum NET_SDK_LOCAL_CFG_TYPE
        {
            /// <summary>
            ///  本地TCP端口绑定配置 对应结构体 NET_DVR_LOCAL_TCP_PORT_BIND_CFG
            /// </summary>
            TCP_PORT_BIND = 0,
            /// <summary>
            /// 本地UDP端口绑定配置  对应结构体 NET_DVR_LOCAL_UDP_PORT_BIND_CFG
            /// </summary>
            UDP_PORT_BIND = 1,
            /// <summary>
            /// 内存池本地配置  对应结构体 NET_DVR_LOCAL_MEM_POOL_CFG
            /// </summary>
            MEM_POOL = 2,
            /// <summary>
            /// 按模块配置超时时间 对应结构体 NET_DVR_LOCAL_MODULE_RECV_TIMEOUT_CFG
            /// </summary>
            MODULE_RECV_TIMEOUT = 3,
            /// <summary>
            /// 是否使用能力集解析库  对应结构体 NET_DVR_LOCAL_ABILITY_PARSE_CFG
            /// </summary>
            ABILITY_PARSE = 4,
            /// <summary>
            ///  对讲模式配置  对应结构体 NET_DVR_LOCAL_TALK_MODE_CFG
            /// </summary>
            TALK_MODE = 5,
            /// <summary>
            ///  密钥配置  对应结构体 NET_DVR_LOCAL_PROTECT_KEY_CFG
            /// </summary>
            PROTECT_KEY = 6,
            /// <summary>
            /// 心跳交互间隔时间配置  对应结构体 NET_DVR_LOCAL_CHECK_DEV
            /// </summary>
            CHECK_DEV = 10

        }

        #endregion

        #region 日志备份类型
        public enum BackupLogType
        {
            /// <summary>
            /// 按文件名备份录像文件
            /// </summary>
            ByFileName = 1,
            /// <summary>
            /// 按时间段备份录像文件
            /// </summary>
            ByDateTime = 2,
            /// <summary>
            /// 备份图片
            /// </summary>
            BackupImage = 3,
            /// <summary>
            /// 恢复审讯事件
            /// </summary>
            RecoveryTrial = 4,
            /// <summary>
            /// 备份日志
            /// </summary>
            BackupLog = 5
        }
        #endregion
    }
}
