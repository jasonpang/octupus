using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tempest;
using WindowsInput;

namespace Octupus.Client
{
    public partial class FormMain : Form
    {
        private OctupusClient Client;

        public FormMain()
        {
            InitializeComponent();
            Client = new OctupusClient();
            Client.Connected += Client_Connected;
            Client.Disconnected += Client_Disconnected;
            Client.ConnectAsync(new Target(Target.AnyIP, 58291));
        }

        void Client_Connected(object sender, ClientConnectionEventArgs e)
        {
            Debug.WriteLine(String.Format("Octupus: Connected to {0}:{1}", e.Connection.RemoteTarget.Hostname, e.Connection.RemoteTarget.Port));
        }

        void Client_Disconnected(object sender, Tempest.ClientDisconnectedEventArgs e)
        {
            Debug.WriteLine(String.Format("Octupus: Disconnected from {Requested={0}, Reason={1}, CustomReason={2}", e.Requested, e.Reason.ToString(), e.CustomReason));
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
