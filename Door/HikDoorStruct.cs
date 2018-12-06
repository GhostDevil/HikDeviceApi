using System;
using System.Runtime.InteropServices;
using static HikDeviceApi.HikConst;
namespace HikDeviceApi.Door
{
    /// <summary>
    /// 日 期:2015-08-15
    /// 作 者:痞子少爷
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
            /// 门权限，按位表示，从低位到高位表示对门1~N是否有权限，值：0- 无权限，1- 有权限 (3代表对门1门2有权限 1代表对门1有权限···)
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

        #region 卡密码开门使能配置结构体
        /// <summary>
        /// 卡密码开门使能配置结构体
        /// </summary>
        /// <remarks>
        /// 设备是否支持卡密码开门使能配置，可以通过设备能力集进行判断，对应门禁主机能力集(AcsAbility)，相关接口：NET_DVR_GetDeviceAbility，能力集类型：ACS_ABILITY，节点：Card 中的 onlyPasswdOpen。 
        /// </remarks>
        public struct NET_DVR_CARD_PASSWD_CFG
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
            /// 卡密码 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CARD_PASSWORD_LEN)]
            public string byCardPassword;
            /// <summary>
            /// 获取卡密码开门使能配置返回的错误码，0表示配置成功，其他值表示失败的错误码 
            /// </summary>
            public uint dwErrorCode;
            /// <summary>
            /// 卡是否有效（用于删除卡，设置时置为0进行删除，获取时此字段始终为1）：0- 无效，1- 有效 
            /// </summary>
            public byte byCardValid;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.I1)]
            public byte byRes2;
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

        #region 周计划配置结构体
        /// <summary>
        /// 周计划配置结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WEEK_PLAN_CFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 是否使能：0- 否，1- 是
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 周计划参数，一周7天，每天最多8个时间段
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]//、MAX_DAYS* MAX_TIMESEGMENT_V30
            public NET_DVR_SINGLE_PLAN_SEGMENT[] struPlanCfg;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 假日计划配置结构体
        /// <summary>
        /// 假日计划配置结构体
        /// </summary>
        public struct NET_DVR_HOLIDAY_PLAN_CFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 是否使能：0- 否，1- 是 
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 假日开始日期
            /// </summary>
            public NET_DVR_DATE struBeginDate;
            /// <summary>
            /// 假日结束日期 
            /// </summary>
            public NET_DVR_DATE struEndDate;
            /// <summary>
            /// 假日计划参数，一周7天，每天最多8个时间段 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.I1)]
            public NET_DVR_SINGLE_PLAN_SEGMENT[] struPlanCfg;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte byRes2;
    }

        #endregion

        #region 假日组配置结构体
        /// <summary>
        /// 假日组配置结构体
        /// </summary>
        public struct NET_DVR_HOLIDAY_GROUP_CFG
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 是否使能：0- 否，1- 是
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 假日组名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HOLIDAY_GROUP_NAME_LEN)]
            public string byGroupName;
            /// <summary>
            /// 假日计划编号，按值表示，采用紧凑型排列，中间遇到0则后续无效
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_HOLIDAY_PLAN_NUM, ArraySubType = UnmanagedType.I1)]
            uint[] dwHolidayPlanNo;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        #endregion

        #region 计划参数结构体
        /// <summary>
        /// 计划参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_PLAN_SEGMENT
        {
            /// <summary>
            /// 是否使能：0- 否，1- 是 
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 门状态模式：0- 无效，1- 休眠，2- 常开状态，3- 常闭状态（门状态计划使用）
            /// </summary>
            public byte byDoorStatus;
            /// <summary>
            /// 验证方式：0- 无效，1- 休眠，2- 刷卡+密码(读卡器验证方式计划使用)，3- 刷卡(读卡器验证方式计划使用)，4- 刷卡或密码(读卡器验证方式计划使用)，5- 指纹，6- 指纹+密码，7- 指纹或刷卡，8- 指纹+刷卡，9- 指纹+刷卡+密码（无先后顺序）
            /// </summary>
            public byte byVerifyMode;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            /// <summary>
            /// 计划时间段，包括开始时间和结束时间
            /// </summary>
            public NET_DVR_TIME_SEGMENT struTimeSegment;
        }

        #endregion

        #region  计划模板配置结构体
        /// <summary>
        /// 计划模板配置结构体
        /// </summary>
        public struct NET_DVR_PLAN_TEMPLATE
        {
            /// <summary>
            /// 结构体大小 
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 是否使能：0- 否，1- 是
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte byRes1;
            /// <summary>
            /// 计划模板名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = TEMPLATE_NAME_LEN)]
            public string byTemplateName;
            /// <summary>
            /// 周计划编号，0表示无效
            /// </summary>
            public uint dwWeekPlanNo;
            /// <summary>
            /// 假日组编号，按值表示，采用紧凑型排列，中间遇到0则后续无效
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_HOLIDAY_GROUP_NUM, ArraySubType = UnmanagedType.I1)]
            public uint[] dwHolidayGroupNo;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
    }


        #endregion

        #region 群组参数配置结构体
        /// <summary>
        /// 群组参数配置结构体
        /// </summary>
        public struct NET_DVR_GROUP_CFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 是否启用该群组：0- 不启用，1- 启用 
            /// </summary>
            public byte byEnable;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 群组有效期参数
            /// </summary>
            public NET_DVR_VALID_PERIOD_CFG struValidPeriodCfg;
            /// <summary>
            /// 群组名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = GROUP_NAME_LEN)]
            public byte[] byGroupName;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
    }

        #endregion

        #region 时间段参数结构体
        /// <summary>
        /// 日期信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DATE
        {
            /// <summary>
            /// 年
            /// </summary>
            public ushort wYear;
            /// <summary>
            /// 月
            /// </summary>
            public byte byMonth;
            /// <summary>
            /// 日
            /// </summary>
            public byte byDay;
        }

        /// <summary>
        /// 时间段参数结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_TIME_SEGMENT
        {
            /// <summary>
            /// 开始时间点（时分秒）
            /// </summary>
            public NET_DVR_SIMPLE_DAYTIME struBeginTime;
            /// <summary>
            /// 结束时间点（时分秒）
            /// </summary>
            public NET_DVR_SIMPLE_DAYTIME struEndTime;
        }
        /// <summary>
        /// 时间点信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SIMPLE_DAYTIME
        {
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
            /// 保留，置为0 
            /// </summary>
            public byte byRes;
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
            public ushort wYear;
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
            /// <summary>
            /// 转换时间格式字符串
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return string.Format("{0}-{1}-{2} {3}:{4}:{5}", wYear, byMonth, byDay, byHour, byMinute, bySecond);
            }
            /// <summary>
            /// 转换日期对象
            /// </summary>
            /// <returns></returns>
            public DateTime ConvertToDateTime()
            {
                return new DateTime(this.wYear, this.byMonth, this.byDay, this.byHour, this.byMinute, this.bySecond);

            }
            /// <summary>
            /// 转换结构体对象
            /// </summary>
            /// <param name="time"></param>
            /// <returns></returns>
            public static NET_DVR_TIME_EX FromDateTime(DateTime time)
            {
                return new NET_DVR_TIME_EX() { wYear = (ushort)time.Year, byMonth = (byte)time.Month, byDay = (byte)time.Day, byHour = (byte)time.Hour, byMinute = (byte)time.Minute, bySecond = (byte)time.Second };
            }
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
            public HikStruct.NET_DVR_TIME struTime;
            /// <summary>
            /// 网络操作的用户名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;
            /// <summary>
            /// 远程主机地址
            /// </summary>
            public HikStruct.NET_DVR_IPADDR struRemoteHostAddr;
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
            /// 读卡器类型：1- DS-K110XM/MK/C/CK，2- DS-K192AM/AMP，3- DS-K192BM/BMP，4- DS-K182AM/AMP，5- DS-K182BM/BMP，6- DS-K182AMF/ACF，7- 韦根或485不在线，8- DS-K1101M/MK，9- DS-K1101C/CK，10- DS-K1102M/MK/M-A，11- DS-K1102C/CK，12- DS-K1103M/MK，13- DS-K1103C/CK，14- DS-K1104M/MK，15- DS-K1104C/CK，16- DS-K1102S/SK/S-A，17- DS-K1102G/GK，18- DS-K1100S-B，19- DS-K1102EM/EMK，20- DS-K1102E/EK，21- DS-K1200EF，22- DS-K1200MF，23- DS-K1200CF，24- DS-K1300EF，25- DS-K1300MF，26- DS-K1300CF，27- DS-K1105E，28- DS-K1105M，29- DS-K1105C，30- DS-K182AMF，31- DS-K196AMF，32- DS-K194AMP，33- DS-K1T200EF/EF-C/MF-MF-C/CF/CF-C，34- DS-K1T300EF/EF-C/MF-MF-C/CF/CF-C
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
            ///// <summary>
            ///// 指纹识别等级：1- 1/10误认率，2- 1/100误认率，3- 1/1000误认率，4- 1/10000误认率，5- 1/100000误认率，6- 1/1000000误认率，7- 1/10000000误认率，8- 1/100000000误认率，9- 3/100误认率，10- 3/1000误认率，11- 3/10000误认率，12- 3/100000误认率，13- 3/1000000误认率，14- 3/10000000误认率，15- 3/100000000误认率，16- Automatic Normal，17- Automatic Secure，18- Automatic More Secure 
            ///// </summary>
            //public byte byFingerPrintCheckLevel;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        #endregion

        #region  门参数配置结构体 
        /// <summary>
        /// 门参数配置结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DOOR_CFG
        {
            /// <summary>
            /// 结构体大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 门名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = DOOR_NAME_LEN)]
            public string byDoorName;
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
            ///  胁迫密码 长度8
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = STRESS_PASSWORD_LEN)]
            public string byStressPassword;
            /// <summary>
            /// 超级密码 长度8
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SUPER_PASSWORD_LEN)]
            public string bySuperPassword;
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
            /// 读卡器当前验证方式：0- 无效，1- 休眠，2- 刷卡+密码，3- 刷卡，4- 刷卡或密码，5- 指纹，6- 指纹加密码，7- 指纹或刷卡，8- 指纹加刷卡，9- 指纹加刷卡加密码
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


    }
}
