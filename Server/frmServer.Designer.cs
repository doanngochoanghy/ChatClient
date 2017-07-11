namespace Server
{
    partial class frmServer
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblIPAdd = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtIPAdd = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lvwClient = new System.Windows.Forms.ListView();
            this.clmName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(205, 252);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // lblIPAdd
            // 
            this.lblIPAdd.AutoSize = true;
            this.lblIPAdd.Location = new System.Drawing.Point(12, 262);
            this.lblIPAdd.Name = "lblIPAdd";
            this.lblIPAdd.Size = new System.Drawing.Size(58, 13);
            this.lblIPAdd.TabIndex = 1;
            this.lblIPAdd.Text = "IP Address";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(262, 262);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port";
            // 
            // txtIPAdd
            // 
            this.txtIPAdd.Location = new System.Drawing.Point(76, 258);
            this.txtIPAdd.Name = "txtIPAdd";
            this.txtIPAdd.ReadOnly = true;
            this.txtIPAdd.Size = new System.Drawing.Size(168, 20);
            this.txtIPAdd.TabIndex = 3;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(294, 258);
            this.txtPort.Name = "txtPort";
            this.txtPort.ReadOnly = true;
            this.txtPort.Size = new System.Drawing.Size(56, 20);
            this.txtPort.TabIndex = 4;
            // 
            // lvwClient
            // 
            this.lvwClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmName});
            this.lvwClient.Location = new System.Drawing.Point(211, 0);
            this.lvwClient.Name = "lvwClient";
            this.lvwClient.Size = new System.Drawing.Size(214, 252);
            this.lvwClient.TabIndex = 5;
            this.lvwClient.UseCompatibleStateImageBehavior = false;
            this.lvwClient.View = System.Windows.Forms.View.Details;
            // 
            // clmName
            // 
            this.clmName.Text = "Name";
            this.clmName.Width = 218;
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 289);
            this.Controls.Add(this.lvwClient);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIPAdd);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIPAdd);
            this.Controls.Add(this.richTextBox1);
            this.Name = "frmServer";
            this.Text = "Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmServer_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblIPAdd;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtIPAdd;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.ListView lvwClient;
        private System.Windows.Forms.ColumnHeader clmName;
    }
}

