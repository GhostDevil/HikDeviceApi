using System;
using System.Runtime.InteropServices;
using static HikDeviceApi.IVMS.IVMSDelegate;

namespace HikDeviceApi.IVMS
{
    /// <summary>
    /// 说明：海康视频综合平台接口
    /// 作者：痞子少爷
    /// 日期：2016-03-29
    /// </summary>
    public static class IVMSAPI
    {
        #region sdk操作
        /// <summary>
        /// 初始化平台接口
        /// </summary>
        /// <returns>-1：初始化失败；>=0 初始化成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_Initialize();

        /// <summary>
        /// 反初始化平台接口
        /// </summary>
        /// <returns>-1：注销失败；>=0 注销成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_UnInitialize();

        /// <summary>
        /// 获取SDK版本号
        /// </summary>
        /// <param name="SDKVersion">SDK版本号，把SDK版本号以字符串方式传出来，由外面定义一个字符串数组，限制长度32字节。</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_GetSDKVersion(string SDKVersion);
        #endregion

        #region 登录登出
        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <returns>-1：注销失败；>=0 注销成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_Logout(long LoginHandle);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Host">平台IP地址，最大长度32字节</param>
        /// <param name="Port">平台端口</param>
        /// <param name="Username">用户名，最大长度128字节</param>
        /// <param name="Password">密码，最大长度128字节</param>
        /// <returns>-1：调用失败；>=0 调用成功，其值代表登录实例号</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_Login(string Host, int Port, string Username, string Password);
        #endregion

        #region 实时播放

        /// <summary>
        /// 开始实时流播放
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="CameraId">摄像机ID（一般应为DB33、国标编号）</param>
        /// <param name="PlayWnd">播放窗口句柄，如果PlayWnd 为NULL，表示不解码</param>
        /// <param name="StreamType">码流模式(暂不支持子码流预览),传入0表示主码流</param>
        /// <param name="CBF_Stream">实时流回调函数的地址，允许为NULL</param>
        /// <param name="UserData">用户数据</param>
        /// <returns>-1：调用失败；>=0 调用成功，表示实时播放实例号</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_StartRealPlay(long LoginHandle, string CameraId, IntPtr PlayWnd, int StreamType, StreamCallbackPF CBF_Stream, IntPtr UserData);
        /// <summary>
        /// 停止实时流播放
        /// </summary>
        /// <param name="RealHandle">启动实时流播放时返回的实例号</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_StopRealPlay(long RealHandle);
        #endregion

        #region 客户端录像
        /// <summary>
        /// 开启客户端录像
        /// </summary>
        /// <param name="RealHandle">启动实时播放时返回的实例号</param>
        /// <param name="FileName">录像文件的文件名(包括路径，不含后缀)</param>
        /// <param name="FileExt">输出参数，文件后缀名</param>
        /// <param name="FileExtLen">输入参数，指明FileExt参数缓冲区长度</param>
        /// <returns>
        /// >=0	调用成功，表示录像实例号;
        /// -1	调用失败
        /// -98	输入参数非法
        /// -99	其它错误
        /// </returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_StartLocalRecord(long RealHandle, string FileName, out string FileExt, long FileExtLen);

        /// <summary>
        /// 停止客户端录像
        /// </summary>
        /// <param name="RealHandle">启动实时播放时返回的实例号</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_StopLocalRecord(long RealHandle);
        #endregion

        #region 远程回放

        /// <summary>
        /// 录像文件检索
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="CameraId">摄像机ID（一般应为DB33、国标编号）</param>
        /// <param name="StartTime">开始时间，时间格式 “YYYYMMDDTHHNNSSZ”(如“20140101T000000Z“)</param>
        /// <param name="EndTime">结束时间  时间格式 “YYYYMMDDTHHNNSSZ”,最大跨度24小时，如果用户指定的时间超过24小时，接口自动从开始时间往后取24小时即可。</param>
        /// <param name="StorageDev">不支持设置，传0即可，自动从平台中查找可用存储介质</param>
        /// <param name="RecordFindCBF">录像查询回调函数</param>
        /// <param name="UserData">用户参数</param>
        /// <returns>-1：调用失败；>=0 调用成功，表示查找实例号</returns>

        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_RecordFind(long LoginHandle,string CameraId, string StartTime, string EndTime,long StorageDev, RecordFindCallBackPF RecordFindCBF ,IntPtr UserData );

