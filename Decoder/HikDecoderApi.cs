using System;
using System.Runtime.InteropServices;
using static HikDeviceApi.Decoder.HikDecoderStruct;
using static HikDeviceApi.Decoder.HikDecoderDelegate;
using static HikDeviceApi.HikStruct;

namespace HikDeviceApi.Decoder
{
    /// <summary>
    /// 日 期:2015-07-07
    /// 作 者:痞子少爷
    /// 描 述:海康多路解码器接口
    /// </summary>
    public static class HikDecoderApi
    {
     
        
        #region 远程回放
        /// <summary>
        /// 设置解码设备远程回放文件配置
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDecChanNum">解码通道</param>
        /// <param name="lpInter">远程回放文件参数结构体</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>

        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetRemotePlay(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_REMOTE_PLAY lpInter);
        /// <summary>
        /// 远程回放文件控制
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDecChanNum">解码通道</param>
        /// <param name="dwControlCode">控制命令,参见RemotePlayControl枚举</param>
        /// <param name="dwInValue">设置参数</param>
        /// <param name="LPOutValue">获取到的参数指针</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        ///<remarks>接口中的dwInValue和lpOutValue参数根据不同的命令号决定是否输入和输出，如当进行NET_DVR_PLAYSETPOS命令操作时，需要对dwInValue参数进行赋值</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetRemotePlayControl(int lUserID, uint dwDecChanNum, uint dwControlCode, uint dwInValue, ref uint LPOutValue);
        /// <summary>
        /// 获取回放状态
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDecChanNum">解码通道</param>
        /// <param name="lpOuter">回放状态</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>多路解码器远程连接前端设备完成按文件名/时间回放文件，并能够获取相应回放状态，进行回放控制等。按时间回放不支持进度控制；由于回放控制命令是转发实现，存在一定的延迟，因此，回放控制命令不宜过于频繁调用，具体视网络状况而定，当把获取的状态作为客户端处理依据时应考虑网络转发的延迟因素；按时间回放时，获取回放状态所得到的文件信息是当前播放的单个片段的信息，并非整个时间范围内全部片段的信息，判断播放是否结束使用NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS结构中的dwCurDataType成员。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetRemotePlayStatus(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS lpOuter);
        #endregion

        #region 解码设置
        /// <summary>
        /// 多路解码器显示通道控制
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDispChanNum">显示通道，从能力集获取</param>
        /// <param name="dwDispChanCmd">显示通道命令，1-显示通道放大某个窗口，2-显示通道窗口还原</param>
        /// <param name="dwCmdParam">命令参数，子窗口号</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixDiaplayControl(int lUserID, uint dwDispChanNum, uint dwDispChanCmd, uint dwCmdParam);

