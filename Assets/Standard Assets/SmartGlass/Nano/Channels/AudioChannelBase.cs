using System;
using SmartGlass.Common;
using SmartGlass.Nano.Packets;

namespace SmartGlass.Nano.Channels
{
    public abstract class AudioChannelBase : StreamingChannel, IStreamingChannel
    {
        public abstract void OnControl(AudioControl control);
        public abstract void OnData(AudioData data);

        internal AudioChannelBase(NanoRdpTransport transport, ChannelOpen openPacket)
            : base(transport, openPacket)
        {
            MessageReceived += OnMessage;
        }

        public void OnMessage(object sender, MessageReceivedEventArgs<INanoPacket> args)
        {
            IStreamerMessage packet = args.Message as IStreamerMessage;
            if (packet == null)
            {
                LogTool.Log($"Not handling packet {args.Message.Header.PayloadType}");
                return;
            }

            switch ((AudioPayloadType)packet.StreamerHeader.PacketType)
            {
                case AudioPayloadType.Control:
                    OnControl((AudioControl)packet);
                    break;
                case AudioPayloadType.Data:
                    OnData((AudioData)packet);
                    break;
            }
        }
    }
}