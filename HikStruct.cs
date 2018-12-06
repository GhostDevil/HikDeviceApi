using System;
using System.Runtime.InteropServices;
using static HikDeviceApi.HikDelegate;
using static HikDeviceApi.HikConst;
using static HikDeviceApi.HikEnum;

namespace HikDeviceApi
{
    /// <summary>
    /// 日 期:2017-11-25
    /// 作 者:痞子少爷
    /// 描 述:海康设备接口结构
    /// </summary>
    public class HikStruct
    {

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
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_IP_DEVICE_V40)]
            public string sDeviceAddress;
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
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sUserName;
            /// <summary>
            /// 登录密码，例如：12345 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPassword;
            /// <summary>
            /// 登录状态回调函数，bUseAsynLogin 为0时有效 
            /// </summary>
            public fLoginResultCallBack cbLoginResult;
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
            public byte[] sSerialNumber; 
            /// <summary>
            /// 报警输入个数
            /// </summary>
            public byte byAlarmInPortNum;	
            /// <summary>
            /// 报警输出个数
            /// </summary>
            public byte byAlarmOutPortNum;
            /// <summary>
            /// 硬盘个数
            /// </summary>
            public byte byDiskNum;	
            /// <summary>
            /// 设备类型, 1:DVR 2:ATM DVR 3:DVS ......
            /// </summary>
            public byte byDVRType;
            /// <summary>
            /// 设备模拟通道个数，数字（IP）通道最大个数为byIPChanNum + byHighDChanNum*256
            /// </summary>
            public byte byChanNum;
            /// <summary>
            /// 模拟通道的起始通道号，从1开始。,例如DVS-1,DVR - 1
            /// </summary>
            public byte byStartChan;
            /// <summary>
            /// 设备语音对讲通道数
            /// </summary>
            public byte byAudioChanNum;
            /// <summary>
            /// 设备最大数字通道个数，低8位，高8位见byHighDChanNum。可以根据IP通道个数来判断是否调用NET_DVR_GetDVRConfig（配置命令NET_DVR_GET_IPPARACFG_V40）获取模拟和数字通道相关参数（NET_DVR_IPPARACFG_V40）。
            /// </summary>
            public byte byIPChanNum;			
            /// <summary>
            /// 零通道编码个数 //2010-01-16
            /// </summary>
            public byte byZeroChanNum;		
            /// <summary>
            /// 主码流传输协议类型 0-private, 1-rtsp,2-同时支持private和rtsp
            /// </summary>
            public byte byMainProto;		
            /// <summary>
            /// 子码流传输协议类型0-private, 1-rtsp,2-同时支持private和rtsp
            /// </summary>
            public byte bySubProto;			
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
            public ushort wDevType;           
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
            public byte byMultiStreamProto;
            /// <summary>
            /// 起始数字通道号,0表示无效
            /// </summary>
            public byte byStartDChan;	
            /// <summary>
            /// 起始数字对讲通道号，区别于模拟对讲通道号，0表示无效
            /// </summary>
            public byte byStartDTalkChan;
            /// <summary>
            /// 数字通道个数，高8位 
            /// </summary>
            public byte byHighDChanNum;	
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

            ///// <summary>
            ///// 音频输入通道数
            ///// </summary>
            //public byte byVoiceInChanNum;
            ///// <summary>
            ///// 音频输入起始通道号，0表示无效
            ///// </summary>
            //public byte byStartVoiceInChanNo;
            ///// <summary>
            ///// 保留，置为0 
            ///// </summary>
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            //public byte[] byRes3;
            ///// <summary>
            ///// 镜像通道个数，录播主机中用于表示导播通道
            ///// </summary>
            //public byte byMirrorChanNum;
            ///// <summary>
            ///// 起始镜像通道号 
            ///// </summary>
            //public int wStartMirrorChanNo;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;		
        }
        #endregion

        #region IPC协议列表的结构体

        /// <summary>
        /// IPC协议列表的结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPC_PROTO_LIST
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 有效的IPC协议个数
            /// </summary>
            public uint dwProtoNum;
            /// <summary>
            /// 有效的IPC协议的参数结构 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IPC_PROTOCOL_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_PROTO_TYPE[] struProto;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region 协议参数的结构体
        /// <summary>
        /// 协议参数的结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PROTO_TYPE
        {
            /// <summary>
            /// 协议值，参考枚举ProtoType
            /// </summary>
            public uint dwType;
            /// <summary>
            /// 协议描述
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = DESC_LEN)]
            public string byDescribe;
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
            public uint dwSize;
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

        #region 分析行为结构体
        /// <summary>
        /// 人员聚集参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_HIGH_DENSITY
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 密度比率, 范围: [0.1, 1.0]
            /// </summary>
            public float fDensity;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 触发人员聚集参数报警阈值 20-360s
            /// </summary>
            public ushort wDuration;     
        }

        /// <summary>
        /// 剧烈运动参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_VIOLENT_MOTION
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 触发剧烈运动报警阈值：1-50秒
            /// </summary>
            public ushort wDuration;
            /// <summary>
            /// 灵敏度参数，范围[1,5]
            /// </summary>
            public byte bySensitivity;
            /// <summary>
            /// 0-纯视频模式，1-音视频联合模式，2-纯音频模式
            /// </summary>
            public byte byMode;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;           
        }

        /// <summary>
        /// 攀高参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_REACH_HIGHT
        {
             /// <summary>
             /// 攀高警戒面
             /// </summary>
            public NET_VCA_LINE struVcaLine;
            /// <summary>
            /// 触发攀高报警阈值：1-120秒
            /// </summary>
            public ushort wDuration;
            /// <summary>
            /// 保留字节
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;           
        }

        /// <summary>
        /// 起床参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_GET_UP
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 触发起床报警阈值1-100 秒
            /// </summary>
            public ushort wDuration;
            /// <summary>
            /// 起身检测模式,0-大床通铺模式,1-高低铺模式,2-大床通铺坐立起身模式
            /// </summary>
            public byte byMode;
            /// <summary>
            /// 灵敏度参数，范围[1,10]
            /// </summary>
            public byte bySensitivity;
            /// <summary>
            /// 保留字节
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;		    
        }
        /// <summary>
        /// 入侵参数 根据报警延迟时间来标识报警中带图片，报警间隔和IO报警一致，1秒发送一个。
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_VCA_INTRUSION
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 报警延迟时间: 1-120秒，建议5秒，判断是有效报警的时间
            /// </summary>
            public ushort wDuration;
            /// <summary>
            /// 灵敏度参数，范围[1-100]
            /// </summary>
            public byte bySensitivity;
            /// <summary>
            /// 占比：区域内所有未报警目标尺寸目标占区域面积的比重，归一化为－；
            /// </summary>
            public byte byRate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 折线警戒面参数结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_ADV_TRAVERSE_PLANE
        {
            /// <summary>
            /// 警戒面折线
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 跨越方向(详见VCA_CROSS_DIRECTION): 0-双向，1-从左到右2-从右到左
            /// </summary>
            public uint dwCrossDirection;
            /// <summary>
            /// 灵敏度参数，范围[1,5] 
            /// </summary>
            public byte bySensitivity;
            /// <summary>
            /// 保留字节
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;		    
        }
        /// <summary>
        /// 穿越警戒面参数 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_VCA_TRAVERSE_PLANE
        {
            /// <summary>
            /// 警戒面底边
            /// </summary>
            public NET_VCA_LINE struPlaneBottom;
            /// <summary>
            /// 穿越方向: 0-双向，1-从左到右，2-从右到左
            /// </summary>
            public VCA_CROSS_DIRECTION dwCrossDirection;
            /// <summary>
            /// 保留
            /// </summary>
            public byte byRes1;
            /// <summary>
            /// 警戒面高度
            /// </summary>
            public byte byPlaneHeight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 38, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

            //             public void init()
            //             {
            //                 struPlaneBottom = new NET_VCA_LINE();
            //                 struPlaneBottom.init();
            //                 byRes2 = new byte[38];
            //             }
        }

        /// <summary>
        /// 进入/离开区域参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_VCA_AREA
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 物品遗留
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_LEFT
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 触发物品遗留报警阈值 10-100秒
            /// </summary>
            public ushort wDuration;
            /// <summary>
            /// 灵敏度参数，范围[1,5] 
            /// </summary>
            public byte bySensitivity;
            /// <summary>
            /// 保留字节
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;        
        }

        /// <summary>
        /// 物品拿取
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_TAKE
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 触发物品拿取报警阈值10-100秒
            /// </summary>
            public ushort wDuration;
            /// <summary>
            /// 灵敏度参数，范围[1,5]
            /// </summary>
            public byte bySensitivity;
            /// <summary>
            /// 保留字节
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;       
        }
        /// <summary>
        /// 操作超时结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_OVER_TIME
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 操作报警时间阈值 4s-60000s
            /// </summary>
            public ushort wDuration;
            /// <summary>
            /// 保留字节
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;   
        }

        /// <summary>
        /// 徘徊参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_LOITER
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 触发徘徊报警的持续时间：1-120秒，建议10秒
            /// </summary>
            public ushort wDuration;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /// <summary>
        /// 丢包/捡包参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_TAKE_LEFT
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 触发丢包/捡包报警的持续时间：1-120秒，建议10秒
            /// </summary>
            public ushort wDuration;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /// <summary>
        /// 停车参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_PARKING
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 触发停车报警持续时间：1-120秒，建议10秒
            /// </summary>
            public ushort wDuration;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /// <summary>
        /// 奔跑参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_RUN
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 人奔跑最大距离, 范围: [0.1, 1.00]
            /// </summary>
            public float fRunDistance;
            /// <summary>
            ///  保留字节
            /// </summary>
            public byte byRes1;
            /// <summary>
            /// 0 像素模式  1 实际模式
            /// </summary>
            public byte byMode;             
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /// <summary>
        /// 人员进入参数结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_HUMAN_ENTER
        {
            /// <summary>
            /// 保留字节
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;		
        }

        /// <summary>
        /// 贴纸条参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_STICK_UP
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 报警持续时间：10-60秒，建议10秒
            /// </summary>
            public ushort wDuration;
            /// <summary>
            /// 灵敏度参数，范围[1,5]
            /// </summary>
            public byte bySensitivity;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /// <summary>
        /// 读卡器参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_SCANNER
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 读卡持续时间：10-60秒
            /// </summary>
            public ushort wDuration;
            /// <summary>
            /// 灵敏度参数，范围[1,5]
            /// </summary>
            public byte bySensitivity;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /// <summary>
        /// 离岗事件
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_LEAVE_POSITION
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 无人报警时间，单位：s，取值1-1800
            /// </summary>
            public ushort wLeaveDelay;
            /// <summary>
            /// 睡觉报警时间，单位：s，取值1-1800
            /// </summary>
            public ushort wStaticDelay;
            /// <summary>
            /// 模式，0-离岗事件，1-睡岗事件，2-离岗睡岗事件
            /// </summary>
            public byte byMode;
            /// <summary>
            /// 值岗人数类型，0-单人值岗，1-双人值岗
            /// </summary>
            public byte byPersonType;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;     
        }

        /// <summary>
        /// 尾随参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_TRAIL
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 保留
            /// </summary>
            public ushort wRes;
            /// <summary>
            /// 灵敏度参数，范围[1,5]
            /// </summary>
            public byte bySensitivity;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /// <summary>
        /// 倒地参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FALL_DOWN
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 触发事件阈值 1-60s
            /// </summary>
            public ushort wDuration;
            /// <summary>
            /// 灵敏度参数，范围[1,5]
            /// </summary>
            public byte bySensitivity; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /// <summary>
        /// 声强突变参数
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_AUDIO_ABNORMAL
        {
            /// <summary>
            /// 声音强度
            /// </summary>
            public ushort wDecibel;
            /// <summary>
            /// 灵敏度参数，范围[1,5] 
            /// </summary>
            public byte bySensitivity;
            /// <summary>
            /// 声音检测模式，0-灵敏度检测，1-分贝阈值检测，2-灵敏度与分贝阈值检测
            /// </summary>
            public byte byAudioMode;
            /// <summary>
            /// 使能，是否开启
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 声音阈值[0,100]
            /// </summary>
            public byte byThreshold;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 54, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;         
        }
        /// <summary>
        /// 如厕超时参数结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_TOILET_TARRY
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 如厕超时时间[1,3600]，单位：秒
            /// </summary>
            public ushort wDelay;        
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 放风场滞留参数结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_YARD_TARRY
        {
            /// <summary>
            /// 区域范围
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 放风场滞留时间[1,120]，单位：秒
            /// </summary>
            public ushort wDelay;        
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 折线攀高参数结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_ADV_REACH_HEIGHT
        {
            /// <summary>
            /// 攀高折线
            /// </summary>
            public NET_VCA_POLYGON struRegion;
            /// <summary>
            /// 跨越方向(详见VCA_CROSS_DIRECTION): 0-双向，1-从左到右2-从右到左
            /// </summary>
            public uint dwCrossDirection;
            /// <summary>
            /// 保留字节
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;		    
        }
        /// <summary>
        /// 多边型结构体 该结构会导致xaml界面出不来！！！！！！！！！？？问题暂时还没有找到  暂时屏蔽结构先
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_VCA_POLYGON
        {
            /// <summary>
            /// 有效点（大于等于3），若是3点在一条线上认为是无效区域，线交叉认为是无效区域 
            /// </summary>
            public uint dwPointNum;
            /// <summary>
            /// 多边形边界点，最大值为10
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = VCA_MAX_POLYGON_POINT_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_VCA_POINT[] struPos;
        }

        /// <summary>
        /// 点坐标结构 智能共用结构 坐标值归一化,浮点数值为当前画面的百分比大小, 精度为小数点后三位
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_VCA_POINT
        {
            /// <summary>
            ///  X轴坐标, 0.001~1
            /// </summary>
            public float fX;
            /// <summary>
            /// Y轴坐标, 0.001~1
            /// </summary>
            public float fY;
        }
        /// <summary>
        /// 线结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_VCA_LINE
        {
            /// <summary>
            /// 起点
            /// </summary>
            public NET_VCA_POINT struStart;
            /// <summary>
            /// 终点
            /// </summary>
            public NET_VCA_POINT struEnd; 

            //             public void init()
            //             {
            //                 struStart = new NET_VCA_POINT();
            //                 struEnd = new NET_VCA_POINT();
            //             }
        }
        
        /// <summary>
        /// 区域框结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_VCA_RECT
        {
            /// <summary>
            /// 边界框左上角点的X轴坐标, 0.001~1
            /// </summary>
            public float fX;
            /// <summary>
            /// 边界框左上角点的Y轴坐标, 0.001~1
            /// </summary>
            public float fY;
            /// <summary>
            /// 边界框的宽度, 0.001~1
            /// </summary>
            public float fWidth;
            /// <summary>
            /// 边界框的高度, 0.001~1
            /// </summary>
            public float fHeight;
        }
        /// <summary>
        /// 警戒事件参数
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct NET_VCA_EVENT_UNION
        {
            /// <summary>
            /// 联合体大小，4*23共92字节 
            /// </summary>
            [FieldOffsetAttribute(0)]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.U4)]
            public uint[] uLen;
            ///// <summary>
            ///// 穿越警戒面参数
            ///// </summary>
            //[FieldOffsetAttribute(4)]
            //public NET_VCA_TRAVERSE_PLANE struTraversePlane;
            ///// <summary>
            ///// 进入/离开区域参数
            ///// </summary>
            //[FieldOffsetAttribute(8)]
            //public NET_VCA_AREA struArea;
            ///// <summary>
            ///// 入侵参数
            ///// </summary>
            //[FieldOffsetAttribute(12)]
            //public NET_VCA_INTRUSION struIntrusion;
            ///// <summary>
            ///// 徘徊参数
            ///// </summary>

            //public NET_VCA_LOITER struLoiter;
            ///// <summary>
            ///// 丢包/捡包参数
            ///// </summary>

            //public NET_VCA_TAKE_LEFT struTakeTeft;
            ///// <summary>
            ///// 停车参数
            ///// </summary>

            //public NET_VCA_PARKING struParking;
            ///// <summary>
            ///// 奔跑参数
            ///// </summary>

            //public NET_VCA_RUN struRun;
            ///// <summary>
            ///// 人员聚集参数
            ///// </summary>

            //public NET_VCA_HIGH_DENSITY struHighDensity;
            ///// <summary>
            ///// 剧烈运动
            ///// </summary>

            //public NET_VCA_VIOLENT_MOTION struViolentMotion;
            ///// <summary>
            ///// 攀高
            ///// </summary>

            //public NET_VCA_REACH_HIGHT struReachHight;
            ///// <summary>
            ///// 起床
            ///// </summary>

            //public NET_VCA_GET_UP struGetUp;
            ///// <summary>
            ///// 物品遗留
            ///// </summary>

            //public NET_VCA_LEFT struLeft;
            ///// <summary>
            ///// 物品拿取
            ///// </summary>

            //public NET_VCA_TAKE struTake;
            ///// <summary>
            ///// 人员进入
            ///// </summary>

            //public NET_VCA_HUMAN_ENTER struHumanEnter;
            ///// <summary>
            ///// 操作超时
            ///// </summary>

            //public NET_VCA_OVER_TIME struOvertime;
            ///// <summary>
            ///// 贴纸条
            ///// </summary>

            //public NET_VCA_STICK_UP struStickUp;
            ///// <summary>
            ///// 读卡器参数
            ///// </summary>

            //public NET_VCA_SCANNER struScanner;
            ///// <summary>
            ///// 离岗参数
            ///// </summary>

            //public NET_VCA_LEAVE_POSITION struLeavePos;
            ///// <summary>
            ///// 尾随参数
            ///// </summary>

            //public NET_VCA_TRAIL struTrail;
            ///// <summary>
            ///// 倒地参数
            ///// </summary>

            //public NET_VCA_FALL_DOWN struFallDown;
            ///// <summary>
            ///// 声强突变
            ///// </summary>

            //public NET_VCA_AUDIO_ABNORMAL struAudioAbnormal;
            ///// <summary>
            ///// 折线攀高参数
            ///// </summary>

            //public NET_VCA_ADV_REACH_HEIGHT struReachHeight;    
            ///// <summary>
            ///// 如厕超时参数
            ///// </summary>

            //public NET_VCA_TOILET_TARRY struToiletTarry;     
            ///// <summary>
            ///// 放风场滞留参数
            ///// </summary>

            //public NET_VCA_YARD_TARRY struYardTarry;       
            ///// <summary>
            ///// 折线警戒面参数   
            ///// </summary>

            //public NET_VCA_ADV_TRAVERSE_PLANE struAdvTraversePlane;         
        }
        /// <summary>
        /// 前端设备地址信息，智能分析仪表示的是前端设备的地址信息，其他设备表示本机的地址
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_VCA_DEV_INFO
        {
            /// <summary>
            /// 前端设备地址
            /// </summary>
            public NET_DVR_IPADDR struDevIP;
            /// <summary>
            /// 前端设备端口号
            /// </summary>
            public ushort wPort;
            /// <summary>
            /// 前端设备通道
            /// </summary>
            public byte byChannel;
            /// <summary>
            /// 保留字节 后端通道
            /// </summary>
            public byte byRes;
        }
        /// <summary>
        /// 简化目标结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_VCA_TARGET_INFO
        {
            /// <summary>
            /// 目标ID ,人员密度过高报警时为0
            /// </summary>
            public uint dwID;
            /// <summary>
            /// 目标边界框
            /// </summary>
            public NET_VCA_RECT struRect;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 简化的规则信息, 包含规则的基本信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_VCA_RULE_INFO
        {
            /// <summary>
            /// 规则ID,0-7
            /// </summary>
            public byte byRuleID;
            /// <summary>
            /// 保留
            /// </summary>
            public byte byRes;
            /// <summary>
            /// 行为事件类型扩展，用于代替字段dwEventType，参考VCA_RULE_EVENT_TYPE_EX
            /// </summary>
            public ushort wEventTypeEx;
            /// <summary>
            /// 规则名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;
            /// <summary>
            /// 警戒事件类型
            /// </summary>
            public VCA_EVENT_TYPE dwEventType;
            /// <summary>
            /// 事件参数
            /// </summary>
            public NET_VCA_EVENT_UNION uEventParam;
        }
        /// <summary>
        /// 行为分析结果上报结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_VCA_RULE_ALARM
        {
            /// <summary>
            /// 结构长度
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 相对时标
            /// </summary>
            public uint dwRelativeTime;
            /// <summary>
            /// 绝对时标
            /// </summary>
            public uint dwAbsTime;
            /// <summary>
            /// 事件规则信息
            /// </summary>
            public NET_VCA_RULE_INFO struRuleInfo;
            /// <summary>
            /// 报警目标信息
            /// </summary>
            public NET_VCA_TARGET_INFO struTargetInfo;
            /// <summary>
            /// 前端设备信息
            /// </summary>
            public NET_VCA_DEV_INFO struDevInfo;
            /// <summary>
            /// 返回图片的长度 为0表示没有图片，大于0表示该结构后面紧跟图片数据
            /// </summary>
            public uint dwPicDataLen;
            /// <summary>
            /// 0-普通图片 1-对比图片
            /// </summary>
            public byte byPicType;
            /// <summary>
            /// 保留字节
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 保留，设置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
            /// <summary>
            /// 指向图片的指针
            /// </summary>
            public IntPtr pImage;
        }
        #endregion

        #region 报警结构体

        #region  报警设备信息结构       
        /// <summary>
        /// 报警设备信息结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_ALARMER
        {
            /// <summary>
            /// UserId 是否有效：0－无效；1－有效 
            /// </summary>
            public byte byUserIDValid;
            /// <summary>
            /// 序列号是否有效 0-无效，1-有效
            /// </summary>
            public byte bySerialValid;
            /// <summary>
            /// 版本号是否有效 0-无效，1-有效
            /// </summary>
            public byte byVersionValid;
            /// <summary>
            /// 设备名字是否有效 0-无效，1-有效
            /// </summary>
            public byte byDeviceNameValid;
            /// <summary>
            ///  MAC地址是否有效 0-无效，1-有效
            /// </summary>
            public byte byMacAddrValid;
            /// <summary>
            /// login端口是否有效 0-无效，1-有效
            /// </summary>
            public byte byLinkPortValid;
            /// <summary>
            /// 设备IP是否有效 0-无效，1-有效
            /// </summary>
            public byte byDeviceIPValid;
            /// <summary>
            /// socket ip是否有效 0-无效，1-有效
            /// </summary>
            public byte bySocketIPValid;
            /// <summary>
            /// NET_DVR_Login或NET_DVR_Login_V30返回值, 布防时有效
            /// </summary>
            public int lUserID;
            /// <summary>
            /// 序列号
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;
            /// <summary>
            /// 版本信息：V3.0以上版本支持的设备最高8位为主版本号，次高8位为次版本号，低16位为修复版本号；V3.0以下版本支持的设备高16位表示主版本，低16位表示次版本
            /// </summary>
            public uint dwDeviceVersion;
            /// <summary>
            /// 设备名字
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sDeviceName;
            /// <summary>
            /// MAC地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;
            /// <summary>
            /// 设备通讯端口
            /// </summary>
            public ushort wLinkPort;
            /// <summary>
            /// 设备IP地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sDeviceIP;
            /// <summary>
            /// 报警主动上传时的socket IP地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sSocketIP;
            /// <summary>
            /// Ip协议 0-IPV4, 1-IPV6
            /// </summary>
            public byte byIpProtocol;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 上传报警信息(9000扩展)
        /// <summary>
        /// 上传报警信息(9000扩展)
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V30
        {
            /// <summary>
            /// 报警类型：0-信号量报警，1-硬盘满，2-信号丢失，3-移动侦测，4-硬盘未格式化，5-读写硬盘出错，6-遮挡报警，7-制式不匹配，8-非法访问，9-视频信号异常，10-录像/抓图异常，11-智能场景变化，12-阵列异常，13-前端/录像分辨率不匹配，15-智能侦测，16-POE供电异常，17-闪光灯异常，18-磁盘满负荷异常报警，19-音频丢失
            /// </summary>
            public uint dwAlarmType;
            /// <summary>
            /// 报警输入端口
            /// </summary>
            public uint dwAlarmInputNumber;
            /// <summary>
            /// 触发的报警输出端口，值为1表示该报警端口输出，如byAlarmOutputNumber[0]=1表示触发第1个报警输出口输出，byAlarmOutputNumber[1]=1表示触发第2个报警输出口，依次类推。 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutputNumber;
            /// <summary>
            /// 触发的录像通道，值为1表示该通道录像，如byAlarmRelateChannel[0]=1表示触发第1个通道录像.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmRelateChannel;
            /// <summary>
            /// 发生报警的通道。当报警类型为2、3、6、9、10、11、13、15、16时有效，如byChannel[0]=1表示第1个通道报警。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChannel;
            /// <summary>
            /// 发生报警的硬盘。当报警类型为1，4，5时有效，byDiskNumber[0]=1表示1号硬盘异常 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byDiskNumber;
            ///// <summary>
            ///// 初始化参数
            ///// </summary>
            //public void Init()
            //{
            //    dwAlarmType = 0;
            //    dwAlarmInputNumber = 0;
            //    byAlarmRelateChannel = new byte[MAX_CHANNUM_V30];
            //    byChannel = new byte[MAX_CHANNUM_V30];
            //    byAlarmOutputNumber = new byte[MAX_ALARMOUT_V30];
            //    byDiskNumber = new byte[MAX_DISKNUM_V30];
            //    for (int i = 0; i < MAX_CHANNUM_V30; i++)
            //    {
            //        byAlarmRelateChannel[i] = Convert.ToByte(0);
            //        byChannel[i] = Convert.ToByte(0);
            //    }
            //    for (int i = 0; i < MAX_ALARMOUT_V30; i++)
            //    {
            //        byAlarmOutputNumber[i] = Convert.ToByte(0);
            //    }
            //    for (int i = 0; i < MAX_DISKNUM_V30; i++)
            //    {
            //        byDiskNumber[i] = Convert.ToByte(0);
            //    }
            //}
            ///// <summary>
            ///// 重置参数
            ///// </summary>
            //public void Reset()
            //{
            //    dwAlarmType = 0;
            //    dwAlarmInputNumber = 0;

            //    for (int i = 0; i < MAX_CHANNUM_V30; i++)
            //    {
            //        byAlarmRelateChannel[i] = Convert.ToByte(0);
            //        byChannel[i] = Convert.ToByte(0);
            //    }
            //    for (int i = 0; i < MAX_ALARMOUT_V30; i++)
            //    {
            //        byAlarmOutputNumber[i] = Convert.ToByte(0);
            //    }
            //    for (int i = 0; i < MAX_DISKNUM_V30; i++)
            //    {
            //        byDiskNumber[i] = Convert.ToByte(0);
            //    }
            //}
        }
        #endregion

        #region 上传的报警信息结构体
        /// <summary>
        /// 上传的报警信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40
        {
            /// <summary>
            /// 报警固定部分
            /// </summary>
            public NET_DVR_ALRAM_FIXED_HEADER struAlarmFixedHeader;
            /// <summary>
            /// 报警可变部分内容。（信号量报警：报警输出口号，通道号；图像、供电报警：通道号；硬盘报警：硬盘号）
            /// </summary>
            public IntPtr pAlarmData;
        }
        /// <summary>
        /// 报警信息固定部分参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ALRAM_FIXED_HEADER
        {
            /// <summary>
            /// 0-信号量报警,1-硬盘满,2-信号丢失，3－移动侦测，4－硬盘未格式化,5-写硬盘出错,6-遮挡报警，7-制式不匹配, 8-非法访问，9-视频信号异常，10-录像异常，11-智能场景变化，12-阵列异常，13-前端/录像分辨率不匹配
            /// </summary>
            public uint dwAlarmType;
            /// <summary>
            /// 发生报警的时间
            /// </summary>
            public NET_DVR_TIME_EX struAlarmTime;
            /// <summary>
            /// 为报警信息联合体
            /// </summary>
            public UNION_ALARMINFO_FIXED uStruAlarm;
        }
        /// <summary>
        /// 报警信息固定部分参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct UNION_ALARMINFO_FIXED
        {
            /// <summary>
            ///  联合体大小，128字节
            /// </summary>
            [FieldOffset(0)]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byUnionLen;
        }
        /// <summary>
        /// 报警输入参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct STRUCT_IO_ALARM
        {
            /// <summary>
            /// 发生报警的报警输入通道号，一次只有一个
            /// </summary>
            public uint dwAlarmInputNo;
            /// <summary>
            /// 触发的报警输出个数，用于后面计算变长数据部分中所有触发的报警输出通道号，四字节表示一个
            /// </summary>
            public uint dwTrigerAlarmOutNum;
            /// <summary>
            /// 触发的录像通道个数，用于后面计算变长数据部分中所有触发的录像通道号，四字节表示一个
            /// </summary>
            public uint dwTrigerRecordChanNum;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 报警报警通道参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct STRUCT_ALARM_CHANNEL
        {
            /// <summary>
            /// 发生报警通道数据个数，用于后面计算变长数据部分中所有发生的报警通道号，四字节表示一个
            /// </summary>
            public uint dwAlarmChanNum;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 124, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 报警硬盘参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct STRUCT_ALARM_HD
        {
            /// <summary>
            /// 发生报警的硬盘数据长度，用于后面计算变长数据部分中所有发生报警的硬盘号，四节表示一个
            /// </summary>
            public uint dwAlarmHardDiskNum;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 124, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 录播主机专用报警参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct STRUCT_ALARM_RecordingHost
        {
            /// <summary>
            ///  报警子类型：1- 一键延迟录像
            /// </summary>
            public byte bySubAlarmType;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 录播结束时间
            /// </summary>
            public NET_DVR_TIME_EX struRecordEndTime;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region 上传报警信息(8000旧设备)
        /// <summary>
        /// 上传报警信息(8000旧设备)
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO
        {
            /// <summary>
            /// 报警类型： 0－信号量报警； 1－硬盘满； 2－信号丢失； 3－移动侦测； 4－硬盘未格式化； 5－读写硬盘出错； 6－遮挡报警； 7－制式不匹配； 8－非法访问；
            /// </summary>
            public int dwAlarmType;
            /// <summary>
            /// 报警输入端口, 当报警类型为9时该变量表示串口状态0表示正常， -1表示错误
            /// </summary>
            public int dwAlarmInputNumber;
            /// <summary>
            /// 触发的输出端口，哪一位为1表示对应哪一个输出
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.U4)]
            public int[] dwAlarmOutputNumber;
            /// <summary>
            /// 触发的录像通道，哪一位为1表示对应哪一路录像, dwAlarmRelateChannel[0]对应第1个通道
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwAlarmRelateChannel;
            /// <summary>
            /// dwAlarmType为2或3,6时，表示哪个通道，dwChannel[0]位对应第1个通道
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwChannel;
            /// <summary>
            /// dwAlarmType为1,4,5时,表示哪个硬盘, dwDiskNumber[0]位对应第1个硬盘
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwDiskNumber;
            public void Init()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;
                dwAlarmOutputNumber = new int[MAX_ALARMOUT];
                dwAlarmRelateChannel = new int[MAX_CHANNUM];
                dwChannel = new int[MAX_CHANNUM];
                dwDiskNumber = new int[MAX_DISKNUM];
                for (int i = 0; i < MAX_ALARMOUT; i++)
                {
                    dwAlarmOutputNumber[i] = 0;
                }
                for (int i = 0; i < MAX_CHANNUM; i++)
                {
                    dwAlarmRelateChannel[i] = 0;
                    dwChannel[i] = 0;
                }
                for (int i = 0; i < MAX_DISKNUM; i++)
                {
                    dwDiskNumber[i] = 0;
                }
            }
            public void Reset()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;

                for (int i = 0; i < MAX_ALARMOUT; i++)
                {
                    dwAlarmOutputNumber[i] = 0;
                }
                for (int i = 0; i < MAX_CHANNUM; i++)
                {
                    dwAlarmRelateChannel[i] = 0;
                    dwChannel[i] = 0;
                }
                for (int i = 0; i < MAX_DISKNUM; i++)
                {
                    dwDiskNumber[i] = 0;
                }
            }
        }
        #endregion

        #endregion

        #region 设备取流配置结构体
        /// <summary>
        /// 设备取流配置结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DEC_STREAM_DEV_EX
        {
            /// <summary>
            /// 流媒体服务器配置
            /// </summary>
            public NET_DVR_STREAM_MEDIA_SERVER struStreamMediaSvrCfg;
            /// <summary>
            /// 解码通道信息
            /// </summary>
            public NET_DVR_DEV_CHAN_INFO_EX struDevChanInfo;
        }


        #endregion

        #region 流媒体服务器基本配置 
        /// <summary>
        /// 流媒体服务器基本配置
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_MEDIA_SERVER
        {
            /// <summary>
            /// 是否启用流媒体服务器取流,0表示无效，非0表示有效
            /// </summary>
            public byte byValid;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 流媒体服务器IP地址或者域名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string byAddress;
            /// <summary>
            /// 流媒体服务器端口
            /// </summary>
            public ushort wDevPort;/*流媒体服务器端口*/
            /// <summary>
            /// 传输协议类型 0-TCP，1-UDP
            /// </summary>
            public byte byTransmitType;/*传输协议类型 0-TCP，1-UDP*/
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region  前端设备信息结构体 
        /// <summary>
        /// 前端设备信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEV_CHAN_INFO_EX
        {
            /// <summary>
            /// 通道类型：0-普通通道，1-零通道，2-流ID，3-本地输入源
            /// </summary>
            public byte byChanType;
            /// <summary>
            /// 流ID，通道类型 byChanType 为 2 时有效
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = STREAM_ID_LEN)]
            public string byStreamId;/* 流ID，通道类型 byChanType 为 2 时有效 */
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 通道号，通道类型 byChanType为 0、1、3 时有效（如果通道类型为本地输入源，该参数值表示本地输入源索引）
            /// </summary>
            public uint dwChannel;//通道类型 byChanType为 0、1、3 时有效（如果通道类型为本地输入源，该参数值表示本地输入源索引）
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            /// <summary>
            /// 设备IP地址或者域名 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string byAddress;/* 设备IP地址或者域名 */
            /// <summary>
            /// 端口号
            /// </summary>
            public ushort wDVRPort;			 	//端口号
            /// <summary>
            /// 该参数无效，通道号见dwChannel 
            /// </summary>
            public byte byChannel;				//该参数无效，通道号见dwChannel 
            /// <summary>
            /// 传输协议类型0-TCP，1-UDP
            /// </summary>
            public byte byTransProtocol;		//传输协议类型0-TCP，1-UDP
            /// <summary>
            /// 传输码流模式 0－主码流 1－子码流
            /// </summary>
            public byte byTransMode;			//传输码流模式 0－主码流 1－子码流
            /// <summary>
            /// 前端设备厂家类型,通过接口获取
            /// </summary>
            public byte byFactoryType;			/*前端设备厂家类型,通过接口获取*/
            /// <summary>
            /// 设备类型(视频综合平台智能板使用)，1-解码器（此时根据视频综合平台能力集中byVcaSupportChanMode字段来决定是使用解码通道还是显示通道），2-编码器
            /// </summary>
            public byte byDeviceType; //设备类型(视频综合平台智能板使用)，1-解码器（此时根据视频综合平台能力集中byVcaSupportChanMode字段来决定是使用解码通道还是显示通道），2-编码器
            /// <summary>
            /// 显示通道号,智能配置使用
            /// </summary>
            public byte byDispChan;//显示通道号,智能配置使用
            /// <summary>
            /// 显示通道子通道号，智能配置时使用
            /// </summary>
            public byte bySubDispChan;//显示通道子通道号，智能配置时使用
            /// <summary>
            /// 分辨率：1- CIF，2- 4CIF，3- 720P，4- 1080P，5- 500W  大屏控制器使用，大屏控制器会根据该参数分配解码资源.
            /// </summary>
            public byte byResolution;	//; 1-CIF 2-4CIF 3-720P 4-1080P 5-500w大屏控制器使用，大屏控制器会根据该参数分配解码资源
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 设备登陆帐号
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sUserName;  	        //监控主机登陆帐号
            /// <summary>
            /// 设备密码 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPassword;  	        //监控主机密码
        }
        #endregion

        #region 动态域名取流配置结构体
        /// <summary>
        /// 动态域名取流配置结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DEC_DDNS_DEV
        {
            /// <summary>
            /// 流媒体服务器配置
            /// </summary>
            public NET_DVR_DEV_DDNS_INFO struDdnsInfo;
            /// <summary>
            /// 解码通道信息
            /// </summary>
            public NET_DVR_STREAM_MEDIA_SERVER struMediaServer;
        }

        /// <summary>
        /// 动态域名参数配置
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEV_DDNS_INFO
        {
            /// <summary>
            /// 设备域名(IPServer或hiDDNS时可填设备序列号或者别名)
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string byDevAddress;
            /// <summary>
            /// 传输协议类型：0- TCP，1- UDP，2- 多播
            /// </summary>
            public byte byTransProtocol;
            /// <summary>
            /// 传输码流模式 0－主码流 1－子码流
            /// </summary>
            public byte byTransMode;
            /// <summary>
            /// 域名服务器类型：0- IPServer，1- Dyndns，2- PeanutHull(花生壳)，3- NO-IP，4- hiDDNS
            /// </summary>
            public byte byDdnsType;
            /// <summary>
            /// 保留，置为0
            /// </summary>      		    
            public byte byRes1;
            /// <summary>
            /// DDNS服务器地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string byDdnsAddress;
            /// <summary>
            /// DDNS服务器端口号
            /// </summary>
            public ushort wDdnsPort;
            /// <summary>
            /// 通道类型：0-普通通道，1-零通道，2-流ID
            /// </summary>
            public byte byChanType;
            /// <summary>
            /// 前端设备厂家类型,通过接口NET_DVR_GetIPCProtoList获取
            /// </summary>
            public byte byFactoryType;
            /// <summary>
            /// 通道号
            /// </summary>
            public uint dwChannel;
            /// <summary>
            ///  流ID，通道类型 byChanType 为 2 时有效
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = STREAM_ID_LEN)]
            public string byStreamId;
            /// <summary>
            /// 设备登陆帐号 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sUserName;
            /// <summary>
            /// 设备密码
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPassword;
            /// <summary>
            /// 设备端口号
            /// </summary>
            public ushort wDevPort;
            /// <summary>
            /// 保留，置为0
            /// </summary>        		 	
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region URL取流配置结构体
        /// <summary>
        /// URL取流配置结构体
        /// </summary>
        /// <remarks>
        /// 通过流媒体服务器取流的URL格式举例:
        /// {rtsp://ip[:port]/urlExtension}[?username=username][?password=password][?linkmode=linkmode]
        /// URL路径也支持其他自定义路径（需前端IPC支持）。
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PU_STREAM_URL
        {
            /// <summary>
            /// 是否启用：0- 禁用，1- 启用
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 取流URL路径
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
            public string strURL;
            /// <summary>
            /// 传输协议类型：0-TCP，1-UDP 
            /// </summary>
            public byte byTransPortocol;
            /// <summary>
            /// 设备ID号，wIPID = iDevInfoIndex + iGroupNO*64 +1
            /// </summary>
            public ushort wIPID;
            /// <summary>
            /// 设备通道号
            /// </summary>
            public byte byChannel;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public void Init()
            {
                byRes = new byte[7];
            }
        }
        #endregion

        //#region  报警布防参数结构体 
        ///// <summary>
        ///// 报警布防参数结构体
        ///// </summary>
        //[StructLayoutAttribute(LayoutKind.Sequential)]
        //public struct NET_DVR_SETUPALARM_PARAM
        //{
        //    /// <summary>
        //    /// 结构体大小 
        //    /// </summary>
        //    public UInt32 dwSize;
        //    /// <summary>
        //    /// 布防优先级：0- 一等级（高），1- 二等级（中），2- 三等级（低）
        //    /// </summary>
        //    public byte byLevel;
        //    /// <summary>
        //    /// 智能交通报警信息上传类型：0- 老报警信息（NET_DVR_PLATE_RESULT），1- 新报警信息(NET_ITS_PLATE_RESULT) 
        //    /// </summary>
        //    public byte byAlarmInfoType;
        //    /// <summary>
        //    /// 0- 移动侦测、视频丢失、遮挡、IO信号量等报警信息以普通方式上传（NET_DVR_ALARMINFO_V30），1- 报警信息以数据可变长方式上传（NET_DVR_ALARMINFO_V40，设备若不支持则仍以普通方式上传） 
        //    /// </summary>
        //    public byte byRetAlarmTypeV40;
        //    /// <summary>
        //    /// CVR上传报警信息回调结构体版本：0- COMM_ALARM_DEVICE，1- COMM_ALARM_DEVICE_V40 
        //    /// </summary>
        //    public byte byRetDevInfoVersion;
        //    /// <summary>
        //    /// VQD报警上传类型类型：0-上传VQD诊断信息（NET_DVR_VQD_DIAGNOSE_INFO），1-VQD诊断异常信息（NET_DVR_VQD_ALARM） 
        //    /// </summary>
        //    public byte byRetVQDAlarmType;
        //    /// <summary>
        //    /// 人脸侦测报警信息类型：1- 表示人脸侦测报警扩展(NET_DVR_FACE_DETECTION)，0- 表示原先支持结构(NET_VCA_FACESNAP_RESULT) 
        //    /// </summary>
        //    public byte byFaceAlarmDetection;
        //    /// <summary>
        //    /// 按位表示，值：0-上传，1-不上传 bit0- 表示二级布防是否上传图片
        //    /// </summary>
        //    public byte bySupport;
        //    /// <summary>
        //    /// 保留，置为0 
        //    /// </summary>
        //    public byte byRes;
        //    /// <summary>
        //    /// 任务处理号
        //    /// </summary>
        //    public ushort wTaskNo;
        //    /// <summary>
        //    /// 保留，置为0 
        //    /// </summary>
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
        //    public byte[] byRes1;
        //    //[MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
        //    //public byte[] byRes;//这里保留音频的压缩参数 
        //}

        //#endregion

        #region 动态解码参数结构体
        /// <summary>
        /// 动态解码参数结构体
        /// </summary>
        public struct NET_DVR_PU_STREAM_CFG
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 流媒体服务器配置参数
            /// </summary>
            public NET_DVR_STREAM_MEDIA_SERVER_CFG struStreamMediaSvrCfg;
            /// <summary>
            /// 设备通道配置参数
            /// </summary>
            public NET_DVR_DEV_CHAN_INFO struDevChanInfo;
    }
        #endregion

        #region 流媒体服务器参数结构体
        /// <summary>
        /// 流媒体服务器参数结构体
        /// </summary>
        public struct NET_DVR_STREAM_MEDIA_SERVER_CFG
        {
            /// <summary>
            /// 是否启用流媒体服务器取流：0-不启用，非0-启用
            /// </summary>
            public byte byValid;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 流媒体服务器的IP地址
            /// </summary>
            public NET_DVR_IPADDR struDevIP;
            /// <summary>
            /// 流媒体服务器端口
            /// </summary>
            public int  wDevPort;
            /// <summary>
            /// 传输协议类型：0-TCP，1-UDP 
            /// </summary>
            public byte byTransmitType;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst =69, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 前端设备信息结构体
        /// <summary>
        /// 前端设备信息结构体
        /// </summary>
        public struct NET_DVR_DEV_CHAN_INFO
        {
            /// <summary>
            /// 设备IP地址
            /// </summary>
            public NET_DVR_IPADDR struIP;
            /// <summary>
            /// 设备端口号 
            /// </summary>
            public int wDVRPort;
            /// <summary>
            /// 通道号,目前设备的模拟通道号是从1开始的，对于9000等设备的IPC接入，数字通道号从33开始 
            /// </summary>
            public byte byChannel;
            /// <summary>
            /// 传输协议类型：0-TCP，1-UDP，2-多播方式，3-RTP 
            /// </summary>
            public byte byTransProtocol;
            /// <summary>
            /// 传输码流模式：0－主码流，1－子码流
            /// </summary>
            public byte byTransMode;
            /// <summary>
            /// 前端设备厂家类型， 通过接口NET_DVR_GetIPCProtoList获取
            /// </summary>
            public byte byFactoryType;
            /// <summary>
            /// 设备类型(视频综合平台使用)：1- IPC，2- ENCODER 
            /// </summary>
            public byte byDeviceType;
            /// <summary>
            /// 显示通道号（智能配置使用），根据能力集决定使用解码通道还是显示通道
            /// </summary>
            public byte byDispChan;
            /// <summary>
            /// 显示通道子通道号（智能配置时使用）
            /// </summary>
            public byte bySubDispChan;
            /// <summary>
            /// 分辨率：1- CIF，2- 4CIF，3- 720P，4- 1080P，5- 500W 
            /// </summary>
            public byte byResolution;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 设备域名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string byDomain;
            /// <summary>
            /// 设备登陆帐号
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sUserName;
            /// <summary>
            /// 设备密码
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPassword;
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

        #region  时间结构参数 
        /// <summary>
        /// 时间结构参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_TIME
        {
            /// <summary>
            /// 年
            /// </summary>
            public uint dwYear;
            /// <summary>
            /// 月
            /// </summary>
            public uint dwMonth;
            /// <summary>
            /// 日
            /// </summary>
            public uint dwDay;
            /// <summary>
            /// 时
            /// </summary>
            public uint dwHour;
            /// <summary>
            /// 分
            /// </summary>
            public uint dwMinute;
            /// <summary>
            /// 秒
            /// </summary>
            public uint dwSecond;
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
            public uint dwSize;
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
                                 /// <summary>
                                 /// 初始化参数
                                 /// </summary>
            public void Init()
            {
                byRes2 = new byte[34];
            }
        }
        #endregion

        #region IP通道信息结构体
        /// <summary>
        /// IP通道信息结构体
        /// </summary>
        /// <remarks>iDevID为设备ID号，iDevID = byIPIDHigh*256 + byIPID。通过iDevID值查找具体的设备信息struIPDevInfo（结构体NET_DVR_IPPARACFG_V40的数组参数），与设备信息数组下标（iDevInfoIndex）换算关系为：iDevID = iDevInfoIndex + iGroupNO*64 +1。</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPCHANINFO
        {
            /// <summary>
            /// IP通道在线状态，是一个只读的属性；0表示HDVR或者NVR设备的数字通道连接对应的IP设备失败，该通道不在线；1表示连接成功，该通道在线 
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// IP设备ID的低8位，byIPID = iDevID % 256 
            /// </summary>
            public byte byIPID;
            /// <summary>
            /// IP设备的通道号，例如设备A（HDVR或者NVR设备）的IP通道01，对应的是设备B里的通道04，则byChannel=4。 
            /// </summary>
            public byte byChannel;
            /// <summary>
            /// IP设备ID的高8位，byIPIDHigh = iDevID /256 
            /// </summary>
            public byte byIPIDHigh;
            /// <summary>
            /// 传输协议类型0-TCP/auto(具体有设备决定)，1-UDP 2-多播 3-仅TCP 4-auto
            /// </summary>
            public byte byTransProtocol;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 初始化参数
            /// </summary>
            public void Init()
            {
                byRes = new byte[31];
            }
        }
        #endregion

        #region IP地址结构体
        /// <summary>
        /// IP地址结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_IPADDR
        {

            /// <summary>
            /// 设备IPv4地址 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpV4;

            /// <summary>
            /// 设备IPv6地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sIpV6;
            ///// <summary>
            ///// 初始化参数
            ///// </summary>
            //public void Init()
            //{
            //    sIpV6 = new byte[128];
            //}
        }
        #endregion

        #region IP设备信息结构体
        /// <summary>
        /// IP设备信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPDEVINFO
        {
            /// <summary>
            /// 该IP设备是否启用
            /// </summary>
            public uint dwEnable;
            /// <summary>
            /// 用户名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;
            /// <summary>
            /// 密码
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            /// <summary>
            /// IP地址
            /// </summary>
            public NET_DVR_IPADDR struIP;
            /// <summary>
            /// 端口号
            /// </summary>
            public ushort wDVRPort;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 初始化参数
            /// </summary>
            public void Init()
            {
                sUserName = new byte[NAME_LEN];
                sPassword = new byte[PASSWD_LEN];
                byRes = new byte[34];
            }
        }
        #endregion

        #region IP设备资源及IP通道资源配置结构体
        /// <summary>
        /// IP设备资源及IP通道资源配置结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// IP设备，下标0对应设备IP ID为1 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO[] struIPDevInfo;
            /// <summary>
            /// 模拟通道资源是否启用：0-禁用，1-启用。DS-9000该配置修改后会自动重启
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable;
            /// <summary>
            /// IP通道信息 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;
            /// <summary>
            /// 初始化参数
            /// </summary>
            public void Init()
            {
                int i = 0;
                struIPDevInfo = new NET_DVR_IPDEVINFO[MAX_IP_DEVICE];

                for (i = 0; i < MAX_IP_DEVICE; i++)
                {
                    struIPDevInfo[i].Init();
                }
                byAnalogChanEnable = new byte[MAX_ANALOG_CHANNUM];
                struIPChanInfo = new NET_DVR_IPCHANINFO[MAX_IP_CHANNEL];
                for (i = 0; i < MAX_IP_CHANNEL; i++)
                {
                    struIPChanInfo[i].Init();
                }
            }
        }
        #endregion

        #region IP设备资源及IP通道资源配置结构体V31
        /// <summary>
        /// IP设备资源及IP通道资源配置结构体V31
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG_V31
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// IP设备，下标0对应设备IP ID为1 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO_V31[] struIPDevInfo;
            /// <summary>
            /// 模拟通道资源是否启用：0-禁用，1-启用。DS-9000该配置修改后会自动重启 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable;
            /// <summary>
            /// IP通道信息 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;
            /// <summary>
            /// 初始化参数
            /// </summary>
            public void Init()
            {
                int i = 0;
                struIPDevInfo = new NET_DVR_IPDEVINFO_V31[MAX_IP_DEVICE];

                for (i = 0; i < MAX_IP_DEVICE; i++)
                {
                    struIPDevInfo[i].Init();
                }
                byAnalogChanEnable = new byte[MAX_ANALOG_CHANNUM];
                struIPChanInfo = new NET_DVR_IPCHANINFO[MAX_IP_CHANNEL];
                for (i = 0; i < MAX_IP_CHANNEL; i++)
                {
                    struIPChanInfo[i].Init();
                }
            }
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
            public uint dwSurplusLockTime;
            /// <summary>
            /// 字符编码类型（SDK所有接口返回的字符串编码类型，透传接口除外）：0- 无字符编码信息(老设备)，1- GB2312(简体中文)，2- GBK，3- BIG5(繁体中文)，4- Shift_JIS(日文)，5- EUC-KR(韩文)，6- UTF-8，7- ISO8859-1，8- ISO8859-2，9- ISO8859-3，…，依次类推，21- ISO8859-15(西欧)
            /// </summary>
            public byte byCharEncodeType;
            /// <summary>
            /// 保留，置为0 [256]
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.I1)]
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

      

        #region 设备DDNS域名信息结构体
        /// <summary>
        /// 设备DDNS域名信息结构体
        /// </summary>
        public struct NET_DVR_DDNS_ADDRESS
        {
            /// <summary>
            /// 设备域名 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            byte[] byDevAddress;
            /// <summary>
            /// DDNS服务器地址 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            byte[] byDevDdns;
            /// <summary>
            /// 域名服务器类型：0- IPServer，1- Dyndns，2- PeanutHull(花生壳)，3- NO-IP，4- hiDDNS
            /// </summary>
            byte byDdnsType;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            byte[] byRes1;
            /// <summary>
            /// 设备端口号
            /// </summary>
            uint wDevPort;
            /// <summary>
            /// DDNS服务器端口
            /// </summary>
            uint wDdnsPort;
            /// <summary>
            /// 保留 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            byte[] byres;
        }
        #endregion

        #region 设备地址联合体
        /// <summary>
        /// 设备地址联合体
        /// </summary>
        public struct Addr
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200, ArraySubType = UnmanagedType.I1)]
            byte[] byRes;
            /// <summary>
            /// 设备IP地址信息结构体
            /// </summary>
            NET_DVR_IP_ADDRESS struIpAddr;
            /// <summary>
            /// 设备DDNS域名信息结构体
            /// </summary>
            NET_DVR_DDNS_ADDRESS struDdnsAddr;

        }

        #endregion

        #region 设备IP地址信息结构体
        /// <summary>
        /// 设备IP地址信息结构体
        /// </summary>
        public struct NET_DVR_IP_ADDRESS
        {
            /// <summary>
            /// 设备IP地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            byte[] byDevAddress;
            /// <summary>
            /// 设备端口号 
            /// </summary>
            uint wDevPort;
            /// <summary>
            /// 保留 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 134, ArraySubType = UnmanagedType.I1)]
            byte[] byres;
        }
        #endregion

        #region  预览参数结构体(预览V40接口)
        /// <summary>
        /// 预览参数结构体(预览V40接口)
        /// </summary>
        /// <remarks>
        /// 该结构体中可以设置当前预览操作是否阻塞（通过bBlocked参数设置）。若设为不阻塞，表示发起与设备的连接就认为连接成功，如果发生码流接收失败、播放失败等情况以预览异常的方式通知上层。在循环播放的时候可以减短停顿的时间，与NET_DVR_RealPlay处理一致。若设为阻塞，表示直到播放操作完成才返回成功与否。 
        /// dwStreamType、bPassbackRecord、byPreviewMode、byStreamID这些参数的取值需要设备支持。 
        /// NET_DVR_RealPlay_V40支持多播方式预览（dwLinkMode设为2），不需要传多播组地址，底层自动从设备获取已配置的多播组地址（NET_DVR_NETCFG_V30->struMulticastIpAddr）并以该多播组地址实现多播。 
        /// Linux 64位系统不支持软解码功能，因此需要将窗口句柄传NULL，设置回调函数，只取流不解码显示。 
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PREVIEWINFO
        {
            /// <summary>
            /// 通道号，目前设备模拟通道号从1开始，数字通道的起始通道号通过NET_DVR_GetDVRConfig（配置命令NET_DVR_GET_IPPARACFG_V40）获取（dwStartDChan）。
            /// </summary>
            public int lChannel;
            /// <summary>
            /// 码流类型：0-主码流，1-子码流，2-码流3，3-虚拟码流，以此类推 
            /// </summary>
            public uint dwStreamType;
            /// <summary>
            /// 连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RTP/HTTP 
            /// </summary>
            public uint dwLinkMode;
            /// <summary>
            /// 播放窗口的句柄，为NULL表示不解码显示。
            /// </summary>
            public IntPtr hPlayWnd;
            /// <summary>
            /// 0- 非阻塞取流，1- 阻塞取流。如果阻塞取流，SDK内部connect失败将会有5s的超时才能够返回，不适合于轮询取流操作。
            /// </summary>
            public bool bBlocked;
            /// <summary>
            /// 0-不启用录像回传，1-启用录像回传。ANR断网补录功能，客户端和设备之间网络异常恢复之后自动将前端数据同步过来，需要设备支持。 
            /// </summary>
            public bool bPassbackRecord;
            /// <summary>
            /// 预览模式，0-正常预览，1-延迟预览
            /// </summary>
            public byte byPreviewMode;
            /// <summary>
            /// 流ID，为字母、数字和"_"的组合，lChannel为0xffffffff时启用此参数.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byStreamID;
            /// <summary>
            /// 应用层取流协议：0- 私有协议，1- RTSP协议。主子码流支持的取流协议通过登录返回结构参数NET_DVR_DEVICEINFO_V30的byMainProto、bySubProto值得知。设备同时支持私协议和RTSP协议时，该参数才有效，默认使用私有协议，可选RTSP协议。
            /// </summary>
            public byte byProtoType;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 216, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 播放库播放缓冲区最大缓冲帧数，取值范围：1~50，置0时默认为1。设置显示缓冲需要在播放库调用PlayM4_Play之前调用，该参数替换原先NET_DVR_SetPlayerBufNumber接口.
            /// </summary>
            public uint dwDisplayBufNum;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;

        }
        #endregion

        #region 检测网络流量参数
        /// <summary>
        /// 检测网络流量结果参数
        /// </summary>
        public struct NET_DVR_FLOW_INFO
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 发送流量大小，单位kbps 
            /// </summary>
            public uint dwSendFlowSize;
            /// <summary>
            /// 接收流量大小，单位kbps 
            /// </summary>
            public uint dwRecvFlowSize;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            byte[] byRes;
        }
        /// <summary>
        /// 检测网络流量参数结构体
        /// </summary>
        public struct NET_DVR_FLOW_TEST_PARAM
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 网卡索引：0–网卡0，1-网卡1，以此类推；最高位为1表示bonding网卡，0表示普通网卡
            /// </summary>
            public uint lCardIndex;
            /// <summary>
            /// 上传时间间隔,单位100 毫秒
            /// </summary>
            public uint dwTime;
            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        #endregion

        #region  设备激活
        /// <summary>
        /// 设备激活
        /// </summary>
        public struct NET_DVR_ACTIVATECFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 初始密码，密码等级弱或者以上
            /// </summary>
            /// <remarks>
            /// 将密码输入分为数字(0~9)、小写字母(a~z)、大写字母(A~Z)、特殊符号（:\"除外）4类，等级分为4个等级，如下所示：
            /// 等级0（风险密码）：密码长度小于8位，或者只包含4类字符中的任意一类，或者密码与用户名一样，或者密码是用户名的倒写。例如：12345、abcdef。
            /// 等级1（弱密码）：包含两类字符，且组合为（数字+小写字母）或（数字+大写字母），且长度大于等于8位。例如：abc12345、123ABCDEF
            /// 等级2（中密码）：包含两类字符，且组合不能为（数字+小写字母）和（数字+大写字母），且长度大于等于8位。例如：12345***++、ABCDabcd。
            /// 等级3（强密码）：包含三类字符及以上，且长度大于等于8位。例如：Abc12345、abc12345+
            /// </remarks>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 108, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        #endregion

        #region SDK信息结构体
        /// <summary>
        /// SDK状态信息结构体
        /// </summary>
        public struct NET_DVR_SDKSTATE
        {
            /// <summary>
            /// 当前注册的用户数 
            /// </summary>
            public int dwTotalLoginNum;
            /// <summary>
            /// 当前实时预览的路数
            /// </summary>
            public int dwTotalRealPlayNum;
            /// <summary>
            /// 当前回放或下载的路数 
            /// </summary>
            public int dwTotalPlayBackNum;
            /// <summary>
            /// 当前建立报警通道的路数
            /// </summary>
            public int dwTotalAlarmChanNum;
            /// <summary>
            /// 当前硬盘格式化的路数
            /// </summary>
            public int dwTotalFormatNum;
            /// <summary>
            /// 当前文件搜索的路数
            /// </summary>
            public int dwTotalFileSearchNum;
            /// <summary>
            /// 当前日志搜索的路数
            /// </summary>
            public int dwTotalLogSearchNum;
            /// <summary>
            /// 当前建立透明通道的路数
            /// </summary>
            public int dwTotalSerialNum;
            /// <summary>
            /// 当前升级的路数 
            /// </summary>
            public int dwTotalUpgradeNum;
            /// <summary>
            /// 当前语音转发的路数
            /// </summary>
            public int dwTotalVoiceComNum;
            /// <summary>
            /// 当前语音广播的路数
            /// </summary>
            public int dwTotalBroadCastNum;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public int[] dwRes;
        }
        /// <summary>
        /// SDK功能信息结构体
        /// </summary>
        public struct NET_DVR_SDKABL
        {
            /// <summary>
            /// 最大注册用户数 
            /// </summary>
            public int dwMaxLoginNum;
            /// <summary>
            /// 最大实时预览的路数
            /// </summary>
            public int dwMaxRealPlayNum;
            /// <summary>
            /// 最大回放或下载的路数 
            /// </summary>
            public int dwMaxPlayBackNum;
            /// <summary>
            /// 最大建立报警通道的路数
            /// </summary>
            public int dwMaxAlarmChanNum;
            /// <summary>
            /// 最大硬盘格式化的路数
            /// </summary>
            public int dwMaxFormatNum;
            /// <summary>
            /// 最大文件搜索的路数
            /// </summary>
            public int dwMaxFileSearchNum;
            /// <summary>
            /// 最大日志搜索的路数 
            /// </summary>
            public int dwMaxLogSearchNum;
            /// <summary>
            /// 最大建立透明通道的路数
            /// </summary>
            public int dwMaxSerialNum;
            /// <summary>
            /// 最大升级的路数
            /// </summary>
            public int dwMaxUpgradeNum;
            /// <summary>
            /// 最大语音转发的路数
            /// </summary>
            public int dwMaxVoiceComNum;
            /// <summary>
            /// 最大语音广播的路数 
            /// </summary>
            public int dwMaxBroadCastNum;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public int[] dwRes;
        }

        #endregion

        #region 窗口设置结构体
        /// <summary>
        /// 窗口设置结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_RECTCFG
        {
            /// <summary>
            /// 矩形左上角起始点X坐标
            /// </summary>
            public ushort wXCoordinate; /*矩形左上角起始点X坐标*/
            /// <summary>
            /// 矩形左上角Y坐标
            /// </summary>
            public ushort wYCoordinate; /*矩形左上角Y坐标*/
            /// <summary>
            /// 矩形宽度
            /// </summary>
            public ushort wWidth;       /*矩形宽度*/
            /// <summary>
            /// 矩形高度
            /// </summary>
            public ushort wHeight;      /*矩形高度*/
        }
        /// <summary>
        /// 矩阵参数配置结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_RECTCFG_EX
        {
            /// <summary>
            /// 矩形左上角起始点X坐标 
            /// </summary>
            public uint dwXCoordinate;
            /// <summary>
            /// 矩形左上角起始点Y坐标 
            /// </summary>
            public uint dwYCoordinate;
            /// <summary>
            /// 矩形宽度
            /// </summary>
            public uint dwWidth;
            /// <summary>
            /// 矩形高度
            /// </summary>
            public uint dwHeight;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        /// <summary>
        /// 视频参数配置
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOEFFECT
        {
            /// <summary>
            /// 亮度，取值范围[0,100]
            /// </summary>
            public byte byBrightnessLevel; /*0-100*/
            /// <summary>
            /// 对比度，取值范围[0,100] 
            /// </summary>
            public byte byContrastLevel; /*0-100*/
            /// <summary>
            /// 锐度，取值范围[0,100] 
            /// </summary>
            public byte bySharpnessLevel; /*0-100*/
            /// <summary>
            /// 饱和度，取值范围[0,100] 
            /// </summary>
            public byte bySaturationLevel; /*0-100*/
            /// <summary>
            /// 色度，取值范围[0,100]，保留
            /// </summary>
            public byte byHueLevel; /*0-100,（保留）*/
            /// <summary>
            /// 使能，按位表示。bit0-SMART IR(防过曝)，bit1-低照度，bit2-强光抑制使能，值：0-否，1-是，例如byEnableFunc and 0x2==1表示使能低照度功能； bit3-锐度类型，值：0-自动，1-手动。
            /// </summary>
            public byte byEnableFunc; //使能，按位表示，bit0-SMART IR(防过曝)，bit1-低照度,bit2-强光抑制使能，0-否，1-是
            /// <summary>
            /// 强光抑制等级，取值范围：[1,3] 
            /// </summary>
            public byte byLightInhibitLevel; //强光抑制等级，[1-3]表示等级
            /// <summary>
            /// 灰度值域:0-[0,255]，1-[16,235]
            /// </summary>
            public byte byGrayLevel; //灰度值域，0-[0-255]，1-[16-235]
        }
        /// <summary>
        /// 信号源信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_INPUTSTREAMCFG_V40
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            public byte byValid;
            /// <summary>
            /// 见NET_DVR_CAM_MODE
            /// </summary>
            public byte byCamMode;
            /// <summary>
            /// 信号源序号
            /// </summary>
            public ushort wInputNo;
            /// <summary>
            /// 视频名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sCamName;
            /// <summary>
            /// 视频参数
            /// </summary>
            public NET_DVR_VIDEOEFFECT struVideoEffect;
            /// <summary>
            /// ip输入时使用
            /// </summary>
            public NET_DVR_PU_STREAM_CFG struPuStream;
            /// <summary>
            /// 信号源所在的板卡号，只能获取
            /// </summary>
            public ushort wBoardNum;
            /// <summary>
            /// 信号源在板卡上的位置，只能获取
            /// </summary>
            public ushort wInputIdxOnBoard;
            /// <summary>
            /// 分辨率
            /// </summary>
            public uint dwResolution;
            /// <summary>
            /// 视频制式，见VIDEO_STANDARD
            /// </summary>
            public byte byVideoFormat;
            /// <summary>
            /// 信号源状态，0-字段无效 1-有信号 2-无信号 3-异常 
            /// </summary>
            public byte byStatus;
            /// <summary>
            /// 网络信号源分组 组名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sGroupName;
            /// <summary>
            /// 关联矩阵，0-不关联  1-关联，当输入信号源为NET_DVR_CAM_BNC，NET_DVR_CAM_VGA，NET_DVR_CAM_DVI，NET_DVR_CAM_HDMI,中的一种时，该参数有效。
            /// </summary>
            public byte byJointMatrix;
            /// <summary>
            /// 拼接信号源的拼接编号(只能获取)
            /// </summary>
            public byte byJointNo;
            /// <summary>
            /// 色彩模式， 0-自定义 1-锐利 2-普通 3-柔和，当为自定义时，使用struVideoEffect设置
            /// </summary>
            public byte byColorMode;
            /// <summary>
            /// 关联屏幕服务器，0-不联，1-关联
            /// </summary>
            public byte byScreenServer;
            /// <summary>
            /// 设备号
            /// </summary>
            public byte byDevNo;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            public byte byRes1;
            /// <summary>
            /// 输入信号源编号（新）
            /// </summary>
            public uint dwInputSignalNo;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /// <summary>
        /// 输入信号源列表结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_INPUT_SIGNAL_LIST
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 设备输入信号源数量
            /// </summary>
            public uint dwInputSignalNums;
            /// <summary>
            /// 指向dwInputSignalNums个NET_DVR_INPUTSTREAMCFG结构大小的缓冲区
            /// </summary>
            public IntPtr pBuffer;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 所分配缓冲区长度，输入参数（大于等于dwInputSignalNums个NET_DVR_INPUTSTREAMCFG结构大小）
            /// </summary>
            public uint dwBufLen;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_BUF_INFO
        {
            public IntPtr pBuf;
            public uint nLen;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IN_PARAM
        {
            public NET_DVR_BUF_INFO struCondBuf;
            public NET_DVR_BUF_INFO struInParamBuf;
            public uint dwRecvTimeOut;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_OUT_PARAM
        {
            public NET_DVR_BUF_INFO struOutBuf;
            public IntPtr lpStatusList;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

    }
}
