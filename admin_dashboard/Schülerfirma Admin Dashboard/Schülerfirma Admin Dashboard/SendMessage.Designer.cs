namespace Schülerfirma_Admin_Dashboard
{
    partial class SendMessage
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
            this.recipientsBox = new System.Windows.Forms.TextBox();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sendMsg = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.templateBox = new System.Windows.Forms.ComboBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.searchResultBox = new System.Windows.Forms.ListBox();
            this.attachmentsList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Empfänger hinzufügen";
            // 
            // recipientsBox
            // 
            this.recipientsBox.Location = new System.Drawing.Point(15, 25);
            this.recipientsBox.Name = "recipientsBox";
            this.recipientsBox.Size = new System.Drawing.Size(313, 20);
            this.recipientsBox.TabIndex = 1;
            this.recipientsBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(15, 136);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(313, 301);
            this.messageBox.TabIndex = 2;
            this.messageBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nachricht schreiben";
            // 
            // sendMsg
            // 
            this.sendMsg.Location = new System.Drawing.Point(54, 443);
            this.sendMsg.Name = "sendMsg";
            this.sendMsg.Size = new System.Drawing.Size(274, 33);
            this.sendMsg.TabIndex = 4;
            this.sendMsg.Text = "Senden";
            this.sendMsg.UseVisualStyleBackColor = true;
            this.sendMsg.Click += new System.EventHandler(this.sendMsg_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 490);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Angehängte Dateien:";
            // 
            // button1
            // 
            this.button1.Image = global::Schülerfirma_Admin_Dashboard.Properties.Resources.paperclip__brand__7001;
            this.button1.Location = new System.Drawing.Point(15, 443);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(20);
            this.button1.Size = new System.Drawing.Size(33, 33);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Vorlage";
            // 
            // templateBox
            // 
            this.templateBox.FormattingEnabled = true;
            this.templateBox.Items.AddRange(new object[] {
            "– Keine – ",
            "Zahlungserinnerung I ",
            "Zahlungserinnerung II ",
            "Zahlungserinnerung III "});
            this.templateBox.Location = new System.Drawing.Point(15, 79);
            this.templateBox.Name = "templateBox";
            this.templateBox.Size = new System.Drawing.Size(313, 21);
            this.templateBox.TabIndex = 9;
            this.templateBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // searchResultBox
            // 
            this.searchResultBox.FormattingEnabled = true;
            this.searchResultBox.Items.AddRange(new object[] {
            "Name 1 ",
            "Name 2 ",
            "Name 3",
            "usw."});
            this.searchResultBox.Location = new System.Drawing.Point(331, 46);
            this.searchResultBox.Name = "searchResultBox";
            this.searchResultBox.Size = new System.Drawing.Size(313, 95);
            this.searchResultBox.TabIndex = 10;
            this.searchResultBox.Visible = false;
            this.searchResultBox.SelectedIndexChanged += new System.EventHandler(this.searchResultBox_SelectedIndexChanged);
            // 
            // attachmentsList
            // 
            this.attachmentsList.FormattingEnabled = true;
            this.attachmentsList.Items.AddRange(new object[] {
            "Datei 1.jpg ",
            "Datei 2.pdf ",
            "Datei 3.txt "});
            this.attachmentsList.Location = new System.Drawing.Point(15, 509);
            this.attachmentsList.Name = "attachmentsList";
            this.attachmentsList.Size = new System.Drawing.Size(313, 69);
            this.attachmentsList.TabIndex = 11;
            // 
            // SendMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 587);
            this.Controls.Add(this.attachmentsList);
            this.Controls.Add(this.searchResultBox);
            this.Controls.Add(this.templateBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sendMsg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.recipientsBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SendMessage";
            this.Opacity = 0.9D;
            this.Text = "Nachricht senden";
            this.Load += new System.EventHandler(this.SendMessage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox recipientsBox;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sendMsg;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox templateBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ListBox searchResultBox;
        private System.Windows.Forms.ListBox attachmentsList;
    }
}