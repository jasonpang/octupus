using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempest;

namespace Octupus.Common.Network
{
    public class MouseMoveMessage : OctupusMessage
    {
        public Point CursorLocation { get; set; }

        public MouseMoveMessage()
            : base(OctupusMessageType.MouseMoveMessage) { }

        public override void WritePayload(ISerializationContext context, IValueWriter writer)
        {
            writer.WriteInt32(this.CursorLocation.X);
            writer.WriteInt32(this.CursorLocation.Y);
        }

        public override void ReadPayload(ISerializationContext context, IValueReader reader)
        {
            this.CursorLocation = new Point(reader.ReadInt32(), reader.ReadInt32());
        }
    }
}
