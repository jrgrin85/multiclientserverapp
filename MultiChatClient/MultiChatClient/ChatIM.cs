using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiChatClient
{
    public partial class ChatIM : Form
    {
        private string targetUser;
        private Form1 form;
        public delegate void UpdateTextBox(string msg);
        public ChatIM()
        {
            InitializeComponent();
        }

        public ChatIM(Form1 _form,string target)
        {
            InitializeComponent();
            targetUser = target;
            form = _form;
            this.Text = "IM: " + targetUser;
        }

        public ChatIM(Form1 _form,string target,string message)
        {
            InitializeComponent();
            targetUser = target;
            form = _form;
            this.Text = "IM: " + targetUser;
            messages.AppendText(message);
            messages.AppendText(Environment.NewLine);
        }

        private void send_Click(object sender, EventArgs e)
        {
            addMessage("You:" + sendText.Text);
            form.SendIM(targetUser, sendText.Text);
        }

        public void addMessage(string msg)
        {
            if(messages.InvokeRequired)
            {
                messages.Invoke(new UpdateTextBox(addMessage), new object[] { msg });
            }
            else
            {
                messages.AppendText(msg);
                messages.AppendText(Environment.NewLine);
            }   
        }

        private void ChatIM_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.RemoveIM(targetUser);
        }
    }
}