        /// <summary>
        /// 按时间远程回放
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="CameraID">摄像机ID（一般应为DB33、国标编号）</param>
        /// <param name="BeginTime">开始时间，时间格式 “YYYYMMDDTHHNNSSZ”</param>
        /// <param name="EndTime">结束时间, 时间格式 “YYYYMMDDTHHNNSSZ”</param>
        /// <param name="StorageDev">不支持设置，传0即可，自动从平台中查找可用存储介质</param>
        /// <param name="playWnd">播放窗口句柄，如果PlayWnd为NULL，表示不解码只回调码流</param>
        /// <param name="CBF_Stream">实时流回调函数的地址，允许为NULL</param>
        /// <param name="UserData">用户数据</param>
        /// <returns>-1：调用失败；>=0 调用成功，表示回放实例号</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_StreamReplayByTime(long LoginHandle,string CameraID,string BeginTime,string EndTime,long StorageDev,IntPtr playWnd, StreamCallbackPF CBF_Stream,IntPtr UserData );

        /// <summary>
        /// 按文件远程回放
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="RecordUrl">流回放的URL，取自录像文件检索XML结果里的RecordUrl项</param>
        /// <param name="PlayWnd">播放窗口句柄，如果PlayWnd为NULL，表示不解码只回调码流</param>
        /// <param name="CBF_Stream">实时流回调函数的地址，允许为NULL</param>
        /// <param name="UserData">用户数据</param>
        /// <returns>-1：调用失败；>=0 调用成功，表示回放实例号</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_StreamReplayByFile(long LoginHandle,string RecordUrl, IntPtr PlayWnd ,  StreamCallbackPF CBF_Stream,IntPtr UserData );
        /// <summary>
        /// 停止远程回放
        /// </summary>
        /// <param name="ReplayHandle">启动回放时返回的实例号</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_StopStreamReplay(long ReplayHandle);

        /// <summary>
        /// 远程回放控制
        /// </summary>
        /// <param name="ReplayHandle">远程回放实例号</param>
        /// <param name="ReplayMode">模式</param>
        /// <returns>-1：调用失败；>=0 调用成功;-97 不支持的调用命令</returns>
        /// <remarks>在远程回放的状态下，控制回放的模式，如快进/快退等。</remarks>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_StreamReplayControl(long ReplayHandle, int ReplayMode);
        /// <summary>
        /// 开始本地文件回放
        /// </summary>
        /// <param name="filename">录像文件名(目前只支持.hikvision类型的文件解码)</param>
        /// <param name="PlayWnd">播放窗口句柄</param>
        /// <param name="CBF_Stream">无效参数，传入NULL即可</param>
        /// <param name="UserData">无效参数，传入NULL即可</param>
        /// <returns>-1：调用失败；>=0 调用成功,文件回放实例号</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_StartFileReplay(string filename, IntPtr PlayWnd, StreamCallbackPF CBF_Stream, IntPtr UserData);
        /// <summary>
        /// 停止本地文件回放
        /// </summary>
        /// <param name="ReplayHandle">启动本地文件回放时返回的实例号</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int StopFileReplay(long ReplayHandle);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReplayHandle">启动本地文件回放时返回的实例号</param>
        /// <param name="ReplayMode">模式</param>
        /// <param name="CBF_Stream">回调函数</param>
        /// <returns>-1：调用失败；>=0 调用成功;-97 不支持的调用命令</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_FileReplayControl(long ReplayHandle, int ReplayMode, StreamCallbackPF CBF_Stream);

        /// <summary>
        /// 获取录像回放进度
        /// </summary>
        /// <param name="ReplayHandle">启动本地文件回放、按文件远程回放、按时间远程回放时返回的实例号</param>
        /// <param name="PlayTimed">已播放时间，单位为秒,用户可结合文件总秒数计算百分比</param>
        /// <returns>
        /// >=0	调用成功
        /// -1	调用失败
        /// -98	输入参数非法
        /// -99	其它错误
        /// </returns>
        /// <remarks>
        /// 用户即可通过本接口获取本地文件回放、按文件远程回放、按时间远程回放的进度，也可通过设置消息回调的方式获得播放进度
        /// </remarks>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_GetFileReplayPos(long ReplayHandle, long PlayTimed);

