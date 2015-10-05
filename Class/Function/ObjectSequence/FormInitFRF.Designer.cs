namespace DSA_Teaser.Class.Function.ObjectSequence
{
    partial class FormInitFRF
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
            this.comboBoxEditSelectStimulus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.checkedListBoxControlSelectResponse = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSelectStimulus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlSelectResponse)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxEditSelectStimulus
            // 
            this.comboBoxEditSelectStimulus.Location = new System.Drawing.Point(12, 35);
            this.comboBoxEditSelectStimulus.Name = "comboBoxEditSelectStimulus";
            this.comboBoxEditSelectStimulus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditSelectStimulus.Size = new System.Drawing.Size(267, 20);
            this.comboBoxEditSelectStimulus.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(102, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Select Stimulus Signal";
            // 
            // checkedListBoxControlSelectResponse
            // 
            this.checkedListBoxControlSelectResponse.Location = new System.Drawing.Point(12, 82);
            this.checkedListBoxControlSelectResponse.Name = "checkedListBoxControlSelectResponse";
            this.checkedListBoxControlSelectResponse.Size = new System.Drawing.Size(267, 284);
            this.checkedListBoxControlSelectResponse.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(110, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Select Response Signal";
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(12, 372);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(110, 28);
            this.simpleButtonOK.TabIndex = 4;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Location = new System.Drawing.Point(169, 372);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(110, 28);
            this.simpleButtonCancel.TabIndex = 5;
            this.simpleButtonCancel.Text = "Cancel";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // FormInitFRF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 409);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.checkedListBoxControlSelectResponse);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.comboBoxEditSelectStimulus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormInitFRF";
            this.Text = "Init FRF";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSelectStimulus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlSelectResponse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSelectStimulus;
        public DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlSelectResponse;
    }
}