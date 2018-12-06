using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using static HikDeviceApi.Door.HikDoorOperate;

namespace HikDeviceApi.Door
{

    /// <summary>
    /// 说明：门禁操作
    /// 时间：2016-06-07
    /// 作者：痞子少爷
    /// </summary>
    public class HikDoorManager
    {
        //public List<Thread> ThreadList = new List<Thread>();
        ///// <summary>
        ///// 门禁事件消息委托
        ///// </summary>
        ///// <param name = "str" > 消息 </ param >
        //public delegate void DoorMessage(string str);
        /// <summary>
        /// 门禁卡信息查询回调委托
        /// </summary>
        /// <param name = "config" > 消息 </param>
        public delegate void GetDoorCardInfo(HikDoorStruct.NET_DVR_CARD_CFG config);
        ///// <summary>
        ///// 门禁消息事件
        ///// </summary>
        //public event DoorMessage Message;
        /// <summary>
        /// 操作信息委托
        /// </summary>
        /// <param name="msg"></param>
        public delegate void ReturnMsg(string msg);
        /// <summary>
        /// 操作信息
        /// </summary>
        public event ReturnMsg TextMsg;
        ///// <summary>
        ///// ab门刷卡事件委托
        ///// </summary>
        ///// <param name="strs">卡号 ip 门编号 读卡器编号 时间</param>
        //public delegate void ABDoorMessage(string[] strs);
        ///// <summary>
        ///// ab门刷卡事件
        ///// </summary>
        //public event ABDoorMessage AbDoorMsg;
        ///// <summary>
        ///// 远程参数配置状态
        ///// </summary>
        //public event HikDeviceApi.HikDelegate.fRemoteConfigCallback RemoteStatus;
        ///// <summary>
        ///// 远程参数配置数据
        ///// </summary>
        //public event HikDeviceApi.HikDelegate.fRemoteConfigCallback RemoteData;
        /// <summary>
        /// 远程参数配置进度
        /// </summary>
        public event HikDelegate.fRemoteConfigCallback RemoteProgress;
        /// <summary>
        /// 远程获取门禁卡完成事件
        /// </summary>
        public delegate void SetCardFinished(List<HikDoorStruct.NET_DVR_CARD_CFG> cards);
        /// <summary>
        /// 登陆状态委托
        /// </summary>
        /// <param name="userInfo">设备操作信息</param>
        /// <param name="state">状态，-1失败 否则为用户ID</param>
        public delegate void LoginState(DoorUseInfo userInfo,int state);
        /// <summary>
        /// 登陆状态事件
        /// </summary>
        public event LoginState LoginStatus;
        /// <summary>
        /// 设置门禁卡数据结束事件
        /// </summary>
        public event SetCardFinished SetCardFinish;
        /// <summary>
        /// 查询门禁卡信息事件
        /// </summary>
        public event GetDoorCardInfo CardInfo;

        /// <summary>
        /// 门禁操作对象
        /// </summary>
        public HikDoorOperate operate = new HikDoorOperate();
        /// <summary>
        /// 使用线程开启门禁服务
        /// </summary>
        /// <param name="useInfo">门禁数据对象</param>
        public void HikDoorServerOpenThread(List<DoorUseInfo> useInfo)
        {
            (new Thread(new ParameterizedThreadStart(StartServer)) { IsBackground = true }).Start(useInfo);
        }
        /// <summary>
        /// 开启门禁服务
        /// </summary>
        /// <param name="useInfo">门禁数据对象</param>
        public void HikDoorServerOpen(List<DoorUseInfo> useInfo)
        {
            StartServer(useInfo);
        }
        private void StartServer(object obj)
        {
            List<DoorUseInfo> useInfo = obj as List<DoorUseInfo>;
            isReConnect = true;
            useInfos.Clear();
            //operate.ListenResult += Operate_ListenResult;
            //operate.SetDVRMessageCallBack_V31();
            //operate.CardInfo += Operate_CardInfo;
            operate.RemoteData += Operate_RemoteData;
            operate.RemoteStatus += Operate_RemoteStatus;
            for (int i = 0; i < useInfo.Count; i++)
            {
                // ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(InitDevice), useInfo[i]);
                (new Thread(new ParameterizedThreadStart(InitDevice)) { IsBackground = true }).Start(useInfo[i]);
                //InitDevice(useInfo[i]);
                Thread.Sleep(1000);
            }
        }

        #region 远程配置
        /// <summary>
        /// 远程配置状态
        /// </summary>
        /// <param name="dwType"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="dwBufLen"></param>
        /// <param name="pUserData"></param>
        private void Operate_RemoteStatus(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData)
        {
            byte[] byStatus = new byte[4];
            Marshal.Copy(lpBuffer, byStatus, 0, 4);
            int dwStatus = BitConverter.ToInt32(byStatus, 0);
            if (dwStatus == (int)HikDoorEnum.RemoteSetStatus.NET_SDK_CALLBACK_STATUS_SUCCESS || dwStatus == (int)HikDoorEnum.RemoteSetStatus.NET_SDK_CALLBACK_STATUS_FAILED || dwStatus == (int)HikDoorEnum.RemoteSetStatus.NET_SDK_CALLBACK_STATUS_EXCEPTION)
            {
                //(new Thread(new ThreadStart(StopRemote)) { IsBackground = true }).Start();
                SetCardInfo info =new SetCardInfo();
                if (pUserData != IntPtr.Zero)
                {
                    info = (SetCardInfo)Marshal.PtrToStructure(pUserData, typeof(SetCardInfo));
                    TextMsg?.Invoke(string.Format("门禁主机--{0} 门禁权限--{1} 门禁卡操作完成  （{2}）", info.DeviceIp, info.DevicePoint, DateTime.Now));
                }
                StopRemote();
                SetCardFinish?.Invoke(cardConfigs);
            }
        }

        private void StopRemote()
        {
            bool b = operate.StopRemoteConfig();
        }
        /// <summary>
        /// 门禁卡信息集合
        /// </summary>
        static List<HikDoorStruct.NET_DVR_CARD_CFG> cardConfigs = new List<HikDoorStruct.NET_DVR_CARD_CFG>();
        /// <summary>
        /// 远程配置数据回调
        /// </summary>
        /// <param name="dwType">配置状态</param>
        /// <param name="lpBuffer">回调数据</param>
        /// <param name="dwBufLen">数据长度</param>
        /// <param name="pUserData">用户数据</param>
        private void Operate_RemoteData(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData)
        {
            HikDoorStruct.NET_DVR_CARD_CFG cardInfo = (HikDoorStruct.NET_DVR_CARD_CFG)Marshal.PtrToStructure(lpBuffer, typeof(HikDoorStruct.NET_DVR_CARD_CFG));
            string cardNo = cardInfo.byCardNo;
            string carType = Enum.GetName(typeof(HikDoorEnum.CardType), cardInfo.byCardType);
            string txt = string.Format("卡号：{0} 卡密码：{1} 卡类型：{2} 刷卡次数：{3} 是否首卡：{4} 有效期使能：{5} 有效：{6} 门禁权限：{19} 起始日期：{7}-{8}-{9} {10}:{11}:{12} 终止日期：{13}-{14}-{15} {16}:{17}:{18}",
                cardNo, cardInfo.byCardPassword, carType, cardInfo.dwSwipeTime, Convert.ToString(cardInfo.byLeaderCard) == "1" ? "是" : "否",
                cardInfo.struValid.byEnable.ToString() == "0" ? "否" : "是",
                cardInfo.byCardValid.ToString() == "1" ? "是" : "否",
                cardInfo.struValid.struBeginTime.wYear, cardInfo.struValid.struBeginTime.byMonth, cardInfo.struValid.struBeginTime.byDay, cardInfo.struValid.struBeginTime.byHour, cardInfo.struValid.struBeginTime.byMinute, cardInfo.struValid.struBeginTime.bySecond,
                cardInfo.struValid.struEndTime.wYear, cardInfo.struValid.struEndTime.byMonth, cardInfo.struValid.struEndTime.byDay, cardInfo.struValid.struEndTime.byHour, cardInfo.struValid.struEndTime.byMinute, cardInfo.struValid.struEndTime.bySecond,
                cardInfo.dwDoorRight
                );
            //TextMsg?.Invoke(SuperFramework.SmartConvert.JSONHelper.ObjectToJson(cardInfo));
            TextMsg?.Invoke(txt);
            cardConfigs.Add(cardInfo);
            CardInfo?.Invoke(cardInfo);
        }



        ///// <summary>
        ///// 门禁动态消息回调
        ///// </summary>
        ///// <param name="lCommand">消息类型</param>
        ///// <param name="pAlarmer">报警设备对象</param>
        ///// <param name="pAlarmInfo">报警消息对象</param>
        ///// <param name="dwBufLen">数据长度</param>
        ///// <param name="pUser">用户数据</param>
        //public void Operate_ListenResult(int lCommand, ref HikDeviceApi.Door.HikDoorStruct.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        //{
        //    try
        //    {

        //        if (lCommand == 0x5002)//门禁消息
        //        {
        //            (new Thread(new ParameterizedThreadStart(DoorMsages)) { IsBackground = false }).Start(new List<object> { pAlarmer, pAlarmInfo });
        //            //DoorMsages(new List<object> { pAlarmer, pAlarmInfo });
        //        }
        //    }
        //    catch (Exception) { }
        //}
        #endregion

