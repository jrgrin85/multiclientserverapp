namespace MultiChatServer
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
            this.startBtn = new System.Windows.Forms.Button();
            this.connectedUsers = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.test = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // ipAddress
            // 
            this.ipAddress.Location = new System.Drawing.Point(77, 10);
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.Size = new System.Drawing.Size(154, 20);
            this.ipAddress.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(77, 40);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(56, 20);
            this.port.TabIndex = 3;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(77, 90);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(85, 23);
            this.startBtn.TabIndex = 4;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // connectedUsers
            // 
            this.connectedUsers.FormattingEnabled = true;
            this.connectedUsers.Location = new System.Drawing.Point(290, 40);
            this.connectedUsers.Name = "connectedUsers";
            this.connectedUsers.Size = new System.Drawing.Size(235, 212);
            this.connectedUsers.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Connected Users";
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(16, 278);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.Size = new System.Drawing.Size(509, 202);
            this.messageTextBox.TabIndex = 7;
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(77, 119);
            this.test.Name = "test";
            this.test.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.test.Size = new System.Drawing.Size(85, 23);
            this.test.TabIndex = 8;
            this.test.Text = "Test";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Messages";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 538);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.test);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.connectedUsers);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipAddress);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "MultClientServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.ListBox connectedUsers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Label label4;
    }
}

