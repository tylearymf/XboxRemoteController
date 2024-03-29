using SmartGlass.Common;

namespace SmartGlass.Messaging.Session.Messages
{
    [SessionMessageType(SessionMessageType.Disconnect)]
    public class DisconnectMessage : SessionMessageBase
    {
        public DisconnectReason Reason { get; set; }
        public uint ErrorCode { get; set; }

        public override void Deserialize(EndianReader reader)
        {
            throw new System.NotImplementedException();
        }

        public override void Serialize(EndianWriter writer)
        {
            writer.WriteBE((uint)Reason);
            writer.WriteBE(ErrorCode);
        }
    }
}