        //#region 门禁动态回调
        //private void DoorMsages(object objs)
        //{
        //    InfoStruct.DoorActualStaus actualStaus;
        //    List<object> obj = (List<object>)objs;
        //    HikDeviceApi.Door.HikDoorStruct.NET_DVR_ALARMER pAlarmer = (HikDeviceApi.Door.HikDoorStruct.NET_DVR_ALARMER)obj[0];
        //    //HikDeviceApi.HikStruct.NET_DVR_ALARMER pAlarmer = (HikDeviceApi.HikStruct.NET_DVR_ALARMER)obj[0];
        //    IntPtr pAlarmInfo = (IntPtr)obj[1];
        //    HikDeviceApi.Door.HikDoorStruct.NET_DVR_ACS_ALARM_INFO struAlarmInfoV30 = new HikDeviceApi.Door.HikDoorStruct.NET_DVR_ACS_ALARM_INFO();
        //    struAlarmInfoV30 = (HikDeviceApi.Door.HikDoorStruct.NET_DVR_ACS_ALARM_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(HikDeviceApi.Door.HikDoorStruct.NET_DVR_ACS_ALARM_INFO));

        //    if (struAlarmInfoV30.dwMajor == (int)HikDeviceApi.Door.HikDoorEnum.MajorType.MAJOR_EVENT)
        //    {

        //        //long carNo = 0;
        //        //long.TryParse(Encoding.UTF8.GetString(struAlarmInfoV30.struAcsEventInfo.byCardNo).Trim('\0'), out carNo);
        //        //actualStaus.cardIdentify = carNo;

        //        //if ((struAlarmInfoV30.dwMinor > 3 && struAlarmInfoV30.dwMinor <= 15) || struAlarmInfoV30.dwMinor == 27 || struAlarmInfoV30.dwMinor == 28)
        //        //    actualStaus.status = 0;
        //        //else if ((struAlarmInfoV30.dwMinor > 15 && struAlarmInfoV30.dwMinor <= 26) || (struAlarmInfoV30.dwMinor > 28 && struAlarmInfoV30.dwMinor <= 34))
        //        //    actualStaus.status = 0;
        //        //else if (struAlarmInfoV30.dwMinor < 3)
        //        //    actualStaus.status = 0;
        //        //else
        //        actualStaus.status = 0;
        //        if (struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门磁正常关闭)//|||| struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.常开状态开始|| struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门磁打开超时  struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门磁异常打开|| struAlarmInfoV30.dwMinor== (int)HikDoorEnum.MajorEvent.门锁打开  struAlarmInfoV30.dwMinor == (int)HikDoorEnum.MajorEvent.开门按钮打开 ||
        //            actualStaus.status = 3;
        //        if (struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门磁正常开启 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.互锁门未关闭 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门磁异常打开)//  || struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.常开状态结束 |||| struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.常关状态结束|| struAlarmInfoV30.dwMinor == (int)HikDoorEnum.MajorEvent.门锁关闭
        //            actualStaus.status = 2;
        //        if (struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.刷卡加密码认证通过 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.合法卡认证通过 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.多重多重认证需要远程开门 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.多重认证成功 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.多重认证超级密码认证成功事件 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.常关状态开始 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.常关状态结束 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.常开状态开始 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.常开状态结束 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.开门按钮打开 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.开门按钮放开 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.报警输出打开 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门磁正常关闭 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门磁正常开启 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门锁关闭 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门锁打开 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.首卡开门结束 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.首卡开门开始 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.刷卡加密码认证超时)
        //        {
        //            if (struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门磁异常打开)
        //            {
        //                SendDoorEventInfo(struAlarmInfoV30, pAlarmer.sDeviceIP, Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorEvent), struAlarmInfoV30.dwMinor), actualStaus.status);
        //            }
        //            else
        //            {
        //                if (struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.无此卡号)
        //                {
        //                    if (struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.互锁门未关闭 ||
        //                        struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.刷卡加密码认证失败 ||
        //                        struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.刷卡加密码超次 ||
        //                        struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.多重认证模式超级权限认证失败 ||
        //                        struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.多重认证模式远程认证失败 ||
        //                        struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门磁打开超时 ||
        //                        struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.报警输出关闭)//去掉门磁打开异常报警struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorEvent.门磁异常打开 ||
        //                        SendAlarmInfo(struAlarmInfoV30, pAlarmer.sDeviceIP, Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorEvent), struAlarmInfoV30.dwMinor));
        //                }
        //                else
        //                {
        //                    AbDoorMsg?.Invoke(new string[] { Encoding.UTF8.GetString(struAlarmInfoV30.struAcsEventInfo.byCardNo).Trim('\0'), pAlarmer.sDeviceIP, struAlarmInfoV30.struAcsEventInfo.dwDoorNo.ToString(), struAlarmInfoV30.struAcsEventInfo.dwCardReaderNo.ToString(), string.Format("{0}-{1}-{2} {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond, Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorEvent), struAlarmInfoV30.dwMinor)) });
        //                }
        //            }
        //        }
        //        else
        //        {
        //            SendDoorEventInfo(struAlarmInfoV30, pAlarmer.sDeviceIP, Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorEvent), struAlarmInfoV30.dwMinor), actualStaus.status);
        //        }

        //        TextMsg?.Invoke(string.Format("门禁主机--{0}  事件类型--{1}  事件时间--{2}  门禁编号--{3}  读卡器编号--{4}  卡号--{5}  门禁卡类型--{6}",
        //            pAlarmer.sDeviceIP,
        //            Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorEvent), struAlarmInfoV30.dwMinor),
        //            string.Format("{0}-{1}-{2} {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //            struAlarmInfoV30.struAcsEventInfo.dwDoorNo,
        //            struAlarmInfoV30.struAcsEventInfo.dwCardReaderNo,
        //            Encoding.UTF8.GetString(struAlarmInfoV30.struAcsEventInfo.byCardNo).Trim('\0'),
        //            Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.CardType), struAlarmInfoV30.struAcsEventInfo.byCardType)
        //            ));

        //    }
        //    else if (struAlarmInfoV30.dwMajor == (int)HikDeviceApi.Door.HikDoorEnum.MajorType.MAJOR_OPERATION)
        //    {
        //        actualStaus.status = 0;
        //        //if (struAlarmInfoV30.dwMinor == (int)HikDoorEnum.MajorOperation.远程关门)
        //        //    actualStaus.status = 3;
        //        //if (struAlarmInfoV30.dwMinor == (int)HikDoorEnum.MajorOperation.远程开门)
        //        //    actualStaus.status = 2;
        //        //if (struAlarmInfoV30.dwMinor == (int)HikDoorEnum.MajorOperation.远程常开)
        //        //    actualStaus.status = 4;
        //        //if (struAlarmInfoV30.dwMinor == (int)HikDoorEnum.MajorOperation.远程常关)
        //        //    actualStaus.status = 5;

        //        if (struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorOperation.远程清空卡号 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorOperation.远程手动关闭报警输出 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorOperation.远程恢复默认参数)//struAlarmInfoV30.dwMinor == (int)HikDoorEnum.MajorOperation.防区撤防 || 
        //            SendAlarmInfo(struAlarmInfoV30, pAlarmer.sDeviceIP, Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorOperation), struAlarmInfoV30.dwMinor));
        //        else if (struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorOperation.远程关门 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorOperation.远程常关 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorOperation.远程开门 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorOperation.远程常开)
        //            SendDoorEventInfo(struAlarmInfoV30, pAlarmer.sDeviceIP, Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorOperation), struAlarmInfoV30.dwMinor), actualStaus.status);
        //        TextMsg?.Invoke(string.Format("门禁主机--{0}  事件类型--{1}  事件时间--{2}  门禁编号--{3}",
        //        pAlarmer.sDeviceIP,
        //        Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorOperation), struAlarmInfoV30.dwMinor),
        //        string.Format("{0}-{1}-{2} {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //        struAlarmInfoV30.struAcsEventInfo.dwDoorNo

        //        ));
        //    }
        //    else if (struAlarmInfoV30.dwMajor == (int)HikDeviceApi.Door.HikDoorEnum.MajorType.MAJOR_EXCEPTION)
        //    {
        //        //if (struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorException.交流电恢复 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorException.看门狗复位 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorException.网络恢复 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorException.蓄电池电压恢复正常 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorException.蓄电池电压低 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorException.设备上电启动 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorException.读卡器掉线恢复 && struAlarmInfoV30.dwMinor != (int)HikDeviceApi.Door.HikDoorEnum.MajorException.RS485连接状态异常恢复)
        //        if (struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorException.交流电断电 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorException.网络断开 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorException.设备掉电关闭 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorException.读卡器掉线 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorException.RS485连接状态异常)
        //            SendAlarmInfo(struAlarmInfoV30, pAlarmer.sDeviceIP, Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorException), struAlarmInfoV30.dwMinor));
        //        TextMsg?.Invoke(string.Format("门禁主机--{0}  事件类型--{1}  事件时间--{2}  门禁编号--{3}",
        //            pAlarmer.sDeviceIP,
        //        Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorException), struAlarmInfoV30.dwMinor),
        //        string.Format("{0}-{1}-{2} {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //        struAlarmInfoV30.struAcsEventInfo.dwDoorNo
        //        ));

        //    }
        //    else if (struAlarmInfoV30.dwMajor == (int)HikDeviceApi.Door.HikDoorEnum.MajorType.MAJOR_ALARM)
        //    {

