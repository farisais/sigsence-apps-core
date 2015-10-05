namespace DSA_Teaser
{
    partial class FormNIDeviceSetup
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
            this.comboBoxChannel = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spinEditMinVal = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditMaxVal = new DevExpress.XtraEditors.SpinEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.textEditRate = new DevExpress.XtraEditors.TextEdit();
            this.textEditSamplePerCh = new DevExpress.XtraEditors.TextEdit();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.checkEditAutoScale = new DevExpress.XtraEditors.CheckEdit();
            this.waveformGraphScope = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.waveformPlot1 = new NationalInstruments.UI.WaveformPlot();
            this.xAxis1 = new NationalInstruments.UI.XAxis();
            this.yAxis1 = new NationalInstruments.UI.YAxis();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxChannel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMinVal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMaxVal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSamplePerCh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAutoScale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waveformGraphScope)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxChannel
            // 
            this.comboBoxChannel.Location = new System.Drawing.Point(111, 36);
            this.comboBoxChannel.Name = "comboBoxChannel";
            this.comboBoxChannel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxChannel.Size = new System.Drawing.Size(172, 20);
            this.comboBoxChannel.TabIndex = 0;
            this.comboBoxChannel.SelectedValueChanged += new System.EventHandler(this.comboBoxChannel_SelectedValueChanged);
            // 
            // spinEditMinVal
            // 
            this.spinEditMinVal.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.spinEditMinVal.Location = new System.Drawing.Point(111, 62);
            this.spinEditMinVal.Name = "spinEditMinVal";
            this.spinEditMinVal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditMinVal.Size = new System.Drawing.Size(172, 20);
            this.spinEditMinVal.TabIndex = 1;
            // 
            // spinEditMaxVal
            // 
            this.spinEditMaxVal.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEditMaxVal.Location = new System.Drawing.Point(111, 88);
            this.spinEditMaxVal.Name = "spinEditMaxVal";
            this.spinEditMaxVal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditMaxVal.Size = new System.Drawing.Size(172, 20);
            this.spinEditMaxVal.TabIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.comboBoxChannel);
            this.groupControl1.Controls.Add(this.spinEditMaxVal);
            this.groupControl1.Controls.Add(this.spinEditMinVal);
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(297, 132);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Channel Parameters";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 91);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(73, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Maximum Value";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 65);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Minimum Value";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 39);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Channel Select";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.textEditRate);
            this.groupControl2.Controls.Add(this.textEditSamplePerCh);
            this.groupControl2.Location = new System.Drawing.Point(3, 141);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(297, 127);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "Timing Parameters";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(20, 76);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(46, 13);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "Rate (Hz)";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(20, 39);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(83, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Sample / Channel";
            // 
            // textEditRate
            // 
            this.textEditRate.EditValue = "25600";
            this.textEditRate.Location = new System.Drawing.Point(111, 73);
            this.textEditRate.Name = "textEditRate";
            this.textEditRate.Properties.Mask.EditMask = "n0";
            this.textEditRate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textEditRate.Size = new System.Drawing.Size(172, 20);
            this.textEditRate.TabIndex = 1;
            this.textEditRate.EditValueChanged += new System.EventHandler(this.textEditRate_EditValueChanged);
            // 
            // textEditSamplePerCh
            // 
            this.textEditSamplePerCh.EditValue = "25600";
            this.textEditSamplePerCh.Location = new System.Drawing.Point(111, 36);
            this.textEditSamplePerCh.Name = "textEditSamplePerCh";
            this.textEditSamplePerCh.Properties.Mask.EditMask = "n0";
            this.textEditSamplePerCh.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditSamplePerCh.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textEditSamplePerCh.Size = new System.Drawing.Size(172, 20);
            this.textEditSamplePerCh.TabIndex = 0;
            this.textEditSamplePerCh.EditValueChanged += new System.EventHandler(this.textEditSamplePerCh_EditValueChanged);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 12);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(610, 300);
            this.xtraTabControl1.TabIndex = 5;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.labelControl6);
            this.xtraTabPage1.Controls.Add(this.checkEditAutoScale);
            this.xtraTabPage1.Controls.Add(this.waveformGraphScope);
            this.xtraTabPage1.Controls.Add(this.groupControl1);
            this.xtraTabPage1.Controls.Add(this.groupControl2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(602, 271);
            this.xtraTabPage1.Text = "Configuration";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(306, 6);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(29, 13);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "Scope";
            // 
            // checkEditAutoScale
            // 
            this.checkEditAutoScale.Location = new System.Drawing.Point(509, 3);
            this.checkEditAutoScale.Name = "checkEditAutoScale";
            this.checkEditAutoScale.Properties.Caption = "AutoScale - Y";
            this.checkEditAutoScale.Size = new System.Drawing.Size(90, 18);
            this.checkEditAutoScale.TabIndex = 6;
            this.checkEditAutoScale.CheckedChanged += new System.EventHandler(this.checkEditAutoScale_CheckedChanged);
            // 
            // waveformGraphScope
            // 
            this.waveformGraphScope.Location = new System.Drawing.Point(306, 25);
            this.waveformGraphScope.Name = "waveformGraphScope";
            this.waveformGraphScope.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot1});
            this.waveformGraphScope.Size = new System.Drawing.Size(293, 243);
            this.waveformGraphScope.TabIndex = 5;
            this.waveformGraphScope.UseColorGenerator = true;
            this.waveformGraphScope.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.xAxis1});
            this.waveformGraphScope.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.yAxis1});
            // 
            // waveformPlot1
            // 
            this.waveformPlot1.XAxis = this.xAxis1;
            this.waveformPlot1.YAxis = this.yAxis1;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(603, 271);
            this.xtraTabPage2.Text = "Triggering";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(603, 271);
            this.xtraTabPage3.Text = "Advance Timing";
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(12, 318);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(127, 28);
            this.simpleButtonOK.TabIndex = 6;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Location = new System.Drawing.Point(187, 318);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(127, 28);
            this.simpleButtonCancel.TabIndex = 7;
            this.simpleButtonCancel.Text = "Cancel";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // FormNIDeviceSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 358);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.xtraTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNIDeviceSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NI Device Setup";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxChannel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMinVal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMaxVal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSamplePerCh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAutoScale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waveformGraphScope)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private NationalInstruments.UI.WindowsForms.WaveformGraph waveformGraphScope;
        private NationalInstruments.UI.WaveformPlot waveformPlot1;
        private NationalInstruments.UI.XAxis xAxis1;
        private NationalInstruments.UI.YAxis yAxis1;
        private DevExpress.XtraEditors.CheckEdit checkEditAutoScale;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxChannel;
        public DevExpress.XtraEditors.SpinEdit spinEditMinVal;
        public DevExpress.XtraEditors.SpinEdit spinEditMaxVal;
        public DevExpress.XtraEditors.TextEdit textEditRate;
        public DevExpress.XtraEditors.TextEdit textEditSamplePerCh;
    }
}