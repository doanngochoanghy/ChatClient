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
    public partial class frmClient : Form
    {
        public frmClient()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        Socket client;
        IPEndPoint ipep;
        Thread listen;
        string name;
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "")
                {
                    btnConnect.Enabled = false;
                    name = txtName.Text;
                    ipep = new IPEndPoint(IPAddress.Parse(txtIP.Text), int.Parse(txtPort.Text));
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    client.Connect(ipep);
                    listen = new Thread(new ThreadStart(ListenServer));
                    listen.IsBackground = true;
                    listen.Start();
                    MessageBox.Show("Kết nối thành công.");
                    AcceptButton = btnSend;
                    txtMessage.Focus();
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập tên.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kết nối tới server lỗi!");
                btnConnect.Enabled = true;
            }
        }
        private void ListenServer()
        {
            while (true)
            {
                try
                {
                    byte[] buff = new byte[2048];
                    client.Receive(buff);
                    string message = Encoding.UTF8.GetString(buff);
                    txtConversation.AppendText(message);
                    txtConversation.ScrollToCaret();
                }
                catch (Exception)
                {
                    client.Disconnect(true);
                }
            }
        }
        private void SendMessage(string message)
        {
            if (btnConnect.Enabled==false)
            {
                if (client.Connected)
                {
                    byte[] buff = Encoding.UTF8.GetBytes(message);
                    client.Send(buff);
                }   
            }
            else
            {
                MessageBox.Show("Chưa kết nối tới server.");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage(txtName.Text + ": " + txtMessage.Text + "\n");
            txtMessage.Text = "";
            txtMessage.Focus();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = true;
            client.Disconnect(false);
        }
    }
}
