﻿using System;
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

namespace Server
{
    public partial class frmServer : Form
    {
        public frmServer()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        Socket server;
        List<Socket> lstClient = new List<Socket>();
        IPEndPoint ipep;
        Thread XuLiClient;
        private void Form1_Load(object sender, EventArgs e)
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipep = new IPEndPoint(ip, 8080);
                    MessageBox.Show(ipep.ToString());
                    break;
                }
            }
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            server.Bind(ipep);
            XuLiClient = new Thread(new ThreadStart(ListenClient));
            XuLiClient.IsBackground = true;
            XuLiClient.Start();
        }
        private void ListenClient()
        {
            server.Listen(10);
            while (true)
            {
                Socket client = server.Accept();
                lstClient.Add(client);
                richTextBox1.AppendText(client.RemoteEndPoint.ToString() + " đã được kết nối\n");
                richTextBox1.ScrollToCaret();
                Thread ServiceClient = new Thread(PhucvuClient);
                ServiceClient.IsBackground = true;
                ServiceClient.Start(client);
            }
        }
        private void PhucvuClient(object obj)
        {
            Socket client = (Socket)obj;
            while (true)
            {
                byte[] buff = new byte[2048];
                client.Receive(buff);
                foreach (var sk in lstClient)
                {
                    sk.Send(buff, buff.Length, SocketFlags.None);
                }
            }
        }
    }
}