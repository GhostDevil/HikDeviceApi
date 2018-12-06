
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static HikDeviceApi.HikConst;
using static HikDeviceApi.HikStruct;
using static HikDeviceApi.VideoRecorder.HikVideoEnum;
using static HikDeviceApi.VideoRecorder.HikVideoStruct;
namespace HikDeviceApi.VideoRecorder
{
    /// <summary>
    /// 日 期:2016-11-15
    /// 作 者:痞子少爷
    /// 描 述:海康硬盘录像机接口使用代理
    /// </summary>
    public class HikVideoOperate
    {
        #region 全局变量
        private uint dwAChanTotalNum = 0;
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 96, ArraySubType = UnmanagedType.U4)]
        private int[] iChannelNum = new int[96];
        #endregion

        #region 事件
        ///// <summary>
        /////报警回掉事件
        ///// </summary>
        // //event HikPublicDelegate.MSGCallBack AlarmCallBack;
        ///// <summary>
        /////报警回掉事件
        ///// </summary>
        ////event HikPublicDelegate.MSGCallBackV31 AlarmCallBackV31;
        ///// <summary>
        ///// 监听回调
        ///// </summary>
        // //event HikDvrDelegate.MSGCallBackV31 ListenResult;
        /// <summary>
        /// (DS-8000老设备)移动侦测、视频丢失、遮挡、IO信号量等报警信息
        /// </summary>
        public event HikVideoDelegate.MSGCallBackHandel ProcessCommAlarm;
        /// <summary>
        /// 移动侦测、视频丢失、遮挡、IO信号量等报警信息
        /// </summary>
        public event HikVideoDelegate.MSGCallBackHandel ProcessCommAlarm_V30;

        /// <summary>
        /// 进出区域、入侵、徘徊、人员聚集等行为分析报警信息
        /// </summary>
        public event HikVideoDelegate.MSGCallBackHandel ProcessCommAlarm_RULE;
        /// <summary>
        /// 交通抓拍结果上传(老报警信息类型)
        /// </summary>
        public event HikVideoDelegate.MSGCallBackHandel ProcessCommAlarm_Plate;
        /// <summary>
        /// 交通抓拍结果上传(新报警信息类型)
        /// </summary>
        public event HikVideoDelegate.MSGCallBackHandel ProcessCommAlarm_ITSPlate;
        #endregion

        #region 视频预览
        //private void Preview(DvrUseInfo info)
        //{
        //    HikVideoApi.NET_DVR_StopRealPlay(info.RealId);
        //    var hwnd = ptzImage.Handle;
        //    NET_DVR_PREVIEWINFO lpPreviewInfo = new NET_DVR_PREVIEWINFO();
        //    lpPreviewInfo.hPlayWnd = hwnd;//预览窗口
        //    lpPreviewInfo.lChannel = cbxPTZChannel.SelectedIndex + 1;//预te览的设备通道
        //    lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
        //    lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
        //    lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
        //    lpPreviewInfo.dwDisplayBufNum = 15; //播放库播放缓冲区最大缓冲帧数
        //    REALDATACALLBACK RealData = new REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
        //    IntPtr pUser = new IntPtr();//用户数据
        //    打开预览 Start live view
        //    info.RealId = HikVideoApi.NET_DVR_RealPlay_V40(info.UserId, ref lpPreviewInfo, null/*RealData*/, pUser);
        //    realId = info.RealId;
        //    dvrUseInfos.Find(o => o.DvrIp == cbxPTZIp.Text).RealId = info.RealId;
        //    TxtPTZMsg(AppLogHelper.GetLogStr(string.Format("DvrIp：{0} 端口：{1} 名称：{2} {3} ({4})", info.DvrIp, info.DvrPoint, info.DvrName, realId >= 0 ? "预览图像成功" : "预览图像失败:" + HikOperate.GetLastError(), DateTime.Now), "图像预览"));
        //}

        #endregion

        #region 录像查找
        /// <summary>
        /// 录像文件查找
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象</param>
        /// <param name="channel">通道号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="useCardNo">是否带ATM信息进行查询</param>
        /// <param name="fileType">录象文件类型（根据dwUseCardNo参数是否带卡号查找分为两类,参考枚举 NocardSelectFileType 和 UseCardSelectFileType）</param>
        /// <param name="isLocked">是否锁定：0-未锁定文件，1-锁定文件，0xff表示所有文件（包括锁定和未锁定）</param>
        /// <returns>返回文件列表</returns>
        public List<BackFile> SearchBackFile(ref DvrUseInfo info, int channel, DateTime startTime, DateTime endTime,uint useCardNo=(uint)UseCardSelectWay.不带ATM信息,uint fileType=0xff,uint isLocked=0xff)
        {

            List<BackFile> files = new List<BackFile>();

            NET_DVR_FILECOND_V40 struFileCond_V40 = new NET_DVR_FILECOND_V40() { lChannel = channel, /*通道号 Channel number*/ dwFileType = fileType, /*0xff-全部，0-定时录像，1-移动侦测，2-报警触发，...*/ dwIsLocked = isLocked /*0-未锁定文件，1-锁定文件，0xff表示所有文件（包括锁定和未锁定）*/, dwUseCardNo = useCardNo };
            //设置录像查找的开始时间 Set the starting time to search video files
            struFileCond_V40.struStartTime.dwYear = (uint)startTime.Year;
            struFileCond_V40.struStartTime.dwMonth = (uint)startTime.Month;
            struFileCond_V40.struStartTime.dwDay = (uint)startTime.Day;
            struFileCond_V40.struStartTime.dwHour = (uint)startTime.Hour;
            struFileCond_V40.struStartTime.dwMinute = (uint)startTime.Minute;
            struFileCond_V40.struStartTime.dwSecond = (uint)startTime.Second;

            //设置录像查找的结束时间 Set the stopping time to search video files
            struFileCond_V40.struStopTime.dwYear = (uint)endTime.Year;
            struFileCond_V40.struStopTime.dwMonth = (uint)endTime.Month;
            struFileCond_V40.struStopTime.dwDay = (uint)endTime.Day;
            struFileCond_V40.struStopTime.dwHour = (uint)endTime.Hour;
            struFileCond_V40.struStopTime.dwMinute = (uint)endTime.Minute;
            struFileCond_V40.struStopTime.dwSecond = (uint)endTime.Second;

            //开始录像文件查找 Start to search video files 
            int m_lFindHandle = HikVideoApi.NET_DVR_FindFile_V40(info.UserId, ref struFileCond_V40);

            if (m_lFindHandle >= 0)
            {

                NET_DVR_FINDDATA_V30 struFileData = new NET_DVR_FINDDATA_V30(); ;
                while (true)
                {
                    //逐个获取查找到的文件信息 Get file information one by one.
                    int result = HikVideoApi.NET_DVR_FindNextFile_V30(m_lFindHandle, ref struFileData);

                    if (result == (int)SelectFileState.NET_DVR_ISFINDING)  //正在查找请等待 Searching, please wait
                    {
                        continue;
                    }
                    else if (result == (int)SelectFileState.NET_DVR_FILE_SUCCESS) //获取文件信息成功 Get the file information successfully
                    {
                        files.Add(new BackFile() { FileName = struFileData.sFileName, StartTime = new DateTime((int)struFileData.struStartTime.dwYear, (int)struFileData.struStartTime.dwMonth, (int)struFileData.struStartTime.dwDay, (int)struFileData.struStartTime.dwHour, (int)struFileData.struStartTime.dwMinute, (int)struFileData.struStartTime.dwSecond), EndTime = new DateTime((int)struFileData.struStopTime.dwYear, (int)struFileData.struStopTime.dwMonth, (int)struFileData.struStopTime.dwDay, (int)struFileData.struStopTime.dwHour, (int)struFileData.struStopTime.dwMinute, (int)struFileData.struStopTime.dwSecond), FileSize = (int)struFileData.dwFileSize, FileType = (int)struFileData.byFileType, Locked = (int)struFileData.byLocked, CardNum = struFileData.sCardNum });
                    }
                    else if (result == (int)SelectFileState.NET_DVR_FILE_NOFIND || result == (int)SelectFileState.NET_DVR_NOMOREFILE)
                    {
                        break; //未查找到文件或者查找结束，退出   No file found or no more file found, search is finished 
                    }
                    else
                    {
                        break;
                    }
                }
                HikVideoApi.NET_DVR_FindClose_V30(m_lFindHandle);
            }
            else
            {
                int error = HikOperate.GetLastError();
                if (error == 19  || error == 4)
                {
                    HikVideoApi.NET_DVR_FindClose_V30(m_lFindHandle);
                    return files;
                }
                if(error == 7 || error == 73)
                    return null;
            }
            return files;
        }
        #endregion

        #region 下载录像
        /// <summary>
        /// 根据文件名称下载录像
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="savePath">保存全路径</param>
        /// <returns>true:下载执行成功，false：操作执行失败</returns>
        public bool DownloadFileByName(ref DvrUseInfo info,string savePath)
        {
            //if (info.DownId >= 0)
            //{
            //    //正在下载，请先停止下载
            //    return false;
            //}

            //string sVideoFileName;  //录像文件保存路径和文件名 the path and file name to save      
            //sVideoFileName = "Downtest_" + sPlayBackFileName + ".mp4";

            //按文件名下载 Download by file name
            info.DownId = HikVideoApi.NET_DVR_GetFileByName(info.UserId, info.DownName, savePath);
            if (info.DownId < 0)
                return false;
            return true;
        }

        /// <summary>
        /// 根据时间下载录像文件
        /// </summary>
        /// <param name="info">登陆设备时的UseInfo对象</param>
        /// <param name="channel">通道号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="savePath">保存全路径</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool DownloadFileByTime(ref DvrUseInfo info,int channel,DateTime startTime,DateTime endTime,string savePath)
        {
            //if (info.DownId >= 0)
            //    return false;//(uint)iChannelNum[channel] 
            NET_DVR_PLAYCOND struDownPara = new NET_DVR_PLAYCOND() { dwChannel = (uint)channel /*通道号 Channel number  */, byDrawFrame = 0, byStreamType = 0 };

            //设置下载的开始时间 Set the starting time
            struDownPara.struStartTime.dwYear = (uint)startTime.Year;
            struDownPara.struStartTime.dwMonth = (uint)startTime.Month;
            struDownPara.struStartTime.dwDay = (uint)startTime.Day;
            struDownPara.struStartTime.dwHour = (uint)startTime.Hour;
            struDownPara.struStartTime.dwMinute = (uint)startTime.Minute;
            struDownPara.struStartTime.dwSecond = (uint)startTime.Second;

            //设置下载的结束时间 Set the stopping time
            struDownPara.struStopTime.dwYear = (uint)endTime.Year;
            struDownPara.struStopTime.dwMonth = (uint)endTime.Month;
            struDownPara.struStopTime.dwDay = (uint)endTime.Day;
            struDownPara.struStopTime.dwHour = (uint)endTime.Hour;
            struDownPara.struStopTime.dwMinute = (uint)endTime.Minute;
            struDownPara.struStopTime.dwSecond = (uint)endTime.Second;

            //按时间下载 Download by time
            info.DownId = HikVideoApi.NET_DVR_GetFileByTime_V40(info.UserId, savePath, ref struDownPara);
          
            if (info.DownId < 0)
                return false;

            uint iOutValue = 0;
            if (!HikVideoApi.NET_DVR_PlayBackControl_V40(info.DownId,(uint)PlayBackControlCmd.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                int x = HikOperate.GetLastError();
                return false;
            }
            return true;
        }
        #endregion

        #region 停止录像下载
        /// <summary>
        /// 停止录像下载
        /// </summary>
        /// <param name="info">执行下载时的UseInfo对象</param>
        /// <returns>true:停止下载执行成功，false：操作执行失败</returns>
        private bool StopDownload(ref DvrUseInfo info)
        {
            if (info.DownId < 0)
                return false;
            if (!HikVideoApi.NET_DVR_StopGetFile(info.DownId))
                return false;
            info.DownId = -1;
            return true;
        }
        #endregion

        #region 获取下载进度
        /// <summary>
        /// 获取下载进度
        /// </summary>
        /// <param name="info">执行下载时的UseInfo对象</param>
        /// <returns>true:获取成功，false：获取失败，网络异常，或者当完成下载时结束下载失败</returns>
        public bool GetDownProgress(ref DvrUseInfo info)
        {
            //获取下载进度
            int iPos = HikVideoApi.NET_DVR_GetDownloadPos(info.DownId);
            info.DownProgress = iPos;
            if (iPos == 100)  //下载完成
            {
                
                HikVideoApi.NET_DVR_StopGetFile(info.DownId);//停止异常
                //info.DownId = -1;
                //if (!HikDvrApi.NET_DVR_StopGetFile(info.DownId))//停止异常
                //    return false;
            }
            else if (iPos == 200 || iPos == -1) //网络异常，下载失败
            {
                HikVideoApi.NET_DVR_StopGetFile(info.DownId);//停止异常
                //info.DownId = -1;
                return false;
            }
            //else
            //    return false;
            return true;
        }
        /// <summary>
        /// 获取下载进度
        /// </summary>
        /// <param name="downId">下载id</param>
        /// <param name="iPos">下载进度</param>
        /// <returns>true:获取成功，false：获取失败，网络异常，或者当完成下载时结束下载失败</returns>
        public bool GetDownProgress(int downId,ref int iPos)
        {
            //获取下载进度
            iPos = HikVideoApi.NET_DVR_GetDownloadPos(downId);
            //info.DownProgress = iPos;
            if (iPos == 100)  //下载完成
            {

                HikVideoApi.NET_DVR_StopGetFile(downId);//停止异常
                //info.DownId = -1;
                //if (!HikDvrApi.NET_DVR_StopGetFile(info.DownId))//停止异常
                //    return false;
            }
            else if (iPos == 200 || iPos == -1) //网络异常，下载失败
            {
                HikVideoApi.NET_DVR_StopGetFile(downId);//停止异常
                //info.DownId = -1;
                return false;
            }
            //else
            //    return false;
            return true;
        }
        #endregion

        #region 录像回放

        #region 按时间回放录像
        /// <summary>
        /// 按时间回放录像
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象</param>
        /// <param name="channel">通道号</param>
        /// <param name="Handle">显示图像控件句柄，如PictureBox</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool PlaybackByTime(ref DvrUseInfo info,int channel,IntPtr Handle, DateTime startTime, DateTime endTime)
        {
            if (info.PlaybackId >= 0)
            {
                //如果已经正在回放，先停止回放
                if (!HikVideoApi.NET_DVR_StopPlayBack(info.PlaybackId))
                    return false;
                info.PlaybackId = -1;
            }

            NET_DVR_VOD_PARA struVodPara = new NET_DVR_VOD_PARA();
            struVodPara.dwSize = (uint)Marshal.SizeOf(struVodPara);
            struVodPara.struIDInfo.dwChannel = (uint)iChannelNum[(int)channel]; //通道号 Channel number  
            struVodPara.hWnd = Handle;//回放窗口句柄

            //设置回放的开始时间 Set the starting time to search video files
            struVodPara.struBeginTime.dwYear = (uint)startTime.Year;
            struVodPara.struBeginTime.dwMonth = (uint)startTime.Month;
            struVodPara.struBeginTime.dwDay = (uint)startTime.Day;
            struVodPara.struBeginTime.dwHour = (uint)startTime.Hour;
            struVodPara.struBeginTime.dwMinute = (uint)startTime.Minute;
            struVodPara.struBeginTime.dwSecond = (uint)startTime.Second;

            //设置回放的结束时间 Set the stopping time to search video files
            struVodPara.struEndTime.dwYear = (uint)endTime.Year;
            struVodPara.struEndTime.dwMonth = (uint)endTime.Month;
            struVodPara.struEndTime.dwDay = (uint)endTime.Day;
            struVodPara.struEndTime.dwHour = (uint)endTime.Hour;
            struVodPara.struEndTime.dwMinute = (uint)endTime.Minute;
            struVodPara.struEndTime.dwSecond = (uint)endTime.Second;


            //按时间回放 Playback by time
            info.PlaybackId = HikVideoApi.NET_DVR_PlayBackByTime_V40(info.UserId, ref struVodPara);
            if (info.PlaybackId < 0)
                return false;

            uint iOutValue = 0;
            return HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)PlayBackControlCmd.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);

        }
        #endregion

        #region 按时文件名放录像
        /// <summary>
        /// 按时文件名放录像
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象</param>
        /// <param name="Handle">显示图像控件句柄，如PictureBox</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool PlaybackByName(ref DvrUseInfo info, IntPtr Handle)
        {
            if (info.PlaybackId >= 0)
            {
                //如果已经正在回放，先停止回放
                if (!HikVideoApi.NET_DVR_StopPlayBack(info.PlaybackId))
                    return false;
                info.PlaybackId = -1;
            }

            //按文件名回放
            info.PlaybackId = HikVideoApi.NET_DVR_PlayBackByName(info.UserId, info.PlaybackName, Handle);
            if (info.PlaybackId < 0)
                return false;
            uint iOutValue = 0;
            return HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)PlayBackControlCmd.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);
           
        }
        #endregion

        #region 正放切为倒放
        /// <summary>
        /// 正放切为倒放
        /// </summary>
        /// <param name="info">执行回放时的UseInfo对象</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool PlaybackReverse(ref DvrUseInfo info)
        {
            uint iOutValue = 0;
            return HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)PlayBackControlCmd.NET_DVR_PLAY_REVERSE, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);
        }
        #endregion

        #region 倒放切为正放
        /// <summary>
        /// 倒放切为正放
        /// </summary>
        /// <param name="info">执行回放时的UseInfo对象</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool PlaybackForWard(ref DvrUseInfo info)
        {
            uint iOutValue = 0;
            return HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)PlayBackControlCmd.NET_DVR_PLAY_FORWARD, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);
        }
        #endregion

        #region 正常速度播放

        /// <summary>
        /// 正常速度播放
        /// </summary>
        /// <param name="info">执行回放时的UseInfo对象</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool PlayNormal(ref DvrUseInfo info)
        {
            uint iOutValue = 0;
            return HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)PlayBackControlCmd.NET_DVR_PLAYNORMAL, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);
        
        }
        #endregion

        #region 单帧播放
        /// <summary>
        /// 单帧播放
        /// </summary>
        /// <param name="info">执行回放时的UseInfo对象</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool PlayFrame(ref DvrUseInfo info)
        {
            uint iOutValue = 0;

            return HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)PlayBackControlCmd.NET_DVR_PLAYFRAME, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);
           
        }
