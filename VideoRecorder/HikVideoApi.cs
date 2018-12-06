using System;
using System.Runtime.InteropServices;
using static HikDeviceApi.VideoRecorder.HikVideoDelegate;
using static HikDeviceApi.VideoRecorder.HikVideoStruct;
using static HikDeviceApi.HikStruct;

namespace HikDeviceApi.VideoRecorder
{
    /// <summary>
    /// 日 期:2015-11-24
    /// 作 者:痞子少爷
    /// 描 述:海康硬盘录像机接口
    /// </summary>
    public static class HikVideoApi
    {
        #region 手动录像
        /// <summary>
        /// 远程手动启动设备录像
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lChannel">通道号，0x00ff表示所有模拟通道，0xff00表示所有数字通道，0xffff表示所有模拟和数字通道</param>
        /// <param name="lRecordType">录像类型：0- 手动，1- 报警，2- 回传，3- 信号，4- 移动，5- 遮挡</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>
        /// 录像类型设置需要设备支持，不支持默认为手动录像。除了CVR，其他设备目前只支持手动录像。
        /// 当某通道已经开启定时录像的前提下首次开启手动录像，此次操作未生效，仍保持定时录像状态，且查询设备状态（见接口NET_DVR_GetDVRWorkState_V30和NET_DVR_GetDVRWorkState；结构体NET_DVR_WORKSTATE_V30和NET_DVR_WORKSTATE）中的录像状态仍为录像；此时关闭手动录像，停止了定时录像，且查询录像状态为不录像；
        /// 第二次开启手动录像，此时手动录像开始；停止手动录像后，重启设备，定时录像重新打开。
        /// </remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDVRRecord(int lUserID,int lChannel,int lRecordType);
        /// <summary>
        /// 远程手动停止设备录像
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lChannel">通道号，0x00ff表示所有模拟通道，0xff00表示所有数字通道，0xffff表示所有模拟和数字通道</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDVRRecord(int lUserID,int lChannel);


        #endregion

        #region 录像回放
        /// <summary>
        /// 按文件名回放录像文件
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="sPlayBackFileName">回放的文件名，长度不能超过100字节 .</param>
        /// <param name="hWnd">回放的窗口句柄，若置为空，SDK仍能收到码流数据，但不解码显示 .</param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackByName(int lUserID, string sPlayBackFileName, IntPtr hWnd);
        /// <summary>
        /// 按时间回放录像文件
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lChannel">通道号</param>
        /// <param name="lpStartTime">文件的开始时间</param>
        /// <param name="lpStopTime">文件结束时间 </param>
        /// <param name="hWnd"> 回放的窗口句柄，若置为空，SDK仍能收到码流数据，但不解码显示 .</param>
        /// <returns></returns>

        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackByTime(int lUserID, int lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, IntPtr hWnd);
        /// <summary>
        /// 按流ID和时间回放录像文件 
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="pVodPara">查找条件 </param>
        /// <returns>-1表示失败，其他值作为NET_DVR_StopPlayBack等函数的参数。</returns>
        /// <remarks>
        /// 该接口指定了当前要播放的录像文件，调用成功后，还必须调用NET_DVR_PlayBackControl_V40接口的开始播放控制命令（NET_DVR_PLAYSTART）才能实现回放。
        /// 当回放的是按事件搜索出的录像文件时，由于每个文件都会有预录和延迟的部分，因此在设置本接口的开始和结束时间参数时可以适当提前开始时间和延长结束时间。建议值：最多10分钟，最少5秒。
        /// 在调用该接口成功后，可以通过接口NET_DVR_SetPlayDataCallBack_V40注册回调函数，捕获录像的码流数据并自行处理。 
        /// 该接口如果传入了播放句柄而加载播放库失败，实际上失败的，但是接口会返回成功。错误会通过异常方式进行回调(NET_DVR_SetExceptionCallBack_V30)，异常消息类型为：EXCEPTION_PLAYBACK，具体错误需要在异常消息回调函数中调用NET_DVR_GetLastError获取。 
        /// </remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackByTime_V40(int lUserID, ref NET_DVR_VOD_PARA pVodPara);
        /// <summary>
        /// 控制录像回放的状态
        /// </summary>
        /// <param name="lPlayHandle">播放句柄，NET_DVR_PlayBackByName或NET_DVR_PlayBackByTime的返回值</param>
        /// <param name="dwControlCode">控制录像回放状态命令</param>
        /// <param name="dwInValue">设置的参数，如设置文件回放的进度(命令值NET_DVR_PLAYSETPOS)时，此参数表示进度值；如开始播放(命令值NET_DVR_PLAYSTART)时，此参数表示断点续传的文件位置（Byte）。</param>
        /// <param name="LPOutValue">获取的参数，如获取当前播放文件总的时间（命令值NET_DVR_GETTOTALTIME ），此参数就是得到的总时间。</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, ref uint LPOutValue);
        /// <summary>
        /// 控制录像回放的状态
        /// </summary>
        /// <param name="lPlayHandle">播放句柄，NET_DVR_PlayBackByName、NET_DVR_PlayBackReverseByName或NET_DVR_PlayBackByTime_V40、NET_DVR_PlayBackReverseByTime_V40的返回值。</param>
        /// <param name="dwControlCode">控制录像回放状态命令</param>
        /// <param name="lpInBuffer">指向输入参数的指针</param>
        /// <param name="dwInValue">输入参数的长度。未使用，保留。</param>
        /// <param name="lpOutBuffer">指向输出参数的指针</param>
        /// <param name="LPOutValue">输出参数的长度</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackControl_V40(int lPlayHandle, uint dwControlCode, IntPtr lpInBuffer, uint dwInValue, IntPtr lpOutBuffer, ref uint LPOutValue);
        /// <summary>
        /// 停止回放录像文件
        /// </summary>
        /// <param name="lPlayHandle">回放句柄，NET_DVR_PlayBackByName、NET_DVR_PlayBackByTime_V40或者NET_DVR_PlayBackReverseByName、NET_DVR_PlayBackReverseByTime_V40的返回值。</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBack(int lPlayHandle);
        /// <summary>
        /// 注册回调函数，捕获录像数据。
        /// </summary>
        /// <param name="lPlayHandle">播放句柄，NET_DVR_PlayBackByName或NET_DVR_PlayBackByTime的返回值。</param>
        /// <param name="fPlayDataCallBack">录像数据回调函数</param>
        /// <param name="dwUser">用户数据</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayDataCallBack(int lPlayHandle, PLAYDATACALLBACK fPlayDataCallBack, uint dwUser);
        /// <summary>
        /// 捕获回放的录像数据，并保存成文件。
        /// </summary>
        /// <param name="lPlayHandle">播放句柄，NET_DVR_PlayBackByName、NET_DVR_PlayBackByTime_V40或者NET_DVR_PlayBackByTime的返回值。</param>
        /// <param name="sFileName">保存数据的文件路径（包括文件名）</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackSaveData(int lPlayHandle, string sFileName);
        /// <summary>
        /// 停止保存录象数据
        /// </summary>
        /// <param name="lPlayHandle">播放句柄，NET_DVR_PlayBackByName或NET_DVR_PlayBackByTime的返回值。</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBackSave(int lPlayHandle);

