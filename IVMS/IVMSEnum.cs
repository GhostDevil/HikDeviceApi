using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikDeviceApi.IVMS
{
    /// <summary>
    /// 日 期:2015-11-24
    /// 作 者:痞子少爷
    /// 描 述:海康硬盘录像机接口枚举
    /// </summary>
    public static class IVMSEnum
    {
        #region 云台控制命令
        /// <summary>
        /// 云台控制命令
        /// </summary>
        public enum PtzCmd
        {
            /// <summary>
            ///  左转
            /// </summary>
            LEFT,
            /// <summary>
            /// 右转
            /// </summary>
            RIGHT,
            /// <summary>
            /// 上转
            /// </summary>
            UP,
            /// <summary>
            /// 下转
            /// </summary>
            DOWN,
            /// <summary>
            /// 拉近
            /// </summary>
            ZOOMIN,
            /// <summary>
            /// 拉远
            /// </summary>
            ZOOMOUT,
            /// <summary>
            /// 左上
            /// </summary>
            LEFT_UP,
            /// <summary>
            /// 左下
            /// </summary>
            LEFT_DOWN,
            /// <summary>
            /// 右上
            /// </summary>
            RIGHT_UP,
            /// <summary>
            /// 右下
            /// </summary>
            RIGHT_DOWN,
            /// <summary>
            /// 停止左转 
            /// </summary>
            LEFT_STOP,
            /// <summary>
            /// 停止右转
            /// </summary>
            RIGHT_STOP,
            /// <summary>
            /// 停止上转
            /// </summary>
            UP_STOP,
            /// <summary>
            /// 停止下转
            /// </summary>
            DOWN_STOP,
            /// <summary>
            /// 停止拉近
            /// </summary>
            ZOOMIN_STOP,
            /// <summary>
            /// 停止拉远
            /// </summary>
            ZOOMOUT_STOP,
            /// <summary>
            /// 停止左上
            /// </summary>
            LEFT_UP_STOP,
            /// <summary>
            /// 停止左下
            /// </summary>
            LEFT_DOWN_STOP,
            /// <summary>
            /// 停止右上
            /// </summary>
            RIGHT_UP_STOP,
            /// <summary>
            /// 停止右下
            /// </summary>
            RIGHT_DOWN_STOP,
            /// <summary>
            ///  到预置点    预置点编号, 取值范围为0-63
            /// </summary>
            GOTO_PRESET,
            /// <summary>
            ///  设置预置点   预置点编号, 取值范围为0-63	预置点名称(字符串指针）
            /// </summary>
            SET_PRESET,
            /// <summary>
            ///  删除预置点   预置点编号, 取值范围为0-63
            /// </summary>
            DEL_PRESET,
            /// <summary>
            /// 清除预置点
            /// </summary>
            CLE_PRESET
        }
        #endregion


        #region 远程回放控制
        /// <summary>
        /// 远程回放控制
        /// </summary>
        public enum RemoteReplayMode
        {
            /// <summary>
            /// 停止播放
            /// </summary>
            PLAYMODE_STOP = 1,
            /// <summary>
            /// 暂停播放
            /// </summary>
            PLAYMODE_PAUSE = 2,
            /// <summary>
            /// 恢复播放
            /// </summary>
            PLAYMODE_RESUME = 3,
            /// <summary>
            /// 	1/8倍速前进播放
            /// </summary>
            PLAYMODE_Eighth_FORWARD = 14,
            /// <summary>
            /// 	1/4倍速前进播放
            /// </summary>
            PLAYMODE_QUARTER_FORWARD = 15,
            /// <summary>
            /// 	1/2倍速前进播放
            /// </summary>
            PLAYMODE_HALF_FORWARD = 16,
            /// <summary>
            /// 	正常速度前进播放
            /// </summary>
            PLAYMODE_1_FORWARD = 17,
            /// <summary>
            /// 2倍速前进播放
            /// </summary>
            PLAYMODE_2_FORWARD = 18,
            /// <summary>
            /// 4倍速前进播放
            /// </summary>
            PLAYMODE_4_FORWARD = 19,
            /// <summary>
            /// 8倍速前进播放
            /// </summary>
            PLAYMODE_8_FORWARD = 20
        }
        #endregion

        #region 远程回放控制
        /// <summary>
        /// 本地回放控制
        /// </summary>
        public enum LocalReplayMode
        {
            /// <summary>
            /// 停止播放
            /// </summary>
            PLAYMODE_STOP = 1,
            /// <summary>
            /// 暂停播放
            /// </summary>
            PLAYMODE_PAUSE = 2,
            ///恢复播放	
            PLAYMODE_RESUME = 3,
            /// <summary>
            /// 16倍速后退播放
            /// </summary>
            PLAYMODE_16_BACKWARD = 6,
            /// <summary>
            /// 8倍速后退播放
            /// </summary>
            PLAYMODE_8_BACKWARD = 7,
            /// <summary>
            /// 4倍速后退播放
            /// </summary>
            PLAYMODE_4_BACKWARD = 8,
            /// <summary>
            /// 2倍速后退播放
            /// </summary>
            PLAYMODE_2_BACKWARD = 9,
            /// <summary>
            /// 	1倍速后退播放
            /// </summary>
            PLAYMODE_1_BACKWARD = 10,
            /// <summary>
            /// 	1/2倍速后退播放
            /// </summary>
            PLAYMODE_HALF_BACKWARD = 11,
            /// <summary>
            /// 	1/4倍速后退播放
            /// </summary>
            PLAYMODE_QUARTER_BACKWARD = 12,
            /// <summary>
            /// 1/8倍速后退播放
            /// </summary>
            PLAYMODE_Eighth_BACKWARD = 13,
            /// <summary>
            /// 1/8倍速前进播放
            /// </summary>
            PLAYMODE_Eighth_FORWARD = 14,
            /// <summary>
            /// 	1/4倍速前进播放
            /// </summary>
            PLAYMODE_QUARTER_FORWARD = 15,
            /// <summary>
            /// 	1/2倍速前进播放
            /// </summary>
            PLAYMODE_HALF_FORWARD = 16,
            /// <summary>
            /// 	正常速度前进播放
            /// </summary>
            PLAYMODE_1_FORWARD = 17,
            /// <summary>
            /// 	2倍速前进播放
            /// </summary>
            PLAYMODE_2_FORWARD = 18,
            /// <summary>
            /// 4倍速前进播放
            /// </summary>
            PLAYMODE_4_FORWARD = 19,
            /// <summary>
            /// 	8倍速前进播放
            /// </summary>
            PLAYMODE_8_FORWARD = 20,
            /// <summary>
            /// 	16倍速前进播放
            /// </summary>
            PLAYMODE_16_FORWARD = 21
        }
        #endregion
    }
}