        //        if (struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorAlarm.读卡器防拆报警 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorAlarm.胁迫报警 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorAlarm.卡号认证失败超次报警 ||
        //            struAlarmInfoV30.dwMinor == (int)HikDeviceApi.Door.HikDoorEnum.MajorAlarm.事件输入报警)
        //            SendAlarmInfo(struAlarmInfoV30, pAlarmer.sDeviceIP, Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorAlarm), struAlarmInfoV30.dwMinor));
        //        TextMsg?.Invoke(string.Format("门禁主机--{0}  事件类型--{1}  事件时间--{2}  门禁编号--{3}  读卡器编号--{4}  卡号--{5}  门禁卡类型--{6}",
        //            pAlarmer.sDeviceIP,
        //            Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.MajorAlarm), struAlarmInfoV30.dwMinor),
        //            string.Format("{0}-{1}-{2} {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //            struAlarmInfoV30.struAcsEventInfo.dwDoorNo,
        //            struAlarmInfoV30.struAcsEventInfo.dwCardReaderNo,
        //            Encoding.UTF8.GetString(struAlarmInfoV30.struAcsEventInfo.byCardNo).Trim('\0'),
        //            Enum.GetName(typeof(HikDeviceApi.Door.HikDoorEnum.CardType), struAlarmInfoV30.struAcsEventInfo.byCardType)

        //            ));
        //    }
        //}
        //#endregion

        //#region 动态数据转发
        ///// <summary>
        ///// 发送门禁报警
        ///// </summary>
        ///// <param name="struAlarmInfoV30">报警信息</param>
        ///// <param name="sDeviceIP">设备ip</param>
        ///// <param name="alarmType">报警类型</param>
        //private void SendAlarmInfo(HikDeviceApi.Door.HikDoorStruct.NET_DVR_ACS_ALARM_INFO struAlarmInfoV30, string sDeviceIP, string alarmType)
        //{
        //    InfoStruct.AlarmInfo alarmInfo = new InfoStruct.AlarmInfo()
        //    {
        //        alertAction = 1,
        //        alertLevel = struAlarmInfoV30.struAcsEventInfo.dwDoorNo,
        //        alertType = 10,
        //        alertTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //        cusNumber = 1,
        //        msgType = InfoStruct.alarmInfo,
        //        alertorIdentify = sDeviceIP,
        //        remark = alarmType
        //    };
        //    if (alarmInfo.remark == null || alarmInfo.remark == "")
        //        return;
        //    Message?.Invoke(SuperFramework.SmartConvert.JSONHelper.ObjectToJson(alarmInfo));

