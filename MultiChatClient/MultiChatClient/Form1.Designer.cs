namespace MultiChatClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ipAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.TextBox();
            this.connect = new System.Windows.Forms.Button();
            this.connectedUsers = new System.Windows.Forms.ListBox();
            this.sendText = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.receiveText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.userText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server IP";
            // 
            // ipAddress
            // 
            this.ipAddress.Location = new System.Drawing.Point(69, 15);
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.Size = new System.Drawing.Size(141, 20);
            this.ipAddress.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server Port";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(69, 44);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(50, 20);
            this.port.TabIndex = 3;
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(69, 110);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(75, 23);
            this.connect.TabIndex = 4;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // connectedUsers
            // 
            this.connectedUsers.FormattingEnabled = true;
            this.connectedUsers.Location = new System.Drawing.Point(6, 173);
            this.connectedUsers.Name = "connectedUsers";
            this.connectedUsers.Size = new System.Drawing.Size(204, 134);
            this.connectedUsers.TabIndex = 5;
            this.connectedUsers.DoubleClick += new System.EventHandler(this.connectedUsers_DoubleClick);
            // 
            // sendText
            // 
            this.sendText.Location = new System.Drawing.Point(6, 375);
            this.sendText.Name = "sendText";
            this.sendText.Size = new System.Drawing.Size(782, 20);
            this.sendText.TabIndex = 6;
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(6, 415);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(75, 23);
            this.send.TabIndex = 7;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // receiveText
            // 
            this.receiveText.Location = new System.Drawing.Point(361, 30);
            this.receiveText.Multiline = true;
            this.receiveText.Name = "receiveText";
            this.receiveText.Size = new System.Drawing.Size(427, 277);
            this.receiveText.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Username";
            // 
            // userText
            // 
            this.userText.Location = new System.Drawing.Point(69, 72);
            this.userText.Name = "userText";
            this.userText.Size = new System.Drawing.Size(141, 20);
            this.userText.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(173, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Status";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Users";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(358, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Messages";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.userText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.receiveText);
            this.Controls.Add(this.send);
            this.Controls.Add(this.sendText);
            this.Controls.Add(this.connectedUsers);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipAddress);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "MultiClientApplication";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.ListBox connectedUsers;
        private System.Windows.Forms.TextBox sendText;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.TextBox receiveText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

