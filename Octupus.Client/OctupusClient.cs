using Octupus.Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tempest;
using Tempest.Providers.Network;
using WindowsInput;
using WindowsInput.Native;

namespace Octupus.Client
{
    public class OctupusClient : TempestClient
    {
        private InputSimulator Input;

        public OctupusClient()
            : base(new NetworkClientConnection(OctupusProtocol.Instance), MessageTypes.Reliable)
        {
            Input = new InputSimulator();
            this.RegisterMessageHandler<KeyUpMessage>(OnKeyUpMessage);
            this.RegisterMessageHandler<KeyDownMessage>(OnKeyDownMessage);
            this.RegisterMessageHandler<MouseWheelMessage>(OnMouseWheelMessage);
            this.RegisterMessageHandler<MouseMoveMessage>(OnMouseMoveMessage);
            this.RegisterMessageHandler<MouseUpMessage>(OnMouseUpMessage);
            this.RegisterMessageHandler<MouseDownMessage>(OnMouseDownMessage);
        }

        private void OnKeyUpMessage(MessageEventArgs<KeyUpMessage> e)
        {
            Input.Keyboard.KeyUp((VirtualKeyCode)e.Message.KeyCode);
        }

        private void OnKeyDownMessage(MessageEventArgs<KeyDownMessage> e)
        {
            Input.Keyboard.KeyDown((VirtualKeyCode)e.Message.KeyCode);
        }

        private void OnMouseWheelMessage(MessageEventArgs<MouseWheelMessage> e)
        {
            Input.Mouse.VerticalScroll(e.Message.ScrollAmount);
        }

        private void OnMouseMoveMessage(MessageEventArgs<MouseMoveMessage> e)
        {
            Input.Mouse.MoveMouseTo(e.Message.CursorLocation.X, e.Message.CursorLocation.Y);
        }

        private void OnMouseUpMessage(MessageEventArgs<MouseUpMessage> e)
        {
            switch (e.Message.Button)
            {
                case Common.MouseButton.Left:
                    Input.Mouse.LeftButtonUp();
                    break;
                case Common.MouseButton.Right:
                    Input.Mouse.RightButtonUp();
                    break;
                case Common.MouseButton.XButton:
                    Input.Mouse.XButtonUp(1);
                    break;
            }
        }

        private void OnMouseDownMessage(MessageEventArgs<MouseDownMessage> e)
        {
            switch (e.Message.Button)
            {
                case Common.MouseButton.Left:
                    Input.Mouse.LeftButtonDown();
                    break;
                case Common.MouseButton.Right:
                    Input.Mouse.RightButtonDown();
                    break;
                case Common.MouseButton.XButton:
                    Input.Mouse.XButtonDown(1);
                    break;
            }
        }
    }
}