        //}
        ///// <summary>
        ///// 发送门禁动态信息
        ///// </summary>
        ///// <param name="struAlarmInfoV30">报警信息</param>
        ///// <param name="sDeviceIP">设备ip</param>
        ///// <param name="eventType">事件类型</param>
        ///// <param name="status">状态</param>
        //private void SendDoorEventInfo(HikDeviceApi.Door.HikDoorStruct.NET_DVR_ACS_ALARM_INFO struAlarmInfoV30, string sDeviceIP, string eventType, int sta)
        //{
        //    InfoStruct.DoorActualStaus actualStaus = new InfoStruct.DoorActualStaus()
        //    {
        //        brushCardTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}", struAlarmInfoV30.struTime.dwYear, struAlarmInfoV30.struTime.dwMonth, struAlarmInfoV30.struTime.dwDay, struAlarmInfoV30.struTime.dwHour, struAlarmInfoV30.struTime.dwMinute, struAlarmInfoV30.struTime.dwSecond),
        //        cusNumber = "1",
        //        inOutFlag = struAlarmInfoV30.struAcsEventInfo.dwCardReaderNo,
        //        msgType = InfoStruct.doorActualStaus,
        //        status = sta,
        //        remark = eventType,
        //        address = sDeviceIP,
        //        cardIdentify = Encoding.UTF8.GetString(struAlarmInfoV30.struAcsEventInfo.byCardNo).Trim('\0'),
        //        doorControlIdentify = struAlarmInfoV30.struAcsEventInfo.dwCardReaderNo,
        //        doorIdentify = struAlarmInfoV30.struAcsEventInfo.dwDoorNo
        //    };
        //    if (struAlarmInfoV30.struAcsEventInfo.dwCardReaderNo > 2)
        //        actualStaus.inOutFlag -= 2;
        //    if (actualStaus.remark == null || actualStaus.remark == "")
        //        return;
        //    Message?.Invoke(SuperFramework.SmartConvert.JSONHelper.ObjectToJson(actualStaus));
        //}
        //#endregion
        /// <summary>
        /// 门禁使用对象，保存句柄
        /// </summary>
        public static List<DoorUseInfo> useInfos = new List<DoorUseInfo>();
        /// <summary>
        /// 更新门禁使用对象
        /// </summary>
        /// <param name="useInfo"></param>
        private void UpdateUseInfo(DoorUseInfo useInfo)
        {
            try
            {
                int useInfosFindIndex = useInfos.FindIndex(o => o.DeviceIp == useInfo.DeviceIp);
                if(useInfosFindIndex>-1)
                    useInfos[useInfosFindIndex] = useInfo;
            }
            catch (Exception) { }
            //for (int i = 0; i < useInfos.Count; i++)
            //{
            //    if (null==useInfos[i].DeviceIp)
            //        continue;
            //    if (useInfos[i].DeviceIp == useInfo.DeviceIp)
            //    {
            //        useInfos[i] = useInfo;
            //        break;
            //    }
            //}

        }
        #region 门禁登录
        /// <summary>
        /// 是否执行重连
        /// </summary>
        static bool isReConnect = true;
        /// <summary>
        /// 登录设备,尝试5次
        /// </summary>
        /// <param name="obj">门禁数据对象</param>
        public void InitDevice(object obj)
        {
            try
            {
                if (!isReConnect)
                    return;
                DoorUseInfo info = (DoorUseInfo)obj;
                int err = 0;
                info.UserId = -1;
                HikStruct.NET_DVR_DEVICEINFO_V30 infov30 = new HikStruct.NET_DVR_DEVICEINFO_V30();
                for (int p = 0; p < 5; p++)
                {
                    if (!isReConnect) break;
                    if (operate.LoginDoor_V30(ref info, ref infov30))
                    {
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", info.DeviceIp, "注册成功!!!", DateTime.Now.ToString()));
                        LoginStatus?.Invoke(info, info.UserId);
                        //if (useInfos.FindAll(o => o.DeviceIp == info.DeviceIp).Count == 0)
                        //    useInfos.Add(info);
                        //else
                        //    UpdateUseInfo(info);
                        HikOperate.SetDeviceDate(info.UserId, DateTime.Now);
                        bool alarm = false;
                        for (int i = 0; i < 5; i++)
                        {
                            alarm = operate.SetupAlarmChan_V41(ref info);
                            if (alarm)
                                break;
                            Thread.Sleep(10);
                        }
                        if (alarm)//operate.SetupAlarmChanV41(ref info))
                        {
                            //UpdateUseInfo(info);
                            TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", info.DeviceIp, "布防成功!!!", DateTime.Now.ToString()));
                        }
                        else
                        {
                            TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", info.DeviceIp, "布防失败,正在重启主机...", DateTime.Now.ToString()));
                            while (true)
                            {
                                if (HikOperate.ReStartPower(info.UserId))
                                {
                                    Thread.Sleep(3000);
                                    bool b = false;
                                    while (!b)
                                    {
                                        b = operate.SetupAlarmChan_V41(ref info);
                                        if (!b)
                                            Thread.Sleep(20);
                                        else
                                            break;
                                    }
                                    if (b)
                                    {
                                        //UpdateUseInfo(info);
                                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", info.DeviceIp, "重新布防成功!!!", DateTime.Now.ToString()));
                                    }
                                    else
                                    {
                                        while (true)
                                        {
                                            if (!isReConnect)
                                                break;
                                            Thread.Sleep(2000);
                                            if (operate.SetupAlarmChan_V41(ref info))
                                            {
                                                //UpdateUseInfo(info);
                                                TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", info.DeviceIp, "重新布防成功!!!", DateTime.Now.ToString()));
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    err = HikOperate.GetLastError();
                                    TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  {2}  （{3}）", info.DeviceIp, "重启失败,再次重试中...错误码：", err, DateTime.Now));
                                    if (!isReConnect) break;
                                }
                            }
                        }
                        break;
                    }
                    err = HikOperate.GetLastError();
                    //IntPtr y = new IntPtr();
                    //string msg = HikOperate.GetErrorMsg(ref y);
                    //else
                    //{
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  {2}  （{3}）", info.DeviceIp, "注册失败,再次重试中...错误码：", err, DateTime.Now));
                    //    LoginStatus?.Invoke(info, -1);
                    //    if (isReConnect)
                    //    {
                    //        Thread.Sleep(10000);
                    //        (new Thread(new ParameterizedThreadStart(InitDevice)) { IsBackground = true }).Start(obj);
                    //    }
                    //}
                    Thread.Sleep(1000*30);
                }
                if (useInfos.FindAll(o => o.DeviceIp == info.DeviceIp).Count == 0)
                    useInfos.Add(info);
                else
                    UpdateUseInfo(info);
            }

            catch (Exception) { }
        }
        /// <summary>
        /// 门禁上线后重新布防
        /// </summary>
        /// <param name="doorIp">门禁主机IP</param>
        /// <returns>成功：true，false：异常或者取消</returns>
        public bool SetupAlarmChanV41(string doorIp)
        {
            if (useInfos.Count == 0)
                return false;
            DoorUseInfo info = useInfos.Find(o => o.DeviceIp == doorIp);
            bool isOk = false;
            for (int i = 0; i < 50; i++)
            {
                isOk = operate.SetupAlarmChan_V41(ref info);
                if (isOk)
                    break;
                else
                    Thread.Sleep(20);
            }
            if (isOk)
            {
                UpdateUseInfo(info);
                TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", info.DeviceIp, "布防成功!!!", DateTime.Now.ToString()));
                return true;
            }
            else
            {
                TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", info.DeviceIp, "布防失败···", DateTime.Now.ToString()));
            }
            return false;
        }
        /// <summary>
        /// 门禁撤防
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <returns>成功：true，false：异常或者取消</returns>
        public bool CloseAlarmChanV30(string doorIp)
        {
            if (useInfos.Count == 0)
                return false;
            DoorUseInfo info = useInfos.Find(o => o.DeviceIp == doorIp);
            bool isOk = false;
            for (int i = 0; i < 10; i++)
            {
                isOk = operate.CloseAlarmChanV30(ref info);
                if (isOk)
                    break;
                else
                    Thread.Sleep(50);
            }
            if (isOk)
            {
                UpdateUseInfo(info);
                TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", info.DeviceIp, "撤防成功!!!", DateTime.Now.ToString()));
                return true;
            }
            else
            {
                TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", info.DeviceIp, "撤防失败···", DateTime.Now.ToString()));

            }
            return false;
        }
        //private void Operate_CardInfo(HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_CFG cardInfo)
        //{

        //}
        #endregion

        #region 停止门禁服务
        /// <summary>
        /// 停止门禁服务
        /// </summary>
        public void HikDoorServerClose()
        {
            isReConnect = false;
            try
            {
                //ThreadPool.SetMinThreads(200, 200);
                for (int i = 0; i < useInfos.Count; i++)
                {
                    //ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(LoginOutDoor), useInfos[i]);
                   (new Thread(new ParameterizedThreadStart(LoginOutDoor)) { IsBackground = true }).Start(useInfos[i]);
                   Thread.Sleep(20);
                    //LoginOutDoor(useInfos[i]);
                }

                //if (ThreadList.Count > 0)
                //{
                //    foreach (Thread item in ThreadList)
                //    {
                //        try
                //        {
                //            item.Abort();
                //        }
                //        catch (Exception) { }
                //    }
                //}
            }
            catch (Exception)
            {
            }
            finally
            {
                GC.Collect();
                Dispose();
            }


        }
        /// <summary>
        /// 清理资源
        /// </summary>
        private void Dispose()
        {
            useInfos.Clear();
            //ThreadList.Clear();
            //operate.ListenResult -= Operate_ListenResult;
        }
        #endregion

        #region 登出门禁
        private void LoginOutDoor(object item)
        {
            try
            {
                if (operate == null)
                    return;
                DoorUseInfo ui = (DoorUseInfo)item;
                if (operate.CloseAlarmChanV30(ref ui))
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", ui.DeviceIp, "撤销布防成功", DateTime.Now));
                else
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", ui.DeviceIp, "撤销布防失败", DateTime.Now));
                if (operate.LoginOut(ref ui))
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", ui.DeviceIp, "注销成功", DateTime.Now));
                else
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", ui.DeviceIp, "注销失败", DateTime.Now));
                UpdateUseInfo(ui);
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 登出门禁设备
        /// </summary>
        /// <param name="doorIp">设备ip</param>
        public void LoginOutDoor(string doorIp)
        {
            try
            {
                DoorUseInfo ui = useInfos.Find(o => o.DeviceIp == doorIp);
                if (operate.CloseAlarmChanV30(ref ui))
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", ui.DeviceIp, "撤销监视成功", DateTime.Now));
                else
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", ui.DeviceIp, "撤销监视失败", DateTime.Now));
                if (operate.LoginOut(ref ui))
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", ui.DeviceIp, "注销成功", DateTime.Now));
                else
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", ui.DeviceIp, "注销失败", DateTime.Now));
                UpdateUseInfo(ui);
            }
            catch (Exception) { }
        }
        #endregion

        #region 门禁控制
        /// <summary>
        /// 门禁控制
        /// </summary>
        /// <param name="doorIp">主机Ip</param>
        /// <param name="doorNo">门禁编号</param>
        /// <param name="controlType">门禁控制类型</param>
        public void DoorControl(string doorIp, string doorNo, string controlType)
        {
            HikDoorEnum.DoorControl control = (HikDoorEnum.DoorControl)Enum.Parse(typeof(HikDoorEnum.DoorControl), controlType);
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;
                    if (operate.ControlGateway(ui, int.Parse(doorNo), control))
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", item.DeviceIp, control + "成功", DateTime.Now));
                    else
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", item.DeviceIp, control + "失败", DateTime.Now));
                    break;
                }

            }
            //int x = HikDeviceApi.HikOperate.GetLastError();
        }
        #endregion

        #region 查询门禁信息
        /// <summary>
        /// 查询门禁信息
        /// </summary>
        /// <param name="chanel">门禁编号</param>
        /// <param name="doorIp">门禁信息结构体对象</param>
        public void GetDoorInfo(string doorIp, int chanel, ref HikDoorStruct.NET_DVR_DOOR_CFG doorStatus)
        {
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;
                    cardConfigs.Clear();
                    //HikDeviceApi.Door.HikDoorStruct.NET_DVR_DOOR_CFG doorStatus = new HikDeviceApi.Door.HikDoorStruct.NET_DVR_DOOR_CFG();//门参数
                    if (operate.GetDoorInfo(ui, chanel, ref doorStatus))
                    {
                        TextMsg?.Invoke(string.Format("门禁状态:  名称--{0}  通道--{1}  门磁类型--{2}  开门按钮类型--{3}  开门持续时间--{4}  胁迫密码--{5}  超级密码--{6}  闭门回锁--{7}  首卡常开--{8}",
                            doorStatus.byDoorName,//Encoding.UTF8.GetString(doorStatus.byDoorName).Trim('\0'),
                            chanel,
                        doorStatus.byMagneticType.ToString() == "0" ? "常闭" : "常开",
                        doorStatus.byOpenButtonType.ToString() == "0" ? "常闭" : "常开",
                        doorStatus.byOpenDuration,
                        doorStatus.byStressPassword, //Encoding.UTF8.GetString(doorStatus.byStressPassword).Trim('\0'),
                        doorStatus.bySuperPassword,//Encoding.UTF8.GetString(doorStatus.bySuperPassword).Trim('\0'),
                        doorStatus.byEnableDoorLock.ToString() == "0" ? "否" : "是",
                        doorStatus.byEnableLeaderCard.ToString() == "0" ? "否" : "是"));
                        TextMsg?.Invoke(string.Format("门禁主机--{0} {1}（{2}）", item.DeviceIp, "门禁信息查询成功", DateTime.Now));
                    }
                    else
                        TextMsg?.Invoke(string.Format("门禁主机--{0} {1}（{2}）", item.DeviceIp, "门禁信息查询失败", DateTime.Now));
                    break;
                }
            }

        }
        #endregion

        #region 获取主机时间
        /// <summary>
        /// 获取主机时间
        /// </summary>
        /// <param name="DoorIp">门禁主机</param>
        /// <param name="date">主机时间</param>
        public void GetDeviceDate(string DoorIp, ref string date)
        {
            foreach (var item in useInfos)
            {
                if (DoorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;
                    date = HikOperate.GetDeviceTime(ui.UserId);
                    if (date != "")
                    {
                        TextMsg?.Invoke(string.Format("门禁主机--{0} {1}（{2}）", item.DeviceIp, "主机时间 " + date, DateTime.Now.ToString()));
                        TextMsg?.Invoke(string.Format("门禁主机--{0} {1}（{2}）", item.DeviceIp, "时间获取成功 ", DateTime.Now.ToString()));
                    }
                    else
                        TextMsg?.Invoke(string.Format("门禁主机--{0} {1}（{2}）", item.DeviceIp, "时间获取失败", DateTime.Now.ToString()));
                    break;
                }

            }

        }
        #endregion

        #region 获取设备型号
        /// <summary>
        /// 获取设备型号
        /// </summary>
        /// <param name="DoorIp">门禁主机Ip</param>
        /// <param name="infoV40">设备参数结构体对象</param>
        public void GetDeviceInfo(string DoorIp, ref HikStruct.NET_DVR_DEVICECFG_V40 infoV40)
        {
            foreach (var item in useInfos)
            {
                if (DoorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;
                    // HikDeviceApi.HikStruct.NET_DVR_DEVICECFG_V40 infoV40 = new HikDeviceApi.HikStruct.NET_DVR_DEVICECFG_V40();
                    string info = operate.GetDeviceInfo(ui, ref infoV40);
                    if (info != "")
                    {
                        TextMsg?.Invoke(string.Format("门禁主机--{0} {1}（{2}）", item.DeviceIp, info, DateTime.Now.ToString()));
                        TextMsg?.Invoke(string.Format("门禁主机--{0} {1}（{2}）", item.DeviceIp, "设备型号获取成功", DateTime.Now.ToString()));
                    }
                    break;
                }

            }

        }
        #endregion

        #region 设置门禁主机时间
        /// <summary>
        /// 设置门禁主机时间
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        public void SetDeviceDate(string doorIp)
        {

            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;
                    DateTime dt = DateTime.Now;
                    if (HikOperate.SetDeviceDate(ui.UserId, dt))
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  主机时间设置成功:{1}  （{2}）", item.DeviceIp, dt, DateTime.Now));
                    else
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  主机时间设置失败:{1}  （{2}）", item.DeviceIp, dt, DateTime.Now));
                    break;
                }

            }

        }
        #endregion

