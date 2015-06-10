using Gma.System.MouseKeyHook;
using Octupus.Common;
using Octupus.Common.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = Tempest.Message;

namespace Octupus.Server
{
    public partial class FormMain : Form
    {
        private IKeyboardMouseEvents Input;
        private OctupusServer Server;

        public FormMain()
        {
            InitializeComponent();
            Input = Hook.GlobalEvents();
            Input.KeyDown += Input_KeyDown;
            Input.KeyUp += Input_KeyUp;
            Input.MouseDownExt += Input_MouseDownExt;
            Input.MouseUpExt += Input_MouseUpExt;
            Input.MouseMoveExt += Input_MouseMoveExt;
            Input.MouseWheel += Input_MouseWheel;
            Server = OctupusServer.StartNew();
        }

        void Input_KeyUp(object sender, KeyEventArgs e)
        {
            BroadcastMessageIfEnabled(new KeyUpMessage() { KeyCode = (int)e.KeyCode });
        }

        void Input_KeyDown(object sender, KeyEventArgs e)
        {
            BroadcastMessageIfEnabled(new KeyDownMessage() { KeyCode = (int)e.KeyCode });
        }

        void Input_MouseWheel(object sender, MouseEventArgs e)
        {
            BroadcastMessageIfEnabled(new MouseWheelMessage() { ScrollAmount = e.Delta });
        }

        void Input_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            BroadcastMessageIfEnabled(new MouseMoveMessage() { CursorLocation = e.Location });
        }

        void Input_MouseUpExt(object sender, MouseEventExtArgs e)
        {
            MouseButton mouseButton = default(MouseButton);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    mouseButton = MouseButton.Left;
                    break;
                case MouseButtons.Right:
                    mouseButton = MouseButton.Right;
                    break;
                case MouseButtons.XButton1:
                    mouseButton = MouseButton.XButton;
                    break;
                default:
                    return;
            }
            BroadcastMessageIfEnabled(new MouseUpMessage() { Button = mouseButton });
        }

        void Input_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            MouseButton mouseButton = default(MouseButton);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    mouseButton = MouseButton.Left;
                    break;
                case MouseButtons.Right:
                    mouseButton = MouseButton.Right;
                    break;
                case MouseButtons.XButton1:
                    mouseButton = MouseButton.XButton;
                    break;
                default:
                    return;
            }
            BroadcastMessageIfEnabled(new MouseDownMessage() { Button = mouseButton });
        }

        private void BroadcastMessageIfEnabled(Message message)
        {
            if (!Control.IsKeyLocked(Keys.NumLock))
                return;
            Server.BroadcastMessage(message);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Input.KeyDown -= Input_KeyDown;
            Input.KeyUp -= Input_KeyUp;
            Input.MouseDownExt -= Input_MouseDownExt;
            Input.MouseUpExt -= Input_MouseUpExt;
            Input.MouseMoveExt -= Input_MouseMoveExt;
            Input.MouseWheel -= Input_MouseWheel;
            Input.Dispose();
            Server.Stop();
        }
    }
}