        /// <summary>
        /// 获取本地文件播放总时间
        /// </summary>
        /// <param name="ReplayHandle">启动本地文件回放时返回的实例号</param>
        /// <param name="TotalTime">文件总时间，单位为秒,用户可结合文件已播放秒数计算百分比</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_GetFileTotalTime(long ReplayHandle, long TotalTime);
        /// <summary>
        /// 设置录像回放进度
        /// </summary>
        /// <param name="ReplayHandle">启动本地文件回放、远程文件回放、远程时间回放时返回的实例号</param>
        /// <param name="PlayTimed">相对录像开始时间的秒数</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        /// <remarks>
        /// 本接口应含本地文件回放、远程文件回放、远程时间回放的播放进度设置
        /// </remarks>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_SetStreamReplayPos(long ReplayHandle, long PlayTimed);

        /// <summary>
        /// 本地文件剪辑
        /// </summary>
        /// <param name="srcFile">源文件名(被剪辑的文件)，由外部提供一个字符串，长度不超过256字节</param>
        /// <param name="DestFile">目的文件名(剪辑后生成的文件)，由外部提供一个字符串，长度不超过256字节</param>
        /// <param name="BeginTime">开始剪切时间，单位为秒(相对时间)</param>
        /// <param name="EndTime">结束剪切时间，单位为秒(相对时间)</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        /// <remarks>
        /// 只支持.hikvision类型文件的剪辑
        /// </remarks>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_FileCut(string srcFile,string DestFile, long BeginTime, long EndTime  );
        #endregion

        #region 获取平台资源

        /// <summary>
        /// 获取资源列表
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="ResType">资源类型 , 0表示组织 , 1表示摄像机</param>
        /// <param name="ParentOrgCode">父组织编码（建议采用DB33规范中的区域编码)， ParentOrg为空时表示获取根组织。</param>
        /// <param name="OrgInfoCBF">组织信息回调函数指针</param>
        /// <param name="UserData">用户参数</param>
        /// <returns>-1：调用失败；>=0 调用成功,获取资源的实例号</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_GetResList(long LoginHandle, int ResType, string ParentOrgCode, OrgInfoCallBackPF OrgInfoCBF, IntPtr UserData);

        /// <summary>
        /// 获取摄像机信息(一次只获取一个指定编号的摄像机信息)
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="CameraId">摄像机ID（一般应为DB33、国标编号）</param>
        /// <param name="CameraInfo">摄像机详细信息，详见摄像机详细信息格式，由外面定义一个字符串数组，限制长度1024字节</param>
        /// <returns>-1：调用失败；>=0 调用成功,获取资源的实例号</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_GetCameraInfo(long LoginHandle,string CameraId, string CameraInfo);

        /// <summary>
        /// 获取摄像机离线机状态
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="CameraIds">摄像机ID集XML</param>
        /// <param name="CameraStatus">摄像机状态集XML，由外面定义一个字符串数组，限制长度1024字节</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        /// <remarks>
        /// 0 为在线   1 为离线
        /// </remarks>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_GetCameraStatus(long LoginHandle, string CameraIds, string CameraStatus );


        #endregion
        /// <summary>
        /// 获取最近错误描述
        /// </summary>
        /// <param name="ErrorDesc">错误描述信息，把更详细的错误信息以字符串方式传出来，由外面定义一个字符串数组，限制长度512字节，该参数可为空。</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_GetLastError(string ErrorDesc);

        /// <summary>
        /// 获取平台版本号
        /// </summary>
        /// <param name="PlatformVersion">平台版本号，把平台版本号以字符串方式传出来，由外面定义一个字符串数组，限制长度32字节。</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_GetPlatformVersion(string PlatformVersion);

        /// <summary>
        /// 获取平台能力
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="AbilityType">能力类型， 1 厂商解码支持能力,2...（待定）</param>
        /// <param name="AbilityXML">能力集XML描述，外面定义一个字符串数组，限制长度1024字节。</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_GetPlatformAbility(long LoginHandle, int AbilityType ,string AbilityXML);

        #region 录像下载

