using System;
using System.Runtime.InteropServices;
using static HikDeviceApi.VideoRecorder.HikVideoStruct;
using static HikDeviceApi.HikDelegate;
using static HikDeviceApi.HikEnum;
using static HikDeviceApi.HikStruct;

namespace HikDeviceApi
{
    /// <summary>
    /// 日 期:2015-11-24
    /// 作 者:痞子少爷
    /// 描 述:海康设备公用接口
    /// </summary>
    public static class HikApi
    {
        #region SDK操作
        /// <summary>
        /// 初始化SDK，调用其他SDK函数的前提。
        /// </summary>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_Init();
        /// <summary>
        /// 释放SDK资源，在结束之前最后调用。
        /// </summary>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_Cleanup();
        /// <summary>
        /// 设置SDK本地参数
        /// </summary>
        /// <param name="enumType">配置类型，不同的取值对应不同的SDK参数</param>
        /// <param name="lpInBuff">输入参数，不同的配置类型，输入参数对应不同的结构</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetSDKLocalCfg(NET_SDK_LOCAL_CFG_TYPE enumType, IntPtr lpInBuff);
        /// <summary>
        /// 获取SDK本地参数
        /// </summary>
        /// <param name="enumType">配置类型，不同的取值对应不同的SDK参数</param>
        /// <param name="lpInBuff">输出参数，不同的配置类型，输出参数对应不同的结构 </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSDKLocalCfg(NET_SDK_LOCAL_CFG_TYPE enumType, IntPtr lpInBuff);

        /// <summary>
        /// 获取SDK的版本信息
        /// </summary>
        /// <returns>SDK版本信息，2个高字节表示主版本，2个低字节表示次版本。如0x00030000：表示版本为3.0</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_GetSDKVersion();
        /// <summary>
        /// 获取SDK的版本号和build信息
        /// </summary>
        /// <returns>SDK的版本号和build信息。2个高字节表示版本号 ：25~32位表示主版本号，17~24位表示次版本号；2个低字节表示build信息。如0x03000101：表示版本号为3.0，build 号是0101。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_GetSDKBuildVersion();
        /// <summary>
        /// 获取当前SDK的状态信息
        /// </summary>
        /// <param name="pSDKState">状态信息结构</param>
        /// <returns>TRUE表示成功，FALSE表示失败.</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSDKState(NET_DVR_SDKSTATE pSDKState);
        /// <summary>
        /// 获取当前SDK的功能信息
        /// </summary>
        /// <param name="pSDKAbl">功能信息结构</param>
        /// <returns>TRUE表示成功，FALSE表示失败.</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSDKAbility(NET_DVR_SDKABL pSDKAbl);
        #endregion

        #region 用户注册

        /// <summary>
        /// 登录设备
        /// </summary>
        /// <param name = "sDVRIP" > 设备IP地址或是静态域名，字符数不大于128个</param>
        /// <param name = "wDVRPort" > 设备端口号 </param >
        /// <param name="sUserName">登录的用户名</param>
        /// <param name = "sPassword" > 用户密码 </param >
        /// <param name="lpDeviceInfo">设备信息</param>
        /// <returns>-1表示失败，其他值表示返回的用户ID值。该用户ID具有唯一性，后续对设备的操作都需要通过此ID实现。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_Login_V30(string sDVRIP, Int32 wDVRPort, string sUserName, string sPassword, ref NET_DVR_DEVICEINFO_V30 lpDeviceInfo);

