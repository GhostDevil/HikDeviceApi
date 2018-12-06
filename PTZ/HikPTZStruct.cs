using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HikDeviceApi.PTZ
{
    /// <summary>
    /// 日 期:2016-12-05
    /// 作 者:痞子少爷
    /// 描 述:云台控制接口结构
    /// </summary>
    public static class HikPTZStruct
    {
        /// <summary>
        /// 云台图象区域位置信息
        /// <para>
        /// 该结构体中的坐标值与当前预览显示框的大小有关，现假设预览显示框为352*288，我们规定原点为预览显示框左上角的顶点，前四个参数计算方法如下：
        /// xTop = 鼠标当前所选区域的起始点坐标的值*255/352；
        /// xBottom = 鼠标当前所选区域的结束点坐标的值*255/352；
        /// yTop = 鼠标当前所选区域的起始点坐标的值*255/288；
        /// yBottom = 鼠标当前所选区域的结束点坐标的值*255/288；
        /// 缩小条件：xTop减去xBottom的值大于2。放大条件：xTop小于xBottom。
        /// </para>
        /// </summary>
        public struct NET_DVR_POINT_FRAME
        {
            /// <summary>
            /// 方框起始点的x坐标
            /// </summary>
            public int xTop;
            /// <summary>
            /// 方框起始点的y坐标
            /// </summary>
            public int yTop;
            /// <summary>
            /// 方框结束点的x坐标
            /// </summary>
            public int xBottom;
            /// <summary>
            /// 方框结束点的y坐标
            /// </summary>
            public int yBottom;
            /// <summary>
            /// 保留
            /// </summary>
            public int bCounter;
        }
        /// <summary>
        /// 巡航路径结构体
        /// </summary>
        public struct NET_DVR_CRUISE_RET
        {
            /// <summary>
            /// 轨迹点信息 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public NET_DVR_CRUISE_POINT[] struCruisePoint;
        }
        /// <summary>
        /// 巡航轨迹点参数结构体
        /// </summary>
        public struct NET_DVR_CRUISE_POINT
        {
            /// <summary>
            /// 预置点号
            /// </summary>
            public byte PresetNum;
            /// <summary>
            /// 停留时间
            /// </summary>
            public byte Dwell;
            /// <summary>
            /// 速度
            /// </summary>
            public byte Speed;
            /// <summary>
            /// 保留，置为0
            /// </summary>
            public byte Reserve;
    }
    
}
}
