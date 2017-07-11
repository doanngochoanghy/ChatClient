using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    [Serializable]
    public class Message
    {
        string messageText;
        ListViewItem messageItem;

        public string MessageText
        {
            get {
                return messageText;
            }

            set {
                messageText = value;
            }
        }

        public ListViewItem MessageItem
        {
            get {
                return messageItem;
            }

            set {
                messageItem = value;
            }
        }
        public Message()
        {
            messageText = null;
            messageItem = null;
        }
        public Message(string text, ListViewItem item)
        {
            messageText = text;
            messageItem = item;
        }
    }
}