        /// <summary>
        /// 获取录像回放时显示的OSD时间
        /// </summary>
        /// <param name="lPlayHandle">播放句柄，NET_DVR_PlayBackByName或NET_DVR_PlayBackByTime_V40的返回值。</param>
        /// <param name="lpOsdTime">获取的OSD时间的指针</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPlayBackOsdTime(int lPlayHandle, ref NET_DVR_TIME lpOsdTime);
        /// <summary>
        /// 录像回放时抓图，并保存在文件中。
        /// </summary>
        /// <param name="lPlayHandle"> 播放句柄，NET_DVR_PlayBackByName或NET_DVR_PlayBackByTime_V40的返回值。</param>
        /// <param name="sFileName">保存图片数据的文件路径（包括文件名） </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>回放时抓下来的图片时间要比抓图时间延后，这是因为预览画面上的OSD时间是解码完成的显示时间，而解码缓冲区会有将近1M左右的数据还没有解出来，要抓取的图片数据是网络缓冲里面的。目前解码库没有直接从解码缓冲区中取出数据的接口。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackCaptureFile(int lPlayHandle, string sFileName);
        #endregion

        #region 录像下载
        /// <summary>
        /// 按文件名下载录像文件
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        /// <param name="sDVRFileName">要下载的录像文件名，文件名长度需小于100字节。</param>
        /// <param name="sSavedFileName">下载后保存到PC机的文件路径，需为绝对路径（包括文件名）。</param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_GetFileByName(int lUserID, string sDVRFileName, string sSavedFileName);
        /// <summary>
        /// 按时间下载录像文件
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        /// <param name="lChannel">通道号</param>
        /// <param name="lpStartTime">开始时间</param>
        /// <param name="lpStopTime">结束时间</param>
        /// <param name="sSavedFileName">下载后保存到PC机的文件路径，需为绝对路径。</param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_GetFileByTime(int lUserID, int lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, string sSavedFileName);
        /// <summary>
        /// 按时间下载录像文件
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login或者NET_DVR_Login_V30的返回值。</param>
        /// <param name="sSavedFileName">下载后保存到PC机的文件路径，需为绝对路径（包括文件名）。</param>
        /// <param name="pDownloadCond">下载条件</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_StopGetFile等函数的参数。</returns>
        /// <remarks>该接口指定了当前要下载的录像文件，调用成功后，还需要调用NET_DVR_PlayBackControl_V40接口的开始播放控制命令（NET_DVR_PLAYSTART）才能实现下载。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_GetFileByTime_V40(int lUserID, string sSavedFileName, ref NET_DVR_PLAYCOND pDownloadCond);
        /// <summary>
        /// 停止下载录像文件
        /// </summary>
        /// <param name="lFileHandle">下载句柄，NET_DVR_GetFileByName、NET_DVR_GetFileByTime_V40或NET_DVR_GetFileByTime的返回值。</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopGetFile(int lFileHandle);
        /// <summary>
        /// 获取当前下载录像文件的进度
        /// </summary>
        /// <param name="lFileHandle">下载句柄，NET_DVR_GetFileByName、NET_DVR_GetFileByTime_V40或NET_DVR_GetFileByTime的返回值 </param>
        /// <returns>-1表示失败；0～100表示下载的进度；100表示下载结束；正常范围0-100，如返回200表明出现网络异常。</returns>
        /// <remarks>该接口用于获取按文件名下载录像文件时的下载进度。</remarks>

        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadPos(int lFileHandle);
        #endregion

