namespace DSA_Teaser.Class.Function.ObjectIndicator
{
    partial class FormCameraConfig
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
            this.comboBoxEditCameraList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.checkEditFlipV = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditFlipH = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCameraList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFlipV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFlipH.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxEditCameraList
            // 
            this.comboBoxEditCameraList.Location = new System.Drawing.Point(12, 31);
            this.comboBoxEditCameraList.Name = "comboBoxEditCameraList";
            this.comboBoxEditCameraList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditCameraList.Size = new System.Drawing.Size(225, 20);
            this.comboBoxEditCameraList.TabIndex = 0;
            this.comboBoxEditCameraList.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditCameraList_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Camera:";
            // 
            // checkEditFlipV
            // 
            this.checkEditFlipV.Location = new System.Drawing.Point(12, 57);
            this.checkEditFlipV.Name = "checkEditFlipV";
            this.checkEditFlipV.Properties.Caption = "Flip Vertical";
            this.checkEditFlipV.Size = new System.Drawing.Size(225, 18);
            this.checkEditFlipV.TabIndex = 2;
            this.checkEditFlipV.CheckedChanged += new System.EventHandler(this.checkEditFlipV_CheckedChanged);
            // 
            // checkEditFlipH
            // 
            this.checkEditFlipH.Location = new System.Drawing.Point(12, 81);
            this.checkEditFlipH.Name = "checkEditFlipH";
            this.checkEditFlipH.Properties.Caption = "Flip Horizontal";
            this.checkEditFlipH.Size = new System.Drawing.Size(225, 18);
            this.checkEditFlipH.TabIndex = 3;
            this.checkEditFlipH.CheckedChanged += new System.EventHandler(this.checkEditFlipH_CheckedChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(12, 105);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(91, 30);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "OK";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(146, 105);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(91, 30);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Cancel";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // FormCameraConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 152);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.checkEditFlipH);
            this.Controls.Add(this.checkEditFlipV);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.comboBoxEditCameraList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCameraConfig";
            this.Text = "Camera Config";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCameraList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFlipV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFlipH.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditCameraList;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit checkEditFlipV;
        private DevExpress.XtraEditors.CheckEdit checkEditFlipH;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}