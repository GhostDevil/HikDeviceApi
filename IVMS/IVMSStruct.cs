using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HikDeviceApi.IVMS
{
    public static class IVMSStruct
    {

        #region 进度结构体
        /// <summary>
        /// 下载进度结构体
        /// </summary>
        public struct tagMsgDownloadProgress
        {
            /// <summary>
            /// 文件下载实例号
            /// </summary>
            public long DownloadHandle;
            /// <summary>
            /// 取值范围0--100表示表示进度值
            /// </summary>
            public long Progress;
            /// <summary>
            /// 0下载中 1正常结束，2异常结束
            /// </summary>
            public long Status;
        }

        /// <summary>
        /// 回放进度结构体
        /// </summary>
        public struct tagMsgPlayProgress
        {
            /// <summary>
            /// 远程回放实例号
            /// </summary>
            public long PlayHandle;
            /// <summary>
            /// (无效参数，可通过Std_GetFileReplayPos获取)已播放的相对时间，单位为秒,用户可结合文件总秒数计算百分比
            /// </summary>
            public long PlayTimed;
            /// <summary>
            /// 0回放中 1正常结束，2异常结束
            /// </summary>
            public long Status;
        }
        #endregion

        /// <summary>
        /// YUV数据结构体
        /// </summary>
        public struct PICTURE_DATA_S
        {
            /// <summary>
            /// pucData[0]:Y 平面指针， pucData[1]:U 平面指针， pucData[2]:V 平面指针
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.Struct)]
            public byte[] pucData;
            /// <summary>
            ///  ulLineSize[0]:Y 平面指针， ulLineSize[1]: U 平面指针, ulLineSize[2]: V 平面指针
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.Struct)]
            public byte[] ulLineSize;
            /// <summary>
            /// 图片高度
            /// </summary>
            public long ulPicHeight;
            /// <summary>
            /// 图片宽度 
            /// </summary>

            public long ulPicWidth;
            /// <summary>
            /// 用于渲染的时间数据（单位为毫秒）
            /// </summary>

            public long ulRenderTime;
            /// <summary>
            /// 保留参数
            /// </summary>
            public long ulReserverParam1;
            /// <summary>
            /// 保留参数
            /// </summary>
            public long ulReserverParam2;
        }
        /// <summary>
        /// 解码异常消息结构体
        /// </summary>
        public struct MSG_DECODE_EXCEPTION
        {
            /// <summary>
            /// 播放实例号
            /// </summary>
            public long PlayHandle;
            /// <summary>
            /// 错误号
            /// </summary>
            public long ErrorNo;
            /// <summary>
            /// 错误描述
            /// </summary>
            public string ErrorDesc;
        }


        /// <summary>
        /// 录像异常消息结构体
        /// </summary>
        public struct MSG_RECORD_EXCEPTION
        {
            /// <summary>
            /// 播放实例号
            /// </summary>
            public long RecordHandle;
            /// <summary>
            /// 错误号
            /// </summary>
            public long ErrorNo;
            /// <summary>
            /// 错误描述
            /// </summary>
            public string ErrorDesc;
        }


        /// <summary>
        /// 录像下载进度消息结构体
        /// </summary>
        public struct MSG_DOWNLOAD_Progress
        {
            /// <summary>
            /// 文件下载实例号
            /// </summary>
            public long DownloadHandle;
            /// <summary>
            /// 取值范围0--100表示表示进度值
            /// </summary>
            public long Progress;
            /// <summary>
            /// 0下载中 1正常结束，2异常结束
            /// </summary>
            public long Status;
        }


        /// <summary>
        /// 录像播放进度消息结构体
        /// </summary>
        public struct MSG_REPLAY_Progress
        {
            /// <summary>
            /// 文件播放实例号
            /// </summary>
            public long PlayHandle;
            /// <summary>
            /// 已播放的相对时间，单位为秒,用户可结合文件总秒数计算百分比
            /// </summary>
            public long PlayTimed;
            /// <summary>
            /// 0下载中 1正常结束，2异常结束
            /// </summary>
            public long Status;
        }

        /// <summary>
        /// 用户离线消息结构体
        /// </summary>
        public struct MSG_USER_OFFLINE
        {
            /// <summary>
            /// 登录实例号
            /// </summary>
            public long LoginHandle;
            /// <summary>
            /// 用户名
            /// </summary>
            public string UserName;
        }
    }
}