        #region 重启设备
        /// <summary>
        /// 重启设备
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        public void ReStartDevice(string doorIp)
        {
            foreach (var item in useInfos)
            {
                if (string.Compare(doorIp, item.DeviceIp, false) == 0)
                {
                    DoorUseInfo ui = item;
                    if (HikOperate.ReStartPower(ui.UserId))
                        TextMsg?.Invoke(string.Format("门禁主机--{0} {1}（{2}）", item.DeviceIp, "主机重启成功", DateTime.Now.ToString()));
                    else
                        TextMsg?.Invoke(string.Format("门禁主机--{0} {1}（{2}）", item.DeviceIp, "主机重启失败", DateTime.Now.ToString()));
                    break;
                }

            }

        }
        #endregion

        #region 获取读卡器参数信息
        /// <summary>
        /// 获取读卡器参数信息
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="readNo">门禁编号</param>
        /// <param name="cardReaderConfig">读卡器参数结构体对象</param>
        public void GetReadCardInfo(string doorIp, int readNo, ref HikDoorStruct.NET_DVR_CARD_READER_CFG cardReaderConfig)
        {
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;
                    // HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_READER_CFG cardReaderConfig = new HikDeviceApi.Door.HikDoorStruct.NET_DVR_CARD_READER_CFG();//读卡器配置
                    if (operate.GetCardReaderInfo(ui, readNo, ref cardReaderConfig))
                    {
                        string type = Enum.GetName(typeof(HikDoorEnum.CardReaderType), cardReaderConfig.byCardReaderType);
                        //switch (cardReaderConfig.byCardReaderType.ToString())
                        //{
                        //    case "1":
                        //        type = HikDeviceApi.Door.HikDoorEnum.CardReaderType.DS_K110XM_MK_C_CK.ToString();

                        //        break;
                        //    case "2":
                        //        type = HikDeviceApi.Door.HikDoorEnum.CardReaderType.DS_K182AM_AMP.ToString();
                        //        break;
                        //    case "3":
                        //        type = HikDeviceApi.Door.HikDoorEnum.CardReaderType.DS_K182BM_BMP.ToString();
                        //        break;
                        //    case "4":
                        //        type = HikDeviceApi.Door.HikDoorEnum.CardReaderType.DS_K192AM_AMP.ToString();
                        //        break;
                        //    case "5":
                        //        type = HikDeviceApi.Door.HikDoorEnum.CardReaderType.DS_K192BM_BMP.ToString();
                        //        break;
                        //    default:
                        //        break;
                        //}
                        TextMsg?.Invoke(string.Format("读卡器状态: 类型--{0}  重复刷卡间隔--{1}  按键超时时间--{2}  读卡失败超次报警--{3}  最大读卡失败次数--{4}  防拆检测--{5}  掉线检测时间--{6}",
                          type,
                          cardReaderConfig.bySwipeInterval.ToString(),
                          cardReaderConfig.byPressTimeout.ToString(),
                          cardReaderConfig.byEnableFailAlarm.ToString() == "0" ? " 否" : "是",
                          cardReaderConfig.byMaxReadCardFailNum.ToString(),
                          cardReaderConfig.byEnableTamperCheck.ToString() == "0" ? " 否" : "是",
                          cardReaderConfig.byOfflineCheckTime.ToString()));
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", item.DeviceIp, "读卡器参数获取成功", DateTime.Now.ToString()));
                    }
                    else
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", item.DeviceIp, "读卡器参数获取失败", DateTime.Now.ToString()));
                    break;
                }
            }

        }
        #endregion

        #region 设置门禁参数
        /// <summary>
        /// 设置门禁参数
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="doorNo">门禁编号</param>
        /// <param name="doorName">门禁名称</param>
        /// <param name="stressPassword">胁迫密码 不大于8位</param>
        /// <param name="supperPassword">超级密码 不大于8位</param>
        /// <param name="isEnableDoorLock">是否启用闭门回锁：0- 否，1- 是</param>
        /// <param name="isEnableLeaderCard">是否启用首卡常开功能：0- 否，1- 是 </param>
        /// <param name="isOpenButton">开门按钮类型：0- 常闭，1- 常开</param>
        /// <param name="isOpenMagnetic">门磁类型：0- 常闭，1- 常开</param>
        /// <param name="alarmTimeout">门禁检测超时时间 0--255s 0表示不报警</param>
        /// <param name="openTime">开门持续时间 1~255s</param>
        /// <param name="disabledOpenTime">残疾人开门持续时间 1--255s</param>
        /// <param name="leaderCardOpenTime">首卡常开持续时间 1--1440min</param>
        public void SetDoorInfo(string doorIp, int doorNo, string doorName, uint stressPassword, uint supperPassword, uint isEnableDoorLock = 1, uint isEnableLeaderCard = 0, uint isOpenButton = 0, uint isOpenMagnetic = 0, uint alarmTimeout = 15, uint openTime = 10, uint disabledOpenTime = 20, uint leaderCardOpenTime = 30)
        {
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;
                    if (operate.SetDoorInfo(ui, doorNo, doorName, stressPassword, supperPassword, isEnableDoorLock, isEnableLeaderCard, isOpenButton, isOpenMagnetic, alarmTimeout, openTime, disabledOpenTime, leaderCardOpenTime))
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  门禁编号--{1}  {2}  （{3}）", item.DeviceIp, doorNo, "门禁参数设置成功", DateTime.Now));
                    else
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  门禁编号--{1}  {2}  （{3}）", item.DeviceIp, doorNo, "门禁参数设置失败", DateTime.Now));
                    break;
                }
            }

        }
        #endregion

        #region 获取门禁卡信息
        /// <summary>
        /// 获取门禁卡信息
        /// </summary>
        /// <param name="doorIp">门禁主机IP</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetCardInfo(string doorIp)
        {
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;

                    if (operate.StartRemoteConfig(ref ui))
                    {
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", item.DeviceIp, "获取门禁卡参数成功", DateTime.Now.ToString()));
                        return true;
                    }
                    else
                    {
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", item.DeviceIp, "读取门禁卡参数失败", DateTime.Now.ToString()));
                        return false;
                    }
                }
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 开启远程配置
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <returns></returns>
        public bool StartRemoteConfig(string doorIp)
        {
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;

                    return operate.StartRemoteConfig(ref ui, HikDoorEnum.ConfigCommand.NET_DVR_SET_CARD_CFG, 1);
                }
            }
            return false;
        }

        #region  设置卡参数
        /// <summary>
        /// 设置卡参数
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="cardNo">卡号</param>
        /// <param name="cardPassWord">卡密码</param>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">截至日期</param>
        /// <param name="doorRiht">门权限，按位表示，从低位到高位表示对门1~N是否有权限，值：0- 无权限，1- 有权限（3代表对门1门2有权限 1代表对门1有权限 2代表对门2有权限···）</param>
        /// <param name="cardValid">卡是否有效：0- 无效，1- 有效（用于删除卡，设置时置为0进行删除，获取时此字段始终为1）</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="struValid">是否启用有效期</param>
        /// <param name="leaderCard">是否为首卡：1- 是，0- 否 </param>
        /// <param name="maxSwipeTime">最大刷卡次数，0 为不限制</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetCardInfo(string doorIp, string cardNo, string cardPassWord, DateTime startDate, DateTime endDate, uint doorRiht, uint cardValid = 1, HikDoorEnum.CardType cardType = HikDoorEnum.CardType.普通卡, uint struValid = 1, uint leaderCard = 0, uint maxSwipeTime = 0)
        {
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;
                    if (operate.StartRemoteConfig(ref ui, HikDoorEnum.ConfigCommand.NET_DVR_SET_CARD_CFG, 1))
                    {
                        if (operate.SetCardConfig(ui, cardNo, cardPassWord, startDate, endDate, doorRiht, cardValid, cardType, struValid, leaderCard, maxSwipeTime))
                        {
                            TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  门禁卡号 {2}  （{3}）", doorIp, "设置门禁卡参数成功", cardNo, DateTime.Now));
                            operate.StopRemoteConfig();
                            return true;
                        }
                        //else
                        //{
                        //    int x = HikOperate.GetLastError();
                        //    TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  门禁卡号 {2}  错误码 {3}  （{4}）", doorIp, "设置门禁卡参数失败", cardNo, x,DateTime.Now));

                        //    return false;
                        //}
                        operate.StopRemoteConfig();
                    }
                    //else
                    //{
                        int x = HikOperate.GetLastError();
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  门禁卡号 {2}  错误码 {3}  （{4}）", doorIp, "设置门禁卡参数失败", cardNo, x, DateTime.Now));
                        return false;
                    //}

                }
            }
            return false;
        }
        #endregion

        #region 设置读卡器信息
        /// <summary>
        /// 设置读卡器信息
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="readNo">读卡器编号</param>
        /// <param name="type">读卡器类型</param>
        /// <param name="enableFailAlarm">是否启用读卡失败超次报警：0- 不启用，1- 启用</param>
        /// <param name="enableTamperCheck">是否启用防拆检测：0- 不启用，1- 启用</param>
        /// <param name="maxReadCardFailNum">最大读卡失败次数</param>
        /// <param name="offlineCheckTime">防拆检测时间 0--255 s</param>
        /// <param name="pressTimeout">按键超时时间1--255 s</param>
        /// <param name="swipeInterval">重复刷卡间隔 单位秒</param>
        /// <param name="errorLedPolarity">错误led极性 0-阴极 1-阳极</param>
        /// <param name="okLedPolarity">正常led极性 0-阴极 1-阳极</param>
        /// <param name="buzzerPolarity">蜂鸣器极性：0- 阴极，1- 阳极</param>
        /// <param name="fingerPrintCheckLevel">指纹识别等级：1- 1/10误认率，2- 1/100误认率，3- 1/1000误认率，4- 1/10000误认率，5- 1/100000误认率，6- 1/1000000误认率，7- 1/10000000误认率，8- 1/100000000误认率，9- 3/100误认率，10- 3/1000误认率，11- 3/10000误认率，12- 3/100000误认率，13- 3/1000000误认率，14- 3/10000000误认率，15- 3/100000000误认率，16- Automatic Normal，17- Automatic Secure，18- Automatic More Secure </param>
        public void SetReadCardInfo(string doorIp, int readNo, HikDoorEnum.CardReaderType type = HikDoorEnum.CardReaderType.DS_K110XM_MK_C_CK, uint enableFailAlarm = 1, uint enableTamperCheck = 1, uint maxReadCardFailNum = 5, uint offlineCheckTime = 5, uint pressTimeout = 5, uint swipeInterval = 0, uint errorLedPolarity = 1, uint okLedPolarity = 1, uint buzzerPolarity = 1, uint fingerPrintCheckLevel = 16)
        {
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;

                    if (operate.SetCardReaderInfo(ui, readNo, type, enableFailAlarm, enableTamperCheck, maxReadCardFailNum, offlineCheckTime, pressTimeout, swipeInterval, errorLedPolarity, okLedPolarity, buzzerPolarity, fingerPrintCheckLevel))
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", item.DeviceIp, "读卡器参数设置成功", DateTime.Now));
                    else
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", item.DeviceIp, "读卡器参数设置失败", DateTime.Now));
                    int x = HikOperate.GetLastError();
                    break;
                }
            }

        }
        #endregion

        #region 获取门禁主机工作状态
        /// <summary>
        /// 获取门禁主机工作状态
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="controllerStatus">门禁主机工作状态结构体对象</param>
        /// <returns>true:获取成功，否则失败</returns>
        public bool GetWorkStatus(string doorIp, ref HikDoorStruct.NET_DVR_ACS_WORK_STATUS controllerStatus)
        {
            string[] doorStatus=new string[] { } , lockStatus=new string[] { } ;
            string ds = "", ls = "",ks="",ms="",rs="",cs="";
          
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    //HikDeviceApi.Door.HikDoorStruct.NET_DVR_ACS_WORK_STATUS controllerStatus = new HikDeviceApi.Door.HikDoorStruct.NET_DVR_ACS_WORK_STATUS();//门禁主机工作状态
                    if (operate.GetDeviceWorkStatus(item.UserId, ref doorStatus, ref lockStatus, ref controllerStatus))
                    {
                        for (int i = 0; i < doorStatus.Length; i++)
                        {
                            ds += string.Format(" {0}-{1}", i+1, doorStatus[i]);
                            ms += string.Format(" {0}-{1}", i + 1, controllerStatus.byMagneticStatus[i] == 0 ? "闭合" : "开启");
                        }
                        for (int i = 0; i < lockStatus.Length; i++)
                        {
                            ls += string.Format(" {0}-{1}", i+1, lockStatus[i]);
                            rs += string.Format(" {0}-{1}", i + 1, controllerStatus.byCardReaderOnlineStatus[i] == 0 ? "不在线" : "在线");
                            cs += string.Format(" {0}-{1}", i + 1, controllerStatus.byCardReaderAntiDismantleStatus[i] == 0 ? "关闭" : "开启");
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            ks += string.Format(" {0}-{1}", i + 1, controllerStatus.byDoorLockStatus[i] == 0 ? "关" : "开");

                        }
                        TextMsg?.Invoke(string.Format("主机状态: 门锁状态--{0}，门状态--{1}，门磁状态--{2}，读卡器状态--{3}，读卡器防拆--{4}，读卡器验证方式--{5}，已添加卡数量--{6}",
                             ks,//operate.ConvertBase(operate.byteToHexStr(controllerStatus.byDoorLockStatus, 1), 16, 10) == "0" ? "关" : "开",
                             ds,
                             ms,// operate.ConvertBase(operate.byteToHexStr(controllerStatus.byMagneticStatus, 1), 16, 10) == "0" ? "闭合" : "开启",
                             rs,//operate.ConvertBase(operate.byteToHexStr(controllerStatus.byCardReaderOnlineStatus, 1), 16, 10) == "0" ? "不在线" : "在线",
                             cs,//operate.ConvertBase(operate.byteToHexStr(controllerStatus.byCardReaderAntiDismantleStatus, 1), 16, 10) == "0" ? "关闭" : "开启",
                             ls,
                             controllerStatus.dwCardNum.ToString()));
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", item.DeviceIp, "门禁主机工作状态获取成功", DateTime.Now.ToString()));
                        return true;
                    }
                    else
                    {
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  {1}  （{2}）", item.DeviceIp, "门禁主机工作状态获取失败", DateTime.Now.ToString()));
                        return false;
                    }
                  
                }
            }
            return false;

        }
        #endregion

        #region 计划设置
        /// <summary>
        /// 设置读卡器验证计划
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="channel">设备通道号</param>
        /// <param name="plan">周计划参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SetCardReadValidPlan(string doorIp, int channel, ref HikDoorStruct.NET_DVR_CARD_READER_PLAN plan)
        {

            bool b = false;
            if (string.IsNullOrWhiteSpace(doorIp))
                return b; 
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    b = operate.SetCardReadValidPlan(item.UserId, channel, ref plan);
                    if (b)
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{0}  验证计划--{0}  设置成功", item.DeviceIp, channel, plan.dwTemplateNo));
                    else
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{0}  验证计划--{0}  设置失败", item.DeviceIp, channel, plan.dwTemplateNo));
                }
            }
            return b;
        }
        /// <summary>
        /// 获取卡权限周计划
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="channel">编号</param>
        /// <param name="weekPlan">计划参数结构体对象</param>
        /// <returns>true：成功，false：失败</returns>
        public bool GetCardWeekPlanInfo(string doorIp, int channel, ref HikDoorStruct.NET_DVR_WEEK_PLAN_CFG weekPlan)
        {
            //foreach (var item in useInfos)
            //{
            //    if (doorIp == item.LoginDeviceIp)
            //    {
            //        if (operate.GetWeekPlanInfo(item.UserId, channel, HikDoorEnum.ConfigCommand.NET_DVR_GET_CARD_RIGHT_WEEK_PLAN, ref weekPlan))
            //        {
            //            TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限周计划使能--{2}", doorIp, channel, weekPlan.byEnable == 0 ? "否" : "是"));
            //            if (weekPlan.struPlanCfg != null)
            //            {
            //                if (weekPlan.struPlanCfg.Length > 0)
            //                {
            //                    for (int i = 0; i < weekPlan.struPlanCfg.Length; i++)
            //                    {
            //                        TextMsg?.Invoke(string.Format("第{0}天：是否使能--{1} 门禁状态--{2} 读卡器验证方式--{3} 起至时间--{4}（{5}）", i, weekPlan.struPlanCfg[i].byEnable == 0 ? "否" : "是", Enum.GetName(typeof(HikDoorEnum.DoorStatus), weekPlan.struPlanCfg[i].byDoorStatus), Enum.GetName(typeof(HikDoorEnum.CardReadVerifyMode), weekPlan.struPlanCfg[i].byVerifyMode), string.Format("{0}:{1}:{2}-{3}:{4}:{5}", weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byHour, weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byMinute, weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.bySecond, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.byHour, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.byMinute, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.bySecond), DateTime.Now));
            //                    }
            //                }
            //                TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限周计划获取--{2}", doorIp, channel, "成功"));
            //                return true;
            //            }

            //        }
            //        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限周计划获取--{2}", doorIp, channel, "失败"));
            //        return false;
            //    }
            //}
            return false;
        }
        /// <summary>
        /// 设置卡权限周计划
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="channel">编号</param>
        /// <param name="plan">周计划参数</param>
        /// <param name="enable">是否使能周计划</param>
        /// <returns>true：成功，false：失败</returns>
        public bool SetCardWeekPlanInfo(string doorIp, int channel, HikDoorStruct.NET_DVR_SINGLE_PLAN_SEGMENT[] plan, uint enable = 1)
        {
            if (useInfos != null && doorIp != null)
            {
                HikDoorStruct.NET_DVR_WEEK_PLAN_CFG weekPlan = new HikDoorStruct.NET_DVR_WEEK_PLAN_CFG() { byEnable = byte.Parse(enable.ToString()) };
                weekPlan.struPlanCfg = plan;
                weekPlan.dwSize = (uint)Marshal.SizeOf(weekPlan);
                DoorUseInfo info = useInfos.Find(o => o.DeviceIp == doorIp);
                if (info.DeviceIp == null)
                    return false;
                if (operate.SetWeekPlanInfo(info.UserId, channel, HikDoorEnum.ConfigCommand.NET_DVR_SET_CARD_RIGHT_WEEK_PLAN, ref weekPlan))
                    {
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限周计划使能--{2}", doorIp, channel, weekPlan.byEnable == 0 ? "否" : "是"));
                        if (weekPlan.struPlanCfg.Length > 0)
                        {
                            for (int i = 0; i < weekPlan.struPlanCfg.Length; i++)
                            {
                                TextMsg?.Invoke(string.Format("第{0}天：是否使能--{1} 门禁状态--{2} 读卡器验证方式--{3} 起至时间--{4}（{5}）", i, weekPlan.struPlanCfg[i].byEnable == 0 ? "否" : "是", Enum.GetName(typeof(HikDoorEnum.DoorStatus), weekPlan.struPlanCfg[i].byDoorStatus), Enum.GetName(typeof(HikDoorEnum.CardReadVerifyMode), weekPlan.struPlanCfg[i].byVerifyMode), string.Format("{0}:{1}:{2}-{3}:{4}:{5}", weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byHour, weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byMinute, weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.bySecond, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.byHour, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.byMinute, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.bySecond), DateTime.Now));
                            }
                        }
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限周计划设置--{2}", doorIp, channel, "成功"));
                        return true;
                    }
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限周计划设置--{2}", doorIp, channel, "失败"));
                    return false;
                }
            return false;
        }

        /// <summary>
        /// 获取读卡器周计划
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="cardReadNo">编号</param>
        /// <param name="weekPlan">计划参数结构体对象</param>
        /// <returns>true：成功，false：失败</returns>
        public bool GetCardReadWeekPlanInfo(string doorIp, int cardReadNo, ref HikDoorStruct.NET_DVR_WEEK_PLAN_CFG weekPlan)
        {
            if (useInfos != null && doorIp != null)
            {
                DoorUseInfo info = useInfos.Find(o => o.DeviceIp == doorIp);
                if (info.DeviceIp == null)
                    return false;
                if (operate.GetWeekPlanInfo(info.UserId, cardReadNo, HikDoorEnum.ConfigCommand.NET_DVR_GET_VERIFY_WEEK_PLAN, ref weekPlan))
                    {
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  读卡器周计划使能--{2}", doorIp, cardReadNo, weekPlan.byEnable == 0 ? "否" : "是"));
                        if (weekPlan.struPlanCfg != null)
                        {
                            if (weekPlan.struPlanCfg.Length > 0)
                            {
                                for (int i = 0; i < weekPlan.struPlanCfg.Length; i++)
                                {
                                    TextMsg?.Invoke(string.Format("第{0}天：是否使能--{1} 门禁状态--{2} 读卡器验证方式--{3} 起至时间--{4}（{5}）", i / 8+1, weekPlan.struPlanCfg[i].byEnable == 0 ? "否" : "是", Enum.GetName(typeof(HikDoorEnum.DoorStatus), weekPlan.struPlanCfg[i].byDoorStatus), Enum.GetName(typeof(HikDoorEnum.CardReadVerifyMode), weekPlan.struPlanCfg[i].byVerifyMode), string.Format("{0}:{1}:{2}-{3}:{4}:{5}", weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byHour, weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byMinute, weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.bySecond, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.byHour, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.byMinute, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.bySecond), DateTime.Now));
                                }
                            }
                            TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  读卡器周计划获取--{2}", doorIp, cardReadNo, "成功"));
                            return true;
                        }

                    }
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  读卡器周计划获取--{2}", doorIp, cardReadNo, "失败"));
                    return false;
                //}
            }
            return false;
        }
        /// <summary>
        /// 设置读卡器周计划
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="cardReadNo">读卡器编号</param>
        /// <param name="plan">周计划参数</param>
        /// <param name="enable">是否使能周计划</param>
        /// <returns>true：成功，false：失败</returns>
        public bool SetCardReadWeekPlanInfo(string doorIp, int cardReadNo, HikDoorStruct.NET_DVR_SINGLE_PLAN_SEGMENT[] plan, uint enable = 1)
        {

            if (useInfos != null && doorIp != null)
            {
                HikDoorStruct.NET_DVR_WEEK_PLAN_CFG weekPlan = new HikDoorStruct.NET_DVR_WEEK_PLAN_CFG() { byEnable = byte.Parse(enable.ToString()) };
                weekPlan.struPlanCfg = plan;
                weekPlan.dwSize = (uint)Marshal.SizeOf(weekPlan);
                DoorUseInfo info = useInfos.Find(o => o.DeviceIp == doorIp);
                if (info.DeviceIp == null)
                    return false;
                if (operate.SetWeekPlanInfo(info.UserId, cardReadNo, HikDoorEnum.ConfigCommand.NET_DVR_SET_VERIFY_WEEK_PLAN, ref weekPlan))
                {
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  读卡器周计划使能--{2}", doorIp, cardReadNo, weekPlan.byEnable == 0 ? "否" : "是"));
                    if (weekPlan.struPlanCfg.Length > 0)
                    {
                        for (int j = 0; j < weekPlan.struPlanCfg.Length; j++)
                        {

                                TextMsg?.Invoke(string.Format("第{0}天：是否使能--{1} 门禁状态--{2} 读卡器验证方式--{3} 起至时间--{4}（{5}）", j / 8 + 1, weekPlan.struPlanCfg[j].byEnable == 0 ? "否" : "是", Enum.GetName(typeof(HikDoorEnum.DoorStatus), weekPlan.struPlanCfg[j].byDoorStatus), Enum.GetName(typeof(HikDoorEnum.CardReadVerifyMode), weekPlan.struPlanCfg[j].byVerifyMode), string.Format("{0}:{1}:{2}-{3}:{4}:{5}", weekPlan.struPlanCfg[j].struTimeSegment.struBeginTime.byHour, weekPlan.struPlanCfg[j].struTimeSegment.struBeginTime.byMinute, weekPlan.struPlanCfg[j].struTimeSegment.struBeginTime.bySecond, weekPlan.struPlanCfg[j].struTimeSegment.struEndTime.byHour, weekPlan.struPlanCfg[j].struTimeSegment.struEndTime.byMinute, weekPlan.struPlanCfg[j].struTimeSegment.struEndTime.bySecond), DateTime.Now));
                        }
                    }
                    TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  读卡器周计划设置--{2}", doorIp, cardReadNo, "成功"));
                    return true;
                }
                int x = HikDeviceApi.HikOperate.GetLastError();
                TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  读卡器周计划设置--{2}", doorIp, cardReadNo, "失败"));
                return false;
            }
            return false;
        }

        /// <summary>
        /// 获取门状态周计划
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="channel">编号</param>
        /// <param name="weekPlan">计划参数结构体对象</param>
        /// <returns>true：成功，false：失败</returns>
        public bool GetDoorStatusWeekPlanInfo(string doorIp, int channel, ref HikDoorStruct.NET_DVR_WEEK_PLAN_CFG weekPlan)
        {
            //if (useInfos != null && doorIp != null && channel != 0)
            //{
            //    UseInfo info = useInfos.Find(o => o.LoginDeviceIp == doorIp);
            //    if (info.LoginDeviceIp == null)
            //        return false;
            //    if (operate.GetWeekPlanInfo(info.UserId, channel, HikDoorEnum.ConfigCommand.NET_DVR_GET_WEEK_PLAN_CFG, ref weekPlan))
            //    {
            //        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  门状态周计划使能--{2}", doorIp, channel, weekPlan.byEnable == 0 ? "否" : "是"));
            //        if (weekPlan.struPlanCfg != null)
            //        {
            //            if (weekPlan.struPlanCfg.Length > 0)
            //            {
            //                for (int i = 0; i < weekPlan.struPlanCfg.Length; i++)
            //                {
            //                    TextMsg?.Invoke(string.Format("第{0}天：是否使能--{1} 门禁状态--{2} 读卡器验证方式--{3} 起至时间--{4}（{5}）", i, weekPlan.struPlanCfg[i].byEnable == 0 ? "否" : "是", Enum.GetName(typeof(HikDoorEnum.DoorStatus), weekPlan.struPlanCfg[i].byDoorStatus), Enum.GetName(typeof(HikDoorEnum.CardReadVerifyMode), weekPlan.struPlanCfg[i].byVerifyMode), string.Format("{0}:{1}:{2}-{3}:{4}:{5}", weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byHour, weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byMinute, weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.bySecond, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.byHour, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.byMinute, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.bySecond), DateTime.Now));
            //                }
            //            }
            //            TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  门状态周计划获取--{2}", doorIp, channel, "成功"));
            //            return true;
            //        }

            //    }
            //    TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  门状态周计划获取--{2}", doorIp, channel, "失败"));
            //    return false;
            //}
            //else
                return false;
        }
        /// <summary>
        /// 设置门状态周计划
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="channel">编号</param>
        /// <param name="weekPlan">周计划参数结构体对象</param>
        /// <returns>true：成功，false：失败</returns>
        public bool SetDoorStatusWeekPlanInfo(string doorIp, int channel, HikDoorStruct.NET_DVR_WEEK_PLAN_CFG weekPlan)
        {
            //foreach (var item in useInfos)
            //{
            //    if (doorIp == item.LoginDeviceIp)
            //    {
            //        if (operate.SetWeekPlanInfo(item.UserId, channel, HikDoorEnum.ConfigCommand.NET_DVR_SET_WEEK_PLAN_CFG, ref weekPlan))
            //        {
            //            TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  门状态周计划使能--{2}", doorIp, channel, weekPlan.byEnable == 0 ? "否" : "是"));
            //            if (weekPlan.struPlanCfg.Length > 0)
            //            {
            //                for (int i = 0; i < weekPlan.struPlanCfg.Length; i++)
            //                {
            //                    TextMsg?.Invoke(string.Format("第{0}天：是否使能--{1} 门禁状态--{2} 读卡器验证方式--{3} 起至时间--{4}（{5}）", i, weekPlan.struPlanCfg[i].byEnable == 0 ? "否" : "是", Enum.GetName(typeof(HikDoorEnum.DoorStatus), weekPlan.struPlanCfg[i].byDoorStatus), Enum.GetName(typeof(HikDoorEnum.CardReadVerifyMode), weekPlan.struPlanCfg[i].byVerifyMode), string.Format("{0}:{1}:{2}-{3}:{4}:{5}", weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byHour, weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byMinute, weekPlan.struPlanCfg[i].struTimeSegment.struBeginTime.bySecond, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.byHour, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.byMinute, weekPlan.struPlanCfg[i].struTimeSegment.struEndTime.bySecond), DateTime.Now));
            //                }
            //            }
            //            TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  门状态周计划设置--{2}", doorIp, channel, "成功"));
            //            return true;
            //        }
            //        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  门状态周计划设置--{2}", doorIp, channel, "失败"));
            //        return false;
            //    }
            //}
            return false;
        }
        /// <summary>
        /// 获取卡权限假日计划
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="channel">编号</param>
        /// <param name="holidayPlan">假日计划参数结构体对象</param>
        /// <returns>true：成功，false：失败</returns>
        public bool GetCardHolidayPlanInfo(string doorIp, int channel, ref HikDoorStruct.NET_DVR_HOLIDAY_PLAN_CFG holidayPlan)
        {
            //foreach (var item in useInfos)
            //{
            //    if (doorIp == item.LoginDeviceIp)
            //    {
            //        if (operate.GetHolidayPlanInfo(item.UserId, channel, HikDoorEnum.ConfigCommand.NET_DVR_GET_CARD_RIGHT_HOLIDAY_PLAN, ref holidayPlan))
            //        {
            //            TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限假日计划使能--{2}", doorIp, channel, holidayPlan.byEnable == 0 ? "否" : "是"));
            //            if (holidayPlan.struPlanCfg != null)
            //            {
            //                if (holidayPlan.struPlanCfg.Length > 0)
            //                {
            //                    for (int i = 0; i < holidayPlan.struPlanCfg.Length; i++)
            //                    {
            //                        TextMsg?.Invoke(string.Format("第{0}天：是否使能--{1} 门禁状态--{2} 读卡器验证方式--{3} 起至时间--{4}（{5}）", i, holidayPlan.struPlanCfg[i].byEnable == 0 ? "否" : "是", Enum.GetName(typeof(HikDoorEnum.DoorStatus), holidayPlan.struPlanCfg[i].byDoorStatus), Enum.GetName(typeof(HikDoorEnum.CardReadVerifyMode), holidayPlan.struPlanCfg[i].byVerifyMode), string.Format("{0}:{1}:{2}-{3}:{4}:{5}", holidayPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byHour, holidayPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byMinute, holidayPlan.struPlanCfg[i].struTimeSegment.struBeginTime.bySecond, holidayPlan.struPlanCfg[i].struTimeSegment.struEndTime.byHour, holidayPlan.struPlanCfg[i].struTimeSegment.struEndTime.byMinute, holidayPlan.struPlanCfg[i].struTimeSegment.struEndTime.bySecond), DateTime.Now));
            //                    }
            //                }
            //                TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限假日计划获取--{2}", doorIp, channel, "成功"));
            //                return true;
            //            }
            //        }
            //        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限假日计划获取--{2}", doorIp, channel, "失败"));
            //        return false;
            //    }
            //}
            return false;
        }

        /// <summary>
        /// 设置卡权限假日计划
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="channel">编号</param>
        /// <param name="holidayPlan">假日计划参数结构体对象</param>
        /// <returns>true：成功，false：失败</returns>
        public bool SetCardHolidayPlanInfo(string doorIp, int channel, HikDoorStruct.NET_DVR_HOLIDAY_PLAN_CFG holidayPlan)
        {
            //foreach (var item in useInfos)
            //{
            //    if (doorIp == item.LoginDeviceIp)
            //    {
            //        if (operate.SetHolidayPlanInfo(item.UserId, channel, HikDoorEnum.ConfigCommand.NET_DVR_SET_CARD_RIGHT_HOLIDAY_PLAN, ref holidayPlan))
            //        {
            //            TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限假日计划使能--{2}  假日开始日期--{3}", doorIp, channel, holidayPlan.byEnable == 0 ? "否" : "是", string.Format("{0}/{1}/{2}", holidayPlan.struBeginDate.wYear, holidayPlan.struBeginDate.byMonth, holidayPlan.struBeginDate.byDay)));
            //            if (holidayPlan.struPlanCfg.Length > 0)
            //            {
            //                for (int i = 0; i < holidayPlan.struPlanCfg.Length; i++)
            //                {
            //                    TextMsg?.Invoke(string.Format("第{0}天：是否使能--{1} 门禁状态--{2} 读卡器验证方式--{3} 起至时间--{4}（{5}）", i, holidayPlan.struPlanCfg[i].byEnable == 0 ? "否" : "是", Enum.GetName(typeof(HikDoorEnum.DoorStatus), holidayPlan.struPlanCfg[i].byDoorStatus), Enum.GetName(typeof(HikDoorEnum.CardReadVerifyMode), holidayPlan.struPlanCfg[i].byVerifyMode), string.Format("{0}:{1}:{2}-{3}:{4}:{5}", holidayPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byHour, holidayPlan.struPlanCfg[i].struTimeSegment.struBeginTime.byMinute, holidayPlan.struPlanCfg[i].struTimeSegment.struBeginTime.bySecond, holidayPlan.struPlanCfg[i].struTimeSegment.struEndTime.byHour, holidayPlan.struPlanCfg[i].struTimeSegment.struEndTime.byMinute, holidayPlan.struPlanCfg[i].struTimeSegment.struEndTime.bySecond), DateTime.Now));
            //                }
            //            }
            //            TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限假日计划设置--{2}", doorIp, channel, "成功"));
            //            return true;
            //        }
            //        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{1}  卡权限假日计划设置--{2}", doorIp, channel, "失败"));
            //        return false;
            //    }
            //}
            return false;
        }


        #endregion

        #region 计划模板
        /// <summary>
        /// 获取卡权限计划模板
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="channel">编号</param>
        /// <param name="plan">计划模板参数结构体对象</param>
        /// <returns>true：成功，false：失败</returns>
        public bool GetCardPlanTemplateInfo(string doorIp, int channel, ref HikDoorStruct.NET_DVR_PLAN_TEMPLATE plan)
        {
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;

                    if (operate.GetPlanTemplateInfo(ui, channel, HikDoorEnum.ConfigCommand.NET_DVR_GET_CARD_RIGHT_PLAN_TEMPLATE, ref plan))
                    {
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  是否使能--{1}  模板名称--{2}  周计划编号--{3}  假日组长度--{4}", doorIp, channel, plan.byEnable == 0 ? "否" : "是", plan.byTemplateName, plan.dwWeekPlanNo, plan.dwHolidayGroupNo.Length));
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 设置卡权限计划模板
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="channel">编号</param>
        /// <param name="plan">计划模板参数结构体对象</param>
        /// <returns>true：成功，false：失败</returns>
        public bool SetCardPlanTemplateInfo(string doorIp, int channel, ref HikDoorStruct.NET_DVR_PLAN_TEMPLATE plan)
        {
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    DoorUseInfo ui = item;

                    if (operate.SetPlanTemplateInfo(ui, channel, HikDoorEnum.ConfigCommand.NET_DVR_SET_CARD_RIGHT_PLAN_TEMPLATE, ref plan))
                    {
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  是否使能--{1}  模板名称--{2}  周计划编号--{3}  假日组长度--{4}", doorIp, channel, plan.byEnable == 0 ? "否" : "是", plan.byTemplateName, plan.dwWeekPlanNo, plan.dwHolidayGroupNo.Length));
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 计划获取
        /// <summary>
        /// 获取读卡器验证计划
        /// </summary>
        /// <param name="doorIp">门禁主机Ip</param>
        /// <param name="chanel">设备通道号</param>
        /// <param name="plan">计划参数信息结构体（输出）</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetCardReadValidPlan(string doorIp, int chanel, ref HikDoorStruct.NET_DVR_CARD_READER_PLAN plan)
        {
            bool b = false;
            if (string.IsNullOrWhiteSpace(doorIp))
                return b;
            foreach (var item in useInfos)
            {
                if (doorIp == item.DeviceIp)
                {
                    b = operate.GetCardReadValidPlan(item.UserId, chanel, ref plan);
                    if (b)
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{0}  验证计划--{0}  获取成功", item.DeviceIp, chanel, plan.dwTemplateNo));
                    else
                        TextMsg?.Invoke(string.Format("门禁主机--{0}  读卡器--{0}  验证计划--{0}  获取失败", item.DeviceIp, chanel, "-1", plan.dwTemplateNo));
                }
            }
            return b;
        }
        #endregion
    }
}
