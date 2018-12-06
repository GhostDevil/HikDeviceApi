using System;
using static HikDeviceApi.IVMS.IVMSStruct;

namespace HikDeviceApi.IVMS
{
    /// <summary>
    /// 日 期:2015-11-24
    /// 作 者:痞子少爷
    /// 描 述:海康硬盘录像机接口回调函数
    /// </summary>
    public static class IVMSDelegate
    {
        /// <summary>
        /// 原始流回调函数
        /// </summary>
        /// <param name="RealHandle">始实时流播放、按文件/时间远程回放播放函数、本地文件播放函数或下载函数返回的实例号</param>
        /// <param name="StreamType">码流类型,0系统头，1数据流</param>
        /// <param name="Data">数据指针,原始数据,厂商回调的数据不要修改</param>
        /// <param name="DataLen">数据长度</param>
        /// <param name="DecoderTag">解码标签</param>
        /// <param name="UserData">用户信息</param>
        public delegate void StreamCallbackPF(long RealHandle, int StreamType, IntPtr Data, int DataLen, string DecoderTag, IntPtr UserData);
        /// <summary>
        /// YUV数据回调函数
        /// </summary>
        /// <param name="RealHandle">始实时流播放、按文件/时间远程回放播放函数、本地文件播放函数或下载函数返回的实例号</param>
        /// <param name="pPictureData">YUV数据结构体</param>
        /// <param name="pUserData">用户数据</param>

        public delegate void VideoData_Stream_PF (long RealHandle, PICTURE_DATA_S pPictureData, IntPtr pUserData);
        /// <summary>
        /// 消息回调函数
        /// </summary>
        /// <param name="MsgType">消息类型(1解码异常，2录像异常,3下载进度，4播放进度，5用户离线 )</param>
        /// <param name="Data">消息数据</param>
        /// <param name="DataLen">数据长度</param>
        /// <param name="UserData">用户信息</param>
        public delegate void  fMsgCallback ( int MsgType, byte[] Data, long DataLen , IntPtr UserData);
        /// <summary>
        /// 状态推送回调函数
        /// </summary>
        /// <param name="CameraId">摄像机ID（一般应为DB33、国标编号）</param>
        /// <param name="Status">摄像机状态, 0表示在线，1表示离线</param>
        /// <param name="UserData">用户信息</param>
        /// <remarks>
        /// 大华设备没有共享和取消共享的概念，所以无法返回共享和取消共享的状态信息。
        /// </remarks>
        public delegate void fStatusCallback (string CameraId, int Status, IntPtr UserData);
        /// <summary>
        /// 画图叠加回调函数
        /// </summary>
        /// <param name="LoginHandle">登录实例号，Std_Login()的返回值</param>
        /// <param name="PlayHandle">播放实例号,可以是实时播放、远程录像回放、本地文件播放的实例号</param>
        /// <param name="hDC">绘图设备句柄</param>
        /// <param name="UserData">用户信息</param>
        public delegate void fDrawCallBack( long LoginHandle, long  PlayHandle, long hDC, IntPtr UserData);
        /// <summary>
        /// 文件检索回调函数
        /// </summary>
        /// <param name="FindHandle">录像查找实例号，Std_RecordFind的返回值</param>
        /// <param name="CameraId">摄像机ID（一般应为DB33编号、国标编号）</param>
        /// <param name="iContinue">无效参数，可设置为0，一次返回所有子项</param>
        /// <param name="iFinish">是否已检索完成,0 没有完成， 1完成</param>
        /// <param name="FileListXML">文件列表描述，一次返回所有文件子项</param>
        /// <param name="UserData">用户参数</param>

        public delegate void RecordFindCallBackPF(long FindHandle,string CameraId, int[] iContinue,int iFinish,string FileListXML,IntPtr UserData );
        /// <summary>
        /// 资源列表回调函数
        /// </summary>
        /// <param name="getResHandle">获取资源列表函数返回的实例号</param>
        /// <param name="iContinue">无效参数，可设置为0，一次返回所有子项</param>
        /// <param name="iFinish">是否已获取完成 ；1 表示完成，0表示未完成</param>
        /// <param name="FileListXML">文件列表描述，一次返回所有文件子项</param>
        /// <param name="UserData">用户参数</param>
        public delegate void OrgInfoCallBackPF(long getResHandle, int[] iContinue, int iFinish,string FileListXML,IntPtr UserData );
        /// <summary>
        /// 订阅报警回调函数
        /// </summary>
        /// <param name="csResourceid">监控点DB33\国标编号</param>
        /// <param name="iAlarmLevel">报警级别</param>
        /// <param name="csAlarmDetail">详细报警报文XML</param>
        /// <param name="pUser">用户参数</param>

        public delegate void fAlarmCallback(string csResourceid, int iAlarmLevel, string csAlarmDetail, IntPtr pUser);
    }
}
