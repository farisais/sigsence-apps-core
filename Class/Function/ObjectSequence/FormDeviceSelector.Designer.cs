namespace DSA_Teaser.Class.Function.ObjectSequence
{
    partial class FormDeviceSelector
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
            this.comboBoxEditDeviceList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControlUSBDevice = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEditDataBuffer = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonTestConnection = new DevExpress.XtraEditors.SimpleButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStatusFound = new System.Windows.Forms.ToolStripStatusLabel();
            this.simpleButtonRefreshList = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.richTextBoxCommand = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDeviceList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlUSBDevice)).BeginInit();
            this.groupControlUSBDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDataBuffer.Properties)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxEditDeviceList
            // 
            this.comboBoxEditDeviceList.Location = new System.Drawing.Point(5, 25);
            this.comboBoxEditDeviceList.Name = "comboBoxEditDeviceList";
            this.comboBoxEditDeviceList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditDeviceList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditDeviceList.Size = new System.Drawing.Size(388, 20);
            this.comboBoxEditDeviceList.TabIndex = 0;
            // 
            // groupControlUSBDevice
            // 
            this.groupControlUSBDevice.Controls.Add(this.labelControl1);
            this.groupControlUSBDevice.Controls.Add(this.textEditDataBuffer);
            this.groupControlUSBDevice.Controls.Add(this.comboBoxEditDeviceList);
            this.groupControlUSBDevice.Location = new System.Drawing.Point(12, 12);
            this.groupControlUSBDevice.Name = "groupControlUSBDevice";
            this.groupControlUSBDevice.Size = new System.Drawing.Size(398, 78);
            this.groupControlUSBDevice.TabIndex = 1;
            this.groupControlUSBDevice.Text = "DAQ Device";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 13);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "Data Buffer:";
            // 
            // textEditDataBuffer
            // 
            this.textEditDataBuffer.Location = new System.Drawing.Point(74, 51);
            this.textEditDataBuffer.Name = "textEditDataBuffer";
            this.textEditDataBuffer.Properties.Mask.EditMask = "d";
            this.textEditDataBuffer.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditDataBuffer.Size = new System.Drawing.Size(319, 20);
            this.textEditDataBuffer.TabIndex = 9;
            // 
            // simpleButtonTestConnection
            // 
            this.simpleButtonTestConnection.Location = new System.Drawing.Point(12, 96);
            this.simpleButtonTestConnection.Name = "simpleButtonTestConnection";
            this.simpleButtonTestConnection.Size = new System.Drawing.Size(308, 23);
            this.simpleButtonTestConnection.TabIndex = 2;
            this.simpleButtonTestConnection.Text = "Test Connection";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelStatusFound});
            this.statusStrip1.Location = new System.Drawing.Point(0, 352);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(422, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(82, 17);
            this.toolStripStatusLabel1.Text = "Device Found:";
            // 
            // toolStripStatusLabelStatusFound
            // 
            this.toolStripStatusLabelStatusFound.Name = "toolStripStatusLabelStatusFound";
            this.toolStripStatusLabelStatusFound.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabelStatusFound.Text = "0";
            // 
            // simpleButtonRefreshList
            // 
            this.simpleButtonRefreshList.Location = new System.Drawing.Point(326, 96);
            this.simpleButtonRefreshList.Name = "simpleButtonRefreshList";
            this.simpleButtonRefreshList.Size = new System.Drawing.Size(84, 23);
            this.simpleButtonRefreshList.TabIndex = 5;
            this.simpleButtonRefreshList.Text = "Refresh";
            this.simpleButtonRefreshList.Click += new System.EventHandler(this.simpleButtonRefreshList_Click);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(12, 319);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(186, 23);
            this.simpleButtonOK.TabIndex = 6;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Location = new System.Drawing.Point(224, 319);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(186, 23);
            this.simpleButtonCancel.TabIndex = 7;
            this.simpleButtonCancel.Text = "Cancel";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // richTextBoxCommand
            // 
            this.richTextBoxCommand.BackColor = System.Drawing.Color.White;
            this.richTextBoxCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxCommand.Location = new System.Drawing.Point(12, 125);
            this.richTextBoxCommand.Name = "richTextBoxCommand";
            this.richTextBoxCommand.ReadOnly = true;
            this.richTextBoxCommand.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxCommand.Size = new System.Drawing.Size(398, 188);
            this.richTextBoxCommand.TabIndex = 8;
            this.richTextBoxCommand.Text = "";
            // 
            // FormDeviceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 374);
            this.Controls.Add(this.richTextBoxCommand);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.simpleButtonRefreshList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.simpleButtonTestConnection);
            this.Controls.Add(this.groupControlUSBDevice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDeviceSelector";
            this.Text = "Device Selector";
            this.Load += new System.EventHandler(this.FormDeviceSelector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDeviceList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlUSBDevice)).EndInit();
            this.groupControlUSBDevice.ResumeLayout(false);
            this.groupControlUSBDevice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDataBuffer.Properties)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlUSBDevice;
        private DevExpress.XtraEditors.SimpleButton simpleButtonTestConnection;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatusFound;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRefreshList;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private System.Windows.Forms.RichTextBox richTextBoxCommand;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit textEditDataBuffer;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditDeviceList;
    }
}