        /// <summary>
        /// 设置当前解码通道开关
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="dwDecChanNum">解码通道 </param>
        /// <param name="dwEnable">0表示关闭；1表示打开</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetDecChanEnable(int lUserID, uint dwDecChanNum, uint dwEnable);
        /// <summary>
        /// 获取当前解码通道开关
        ///      解码通道的解码开关，用于控制解码通道的解码过程，设置此开关为关时，无论当前解码通道是处在动态解码还是轮巡过程，都将停止解码，生效后该通道处于黑屏状态或者最后一帧画面；设置开关为开时，将恢复先前过程。
        /// 提示： 此功能可与轮巡，轮巡开关等配合使用。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDecChanNum">解码通道</param>
        /// <param name="lpdwEnable">指向DWORD的指针，取出的值0表示关闭，1表示打开</param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanEnable(int lUserID, uint dwDecChanNum, IntPtr lpdwEnable);
        /// <summary>
        /// 获取当前解码通道开关
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="dwDecChanNum">解码通道</param>
        /// <param name="lpdwEnable">指向DWORD的指针，取出的值0表示关闭，1表示打开</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>
        /// 解码通道的解码开关，用于控制解码通道的解码过程，设置此开关为关时，无论当前解码通道是处在动态解码还是轮巡过程，都将停止解码，生效后该通道处于黑屏状态或者最后一帧画面；设置开关为开时，将恢复先前过程。
        /// 提示： 此功能可与轮巡，轮巡开关等配合使用。
        /// </remarks>

        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanEnable(int lUserID, uint dwDecChanNum, ref uint lpdwEnable);
        /// <summary>
        /// 获取当前解码通道状态
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDecChanNum">解码通道</param>
        /// <param name="lpInter">解码通道状态</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>

        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanStatus(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_CHAN_STATUS lpInter);
        /// <summary>
        /// 启动动态解码。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="dwDecChanNum">解码通道。对于DS-6400HD-S，该参数为窗口号。</param>
        /// <param name="lpDynamicInfo">动态解码参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>

        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStartDynamic_V41(int lUserID, uint dwDecChanNum, ref NET_DVR_PU_STREAM_CFG_V41 lpDynamicInfo);
        /// <summary>
        /// 获取当前解码通道信息
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="dwDecChanNum">显示通道，从能力集获取</param>
        /// <param name="lpOuter">显示通道配置参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanInfo_V41(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_CHAN_INFO_V41 lpOuter);
        /// <summary>
        /// 获取解码器显示通道配置
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDispChanNum">显示通道，从能力集获取</param>
        /// <param name="lpDisplayCfg">显示通道配置参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDisplayCfg_V41(int lUserID, uint dwDispChanNum, ref NET_DVR_MATRIX_VOUTCFG lpDisplayCfg);
        /// <summary>
        /// 设置解码器显示通道配置
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDispChanNum">显示通道，从能力集获取</param>
        /// <param name="lpDisplayCfg">显示通道配置参数</param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetDisplayCfg_V41(int lUserID, uint dwDispChanNum, ref NET_DVR_MATRIX_VOUTCFG lpDisplayCfg);
        /// <summary>
        /// 停止动态解码
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDecChanNum">解码通道。对于DS-6400HD-S，该参数为窗口号。</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStopDynamic(int lUserID, uint dwDecChanNum);
        /// <summary>
        /// 多路解码器启动被动解码
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDecChanNum">解码通道</param>
        /// <param name="lpPassiveMode">被动解码参数</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_MatrixSendData等函数的参数。</returns>

        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_MatrixStartPassiveDecode(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_PASSIVEMODE lpPassiveMode);
        /// <summary>
        /// 向被动解码通道发送数据
        /// </summary>
        /// <param name="lPassiveHandle">NET_DVR_MatrixStartPassiveDecode的返回值</param>
        /// <param name="pSendBuf">发送数据缓冲区</param>
        /// <param name="dwBufSize"> 发送缓冲区大小，发送数据大小需小于256K字节，推荐30K字节 </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSendData(int lPassiveHandle, IntPtr pSendBuf, uint dwBufSize);
        /// <summary>
        /// 多路解码器停止被动解码
        /// </summary>
        /// <param name="lPassiveHandle">NET_DVR_MatrixStartPassiveDecode的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStopPassiveDecode(int lPassiveHandle);
        /// <summary>
        /// 获取多路解码器被动解码状态
        /// </summary>
        /// <param name="lPassiveHandle"> NET_DVR_MatrixStartPassiveDecode的返回值</param>
        /// <returns>-1表示失败，1-发送成功，2-暂停发送，3-恢复发送，4-错误，5-心跳信息。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetPassiveDecodeStatus(int lPassiveHandle);
        /// <summary>
        /// 被动解码播放控制
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        /// <param name="dwDecChanNum">解码通道号</param>
        /// <param name="lpPassiveMode">解码通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_MatrixPassiveDecodeControl(int lUserID, uint dwDecChanNum, ref NET_DVR_PASSIVEDECODE_CONTROL lpPassiveMode);

        #endregion

