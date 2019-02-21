namespace MultiChatClient
{
    partial class ChatIM
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
            this.messages = new System.Windows.Forms.TextBox();
            this.sendText = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messages
            // 
            this.messages.Location = new System.Drawing.Point(12, 12);
            this.messages.Multiline = true;
            this.messages.Name = "messages";
            this.messages.ReadOnly = true;
            this.messages.Size = new System.Drawing.Size(489, 342);
            this.messages.TabIndex = 0;
            // 
            // sendText
            // 
            this.sendText.Location = new System.Drawing.Point(12, 380);
            this.sendText.Name = "sendText";
            this.sendText.Size = new System.Drawing.Size(489, 20);
            this.sendText.TabIndex = 1;
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(12, 415);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(75, 23);
            this.send.TabIndex = 2;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // ChatIM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 450);
            this.Controls.Add(this.send);
            this.Controls.Add(this.sendText);
            this.Controls.Add(this.messages);
            this.Name = "ChatIM";
            this.Text = "ChatIM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatIM_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox messages;
        private System.Windows.Forms.TextBox sendText;
        private System.Windows.Forms.Button send;
    }
}