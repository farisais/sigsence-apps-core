namespace DSA_Teaser.Class.Function.ObjectSequence
{
    partial class FormFFTSourceSelect
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
            this.listBoxControlSource = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControlSource = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEditWindow = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditOutput = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditWindow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditOutput.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxControlSource
            // 
            this.listBoxControlSource.Location = new System.Drawing.Point(9, 31);
            this.listBoxControlSource.Name = "listBoxControlSource";
            this.listBoxControlSource.Size = new System.Drawing.Size(212, 313);
            this.listBoxControlSource.TabIndex = 0;
            // 
            // labelControlSource
            // 
            this.labelControlSource.Location = new System.Drawing.Point(12, 12);
            this.labelControlSource.Name = "labelControlSource";
            this.labelControlSource.Size = new System.Drawing.Size(69, 13);
            this.labelControlSource.TabIndex = 1;
            this.labelControlSource.Text = "Select Source:";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(12, 350);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(156, 25);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "OK";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(211, 350);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(156, 25);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "Cancel";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // comboBoxEditWindow
            // 
            this.comboBoxEditWindow.Location = new System.Drawing.Point(227, 31);
            this.comboBoxEditWindow.Name = "comboBoxEditWindow";
            this.comboBoxEditWindow.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditWindow.Size = new System.Drawing.Size(140, 20);
            this.comboBoxEditWindow.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(227, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(69, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Window Type:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(227, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Output Type:";
            // 
            // comboBoxEditOutput
            // 
            this.comboBoxEditOutput.Location = new System.Drawing.Point(227, 76);
            this.comboBoxEditOutput.Name = "comboBoxEditOutput";
            this.comboBoxEditOutput.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditOutput.Properties.Items.AddRange(new object[] {
            "Linear",
            "DB"});
            this.comboBoxEditOutput.Size = new System.Drawing.Size(140, 20);
            this.comboBoxEditOutput.TabIndex = 7;
            // 
            // FormFFTSourceSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 383);
            this.Controls.Add(this.comboBoxEditOutput);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.comboBoxEditWindow);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControlSource);
            this.Controls.Add(this.listBoxControlSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFFTSourceSelect";
            this.Text = "FFT Source Select";
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditWindow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditOutput.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControlSource;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        public DevExpress.XtraEditors.ListBoxControl listBoxControlSource;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditWindow;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditOutput;
    }
}