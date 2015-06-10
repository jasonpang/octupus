using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempest;

namespace Octupus.Common.Network
{
    public class MouseWheelMessage : OctupusMessage
    {
        public int ScrollAmount { get; set; }

        public MouseWheelMessage()
            : base(OctupusMessageType.MouseWheelMessage) { }

        public override void WritePayload(ISerializationContext context, IValueWriter writer) 
        {
            writer.WriteInt32(ScrollAmount);
        }

        public override void ReadPayload(ISerializationContext context, IValueReader reader)
        {
            this.ScrollAmount = reader.ReadInt32();
        }
    }
}
