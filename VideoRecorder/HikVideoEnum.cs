namespace HikDeviceApi.VideoRecorder
{
    /// <summary>
    /// 日 期:2015-09-09
    /// 作 者:痞子少爷
    /// 描 述:海康硬盘录像机接口枚举
    /// </summary>
    public static class HikVideoEnum
    {
        #region 控制录像回放状态命令
        /// <summary>
        /// 控制录像回放状态命令
        /// </summary>
        public enum PlayBackControlCmd
        {
            /// <summary>
            /// 开始播放
            /// </summary>
            NET_DVR_PLAYSTART = 1,
            /// <summary>
            ///  暂停播放
            /// </summary>
            NET_DVR_PLAYPAUSE = 3,
            /// <summary>
            /// 恢复播放（在暂停后调用将恢复暂停前的速度播放）
            /// </summary>
            NET_DVR_PLAYRESTART = 4,
            /// <summary>
            ///  快放
            /// </summary>
            NET_DVR_PLAYFAST = 5,
            /// <summary>
            /// 慢放
            /// </summary>
            NET_DVR_PLAYSLOW = 6,
            /// <summary>
            ///  正常速度播放（快放或者慢放时调用将恢复单倍速度播放） 
            /// </summary>
            NET_DVR_PLAYNORMAL = 7,
            /// <summary>
            /// 单帧放（恢复正常回放使用NET_DVR_PLAYNORMAL命令）
            /// </summary>
            NET_DVR_PLAYFRAME = 8,
            /// <summary>
            /// 打开声音
            /// </summary>
            NET_DVR_PLAYSTARTAUDIO = 9,
            /// <summary>
            ///  关闭声音
            /// </summary>
            NET_DVR_PLAYSTOPAUDIO = 10,
            /// <summary>
            /// 调节音量，取值范围[0,0xffff]
            /// </summary>
            NET_DVR_PLAYAUDIOVOLUME = 11,
            /// <summary>
            /// 改变文件回放的进度
            /// </summary>
            NET_DVR_PLAYSETPOS = 12,
            /// <summary>
            /// 获取按文件或者按时间回放的进度
            /// </summary>
            NET_DVR_PLAYGETPOS = 13,
            /// <summary>
            /// 获取当前已经播放的时间(按文件回放的时候有效)
            /// </summary>
            NET_DVR_PLAYGETTIME = 14,
            /// <summary>
            /// 获取当前已经播放的帧数(按文件回放的时候有效，倒放不支持)
            /// </summary>
            NET_DVR_PLAYGETFRAME = 15,
            /// <summary>
            /// 获取当前播放文件总的帧数(按文件回放的时候有效，倒放不支持)
            /// </summary>
            NET_DVR_GETTOTALFRAMES = 16,
            /// <summary>
            ///  获取当前播放文件总的时间(按文件回放的时候有效) 
            /// </summary>
            NET_DVR_GETTOTALTIME = 17,
            /// <summary>
            ///  丢B帧
            /// </summary>
            NET_DVR_THROWBFRAME = 20,
            /// <summary>
            /// 设置码流速度，速度单位：kbps，最小为256kbps，最大为设备带宽
            /// </summary>
            NET_DVR_SETSPEED = 24,
            /// <summary>
            ///  保持与设备的心跳(如果回调阻塞，建议2秒发送一次) 
            /// </summary>
            NET_DVR_KEEPALIVE = 25,
            /// <summary>
            ///  按绝对时间定位
            /// </summary>
            NET_DVR_PLAYSETTIME = 26,
            /// <summary>
            /// 获取按时间回放对应时间段内的所有文件的总长度
            /// </summary>
            NET_DVR_PLAYGETTOTALLEN = 27,
            /// <summary>
            ///  倒放切换为正放
            /// </summary>
            NET_DVR_PLAY_FORWARD = 29,
            /// <summary>
            /// 正放切换为倒放
            /// </summary>
            NET_DVR_PLAY_REVERSE = 30,
            /// <summary>
            /// 设置转封装类型
            /// </summary>
            NET_DVR_SET_TRANS_TYPE = 32,
            /// <summary>
            ///  回放转码
            /// </summary>
            NET_DVR_PLAY_CONVERT = 33,
            /// <summary>
            /// 开始抽帧回放
            /// </summary>
            NET_DVR_START_DRAWFRAME = 34,
            /// <summary>
            /// 停止抽帧回放
            /// </summary>
            NET_DVR_STOP_DRAWFRAME = 35

        }
        #endregion

        #region 协议类型
        /// <summary>
        /// 协议类型
        /// </summary>
        public enum ProtoType
        {
            ENUM_BUSINESS_INVALID = -1,
            ENUM_BUSINESS_HIKVISION = 0,
            ENUM_BUSINESS_PANASONIC,
            ENUM_BUSINESS_SONY,
            ENUM_BUSINESS_AXIS,
            ENUM_BUSINESS_SANYO,
            ENUM_BUSINESS_BOSCH,
            ENUM_BUSINESS_ZAVIO,
            ENUM_BUSINESS_GRANDEYE,
            ENUM_BUSINESS_PROVIDEO,
            ENUM_BUSINESS_ARECONT,        //9
            ENUM_BUSINESS_ACTI,
            ENUM_BUSINESS_PELCO,
            ENUM_BUSINESS_VIVOTEK,
            ENUM_BUSINESS_INFINOVA,
            ENUM_BUSINESS_DAHUA,          //14
            ENUM_BUSINESS_HIK_STD_H264 = 0x20,
            ENUM_BUSINESS_HIK_STD_MPEG4,
            /// <summary>
            /// 景阳
            /// </summary>
            ENUM_BUSINESS_SUNELL,         //景阳

            ENUM_BUSINESS_ATEME,
            /// <summary>
            /// 朗驰
            /// </summary>
            ENUM_BUSINESS_LAUNCH,         //朗驰
            /// <summary>
            /// 雅安
            /// </summary>
            ENUM_BUSINESS_YAAN,           //雅安
            /// <summary>
            /// 蓝色星际
            /// </summary>
            ENUM_BUSINESS_BLUESKY,        //蓝色星际
            /// <summary>
            /// 蓝色星际
            /// </summary>
            ENUM_BUSINESS_BLUESKYLIMIT,   //蓝色星际
            /// <summary>
            /// 天地伟业
            /// </summary>
            ENUM_BUSINESS_TDWY,           //天地伟业
            /// <summary>
            /// 汉邦高科
            /// </summary>
            ENUM_BUSINESS_HBGK,           //汉邦高科
            /// <summary>
            /// 金三立
            /// </summary>
            ENUM_BUSINESS_SANTACHI,       //金三立
            /// <summary>
            /// 恒忆
            /// </summary>
            ENUM_BUSINESS_HIGHEASY,       //恒忆
            ENUM_BUSINESS_SAMSUNG,
            /// <summary>
            /// url类型取流
            /// </summary>
            ENUM_BUSINESS_URL_RTSP = 0x40,//url类型取流
            ENUM_BUSINESS_ONVIF,
            /// <summary>
            /// 最大厂商类型
            /// </summary>
            ENUM_MAX_BUSINESS_TYPE,       //最大厂商类型
        }
        #endregion

        #region 交易类型
        /// <summary>
        /// 交易类型
        /// </summary>
        public enum TransactionType
        {
            /// <summary>
            /// 全部
            /// </summary>
            All = 0,
            /// <summary>
            /// 查询
            /// </summary>
            Select,
            /// <summary>
            /// 取款
            /// </summary>
            DrawMoney,
            /// <summary>
            /// 存款
            /// </summary>
            DepositMoney,
            /// <summary>
            /// 修改密码
            /// </summary>
            AlartPwd,
            /// <summary>
            /// 转账
            /// </summary>
            BringForWard,
            /// <summary>
            /// 无卡查询
            /// </summary>
            NoCardQueries,
            /// <summary>
            /// 无卡存款
            /// </summary>
            NoCardDeposit,
            /// <summary>
            /// 吞钞
            /// </summary>
            SwallowingNotes,
            /// <summary>
            /// 吞卡
            /// </summary>
            SwallowingCard,
            /// <summary>
            /// 自定义
            /// </summary>
            Custom
        }
        #endregion

        #region ATM信息查询形式
        /// <summary>
        /// ATM信息查询形式
        /// </summary>
        public enum UseCardSelectWay
        {
            /// <summary>
            /// 不带ATM信息
            /// </summary>
            不带ATM信息,
            /// <summary>
            /// 按交易卡号查询
            /// </summary>
            按交易卡号查询,
            /// <summary>
            /// 按交易类型查询
            /// </summary>
            按交易类型查询,
            /// <summary>
            /// 按交易金额查询
            /// </summary>
            按交易金额查询,
            /// <summary>
            /// 按卡号_交易类型及交易金额的组合查询
            /// </summary>
            按卡号_交易类型及交易金额的组合查询,
            /// <summary>
            /// 按课程名称查找_此时卡号表示课程名称
            /// </summary>
            按课程名称查找_此时卡号表示课程名称

        }
        #endregion

        #region 远程手动启动设备录像类型
        /// <summary>
        /// 远程手动启动设备录像类型
        /// </summary>
        public enum RecordType
        {
            /// <summary>
            /// 手动
            /// </summary>
            Manual = 0,
            /// <summary>
            /// 报警
            /// </summary>
            Alarm = 1,
            /// <summary>
            /// 回传
            /// </summary>
            Return = 2,
            /// <summary>
            /// 信号
            /// </summary>
            Signal = 3,
            /// <summary>
            /// 移动
            /// </summary>
            Move = 4,
            /// <summary>
            /// 遮挡
            /// </summary>
            Occlusion = 5
        }
        #endregion

        //#region 码流类型
        ///// <summary>
        ///// 码流类型
        ///// </summary>
        //public enum StreamType
        //{
        //    /// <summary>
        //    /// 无意义
        //    /// </summary>
        //    Inanition = 0,
        //    /// <summary>
        //    /// 主码流
        //    /// </summary>
        //    MainStream,
        //    /// <summary>
        //    /// 子码流
        //    /// </summary>
        //    SubcodeFlow,
        //    /// <summary>
        //    /// 码流三
        //    /// </summary>
        //    ThreeStream,
        //    /// <summary>
        //    /// 全部
        //    /// </summary>    
        //    All = 0xff
        //}
        //#endregion

        #region 不带卡号查询文件类型
        /// <summary>
        /// 不带卡号查询文件类型
        /// </summary>
        public enum NoCardSelectFileType
        {
            /// <summary>
            /// 全部
            /// </summary>
            全部 = 0xff,
            /// <summary>
            /// 定时录像
            /// </summary>
            定时录像 = 0,
            /// <summary>
            /// 移动侦测
            /// </summary>
            移动侦测,
            /// <summary>
            /// 报警触发
            /// </summary>
            报警触发,
            /// <summary>
            /// 报警触发或移动侦测
            /// </summary>
            报警触发或移动侦测,
            /// <summary>
            /// 报警触发和移动侦测
            /// </summary>
            报警触发和移动侦测,
            /// <summary>
            /// 命令触发
            /// </summary>
            命令触发,
            /// <summary>
            /// 手动录像
            /// </summary>
            手动录像,
            /// <summary>
            /// 智能录像
            /// </summary>
            智能录像,
            /// <summary>
            /// PIR报警
            /// </summary>
            PIR报警,
            /// <summary>
            /// 无线报警
            /// </summary>
            无线报警,
            /// <summary>
            /// 呼救报警
            /// </summary>
            呼救报警,
            /// <summary>
            /// 移动侦测_PIR_无线_呼救等所有报警类型(或)
            /// </summary>
            移动侦测_PIR_无线_呼救等所有报警类型,
            /// <summary>
            /// 智能交通事件
            /// </summary>
            智能交通事件,
            /// <summary>
            /// 越界侦测
            /// </summary>
            越界侦测,
            /// <summary>
            /// 区域入侵侦测
            /// </summary>
            区域入侵侦测,
            /// <summary>
            /// 音频异常侦测
            /// </summary>
            音频异常侦测,
            /// <summary>
            /// 场景变更侦测
            /// </summary>
            场景变更侦测,
            /// <summary>
            /// 智能侦测（越界侦测|区域入侵侦测|进入区域侦测|离开区域侦测|人脸侦测）
            /// </summary>
            智能侦测_越界侦测_区域入侵侦测_进入区域侦测_离开区域侦测_人脸侦测,
            /// <summary>
            /// 人脸侦测
            /// </summary>
            人脸侦测,
            /// <summary>
            /// 信号量
            /// </summary>
            信号量,
            /// <summary>
            /// 回传
            /// </summary>
            回传,
            /// <summary>
            /// 回迁录像
            /// </summary>
            回迁录像,
            /// <summary>
            /// 遮挡
            /// </summary>
            遮挡,
            /// <summary>
            /// 进入区域侦测
            /// </summary>
            进入区域侦测,
            /// <summary>
            /// 离开区域侦测
            /// </summary>
            离开区域侦测,
            /// <summary>
            /// 徘徊侦测
            /// </summary>
            徘徊侦测,
            /// <summary>
            /// 人员聚集侦测
            /// </summary>
            人员聚集侦测,
            /// <summary>
            /// 快速运动侦测
            /// </summary>
            快速运动侦测,
            /// <summary>
            /// 停车侦测
            /// </summary>
            停车侦测,
            /// <summary>
            /// 物品遗留侦测
            /// </summary>
            物品遗留侦测,
            /// <summary>
            /// 物品拿取侦测
            /// </summary>
            物品拿取侦测

        }
        #endregion

        #region 带卡号查询文件类型
        /// <summary>
        /// 带卡号查询文件类型
        /// </summary>
        public enum UseCardSelectFileType
        {
            /// <summary>
            /// 全部
            /// </summary>
            全部 = 0xff,
            /// <summary>
            /// 定时录像
            /// </summary>
            定时录像 = 0,
            /// <summary>
            /// 移动侦测
            /// </summary>
            移动侦测,
            /// <summary>
            /// 接近报警
            /// </summary>
            接近报警,
            /// <summary>
            /// 出钞报警
            /// </summary>
            出钞报警,
            /// <summary>
            /// 进钞报警
            /// </summary>
            进钞报警,
            /// <summary>
            /// 命令触发
            /// </summary>
            命令触发,
            /// <summary>
            /// 手动录像
            /// </summary>
            手动录像,
            /// <summary>
            /// 震动报警
            /// </summary>
            震动报警,
            /// <summary>
            /// 环境报警
            /// </summary>
            环境报警,
            /// <summary>
            /// 智能报警
            /// </summary>
            智能报警,
            /// <summary>
            /// PIR报警
            /// </summary>
            PIR报警,
            /// <summary>
            /// 无线报警
            /// </summary>
            无线报警,
            /// <summary>
            /// 呼救报警
            /// </summary>
            呼救报警,
            /// <summary>
            /// 移动侦测_PIR_无线_呼救等所有报警类型
            /// </summary>
            移动侦测_PIR_无线_呼救等所有报警类型,
            /// <summary>
            /// 智能交通事件
            /// </summary>
            智能交通事件,
            /// <summary>
            /// 越界侦测
            /// </summary>
            越界侦测,
            /// <summary>
            /// 区域入侵侦测
            /// </summary>
            区域入侵侦测,
            /// <summary>
            /// 音频异常侦测
            /// </summary>
            音频异常侦测,
            /// <summary>
            /// 场景变更侦测
            /// </summary>
            场景变更侦测,
            /// <summary>
            /// 智能侦测_越界侦测_区域入侵侦测_进入区域侦测_离开区域侦测_人脸侦测
            /// </summary>
            智能侦测_越界侦测_区域入侵侦测_进入区域侦测_离开区域侦测_人脸侦测,
            /// <summary>
            /// 人脸侦测
            /// </summary>
            人脸侦测,
            /// <summary>
            /// 信号量
            /// </summary>
            信号量,
            /// <summary>
            /// 回传
            /// </summary>
            回传,
            /// <summary>
            /// 回迁录像
            /// </summary>
            回迁录像,
            /// <summary>
            /// 遮挡
            /// </summary>
            遮挡,
            /// <summary>
            /// 进入区域侦测
            /// </summary>
            进入区域侦测,
            /// <summary>
            /// 离开区域侦测
            /// </summary>
            离开区域侦测,
            /// <summary>
            /// 徘徊侦测
            /// </summary>
            徘徊侦测,
            /// <summary>
            /// 人员聚集侦测
            /// </summary>
            人员聚集侦测,
            /// <summary>
            /// 快速运动侦测
            /// </summary>
            快速运动侦测,
            /// <summary>
            /// 停车侦测
            /// </summary>
            停车侦测,
            /// <summary>
            /// 物品遗留侦测
            /// </summary>
            物品遗留侦测,
            /// <summary>
            /// 物品拿取侦测
            /// </summary>
            物品拿取侦测
        }
        #endregion

        #region 文件查询状态
        /// <summary>
        /// 文件查询状态
        /// </summary>
        public enum SelectFileState
        {
            /// <summary>
            /// 获得文件信息
            /// </summary>
            NET_DVR_FILE_SUCCESS = 1000,
            /// <summary>
            /// 没有文件
            /// </summary>
            NET_DVR_FILE_NOFIND = 1001,
            /// <summary>
            /// 正在查找文件
            /// </summary>
            NET_DVR_ISFINDING = 1002,
            /// <summary>
            /// 查找文件时没有更多的文件
            /// </summary>
            NET_DVR_NOMOREFILE = 1003,
            /// <summary>
            /// 查找文件时异常
            /// </summary>
            NET_DVR_FILE_EXCEPTION = 1004
        }
        #endregion

        //public enum FileType
        //{
        //    定时录像=0,
        //    移动侦测,
        //    报警触发,
        //    报警_移动侦测,
        //    报警_移动侦测,
        //    命令触发,
        //    手动录像,
        //    震动报警,
        //    环境报警,
        //    智能报警,
        //    PIR报警,
        //    无线报警,
        //    呼救报警,
        //    移动侦测_PIR_无线_呼救等所有报警类型,
        //    智能交通事件,
        //    越界侦测,
        //    区域入侵,
        //    声音异常,
        //    场景变更侦测
        //}

        #region 取流方式
        /// <summary>
        /// 取流方式
        /// </summary>
        public enum GetStreamType
        {
            /// <summary>
            /// 直接从设备取流，对应联合体中结构NET_DVR_IPCHANINFO
            /// </summary>
            Device,
            /// <summary>
            ///  从流媒体取流，对应联合体中结构NET_DVR_IPSERVER_STREAM
            /// </summary>
            Stream,
            /// <summary>
            /// 通过IPServer获得IP地址后取流，对应联合体中结构NET_DVR_PU_STREAM_CFG
            /// </summary>
            IPServer_Ip,
            /// <summary>
            /// 通过IPServer找到设备，再通过流媒体取设备的流，对应联合体中结构NET_DVR_DDNS_STREAM_CFG
            /// </summary>
            IPServer_Device,
            /// <summary>
            /// 通过流媒体由URL去取流，对应联合体中结构NET_DVR_PU_STREAM_URL
            /// </summary>
            Stream_URL,
            /// <summary>
            /// 通过hiDDNS域名连接设备然后从设备取流，对应联合体中结构NET_DVR_HKDDNS_STREAM
            /// </summary>
            DDNS,
            /// <summary>
            /// 直接从设备取流(扩展)，对应联合体中结构NET_DVR_IPCHANINFO_V40 
            /// </summary>
            Device_Extend

        }
        #endregion

        #region 硬盘状态
        /// <summary>
        /// 硬盘状态
        /// </summary>
        public enum DiskStatic
        {
            /// <summary>
            /// 活动
            /// </summary>
            Activity = 0,
            /// <summary>
            /// 休眠
            /// </summary>
            Dormancy,
            /// <summary>
            /// 异常
            /// </summary>
            Abnormal,
            /// <summary>
            /// 休眠硬盘出错
            /// </summary>
            DormancyError,
            /// <summary>
            /// 未格式化
            /// </summary>
            Unformatted,
            /// <summary>
            /// 未连接状态(网络硬盘)
            /// </summary>
            NotConected,
            /// <summary>
            /// 硬盘正在格式化
            /// </summary>
            IsFormatted
        }
        #endregion

//        /// <summary>
//        /// 获取设备配置命令
//        /// </summary>
//        public enum DvrConfigCmd
//        {

////            NET_DVR_GET_PICCFG_V40 获取图像参数 通道号 NET_DVR_PICCFG_V40 6179 
////NET_DVR_GET_COMPRESSCFG_V30 获取压缩参数 通道号 NET_DVR_COMPRESSIONCFG_V30 1040 
////NET_DVR_GET_RECORDCFG_V40 获取录像计划参数 通道号 NET_DVR_RECORD_V40 1008 
////NET_DVR_GET_JPEG_CAPTURE_CFG 获取设备抓图配置 通道号 NET_DVR_JPEG_CAPTURE_CFG 1280 
////NET_DVR_GET_SCHED_CAPTURECFG 获取抓图计划 通道号 NET_DVR_SCHED_CAPTURECFG 1282 
////NET_DVR_GET_SHOWSTRING_V30 获取叠加字符参数 通道号 NET_DVR_SHOWSTRING_V30 1030 
////NET_DVR_GET_CCDPARAMCFG 获取前端参数 无效 NET_DVR_CAMERAPARAMCFG 1067 
////NET_DVR_GET_CCDPARAMCFG_EX 获取前端参数(扩展) 通道号 NET_DVR_CAMERAPARAMCFG_EX 3368 
////NET_DVR_GET_ISP_CAMERAPARAMCFG 获取ISP前端参数配置 通道号 NET_DVR_ISP_CAMERAPARAMCFG 3255 
////NET_IPC_GET_AUX_ALARMCFG 获取辅助(PIR/无线)报警参数 通道号 NET_IPC_AUX_ALARMCFG 3209 
////NET_DVR_GET_VIDEO_INPUT_EFFECT 获取通道视频输入图像参数 通道号 NET_DVR_VIDEO_INPUT_EFFECT 1286 
////NET_DVR_GET_MOTION_HOLIDAY_HANDLE 获取移动侦测假日报警处理方式 通道号 NET_DVR_HOLIDAY_HANDLE 1242 
////NET_DVR_GET_VILOST_HOLIDAY_HANDLE 获取视频信号丢失假日报警处理方式 通道号 NET_DVR_HOLIDAY_HANDLE 1244 
////NET_DVR_GET_HIDE_HOLIDAY_HANDLE 获取遮盖假日报警处理方式 通道号 NET_DVR_HOLIDAY_HANDLE 1246 
////NET_DVR_GET_HOLIDAY_RECORD 获取假日录像参数 通道号 NET_DVR_HOLIDAY_RECORD 1252 
////NET_DVR_GET_LINK_STATUS 获取通道的工作状态 组号 NET_DVR_LINK_STATUS 1256 
////NET_DVR_GET_RECORD_CHANNEL_INFO 获取通道录像状态信息 组号，从0开始，每组64个通道 NET_DVR_CHAN_GROUP_RECORD_STATUS 6013 
////NET_DVR_GET_WD1_CFG 获取WD1使能开关状态 通道号 NET_DVR_WD1_CFG 6136 
////NET_DVR_GET_STREAM_CABAC 获取码流压缩性能选项 通道号 NET_DVR_STREAM_CABAC 6118 
////NET_DVR_GET_ACCESS_CAMERA_INFO 获取通道对应的前端相机信息 通道号 NET_DVR_ACCESS_CAMERA_INFO 6201 
////NET_DVR_GET_VIDEO_AUDIOIN_CFG 获取视频的音频输入参数 通道号 NET_DVR_VIDEO_AUDIOIN_CFG 9118 
////NET_DVR_GET_AUDIO_INPUT 获取音频输入参数 通道号 NET_DVR_AUDIO_INPUT_PARAM 3201 
////NET_DVR_GET_AUDIOOUT_VOLUME 获取输出音频大小 通道号 NET_DVR_AUDIOOUT_VOLUME 3237 
////NET_DVR_GET_CAMERA_DEHAZE_CFG 获取去雾参数 通道号 NET_DVR_CAMERA_DEHAZE_CFG 3203 
////NET_DVR_GET_LOW_LIGHTCFG 获取快球低照度信息 通道号 NET_DVR_LOW_LIGHT_CFG 3303 
////NET_DVR_GET_FOCUSMODECFG 获取快球聚焦模式信息 通道号 NET_DVR_FOCUSMODE_CFG 3305 
////NET_DVR_GET_INFRARECFG 获取快球红外信息 通道号 NET_DVR_INFRARE_CFG 3307 
////NET_DVR_GET_AEMODECFG 获取快球其他参数信息 通道号 NET_DVR_AEMODECFG 3309 
////NET_DVR_GET_CORRIDOR_MODE 获取旋转功能配置 通道号 NET_DVR_CORRIDOR_MODE 3354 
////NET_DVR_GET_SIGNAL_SYNC 获取信号灯同步配置参数 通道号 NET_DVR_SIGNAL_SYNCCFG 3396 

//        }
        ///// <summary>
        ///// 设置设备配置命令
        ///// </summary>
        //public enum SetConfigCmd
        //{

        //}
    }
}
