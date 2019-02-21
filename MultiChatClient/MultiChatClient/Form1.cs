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

namespace MultiChatClient
{
    public partial class Form1 : Form
    {
        bool isRunning;
        Socket client;
        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);
        private static String response = String.Empty;
        public delegate void UpdateUserCallBack(string user);
        public delegate void ClearUserCallBack();
        public delegate void UpdateTextCallBack(string text);
        private Dictionary<string, ChatIM> IMs;

        public Form1()
        {
            InitializeComponent();
            isRunning = false;
            //connectedUsers.Items.Add("Test Object");
            IMs = new Dictionary<string, ChatIM>();
        }



        private void connect_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                connect.Text = "Connect";
                Send(client, "disconnect:" + userText.Text + "<EOF>");
                sendDone.WaitOne();
            }
            else
            {

                try
                {
                    isRunning = true;
                    connect.Text = "Disconnect";
                    IPAddress ip = IPAddress.Parse(ipAddress.Text.Trim());
                    IPEndPoint remoteEP = new IPEndPoint(ip, int.Parse(port.Text.Trim()));
                    // Create a TCP/IP socket.  
                    client = new Socket(ip.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);

                    // Connect to the remote endpoint.  
                    client.BeginConnect(remoteEP,
                        new AsyncCallback(OnConnect), client);
                    connectDone.WaitOne();
                }
                catch(Exception ex)
                {
                    isRunning = false;
                    connect.Text = "Connect";
                    Console.WriteLine(ex.Message);
                }
                
            }
        }

        public void OnConnect(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();

                Send(client, "connect:" + userText.Text + "<EOF>");
                sendDone.WaitOne();

                Receive(client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void OnReceive(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.socket;
            int bytesRead;

            if (handler.Connected)
            {

                // Read data from the client socket. 
                try
                {
                    bytesRead = handler.EndReceive(ar);
                    if (bytesRead > 0)
                    {
                        // There  might be more data, so store the data received so far.
                        state.sb.Remove(0, state.sb.Length);
                        state.sb.Append(Encoding.ASCII.GetString(
                                         state.dataBuffer, 0, bytesRead));

                        // Display Text in Rich Text Box
                        content = state.sb.ToString();
                        Console.WriteLine(content);

                        handler.BeginReceive(state.dataBuffer, 0, state.bufferSize, 0,
                            new AsyncCallback(OnReceive), state);

                    }
                }

                catch (SocketException socketException)
                {
                    //WSAECONNRESET, the other side closed impolitely
                    if (socketException.ErrorCode == 10054 || ((socketException.ErrorCode != 10004) && (socketException.ErrorCode != 10053)))
                    {
                        Disconnect();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    Disconnect();

                }
            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            Send(client, userText.Text + ":" + sendText.Text.Trim() + "<EOF>");
            sendDone.WaitOne();
            sendText.Clear();
        }

        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.socket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.dataBuffer, 0, state.bufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Disconnect();
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket   
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.socket;

                int bytesRead = client.EndReceive(ar);
                state.sb.Append(Encoding.ASCII.GetString(state.dataBuffer, 0, bytesRead));
                response = state.sb.ToString();

                if (response.Contains("connect:") && response.IndexOf("connect:") == 0)
                {
                    string result = response.Substring(8);
                    int index = result.IndexOf(",");
                    if (index > -1)
                    {
                        bool done = false;
                        while (done == false)
                        {
                            string user = result.Substring(0, index);
                            if (user != userText.Text)
                            {
                                AddUsers(user);// connectedUsers.Items.Add(user);
                            }

                            result = result.Substring(index + 1);
                            index = result.IndexOf(",");
                            if (index == -1)
                            {
                                connectedUsers.Items.Add(result);
                                done = true;
                            }
                        }
                    }
                    else
                    {
                        if (userText.Text != result)
                        {
                            AddUsers(result);// connectedUsers.Items.Add(result);
                        }

                    }
                }
                else if (response.Contains("disconnect:") && response.IndexOf("disconnect:") == 0)
                {
                    string user = response.Substring(11);
                    RemoveUsers(user);// connectedUsers.Items.Remove(user);
                }
                else if (response.Contains("IM_") && response.IndexOf("IM_") == 0)
                {
                    string message = response.Substring(3);
                    string user = message.Substring(0, message.IndexOf(":"));
                    //string message = str.Substring(str.IndexOf(":") + 1);
                    if (!IMs.ContainsKey(user))
                    {
                        ChatIM im = new ChatIM(this, user);

                        IMs.Add(user, im);
                        im.addMessage(message);
                        im.ShowDialog();

                    }
                    else
                    {
                        IMs[user].addMessage(message);
                    }
                }
                else if (response == "Server_Disconnect")
                {
                    Disconnect();
                    return;
                }
                else if (response == "USER_EXISTS")
                {
                    MessageBox.Show("The user you entered in already exists");
                    Disconnect();
                    return;
                }
                else
                {
                    UpdateTextBox(response);
                }
                state.sb.Clear();
                client.BeginReceive(state.dataBuffer, 0, state.bufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Disconnect();
            }
        }

        private void openIM(ChatIM im)
        {
            im.Show();
        }

        private void AddUsers(string user)
        {
            if(connectedUsers.InvokeRequired)
            {
                connectedUsers.Invoke(new UpdateUserCallBack(AddUsers), new object[] { user });
            }
            else
            {
                connectedUsers.Items.Add(user);
            }
        }

        private void RemoveUsers(string user)
        {
            if(connectedUsers.InvokeRequired)
            {
                connectedUsers.Invoke(new UpdateUserCallBack(RemoveUsers), new object[] { user });
            }
            else
            {
                connectedUsers.Items.Remove(user);
            }
        }

        public void SendIM(string target,string message)
        {
            message = "IM_" + target + ":" + userText.Text + ":" + message + "<EOF>";
            Send(client, message);
        }


        private void Send(Socket client, String data)
        {
            try
            {
                // Convert the string data to byte data using ASCII encoding.  
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                // Begin sending the data to the remote device.  
                client.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), client);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
            
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Disconnect();
            }
        }
       
        public void Disconnect()
        {
            if(client != null && client.Connected)
            {
                client.Close();
            }
            
            foreach(KeyValuePair<string,ChatIM> im in IMs)
            {
                im.Value.Close();
            }
            IMs.Clear();
            clearUsers();
            receiveText.Clear();
            sendText.Clear();
            connect.Text = "Start";
            isRunning = false;
        }

        private void clearUsers()
        {
            if(connectedUsers.InvokeRequired)
            {
                connectedUsers.Invoke(new ClearUserCallBack(clearUsers), null);
            }
            else
            {
                connectedUsers.Items.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(client != null)
            {
                if (client.Connected)
                {
                    MessageBox.Show("Connected to server");
                }
                else
                {
                    MessageBox.Show("Not connected to server");
                }
            }
            else
            {
                MessageBox.Show("Not connected to server");
            }
        }

        private void UpdateTextBox(string text)
        {
            if(receiveText.InvokeRequired)
            {
                receiveText.Invoke(new UpdateTextCallBack(UpdateTextBox), new object[] { text });
            }
            else
            {
                receiveText.AppendText(text);
                receiveText.AppendText(Environment.NewLine);
            }
        }

        private void connectedUsers_DoubleClick(object sender, EventArgs e)
        {
            if(connectedUsers.SelectedItem != null)
            {
                string target = connectedUsers.SelectedItem.ToString();
                if (!IMs.ContainsKey(target))
                {
                    ChatIM im = new ChatIM(this, target);//,"this is a test");
                    //im.addMessage("hello");
                    IMs.Add(connectedUsers.SelectedItem.ToString(), im);
                    im.ShowDialog();
                }             
            }
        }

        public void RemoveIM(string target)
        {
            IMs.Remove(target);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }
    }
}
