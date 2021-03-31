using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace TCPServerAsync
{
    public partial class ServerForm : Form
    {

        TcpListener listener;
        TcpClient client;
        int port = 12345;
        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {

        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
            }
            catch (Exception error) { MessageBox.Show(error.Message, Text); return; }

            btnStartServer.Enabled = false; //Det ska inte gå att starta mer än en gång.
            StartReception();
        }

        public async void StartReception()
        {
            try
            {
                client = await listener.AcceptTcpClientAsync();
            }
            catch (Exception error) { MessageBox.Show(error.Message, Text); return; }

            StartReading(client);
        }

        public async void StartReading (TcpClient k)
        {
            byte[] buffert = new byte[1024];

            int n = 0;
            try
            {
                n = await k.GetStream().ReadAsync(buffert, 0, buffert.Length);
            }
            catch (Exception error) { MessageBox.Show(error.Message, Text); return; }

            tbxInbox.AppendText(Encoding.Unicode.GetString(buffert, 0, n));

            StartReading(k);
        }

        private void tbxInbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