        /// <summary>
        /// 登录设备（支持异步登录）
        /// </summary>
        /// <param name="pLoginInfo">登录参数，包括设备地址、登录用户、密码等</param>
        /// <param name="lpDeviceInfo">设备信息(同步登录即pLoginInfo中bUseAsynLogin为0时有效) </param>
        /// <returns>异步登录的状态、用户ID和设备信息通过NET_DVR_USER_LOGIN_INFO结构体中设置的回调函数(fLoginResultCallBack)返回。对于同步登录，接口返回-1表示登录失败，其他值表示返回的用户ID值</returns>
        /// <remarks>
        /// pLoginInfo中bUseAsynLogin为0时登录为同步模式，接口返回成功即表示登录成功；pLoginInfo中bUseAsynLogin为1时登录为异步模式，登录是否成功在输入参数设置的回调函数中返回。 
        /// DS-7116、DS-81xx、DS-90xx、DS-91xx等系列设备允许有32个注册用户名，且同时最多允许128个用户注册；DS-80xx等设备允许有16个注册用户名，且同时最多允许128个用户注册。 
        /// SDK支持2048个注册，返回UserID的取值范围为0 ~2047。 
        /// </remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_Login_V40(NET_DVR_USER_LOGIN_INFO pLoginInfo, ref NET_DVR_DEVICEINFO_V40 lpDeviceInfo);
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <param name="iUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_Logout(int iUserID);
        /// <summary>
        /// 激活设备
        /// </summary>
        /// <param name="sDVRIP">设备IP地址</param>
        /// <param name="wDVRPort">设备端口</param>
        /// <param name="lpActivateCfg">激活参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>出厂设备需要先激活，然后再使用激活使用的初始密码登录设备。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_ActivateDevice(string sDVRIP, int wDVRPort, NET_DVR_ACTIVATECFG lpActivateCfg);
        /// <summary>
        /// 通过解析服务器，获取设备的动态IP地址和端口号。
        /// </summary>
        /// <param name="sServerIP">解析服务器(IPServer或者hiDDNS)的IP地址或者域名</param>
        /// <param name="wServerPort">解析服务器的端口号。IP Server端口号为7071，hiDDNS服务器的端口号为80。</param>
        /// <param name="sDVRName">设备名称</param>
        /// <param name="wDVRNameLen">设备名称的长度</param>
        /// <param name="sDVRSerialNumber">设备的序列号</param>
        /// <param name="wDVRSerialLen">设备序列号的长度</param>
        /// <param name="sGetIP">获取到的设备IP地址指针</param>
        /// <param name="dwPort">获取到的设备端口号指针</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>
        /// 该接口中的设备名称和设备序列号不能同时为空。通过设备域名或者序列号解析出设备当前IP地址和端口，然后调用NET_DVR_Login_V40登录设备。支持的解析服务器有IPServer和hiDDNS。
        /// </remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRIPByResolveSvr_EX(string sServerIP, int wServerPort, string sDVRName, int wDVRNameLen, string sDVRSerialNumber, int wDVRSerialLen, IntPtr sGetIP, IntPtr dwPort
);


        #endregion

        #region 设备能力
        /// <summary>
        /// 获取设备支持的IPC协议表
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lpProtoList">IPC协议列表结构</param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetIPCProtoList(int lUserID, ref NET_DVR_IPC_PROTO_LIST lpProtoList);

        /// <summary>
        /// 获取设备能力集
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwAbilityType">能力类型，具体定义见下表</param>
        /// <param name="pInBuf">输入缓冲区指针（按照设备规定的能力参数的描述方式组合，可以是XML文本或结构体形式)</param>
        /// <param name="dwInLength">输入缓冲区的长度</param>
        /// <param name="pOutBuf">输出缓冲区指针（按照设备规定的能力集的描述方式，可以是XML文本或结构体形式)</param>
        /// <param name="dwOutLength">接收数据的缓冲区的长度</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDeviceAbility(int lUserID, uint dwAbilityType, IntPtr pInBuf, uint dwInLength, IntPtr pOutBuf, uint dwOutLength);

        #endregion

        #region 报警输出
        /// <summary>
        /// 获取设备报警输出
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lpAlarmOutState">报警输出状态</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmOut_V30(int lUserID, IntPtr lpAlarmOutState);
        /// <summary>
        /// 设置设备报警输出
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lAlarmOutPort">报警输出口。初始输出口从0开始，0x00ff表示全部模拟输出，0xff00表示全部数字输出，DS-90xx系列设备同时支持对IP接入的报警输出进行处理，对应32-95为数字报警输出。</param>
        /// <param name="lAlarmOutStatic"> 报警输出状态：0－停止输出，1－输出 </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAlarmOut(int lUserID, int lAlarmOutPort, int lAlarmOutStatic);
        #endregion

