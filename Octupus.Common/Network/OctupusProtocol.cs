using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempest;

namespace Octupus.Common.Network
{
    public enum OctupusMessageType : ushort
    {
        KeyUpMessage = 1,
        KeyDownMessage = 2,
        MouseDownMessage = 3,
        MouseUpMessage = 4,
        MouseMoveMessage = 5,
        MouseWheelMessage = 6
    }

    public abstract class OctupusMessage : Message
    {
        protected OctupusMessage(OctupusMessageType type)
            : base(OctupusProtocol.Instance, (ushort)type)
        {
        }
    }

    public static class OctupusProtocol
    {
        public static Protocol Instance = new Protocol(2);

        static OctupusProtocol()
        {
            // We need to tell our protocol about all the message
            // types belonging to it. Discover() does this automatically.
            Instance.Discover();
        }
    }
}
