using System;
using static HikDeviceApi.Decoder.HikDecoderStruct;

namespace HikDeviceApi.Decoder
{
    /// <summary>
    /// 日 期:2015-08-15
    /// 作 者:痞子少爷
    /// 描 述:海康多路解码器接口委托
    /// </summary>
    public static class HikDecoderDelegate
    {
        /// <summary>
        /// 语音转发音频数据回调函数
        /// </summary>
        /// <param name="lVoiceComHandle">NET_DVR_StartVoiceCom_V30的返回值</param>
        /// <param name="pRecvDataBuffer">存放音频数据的缓冲区指针</param>
        /// <param name="dwBufSize">音频数据大小</param>
        /// <param name="byAudioFlag">音频数据类型：0－本地采集的数据；1－设备发送过来的语音数据</param>
        /// <param name="pUser">用户数据指针</param>
        public delegate void VOICEDATACALLBACKV30(int lVoiceComHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, System.IntPtr pUser);

        /// <summary>
        /// 语音广播音频数据回调函数
        /// </summary>
        /// <param name="pRecvDataBuffer">存放PC本地采集的音频数据（PCM）的缓冲区指针</param>
        /// <param name="dwBufSize">音频数据大小</param>
        /// <param name="pUser">用户数据指针</param>
        public delegate void VOICEAUDIOSTART(string pRecvDataBuffer, uint dwBufSize, IntPtr pUser);
        /// <summary>
        /// 建立透明通道回调函数
        /// </summary>
        /// <param name="lSerialHandle">NET_DVR_SerialStart的返回值</param>
        /// <param name="pRecvDataBuffer">存放数据的缓冲区指针</param>
        /// <param name="dwBufSize">数据大小</param>
        /// <param name="dwUser">用户数据</param>
        public delegate void SERIALDATACALLBACK(int lSerialHandle, string pRecvDataBuffer, uint dwBufSize, uint dwUser);
        /// <summary>
        /// 获取回放状态委托
        /// </summary>
        /// <param name="status">回放状态信息结构体</param>
        /// <param name="decodeIp">解码器ip</param>
        /// <param name="decodeChannel">解码通道</param>
        public delegate void PlayBackStatus(NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS status, string decodeIp, int decodeChannel);
    }
}