        /// <summary>
        /// 按时间下载录像
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="CameraID">摄像机ID（一般应为DB33、国标编号）</param>
        /// <param name="BeginTime">开始时间，格式 “YYYYMMDDTHHNNSSZ”</param>
        /// <param name="EndTime">结束时间, 格式 “YYYYMMDDTHHNNSSZ”,不超过24小时</param>
        /// <param name="StorageDev">不支持设置，传0即可，自动从平台中查找可用存储介质</param>
        /// <param name="DownloadSpeed">下载的倍速，1，2，4，8；该参数即作为输入参数，也作为输出参数，输入时表示要求下载倍速，输出时表示实际下载倍速。</param>
        /// <param name="filename">录像文件的文件名(包括路径，不含后缀)</param>
        /// <param name="fileExt">文件后缀名，输出参数，原则上按照解码插件DECODETAG作为文件后缀，非DB33标准的视频流由平台自定义，由平台返回。</param>
        /// <param name="fileExtLen">输入参数，指明FileExt参数缓冲区长度</param>
        /// <returns>-1：调用失败；>=0 调用成功,表示录像下载实例号</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_StartDownloadByTime(long LoginHandle,string CameraID,string BeginTime, string EndTime,long StorageDev,ref int DownloadSpeed, string filename, ref string fileExt, long fileExtLen );

        /// <summary>
        /// 按文件下载录像
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="DownloadSpeed">下载的倍速，1，2，4，8；该参数即作为输入参数，也作为输出参数，输入时表示要求下载倍速，输出时表示实际下载倍速。</param>
        /// <param name="RecordUrl">录像URL，录像检索时返回的RecordUrl </param>
        /// <param name="filename">用于保存录像文件的文件名(包括路径，不含后缀)</param>
        /// <param name="fileExt">输出参数，原则上按照解码插件DECODETAG，非DB33标准的视频流由平台自定义，由平台返回。</param>
        /// <param name="fileExtLen">输入参数，指明FileExt参数缓冲区长度</param>
        /// <returns>-1：调用失败；>=0 调用成功,表示录像下载实例号</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_StartDownloadByFile(long LoginHandle, ref int DownloadSpeed, string RecordUrl,string filename, ref string fileExt, long fileExtLen );
        /// <summary>
        /// 停止录像下载
        /// </summary>
        /// <param name="DownloadHandle">开始录像下载时返回的实例号</param>
        /// <returns>-1：调用失败；>=0 调用成功,表示总共下载了几个文件； -98 输入参数非法；-99它错误</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_StopDownload(long DownloadHandle);

        /// <summary>
        /// 获取下载进度
        /// </summary>
        /// <param name="DownloadHandle">开始录像下载时返回的实例号</param>
        /// <returns>0-100	调用成功，表示表示进度值;-1	调用失败</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_GetDownloadPos(long DownloadHandle);
        #endregion

        #region 云台控制

        /// <summary>
        /// 云台控制
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="CameraId">摄像机ID（一般应为DB33、国标编号）</param>
        /// <param name="PtzCmd">控制命令，不区分大小写</param>
        /// <param name="Param1">速度,取值范围为0-63,缺省为49</param>
        /// <param name="Param2">预置点名称(字符串指针）</param>
        /// <param name="Param3">空</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_PtzCtrl(long LoginHandle,string CameraId, string PtzCmd, int Param1,int Param2,int Param3 );

        /// <summary>
        /// 云台控制3D
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="CameraId">摄像机ID（一般应为DB33、国标编号）</param>
        /// <param name="xTop">方框起始点的x坐标（左上x坐标）</param>
        /// <param name="yTop">方框起始点的y坐标 （左上y坐标）</param>
        /// <param name="xBottom">方框结束点的x坐标 （右下x坐标）</param>
        /// <param name="yBottom">方框结束点的y坐标 （右下y坐标）</param>
        /// <param name="bCounter">画框方向：1- 左上，2- 右上，3- 左下，4- 右下</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_PtzCtrlStd_PtzCtrl3D(long LoginHandle,string CameraId, int xTop,int yTop,int xBottom, int yBottom,int bCounter);
        #endregion

        #region 声音音量
        /// <summary>
        /// 打开声音
        /// </summary>
        /// <param name="PlayHandle">启动实时播放、录像播放、本地文件播放时返回的实例号</param>
        /// <returns>
        /// >=0	调用成功
        /// -1	调用失败
        /// -98	输入参数非法
        /// -99	其它错误
        /// </returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_OpenSound(long PlayHandle);