        #region 录像文件查找

        /// <summary>
        /// 根据文件类型、时间查找设备录像文件。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="lChannel">通道号</param>
        /// <param name="dwFileType">要查找的文件类型：0xff-全部；0-定时录像；1-移动侦测；2-报警触发；3-报警或动测；4-报警和动测；5-命令触发；6-手动录像；7-智能录像 </param>
        /// <param name="lpStartTime">文件的开始时间</param>
        /// <param name="lpStopTime">文件的结束时间</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_FindClose等函数的参数。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile(int lUserID, int lChannel, uint dwFileType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);
        /// <summary>
        /// 逐个获取查找到的文件信息
        /// </summary>
        /// <param name="lFindHandle">文件查找句柄，NET_DVR_FindFile的返回值</param>
        /// <param name="lpFindData">保存文件信息的指针 </param>
        /// <returns></returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile(int lFindHandle, ref NET_DVR_FIND_DATA lpFindData);
        /// <summary>
        /// 关闭文件查找，释放资源。
        /// </summary>
        /// <param name="lFindHandle">文件查找句柄NET_DVR_FindFile的返回值 </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_FindClose(int lFindHandle);
        /// <summary>
        /// 逐个获取查找到的文件信息
        /// </summary>
        /// <param name="lFindHandle">文件查找句柄，NET_DVR_FindFile_V40或者NET_DVR_FindFile_V30的返回值</param>
        /// <param name="lpFindData">保存文件信息的指针</param>
        /// <returns>-1表示失败，其他值表示当前的获取状态等信息。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile_V30(int lFindHandle, ref NET_DVR_FINDDATA_V30 lpFindData);
        /// <summary>
        /// 逐个获取查找到的文件信息
        /// </summary>
        /// <param name="lFindHandle">文件查找句柄，NET_DVR_FindFile_V40或者NET_DVR_FindFile_V30的返回值</param>
        /// <param name="lpFindData">保存文件信息的指针</param>
        /// <returns>-1表示失败，其他值表示当前的获取状态等信息。</returns>
        /// <remarks>
        /// 在调用该接口获取查找文件之前，必须先调用NET_DVR_FindFile_V40得到当前的查找句柄。此接口用于获取一条已查找到的文件信息，若要获取全部的已查找到的文件信息，需要循环调用此接口。通过此接口可以同时获取到与当前录像文件相关的卡号信息和文件是否被锁定的信息。
        /// 每次可查询文件最大个数为4000
        /// </remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile_V40(int lFindHandle, ref NET_DVR_FINDDATA_V40 lpFindData);
        /// <summary>
        /// 根据文件类型、时间查找设备录像文件。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="pFindCond">欲查找的文件信息结构</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_FindClose等函数的参数。</returns>
        /// <remarks>该接口指定了要查找的录像文件的信息，调用成功后，就可以调用NET_DVR_FindNextFile接口来获取文件信息。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile_V30(int lUserID, ref NET_DVR_FILECOND pFindCond);
        /// <summary>
        /// 根据文件类型、时间查找设备录像文件。
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="pFindCond">欲查找的文件信息结构</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_FindClose等函数的参数。</returns>
        /// <remarks>该接口指定了要查找的录像文件的信息，调用成功后，就可以调用NET_DVR_FindNextFile_V40接口来获取文件信息。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile_V40(int lUserID, ref NET_DVR_FILECOND_V40 pFindCond);
        /// <summary>
        /// 关闭文件查找，释放资源。
        /// </summary>
        /// <param name="lFindHandle">文件查找句柄，NET_DVR_FindFile_V40、NET_DVR_FindFileByEvent或者NET_DVR_FindFile_V30的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_FindClose_V30(int lFindHandle);
        #endregion

