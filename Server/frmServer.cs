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
                    txtIPAdd.Text = ip.ToString();
                    txtPort.Text = "8080";
                    break;
                }
            }
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            server.Bind(ipep);
            XuLiClient = new Thread(new ThreadStart(ListenClient));
            XuLiClient.IsBackground = true;
            XuLiClient.Start();
            richTextBox1.Text = "Server đã sẵn sàng!\n";
        }
        private void ListenClient()
        {
            server.Listen(10);
            while (true)
            {
                Socket client = server.Accept();
                byte[] buffer = new byte[2048];
                client.Receive(buffer);
                string name = Encoding.UTF8.GetString(buffer);
                ListViewItem item = new ListViewItem(name);
                item.SubItems.Add(client.RemoteEndPoint.ToString());
                listView1.Items.Add(item);
                richTextBox1.AppendText(item.SubItems[1].Text +"\n");
                lstClient.Add(client);
                richTextBox1.AppendText(name + client.RemoteEndPoint.ToString() + " đã được kết nối\n");
                richTextBox1.ScrollToCaret();
                foreach (var i in lstClient)
                {
                    richTextBox1.AppendText(i.RemoteEndPoint.ToString() + "\n");
                }
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
                if (client.Connected == true)
                {
                    try
                    {
                        client.Receive(buff);
                    }
                    catch (Exception)
                    {
                        client.Close();
                        lstClient.Remove(client);
                    }
                    foreach (var sk in lstClient)
                    {
                        if (sk.Connected == true)
                        {
                            try
                            {
                                sk.Send(buff, buff.Length, SocketFlags.None);
                            }
                            catch (Exception)
                            {
                                sk.Close();
                                lstClient.Remove(sk);
                            }
                        }
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmServer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