        #region 语音控制
        /// <summary>
        /// 启动语音对讲(Windows 32位系统支持)。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwVoiceChan">语音通道号。对于设备本身的语音对讲通道，从1开始；对于设备的IP通道，为登录返回的起始对讲通道号(byStartDTalkChan) + IP通道索引，例如客户端通过NVR跟其IP Channel02所接前端IPC进行对讲，则dwVoiceChan=byStartDTalkChan+2 </param>
        /// <param name="bNeedCBNoEncData">需要回调的语音数据类型：0- 编码后的语音数据，1- 编码前的PCM原始数据</param>
        /// <param name="fVoiceDataCallBack">音频数据回调函数</param>
        /// <param name="pUser">用户数据指针</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_StopVoiceCom等函数的句柄参数。</returns>
        /// <remarks>在调用开始语音对讲之前可先配置设备的语音对讲音频编码类型，即可先调用参数配置中的NET_DVR_COMPRESSION_AUDIO 结构配置。</remarks>

        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_V30(int lUserID, uint dwVoiceChan, bool bNeedCBNoEncData, VOICEDATACALLBACKV30 fVoiceDataCallBack, IntPtr pUser);
        /// <summary>
        /// 设置语音对讲客户端的音量
        /// </summary>
        /// <param name="lVoiceComHandle">NET_DVR_StartVoiceCom或NET_DVR_StartVoiceCom_V30的返回值</param>
        /// <param name="wVolume">设置音量，取值范围[0,0xffff]</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVoiceComClientVolume(int lVoiceComHandle, ushort wVolume);

        /// <summary>
        /// 停止语音对讲或者语音转发
        /// </summary>
        /// <param name="lVoiceComHandle">NET_DVR_StartVoiceCom或NET_DVR_StartVoiceCom_V30、NET_DVR_StartVoiceCom_MR或NET_DVR_StartVoiceCom_MR_V30的返回值 </param>
        /// <returns>TRUE表示成功,FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopVoiceCom(int lVoiceComHandle);
        /// <summary>
        /// 启动语音转发，获取编码后的音频数据。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwVoiceChan">语音通道号。对于设备本身的语音对讲通道，从1开始；对于设备的IP通道，为登录返回的起始对讲通道号(byStartDTalkChan) + IP通道索引，例如客户端通过NVR跟其IP Channel02所接前端IPC进行对讲，则dwVoiceChan=byStartDTalkChan+2</param>
        /// <param name="fVoiceDataCallBack">音频数据回调函数，得到的数据是编码以后的音频数据，需调用我们提供的音频解码函数（详见音频编解码章节的说明）后可得到PCM数据 </param>
        /// <param name="pUser">用户数据指针</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_VoiceComSendData、NET_DVR_StopVoiceCom等函数的句柄参数。</returns>
        /// <remarks>在调用开始语音转发之前可先配置设备的音频编码类型，即可先调用参数配置中的NET_DVR_COMPRESSION_AUDIO 结构配置。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_MR_V30(int lUserID, uint dwVoiceChan, VOICEDATACALLBACKV30 fVoiceDataCallBack, IntPtr pUser);
        /// <summary>
        /// 转发语音数据
        /// </summary>
        /// <param name="lVoiceComHandle">NET_DVR_StartVoiceCom_MR或NET_DVR_StartVoiceCom_MR_V30的返回值</param>
        /// <param name="pSendBuf">存放语音数据的缓冲区</param>
        /// <param name="dwBufSize">语音数据大小。当前是G722音频编码类型时，每次发送的数据为80字节；当前是G711音频编码类型时，每次发送的数据为160字节。</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>该接口实现将获取到的经过编码后的音频数据转发给设备。音频编码类型不同，采用不同的接口实现音频数据编解码。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_VoiceComSendData(int lVoiceComHandle, string pSendBuf, uint dwBufSize);
        /// <summary>
        /// 启动语音广播的PC端声音捕获(Windows 32位系统支持)
        /// </summary>
        /// <param name="fVoiceAudioStart">语音广播音频数据回调函数</param>
        /// <param name="pUser">用户数据指针</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>实现语音广播功能需先调用NET_DVR_ClientAudioStart_V30或者NET_DVR_ClientAudioStart接口采集本地PC的音频数据，再调用NET_DVR_AddDVR或者NET_DVR_AddDVR_V30逐个添加设备同时将采集到的数据发送给设备。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStart_V30(VOICEAUDIOSTART fVoiceAudioStart, IntPtr pUser);

