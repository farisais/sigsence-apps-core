namespace DSA_Teaser
{
    partial class UserControlSignalGenerator
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxSignalType = new System.Windows.Forms.ComboBox();
            this.textEditFrequency = new DevExpress.XtraEditors.TextEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEditSamplingRate = new DevExpress.XtraEditors.TextEdit();
            this.textEditPhase = new DevExpress.XtraEditors.TextEdit();
            this.textEditAmplitude = new DevExpress.XtraEditors.TextEdit();
            this.textEditSampleSize = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.vGridControlSignalGenerator = new DevExpress.XtraVerticalGrid.VGridControl();
            this.repositoryItemComboBoxSignalType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.categoryRowDesign = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.editorRowName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryRowData = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.editorRowSignalType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRowFrequency = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRowAmplitude = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRowSampleSize = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRowSamplingRate = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRowPhase = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFrequency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSamplingRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPhase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAmplitude.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSampleSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControlSignalGenerator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxSignalType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxSignalType
            // 
            this.comboBoxSignalType.FormattingEnabled = true;
            this.comboBoxSignalType.Items.AddRange(new object[] {
            "Sine Signal",
            "Square Signal",
            "Sawtooth Signal",
            "Noise Signal"});
            this.comboBoxSignalType.Location = new System.Drawing.Point(12, 28);
            this.comboBoxSignalType.Name = "comboBoxSignalType";
            this.comboBoxSignalType.Size = new System.Drawing.Size(103, 21);
            this.comboBoxSignalType.TabIndex = 1;
            this.comboBoxSignalType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSignalType_SelectedIndexChanged);
            // 
            // textEditFrequency
            // 
            this.textEditFrequency.Location = new System.Drawing.Point(12, 69);
            this.textEditFrequency.Name = "textEditFrequency";
            this.textEditFrequency.Properties.Mask.EditMask = "n";
            this.textEditFrequency.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditFrequency.Size = new System.Drawing.Size(103, 20);
            this.textEditFrequency.StyleController = this.layoutControl1;
            this.textEditFrequency.TabIndex = 11;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textEditFrequency);
            this.layoutControl1.Controls.Add(this.textEditSamplingRate);
            this.layoutControl1.Controls.Add(this.textEditPhase);
            this.layoutControl1.Controls.Add(this.textEditAmplitude);
            this.layoutControl1.Controls.Add(this.textEditSampleSize);
            this.layoutControl1.Controls.Add(this.comboBoxSignalType);
            this.layoutControl1.Location = new System.Drawing.Point(15, 12);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(127, 279);
            this.layoutControl1.TabIndex = 16;
            this.layoutControl1.Text = "layoutControl1";
            this.layoutControl1.Visible = false;
            // 
            // textEditSamplingRate
            // 
            this.textEditSamplingRate.Location = new System.Drawing.Point(12, 189);
            this.textEditSamplingRate.Name = "textEditSamplingRate";
            this.textEditSamplingRate.Properties.Mask.EditMask = "d";
            this.textEditSamplingRate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditSamplingRate.Size = new System.Drawing.Size(103, 20);
            this.textEditSamplingRate.StyleController = this.layoutControl1;
            this.textEditSamplingRate.TabIndex = 15;
            // 
            // textEditPhase
            // 
            this.textEditPhase.Location = new System.Drawing.Point(12, 229);
            this.textEditPhase.Name = "textEditPhase";
            this.textEditPhase.Properties.Mask.EditMask = "n";
            this.textEditPhase.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditPhase.Size = new System.Drawing.Size(103, 20);
            this.textEditPhase.StyleController = this.layoutControl1;
            this.textEditPhase.TabIndex = 13;
            // 
            // textEditAmplitude
            // 
            this.textEditAmplitude.Location = new System.Drawing.Point(12, 109);
            this.textEditAmplitude.Name = "textEditAmplitude";
            this.textEditAmplitude.Properties.Mask.EditMask = "n";
            this.textEditAmplitude.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditAmplitude.Size = new System.Drawing.Size(103, 20);
            this.textEditAmplitude.StyleController = this.layoutControl1;
            this.textEditAmplitude.TabIndex = 12;
            // 
            // textEditSampleSize
            // 
            this.textEditSampleSize.Location = new System.Drawing.Point(12, 149);
            this.textEditSampleSize.Name = "textEditSampleSize";
            this.textEditSampleSize.Properties.Mask.EditMask = "d";
            this.textEditSampleSize.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditSampleSize.Size = new System.Drawing.Size(103, 20);
            this.textEditSampleSize.StyleController = this.layoutControl1;
            this.textEditSampleSize.TabIndex = 14;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(127, 279);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.comboBoxSignalType;
            this.layoutControlItem2.CustomizationFormText = "Signal Type";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(107, 41);
            this.layoutControlItem2.Text = "Signal Type";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditFrequency;
            this.layoutControlItem1.CustomizationFormText = "Frequency";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 41);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(107, 40);
            this.layoutControlItem1.Text = "Frequency";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEditAmplitude;
            this.layoutControlItem3.CustomizationFormText = "Amplitude";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 81);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(107, 40);
            this.layoutControlItem3.Text = "Amplitude";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textEditSampleSize;
            this.layoutControlItem4.CustomizationFormText = "Sample Size";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 121);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(107, 40);
            this.layoutControlItem4.Text = "Sample Size";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.textEditSamplingRate;
            this.layoutControlItem5.CustomizationFormText = "Sampling Rate";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 161);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(107, 40);
            this.layoutControlItem5.Text = "Sampling Rate";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.textEditPhase;
            this.layoutControlItem6.CustomizationFormText = "Phase";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 201);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(107, 58);
            this.layoutControlItem6.Text = "Phase";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(68, 13);
            // 
            // vGridControlSignalGenerator
            // 
            this.vGridControlSignalGenerator.Appearance.RecordValue.Options.UseTextOptions = true;
            this.vGridControlSignalGenerator.Appearance.RecordValue.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.vGridControlSignalGenerator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vGridControlSignalGenerator.LayoutStyle = DevExpress.XtraVerticalGrid.LayoutViewStyle.SingleRecordView;
            this.vGridControlSignalGenerator.Location = new System.Drawing.Point(0, 0);
            this.vGridControlSignalGenerator.Name = "vGridControlSignalGenerator";
            this.vGridControlSignalGenerator.OptionsView.AutoScaleBands = true;
            this.vGridControlSignalGenerator.RecordWidth = 97;
            this.vGridControlSignalGenerator.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxSignalType,
            this.repositoryItemTextEdit1});
            this.vGridControlSignalGenerator.RowHeaderWidth = 103;
            this.vGridControlSignalGenerator.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.categoryRowDesign,
            this.categoryRowData});
            this.vGridControlSignalGenerator.Size = new System.Drawing.Size(345, 378);
            this.vGridControlSignalGenerator.TabIndex = 17;
            // 
            // repositoryItemComboBoxSignalType
            // 
            this.repositoryItemComboBoxSignalType.AutoHeight = false;
            this.repositoryItemComboBoxSignalType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxSignalType.Items.AddRange(new object[] {
            "Sine Signal",
            "Square Signal",
            "Sawtooth Signal",
            "Noise Signal"});
            this.repositoryItemComboBoxSignalType.Name = "repositoryItemComboBoxSignalType";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // categoryRowDesign
            // 
            this.categoryRowDesign.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.editorRowName});
            this.categoryRowDesign.Height = 19;
            this.categoryRowDesign.Name = "categoryRowDesign";
            this.categoryRowDesign.Properties.Caption = "Design";
            // 
            // editorRowName
            // 
            this.editorRowName.Name = "editorRowName";
            this.editorRowName.Properties.Caption = "Name";
            // 
            // categoryRowData
            // 
            this.categoryRowData.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.editorRowSignalType,
            this.editorRowFrequency,
            this.editorRowAmplitude,
            this.editorRowSampleSize,
            this.editorRowSamplingRate,
            this.editorRowPhase});
            this.categoryRowData.Height = 19;
            this.categoryRowData.Name = "categoryRowData";
            this.categoryRowData.Properties.Caption = "Data";
            // 
            // editorRowSignalType
            // 
            this.editorRowSignalType.Name = "editorRowSignalType";
            this.editorRowSignalType.Properties.Caption = "Signal Type";
            this.editorRowSignalType.Properties.RowEdit = this.repositoryItemComboBoxSignalType;
            // 
            // editorRowFrequency
            // 
            this.editorRowFrequency.Name = "editorRowFrequency";
            this.editorRowFrequency.Properties.Caption = "Frequency";
            this.editorRowFrequency.Properties.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.editorRowFrequency.Properties.RowEdit = this.repositoryItemTextEdit1;
            this.editorRowFrequency.Properties.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            // 
            // editorRowAmplitude
            // 
            this.editorRowAmplitude.Name = "editorRowAmplitude";
            this.editorRowAmplitude.Properties.Caption = "Amplitude";
            // 
            // editorRowSampleSize
            // 
            this.editorRowSampleSize.Height = 16;
            this.editorRowSampleSize.Name = "editorRowSampleSize";
            this.editorRowSampleSize.Properties.Caption = "Sample Size";
            // 
            // editorRowSamplingRate
            // 
            this.editorRowSamplingRate.Name = "editorRowSamplingRate";
            this.editorRowSamplingRate.Properties.Caption = "Sampling Rate";
            // 
            // editorRowPhase
            // 
            this.editorRowPhase.Name = "editorRowPhase";
            this.editorRowPhase.Properties.Caption = "Phase";
            // 
            // UserControlSignalGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vGridControlSignalGenerator);
            this.Controls.Add(this.layoutControl1);
            this.Name = "UserControlSignalGenerator";
            this.Size = new System.Drawing.Size(345, 378);
            this.Load += new System.EventHandler(this.UserControlSignalGenerator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditFrequency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditSamplingRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPhase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAmplitude.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSampleSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControlSignalGenerator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxSignalType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.TextEdit textEditFrequency;
        public DevExpress.XtraEditors.TextEdit textEditAmplitude;
        public DevExpress.XtraEditors.TextEdit textEditPhase;
        public DevExpress.XtraEditors.TextEdit textEditSampleSize;
        public DevExpress.XtraEditors.TextEdit textEditSamplingRate;
        public System.Windows.Forms.ComboBox comboBoxSignalType;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        public DevExpress.XtraVerticalGrid.VGridControl vGridControlSignalGenerator;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRowDesign;
        public DevExpress.XtraVerticalGrid.Rows.EditorRow editorRowName;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRowData;
        public DevExpress.XtraVerticalGrid.Rows.EditorRow editorRowSignalType;
        public DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxSignalType;
        public DevExpress.XtraVerticalGrid.Rows.EditorRow editorRowFrequency;
        public DevExpress.XtraVerticalGrid.Rows.EditorRow editorRowAmplitude;
        public DevExpress.XtraVerticalGrid.Rows.EditorRow editorRowSampleSize;
        public DevExpress.XtraVerticalGrid.Rows.EditorRow editorRowSamplingRate;
        public DevExpress.XtraVerticalGrid.Rows.EditorRow editorRowPhase;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}
