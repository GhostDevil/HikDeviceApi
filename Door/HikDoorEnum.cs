namespace HikDeviceApi.Door
{
    /// <summary>
    /// 日 期:2015-12-28
    /// 作 者:痞子少爷
    /// 描 述:海康26系列门禁接口枚举
    /// </summary>
    public static class HikDoorEnum
    {
        #region  门禁控制 
        /// <summary>
        /// 门禁控制
        /// </summary>
        public enum DoorControl
        {
            /// <summary>
            /// 关闭
            /// </summary>
            关闭 = 0,
            /// <summary>
            /// 打开
            /// </summary>
            打开 = 1,
            /// <summary>
            /// 常开
            /// </summary>
            常开 = 2,
            /// <summary>
            /// 常关
            /// </summary>
            常关 = 3

        }
        #endregion

        #region  卡片类型
        /// <summary>
        /// 卡片类型
        /// </summary>
        public enum CardType
        {
            /// <summary>
            /// 无效
            /// </summary>
            无效,
            /// <summary>
            /// 普通卡
            /// </summary>
            普通卡,
            /// <summary>
            /// 残疾人卡
            /// </summary>
            残疾人卡,
            /// <summary>
            /// 黑名单卡
            /// </summary>
            黑名单卡,
            /// <summary>
            /// 巡更卡
            /// </summary>
            巡更卡,
            /// <summary>
            /// 胁迫卡
            /// </summary>
            胁迫卡,
            /// <summary>
            /// 超级卡
            /// </summary>
            超级卡,
            /// <summary>
            /// 来宾卡
            /// </summary>
            来宾卡

        }
        #endregion

        #region 门禁事件类型
        /// <summary>
        /// 反馈主类型
        /// </summary>
        public enum MajorType
        {
            /// <summary>
            ///  报警
            /// </summary>
            MAJOR_ALARM = 0x1,
            /// <summary>
            /// 异常
            /// </summary>
            MAJOR_EXCEPTION = 0x2,
            /// <summary>
            /// 操作
            /// </summary>
            MAJOR_OPERATION = 0x3,
            /// <summary>
            /// 事件
            /// </summary>
            MAJOR_EVENT = 0x5

        }
        /// <summary>
        /// 操作类型
        /// </summary>
        public enum MajorOperation
        {
            /// <summary>
            ///  本地升级
            /// </summary>
            本地升级 = 0x5a,
            /// <summary>
            ///  远程登录
            /// </summary>
            远程登录 = 0x70,
            /// <summary>
            /// 远程注销登陆
            /// </summary>
            远程注销登陆 = 0x71,
            /// <summary>
            /// 远程布防
            /// </summary>
            远程布防 = 0x79,
            /// <summary>
            /// 远程撤防
            /// </summary>
            远程撤防 = 0x7a,
            /// <summary>
            ///  远程重启
            /// </summary>
            远程重启 = 0x7b,
            /// <summary>
            /// 远程升级
            /// </summary>
            远程升级 = 0x7e,
            /// <summary>
            /// 远程导出配置文件
            /// </summary>
            远程导出配置文件 = 0x86,
            /// <summary>
            ///  远程导入配置文件
            /// </summary>
            远程导入配置文件 = 0x87,
            /// <summary>
            /// 远程手动开启报警输出
            /// </summary>
            远程手动开启报警输出 = 0xd6,
            /// <summary>
            ///  远程手动关闭报警输出
            /// </summary>
            远程手动关闭报警输出 = 0xd7,
            /// <summary>
            ///  远程开门
            /// </summary>
            远程开门 = 0x400,
            /// <summary>
            ///  远程关门
            /// </summary>
            远程关门 = 0x401,
            /// <summary>
            /// 远程常开
            /// </summary>
            远程常开 = 0x402,
            /// <summary>
            /// 远程常关
            /// </summary>
            远程常关 = 0x403,
            /// <summary>
            /// 远程手动校时
            /// </summary>
            远程手动校时 = 0x404,
            /// <summary>
            /// NTP自动校时
            /// </summary>
            NTP自动校时 = 0x405,
            /// <summary>
            /// 远程清空卡号
            /// </summary>
            远程清空卡号 = 0x406,
            /// <summary>
            /// 远程恢复默认参数
            /// </summary>
            远程恢复默认参数 = 0x407,
            /// <summary>
            /// 防区布防
            /// </summary>
            防区布防 = 0x408,
            /// <summary>
            /// 防区撤防
            /// </summary>
            防区撤防 = 0x409,
            /// <summary>
            /// 本地恢复默认参数
            /// </summary>
            本地恢复默认参数 = 0x40a


        }
        /// <summary>
        /// 异常类型
        /// </summary>
        public enum MajorException
        {
            /// <summary>
            /// 网络断开
            /// </summary>
            网络断开 = 0x27,
            /// <summary>
            /// RS485连接状态异常
            /// </summary>
            RS485连接状态异常 = 0x3a,
            /// <summary>
            /// RS485连接状态异常恢复
            /// </summary>
            RS485连接状态异常恢复 = 0x3b,
            /// <summary>
            /// 设备上电启动
            /// </summary>
            设备上电启动 = 0x400,
            /// <summary>
            ///  设备掉电关闭
            /// </summary>
            设备掉电关闭 = 0x401,
            /// <summary>
            /// 看门狗复位
            /// </summary>
            看门狗复位 = 0x402,
            /// <summary>
            /// 蓄电池电压低
            /// </summary>
            蓄电池电压低 = 0x403,
            /// <summary>
            ///  蓄电池电压恢复正常
            /// </summary>
            蓄电池电压恢复正常 = 0x404,
            /// <summary>
            /// 交流电断电
            /// </summary>
            交流电断电 = 0x405,
            /// <summary>
            /// 交流电恢复
            /// </summary>
            交流电恢复 = 0x406,
            /// <summary>
            /// 网络恢复
            /// </summary>
            网络恢复 = 0x407,
            /// <summary>
            /// FLASH读写异常 
            /// </summary>
            FLASH读写异常 = 0x408,
            /// <summary>
            /// 读卡器掉线
            /// </summary>
            读卡器掉线 = 0x409,
            /// <summary>
            /// 读卡器掉线恢复
            /// </summary>
            读卡器掉线恢复 = 0x40a


        }
        /// <summary>
        /// 报警类型
        /// </summary>
        public enum MajorAlarm
        {
            /// <summary>
            ///  防区短路报警
            /// </summary>
            防区短路报警 = 0x400,
            /// <summary>
            /// 防区断路报警
            /// </summary>
            防区断路报警 = 0x401,
            /// <summary>
            /// 防区异常报警
            /// </summary>
            防区异常报警 = 0x402,
            /// <summary>
            /// 防区报警恢复
            /// </summary>
            防区报警恢复 = 0x403,
            /// <summary>
            /// 防区防拆报警
            /// </summary>
            防区防拆报警 = 0x404,
            /// <summary>
            /// 防区防拆恢复
            /// </summary>
            防区防拆恢复 = 0x405,
            /// <summary>
            /// 读卡器防拆报警
            /// </summary>
            读卡器防拆报警 = 0x406,
            /// <summary>
            /// 读卡器防拆恢复
            /// </summary>
            读卡器防拆恢复 = 0x407,
            /// <summary>
            /// 事件输入报警
            /// </summary>
            事件输入报警 = 0x408,
            /// <summary>
            /// 事件输入恢复
            /// </summary>
            事件输入恢复 = 0x409,
            /// <summary>
            /// 胁迫报警
            /// </summary>
            胁迫报警 = 0x40a,
            /// <summary>
            /// 离线事件满90%报警
            /// </summary>
            离线事件满90报警 = 0x40b,
            /// <summary>
            /// 卡号认证失败超次报警
            /// </summary>
            卡号认证失败超次报警 = 0x40c


        }
        /// <summary>
        /// 事件类型
        /// </summary>
        public enum MajorEvent
        {

            /// <summary>
            /// 合法卡认证通过
            /// </summary>
            合法卡认证通过 = 0x01,
            /// <summary>
            /// 刷卡加密码认证通过
            /// </summary>
            刷卡加密码认证通过 = 0x02,
            /// <summary>
            /// 刷卡加密码认证失败
            /// </summary>
            刷卡加密码认证失败 = 0x03,
            /// <summary>
            /// 刷卡加密码认证超时
            /// </summary>
            刷卡加密码认证超时 = 0x04,
            /// <summary>
            /// 刷卡加密码超次
            /// </summary>
            刷卡加密码超次 = 0x05,
            /// <summary>
            /// 未分配权限
            /// </summary>
            未分配权限 = 0x06,
            /// <summary>
            ///  无效时段
            /// </summary>
            无效时段 = 0x07,
            /// <summary>
            /// 卡号过期
            /// </summary>
            卡号过期 = 0x08,
            /// <summary>
            /// 无此卡号
            /// </summary>
            无此卡号 = 0x09,
            /// <summary>
            /// 反潜回认证失败
            /// </summary>
            反潜回认证失败 = 0x0a,
            /// <summary>
            /// 互锁门未关闭
            /// </summary>
            互锁门未关闭 = 0x0b,
            /// <summary>
            /// 卡不属于多重认证群组
            /// </summary>
            卡不属于多重认证群组 = 0x0c,
            /// <summary>
            /// 卡不在多重认证时间段内
            /// </summary>
            卡不在多重认证时间段内 = 0x0d,
            /// <summary>
            ///  多重认证模式超级权限认证失败
            /// </summary>
            多重认证模式超级权限认证失败 = 0x0e,
            /// <summary>
            /// 多重认证模式远程认证失败
            /// </summary>
            多重认证模式远程认证失败 = 0x0f,
            /// <summary>
            /// 多重认证成功
            /// </summary>
            多重认证成功 = 0x10,
            /// <summary>
            /// 首卡开门开始
            /// </summary>
            首卡开门开始 = 0x11,
            /// <summary>
            /// 首卡开门结束
            /// </summary>
            首卡开门结束 = 0x12,
            /// <summary>
            /// 常开状态开始
            /// </summary>
            常开状态开始 = 0x13,
            /// <summary>
            /// 常开状态结束
            /// </summary>
            常开状态结束 = 0x14,
            /// <summary>
            ///  门锁打开
            /// </summary>
            门锁打开 = 0x15,
            /// <summary>
            /// 门锁关闭
            /// </summary>
            门锁关闭 = 0x16,
            /// <summary>
            /// 开门按钮打开
            /// </summary>
            开门按钮打开 = 0x17,
            /// <summary>
            /// 开门按钮放开
            /// </summary>
            开门按钮放开 = 0x18,
            /// <summary>
            /// 正常开门（门磁）
            /// </summary>
            门磁正常开启 = 0x19,
            /// <summary>
            /// 正常关门（门磁）
            /// </summary>
            门磁正常关闭 = 0x1a,
            /// <summary>
            ///  门异常打开（门磁）
            /// </summary>
            门磁异常打开 = 0x1b,
            /// <summary>
            /// 门打开超时（门磁）
            /// </summary>
            门磁打开超时 = 0x1c,
            /// <summary>
            /// 报警输出打开
            /// </summary>
            报警输出打开 = 0x1d,
            /// <summary>
            /// 报警输出关闭
            /// </summary>
            报警输出关闭 = 0x1e,
            /// <summary>
            /// 常关状态开始
            /// </summary>
            常关状态开始 = 0x1f,
            /// <summary>
            /// 常关状态结束
            /// </summary>
            常关状态结束 = 0x20,
            /// <summary>
            /// 多重多重认证需要远程开门
            /// </summary>
            多重多重认证需要远程开门 = 0x21,
            /// <summary>
            /// 多重认证超级密码认证成功事件
            /// </summary>
            多重认证超级密码认证成功事件 = 0x22,
            /// <summary>
            /// 多重认证重复认证事件
            /// </summary>
            多重认证重复认证事件 = 0x23,
            /// <summary>
            /// 多重认证重复认证事件
            /// </summary>
            多重认证重复超时事件 = 0x24


        }
        #endregion

        #region 执行命令
        /// <summary>
        /// 配置执行命令
        /// </summary>
        public enum ConfigCommand
        {
            /// <summary>
            /// 设置门参数,对应NET_DVR_DOOR_CFG结构体
            /// </summary>
            NET_DVR_SET_DOOR_CFG = 2109,
            /// <summary>
            /// 获取门参数，对应NET_DVR_DOOR_CFG结构体
            /// </summary>
            NET_DVR_GET_DOOR_CFG = 2108,
            /// <summary>
            /// 获取门禁主机工作状态，对应NET_DVR_ACS_WORK_STATUS结构体
            /// </summary>
            NET_DVR_GET_ACS_WORK_STATUS = 2123,
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
            NET_DVR_GET_DEVICECFG_V40 = 1100,
            /// <summary>
            /// 获取读卡器参数，对应NET_DVR_CARD_READER_CFG结构体
            /// </summary>
            NET_DVR_GET_CARD_READER_CFG = 2140,
            /// <summary>
            /// 设置读卡器参数，对应NET_DVR_CARD_READER_CFG结构体
            /// </summary>
            NET_DVR_SET_CARD_READER_CFG = 2141,
            /// <summary>
            /// 门禁主机报警
            /// </summary>
            COMM_ALARM_ACS = 0x5002,
            /// <summary>
            /// 获取门状态计划参数，门编号，从1开始，对应NET_DVR_DOOR_STATUS_PLAN结构体
            /// </summary>
            NET_DVR_GET_DOOR_STATUS_PLAN = 2110,
            /// <summary>
            /// 获取读卡器验证计划参数，读卡器编号，从1开始 ,对应NET_DVR_CARD_READER_PLAN结构体
            /// </summary>
            NET_DVR_GET_CARD_READER_PLAN = 2142,
            /// <summary>
            /// 获取卡参数 ,对应NET_DVR_CARD_CFG_COND结构体
            /// </summary>
            NET_DVR_GET_CARD_CFG = 2116,
            /// <summary>
            /// 设置卡参数，对应NET_DVR_CARD_CFG_COND结构体
            /// </summary>
            NET_DVR_SET_CARD_CFG = 2117,
            /// <summary>
            /// 门禁主机数据类型
            /// </summary>
            ENUM_ACS_SEND_DATA = 0x3,
            /// <summary>
            /// 获取门状态周计划参数
            /// </summary>
            NET_DVR_GET_WEEK_PLAN_CFG = 2100,
            /// <summary>
            /// 设置门状态周计划参数
            /// </summary>
            NET_DVR_SET_WEEK_PLAN_CFG = 2101,

            /// <summary>
            /// 设置读卡器验证方式周计划参数
            /// </summary>
            NET_DVR_SET_VERIFY_WEEK_PLAN = 2125,

            /// <summary>
            /// 设置卡权限周计划参数
            /// </summary>
            NET_DVR_SET_CARD_RIGHT_WEEK_PLAN = 2127,

            /// <summary>
            /// 设置门状态假日计划参数
            /// </summary>

            NET_DVR_SET_DOOR_STATUS_HOLIDAY_PLAN = 2103,

            /// <summary>
            /// 设置读卡器验证方式假日计划参数
            /// </summary>
            NET_DVR_SET_VERIFY_HOLIDAY_PLAN = 2129,

            /// <summary>
            /// 设置卡权限假日计划参数
            /// </summary>
            NET_DVR_SET_CARD_RIGHT_HOLIDAY_PLAN = 2131,

            /// <summary>
            /// 设置门状态假日组参数
            /// </summary>
            NET_DVR_SET_DOOR_STATUS_HOLIDAY_GROUP = 2105,

            /// <summary>
            /// 设置读卡器验证方式假日组参数
            /// </summary>
            NET_DVR_SET_VERIFY_HOLIDAY_GROUP = 2133,

            /// <summary>
            /// 设置卡权限假日组参数
            /// </summary>
            NET_DVR_SET_CARD_RIGHT_HOLIDAY_GROUP = 2135,

            /// <summary>
            /// 设置门状态计划模板参数
            /// </summary>
            NET_DVR_SET_DOOR_STATUS_PLAN_TEMPLATE = 2107,

            /// <summary>
            /// 设置读卡器验证方式计划模板参数
            /// </summary>
            NET_DVR_SET_VERIFY_PLAN_TEMPLATE = 2137,

            /// <summary>
            /// 设置卡权限计划模板参数
            /// </summary>
            NET_DVR_SET_CARD_RIGHT_PLAN_TEMPLATE = 2139,


            /// <summary>
            /// 设置门状态计划参数
            /// </summary>
            NET_DVR_SET_DOOR_STATUS_PLAN = 2111,

            /// <summary>
            /// 设置读卡器验证计划参数
            /// </summary>
            NET_DVR_SET_CARD_READER_PLAN = 2143,

            /// <summary>
            /// 设置群组参数
            /// </summary>
            NET_DVR_SET_GROUP_CFG = 2113,

            /// <summary>
            /// 设置多重卡参数
            /// </summary>
            NET_DVR_SET_MULTI_CARD_CFG = 2115,

            /// <summary>
            /// 设置反潜回参数
            /// </summary>
            NET_DVR_SET_SNEAK_CFG = 2120,

            /// <summary>
            /// 设置读卡器反潜回参数
            /// </summary>
            NET_DVR_SET_CARD_READER_ANTI_SNEAK_CFG = 2147,

            /// <summary>
            /// 设置多门互锁参数
            /// </summary>
            NET_DVR_SET_MULTI_DOOR_INTERLOCK_CFG = 2122,

            /// <summary>
            /// 设置事件报警输入参数
            /// </summary>
            NET_DVR_SET_CASE_SENSOR_CFG = 2145,

            /// <summary>
            /// 设置手机关联门权限参数
            /// </summary>
            NET_DVR_SET_PHONE_DOOR_RIGHT_CFG = 2149,

            /// <summary>
            /// 设置事件/卡号联动配置参数
            /// </summary>
            NET_DVR_SET_EVENT_CARD_LINKAGE_CFG = 2154,

            /// <summary>
            /// 设置门禁主机参数
            /// </summary>
            NET_DVR_SET_ACS_CFG = 2160,


            /// <summary>
            /// 获取指纹参数
            /// </summary>
            NET_DVR_GET_FINGERPRINT_CFG = 2150,

            /// <summary>
            /// 设置指纹参数
            /// </summary>
            NET_DVR_SET_FINGERPRINT_CFG = 2151,

            /// <summary>
            /// 获取卡密码开门使能参数
            /// </summary>
            NET_DVR_GET_CARD_PASSWD_CFG = 2161,

            /// <summary>
            /// 设置卡密码开门使能参数
            /// </summary>
            NET_DVR_SET_CARD_PASSWD_CFG = 2162,
            /// <summary>
            /// 获取门状态计划模板参数
            /// </summary>
            NET_DVR_GET_DOOR_STATUS_PLAN_TEMPLATE = 2106,
            /// <summary>
            /// 获取读卡器验证方式计划模板参数
            /// </summary>
            NET_DVR_GET_VERIFY_PLAN_TEMPLATE = 2136,
            /// <summary>
            /// 获取卡权限计划模板参数
            /// </summary>
            NET_DVR_GET_CARD_RIGHT_PLAN_TEMPLATE = 2138,
            /// <summary>
            /// 获取门状态假日计划参数
            /// </summary>
            NET_DVR_GET_DOOR_STATUS_HOLIDAY_PLAN = 2102,
            /// <summary>
            /// 获取卡权限周计划参数
            /// </summary>
            NET_DVR_GET_CARD_RIGHT_WEEK_PLAN = 2126,
            /// <summary>
            /// 获取卡权限假日计划参数
            /// </summary>
            NET_DVR_GET_CARD_RIGHT_HOLIDAY_PLAN = 2130,
            /// <summary>
            /// 获取读卡器验证方式周计划参数
            /// </summary>
            NET_DVR_GET_VERIFY_WEEK_PLAN = 2124,
            /// <summary>
            ///  清空门禁主机参数
            /// </summary>
            NET_DVR_CLEAR_ACS_PARAM = 2118,
            /// <summary>
            /// 删除指纹参数
            /// </summary>
            NET_DVR_DEL_FINGERPRINT_CFG = 2152 

        }
        #endregion

        #region 读卡器类型
        /// <summary>
        /// 读卡器类型
        /// </summary>
        public enum CardReaderType
        {
            /// <summary>
            /// DS_K110XM_MK_C_CK
            /// </summary>
            DS_K110XM_MK_C_CK = 1,
            /// <summary>
            /// DS_K192AM_AMP
            /// </summary>
            DS_K192AM_AMP = 2,
            /// <summary>
            /// DS_K192BM_BMP
            /// </summary>
            DS_K192BM_BMP = 3,
            /// <summary>
            /// DS_K182AM_AMP
            /// </summary>
            DS_K182AM_AMP = 4,
            /// <summary>
            /// DS_K182BM_BMP
            /// </summary>
            DS_K182BM_BMP = 5

            //6- DS-K182AMF/ACF，7- 韦根或485不在线，8- DS-K1101M/MK，9- DS-K1101C/CK，10- DS-K1102M/MK/M-A，11- DS-K1102C/CK，12- DS-K1103M/MK，13- DS-K1103C/CK，14- DS-K1104M/MK，15- DS-K1104C/CK，16- DS-K1102S/SK/S-A，17- DS-K1102G/GK，18- DS-K1100S-B，19- DS-K1102EM/EMK，20- DS-K1102E/EK，21- DS-K1200EF，22- DS-K1200MF，23- DS-K1200CF，24- DS-K1300EF，25- DS-K1300MF，26- DS-K1300CF，27- DS-K1105E，28- DS-K1105M，29- DS-K1105C，30- DS-K182AMF，31- DS-K196AMF，32- DS-K194AMP，33- DS-K1T200EF/EF-C/MF-MF-C/CF/CF-C，34- DS-K1T300EF/EF-C/MF-MF-C/CF/CF-C
        }
        #endregion

        #region 远程配置
        /// <summary>
        /// 远程配置状态
        /// </summary>
        public enum RemoteSetState
        {
            /// <summary>
            /// 状态值
            /// </summary>
            NET_SDK_CALLBACK_TYPE_STATUS = 0,
            /// <summary>
            /// 进度值
            /// </summary>
            NET_SDK_CALLBACK_TYPE_PROGRESS,
            /// <summary>
            /// 信息数据
            /// </summary>
            NET_SDK_CALLBACK_TYPE_DATA
        }
        /// <summary>
        /// 门禁远程配置状态值
        /// </summary>
        public enum RemoteSetStatus
        {
            /// <summary>
            /// 表示获取和配置成功并且结束
            /// </summary>
            NET_SDK_CALLBACK_STATUS_SUCCESS = 1000, //成功
            /// <summary>
            /// 处理中
            /// </summary>
            NET_SDK_CALLBACK_STATUS_PROCESSING,
            /// <summary>
            /// 失败
            /// </summary>
            NET_SDK_CALLBACK_STATUS_FAILED,
            /// <summary>
            /// 异常
            /// </summary>
            NET_SDK_CALLBACK_STATUS_EXCEPTION
        }
        #endregion

        #region 需要修改的卡参数
        /// <summary>
        /// 需要修改的卡参数（dwModifyParamType 设置卡参数时有效）
        /// </summary>
        public enum ModifyParamType
        {
            /// <summary>
            /// 不修改
            /// </summary>
            CARD_PARAM_CARD_NO=0,

            /// <summary>
            ///  卡是否有效参数
            /// </summary>
            CARD_PARAM_CARD_VALID = 0x00000001,
            /// <summary>
            /// 有效期参数
            /// </summary>
            CARD_PARAM_VALID = 0x00000002,
            /// <summary>
            ///  卡类型参数
            /// </summary>
            CARD_PARAM_CARD_TYPE = 0x00000004,
            /// <summary>
            ///  门权限参数
            /// </summary>
            CARD_PARAM_DOOR_RIGHT = 0x00000008,
            /// <summary>
            ///  首卡参数
            /// </summary>
            CARD_PARAM_LEADER_CARD = 0x00000010,
            /// <summary>
            ///  最大刷卡次数参数
            /// </summary>
            CARD_PARAM_SWIPE_NUM = 0x00000020,
            /// <summary>
            ///  所属群组参数
            /// </summary>
            CARD_PARAM_GROUP = 0x00000040,
            /// <summary>
            ///  卡密码参数
            /// </summary>
            CARD_PARAM_PASSWORD = 0x00000080,
            /// <summary>
            ///  卡权限计划参数
            /// </summary>
            CARD_PARAM_RIGHT_PLAN = 0x00000100,
            /// <summary>
            ///  已刷卡次数
            /// </summary>
            CARD_PARAM_SWIPED_NUM = 0x00000200

        }
        #endregion

        #region 门禁状态
        /// <summary>
        /// 门禁状态
        /// </summary>
        public enum DoorStatus
        {
            /// <summary>
            /// 无效
            /// </summary>
            无效 = 0,
            /// <summary>
            /// 休眠
            /// </summary>
            休眠,
            /// <summary>
            /// 常开状态
            /// </summary>
            常开状态,
            /// <summary>
            /// 常闭状态
            /// </summary>
            常闭状态,
            /// <summary>
            /// 普通状态
            /// </summary>
            普通状态
        }
        #endregion

        #region 读卡器验证方式
        /// <summary>
        /// 读卡器验证方式
        /// </summary>
        public enum CardReadVerifyMode
        {
            /// <summary>
            /// 无效
            /// </summary>
            无效 = 0,
            /// <summary>
            /// 休眠
            /// </summary>
            休眠,
            /// <summary>
            /// 刷卡加密码_读卡器验证方式计划使用
            /// </summary>
            刷卡加密码,
            /// <summary>
            /// 刷卡_读卡器验证方式计划使用
            /// </summary>
            刷卡,
            /// <summary>
            /// 刷卡或密码_读卡器验证方式计划使用_
            /// </summary>
            刷卡或密码,
            /// <summary>
            /// 指纹
            /// </summary>
            指纹,
            /// <summary>
            /// 指纹加密码
            /// </summary>
            指纹加密码,
            /// <summary>
            /// 指纹或刷卡
            /// </summary>
            指纹或刷卡,
            /// <summary>
            /// 指纹加刷卡
            /// </summary>
            指纹加刷卡,
            /// <summary>
            /// 指纹加刷卡加密码_无先后顺序
            /// </summary>
            指纹加刷卡加密码
        }
        #endregion

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
    }
}