        #region 录像文件锁定解锁
        /// <summary>
        /// 按文件名锁定录像文件
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="sLockFileName">要锁定的录像文件名，文件名长度需小于100字节</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>在使用该接口锁定录像文件前，可以先调用录像文件查找的接口获取文件名。当文件被锁定后，将不会被覆盖。</remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_LockFileByName(int lUserID, string sLockFileName);
        /// <summary>
        /// 按文件名解锁录像文件
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="sUnlockFileName">要解锁的录像文件名</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_UnlockFileByName(int lUserID, string sUnlockFileName);
        #endregion

        #region 声音控制
        /// <summary>
        /// 设置声音播放模式
        /// </summary>
        /// <param name="dwMode">[in] 声音播放模式：1－独占声卡，单路音频模式；2－共享声卡，多路音频模式</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAudioMode(uint dwMode);
        /// <summary>
        /// 独占声卡模式下开启声音
        /// </summary>
        /// <param name="lRealHandle">[in] NET_DVR_RealPlay或NET_DVR_RealPlay_V30的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSound(Int32 lRealHandle);
        /// <summary>
        /// 独占声卡模式下关闭声音
        /// </summary>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSound();
        /// <summary>
        /// 共享声卡模式下开启声音
        /// </summary>
        /// <param name="lRealHandle">[in] NET_DVR_RealPlay或NET_DVR_RealPlay_V30的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSoundShare(Int32 lRealHandle);
        /// <summary>
        /// 共享声卡模式下关闭声音
        /// </summary>
        /// <param name="lRealHandle">[in] NET_DVR_RealPlay或NET_DVR_RealPlay_V30的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSoundShare(Int32 lRealHandle);
        /// <summary>
        /// 调节播放音量
        /// </summary>
        /// <param name="lRealHandle">[in] NET_DVR_RealPlay或NET_DVR_RealPlay_V30的返回值</param>
        /// <param name="wVolume">音量，取值范围[0,65535]0xffff</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_Volume(Int32 lRealHandle, ushort wVolume);
        #endregion

        #region 实时预览
        /// <summary>
        ///  实时预览
        /// </summary>
        /// <param name="iUserID">NET_DVR_Login_V40等登录接口的返回值 </param>
        /// <param name="lpPreviewInfo">预览参数</param>
        /// <param name="fRealDataCallBack_V30">码流数据回调函数</param>
        /// <param name="pUser">用户数据</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_RealPlay_V40(int iUserID, ref NET_DVR_PREVIEWINFO lpPreviewInfo, REALDATACALLBACK fRealDataCallBack_V30, IntPtr pUser);
        /// <summary>
        /// 停止实时预览
        /// </summary>
        /// <param name="iRealHandle">实时预览句柄</param>
        /// <returns>TRUE表示成功，FALSE表示失败</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRealPlay(int iRealHandle);
        #endregion

        #region 硬盘管理
        /// <summary>
        /// 远程格式化设备硬盘
        /// </summary>
        /// <param name="lUserID">NET_DVR_Login_V40等登录接口的返回值</param>
        /// <param name="lDiskNumber">硬盘号，从0开始，0xff表示对所有硬盘有效（不包括只读硬盘）</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_CloseFormatHandle等函数的参数。</returns>
        /// <remarks>
        /// 格式化过程中如果网络断了，设备上的格式化操作依然会继续，但是客户端无法收到状态。
        /// </remarks>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern int NET_DVR_FormatDisk(int lUserID, int lDiskNumber);

        /// <summary>
        /// 获取格式化硬盘的进度
        /// </summary>
        /// <param name="lFormatHandle">格式化硬盘句柄，NET_DVR_FormatDisk的返回值</param>
        /// <param name="pCurrentFormatDisk">指向保存当前正在格式化的硬盘号的指针，硬盘号从0开始，-1为初始状态 </param>
        /// <param name="pCurrentDiskPos">指向保存当前正在格式化的硬盘的进度的指针，进度是0～100</param>
        /// <param name="pFormatStatic">指向保存硬盘格式化状态的指针，0-正在格式化；1-硬盘全部格式化完成；2-格式化当前硬盘出错，不能继续格式化此硬盘，本地和网络硬盘都会出现此错误；3-由于网络异常造成网络硬盘丢失而不能开始格式化当前硬盘 。</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_GetFormatProgress(int lFormatHandle, IntPtr pCurrentFormatDisk, IntPtr pCurrentDiskPos, IntPtr pFormatStatic);

        /// <summary>
        /// 关闭格式化硬盘句柄，释放资源。
        /// </summary>
        /// <param name="lFormatHandle">NET_DVR_ FormatDisk的返回值</param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        [DllImport(@"DLL\HIKDLL\HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseFormatHandle(int lFormatHandle);


        #endregion
    }
}