        /// <summary>
        /// 停止语音广播的PC端声音捕获
        /// </summary>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStop();

        /// <summary>
        /// 添加设备的某个语音通道到可以接收PC端声音的广播组
        /// </summary>
        /// <param name="lUserID"> NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwVoiceChan">语音通道号，从1开始</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_DelDVR_V30等函数的参数。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_AddDVR_V30(int lUserID, uint dwVoiceChan);

        /// <summary>
        /// 从可接收PC机声音的广播组里删除该设备的语音通道
        /// </summary>
        /// <param name="lVoiceHandle">NET_DVR_AddDVR_V30的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_DelDVR_V30(int lVoiceHandle);

        #endregion

        #region 透明通道
        /// <summary>
        /// 建立透明通道
        /// </summary>
        /// <param name="lUserID"> NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="lSerialPort"> 串口号：1－232串口；2－485串口 </param>
        /// <param name="fSerialDataCallBack">回调函数</param>
        /// <param name="dwUser">用户数据</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_SerialSend等函数的句柄参数。</returns>
        /// <remarks>如果串口接的外设不支持双工（全双工或者半双工），设备不会有数据返回给SDK。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStart(int lUserID, int lSerialPort, SERIALDATACALLBACK fSerialDataCallBack, uint dwUser);

        /// <summary>
        /// 通过透明通道向设备串口发送数据
        /// </summary>
        /// <param name="lSerialHandle">NET_DVR_SerialStart的返回值</param>
        /// <param name="lChannel">串口号，从1开始</param>
        /// <param name="pSendBuf">发送数据的缓冲区指针</param>
        /// <param name="dwBufSize">缓冲区的大小，最多1016字节</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialSend(int lSerialHandle, int lChannel, string pSendBuf, uint dwBufSize);
        /// <summary>
        /// 断开透明通道
        /// </summary>
        /// <param name="lSerialHandle">NET_DVR_SerialStart的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStop(int lSerialHandle);
        /// <summary>
        /// 直接向232串口发送数据，不需要建立透明通道。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="pSendBuf">发送数据的缓冲区指针</param>
        /// <param name="dwBufSize">缓冲区的大小，最多1016字节</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SendTo232Port(int lUserID, string pSendBuf, uint dwBufSize);
        /// <summary>
        /// 直接向串口发送数据，不需要建立透明通道。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwSerialPort">串口类型：1-232，2-485 </param>
        /// <param name="dwSerialIndex">表示第几个232或者485，从1开始</param>
        /// <param name="pSendBuf">发送数据的缓冲区指针 </param>
        /// <param name="dwBufSize">缓冲区的大小，最多1016字节</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SendToSerialPort(int lUserID, uint dwSerialPort, uint dwSerialIndex, string pSendBuf, uint dwBufSize);

        /// <summary>
        /// 设置透明通道信息
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lpTranInfo">透明通道参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>
        /// 此处透明通道配置是配置解码器与前端设备之间建立网络透明通道，而不是客户端与解码器之间建立透明通道，多路解码器本身不支持与客户端建立232透明通道和485透明通道，多路解码器本地串口只能作为串口控制台接入（通过RS232）或是控制键盘等输入性设备接入（通过RS232/RS485）。
        /// 提示： 目前多路解码器支持最多建立64条透明通道，包括232透明通道和485透明通道，但其中只有最多一条232全双工透明通道和最多一条485全双工透明通道，可以不设置全双工透明通道。
        /// </remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetTranInfo_V30(int lUserID, ref NET_DVR_MATRIX_TRAN_CHAN_CONFIG_V30 lpTranInfo);
        /// <summary>
        /// 获取透明通道信息
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lpTranInfo">透明通道参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetTranInfo_V30(int lUserID, ref NET_DVR_MATRIX_TRAN_CHAN_CONFIG_V30 lpTranInfo);
        #endregion