#endregion

        #region 快速播放
        /// <summary>
        /// 快速播放
        /// </summary>
        /// <param name="info">执行回放时的UseInfo对象</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool PlayFast(ref DvrUseInfo info)
        {
            uint iOutValue = 0;

            return HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)PlayBackControlCmd.NET_DVR_PLAYFAST, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);
           
        }
        #endregion

        #region 慢速播放
        /// <summary>
        /// 慢速播放
        /// </summary>
        /// <param name="info">执行回放时的UseInfo对象</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool PlaySlow(ref DvrUseInfo info)
        {
            uint iOutValue = 0;

            return HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)PlayBackControlCmd.NET_DVR_PLAYSLOW, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);
           
        }
        #endregion

        #region 暂停播放
        /// <summary>
        /// 暂停播放
        /// </summary>
        /// <param name="info">执行回放时的UseInfo对象</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool PlayPause(ref DvrUseInfo info)
        {
            uint iOutValue = 0;

            return HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)PlayBackControlCmd.NET_DVR_PLAYPAUSE, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);
            
        }
        #endregion

        #region 恢复播放
        /// <summary>
        /// 恢复播放
        /// </summary>
        /// <param name="info">执行回放时的UseInfo对象</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool PlayReStart(ref DvrUseInfo info)
        {
            uint iOutValue = 0;
            return HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)PlayBackControlCmd.NET_DVR_PLAYRESTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);

        }
        #endregion

        #region 获取回放进度
        /// <summary>
        /// 获取回放进度
        /// </summary>
        /// <param name="info">执行回放时的UseInfo对象</param>
        /// <returns>true:获取成功，false：获取失败，网络异常，或者当完成回放时结束回放失败</returns>
        public bool GetPlaybackProgress(ref DvrUseInfo info)
        {
            uint iOutValue = 0;
            int iPos = 0;
            IntPtr lpOutBuffer = Marshal.AllocHGlobal(4);
            //获取回放进度
            if (!HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)PlayBackControlCmd.NET_DVR_PLAYGETPOS, IntPtr.Zero, 0, lpOutBuffer, ref iOutValue))
                return false;

            iPos = (int)Marshal.PtrToStructure(lpOutBuffer, typeof(int));

            info.PlaybackProgress = iPos;
            if (iPos == 100)  //回放结束
                if (!HikVideoApi.NET_DVR_StopPlayBack(info.PlaybackId))
                    return false;
            if (iPos == 200) //网络异常，回放失败
                return false;
            Marshal.FreeHGlobal(lpOutBuffer);
            return true;
        }
        #endregion

        #region 停止回放
        /// <summary>
        /// 停止回放
        /// </summary>
        /// <param name="info">执行回放时使用的UseInfo对象</param>
        /// <returns>true：操作成功，false：操作失败</returns>
        public bool StopPlayBack(ref DvrUseInfo info)
        {
            bool b = false;
            //停止回放 Stop playback
            if (info.PlaybackId >= 0)
            {
                b = HikVideoApi.NET_DVR_StopPlayBack(info.PlaybackId);
                info.PlaybackId = -1;
            }
            return b;
        }
        #endregion

        #region 控制录像回放状态
        /// <summary>
        /// 控制录像回放状态
        /// </summary>
        /// <param name="info">执行回放时的UseInfo对象</param>
        /// <param name="cmd">控制录像回放状态命令</param>
        /// <returns>true：执行成功，false：执行失败</returns>
        public bool PlaybackControl(ref DvrUseInfo info, PlayBackControlCmd cmd)
        {
            uint iOutValue = 0;
            return HikVideoApi.NET_DVR_PlayBackControl_V40(info.PlaybackId, (uint)cmd, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);
        }
        #endregion
        #endregion

        #region 抓取图片
        /// <summary>
        /// 抓图（在预览时）
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象</param>
        /// <returns>true：抓图保存成功，false：抓图保存失败</returns>
        public bool GetBMPImage(ref DvrUseInfo info)
        {
            if (info.PlaybackId < 0)
                return false;

            //BMP抓图 Capture a BMP picture
            return HikVideoApi.NET_DVR_PlayBackCaptureFile(info.PlaybackId, info.SaveBMPPath);
        }
        #endregion

        #region 登出设备

        /// <summary>
        /// 登出设备
        /// </summary>
        /// <param name="info">所有操作使用的UseInfo对象</param>
        /// <returns>true：操作成功，false：操作失败</returns>
        public bool Logout(ref DvrUseInfo info)
        {
            bool b = false;
            //停止回放 Stop playback
            if (info.PlaybackId >= 0)
            {
                b= HikVideoApi.NET_DVR_StopPlayBack(info.PlaybackId);
                info.PlaybackId = -1;
            }

            //停止下载 Stop download
            if (info.DownId >= 0)
            {
                b=HikVideoApi.NET_DVR_StopGetFile(info.DownId);
                info.DownId = -1;
            }

            //注销登录 Logout the device
            if (info.UserId >= 0)
            {
                b = HikApi.NET_DVR_Logout(info.UserId);
                info.UserId = -1;
            }
            return b;
        }
        #endregion

        #region 登录设备
        /// <summary>
        /// 登录设备
        /// </summary>
        /// <param name="info">登录设备所需的信息</param>
        /// <returns>true：成功，false：失败</returns>
        public bool LoginDvr_V30(ref DvrUseInfo info)
        {
            bool b = false;   
            if (info.UserId <=-1)
            {
                string DVRIPAddress = info.DvrIp; //设备IP地址或者域名
                short DVRPortNumber = short.Parse(info.DvrPoint.ToString());//设备服务端口号
                string DVRUserName = info.UserName;//设备登录用户名
                string DVRPassword =info.UserPwd;//设备登录密码
                NET_DVR_DEVICEINFO_V30 DeviceInfo = new NET_DVR_DEVICEINFO_V30();
                //登录设备 Login the device
                info.UserId = HikApi.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (info.UserId < 0)
                    b= false;
                else
                    b= true;
            }
            return b;
        }
        #endregion

        #region 注册回调函数
        ///// <summary>
        ///// 注册回调函数，接收设备报警消息等。
        ///// </summary>
        ///// <returns>TRUE表示成功，FALSE表示失败。</returns>
        //public bool SetDVRMessageCallBack_V30()
        //{
        //    if (AlarmCallBack == null)
        //        AlarmCallBack += HikDvrOperate_AlarmCallBack;
        //    if (AlarmCallBackV31 == null)
        //        AlarmCallBackV31 += HikDvrOperate_AlarmCallBack;
        //    return HikPublicApi.NET_DVR_SetDVRMessageCallBack_V30(AlarmCallBack, IntPtr.Zero);
        //}
        ///// <summary>
        ///// 注册回调函数，接收设备报警消息等。
        ///// </summary>
        ///// <returns>TRUE表示成功，FALSE表示失败。</returns>
        //public bool SetDVRMessageCallBack_V31()
        //{
        //    //if (ListenResult == null)
        //    //    ListenResult += HikDvrOperate_AlarmCallBack;
        //    //if (AlarmCallBack == null)
        //    //    AlarmCallBack += HikDvrOperate_AlarmCallBack;
        //    if (AlarmCallBackV31 == null)
        //        AlarmCallBackV31 += HikDvrOperate_AlarmCallBack;
        //    return HikPublicApi.NET_DVR_SetDVRMessageCallBack_V31(AlarmCallBackV31, IntPtr.Zero);
        //    //return false;
        //}
        #endregion

        #region 回掉处理

        /// <summary>
        /// 回掉处理
        /// </summary>
        /// <param name="lCommand"></param>
        /// <param name="pAlarmer"></param>
        /// <param name="pAlarmInfo"></param>
        /// <param name="dwBufLen"></param>
        /// <param name="pUser"></param>
        public void HikDvrOperate_AlarmCallBack(int lCommand, ref NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            try
            {
                //通过lCommand来判断接收到的报警信息类型，不同的lCommand对应不同的pAlarmInfo内容
                switch (lCommand)
                {
                    case COMM_ALARM: //(DS-8000老设备)移动侦测、视频丢失、遮挡、IO信号量等报警信息
                        ProcessCommAlarm?.Invoke(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
                        break;
                    case COMM_ALARM_V30://移动侦测、视频丢失、遮挡、IO信号量等报警信息
                        ProcessCommAlarm_V30?.Invoke(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
                        break;
                    case COMM_ALARM_RULE://进出区域、入侵、徘徊、人员聚集等行为分析报警信息
                        ProcessCommAlarm_RULE?.Invoke(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
                        break;
                    case COMM_UPLOAD_PLATE_RESULT://交通抓拍结果上传(老报警信息类型)
                        ProcessCommAlarm_Plate?.Invoke(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
                        break;
                    case COMM_ITS_PLATE_RESULT://交通抓拍结果上传(新报警信息类型)
                        ProcessCommAlarm_ITSPlate?.Invoke(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception) { }
        }
        #endregion

        #region 对设备进行布撤防
        /// <summary>
        /// 建立报警上传通道，接收报警信息。
        /// </summary>
        /// <param name="info">登陆设备时的UserInfo对象</param>
        /// <param name="level">布防优先级：0- 一等级（高），1- 二等级（中），2- 三等级（低，保留）</param>
        /// <returns>true：成功，false：失败</returns>
        public bool SetupAlarmChan_V41(ref DvrUseInfo info,byte level=1)
        {
            int alarmId = info.AlarmId;
            HikOperate.SetupAlarmChan_V41(info.UserId, ref alarmId, level);
            info.AlarmId = alarmId;
            if (info.AlarmId < 0)
                return false;
            else
                return true;
        }
     
        /// <summary>
        /// 撤销报警上传通道
        /// </summary>
        /// <param name="info">建立报警上传通道时的UserInfo对象</param>
        /// <returns>true：成功，false：失败</returns>
        public bool CloseAlarmChanV30(ref DvrUseInfo info)
        {

            if (info.UserId >= 0)
                return HikApi.NET_DVR_CloseAlarmChan_V30(info.AlarmId);
            else
                return false;
        }
        #endregion

        #region 对设备进行监听
        /// <summary>
        /// 启动监听，接收设备主动上传的报警等信息（支持多线程）。
        /// </summary>
        /// <param name="alarmCallBack">报警消息回调</param>
        /// <param name="listenId">返回监听ID</param>
        /// <param name="listenIp">监听IP</param>
        /// <param name="listenPort">监听端口</param>
        /// <returns>true：成功，false：失败</returns>
        public bool StartLisenV30(HikDelegate.MSGCallBack alarmCallBack, ref int listenId, string listenIp,int listenPort=7100)
        {
                if (string.IsNullOrWhiteSpace(listenIp) || string.IsNullOrWhiteSpace(listenPort.ToString()))
                    return false;
                listenId = HikApi.NET_DVR_StartListen_V30(listenIp, (short)listenPort, alarmCallBack, IntPtr.Zero);
                uint x = HikApi.NET_DVR_GetLastError();
                if (listenId < 0)
                    return false;
                else
                    return true;
        }
       
        /// <summary>
        /// 停止设备监听
        /// </summary>
        /// <param name="listenId">监听设备时Id</param>
        /// <returns>true：成功，false：失败</returns>
        public bool StopListenV30(ref int listenId)
        {
            
            bool b= HikApi.NET_DVR_StopListen_V30(listenId);
            if (b)
            {
                listenId = -1;
                return true;
            }
            else
                return false;
          
        }
        #endregion

        #region 工作状态

        /// <summary>
        /// 获取设备工作状态
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="lpWorkState">设备工作状态信息体</param>
        /// <returns>true：成功，false：失败</returns>
        public bool GetDeviceWorkState(int userId, ref NET_DVR_WORKSTATE lpWorkState)
        {
            return HikApi.NET_DVR_GetDVRWorkState(userId, ref lpWorkState);
        }
        /// <summary>
        /// 获取设备工作状态
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="status">工作状态</param>
        /// <returns>true：获取成功，false：获取失败</returns>
        public bool GetDVRWorkState_V30(int userId, ref NET_DVR_WORKSTATE_V30 status)
        {
            return HikApi.NET_DVR_GetDVRWorkState_V30(userId, ref status);
        }
        /// <summary>
        /// 设备工作状态事件
        /// </summary>
        static event HikVideoDelegate.DEV_WORK_STATE_CB workState;//HikPublicDelegate.DEV_WORK_STATE_CB
        /// <summary>
        /// 设备巡检工作状态委托
        /// </summary>
        /// <param name="state"></param>
        public delegate void DeviceWrokState(NET_DVR_WORKSTATE_V40 state);//HikStruct.NET_DVR_WORKSTATE_V40
        /// <summary>
        /// 设备巡检工作状态事件
        /// </summary>
        public static event DeviceWrokState WorkState;
        /// <summary>
        /// 启动设备状态巡检
        /// </summary>
        /// <param name="timer">定时检测设备工作状态，单位：ms，0表示使用默认值(30000)，最小值为1000 </param>
        /// <returns>TRUE表示成功，FALSE表示失败。</returns>
        /// <remarks>
        /// 启动后，SDK定时巡检设备，获取到的设备状态信息在结构体的回调函数中返回。相当于实现定时调用NET_DVR_GetDeviceConfig(命令：NET_DVR_GET_WORK_STATUS)。
        /// </remarks>
        public bool StartGetDevState(int timer = 0)
        {
            workState += FrmWNServer_workState;
            NET_DVR_CHECK_DEV_STATE state = new NET_DVR_CHECK_DEV_STATE() { byRes = new byte[60], dwTimeout = (uint)timer, pUserData = IntPtr.Zero, fnStateCB = workState };
            uint dwSize = (uint)Marshal.SizeOf(state);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(state, ptrIpParaCfgV40, false);
            return HikApi.NET_DVR_StartGetDevState(ptrIpParaCfgV40);
        }

        #region 工作状态处理
        /// <summary>
        /// 海康dvr设备工作状态处理
        /// </summary>
        /// <param name="pUserdata"></param>
        /// <param name="lUserID"></param>
        /// <param name="lpWorkState"></param>
        private void FrmWNServer_workState(IntPtr pUserdata, int lUserID, NET_DVR_WORKSTATE_V40 lpWorkState)//HikDeviceApi.HikStruct.NET_DVR_WORKSTATE_V40
        {
            WorkState?.Invoke(lpWorkState);
            //(new Thread(new ParameterizedThreadStart(State)) { IsBackground = true }).Start(lpWorkState);
        }
        #endregion

        /// <summary>
        /// 停止设备巡检
        /// </summary>
        /// <returns></returns>
        public bool StopGetDevState()
        {
            workState -= FrmWNServer_workState;
            return HikApi.NET_DVR_StopGetDevState();
        }
        #endregion

        #region  获取通道信息

        /// <summary>
        /// 获取IP通道信息
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象</param>
        /// <returns>通道属性对象集合</returns>
        private List<ChannelState> GetIPChannelInfo(ref DvrUseInfo info)
        {
            NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40 = new NET_DVR_IPPARACFG_V40();
            NET_DVR_GET_STREAM_UNION m_unionGetStream = new NET_DVR_GET_STREAM_UNION();
            NET_DVR_IPCHANINFO m_struChanInfo = new NET_DVR_IPCHANINFO();
            List<ChannelState> states = new List<ChannelState>();
            uint dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40);

            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(m_struIpParaCfgV40, ptrIpParaCfgV40, false);

            uint dwReturn = 0;
            if (!HikApi.NET_DVR_GetDVRConfig(info.UserId, NET_DVR_GET_IPPARACFG_V40, -1, ptrIpParaCfgV40, dwSize, ref dwReturn))
            {
                return states;
            }
            else
            {
                m_struIpParaCfgV40 = (NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(NET_DVR_IPPARACFG_V40));

                byte byStreamType;
                for (int i = 0; i < m_struIpParaCfgV40.dwDChanNum; i++)
                {
                    ChannelState state = new ChannelState();
                    iChannelNum[i + dwAChanTotalNum] = i + (int)m_struIpParaCfgV40.dwStartDChan;
                    byStreamType = m_struIpParaCfgV40.struStreamMode[i].byGetStreamType;
                    m_unionGetStream = m_struIpParaCfgV40.struStreamMode[i].uGetStream;
                    switch (byStreamType)
                    {
                        //目前NVR仅支持 0- 直接从设备取流一种方式
                        case 0:
                            dwSize = (uint)Marshal.SizeOf(m_unionGetStream);
                            IntPtr ptrChanInfo = Marshal.AllocHGlobal((Int32)dwSize);
                            Marshal.StructureToPtr(m_unionGetStream, ptrChanInfo, false);
                            m_struChanInfo = (NET_DVR_IPCHANINFO)Marshal.PtrToStructure(ptrChanInfo, typeof(NET_DVR_IPCHANINFO));
                            state.Num = i + 1;
                            state.IsIpChannel = true;
                           
                            if (m_struChanInfo.byIPID == 0)
                            {
                                state.IsNull = true;  //通道空闲，没有添加前端设备                 
                            }
                            else
                            {
                                state.IsNull = false;
                                if (m_struChanInfo.byEnable == 0)
                                    state.IsOnline = false; //通道不在线
                                else
                                    state.IsOnline = true; //通道在线
                            }
                            Marshal.FreeHGlobal(ptrChanInfo);
                            states.Add(state);
                            break;

                        default:
                            break;
                    }
                }
            }
            Marshal.FreeHGlobal(ptrIpParaCfgV40);
            return states;
        }
        /// <summary>
        /// 获取模拟通道信息
        /// </summary>
        /// <param name="info">登录设备时的UseInfo对象</param>
        /// <returns>通道属性对象集合</returns>
        private List<ChannelState> GetAnalogChannelInfo(ref DvrUseInfo info)
        {
            NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40 = new NET_DVR_IPPARACFG_V40();
            NET_DVR_DEVICEINFO_V30 deviceInfo = new NET_DVR_DEVICEINFO_V30();
            List<ChannelState> states = new List<ChannelState>();
            uint dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40);

            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(m_struIpParaCfgV40, ptrIpParaCfgV40, false);

            uint dwReturn = 0;
            if (!HikApi.NET_DVR_GetDVRConfig(info.UserId, NET_DVR_GET_IPPARACFG_V40, -1, ptrIpParaCfgV40, dwSize, ref dwReturn))
            {
                return states;
            }
            else
            {
                m_struIpParaCfgV40 = (NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(NET_DVR_IPPARACFG_V40));
                for (int i = 0; i < dwAChanTotalNum; i++)
                {
                    ChannelState state = new ChannelState() { Num = i + 1, IsIpChannel = false };
                    if (m_struIpParaCfgV40.byAnalogChanEnable[i] == 0)
                        state.IsEnabled = false;  //通道已被禁用 This channel has been disabled               
                    else
                        state.IsEnabled = true;  //通道处于启用状态
                    states.Add(state);
                    iChannelNum[i] = i + (int)deviceInfo.byStartChan;
                }
            }
            Marshal.FreeHGlobal(ptrIpParaCfgV40);
            return states;
        }
        #endregion
    }
}
