using System;
using System.Runtime.InteropServices;
namespace PreviewDemo
{
    /// <summary>
    /// CHCNetSDK 的摘要说明。
    /// </summary>
    public class CHCNetSDK
    {
        public CHCNetSDK()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /************************************视频综合平台(begin)***************************************/
        public const int MAX_SUBSYSTEM_NUM = 80;   //一个矩阵系统中最多子系统数量
        public const int MAX_SERIALLEN = 36;  //最大序列号长度

        public const int MAX_LOOPPLANNUM = 16;  //最大计划切换组
        public const int DECODE_TIMESEGMENT = 4;     //计划解码每天时间段数

        public const int MAX_DOMAIN_NAME = 64;  /* 最大域名长度 */
        public const int MAX_DISKNUM_V30 = 33; //9000设备最大硬盘数/* 最多33个硬盘(包括16个内置SATA硬盘、1个eSATA硬盘和16个NFS盘) */
        public const int MAX_DAYS = 7;       //每周天数

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public struct NET_DVR_SUBSYSTEMINFO
        {
            public byte bySubSystemType;//子系统类型，1-解码用子系统，2-编码用子系统，0-NULL（此参数只能获取）
            public byte byChan;//子系统通道数（此参数只能获取）
            public byte byLoginType;//注册类型，1-直连，2-DNS，3-花生壳
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_IPADDR struSubSystemIP;/*IP地址（可修改）*/
            public ushort wSubSystemPort;//子系统端口号（可修改）
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes2;

            public NET_DVR_IPADDR struSubSystemIPMask;//子网掩码
            public NET_DVR_IPADDR struGatewayIpAddr;	/* 网关地址*/

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 （此参数只能获取）*/
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sPassword;/*密码（此参数只能获取）*/

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string sDomainName;//域名(可修改)
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string sDnsAddress;/*DNS域名或IP地址*/
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号（此参数只能获取）
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_ALLSUBSYSTEMINFO
        {
            public uint dwSize;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_SUBSYSTEM_NUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_SUBSYSTEMINFO[] struSubSystemInfo;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_LOOPPLAN_SUBCFG
        {
            public uint dwSize;
            public uint dwPoolTime; /*轮询间隔，单位：秒*/
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_CYCLE_CHAN_V30, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_INFO_V30[] struChanConInfo;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_LOOPPLAN_ARRAYCFG
        {
            public uint dwSize;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_LOOPPLANNUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_LOOPPLAN_SUBCFG[] struLoopPlanSubCfg;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_ALARMMODECFG
        {
            public uint dwSize;
            public byte byAlarmMode;//报警触发类型，1-轮询，2-保持 
            public ushort wLoopTime;//轮询时间, 单位：秒 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESYSTEMINFO
        {
            public byte bySerialNum;//子系统序号
            public byte byChan;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESPLITTERINFO
        {
            public uint dwSize;
            public NET_DVR_IPADDR struIP;/*码分器IP地址*/
            public ushort wPort;//码分器端口号
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sPassword;/*密码 */
            public byte byChan;//码分器485号
            public byte by485Port;//485口地址
            public ushort wBaudRate;// 波特率(bps)，0－50，1－75，2－110，3－150，4－300，5－600，6－1200，7－2400，8－4800，9－9600，10－19200， 11－38400，12－57600，13－76800，14－115.2k;
            public byte byDataBit;//数据有几位 0－5位，1－6位，2－7位，3－8位;
            public byte byStopBit;// 停止位：0－1位，1－2位;
            public byte byParity;// 校验：0－无校验；1－奇校验；2－偶校验;
            public byte byFlowControl;// 0－无；1－软流控；2-硬流控
            public ushort wDecoderType;// 解码器类型, 从0开始，与获取的解码器协议列表中的下标相对应
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes2;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESPLITTERCFG
        {
            public uint dwSize;
            public NET_DVR_CODESYSTEMINFO struCodeSubsystemInfo;//编码子系统对应信息
            public NET_DVR_CODESPLITTERINFO struCodeSplitterInfo;//码分器信息
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_ASSOCIATECFG
        {
            public byte byAssociateType;//关联类型，1-报警
            public ushort wAlarmDelay;//报警延时，0－5秒；1－10秒；2－30秒；3－1分钟；4－2分钟；5－5分钟；6－10分钟；
            public byte byAlarmNum;//报警号，具体的值由应用赋，相同的报警赋相同的值
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_DYNAMICDECODE
        {
            public uint dwSize;
            public NET_DVR_ASSOCIATECFG struAssociateCfg;//触发动态解码关联结构
            public NET_DVR_PU_STREAM_CFG struPuStreamCfg;//动态解码结构
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_DECODESCHED
        {
            public NET_DVR_SCHEDTIME struSchedTime;
            public byte byDecodeType;/*0-无，1-轮询解码，2-动态解码*/
            public byte byLoopGroup;//轮询组号
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_PU_STREAM_CFG struDynamicDec;//动态解码
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PLANDECODE
        {
            public uint dwSize;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_DAYS * DECODE_TIMESEGMENT, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_DECODESCHED[] struDecodeSched;//周一作为开始，和9000一致
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byres;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOPLATFORM_ABILITY
        {
            public uint dwSize;
            public byte byCodeSubSystemNums;//编码子系统数量
            public byte byDecodeSubSystemNums;//解码子系统数量
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byWindowMode; /*显示通道支持的窗口模式*/
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int VIDEOPLATFORM_ABILITY = 0x210; //视频综合平台能力集
        /************************************视频综合平台(end)***************************************/

        //SDK类型
        public const int SDK_PLAYMPEG4 = 1;//播放库
        public const int SDK_HCNETSDK = 2;//网络库

        //数据库操作NVR信息
        //数据库操作
        public const int INSERTTYPE = 0;        //代表插入
        public const int MODIFYTYPE = 1;        //代表修改
        public const int DELETETYPE = 2;        //代表删除
        /****************************************日志操作******************************************/
        //操作类型
        public const int DEF_OPE_PREVIEW = 1;   //预览
        public const int DEF_OPE_TALK = 2;  //对讲
        public const int DEF_OPE_SETALARM = 3;   //布防
        public const int DEF_OPE_PTZCTRL = 4;   //云台控制
        public const int DEF_OPE_VIDEOPARAM = 5;   //视频参数设置
        public const int DEF_OPE_PLAYBACK = 6;   //回放
        public const int DEF_OPE_REMOTECFG = 7;   //远程配置
        public const int DEF_OPE_GETSERVSTATE = 8;   //获取设备状态
        public const int DEF_OPE_CHECKTIME = 9;   //校时



        //操作日志内容	
        public const int DEF_OPE_PRE_STARTPREVIEW = 1;   //开始预览
        public const int DEF_OPE_PRE_STOPPREVIEW = 2;   //停止预览
        public const int DEF_OPE_PRE_STRATCYCPLAY = 3;   //开始循环播放
        public const int DEF_OPE_PRE_STOPCYCPLAY = 4;   //停止循环播放
        public const int DEF_OPE_PRE_STARTRECORD = 5;   //开始录像
        public const int DEF_OPE_PRE_STOPRECORD = 6;   //停止录像
        public const int DEF_OPE_PRE_CAPTURE = 7;   //抓图
        public const int DEF_OPE_PRE_OPENSOUND = 8;   //打开声音
        public const int DEF_OPE_PRE_CLOSESOUND = 9;   //关闭声音

        //对讲
        public const int DEF_OPE_TALK_STARTTALK = 1;   //开始对讲
        public const int DEF_OPE_TALK_STOPTALK = 2;   //停止对讲

        public const int DEF_OPE_ALARM_SETALARM = 1;   //布防
        public const int DEF_OPE_ALARM_WITHDRAWALARM = 2;   //撤防

        public const int DEF_OPE_PTZ_PTZCTRL = 1;   //云台控制

        public const int DEF_OPE_VIDEOPARAM_SET = 1;   //视频参数

        //回放
        public const int DEF_OPE_PLAYBACK_LOCALSEARCH = 1;   //本地回放查询文件
        public const int DEF_OPE_PLAYBACK_LOCALPLAY = 2;   //本地回放文件
        public const int DEF_OPE_PLAYBACK_LOCALDOWNLOAD = 3;   //本地回放下载文件
        public const int DEF_OPE_PLAYBACK_REMOTESEARCH = 4;   //远程回放查询文件
        public const int DEF_OPE_PLAYBACK_REMOTEPLAYFILE = 5;   //远程按文件回放
        public const int DEF_OPE_PLAYBACK_REMOTEPLAYTIME = 6;   //远程按时间回放
        public const int DEF_OPE_PLAYBACK_REMOTEDOWNLOAD = 7;   //远程回放下载文件

        public const int DEF_OPE_REMOTE_REMOTECFG = 1;   //远程参数配置

        public const int DEF_OPE_STATE_GETSERVSTATE = 1;//获取设备状态

        public const int DEF_OPE_CHECKT_CHECKTIME = 1;//校时

        //报警类型
        public const int DEF_ALARM_IO = 1;   //信号量报警
        public const int DEF_ALARM_HARDFULL = 2;   //硬盘满报警
        public const int DEF_ALARM_VL = 3;  //视频信号丢失报警
        public const int DEF_ALARM_MV = 4;	 //移动侦测报警
        public const int DEF_ALARM_HARDFORMAT = 5;   //硬盘未格式化报警
        public const int DEF_ALARM_HARDERROR = 6;   //硬盘错报警
        public const int DEF_ALARM_VH = 7;	 //遮挡报警
        public const int DEF_ALARM_NOPATCH = 8;   //制式不匹配报警
        public const int DEF_ALARM_ERRORVISIT = 9;   //非法访问报警
        public const int DEF_ALARM_EXCEPTION = 10;  //巡检异常
        public const int DEF_ALARM_RECERROR = 11;  //巡检异常

        //系统日志类型
        public const int DEF_SYS_LOGIN = 1;   //登陆 
        public const int DEF_SYS_LOGOUT = 2;   //注销
        public const int DEF_SYS_LOCALCFG = 3;   //本地配置

        /****************************************日志操作******************************************/


        public const int NAME_LEN = 32;//用户名长度
        public const int PASSWD_LEN = 16;//密码长度
        public const int MAX_NAMELEN = 16;//DVR本地登陆名
        public const int MAX_RIGHT = 32;//设备支持的权限（1-12表示本地权限，13-32表示远程权限）
        public const int SERIALNO_LEN = 48;//序列号长度
        public const int MACADDR_LEN = 6;//mac地址长度
        public const int MAX_ETHERNET = 2;//设备可配以太网络
        public const int PATHNAME_LEN = 128;//路径长度

        public const int MAX_TIMESEGMENT_V30 = 8;//9000设备最大时间段数
        public const int MAX_TIMESEGMENT = 4;//8000设备最大时间段数

        public const int MAX_SHELTERNUM = 4;//8000设备最大遮挡区域数
        public const int PHONENUMBER_LEN = 32;//pppoe拨号号码最大长度

        public const int MAX_DISKNUM = 16;//8000设备最大硬盘数
        public const int MAX_DISKNUM_V10 = 8;//1.2版本之前版本

        public const int MAX_WINDOW_V30 = 32;//9000设备本地显示最大播放窗口数
        public const int MAX_WINDOW = 16;//8000设备最大硬盘数
        public const int MAX_VGA_V30 = 4;//9000设备最大可接VGA数
        public const int MAX_VGA = 1;//8000设备最大可接VGA数

        public const int MAX_USERNUM_V30 = 32;//9000设备最大用户数
        public const int MAX_USERNUM = 16;//8000设备最大用户数
        public const int MAX_EXCEPTIONNUM_V30 = 32;//9000设备最大异常处理数
        public const int MAX_EXCEPTIONNUM = 16;//8000设备最大异常处理数
        public const int MAX_LINK = 6;//8000设备单通道最大视频流连接数

        public const int MAX_DECPOOLNUM = 4;//单路解码器每个解码通道最大可循环解码数
        public const int MAX_DECNUM = 4;//单路解码器的最大解码通道数（实际只有一个，其他三个保留）
        public const int MAX_TRANSPARENTNUM = 2;//单路解码器可配置最大透明通道数
        public const int MAX_CYCLE_CHAN = 16; //单路解码器最大轮循通道数
        public const int MAX_CYCLE_CHAN_V30 = 64;//最大轮询通道数（扩展）
        public const int MAX_DIRNAME_LENGTH = 80;//最大目录长度
        public const int MAX_WINDOWS = 16;//最大窗口数

        public const int MAX_STRINGNUM_V30 = 8;//9000设备最大OSD字符行数数
        public const int MAX_STRINGNUM = 4;//8000设备最大OSD字符行数数
        public const int MAX_STRINGNUM_EX = 8;//8000定制扩展
        public const int MAX_AUXOUT_V30 = 16;//9000设备最大辅助输出数
        public const int MAX_AUXOUT = 4;//8000设备最大辅助输出数
        public const int MAX_HD_GROUP = 16;//9000设备最大硬盘组数
        public const int MAX_NFS_DISK = 8; //8000设备最大NFS硬盘数

        public const int IW_ESSID_MAX_SIZE = 32;//WIFI的SSID号长度
        public const int IW_ENCODING_TOKEN_MAX = 32;//WIFI密锁最大字节数
        public const int MAX_SERIAL_NUM = 64;//最多支持的透明通道路数
        public const int MAX_DDNS_NUMS = 10;//9000设备最大可配ddns数
        public const int MAX_EMAIL_ADDR_LEN = 48;//最大email地址长度
        public const int MAX_EMAIL_PWD_LEN = 32;//最大email密码长度

        public const int MAXPROGRESS = 100;//回放时的最大百分率
        public const int MAX_SERIALNUM = 2;//8000设备支持的串口数 1-232， 2-485
        public const int CARDNUM_LEN = 20;//卡号长度
        public const int MAX_VIDEOOUT_V30 = 4;//9000设备的视频输出数
        public const int MAX_VIDEOOUT = 2;//8000设备的视频输出数

        public const int MAX_PRESET_V30 = 256;// 9000设备支持的云台预置点数
        public const int MAX_TRACK_V30 = 256;// 9000设备支持的云台轨迹数
        public const int MAX_CRUISE_V30 = 256;// 9000设备支持的云台巡航数
        public const int MAX_PRESET = 128;// 8000设备支持的云台预置点数 
        public const int MAX_TRACK = 128;// 8000设备支持的云台轨迹数
        public const int MAX_CRUISE = 128;// 8000设备支持的云台巡航数 

        public const int CRUISE_MAX_PRESET_NUMS = 32;// 一条巡航最多的巡航点 

        public const int MAX_SERIAL_PORT = 8;//9000设备支持232串口数
        public const int MAX_PREVIEW_MODE = 8;// 设备支持最大预览模式数目 1画面,4画面,9画面,16画面.... 
        public const int MAX_MATRIXOUT = 16;// 最大模拟矩阵输出个数 
        public const int LOG_INFO_LEN = 11840; // 日志附加信息 
        public const int DESC_LEN = 16;// 云台描述字符串长度 
        public const int PTZ_PROTOCOL_NUM = 200;// 9000最大支持的云台协议数 

        public const int MAX_AUDIO = 1;//8000语音对讲通道数
        public const int MAX_AUDIO_V30 = 2;//9000语音对讲通道数
        public const int MAX_CHANNUM = 16;//8000设备最大通道数
        public const int MAX_ALARMIN = 16;//8000设备最大报警输入数
        public const int MAX_ALARMOUT = 4;//8000设备最大报警输出数
        //9000 IPC接入
        public const int MAX_ANALOG_CHANNUM = 32;//最大32个模拟通道
        public const int MAX_ANALOG_ALARMOUT = 32; //最大32路模拟报警输出 
        public const int MAX_ANALOG_ALARMIN = 32;//最大32路模拟报警输入

        public const int MAX_IP_DEVICE = 32;//允许接入的最大IP设备数
        public const int MAX_IP_CHANNEL = 32;//允许加入的最多IP通道数
        public const int MAX_IP_ALARMIN = 128;//允许加入的最多报警输入数
        public const int MAX_IP_ALARMOUT = 64;//允许加入的最多报警输出数

        //SDK_V31 ATM
        public const int MAX_ATM_NUM = 1;
        public const int MAX_ACTION_TYPE = 12;
        public const int ATM_FRAMETYPE_NUM = 4;
        public const int MAX_ATM_PROTOCOL_NUM = 1025;
        public const int ATM_PROTOCOL_SORT = 4;
        public const int ATM_DESC_LEN = 32;
        // SDK_V31 ATM

        /* 最大支持的通道数 最大模拟加上最大IP支持 */
        public const int MAX_CHANNUM_V30 = MAX_ANALOG_CHANNUM + MAX_IP_CHANNEL;//64
        public const int MAX_ALARMOUT_V30 = MAX_ANALOG_ALARMOUT + MAX_IP_ALARMOUT;//96
        public const int MAX_ALARMIN_V30 = MAX_ANALOG_ALARMIN + MAX_IP_ALARMIN;//160

        public const int MAX_INTERVAL_NUM = 4;

        //码流连接方式
        public const int NORMALCONNECT = 1;
        public const int MEDIACONNECT = 2;

        //设备型号(大类)
        public const int HCDVR = 1;
        public const int MEDVR = 2;
        public const int PCDVR = 3;
        public const int HC_9000 = 4;
        public const int HF_I = 5;
        public const int PCNVR = 6;
        public const int HC_76NVR = 8;

        //NVR类型
        public const int DS8000HC_NVR = 0;
        public const int DS9000HC_NVR = 1;
        public const int DS8000ME_NVR = 2;

        /*******************全局错误码 begin**********************/
        public const int NET_DVR_NOERROR = 0;//没有错误
        public const int NET_DVR_PASSWORD_ERROR = 1;//用户名密码错误
        public const int NET_DVR_NOENOUGHPRI = 2;//权限不足
        public const int NET_DVR_NOINIT = 3;//没有初始化
        public const int NET_DVR_CHANNEL_ERROR = 4;//通道号错误
        public const int NET_DVR_OVER_MAXLINK = 5;//连接到DVR的客户端个数超过最大
        public const int NET_DVR_VERSIONNOMATCH = 6;//版本不匹配
        public const int NET_DVR_NETWORK_FAIL_CONNECT = 7;//连接服务器失败
        public const int NET_DVR_NETWORK_SEND_ERROR = 8;//向服务器发送失败
        public const int NET_DVR_NETWORK_RECV_ERROR = 9;//从服务器接收数据失败
        public const int NET_DVR_NETWORK_RECV_TIMEOUT = 10;//从服务器接收数据超时
        public const int NET_DVR_NETWORK_ERRORDATA = 11;//传送的数据有误
        public const int NET_DVR_ORDER_ERROR = 12;//调用次序错误
        public const int NET_DVR_OPERNOPERMIT = 13;//无此权限
        public const int NET_DVR_COMMANDTIMEOUT = 14;//DVR命令执行超时
        public const int NET_DVR_ERRORSERIALPORT = 15;//串口号错误
        public const int NET_DVR_ERRORALARMPORT = 16;//报警端口错误
        public const int NET_DVR_PARAMETER_ERROR = 17;//参数错误
        public const int NET_DVR_CHAN_EXCEPTION = 18;//服务器通道处于错误状态
        public const int NET_DVR_NODISK = 19;//没有硬盘
        public const int NET_DVR_ERRORDISKNUM = 20;//硬盘号错误
        public const int NET_DVR_DISK_FULL = 21;//服务器硬盘满
        public const int NET_DVR_DISK_ERROR = 22;//服务器硬盘出错
        public const int NET_DVR_NOSUPPORT = 23;//服务器不支持
        public const int NET_DVR_BUSY = 24;//服务器忙
        public const int NET_DVR_MODIFY_FAIL = 25;//服务器修改不成功
        public const int NET_DVR_PASSWORD_FORMAT_ERROR = 26;//密码输入格式不正确
        public const int NET_DVR_DISK_FORMATING = 27;//硬盘正在格式化，不能启动操作
        public const int NET_DVR_DVRNORESOURCE = 28;//DVR资源不足
        public const int NET_DVR_DVROPRATEFAILED = 29;//DVR操作失败
        public const int NET_DVR_OPENHOSTSOUND_FAIL = 30;//打开PC声音失败
        public const int NET_DVR_DVRVOICEOPENED = 31;//服务器语音对讲被占用
        public const int NET_DVR_TIMEINPUTERROR = 32;//时间输入不正确
        public const int NET_DVR_NOSPECFILE = 33;//回放时服务器没有指定的文件
        public const int NET_DVR_CREATEFILE_ERROR = 34;//创建文件出错
        public const int NET_DVR_FILEOPENFAIL = 35;//打开文件出错
        public const int NET_DVR_OPERNOTFINISH = 36; //上次的操作还没有完成
        public const int NET_DVR_GETPLAYTIMEFAIL = 37;//获取当前播放的时间出错
        public const int NET_DVR_PLAYFAIL = 38;//播放出错
        public const int NET_DVR_FILEFORMAT_ERROR = 39;//文件格式不正确
        public const int NET_DVR_DIR_ERROR = 40;//路径错误
        public const int NET_DVR_ALLOC_RESOURCE_ERROR = 41;//资源分配错误
        public const int NET_DVR_AUDIO_MODE_ERROR = 42;//声卡模式错误
        public const int NET_DVR_NOENOUGH_BUF = 43;//缓冲区太小
        public const int NET_DVR_CREATESOCKET_ERROR = 44;//创建SOCKET出错
        public const int NET_DVR_SETSOCKET_ERROR = 45;//设置SOCKET出错
        public const int NET_DVR_MAX_NUM = 46;//个数达到最大
        public const int NET_DVR_USERNOTEXIST = 47;//用户不存在
        public const int NET_DVR_WRITEFLASHERROR = 48;//写FLASH出错
        public const int NET_DVR_UPGRADEFAIL = 49;//DVR升级失败
        public const int NET_DVR_CARDHAVEINIT = 50;//解码卡已经初始化过
        public const int NET_DVR_PLAYERFAILED = 51;//调用播放库中某个函数失败
        public const int NET_DVR_MAX_USERNUM = 52;//设备端用户数达到最大
        public const int NET_DVR_GETLOCALIPANDMACFAIL = 53;//获得客户端的IP地址或物理地址失败
        public const int NET_DVR_NOENCODEING = 54;//该通道没有编码
        public const int NET_DVR_IPMISMATCH = 55;//IP地址不匹配
        public const int NET_DVR_MACMISMATCH = 56;//MAC地址不匹配
        public const int NET_DVR_UPGRADELANGMISMATCH = 57;//升级文件语言不匹配
        public const int NET_DVR_MAX_PLAYERPORT = 58;//播放器路数达到最大
        public const int NET_DVR_NOSPACEBACKUP = 59;//备份设备中没有足够空间进行备份
        public const int NET_DVR_NODEVICEBACKUP = 60;//没有找到指定的备份设备
        public const int NET_DVR_PICTURE_BITS_ERROR = 61;//图像素位数不符，限24色
        public const int NET_DVR_PICTURE_DIMENSION_ERROR = 62;//图片高*宽超限， 限128*256
        public const int NET_DVR_PICTURE_SIZ_ERROR = 63;//图片大小超限，限100K
        public const int NET_DVR_LOADPLAYERSDKFAILED = 64;//载入当前目录下Player Sdk出错
        public const int NET_DVR_LOADPLAYERSDKPROC_ERROR = 65;//找不到Player Sdk中某个函数入口
        public const int NET_DVR_LOADDSSDKFAILED = 66;//载入当前目录下DSsdk出错
        public const int NET_DVR_LOADDSSDKPROC_ERROR = 67;//找不到DsSdk中某个函数入口
        public const int NET_DVR_DSSDK_ERROR = 68;//调用硬解码库DsSdk中某个函数失败
        public const int NET_DVR_VOICEMONOPOLIZE = 69;//声卡被独占
        public const int NET_DVR_JOINMULTICASTFAILED = 70;//加入多播组失败
        public const int NET_DVR_CREATEDIR_ERROR = 71;//建立日志文件目录失败
        public const int NET_DVR_BINDSOCKET_ERROR = 72;//绑定套接字失败
        public const int NET_DVR_SOCKETCLOSE_ERROR = 73;//socket连接中断，此错误通常是由于连接中断或目的地不可达
        public const int NET_DVR_USERID_ISUSING = 74;//注销时用户ID正在进行某操作
        public const int NET_DVR_SOCKETLISTEN_ERROR = 75;//监听失败
        public const int NET_DVR_PROGRAM_EXCEPTION = 76;//程序异常
        public const int NET_DVR_WRITEFILE_FAILED = 77;//写文件失败
        public const int NET_DVR_FORMAT_READONLY = 78;//禁止格式化只读硬盘
        public const int NET_DVR_WITHSAMEUSERNAME = 79;//用户配置结构中存在相同的用户名
        public const int NET_DVR_DEVICETYPE_ERROR = 80;//导入参数时设备型号不匹配
        public const int NET_DVR_LANGUAGE_ERROR = 81;//导入参数时语言不匹配
        public const int NET_DVR_PARAVERSION_ERROR = 82;//导入参数时软件版本不匹配
        public const int NET_DVR_IPCHAN_NOTALIVE = 83; //预览时外接IP通道不在线
        public const int NET_DVR_RTSP_SDK_ERROR = 84;//加载高清IPC通讯库StreamTransClient.dll失败
        public const int NET_DVR_CONVERT_SDK_ERROR = 85;//加载转码库失败
        public const int NET_DVR_IPC_COUNT_OVERFLOW = 86;//超出最大的ip接入通道数

        public const int NET_PLAYM4_NOERROR = 500;//no error
        public const int NET_PLAYM4_PARA_OVER = 501;//input parameter is invalid
        public const int NET_PLAYM4_ORDER_ERROR = 502;//The order of the function to be called is error
        public const int NET_PLAYM4_TIMER_ERROR = 503;//Create multimedia clock failed
        public const int NET_PLAYM4_DEC_VIDEO_ERROR = 504;//Decode video data failed
        public const int NET_PLAYM4_DEC_AUDIO_ERROR = 505;//Decode audio data failed
        public const int NET_PLAYM4_ALLOC_MEMORY_ERROR = 506;//Allocate memory failed
        public const int NET_PLAYM4_OPEN_FILE_ERROR = 507;//Open the file failed
        public const int NET_PLAYM4_CREATE_OBJ_ERROR = 508;//Create thread or event failed
        public const int NET_PLAYM4_CREATE_DDRAW_ERROR = 509;//Create DirectDraw object failed
        public const int NET_PLAYM4_CREATE_OFFSCREEN_ERROR = 510;//failed when creating off-screen surface
        public const int NET_PLAYM4_BUF_OVER = 511;//buffer is overflow
        public const int NET_PLAYM4_CREATE_SOUND_ERROR = 512;//failed when creating audio device
        public const int NET_PLAYM4_SET_VOLUME_ERROR = 513;//Set volume failed
        public const int NET_PLAYM4_SUPPORT_FILE_ONLY = 514;//The function only support play file
        public const int NET_PLAYM4_SUPPORT_STREAM_ONLY = 515;//The function only support play stream
        public const int NET_PLAYM4_SYS_NOT_SUPPORT = 516;//System not support
        public const int NET_PLAYM4_FILEHEADER_UNKNOWN = 517;//No file header
        public const int NET_PLAYM4_VERSION_INCORRECT = 518;//The version of decoder and encoder is not adapted
        public const int NET_PALYM4_INIT_DECODER_ERROR = 519;//Initialize decoder failed
        public const int NET_PLAYM4_CHECK_FILE_ERROR = 520;//The file data is unknown
        public const int NET_PLAYM4_INIT_TIMER_ERROR = 521;//Initialize multimedia clock failed
        public const int NET_PLAYM4_BLT_ERROR = 522;//Blt failed
        public const int NET_PLAYM4_UPDATE_ERROR = 523;//Update failed
        public const int NET_PLAYM4_OPEN_FILE_ERROR_MULTI = 524;//openfile error, streamtype is multi
        public const int NET_PLAYM4_OPEN_FILE_ERROR_VIDEO = 525;//openfile error, streamtype is video
        public const int NET_PLAYM4_JPEG_COMPRESS_ERROR = 526;//JPEG compress error
        public const int NET_PLAYM4_EXTRACT_NOT_SUPPORT = 527;//Don't support the version of this file
        public const int NET_PLAYM4_EXTRACT_DATA_ERROR = 528;//extract video data failed
        /*******************全局错误码 end**********************/

        /*************************************************
        NET_DVR_IsSupport()返回值
        1－9位分别表示以下信息（位与是TRUE)表示支持；
        **************************************************/
        public const int NET_DVR_SUPPORT_DDRAW = 1;//支持DIRECTDRAW，如果不支持，则播放器不能工作
        public const int NET_DVR_SUPPORT_BLT = 2;//显卡支持BLT操作，如果不支持，则播放器不能工作
        public const int NET_DVR_SUPPORT_BLTFOURCC = 4;//显卡BLT支持颜色转换，如果不支持，播放器会用软件方法作RGB转换
        public const int NET_DVR_SUPPORT_BLTSHRINKX = 8;//显卡BLT支持X轴缩小；如果不支持，系统会用软件方法转换
        public const int NET_DVR_SUPPORT_BLTSHRINKY = 16;//显卡BLT支持Y轴缩小；如果不支持，系统会用软件方法转换
        public const int NET_DVR_SUPPORT_BLTSTRETCHX = 32;//显卡BLT支持X轴放大；如果不支持，系统会用软件方法转换
        public const int NET_DVR_SUPPORT_BLTSTRETCHY = 64;//显卡BLT支持Y轴放大；如果不支持，系统会用软件方法转换
        public const int NET_DVR_SUPPORT_SSE = 128;//CPU支持SSE指令，Intel Pentium3以上支持SSE指令
        public const int NET_DVR_SUPPORT_MMX = 256;//CPU支持MMX指令集，Intel Pentium3以上支持SSE指令

        /**********************云台控制命令 begin*************************/
        public const int LIGHT_PWRON = 2;// 接通灯光电源
        public const int WIPER_PWRON = 3;// 接通雨刷开关 
        public const int FAN_PWRON = 4;// 接通风扇开关
        public const int HEATER_PWRON = 5;// 接通加热器开关
        public const int AUX_PWRON1 = 6;// 接通辅助设备开关
        public const int AUX_PWRON2 = 7;// 接通辅助设备开关 
        public const int SET_PRESET = 8;// 设置预置点 
        public const int CLE_PRESET = 9;// 清除预置点 

        public const int ZOOM_IN = 11;// 焦距以速度SS变大(倍率变大)
        public const int ZOOM_OUT = 12;// 焦距以速度SS变小(倍率变小)
        public const int FOCUS_NEAR = 13;// 焦点以速度SS前调 
        public const int FOCUS_FAR = 14;// 焦点以速度SS后调
        public const int IRIS_OPEN = 15;// 光圈以速度SS扩大
        public const int IRIS_CLOSE = 16;// 光圈以速度SS缩小 

        public const int TILT_UP = 21;/* 云台以SS的速度上仰 */
        public const int TILT_DOWN = 22;/* 云台以SS的速度下俯 */
        public const int PAN_LEFT = 23;/* 云台以SS的速度左转 */
        public const int PAN_RIGHT = 24;/* 云台以SS的速度右转 */
        public const int UP_LEFT = 25;/* 云台以SS的速度上仰和左转 */
        public const int UP_RIGHT = 26;/* 云台以SS的速度上仰和右转 */
        public const int DOWN_LEFT = 27;/* 云台以SS的速度下俯和左转 */
        public const int DOWN_RIGHT = 28;/* 云台以SS的速度下俯和右转 */
        public const int PAN_AUTO = 29;/* 云台以SS的速度左右自动扫描 */

        public const int FILL_PRE_SEQ = 30;/* 将预置点加入巡航序列 */
        public const int SET_SEQ_DWELL = 31;/* 设置巡航点停顿时间 */
        public const int SET_SEQ_SPEED = 32;/* 设置巡航速度 */
        public const int CLE_PRE_SEQ = 33;/* 将预置点从巡航序列中删除 */
        public const int STA_MEM_CRUISE = 34;/* 开始记录轨迹 */
        public const int STO_MEM_CRUISE = 35;/* 停止记录轨迹 */
        public const int RUN_CRUISE = 36;/* 开始轨迹 */
        public const int RUN_SEQ = 37;/* 开始巡航 */
        public const int STOP_SEQ = 38;/* 停止巡航 */
        public const int GOTO_PRESET = 39;/* 快球转到预置点 */
        /**********************云台控制命令 end*************************/

        /*************************************************
        回放时播放控制命令宏定义 
        NET_DVR_PlayBackControl
        NET_DVR_PlayControlLocDisplay
        NET_DVR_DecPlayBackCtrl的宏定义
        具体支持查看函数说明和代码
        **************************************************/
        public const int NET_DVR_PLAYSTART = 1;//开始播放
        public const int NET_DVR_PLAYSTOP = 2;//停止播放
        public const int NET_DVR_PLAYPAUSE = 3;//暂停播放
        public const int NET_DVR_PLAYRESTART = 4;//恢复播放
        public const int NET_DVR_PLAYFAST = 5;//快放
        public const int NET_DVR_PLAYSLOW = 6;//慢放
        public const int NET_DVR_PLAYNORMAL = 7;//正常速度
        public const int NET_DVR_PLAYFRAME = 8;//单帧放
        public const int NET_DVR_PLAYSTARTAUDIO = 9;//打开声音
        public const int NET_DVR_PLAYSTOPAUDIO = 10;//关闭声音
        public const int NET_DVR_PLAYAUDIOVOLUME = 11;//调节音量
        public const int NET_DVR_PLAYSETPOS = 12;//改变文件回放的进度
        public const int NET_DVR_PLAYGETPOS = 13;//获取文件回放的进度
        public const int NET_DVR_PLAYGETTIME = 14;//获取当前已经播放的时间(按文件回放的时候有效)
        public const int NET_DVR_PLAYGETFRAME = 15;//获取当前已经播放的帧数(按文件回放的时候有效)
        public const int NET_DVR_GETTOTALFRAMES = 16;//获取当前播放文件总的帧数(按文件回放的时候有效)
        public const int NET_DVR_GETTOTALTIME = 17;//获取当前播放文件总的时间(按文件回放的时候有效)
        public const int NET_DVR_THROWBFRAME = 20;//丢B帧
        public const int NET_DVR_SETSPEED = 24;//设置码流速度
        public const int NET_DVR_KEEPALIVE = 25;//保持与设备的心跳(如果回调阻塞，建议2秒发送一次)

        //远程按键定义如下：
        /* key value send to CONFIG program */
        public const int KEY_CODE_1 = 1;
        public const int KEY_CODE_2 = 2;
        public const int KEY_CODE_3 = 3;
        public const int KEY_CODE_4 = 4;
        public const int KEY_CODE_5 = 5;
        public const int KEY_CODE_6 = 6;
        public const int KEY_CODE_7 = 7;
        public const int KEY_CODE_8 = 8;
        public const int KEY_CODE_9 = 9;
        public const int KEY_CODE_0 = 10;
        public const int KEY_CODE_POWER = 11;
        public const int KEY_CODE_MENU = 12;
        public const int KEY_CODE_ENTER = 13;
        public const int KEY_CODE_CANCEL = 14;
        public const int KEY_CODE_UP = 15;
        public const int KEY_CODE_DOWN = 16;
        public const int KEY_CODE_LEFT = 17;
        public const int KEY_CODE_RIGHT = 18;
        public const int KEY_CODE_EDIT = 19;
        public const int KEY_CODE_ADD = 20;
        public const int KEY_CODE_MINUS = 21;
        public const int KEY_CODE_PLAY = 22;
        public const int KEY_CODE_REC = 23;
        public const int KEY_CODE_PAN = 24;
        public const int KEY_CODE_M = 25;
        public const int KEY_CODE_A = 26;
        public const int KEY_CODE_F1 = 27;
        public const int KEY_CODE_F2 = 28;

        /* for PTZ control */
        public const int KEY_PTZ_UP_START = KEY_CODE_UP;
        public const int KEY_PTZ_UP_STOP = 32;

        public const int KEY_PTZ_DOWN_START = KEY_CODE_DOWN;
        public const int KEY_PTZ_DOWN_STOP = 33;


        public const int KEY_PTZ_LEFT_START = KEY_CODE_LEFT;
        public const int KEY_PTZ_LEFT_STOP = 34;

        public const int KEY_PTZ_RIGHT_START = KEY_CODE_RIGHT;
        public const int KEY_PTZ_RIGHT_STOP = 35;

        public const int KEY_PTZ_AP1_START = KEY_CODE_EDIT;/* 光圈+ */
        public const int KEY_PTZ_AP1_STOP = 36;

        public const int KEY_PTZ_AP2_START = KEY_CODE_PAN;/* 光圈- */
        public const int KEY_PTZ_AP2_STOP = 37;

        public const int KEY_PTZ_FOCUS1_START = KEY_CODE_A;/* 聚焦+ */
        public const int KEY_PTZ_FOCUS1_STOP = 38;

        public const int KEY_PTZ_FOCUS2_START = KEY_CODE_M;/* 聚焦- */
        public const int KEY_PTZ_FOCUS2_STOP = 39;

        public const int KEY_PTZ_B1_START = 40;/* 变倍+ */
        public const int KEY_PTZ_B1_STOP = 41;

        public const int KEY_PTZ_B2_START = 42;/* 变倍- */
        public const int KEY_PTZ_B2_STOP = 43;

        //9000新增
        public const int KEY_CODE_11 = 44;
        public const int KEY_CODE_12 = 45;
        public const int KEY_CODE_13 = 46;
        public const int KEY_CODE_14 = 47;
        public const int KEY_CODE_15 = 48;
        public const int KEY_CODE_16 = 49;

        /*************************参数配置命令 begin*******************************/
        //用于NET_DVR_SetDVRConfig和NET_DVR_GetDVRConfig,注意其对应的配置结构
        public const int NET_DVR_GET_DEVICECFG = 100;//获取设备参数
        public const int NET_DVR_SET_DEVICECFG = 101;//设置设备参数
        public const int NET_DVR_GET_NETCFG = 102;//获取网络参数
        public const int NET_DVR_SET_NETCFG = 103;//设置网络参数
        public const int NET_DVR_GET_PICCFG = 104;//获取图象参数
        public const int NET_DVR_SET_PICCFG = 105;//设置图象参数
        public const int NET_DVR_GET_COMPRESSCFG = 106;//获取压缩参数
        public const int NET_DVR_SET_COMPRESSCFG = 107;//设置压缩参数
        public const int NET_DVR_GET_RECORDCFG = 108;//获取录像时间参数
        public const int NET_DVR_SET_RECORDCFG = 109;//设置录像时间参数
        public const int NET_DVR_GET_DECODERCFG = 110;//获取解码器参数
        public const int NET_DVR_SET_DECODERCFG = 111;//设置解码器参数
        public const int NET_DVR_GET_RS232CFG = 112;//获取232串口参数
        public const int NET_DVR_SET_RS232CFG = 113;//设置232串口参数
        public const int NET_DVR_GET_ALARMINCFG = 114;//获取报警输入参数
        public const int NET_DVR_SET_ALARMINCFG = 115;//设置报警输入参数
        public const int NET_DVR_GET_ALARMOUTCFG = 116;//获取报警输出参数
        public const int NET_DVR_SET_ALARMOUTCFG = 117;//设置报警输出参数
        public const int NET_DVR_GET_TIMECFG = 118;//获取DVR时间
        public const int NET_DVR_SET_TIMECFG = 119;//设置DVR时间
        public const int NET_DVR_GET_PREVIEWCFG = 120;//获取预览参数
        public const int NET_DVR_SET_PREVIEWCFG = 121;//设置预览参数
        public const int NET_DVR_GET_VIDEOOUTCFG = 122;//获取视频输出参数
        public const int NET_DVR_SET_VIDEOOUTCFG = 123;//设置视频输出参数
        public const int NET_DVR_GET_USERCFG = 124;//获取用户参数
        public const int NET_DVR_SET_USERCFG = 125;//设置用户参数
        public const int NET_DVR_GET_EXCEPTIONCFG = 126;//获取异常参数
        public const int NET_DVR_SET_EXCEPTIONCFG = 127;//设置异常参数
        public const int NET_DVR_GET_ZONEANDDST = 128;//获取时区和夏时制参数
        public const int NET_DVR_SET_ZONEANDDST = 129;//设置时区和夏时制参数
        public const int NET_DVR_GET_SHOWSTRING = 130;//获取叠加字符参数
        public const int NET_DVR_SET_SHOWSTRING = 131;//设置叠加字符参数
        public const int NET_DVR_GET_EVENTCOMPCFG = 132;//获取事件触发录像参数
        public const int NET_DVR_SET_EVENTCOMPCFG = 133;//设置事件触发录像参数

        public const int NET_DVR_GET_AUXOUTCFG = 140;//获取报警触发辅助输出设置(HS设备辅助输出2006-02-28)
        public const int NET_DVR_SET_AUXOUTCFG = 141;//设置报警触发辅助输出设置(HS设备辅助输出2006-02-28)
        public const int NET_DVR_GET_PREVIEWCFG_AUX = 142;//获取-s系列双输出预览参数(-s系列双输出2006-04-13)
        public const int NET_DVR_SET_PREVIEWCFG_AUX = 143;//设置-s系列双输出预览参数(-s系列双输出2006-04-13)

        public const int NET_DVR_GET_PICCFG_EX = 200;//获取图象参数(SDK_V14扩展命令)
        public const int NET_DVR_SET_PICCFG_EX = 201;//设置图象参数(SDK_V14扩展命令)
        public const int NET_DVR_GET_USERCFG_EX = 202;//获取用户参数(SDK_V15扩展命令)
        public const int NET_DVR_SET_USERCFG_EX = 203;//设置用户参数(SDK_V15扩展命令)
        public const int NET_DVR_GET_COMPRESSCFG_EX = 204;//获取压缩参数(SDK_V15扩展命令2006-05-15)
        public const int NET_DVR_SET_COMPRESSCFG_EX = 205;//设置压缩参数(SDK_V15扩展命令2006-05-15)

        public const int NET_DVR_GET_NETAPPCFG = 222;//获取网络应用参数 NTP/DDNS/EMAIL
        public const int NET_DVR_SET_NETAPPCFG = 223;//设置网络应用参数 NTP/DDNS/EMAIL
        public const int NET_DVR_GET_NTPCFG = 224;//获取网络应用参数 NTP
        public const int NET_DVR_SET_NTPCFG = 225;//设置网络应用参数 NTP
        public const int NET_DVR_GET_DDNSCFG = 226;//获取网络应用参数 DDNS
        public const int NET_DVR_SET_DDNSCFG = 227;//设置网络应用参数 DDNS
        //对应NET_DVR_EMAILPARA
        public const int NET_DVR_GET_EMAILCFG = 228;//获取网络应用参数 EMAIL
        public const int NET_DVR_SET_EMAILCFG = 229;//设置网络应用参数 EMAIL

        public const int NET_DVR_GET_NFSCFG = 230;/* NFS disk config */
        public const int NET_DVR_SET_NFSCFG = 231;/* NFS disk config */

        public const int NET_DVR_GET_SHOWSTRING_EX = 238;//获取叠加字符参数扩展(支持8条字符)
        public const int NET_DVR_SET_SHOWSTRING_EX = 239;//设置叠加字符参数扩展(支持8条字符)
        public const int NET_DVR_GET_NETCFG_OTHER = 244;//获取网络参数
        public const int NET_DVR_SET_NETCFG_OTHER = 245;//设置网络参数

        //对应NET_DVR_EMAILCFG结构
        public const int NET_DVR_GET_EMAILPARACFG = 250;//Get EMAIL parameters
        public const int NET_DVR_SET_EMAILPARACFG = 251;//Setup EMAIL parameters

        public const int NET_DVR_GET_DDNSCFG_EX = 274;//获取扩展DDNS参数
        public const int NET_DVR_SET_DDNSCFG_EX = 275;//设置扩展DDNS参数

        public const int NET_DVR_SET_PTZPOS = 292;//云台设置PTZ位置
        public const int NET_DVR_GET_PTZPOS = 293;//云台获取PTZ位置
        public const int NET_DVR_GET_PTZSCOPE = 294;//云台获取PTZ范围

        /***************************DS9000新增命令(_V30) begin *****************************/
        //网络(NET_DVR_NETCFG_V30结构)
        public const int NET_DVR_GET_NETCFG_V30 = 1000;//获取网络参数
        public const int NET_DVR_SET_NETCFG_V30 = 1001;//设置网络参数

        //图象(NET_DVR_PICCFG_V30结构)
        public const int NET_DVR_GET_PICCFG_V30 = 1002;//获取图象参数
        public const int NET_DVR_SET_PICCFG_V30 = 1003;//设置图象参数

        //录像时间(NET_DVR_RECORD_V30结构)
        public const int NET_DVR_GET_RECORDCFG_V30 = 1004;//获取录像参数
        public const int NET_DVR_SET_RECORDCFG_V30 = 1005;//设置录像参数

        //用户(NET_DVR_USER_V30结构)
        public const int NET_DVR_GET_USERCFG_V30 = 1006;//获取用户参数
        public const int NET_DVR_SET_USERCFG_V30 = 1007;//设置用户参数

        //9000DDNS参数配置(NET_DVR_DDNSPARA_V30结构)
        public const int NET_DVR_GET_DDNSCFG_V30 = 1010;//获取DDNS(9000扩展)
        public const int NET_DVR_SET_DDNSCFG_V30 = 1011;//设置DDNS(9000扩展)

        //EMAIL功能(NET_DVR_EMAILCFG_V30结构)
        public const int NET_DVR_GET_EMAILCFG_V30 = 1012;//获取EMAIL参数 
        public const int NET_DVR_SET_EMAILCFG_V30 = 1013;//设置EMAIL参数 

        //巡航参数 (NET_DVR_CRUISE_PARA结构)
        public const int NET_DVR_GET_CRUISE = 1020;
        public const int NET_DVR_SET_CRUISE = 1021;

        //报警输入结构参数 (NET_DVR_ALARMINCFG_V30结构)
        public const int NET_DVR_GET_ALARMINCFG_V30 = 1024;
        public const int NET_DVR_SET_ALARMINCFG_V30 = 1025;

        //报警输出结构参数 (NET_DVR_ALARMOUTCFG_V30结构)
        public const int NET_DVR_GET_ALARMOUTCFG_V30 = 1026;
        public const int NET_DVR_SET_ALARMOUTCFG_V30 = 1027;

        //视频输出结构参数 (NET_DVR_VIDEOOUT_V30结构)
        public const int NET_DVR_GET_VIDEOOUTCFG_V30 = 1028;
        public const int NET_DVR_SET_VIDEOOUTCFG_V30 = 1029;

        //叠加字符结构参数 (NET_DVR_SHOWSTRING_V30结构)
        public const int NET_DVR_GET_SHOWSTRING_V30 = 1030;
        public const int NET_DVR_SET_SHOWSTRING_V30 = 1031;

        //异常结构参数 (NET_DVR_EXCEPTION_V30结构)
        public const int NET_DVR_GET_EXCEPTIONCFG_V30 = 1034;
        public const int NET_DVR_SET_EXCEPTIONCFG_V30 = 1035;

        //串口232结构参数 (NET_DVR_RS232CFG_V30结构)
        public const int NET_DVR_GET_RS232CFG_V30 = 1036;
        public const int NET_DVR_SET_RS232CFG_V30 = 1037;

        //网络硬盘接入结构参数 (NET_DVR_NET_DISKCFG结构)
        public const int NET_DVR_GET_NET_DISKCFG = 1038;//网络硬盘接入获取
        public const int NET_DVR_SET_NET_DISKCFG = 1039;//网络硬盘接入设置

        //压缩参数 (NET_DVR_COMPRESSIONCFG_V30结构)
        public const int NET_DVR_GET_COMPRESSCFG_V30 = 1040;
        public const int NET_DVR_SET_COMPRESSCFG_V30 = 1041;

        //获取485解码器参数 (NET_DVR_DECODERCFG_V30结构)
        public const int NET_DVR_GET_DECODERCFG_V30 = 1042;//获取解码器参数
        public const int NET_DVR_SET_DECODERCFG_V30 = 1043;//设置解码器参数

        //获取预览参数 (NET_DVR_PREVIEWCFG_V30结构)
        public const int NET_DVR_GET_PREVIEWCFG_V30 = 1044;//获取预览参数
        public const int NET_DVR_SET_PREVIEWCFG_V30 = 1045;//设置预览参数

        //辅助预览参数 (NET_DVR_PREVIEWCFG_AUX_V30结构)
        public const int NET_DVR_GET_PREVIEWCFG_AUX_V30 = 1046;//获取辅助预览参数
        public const int NET_DVR_SET_PREVIEWCFG_AUX_V30 = 1047;//设置辅助预览参数

        //IP接入配置参数 （NET_DVR_IPPARACFG结构）
        public const int NET_DVR_GET_IPPARACFG = 1048; //获取IP接入配置信息 
        public const int NET_DVR_SET_IPPARACFG = 1049;//设置IP接入配置信息

        //IP报警输入接入配置参数 （NET_DVR_IPALARMINCFG结构）
        public const int NET_DVR_GET_IPALARMINCFG = 1050; //获取IP报警输入接入配置信息 
        public const int NET_DVR_SET_IPALARMINCFG = 1051; //设置IP报警输入接入配置信息

        //IP报警输出接入配置参数 （NET_DVR_IPALARMOUTCFG结构）
        public const int NET_DVR_GET_IPALARMOUTCFG = 1052;//获取IP报警输出接入配置信息 
        public const int NET_DVR_SET_IPALARMOUTCFG = 1053;//设置IP报警输出接入配置信息

        //硬盘管理的参数获取 (NET_DVR_HDCFG结构)
        public const int NET_DVR_GET_HDCFG = 1054;//获取硬盘管理配置参数
        public const int NET_DVR_SET_HDCFG = 1055;//设置硬盘管理配置参数

        //盘组管理的参数获取 (NET_DVR_HDGROUP_CFG结构)
        public const int NET_DVR_GET_HDGROUP_CFG = 1056;//获取盘组管理配置参数
        public const int NET_DVR_SET_HDGROUP_CFG = 1057;//设置盘组管理配置参数

        //设备编码类型配置(NET_DVR_COMPRESSION_AUDIO结构)
        public const int NET_DVR_GET_COMPRESSCFG_AUD = 1058;//获取设备语音对讲编码参数
        public const int NET_DVR_SET_COMPRESSCFG_AUD = 1059;//设置设备语音对讲编码参数

        //IP接入配置参数 （NET_DVR_IPPARACFG_V31结构）
        public const int NET_DVR_GET_IPPARACFG_V31 = 1060;//获取IP接入配置信息 
        public const int NET_DVR_SET_IPPARACFG_V31 = 1061; //设置IP接入配置信息

        //远程控制命令
        public const int NET_DVR_BARRIERGATE_CTRL = 3128; //道闸控制
        /***************************DS9000新增命令(_V30) end *****************************/

        /*************************参数配置命令 end*******************************/

        /*******************查找文件和日志函数返回值*************************/
        public const int NET_DVR_FILE_SUCCESS = 1000;//获得文件信息
        public const int NET_DVR_FILE_NOFIND = 1001;//没有文件
        public const int NET_DVR_ISFINDING = 1002;//正在查找文件
        public const int NET_DVR_NOMOREFILE = 1003;//查找文件时没有更多的文件
        public const int NET_DVR_FILE_EXCEPTION = 1004;//查找文件时异常

        /*********************回调函数类型 begin************************/
        public const int COMM_ALARM = 4352;//8000报警信息主动上传
        public const int COMM_TRADEINFO = 5376;//ATMDVR主动上传交易信息
        public const int COMM_ALARM_V30 = 16384;//9000报警信息主动上传
        public const int COMM_UPLOAD_PLATE_RESULT = 0x2800;//交通抓拍结果上传
        public const int COMM_ITS_PLATE_RESULT = 0x3050;//交通抓拍结果上传
        public const int COMM_IPCCFG = 16385;//9000设备IPC接入配置改变报警信息主动上传
        public const int COMM_IPCCFG_V31 = 16386;//9000设备IPC接入配置改变报警信息主动上传扩展 9000_1.1
        public const int COMM_ALARM_RULE_CALC = 0x1110;  //行为统计报警上传(人员密度)

        /*************操作异常类型(消息方式, 回调方式(保留))****************/
        public const int EXCEPTION_EXCHANGE = 32768;//用户交互时异常
        public const int EXCEPTION_AUDIOEXCHANGE = 32769;//语音对讲异常
        public const int EXCEPTION_ALARM = 32770;//报警异常
        public const int EXCEPTION_PREVIEW = 32771;//网络预览异常
        public const int EXCEPTION_SERIAL = 32772;//透明通道异常
        public const int EXCEPTION_RECONNECT = 32773;//预览时重连
        public const int EXCEPTION_ALARMRECONNECT = 32774;//报警时重连
        public const int EXCEPTION_SERIALRECONNECT = 32775;//透明通道重连
        public const int EXCEPTION_PLAYBACK = 32784;//回放异常
        public const int EXCEPTION_DISKFMT = 32785;//硬盘格式化

        /********************预览回调函数*********************/
        public const int NET_DVR_SYSHEAD = 1;//系统头数据
        public const int NET_DVR_STREAMDATA = 2;//视频流数据（包括复合流和音视频分开的视频流数据）
        public const int NET_DVR_AUDIOSTREAMDATA = 3;//音频流数据
        public const int NET_DVR_STD_VIDEODATA = 4;//标准视频流数据
        public const int NET_DVR_STD_AUDIODATA = 5;//标准音频流数据

        //回调预览中的状态和消息
        public const int NET_DVR_REALPLAYEXCEPTION = 111;//预览异常
        public const int NET_DVR_REALPLAYNETCLOSE = 112;//预览时连接断开
        public const int NET_DVR_REALPLAY5SNODATA = 113;//预览5s没有收到数据
        public const int NET_DVR_REALPLAYRECONNECT = 114;//预览重连

        /********************回放回调函数*********************/
        public const int NET_DVR_PLAYBACKOVER = 101;//回放数据播放完毕
        public const int NET_DVR_PLAYBACKEXCEPTION = 102;//回放异常
        public const int NET_DVR_PLAYBACKNETCLOSE = 103;//回放时候连接断开
        public const int NET_DVR_PLAYBACK5SNODATA = 104;//回放5s没有收到数据

        /*********************回调函数类型 end************************/
        //设备型号(DVR类型)
        /* 设备类型 */
        public const int DVR = 1;/*对尚未定义的dvr类型返回NETRET_DVR*/
        public const int ATMDVR = 2;/*atm dvr*/
        public const int DVS = 3;/*DVS*/
        public const int DEC = 4;/* 6001D */
        public const int ENC_DEC = 5;/* 6001F */
        public const int DVR_HC = 6;/*8000HC*/
        public const int DVR_HT = 7;/*8000HT*/
        public const int DVR_HF = 8;/*8000HF*/
        public const int DVR_HS = 9;/* 8000HS DVR(no audio) */
        public const int DVR_HTS = 10; /* 8016HTS DVR(no audio) */
        public const int DVR_HB = 11; /* HB DVR(SATA HD) */
        public const int DVR_HCS = 12; /* 8000HCS DVR */
        public const int DVS_A = 13; /* 带ATA硬盘的DVS */
        public const int DVR_HC_S = 14; /* 8000HC-S */
        public const int DVR_HT_S = 15;/* 8000HT-S */
        public const int DVR_HF_S = 16;/* 8000HF-S */
        public const int DVR_HS_S = 17; /* 8000HS-S */
        public const int ATMDVR_S = 18;/* ATM-S */
        public const int LOWCOST_DVR = 19;/*7000H系列*/
        public const int DEC_MAT = 20; /*多路解码器*/
        public const int DVR_MOBILE = 21;/* mobile DVR */
        public const int DVR_HD_S = 22;   /* 8000HD-S */
        public const int DVR_HD_SL = 23;/* 8000HD-SL */
        public const int DVR_HC_SL = 24;/* 8000HC-SL */
        public const int DVR_HS_ST = 25;/* 8000HS_ST */
        public const int DVS_HW = 26; /* 6000HW */
        public const int DS630X_D = 27; /* 多路解码器 */
        public const int IPCAM = 30;/*IP 摄像机*/
        public const int MEGA_IPCAM = 31;/*X52MF系列,752MF,852MF*/
        public const int IPCAM_X62MF = 32;/*X62MF系列可接入9000设备,762MF,862MF*/
        public const int IPDOME = 40; /*IP 标清球机*/
        public const int IPDOME_MEGA200 = 41;/*IP 200万高清球机*/
        public const int IPDOME_MEGA130 = 42;/*IP 130万高清球机*/
        public const int IPMOD = 50;/*IP 模块*/
        public const int DS71XX_H = 71;/* DS71XXH_S */
        public const int DS72XX_H_S = 72;/* DS72XXH_S */
        public const int DS73XX_H_S = 73;/* DS73XXH_S */
        public const int DS76XX_H_S = 76;/* DS76XX_H_S */
        public const int DS81XX_HS_S = 81;/* DS81XX_HS_S */
        public const int DS81XX_HL_S = 82;/* DS81XX_HL_S */
        public const int DS81XX_HC_S = 83;/* DS81XX_HC_S */
        public const int DS81XX_HD_S = 84;/* DS81XX_HD_S */
        public const int DS81XX_HE_S = 85;/* DS81XX_HE_S */
        public const int DS81XX_HF_S = 86;/* DS81XX_HF_S */
        public const int DS81XX_AH_S = 87;/* DS81XX_AH_S */
        public const int DS81XX_AHF_S = 88;/* DS81XX_AHF_S */
        public const int DS90XX_HF_S = 90;  /*DS90XX_HF_S*/
        public const int DS91XX_HF_S = 91;  /*DS91XX_HF_S*/
        public const int DS91XX_HD_S = 92; /*91XXHD-S(MD)*/
        /**********************设备类型 end***********************/

        /*************************************************
        参数配置结构、参数(其中_V30为9000新增)
        **************************************************/
        //校时结构参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIME
        {
            public int dwYear;
            public int dwMonth;
            public int dwDay;
            public int dwHour;
            public int dwMinute;
            public int dwSecond;
        }

        //时间段(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SCHEDTIME
        {
            public byte byStartHour;//开始时间
            public byte byStartMin;//开始时间
            public byte byStopHour;//结束时间
            public byte byStopMin;//结束时间
        }

        /*设备报警和异常处理方式*/
        public const int NOACTION = 0;/*无响应*/
        public const int WARNONMONITOR = 1;/*监视器上警告*/
        public const int WARNONAUDIOOUT = 2;/*声音警告*/
        public const int UPTOCENTER = 4;/*上传中心*/
        public const int TRIGGERALARMOUT = 8;/*触发报警输出*/

        //报警和异常处理结构(子结构)(多处使用)(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HANDLEEXCEPTION_V30
        {
            public uint dwHandleType;/*处理方式,处理方式的"或"结果*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelAlarmOut;//报警触发的输出通道,报警触发的输出,为1表示触发该输出
        }

        //报警和异常处理结构(子结构)(多处使用)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HANDLEEXCEPTION
        {
            public uint dwHandleType;/*处理方式,处理方式的"或"结果*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelAlarmOut;//报警触发的输出通道,报警触发的输出,为1表示触发该输出
        }

        //DVR设备参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICECFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDVRName;//DVR名称
            public uint dwDVRID;//DVR ID,用于遥控器 //V1.4(0-99), V1.5(0-255)
            public uint dwRecycleRecord;//是否循环录像,0:不是; 1:是
            //以下不可更改
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号
            public uint dwSoftwareVersion;//软件版本号,高16位是主版本,低16位是次版本
            public uint dwSoftwareBuildDate;//软件生成日期,0xYYYYMMDD
            public uint dwDSPSoftwareVersion;//DSP软件版本,高16位是主版本,低16位是次版本
            public uint dwDSPSoftwareBuildDate;// DSP软件生成日期,0xYYYYMMDD
            public uint dwPanelVersion;// 前面板版本,高16位是主版本,低16位是次版本
            public uint dwHardwareVersion;// 硬件版本,高16位是主版本,低16位是次版本
            public byte byAlarmInPortNum;//DVR报警输入个数
            public byte byAlarmOutPortNum;//DVR报警输出个数
            public byte byRS232Num;//DVR 232串口个数
            public byte byRS485Num;//DVR 485串口个数
            public byte byNetworkPortNum;//网络口个数
            public byte byDiskCtrlNum;//DVR 硬盘控制器个数
            public byte byDiskNum;//DVR 硬盘个数
            public byte byDVRType;//DVR类型, 1:DVR 2:ATM DVR 3:DVS ......
            public byte byChanNum;//DVR 通道个数
            public byte byStartChan;//起始通道号,例如DVS-1,DVR - 1
            public byte byDecordChans;//DVR 解码路数
            public byte byVGANum;//VGA口的个数
            public byte byUSBNum;//USB口的个数
            public byte byAuxoutNum;//辅口的个数
            public byte byAudioNum;//语音口的个数
            public byte byIPChanNum;//最大数字通道数
        }

        /*IP地址*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_IPADDR
        {

            /// char[16]
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpV4;

            /// BYTE[128]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[128];
            }
        }

        /*网络数据结构(子结构)(9000扩展)*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ETHERNET_V30
        {
            public NET_DVR_IPADDR struDVRIP;//DVR IP地址
            public NET_DVR_IPADDR struDVRIPMask;//DVR IP地址掩码
            public uint dwNetInterface;//网络接口1-10MBase-T 2-10MBase-T全双工 3-100MBase-TX 4-100M全双工 5-10M/100M自适应
            public ushort wDVRPort;//端口号
            public ushort wMTU;//增加MTU设置，默认1500。
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;// 物理地址
        }

        /*网络数据结构(子结构)*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_ETHERNET
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;//DVR IP地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIPMask;//DVR IP地址掩码
            public uint dwNetInterface;//网络接口 1-10MBase-T 2-10MBase-T全双工 3-100MBase-TX 4-100M全双工 5-10M/100M自适应
            public ushort wDVRPort;//端口号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;//服务器的物理地址
        }

        //pppoe结构
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPOECFG
        {
            public uint dwPPPOE;//0-不启用,1-启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPPPoEUser;//PPPoE用户名
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPPPoEPassword;// PPPoE密码
            public NET_DVR_IPADDR struPPPoEIP;//PPPoE IP地址
        }

        //网络配置结构(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_NETCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ETHERNET, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_ETHERNET_V30[] struEtherNet;//以太网口
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPADDR[] struRes1;/*保留*/
            public NET_DVR_IPADDR struAlarmHostIpAddr;/* 报警主机IP地址 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U2)]
            public ushort[] wRes2;
            public ushort wAlarmHostIpPort;
            public byte byUseDhcp;
            public byte byRes3;
            public NET_DVR_IPADDR struDnsServer1IpAddr;/* 域名服务器1的IP地址 */
            public NET_DVR_IPADDR struDnsServer2IpAddr;/* 域名服务器2的IP地址 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] byIpResolver;
            public ushort wIpResolverPort;
            public ushort wHttpPortNo;
            public NET_DVR_IPADDR struMulticastIpAddr;/* 多播组地址 */
            public NET_DVR_IPADDR struGatewayIpAddr;/* 网关地址 */
            public NET_DVR_PPPOECFG struPPPoE;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //网络配置结构
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_NETCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ETHERNET, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_ETHERNET[] struEtherNet;/* 以太网口 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sManageHostIP;//远程管理主机地址
            public ushort wManageHostPort;//远程管理主机端口号
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIPServerIP;//IPServer服务器地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sMultiCastIP;//多播组地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sGatewayIP;//网关地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sNFSIP;//NFS主机IP地址
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PATHNAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNFSDirectory;//NFS目录
            public uint dwPPPOE;//0-不启用,1-启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPPPoEUser;//PPPoE用户名
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPPPoEPassword;// PPPoE密码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sPPPoEIP;//PPPoE IP地址(只读)
            public ushort wHttpPort;//HTTP端口号
        }

        //通道图象结构
        //移动侦测(子结构)(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MOTION_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 96 * 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotionScope;/*侦测区域,0-96位,表示64行,共有96*64个小宏块,为1表示是移动侦测区域,0-表示不是*/
            public byte byMotionSensitive;/*移动侦测灵敏度, 0 - 5,越高越灵敏,oxff关闭*/
            public byte byEnableHandleMotion;/* 是否处理移动侦测 0－否 1－是*/
            public byte byPrecision;/* 移动侦测算法的进度: 0--16*16, 1--32*32, 2--64*64 ... */
            public byte reservedData;
            public NET_DVR_HANDLEEXCEPTION_V30 struMotionHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;/*布防时间*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;/* 报警触发的录象通道*/
        }

        //移动侦测(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MOTION
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 396, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotionScope;/*侦测区域,共有22*18个小宏块,为1表示改宏块是移动侦测区域,0-表示不是*/
            public byte byMotionSensitive;/*移动侦测灵敏度, 0 - 5,越高越灵敏,0xff关闭*/
            public byte byEnableHandleMotion;/* 是否处理移动侦测 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 2)]
            public string reservedData;
            public NET_DVR_HANDLEEXCEPTION strMotionHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//报警触发的录象通道,为1表示触发该通道
        }

        //遮挡报警(子结构)(9000扩展)  区域大小704*576
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HIDEALARM_V30
        {
            public uint dwEnableHideAlarm;/* 是否启动遮挡报警 ,0-否,1-低灵敏度 2-中灵敏度 3-高灵敏度*/
            public ushort wHideAlarmAreaTopLeftX;/* 遮挡区域的x坐标 */
            public ushort wHideAlarmAreaTopLeftY;/* 遮挡区域的y坐标 */
            public ushort wHideAlarmAreaWidth;/* 遮挡区域的宽 */
            public ushort wHideAlarmAreaHeight;/*遮挡区域的高*/
            public NET_DVR_HANDLEEXCEPTION_V30 strHideAlarmHandleType;	/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
        }

        //遮挡报警(子结构)  区域大小704*576
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HIDEALARM
        {
            public uint dwEnableHideAlarm;/* 是否启动遮挡报警 ,0-否,1-低灵敏度 2-中灵敏度 3-高灵敏度*/
            public ushort wHideAlarmAreaTopLeftX;/* 遮挡区域的x坐标 */
            public ushort wHideAlarmAreaTopLeftY;/* 遮挡区域的y坐标 */
            public ushort wHideAlarmAreaWidth;/* 遮挡区域的宽 */
            public ushort wHideAlarmAreaHeight;/*遮挡区域的高*/
            public NET_DVR_HANDLEEXCEPTION strHideAlarmHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
        }

        //信号丢失报警(子结构)(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VILOST_V30
        {
            public byte byEnableHandleVILost;/* 是否处理信号丢失报警 */
            public NET_DVR_HANDLEEXCEPTION_V30 strVILostHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
        }

        //信号丢失报警(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VILOST
        {
            public byte byEnableHandleVILost;/* 是否处理信号丢失报警 */
            public NET_DVR_HANDLEEXCEPTION strVILostHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
        }

        //遮挡区域(子结构)
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_SHELTER
        {
            public ushort wHideAreaTopLeftX;/* 遮挡区域的x坐标 */
            public ushort wHideAreaTopLeftY;/* 遮挡区域的y坐标 */
            public ushort wHideAreaWidth;/* 遮挡区域的宽 */
            public ushort wHideAreaHeight;/*遮挡区域的高*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COLOR
        {
            public byte byBrightness;/*亮度,0-255*/
            public byte byContrast;/*对比度,0-255*/
            public byte bySaturation;/*饱和度,0-255*/
            public byte byHue;/*色调,0-255*/
        }

        //通道图象结构(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PICCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            //		[MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public string sChanName;
            public uint dwVideoFormat;/* 只读 视频制式 1-NTSC 2-PAL*/
            public NET_DVR_COLOR struColor;//	图像参数
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 60)]
            public string reservedData;/*保留*/
            //显示通道名
            public uint dwShowChanName;// 预览的图象上是否显示通道名称,0-不显示,1-显示 区域大小704*576
            public ushort wShowNameTopLeftX;/* 通道名称显示位置的x坐标 */
            public ushort wShowNameTopLeftY;/* 通道名称显示位置的y坐标 */
            //视频信号丢失报警
            public NET_DVR_VILOST_V30 struVILost;
            public NET_DVR_VILOST_V30 struRes;/*保留*/
            //移动侦测
            public NET_DVR_MOTION_V30 struMotion;
            //遮挡报警
            public NET_DVR_HIDEALARM_V30 struHideAlarm;
            //遮挡  区域大小704*576
            public uint dwEnableHide;/* 是否启动遮挡 ,0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SHELTERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHELTER[] struShelter;
            //OSD
            public uint dwShowOsd;// 预览的图象上是否显示OSD,0-不显示,1-显示 区域大小704*576
            public ushort wOSDTopLeftX;/* OSD的x坐标 */
            public ushort wOSDTopLeftY;/* OSD的y坐标 */
            public byte byOSDType;/* OSD类型(主要是年月日格式) */
            /* 0: XXXX-XX-XX 年月日 */
            /* 1: XX-XX-XXXX 月日年 */
            /* 2: XXXX年XX月XX日 */
            /* 3: XX月XX日XXXX年 */
            /* 4: XX-XX-XXXX 日月年*/
            /* 5: XX日XX月XXXX年 */
            public byte byDispWeek;/* 是否显示星期 */
            public byte byOSDAttrib;/* OSD属性:透明，闪烁 */
            /* 0: 不显示OSD */
            /* 1: 透明,闪烁 */
            /* 2: 透明,不闪烁 */
            /* 3: 闪烁,不透明 */
            /* 4: 不透明,不闪烁 */
            public byte byHourOSDType;/* OSD小时制:0-24小时制,1-12小时制 */
            public byte byFontSize;//字体大小，16*16(中)/8*16(英)，1-32*32(中)/16*32(英)，2-64*64(中)/32*64(英)  3-48*48(中)/24*48(英) 0xff-自适应(adaptive)
            public byte byOSDColorType;	//0-默认（黑白）；1-自定义
            public byte byAlignment;//对齐方式 0-自适应，1-右对齐, 2-左对齐
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 61, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }

        //通道图象结构SDK_V14扩展
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PICCFG_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sChanName;
            public uint dwVideoFormat;/* 只读 视频制式 1-NTSC 2-PAL*/
            public byte byBrightness;/*亮度,0-255*/
            public byte byContrast;/*对比度,0-255*/
            public byte bySaturation;/*饱和度,0-255 */
            public byte byHue;/*色调,0-255*/
            //显示通道名
            public uint dwShowChanName;// 预览的图象上是否显示通道名称,0-不显示,1-显示 区域大小704*576
            public ushort wShowNameTopLeftX;/* 通道名称显示位置的x坐标 */
            public ushort wShowNameTopLeftY;/* 通道名称显示位置的y坐标 */
            //信号丢失报警
            public NET_DVR_VILOST struVILost;
            //移动侦测
            public NET_DVR_MOTION struMotion;
            //遮挡报警
            public NET_DVR_HIDEALARM struHideAlarm;
            //遮挡  区域大小704*576
            public uint dwEnableHide;/* 是否启动遮挡 ,0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SHELTERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHELTER[] struShelter;
            //OSD
            public uint dwShowOsd;// 预览的图象上是否显示OSD,0-不显示,1-显示 区域大小704*576
            public ushort wOSDTopLeftX;/* OSD的x坐标 */
            public ushort wOSDTopLeftY;/* OSD的y坐标 */
            public byte byOSDType;/* OSD类型(主要是年月日格式) */
            /* 0: XXXX-XX-XX 年月日 */
            /* 1: XX-XX-XXXX 月日年 */
            /* 2: XXXX年XX月XX日 */
            /* 3: XX月XX日XXXX年 */
            /* 4: XX-XX-XXXX 日月年*/
            /* 5: XX日XX月XXXX年 */
            public byte byDispWeek;/* 是否显示星期 */
            public byte byOSDAttrib;/* OSD属性:透明，闪烁 */
            /* 0: 不显示OSD */
            /* 1: 透明,闪烁 */
            /* 2: 透明,不闪烁 */
            /* 3: 闪烁,不透明 */
            /* 4: 不透明,不闪烁 */
            public byte byHourOsdType;/* OSD小时制:0-24小时制,1-12小时制 */
        }

        //通道图象结构(SDK_V13及之前版本)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PICCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sChanName;
            public uint dwVideoFormat;/* 只读 视频制式 1-NTSC 2-PAL*/
            public byte byBrightness;/*亮度,0-255*/
            public byte byContrast;/*对比度,0-255*/
            public byte bySaturation;/*饱和度,0-255 */
            public byte byHue;/*色调,0-255*/
            //显示通道名
            public uint dwShowChanName;// 预览的图象上是否显示通道名称,0-不显示,1-显示 区域大小704*576
            public ushort wShowNameTopLeftX;/* 通道名称显示位置的x坐标 */
            public ushort wShowNameTopLeftY;/* 通道名称显示位置的y坐标 */
            //信号丢失报警
            public NET_DVR_VILOST struVILost;
            //移动侦测
            public NET_DVR_MOTION struMotion;
            //遮挡报警
            public NET_DVR_HIDEALARM struHideAlarm;
            //遮挡  区域大小704*576
            public uint dwEnableHide;/* 是否启动遮挡 ,0-否,1-是*/
            public ushort wHideAreaTopLeftX;/* 遮挡区域的x坐标 */
            public ushort wHideAreaTopLeftY;/* 遮挡区域的y坐标 */
            public ushort wHideAreaWidth;/* 遮挡区域的宽 */
            public ushort wHideAreaHeight;/*遮挡区域的高*/
            //OSD
            public uint dwShowOsd;// 预览的图象上是否显示OSD,0-不显示,1-显示 区域大小704*576
            public ushort wOSDTopLeftX;/* OSD的x坐标 */
            public ushort wOSDTopLeftY;/* OSD的y坐标 */
            public byte byOSDType;/* OSD类型(主要是年月日格式) */
            /* 0: XXXX-XX-XX 年月日 */
            /* 1: XX-XX-XXXX 月日年 */
            /* 2: XXXX年XX月XX日 */
            /* 3: XX月XX日XXXX年 */
            /* 4: XX-XX-XXXX 日月年*/
            /* 5: XX日XX月XXXX年 */
            public byte byDispWeek;/* 是否显示星期 */
            public byte byOSDAttrib;/* OSD属性:透明，闪烁 */
            /* 0: 不显示OSD */
            /* 1: 透明,闪烁 */
            /* 2: 透明,不闪烁 */
            /* 3: 闪烁,不透明 */
            /* 4: 不透明,不闪烁 */
            public byte reservedData2;
        }

        //码流压缩参数(子结构)(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO_V30
        {
            public byte byStreamType;//码流类型 0-视频流, 1-复合流, 表示事件压缩参数时最高位表示是否启用压缩参数
            public byte byResolution;//分辨率0-DCIF 1-CIF, 2-QCIF, 3-4CIF, 4-2CIF 5（保留）16-VGA（640*480） 17-UXGA（1600*1200） 18-SVGA （800*600）19-HD720p（1280*720）20-XVGA  21-HD900p
            public byte byBitrateType;//码率类型 0:变码率, 1:定码率
            public byte byPicQuality;//图象质量 0-最好 1-次好 2-较好 3-一般 4-较差 5-差
            public uint dwVideoBitrate;//视频码率 0-保留 1-16K 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K
            // 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K
            //最高位(31位)置成1表示是自定义码流, 0-30位表示码流值。
            public uint dwVideoFrameRate;//帧率 0-全部; 1-1/16; 2-1/8; 3-1/4; 4-1/2; 5-1; 6-2; 7-4; 8-6; 9-8; 10-10; 11-12; 12-16; 13-20; V2.0版本中新加14-15; 15-18; 16-22;
            public ushort wIntervalFrameI;//I帧间隔
            //2006-08-11 增加单P帧的配置接口，可以改善实时流延时问题
            public byte byIntervalBPFrame;//0-BBP帧; 1-BP帧; 2-单P帧
            public byte byres1; //保留
            public byte byVideoEncType;//视频编码类型 0 hik264;1标准h264; 2标准mpeg4;
            public byte byAudioEncType; //音频编码类型 0－OggVorbis
            public byte byVideoEncComplexity; //视频编码复杂度，0-低，1-中，2高,0xfe:自动，和源一致
            public byte byEnableSvc; //0 - 不启用SVC功能；1- 启用SVC功能; 2-自动启用SVC功能
            public byte byFormatType; //封装类型，1-裸流，2-RTP封装，3-PS封装，4-TS封装，5-私有，6-FLV，7-ASF，8-3GP,9-RTP+PS（国标：GB28181），0xff-无效
            public byte byAudioBitRate; //音频码率 参考 BITRATE_ENCODE_INDEX
            public byte byStreamSmooth;//码流平滑 1～100（1等级表示清晰(Clear)，100表示平滑(Smooth)）
            public byte byAudioSamplingRate;//音频采样率0-默认,1- 16kHZ, 2-32kHZ, 3-48kHZ, 4- 44.1kHZ,5-8kHZ
            public byte bySmartCodec;//高性能编码 0-关闭，1-打开
            public byte byres;
            //平均码率（在SmartCodec使能开启下生效）, 0-0K 1-16K 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K 24-2560K 25-3072K 26-4096K 27-5120K 28-6144K 29-7168K 30-8192K
            //最高位(15位)置成1表示是自定义码流, 0-14位表示码流值(MIN- 0 K)。
            public ushort wAverageVideoBitrate; 
        }

        //通道压缩参数(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_V30
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_V30 struNormHighRecordPara;//录像 对应8000的普通
            public NET_DVR_COMPRESSION_INFO_V30 struRes;//保留 char reserveData[28];
            public NET_DVR_COMPRESSION_INFO_V30 struEventRecordPara;//事件触发压缩参数
            public NET_DVR_COMPRESSION_INFO_V30 struNetPara;//网传(子码流)
        }

        //码流压缩参数(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO
        {
            public byte byStreamType;//码流类型0-视频流,1-复合流,表示压缩参数时最高位表示是否启用压缩参数
            public byte byResolution;//分辨率0-DCIF 1-CIF, 2-QCIF, 3-4CIF, 4-2CIF, 5-2QCIF(352X144)(车载专用)
            public byte byBitrateType;//码率类型0:变码率，1:定码率
            public byte byPicQuality;//图象质量 0-最好 1-次好 2-较好 3-一般 4-较差 5-差
            public uint dwVideoBitrate; //视频码率 0-保留 1-16K(保留) 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K
            // 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K
            //最高位(31位)置成1表示是自定义码流, 0-30位表示码流值(MIN-32K MAX-8192K)。
            public uint dwVideoFrameRate;//帧率 0-全部; 1-1/16; 2-1/8; 3-1/4; 4-1/2; 5-1; 6-2; 7-4; 8-6; 9-8; 10-10; 11-12; 12-16; 13-20;
        }

        //通道压缩参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO struRecordPara;//录像/事件触发录像
            public NET_DVR_COMPRESSION_INFO struNetPara;//网传/保留
        }

        //码流压缩参数(子结构)(扩展) 增加I帧间隔
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO_EX
        {
            public byte byStreamType;//码流类型0-视频流, 1-复合流
            public byte byResolution;//分辨率0-DCIF 1-CIF, 2-QCIF, 3-4CIF, 4-2CIF, 5-2QCIF(352X144)(车载专用)
            public byte byBitrateType;//码率类型0:变码率，1:定码率
            public byte byPicQuality;//图象质量 0-最好 1-次好 2-较好 3-一般 4-较差 5-差
            public uint dwVideoBitrate;//视频码率 0-保留 1-16K(保留) 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K
            // 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K
            //最高位(31位)置成1表示是自定义码流, 0-30位表示码流值(MIN-32K MAX-8192K)。
            public uint dwVideoFrameRate;//帧率 0-全部; 1-1/16; 2-1/8; 3-1/4; 4-1/2; 5-1; 6-2; 7-4; 8-6; 9-8; 10-10; 11-12; 12-16; 13-20, //V2.0增加14-15, 15-18, 16-22;
            public ushort wIntervalFrameI;//I帧间隔
            //2006-08-11 增加单P帧的配置接口，可以改善实时流延时问题
            public byte byIntervalBPFrame;//0-BBP帧; 1-BP帧; 2-单P帧
            public byte byRes;
        }

        //通道压缩参数(扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_EX
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_EX struRecordPara;//录像
            public NET_DVR_COMPRESSION_INFO_EX struNetPara;//网传
        }

        //时间段录像参数配置(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_RECORDSCHED
        {
            public NET_DVR_SCHEDTIME struRecordTime;
            public byte byRecordType;//0:定时录像，1:移动侦测，2:报警录像，3:动测|报警，4:动测&报警, 5:命令触发, 6: 智能录像
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string reservedData;
        }

        //全天录像参数配置(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORDDAY
        {
            public ushort wAllDayRecord;/* 是否全天录像 0-否 1-是*/
            public byte byRecordType;/* 录象类型 0:定时录像，1:移动侦测，2:报警录像，3:动测|报警，4:动测&报警 5:命令触发, 6: 智能录像*/
            public byte reservedData;
        }

        //通道录像参数配置(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD_V30
        {
            public uint dwSize;
            public uint dwRecord;/*是否录像 0-否 1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED[] struRecordSched;
            public uint dwRecordTime;/* 录象延时长度 0-5秒， 1-20秒， 2-30秒， 3-1分钟， 4-2分钟， 5-5分钟， 6-10分钟*/
            public uint dwPreRecordTime;/* 预录时间 0-不预录 1-5秒 2-10秒 3-15秒 4-20秒 5-25秒 6-30秒 7-0xffffffff(尽可能预录) */
            public uint dwRecorderDuration;/* 录像保存的最长时间 */
            public byte byRedundancyRec;/*是否冗余录像,重要数据双备份：0/1*/
            public byte byAudioRec;/*录像时复合流编码时是否记录音频数据：国外有此法规*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byReserve;
        }

        //通道录像参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD
        {
            public uint dwSize;
            public uint dwRecord;/*是否录像 0-否 1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED[] struRecordSched;
            public uint dwRecordTime;/* 录象时间长度 */
            public uint dwPreRecordTime;/* 预录时间 0-不预录 1-5秒 2-10秒 3-15秒 4-20秒 5-25秒 6-30秒 7-0xffffffff(尽可能预录) */
        }

        //云台协议表结构配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZ_PROTOCOL
        {
            public uint dwType;/*解码器类型值，从1开始连续递增*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DESC_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDescribe;/*解码器的描述符，和8000中的一致*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PTZ_PROTOCOL_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_PTZ_PROTOCOL[] struPtz;/*最大200中PTZ协议*/
            public uint dwPtzNum;/*有效的ptz协议数目，从0开始(即计算时加1)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /***************************云台类型(end)******************************/

        //通道解码器(云台)参数配置(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERCFG_V30
        {
            public uint dwSize;
            public uint dwBaudRate;//波特率(bps)，0－50，1－75，2－110，3－150，4－300，5－600，6－1200，7－2400，8－4800，9－9600，10－19200， 11－38400，12－57600，13－76800，14－115.2k;
            public byte byDataBit;// 数据有几位 0－5位，1－6位，2－7位，3－8位;
            public byte byStopBit;// 停止位 0－1位，1－2位
            public byte byParity;// 校验 0－无校验，1－奇校验，2－偶校验;
            public byte byFlowcontrol;// 0－无，1－软流控,2-硬流控
            public ushort wDecoderType;//解码器类型, 从0开始，对应ptz协议列表
            public ushort wDecoderAddress;/*解码器地址:0 - 255*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PRESET_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetPreset;/* 预置点是否设置,0-没有设置,1-设置*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CRUISE_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetCruise;/* 巡航是否设置: 0-没有设置,1-设置 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRACK_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetTrack;/* 轨迹是否设置,0-没有设置,1-设置*/
        }

        //通道解码器(云台)参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERCFG
        {
            public uint dwSize;
            public uint dwBaudRate; //波特率(bps)，0－50，1－75，2－110，3－150，4－300，5－600，6－1200，7－2400，8－4800，9－9600，10－19200， 11－38400，12－57600，13－76800，14－115.2k;
            public byte byDataBit; // 数据有几位 0－5位，1－6位，2－7位，3－8位;
            public byte byStopBit;// 停止位 0－1位，1－2位;
            public byte byParity; // 校验 0－无校验，1－奇校验，2－偶校验;
            public byte byFlowcontrol;// 0－无，1－软流控,2-硬流控
            public ushort wDecoderType;//解码器类型, 0－YouLi，1－LiLin-1016，2－LiLin-820，3－Pelco-p，4－DM DynaColor，5－HD600，6－JC-4116，7－Pelco-d WX，8－Pelco-d PICO
            public ushort wDecoderAddress;/*解码器地址:0 - 255*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PRESET, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetPreset;/* 预置点是否设置,0-没有设置,1-设置*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CRUISE, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetCruise;/* 巡航是否设置: 0-没有设置,1-设置 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRACK, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetTrack;/* 轨迹是否设置,0-没有设置,1-设置*/
        }

        //ppp参数配置(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPCFG_V30
        {
            public NET_DVR_IPADDR struRemoteIP;//远端IP地址
            public NET_DVR_IPADDR struLocalIP;//本地IP地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIPMask;//本地IP地址掩码
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            public byte byPPPMode;//PPP模式, 0－主动，1－被动
            public byte byRedial;//是否回拨 ：0-否,1-是
            public byte byRedialMode;//回拨模式,0-由拨入者指定,1-预置回拨号码
            public byte byDataEncrypt;//数据加密,0-否,1-是
            public uint dwMTU;//MTU
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PHONENUMBER_LEN)]
            public string sTelephoneNumber;//电话号码
        }

        //ppp参数配置(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPCFG
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteIP;//远端IP地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIP;//本地IP地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIPMask;//本地IP地址掩码
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            public byte byPPPMode;//PPP模式, 0－主动，1－被动
            public byte byRedial;//是否回拨 ：0-否,1-是
            public byte byRedialMode;//回拨模式,0-由拨入者指定,1-预置回拨号码
            public byte byDataEncrypt;//数据加密,0-否,1-是
            public uint dwMTU;//MTU
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PHONENUMBER_LEN)]
            public string sTelephoneNumber;//电话号码
        }

        //RS232串口参数配置(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_RS232
        {
            public uint dwBaudRate;/*波特率(bps)，0－50，1－75，2－110，3－150，4－300，5－600，6－1200，7－2400，8－4800，9－9600，10－19200， 11－38400，12－57600，13－76800，14－115.2k;*/
            public byte byDataBit;/* 数据有几位 0－5位，1－6位，2－7位，3－8位 */
            public byte byStopBit;/* 停止位 0－1位，1－2位 */
            public byte byParity;/* 校验 0－无校验，1－奇校验，2－偶校验 */
            public byte byFlowcontrol;/* 0－无，1－软流控,2-硬流控 */
            public uint dwWorkMode; /* 工作模式，0－232串口用于PPP拨号，1－232串口用于参数控制，2－透明通道 */
        }

        //RS232串口参数配置(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RS232CFG_V30
        {
            public uint dwSize;
            public NET_DVR_SINGLE_RS232 struRs232;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 84, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_PPPCFG_V30 struPPPConfig;
        }

        //RS232串口参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RS232CFG
        {
            public uint dwSize;
            public uint dwBaudRate;//波特率(bps)，0－50，1－75，2－110，3－150，4－300，5－600，6－1200，7－2400，8－4800，9－9600，10－19200， 11－38400，12－57600，13－76800，14－115.2k;
            public byte byDataBit;// 数据有几位 0－5位，1－6位，2－7位，3－8位;
            public byte byStopBit;// 停止位 0－1位，1－2位;
            public byte byParity;// 校验 0－无校验，1－奇校验，2－偶校验;
            public byte byFlowcontrol;// 0－无，1－软流控,2-硬流控
            public uint dwWorkMode;// 工作模式，0－窄带传输(232串口用于PPP拨号)，1－控制台(232串口用于参数控制)，2－透明通道
            public NET_DVR_PPPCFG struPPPConfig;
        }

        //报警输入参数配置(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmInName;/* 名称 */
            public byte byAlarmType; //报警器类型,0：常开,1：常闭
            public byte byAlarmInHandle; /* 是否处理 0-不处理 1-处理*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_HANDLEEXCEPTION_V30 struAlarmHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//报警触发的录象通道,为1表示触发该通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePreset;/* 是否调用预置点 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;/* 调用的云台预置点序号,一个报警输入可以调用多个通道的云台预置点, 0xff表示不调用预置点。*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 192, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;/*保留*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCruise;/* 是否调用巡航 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseNo;/* 巡航 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePtzTrack;/* 是否调用轨迹 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byPTZTrack;/* 调用的云台的轨迹序号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        //报警输入参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmInName;/* 名称 */
            public byte byAlarmType;//报警器类型,0：常开,1：常闭
            public byte byAlarmInHandle;/* 是否处理 0-不处理 1-处理*/
            public NET_DVR_HANDLEEXCEPTION struAlarmHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//报警触发的录象通道,为1表示触发该通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePreset;/* 是否调用预置点 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;/* 调用的云台预置点序号,一个报警输入可以调用多个通道的云台预置点, 0xff表示不调用预置点。*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCruise;/* 是否调用巡航 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseNo;/* 巡航 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePtzTrack;/* 是否调用轨迹 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byPTZTrack;/* 调用的云台的轨迹序号 */
        }

        //上传报警信息(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V30
        {
            public int dwAlarmType;/*0-信号量报警,1-硬盘满,2-信号丢失,3－移动侦测,4－硬盘未格式化,5-读写硬盘出错,6-遮挡报警,7-制式不匹配, 8-非法访问, 0xa-GPS定位信息(车载定制)*/
            public int dwAlarmInputNumber;/*报警输入端口*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutputNumber;/*触发的输出端口，为1表示对应输出*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmRelateChannel;/*触发的录像通道，为1表示对应录像, dwAlarmRelateChannel[0]对应第1个通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChannel;/*dwAlarmType为2或3,6时，表示哪个通道，dwChannel[0]对应第1个通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byDiskNumber;/*dwAlarmType为1,4,5时,表示哪个硬盘, dwDiskNumber[0]对应第1个硬盘*/
            public void Init()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;
                byAlarmRelateChannel = new byte[MAX_CHANNUM_V30];
                byChannel = new byte[MAX_CHANNUM_V30];
                byAlarmOutputNumber = new byte[MAX_ALARMOUT_V30];
                byDiskNumber = new byte[MAX_DISKNUM_V30];
                for (int i = 0; i < MAX_CHANNUM_V30; i++)
                {
                    byAlarmRelateChannel[i] = Convert.ToByte(0);
                    byChannel[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_ALARMOUT_V30; i++)
                {
                    byAlarmOutputNumber[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_DISKNUM_V30; i++)
                {
                    byDiskNumber[i] = Convert.ToByte(0);
                }
            }
            public void Reset()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;

                for (int i = 0; i < MAX_CHANNUM_V30; i++)
                {
                    byAlarmRelateChannel[i] = Convert.ToByte(0);
                    byChannel[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_ALARMOUT_V30; i++)
                {
                    byAlarmOutputNumber[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_DISKNUM_V30; i++)
                {
                    byDiskNumber[i] = Convert.ToByte(0);
                }
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO
        {
            public int dwAlarmType;/*0-信号量报警,1-硬盘满,2-信号丢失,3－移动侦测,4－硬盘未格式化,5-读写硬盘出错,6-遮挡报警,7-制式不匹配, 8-非法访问, 9-串口状态, 0xa-GPS定位信息(车载定制)*/
            public int dwAlarmInputNumber;/*报警输入端口, 当报警类型为9时该变量表示串口状态0表示正常， -1表示错误*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.U4)]
            public int[] dwAlarmOutputNumber;/*触发的输出端口，哪一位为1表示对应哪一个输出*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwAlarmRelateChannel;/*触发的录像通道，哪一位为1表示对应哪一路录像, dwAlarmRelateChannel[0]对应第1个通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwChannel;/*dwAlarmType为2或3,6时，表示哪个通道，dwChannel[0]位对应第1个通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwDiskNumber;/*dwAlarmType为1,4,5时,表示哪个硬盘, dwDiskNumber[0]位对应第1个硬盘*/
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


        //////////////////////////////////////////////////////////////////////////////////////
        //IPC接入参数配置
        /* IP设备结构 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPDEVINFO
        {
            public uint dwEnable;/* 该IP设备是否启用 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword; /* 密码 */
            public NET_DVR_IPADDR struIP;/* IP地址 */
            public ushort wDVRPort;/* 端口号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* 保留 */

            public void Init()
            {
                sUserName = new byte[NAME_LEN];
                sPassword = new byte[PASSWD_LEN];
                byRes = new byte[34];
            }
        }

        //ipc接入设备信息扩展，支持ip设备的域名添加
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IPDEVINFO_V31
        {
            public byte byEnable;//该IP设备是否有效
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留字段，置0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;//用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;//密码
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] byDomain;//设备域名
            public NET_DVR_IPADDR struIP;//IP地址
            public ushort wDVRPort;// 端口号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//保留字段，置0

            public void Init()
            {
                byRes1 = new byte[3];
                sUserName = new byte[NAME_LEN];
                sPassword = new byte[PASSWD_LEN];
                byDomain = new byte[MAX_DOMAIN_NAME];

                byRes2 = new byte[34];
            }
        }

        /* IP通道匹配参数 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPCHANINFO
        {
            public byte byEnable;/* 该通道是否在线 */
            public byte byIPID;/* IP设备ID 取值1- MAX_IP_DEVICE */
            public byte byChannel;/* 通道号 */
            public byte byProType;//协议类型，0-海康协议(default)，1-松下协议，2-索尼
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留,置0
            public void Init()
            {
                byRes = new byte[32];
            }
        }

        /* IP接入配置结构 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG
        {
            public uint dwSize;/* 结构大小 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO[] struIPDevInfo;/* IP设备 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable; /* 模拟通道是否启用，从低到高表示1-32通道，0表示无效 1有效 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IP通道 */

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

        /* 扩展IP接入配置结构 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG_V31
        {
            public uint dwSize;/* 结构大小 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_IPDEVINFO_V31[] struIPDevInfo; /* IP设备 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable; /* 模拟通道是否启用，从低到高表示1-32通道，0表示无效 1有效 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IP通道 */

            public void Init()
            {
                int i = 0;
                struIPDevInfo = new tagNET_DVR_IPDEVINFO_V31[MAX_IP_DEVICE];

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

        /* 报警输出参数 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTINFO
        {
            public byte byIPID;/* IP设备ID取值1- MAX_IP_DEVICE */
            public byte byAlarmOut;/* 报警输出号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* 保留 */

            public void Init()
            {
                byRes = new byte[18];
            }
        }

        /* IP报警输出配置结构 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTCFG
        {
            public uint dwSize; /* 结构大小 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;/* IP报警输出 */

            public void Init()
            {
                struIPAlarmOutInfo = new NET_DVR_IPALARMOUTINFO[MAX_IP_ALARMOUT];
                for (int i = 0; i < MAX_IP_ALARMOUT; i++)
                {
                    struIPAlarmOutInfo[i].Init();
                }
            }
        }

        /* 报警输入参数 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMININFO
        {
            public byte byIPID;/* IP设备ID取值1- MAX_IP_DEVICE */
            public byte byAlarmIn;/* 报警输入号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* 保留 */
        }

        /* IP报警输入配置结构 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMINCFG
        {
            public uint dwSize;/* 结构大小 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo;/* IP报警输入 */
        }

        //ipc alarm info
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IPALARMINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO[] struIPDevInfo; /* IP设备 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable; /* 模拟通道是否启用，0-未启用 1-启用 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IP通道 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo;/* IP报警输入 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;/* IP报警输出 */
        }

        //ipc配置改变报警信息扩展 9000_1.1
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IPALARMINFO_V31
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_IPDEVINFO_V31[] struIPDevInfo; /* IP设备 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable;/* 模拟通道是否启用，0-未启用 1-启用 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IP通道 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo; /* IP报警输入 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;/* IP报警输出 */
        }

        public enum HD_STAT
        {
            HD_STAT_OK = 0,/* 正常 */
            HD_STAT_UNFORMATTED = 1,/* 未格式化 */
            HD_STAT_ERROR = 2,/* 错误 */
            HD_STAT_SMART_FAILED = 3,/* SMART状态 */
            HD_STAT_MISMATCH = 4,/* 不匹配 */
            HD_STAT_IDLE = 5, /* 休眠*/
            NET_HD_STAT_OFFLINE = 6,/*网络盘处于未连接状态 */
        }

        //本地硬盘信息配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_HD
        {
            public uint dwHDNo;/*硬盘号, 取值0~MAX_DISKNUM_V30-1*/
            public uint dwCapacity;/*硬盘容量(不可设置)*/
            public uint dwFreeSpace;/*硬盘剩余空间(不可设置)*/
            public uint dwHdStatus;/*硬盘状态(不可设置) HD_STAT*/
            public byte byHDAttr;/*0-默认, 1-冗余; 2-只读*/
            public byte byHDType;/*0-本地硬盘,1-ESATA硬盘,2-NAS硬盘*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwHdGroup; /*属于哪个盘组 1-MAX_HD_GROUP*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HDCFG
        {
            public uint dwSize;
            public uint dwHDCount;/*硬盘数(不可设置)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_HD[] struHDInfo;//硬盘相关操作都需要重启才能生效；
        }

        //本地盘组信息配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_HDGROUP
        {
            public uint dwHDGroupNo;/*盘组号(不可设置) 1-MAX_HD_GROUP*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byHDGroupChans;/*盘组对应的录像通道, 0-表示该通道不录象到该盘组，1-表示录象到该盘组*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HDGROUP_CFG
        {
            public uint dwSize;
            public uint dwHDGroupCount;/*盘组总数(不可设置)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_HD_GROUP, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_HDGROUP[] struHDGroupAttr;//硬盘相关操作都需要重启才能生效
        }

        //配置缩放参数的结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SCALECFG
        {
            public uint dwSize;
            public uint dwMajorScale;/* 主显示 0-不缩放，1-缩放*/
            public uint dwMinorScale;/* 辅显示 0-不缩放，1-缩放*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        //DVR报警输出(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmOutName;/* 名称 */
            public uint dwAlarmOutDelay;/* 输出保持时间(-1为无限，手动关闭) */
            //0-5秒,1-10秒,2-30秒,3-1分钟,4-2分钟,5-5分钟,6-10分钟,7-手动
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmOutTime;/* 报警输出激活时间段 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //DVR报警输出
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmOutName;/* 名称 */
            public uint dwAlarmOutDelay;/* 输出保持时间(-1为无限，手动关闭) */
            //0-5秒,1-10秒,2-30秒,3-1分钟,4-2分钟,5-5分钟,6-10分钟,7-手动
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmOutTime;/* 报警输出激活时间段 */
        }

        //DVR本地预览参数(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PREVIEWCFG_V30
        {
            public uint dwSize;
            public byte byPreviewNumber;//预览数目,0-1画面,1-4画面,2-9画面,3-16画面,0xff:最大画面
            public byte byEnableAudio;//是否声音预览,0-不预览,1-预览
            public ushort wSwitchTime;//切换时间,0-不切换,1-5s,2-10s,3-20s,4-30s,5-60s,6-120s,7-300s
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PREVIEW_MODE * MAX_WINDOW_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySwitchSeq;//切换顺序,如果lSwitchSeq[i]为 0xff表示不用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //DVR本地预览参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PREVIEWCFG
        {
            public uint dwSize;
            public byte byPreviewNumber;//预览数目,0-1画面,1-4画面,2-9画面,3-16画面,0xff:最大画面
            public byte byEnableAudio;//是否声音预览,0-不预览,1-预览
            public ushort wSwitchTime;//切换时间,0-不切换,1-5s,2-10s,3-20s,4-30s,5-60s,6-120s,7-300s
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_WINDOW, ArraySubType = UnmanagedType.I1)]
            public byte[] bySwitchSeq;//切换顺序,如果lSwitchSeq[i]为 0xff表示不用
        }

        //DVR视频输出
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VGAPARA
        {
            public ushort wResolution;/* 分辨率 */
            public ushort wFreq;/* 刷新频率 */
            public uint dwBrightness;/* 亮度 */
        }

        //MATRIX输出参数结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIXPARA_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.U2)]
            public ushort[] wOrder;/* 预览顺序, 0xff表示相应的窗口不预览 */
            public ushort wSwitchTime;// 预览切换时间 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIXPARA
        {
            public ushort wDisplayLogo;/* 显示视频通道号 */
            public ushort wDisplayOsd;/* 显示时间 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VOOUT
        {
            public byte byVideoFormat;/* 输出制式,0-PAL,1-NTSC */
            public byte byMenuAlphaValue;/* 菜单与背景图象对比度 */
            public ushort wScreenSaveTime;/* 屏幕保护时间 0-从不,1-1分钟,2-2分钟,3-5分钟,4-10分钟,5-20分钟,6-30分钟 */
            public ushort wVOffset;/* 视频输出偏移 */
            public ushort wBrightness;/* 视频输出亮度 */
            public byte byStartMode;/* 启动后视频输出模式(0:菜单,1:预览)*/
            public byte byEnableScaler;/* 是否启动缩放 (0-不启动, 1-启动)*/
        }

        //DVR视频输出(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOOUT_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VIDEOOUT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VOOUT[] struVOOut;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VGA_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VGAPARA[] struVGAPara;/* VGA参数 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_MATRIXOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIXPARA_V30[] struMatrixPara;/* MATRIX参数 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //DVR视频输出
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOOUT
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VIDEOOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VOOUT[] struVOOut;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VGA, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VGAPARA[] struVGAPara;	/* VGA参数 */
            public NET_DVR_MATRIXPARA struMatrixPara;/* MATRIX参数 */
        }

        //单用户参数(子结构)(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_INFO_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalRight;/* 本地权限 */
            /*数组0: 本地控制云台*/
            /*数组1: 本地手动录象*/
            /*数组2: 本地回放*/
            /*数组3: 本地设置参数*/
            /*数组4: 本地查看状态、日志*/
            /*数组5: 本地高级操作(升级，格式化，重启，关机)*/
            /*数组6: 本地查看参数 */
            /*数组7: 本地管理模拟和IP camera */
            /*数组8: 本地备份 */
            /*数组9: 本地关机/重启 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.I1)]
            public byte[] byRemoteRight;/* 远程权限 */
            /*数组0: 远程控制云台*/
            /*数组1: 远程手动录象*/
            /*数组2: 远程回放 */
            /*数组3: 远程设置参数*/
            /*数组4: 远程查看状态、日志*/
            /*数组5: 远程高级操作(升级，格式化，重启，关机)*/
            /*数组6: 远程发起语音对讲*/
            /*数组7: 远程预览*/
            /*数组8: 远程请求报警上传、报警输出*/
            /*数组9: 远程控制，本地输出*/
            /*数组10: 远程控制串口*/
            /*数组11: 远程查看参数 */
            /*数组12: 远程管理模拟和IP camera */
            /*数组13: 远程关机/重启 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPreviewRight;/* 远程可以预览的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalPlaybackRight;/* 本地可以回放的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPlaybackRight;/* 远程可以回放的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalRecordRight;/* 本地可以录像的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetRecordRight;/* 远程可以录像的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalPTZRight;/* 本地可以PTZ的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPTZRight;/* 远程可以PTZ的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalBackupRight;/* 本地备份权限通道 0-有权限，1-无权限*/
            public NET_DVR_IPADDR struUserIP;/* 用户IP地址(为0时表示允许任何地址) */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/* 物理地址 */
            public byte byPriority;/* 优先级，0xff-无，0--低，1--中，2--高 */
            /*
            无……表示不支持优先级的设置
            低……默认权限:包括本地和远程回放,本地和远程查看日志和状态,本地和远程关机/重启
            中……包括本地和远程控制云台,本地和远程手动录像,本地和远程回放,语音对讲和远程预览
                  本地备份,本地/远程关机/重启
            高……管理员
            */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 17, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //单用户参数(SDK_V15扩展)(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_USER_INFO_EX
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwLocalRight;/* 权限 */
            /*数组0: 本地控制云台*/
            /*数组1: 本地手动录象*/
            /*数组2: 本地回放*/
            /*数组3: 本地设置参数*/
            /*数组4: 本地查看状态、日志*/
            /*数组5: 本地高级操作(升级，格式化，重启，关机)*/
            public uint dwLocalPlaybackRight;/* 本地可以回放的通道 bit0 -- channel 1*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRemoteRight;/* 权限 */
            /*数组0: 远程控制云台*/
            /*数组1: 远程手动录象*/
            /*数组2: 远程回放 */
            /*数组3: 远程设置参数*/
            /*数组4: 远程查看状态、日志*/
            /*数组5: 远程高级操作(升级，格式化，重启，关机)*/
            /*数组6: 远程发起语音对讲*/
            /*数组7: 远程预览*/
            /*数组8: 远程请求报警上传、报警输出*/
            /*数组9: 远程控制，本地输出*/
            /*数组10: 远程控制串口*/
            public uint dwNetPreviewRight;/* 远程可以预览的通道 bit0 -- channel 1*/
            public uint dwNetPlaybackRight;/* 远程可以回放的通道 bit0 -- channel 1*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sUserIP;/* 用户IP地址(为0时表示允许任何地址) */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/* 物理地址 */
        }

        //单用户参数(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_USER_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwLocalRight;/* 权限 */
            /*数组0: 本地控制云台*/
            /*数组1: 本地手动录象*/
            /*数组2: 本地回放*/
            /*数组3: 本地设置参数*/
            /*数组4: 本地查看状态、日志*/
            /*数组5: 本地高级操作(升级，格式化，重启，关机)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRemoteRight;/* 权限 */
            /*数组0: 远程控制云台*/
            /*数组1: 远程手动录象*/
            /*数组2: 远程回放 */
            /*数组3: 远程设置参数*/
            /*数组4: 远程查看状态、日志*/
            /*数组5: 远程高级操作(升级，格式化，重启，关机)*/
            /*数组6: 远程发起语音对讲*/
            /*数组7: 远程预览*/
            /*数组8: 远程请求报警上传、报警输出*/
            /*数组9: 远程控制，本地输出*/
            /*数组10: 远程控制串口*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sUserIP;/* 用户IP地址(为0时表示允许任何地址) */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/* 物理地址 */
        }

        //DVR用户参数(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO_V30[] struUser;
        }

        //DVR用户参数(SDK_V15扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO_EX[] struUser;
        }

        //DVR用户参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO[] struUser;
        }

        //DVR异常参数(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EXCEPTION_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EXCEPTIONNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_HANDLEEXCEPTION_V30[] struExceptionHandleType;
            /*数组0-盘满,1- 硬盘出错,2-网线断,3-局域网内IP 地址冲突, 4-非法访问, 5-输入/输出视频制式不匹配, 6-视频信号异常, 7-录像异常*/
        }

        //DVR异常参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EXCEPTION
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EXCEPTIONNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_HANDLEEXCEPTION[] struExceptionHandleType;
            /*数组0-盘满,1- 硬盘出错,2-网线断,3-局域网内IP 地址冲突,4-非法访问, 5-输入/输出视频制式不匹配, 6-视频信号异常*/
        }

        //通道状态(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CHANNELSTATE_V30
        {
            public byte byRecordStatic;//通道是否在录像,0-不录像,1-录像
            public byte bySignalStatic;//连接的信号状态,0-正常,1-信号丢失
            public byte byHardwareStatic;//通道硬件状态,0-正常,1-异常,例如DSP死掉
            public byte byRes1;//保留
            public uint dwBitRate;//实际码率
            public uint dwLinkNum;//客户端连接的个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LINK, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPADDR[] struClientIP;//客户端的IP地址
            public uint dwIPLinkNum;//如果该通道为IP接入，那么表示IP接入当前的连接数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                struClientIP = new NET_DVR_IPADDR[MAX_LINK];

                for (int i = 0; i < MAX_LINK; i++)
                {
                    struClientIP[i].Init();
                }
                byRes = new byte[12];
            }
        }

        //通道状态
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CHANNELSTATE
        {
            public byte byRecordStatic;//通道是否在录像,0-不录像,1-录像
            public byte bySignalStatic;//连接的信号状态,0-正常,1-信号丢失
            public byte byHardwareStatic;//通道硬件状态,0-正常,1-异常,例如DSP死掉
            public byte reservedData;//保留
            public uint dwBitRate;//实际码率
            public uint dwLinkNum;//客户端连接的个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LINK, ArraySubType = UnmanagedType.U4)]
            public uint[] dwClientIP;//客户端的IP地址
        }

        //硬盘状态
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISKSTATE
        {
            public uint dwVolume;//硬盘的容量
            public uint dwFreeSpace;//硬盘的剩余空间
            public uint dwHardDiskStatic;//硬盘的状态,0-活动,1-休眠,2-不正常
        }

        //DVR工作状态(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE_V30
        {
            public uint dwDeviceStatic;//设备的状态,0-正常,1-CPU占用率太高,超过85%,2-硬件错误,例如串口死掉
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE_V30[] struChanStatic;//通道的状态
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatic;//报警端口的状态,0-没有报警,1-有报警
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutStatic;//报警输出端口的状态,0-没有输出,1-有报警输出
            public uint dwLocalDisplay;//本地显示状态,0-正常,1-不正常
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAudioChanStatus;//表示语音通道的状态 0-未使用，1-使用中, 0xff无效
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                struHardDiskStatic = new NET_DVR_DISKSTATE[MAX_DISKNUM_V30];
                struChanStatic = new NET_DVR_CHANNELSTATE_V30[MAX_CHANNUM_V30];
                for (int i = 0; i < MAX_CHANNUM_V30; i++)
                {
                    struChanStatic[i].Init();
                }
                byAlarmInStatic = new byte[MAX_ALARMOUT_V30];
                byAlarmOutStatic = new byte[MAX_ALARMOUT_V30];
                byAudioChanStatus = new byte[MAX_AUDIO_V30];
                byRes = new byte[10];
            }
        }

        //DVR工作状态
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE
        {
            public uint dwDeviceStatic;//设备的状态,0-正常,1-CPU占用率太高,超过85%,2-硬件错误,例如串口死掉
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE[] struChanStatic;//通道的状态
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatic;//报警端口的状态,0-没有报警,1-有报警
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutStatic;//报警输出端口的状态,0-没有输出,1-有报警输出
            public uint dwLocalDisplay;//本地显示状态,0-正常,1-不正常

            public void Init()
            {
                struHardDiskStatic = new NET_DVR_DISKSTATE[MAX_DISKNUM];
                struChanStatic = new NET_DVR_CHANNELSTATE[MAX_CHANNUM];
                byAlarmInStatic = new byte[MAX_ALARMIN];
                byAlarmOutStatic = new byte[MAX_ALARMOUT];
            }
        }

        /************************DVR日志 begin***************************/
        /* 报警 */
        //主类型
        public const int MAJOR_ALARM = 1;
        //次类型
        public const int MINOR_ALARM_IN = 1;/* 报警输入 */
        public const int MINOR_ALARM_OUT = 2;/* 报警输出 */
        public const int MINOR_MOTDET_START = 3; /* 移动侦测报警开始 */
        public const int MINOR_MOTDET_STOP = 4; /* 移动侦测报警结束 */
        public const int MINOR_HIDE_ALARM_START = 5;/* 遮挡报警开始 */
        public const int MINOR_HIDE_ALARM_STOP = 6;/* 遮挡报警结束 */
        public const int MINOR_VCA_ALARM_START = 7;/*智能报警开始*/
        public const int MINOR_VCA_ALARM_STOP = 8;/*智能报警停止*/
        public const int MINOR_ITS_ALARM_START = 0x09; // 交通事件报警开始 
        public const int MINOR_ITS_ALARM_STOP = 0x0a; // 交通事件报警结束 
        public const int MINOR_NETALARM_START = 0x0b; // 网络报警开始 
        public const int MINOR_NETALARM_STOP = 0x0c; // 网络报警结束 
        public const int MINOR_NETALARM_RESUME = 0x0d; // 网络报警恢复 
        public const int MINOR_WIRELESS_ALARM_START = 0x0e; // 无线报警开始 
        public const int MINOR_WIRELESS_ALARM_STOP = 0x0f; // 无线报警结束 
        public const int MINOR_PIR_ALARM_START = 0x10; // 人体感应报警开始 
        public const int MINOR_PIR_ALARM_STOP = 0x11; // 人体感应报警结束 
        public const int MINOR_CALLHELP_ALARM_START = 0x12; // 呼救报警开始 
        public const int MINOR_CALLHELP_ALARM_STOP = 0x13; // 呼救报警结束 
        public const int MINOR_DETECTFACE_ALARM_START = 0x16; // 人脸侦测报警开始 
        public const int MINOR_DETECTFACE_ALARM_STOP = 0x17; // 人脸侦测报警结束 
        public const int MINOR_VQD_ALARM_START = 0x18; //VQD报警 
        public const int MINOR_VQD_ALARM_STOP = 0x19; //VQD报警结束 
        public const int MINOR_VCA_SECNECHANGE_DETECTION = 0x1a; // 场景侦测报警 
        public const int MINOR_SMART_REGION_EXITING_BEGIN = 0x1b; // 离开区域侦测开始 
        public const int MINOR_SMART_REGION_EXITING_END = 0x1c; // 离开区域侦测结束 
        public const int MINOR_SMART_LOITERING_BEGIN = 0x1d; // 徘徊侦测开始 
        public const int MINOR_SMART_LOITERING_END = 0x1e; // 徘徊侦测结束 
        public const int MINOR_VCA_ALARM_LINE_DETECTION_BEGIN = 0x20; // 越界侦测开始 
        public const int MINOR_VCA_ALARM_LINE_DETECTION_END = 0x21; // 越界侦测结束 
        public const int MINOR_VCA_ALARM_INTRUDE_BEGIN = 0x22; // 区域入侵侦测开始 
        public const int MINOR_VCA_ALARM_INTRUDE_END = 0x23; // 区域入侵侦测结束 
        public const int MINOR_VCA_ALARM_AUDIOINPUT = 0x24; // 音频丢失侦测 
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL = 0x25; // 音频异常侦测 
        public const int MINOR_VCA_DEFOCUS_DETECTION_BEGIN = 0x26; // 虚焦侦测开始 
        public const int MINOR_VCA_DEFOCUS_DETECTION_END = 0x27; // 虚焦侦测结束
        public const int MINOR_EXT_ALARM = 0x28; // IPC外部报警
        public const int MINOR_VCA_FACE_ALARM_BEGIN = 0x29; // 人脸侦测开始 
        public const int MINOR_SMART_REGION_ENTRANCE_BEGIN = 0x2a; // 进入区域侦测开始 
        public const int MINOR_SMART_REGION_ENTRANCE_END = 0x2b; // 进入区域侦测结束 
        public const int MINOR_SMART_PEOPLE_GATHERING_BEGIN = 0x2c; // 人员聚集侦测开始 
        public const int MINOR_SMART_PEOPLE_GATHERING_END = 0x2d; // 人员聚集侦测结束 
        public const int MINOR_SMART_FAST_MOVING_BEGIN = 0x2e; // 快速运动侦测开始 
        public const int MINOR_SMART_FAST_MOVING_END = 0x2f; // 快速运动侦测结束 
        public const int MINOR_VCA_FACE_ALARM_END = 0x30; // 人脸侦测结束 
        public const int MINOR_VCA_SCENE_CHANGE_ALARM_BEGIN = 0x31; // 场景变更侦测开始 
        public const int MINOR_VCA_SCENE_CHANGE_ALARM_END = 0x32; // 场景变更侦测结束 
        public const int MINOR_VCA_ALARM_AUDIOINPUT_BEGIN = 0x33; // 音频丢失侦测开始 
        public const int MINOR_VCA_ALARM_AUDIOINPUT_END = 0x34; // 音频丢失侦测结束 
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL_BEGIN = 0x35; // 声强突变侦测开始 
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL_END = 0x36; // 声强突变侦测结束 
        
        public const int MINOR_VCA_LECTURE_DETECTION_BEGIN = 0x37;  //授课侦测开始报警
        public const int MINOR_VCA_LECTURE_DETECTION_END = 0x38;  //授课侦测结束报警
        public const int MINOR_VCA_ALARM_AUDIOSTEEPDROP = 0x39;  //声强陡降 2014-03-21
        public const int MINOR_VCA_ANSWER_DETECTION_BEGIN = 0x3a;  //回答问题侦测开始报警
        public const int MINOR_VCA_ANSWER_DETECTION_END = 0x3b;  //回答问题侦测结束报警

        public const int MINOR_SMART_PARKING_BEGIN = 0x3c; // 停车侦测开始 
        public const int MINOR_SMART_PARKING_END = 0x3d; // 停车侦测结束 
        public const int MINOR_SMART_UNATTENDED_BAGGAGE_BEGIN = 0x3e; // 物品遗留侦测开始 
        public const int MINOR_SMART_UNATTENDED_BAGGAGE_END = 0x3f; // 物品遗留侦测结束 
        public const int MINOR_SMART_OBJECT_REMOVAL_BEGIN = 0x40; // 物品拿取侦测开始 
        public const int MINOR_SMART_OBJECT_REMOVAL_END = 0x41; // 物品拿取侦测结束 
        public const int MINOR_SMART_VEHICLE_ALARM_START = 0x46;   //车牌检测开始
        public const int MINOR_SMART_VEHICLE_ALARM_STOP = 0x47;   //车牌检测结束
        public const int MINOR_THERMAL_FIREDETECTION = 0x48;   //热成像火点检测侦测开始
        public const int MINOR_THERMAL_FIREDETECTION_END = 0x49;   //热成像火点检测侦测结束
        public const int MINOR_SMART_VANDALPROOF_BEGIN = 0x50;   //防破坏检测开始
        public const int MINOR_SMART_VANDALPROOF_END = 0x51; //防破坏检测结束

        public const int MINOR_ALARMIN_SHORT_CIRCUIT = 0x400; // 防区短路报警 
        public const int MINOR_ALARMIN_BROKEN_CIRCUIT = 0x401; // 防区断路报警 
        public const int MINOR_ALARMIN_EXCEPTION = 0x402; // 防区异常报警 
        public const int MINOR_ALARMIN_RESUME = 0x403; // 防区报警恢复 
        public const int MINOR_HOST_DESMANTLE_ALARM = 0x404 ; //防区防拆报警  
        public const int MINOR_HOST_DESMANTLE_RESUME = 0x405; // 防区防拆恢复 
        public const int MINOR_CARD_READER_DESMANTLE_ALARM = 0x406 ; //读卡器防拆报警 
        public const int MINOR_CARD_READER_DESMANTLE_RESUME = 0x407; // 读卡器防拆恢复  
        public const int MINOR_CASE_SENSOR_ALARM = 0x408; // 事件输入报警 
        public const int MINOR_CASE_SENSOR_RESUME = 0x409; // 事件输入恢复 
        public const int MINOR_STRESS_ALARM = 0x40a; // 胁迫报警 
        public const int MINOR_OFFLINE_ECENT_NEARLY_FULL = 0x40b; // 离线事件满90%报警 
        public const int MINOR_CARD_MAX_AUTHENTICATE_FAIL = 0x40c; // 卡号认证失败超次报警 
        public const int MINOR_SD_CARD_FULL = 0x40d;  //SD卡存储满报警
        public const int MINOR_LINKAGE_CAPTURE_PIC = 0x40e;  //联动抓拍事件报警

        /* 异常 */
        //主类型
        public const int MAJOR_EXCEPTION = 2;
        //次类型
        public const int MINOR_VI_LOST = 33;/* 视频信号丢失 */
        public const int MINOR_ILLEGAL_ACCESS = 34;/* 非法访问 */
        public const int MINOR_HD_FULL = 35;/* 硬盘满 */
        public const int MINOR_HD_ERROR = 36;/* 硬盘错误 */
        public const int MINOR_DCD_LOST = 37;/* MODEM 掉线(保留不使用) */
        public const int MINOR_IP_CONFLICT = 38;/* IP地址冲突 */
        public const int MINOR_NET_BROKEN = 39;/* 网络断开*/
        public const int MINOR_REC_ERROR = 40;/* 录像出错 */
        public const int MINOR_IPC_NO_LINK = 41;/* IPC连接异常 */
        public const int MINOR_VI_EXCEPTION = 42;/* 视频输入异常(只针对模拟通道) */
        public const int MINOR_IPC_IP_CONFLICT = 43;/*ipc ip 地址 冲突*/
        public const int MINOR_RAID_ERROR = 0x20; // 阵列异常  
        public const int MINOR_SENCE_EXCEPTION = 0x2c; // 场景异常 
        public const int MINOR_PIC_REC_ERROR = 0x2d; // 抓图出错,获取图片文件失败 
        public const int MINOR_VI_MISMATCH = 0x2e; // 视频制式不匹配 
        public const int MINOR_RESOLUTION_MISMATCH = 0x2f; // 编码分辨率和前端分辨率不匹配 

        //2010-01-22 增加视频综合平台异常日志次类型
        public const int MINOR_NET_ABNORMAL = 0x35; /*网络状态异常*/
        public const int MINOR_MEM_ABNORMAL = 0x36; /*内存状态异常*/
        public const int MINOR_FILE_ABNORMAL = 0x37; /*文件状态异常*/
        public const int MINOR_PANEL_ABNORMAL = 0x38; /*前面板连接异常*/
        public const int MINOR_PANEL_RESUME = 0x39; /*前面板恢复正常*/	
        public const int MINOR_RS485_DEVICE_ABNORMAL = 0x3a; /*RS485连接状态异常*/
        public const int MINOR_RS485_DEVICE_REVERT = 0x3b; /*RS485连接状态异常恢复*/
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALREBOOT = 0x3c; // 子板异常启动 
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALINSERT = 0x3d; // 子板插入 
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALPULLOUT = 0x3e; // 子板拔出 
        public const int MINOR_SCREEN_ABNARMALTEMPERATURE = 0x3f; // 温度异常 
        public const int MINOR_RECORD_OVERFLOW = 0x41; // 缓冲区溢出 
        public const int MINOR_DSP_ABNORMAL = 0x42; // DSP异常 
        public const int MINOR_ANR_RECORD_FAIED = 0x43; // ANR录像失败 
        public const int MINOR_SPARE_WORK_DEVICE_EXCEPT = 0x44; // 热备设备工作机异常 
        public const int MINOR_START_IPC_MAS_FAILED = 0x45; // 开启IPC MAS失败 
        public const int MINOR_IPCM_CRASH = 0x46; // IPCM异常重启 
        public const int MINOR_POE_POWER_EXCEPTION = 0x47; // POE供电异常 
        public const int MINOR_UPLOAD_DATA_CS_EXCEPTION = 0x48; // 云存储数据上传失败 
        public const int MINOR_DIAL_EXCEPTION = 0x49;         /*拨号异常*/
        public const int MINOR_DEV_EXCEPTION_OFFLINE = 0x50;  //设备异常下线
        public const int MINOR_UPGRADEFAIL = 0x51; //远程升级设备失败
        public const int MINOR_AI_LOST = 0x52; /* 音频信号丢失 */

        public const int MINOR_DEV_POWER_ON = 0x400; // 设备上电启动 
        public const int MINOR_DEV_POWER_OFF = 0x401; // 设备掉电关闭 
        public const int MINOR_WATCH_DOG_RESET = 0x402; // 看门狗复位 
        public const int MINOR_LOW_BATTERY = 0x403; // 蓄电池电压低 
        public const int MINOR_BATTERY_RESUME = 0x404; // 蓄电池电压恢复正常 
        public const int MINOR_AC_OFF = 0x405; // 交流电断电 
        public const int MINOR_AC_RESUME = 0x406; // 交流电恢复 
        public const int MINOR_NET_RESUME = 0x407; // 网络恢复 
        public const int MINOR_FLASH_ABNORMAL = 0x408; // FLASH读写异常 
        public const int MINOR_CARD_READER_OFFLINE = 0x409; // 读卡器掉线 
        public const int MINOR_CARD_READER_RESUME = 0x40a; // 读卡器掉线恢复 
        public const int MINOR_SUBSYSTEM_IP_CONFLICT = 0x4000; // 子板IP冲突 
        public const int MINOR_SUBSYSTEM_NET_BROKEN = 0x4001; // 子板断网 
        public const int MINOR_FAN_ABNORMAL = 0x4002; // 风扇异常 
        public const int MINOR_BACKPANEL_TEMPERATURE_ABNORMAL = 0x4003; // 背板温度异常 

        //视频综合平台
        public const int MINOR_FANABNORMAL = 49;/* 视频综合平台：风扇状态异常 */
        public const int MINOR_FANRESUME = 50;/* 视频综合平台：风扇状态恢复正常 */
        public const int MINOR_SUBSYSTEM_ABNORMALREBOOT = 51;/* 视频综合平台：6467异常重启 */
        public const int MINOR_MATRIX_STARTBUZZER = 52;/* 视频综合平台：dm6467异常，启动蜂鸣器 */

        /* 操作 */
        //主类型
        public const int MAJOR_OPERATION = 3;

        //次类型
        public const int MINOR_VCA_MOTIONEXCEPTION = 0x29; //智能侦测异常
        public const int MINOR_START_DVR = 0x41; // 开机 
        public const int MINOR_STOP_DVR = 0x42; // 关机 
        public const int MINOR_STOP_ABNORMAL = 0x43; // 异常关机 
        public const int MINOR_REBOOT_DVR = 0x44; // 本地重启设备 

        public const int MINOR_LOCAL_LOGIN = 0x50; // 本地登陆 
        public const int MINOR_LOCAL_LOGOUT = 0x51; // 本地注销登陆 
        public const int MINOR_LOCAL_CFG_PARM = 0x52; // 本地配置参数 
        public const int MINOR_LOCAL_PLAYBYFILE = 0x53; // 本地按文件回放或下载 
        public const int MINOR_LOCAL_PLAYBYTIME = 0x54; // 本地按时间回放或下载 
        public const int MINOR_LOCAL_START_REC = 0x55; // 本地开始录像 
        public const int MINOR_LOCAL_STOP_REC = 0x56; // 本地停止录像 
        public const int MINOR_LOCAL_PTZCTRL = 0x57; // 本地云台控制 
        public const int MINOR_LOCAL_PREVIEW = 0x58; // 本地预览(保留不使用) 
        public const int MINOR_LOCAL_MODIFY_TIME = 0x59; // 本地修改时间(保留不使用) 
        public const int MINOR_LOCAL_UPGRADE = 0x5a; // 本地升级 
        public const int MINOR_LOCAL_RECFILE_OUTPUT = 0x5b; // 本地备份录象文件 
        public const int MINOR_LOCAL_FORMAT_HDD = 0x5c; // 本地初始化硬盘 
        public const int MINOR_LOCAL_CFGFILE_OUTPUT = 0x5d; // 导出本地配置文件 
        public const int MINOR_LOCAL_CFGFILE_INPUT = 0x5e; // 导入本地配置文件 
        public const int MINOR_LOCAL_COPYFILE = 0x5f; // 本地备份文件 
        public const int MINOR_LOCAL_LOCKFILE = 0x60; // 本地锁定录像文件 
        public const int MINOR_LOCAL_UNLOCKFILE = 0x61; // 本地解锁录像文件 
        public const int MINOR_LOCAL_DVR_ALARM = 0x62; // 本地手动清除和触发报警 
        public const int MINOR_IPC_ADD = 0x63; // 本地添加IPC 
        public const int MINOR_IPC_DEL = 0x64; // 本地删除IPC 
        public const int MINOR_IPC_SET = 0x65; // 本地设置IPC 
        public const int MINOR_LOCAL_START_BACKUP = 0x66; // 本地开始备份 
        public const int MINOR_LOCAL_STOP_BACKUP = 0x67; // 本地停止备份 
        public const int MINOR_LOCAL_COPYFILE_START_TIME = 0x68; // 本地备份开始时间 
        public const int MINOR_LOCAL_COPYFILE_END_TIME = 0x69; // 本地备份结束时间 
        public const int MINOR_LOCAL_ADD_NAS = 0x6a; // 本地添加网络硬盘 
        public const int MINOR_LOCAL_DEL_NAS = 0x6b; // 本地删除NAS盘 
        public const int MINOR_LOCAL_SET_NAS = 0x6c; // 本地设置NAS盘 
        public const int MINOR_LOCAL_RESET_PASSWD = 0x6d; /* 本地恢复管理员默认密码*/ 

        public const int MINOR_REMOTE_LOGIN = 0x70; // 远程登录 
        public const int MINOR_REMOTE_LOGOUT = 0x71; // 远程注销登陆 
        public const int MINOR_REMOTE_START_REC = 0x72; // 远程开始录像 
        public const int MINOR_REMOTE_STOP_REC = 0x73; // 远程停止录像 
        public const int MINOR_START_TRANS_CHAN = 0x74; // 开始透明传输 
        public const int MINOR_STOP_TRANS_CHAN = 0x75; // 停止透明传输 
        public const int MINOR_REMOTE_GET_PARM = 0x76; // 远程获取参数 
        public const int MINOR_REMOTE_CFG_PARM = 0x77; // 远程配置参数 
        public const int MINOR_REMOTE_GET_STATUS = 0x78; // 远程获取状态 
        public const int MINOR_REMOTE_ARM = 0x79; // 远程布防 
        public const int MINOR_REMOTE_DISARM = 0x7a; // 远程撤防 
        public const int MINOR_REMOTE_REBOOT = 0x7b; // 远程重启 
        public const int MINOR_START_VT = 0x7c; // 开始语音对讲 
        public const int MINOR_STOP_VT = 0x7d; // 停止语音对讲 
        public const int MINOR_REMOTE_UPGRADE = 0x7e; // 远程升级 
        public const int MINOR_REMOTE_PLAYBYFILE = 0x7f; // 远程按文件回放 
        public const int MINOR_REMOTE_PLAYBYTIME = 0x80; // 远程按时间回放 
        public const int MINOR_REMOTE_PTZCTRL = 0x81; // 远程云台控制 
        public const int MINOR_REMOTE_FORMAT_HDD = 0x82; // 远程格式化硬盘 
        public const int MINOR_REMOTE_STOP = 0x83; // 远程关机 
        public const int MINOR_REMOTE_LOCKFILE = 0x84; // 远程锁定文件 
        public const int MINOR_REMOTE_UNLOCKFILE = 0x85; // 远程解锁文件 
        public const int MINOR_REMOTE_CFGFILE_OUTPUT = 0x86; // 远程导出配置文件 
        public const int MINOR_REMOTE_CFGFILE_INTPUT = 0x87; // 远程导入配置文件 
        public const int MINOR_REMOTE_RECFILE_OUTPUT = 0x88; // 远程导出录象文件 
        public const int MINOR_REMOTE_DVR_ALARM = 0x89; // 远程手动清除和触发报警 
        public const int MINOR_REMOTE_IPC_ADD = 0x8a; // 远程添加IPC 
        public const int MINOR_REMOTE_IPC_DEL = 0x8b; // 远程删除IPC 
        public const int MINOR_REMOTE_IPC_SET = 0x8c; // 远程设置IPC 
        public const int MINOR_REBOOT_VCA_LIB = 0x8d; // 重启智能库 
        public const int MINOR_REMOTE_ADD_NAS = 0x8e; // 远程添加NAS盘 
        public const int MINOR_REMOTE_DEL_NAS = 0x8f; // 远程删除NAS盘 

        public const int MINOR_REMOTE_SET_NAS = 0x90; // 远程设置NAS盘 
        public const int MINOR_LOCAL_START_REC_CDRW = 0x91; // 本地开始刻录 
        public const int MINOR_LOCAL_STOP_REC_CDRW = 0x92; // 本地停止刻录 
        public const int MINOR_REMOTE_START_REC_CDRW = 0x93; // 远程开始刻录 
        public const int MINOR_REMOTE_STOP_REC_CDRW = 0x94; // 远程停止刻录 
        public const int MINOR_LOCAL_PIC_OUTPUT = 0x95; // 本地备份图片文件 
        public const int MINOR_REMOTE_PIC_OUTPUT = 0x96; // 远程备份图片文件 
        public const int MINOR_LOCAL_INQUEST_RESUME = 0x97; // 本地恢复审讯事件 
        public const int MINOR_REMOTE_INQUEST_RESUME = 0x98; // 远程恢复审讯事件 
        public const int MINOR_LOCAL_ADD_FILE = 0x99; // 本地导入文件 
        public const int MINOR_REMOTE_DELETE_HDISK = 0x9a; // 远程删除异常不存在的硬盘 
        public const int MINOR_REMOTE_LOAD_HDISK = 0x9b; // 远程加载硬盘 
        public const int MINOR_REMOTE_UNLOAD_HDISK = 0x9c; // 远程卸载硬盘 
        public const int MINOR_LOCAL_OPERATE_LOCK = 0x9d; // 本地操作锁定 
        public const int MINOR_LOCAL_OPERATE_UNLOCK = 0x9e; // 本地操作解除锁定 
        public const int MINOR_LOCAL_DEL_FILE = 0x9f; // 本地删除审讯文件 

        public const int MINOR_SUBSYSTEMREBOOT = 0xa0; /*视频综合平台：dm6467 正常重启*/
        public const int MINOR_MATRIX_STARTTRANSFERVIDEO = 0xa1; /*视频综合平台：矩阵切换开始传输图像*/
        public const int MINOR_MATRIX_STOPTRANSFERVIDEO = 0xa2; /*视频综合平台：矩阵切换停止传输图像*/
        public const int MINOR_REMOTE_SET_ALLSUBSYSTEM = 0xa3; /*视频综合平台：设置所有6467子系统信息*/
        public const int MINOR_REMOTE_GET_ALLSUBSYSTEM = 0xa4; /*视频综合平台：获取所有6467子系统信息*/
        public const int MINOR_REMOTE_SET_PLANARRAY = 0xa5; /*视频综合平台：设置计划轮巡组*/
        public const int MINOR_REMOTE_GET_PLANARRAY = 0xa6; /*视频综合平台：获取计划轮巡组*/
        public const int MINOR_MATRIX_STARTTRANSFERAUDIO = 0xa7; /*视频综合平台：矩阵切换开始传输音频*/
        public const int MINOR_MATRIX_STOPRANSFERAUDIO = 0xa8; /*视频综合平台：矩阵切换停止传输音频*/
        public const int MINOR_LOGON_CODESPITTER = 0xa9; /*视频综合平台：登陆码分器*/
        public const int MINOR_LOGOFF_CODESPITTER = 0xaa; /*视频综合平台：退出码分器*/

        //KY2013 3.0.0
        public const int MINOR_LOCAL_MAIN_AUXILIARY_PORT_SWITCH = 0X302; //本地主辅口切换
        public const int MINOR_LOCAL_HARD_DISK_CHECK = 0x303; //本地物理硬盘自检
        //2010-01-22 增加视频综合平台中解码器操作日志
        public const int  MINOR_START_DYNAMIC_DECODE = 0xb; /*开始动态解码*/
        public const int  MINOR_STOP_DYNAMIC_DECODE = 0xb1; /*停止动态解码*/
        public const int  MINOR_GET_CYC_CFG = 0xb2; /*获取解码器通道轮巡配置*/
        public const int  MINOR_SET_CYC_CFG = 0xb3; /*设置解码通道轮巡配置*/
        public const int  MINOR_START_CYC_DECODE = 0xb4; /*开始轮巡解码*/
        public const int MINOR_STOP_CYC_DECODE = 0xb5; /*停止轮巡解码*/
        public const int  MINOR_GET_DECCHAN_STATUS = 0xb6; /*获取解码通道状态*/
        public const int  MINOR_GET_DECCHAN_INFO = 0xb7; /*获取解码通道当前信息*/
        public const int  MINOR_START_PASSIVE_DEC = 0xb8; /*开始被动解码*/
        public const int  MINOR_STOP_PASSIVE_DEC = 0xb9; /*停止被动解码*/
        public const int  MINOR_CTRL_PASSIVE_DEC = 0xba; /*控制被动解码*/
        public const int  MINOR_RECON_PASSIVE_DEC = 0xbb; /*被动解码重连*/
        public const int  MINOR_GET_DEC_CHAN_SW = 0xbc; /*获取解码通道总开关*/
        public const int  MINOR_SET_DEC_CHAN_SW = 0xbd; /*设置解码通道总开关*/
        public const int  MINOR_CTRL_DEC_CHAN_SCALE = 0xbe; /*解码通道缩放控制*/
        public const int  MINOR_SET_REMOTE_REPLAY = 0xbf; /*设置远程回放*/
        public const int  MINOR_GET_REMOTE_REPLAY = 0xc0; /*获取远程回放状态*/
        public const int  MINOR_CTRL_REMOTE_REPLAY = 0xc1; /*远程回放控制*/
        public const int  MINOR_SET_DISP_CFG = 0xc2; /*设置显示通道*/
        public const int  MINOR_GET_DISP_CFG = 0xc3; /*获取显示通道设置*/
        public const int  MINOR_SET_PLANTABLE = 0xc4; /*设置计划轮巡表*/
        public const int  MINOR_GET_PLANTABLE = 0xc5;/*获取计划轮巡表*/
        public const int  MINOR_START_PPPPOE = 0xc6; /*开始PPPoE连接*/
        public const int  MINOR_STOP_PPPPOE = 0xc7; /*结束PPPoE连接*/
        public const int  MINOR_UPLOAD_LOGO = 0xc8; /*上传LOGO*/

        //推模式操作日志
        public const int  MINOR_LOCAL_PIN = 0xc9; /* 本地PIN功能操作 */
        public const int  MINOR_LOCAL_DIAL = 0xca; /* 本地手动启动断开拨号 */    
        public const int  MINOR_SMS_CONTROL = 0xcb; /* 短信控制上下线 */    
        public const int  MINOR_CALL_ONLINE = 0xc; /* 呼叫控制上线 */    
        public const int  MINOR_REMOTE_PIN = 0xcd; /* 远程PIN功能操作 */

        public const int MINOR_REMOTE_BYPASS = 0xd0; // 远程旁路 
        public const int MINOR_REMOTE_UNBYPASS = 0xd1; // 远程旁路恢复 
        public const int MINOR_REMOTE_SET_ALARMIN_CFG = 0xd2; // 远程设置报警输入参数 
        public const int MINOR_REMOTE_GET_ALARMIN_CFG = 0xd3; // 远程获取报警输入参数 
        public const int MINOR_REMOTE_SET_ALARMOUT_CFG = 0xd4; // 远程设置报警输出参数 
        public const int MINOR_REMOTE_GET_ALARMOUT_CFG = 0xd5; // 远程获取报警输出参数 
        public const int MINOR_REMOTE_ALARMOUT_OPEN_MAN = 0xd6; // 远程手动开启报警输出 
        public const int MINOR_REMOTE_ALARMOUT_CLOSE_MAN = 0xd7; // 远程手动关闭报警输出 
        public const int MINOR_REMOTE_ALARM_ENABLE_CFG = 0xd8; // 远程设置报警主机的RS485串口使能状态 
        public const int MINOR_DBDATA_OUTPUT = 0xd9; // 导出数据库记录 
        public const int MINOR_DBDATA_INPUT = 0xda; // 导入数据库记录 
        public const int MINOR_MU_SWITCH = 0xdb; // 级联切换 
        public const int MINOR_MU_PTZ = 0xdc; // 级联PTZ控制
        public const int MINOR_DELETE_LOGO = 0xdd; /* 删除logo */

        public const int MINOR_LOCAL_CONF_REB_RAID = 0x101; // 本地配置自动重建 
        public const int MINOR_LOCAL_CONF_SPARE = 0x102; // 本地配置热备 
        public const int MINOR_LOCAL_ADD_RAID = 0x103; // 本地创建阵列 
        public const int MINOR_LOCAL_DEL_RAID = 0x104; // 本地删除阵列 
        public const int MINOR_LOCAL_MIG_RAID = 0x105; // 本地迁移阵列 
        public const int MINOR_LOCAL_REB_RAID = 0x106; // 本地手动重建阵列 
        public const int MINOR_LOCAL_QUICK_CONF_RAID = 0x107; // 本地一键配置 
        public const int MINOR_LOCAL_ADD_VD = 0x108; // 本地创建虚拟磁盘 
        public const int MINOR_LOCAL_DEL_VD = 0x109; // 本地删除虚拟磁盘 
        public const int MINOR_LOCAL_RP_VD = 0x10a; // 本地修复虚拟磁盘 
        public const int MINOR_LOCAL_FORMAT_EXPANDVD = 0x10b; // 本地扩展虚拟磁盘扩容 
        public const int MINOR_LOCAL_RAID_UPGRADE = 0x10c; // 本地raid卡升级 
        public const int MINOR_LOCAL_STOP_RAID = 0x10d; // 本地暂停RAID操作(即安全拔盘) 
        public const int MINOR_REMOTE_CONF_REB_RAID = 0x111; // 远程配置自动重建 
        public const int MINOR_REMOTE_CONF_SPARE = 0x112; // 远程配置热备 
        public const int MINOR_REMOTE_ADD_RAID = 0x113; // 远程创建阵列 
        public const int MINOR_REMOTE_DEL_RAID = 0x114; // 远程删除阵列 
        public const int MINOR_REMOTE_MIG_RAID = 0x115; // 远程迁移阵列 
        public const int MINOR_REMOTE_REB_RAID = 0x116; // 远程手动重建阵列 
        public const int MINOR_REMOTE_QUICK_CONF_RAID = 0x117; // 远程一键配置 
        public const int MINOR_REMOTE_ADD_VD = 0x118; // 远程创建虚拟磁盘 
        public const int MINOR_REMOTE_DEL_VD = 0x119; // 远程删除虚拟磁盘 
        public const int MINOR_REMOTE_RP_VD = 0x11a; // 远程修复虚拟磁盘 
        public const int MINOR_REMOTE_FORMAT_EXPANDVD = 0x11b; // 远程虚拟磁盘扩容 
        public const int MINOR_REMOTE_RAID_UPGRADE = 0x11c; // 远程raid卡升级 
        public const int MINOR_REMOTE_STOP_RAID = 0x11d; // 远程暂停RAID操作(即安全拔盘) 
        public const int MINOR_LOCAL_START_PIC_REC = 0x121; // 本地开始抓图 
        public const int MINOR_LOCAL_STOP_PIC_REC = 0x122; // 本地停止抓图 
        public const int MINOR_LOCAL_SET_SNMP = 0x125; // 本地配置SNMP 
        public const int MINOR_LOCAL_TAG_OPT = 0x126; // 本地标签操作 
        public const int MINOR_REMOTE_START_PIC_REC = 0x131; // 远程开始抓图 
        public const int MINOR_REMOTE_STOP_PIC_REC = 0x132; // 远程停止抓图 
        public const int MINOR_REMOTE_SET_SNMP = 0x135; // 远程配置SNMP 
        public const int MINOR_REMOTE_TAG_OPT = 0x136; // 远程标签操作 

        public const int MINOR_LOCAL_VOUT_SWITCH = 0x140; // 本地输出口切换操作 
        public const int MINOR_STREAM_CABAC = 0x141; // 码流压缩性能选项配置操作 

        public const int MINOR_LOCAL_SPARE_OPT = 0x142;   /*本地N+1 热备相关操作*/
        public const int MINOR_REMOTE_SPARE_OPT = 0x143;   /*远程N+1 热备相关操作*/
        public const int MINOR_LOCAL_IPCCFGFILE_OUTPUT = 0x144;  	/* 本地导出ipc配置文件*/
        public const int MINOR_LOCAL_IPCCFGFILE_INPUT = 0x145;   /* 本地导入ipc配置文件 */
        public const int MINOR_LOCAL_IPC_UPGRADE = 0x146;   /* 本地升级IPC */
        public const int MINOR_REMOTE_IPCCFGFILE_OUTPUT = 0x147;   /* 远程导出ipc配置文件*/
        public const int MINOR_REMOTE_IPCCFGFILE_INPUT = 0x148;   /* 远程导入ipc配置文件*/
        public const int MINOR_REMOTE_IPC_UPGRADE = 0x149;   /* 远程升级IPC */

        public const int MINOR_SET_MULTI_MASTER = 0x201; // 设置大屏主屏 
        public const int MINOR_SET_MULTI_SLAVE = 0x202; // 设置大屏子屏 
        public const int MINOR_CANCEL_MULTI_MASTER = 0x203; // 取消大屏主屏 
        public const int MINOR_CANCEL_MULTI_SLAVE = 0x204; // 取消大屏子屏 

        public const int MINOR_DISPLAY_LOGO = 0x205;    /*显示LOGO*/
        public const int MINOR_HIDE_LOGO = 0x206;    /*隐藏LOGO*/
        public const int MINOR_SET_DEC_DELAY_LEVEL = 0x207;    /*解码通道延时级别设置*/
        public const int MINOR_SET_BIGSCREEN_DIPLAY_AREA = 0x208;    /*设置大屏显示区域*/
        public const int MINOR_CUT_VIDEO_SOURCE = 0x209;    /*大屏视频源切割设置*/
        public const int MINOR_SET_BASEMAP_AREA = 0x210;    /*大屏底图区域设置*/
        public const int MINOR_DOWNLOAD_BASEMAP = 0x211;    /*下载大屏底图*/
        public const int MINOR_CUT_BASEMAP = 0x212;    /*底图切割配置*/
        public const int MINOR_CONTROL_ELEC_ENLARGE = 0x213;    /*电子放大操作(放大或还原)*/
        public const int MINOR_SET_OUTPUT_RESOLUTION = 0x214;    /*显示输出分辨率设置*/
        public const int MINOR_SET_TRANCSPARENCY = 0X215;    /*图层透明度设置*/
        public const int MINOR_SET_OSD = 0x216;    /*显示OSD设置*/
        public const int MINOR_RESTORE_DEC_STATUS = 0x217;    /*恢复初始状态(场景切换时，解码恢复初始状态)*/

        public const int MINOR_SCREEN_SET_INPUT = 0x251; // 修改输入源 
        public const int MINOR_SCREEN_SET_OUTPUT = 0x252; // 修改输出通道 
        public const int MINOR_SCREEN_SET_OSD = 0x253; // 修改虚拟LED 
        public const int MINOR_SCREEN_SET_LOGO = 0x254; // 修改LOGO 
        public const int MINOR_SCREEN_SET_LAYOUT = 0x255; // 设置场景 
        public const int MINOR_SCREEN_PICTUREPREVIEW = 0x256; // 回显操作 

        public const int MINOR_SCREEN_GET_OSD = 0x257; // 获取虚拟LED 
        public const int MINOR_SCREEN_GET_LAYOUT = 0x258; // 获取场景 
        public const int MINOR_SCREEN_LAYOUT_CTRL = 0x259; // 场景控制 
        public const int MINOR_GET_ALL_VALID_WND = 0x260; // 获取所有有效窗口 
        public const int MINOR_GET_SIGNAL_WND = 0x261; // 获取单个窗口信息 
        public const int MINOR_WINDOW_CTRL = 0x262; // 窗口控制 
        public const int MINOR_GET_LAYOUT_LIST = 0x263; // 获取场景列表 
        public const int MINOR_LAYOUT_CTRL = 0x264; // 场景控制 
        public const int MINOR_SET_LAYOUT = 0x265; // 设置单个场景 
        public const int MINOR_GET_SIGNAL_LIST = 0x266; // 获取输入信号源列表 
        public const int MINOR_GET_PLAN_LIST = 0x267; // 获取预案列表 
        public const int MINOR_SET_PLAN = 0x268; // 修改预案 
        public const int MINOR_CTRL_PLAN = 0x269; // 控制预案 
        public const int MINOR_CTRL_SCREEN = 0x270; // 屏幕控制 
        public const int MINOR_ADD_NETSIG = 0x271; // 添加信号源 
        public const int MINOR_SET_NETSIG = 0x272; // 修改信号源 
        public const int MINOR_SET_DECBDCFG = 0x273; // 设置解码板参数 
        public const int MINOR_GET_DECBDCFG = 0x274; // 获取解码板参数 
        public const int MINOR_GET_DEVICE_STATUS = 0x275; // 获取设备信息 
        public const int MINOR_UPLOAD_PICTURE = 0x276; // 底图上传 
        public const int MINOR_SET_USERPWD = 0x277; // 设置用户密码 
        public const int MINOR_ADD_LAYOUT = 0x278; // 添加场景 
        public const int MINOR_DEL_LAYOUT = 0x279; // 删除场景 
        public const int MINOR_DEL_NETSIG = 0x280; // 删除信号源 
        public const int MINOR_ADD_PLAN = 0x281; // 添加预案 
        public const int MINOR_DEL_PLAN = 0x282; // 删除预案 
        public const int MINOR_GET_EXTERNAL_MATRIX_CFG = 0x283; // 获取外接矩阵配置 
        public const int MINOR_SET_EXTERNAL_MATRIX_CFG = 0x284; // 设置外接矩阵配置 
        public const int MINOR_GET_USER_CFG = 0x285; // 获取用户配置 
        public const int MINOR_SET_USER_CFG = 0x286; // 设置用户配置 
        public const int MINOR_GET_DISPLAY_PANEL_LINK_CFG = 0x287; // 获取显示墙连接配置 
        public const int MINOR_SET_DISPLAY_PANEL_LINK_CFG = 0x288; // 设置显示墙连接配置 

        public const int MINOR_GET_WALLSCENE_PARAM = 0x289; // 获取电视墙场景 
        public const int MINOR_SET_WALLSCENE_PARAM = 0x28a; // 设置电视墙场景 
        public const int MINOR_GET_CURRENT_WALLSCENE = 0x28b; // 获取当前使用场景 
        public const int MINOR_SWITCH_WALLSCENE = 0x28c; // 场景切换 
        public const int MINOR_SIP_LOGIN = 0x28d; //SIP注册成功
        public const int MINOR_VOIP_START = 0x28e; //VOIP对讲开始
        public const int MINOR_VOIP_STOP = 0x28f; //VOIP对讲停止
        public const int MINOR_WIN_TOP = 0x290; //电视墙窗口置顶
        public const int MINOR_WIN_BOTTOM = 0x291; //电视墙窗口置底
        
        // Netra 2.2.2
        public const int MINOR_LOCAL_LOAD_HDISK = 0x300; // 本地加载硬盘 
        public const int MINOR_LOCAL_DELETE_HDISK = 0x301; // 本地删除异常不存在的硬盘
 
        //Netra3.1.0
        public const int MINOR_LOCAL_CFG_DEVICE_TYPE = 0x310; //本地配置设备类型
        public const int MINOR_REMOTE_CFG_DEVICE_TYPE = 0x311; //远程配置设备类型
        public const int MINOR_LOCAL_CFG_WORK_HOT_SERVER = 0x312; //本地配置工作机热备服务器
        public const int MINOR_REMOTE_CFG_WORK_HOT_SERVER = 0x313; //远程配置工作机热备服务器
        public const int MINOR_LOCAL_DELETE_WORK = 0x314; //本地删除工作机
        public const int MINOR_REMOTE_DELETE_WORK = 0x315; //远程删除工作机
        public const int MINOR_LOCAL_ADD_WORK = 0x316; //本地添加工作机
        public const int MINOR_REMOTE_ADD_WORK = 0x317; //远程添加工作机
        public const int MINOR_LOCAL_IPCHEATMAP_OUTPUT = 0x318; /* 本地导出热度图文件      */
        public const int MINOR_LOCAL_IPCHEATFLOW_OUTPUT = 0x319; /* 本地导出热度流量文件      */
        public const int MINOR_REMOTE_SMS_SEND = 0x350; /*远程发送短信*/
        public const int MINOR_LOCAL_SMS_SEND = 0x351; /*本地发送短信*/
        public const int MINOR_ALARM_SMS_SEND = 0x352; /*发送短信报警*/
        public const int MINOR_SMS_RECV = 0x353; /*接收短信*/
        //（备注：0x350、0x351是指人工在GUI或IE控件上编辑并发送短信）
        public const int MINOR_LOCAL_SMS_SEARCH = 0x354; /*本地搜索短信*/
        public const int MINOR_REMOTE_SMS_SEARCH = 0x355; /*远程搜索短信*/
        public const int MINOR_LOCAL_SMS_READ = 0x356; /*本地查看短信*/
        public const int MINOR_REMOTE_SMS_READ = 0x357; /*远程查看短信*/
        public const int MINOR_REMOTE_DIAL_CONNECT = 0x358; /*远程开启手动拨号*/
        public const int MINOR_REMOTE_DIAL_DISCONN = 0x359; /*远程停止手动拨号*/
        public const int MINOR_LOCAL_WHITELIST_SET = 0x35A; /*本地配置白名单*/
        public const int MINOR_REMOTE_WHITELIST_SET = 0x35B; /*远程配置白名单*/
        public const int MINOR_LOCAL_DIAL_PARA_SET = 0x35C; /*本地配置拨号参数*/
        public const int MINOR_REMOTE_DIAL_PARA_SET = 0x35D; /*远程配置拨号参数*/
        public const int MINOR_LOCAL_DIAL_SCHEDULE_SET = 0x35E; /*本地配置拨号计划*/
        public const int MINOR_REMOTE_DIAL_SCHEDULE_SET = 0x35F; /*远程配置拨号计划*/
        public const int MINOR_PLAT_OPER = 0x36; /* 平台操作*/
        
        public const int MINOR_REMOTE_OPEN_DOOR = 0x400; // 远程开门 
        public const int MINOR_REMOTE_CLOSE_DOOR = 0x401; // 远程关门 
        public const int MINOR_REMOTE_ALWAYS_OPEN = 0x402; // 远程常开 
        public const int MINOR_REMOTE_ALWAYS_CLOSE = 0x403; // 远程常关 
        public const int MINOR_REMOTE_CHECK_TIME = 0x404; // 远程手动校时 
        public const int MINOR_NTP_CHECK_TIME = 0x405; // NTP自动校时 
        public const int MINOR_REMOTE_CLEAR_CARD = 0x406; // 远程清空卡号 
        public const int MINOR_REMOTE_RESTORE_CFG = 0x407; // 远程恢复默认参数 
        public const int MINOR_ALARMIN_ARM = 0x408; // 防区布防 
        public const int MINOR_ALARMIN_DISARM = 0x409; // 防区撤防 
        public const int MINOR_LOCAL_RESTORE_CFG = 0x40a; // 本地恢复默认参数 
        public const int MINOR_REMOTE_CAPTURE_PIC = 0x40b; //远程抓拍
        public const int MINOR_MOD_NET_REPORT_CFG = 0x40c; //修改网络中心参数配置
        public const int MINOR_MOD_GPRS_REPORT_PARAM = 0x40d; //修改GPRS中心参数配置
        public const int MINOR_MOD_REPORT_GROUP_PARAM = 0x40e; //修改中心组参数配置
        public const int MINOR_UNLOCK_PASSWORD_OPEN_DOOR = 0x40f; //解除码输入

        public const int MINOR_SET_TRIGGERMODE_CFG = 0x1001; // 设置触发模式参数 
        public const int MINOR_GET_TRIGGERMODE_CFG = 0x1002; // 获取触发模式参数 
        public const int MINOR_SET_IOOUT_CFG = 0x1003; // 设置IO输出参数 
        public const int MINOR_GET_IOOUT_CFG = 0x1004; // 获取IO输出参数 
        public const int MINOR_GET_TRIGGERMODE_DEFAULT = 0x1005; // 获取触发模式推荐参数 
        public const int MINOR_GET_ITCSTATUS = 0x1006; // 获取状态检测参数 
        public const int MINOR_SET_STATUS_DETECT_CFG = 0x1007; // 设置状态检测参数 
        public const int MINOR_GET_STATUS_DETECT_CFG = 0x1008; // 获取状态检测参数 
        public const int MINOR_GET_VIDEO_TRIGGERMODE_CFG = 0x1009; // 获取视频电警模式参数 
        public const int MINOR_SET_VIDEO_TRIGGERMODE_CFG = 0x100a; // 设置视频电警模式参数 

        public const int MINOR_LOCAL_ADD_CAR_INFO = 0x2001; // 本地添加车辆信息 
        public const int MINOR_LOCAL_MOD_CAR_INFO = 0x2002; // 本地修改车辆信息 
        public const int MINOR_LOCAL_DEL_CAR_INFO = 0x2003; // 本地删除车辆信息 
        public const int MINOR_LOCAL_FIND_CAR_INFO = 0x2004; // 本地查找车辆信息 
        public const int MINOR_LOCAL_ADD_MONITOR_INFO = 0x2005; // 本地添加布控信息 
        public const int MINOR_LOCAL_MOD_MONITOR_INFO = 0x2006; // 本地修改布控信息 
        public const int MINOR_LOCAL_DEL_MONITOR_INFO = 0x2007; // 本地删除布控信息 
        public const int MINOR_LOCAL_FIND_MONITOR_INFO = 0x2008; // 本地查询布控信息 
        public const int MINOR_LOCAL_FIND_NORMAL_PASS_INFO = 0x2009; // 本地查询正常通行信息 
        public const int MINOR_LOCAL_FIND_ABNORMAL_PASS_INFO = 0x200a; // 本地查询异常通行信息 
        public const int MINOR_LOCAL_FIND_PEDESTRIAN_PASS_INFO = 0x200b; // 本地查询正常通行信息 
        public const int MINOR_LOCAL_PIC_PREVIEW = 0x200c; // 本地图片预览 
        public const int MINOR_LOCAL_SET_GATE_PARM_CFG = 0x200d; // 设置本地配置出入口参数 
        public const int MINOR_LOCAL_GET_GATE_PARM_CFG = 0x200e; // 获取本地配置出入口参数 
        public const int MINOR_LOCAL_SET_DATAUPLOAD_PARM_CFG = 0x200f; // 设置本地配置数据上传参数 
        public const int MINOR_LOCAL_GET_DATAUPLOAD_PARM_CFG = 0x2010; // 获取本地配置数据上传参数 

        public const int MINOR_LOCAL_DEVICE_CONTROL = 0x2011; // 本地设备控制(本地开关闸) 
        public const int MINOR_LOCAL_ADD_EXTERNAL_DEVICE_INFO = 0x2012; // 本地添加外接设备信息 
        public const int MINOR_LOCAL_MOD_EXTERNAL_DEVICE_INFO = 0x2013; // 本地修改外接设备信息 
        public const int MINOR_LOCAL_DEL_EXTERNAL_DEVICE_INFO = 0x2014; // 本地删除外接设备信息 
        public const int MINOR_LOCAL_FIND_EXTERNAL_DEVICE_INFO = 0x2015; // 本地查询外接设备信息 
        public const int MINOR_LOCAL_ADD_CHARGE_RULE = 0x2016; // 本地添加收费规则 
        public const int MINOR_LOCAL_MOD_CHARGE_RULE = 0x2017; // 本地修改收费规则 
        public const int MINOR_LOCAL_DEL_CHARGE_RULE = 0x2018; // 本地删除收费规则 
        public const int MINOR_LOCAL_FIND_CHARGE_RULE = 0x2019; // 本地查询收费规则 
        public const int MINOR_LOCAL_COUNT_NORMAL_CURRENTINFO = 0x2020; // 本地统计正常通行信息 
        public const int MINOR_LOCAL_EXPORT_NORMAL_CURRENTINFO_REPORT = 0x2021; // 本地导出正常通行信息统计报表 
        public const int MINOR_LOCAL_COUNT_ABNORMAL_CURRENTINFO = 0x2022; // 本地统计异常通行信息 
        public const int MINOR_LOCAL_EXPORT_ABNORMAL_CURRENTINFO_REPORT = 0x2023; // 本地导出异常通行信息统计报表 
        public const int MINOR_LOCAL_COUNT_PEDESTRIAN_CURRENTINFO = 0x2024; // 本地统计行人通行信息 
        public const int MINOR_LOCAL_EXPORT_PEDESTRIAN_CURRENTINFO_REPORT = 0x2025; // 本地导出行人通行信息统计报表 
        public const int MINOR_LOCAL_FIND_CAR_CHARGEINFO = 0x2026; // 本地查询过车收费信息 
        public const int MINOR_LOCAL_COUNT_CAR_CHARGEINFO = 0x2027; // 本地统计过车收费信息 
        public const int MINOR_LOCAL_EXPORT_CAR_CHARGEINFO_REPORT = 0x2028; // 本地导出过车收费信息统计报表 
        public const int MINOR_LOCAL_FIND_SHIFTINFO = 0x2029; // 本地查询交接班信息 
        public const int MINOR_LOCAL_FIND_CARDINFO = 0x2030; // 本地查询卡片信息 
        public const int MINOR_LOCAL_ADD_RELIEF_RULE = 0x2031; // 本地添加减免规则 
        public const int MINOR_LOCAL_MOD_RELIEF_RULE = 0x2032; // 本地修改减免规则 
        public const int MINOR_LOCAL_DEL_RELIEF_RULE = 0x2033; // 本地删除减免规则 
        public const int MINOR_LOCAL_FIND_RELIEF_RULE = 0x2034; // 本地查询减免规则 
        public const int MINOR_LOCAL_GET_ENDETCFG = 0x2035; // 本地获取出入口控制机离线检测配置 
        public const int MINOR_LOCAL_SET_ENDETCFG = 0x2036; // 本地设置出入口控制机离线检测配置 
        public const int MINOR_LOCAL_SET_ENDEV_ISSUEDDATA = 0x2037; // 本地设置出入口控制机下发卡片信息 
        public const int MINOR_LOCAL_DEL_ENDEV_ISSUEDDATA = 0x2038; // 本地清空出入口控制机下发卡片信息 
        public const int MINOR_REMOTE_DEVICE_CONTROL = 0x2101; // 远程设备控制 
        public const int MINOR_REMOTE_SET_GATE_PARM_CFG = 0x2102; // 设置远程配置出入口参数 
        public const int MINOR_REMOTE_GET_GATE_PARM_CFG = 0x2103; // 获取远程配置出入口参数 
        public const int MINOR_REMOTE_SET_DATAUPLOAD_PARM_CFG = 0x2104; // 设置远程配置数据上传参数 
        public const int MINOR_REMOTE_GET_DATAUPLOAD_PARM_CFG = 0x2105; // 获取远程配置数据上传参数 
        public const int MINOR_REMOTE_GET_BASE_INFO = 0x2106; // 远程获取终端基本信息 
        public const int MINOR_REMOTE_GET_OVERLAP_CFG = 0x2107; // 远程获取字符叠加参数配置 
        public const int MINOR_REMOTE_SET_OVERLAP_CFG = 0x2108; // 远程设置字符叠加参数配置 
        public const int MINOR_REMOTE_GET_ROAD_INFO = 0x2109; // 远程获取路口信息 
        public const int MINOR_REMOTE_START_TRANSCHAN = 0x210a; // 远程建立同步数据服务器 
        public const int MINOR_REMOTE_GET_ECTWORKSTATE = 0x210b; // 远程获取出入口终端工作状态 
        public const int MINOR_REMOTE_GET_ECTCHANINFO = 0x210c; // 远程获取出入口终端通道状态 
        public const int MINOR_REMOTE_ADD_EXTERNAL_DEVICE_INFO = 0x210d; // 远程添加外接设备信息 
        public const int MINOR_REMOTE_MOD_EXTERNAL_DEVICE_INFO = 0x210e; // 远程修改外接设备信息 
        public const int MINOR_REMOTE_GET_ENDETCFG = 0x210f; // 远程获取出入口控制机离线检测配置 
        public const int MINOR_REMOTE_SET_ENDETCFG = 0x2110; // 远程设置出入口控制机离线检测配置 
        public const int MINOR_REMOTE_ENDEV_ISSUEDDATA = 0x2111; // 远程设置出入口控制机下发卡片信息 
        public const int MINOR_REMOTE_DEL_ENDEV_ISSUEDDATA = 0x2112; // 远程清空出入口控制机下发卡片信息 

        public const int MINOR_REMOTE_ON_CTRL_LAMP = 0x2115; // 开启远程控制车位指示灯 
        public const int MINOR_REMOTE_OFF_CTRL_LAMP = 0x2116; // 关闭远程控制车位指示灯 
        public const int MINOR_SET_VOICE_LEVEL_PARAM = 0x2117; // 设置音量大小 
        public const int MINOR_SET_VOICE_INTERCOM_PARAM = 0x2118; // 设置音量录音 
        public const int MINOR_SET_INTELLIGENT_PARAM = 0x2119; // 智能配置 
        public const int MINOR_LOCAL_SET_RAID_SPEED = 0x211a; // 本地设置raid速度 
        public const int MINOR_REMOTE_SET_RAID_SPEED = 0x211b; // 远程设置raid速度 
        public const int MINOR_REMOTE_CREATE_STORAGE_POOL = 0x211c; // 远程添加存储池 
        public const int MINOR_REMOTE_DEL_STORAGE_POOL = 0x211d; // 远程删除存储池 

        public const int MINOR_REMOTE_DEL_PIC = 0x2120; // 远程删除图片数据 
        public const int MINOR_REMOTE_DEL_RECORD = 0x2121; // 远程删除录像数据 
        public const int MINOR_REMOTE_CLOUD_ENABLE = 0x2123; // 远程设置云存储启用 
        public const int MINOR_REMOTE_CLOUD_DISABLE = 0x2124; // 远程设置云存储禁用 
        public const int MINOR_REMOTE_CLOUD_MODIFY_PARAM = 0x2125; // 远程修改云存储池参数 
        public const int MINOR_REMOTE_CLOUD_MODIFY_VOLUME = 0x2126; // 远程修改云存储池容量 
        public const int MINOR_REMOTE_GET_GB28181_SERVICE_PARAM = 0x2127; //远程获取GB28181服务参数
        public const int MINOR_REMOTE_SET_GB28181_SERVICE_PARAM = 0x2128; //远程设置GB28181服务参数
        public const int MINOR_LOCAL_GET_GB28181_SERVICE_PARAM = 0x2129; //本地获取GB28181服务参数
        public const int MINOR_LOCAL_SET_GB28181_SERVICE_PARAM = 0x212a; //本地配置B28181服务参数
        public const int MINOR_REMOTE_SET_SIP_SERVER = 0x212b; //远程配置SIP SERVER
        public const int MINOR_LOCAL_SET_SIP_SERVER = 0x212c; //本地配置SIP SERVER
        public const int MINOR_LOCAL_BLACKWHITEFILE_OUTPUT = 0x212d; //本地黑白名单导出
        public const int MINOR_LOCAL_BLACKWHITEFILE_INPUT = 0x212e; //本地黑白名单导入
        public const int MINOR_REMOTE_BALCKWHITECFGFILE_OUTPUT = 0x212f; //远程黑白名单导出
        public const int MINOR_REMOTE_BALCKWHITECFGFILE_INPUT = 0x2130; //远程黑白名单导入

        public const int MINOR_REMOTE_CREATE_MOD_VIEWLIB_SPACE = 0x2200; // 远程创建/修改视图库空间 
        public const int MINOR_REMOTE_DELETE_VIEWLIB_FILE = 0x2201; // 远程删除视图库文件 
        public const int MINOR_REMOTE_DOWNLOAD_VIEWLIB_FILE = 0x2202; // 远程下载视图库文件 
        public const int MINOR_REMOTE_UPLOAD_VIEWLIB_FILE = 0x2203; // 远程上传视图库文件 
        public const int MINOR_LOCAL_CREATE_MOD_VIEWLIB_SPACE = 0x2204; // 本地创建/修改视图库空间 

        public const int  MINOR_LOCAL_SET_DEVICE_ACTIVE = 0x3000; //本地激活设备
        public const int  MINOR_REMOTE_SET_DEVICE_ACTIVE = 0x3001; //远程激活设备
        public const int  MINOR_LOCAL_PARA_FACTORY_DEFAULT = 0x3002; //本地回复出厂设置
        public const int  MINOR_REMOTE_PARA_FACTORY_DEFAULT = 0x3003; //远程恢复出厂设置

        /*日志附加信息*/
        //主类型
        public const int MAJOR_INFORMATION = 4;/*附加信息*/
        //次类型
        public const int MINOR_HDD_INFO = 0xa1; // 硬盘信息 
        public const int MINOR_SMART_INFO = 0xa2; // S.M.A.R.T信息 
        public const int MINOR_REC_START = 0xa3; // 开始录像 
        public const int MINOR_REC_STOP = 0xa4; // 停止录像 
        public const int MINOR_REC_OVERDUE = 0xa5; // 过期录像删除 
        public const int MINOR_LINK_START = 0xa6; // 连接前端设备 
        public const int MINOR_LINK_STOP = 0xa7; // 断开前端设备 
        public const int MINOR_NET_DISK_INFO = 0xa8; // 网络硬盘信息 
        public const int MINOR_RAID_INFO = 0xa9; // raid相关信息 
        public const int MINOR_RUN_STATUS_INFO = 0xaa; // 系统运行状态信息 
        public const int MINOR_SPARE_START_BACKUP = 0xab; // 热备系统开始备份指定工作机 
        public const int MINOR_SPARE_STOP_BACKUP = 0xac; // 热备系统停止备份指定工作机 
        public const int MINOR_SPARE_CLIENT_INFO = 0xad; // 热备客户机信息 
        public const int MINOR_ANR_RECORD_START = 0xae; // ANR录像开始 
        public const int MINOR_ANR_RECORD_END = 0xaf; // ANR录像结束 
        public const int MINOR_ANR_ADD_TIME_QUANTUM = 0xb0; // ANR添加时间段 
        public const int MINOR_ANR_DEL_TIME_QUANTUM = 0xb1; // ANR删除时间段 
        public const int MINOR_PIC_REC_START = 0xb3; // 开始抓图 
        public const int MINOR_PIC_REC_STOP = 0xb4; // 停止抓图 
        public const int MINOR_PIC_REC_OVERDUE = 0xb5 ; //过期图片文件删除 
        public const int MINOR_CLIENT_LOGIN = 0xb6; // 登录服务器成功 
        public const int MINOR_CLIENT_RELOGIN = 0xb7; // 重新登录服务器 
        public const int MINOR_CLIENT_LOGOUT = 0xb8; // 退出服务器成功 
        public const int MINOR_CLIENT_SYNC_START = 0xb9; // 录像同步开始 
        public const int MINOR_CLIENT_SYNC_STOP = 0xba; // 录像同步终止 
        public const int MINOR_CLIENT_SYNC_SUCC = 0xbb; // 录像同步成功 
        public const int MINOR_CLIENT_SYNC_EXCP = 0xbc; // 录像同步异常 
        public const int MINOR_GLOBAL_RECORD_ERR_INFO = 0xbd; // 全局错误记录信息 
        public const int MINOR_BUFFER_STATE = 0xbe; // 缓冲区状态日志记录 
        public const int MINOR_DISK_ERRORINFO_V2 = 0xbf; // 硬盘错误详细信息V2 
        public const int MINOR_UNLOCK_RECORD = 0xc3; // 开锁记录 
        public const int MINOR_VIS_ALARM = 0xc4; // 防区报警 
        public const int MINOR_TALK_RECORD = 0xc5; // 通话记录 

        /*日志附加信息*/
        //主类型
        public const int MAJOR_EVENT = 0x5;/*事件*/
        //次类型
        public const int MINOR_LEGAL_CARD_PASS = 0x01; // 合法卡认证通过 
        public const int MINOR_CARD_AND_PSW_PASS = 0x02; // 刷卡加密码认证通过 
        public const int MINOR_CARD_AND_PSW_FAIL = 0x03; // 刷卡加密码认证失败 
        public const int MINOR_CARD_AND_PSW_TIMEOUT = 0x04; // 数卡加密码认证超时 
        public const int MINOR_CARD_AND_PSW_OVER_TIME = 0x05; // 刷卡加密码超次 
        public const int MINOR_CARD_NO_RIGHT = 0x06; // 未分配权限 
        public const int MINOR_CARD_INVALID_PERIOD = 0x07; // 无效时段 
        public const int MINOR_CARD_OUT_OF_DATE = 0x08; // 卡号过期 
        public const int MINOR_INVALID_CARD = 0x09; // 无此卡号 
        public const int MINOR_ANTI_SNEAK_FAIL = 0x0a; // 反潜回认证失败 
        public const int MINOR_INTERLOCK_DOOR_NOT_CLOSE = 0x0b; // 互锁门未关闭 
        public const int MINOR_NOT_BELONG_MULTI_GROUP = 0x0c; // 卡不属于多重认证群组 
        public const int MINOR_INVALID_MULTI_VERIFY_PERIOD = 0x0d; // 卡不在多重认证时间段内 
        public const int MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL = 0x0e; // 多重认证模式超级权限认证失败 
        public const int MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL = 0x0f; // 多重认证模式远程认证失败 
        public const int MINOR_MULTI_VERIFY_SUCCESS = 0x10; // 多重认证成功 
        public const int MINOR_LEADER_CARD_OPEN_BEGIN = 0x11; // 首卡开门开始 
        public const int MINOR_LEADER_CARD_OPEN_END = 0x12; // 首卡开门结束 
        public const int MINOR_ALWAYS_OPEN_BEGIN = 0x13; // 常开状态开始 
        public const int MINOR_ALWAYS_OPEN_END = 0x14; // 常开状态结束 
        public const int MINOR_LOCK_OPEN = 0x15; // 门锁打开 
        public const int MINOR_LOCK_CLOSE = 0x16; // 门锁关闭 
        public const int MINOR_DOOR_BUTTON_PRESS = 0x17; // 开门按钮打开 
        public const int MINOR_DOOR_BUTTON_RELEASE = 0x18; // 开门按钮放开 
        public const int MINOR_DOOR_OPEN_NORMAL = 0x19; // 正常开门（门磁） 
        public const int MINOR_DOOR_CLOSE_NORMAL = 0x1a; // 正常关门（门磁） 
        public const int MINOR_DOOR_OPEN_ABNORMAL = 0x1b; // 门异常打开（门磁） 
        public const int MINOR_DOOR_OPEN_TIMEOUT = 0x1c; // 门打开超时（门磁）  
        public const int MINOR_ALARMOUT_ON = 0x1d; // 报警输出打开 
        public const int MINOR_ALARMOUT_OFF = 0x1e; // 报警输出关闭 
        public const int MINOR_ALWAYS_CLOSE_BEGIN = 0x1f; // 常关状态开始 
        public const int MINOR_ALWAYS_CLOSE_END = 0x20; // 常关状态结束 
        public const int MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN = 0x21; // 多重多重认证需要远程开门 
        public const int MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS = 0x22; // 多重认证超级密码认证成功事件 
        public const int MINOR_MULTI_VERIFY_REPEAT_VERIFY = 0x23; // 多重认证重复认证事件 
        public const int MINOR_MULTI_VERIFY_TIMEOUT = 0x24; // 多重认证重复认证事件 
        public const int MINOR_DOORBELL_RINGING = 0x25; // 门铃响
        public const int MINOR_FINGERPRINT_COMPARE_PASS = 0x26; // 指纹比对通过
        public const int MINOR_FINGERPRINT_COMPARE_FAIL = 0x27; // 指纹比对失败
        public const int MINOR_CARD_FINGERPRINT_VERIFY_PASS = 0x28; // 刷卡加指纹认证通过
        public const int MINOR_CARD_FINGERPRINT_VERIFY_FAIL = 0x29 ; // 刷卡加指纹认证失败
        public const int MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT = 0x2a; // 刷卡加指纹认证超时
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS = 0x2b; // 刷卡加指纹加密码认证通过
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL = 0x2c; // 刷卡加指纹加密码认证失败
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT = 0x2d ; // 刷卡加指纹加密码认证超时
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_PASS = 0x2e; // 指纹加密码认证通过
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL = 0x2f; // 指纹加密码认证失败
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT = 0x30; // 指纹加密码认证超时
        public const int MINOR_FINGERPRINT_INEXISTENCE = 0x31; // 指纹不存在

        //当日志的主类型为MAJOR_OPERATION=03，次类型为MINOR_LOCAL_CFG_PARM=0x52或者MINOR_REMOTE_GET_PARM=0x76或者MINOR_REMOTE_CFG_PARM=0x77时，dwParaType:参数类型有效，其含义如下：
        public const int PARA_VIDEOOUT = 1;
        public const int PARA_IMAGE = 2;
        public const int PARA_ENCODE = 4;
        public const int PARA_NETWORK = 8;
        public const int PARA_ALARM = 16;
        public const int PARA_EXCEPTION = 32;
        public const int PARA_DECODER = 64;/*解码器*/
        public const int PARA_RS232 = 128;
        public const int PARA_PREVIEW = 256;
        public const int PARA_SECURITY = 512;
        public const int PARA_DATETIME = 1024;
        public const int PARA_FRAMETYPE = 2048;/*帧格式*/

        //vca
//        public const int PARA_VCA_RULE = 4096;//行为规则

        public const int PARA_DETECTION = 0x1000;   //侦测配置
        public const int PARA_VCA_RULE = 0x1001;  //行为规则 
        public const int PARA_VCA_CTRL = 0x1002;  //配置智能控制信息
        public const int PARA_VCA_PLATE = 0x1003; // 车牌识别

        public const int PARA_CODESPLITTER = 0x2000; /*码分器参数*/
        //2010-01-22 增加视频综合平台日志信息次类型
        public const int PARA_RS485 = 0x2001; /* RS485配置信息*/
        public const int PARA_DEVICE = 0x2002; /* 设备配置信息*/
        public const int PARA_HARDDISK = 0x2003; /* 硬盘配置信息 */
        public const int PARA_AUTOBOOT = 0x2004; /* 自动重启配置信息*/
        public const int PARA_HOLIDAY = 0x2005; /* 节假日配置信息*/			
        public const int PARA_IPC = 0x2006; /* IP通道配置 */	

        //日志信息(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_LOG_V30
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;//主类型 1-报警; 2-异常; 3-操作; 0xff-全部
            public uint dwMinorType;//次类型 0-全部;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;//操作面板的用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;//网络操作的用户名
            public NET_DVR_IPADDR struRemoteHostAddr;//远程主机地址
            public uint dwParaType;//参数类型
            public uint dwChannel;//通道号
            public uint dwDiskNumber;//硬盘号
            public uint dwAlarmInPort;//报警输入端口
            public uint dwAlarmOutPort;//报警输出端口
            public uint dwInfoLen;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = LOG_INFO_LEN)]
            public string sInfo;
        }

        //日志信息
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_LOG
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;//主类型 1-报警; 2-异常; 3-操作; 0xff-全部
            public uint dwMinorType;//次类型 0-全部;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;//操作面板的用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;//网络操作的用户名
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteHostAddr;//远程主机地址
            public uint dwParaType;//参数类型
            public uint dwChannel;//通道号
            public uint dwDiskNumber;//硬盘号
            public uint dwAlarmInPort;//报警输入端口
            public uint dwAlarmOutPort;//报警输出端口
        }

        /************************DVR日志 end***************************/

        //报警输出状态(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTSTATUS_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] Output;

            public void Init()
            {
                Output = new byte[MAX_ALARMOUT_V30];
            }
        }

        //报警输出状态
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTSTATUS
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] Output;
        }

        //交易信息
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_TRADEINFO
        {
            public ushort m_Year;
            public ushort m_Month;
            public ushort m_Day;
            public ushort m_Hour;
            public ushort m_Minute;
            public ushort m_Second;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] DeviceName;//设备名称
            public uint dwChannelNumer;//通道号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] CardNumber;//卡号
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 12)]
            public string cTradeType;//交易类型
            public uint dwCash;//交易金额
        }

        //ATM专用
        /****************************ATM(begin)***************************/
        public const int NCR = 0;
        public const int DIEBOLD = 1;
        public const int WINCOR_NIXDORF = 2;
        public const int SIEMENS = 3;
        public const int OLIVETTI = 4;
        public const int FUJITSU = 5;
        public const int HITACHI = 6;
        public const int SMI = 7;
        public const int IBM = 8;
        public const int BULL = 9;
        public const int YiHua = 10;
        public const int LiDe = 11;
        public const int GDYT = 12;
        public const int Mini_Banl = 13;
        public const int GuangLi = 14;
        public const int DongXin = 15;
        public const int ChenTong = 16;
        public const int NanTian = 17;
        public const int XiaoXing = 18;
        public const int GZYY = 19;
        public const int QHTLT = 20;
        public const int DRS918 = 21;
        public const int KALATEL = 22;
        public const int NCR_2 = 23;
        public const int NXS = 24;

        /*帧格式*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FRAMETYPECODE
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] code;/* 代码 */
        }

        //ATM参数(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FRAMEFORMAT_V30
        {
            public uint dwSize;
            public NET_DVR_IPADDR struATMIP;/* ATM IP地址 */
            public uint dwATMType;/* ATM类型 */
            public uint dwInputMode;/* 输入方式	0-网络侦听 1-网络接收 2-串口直接输入 3-串口ATM命令输入*/
            public uint dwFrameSignBeginPos;/* 报文标志位的起始位置*/
            public uint dwFrameSignLength;/* 报文标志位的长度 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byFrameSignContent;/* 报文标志位的内容 */
            public uint dwCardLengthInfoBeginPos;/* 卡号长度信息的起始位置 */
            public uint dwCardLengthInfoLength;/* 卡号长度信息的长度 */
            public uint dwCardNumberInfoBeginPos;/* 卡号信息的起始位置 */
            public uint dwCardNumberInfoLength;/* 卡号信息的长度 */
            public uint dwBusinessTypeBeginPos;/* 交易类型的起始位置 */
            public uint dwBusinessTypeLength;/* 交易类型的长度 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_FRAMETYPECODE[] frameTypeCode;/* 类型 */
            public ushort wATMPort;/* 卡号捕捉端口号(网络协议方式) */
            public ushort wProtocolType;/* 网络协议类型 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //ATM参数
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FRAMEFORMAT
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sATMIP;/* ATM IP地址 */
            public uint dwATMType;/* ATM类型 */
            public uint dwInputMode;/* 输入方式	0-网络侦听 1-网络接收 2-串口直接输入 3-串口ATM命令输入*/
            public uint dwFrameSignBeginPos;/* 报文标志位的起始位置*/
            public uint dwFrameSignLength;/* 报文标志位的长度 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byFrameSignContent;/* 报文标志位的内容 */
            public uint dwCardLengthInfoBeginPos;/* 卡号长度信息的起始位置 */
            public uint dwCardLengthInfoLength;/* 卡号长度信息的长度 */
            public uint dwCardNumberInfoBeginPos;/* 卡号信息的起始位置 */
            public uint dwCardNumberInfoLength;/* 卡号信息的长度 */
            public uint dwBusinessTypeBeginPos;/* 交易类型的起始位置 */
            public uint dwBusinessTypeLength;/* 交易类型的长度 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_FRAMETYPECODE[] frameTypeCode;/* 类型 */
        }

        //SDK_V31 ATM
        /*过滤设置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_FILTER
        {
            public byte byEnable;/*0,不启用;1,启用*/
            public byte byMode;/*0,ASCII;1,HEX*/
            public byte byFrameBeginPos;// 报文标志位的起始位置     
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byFilterText;/*过滤字符串*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*起始标识设置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IDENTIFICAT
        {
            public byte byStartMode;/*0,ASCII;1,HEX*/
            public byte byEndMode;/*0,ASCII;1,HEX*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_FRAMETYPECODE struStartCode;/*起始字符*/
            public NET_DVR_FRAMETYPECODE struEndCode;/*结束字符*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        /*报文信息位置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PACKAGE_LOCATION
        {
            public byte byOffsetMode;/*0,token;1,fix*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwOffsetPos;/*mode为1的时候使用*/
            public NET_DVR_FRAMETYPECODE struTokenCode;/*标志位*/
            public byte byMultiplierValue;/*标志位多少次出现*/
            public byte byEternOffset;/*附加的偏移量*/
            public byte byCodeMode;/*0,ASCII;1,HEX*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*报文信息长度*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PACKAGE_LENGTH
        {
            public byte byLengthMode;/*长度类型，0,variable;1,fix;2,get from package(设置卡号长度使用)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwFixLength;/*mode为1的时候使用*/
            public uint dwMaxLength;
            public uint dwMinLength;
            public byte byEndMode;/*终结符0,ASCII;1,HEX*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_FRAMETYPECODE struEndCode;/*终结符*/
            public uint dwLengthPos;/*lengthMode为2的时候使用，卡号长度在报文中的位置*/
            public uint dwLengthLen;/*lengthMode为2的时候使用，卡号长度的长度*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        /*OSD 叠加的位置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_OSD_POSITION
        {
            public byte byPositionMode;/*叠加风格，共2种；0，不显示；1，Custom*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwPos_x;/*x坐标，positionmode为Custom时使用*/
            public uint dwPos_y;/*y坐标，positionmode为Custom时使用*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*日期显示格式*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_DATE_FORMAT
        {
            public byte byItem1;/*Month,0.mm;1.mmm;2.mmmm*/
            public byte byItem2;/*Day,0.dd;*/
            public byte byItem3;/*Year,0.yy;1.yyyy*/
            public byte byDateForm;/*0~5，3个item的排列组合*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chSeprator;/*分隔符*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chDisplaySeprator;/*显示分隔符*/
            public byte byDisplayForm;/*0~5，3个item的排列组合*///lili mode by lili
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 27, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        /*时间显示格式*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVRT_TIME_FORMAT
        {
            public byte byTimeForm;/*1. HH MM SS;0. HH MM*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byHourMode;/*0,12;1,24*/ //lili mode
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chSeprator;/*报文分隔符，暂时没用*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chDisplaySeprator;/*显示分隔符*/
            public byte byDisplayForm;/*0~5，3个item的排列组合*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
            public byte byDisplayHourMode;/*0,12;1,24*/ //lili mode
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 19, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes4;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_OVERLAY_CHANNEL
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byChannel;/*叠加的通道*/
            public uint dwDelayTime;/*叠加延时时间*/
            public byte byEnableDelayTime;/*是否启用叠加延时，在无退卡命令时启用*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_ACTION
        {
            public tagNET_DVR_PACKAGE_LOCATION struPackageLocation;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            public NET_DVR_FRAMETYPECODE struActionCode;/*交易类型等对应的码*/
            public NET_DVR_FRAMETYPECODE struPreCode;/*叠加字符前的字符*/
            public byte byActionCodeMode;/*交易类型等对应的码0,ASCII;1,HEX*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_DATE
        {
            public tagNET_DVR_PACKAGE_LOCATION struPackageLocation;
            public tagNET_DVR_DATE_FORMAT struDateForm;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_TIME
        {
            public tagNET_DVR_PACKAGE_LOCATION location;
            public tagNET_DVRT_TIME_FORMAT struTimeForm;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_OTHERS
        {
            public tagNET_DVR_PACKAGE_LOCATION struPackageLocation;
            public tagNET_DVR_PACKAGE_LENGTH struPackageLength;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            public NET_DVR_FRAMETYPECODE struPreCode;/*叠加字符前的字符*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_FRAMETYPE_NEW
        {
            public byte byEnable;/*是否启用0,不启用;1,启用*/
            public byte byInputMode;/*输入方式:网络监听、串口监听、网络协议、串口协议、串口服务器*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byAtmName;/*ATM 名称*/
            public NET_DVR_IPADDR struAtmIp;/*ATM 机IP  */
            public ushort wAtmPort;/* 卡号捕捉端口号(网络协议方式) 或串口服务器端口号*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public uint dwAtmType;/*ATM 机类型*/
            public tagNET_DVR_IDENTIFICAT struIdentification;/*报文标志*/
            public tagNET_DVR_FILTER struFilter;/*过滤设置*/
            public tagNET_DVR_ATM_PACKAGE_OTHERS struCardNoPara;/*叠加卡号设置*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ACTION_TYPE, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_ATM_PACKAGE_ACTION[] struTradeActionPara;/*叠加交易行为设置*/
            public tagNET_DVR_ATM_PACKAGE_OTHERS struAmountPara;/*叠加交易金额设置*/
            public tagNET_DVR_ATM_PACKAGE_OTHERS struSerialNoPara;/*叠加交易序号设置*/
            public tagNET_DVR_OVERLAY_CHANNEL struOverlayChan;/*叠加通道设置*/
            public tagNET_DVR_ATM_PACKAGE_DATE byRes4;/*叠加日期设置，暂时保留*/
            public tagNET_DVR_ATM_PACKAGE_TIME byRes5;/*叠加时间设置，暂时保留*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 132, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_FRAMEFORMAT_V31
        {
            public uint dwSize;
            public tagNET_DVR_ATM_FRAMETYPE_NEW struAtmFrameTypeNew;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_ATM_FRAMETYPE_NEW[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_ATM_PROTOIDX
        {
            public uint dwAtmType;/*ATM协议类型，同时作为索引序号*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = ATM_DESC_LEN)]
            public string chDesc;/*ATM协议简单描述*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PROTOCOL
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ATM_PROTOCOL_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_ATM_PROTOIDX[] struAtmProtoidx;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ATM_PROTOCOL_SORT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwAtmNumPerSort;/*每段协议数*/
        }

        /*****************************DS-6001D/F(begin)***************************/
        //DS-6001D Decoder
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderIP;//解码设备连接的服务器IP
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderUser;//解码设备连接的服务器的用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderPasswd;//解码设备连接的服务器的密码
            public byte bySendMode;//解码设备连接服务器的连接模式
            public byte byEncoderChannel;//解码设备连接的服务器的通道号
            public ushort wEncoderPort;//解码设备连接的服务器的端口号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] reservedData;//保留
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERSTATE
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderIP;//解码设备连接的服务器IP
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderUser;//解码设备连接的服务器的用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderPasswd;//解码设备连接的服务器的密码
            public byte byEncoderChannel;//解码设备连接的服务器的通道号
            public byte bySendMode;//解码设备连接的服务器的连接模式
            public ushort wEncoderPort;//解码设备连接的服务器的端口号
            public uint dwConnectState;//解码设备连接服务器的状态
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] reservedData;//保留
        }

        /*解码设备控制码定义*/
        public const int NET_DEC_STARTDEC = 1;
        public const int NET_DEC_STOPDEC = 2;
        public const int NET_DEC_STOPCYCLE = 3;
        public const int NET_DEC_CONTINUECYCLE = 4;

        /*连接的通道配置*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DECCHANINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/* DVR IP地址 */
            public ushort wDVRPort;/* 端口号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            public byte byChannel;/* 通道号 */
            public byte byLinkMode;/* 连接模式 */
            public byte byLinkType;/* 连接类型 0－主码流 1－子码流 */
        }

        /*每个解码通道的配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECINFO
        {
            public byte byPoolChans;/*每路解码通道上的循环通道数量, 最多4通道 0表示没有解码*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECPOOLNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DECCHANINFO[] struchanConInfo;
            public byte byEnablePoll;/*是否轮巡 0-否 1-是*/
            public byte byPoolTime;/*轮巡时间 0-保留 1-10秒 2-15秒 3-20秒 4-30秒 5-45秒 6-1分钟 7-2分钟 8-5分钟 */
        }

        /*整个设备解码配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECCFG
        {
            public uint dwSize;
            public uint dwDecChanNum;/*解码通道的数量*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DECINFO[] struDecInfo;
        }

        //2005-08-01
        /* 解码设备透明通道设置 */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PORTINFO
        {
            public uint dwEnableTransPort;/* 是否启动透明通道 0－不启用 1－启用*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDecoderIP;/* DVR IP地址 */
            public ushort wDecoderPort;/* 端口号 */
            public ushort wDVRTransPort;/* 配置前端DVR是从485/232输出，1表示232串口,2表示485串口 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string cReserve;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PORTCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRANSPARENTNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_PORTINFO[] struTransPortInfo;/* 数组0表示232 数组1表示485 */
        }

        /* 控制网络文件回放 */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PLAYREMOTEFILE
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDecoderIP;/* DVR IP地址 */
            public ushort wDecoderPort;/* 端口号 */
            public ushort wLoadMode;/* 回放下载模式 1－按名字 2－按时间 */

            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct mode_size
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
                [FieldOffsetAttribute(0)]
                public byte[] byFile;/* 回放的文件名 */

                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct bytime
                {
                    public uint dwChannel;
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
                    public byte[] sUserName;/*请求视频用户名*/
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;/* 密码 */
                    public NET_DVR_TIME struStartTime;/* 按时间回放的开始时间 */
                    public NET_DVR_TIME struStopTime;/* 按时间回放的结束时间 */
                }
            }
        }

        /*当前设备解码连接状态*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DECCHANSTATUS
        {
            public uint dwWorkType;/*工作方式：1：轮巡、2：动态连接解码、3：文件回放下载 4：按时间回放下载*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/*连接的设备ip*/
            public ushort wDVRPort;/*连接端口号*/
            public byte byChannel;/* 通道号 */
            public byte byLinkMode;/* 连接模式 */
            public uint dwLinkType;/*连接类型 0－主码流 1－子码流*/

            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct objectInfo
            {
                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct userInfo
                {
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
                    public byte[] sUserName;/*请求视频用户名*/
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;/* 密码 */
                    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 52)]
                    public string cReserve;
                }

                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct fileInfo
                {
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
                    public byte[] fileName;
                }
                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct timeInfo
                {
                    public uint dwChannel;
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
                    public byte[] sUserName;/*请求视频用户名*/
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;/* 密码 */
                    public NET_DVR_TIME struStartTime;/* 按时间回放的开始时间 */
                    public NET_DVR_TIME struStopTime;/* 按时间回放的结束时间 */
                }
            }
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECSTATUS
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DECCHANSTATUS[] struTransPortInfo;
        }

        //单字符参数(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_SHOWSTRINGINFO
        {
            public ushort wShowString;// 预览的图象上是否显示字符,0-不显示,1-显示 区域大小704*576,单个字符的大小为32*32
            public ushort wStringSize;/* 该行字符的长度，不能大于44个字符 */
            public ushort wShowStringTopLeftX;/* 字符显示位置的x坐标 */
            public ushort wShowStringTopLeftY;/* 字符名称显示位置的y坐标 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 44)]
            public string sString;/* 要显示的字符内容 */
        }

        //叠加字符(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;/* 要显示的字符内容 */
        }

        //叠加字符扩展(8条字符)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM_EX, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;/* 要显示的字符内容 */
        }

        //叠加字符
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;/* 要显示的字符内容 */
        }

        /****************************DS9000新增结构(begin)******************************/

        /*EMAIL参数结构*/
        //与原结构体有差异
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struReceiver
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sName;/* 收件人姓名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAddress;/* 收件人地址 */
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EMAILCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAccount;/* 账号*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_PWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/*密码 */

            [StructLayoutAttribute(LayoutKind.Sequential)]
            public struct struSender
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
                public byte[] sName;/* 发件人姓名 */
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
                public byte[] sAddress;/* 发件人地址 */
            }

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSmtpServer;/* smtp服务器 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPop3Server;/* pop3服务器 */

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.Struct)]
            public struReceiver[] struStringInfo;/* 最多可以设置3个收件人 */

            public byte byAttachment;/* 是否带附件 */
            public byte bySmtpServerVerify;/* 发送服务器要求身份验证 */
            public byte byMailInterval;/* mail interval */
            public byte byEnableSSL;//ssl是否启用9000_1.1
            public ushort wSmtpPort;//gmail的465，普通的为25  
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 74, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }

        /*DVR实现巡航数据结构*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISE_PARA
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;/* 预置点号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseSpeed;/* 巡航速度 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.U2)]
            public ushort[] wDwellTime;/* 停留时间 */
            public byte byEnableThisCruise;/* 是否启用 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }
        /****************************DS9000新增结构(end)******************************/

        //时间点
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIMEPOINT
        {
            public uint dwMonth;//月 0-11表示1-12个月
            public uint dwWeekNo;//第几周 0－第1周 1－第2周 2－第3周 3－第4周 4－最后一周
            public uint dwWeekDate;//星期几 0－星期日 1－星期一 2－星期二 3－星期三 4－星期四 5－星期五 6－星期六
            public uint dwHour;//小时	开始时间0－23 结束时间1－23
            public uint dwMin;//分	0－59
        }

        //夏令时参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ZONEANDDST
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public uint dwEnableDST;//是否启用夏时制 0－不启用 1－启用
            public byte byDSTBias;//夏令时偏移值，30min, 60min, 90min, 120min, 以分钟计，传递原始数值
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_TIMEPOINT struBeginPoint;//夏时制开始时间
            public NET_DVR_TIMEPOINT struEndPoint;//夏时制停止时间
        }

        //图片质量
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_JPEGPARA
        {
            /*注意：当图像压缩分辨率为VGA时，支持0=CIF, 1=QCIF, 2=D1抓图，
            当分辨率为3=UXGA(1600x1200), 4=SVGA(800x600), 5=HD720p(1280x720),6=VGA,7=XVGA, 8=HD900p
            仅支持当前分辨率的抓图*/
            public ushort wPicSize;/* 0=CIF, 1=QCIF, 2=D1 3=UXGA(1600x1200), 4=SVGA(800x600), 5=HD720p(1280x720),6=VGA*/
            public ushort wPicQuality;/* 图片质量系数 0-最好 1-较好 2-一般 */
        }

        /* aux video out parameter */
        //辅助输出参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_AUXOUTCFG
        {
            public uint dwSize;
            public uint dwAlarmOutChan;/* 选择报警弹出大报警通道切换时间：1画面的输出通道: 0:主输出/1:辅1/2:辅2/3:辅3/4:辅4 */
            public uint dwAlarmChanSwitchTime;/* :1秒 - 10:10秒 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUXOUT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwAuxSwitchTime;/* 辅助输出切换时间: 0-不切换,1-5s,2-10s,3-20s,4-30s,5-60s,6-120s,7-300s */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUXOUT * MAX_WINDOW, ArraySubType = UnmanagedType.I1)]
            public byte[] byAuxOrder;/* 辅助输出预览顺序, 0xff表示相应的窗口不预览 */
        }

        //ntp
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_NTPPARA
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sNTPServer;/* Domain Name or IP addr of NTP server */
            public ushort wInterval;/* adjust time interval(hours) */
            public byte byEnableNTP;/* enable NPT client 0-no，1-yes*/
            public byte cTimeDifferenceH;/* 与国际标准时间的 小时偏移-12 ... +13 */
            public byte cTimeDifferenceM;/* 与国际标准时间的 分钟偏移0, 30, 45*/
            public byte res1;
            public ushort wNtpPort; /* ntp server port 9000新增 设备默认为123*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res2;
        }

        //ddns
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* DDNS账号用户名/密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName; /* 域名 */
            public byte byEnableDDNS;/*是否应用 0-否，1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA_EX
        {
            public byte byHostIndex;/* 0-Hikvision DNS 1－Dyndns 2－PeanutHull(花生壳)*/
            public byte byEnableDDNS;/*是否应用DDNS 0-否，1-是*/
            public ushort wDDNSPort;/* DDNS端口号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* DDNS用户名*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* DDNS密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName;/* 设备配备的域名地址 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sServerName;/* DDNS 对应的服务器地址，可以是IP地址或域名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //9000扩展
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struDDNS
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* DDNS账号用户名*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName;/* 设备配备的域名地址 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sServerName;/* DDNS协议对应的服务器地址，可以是IP地址或域名 */
            public ushort wDDNSPort;/* 端口号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA_V30
        {
            public byte byEnableDDNS;
            public byte byHostIndex;/* 0-Hikvision DNS(保留) 1－Dyndns 2－PeanutHull(花生壳)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DDNS_NUMS, ArraySubType = UnmanagedType.Struct)]
            public struDDNS[] struDDNS;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //email
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EMAILPARA
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* 邮件账号/密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sSmtpServer;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sPop3Server;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sMailAddr;/* email */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sEventMailAddr1;/* 上传报警/异常等的email */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sEventMailAddr2;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        //网络参数配置
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_NETAPPCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDNSIp; /* DNS服务器地址 */
            public NET_DVR_NTPPARA struNtpClientParam;/* NTP参数 */
            public NET_DVR_DDNSPARA struDDNSClientParam;/* DDNS参数 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 464, ArraySubType = UnmanagedType.I1)]
            public byte[] res;/* 保留 */
        }

        //nfs结构配置
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_SINGLE_NFS
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sNfsHostIPAddr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PATHNAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNfsDirectory;

            public void Init()
            {
                this.sNfsDirectory = new byte[PATHNAME_LEN];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_NFSCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NFS_DISK, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_NFS[] struNfsDiskParam;

            public void Init()
            {
                this.struNfsDiskParam = new NET_DVR_SINGLE_NFS[MAX_NFS_DISK];

                for (int i = 0; i < MAX_NFS_DISK; i++)
                {
                    struNfsDiskParam[i].Init();
                }
            }
        }

        //巡航点配置(HIK IP快球专用)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISE_POINT
        {
            public byte PresetNum;//预置点
            public byte Dwell;//停留时间
            public byte Speed;//速度
            public byte Reserve;//保留

            public void Init()
            {
                PresetNum = 0;
                Dwell = 0;
                Speed = 0;
                Reserve = 0;
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISE_RET
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CRUISE_POINT[] struCruisePoint;//最大支持32个巡航点

            public void Init()
            {
                struCruisePoint = new NET_DVR_CRUISE_POINT[32];
                for (int i = 0; i < 32; i++)
                {
                    struCruisePoint[i].Init();
                }
            }
        }

        /************************************多路解码器(begin)***************************************/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_NETCFG_OTHER
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sFirstDNSIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sSecondDNSIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DECINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/* DVR IP地址 */
            public ushort wDVRPort;/* 端口号 */
            public byte byChannel;/* 通道号 */
            public byte byTransProtocol;/* 传输协议类型 0-TCP, 1-UDP */
            public byte byTransMode;/* 传输码流模式 0－主码流 1－子码流*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 监控主机登陆帐号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 监控主机密码 */
        }

        //启动/停止动态解码
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DYNAMIC_DEC
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;/* 动态解码通道信息 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_CHAN_STATUS
        {
            public uint dwSize;
            public uint dwIsLinked;/* 解码通道状态 0－休眠 1－正在连接 2－已连接 3-正在解码 */
            public uint dwStreamCpRate;/* Stream copy rate, X kbits/second */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string cRes;/* 保留 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_CHAN_INFO
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;/* 解码通道信息 */
            public uint dwDecState;/* 0-动态解码 1－循环解码 2－按时间回放 3－按文件回放 */
            public NET_DVR_TIME StartTime;/* 按时间回放开始时间 */
            public NET_DVR_TIME StopTime;/* 按时间回放停止时间 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;/* 按文件回放文件名 */
        }

        //连接的通道配置 2007-11-05
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DECCHANINFO
        {
            public uint dwEnable;/* 是否启用 0－否 1－启用*/
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;/* 轮循解码通道信息 */
        }

        //2007-11-05 新增每个解码通道的配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_LOOP_DECINFO
        {
            public uint dwSize;
            public uint dwPoolTime;/*轮巡时间 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CYCLE_CHAN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_DECCHANINFO[] struchanConInfo;
        }

        //2007-12-22
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct TTY_CONFIG
        {
            public byte baudrate;/* 波特率 */
            public byte databits;/* 数据位 */
            public byte stopbits;/* 停止位 */
            public byte parity;/* 奇偶校验位 */
            public byte flowcontrol;/* 流控 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_TRAN_CHAN_INFO
        {
            public byte byTranChanEnable;/* 当前透明通道是否打开 0：关闭 1：打开 */
            /*
             *	多路解码器本地有1个485串口，1个232串口都可以作为透明通道,设备号分配如下：
             *	0 RS485
             *	1 RS232 Console
             */
            public byte byLocalSerialDevice;/* Local serial device */
            /*
             *	远程串口输出还是两个,一个RS232，一个RS485
             *	1表示232串口
             *	2表示485串口
             */
            public byte byRemoteSerialDevice;/* Remote output serial device */
            public byte res1;/* 保留 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteDevIP;/* Remote Device IP */
            public ushort wRemoteDevPort;/* Remote Net Communication Port */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] res2;/* 保留 */
            public TTY_CONFIG RemoteSerialDevCfg;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_TRAN_CHAN_CONFIG
        {
            public uint dwSize;
            public byte by232IsDualChan;/* 设置哪路232透明通道是全双工的 取值1到MAX_SERIAL_NUM */
            public byte by485IsDualChan;/* 设置哪路485透明通道是全双工的 取值1到MAX_SERIAL_NUM */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] res;/* 保留 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SERIAL_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_TRAN_CHAN_INFO[] struTranInfo;/*同时支持建立MAX_SERIAL_NUM个透明通道*/
        }

        //2007-12-24 Merry Christmas Eve...
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/* DVR IP地址 */
            public ushort wDVRPort;/* 端口号 */
            public byte byChannel;/* 通道号 */
            public byte byReserve;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            public uint dwPlayMode;/* 0－按文件 1－按时间*/
            public NET_DVR_TIME StartTime;
            public NET_DVR_TIME StopTime;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY_CONTROL
        {
            public uint dwSize;
            public uint dwPlayCmd;/* 播放命令 见文件播放命令*/
            public uint dwCmdParam;/* 播放命令参数 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS
        {
            public uint dwSize;
            public uint dwCurMediaFileLen;/* 当前播放的媒体文件长度 */
            public uint dwCurMediaFilePosition;/* 当前播放文件的播放位置 */
            public uint dwCurMediaFileDuration;/* 当前播放文件的总时间 */
            public uint dwCurPlayTime;/* 当前已经播放的时间 */
            public uint dwCurMediaFIleFrames;/* 当前播放文件的总帧数 */
            public uint dwCurDataType;/* 当前传输的数据类型，19-文件头，20-流数据， 21-播放结束标志 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 72, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        //2009-4-11 added by likui 多路解码器new
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_MATRIX_PASSIVEMODE
        {
            public ushort wTransProtol;//传输协议，0-TCP, 1-UDP, 2-MCAST
            public ushort wPassivePort;//UDP端口, TCP时默认
            // char	sMcastIP[16];		//TCP,UDP时无效, MCAST时为多播地址
            public NET_DVR_IPADDR struMcastIP;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagDEV_CHAN_INFO
        {
            public NET_DVR_IPADDR struIP;/* DVR IP地址 */
            public ushort wDVRPort;/* 端口号 */
            public byte byChannel;/* 通道号 */
            public byte byTransProtocol;/* 传输协议类型0-TCP，1-UDP */
            public byte byTransMode;/* 传输码流模式 0－主码流 1－子码流*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 71, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 监控主机登陆帐号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 监控主机密码 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagMATRIX_TRAN_CHAN_INFO
        {
            public byte byTranChanEnable;/* 当前透明通道是否打开 0：关闭 1：打开 */
            /*
             *	多路解码器本地有1个485串口，1个232串口都可以作为透明通道,设备号分配如下：
             *	0 RS485
             *	1 RS232 Console
             */
            public byte byLocalSerialDevice;/* Local serial device */
            /*
             *	远程串口输出还是两个,一个RS232，一个RS485
             *	1表示232串口
             *	2表示485串口
             */
            public byte byRemoteSerialDevice;/* Remote output serial device */
            public byte byRes1;/* 保留 */
            public NET_DVR_IPADDR struRemoteDevIP;/* Remote Device IP */
            public ushort wRemoteDevPort;/* Remote Net Communication Port */
            public byte byIsEstablished;/* 透明通道建立成功标志，0-没有成功，1-建立成功 */
            public byte byRes2;/* 保留 */
            public TTY_CONFIG RemoteSerialDevCfg;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byUsername;/* 32BYTES */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPassword;/* 16BYTES */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagMATRIX_TRAN_CHAN_CONFIG
        {
            public uint dwSize;
            public byte by232IsDualChan;/* 设置哪路232透明通道是全双工的 取值1到MAX_SERIAL_NUM */
            public byte by485IsDualChan;/* 设置哪路485透明通道是全双工的 取值1到MAX_SERIAL_NUM */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] vyRes;/* 保留 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SERIAL_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagMATRIX_TRAN_CHAN_INFO[] struTranInfo;/*同时支持建立MAX_SERIAL_NUM个透明通道*/
        }

        /*流媒体服务器基本配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_MEDIA_SERVER_CFG
        {
            public byte byValid;/*是否启用流媒体服务器取流,0表示无效，非0表示有效*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_IPADDR struDevIP;
            public ushort wDevPort;/*流媒体服务器端口*/
            public byte byTransmitType;/*传输协议类型 0-TCP，1-UDP*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 69, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PU_STREAM_CFG
        {
            public uint dwSize;
            public NET_DVR_STREAM_MEDIA_SERVER_CFG struStreamMediaSvrCfg;
            public tagDEV_CHAN_INFO struDevChanInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_CHAN_INFO_V30
        {
            public uint dwEnable;/* 是否启用 0－否 1－启用*/
            public NET_DVR_STREAM_MEDIA_SERVER_CFG streamMediaServerCfg;
            public tagDEV_CHAN_INFO struDevChanInfo;/* 轮循解码通道信息 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagMATRIX_LOOP_DECINFO_V30
        {
            public uint dwSize;
            public uint dwPoolTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CYCLE_CHAN_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_INFO_V30[] struchanConInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagDEC_MATRIX_CHAN_INFO
        {
            public uint dwSize;
            public NET_DVR_STREAM_MEDIA_SERVER_CFG streamMediaServerCfg;/*流媒体服务器配置*/
            public tagDEV_CHAN_INFO struDevChanInfo;/* 解码通道信息 */
            public uint dwDecState;/* 0-动态解码 1－循环解码 2－按时间回放 3－按文件回放 */
            public NET_DVR_TIME StartTime;/* 按时间回放开始时间 */
            public NET_DVR_TIME StopTime;/* 按时间回放停止时间 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;/* 按文件回放文件名 */
            public uint dwGetStreamMode;/*取流模式:1-主动，2-被动*/
            public tagNET_MATRIX_PASSIVEMODE struPassiveMode;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_MATRIX_ABILITY
        {
            public uint dwSize;
            public byte byDecNums;
            public byte byStartChan;
            public byte byVGANums;
            public byte byBNCNums;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byVGAWindowMode;/*VGA支持的窗口模式*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byBNCWindowMode;/*BNC支持的窗口模式*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        //上传logo结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_DISP_LOGOCFG
        {
            public uint dwCorordinateX;//图片显示区域X坐标
            public uint dwCorordinateY;//图片显示区域Y坐标
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byFlash;//是否闪烁1-闪烁，0-不闪烁
            public byte byTranslucent;//是否半透明1-半透明，0-不半透明
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//保留
            public uint dwLogoSize;//LOGO大小，包括BMP的文件头
        }

        /*编码类型*/
        public const int NET_DVR_ENCODER_UNKOWN = 0;/*未知编码格式*/
        public const int NET_DVR_ENCODER_H264 = 1;/*HIK 264*/
        public const int NET_DVR_ENCODER_S264 = 2;/*Standard H264*/
        public const int NET_DVR_ENCODER_MPEG4 = 3;/*MPEG4*/
        /* 打包格式 */
        public const int NET_DVR_STREAM_TYPE_UNKOWN = 0;/*未知打包格式*/
        public const int NET_DVR_STREAM_TYPE_HIKPRIVT = 1; /*海康自定义打包格式*/
        public const int NET_DVR_STREAM_TYPE_TS = 7;/* TS打包 */
        public const int NET_DVR_STREAM_TYPE_PS = 8;/* PS打包 */
        public const int NET_DVR_STREAM_TYPE_RTP = 9;/* RTP打包 */

        /*解码通道状态*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_CHAN_STATUS
        {
            public byte byDecodeStatus;/*当前状态:0:未启动，1：启动解码*/
            public byte byStreamType;/*码流类型*/
            public byte byPacketType;/*打包方式*/
            public byte byRecvBufUsage;/*接收缓冲使用率*/
            public byte byDecBufUsage;/*解码缓冲使用率*/
            public byte byFpsDecV;/*视频解码帧率*/
            public byte byFpsDecA;/*音频解码帧率*/
            public byte byCpuLoad;/*DSP CPU使用率*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDecodedV;/*解码的视频帧*/
            public uint dwDecodedA;/*解码的音频帧*/
            public ushort wImgW;/*解码器当前的图像大小,宽*/
            public ushort wImgH; //高
            public byte byVideoFormat;/*视频制式:0-NON,NTSC--1,PAL--2*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 27, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*显示通道状态*/
        public const int NET_DVR_MAX_DISPREGION = 16;         /*每个显示通道最多可以显示的窗口*/
        //VGA分辨率，目前能用的是：VGA_THS8200_MODE_XGA_60HZ、VGA_THS8200_MODE_SXGA_60HZ、
        //
        public enum VGA_MODE
        {
            VGA_NOT_AVALIABLE,
            VGA_THS8200_MODE_SVGA_60HZ,//（800*600）
            VGA_THS8200_MODE_SVGA_75HZ, //（800*600）
            VGA_THS8200_MODE_XGA_60HZ,//（1024*768）
            VGA_THS8200_MODE_XGA_70HZ, //（1024*768）
            VGA_THS8200_MODE_SXGA_60HZ,//（1280*1024）
            VGA_THS8200_MODE_720P_60HZ,//（1280*720 ）
            VGA_THS8200_MODE_1080i_60HZ,//（1920*1080）
            VGA_THS8200_MODE_1080P_30HZ,//（1920*1080）
            VGA_THS8200_MODE_1080P_25HZ,//（1920*1080）
            VGA_THS8200_MODE_UXGA_30HZ,//（1600*1200）
        }

        /*视频制式标准*/
        public enum VIDEO_STANDARD
        {
            VS_NON = 0,
            VS_NTSC = 1,
            VS_PAL = 2,
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_VGA_DISP_CHAN_CFG
        {
            public uint dwSize;
            public byte byAudio;/*音频是否开启,0-否，1-是*/
            public byte byAudioWindowIdx;/*音频开启子窗口*/
            public byte byVgaResolution;/*VGA的分辨率*/
            public byte byVedioFormat;/*视频制式，1:NTSC,2:PAL,0-NON*/
            public uint dwWindowMode;/*画面模式，从能力集里获取，目前支持1,2,4,9,16*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_WINDOWS, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecChan;/*各个子窗口关联的解码通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISP_CHAN_STATUS
        {
            public byte byDispStatus;/*显示状态：0：未显示，1：启动显示*/
            public byte byBVGA; /*VGA/BNC*/
            public byte byVideoFormat;/*视频制式:1:NTSC,2:PAL,0-NON*/
            public byte byWindowMode;/*当前画面模式*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecChan;/*各个子窗口关联的解码通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_DVR_MAX_DISPREGION, ArraySubType = UnmanagedType.I1)]
            public byte[] byFpsDisp;/*每个子画面的显示帧率*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        public const int MAX_DECODECHANNUM = 32;//多路解码器最大解码通道数
        public const int MAX_DISPCHANNUM = 24;//多路解码器最大显示通道数

        /*解码器设备状态*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR__DECODER_WORK_STATUS
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECODECHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_STATUS[] struDecChanStatus;/*解码通道状态*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISPCHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISP_CHAN_STATUS[] struDispChanStatus;/*显示通道状态*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMIN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatus;/*报警输入状态*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byAalarmOutStatus;/*报警输出状态*/
            public byte byAudioInChanStatus;/*语音对讲状态*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 127, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /************************************多路解码器(end)***************************************/

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_EMAILCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sPassWord;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sFromName;/* Sender *///字符串中的第一个字符和最后一个字符不能是"@",并且字符串中要有"@"字符
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string sFromAddr;/* Sender address */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sToName1;/* Receiver1 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sToName2;/* Receiver2 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string sToAddr1;/* Receiver address1 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string sToAddr2;/* Receiver address2 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sEmailServer;/* Email server address */
            public byte byServerType;/* Email server type: 0-SMTP, 1-POP, 2-IMTP…*/
            public byte byUseAuthen;/* Email server authentication method: 1-enable, 0-disable */
            public byte byAttachment;/* enable attachment */
            public byte byMailinterval;/* mail interval 0-2s, 1-3s, 2-4s. 3-5s*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_NEW
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_EX struLowCompression;//定时录像
            public NET_DVR_COMPRESSION_INFO_EX struEventCompression;//事件触发录像
        }

        //球机位置信息
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZPOS
        {
            public ushort wAction;//获取时该字段无效
            public ushort wPanPos;//水平参数
            public ushort wTiltPos;//垂直参数
            public ushort wZoomPos;//变倍参数
        }

        //球机范围信息
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZSCOPE
        {
            public ushort wPanPosMin;//水平参数min
            public ushort wPanPosMax;//水平参数max
            public ushort wTiltPosMin;//垂直参数min
            public ushort wTiltPosMax;//垂直参数max
            public ushort wZoomPosMin;//变倍参数min
            public ushort wZoomPosMax;//变倍参数max
        }

        //rtsp配置 ipcamera专用
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RTSPCFG
        {
            public uint dwSize;//长度
            public ushort wPort;//rtsp服务器侦听端口
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 54, ArraySubType = UnmanagedType.I1)]
            public byte[] byReserve;//预留
        }

        /********************************接口参数结构(begin)*********************************/

        //NET_DVR_Login()参数结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号
            public byte byAlarmInPortNum;//DVR报警输入个数
            public byte byAlarmOutPortNum;//DVR报警输出个数
            public byte byDiskNum;//DVR硬盘个数
            public byte byDVRType;//DVR类型, 1:DVR 2:ATM DVR 3:DVS ......
            public byte byChanNum;//DVR 通道个数
            public byte byStartChan;//起始通道号,例如DVS-1,DVR - 1
        }

        //NET_DVR_Login_V30()参数结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号
            public byte byAlarmInPortNum;//报警输入个数
            public byte byAlarmOutPortNum;//报警输出个数
            public byte byDiskNum;//硬盘个数
            public byte byDVRType;//设备类型, 1:DVR 2:ATM DVR 3:DVS ...
            public byte byChanNum;//模拟通道个数
            public byte byStartChan;//起始通道号,例如DVS-1,DVR - 1
            public byte byAudioChanNum;//语音通道数
            public byte byIPChanNum;//最大数字通道个数  
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
        }

        //sdk网络环境枚举变量，用于远程升级
        public enum SDK_NETWORK_ENVIRONMENT
        {
            LOCAL_AREA_NETWORK = 0,
            WIDE_AREA_NETWORK,
        }

        //显示模式
        public enum DISPLAY_MODE
        {
            NORMALMODE = 0,
            OVERLAYMODE
        }

        //发送模式
        public enum SEND_MODE
        {
            PTOPTCPMODE = 0,
            PTOPUDPMODE,
            MULTIMODE,
            RTPMODE,
            RESERVEDMODE
        }

        //抓图模式
        public enum CAPTURE_MODE
        {
            BMP_MODE = 0,		//BMP模式
            JPEG_MODE = 1		//JPEG模式 
        }

        //实时声音模式
        public enum REALSOUND_MODE
        {
            MONOPOLIZE_MODE = 1,//独占模式
            SHARE_MODE = 2		//共享模式
        }



        //SDK状态信息(9000新增)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SDKSTATE
        {
            public uint dwTotalLoginNum;//当前login用户数
            public uint dwTotalRealPlayNum;//当前realplay路数
            public uint dwTotalPlayBackNum;//当前回放或下载路数
            public uint dwTotalAlarmChanNum;//当前建立报警通道路数
            public uint dwTotalFormatNum;//当前硬盘格式化路数
            public uint dwTotalFileSearchNum;//当前日志或文件搜索路数
            public uint dwTotalLogSearchNum;//当前日志或文件搜索路数
            public uint dwTotalSerialNum;//当前透明通道路数
            public uint dwTotalUpgradeNum;//当前升级路数
            public uint dwTotalVoiceComNum;//当前语音转发路数
            public uint dwTotalBroadCastNum;//当前语音广播路数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        //SDK功能支持信息(9000新增)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SDKABL
        {
            public uint dwMaxLoginNum;//最大login用户数 MAX_LOGIN_USERS
            public uint dwMaxRealPlayNum;//最大realplay路数 WATCH_NUM
            public uint dwMaxPlayBackNum;//最大回放或下载路数 WATCH_NUM
            public uint dwMaxAlarmChanNum;//最大建立报警通道路数 ALARM_NUM
            public uint dwMaxFormatNum;//最大硬盘格式化路数 SERVER_NUM
            public uint dwMaxFileSearchNum;//最大文件搜索路数 SERVER_NUM
            public uint dwMaxLogSearchNum;//最大日志搜索路数 SERVER_NUM
            public uint dwMaxSerialNum;//最大透明通道路数 SERVER_NUM
            public uint dwMaxUpgradeNum;//最大升级路数 SERVER_NUM
            public uint dwMaxVoiceComNum;//最大语音转发路数 SERVER_NUM
            public uint dwMaxBroadCastNum;//最大语音广播路数 MAX_CASTNUM
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        //报警设备信息
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_ALARMER
        {
            public byte byUserIDValid;/* userid是否有效 0-无效，1-有效 */
            public byte bySerialValid;/* 序列号是否有效 0-无效，1-有效 */
            public byte byVersionValid;/* 版本号是否有效 0-无效，1-有效 */
            public byte byDeviceNameValid;/* 设备名字是否有效 0-无效，1-有效 */
            public byte byMacAddrValid; /* MAC地址是否有效 0-无效，1-有效 */
            public byte byLinkPortValid;/* login端口是否有效 0-无效，1-有效 */
            public byte byDeviceIPValid;/* 设备IP是否有效 0-无效，1-有效 */
            public byte bySocketIPValid;/* socket ip是否有效 0-无效，1-有效 */
            public int lUserID; /* NET_DVR_Login()返回值, 布防时有效 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;/* 序列号 */
            public uint dwDeviceVersion;/* 版本信息 高16位表示主版本，低16位表示次版本*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sDeviceName;/* 设备名字 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;/* MAC地址 */
            public ushort wLinkPort; /* link port */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sDeviceIP;/* IP地址 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sSocketIP;/* 报警主动上传时的socket IP地址 */
            public byte byIpProtocol; /* Ip协议 0-IPV4, 1-IPV6 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //硬解码显示区域参数(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISPLAY_PARA
        {
            public int bToScreen;
            public int bToVideoOut;
            public int nLeft;
            public int nTop;
            public int nWidth;
            public int nHeight;
            public int nReserved;
        }

        //硬解码预览参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARDINFO
        {
            public int lChannel;//通道号
            public int lLinkMode;//最高位(31)为0表示主码流，为1表示子，0－30位表示码流连接方式:0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-电话线，5－128k宽带，6－256k宽带，7－384k宽带，8－512k宽带；
            [MarshalAsAttribute(UnmanagedType.LPStr)]
            public string sMultiCastIP;
            public NET_DVR_DISPLAY_PARA struDisplayPara;
        }

        //录象文件参数
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FIND_DATA
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;//文件名
            public NET_DVR_TIME struStartTime;//文件的开始时间
            public NET_DVR_TIME struStopTime;//文件的结束时间
            public uint dwFileSize;//文件的大小
        }

        //录象文件参数(9000)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FINDDATA_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;//文件名
            public NET_DVR_TIME struStartTime;//文件的开始时间
            public NET_DVR_TIME struStopTime;//文件的结束时间
            public uint dwFileSize;//文件的大小
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
            public byte byLocked;//9000设备支持,1表示此文件已经被锁定,0表示正常的文件
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //录象文件参数(带卡号)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FINDDATA_CARD
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;//文件名
            public NET_DVR_TIME struStartTime;//文件的开始时间
            public NET_DVR_TIME struStopTime;//文件的结束时间
            public uint dwFileSize;//文件的大小
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
        }

        //录象文件查找条件结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FILECOND
        {
            public int lChannel;//通道号
            public uint dwFileType;//录象文件类型0xff－全部，0－定时录像,1-移动侦测 ，2－报警触发，
            //3-报警|移动侦测 4-报警&移动侦测 5-命令触发 6-手动录像
            public uint dwIsLocked;//是否锁定 0-正常文件,1-锁定文件, 0xff表示所有文件
            public uint dwUseCardNo;//是否使用卡号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] sCardNumber;//卡号
            public NET_DVR_TIME struStartTime;//开始时间
            public NET_DVR_TIME struStopTime;//结束时间
        }

        //云台区域选择放大缩小(HIK 快球专用)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_POINT_FRAME
        {
            public int xTop;//方框起始点的x坐标
            public int yTop;//方框结束点的y坐标
            public int xBottom;//方框结束点的x坐标
            public int yBottom;//方框结束点的y坐标
            public int bCounter;//保留
        }

        //语音对讲参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_COMPRESSION_AUDIO
        {
            public byte byAudioEncType;//音频编码类型 0-G722; 1-G711
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byres;//这里保留音频的压缩参数 
        }




        ////////////////////////////////////////////////////////////////////////////////////////
        ///抓拍机
        ///
        public const int MAX_OVERLAP_ITEM_NUM = 50;       //最大字符叠加种数
        public const int NET_ITS_GET_OVERLAP_CFG = 5072;//获取字符叠加参数配置（相机或ITS终端）
        public const int NET_ITS_SET_OVERLAP_CFG = 5073;//设置字符叠加参数配置（相机或ITS终端）

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PLATE_INFO
        {
            public byte byPlateType;
            public byte byColor;
            public byte byBright;
            public byte byLicenseLen;
            public byte byEntireBelieve;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 35, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_VCA_RECT struPlateRect;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public string sLicense;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LICENSE_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byBelieve;

            public void Init()
            {
                byRes = new byte[35];
                byBelieve = new byte[MAX_LICENSE_LEN];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VEHICLE_INFO
        {
            public uint dwIndex;
            public byte byVehicleType;
            public byte byColorDepth;
            public byte byColor;
            public byte byRes1;
            public ushort wSpeed;
            public ushort wLength;
            public byte byIllegalType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 35, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[35];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PLATE_RESULT
        {
            public uint dwSize;
            public byte byResultType;
            public byte byChanIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwRelativeTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byAbsTime;
            public uint dwPicLen;
            public uint dwPicPlateLen;
            public uint dwVideoLen;
            public byte byTrafficLight;
            public byte byPicNum;
            public byte byDriveChan;
            public byte byRes2;
            public uint dwBinPicLen;
            public uint dwCarPicLen;
            public uint dwFarCarPicLen;
            public IntPtr pBuffer3;
            public IntPtr pBuffer4;
            public IntPtr pBuffer5;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
            public NET_DVR_PLATE_INFO struPlateInfo;
            public NET_DVR_VEHICLE_INFO struVehicleInfo;
            public IntPtr pBuffer1;
            public IntPtr pBuffer2;

            public void Init()
            {
                byRes1 = new byte[2];
                byAbsTime = new byte[32];
                byRes3 = new byte[8];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIME_V30
        {
            public ushort wYear;
            public byte byMonth;
            public byte byDay;
            public byte byHour;
            public byte byMinute;
            public byte bySecond;
            public byte byRes;
            public ushort wMilliSec;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_PICTURE_INFO
        {
            public uint dwDataLen;              //媒体数据长度
            public byte byType;                           // 0:车牌图;1:场景图;2:合成图;3:码流
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;          //保留
            public uint dwRedLightTime;                   //经过的红灯时间  （s）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byAbsTime;                 //绝对时间点,yyyymmddhhmmssxxx,e.g.20090810235959999  最后三位为毫秒数
            public NET_VCA_RECT struPlateRect;         //车牌位置
            public NET_VCA_RECT struPlateRecgRect;   //牌识区域坐标
            public IntPtr pBuffer;     //数据指针
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;              //保留
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_PLATE_RESULT
        {
            public uint dwSize;
            public uint dwMatchNo;
            public byte byGroupNum;
            public byte byPicNo;
            public byte bySecondCam;    //是否第二相机抓拍（如远近景抓拍的远景相机，或前后抓拍的后相机，特殊项目中会用到）
            public byte byFeaturePicNo; //闯红灯电警，取第几张图作为特写图,0xff-表示不取
            public byte byDriveChan;                //触发车道号
            public byte byVehicleType;     //0- 未知，1-客车，2-货车，3-轿车，4-面包车，5-小货车
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;                        //保留
            public ushort wIllegalType;       //违章类型采用国标定义
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byIllegalSubType;   //违章子类型
            public byte byPostPicNo;    //违章时取第几张图片作为卡口图,0xff-表示不取
            public byte byChanIndex;                //通道号（保留）
            public ushort wSpeedLimit;            //限速上限（超速时有效）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_PLATE_INFO struPlateInfo;       //车牌信息结构
            public NET_DVR_VEHICLE_INFO struVehicleInfo;        //车辆信息
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 48, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSiteID;          //监测点编号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 48, ArraySubType = UnmanagedType.I1)]
            public byte[] byDeviceID;                                   //设备编号
            public byte byDir;                //监测方向，1-上行，2-下行，3-双向，4-由东向西，5-由南向北,6-由西向东，7-由北向南，8-其它
            public byte byDetectType;    //检测方式,1-地感触发，2-视频触发，3-多帧识别，4-雷达触发
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 22, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3; //保留
            public NET_DVR_TIME_V30 struSnapFirstPicTime;//端点时间(ms)（抓拍第一张图片的时间）
            public uint dwIllegalTime;//违法持续时间（ms） = 抓拍最后一张图片的时间 - 抓拍第一张图片的时间
            public uint dwPicNum;            //图片数量（与picGroupNum不同，代表本条信息附带的图片数量，图片信息由struVehicleInfoEx定义   
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.Struct)]
            public NET_ITS_PICTURE_INFO[] struPicInfo;                //图片信息,单张回调，最多6张图，由序号区分            
        }

        //字符叠加配置条件参数结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAPCFG_COND
        {
            public uint dwSize;
            public uint dwChannel;//通道号 
            public uint dwConfigMode;//配置模式：0- 终端，1- 前端(直连前端或终端接前端)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
            public uint dwOperateType;
        }

        //单条字符叠加信息结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_SINGLE_ITEM_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public byte byItemType;//类型
            public byte byChangeLineNum;//叠加项后的换行数，取值范围：[0,10]，默认：0 
            public byte bySpaceNum;//叠加项后的空格数，取值范围：[0-255]，默认：0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }

        //字符串参数配置结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_ITEM_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_OVERLAP_ITEM_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_ITS_OVERLAP_SINGLE_ITEM_PARAM[] struSingleItem;//字符串内容信息
            public uint dwLinePercent;
            public uint dwItemsStlye;
            public ushort wStartPosTop;
            public ushort wStartPosLeft;
            public ushort wCharStyle;
            public ushort wCharSize;
            public ushort wCharInterval;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public uint dwForeClorRGB;//前景色的RGB值，bit0~bit7: B，bit8~bit15: G，bit16~bit23: R，默认：x00FFFFFF-白
            public uint dwBackClorRGB;//背景色的RGB值，只对图片外叠加有效，bit0~bit7: B，bit8~bit15: G，bit16~bit23: R，默认：x00000000-黑 
            public byte byColorAdapt;//颜色是否自适应：0-否，1-是
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }

        //字符叠加内容信息结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_INFO_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] bySite;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRoadNum;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byInstrumentNum;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byDirection;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byDirectionDesc;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byLaneDes;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//这里保留音频的压缩参数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 44, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSite1;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSite2;//这里保留音频的压缩参数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//这里保留音频的压缩参数 
        }

        //字符叠加配置条件参数结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_CFG
        {
            public uint dwSize;
            public byte byEnable;//是否启用：0- 不启用，1- 启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//这里保留音频的压缩参数
            public NET_ITS_OVERLAP_ITEM_PARAM struOverLapItem;//字符串参数
            public NET_ITS_OVERLAP_INFO_PARAM struOverLapInfo;//字符串内容信息
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//这里保留音频的压缩参数 
        }

        //报警布防参数结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SETUPALARM_PARAM
        {
            public uint dwSize;
            public byte byLevel;//布防优先级：0- 一等级（高），1- 二等级（中），2- 三等级（低，保留）
            public byte byAlarmInfoType;//上传报警信息类型（智能交通摄像机支持）：0- 老报警信息（NET_DVR_PLATE_RESULT），1- 新报警信息(NET_ITS_PLATE_RESULT) 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//这里保留音频的压缩参数 
        }

        //道闸控制参数
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_BARRIERGATE_CFG
        {
            public uint dwSize;
            public uint dwChannel;
            public byte byLaneNo;
            public byte byBarrierGateCtrl;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int MAX_RELAY_NUM = 12;
        public const int MAX_IOIN_NUM = 8;
        public const int MAX_VEHICLE_TYPE_NUM = 8;

        public const int NET_DVR_GET_ENTRANCE_PARAMCFG = 3126; //获取出入口控制参数
        public const int NET_DVR_SET_ENTRANCE_PARAMCFG = 3127; //设置出入口控制参数

        //出入口控制条件
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_BARRIERGATE_COND
        {
            public byte byLaneNo;//车道号：0- 表示无效值(设备需要做有效值判断)，1- 车道1
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //继电器关联配置
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_RELAY_PARAM
        {
            public byte byAccessDevInfo;//0-不接入设备，1-开道闸、2-关道闸、3-停道闸、4-报警信号、5-常亮灯
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //车辆信息管控参数
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_VEHICLE_CONTROL
        {
            public byte byGateOperateType;//操作类型：0- 无操作，1- 开道闸
            public byte byRes1;
            public ushort wAlarmOperateType; //报警处理类型：0- 无操作，bit0- 继电器输出报警，bit1- 布防上传报警，bit3- 告警主机上传，值：0-表示关，1-表示开，可复选
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //出入口控制参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ENTRANCE_CFG
        {
            public uint dwSize;
            public byte byEnable;
            public byte byBarrierGateCtrlMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public uint dwRelateTriggerMode;
            public uint dwMatchContent;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RELAY_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RELAY_PARAM[] struRelayRelateInfo;//继电器关联配置信息
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IOIN_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byGateSingleIO;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VEHICLE_TYPE_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VEHICLE_CONTROL[] struVehicleCtrl;//车辆信息管控
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//保留
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MANUALSNAP
        {
            public byte byOSDEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }




        /********************************接口参数结构(end)*********************************/


        /********************************SDK接口函数声明*********************************/

        /*********************************************************
        Function:	NET_DVR_Init
        Desc:		初始化SDK，调用其他SDK函数的前提。
        Input:	
        Output:	
        Return:	TRUE表示成功，FALSE表示失败。
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Init();

        /*********************************************************
        Function:	NET_DVR_Cleanup
        Desc:		释放SDK资源，在结束之前最后调用
        Input:	
        Output:	
        Return:	TRUE表示成功，FALSE表示失败
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Cleanup();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessage(uint nMessage, IntPtr hWnd);

        /*********************************************************
        Function:	EXCEPYIONCALLBACK
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void EXCEPYIONCALLBACK(uint dwType, int lUserID, int lHandle, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetExceptionCallBack_V30(uint nMessage, IntPtr hWnd, EXCEPYIONCALLBACK fExceptionCallBack, IntPtr pUser);


        /*********************************************************
        Function:	MESSCALLBACK
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate int MESSCALLBACK(int lCommand, string sDVRIP, string pBuf, uint dwBufLen);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack(MESSCALLBACK fMessCallBack);

        /*********************************************************
        Function:	MESSCALLBACKEX
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate int MESSCALLBACKEX(int iCommand, int iUserID, string pBuf, uint dwBufLen);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack_EX(MESSCALLBACKEX fMessCallBack_EX);

        /*********************************************************
        Function:	MESSCALLBACKNEW
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate int MESSCALLBACKNEW(int lCommand, string sDVRIP, string pBuf, uint dwBufLen, ushort dwLinkDVRPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack_NEW(MESSCALLBACKNEW fMessCallBack_NEW);

        /*********************************************************
        Function:	MESSAGECALLBACK
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate int MESSAGECALLBACK(int lCommand, System.IntPtr sDVRIP, System.IntPtr pBuf, uint dwBufLen, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack(MESSAGECALLBACK fMessageCallBack, uint dwUser);


        /*********************************************************
        Function:	MSGCallBack
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void MSGCallBack(int lCommand, ref NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V30(MSGCallBack fMessageCallBack, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V31(MSGCallBack fMessageCallBack, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConnectTime(uint dwWaitTime, uint dwTryTimes);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetReconnect(uint dwInterval, int bEnableRecon);

        [DllImport(@"HCNetSDK.dll")]
        public static extern uint NET_DVR_GetSDKVersion();

        [DllImport(@"HCNetSDK.dll")]
        public static extern uint NET_DVR_GetSDKBuildVersion();

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_IsSupport();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartListen(string sLocalIP, ushort wLocalPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopListen();

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartListen_V30(String sLocalIP, ushort wLocalPort, MSGCallBack DataCallback, IntPtr pUserData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopListen_V30(Int32 lListenHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_Login(string sDVRIP, ushort wDVRPort, string sUserName, string sPassword, ref NET_DVR_DEVICEINFO lpDeviceInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Logout(int iUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern uint NET_DVR_GetLastError();

        [DllImport(@"HCNetSDK.dll")]
        public static extern string NET_DVR_GetErrorMsg(ref int pErrorNo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetShowMode(uint dwShowType, uint colorKey);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRIPByResolveSvr(string sServerIP, ushort wServerPort, string sDVRName, ushort wDVRNameLen, string sDVRSerialNumber, ushort wDVRSerialLen, IntPtr pGetIP);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRIPByResolveSvr_EX(string sServerIP, ushort wServerPort, ref byte sDVRName, ushort wDVRNameLen, ref byte sDVRSerialNumber, ushort wDVRSerialLen, string sGetIP, ref uint dwPort);

        //预览相关接口
        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_RealPlay(int iUserID, ref NET_DVR_CLIENTINFO lpClientInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_SDK_RealPlay(int iUserLogID, ref NET_SDK_CLIENTINFO lpDVRClientInfo);
        /*********************************************************
        Function:	REALDATACALLBACK
        Desc:		预览回调
        Input:	lRealHandle 当前的预览句柄 
                dwDataType 数据类型
                pBuffer 存放数据的缓冲区指针 
                dwBufSize 缓冲区大小 
                pUser 用户数据 
        Output:	
        Return:	void
        **********************************************************/
        public delegate void REALDATACALLBACK(Int32 lRealHandle, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser);
        [DllImport(@"HCNetSDK.dll")]

        /*********************************************************
        Function:	NET_DVR_RealPlay_V30
        Desc:		实时预览。
        Input:	lUserID [in] NET_DVR_Login()或NET_DVR_Login_V30()的返回值 
                lpClientInfo [in] 预览参数 
                cbRealDataCallBack [in] 码流数据回调函数 
                pUser [in] 用户数据 
                bBlocked [in] 请求码流过程是否阻塞：0－否；1－是 
        Output:	
        Return:	1表示失败，其他值作为NET_DVR_StopRealPlay等函数的句柄参数
        **********************************************************/
        public static extern int NET_DVR_RealPlay_V30(int iUserID, ref NET_DVR_CLIENTINFO lpClientInfo, REALDATACALLBACK fRealDataCallBack_V30, IntPtr pUser, UInt32 bBlocked);

        /*********************************************************
        Function:	NET_DVR_StopRealPlay
        Desc:		停止预览。
        Input:	lRealHandle [in] 预览句柄，NET_DVR_RealPlay或者NET_DVR_RealPlay_V30的返回值 
        Output:	
        Return:	
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRealPlay(int iRealHandle);

        /*********************************************************
        Function:	DRAWFUN
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void DRAWFUN(int lRealHandle, IntPtr hDc, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RigisterDrawFun(int lRealHandle, DRAWFUN fDrawFun, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayerBufNumber(Int32 lRealHandle, uint dwBufNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ThrowBFrame(Int32 lRealHandle, uint dwNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAudioMode(uint dwMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSound(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSound();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSoundShare(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSoundShare(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Volume(Int32 lRealHandle, ushort wVolume);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveRealData(Int32 lRealHandle, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopSaveRealData(Int32 lRealHandle);

        /*********************************************************
        Function:	REALDATACALLBACK
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void SETREALDATACALLBACK(int lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetRealDataCallBack(int lRealHandle, SETREALDATACALLBACK fRealDataCallBack, uint dwUser);

        /*********************************************************
        Function:	STDDATACALLBACK
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void STDDATACALLBACK(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetStandardDataCallBack(int lRealHandle, STDDATACALLBACK fStdDataCallBack, uint dwUser);


        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CapturePicture(Int32 lRealHandle, string sPicFileName);

        //动态生成I帧
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MakeKeyFrame(Int32 lUserID, Int32 lChannel);//主码流

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MakeKeyFrameSub(Int32 lUserID, Int32 lChannel);//子码流

        //云台控制相关接口
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZCtrl(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZCtrl_Other(Int32 lUserID, int lChannel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl(Int32 lRealHandle, uint dwPTZCommand, uint dwStop);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl_Other(Int32 lUserID, Int32 lChannel, uint dwPTZCommand, uint dwStop);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ(Int32 lRealHandle, string pPTZCodeBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ_Other(int lUserID, int lChannel, string pPTZCodeBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset_Other(int lUserID, int lChannel, uint dwPTZPresetCmd, uint dwPresetIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ_EX(int lRealHandle, string pPTZCodeBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl_EX(int lRealHandle, uint dwPTZCommand, uint dwStop);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset_EX(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise(int lRealHandle, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise_Other(int lUserID, int lChannel, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise_EX(int lRealHandle, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack(int lRealHandle, uint dwPTZTrackCmd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack_Other(int lUserID, int lChannel, uint dwPTZTrackCmd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack_EX(int lRealHandle, uint dwPTZTrackCmd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed(int lRealHandle, uint dwPTZCommand, uint dwStop, uint dwSpeed);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed_Other(int lUserID, int lChannel, int dwPTZCommand, int dwStop, int dwSpeed);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed_EX(int lRealHandle, uint dwPTZCommand, uint dwStop, uint dwSpeed);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZCruise(int lUserID, int lChannel, int lCruiseRoute, ref NET_DVR_CRUISE_RET lpCruiseRet);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZMltTrack(int lRealHandle, uint dwPTZTrackCmd, uint dwTrackIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZMltTrack_Other(int lUserID, int lChannel, uint dwPTZTrackCmd, uint dwTrackIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZMltTrack_EX(int lRealHandle, uint dwPTZTrackCmd, uint dwTrackIndex);

        //文件查找与回放
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile(int lUserID, int lChannel, uint dwFileType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile(int lFindHandle, ref NET_DVR_FIND_DATA lpFindData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindClose(int lFindHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile_V30(int lFindHandle, ref NET_DVR_FINDDATA_V30 lpFindData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile_V30(int lUserID, ref NET_DVR_FILECOND pFindCond);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindClose_V30(int lFindHandle);

        //2007-04-16增加查询结果带卡号的文件查找
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile_Card(int lFindHandle, ref NET_DVR_FINDDATA_CARD lpFindData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile_Card(int lUserID, int lChannel, uint dwFileType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_LockFileByName(int lUserID, string sLockFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_UnlockFileByName(int lUserID, string sUnlockFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackByName(int lUserID, string sPlayBackFileName, IntPtr hWnd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackByTime(int lUserID, int lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, System.IntPtr hWnd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, ref uint LPOutValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBack(int lPlayHandle);

        /*********************************************************
        Function:	PLAYDATACALLBACK
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void PLAYDATACALLBACK(int lPlayHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayDataCallBack(int lPlayHandle, PLAYDATACALLBACK fPlayDataCallBack, uint dwUser);


        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackSaveData(int lPlayHandle, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBackSave(int lPlayHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPlayBackOsdTime(int lPlayHandle, ref NET_DVR_TIME lpOsdTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackCaptureFile(int lPlayHandle, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetFileByName(int lUserID, string sDVRFileName, string sSavedFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetFileByTime(int lUserID, int lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, string sSavedFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopGetFile(int lFileHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadPos(int lFileHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetPlayBackPos(int lPlayHandle);

        //升级
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_Upgrade(int lUserID, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetUpgradeState(int lUpgradeHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetUpgradeProgress(int lUpgradeHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseUpgradeHandle(int lUpgradeHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetNetworkEnvironment(uint dwEnvironmentLevel);

        //远程格式化硬盘
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FormatDisk(int lUserID, int lDiskNumber);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetFormatProgress(int lFormatHandle, ref int pCurrentFormatDisk, ref int pCurrentDiskPos, ref int pFormatStatic);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseFormatHandle(int lFormatHandle);

        //报警
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseAlarmChan(int lAlarmHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan_V30(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan_V41(int lUserID, ref NET_DVR_SETUPALARM_PARAM lpSetupParam);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseAlarmChan_V30(int lAlarmHandle);

        //语音对讲
        /*********************************************************
        Function:	VOICEDATACALLBACK
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void VOICEDATACALLBACK(int lVoiceComHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom(int lUserID, VOICEDATACALLBACK fVoiceDataCallBack, uint dwUser);

        /*********************************************************
        Function:	VOICEDATACALLBACKV30
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void VOICEDATACALLBACKV30(int lVoiceComHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, System.IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_V30(int lUserID, uint dwVoiceChan, bool bNeedCBNoEncData, VOICEDATACALLBACKV30 fVoiceDataCallBack, IntPtr pUser);


        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVoiceComClientVolume(int lVoiceComHandle, ushort wVolume);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopVoiceCom(int lVoiceComHandle);

        //语音转发
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_MR(int lUserID, VOICEDATACALLBACK fVoiceDataCallBack, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_MR_V30(int lUserID, uint dwVoiceChan, VOICEDATACALLBACKV30 fVoiceDataCallBack, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_VoiceComSendData(int lVoiceComHandle, string pSendBuf, uint dwBufSize);

        //语音广播
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStart();

        /*********************************************************
        Function:	VOICEAUDIOSTART
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void VOICEAUDIOSTART(string pRecvDataBuffer, uint dwBufSize, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStart_V30(VOICEAUDIOSTART fVoiceAudioStart, IntPtr pUser);


        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStop();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_AddDVR(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_AddDVR_V30(int lUserID, uint dwVoiceChan);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DelDVR(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DelDVR_V30(int lVoiceHandle);


        //透明通道设置
        /*********************************************************
        Function:	SERIALDATACALLBACK
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void SERIALDATACALLBACK(int lSerialHandle, string pRecvDataBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStart(int lUserID, int lSerialPort, SERIALDATACALLBACK fSerialDataCallBack, uint dwUser);

        //485作为透明通道时，需要指明通道号，因为不同通道号485的设置可以不同(比如波特率)
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialSend(int lSerialHandle, int lChannel, string pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStop(int lSerialHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendTo232Port(int lUserID, string pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendToSerialPort(int lUserID, uint dwSerialPort, uint dwSerialIndex, string pSendBuf, uint dwBufSize);

        //解码 nBitrate = 16000
        [DllImport(@"HCNetSDK.dll")]
        public static extern System.IntPtr NET_DVR_InitG722Decoder(int nBitrate);

        [DllImport(@"HCNetSDK.dll")]
        public static extern void NET_DVR_ReleaseG722Decoder(IntPtr pDecHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecodeG722Frame(IntPtr pDecHandle, ref byte pInBuffer, ref byte pOutBuffer);

        //编码
        [DllImport(@"HCNetSDK.dll")]
        public static extern IntPtr NET_DVR_InitG722Encoder();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EncodeG722Frame(IntPtr pEncodeHandle, ref byte pInBuffer, ref byte pOutBuffer);

        [DllImport(@"HCNetSDK.dll")]
        public static extern void NET_DVR_ReleaseG722Encoder(IntPtr pEncodeHandle);

        //远程控制本地显示
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClickKey(int lUserID, int lKeyIndex);

        //远程控制设备端手动录像
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDVRRecord(int lUserID, int lChannel, int lRecordType);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDVRRecord(int lUserID, int lChannel);

        //解码卡
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDevice_Card(ref int pDeviceTotalChan);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDevice_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDDraw_Card(IntPtr hParent, uint colorKey);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDDraw_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_RealPlay_Card(int lUserID, ref NET_DVR_CARDINFO lpCardInfo, int lChannelNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ResetPara_Card(int lRealHandle, ref NET_DVR_DISPLAY_PARA lpDisplayPara);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RefreshSurface_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClearSurface_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RestoreSurface_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSound_Card(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSound_Card(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVolume_Card(int lRealHandle, ushort wVolume);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_AudioPreview_Card(int lRealHandle, int bEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetCardLastError_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern System.IntPtr NET_DVR_GetChanHandle_Card(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CapturePicture_Card(int lRealHandle, string sPicFileName);

        //获取解码卡序列号此接口无效，改用GetBoardDetail接口获得(2005-12-08支持)
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSerialNum_Card(int lChannelNum, ref uint pDeviceSerialNo);

        //日志
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindDVRLog(int lUserID, int lSelectMode, uint dwMajorType, uint dwMinorType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextLog(int lLogHandle, ref NET_DVR_LOG lpLogData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindLogClose(int lLogHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindDVRLog_V30(int lUserID, int lSelectMode, uint dwMajorType, uint dwMinorType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, bool bOnlySmart);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextLog_V30(int lLogHandle, ref NET_DVR_LOG_V30 lpLogData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindLogClose_V30(int lLogHandle);

        //截止2004年8月5日,共113个接口
        //ATM DVR
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFileByCard(int lUserID, int lChannel, uint dwFileType, int nFindType, ref byte sCardNumber, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);


        //2005-09-15
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CaptureJPEGPicture(int lUserID, int lChannel, ref NET_DVR_JPEGPARA lpJpegPara, string sPicFileName);

        //JPEG抓图到内存
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CaptureJPEGPicture_NEW(int lUserID, int lChannel, ref NET_DVR_JPEGPARA lpJpegPara, string sJpegPicBuffer, uint dwPicSize, ref uint lpSizeReturned);

        //2006-02-16
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetRealPlayerIndex(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetPlayBackPlayerIndex(int lPlayHandle);

        //2006-08-28 704-640 缩放配置
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetScaleCFG(int lUserID, uint dwScale);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetScaleCFG(int lUserID, ref uint lpOutScale);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetScaleCFG_V30(int lUserID, ref NET_DVR_SCALECFG pScalecfg);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetScaleCFG_V30(int lUserID, ref NET_DVR_SCALECFG pScalecfg);

        //2006-08-28 ATM机端口设置
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetATMPortCFG(int lUserID, ushort wATMPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetATMPortCFG(int lUserID, ref ushort LPOutATMPort);

        //2006-11-10 支持显卡辅助输出
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDDrawDevice();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDDrawDevice();

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDDrawDeviceTotalNums();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDDrawDevice(int lPlayPort, uint nDeviceNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZSelZoomIn(int lRealHandle, ref NET_DVR_POINT_FRAME pStruPointFrame);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZSelZoomIn_EX(int lUserID, int lChannel, ref NET_DVR_POINT_FRAME pStruPointFrame);

        //解码设备DS-6001D/DS-6001F
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDecode(int lUserID, int lChannel, ref NET_DVR_DECODERINFO lpDecoderinfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDecode(int lUserID, int lChannel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecoderState(int lUserID, int lChannel, ref NET_DVR_DECODERSTATE lpDecoderState);

        //2005-08-01
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDecInfo(int lUserID, int lChannel, ref NET_DVR_DECCFG lpDecoderinfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecInfo(int lUserID, int lChannel, ref NET_DVR_DECCFG lpDecoderinfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDecTransPort(int lUserID, ref NET_DVR_PORTCFG lpTransPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecTransPort(int lUserID, ref NET_DVR_PORTCFG lpTransPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecPlayBackCtrl(int lUserID, int lChannel, uint dwControlCode, uint dwInValue, ref uint LPOutValue, ref NET_DVR_PLAYREMOTEFILE lpRemoteFileInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDecSpecialCon(int lUserID, int lChannel, ref NET_DVR_DECCHANINFO lpDecChanInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDecSpecialCon(int lUserID, int lChannel, ref NET_DVR_DECCHANINFO lpDecChanInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecCtrlDec(int lUserID, int lChannel, uint dwControlCode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecCtrlScreen(int lUserID, int lChannel, uint dwControl);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecCurLinkStatus(int lUserID, int lChannel, ref NET_DVR_DECSTATUS lpDecStatus);

        //多路解码器
        //2007-11-30 V211支持以下接口 //11
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStartDynamic(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DYNAMIC_DEC lpDynamicInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStopDynamic(int lUserID, uint dwDecChanNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanInfo(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_CHAN_INFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetLoopDecChanInfo(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_LOOP_DECINFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecChanInfo(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_LOOP_DECINFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetLoopDecChanEnable(int lUserID, uint dwDecChanNum, uint dwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecChanEnable(int lUserID, uint dwDecChanNum, ref uint lpdwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecEnable(int lUserID, ref uint lpdwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetDecChanEnable(int lUserID, uint dwDecChanNum, uint dwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanEnable(int lUserID, uint dwDecChanNum, ref uint lpdwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanStatus(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_CHAN_STATUS lpInter);

        //2007-12-22 增加支持接口 //18
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetTranInfo(int lUserID, ref NET_DVR_MATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetTranInfo(int lUserID, ref NET_DVR_MATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetRemotePlay(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_REMOTE_PLAY lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetRemotePlayControl(int lUserID, uint dwDecChanNum, uint dwControlCode, uint dwInValue, ref uint LPOutValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetRemotePlayStatus(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS lpOuter);

        //2009-4-13 新增
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStartDynamic_V30(int lUserID, uint dwDecChanNum, ref NET_DVR_PU_STREAM_CFG lpDynamicInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetLoopDecChanInfo_V30(int lUserID, uint dwDecChanNum, ref tagMATRIX_LOOP_DECINFO_V30 lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecChanInfo_V30(int lUserID, uint dwDecChanNum, ref tagMATRIX_LOOP_DECINFO_V30 lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanInfo_V30(int lUserID, uint dwDecChanNum, ref tagDEC_MATRIX_CHAN_INFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetTranInfo_V30(int lUserID, ref tagMATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetTranInfo_V30(int lUserID, ref tagMATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDisplayCfg(int lUserID, uint dwDispChanNum, ref tagNET_DVR_VGA_DISP_CHAN_CFG lpDisplayCfg);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetDisplayCfg(int lUserID, uint dwDispChanNum, ref tagNET_DVR_VGA_DISP_CHAN_CFG lpDisplayCfg);


        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_MatrixStartPassiveDecode(int lUserID, uint dwDecChanNum, ref tagNET_MATRIX_PASSIVEMODE lpPassiveMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSendData(int lPassiveHandle, System.IntPtr pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStopPassiveDecode(int lPassiveHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_UploadLogo(int lUserID, uint dwDispChanNum, ref tagNET_DVR_DISP_LOGOCFG lpDispLogoCfg, System.IntPtr sLogoBuffer);

        public const int NET_DVR_SHOWLOGO = 1;/*显示LOGO*/
        public const int NET_DVR_HIDELOGO = 2;/*隐藏LOGO*/

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_LogoSwitch(int lUserID, uint dwDecChan, uint dwLogoSwitch);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDeviceStatus(int lUserID, ref tagNET_DVR__DECODER_WORK_STATUS lpDecoderCfg);

        /*显示通道命令码定义*/
        //上海世博 定制
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RigisterPlayBackDrawFun(int lRealHandle, DRAWFUN fDrawFun, uint dwUser);


        public const int DISP_CMD_ENLARGE_WINDOW = 1;	/*显示通道放大某个窗口*/
        public const int DISP_CMD_RENEW_WINDOW = 2;	/*显示通道窗口还原*/

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixDiaplayControl(int lUserID, uint dwDispChanNum, uint dwDispChanCmd, uint dwCmdParam);

        //end
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RefreshPlay(int lPlayHandle);

        //恢复默认值
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RestoreConfig(int lUserID);

        //保存参数
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveConfig(int lUserID);

        //重启
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RebootDVR(int lUserID);

        //关闭DVR
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ShutDownDVR(int lUserID);

        //参数配置 begin
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpOutBuffer, uint dwOutBufferSize, ref uint lpBytesReturned);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRConfig(int lUserID, uint dwCommand, int lChannel, System.IntPtr lpInBuffer, uint dwInBufferSize);

        //报警主机设备用户配置
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAlarmDeviceUser(int lUserID, int lUserIndex, ref NET_DVR_ALARM_DEVICE_USER lpDeviceUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmDeviceUser(int lUserID, int lUserIndex, ref NET_DVR_ALARM_DEVICE_USER lpDeviceUser);

        //批量参数配置
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDeviceConfig(int lUserID, uint dwCommand, uint dwCount, IntPtr lpInBuffer, uint dwInBufferSize, IntPtr lpStatusList, IntPtr lpOutBuffer, uint dwOutBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDeviceConfig(int lUserID, uint dwCommand, uint dwCount, IntPtr lpInBuffer, uint dwInBufferSize, IntPtr lpStatusList, IntPtr lpInParamBuffer, uint dwInParamBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RemoteControl(int lUserID, uint dwCommand, IntPtr lpInBuffer, uint dwInBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRWorkState_V30(int lUserID, IntPtr pWorkState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRWorkState(int lUserID, ref NET_DVR_WORKSTATE lpWorkState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVideoEffect(int lUserID, int lChannel, uint dwBrightValue, uint dwContrastValue, uint dwSaturationValue, uint dwHueValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetVideoEffect(int lUserID, int lChannel, ref uint pBrightValue, ref uint pContrastValue, ref uint pSaturationValue, ref uint pHueValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetframeformat(int lUserID, ref NET_DVR_FRAMEFORMAT lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetframeformat(int lUserID, ref NET_DVR_FRAMEFORMAT lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetframeformat_V30(int lUserID, ref NET_DVR_FRAMEFORMAT_V30 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetframeformat_V30(int lUserID, ref NET_DVR_FRAMEFORMAT_V30 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Getframeformat_V31(int lUserID, ref tagNET_DVR_FRAMEFORMAT_V31 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Setframeformat_V31(int lUserID, ref tagNET_DVR_FRAMEFORMAT_V31 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAtmProtocol(int lUserID, ref tagNET_DVR_ATM_PROTOCOL lpAtmProtocol);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmOut_V30(int lUserID, IntPtr lpAlarmOutState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmOut(int lUserID, ref NET_DVR_ALARMOUTSTATUS lpAlarmOutState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAlarmOut(int lUserID, int lAlarmOutPort, int lAlarmOutStatic);

        //视频参数调节
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetVideoEffect(int lRealHandle, uint dwBrightValue, uint dwContrastValue, uint dwSaturationValue, uint dwHueValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetVideoEffect(int lRealHandle, ref uint pBrightValue, ref uint pContrastValue, ref uint pSaturationValue, ref uint pHueValue);

        //配置文件
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile(int lUserID, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConfigFile(int lUserID, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile_V30(int lUserID, string sOutBuffer, uint dwOutSize, ref uint pReturnSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile_EX(int lUserID, string sOutBuffer, uint dwOutSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConfigFile_EX(int lUserID, string sInBuffer, uint dwInSize);

        //启用日志文件写入接口
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetLogToFile(int bLogEnable, string strLogDir, bool bAutoDel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSDKState(ref NET_DVR_SDKSTATE pSDKState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSDKAbility(ref NET_DVR_SDKABL pSDKAbl);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZProtocol(int lUserID, ref NET_DVR_PTZCFG pPtzcfg);

        //前面板锁定
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_LockPanel(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_UnLockPanel(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetRtspConfig(int lUserID, uint dwCommand, ref NET_DVR_RTSPCFG lpInBuffer, uint dwInBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetRtspConfig(int lUserID, uint dwCommand, ref NET_DVR_RTSPCFG lpOutBuffer, uint dwOutBufferSize);



        //SDK_V222
        //智能设备类型
        public const int DS6001_HF_B = 60;//行为分析：DS6001-HF/B
        public const int DS6001_HF_P = 61;//车牌识别：DS6001-HF/P
        public const int DS6002_HF_B = 62;//双机跟踪：DS6002-HF/B
        public const int DS6101_HF_B = 63;//行为分析：DS6101-HF/B
        public const int IDS52XX = 64;//智能分析仪IVMS
        public const int DS9000_IVS = 65;//9000系列智能DVR
        public const int DS8004_AHL_A = 66;//智能ATM, DS8004AHL-S/A
        public const int DS6101_HF_P = 67;//车牌识别：DS6101-HF/P

        //能力获取命令
        public const int VCA_DEV_ABILITY = 256;//设备智能分析的总能力
        public const int VCA_CHAN_ABILITY = 272;//行为分析能力
        public const int MATRIXDECODER_ABILITY = 512;//多路解码器显示、解码能力
        //获取/设置大接口参数配置命令
        //车牌识别（NET_VCA_PLATE_CFG）
        public const int NET_DVR_SET_PLATECFG = 150;//设置车牌识别参数
        public const int NET_DVR_GET_PLATECFG = 151;//获取车牌识别参数
        //行为对应（NET_VCA_RULECFG）
        public const int NET_DVR_SET_RULECFG = 152;//设置行为分析规则
        public const int NET_DVR_GET_RULECFG = 153;//获取行为分析规则

        //双摄像机标定参数（NET_DVR_LF_CFG）
        public const int NET_DVR_SET_LF_CFG = 160;//设置双摄像机的配置参数
        public const int NET_DVR_GET_LF_CFG = 161;//获取双摄像机的配置参数

        //智能分析仪取流配置结构
        public const int NET_DVR_SET_IVMS_STREAMCFG = 162;//设置智能分析仪取流参数
        public const int NET_DVR_GET_IVMS_STREAMCFG = 163;//获取智能分析仪取流参数

        //智能控制参数结构
        public const int NET_DVR_SET_VCA_CTRLCFG = 164;//设置智能控制参数
        public const int NET_DVR_GET_VCA_CTRLCFG = 165;//获取智能控制参数

        //屏蔽区域NET_VCA_MASK_REGION_LIST
        public const int NET_DVR_SET_VCA_MASK_REGION = 166;//设置屏蔽区域参数
        public const int NET_DVR_GET_VCA_MASK_REGION = 167;//获取屏蔽区域参数

        //ATM进入区域 NET_VCA_ENTER_REGION
        public const int NET_DVR_SET_VCA_ENTER_REGION = 168;//设置进入区域参数
        public const int NET_DVR_GET_VCA_ENTER_REGION = 169;//获取进入区域参数

        //标定线配置NET_VCA_LINE_SEGMENT_LIST
        public const int NET_DVR_SET_VCA_LINE_SEGMENT = 170;//设置标定线
        public const int NET_DVR_GET_VCA_LINE_SEGMENT = 171;//获取标定线

        // ivms屏蔽区域NET_IVMS_MASK_REGION_LIST
        public const int NET_DVR_SET_IVMS_MASK_REGION = 172;//设置IVMS屏蔽区域参数
        public const int NET_DVR_GET_IVMS_MASK_REGION = 173;//获取IVMS屏蔽区域参数
        // ivms进入检测区域NET_IVMS_ENTER_REGION
        public const int NET_DVR_SET_IVMS_ENTER_REGION = 174;//设置IVMS进入区域参数
        public const int NET_DVR_GET_IVMS_ENTER_REGION = 175;//获取IVMS进入区域参数

        public const int NET_DVR_SET_IVMS_BEHAVIORCFG = 176;//设置智能分析仪行为规则参数
        public const int NET_DVR_GET_IVMS_BEHAVIORCFG = 177;//获取智能分析仪行为规则参数

        // IVMS 回放检索
        public const int NET_DVR_IVMS_SET_SEARCHCFG = 178;//设置IVMS回放检索参数
        public const int NET_DVR_IVMS_GET_SEARCHCFG = 179;//获取IVMS回放检索参数

        //报警回调命令
        //对应NET_VCA_PLATE_RESULT
        public const int COMM_ALARM_PLATE = 4353;//车牌识别报警信息
        //对应NET_VCA_RULE_ALARM
        public const int COMM_ALARM_RULE = 4354;//行为分析报警信息

        //结构参数宏定义 
        public const int VCA_MAX_POLYGON_POINT_NUM = 10;//检测区域最多支持10个点的多边形
        public const int MAX_RULE_NUM = 8;//最多规则条数
        public const int MAX_TARGET_NUM = 30;//最多目标个数
        public const int MAX_CALIB_PT = 6;//最大标定点个数
        public const int MIN_CALIB_PT = 4;//最小标定点个数
        public const int MAX_TIMESEGMENT_2 = 2;//最大时间段数
        public const int MAX_LICENSE_LEN = 16;//车牌号最大长度
        public const int MAX_PLATE_NUM = 3;//车牌个数
        public const int MAX_MASK_REGION_NUM = 4;//最多四个屏蔽区域
        public const int MAX_SEGMENT_NUM = 6;//摄像机标定最大样本线数目
        public const int MIN_SEGMENT_NUM = 3;//摄像机标定最小样本线数目

        //智能控制信息
        public const int MAX_VCA_CHAN = 16;//最大智能通道数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CTRLINFO
        {
            public byte byVCAEnable;//是否开启智能
            public byte byVCAType;//智能能力类型，VCA_CHAN_ABILITY_TYPE 
            public byte byStreamWithVCA;//码流中是否带智能信息
            public byte byMode;//模式，VCA_CHAN_MODE_TYPE ,atm能力的时候需要配置
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，设置为0 
        }

        //智能控制信息结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CTRLCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VCA_CHAN, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_CTRLINFO[] struCtrlInfo;//控制信息,数组0对应设备的起始通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //智能设备能力集
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DEV_ABILITY
        {
            public uint dwSize;//结构长度
            public byte byVCAChanNum;//智能通道个数
            public byte byPlateChanNum;//车牌通道个数
            public byte byBBaseChanNum;//行为基本版个数
            public byte byBAdvanceChanNum;//行为高级版个数
            public byte byBFullChanNum;//行为完整版个数
            public byte byATMChanNum;//智能ATM个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //行为分析能力类型
        public enum VCA_ABILITY_TYPE
        {
            TRAVERSE_PLANE_ABILITY = 1,//穿越警戒面
            ENTER_AREA_ABILITY = 2,//进入区域
            EXIT_AREA_ABILITY = 4,//离开区域
            INTRUSION_ABILITY = 8,//入侵
            LOITER_ABILITY = 16,//徘徊
            LEFT_TAKE_ABILITY = 32,//丢包捡包
            PARKING_ABILITY = 64,//停车
            RUN_ABILITY = 128,//奔跑
            HIGH_DENSITY_ABILITY = 256,//内人员密度
            LF_TRACK_ABILITY = 512,//双摄像机跟踪
            STICK_UP_ABILITY = 1073741824,//贴纸条
            INSTALL_SCANNER_ABILITY = -2147483648,//安装读卡器
        }

        //智能通道类型
        public enum VCA_CHAN_ABILITY_TYPE
        {
            VCA_BEHAVIOR_BASE = 1,//行为分析基本版
            VCA_BEHAVIOR_ADVANCE = 2,//行为分析高级版
            VCA_BEHAVIOR_FULL = 3,//行为分析完整版
            VCA_PLATE = 4,//车牌能力
            VCA_ATM = 5,//ATM能力
        }

        //智能ATM模式类型(ATM能力特有)
        public enum VCA_CHAN_MODE_TYPE
        {
            VCA_ATM_PANEL = 0,//ATM面板
            VCA_ATM_SURROUND = 1,//ATM环境
        }

        //通道能力输入参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CHAN_IN_PARAM
        {
            public byte byVCAType;//VCA_CHAN_ABILITY_TYPE枚举值
            public byte byMode;//模式，VCA_CHAN_MODE_TYPE ,atm能力的时候需要配置
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，设置为0 
        }

        //行为能力集结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_BEHAVIOR_ABILITY
        {
            public uint dwSize;//结构长度
            public uint dwAbilityType;//支持的能力类型，按位表示，见VCA_ABILITY_TYPE定义
            public byte byMaxRuleNum;//最大规则数
            public byte byMaxTargetNum;//最大目标数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留，设置为0
        }

        /*********************************************************
        Function:	NET_DVR_GetDeviceAbility
        Desc:		
        Input:	
        Output:	
        Return:	TRUE表示成功，FALSE表示失败。
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDeviceAbility(int lUserID, uint dwAbilityType, IntPtr pInBuf, uint dwInLength, IntPtr pOutBuf, uint dwOutLength);



        //智能共用结构
        //坐标值归一化,浮点数值为当前画面的百分比大小, 精度为小数点后三位
        //点坐标结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_POINT
        {
            public float fX;// X轴坐标, 0.001~1
            public float fY;//Y轴坐标, 0.001~1
        }

        //区域框结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_RECT
        {
            public float fX;//边界框左上角点的X轴坐标, 0.001~1
            public float fY;//边界框左上角点的Y轴坐标, 0.001~1
            public float fWidth;//边界框的宽度, 0.001~1
            public float fHeight;//边界框的高度, 0.001~1
        }

        //行为分析事件类型
        public enum VCA_EVENT_TYPE
        {
            VCA_TRAVERSE_PLANE = 1,//穿越警戒面
            VCA_ENTER_AREA = 2,//目标进入区域,支持区域规则
            VCA_EXIT_AREA = 4,//目标离开区域,支持区域规则
            VCA_INTRUSION = 8,//周界入侵,支持区域规则
            VCA_LOITER = 16,//徘徊,支持区域规则
            VCA_LEFT_TAKE = 32,//丢包捡包,支持区域规则
            VCA_PARKING = 64,//停车,支持区域规则
            VCA_RUN = 128,//奔跑,支持区域规则
            VCA_HIGH_DENSITY = 256,//区域内人员密度,支持区域规则
            VCA_STICK_UP = 1073741824,//贴纸条,支持区域规则
            VCA_INSTALL_SCANNER = -2147483648,//安装读卡器,支持区域规则
        }

        //警戒面穿越方向类型
        public enum VCA_CROSS_DIRECTION
        {
            VCA_BOTH_DIRECTION,// 双向 
            VCA_LEFT_GO_RIGHT,// 由左至右 
            VCA_RIGHT_GO_LEFT,// 由右至左 
        }

        //线结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE
        {
            public tagNET_VCA_POINT struStart;//起点 
            public tagNET_VCA_POINT struEnd; //终点

            //             public void init()
            //             {
            //                 struStart = new tagNET_VCA_POINT();
            //                 struEnd = new tagNET_VCA_POINT();
            //             }
        }

        //该结构会导致xaml界面出不来！！！！！！！！！？？问题暂时还没有找到  
        //暂时屏蔽结构先
        //多边型结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_POLYGON
        {
            /// DWORD->unsigned int
            public uint dwPointNum;

            /// NET_VCA_POINT[10]
            //             [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            //             public tagNET_VCA_POINT[] struPos;

        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_TRAVERSE_PLANE
        {
            public tagNET_VCA_LINE struPlaneBottom;//警戒面底边
            public VCA_CROSS_DIRECTION dwCrossDirection;//穿越方向: 0-双向，1-从左到右，2-从右到左
            public byte byRes1;//保留
            public byte byPlaneHeight;//警戒面高度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 38, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

            //             public void init()
            //             {
            //                 struPlaneBottom = new tagNET_VCA_LINE();
            //                 struPlaneBottom.init();
            //                 byRes2 = new byte[38];
            //             }
        }

        //进入/离开区域参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_AREA
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //根据报警延迟时间来标识报警中带图片，报警间隔和IO报警一致，1秒发送一个。
        //入侵参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_INTRUSION
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//报警延迟时间: 1-120秒，建议5秒，判断是有效报警的时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //徘徊参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PARAM_LOITER
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//触发徘徊报警的持续时间：1-120秒，建议10秒
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //丢包/捡包参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_TAKE_LEFT
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//触发丢包/捡包报警的持续时间：1-120秒，建议10秒
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //停车参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PARKING
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//触发停车报警持续时间：1-120秒，建议10秒
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //奔跑参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RUN
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public float fRunDistance;//人奔跑最大距离, 范围: [0.1, 1.00]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //人员聚集参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_HIGH_DENSITY
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public float fDensity;//密度比率, 范围: [0.1, 1.0]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //贴纸条参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_STICK_UP
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//报警持续时间：10-60秒，建议10秒
            public byte bySensitivity;//灵敏度参数，范围[1,5]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //读卡器参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_SCANNER
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//读卡持续时间：10-60秒
            public byte bySensitivity;//灵敏度参数，范围[1,5]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //警戒事件参数
        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct tagNET_VCA_EVENT_UNION
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.U4)]
            [FieldOffsetAttribute(0)]
            public uint[] uLen;//参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_TRAVERSE_PLANE struTraversePlane;//穿越警戒面参数 
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_AREA struArea;//进入/离开区域参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_INTRUSION struIntrusion;//入侵参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_PARAM_LOITER struLoiter;//徘徊参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_TAKE_LEFT struTakeTeft;//丢包/捡包参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_PARKING struParking;//停车参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_RUN struRun;//奔跑参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_HIGH_DENSITY struHighDensity;//人员聚集参数  
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_STICK_UP struStickUp;//贴纸条
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_SCANNER struScanner;//读卡器参数 

            //             public void init()
            //             {
            //                 uLen = new uint[23];
            //                 struTraversePlane = new tagNET_VCA_TRAVERSE_PLANE();
            //                 struTraversePlane.init();
            //                 struArea = new tagNET_VCA_AREA();
            //                 struArea.init();
            //                 struIntrusion = new tagNET_VCA_INTRUSION();
            //                 struIntrusion.init();
            //                 struLoiter = new tagNET_VCA_PARAM_LOITER();
            //                 struLoiter.init();
            //                 struTakeTeft = new tagNET_VCA_TAKE_LEFT();
            //                 struTakeTeft.init();
            //                 struParking = new tagNET_VCA_PARKING();
            //                 struParking.init();
            //                 struRun = new tagNET_VCA_RUN();
            //                 struRun.init();
            //                 struHighDensity = new tagNET_VCA_HIGH_DENSITY();
            //                 struHighDensity.init();
            //                 struStickUp = new tagNET_VCA_STICK_UP();
            //                 struStickUp.init();
            //                 struScanner = new tagNET_VCA_SCANNER();
            //                 struScanner.init();
            //             }
        }

        // 尺寸过滤器类型
        public enum SIZE_FILTER_MODE
        {
            IMAGE_PIX_MODE,//根据像素大小设置
            REAL_WORLD_MODE,//根据实际大小设置
        }

        //尺寸过滤器
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_SIZE_FILTER
        {
            public byte byActive;//是否激活尺寸过滤器 0-否 非0-是
            public byte byMode;//过滤器模式SIZE_FILTER_MODE
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，置0
            public NET_VCA_RECT struMiniRect;//最小目标框,全0表示不设置
            public NET_VCA_RECT struMaxRect;//最大目标框,全0表示不设置
        }

        //警戒规则结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_ONE_RULE
        {
            public byte byActive;//是否激活规则,0-否,非0-是
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，设置为0字段
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//规则名称
            public VCA_EVENT_TYPE dwEventType;//行为分析事件类型
            public tagNET_VCA_EVENT_UNION uEventParam;//行为分析事件参数
            public tagNET_VCA_SIZE_FILTER struSizeFilter;//尺寸过滤器
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            public NET_DVR_HANDLEEXCEPTION_V30 struHandleType;//处理方式 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//报警触发的录象通道,为1表示触发该通道
        }

        //行为分析配置结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULECFG
        {
            public uint dwSize;//结构长度
            public byte byPicProType;//报警时图片处理方式 0-不处理 非0-上传
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPictureParam;//图片规格结构
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_ONE_RULE[] struRule;//规则数组
        }

        //简化目标结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_TARGET_INFO
        {
            public uint dwID;//目标ID ,人员密度过高报警时为0
            public NET_VCA_RECT struRect; //目标边界框 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }

        //简化的规则信息, 包含规则的基本信息
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULE_INFO
        {
            public byte byRuleID;//规则ID,0-7
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//规则名称
            public VCA_EVENT_TYPE dwEventType;//警戒事件类型
            public tagNET_VCA_EVENT_UNION uEventParam;//事件参数
        }

        //前端设备地址信息，智能分析仪表示的是前端设备的地址信息，其他设备表示本机的地址
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DEV_INFO
        {
            public NET_DVR_IPADDR struDevIP;//前端设备地址，
            public ushort wPort;//前端设备端口号， 
            public byte byChannel;//前端设备通道，
            public byte byRes;// 保留字节
        }

        //行为分析结果上报结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULE_ALARM
        {
            public uint dwSize;//结构长度
            public uint dwRelativeTime;//相对时标
            public uint dwAbsTime;//绝对时标
            public tagNET_VCA_RULE_INFO struRuleInfo;//事件规则信息
            public tagNET_VCA_TARGET_INFO struTargetInfo;//报警目标信息
            public tagNET_VCA_DEV_INFO struDevInfo;//前端设备信息
            public uint dwPicDataLen;//返回图片的长度 为0表示没有图片，大于0表示该结构后面紧跟图片数据*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;//保留，设置为0
            public IntPtr pImage;//指向图片的指针
        }

        //参数关键字
        public enum IVS_PARAM_KEY
        {
            OBJECT_DETECT_SENSITIVE = 1,//目标检测灵敏度
            BACKGROUND_UPDATE_RATE = 2,//背景更新速度
            SCENE_CHANGE_RATIO = 3,//场景变化检测最小值
            SUPPRESS_LAMP = 4,//是否抑制车头灯
            MIN_OBJECT_SIZE = 5,//能检测出的最小目标大小
            OBJECT_GENERATE_RATE = 6,//目标生成速度
            MISSING_OBJECT_HOLD = 7,//目标消失后继续跟踪时间
            MAX_MISSING_DISTANCE = 8,//目标消失后继续跟踪距离
            OBJECT_MERGE_SPEED = 9,//多个目标交错时，目标的融合速度
            REPEATED_MOTION_SUPPRESS = 10,//重复运动抑制
            ILLUMINATION_CHANGE = 11,//光影变化抑制开关
            TRACK_OUTPUT_MODE = 12,//轨迹输出模式：0-输出目标的中心，1-输出目标的底部中心
            ENTER_CHANGE_HOLD = 13,//检测区域变化阈值
            RESUME_DEFAULT_PARAM = 255,//恢复默认关键字参数
        }

        //设置/获取参数关键字
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetBehaviorParamKey(int lUserID, int lChannel, uint dwParameterKey, int nValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetBehaviorParamKey(int lUserID, int lChannel, uint dwParameterKey, ref int pValue);

        //行为分析规则DSP信息叠加结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DRAW_MODE
        {
            public uint dwSize;
            public byte byDspAddTarget;//编码是否叠加目标
            public byte byDspAddRule;//编码是否叠加规则
            public byte byDspPicAddTarget;//抓图是否叠加目标
            public byte byDspPicAddRule;//抓图是否叠加规则
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        //获取/设置行为分析目标叠加接口
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetVCADrawMode(int lUserID, int lChannel, ref tagNET_VCA_DRAW_MODE lpDrawMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVCADrawMode(int lUserID, int lChannel, ref tagNET_VCA_DRAW_MODE lpDrawMode);

        //标定点子结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_CB_POINT
        {
            public tagNET_VCA_POINT struPoint;//标定点，主摄像机（枪机）
            public NET_DVR_PTZPOS struPtzPos;//球机输入的PTZ坐标
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //标定参数配置结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_CALIBRATION_PARAM
        {
            public byte byPointNum;//有效标定点个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CALIB_PT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_CB_POINT[] struCBPoint;//标定点组
        }

        //LF双摄像机配置结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_CFG
        {
            public uint dwSize;//结构长度	
            public byte byEnable;//标定使能
            public byte byFollowChan;// 被控制的从通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public tagNET_DVR_LF_CALIBRATION_PARAM struCalParam;//标定点组
        }

        //L/F跟踪模式
        public enum TRACK_MODE
        {
            MANUAL_CTRL = 0,//手动跟踪
            ALARM_TRACK,//报警触发跟踪
            TARGET_TRACK,//目标跟踪
        }

        //L/F手动控制结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_MANUAL_CTRL_INFO
        {
            public tagNET_VCA_POINT struCtrlPoint;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //L/F目标跟踪结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_TRACK_TARGET_INFO
        {
            public uint dwTargetID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_TRACK_MODE
        {
            public uint dwSize;//结构长度
            public byte byTrackMode;//跟踪模式
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，置0
            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct uModeParam
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
                [FieldOffsetAttribute(0)]
                public uint[] dwULen;
                [FieldOffsetAttribute(0)]
                public tagNET_DVR_LF_MANUAL_CTRL_INFO struManualCtrl;//手动跟踪结构
                [FieldOffsetAttribute(0)]
                public tagNET_DVR_LF_TRACK_TARGET_INFO struTargetTrack;//目标跟踪结构
            }
        }

        //双摄像机跟踪模式设置接口
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetLFTrackMode(int lUserID, int lChannel, ref tagNET_DVR_LF_TRACK_MODE lpTrackMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetLFTrackMode(int lUserID, int lChannel, ref tagNET_DVR_LF_TRACK_MODE lpTrackMode);

        //识别场景
        public enum VCA_RECOGNIZE_SCENE
        {
            VCA_LOW_SPEED_SCENE = 0,//低速通过场景（收费站、小区门口、停车场）
            VCA_HIGH_SPEED_SCENE = 1,//高速通过场景（卡口、高速公路、移动稽查)
            VCA_MOBILE_CAMERA_SCENE = 2,//移动摄像机应用） 
        }

        //识别结果标志
        public enum VCA_RECOGNIZE_RESULT
        {
            VCA_RECOGNIZE_FAILURE = 0,//识别失败
            VCA_IMAGE_RECOGNIZE_SUCCESS,//图像识别成功
            VCA_VIDEO_RECOGNIZE_SUCCESS_OF_BEST_LICENSE,//视频识别更优结果
            VCA_VIDEO_RECOGNIZE_SUCCESS_OF_NEW_LICENSE,//视频识别到新的车牌
            VCA_VIDEO_RECOGNIZE_FINISH_OF_CUR_LICENSE,//视频识别车牌结束
        }

        //车牌颜色
        public enum VCA_PLATE_COLOR
        {
            VCA_BLUE_PLATE = 0,//蓝色车牌
            VCA_YELLOW_PLATE,//黄色车牌
            VCA_WHITE_PLATE,//白色车牌
            VCA_BLACK_PLATE,       //黑色车牌
            VCA_GREEN_PLATE,       //绿色车牌
            VCA_BKAIR_PLATE,       //民航黑色车牌
            VCA_OTHER = 0xff       //其他
        }

        //车牌类型
        public enum VCA_PLATE_TYPE
        {
            VCA_STANDARD92_PLATE = 0,	//标准民用车与军车
            VCA_STANDARD02_PLATE,		//02式民用车牌 
            VCA_WJPOLICE_PLATE,		    //武警车 
            VCA_JINGCHE_PLATE,			//警车
            STANDARD92_BACK_PLATE, 	    //民用车双行尾牌
            VCA_SHIGUAN_PLATE,          //使馆车牌
            VCA_NONGYONG_PLATE,         //农用车
            VCA_MOTO_PLATE              //摩托车
        }

        //视频识别触发类型
        public enum VCA_TRIGGER_TYPE
        {
            INTER_TRIGGER = 0,// 模块内部触发识别
            EXTER_TRIGGER = 1,// 外部物理信号触发：线圈、雷达、手动触发信号；
        }

        public const int MAX_CHINESE_CHAR_NUM = 64;    // 最大汉字类别数量
        //车牌可动态修改参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATE_PARAM
        {
            public NET_VCA_RECT struSearchRect;//搜索区域(归一化)
            public NET_VCA_RECT struInvalidateRect;//无效区域，在搜索区域内部 (归一化)
            public ushort wMinPlateWidth;//车牌最小宽度
            public ushort wTriggerDuration;//触发持续帧数
            public byte byTriggerType;//触发模式, VCA_TRIGGER_TYPE
            public byte bySensitivity;//灵敏度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，置0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byCharPriority;// 汉字优先级
        }

        /*wMinPlateWidth:该参数默认配置为80像素；该参数的配置对于车牌海康威视车牌识别说明文档 
        识别有影响，如果设置过大，那么如果场景中出现小车牌就会漏识别；如果场景中车牌宽度普遍较大，可以把该参数设置稍大，便于减少对虚假车牌的处理。在标清情况下建议设置为80， 在高清情况下建议设置为120
        wTriggerDuration － 外部触发信号持续帧数量，其含义是从触发信号开始识别的帧数量。该值在低速场景建议设置为50～100；高速场景建议设置为15～25；移动识别时如果也有外部触发，设置为15～25；具体可以根据现场情况进行配置
        */
        //车牌识别参数子结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATEINFO
        {
            public VCA_RECOGNIZE_SCENE eRecogniseScene;//识别场景(低速和高速)
            public tagNET_VCA_PLATE_PARAM struModifyParam;//车牌可动态修改参数
        }

        //车牌识别配置参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATECFG
        {
            public uint dwSize;
            public byte byPicProType;//报警时图片处理方式 0-不处理 1-上传
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，设置为0
            public NET_DVR_JPEGPARA struPictureParam;//图片规格结构
            public tagNET_VCA_PLATEINFO struPlateInfo;//车牌信息
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            public NET_DVR_HANDLEEXCEPTION_V30 struHandleType;//处理方式
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//报警触发的录象通道,为1表示触发该通道
        }

        //车牌识别结果子结构
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_VCA_PLATE_INFO
        {
            public VCA_RECOGNIZE_RESULT eResultFlag;//识别结果标志 
            public VCA_PLATE_TYPE ePlateType;//车牌类型
            public VCA_PLATE_COLOR ePlateColor;//车牌颜色
            public NET_VCA_RECT struPlateRect;//车牌位置
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;//保留，设置为0 
            public uint dwLicenseLen;//车牌长度
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public string sLicense;//车牌号码 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public string sBelieve;//各个识别字符的置信度，如检测到车牌"浙A12345", 置信度为10,20,30,40,50,60,70，则表示"浙"字正确的可能性只有10%，"A"字的正确的可能性是20%
        }

        //车牌检测结果
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATE_RESULT
        {
            public uint dwSize;//结构长度
            public uint dwRelativeTime;//相对时标
            public uint dwAbsTime;//绝对时标
            public byte byPlateNum;//车牌个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PLATE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_PLATE_INFO[] struPlateInfo;//车牌信息结构
            public uint dwPicDataLen;//返回图片的长度 为0表示没有图片，大于0表示该结构后面紧跟图片数据
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes2;//保留，设置为0 图片的高宽
            public System.IntPtr pImage;//指向图片的指针
        }

        //分析仪行为分析规则结构
        //警戒规则结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ONE_RULE_
        {
            public byte byActive;/* 是否激活规则,0-否, 非0-是 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留，设置为0字段
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//规则名称
            public VCA_EVENT_TYPE dwEventType;//行为分析事件类型
            public tagNET_VCA_EVENT_UNION uEventParam;//行为分析事件参数
            public tagNET_VCA_SIZE_FILTER struSizeFilter;//尺寸过滤器
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 68, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;/*保留，设置为0*/
        }

        // 分析仪规则结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_RULECFG
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_ONE_RULE_[] struRule; //规则数组
        }

        // IVMS行为分析配置结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_BEHAVIORCFG
        {
            public uint dwSize;
            public byte byPicProType;//报警时图片处理方式 0-不处理 非0-上传
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPicParam;//图片规格结构
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_RULECFG[] struRuleCfg;//每个时间段对应规则
        }

        //智能分析仪取流计划子结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_DEVSCHED
        {
            public NET_DVR_SCHEDTIME struTime;//时间参数
            public NET_DVR_PU_STREAM_CFG struPUStream;//前端取流参数
        }

        //智能分析仪参数配置结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_STREAMCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_DEVSCHED[] struDevSched;//按时间段配置前端取流以及规则信息
        }

        //屏蔽区域
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_MASK_REGION
        {
            public byte byEnable;//是否激活, 0-否，非0-是
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，置0
            public tagNET_VCA_POLYGON struPolygon;//屏蔽多边形
        }

        //屏蔽区域链表结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_MASK_REGION_LIST
        {
            public uint dwSize;//结构长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留，置0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_MASK_REGION_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_MASK_REGION[] struMask;//屏蔽区域数组
        }

        //ATM进入区域参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_ENTER_REGION
        {
            public uint dwSize;
            public byte byEnable;//是否激活，0-否，非0-是
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public tagNET_VCA_POLYGON struPolygon;//进入区域
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //	重启智能库
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_VCA_RestartLib(int lUserID, int lChannel);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE_SEGMENT
        {
            public tagNET_VCA_POINT struStartPoint;//表示高度线时，表示头部点
            public tagNET_VCA_POINT struEndPoint;//表示高度线时，表示脚部点
            public float fValue;//高度值，单位米
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //标定线链表
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE_SEG_LIST
        {
            public uint dwSize;//结构长度
            public byte bySegNum;//标定线条数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;//保留，置0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SEGMENT_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_LINE_SEGMENT[] struSeg;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetRealHeight(int lUserID, int lChannel, ref tagNET_VCA_LINE lpLine, ref Single lpHeight);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetRealLength(int lUserID, int lChannel, ref tagNET_VCA_LINE lpLine, ref Single lpLength);

        //IVMS屏蔽区域链表
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_MASK_REGION_LIST
        {
            public uint dwSize;//结构长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_MASK_REGION_LIST[] struList;
        }

        //IVMS的ATM进入区域参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ENTER_REGION
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_ENTER_REGION[] struEnter;//进入区域
        }

        // ivms 报警图片上传结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ALARM_JPEG
        {
            public byte byPicProType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPicParam;
        }

        // IVMS 后检索配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_SEARCHCFG
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DEC_REMOTE_PLAY struRemotePlay;// 远程回放
            public tagNET_IVMS_ALARM_JPEG struAlarmJpeg;// 报警上传图片配置
            public tagNET_IVMS_RULECFG struRuleCfg;//IVMS 行为规则配置
        }

        //2009-7-22
        public const int NET_DVR_GET_AP_INFO_LIST = 305;//获取无线网络资源参数
        public const int NET_DVR_SET_WIFI_CFG = 306;//设置IP监控设备无线参数
        public const int NET_DVR_GET_WIFI_CFG = 307;//获取IP监控设备无线参数
        public const int NET_DVR_SET_WIFI_WORKMODE = 308;//设置IP监控设备网口工作模式参数
        public const int NET_DVR_GET_WIFI_WORKMODE = 309;//获取IP监控设备网口工作模式参数

        //public const int IW_ESSID_MAX_SIZE = 32;
        public const int WIFI_WEP_MAX_KEY_COUNT = 4;
        public const int WIFI_WEP_MAX_KEY_LENGTH = 33;
        public const int WIFI_WPA_PSK_MAX_KEY_LENGTH = 63;
        public const int WIFI_WPA_PSK_MIN_KEY_LENGTH = 8;
        public const int WIFI_MAX_AP_COUNT = 20;

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_AP_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = IW_ESSID_MAX_SIZE)]
            public string sSsid;
            public uint dwMode;/* 0 mange 模式;1 ad-hoc模式，参见NICMODE */
            public uint dwSecurity;  /*0 不加密；1 wep加密；2 wpa-psk;3 wpa-Enterprise，参见WIFISECURITY*/
            public uint dwChannel;/*1-11表示11个通道*/
            public uint dwSignalStrength;/*0-100信号由最弱变为最强*/
            public uint dwSpeed;/*速率,单位是0.01mbps*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_AP_INFO_LIST
        {
            public uint dwSize;
            public uint dwCount;/*无线AP数量，不超过20*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = WIFI_MAX_AP_COUNT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_AP_INFO[] struApInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_WIFIETHERNET
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpAddress;/*IP地址*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpMask;/*掩码*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/*物理地址，只用来显示*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] bRes;
            public uint dwEnableDhcp;/*是否启动dhcp  0不启动 1启动*/
            public uint dwAutoDns;/*如果启动dhcp是否自动获取dns,0不自动获取 1自动获取；对于有线如果启动dhcp目前自动获取dns*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sFirstDns; /*第一个dns域名*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sSecondDns;/*第二个dns域名*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sGatewayIpAddr;/* 网关地址*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] bRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR__WIFI_CFG_EX
        {
            public tagNET_DVR_WIFIETHERNET struEtherNet;/*wifi网口*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = IW_ESSID_MAX_SIZE)]
            public string sEssid;/*SSID*/
            public uint dwMode;/* 0 mange 模式;1 ad-hoc模式，参见*/
            public uint dwSecurity;/*0 不加密；1 wep加密；2 wpa-psk; */
            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct key
            {
                [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
                public struct wep
                {
                    public uint dwAuthentication;/*0 -开放式 1-共享式*/
                    public uint dwKeyLength;/* 0 -64位；1- 128位；2-152位*/
                    public uint dwKeyType;/*0 16进制;1 ASCI */
                    public uint dwActive;/*0 索引：0---3表示用哪一个密钥*/
                    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = WIFI_WEP_MAX_KEY_COUNT * WIFI_WEP_MAX_KEY_LENGTH)]
                    public string sKeyInfo;
                }

                [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
                public struct wpa_psk
                {
                    public uint dwKeyLength;/*8-63个ASCII字符*/
                    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = WIFI_WPA_PSK_MAX_KEY_LENGTH)]
                    public string sKeyInfo;
                    public byte sRes;
                }
            }
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_WIFI_CFG
        {
            public uint dwSize;
            public tagNET_DVR__WIFI_CFG_EX struWifiCfg;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_WIFI_WORKMODE
        {
            public uint dwSize;
            public uint dwNetworkInterfaceMode;/*0 自动切换模式　1 有线模式*/
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveRealData_V30(int lRealHandle, uint dwTransType, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EncodeG711Frame(uint iType, ref byte pInBuffer, ref byte pOutBuffer);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecodeG711Frame(uint iType, ref byte pInBuffer, ref byte pOutBuffer);

        //2009-7-22 end

        //SDK 9000_1.1
        //网络硬盘结构配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_SINGLE_NET_DISK_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public NET_DVR_IPADDR struNetDiskAddr;//网络硬盘地址
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PATHNAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDirectory;// PATHNAME_LEN = 128
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 68, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//保留
        }

        public const int MAX_NET_DISK = 16;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_NET_DISKCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NET_DISK, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_SINGLE_NET_DISK_INFO[] struNetDiskParam;
        }

        //事件类型
        //主类型
        public enum MAIN_EVENT_TYPE
        {
            EVENT_MOT_DET = 0,//移动侦测
            EVENT_ALARM_IN = 1,//报警输入
            EVENT_VCA_BEHAVIOR = 2,//行为分析
        }

        //行为分析主类型对应的此类型， 0xffff表示全部
        public enum BEHAVIOR_MINOR_TYPE
        {
            EVENT_TRAVERSE_PLANE = 0,// 穿越警戒面,
            EVENT_ENTER_AREA,//目标进入区域,支持区域规则
            EVENT_EXIT_AREA,//目标离开区域,支持区域规则
            EVENT_INTRUSION,// 周界入侵,支持区域规则
            EVENT_LOITER,//徘徊,支持区域规则
            EVENT_LEFT_TAKE,//丢包捡包,支持区域规则
            EVENT_PARKING,//停车,支持区域规则
            EVENT_RUN,//奔跑,支持区域规则
            EVENT_HIGH_DENSITY,//区域内人员密度,支持区域规则
            EVENT_STICK_UP,//贴纸条,支持区域规则
            EVENT_INSTALL_SCANNER,//安装读卡器,支持区域规则
        }

        //事件搜索条件 200-04-07 9000_1.1
        public const int SEARCH_EVENT_INFO_LEN = 300;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        //报警输入
        public struct struAlarmParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInNo;//报警输入号，byAlarmInNo[0]若置1则表示查找由报警输入1触发的事件
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN - MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byAlarmInNo = new byte[MAX_ALARMIN_V30];
                byRes = new byte[SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30];
            }
        }

        //移动侦测
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struMotionParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotDetChanNo;//移动侦测通道，byMotDetChanNo[0]若置1则表示查找由通道1发生移动侦测触发的事件
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byMotDetChanNo = new byte[MAX_CHANNUM_V30];
                byRes = new byte[SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30];
            }
        }

        //行为分析
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struVcaParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChanNo;//触发事件的通道
            public byte byRuleID;//规则ID，0xff表示全部
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 43, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留

            public void init()
            {
                byChanNo = new byte[MAX_CHANNUM_V30];
                byRes1 = new byte[43];
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct uSeniorParam
        {
            //             [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN, ArraySubType = UnmanagedType.I1)]
            //             public byte[] byLen;
            [FieldOffset(0)]
            public struMotionParam struMotionPara;
            [FieldOffset(0)]
            public struAlarmParam struAlarmPara;

            //             public struVcaParam struVcaPara;

            public void init()
            {
                //                 byLen = new byte[SEARCH_EVENT_INFO_LEN];
                struAlarmPara = new struAlarmParam();
                struAlarmPara.init();
                //                 struMotionPara = new struMotionParam();
                //                 struMotionPara.init();
                //                 struVcaPara = new struVcaParam();
                //                 struVcaPara.init();
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_SEARCH_EVENT_PARAM
        {
            public ushort wMajorType;//0-移动侦测，1-报警输入, 2-智能事件
            public ushort wMinorType;//搜索次类型- 根据主类型变化，0xffff表示全部
            public NET_DVR_TIME struStartTime;//搜索的开始时间，停止时间: 同时为(0, 0) 表示从最早的时间开始，到最后，最前面的4000个事件
            public NET_DVR_TIME struEndTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 132, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
            public uSeniorParam uSeniorPara;

            public void init()
            {
                byRes = new byte[132];
                uSeniorPara = new uSeniorParam();
                uSeniorPara.init();
            }
        }

        //报警输入结果
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struAlarmRet
        {
            public uint dwAlarmInNo;//报警输入号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byRes = new byte[SEARCH_EVENT_INFO_LEN];
            }
        }
        //移动侦测结果
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struMotionRet
        {
            public uint dwMotDetNo;//移动侦测通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byRes = new byte[SEARCH_EVENT_INFO_LEN];
            }
        }
        //行为分析结果 
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struVcaRet
        {
            public uint dwChanNo;//触发事件的通道号
            public byte byRuleID;//规则ID
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//规则名称
            public tagNET_VCA_EVENT_UNION uEvent;//行为事件参数，wMinorType = VCA_EVENT_TYPE决定事件类型

            public void init()
            {
                byRes1 = new byte[3];
                byRuleName = new byte[NAME_LEN];
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct uSeniorRet
        {
            [FieldOffset(0)]
            public struAlarmRet struAlarmRe;
            [FieldOffset(0)]
            public struMotionRet struMotionRe;
            //             public struVcaRet struVcaRe;

            public void init()
            {
                struAlarmRe = new struAlarmRet();
                struAlarmRe.init();
                //                 struVcaRe = new struVcaRet();
                //                 struVcaRe.init();
            }
        }
        //查找返回结果
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_SEARCH_EVENT_RET
        {
            public ushort wMajorType;//主类型MA
            public ushort wMinorType;//次类型
            public NET_DVR_TIME struStartTime;//事件开始的时间
            public NET_DVR_TIME struEndTime;//事件停止的时间，脉冲事件时和开始时间一样
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public uSeniorRet uSeniorRe;

            public void init()
            {
                byChan = new byte[MAX_CHANNUM_V30];
                byRes = new byte[36];
                uSeniorRe = new uSeniorRet();
                uSeniorRe.init();
            }
        }


        //邮件服务测试 9000_1.1
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EmailTest(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFileByEvent(int lUserID, ref tagNET_DVR_SEARCH_EVENT_PARAM lpSearchEventParam);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextEvent(int lSearchHandle, ref tagNET_DVR_SEARCH_EVENT_RET lpSearchEventRet);


        //2009-8-18 抓拍机
        public const int PLATE_INFO_LEN = 1024;
        public const int PLATE_NUM_LEN = 16;
        public const int FILE_NAME_LEN = 256;

        // 车牌颜色
        public enum Anonymous_26594f67_851c_4f7d_bec4_094765b7ff83
        {
            BLUE_PLATE, // 蓝色车牌 
            YELLOW_PLATE, // 黄色车牌
            WHITE_PLATE,// 白色车牌
            BLACK_PLATE,// 黑色车牌
        }

        //liscense plate result
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PLATE_RET
        {
            public uint dwSize;//结构长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PLATE_NUM_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPlateNum;//车牌号
            public byte byVehicleType;// 车类型
            public byte byTrafficLight;//0-绿灯；1-红灯
            public byte byPlateColor;//车牌颜色
            public byte byDriveChan;//触发车道号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byTimeInfo;/*时间信息*///plate_172.6.113.64_20090724155526948_197170484 
            //目前是17位，精确到ms:20090724155526948
            public byte byCarSpeed;/*单位km/h*/
            public byte byCarSpeedH;/*cm/s高8位*/
            public byte byCarSpeedL;/*cm/s低8位*/
            public byte byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PLATE_INFO_LEN - 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byInfo;
            public uint dwPicLen;
        }
        /*注：后面紧跟 dwPicLen 长度的 图片 信息*/

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_INVOKE_PLATE_RECOGNIZE(int lUserID, int lChannel, string pPicFileName, ref tagNET_DVR_PLATE_RET pPlateRet, string pPicBuf, uint dwPicBufLen);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_CCD_CFG
        {
            public uint dwSize;//结构长度
            public byte byBlc;/*背光补偿0-off; 1-on*/
            public byte byBlcMode;/*blc类型0-自定义1-上；2-下；3-左；4-右；5-中；注：此项在blc为 on 时才起效*/
            public byte byAwb;/*自动白平衡0-自动1; 1-自动2; 2-自动控制*/
            public byte byAgc;/*自动增益0-关; 1-低; 2-中; 3-高*/
            public byte byDayNight;/*日夜转换；0 彩色；1黑白；2自动*/
            public byte byMirror;/*镜像0-关;1-左右;2-上下;3-中心*/
            public byte byShutter;/*快门0-自动; 1-1/25; 2-1/50; 3-1/100; 4-1/250;5-1/500; 6-1/1k ;7-1/2k; 8-1/4k; 9-1/10k; 10-1/100k;*/
            public byte byIrCutTime;/*IRCUT切换时间，5, 10, 15, 20, 25*/
            public byte byLensType;/*镜头类型0-电子光圈; 1-自动光圈*/
            public byte byEnVideoTrig;/*视频触发使能：1-支持；0-不支持。视频触发模式下视频快门速度按照byShutter速度，抓拍图片的快门速度按照byCapShutter速度，抓拍完成后会自动调节回视频模式*/
            public byte byCapShutter;/*抓拍时的快门速度，1-1/25; 2-1/50; 3-1/100; 4-1/250;5-1/500; 6-1/1k ;7-1/2k; 8-1/4k; 9-1/10k; 10-1/100k; 11-1/150; 12-1/200*/
            public byte byEnRecognise;/*1-支持识别；0-不支持识别*/
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetCCDCfg(int lUserID, int lChannel, ref tagNET_DVR_CCD_CFG lpCCDCfg);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetCCDCfg(int lUserID, int lChannel, ref tagNET_DVR_CCD_CFG lpCCDCfg);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagCAMERAPARAMCFG
        {
            public uint dwSize;
            public uint dwPowerLineFrequencyMode;/*0-50HZ; 1-60HZ*/
            public uint dwWhiteBalanceMode;/*0手动白平衡; 1自动白平衡1（范围小）; 2 自动白平衡2（范围宽，2200K-15000K）;3自动控制3*/
            public uint dwWhiteBalanceModeRGain;/*手动白平衡时有效，手动白平衡 R增益*/
            public uint dwWhiteBalanceModeBGain;/*手动白平衡时有效，手动白平衡 B增益*/
            public uint dwExposureMode;/*0 手动曝光 1自动曝光*/
            public uint dwExposureSet;/* 0-USERSET, 1-自动x2，2-自动4，3-自动81/25, 4-1/50, 5-1/100, 6-1/250, 7-1/500, 8-1/750, 9-1/1000, 10-1/2000, 11-1/4000,12-1/10,000; 13-1/100,000*/
            public uint dwExposureUserSet;/* 自动自定义曝光时间*/
            public uint dwExposureTarget;/*手动曝光时间 范围（Manumal有效，微秒）*/
            public uint dwIrisMode;/*0 自动光圈 1手动光圈*/
            public uint dwGainLevel;/*增益：0-100*/
            public uint dwBrightnessLevel;/*0-100*/
            public uint dwContrastLevel;/*0-100*/
            public uint dwSharpnessLevel;/*0-100*/
            public uint dwSaturationLevel;/*0-100*/
            public uint dwHueLevel;/*0-100，（保留）*/
            public uint dwGammaCorrectionEnabled;/*0 dsibale  1 enable*/
            public uint dwGammaCorrectionLevel;/*0-100*/
            public uint dwWDREnabled;/*宽动态：0 dsibale  1 enable*/
            public uint dwWDRLevel1;/*0-F*/
            public uint dwWDRLevel2;/*0-F*/
            public uint dwWDRContrastLevel;/*0-100*/
            public uint dwDayNightFilterType;/*日夜切换：0 day,1 night,2 auto */
            public uint dwSwitchScheduleEnabled;/*0 dsibale  1 enable,(保留)*/
            //模式1(保留)
            public uint dwBeginTime;	/*0-100*/
            public uint dwEndTime;/*0-100*/
            //模式2
            public uint dwDayToNightFilterLevel;//0-7
            public uint dwNightToDayFilterLevel;//0-7
            public uint dwDayNightFilterTime;//(60秒)
            public uint dwBacklightMode;/*背光补偿:0 USERSET 1 UP、2 DOWN、3 LEFT、4 RIGHT、5MIDDLE*/
            public uint dwPositionX1;//（X坐标1）
            public uint dwPositionY1;//（Y坐标1）
            public uint dwPositionX2;//（X坐标2）
            public uint dwPositionY2;//（Y坐标2）
            public uint dwBacklightLevel;/*0x0-0xF*/
            public uint dwDigitalNoiseRemoveEnable; /*数字去噪：0 dsibale  1 enable*/
            public uint dwDigitalNoiseRemoveLevel;/*0x0-0xF*/
            public uint dwMirror; /* 镜像：0 Left;1 Right,;2 Up;3Down */
            public uint dwDigitalZoom;/*数字缩放:0 dsibale  1 enable*/
            public uint dwDeadPixelDetect;/*坏点检测,0 dsibale  1 enable*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        public const int NET_DVR_GET_CCDPARAMCFG = 1067;       //IPC获取CCD参数配置
        public const int NET_DVR_SET_CCDPARAMCFG = 1068;      //IPC设置CCD参数配置

        //图像增强仪
        //图像增强去燥区域配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGEREGION
        {
            public uint dwSize;//总的结构长度
            public ushort wImageRegionTopLeftX;/* 图像增强去燥的左上x坐标 */
            public ushort wImageRegionTopLeftY;/* 图像增强去燥的左上y坐标 */
            public ushort wImageRegionWidth;/* 图像增强去燥区域的宽 */
            public ushort wImageRegionHeight;/*图像增强去燥区域的高*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //图像增强、去噪级别及稳定性使能配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGESUBPARAM
        {
            public NET_DVR_SCHEDTIME struImageStatusTime;//图像状态时间段
            public byte byImageEnhancementLevel;//图像增强的级别，0-7，0表示关闭
            public byte byImageDenoiseLevel;//图像去噪的级别，0-7，0表示关闭
            public byte byImageStableEnable;//图像稳定性使能，0表示关闭，1表示打开
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_IMAGEREGION = 1062;       //图像增强仪图像增强去燥区域获取
        public const int NET_DVR_SET_IMAGEREGION = 1063;       //图像增强仪图像增强去燥区域获取
        public const int NET_DVR_GET_IMAGEPARAM = 1064;       // 图像增强仪图像参数(去噪、增强级别，稳定性使能)获取
        public const int NET_DVR_SET_IMAGEPARAM = 1065;       // 图像增强仪图像参数(去噪、增强级别，稳定性使能)设置

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGEPARAM
        {
            public uint dwSize;
            //图像增强时间段参数配置，周日开始	
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagIMAGESUBPARAM[] struImageParamSched;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetParamSetMode(int lUserID, ref uint dwParamSetMode);

        public struct NET_DVR_CLIENTINFO
        {
            public Int32 lChannel;//通道号
            public Int32 lLinkMode;//最高位(31)为0表示主码流，为1表示子码流，0－30位表示码流连接方式: 0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-音视频分开(TCP)
            public IntPtr hPlayWnd;//播放窗口的句柄,为NULL表示不播放图象
            public string sMultiCastIP;//多播组地址
        }

        public struct NET_SDK_CLIENTINFO
        {
            public Int32 lChannel;//通道号
            public Int32 lLinkType; //连接sdk的方式，是否通过流媒体的标志
            public Int32 lLinkMode;//最高位(31)为0表示主码流，为1表示子码流，0－30位表示码流连接方式: 0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-音视频分开(TCP)
            public IntPtr hPlayWnd;//播放窗口的句柄,为NULL表示不播放图象
            public string sMultiCastIP;//多播组地址
            public Int32 iMediaSrvNum;
            public System.IntPtr pMediaSrvDir;
        }

        /*********************************************************
        Function:	NET_DVR_Login_V30
        Desc:		
        Input:	sDVRIP [in] 设备IP地址 
                wServerPort [in] 设备端口号 
                sUserName [in] 登录的用户名 
                sPassword [in] 用户密码 
        Output:	lpDeviceInfo [out] 设备信息 
        Return:	-1表示失败，其他值表示返回的用户ID值
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_Login_V30(string sDVRIP, Int32 wDVRPort, string sUserName, string sPassword, ref NET_DVR_DEVICEINFO_V30 lpDeviceInfo);

        /*********************************************************
        Function:	NET_DVR_Logout_V30
        Desc:		用户注册设备。
        Input:	lUserID [in] 用户ID号
        Output:	
        Return:	TRUE表示成功，FALSE表示失败
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Logout_V30(Int32 lUserID);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SNAPCFG
        {
            public uint dwSize;
            public byte byRelatedDriveWay;
            public byte bySnapTimes;
            public ushort wSnapWaitTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U2)]
            public ushort[] wIntervalTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*********************************************************
        Function:	NET_DVR_ContinuousShoot
        Desc:		手动触发连拍。
        Input:	    lUserID [in] 用户ID号
                    lpInter [in] 手动连拍参数结构
        Output:	
        Return:	TRUE表示成功，FALSE表示失败
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ContinuousShoot(Int32 lUserID, ref NET_DVR_SNAPCFG lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ManualSnap(Int32 lUserID, ref NET_DVR_MANUALSNAP lpInter, ref NET_DVR_PLATE_RESULT lpOuter);

        #region  取流模块相关结构与接口

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct PLAY_INFO
        {
            public int iUserID;      //注册用户ID
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string strDeviceIP;
            public int iDevicePort;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string strDevAdmin;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string strDevPsd;
            public int iChannel;      //播放通道号(从0开始)
            public int iLinkMode;   //最高位(31)为0表示主码流，为1表示子码流，0－30位表示码流连接方式: 0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-音视频分开(TCP)
            public bool bUseMedia;     //是否启用流媒体
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string strMediaIP; //流媒体IP地址
            public int iMediaPort;   //流媒体端口号
        }


        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_Init();

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_UnInit();


        [DllImport("GetStream.dll")]
        public static extern int CLIENT_SDK_GetStream(PLAY_INFO lpPlayInfo); //

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SetRealDataCallBack(int iRealHandle, SETREALDATACALLBACK fRealDataCallBack, uint lUser); //

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_StopStream(int iRealHandle);

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_GetVideoEffect(int iRealHandle, ref int iBrightValue, ref int iContrastValue, ref int iSaturationValue, ref int iHueValue);

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_SetVideoEffect(int iRealHandle, int iBrightValue, int iContrastValue, int iSaturationValue, int iHueValue);

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_MakeKeyFrame(int iRealHandle);

        #endregion


        #region VOD点播放库

        public const int WM_NETERROR = 0x0400 + 102;          //网络异常消息
        public const int WM_STREAMEND = 0x0400 + 103;		  //文件播放结束

        public const int FILE_HEAD = 0;      //文件头
        public const int VIDEO_I_FRAME = 1;  //视频I帧
        public const int VIDEO_B_FRAME = 2;  //视频B帧
        public const int VIDEO_P_FRAME = 3;  //视频P帧
        public const int VIDEO_BP_FRAME = 4; //视频BP帧
        public const int VIDEO_BBP_FRAME = 5; //视频B帧B帧P帧
        public const int AUDIO_PACKET = 10;   //音频包

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct BLOCKTIME
        {
            public ushort wYear;
            public byte bMonth;
            public byte bDay;
            public byte bHour;
            public byte bMinute;
            public byte bSecond;
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct VODSEARCHPARAM
        {
            public IntPtr sessionHandle;                                    //[in]VOD客户端句柄
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string dvrIP;                                            //	[in]DVR的网络地址
            public uint dvrPort;                                            //	[in]DVR的端口地址
            public uint channelNum;                                         //  [in]DVR的通道号
            public BLOCKTIME startTime;                                     //	[in]查询的开始时间
            public BLOCKTIME stopTime;                                      //	[in]查询的结束时间
            public bool bUseIPServer;                                       //  [in]是否使用IPServer 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string SerialNumber;                                     //  [in]设备的序列号
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SECTIONLIST
        {
            public BLOCKTIME startTime;
            public BLOCKTIME stopTime;
            public byte byRecType;
            public IntPtr pNext;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct VODOPENPARAM
        {
            public IntPtr sessionHandle;                                    //[in]VOD客户端句柄
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string dvrIP;                                            //	[in]DVR的网络地址
            public uint dvrPort;                                            //	[in]DVR的端口地址
            public uint channelNum;                                         //  [in]DVR的通道号
            public BLOCKTIME startTime;                                     //	[in]查询的开始时间
            public BLOCKTIME stopTime;                                      //	[in]查询的结束时间
            public uint uiUser;
            public bool bUseIPServer;                                       //  [in]是否使用IPServer 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string SerialNumber;                                     //  [in]设备的序列号

            public VodStreamFrameData streamFrameData;
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CONNPARAM
        {
            public uint uiUser;
            public ErrorCallback errorCB;
        }


        // 异常回调函数
        public delegate void ErrorCallback(System.IntPtr hSession, uint dwUser, int lErrorType);
        //帧数据回调函数
        public delegate void VodStreamFrameData(System.IntPtr hStream, uint dwUser, int lFrameType, System.IntPtr pBuffer, uint dwSize);

        //模块初始化
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODServerConnect(string strServerIp, uint uiServerPort, ref IntPtr hSession, ref CONNPARAM struConn, IntPtr hWnd);

        //模块销毁
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODServerDisconnect(IntPtr hSession);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStreamSearch(IntPtr pSearchParam, ref IntPtr pSecList);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODDeleteSectionList(IntPtr pSecList);

        // 根据ID、时间段打开流获取流句柄
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODOpenStream(IntPtr pOpenParam, ref IntPtr phStream);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODCloseStream(IntPtr hStream);

        //根据ID、时间段打开批量下载
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODOpenDownloadStream(ref VODOPENPARAM struVodParam, ref IntPtr phStream);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODCloseDownloadStream(IntPtr hStream);

        // 开始流解析，发送数据帧
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStartStreamData(IntPtr phStream);
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODPauseStreamData(IntPtr hStream, bool bPause);
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStopStreamData(IntPtr hStream);

        // 根据时间定位
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODSeekStreamData(IntPtr hStream, IntPtr pStartTime);


        // 根据时间定位
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODSetStreamSpeed(IntPtr hStream, int iSpeed);

        // 根据时间定位
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODGetStreamCurrentTime(IntPtr hStream, ref BLOCKTIME pCurrentTime);

        #endregion


        #region 帧分析库


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct PACKET_INFO
        {
            public int nPacketType;     // packet type
            // 0:  file head
            // 1:  video I frame
            // 2:  video B frame
            // 3:  video P frame
            // 10: audio frame
            // 11: private frame only for PS


            //      [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)]
            public IntPtr pPacketBuffer;
            public uint dwPacketSize;
            public int nYear;
            public int nMonth;
            public int nDay;
            public int nHour;
            public int nMinute;
            public int nSecond;
            public uint dwTimeStamp;
        }



        /******************************************************************************
        * function：get a empty port number
        * parameters：
        * return： 0 - 499 : empty port number
        *          -1      : server is full  			
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern int AnalyzeDataGetSafeHandle();


        /******************************************************************************
        * function：open standard stream data for analyzing
        * parameters：lHandle - working port number
        *             pHeader - pointer to file header or info header
        * return：TRUE or FALSE
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataOpenStreamEx(int iHandle, byte[] pFileHead);


        /******************************************************************************
        * function：close analyzing
        * parameters：lHandle - working port number
        * return：
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataClose(int iHandle);


        /******************************************************************************
        * function：input stream data
        * parameters：lHandle		- working port number
        *			  pBuffer		- data pointer
        *			  dwBuffersize	- data size
        * return：TRUE or FALSE
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataInputData(int iHandle, IntPtr pBuffer, uint uiSize); //byte []


        /******************************************************************************
        * function：get analyzed packet
        * parameters：lHandle		- working port number
        *			  pPacketInfo	- returned structure
        * return：-1 : error
        *          0 : succeed
        *		   1 : failed
        *		   2 : file end (only in file mode)				
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern int AnalyzeDataGetPacket(int iHandle, ref PACKET_INFO pPacketInfo);  //要把pPacketInfo转换成PACKET_INFO结构


        /******************************************************************************
        * function：get remain data from input buffer
        * parameters：lHandle		- working port number
        *			  pBuf	        - pointer to the mem which stored remain data
        *             dwSize        - size of remain data  
        * return： TRUE or FALSE				
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataGetTail(int iHandle, ref IntPtr pBuffer, ref uint uiSize);


        [DllImport("AnalyzeData.dll")]
        public static extern uint AnalyzeDataGetLastError(int iHandle);

        #endregion


        #region 录像库

        public const int DATASTREAM_HEAD = 0;		//数据头
        public const int DATASTREAM_BITBLOCK = 1;		//字节数据
        public const int DATASTREAM_KEYFRAME = 2;		//关键帧数据
        public const int DATASTREAM_NORMALFRAME = 3;		//非关键帧数据


        public const int MESSAGEVALUE_DISKFULL = 0x01;
        public const int MESSAGEVALUE_SWITCHDISK = 0x02;
        public const int MESSAGEVALUE_CREATEFILE = 0x03;
        public const int MESSAGEVALUE_DELETEFILE = 0x04;
        public const int MESSAGEVALUE_SWITCHFILE = 0x05;




        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct STOREINFO
        {
            public int iMaxChannels;
            public int iDiskGroup;
            public int iStreamType;
            public bool bAnalyze;
            public bool bCycWrite;
            public uint uiFileSize;

            public CALLBACKFUN_MESSAGE funCallback;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CREATEFILE_INFO
        {
            public int iHandle;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strCameraid;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strFileName;

            public BLOCKTIME tFileCreateTime;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CLOSEFILE_INFO
        {
            public int iHandle;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strCameraid;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strFileName;

            public BLOCKTIME tFileSwitchTime;
        }



        public delegate int CALLBACKFUN_MESSAGE(int iMessageType, System.IntPtr pBuf, int iBufLen);


        [DllImport("RecordDLL.dll")]
        public static extern int Initialize(STOREINFO struStoreInfo);

        [DllImport("RecordDLL.dll")]
        public static extern int Release();

        [DllImport("RecordDLL.dll")]
        public static extern int OpenChannelRecord(string strCameraid, IntPtr pHead, uint dwHeadLength);

        [DllImport("RecordDLL.dll")]
        public static extern bool CloseChannelRecord(int iRecordHandle);

        [DllImport("RecordDLL.dll")]
        public static extern int GetData(int iHandle, int iDataType, IntPtr pBuf, uint uiSize);

        #endregion

        //设备区域设置
        public const int REGIONTYPE = 0;//代表区域
        public const int MATRIXTYPE = 11;//矩阵节点
        public const int DEVICETYPE = 2;//代表设备
        public const int CHANNELTYPE = 3;//代表通道
        public const int USERTYPE = 5;//代表用户

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_LOG_MATRIX
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;
            public uint dwMinorType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;
            public NET_DVR_IPADDR struRemoteHostAddr;
            public uint dwParaType;
            public uint dwChannel;
            public uint dwDiskNumber;
            public uint dwAlarmInPort;
            public uint dwAlarmOutPort;
            public uint dwInfoLen;
            public byte byDevSequence;//槽位号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;//MAC地址
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = LOG_INFO_LEN - SERIALNO_LEN - MACADDR_LEN - 1)]
            public string sInfo;
        }

        //视频综合平台软件
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagVEDIOPLATLOG
        {
            public byte bySearchCondition;//搜索条件，0-按槽位号搜索，1-按序列号搜索 2-按MAC地址进行搜索
            public byte byDevSequence;//槽位号，0-79：对应子系统的槽位号；
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;//MAC地址
        }

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextLog_MATRIX(int iLogHandle, ref NET_DVR_LOG_MATRIX lpLogData);


        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindDVRLog_Matrix(int iUserID, int lSelectMode, uint dwMajorType, uint dwMinorType, ref tagVEDIOPLATLOG lpVedioPlatLog, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);




        /*************************************************
        启动远程配置宏定义 
        NET_DVR_StartRemoteConfig
        具体支持查看函数说明和代码
        **************************************************/
        public const int MAX_CARDNO_LEN = 48;
        public const int MAX_OPERATE_INDEX_LEN = 32;
        public const int NET_DVR_GET_ALL_VEHICLE_CONTROL_LIST = 3124;// 获取所有车辆黑白名单信息
        public const int NET_DVR_VEHICLE_DELINFO_CTRL = 3125; // 删除设备内黑名单数据库信息 
        public const int NET_DVR_VEHICLELIST_CTRL_START = 3133;

        /*********************************************************
        Function:	REMOTECONFIGCALLBACK
        Desc:		(回调函数)
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void REMOTECONFIGCALLBACK(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData);
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_StartRemoteConfig(Int32 lUserID, uint dwCommand, IntPtr lpInBuffer, Int32 dwInBufferLen, REMOTECONFIGCALLBACK cbStateCallback, IntPtr pUserData);

        // 建立发送长连接数据与关闭长连接配置接口所创建的句柄，释放资源
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendRemoteConfig(Int32 lHandle, uint dwDataType, IntPtr pSendBuf, uint dwBufSize);

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRemoteConfig(Int32 lHandle);

        // 逐个获取查找到的车辆数据信息。
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_GetNextRemoteConfig(Int32 lHandle, IntPtr lpOutBuff, uint dwOutBuffSize);


        // 智能交通操作类型
        public enum VCA_OPERATE_TYPE
        {
            VCA_LICENSE_TYPE = 0x1,  //车牌号码
            VCA_PLATECOLOR_TYPE = 0x2,  //车牌颜色
            VCA_CARDNO_TYPE = 0x4,  //卡号
            VCA_PLATETYPE_TYPE = 0x8,  //车牌类型
            VCA_LISTTYPE_TYPE = 0x10,  //车辆名单类型
            VCA_INDEX_TYPE = 0x20,  //数据流水号 2014-02-25
            VCA_OPERATE_INDEX_TYPE = 0x40  //操作数 2014-03-03
        }
        // NET_DVR_StartRemoteConfig CallBack 返回类型
        public enum NET_SDK_CALLBACK_TYPE
        {
            NET_SDK_CALLBACK_TYPE_STATUS = 0,
            NET_SDK_CALLBACK_TYPE_PROGRESS,
            NET_SDK_CALLBACK_TYPE_DATA
        }

        // NET_DVR_StartRemoteConfig CallBack调用设备返回的状态值
        public enum NET_SDK_CALLBACK_STATUS_NORMAL
        {
            NET_SDK_CALLBACK_STATUS_SUCCESS = 1000,		// 成功
            NET_SDK_CALLBACK_STATUS_PROCESSING,			// 处理中
            NET_SDK_CALLBACK_STATUS_FAILED,				// 失败
            NET_SDK_CALLBACK_STATUS_EXCEPTION,			// 异常
            NET_SDK_CALLBACK_STATUS_LANGUAGE_MISMATCH,	//（IPC配置文件导入）语言不匹配
            NET_SDK_CALLBACK_STATUS_DEV_TYPE_MISMATCH,	//（IPC配置文件导入）设备类型不匹配
            NET_DVR_CALLBACK_STATUS_SEND_WAIT           // 发送等待
        }

        // 数据全部获取接口 （长连接获取）
        public struct NET_DVR_VEHICLE_CONTROL_COND
        {
            public uint dwChannel;
            public uint dwOperateType;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public String sLicense;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_CARDNO_LEN)]
            public String sCardNo;
            public byte byListType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDataIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }

        // NET_DVR_GetNextRemoteConfig 设备返回状态
        public enum NET_SDK_GET_NEXT_STATUS
        {
            NET_SDK_GET_NEXT_STATUS_SUCCESS = 1000,	// 成功读取到数据，客户端处理完本次数据后需要再次调用NET_DVR_RemoteConfigGetNext获取下一条数据
            NET_SDK_GET_NETX_STATUS_NEED_WAIT,		// 需等待设备发送数据，继续调用NET_DVR_RemoteConfigGetNext函数
            NET_SDK_GET_NEXT_STATUS_FINISH,			// 数据全部取完，此时客户端可调用NET_DVR_StopRemoteConfig结束长连接
            NET_SDK_GET_NEXT_STATUS_FAILED,			// 出现异常，客户端可调用NET_DVR_StopRemoteConfig结束长连接
        }

        // 出入口黑白名单的数据同步信息结构体
        public struct tagNET_DVR_VEHICLE_CONTROL_LIST_INFO
        {
            public uint dwSize;
            public uint dwChannel;//通道号0xff - 全部通道（ITC 默认是1）
            public uint dwDataIndex;//数据流水号（平台维护的数据唯一值，客户端操作的时候，该值不会起效。该值主要用于数据增量同步）
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public String sLicense; //车牌号码
            public byte byListType;//名单属性（黑白名单）0-白名单，1-黑名单
            public byte byPlateType;	//车牌类型
            public byte byPlateColor;	//车牌颜色
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 21, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_CARDNO_LEN)]
            public String sCardNo; // 卡号
            public NET_DVR_TIME_V30 struStartTime;//有效开始时间
            public NET_DVR_TIME_V30 struStopTime;//有效结束时间
            //操作数（平台同步表流水号不会重复，用于增量更新，代表同步到同步表的某一条记录了，存在相机内存，重启后会清0）2014-03-03
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_OPERATE_INDEX_LEN)]
            public String sOperateIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 224, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1; // 保留字节
        }

        // 数据全部获取接口 （长连接获取）
        public struct tagNET_DVR_VEHICLE_CONTROL_COND
        {
            public uint dwChannel;//通道号0xffffffff - 全部通道（ITC 默认是1）
            public uint dwOperateType;//操作类型，参照VCA_OPERATE _TYPE。（可复选）
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public String sLicense; //车牌号码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_CARDNO_LEN)]
            public String sCardNo; // 卡号
            public byte byListType;//名单属性（黑白名单）0-白名单，1-黑名单，0xff-全部
            //2014-02-25
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDataIndex;//数据流水号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // 清除设备车牌黑名单数据库信息 结构体
        public struct NET_DVR_VEHICLE_CONTROL_DELINFO
        {
            public uint dwSize;
            public uint dwDelType;//删除条件类型，删除条件类型，参照VCA_OPERATE _TYPE。（可复选）
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public String sLicense; //车牌号码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public String sCardNo; // 卡号 
            public byte byPlateType;	//车牌类型
            public byte byPlateColor;	//车牌颜色
            public byte byOperateType;	//删除操作类型(0-条件删除,0xff-删除全部)
            //2014-02-25
            public byte byListType;//名单属性（黑白名单）0-白名单，1-黑名单 2014-03-03
            public uint dwDataIndex;//数据流水号 	
            //操作数（平台同步表流水号不会重复，用于增量更新，代表同步到同步表的某一条记录了，存在相机内存，重启后会清0）2014-03-03
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_OPERATE_INDEX_LEN)]
            public String sOperateIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //本地配置 报警监听
        [DllImportAttribute(@"HCNetSDK.dll")]
        unsafe public static extern bool NET_DVR_GetLocalIP(byte[] strIP, Int32* pValidNum, bool* pEnableBind);

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetValidIP(uint dwIPIndex, bool bEnableBind);
       
        // 门参数配置结构体
        public const int DOOR_NAME_LEN = 32;
        public const int STRESS_PASSWORD_LEN = 8;
        public const int SUPER_PASSWORD_LEN = 8;
        public const int UNLOCK_PASSWORD_LEN = 8;
        public const int NET_DVR_GET_DOOR_CFG = 2108; // 获取门参数
        public const int NET_DVR_SET_DOOR_CFG = 2109; // 设置门参数
        public const int COMM_ALARM_ACS = 0x5002; // 门禁主机报警
        public const int ACS_CARD_NO_LEN = 32; // 门禁卡号长度
        public const int MAX_DOOR_NUM = 32;
        public const int MAX_GROUP_NUM = 32;
        public const int MAX_CARD_RIGHT_PLAN_NUM = 4;
        public const int CARD_PASSWORD_LEN = 8;

        public struct NET_DVR_DOOR_CFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DOOR_NAME_LEN)]
            public String byDoorName;
            public byte byMagneticType;
            public byte byOpenButtonType;
            public byte byOpenDuration;
            public byte byDisabledOpenDuration;
            public byte byMagneticAlarmTimeout;
            public byte byEnableDoorLock;
            public byte byEnableLeaderCard;
            public byte byRes1;
            public uint dwLeaderCardOpenDuration;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = STRESS_PASSWORD_LEN)]
            public String byStressPassword;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = SUPER_PASSWORD_LEN)]
            public String bySuperPassword;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = UNLOCK_PASSWORD_LEN)]
            public String byUnlockPassword;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 56)]
            public String byRes2;
        }

        // 门禁主机报警信息结构体
        unsafe public struct NET_DVR_ACS_ALARM_INFO
        {
            public uint dwSize;
            public uint dwMajor;
            public uint dwMinor;
            public NET_DVR_TIME struTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;
            public NET_DVR_IPADDR struRemoteHostAddr ;
            public NET_DVR_ACS_EVENT_INFO struAcsEventInfo;
            public uint dwPicDataLen; //图片数据大小，不为0是表示后面带数据
            public void* pPicData;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        
        // 门禁主机事件信息
        public struct NET_DVR_ACS_EVENT_INFO
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //卡号，为0无效
            public byte byCardType; //卡类型，1-普通卡，2-残疾人卡，3-黑名单卡，4-巡更卡，5-胁迫卡，6-超级卡，7-来宾卡，为0无效
            public byte byWhiteListNo; //白名单单号,1-8，为0无效
            public byte byReportChannel; //报告上传通道，1-布防上传，2-中心组1上传，3-中心组2上传，为0无效
            public byte byCardReaderKind; //读卡器属于哪一类，0-无效，1-IT读卡器，2-身份证读卡器，3-二维码读卡器
            public uint dwCardReaderNo; //读卡器编号，为0无效
            public uint dwDoorNo; //门编号，为0无效
            public uint dwVerifyNo; //多重卡认证序号，为0无效
            public uint dwAlarmInNo; //报警输入号，为0无效
            public uint dwAlarmOutNo; //报警输出号，为0无效
            public uint dwCaseSensorNo; //事件触发器编号
            public uint dwRs485No; //RS485通道号，为0无效
            public uint dwMultiCardGroupNo; //群组编号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        //批量参数配置
        public const int ZERO_CHAN_INDEX = 500;
        public const int STREAM_ID_LEN = 32;
        public const int MAX_CHANNUM_V40 = 512;

        //compression parameter
        public const int NORM_HIGH_STREAM_COMPRESSION = 0; //主码流图像压缩参数,压缩能力强,性能可以更高
        public const int SUB_STREAM_COMPRESSION = 1; //子码流图像压缩参数
        public const int EVENT_INVOKE_COMPRESSION = 2; //事件触发图像压缩参数,一些参数相对固定
        public const int THIRD_STREAM_COMPRESSION = 3;  //第三码流
        public const int TRANS_STREAM_COMPRESSION = 4;  //转码码流

        public const int NET_DVR_GET_AUDIO_INPUT = 3201; //获取音频输入参数
        public const int NET_DVR_SET_AUDIO_INPUT = 3202; //设置音频输入参数
        public const int NET_DVR_GET_MULTI_STREAM_COMPRESSIONCFG = 3216; //远程获取多码流压缩参数
        public const int NET_DVR_SET_MULTI_STREAM_COMPRESSIONCFG = 3217; //远程设置多码流压缩参数

        public struct NET_DVR_VALID_PERIOD_CFG
        {
            public byte byEnable; //使能有效期，0-不使能，1使能
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_TIME_EX struBeginTime; //有效期起始时间
            public NET_DVR_TIME_EX struEndTime; //有效期结束时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        public struct NET_DVR_TIME_EX
        {
            public Int16 wYear;
            public byte byMonth;
            public byte byDay;
            public byte byHour;
            public byte byMinute;
            public byte bySecond;
            public byte byRes;
        }
        // 流信息 - 72字节长
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_INFO
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byID;
            public uint dwChannel;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //多码流压缩参数配置条件结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MULTI_STREAM_COMPRESSIONCFG_COND
        {
            public uint dwSize;
            public NET_DVR_STREAM_INFO struStreamInfo;
            public uint dwStreamType; //码流类型，0-主码流，1-子码流，2-事件类型，3-码流3，……
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //多码流压缩参数结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MULTI_STREAM_COMPRESSIONCFG
        {
            public uint dwSize;
            public uint dwStreamType; //码流类型，0-主码流，1-子码流，2-事件类型，3-码流3，……
            public NET_DVR_COMPRESSION_INFO_V30 struStreamPara; //码流压缩参数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 80, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_AUDIO_INPUT_PARAM
        {
            public byte byAudioInputType;  //音频输入类型，0-mic in，1-line in
            public byte byVolume; //volume,[0-100]
            public byte byEnableNoiseFilter; //是否开启声音过滤-关，-开
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //时间段录像参数配置(子结构)
        public struct NET_DVR_RECORDSCHED_V40
        {
            public NET_DVR_SCHEDTIME struRecordTime;
            /*录像类型，0:定时录像，1:移动侦测，2:报警录像，3:动测|报警，4:动测&报警 5:命令触发, 
            6-智能报警录像，10-PIR报警，11-无线报警，12-呼救报警，13-全部事件,14-智能交通事件, 
            15-越界侦测,16-区域入侵,17-声音异常,18-场景变更侦测,
            19-智能侦测(越界侦测|区域入侵|人脸侦测|声音异常|场景变更侦测),20－人脸侦测,21-POS录像,
            22-进入区域侦测, 23-离开区域侦测,24-徘徊侦测,25-人员聚集侦测,26-快速运动侦测,27-停车侦测,
            28-物品遗留侦测,29-物品拿取侦测,30-火点检测，31-防破坏检测*/
            public byte byRecordType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //全天录像参数配置(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORDDAY_V40
        {
            public byte byAllDayRecord;/* 是否全天录像 0-否 1-是*/
                                       /*录像类型，0:定时录像，1:移动侦测，2:报警录像，3:动测|报警，4:动测&报警 5:命令触发, 
                                       6-智能报警录像，10-PIR报警，11-无线报警，12-呼救报警，13-全部事件,14-智能交通事件, 
                                       15-越界侦测,16-区域入侵,17-声音异常,18-场景变更侦测,
                                       19-智能侦测(越界侦测|区域入侵|人脸侦测|声音异常|场景变更侦测),20－人脸侦测,21-POS录像,
                                       22-进入区域侦测, 23-离开区域侦测,24-徘徊侦测,25-人员聚集侦测,26-快速运动侦测,27-停车侦测,
                                       28-物品遗留侦测,29-物品拿取侦测,30-火点检测，31-防破坏检测*/
            public byte byRecordType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD_V40
        {
            public uint dwSize;
            public uint dwRecord; /*是否录像 0-否 1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY_V40[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED_V40[] struRecordSched;
            public uint dwRecordTime; /* 录象延时长度 0-5秒， 1-10秒， 2-30秒， 3-1分钟， 4-2分钟， 5-5分钟， 6-10分钟*/
            public uint dwPreRecordTime; /* 预录时间 0-不预录 1-5秒 2-10秒 3-15秒 4-20秒 5-25秒 6-30秒 7-0xffffffff(尽可能预录) */
            public uint dwRecorderDuration; /* 录像保存的最长时间 */
            public byte byRedundancyRec; /*是否冗余录像,重要数据双备份：0/1*/
            public byte byAudioRec; /*录像时复合流编码时是否记录音频数据：国外有此法规*/
            public byte byStreamType;  // 0-主码流，1-子码流，2-主子码流同时 3-三码流
            public byte byPassbackRecord;  // 0:不回传录像 1：回传录像
            public ushort wLockDuration;  // 录像锁定时长，单位小时 0表示不锁定，0xffff表示永久锁定，录像段的时长大于锁定的持续时长的录像，将不会锁定
            public byte byRecordBackup;  // 0:录像不存档 1：录像存档
            public byte bySVCLevel;	//SVC抽帧类型：0-不抽，1-抽二分之一 2-抽四分之三
            public byte byRecordManage;   //录像调度，0-启用， 1-不启用; 启用时进行定时录像；不启用时不进行定时录像，但是录像计划仍在使用，比如移动侦测，回传都还在按这条录像计划进行
            public byte byExtraSaveAudio;//音频单独存储
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 126, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_CARD_CFG = 2116;    //获取卡参数
        public const int NET_DVR_SET_CARD_CFG = 2117;    //设置卡参数

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG
        {
            public uint dwSize;
            public uint dwModifyParamType; 
            // 需要修改的卡参数，设置卡参数时有效，按位表示，每位代表一种参数，1为需要修改，0为不修改
            // #define CARD_PARAM_CARD_VALID       0x00000001 //卡是否有效参数
            // #define CARD_PARAM_VALID            0x00000002  //有效期参数
            // #define CARD_PARAM_CARD_TYPE        0x00000004  //卡类型参数
            // #define CARD_PARAM_DOOR_RIGHT       0x00000008  //门权限参数
            // #define CARD_PARAM_LEADER_CARD      0x00000010  //首卡参数
            // #define CARD_PARAM_SWIPE_NUM        0x00000020  //最大刷卡次数参数
            // #define CARD_PARAM_GROUP            0x00000040  //所属群组参数
            // #define CARD_PARAM_PASSWORD         0x00000080  //卡密码参数
            // #define CARD_PARAM_RIGHT_PLAN       0x00000100  //卡权限计划参数
            // #define CARD_PARAM_SWIPED_NUM       0x00000200  //已刷卡次数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //卡号
            public byte byCardValid; //卡是否有效，0-无效，1-有效（用于删除卡，设置时置为0进行删除，获取时此字段始终为1）
            public byte byCardType; //卡类型，1-普通卡，2-残疾人卡，3-黑名单卡，4-巡更卡，5-胁迫卡，6-超级卡，7-来宾卡，8-解除卡，默认普通卡
            public byte byLeaderCard; //是否为首卡，1-是，0-否
            public byte byRes1;
            public uint dwDoorRight; //门权限，按位表示，1为有权限，0为无权限，从低位到高位表示对门1-N是否有权限
            public NET_DVR_VALID_PERIOD_CFG struValid; //有效期参数
            public uint dwBelongGroup; //所属群组，按位表示，1-属于，0-不属于，从低位到高位表示是否从属群组1-N
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CARD_PASSWORD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardPassword; //卡密码
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOOR_NUM*MAX_CARD_RIGHT_PLAN_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardRightPlan; //卡权限计划，取值为计划模板编号，同个门不同计划模板采用权限或的方式处理
            public uint dwMaxSwipeTime; //最大刷卡次数，0为无次数限制
            public uint dwSwipeTime; //已刷卡次数
            public ushort wRoomNumber; //房间号 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 22, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG_COND
        {
            public uint dwSize;
            public uint dwCardNum; //设置或获取卡数量，获取时置为0xffffffff表示获取所有卡信息
            public byte  byCheckCardNo; //设备是否进行卡号校验，0-不校验，1-校验
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[]  byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG_SEND_DATA
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //卡号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        public const int NET_DVR_GET_GROUP_CFG = 2112;   //获取群组参数
        public const int NET_DVR_SET_GROUP_CFG = 2113;    //设置群组参数

        //新增结构体（群组）
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_GROUP_CFG
        {
            public uint dwSize;
            public byte byEnable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            public NET_DVR_VALID_PERIOD_CFG struValidPeriodCfg;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byGroupName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes2;

        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARM_DEVICE_USER
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] sUserName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] sPassword;
            public NET_DVR_IPADDR struUserIP;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byAMCAddr;
            public byte byUserType;
            public byte byAlarmOnRight;
            public byte byAlarmOffRight;
            public byte byBypassRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byOtherRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetPreviewRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetRecordRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetPlaybackRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetPTZRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public byte[] byRes2;

        }

        public const int NET_DVR_GET_FINGERPRINT_CFG = 2150;    //获取指纹参数
        public const int NET_DVR_SET_FINGERPRINT_CFG = 2151;    //设置指纹参数
        public const int NET_DVR_DEL_FINGERPRINT_CFG = 2152;    //删除指纹参数
        public const int MAX_FINGER_PRINT_LEN = 768;            //最大指纹长度

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_CFG
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo;  //指纹关联卡号
            public uint dwFingerPrintLen; //指纹数据长度
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCardReader; //需要下发指纹的读卡器，按数组表示，0-不下发该读卡器，1-下发到该读卡器
            public byte byFingerPrintID;     //指纹编号，有效值范围为1-10
            public byte byFingerType;       //指纹类型  0-普通指纹，1-胁迫指纹
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FINGER_PRINT_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byFingerData;        //指纹数据内容
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct  StrucTEST
        {
            public uint dwSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_STATUS
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; //指纹关联的卡号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byCardReaderRecvStatus;   //指纹读卡器状态，按数组表示
            public byte byFingerPrintID;  //指纹编号，有效值范围为1-10
            public byte byFingerType;   //指纹类型  0-普通指纹，1-胁迫指纹
            public byte byTotalStatus;  //下发总的状态，0-当前指纹未下完所有读卡器，1-已下完所有读卡器(这里的所有指的是门禁主机往所有的读卡器下发了，不管成功与否)
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 61)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_INFO_COND
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; //指纹关联的卡号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byEnableCardReader; //指纹的读卡器信息，按数组表示
            public uint dwFingerPrintNum; //设置或获取卡数量，获取时置为0xffffffff表示获取所有卡信息
            public byte byFingerPrintID;  //指纹编号，有效值范围为-10   0xff表示该卡所有指纹
            public byte byCallbackMode;   //设备回调方式，0-设备所有读卡器下完了范围，1-在时间段内下了部分也返回
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            public byte[] byRes1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_BYCARD
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; //指纹关联的卡号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byEnableCardReader; //指纹的读卡器信息，按数组表示
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] byFingerPrintID;    //需要获取的指纹信息，按数组下标，值表示0-不删除，1-删除该指纹
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 34)]
            public byte[] byRes1;

            //public void init()
            //{
            //    byCardNo = new byte[32];
            //    byEnableCardReader = new byte[512];
            //    byFingerPrintID = new byte[10];
            //    byRes1 = new byte[34];
            //}
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_BYREADER
        {
            public uint dwCardReaderNo;  //按值表示，指纹读卡器编号
            public byte byClearAllCard;  //是否删除所有卡的指纹信息，0-按卡号删除指纹信息，1-删除所有卡的指纹信息
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; //指纹关联的卡号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 548)]
            public byte[] byRes;

            //public void init()
            //{
            //    byRes1 = new byte[3];
            //    byCardNo = new byte[32];
            //    byRes = new byte[548];
            //}
        }

        //public const int DEL_FINGER_PRINT_MODE_LEN = 588; //联合体大小
        //[StructLayoutAttribute(LayoutKind.Sequential)]
        //public struct NET_DVR_DEL_FINGER_PRINT_MODE
        //{
        //    public NET_DVR_FINGER_PRINT_BYCARD struByCard;   //按卡号的方式删除
        //    public NET_DVR_FINGER_PRINT_BYREADER struByReader; //按读卡器的方式删除

        //    //public void init()
        //    //{
        //    //    struByCard = new NET_DVR_FINGER_PRINT_BYCARD();
        //    //    struByCard.init();
        //    //}
        //}

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_INFO_CTRL_BYCARD
        {
            public uint dwSize;
            public byte byMode;          //删除方式，0-按卡号方式删除，1-按读卡器删除
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;

            public NET_DVR_FINGER_PRINT_BYCARD struByCard;   //按卡号的方式删除
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_INFO_CTRL_BYREADER
        {
            public uint dwSize;
            public byte byMode;          //删除方式，0-按卡号方式删除，1-按读卡器删除
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;

            public NET_DVR_FINGER_PRINT_BYREADER struByReader; //按读卡器的方式删除
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;

        }

        public const int NET_DVR_GET_CARD_READER_CFG = 2140;      //获取读卡器参数
        public const int NET_DVR_SET_CARD_READER_CFG = 2141;      //设置读卡器参数

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_READER_CFG
        {
            public uint dwSize;
            public byte byEnable; //是否使能，1-使能，0-不使能
            public byte byCardReaderType; //读卡器类型，1-DS-K110XM/MK/C/CK，2-DS-K192AM/AMP，3-DS-K192BM/BMP，4-DS-K182AM/AMP，5-DS-K182BM/BMP，6-DS-K182AMF/ACF，7-韦根或485不在线,8- DS-K1101M/MK，9- DS-K1101C/CK，10- DS-K1102M/MK/M-A
                                   //11- DS-K1102C/CK，12- DS-K1103M/MK，13- DS-K1103C/CK，14- DS-K1104M/MK，15- DS-K1104C/CK，16- DS-K1102S/SK/S-A，17- DS-K1102G/GK，18- DS-K1100S-B，19- DS-K1102EM/EMK，20- DS-K1102E/EK，
                                   //21- DS-K1200EF，22- DS-K1200MF，23- DS-K1200CF，24- DS-K1300EF，25- DS-K1300MF，26- DS-K1300CF，27- DS-K1105E，28- DS-K1105M，29- DS-K1105C，30- DS-K182AMF，31- DS-K196AMF，32-DS-K194AMP
                                   //33-DS-K1T200EF/EF-C/MF/MF-C/CF/CF-C,34-DS-K1T300EF/EF-C/MF/MF-C/CF/CF-C，35-DS-K1T105E/E-C/M/M-C/C/C-C
            public byte byOkLedPolarity; //OK LED极性，0-阴极，1-阳极
            public byte byErrorLedPolarity; //Error LED极性，0-阴极，1-阳极
            public byte byBuzzerPolarity; //蜂鸣器极性，0-阴极，1-阳极
            public byte bySwipeInterval; //重复刷卡间隔时间，单位：秒
            public byte byPressTimeout;  //按键超时时间，单位：秒
            public byte byEnableFailAlarm; //是否启用读卡失败超次报警，0-不启用，1-启用
            public byte byMaxReadCardFailNum; //最大读卡失败次数
            public byte byEnableTamperCheck;  //是否支持防拆检测，0-disable ，1-enable
            public byte byOfflineCheckTime;  //掉线检测时间 单位秒
            public byte byFingerPrintCheckLevel;   //指纹识别等级，1-1/10误认率，2-1/100误认率，3-1/1000误认率，4-1/10000误认率，5-1/100000误认率，6-1/1000000误认率，7-1/10000000误认率，8-1/100000000误认率，9-3/100误认率，10-3/1000误认率，11-3/10000误认率，12-3/100000误认率，13-3/1000000误认率，14-3/10000000误认率，15-3/100000000误认率，16-Automatic Normal,17-Automatic Secure,18-Automatic More Secure
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_WEEK_PLAN_CFG = 2100; //获取门状态周计划参数
        public const int NET_DVR_SET_WEEK_PLAN_CFG = 2101; //设置门状态周计划参数
        public const int NET_DVR_GET_DOOR_STATUS_HOLIDAY_PLAN = 2102; //获取门状态假日计划参数
        public const int NET_DVR_SET_DOOR_STATUS_HOLIDAY_PLAN = 2103; //设置门状态假日计划参数
        public const int NET_DVR_GET_DOOR_STATUS_HOLIDAY_GROUP = 2104; //获取门状态假日组参数
        public const int NET_DVR_SET_DOOR_STATUS_HOLIDAY_GROUP = 2105; //设置门状态假日组参数
        public const int NET_DVR_GET_DOOR_STATUS_PLAN_TEMPLATE = 2106; //获取门状态计划模板参数
        public const int NET_DVR_SET_DOOR_STATUS_PLAN_TEMPLATE = 2107; //设置门状态计划模板参数
        public const int NET_DVR_GET_DOOR_STATUS_PLAN = 2110; //获取门状态计划参数
        public const int NET_DVR_SET_DOOR_STATUS_PLAN = 2111; //设置门状态计划参数
        public const int NET_DVR_CLEAR_ACS_PARAM = 2118; //清空门禁主机参数
        public const int NET_DVR_GET_VERIFY_WEEK_PLAN = 2124; //获取读卡器验证方式周计划参数
        public const int NET_DVR_SET_VERIFY_WEEK_PLAN = 2125; //设置读卡器验证方式周计划参数
        public const int NET_DVR_GET_CARD_RIGHT_WEEK_PLAN = 2126; //获取卡权限周计划参数
        public const int NET_DVR_SET_CARD_RIGHT_WEEK_PLAN = 2127; //设置卡权限周计划参数
        public const int NET_DVR_GET_VERIFY_HOLIDAY_PLAN = 2128; //获取读卡器验证方式假日计划参数
        public const int NET_DVR_SET_VERIFY_HOLIDAY_PLAN = 2129; //设置读卡器验证方式假日计划参数
        public const int NET_DVR_GET_CARD_RIGHT_HOLIDAY_PLAN = 2130; //获取卡权限假日计划参数
        public const int NET_DVR_SET_CARD_RIGHT_HOLIDAY_PLAN = 2131; //设置卡权限假日计划参数
        public const int NET_DVR_GET_VERIFY_HOLIDAY_GROUP = 2132; //获取读卡器验证方式假日组参数
        public const int NET_DVR_SET_VERIFY_HOLIDAY_GROUP = 2133; //设置读卡器验证方式假日组参数
        public const int NET_DVR_GET_CARD_RIGHT_HOLIDAY_GROUP = 2134; //获取卡权限假日组参数
        public const int NET_DVR_SET_CARD_RIGHT_HOLIDAY_GROUP = 2135; //设置卡权限假日组参数
        public const int NET_DVR_GET_VERIFY_PLAN_TEMPLATE = 2136; //获取读卡器验证方式计划模板参数
        public const int NET_DVR_SET_VERIFY_PLAN_TEMPLATE = 2137; //设置读卡器验证方式计划模板参数
        public const int NET_DVR_GET_CARD_RIGHT_PLAN_TEMPLATE = 2138; //获取卡权限计划模板参数
        public const int NET_DVR_SET_CARD_RIGHT_PLAN_TEMPLATE = 2139; //设置卡权限计划模板参数
        public const int NET_DVR_GET_CARD_READER_PLAN = 2142; //获取读卡器验证计划参数
        public const int NET_DVR_SET_CARD_READER_PLAN = 2143; //设置读卡器验证计划参数
        public const int NET_DVR_GET_CARD_USERINFO_CFG = 2163; //获取卡号关联用户信息参数
        public const int NET_DVR_SET_CARD_USERINFO_CFG = 2164; //设置卡号关联用户信息参数

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DATE
        {
            public ushort wYear; //年
            public byte byMonth; //月
            public byte byDay; //日
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SIMPLE_DAYTIME
        {
            public byte byHour; //时
            public byte byMinute; //分
            public byte bySecond; //秒
            public byte byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_TIME_SEGMENT
        {
            public NET_DVR_SIMPLE_DAYTIME struBeginTime; //开始时间点
            public NET_DVR_SIMPLE_DAYTIME struEndTime;   //结束时间点
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_PLAN_SEGMENT
        {
            public byte byEnable; //是否使能，1-使能，0-不使能
            public byte byDoorStatus; //门状态模式，0-无效，1-常开状态，2-常闭状态，3-普通状态（门状态计划使用）
            public byte byVerifyMode; //验证方式，0-无效，1-刷卡，2-刷卡+密码(读卡器验证方式计划使用)，3-刷卡,4-刷卡或密码(读卡器验证方式计划使用), 5-指纹，6-指纹+密码，7-指纹或刷卡，8-指纹+刷卡，9-指纹+刷卡+密码（无先后顺序）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
            public NET_DVR_TIME_SEGMENT struTimeSegment; //时间段参数
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WEEK_PLAN_CFG
        {
            public uint dwSize;
            public byte byEnable;  //是否使能，1-使能，0-不使能
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_DAYS * CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_PLAN_SEGMENT[] struPlanCfg; //周计划参数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_HOLIDAY_PLAN_CFG
        {
            public uint dwSize;
            public byte byEnable;  //是否使能，1-使能，0-不使能
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            public CHCNetSDK.NET_DVR_DATE struBeginDate; //假日开始日期
            public CHCNetSDK.NET_DVR_DATE struEndDate; //假日结束日期
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public CHCNetSDK.NET_DVR_SINGLE_PLAN_SEGMENT[] struPlanCfg; //时间段参数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes2;
        }

        public const int TEMPLATE_NAME_LEN = 32; //计划模板名称长度
        public const int MAX_HOLIDAY_GROUP_NUM = 16; //计划模板最大假日组数
        public const int HOLIDAY_GROUP_NAME_LEN = 32; //假日组名称长度
        public const int MAX_HOLIDAY_PLAN_NUM = 16; //假日组最大假日计划数

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_HOLIDAY_GROUP_CFG
        {
            public uint dwSize;
            public byte byEnable;  //是否使能，1-使能，0-不使能
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.HOLIDAY_GROUP_NAME_LEN)]
            public byte[] byGroupName; //假日组名称
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_HOLIDAY_GROUP_NUM, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHolidayPlanNo; //假日组编号，就前填充，遇0无效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PLAN_TEMPLATE
        {
            public uint dwSize;
            public byte byEnable; //是否启用，1-启用，0-不启用
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.TEMPLATE_NAME_LEN)]
            public byte[] byTemplateName; //模板名称
            public uint dwWeekPlanNo; //周计划编号，0为无效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_HOLIDAY_GROUP_NUM, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHolidayGroupNo; //假日组编号，就前填充，遇0无效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DOOR_STATUS_PLAN
        {
            public uint dwSize;
            public uint dwTemplateNo; //计划模板编号，为0表示取消关联，恢复默认状态（普通状态）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_READER_PLAN
        {
            public uint dwSize;
            public uint dwTemplateNo; //计划模板编号，为0表示取消关联，恢复默认状态（刷卡开门）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;
        }

        public const int ACS_PARAM_DOOR_STATUS_WEEK_PLAN = 0x00000001;      //门状态周计划参数
        public const int ACS_PARAM_VERIFY_WEEK_PALN = 0x00000002;           //读卡器周计划参数
        public const int ACS_PARAM_CARD_RIGHT_WEEK_PLAN = 0x00000004;       //卡权限周计划参数
        public const int ACS_PARAM_DOOR_STATUS_HOLIDAY_PLAN = 0x00000008;   //门状态假日计划参数
        public const int ACS_PARAM_VERIFY_HOLIDAY_PALN = 0x00000010;        //读卡器假日计划参数
        public const int ACS_PARAM_CARD_RIGHT_HOLIDAY_PLAN = 0x00000020;    //卡权限假日计划参数
        public const int ACS_PARAM_DOOR_STATUS_HOLIDAY_GROUP = 0x00000040;  //门状态假日组参数
        public const int ACS_PARAM_VERIFY_HOLIDAY_GROUP = 0x00000080;       //读卡器验证方式假日组参数
        public const int ACS_PARAM_CARD_RIGHT_HOLIDAY_GROUP = 0x00000100;   //卡权限假日组参数
        public const int ACS_PARAM_DOOR_STATUS_PLAN_TEMPLATE = 0x00000200;  //门状态计划模板参数
        public const int ACS_PARAM_VERIFY_PALN_TEMPLATE = 0x00000400;       //读卡器验证方式计划模板参数
        public const int ACS_PARAM_CARD_RIGHT_PALN_TEMPLATE = 0x00000800;   //卡权限计划模板参数
        public const int ACS_PARAM_CARD = 0x00001000;                       //卡参数
        public const int ACS_PARAM_GROUP = 0x00002000;                      //群组参数
        public const int ACS_PARAM_ANTI_SNEAK_CFG = 0x00004000;             //反潜回参数
        public const int ACS_PAPAM_EVENT_CARD_LINKAGE = 0x00008000;         //事件及卡号联动参数
        public const int ACS_PAPAM_CARD_PASSWD_CFG = 0x00010000;            //密码开门使能参数

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_PARAM_TYPE
        {
            public uint dwSize;
            public uint dwParamType;
            //参数类型，按位表示
            //#define ACS_PARAM_DOOR_STATUS_WEEK_PLAN        0x00000001 //门状态周计划参数
            //#define ACS_PARAM_VERIFY_WEEK_PALN             0x00000002 //读卡器周计划参数
            //#define ACS_PARAM_CARD_RIGHT_WEEK_PLAN         0x00000004 //卡权限周计划参数
            //#define ACS_PARAM_DOOR_STATUS_HOLIDAY_PLAN     0x00000008 //门状态假日计划参数
            //#define ACS_PARAM_VERIFY_HOLIDAY_PALN          0x00000010 //读卡器假日计划参数
            //#define ACS_PARAM_CARD_RIGHT_HOLIDAY_PLAN      0x00000020 //卡权限假日计划参数
            //#define ACS_PARAM_DOOR_STATUS_HOLIDAY_GROUP    0x00000040 //门状态假日组参数
            //#define ACS_PARAM_VERIFY_HOLIDAY_GROUP         0x00000080 //读卡器验证方式假日组参数
            //#define ACS_PARAM_CARD_RIGHT_HOLIDAY_GROUP     0x00000100 //卡权限假日组参数
            //#define ACS_PARAM_DOOR_STATUS_PLAN_TEMPLATE    0x00000200 //门状态计划模板参数
            //#define ACS_PARAM_VERIFY_PALN_TEMPLATE         0x00000400 //读卡器验证方式计划模板参数
            //#define ACS_PARAM_CARD_RIGHT_PALN_TEMPLATE     0x00000800 //卡权限计划模板参数
            //#define ACS_PARAM_CARD                         0x00001000 //卡参数
            //#define ACS_PARAM_GROUP                        0x00002000 //群组参数
            //#define ACS_PARAM_ANTI_SNEAK_CFG			     0x00004000 //反潜回参数
            //#define ACS_PAPAM_EVENT_CARD_LINKAGE           0x00008000 //事件及卡号联动参数
            //#define ACS_PAPAM_CARD_PASSWD_CFG              0x00010000 //密码开门使能参数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_USER_INFO_CFG
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN)]
            public byte[] byUsername;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] byRes2;
        }

    }
    
}
