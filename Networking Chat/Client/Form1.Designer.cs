namespace Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textbox = new TextBox();
            send = new Button();
            ip = new TextBox();
            setaddress = new Button();
            username = new Label();
            chatlog = new TextBox();
            SuspendLayout();
            // 
            // textbox
            // 
            textbox.BorderStyle = BorderStyle.None;
            textbox.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textbox.Location = new Point(12, 847);
            textbox.Name = "textbox";
            textbox.Size = new Size(581, 64);
            textbox.TabIndex = 0;
            // 
            // send
            // 
            send.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            send.Location = new Point(599, 847);
            send.Name = "send";
            send.Size = new Size(66, 64);
            send.TabIndex = 1;
            send.Text = "OK";
            send.UseVisualStyleBackColor = true;
            send.Click += send_Click;
            // 
            // ip
            // 
            ip.BorderStyle = BorderStyle.None;
            ip.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ip.Location = new Point(12, 25);
            ip.Name = "ip";
            ip.Size = new Size(299, 64);
            ip.TabIndex = 3;
            ip.Text = "IP ADDRESS";
            ip.TextAlign = HorizontalAlignment.Center;
            // 
            // setaddress
            // 
            setaddress.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            setaddress.Location = new Point(326, 25);
            setaddress.Name = "setaddress";
            setaddress.Size = new Size(66, 64);
            setaddress.TabIndex = 4;
            setaddress.Text = "OK";
            setaddress.UseVisualStyleBackColor = true;
            setaddress.Click += setaddress_Click;
            // 
            // username
            // 
            username.BackColor = Color.White;
            username.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            username.Location = new Point(409, 25);
            username.Name = "username";
            username.Size = new Size(256, 64);
            username.TabIndex = 5;
            username.Text = "CLIENT";
            username.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // chatlog
            // 
            chatlog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            chatlog.BackColor = Color.White;
            chatlog.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chatlog.Location = new Point(12, 95);
            chatlog.Multiline = true;
            chatlog.Name = "chatlog";
            chatlog.ReadOnly = true;
            chatlog.Size = new Size(653, 746);
            chatlog.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateGray;
            BackgroundImage = Properties.Resources.bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(677, 930);
            Controls.Add(chatlog);
            Controls.Add(username);
            Controls.Add(setaddress);
            Controls.Add(ip);
            Controls.Add(send);
            Controls.Add(textbox);
            Name = "Form1";
            Text = "Server";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textbox;
        private Button send;
        private TextBox ip;
        private Button setaddress;
        private Label username;
        private TextBox chatlog;
    }
}
