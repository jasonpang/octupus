﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempest;

namespace Octupus.Common.Network
{
    public class MouseDownMessage : OctupusMessage
    {
        public MouseButton Button { get; set; }

        public MouseDownMessage()
            : base(OctupusMessageType.MouseDownMessage) { }

        public override void WritePayload(ISerializationContext context, IValueWriter writer)
        {
            writer.WriteInt32((int)Button);
        }

        public override void ReadPayload(ISerializationContext context, IValueReader reader)
        {
            this.Button = (MouseButton)reader.ReadInt32();
        }
    }
}
