using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempest;

namespace Octupus.Common.Network
{
    public sealed class KeyDownMessage : OctupusMessage
    {
        public int KeyCode { get; set; }

        public KeyDownMessage()
            : base(OctupusMessageType.KeyDownMessage) { }

        public override void WritePayload(ISerializationContext context, IValueWriter writer)
        {
            writer.WriteInt32(KeyCode);
        }

        public override void ReadPayload(ISerializationContext context, IValueReader reader)
        {
            this.KeyCode = reader.ReadInt32();
        }
    }
}
