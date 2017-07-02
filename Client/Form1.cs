using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        Socket client;
        IPEndPoint ipep;
        Thread Listen;
        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            ipep = new IPEndPoint(IPAddress.Parse(txtIP.Text),8080);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client.Connect(ipep);
            Listen = new Thread(new ThreadStart(ListenServer));
            Listen.IsBackground = true;
            Listen.Start();
        }
        private void ListenServer()
        {
            while (true)
            {
                byte[] buff = new byte[2048];
                client.Receive(buff);
                string message = Encoding.UTF8.GetString(buff);
                txtConversation.AppendText(message);
                txtConversation.ScrollToCaret();
            }
        }
        private void SendMessage(string message)
        {
            byte[] buff = Encoding.UTF8.GetBytes(message);
            client.Send(buff);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage(txtMessage.Text);
        }
    }
}