        #region 场景使用
        /// <summary>
        /// 获取当前正在使用的场景模式
        /// </summary>
        /// <param name="lUserID">用户ID号，NET_DVR_Login_V30等的返回值 </param>
        /// <param name="dwSceneNum">场景号</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetCurrentSceneMode(int lUserID, ref uint dwSceneNum);
        /// <summary>
        /// 场景切换控制
        /// </summary>
        /// <param name="lUserID">用户ID号，NET_DVR_Login_V30等的返回值</param>
        /// <param name="dwSceneNum">场景号，能力集中获取。</param>
        /// <param name="dwCmd">命令类型： 1- 场景模式切换（如果要切换的是当前场景，则不进行切换）；2- 初始化场景（将此场景的配置清空，如果是当前场景，则同时对当前场景进行清屏操作）；3- 强制切换（无论是否是当前场景，强制切换）；4- 保存场景</param>
        /// <param name="dwCmdParam">命令参数，保留</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSceneControl(int lUserID, uint dwSceneNum,uint dwCmd, uint dwCmdParam);
        #endregion

        #region LOGO设置

        /// <summary>
        /// 上传LOGO。Logo要求bmp格式，24位图，支持分辨率：32*32 ~ 256*128。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDispChanNum">解码通道号</param>
        /// <param name="lpDispLogoCfg">LOGO的参数</param>
        /// <param name="sLogoBuffer">LOGO数据缓冲区，最大100K，图像的宽和高必须是32的倍数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_UploadLogo(int lUserID, uint dwDispChanNum, ref NET_DVR_DISP_LOGOCFG lpDispLogoCfg, byte[] sLogoBuffer);
        /// <summary>
        /// 上传LOGO。Logo要求bmp格式，24位图，支持分辨率：32*32 ~ 256*128。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwLogoNO">LOGO序号</param>
        /// <param name="lpLogoInfo">LOGO的参数</param>
        /// <param name="sLogoBuffer">LOGO数据缓冲区，最大100K，图像的宽和高必须是32的倍数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_UploadLogo_NEW(int lUserID, uint dwLogoNO, ref NET_DVR_MATRIX_LOGO_INFO lpLogoInfo, byte[] sLogoBuffer);

        /// <summary>
        /// LOGO显示控制
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="dwDecChan">解码通道号</param>
        /// <param name="dwLogoSwitch">开关命令，1-显示 2-隐藏</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_LogoSwitch(int lUserID, uint dwDecChan, uint dwLogoSwitch);
        #endregion

        #region 设备状态
        /// <summary>
        /// 解码器获取设备状态
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lpDecoderCfg">设备状态参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDeviceStatus_V41(int lUserID,ref NET_DVR_DECODER_WORK_STATUS_V41 lpDecoderCfg);
        /// <summary>
        /// 解码器获取设备状态
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lpDecoderCfg">设备状态参数</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDeviceStatus(int lUserID, ref NET_DVR_DECODER_WORK_STATUS lpDecoderCfg);
        /// <summary>
        /// 获取多路解码器解码通道配置
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        /// <param name="dwDecChan">解码通道号</param>
        /// <param name="lpInter">解码通道控制参数，包括解码通道显示缩放控制和解码延时控制 (NET_DVR_MATRIX_DECCHAN_CONTROL)</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanCfg(int lUserID,uint dwDecChan, ref NET_DVR_MATRIX_DECCHAN_CONTROL lpInter);
        /// <summary>
        /// 设置多路解码器解码通道参数
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        /// <param name="dwDecChan">解码通道号</param>
        /// <param name="lpInter">解码通道控制参数，包括解码通道显示缩放控制和解码延时控制 (NET_DVR_MATRIX_DECCHAN_CONTROL)</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetDecChanCfg(int lUserID,uint dwDecChan, ref NET_DVR_MATRIX_DECCHAN_CONTROL lpInter);


        #endregion

        /// <summary>
        /// 获取输入信号源
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="dwDevNum">信号源个数</param>
        /// <param name="lpInputSignalList">信号源列表</param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetInputSignalList_V40(int lUserID, uint dwDevNum, ref NET_DVR_INPUT_SIGNAL_LIST lpInputSignalList);
    }
}
