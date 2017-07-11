using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
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
                ListViewItem itemClient = new ListViewItem(name);
                itemClient.SubItems.Add(client.RemoteEndPoint.ToString());
                itemClient.Tag = client;
                lvwClient.Items.Add(itemClient);
                UpdateListClient();
                richTextBox1.AppendText(client.RemoteEndPoint.ToString() + " đã được kết nối\n");
                richTextBox1.ScrollToCaret();
                Thread ServiceClient = new Thread(PhucvuClient);
                ServiceClient.Name = client.RemoteEndPoint.ToString();
                ServiceClient.IsBackground = true;
                ServiceClient.Start(itemClient);
                }
            }
        private void PhucvuClient(object obj)
            {
            ListViewItem itemclient = (ListViewItem)obj;
            Socket client = (Socket)itemclient.Tag;
            while (true)
                {
                byte[] buff = new byte[2048];
                if (client.Connected)
                    {
                    try
                        {
                        client.Receive(buff);
                        foreach (ListViewItem i in lvwClient.Items)
                            {
                            Socket sk = (Socket)i.Tag;
                            if (sk.Connected == true)
                                {
                                try
                                    {
                                    sk.Send(buff, buff.Length, SocketFlags.None);
                                    }
                                catch (Exception)
                                    {
                                    sk.Close();
                                    lvwClient.Items.Remove(i);
                                    UpdateListClient();
                                    break;
                                    }
                                }
                            }
                        }
                    catch (Exception)
                        {
                        client.Close();
                        lvwClient.Items.Remove(itemclient);
                        UpdateListClient();
                        break;
                        }
                    }
                }
            }

        private void UpdateListClient()
            {
            byte[] buff = SerializeListClient(lvwClient);
            string message = Encoding.UTF8.GetString(buff);
            foreach (ListViewItem item in lvwClient.Items)
                {
                ((Socket)(item.Tag)).Send(buff);
                }
            }

        private byte[] SerializeListClient(ListView items)
            {
            if (items == null)
                return null;
            ListViewItem[] lstItem = new ListViewItem[items.Items.Count];
            items.Items.CopyTo(lstItem, 0);
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, lstItem);
            return ms.ToArray();
            }

        private void frmServer_FormClosed(object sender, FormClosedEventArgs e)
            {
            Application.Exit();
            }
        }
    }
