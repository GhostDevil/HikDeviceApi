using System;
using System.Runtime.InteropServices;

namespace HikDeviceApi.Door
{
    /// <summary>
    /// 版 本:Release
    /// 日 期:2015-08-15
    /// 作 者:逍遥
    /// 描 述:海康26系列门禁结构体
    /// </summary>
    public class HikDoorStruct
    {
        #region  卡参数配置条件结构体 
        /// <summary>
        /// 卡参数配置条件结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG_COND
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 设置或获取卡数量，获取时置为0xffffffff表示获取所有卡信息 
            /// </summary>
            public uint dwCardNum;
            /// <summary>
            /// 设备是否进行卡号校验：0- 不校验，1- 校验 
            /// </summary>
            public byte byCheckCardNo;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region  获取卡参数的发送数据结构体 
        /// <summary>
        /// 获取卡参数的发送数据结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG_SEND_DATA
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 卡号 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACS_CARD_NO_LEN)]
            public string byCardNo;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region  卡参数配置结构体 
        /// <summary>
        /// 卡参数配置结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 需要修改的卡参数（设置卡参数时有效），按位表示，每位代表一种参数，值：0- 不修改，1- 需要修改
            /// </summary>
            public uint dwModifyParamType;
            /// <summary>
            /// 卡号
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACS_CARD_NO_LEN)]
            public string byCardNo;
            /// <summary>
            /// 卡是否有效：0- 无效，1- 有效（用于删除卡，设置时置为0进行删除，获取时此字段始终为1）
            /// </summary>
            public byte byCardValid;
            /// <summary>
            /// 卡类型：1- 普通卡（默认），2- 残疾人卡，3- 黑名单卡，4- 巡更卡，5- 胁迫卡，6- 超级卡，7- 来宾卡
            /// </summary>
            public byte byCardType;
            /// <summary>
            /// 是否为首卡：1- 是，0- 否 
            /// </summary>
            public byte byLeaderCard;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            public byte byRes1;
            /// <summary>
            /// 门权限，按位表示，从低位到高位表示对门1~N是否有权限，值：0- 无权限，1- 有权限 
            /// </summary>
            public uint dwDoorRight;
            /// <summary>
            /// 有效期参数
            /// </summary>
            public NET_DVR_VALID_PERIOD_CFG struValid;
            /// <summary>
            /// 所属群组，按位表示，从低位到高位表示是否从属群组1~N，值：0- 不属于，1- 属于
            /// </summary>
            public uint dwBelongGroup;
            /// <summary>
            /// 卡密码 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CARD_PASSWORD_LEN)]
            public string byCardPassword;
            /// <summary>
            /// 卡权限计划，取值为计划模板编号，同个门不同计划模板采用权限或的方式处理
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DOOR_NUM * MAX_CARD_RIGHT_PLAN_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardRightPlan;//[MAX_DOOR_NUM][MAX_CARD_RIGHT_PLAN_NUM];
            /// <summary>
            /// 最大刷卡次数，0为无次数限制 
            /// </summary>                             
            public uint dwMaxSwipeTime;
            /// <summary>
            /// 已刷卡次数
            /// </summary>
            public uint dwSwipeTime;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        #endregion

        #region  有效期参数结构体 
        /// <summary>
        /// 有效期参数结构体
        /// </summary>
        public struct NET_DVR_VALID_PERIOD_CFG
        {
            /// <summary>
            /// 是否启用该有效期：0- 不启用，1- 启用 
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 有效期起始时间
            /// </summary>
            public NET_DVR_TIME_EX struBeginTime;
            /// <summary>
            /// 有效期结束时间 
            /// </summary>
            public NET_DVR_TIME_EX struEndTime;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region  时间参数结构体 
        /// <summary>
        /// 时间参数结构体
        /// </summary>
        public struct NET_DVR_TIME_EX
        {
            /// <summary>
            /// 年
            /// </summary>
            public UInt16 wYear;
            /// <summary>
            /// 月
            /// </summary>
            public byte byMonth;
            /// <summary>
            /// 日
            /// </summary>
            public byte byDay;
            /// <summary>
            /// 时
            /// </summary>
            public byte byHour;
            /// <summary>
            /// 分
            /// </summary>
            public byte byMinute;
            /// <summary>
            /// 秒
            /// </summary>
            public byte bySecond;
            /// <summary>
            /// 保留置为0
            /// </summary>
            public byte byRes;
        }

        #endregion

        #region  报警布防参数结构体 
        /// <summary>
        /// 报警布防参数结构体
        /// </summary>
        public struct NET_DVR_SETUPALARM_PARAM
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public UInt32 dwSize;
            /// <summary>
            /// 布防优先级：0- 一等级（高），1- 二等级（中），2- 三等级（低）
            /// </summary>
            public byte byLevel;
            /// <summary>
            /// 智能交通报警信息上传类型：0- 老报警信息（NET_DVR_PLATE_RESULT），1- 新报警信息(NET_ITS_PLATE_RESULT) 
            /// </summary>
            public byte byAlarmInfoType;
            /// <summary>
            /// 0- 移动侦测、视频丢失、遮挡、IO信号量等报警信息以普通方式上传（NET_DVR_ALARMINFO_V30），1- 报警信息以数据可变长方式上传（NET_DVR_ALARMINFO_V40，设备若不支持则仍以普通方式上传） 
            /// </summary>
            public byte byRetAlarmTypeV40;
            /// <summary>
            /// CVR上传报警信息回调结构体版本：0- COMM_ALARM_DEVICE，1- COMM_ALARM_DEVICE_V40 
            /// </summary>
            public byte byRetDevInfoVersion;
            /// <summary>
            /// VQD报警上传类型类型：0-上传VQD诊断信息（NET_DVR_VQD_DIAGNOSE_INFO），1-VQD诊断异常信息（NET_DVR_VQD_ALARM） 
            /// </summary>
            public byte byRetVQDAlarmType;
            /// <summary>
            /// 人脸侦测报警信息类型：1- 表示人脸侦测报警扩展(NET_DVR_FACE_DETECTION)，0- 表示原先支持结构(NET_VCA_FACESNAP_RESULT) 
            /// </summary>
            public byte byFaceAlarmDetection;
            /// <summary>
            /// 按位表示，值：0-上传，1-不上传 bit0- 表示二级布防是否上传图片
            /// </summary>
            public byte bySupport;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            public byte byRes;
            /// <summary>
            /// 任务处理号
            /// </summary>
            public ushort wTaskNo;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        #endregion

        #region  门禁主机报警信息结构体 
        /// <summary>
        /// 门禁主机报警信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_ALARM_INFO
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public int dwSize;
            /// <summary>
            /// 报警主类型
            /// </summary>
            public int dwMajor;
            /// <summary>
            /// 报警次类型，次类型含义根据主类型不同而不同
            /// </summary>
            public int dwMinor;
            /// <summary>
            /// 报警时间 
            /// </summary>
            public NET_DVR_TIME struTime;
            /// <summary>
            /// 网络操作的用户名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;
            /// <summary>
            /// 远程主机地址
            /// </summary>
            public NET_DVR_IPADDR struRemoteHostAddr;
            /// <summary>
            /// 报警信息详细参数
            /// </summary>
            public NET_DVR_ACS_EVENT_INFO struAcsEventInfo;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region  门禁主机报警事件信息 
        /// <summary>
        /// 门禁主机报警信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_EVENT_INFO
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public int dwSize;
            /// <summary>
            /// 卡号
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN)]
            public byte[] byCardNo;
            /// <summary>
            /// 卡类型：1- 普通卡，2- 残疾人卡，3- 黑名单卡，4- 巡更卡，5- 胁迫卡，6- 超级卡，7- 来宾卡，为0表示无效 
            /// </summary>
            public byte byCardType;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Res1;
            /// <summary>
            /// 读卡器编号，为0表示无效 
            /// </summary>
            public int dwCardReaderNo;
            /// <summary>
            /// 门编号，为0表示无效 
            /// </summary>
            public int dwDoorNo;
            /// <summary>
            /// 多重卡认证序号，为0表示无效 
            /// </summary>
            public int dwVerifyNo;
            /// <summary>
            /// 报警输入号，为0表示无效
            /// </summary>
            public int dwAlarmInNo;
            /// <summary>
            /// 报警输出号，为0表示无效 
            /// </summary>
            public int dwAlarmOutNo;
            /// <summary>
            /// 事件报警输入编号
            /// </summary>
            public int dwCaseSensorNo;
            /// <summary>
            /// RS485通道号，为0表示无效
            /// </summary>
            public int dwRs485No;
            /// <summary>
            /// 群组编号
            /// </summary>
            public int dwMultiCardGroupNo;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }
        #endregion

        #region  读卡器参数配置结构体 
        /// <summary>
        /// 读卡器参数配置结构体
        /// </summary>
        public struct NET_DVR_CARD_READER_CFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 是否使能：0- 不启用，1- 启用 
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 读卡器类型：1- DS-K110XM/MK/C/CK，2- DS-K192AM/AMP，3- DS-K192BM/BMP，4- DS-K182AM/AMP，5- DS-K182BM/BMP
            /// </summary>
            public byte byCardReaderType;
            /// <summary>
            /// OK LED极性：0- 阴极，1- 阳极 
            /// </summary>
            public byte byOkLedPolarity;
            /// <summary>
            /// Error LED极性：0- 阴极，1- 阳极
            /// </summary>
            public byte byErrorLedPolarity;
            /// <summary>
            /// 蜂鸣器极性：0- 阴极，1- 阳极
            /// </summary>
            public byte byBuzzerPolarity;
            /// <summary>
            /// 重复刷卡间隔时间，单位：秒 
            /// </summary>
            public byte bySwipeInterval;
            /// <summary>
            /// 按键超时时间，单位：秒，取值范围：1~255 
            /// </summary>
            public byte byPressTimeout;
            /// <summary>
            /// 是否启用读卡失败超次报警：0- 不启用，1- 启用
            /// </summary>
            public byte byEnableFailAlarm;
            /// <summary>
            /// 最大读卡失败次数，取值范围：1~10 
            /// </summary>
            public byte byMaxReadCardFailNum;
            /// <summary>
            /// 是否启用防拆检测：0- 不启用，1- 启用
            /// </summary>
            public byte byEnableTamperCheck;
            /// <summary>
            /// 掉线检测时间，单位：秒，取值范围：0~255 
            /// </summary>
            public byte byOfflineCheckTime;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region  IP设备资源及IP通道资源配置结构体 
        /// <summary>
        /// IP设备资源及IP通道资源配置结构体
        /// </summary>
        public struct NET_DVR_IPPARACFG_V40
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint dwSize;/* 结构大小 */
            /// <summary>
            /// 设备支持的总组数（只读）。若设备支持的组数大于1，NET_DVR_GetDVRConfig（或者NET_DVR_SetDVRConfig）获取（或设置）相关通道参数需要按照组数调用多次命令分别获取（或设置）各组通道参数，此时接口中lChannel对应组号。 
            /// </summary>
            public uint dwGroupNum;
            /// <summary>
            /// 最大模拟通道个数（只读）
            /// </summary>
            public uint dwAChanNum;
            /// <summary>
            /// 数字通道个数（只读） 
            /// </summary>
            public uint dwDChanNum;
            /// <summary>
            /// 起始数字通道（只读） 
            /// </summary>
            public uint dwStartDChan;
            /// <summary>
            /// 模拟通道是否启用，从低到高表示1-32通道，0表示无效 1有效
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable; /* 模拟通道是否启用，从低到高表示1-32通道，0表示无效 1有效 */
            /// <summary>
            /// IP设备
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE_V40, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO_V31[] struIPDevInfo; /* IP设备 */
            /// <summary>
            /// IP通道
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_STREAM_MODE[] struStreamMode;/* IP通道 */
            /// <summary>
            /// 模拟通道是否启用，从低到高表示1-32通道，0表示无效 1有效
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2; /* 模拟通道是否启用，从低到高表示1-32通道，0表示无效 1有效 */
        }
        #endregion

        #region  设备参数结构体V40 
        /// <summary>
        /// 设备参数结构体V40
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICECFG_V40
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 设备名称 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDVRName;
            /// <summary>
            /// 设备ID号，用于遥控器，v1.4的设备号范围为(0~99), v1.5及以上版本的设备号为(1~255) 
            /// </summary>
            public uint dwDVRID;
            /// <summary>
            /// 是否循环录像：0－不是；1－是 
            /// </summary>
            public uint dwRecycleRecord;
            /// <summary>
            /// （只读，不可修改）设备序列号
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;
            /// <summary>
            /// （只读，不可修改）软件版本号，V3.0以上版本支持的设备最高8位为主版本号，次高8位为次版本号，低16位为修复版本号；V3.0以下版本支持的设备高16位表示主版本，低16位表示次版本 
            /// </summary>
            public uint dwSoftwareVersion;	
            /// <summary>
            /// （只读，不可修改）软件生成日期，0xYYYYMMDD
            /// </summary>
            public uint dwSoftwareBuildDate;
            /// <summary>
            /// （只读，不可修改）DSP软件版本，高16位是主版本，低16位是次版本
            /// </summary>
            public uint dwDSPSoftwareVersion;
            /// <summary>
            /// （只读，不可修改）DSP软件生成日期，0xYYYYMMDD
            /// </summary>
            public uint dwDSPSoftwareBuildDate;	
            /// <summary>
            /// （只读，不可修改）前面板版本，高16位是主版本，低16位是次版本 
            /// </summary>
            public uint dwPanelVersion;	
            /// <summary>
            /// （只读，不可修改）硬件版本，高16位是主版本，低16位是次版本 
            /// </summary>
            public uint dwHardwareVersion;
            /// <summary>
            /// （只读，不可修改）设备模拟报警输入个数
            /// </summary>
            public byte byAlarmInPortNum;
            /// <summary>
            /// （只读，不可修改）设备模拟报警输出个数
            /// </summary>
            public byte byAlarmOutPortNum;
            /// <summary>
            /// （只读，不可修改）设备232串口个数
            /// </summary>
            public byte byRS232Num;
            /// <summary>
            /// （只读，不可修改）设备485串口个数 
            /// </summary>
            public byte byRS485Num;
            /// <summary>
            /// （只读，不可修改）网络口个数 
            /// </summary>
            public byte byNetworkPortNum;
            /// <summary>
            /// （只读，不可修改）硬盘控制器个数
            /// </summary>
            public byte byDiskCtrlNum;
            /// <summary>
            /// （只读，不可修改）硬盘个数
            /// </summary>
            public byte byDiskNum;
            /// <summary>
            /// （只读，不可修改）设备类型  1:DVR 2:ATM DVR 3:DVS ......
            /// </summary>
            public byte byDVRType;
            /// <summary>
            /// （只读，不可修改）设备模拟通道个数
            /// </summary>
            public byte byChanNum;
            /// <summary>
            /// （只读，不可修改）模拟通道的起始通道号
            /// </summary>
            public byte byStartChan;
            /// <summary>
            /// （只读，不可修改）设备解码路数 
            /// </summary>
            public byte byDecordChans;
            /// <summary>
            /// （只读，不可修改）VGA口的个数
            /// </summary>
            public byte byVGANum;
            /// <summary>
            /// （只读，不可修改）USB口的个数 
            /// </summary>
            public byte byUSBNum;
            /// <summary>
            /// （只读，不可修改）辅口的个数
            /// </summary>
            public byte byAuxoutNum;
            /// <summary>
            /// （只读，不可修改）语音口的个数 
            /// </summary>
            public byte byAudioNum;
            /// <summary>
            /// （只读，不可修改）最大数字通道低8位，高8位见byHighIPChanNum 
            /// </summary>
            public byte byIPChanNum; 
            /// <summary>
            ///（只读，不可修改）零通道编码个数
            /// </summary>
            public byte byZeroChanNum;
            /// <summary>
            /// （只读，不可修改）能力，位与结果为0表示不支持，1表示支持
            /// </summary>
            public byte bySupport;
            ///// bySupport & 0x1, 表示是否支持智能搜索
            /////bySupport & 0x2, 表示是否支持备份
            /////bySupport & 0x4, 表示是否支持压缩参数能力获取
            /////bySupport & 0x8, 表示是否支持多网卡
            /////bySupport & 0x10, 表示支持远程SADP
            /////bySupport & 0x20, 表示支持Raid卡功能
            /////bySupport & 0x40, 表示支持IPSAN搜索
            /////bySupport & 0x80, 表示支持rtp over rtsp
            /// <summary>
            /// Esata的默认用途，0-默认备份，1-默认录像
            /// </summary>
            public byte byEsataUseage;		//Esata的默认用途，0-默认备份，1-默认录像
            /// <summary>
            /// 0-关闭即插即用，1-打开即插即用
            /// </summary>
            public byte byIPCPlug;			//0-关闭即插即用，1-打开即插即用
            /// <summary>
            /// 存储模式：0-盘组模式，1-磁盘配额，2-抽帧模式
            /// </summary>
            public byte byStorageMode;      //0-盘组模式,1-磁盘配额, 2抽帧模式
            /// <summary>
            /// （只读，不可修改）能力集扩充，位与结果：0表示不支持，1表示支持
            /// </summary>
            public byte bySupport1;            ///// bySupport1 & 0x1, 表示是否支持snmp v30
                                               /////bySupport1 & 0x2, 支持区分回放和下载
                                               /////bySupport1 & 0x4, 是否支持布防优先级	
                                               /////bySupport1 & 0x8, 智能设备是否支持布防时间段扩展
                                               /////bySupport1 & 0x10, 表示是否支持多磁盘数（超过33个）
                                               /////bySupport1 & 0x20, 表示是否支持rtsp over http
            /// <summary>
            /// （只读，不可修改）设备型号
            /// </summary>
            public ushort wDevType;
            /// <summary>
            /// （只读，不可修改）设备型号名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = DEV_TYPE_NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDevTypeName;
            /// <summary>
            /// （只读，不可修改）能力集扩充，位与结果：0表示不支持，1表示支持
            /// </summary>
            public byte bySupport2;  // bySupport2 & 0x1, 表示是否支持是否支持扩展的OSD字符叠加(终端和抓拍机扩展区分)
            /// <summary>
            /// （只读，不可修改）模拟报警输入个数 
            /// </summary>          
            public byte byAnalogAlarmInPortNum;
            /// <summary>
            /// （只读，不可修改）模拟报警输入起始号
            /// </summary>
            public byte byStartAlarmInNo;
            /// <summary>
            /// （只读，不可修改）模拟报警输出起始号
            /// </summary>
            public byte byStartAlarmOutNo;
            /// <summary>
            /// （只读，不可修改）IP报警输入起始号，0表示参数无效
            /// </summary>
            public byte byStartIPAlarmInNo;
            /// <summary>
            /// （只读，不可修改）IP报警输出起始号，0表示参数无效
            /// </summary>
            public byte byStartIPAlarmOutNo;
            /// <summary>
            /// （只读，不可修改）最大数字通道高8位，低8位见byIPChanNum 
            /// </summary>
            public byte byHighIPChanNum;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

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
        }
        #endregion 

        #region  取流方式配置结构体 
        /// <summary>
        /// 取流方式配置结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_MODE
        {
            /// <summary>
            /// 取流方式：
            ///0- 直接从设备取流，对应联合体中结构NET_DVR_IPCHANINFO；
            ///1- 从流媒体取流，对应联合体中结构NET_DVR_IPSERVER_STREAM；
            ///2- 通过IPServer获得IP地址后取流，对应联合体中结构NET_DVR_PU_STREAM_CFG；
            ///3- 通过IPServer找到设备，再通过流媒体取设备的流，对应联合体中结构NET_DVR_DDNS_STREAM_CFG；
            ///4- 通过流媒体由URL去取流，对应联合体中结构NET_DVR_PU_STREAM_URL；
            ///5- 通过hiDDNS域名连接设备然后从设备取流，对应联合体中结构NET_DVR_HKDDNS_STREAM；
            ///6- 直接从设备取流(扩展)，对应联合体中结构NET_DVR_IPCHANINFO_V40
            /// </summary>
            public byte byGetStreamType;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 不同取流方式联合体 
            /// </summary>
            public NET_DVR_GET_STREAM_UNION uGetStream;
            /// <summary>
            /// 初始化参数
            /// </summary>
            public void Init()
            {
                byGetStreamType = 0;
                byRes = new byte[3];
                //uGetStream.Init();
            }
        }
        #endregion

        #region  取流方式联合体 
        /// <summary>
        /// 取流方式联合体
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct NET_DVR_GET_STREAM_UNION
        {
            [FieldOffset(0)]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 492, ArraySubType = UnmanagedType.I1)]
            public byte[] byUnion;
            public void Init()
            {
                byUnion = new byte[492];
            }
        }
        #endregion

        #region  设备ip地址 
        /// <summary>
        /// 设备ip地址
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_IPADDR
        {

            /// ipv4地址
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpV4;

            /// BYTE[128]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[128];
            }
        }
        #endregion

        #region  设备时间结构体 
        /// <summary>
        /// 设备时间结构体
        /// </summary>
        public struct NET_DVR_TIME
        {
            public uint dwYear;
            public uint dwMonth;
            public uint dwDay;
            public uint dwHour;
            public uint dwMinute;
            public uint dwSecond;

        }
        #endregion

        #region  门参数配置结构体 
        /// <summary>
        /// 门参数配置结构体
        /// </summary>
        public struct NET_DVR_DOOR_CFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 门名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = DOOR_NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDoorName;
            /// <summary>
            /// 门磁类型：0- 常闭，1- 常开
            /// </summary>
            public byte byMagneticType;
            /// <summary>
            /// 开门按钮类型：0- 常闭，1- 常开
            /// </summary>
            public byte byOpenButtonType;
            /// <summary>
            /// 开门持续时间，取值范围：1~255s 
            /// </summary>
            public byte byOpenDuration;
            /// <summary>
            /// 残疾人卡开门持续时间，取值范围：1~255s 
            /// </summary>
            public byte byDisabledOpenDuration;
            /// <summary>
            /// 门磁检测超时报警时间，取值范围：0~255s，0表示不报警
            /// </summary>
            public byte byMagneticAlarmTimeout;
            /// <summary>
            /// 是否启用闭门回锁：0- 否，1- 是
            /// </summary>
            public byte byEnableDoorLock;
            /// <summary>
            /// 是否启用首卡常开功能：0- 否，1- 是 
            /// </summary>
            public byte byEnableLeaderCard;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            public byte byRes1;
            /// <summary>
            /// 首卡常开持续时间，取值范围：1~1440，单位：min（分钟）
            /// </summary>
            public uint dwLeaderCardOpenDuration;
            /// <summary>
            ///  胁迫密码
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = STRESS_PASSWORD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byStressPassword;
            /// <summary>
            /// 超级密码
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SUPER_PASSWORD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] bySuperPassword;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

        }
        #endregion

        #region  门禁主机工作状态结构体   
        /// <summary>
        /// 门禁主机工作状态结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_WORK_STATUS
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 门锁状态：0- 关，1- 开 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DOOR_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byDoorLockStatus;
            /// <summary>
            /// 门状态：1-休眠状态，2- 常开状态，3- 常闭状态，4- 普通状态
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DOOR_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byDoorStatus;
            /// <summary>
            /// 门磁状态：0- 闭合，1- 开启 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DOOR_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byMagneticStatus;
            /// <summary>
            /// 事件报警输入状态：0- 无输入，1- 有输入
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASE_SENSOR_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCaseStatus;
            /// <summary>
            /// 蓄电池电压值，实际值乘10，单位：伏特 
            /// </summary>
            public ushort wBatteryVoltage;
            /// <summary>
            /// 蓄电池是否处于低压状态：0- 否，1- 是 
            /// </summary>
            public byte byBatteryLowVoltage;
            /// <summary>
            /// 设备供电状态：1- 交流电供电，2- 蓄电池供电 
            /// </summary>
            public byte byPowerSupplyStatus;
            /// <summary>
            /// 多门互锁状态：0- 关闭，1- 开启 
            /// </summary>
            public byte byMultiDoorInterlockStatus;
            /// <summary>
            /// 反潜回状态：0-关闭，1-开启 
            /// </summary>
            public byte byAntiSneakStatus;
            /// <summary>
            /// 主机防拆状态：0- 关闭，1- 开启
            /// </summary>
            public byte byHostAntiDismantleStatus;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            public byte byRes1;
            /// <summary>
            /// 读卡器在线状态：0- 不在线，1- 在线 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CARD_READER_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardReaderOnlineStatus;
            /// <summary>
            /// 读卡器防拆状态：0- 关闭，1- 开启
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CARD_READER_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardReaderAntiDismantleStatus;
            /// <summary>
            /// 读卡器当前验证方式：1- 休眠，2- 刷卡+密码，3- 刷卡
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CARD_READER_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardReaderVerifyMode;
            /// <summary>
            /// 报警输入口布防状态，0-对应报警输入口处于撤防状态，1-0-对应报警输入口处于布防状态
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ALARMHOST_ALARMIN_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetupAlarmStatus;
            /// <summary>
            /// 报警输入口报警状态：0- 对应报警输入口当前无报警，1- 对应报警输入口当前有报警
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ALARMHOST_ALARMIN_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatus;
            /// <summary>
            /// 报警输出口状态：0- 对应报警输出口无报警，1- 对应报警输出口有报警
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ALARMHOST_ALARMOUT_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutStatus;
            /// <summary>
            /// 已添加的卡数量
            /// </summary>
            public uint dwCardNum;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

        }
        #endregion

        #region  门状态计划配置结构体 
        /// <summary>
        /// 门状态计划配置结构体
        /// </summary>
        public struct NET_DVR_DOOR_STATUS_PLAN
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 计划模板编号，为0表示取消关联、恢复默认状态（普通状态） 
            /// </summary>
            public uint dwTemplateNo;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }
        #endregion

        #region  读卡器验证计划配置结构体 
        /// <summary>
        /// 读卡器验证计划配置结构体
        /// </summary>
        public struct NET_DVR_CARD_READER_PLAN
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 计划模板编号，为0表示取消关联、恢复默认状态（刷卡开门） 
            /// </summary>
            public uint dwTemplateNo;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }
        #endregion

        #region  设备参数结构体 
        /// <summary>
        /// 设备参数结构体V40
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V40
        {
            /// <summary>
            /// 设备参数
            /// </summary>
            public NET_DVR_DEVICEINFO_V30 struDeviceV30;
            /// <summary>
            /// 设备是否支持锁定功能，bySupportLock为1时，dwSurplusLockTime和byRetryLoginTime有效 
            /// </summary>
            public byte bySupportLock;
            /// <summary>
            /// 剩余可尝试登陆的次数，用户名、密码错误时，此参数有效 
            /// </summary>
            public byte byRetryLoginTime;
            /// <summary>
            /// 密码安全等级：0- 无效，1- 默认密码，2- 有效密码，3- 风险较高的密码，当管理员用户的密码为出厂默认密码（12345）或者风险较高的密码时，建议上层客户端提示用户更改密码
            /// </summary>
            public byte byPasswordLevel;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 剩余时间，单位：秒，用户锁定时此参数有效
            /// </summary>
            public UInt32 dwSurplusLockTime;
            /// <summary>
            /// 保留，置为0 [256]
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

        }
        #endregion

        #region  用户登录参数结构体 
        /// <summary>
        /// 用户登录参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_USER_LOGIN_INFO
        {
            /// <summary>
            /// 设备地址，IP 或者普通域名 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE_V40)]
            public byte[] sDeviceAddress;
            /// <summary>
            /// 保留，设为 0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 设备端口号，例如：8000 
            /// </summary>
            public uint wPort;

            /// <summary>
            /// 登录用户名，例如：admin 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN)]
            public byte[] sUserName;
            /// <summary>
            /// 登录密码，例如：12345 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN)]
            public byte[] sPassword;
            /// <summary>
            /// 登录状态回调函数，bUseAsynLogin 为0时有效 
            /// </summary>
            public HikDeviceApi.Door.HikDoorDelegate.fLoginResultCallBack cbLoginResult;
            /// <summary>
            /// 用户数据 
            /// </summary>
            public IntPtr pUser;
            /// <summary>
            /// 是否异步登录：0- 否，1- 是 
            /// </summary>
            public bool bUseAsynLogin;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

        }
        #endregion

        #region  设备参数结构体V30 For V30Login 
        /// <summary>
        /// 设备参数结构体V30 For V30Login
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V30
        {
            /// <summary>
            /// 序列号
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;  //序列号
            /// <summary>
            /// 报警输入个数
            /// </summary>
            public byte byAlarmInPortNum;		        //报警输入个数
            /// <summary>
            /// 报警输出个数
            /// </summary>
            public byte byAlarmOutPortNum;		        //报警输出个数
            /// <summary>
            /// 硬盘个数
            /// </summary>
            public byte byDiskNum;				    //硬盘个数
            /// <summary>
            /// 设备类型, 1:DVR 2:ATM DVR 3:DVS ......
            /// </summary>
            public byte byDVRType;				    //设备类型, 1:DVR 2:ATM DVR 3:DVS ......
            /// <summary>
            /// 模拟通道个数
            /// </summary>
            public byte byChanNum;				    //模拟通道个数
            /// <summary>
            /// 起始通道号,例如DVS-1,DVR - 1
            /// </summary>
            public byte byStartChan;			        //起始通道号,例如DVS-1,DVR - 1
            /// <summary>
            /// 语音通道数
            /// </summary>
            public byte byAudioChanNum;                //语音通道数
            /// <summary>
            /// 最大数字通道个数，低位
            /// </summary>
            public byte byIPChanNum;					//最大数字通道个数，低位  
            /// <summary>
            /// 零通道编码个数 //2010-01-16
            /// </summary>
            public byte byZeroChanNum;			//零通道编码个数 //2010-01-16
            /// <summary>
            /// 主码流传输协议类型 0-private, 1-rtsp,2-同时支持private和rtsp
            /// </summary>
            public byte byMainProto;			//主码流传输协议类型 0-private, 1-rtsp,2-同时支持private和rtsp
            /// <summary>
            /// 子码流传输协议类型0-private, 1-rtsp,2-同时支持private和rtsp
            /// </summary>
            public byte bySubProto;				//子码流传输协议类型0-private, 1-rtsp,2-同时支持private和rtsp
            /// <summary>
            /// 能力，位与结果为0表示不支持，1表示支持
            /// </summary>
            public byte bySupport;        //能力，位与结果为0表示不支持，1表示支持，
                                          //bySupport & 0x1, 表示是否支持智能搜索
                                          //bySupport & 0x2, 表示是否支持备份
                                          //bySupport & 0x4, 表示是否支持压缩参数能力获取
                                          //bySupport & 0x8, 表示是否支持多网卡
                                          //bySupport & 0x10, 表示支持远程SADP
                                          //bySupport & 0x20, 表示支持Raid卡功能
                                          //bySupport & 0x40, 表示支持IPSAN 目录查找
                                          //bySupport & 0x80, 表示支持rtp over rtsp
                                          /// <summary>
                                          /// 能力集扩充，位与结果为0表示不支持，1表示支持
                                          /// </summary>
            public byte bySupport1;        // 能力集扩充，位与结果为0表示不支持，1表示支持
                                           //bySupport1 & 0x1, 表示是否支持snmp v30
                                           //bySupport1 & 0x2, 支持区分回放和下载
                                           //bySupport1 & 0x4, 是否支持布防优先级	
                                           //bySupport1 & 0x8, 智能设备是否支持布防时间段扩展
                                           //bySupport1 & 0x10, 表示是否支持多磁盘数（超过33个）
                                           //bySupport1 & 0x20, 表示是否支持rtsp over http	
                                           //bySupport1 & 0x80, 表示是否支持车牌新报警信息2012-9-28, 且还表示是否支持NET_DVR_IPPARACFG_V40结构体
                                           /// <summary>
                                           /// 能力，位与结果为0表示不支持，非0表示支持
                                           /// </summary>
            public byte bySupport2; /*能力，位与结果为0表示不支持，非0表示支持							
							bySupport2 & 0x1, 表示解码器是否支持通过URL取流解码
							bySupport2 & 0x2,  表示支持FTPV40
							bySupport2 & 0x4,  表示支持ANR
							bySupport2 & 0x8,  表示支持CCD的通道参数配置
							bySupport2 & 0x10,  表示支持布防报警回传信息（仅支持抓拍机报警 新老报警结构）
							bySupport2 & 0x20,  表示是否支持单独获取设备状态子项
							bySupport2 & 0x40,  表示是否是码流加密设备*/
            /// <summary>
            /// 设备型号
            /// </summary>
            public ushort wDevType;              //设备型号
            /// <summary>
            /// 能力集扩展，位与结果为0表示不支持，1表示支持
            /// </summary>
            public byte bySupport3; //能力集扩展，位与结果为0表示不支持，1表示支持
                                    //bySupport3 & 0x1, 表示是否多码流
                                    // bySupport3 & 0x4 表示支持按组配置， 具体包含 通道图像参数、报警输入参数、IP报警输入、输出接入参数、
                                    // 用户参数、设备工作状态、JPEG抓图、定时和时间抓图、硬盘盘组管理 
                                    //bySupport3 & 0x8为1 表示支持使用TCP预览、UDP预览、多播预览中的"延时预览"字段来请求延时预览（后续都将使用这种方式请求延时预览）。而当bySupport3 & 0x8为0时，将使用 "私有延时预览"协议。
                                    //bySupport3 & 0x10 表示支持"获取报警主机主要状态（V40）"。
                                    //bySupport3 & 0x20 表示是否支持通过DDNS域名解析取流
                                    /// <summary>
                                    /// 是否支持多码流,按位表示,0-不支持,1-支持,bit1-码流3,bit2-码流4,bit7-主码流，bit-8子码流
                                    /// </summary>
            public byte byMultiStreamProto;//是否支持多码流,按位表示,0-不支持,1-支持,bit1-码流3,bit2-码流4,bit7-主码流，bit-8子码流
            /// <summary>
            /// 起始数字通道号,0表示无效
            /// </summary>
            public byte byStartDChan;		//起始数字通道号,0表示无效
            /// <summary>
            /// 起始数字对讲通道号，区别于模拟对讲通道号，0表示无效
            /// </summary>
            public byte byStartDTalkChan;	//起始数字对讲通道号，区别于模拟对讲通道号，0表示无效
            /// <summary>
            /// 数字通道个数，高8位 
            /// </summary>
            public byte byHighDChanNum;		//数字通道个数，高位
            /// <summary>
            /// 能力集扩展，按位表示，位与结果：0- 不支持，1- 支持
            /// </summary>
            public byte bySupport4;
            /// <summary>
            /// 支持语种能力,按位表示,每一位0-不支持,1-支持  
            /// </summary>
            public byte byLanguageType;// 支持语种能力,按位表示,每一位0-不支持,1-支持  
                                       //  byLanguageType 等于0 表示 老设备
                                       //  byLanguageType & 0x1表示支持中文
                                       //  byLanguageType & 0x2表示支持英文
                                       /// <summary>
                                       /// 保留
                                       /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;		//保留
        }
        #endregion

        #region  报警设备信息结构 

        /// <summary>
        /// 报警设备信息结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_ALARMER
        {
            /// <summary>
            /// userid是否有效：0－无效；1－有效 
            /// </summary>
            public byte byUserIDValid;/* userid是否有效 0-无效，1-有效 */
            /// <summary>
            /// 序列号是否有效 0-无效，1-有效
            /// </summary>
            public byte bySerialValid;/* 序列号是否有效 0-无效，1-有效 */
            /// <summary>
            /// 版本号是否有效 0-无效，1-有效
            /// </summary>
            public byte byVersionValid;/* 版本号是否有效 0-无效，1-有效 */
            /// <summary>
            /// 设备名字是否有效 0-无效，1-有效
            /// </summary>
            public byte byDeviceNameValid;/* 设备名字是否有效 0-无效，1-有效 */
            /// <summary>
            ///  MAC地址是否有效 0-无效，1-有效
            /// </summary>
            public byte byMacAddrValid; /* MAC地址是否有效 0-无效，1-有效 */
            /// <summary>
            /// login端口是否有效 0-无效，1-有效
            /// </summary>
            public byte byLinkPortValid;/* login端口是否有效 0-无效，1-有效 */
            /// <summary>
            /// 设备IP是否有效 0-无效，1-有效
            /// </summary>
            public byte byDeviceIPValid;/* 设备IP是否有效 0-无效，1-有效 */
            /// <summary>
            /// socket ip是否有效 0-无效，1-有效
            /// </summary>
            public byte bySocketIPValid;/* socket ip是否有效 0-无效，1-有效 */
            /// <summary>
            /// NET_DVR_Login()返回值, 布防时有效
            /// </summary>
            public int lUserID; /* NET_DVR_Login()返回值, 布防时有效 */
            /// <summary>
            /// 序列号
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;/* 序列号 */
            /// <summary>
            /// 版本信息 高16位表示主版本，低16位表示次版本
            /// </summary>
            public uint dwDeviceVersion;/* 版本信息 高16位表示主版本，低16位表示次版本*/
            /// <summary>
            /// 设备名字
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sDeviceName;/* 设备名字 */
            /// <summary>
            /// MAC地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;/* MAC地址 */
            /// <summary>
            /// 设备通讯端口
            /// </summary>
            public ushort wLinkPort; /* link port */
            /// <summary>
            /// IP地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sDeviceIP;/* IP地址 */
            /// <summary>
            /// 报警主动上传时的socket IP地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sSocketIP;/* 报警主动上传时的socket IP地址 */
            /// <summary>
            /// Ip协议 0-IPV4, 1-IPV6
            /// </summary>
            public byte byIpProtocol; /* Ip协议 0-IPV4, 1-IPV6 */
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region  SDK类型 
        //SDK类型
        /// <summary>
        /// 播放库
        /// </summary>
        public const int SDK_PLAYMPEG4 = 1;//播放库
        /// <summary>
        /// 网络库
        /// </summary>
        public const int SDK_HCNETSDK = 2;//网络库
        /// <summary>
        /// 用户名长度
        /// </summary>
        public const int NAME_LEN = 32;//用户名长度
        /// <summary>
        /// 密码长度
        /// </summary>
        public const int PASSWD_LEN = 16;//密码长度
        /// <summary>
        /// GUID长度
        /// </summary>
        public const int GUID_LEN = 16;      //GUID长度
        /// <summary>
        /// 设备类型名称长度
        /// </summary>
        public const int DEV_TYPE_NAME_LEN = 24;      //设备类型名称长度
        /// <summary>
        /// DVR本地登陆名
        /// </summary>
        public const int MAX_NAMELEN = 16;//DVR本地登陆名
        /// <summary>
        /// 设备支持的权限（1-12表示本地权限，13-32表示远程权限）
        /// </summary>
        public const int MAX_RIGHT = 32;//设备支持的权限（1-12表示本地权限，13-32表示远程权限）
        /// <summary>
        /// 序列号长度
        /// </summary>
        public const int SERIALNO_LEN = 48;//序列号长度
        /// <summary>
        /// mac地址长度
        /// </summary>
        public const int MACADDR_LEN = 6;//mac地址长度
        /// <summary>
        /// 设备可配以太网络
        /// </summary>
        public const int MAX_ETHERNET = 2;//设备可配以太网络
        /// <summary>
        /// 设备可配最大网卡数目
        /// </summary>
        public const int MAX_NETWORK_CARD = 4; //设备可配最大网卡数目
        /// <summary>
        /// 路径长度
        /// </summary>
        public const int PATHNAME_LEN = 128;//路径长度
        /// <summary>
        /// 号码最大长度
        /// </summary>
        public const int MAX_NUMBER_LEN = 32;	//号码最大长度
        /// <summary>
        /// 设备名称最大长度
        /// </summary>
        public const int MAX_NAME_LEN = 128; //设备名称最大长度
        /// <summary>
        /// 9000设备最大时间段数
        /// </summary>
        public const int MAX_TIMESEGMENT_V30 = 8;//9000设备最大时间段数
        /// <summary>
        /// 8000设备最大时间段数
        /// </summary>
        public const int MAX_TIMESEGMENT = 4;//8000设备最大时间段数
        /// <summary>
        /// 抓拍机红外滤光片预置点数
        /// </summary>
        public const int MAX_ICR_NUM = 8;   //抓拍机红外滤光片预置点数
        /// <summary>
        /// 允许接入的最大IP设备数
        /// </summary>
        public const int MAX_IP_DEVICE_V40 = 64;//允许接入的最大IP设备数
        /// <summary>
        /// 最大32个模拟通道
        /// </summary>
        public const int MAX_ANALOG_CHANNUM = 32;//最大32个模拟通道
        /// <summary>
        /// 最大32路模拟报警输出
        /// </summary>
        public const int MAX_ANALOG_ALARMOUT = 32; //最大32路模拟报警输出 
        /// <summary>
        /// 最大32路模拟报警输入
        /// </summary>
        public const int MAX_ANALOG_ALARMIN = 32;//最大32路模拟报警输入
        /// <summary>
        /// 允许加入的最多IP通道数
        /// </summary>
        public const int MAX_IP_CHANNEL = 32;//允许加入的最多IP通道数
        /// <summary>
        /// 允许加入的最多报警输入数
        /// </summary>
        public const int MAX_IP_ALARMIN = 128;//允许加入的最多报警输入数
        /// <summary>
        /// 允许加入的最多报警输出数
        /// </summary>
        public const int MAX_IP_ALARMOUT = 64;//允许加入的最多报警输出数
        /// <summary>
        /// 允许加入的最多报警输入数
        /// </summary>
        public const int MAX_IP_ALARMIN_V40 = 4096;    //允许加入的最多报警输入数
        /// <summary>
        /// 允许加入的最多报警输出数
        /// </summary>
        public const int MAX_IP_ALARMOUT_V40 = 4096;    //允许加入的最多报警输出数
        /// <summary>
        /// 最大域名长度
        /// </summary>
        public const int MAX_DOMAIN_NAME = 64;  /* 最大域名长度 */
        /// <summary>
        /// 卡密码长度
        /// </summary>
        public const int CARD_PASSWORD_LEN = 8;//卡密码长度

        public const int MAX_CARD_RIGHT_PLAN_NUM = 4;
        public const int MAX_CHANNUM_V30 = MAX_ANALOG_CHANNUM + MAX_IP_CHANNEL;//64
        public const int MAX_ALARMOUT_V30 = MAX_ANALOG_ALARMOUT + MAX_IP_ALARMOUT;//96
        public const int MAX_ALARMIN_V30 = MAX_ANALOG_ALARMIN + MAX_IP_ALARMIN;//160
        public const int ACS_CARD_NO_LEN = 32;
        public const int MAX_CASE_SENSOR_NUM = 8;
        public const int MAX_DOOR_NUM = 32;
        public const int MAX_CARD_READER_NUM = 64;
        public const int MAX_ALARMHOST_ALARMIN_NUM = 512;
        public const int MAX_ALARMHOST_ALARMOUT_NUM = 512;
        public const int DOOR_NAME_LEN = 32;
        public const int SUPER_PASSWORD_LEN = 8;
        public const int STRESS_PASSWORD_LEN = 8;
        #endregion
    }
}
