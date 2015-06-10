using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            Client.Disconnected += Client_Disconnected;
            Client.ConnectAsync(new Target(Target.AnyIP, 58291));
        }

        void Client_Disconnected(object sender, Tempest.ClientDisconnectedEventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