        #region Ip地址使用
        /// <summary>
        /// 获取所有IP，用于支持多网卡接口。
        /// </summary>
        /// <param name="strIP">存放IP的缓冲区，不能为空 </param>
        /// <param name="pValidNum">所有有效 IP 的数量</param>
        /// <param name="pEnableBind">是否绑定</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>该接口获取客户端本地多网卡的所有IP地址，可以通过接口NET_DVR_SetValidIP选择要使用的IP地址。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetLocalIP(byte[] strIP, ref uint pValidNum, ref bool pEnableBind);
        /// <summary>
        /// 选择使用哪个IP
        /// </summary>
        /// <param name="dwIPIndex">选择使用的IP下标，由NET_DVR_GetLocalIP获取</param>
        /// <param name="bEnableBind">是否绑定</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetValidIP(uint dwIPIndex, bool bEnableBind);
        #endregion

        #region 日志操作
        /// <summary>
        /// 启用日志文件写入接口
        /// </summary>
        /// <param name="bLogEnable">志的等级（默认为0）：0-表示关闭日志，1-表示只输出ERROR错误日志，2-输出ERROR错误信息和DEBUG调试信息，3-输出ERROR错误信息、DEBUG调试信息和INFO普通信息等所有信息 </param>
        /// <param name="strLogDir">日志文件的路径，windows默认值为"C:\\SdkLog\\"；linux默认值"/home/sdklog/"</param>
        /// <param name="bAutoDel">是否删除超出的文件数，默认值为TRUE </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetLogToFile(int bLogEnable, string strLogDir, bool bAutoDel);

        /// <summary>
        /// 日志备份
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwBackupType">备份类型：1- 按文件名备份录像文件，2- 按时间段备份录像文件，3- 备份图片，4- 恢复审讯事件，5- 备份日志 </param>
        /// <param name="lpBackupBuff">指向备份参数指针</param>
        /// <param name="dwBackupBuffSize">备份参数大小</param>
        /// <returns>-1失败，0-510的返回句柄值作为NET_DVR_GetBackupProgress，NET_DVR_StopBackup的参数。</returns>
        /// <remarks>不同的备份类型（dwBackupType）对应不同的备份参数结构体（lpBackupBuff）</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_Backup(int lUserID, uint dwBackupType, IntPtr lpBackupBuff, uint dwBackupBuffSize);
        /// <summary>
        /// 停止备份
        /// </summary>
        /// <param name="lHandle">NET_DVR_BackupByName或NET_DVR_BackupByTime的返回值，或者NET_DVR_BackupPicture的返回值.</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopBackup(int lHandle);
        /// <summary>
        /// 获取备份的进度
        /// </summary>
        /// <param name="lHandle">NET_DVR_BackupByName或NET_DVR_BackupByTime的返回值</param>
        /// <param name="pState">当前备份的进度，进度值的取值范围为[0,100]</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>在进度为100或者备份出错时, 需调用NET_DVR_StopBackup()停止备份。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetBackupProgress(int lHandle, uint pState);

        #endregion

        #region 超时重连

        /// <summary>
        /// 设置接收超时时间
        /// </summary>
        /// <param name="nRecvTimeOut">接收超时时间，单位毫秒，默认为5000，最小为1000毫秒。</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetRecvTimeOut(uint nRecvTimeOut);
        /// <summary>
        /// 设置重连时间
        /// </summary>
        /// <param name="dwInterval">重连间隔，单位:毫秒</param>
        /// <param name="bEnableRecon">是否重连，0-不重连，1-重连，参数默认值为1</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetReconnect(uint dwInterval, int bEnableRecon);
        /// <summary>
        /// 设置连接时间
        /// </summary>
        /// <param name="dwWaitTime">超时时间，单位毫秒，取值范围[300,75000]，实际最大超时时间因系统的connect超时时间而不同</param>
        /// <param name="dwTryTimes">连接尝试次数（保留）</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConnectTime(uint dwWaitTime, uint dwTryTimes);
        #endregion

        #region 获取错误
        /// <summary>
        /// 获取最后的错误码
        /// </summary>
        /// <returns>返回错误码</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern uint NET_DVR_GetLastError();
        /// <summary>
        /// 返回最后操作的错误码信息
        /// </summary>
        /// <returns>返回错误码信息</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern string NET_DVR_GetErrorMsg(ref int pErrorNo);
        #endregion

        #region 配置设备

