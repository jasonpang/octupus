using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempest;

namespace Octupus.Common.Network
{
    public sealed class KeyUpMessage : OctupusMessage
    {
        public int KeyCode { get; set; }

        public KeyUpMessage()
            : base(OctupusMessageType.KeyUpMessage) { }

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
