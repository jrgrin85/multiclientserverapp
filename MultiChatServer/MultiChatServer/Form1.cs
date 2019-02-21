using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiChatServer
{
    public partial class Form1 : Form
    {
        bool isRunning;
        static readonly object _lock = new object();
        public Dictionary<string, Socket> list_clients = new Dictionary<string, Socket>();
        public Dictionary<string, string> userNames = new Dictionary<string, string>();
        public delegate void UpdateUserCallBack(string user);
        public delegate void UpdateMessageCallBack(string msg);
        public delegate void UpdateTextBoxCallBack(bool enable);
        Thread thread;
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        Socket listener;

        public Form1()
        {
            InitializeComponent();
            isRunning = false;
            list_clients = new Dictionary<string, Socket>();
            userNames = new Dictionary<string, string>();
            //count = 0;
            // Add Event to handle when a client is connected
            //Changed += new ChangedEventHandler(ClientAdded);
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                StopServer();
                startBtn.Text = "Start";
                ipAddress.Enabled = true;
                port.Enabled = true;

            }
            else
            {
                //IPAddress ip = IPAddress.Parse(ipAddress.Text);
                //tcpServer = new TcpListener(ip, int.Parse(port.Text));
                //tcpServer.Start();
                isRunning = true;
                startBtn.Text = "Disconnect";
                ipAddress.Enabled = false;
                port.Enabled = false;
                if(thread == null)
                {
                    thread = new Thread(StartServer);
                }
                else
                {
                    if(!thread.IsAlive)
                    {
                        thread = new Thread(StartServer);
                    }
                }
                thread.IsBackground = true;
                thread.Start();
            }
        }

        public void StartServer()
        {
            // Establish the local endpoint for the socket.  
            IPAddress ip = IPAddress.Parse(ipAddress.Text.Trim());
            int portNum = int.Parse(port.Text.Trim());
            IPEndPoint localEndPoint = new IPEndPoint(ip, portNum);

            // Create a TCP/IP socket.  
            listener = new Socket(ip.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                //ipAddress.Enabled = false;
                //port.Enabled = false;

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                if(thread.IsAlive)
                {
                    thread.Abort();
                }
                
                ipAddress.Enabled = true;
                port.Enabled = true;
                isRunning = false;
                startBtn.Text = "Start";
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();
            try
            {
                // Get the socket that handles the client request.  
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                // Create the state object.  
                StateObject state = new StateObject();
                state.socket = handler;
                handler.BeginReceive(state.dataBuffer, 0, state.bufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());// MessageBox.Show(ex.Message);
            }
            
        }

        public void ReadCallback(IAsyncResult ar)
        {
            try
            {
                String content = String.Empty;

                // Retrieve the state object and the handler socket  
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.socket;

                // Read data from the client socket.   
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(
                        state.dataBuffer, 0, bytesRead));

                    // Check for end-of-file tag. If it is not there, read   
                    // more data.  
                    content = state.sb.ToString();
                    if (content.IndexOf("<EOF>") > -1)
                    {
                        // All the data has been read from the   
                        // client. Display it on the console.  
                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                            content.Length, content);
                        // Echo the data back to the client.  

                        string result = content.Substring(0, content.IndexOf("<EOF>"));
                        string reply = "";
                        if (result.Contains("connect:") && result.Contains("disconnect:") == false)
                        {
                            string user = result.Substring(8);
                            string id = Guid.NewGuid().ToString();
                            if(userNames.ContainsKey(user))
                            {
                                Send(handler, "USER_EXISTS");
                                handler.Close();
                                return;
                            }
                            if (userNames.ContainsKey(user) == false)
                            {
                                AddUsers(user);
                                reply = "connect:";
                                int count = 0;
                                foreach (KeyValuePair<string, string> _user in userNames)
                                {
                                    reply += _user.Key;
                                    count += 1;
                                    if (count < userNames.Count)
                                    {
                                        reply += ",";
                                    }
                                }
                                userNames.Add(user, id);


                                foreach (KeyValuePair<string, Socket> _client in list_clients)
                                {
                                    Send(_client.Value, "connect:" + user);
                                }

                                list_clients.Add(id, handler);
                                UpdateMessageTextBox(user + " connected");
                            }

                            
                        }
                        else if (result.Contains("disconnect:"))
                        {
                            string user = result.Substring(11);
                            string id = userNames[user];
                            userNames.Remove(user);
                            RemoveUsers(user);
                            Socket clientSocket = list_clients[id];
                            clientSocket.Close();
                            list_clients.Remove(id);
                            foreach (KeyValuePair<string, Socket> _client in list_clients)
                            {
                                Send(_client.Value, "disconnect:" + user);
                            }

                            UpdateMessageTextBox(user + " disconnected");
                        }
                        else if(result.Contains("IM_") && result.IndexOf("IM_") == 0)
                        {
                            string str = result.Substring(3);
                            string targetUser = str.Substring(0, str.IndexOf(":"));
                            string message = str.Substring(str.IndexOf(":") + 1);
                            string id = userNames[targetUser];
                            Send(list_clients[id], "IM_" + message);
                        }
                        else
                        {
                            foreach (KeyValuePair<string, Socket> _client in list_clients)
                            {
                                Send(_client.Value, result);
                            }
                            UpdateMessageTextBox(result);
                        }

                        if (reply != "" && reply != "connect:")
                        {
                            Send(handler, reply);
                        }

                        // Create the state object.  
                        StateObject so = new StateObject();
                        so.socket = handler;
                        handler.BeginReceive(so.dataBuffer, 0, so.bufferSize, 0,
                        new AsyncCallback(ReadCallback), so);
                    }
                    else
                    {
                        // Not all data received. Get more.  
                        handler.BeginReceive(state.dataBuffer, 0, state.bufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                StateObject so = (StateObject)ar.AsyncState;
                Socket handler = so.socket;
                if(list_clients.ContainsValue(handler))
                {
                    int index = list_clients.Values.ToList().IndexOf(handler);
                    KeyValuePair<string, Socket> item = list_clients.ToList()[index];
                    string id = item.Key;
                    list_clients.Remove(id);

                    index = userNames.Values.ToList().IndexOf(id);
                    KeyValuePair<string, string> user = userNames.ToList()[index];
                    string name = user.Key;
                    userNames.Remove(name);

                    RemoveUsers(name);
                }
            }
        }

        private void UpdateMessageTextBox(string message)
        {
            if(messageTextBox.InvokeRequired)
            {
                messageTextBox.Invoke(new UpdateMessageCallBack(UpdateMessageTextBox), new object[] { message });
            }
            else
            {
                messageTextBox.AppendText(message);
                messageTextBox.AppendText(Environment.NewLine);
            }
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

        private void Send(Socket handler, string data)
        {
            try
            {
                // Convert the string data to byte data using ASCII encoding.  
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                // Begin sending the data to the remote device.  
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                 new AsyncCallback(SendCallback), handler);
                //handler.Send(byteData);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
                sendDone.Set();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void StopServer()
        {
            CloseConnections();
            listener.Close();
            list_clients.Clear();
            userNames.Clear();
            connectedUsers.Items.Clear();
            thread.Abort();
            messageTextBox.Clear();
        }

        private void CloseConnections()
        {
            foreach(KeyValuePair<string,Socket> client in list_clients)
            {
                Send(client.Value, "Server_Disconnect");
                sendDone.WaitOne();

                client.Value.Close();
            }
        }

        private void test_Click(object sender, EventArgs e)
        {
                foreach (KeyValuePair<string, Socket> _client in list_clients)
                {
                    Send(_client.Value, "Server:This is a test");
                    sendDone.WaitOne();
                }     
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnections();
            listener.Close();
            list_clients.Clear();
            userNames.Clear();
            connectedUsers.Items.Clear();
            thread.Abort();
            messageTextBox.Clear();
        }
    }
}