        /// <summary>
        /// 获取设备的配置信息
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwCommand">设备配置命令</param>
        /// <param name="lChannel">通道号，不同的命令对应不同的取值，如果该参数无效则置为0xFFFFFFFF即可。</param>
        /// <param name="lpOutBuffer">接收数据的缓冲指针</param>
        /// <param name="dwOutBufferSize">接收数据的缓冲长度(以字节为单位)，不能为0</param>
        /// <param name="lpBytesReturned">实际收到的数据长度指针，不能为NULL </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpOutBuffer, uint dwOutBufferSize, ref uint lpBytesReturned);
        /// <summary>
        /// 设置设备的配置信息
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="dwCommand">设备配置命令</param>
        /// <param name="lChannel">通道号，不同的命令对应不同的取值，如果该参数无效则置为0xFFFFFFFF即可。</param>
        /// <param name="lpInBuffer">输入数据的缓冲指针</param>
        /// <param name="dwInBufferSize">输入数据的缓冲长度(以字节为单位)</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>

        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpInBuffer, uint dwInBufferSize);

        /// <summary>
        /// 导出配置文件
        /// </summary>
        /// <param name="lUserID">用户ID号，NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="sFileName">存放配置参数的缓冲区</param>
        /// <param name="dwInSize">缓冲区大小</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConfigFile_EX(int lUserID, string sFileName, uint dwInSize);
        /// <summary>
        /// 批量获取设备配置信息（带发送数据）
        /// <para>
        /// 该接口是带有发送数据的批量获取配置信息的通用接口。lpInBuffer指定需要获取的dwCount个监控点信息，lpOutBuffer保存获取得到的dwCount个监控点的配置信息。 不同的获取功能对应不同的结构体和命令号
        /// </para>
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwCommand">设备配置命令，参见配置命令</param>
        /// <param name="dwCount">要设置的设备个数，设为1</param>
        /// <param name="lpInBuffer">配置条件缓冲区</param>
        /// <param name="dwInBufferSize">缓冲区长度 </param>
        /// <param name="lpStatusList">错误信息列表，和要查询的监控点一一对应，例如lpStatusList[2]就对应lpInBuffer[2]，由用户分配内存，每个错误信息为4个字节(1个32位无符号整数值)，参数值：0或者1表示成功，其他值为失</param>
        /// <param name="lpOutBuffer">设备返回的参数内容，和要查询的监控点一一对应。如果某个监控点对应的lpStatusList信息为大于1的值，对应lpOutBuffer的内容就是无效的 </param>
        /// <param name="dwOutBufferSize">输出缓冲区大小 </param>
        /// <returns>TRUE表示成功，但不代表每一个配置都成功，哪一个成功，对应查看lpStatusList[n]值；FALSE表示全部失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDeviceConfig(int lUserID, uint dwCommand, uint dwCount, IntPtr lpInBuffer, uint dwInBufferSize, IntPtr lpStatusList, IntPtr lpOutBuffer, uint dwOutBufferSize);
        /// <summary>
        /// 批量设置设备配置信息（带发送数据）
        /// <para>
        /// 该接口是带有发送数据的批量设置配置信息的通用接口。lpInBuffer指定需要设置的dwCount个监控点信息，lpOutBuffer保存将要设置的dwCount个监控点的配置信息。 不同的设置功能对应不同的结构体和命令号
        /// </para>
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="dwCommand">设备配置命令，参见配置命令</param>
        /// <param name="dwCount">一次要设置的子设备个数</param>
        /// <param name="lpInBuffer">配置条件缓冲区 </param>
        /// <param name="dwInBufferSize">配置条件缓冲区长度</param>
        /// <param name="lpStatusList"> 错误信息列表，和要查询的监控点一一对应，例如lpStatusList[2]就对应lpInBuffer[2]，由用户分配内存，每个错误信息为4个字节(1个32位无符号整数值)，参数值：0或者1表示成功，其他值为失败对应的错误号</param>
        /// <param name="lpInParamBuffer"> 需要设置给设备的参数内容，和要查询的监控点一一对应。如果某个监控点对应的lpStatusList信息为大于1的值，表示对应的lpInBuffer设置失败，为0或1则表示设置成功</param>
        /// <param name="dwInParamBufferSize"> 设置内容缓冲区大小 </param>
        /// <returns>TRUE表示成功，但不代表每一个配置都成功，哪一个成功，对应查看lpStatusList[n]值；FALSE表示全部失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDeviceConfig(int lUserID, uint dwCommand, uint dwCount, IntPtr lpInBuffer, uint dwInBufferSize, IntPtr lpStatusList, IntPtr lpInParamBuffer, uint dwInParamBufferSize);

        /// <summary>
        /// 批量设置设备配置信息（带发送数据）
        /// <para>
        /// 该接口是带有发送数据的批量设置监控点配置信息的通用接口扩展接口，支持设置接收数据超时时间。不同的设置功能对应不同的结构体和命令号
        /// </para>
        /// </summary>
        /// <param name="lUserID"> NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="dwCommand">设备配置命令</param>
        /// <param name="dwCount"> 一次要设置的监控点个数，0和1都表示1个监控点信息，2表示2个监控点信息，以此递增，最大64个 </param>
        /// <param name="lpInParam">输入参数缓冲区，其中参数取值根据不同的dwCommand而不同</param>
        /// <param name="lpOutParam">输出参数缓冲区，其中参数取值根据不同的dwCommand而不同</param>
        /// <returns>TRUE表示成功，但不代表每一个配置都成功，哪一个成功，对应查看lpOutParam->lpStatusList值；FALSE表示全部失败。接口返回失败请调用NET_DVR_GetLastError获取错误码，通过错误码判断出错原因。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDeviceConfigEx(Int32 lUserID, uint dwCommand, uint dwCount, ref NET_DVR_IN_PARAM lpInParam, ref NET_DVR_OUT_PARAM lpOutParam);
        /// <summary>
        /// 导出配置文件
        /// </summary>
        /// <param name="lUserID">用户ID号，NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="sOutBuffer">存放配置参数的缓冲区</param>
        /// <param name="dwOutSize">缓冲区大小</param>
        /// <param name="pReturnSize">实际获得的缓冲区大小</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>当sOutBuffer = NULL、dwOutSize = 0且pReturnSize != NULL时用于获取参数配置文件的所需的缓冲区长度；当sOutBuffer != NULL且dwOutSize != 0时用于获取参数配置文件的所需的缓冲区内容。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile_V30(int lUserID, string sOutBuffer, uint dwOutSize, ref uint pReturnSize);

        #endregion

        #region 恢复重启
        /// <summary>
        /// 恢复设备默认参数
        /// </summary>
        /// <param name="lUserID">用户ID号，NET_DVR_Login_V40等登录接口的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_RestoreConfig(int lUserID);
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <param name="lUserID">用户ID号，NET_DVR_Login_V40等登录接口的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_ShutDownDVR(int lUserID);
        /// <summary>
        /// 重启设备
        /// </summary>
        /// <param name="lUserID"> 用户ID号，NET_DVR_Login_V40等登录接口的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_RebootDVR(int lUserID);

        #endregion

        #region 远程控制
        /// <summary>
        /// 远程控制
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwCommand">控制命令</param>
        /// <param name="lpInBuffer">输入参数，具体内容跟控制命令相关</param>
        /// <param name="dwInBufferSize">输入参数长度</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>
        /// 用于手动检测设备是否在线，接口返回TRUE表示在线，FALSE表示与设备通信失败或者返回错误状态。
        /// 也可对不同设备进行其他操作，参见枚举RemoteCommand
        /// </remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_RemoteControl(int lUserID, uint dwCommand, IntPtr lpInBuffer, uint dwInBufferSize);
        #endregion

        #region 报警布防

        /// <summary>
        /// 建立报警上传通道，获取报警等信息
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值 </param>
        /// <param name="lpSetupParam">报警布防参数</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_CloseAlarmChan_V30函数的句柄参数</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan_V41(int lUserID, ref HikStruct.NET_DVR_SETUPALARM_PARAM lpSetupParam);

        /// <summary>
        /// 撤销报警上传通道
        /// </summary>
        /// <param name="lAlarmHandle">NET_DVR_SetupAlarmChan_V30或者NET_DVR_SetupAlarmChan_V41的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseAlarmChan_V30(int lAlarmHandle);
        ///// <summary>
        ///// 建立报警上传通道，获取报警等信息。
        ///// </summary>
        ///// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        ///// <returns>-1表示失败，其他值作为NET_DVR_CloseAlarmChan_V30函数的句柄参数</returns>
        //[DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        //public static extern int NET_DVR_SetupAlarmChan_V30(int lUserID);


        /// <summary>
        /// 启动监听，接收设备主动上传的报警等信息（支持多线程）。
        /// </summary>
        /// <param name="sLocalIP">PC机本地IP地址，可以置为NULL</param>
        /// <param name="wLocalPort">PC本地监听端口号。由用户设置，必须和设备端设置的一致。</param>
        /// <param name="DataCallback">回调函数</param>
        /// <param name="pUserData">用户数据</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_StopListen_V30函数的句柄参数。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_StartListen_V30(string sLocalIP, short wLocalPort, MSGCallBack DataCallback, IntPtr pUserData);
        /// <summary>
        /// 停止监听（支持多线程）。
        /// </summary>
        /// <param name="lListenHandle">监听句柄，NET_DVR_StartListen_V30的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopListen_V30(Int32 lListenHandle);

        #endregion

        #region  回调函数
        /// <summary>
        /// 注册接收异常、重连等消息的窗口句柄或回调函数。
        /// </summary>
        /// <param name="nMessage">消息</param>
        /// <param name="hWnd">接收异常信息消息的窗口句柄</param>
        /// <param name="cbExceptionCallBack">接收异常消息的回调函数</param>
        /// <param name="pUser">用户数据</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetExceptionCallBack_V30(int nMessage, IntPtr hWnd, fExceptionCallBack cbExceptionCallBack, IntPtr pUser);
        /// <summary>
        /// 注册回调函数，接收设备报警消息等。
        /// </summary>
        /// <param name="fMessageCallBack"> 回调函数 </param>
        /// <param name="pUser">用户数据</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V31(MSGCallBack_V31 fMessageCallBack, IntPtr pUser);
        /// <summary>
        /// 注册回调函数，接收设备报警消息等。
        /// </summary>
        /// <param name="index">回调函数索引，取值范围：[0,15] </param>
        /// <param name="fMessageCallBack"> 回调函数 </param>
        /// <param name="pUser">用户数据</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V50(int index, MSGCallBack fMessageCallBack, IntPtr pUser);
        /// <summary>
        /// 注册回调函数，接收设备报警消息等（同时回调设备IP地址和与设备间连接的设备端口号，用以区分不同设备）。
        /// </summary>
        /// <param name="cbMessCallBack">回调函数 </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack_NEW(fMessCallBack_NEW cbMessCallBack);


        /// <summary>
        /// 注册回调函数，接收设备报警消息等。
        /// </summary>
        /// <param name="fMessageCallBack"> 回调函数 </param>
        /// <param name="pUser">用户数据</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V30(MSGCallBack fMessageCallBack, IntPtr pUser);
        #endregion

        #region 升级固件
        ///// <summary>
        ///// 远程升级设备固件
        ///// </summary>
        ///// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        ///// <param name="sFileName">升级的文件路径（包括文件名）。路径长度和操作系统有关，sdk不做限制，windows默认路径长度小于等于256字节（包括文件名在内）。 </param>
        ///// <returns>-1表示失败，其他值作为NET_DVR_GetUpgradeState等函数的参数。</returns>
        //[DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        //public static extern int NET_DVR_Upgrade(int lUserID, string sFileName);
        /// <summary>
        /// 远程升级设备固件
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="dwUpgradeType">升级类型</param>
        /// <param name="sFileName">升级的文件路径（包括文件名）。路径长度和操作系统有关，sdk不做限制，windows默认路径长度小于等于256字节（包括文件名在内）。 </param>
        /// <param name="pInbuffer">升级条件缓冲区，升级类型为1时有效，对应4字节设备编号（0为升级主机，1~8为升级设备下挂的对应485地址的读卡器）。</param>
        /// <param name="dwBufferLen">缓冲区大小</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_GetUpgradeState等函数的参数。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_Upgrade_V40(int lUserID, uint dwUpgradeType, string sFileName, IntPtr pInbuffer, uint dwBufferLen);
        /// <summary>
        /// 获取远程升级的状态
        /// </summary>
        /// <param name="lUpgradeHandle">NET_DVR_Upgrade_V40或NET_DVR_Upgrade的返回值</param>
        /// <returns>-1表示失败，其他值定义参照UpgradeState枚举</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_GetUpgradeState(int lUpgradeHandle);
        /// <summary>
        /// 获取远程升级的进度
        /// </summary>
        /// <param name="lUpgradeHandle">NET_DVR_Upgrade_V40或NET_DVR_Upgrade的返回值</param>
        /// <returns>-1表示失败，0～100表示升级进度。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_GetUpgradeProgress(int lUpgradeHandle);
        /// <summary>
        /// 关闭远程升级句柄，释放资源。
        /// </summary>
        /// <param name="lUpgradeHandle">NET_DVR_Upgrade_V40或NET_DVR_Upgrade的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseUpgradeHandle(int lUpgradeHandle);
        /// <summary>
        /// 设置远程升级时网络环境
        /// </summary>
        /// <param name="dwEnvironmentLevel">网络环境级别</param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetNetworkEnvironment(uint dwEnvironmentLevel);
        #endregion

        #region 设备巡检
        /// <summary>
        /// 启动设备状态巡检
        /// </summary>
        /// <param name="pParams">设备工作状态巡检参数 NET_DVR_CHECK_DEV_STATE</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>启动后，SDK定时巡检设备，获取到的设备状态信息在结构体的回调函数中返回。相当于实现定时调用NET_DVR_GetDeviceConfig(命令：NET_DVR_GET_WORK_STATUS)。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ThrowOnUnmappableChar = false)]
        public static extern bool NET_DVR_StartGetDevState(IntPtr pParams);//NET_DVR_CHECK_DEV_STATE

        /// <summary>
        /// 停止设备状态巡检
        /// </summary>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopGetDevState();

        #endregion

        #region 设备状态
        ///// <summary>
        ///// 获取设备的工作状态
        ///// </summary>
        ///// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        ///// <param name="pWorkState">获取的设备工作状态结构体参数</param>
        ///// <returns>TRUE表示成功，FALSE表示失败。</returns>
        //[DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        //public static extern bool NET_DVR_GetDVRWorkState_V30(int lUserID, IntPtr pWorkState);
        /// <summary>
        /// 获取设备的工作状态
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        /// <param name="pWorkState">获取的设备工作状态结构体参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRWorkState_V30(int lUserID, ref NET_DVR_WORKSTATE_V30 pWorkState);
        /// <summary>
        /// 获取设备的工作状态
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值 </param>
        /// <param name="lpWorkState">获取的设备工作状态结构体参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRWorkState(int lUserID, ref NET_DVR_WORKSTATE lpWorkState);
        /// <summary>
        /// 获取设备状态。 
        /// 全部获取时dwCount置为0xffffffff，lpInBuffer置为NULL，dwInBufferSize置为0，lpStatusList置为NULL。lpOutBuffer前面4个字节为个数(N)，后面为设备返回的N个信息内容，如果设置的lpOutBuffer缓冲区不足，仅返回部分信息，可以根据返回的个数（前4字节的值）重新获取。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="dwCommand">获取设备状态的命令值 </param>
        /// <param name="dwCount">要获取的状态个数 </param>
        /// <param name="lpInBuffer">状态获取条件缓冲区</param>
        /// <param name="dwInBufferSize">缓冲区长度</param>
        /// <param name="lpStatusList">错误信息列表，和要查询的监控点一一对应，例如lpStatusList[2]就对应lpInBuffer[2]，由用户分配内存，每个错误信息为4个字节(1个32位无符号整数值)，参数值：0- 成功，大于0- 失败</param>
        /// <param name="lpOutBuffer">设备返回的状态内容，和要查询的监控点一一对应。如果某个监控点对应的lpStatusList信息为大于0值，对应lpOutBuffer的内容就是无效的</param>
        /// <param name="dwOutBufferSize">输出缓冲区大小 </param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDeviceStatus(int lUserID, int dwCommand, uint dwCount, IntPtr lpInBuffer, uint dwInBufferSize, ref IntPtr lpStatusList, ref IntPtr lpOutBuffer, uint dwOutBufferSize);

        #endregion

        #region 流量监测
        /// <summary>
        /// 开始网络流量检测
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="pFlowTest">网络流量参数</param>
        /// <param name="fFlowTestCallback">网络流量检测回调函数</param>
        /// <param name="pUser">用户数据</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_StopNetworkFlowTest等函数的句柄参数。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_StartNetworkFlowTest(int lUserID, NET_DVR_FLOW_TEST_PARAM pFlowTest, FLOWTESTCALLBACK fFlowTestCallback, IntPtr pUser);
        /// <summary>
        /// 停止网络流量检测
        /// </summary>
        /// <param name="lHandle">NET_DVR_StartNetworkFlowTest的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopNetworkFlowTest(int lHandle);

        #endregion
    }
}