        /// <summary>
        /// 关闭声音
        /// </summary>
        /// <param name="PlayHandle">启动实时播放、录像播放、本地文件播放时返回的实例号</param>
        /// <returns>
        /// >=0	调用成功
        /// -1	调用失败
        /// -98	输入参数非法
        /// -99	其它错误
        /// </returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_CloseSound(long PlayHandle);

        /// <summary>
        /// 设置系统音量
        /// </summary>
        /// <param name="Volume">音量值（取值从0到255，255为最大音量）</param>
        /// <returns>
        /// >=0	调用成功
        /// -1	调用失败
        /// -98	输入参数非法
        /// -99	其它错误
        /// </returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_SetVolume(int Volume);
        /// <summary>
        /// 获取系统音量
        /// </summary>
        /// <param name="Volume">输出参数,音量值（取值从0到255，255为最大音量）</param>
        /// <returns>
        /// >=0	调用成功
        /// -1	调用失败
        /// -98	输入参数非法
        /// -99	其它错误
        /// </returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_GetVolume(int Volume);
        #endregion

        /// <summary>
        /// 抓图(在实时播放、回放、本地录像播放时均可用本接口抓图)
        /// </summary>
        /// <param name="PlayHandle">启动实时播放、录像播放或本地文件播放时返回的实例号</param>
        /// <param name="PicFile">不带后缀的图片文件的文件名（含全路径）（示例：PicFile：“C:/TEST” PicFormat:1 最终图片名称为：“C：/TEST.bmp”）</param>
        /// <param name="PicFormat">图片保存格式；0 表示JPG  1表示BMP</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern int Std_Capture(long PlayHandle, string PicFile, int PicFormat);

        #region  信息回调 
        /// <summary>
        /// 设置原始数据回调
        /// </summary>
        /// <param name="RealHandle">启动实时播放、远程录像回放、本地录像回放时的实例号。</param>
        /// <param name="CBF_Stream">原始流回调函数的地址</param>
        /// <param name="UserData">用户信息</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        /// <remarks>
        ///   本接口应支持实时流的获取、支持回放时文件流的获取
        /// </remarks>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_SetRealDataCBF(long RealHandle, StreamCallbackPF CBF_Stream,IntPtr UserData);
        /// <summary>
        /// 设置消息回调函数
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="CBF_Message">消息回调函数的地址</param>
        /// <param name="UserData">用户信息</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        /// <remarks>
        /// 用于回调解码异常消息，录像异常消息、录像下载进度消息、录像播放进度消息，用户离线消息等
        /// </remarks>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_SetMessageCBF(long LoginHandle, fMsgCallback CBF_Message, IntPtr UserData);

        /// <summary>
        /// 设置状态推送回调
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="CBF_Status">消息回调函数的地址</param>
        /// <param name="UserData">用户信息</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        /// <remarks>
        /// 用于摄像机状态发生变化时向客户端推送
        /// </remarks>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_SetStatusCBF(long LoginHandle, fStatusCallback CBF_Status, IntPtr UserData);

        /// <summary>
        /// 设置画图叠加回调
        /// </summary>
        /// <param name="RealHandle">启动实时流播放、录像回放时返回的实例号</param>
        /// <param name="CBF_Draw">回调函数的地址</param>
        /// <param name="UserData">用户信息</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_SetDrawCBF(long RealHandle, fDrawCallBack CBF_Draw, IntPtr UserData);

        /// <summary>
        /// 设置下载数据回调
        /// </summary>
        /// <param name="DownloadHandle">开始下载时返回的实例号</param>
        /// <param name="CBF_Stream">回调函数的地址</param>
        /// <param name="UserData">用户参数</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_SetDownloadDataCBF(long DownloadHandle, StreamCallbackPF CBF_Stream, IntPtr UserData);

        /// <summary>
        /// 订阅报警信息
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="CBF_Message">回调函数的地址</param>
        /// <param name="UserData">用户信息</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_SubscribeAlarm(long LoginHandle, fAlarmCallback CBF_Message, IntPtr UserData);

        /// <summary>
        /// 取消订阅报警
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <returns>-1：调用失败；>=0 调用成功</returns>
        [DllImport(@"IVMSDLL\StdClient_Hikvision236.dll")]
        public static extern int Std_UnSubscribeAlarm(long LoginHandle);
        #endregion

    }
}
