using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikDeviceApi
{
    /// <summary>
    /// 日 期:2017-06-25
    /// 作 者:痞子少爷
    /// 描 述:海康设备公用接口常量
    /// </summary>
    public static class HikConst
    {
        /// <summary>
        /// 用户名长度
        /// </summary>
        public const int NAME_LEN = 32;
        /// <summary>
        /// 密码长度
        /// </summary>
        public const int PASSWD_LEN = 16;
        /// <summary>
        /// GUID长度
        /// </summary>
        public const int GUID_LEN = 16;
        /// <summary>
        /// 设备类型名称长度
        /// </summary>
        public const int DEV_TYPE_NAME_LEN = 24;
        /// <summary>
        /// DVR本地登陆名
        /// </summary>
        public const int MAX_NAMELEN = 16;
        /// <summary>
        /// 设备支持的权限（1-12表示本地权限，13-32表示远程权限）
        /// </summary>
        public const int MAX_RIGHT = 32;
        /// <summary>
        /// 序列号长度
        /// </summary>
        public const int SERIALNO_LEN = 48;
        /// <summary>
        /// mac地址长度
        /// </summary>
        public const int MACADDR_LEN = 6;
        /// <summary>
        /// 设备可配以太网络
        /// </summary>
        public const int MAX_ETHERNET = 2;
        /// <summary>
        /// 设备可配最大网卡数目
        /// </summary>
        public const int MAX_NETWORK_CARD = 4;

        /// <summary>
        /// 设备名称最大长度
        /// </summary>
        public const int MAX_NAME_LEN = 128;
        /// <summary>
        /// 9000设备最大时间段数
        /// </summary>
        public const int MAX_TIMESEGMENT_V30 = 8;
        /// <summary>
        /// 8000设备最大时间段数
        /// </summary>
        public const int MAX_TIMESEGMENT = 4;

        /// <summary>
        /// ipc 协议最大个数
        /// </summary>
        public const int IPC_PROTOCOL_NUM = 50;
        /// <summary>
        /// 8000语音对讲通道数
        /// </summary>
        public const int MAX_AUDIO = 1;
        /// <summary>
        /// 9000语音对讲通道数
        /// </summary>
        public const int MAX_AUDIO_V30 = 2;
        /// <summary>
        /// 8000设备最大通道数
        /// </summary>
        public const int MAX_CHANNUM = 16;
        /// <summary>
        /// 8000设备最大报警输入数
        /// </summary>
        public const int MAX_ALARMIN = 16;
        /// <summary>
        /// 8000设备最大报警输出数
        /// </summary>
        public const int MAX_ALARMOUT = 4;
        /// <summary>
        /// 9000设备最大硬盘数/* 最多33个硬盘(包括16个内置SATA硬盘、1个eSATA硬盘和16个NFS盘)
        /// </summary>
        public const int MAX_DISKNUM_V30 = 33;
        /// <summary>
        /// 最大域名长度
        /// </summary>
        public const int MAX_DOMAIN_NAME = 64;
        /// <summary>
        /// 8000设备最大硬盘数
        /// </summary>
        public const int MAX_DISKNUM = 16;
        /// <summary>
        /// 1.2版本之前版本最大硬盘书
        /// </summary>
        public const int MAX_DISKNUM_V10 = 8;
        /// <summary>
        /// 8000设备单通道最大视频流连接数
        /// </summary>
        public const int MAX_LINK = 6;
        /// <summary>
        /// 云台描述字符串长度
        /// </summary>
        public const int DESC_LEN = 16;
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
        public const int MAX_IP_ALARMOUT_V40 = 4096;

        /* 最大支持的通道数 最大模拟加上最大IP支持 */
        public const int MAX_CHANNUM_V30 = MAX_ANALOG_CHANNUM + MAX_IP_CHANNEL;//64
        public const int MAX_ALARMOUT_V30 = MAX_ANALOG_ALARMOUT + MAX_IP_ALARMOUT;//96
        public const int MAX_ALARMIN_V30 = MAX_ANALOG_ALARMIN + MAX_IP_ALARMIN;//160
        public const int MAX_CHANNUM_V40 = 512;
        public const int MAX_ALARMOUT_V40 = MAX_IP_ALARMOUT_V40 + MAX_ANALOG_ALARMOUT;//4128
        public const int MAX_ALARMIN_V40 = MAX_IP_ALARMIN_V40 + MAX_ANALOG_ALARMOUT;//4128

        /// <summary>
        /// 允许接入的最大IP设备数
        /// </summary>
        public const int MAX_IP_DEVICE = 32;

        public const int STREAM_ID_LEN = 32;

        /// <summary>
        /// 路径长度
        /// </summary>
        public const int PATHNAME_LEN = 128;
        /// <summary>
        /// 号码最大长度
        /// </summary>
        public const int MAX_NUMBER_LEN = 32;


        public const int MAX_DISPNUM_V41 = 32;
        public const int MAX_WINDOWS_NUM = 12;
        public const int MAX_VOUTNUM = 32;
        public const int MAX_SUPPORT_RES = 32;


        /// <summary>
        /// 9000设备支持232串口数
        /// </summary>
        public const int MAX_SERIAL_PORT = 8;
        /// <summary>
        /// 设备支持最大预览模式数目 1画面,4画面,9画面,16画面.... 
        /// </summary>
        public const int MAX_PREVIEW_MODE = 8;
        /// <summary>
        /// 最大模拟矩阵输出个数
        /// </summary>
        public const int MAX_MATRIXOUT = 16;
        /// <summary>
        /// 日志附加信息
        /// </summary>
        public const int LOG_INFO_LEN = 11840;

        /// <summary>
        /// 9000最大支持的云台协议数 
        /// </summary>
        public const int PTZ_PROTOCOL_NUM = 200;
        /// <summary>
        /// 最大大屏数量
        /// </summary>
        public const int MAX_BIGSCREENNUM = 100;
        /// <summary>
        /// 视频综合平台能力集
        /// </summary>
        public const int VIDEOPLATFORM_ABILITY = 0x210;
        /// <summary>
        /// 解码器能力集
        /// </summary>
        public const int MATRIXDECODER_ABILITY_V41 = 0x260;
        /// <summary>
        /// 获取大屏拼接参数   
        /// </summary>
        public const int NET_DVR_MATRIX_BIGSCREENCFG_GET = 1140;
        /// <summary>
        /// 最大窗口数
        /// </summary>
        public const int MAX_WINDOWS = 16;
        /// <summary>
        /// 各个子窗口关联的解码通道最大值
        /// </summary>
        public const int MAX_WINDOWS_V41 = 36;
        /// <summary>
        /// 透明通道最大值
        /// </summary>
        public const int MAX_SERIAL_NUM = 64;
        /// <summary>
        /// 获取解码通道状态最大值
        /// </summary>
        public const int MAX_DECODECHANNUM = 32;
        /// <summary>
        /// 最大显示输出个数
        /// </summary>
        public const int MAX_DISPLAY_NUM = 512; 
        /// <summary>
        /// 显示通道状态数量
        /// </summary>
        public const int MAX_DISPCHANNUM = 24;
        /// <summary>
        /// 子画面的显示帧率数量
        /// </summary>
        public const int NET_DVR_MAX_DISPREGION = 16;

        #region  SDK门禁类型 
        /// <summary>
        /// 卡密码长度
        /// </summary>
        public const int CARD_PASSWORD_LEN = 8;
        /// <summary>
        /// 一周最大天数
        /// </summary>
        public const int MAX_DAYS = 7;
        /// <summary>
        /// 假日组数量
        /// </summary>
        public const int MAX_HOLIDAY_GROUP_NUM = 4;
        /// <summary>
        /// 计划模板名称长度
        /// </summary>
        public const int TEMPLATE_NAME_LEN = 32;
        /// <summary>
        /// 群组名称长度
        /// </summary>
        public const int GROUP_NAME_LEN = 32;
        /// <summary>
        /// 假日组名称长度
        /// </summary>
        public const int HOLIDAY_GROUP_NAME_LEN = 32;
        /// <summary>
        /// 假日组计划编号长度
        /// </summary>
        public const int MAX_HOLIDAY_PLAN_NUM = 16;
        /// <summary>
        /// 卡权限组编号程度
        /// </summary>
        public const int MAX_CARD_RIGHT_PLAN_NUM = 4;
        /// <summary>
        /// 门禁卡号长度
        /// </summary>
        public const int ACS_CARD_NO_LEN = 32;
        /// <summary>
        /// 报警事件输入状态长度
        /// </summary>
        public const int MAX_CASE_SENSOR_NUM = 8;
        /// <summary>
        /// 门编号长度
        /// </summary>
        public const int MAX_DOOR_NUM = 32;
        /// <summary>
        /// 读卡器编号长度
        /// </summary>
        public const int MAX_CARD_READER_NUM = 64;
        /// <summary>
        /// 报警输入口编号长度
        /// </summary>
        public const int MAX_ALARMHOST_ALARMIN_NUM = 512;
        /// <summary>
        /// 报警输出口编号长度
        /// </summary>
        public const int MAX_ALARMHOST_ALARMOUT_NUM = 512;
        /// <summary>
        /// 门禁名称长度
        /// </summary>
        public const int DOOR_NAME_LEN = 32;
        /// <summary>
        /// 超级密码长度
        /// </summary>
        public const int SUPER_PASSWORD_LEN = 8;
        /// <summary>
        /// 胁迫密码长度
        /// </summary>
        public const int STRESS_PASSWORD_LEN = 8;
        #endregion

        /// <summary>
        /// 获取IP接入配置信息 （NET_DVR_IPPARACFG_V40结构）
        /// </summary>
        public const int NET_DVR_GET_IPPARACFG_V40 = 1062;
        /// <summary>
        /// //设置IP接入配置信息 （NET_DVR_IPPARACFG_V40结构）
        /// </summary>
        public const int NET_DVR_SET_IPPARACFG_V40 = 1063;

        /// <summary>
        /// 9000报警信息主动上传
        /// </summary>
        public const int COMM_ALARM_V30 = 0x4000;
        /// <summary>
        /// 8000报警信息主动上传，对应NET_DVR_ALARMINFO
        /// </summary>
        public const int COMM_ALARM = 0x1100;
        /// <summary>
        /// 行为分析报警信息，对应NET_VCA_RULE_ALARM
        /// </summary>
        public const int COMM_ALARM_RULE = 0x1102;
        /// <summary>
        /// 上传车牌信息
        /// </summary>
        public const int COMM_UPLOAD_PLATE_RESULT = 0x2800;
        /// <summary>
        /// 终端图片上传
        /// </summary>
        public const int COMM_ITS_PLATE_RESULT = 0x3050;

        /// <summary>
        /// 播放库
        /// </summary>
        public const int SDK_PLAYMPEG4 = 1;
        /// <summary>
        /// 网络库
        /// </summary>
        public const int SDK_HCNETSDK = 2;

        #region 电视墙
        /// <summary>
        /// 外部结构体卡号长度
        /// </summary>
        public const int CARDNUM_LEN_OUT = 32;
        /// <summary>
        /// 窗口参数设置
        /// </summary>
        public const int NET_DVR_SET_WINCFG = 1202;
        /// <summary>
        /// 设置大屏拼接参数
        /// </summary>
        public const int NET_DVR_MATRIX_BIGSCREENCFG_SET = 1141;
        /// <summary>
        /// 获取电视墙中屏幕参数
        /// </summary>
        public const int NET_DVR_MATRIX_WALL_GET = 9002;
        /// <summary>
        /// 设置电视墙中屏幕参数
        /// </summary>
        public const int NET_DVR_MATRIX_WALL_SET = 9001;
        /// <summary>
        /// 获取电视墙窗口参数
        /// </summary>
        public const int NET_DVR_WALLWIN_GET = 9003;
        /// <summary>
        /// 设置电视墙窗口参数
        /// </summary>
        public const int NET_DVR_WALLWIN_SET = 9004;
        /// <summary>
        /// 设置电视墙窗口相关参数
        /// </summary>
        public const int NET_DVR_WALLWINPARAM_SET = 9005;
        /// <summary>
        /// 获取电视墙窗口相关参数
        /// </summary>
        public const int NET_DVR_WALLWINPARAM_GET = 9006;
       
        /// <summary>
        /// 获取显示输出位置参数
        /// </summary>
        public const int NET_DVR_GET_VIDEOWALLDISPLAYPOSITION = 1734;
        /// <summary>
        /// 设置显示输出位置参数
        /// </summary>
        public const int NET_DVR_SET_VIDEOWALLDISPLAYPOSITION = 1733;
        /// <summary>
        /// 获取设备显示输出号
        /// </summary>
        public const int NET_DVR_GET_VIDEOWALLDISPLAYNO = 1732;
        /// <summary>
        /// 获取电视墙窗口参数
        /// </summary>
        public const int NET_DVR_GET_VIDEOWALLWINDOWPOSITION = 1735;
        /// <summary>
        /// 设置电视墙窗口参数
        /// </summary>
        public const int NET_DVR_SET_VIDEOWALLWINDOWPOSITION = 1736;
        /// <summary>
        /// 获取编码通道关联资源参数
        /// </summary>
        public const int NET_DVR_GET_CHAN_RELATION = 9209;
        /// <summary>
        /// 设置编码通道关联资源参数
        /// </summary>
        public const int NET_DVR_SET_CHAN_RELATION = 9210;
        /// <summary>
        /// 获取所有编码通道关联资源参数
        /// </summary>
        public const int NET_DVR_GET_ALL_CHAN_RELATION = 9211;
        public const int VW_BASE_RESOLUTION_FIRST = 1920;
        public const int VW_BASE_RESOLUTION_SECOND = 1920;

        public const int MAX_VIDEOWALL_NUM = 16;
        public const int MAX_VW_DISPLAYPOSITION = 256;
        public const int MAX_VM_WIN_NUM = 256;
        public const int MAX_WALL_WIN_COUNT= 256;


        //智能分析结构参数宏定义 
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


        #endregion
    }
}
