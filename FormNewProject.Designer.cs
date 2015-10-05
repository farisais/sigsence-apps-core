namespace DSA_Teaser
{
    partial class FormNewProject
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroupProjectType = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditFileName = new DevExpress.XtraEditors.TextEdit();
            this.labelControlLocation = new DevExpress.XtraEditors.LabelControl();
            this.textEditPath = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.saveFileNewProject = new System.Windows.Forms.SaveFileDialog();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupProjectType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(310, 231);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(104, 25);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "OK";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(440, 231);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(104, 25);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "Cancel";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(26, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Project Type";
            // 
            // radioGroupProjectType
            // 
            this.radioGroupProjectType.Location = new System.Drawing.Point(26, 52);
            this.radioGroupProjectType.Name = "radioGroupProjectType";
            this.radioGroupProjectType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupProjectType.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupProjectType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Empty Project"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "DAQ Assistance"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Recording Assistance")});
            this.radioGroupProjectType.Size = new System.Drawing.Size(144, 108);
            this.radioGroupProjectType.TabIndex = 7;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(214, 52);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(27, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Name";
            // 
            // textEditFileName
            // 
            this.textEditFileName.Location = new System.Drawing.Point(214, 71);
            this.textEditFileName.Name = "textEditFileName";
            this.textEditFileName.Size = new System.Drawing.Size(334, 20);
            this.textEditFileName.TabIndex = 9;
            // 
            // labelControlLocation
            // 
            this.labelControlLocation.Location = new System.Drawing.Point(214, 106);
            this.labelControlLocation.Name = "labelControlLocation";
            this.labelControlLocation.Size = new System.Drawing.Size(40, 13);
            this.labelControlLocation.TabIndex = 10;
            this.labelControlLocation.Text = "Location";
            // 
            // textEditPath
            // 
            this.textEditPath.Location = new System.Drawing.Point(214, 125);
            this.textEditPath.Name = "textEditPath";
            this.textEditPath.Size = new System.Drawing.Size(334, 20);
            this.textEditPath.TabIndex = 11;
            // 
            // simpleButtonBrowse
            // 
            this.simpleButtonBrowse.Location = new System.Drawing.Point(466, 151);
            this.simpleButtonBrowse.Name = "simpleButtonBrowse";
            this.simpleButtonBrowse.Size = new System.Drawing.Size(82, 25);
            this.simpleButtonBrowse.TabIndex = 12;
            this.simpleButtonBrowse.Text = "Browse";
            this.simpleButtonBrowse.Click += new System.EventHandler(this.simpleButtonBrowse_Click);
            // 
            // saveFileNewProject
            // 
            this.saveFileNewProject.FileName = "SigsenceProject";
            this.saveFileNewProject.Filter = "Sigsence Solution File (*.sgs)|*.sgs|All files (*.*)|*.*";
            this.saveFileNewProject.InitialDirectory = "C:\\Users\\faris\\Documents\\Sigsence DeskApp V.1.0\\Projects";
            this.saveFileNewProject.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileNewProject_FileOk);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(560, 271);
            this.shapeContainer1.TabIndex = 13;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 193;
            this.lineShape2.X2 = 193;
            this.lineShape2.Y1 = 176;
            this.lineShape2.Y2 = 48;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 28;
            this.lineShape1.X2 = 541;
            this.lineShape1.Y1 = 218;
            this.lineShape1.Y2 = 218;
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(212, 158);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Create Directory for this Project";
            this.checkEdit1.Size = new System.Drawing.Size(181, 18);
            this.checkEdit1.TabIndex = 14;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // FormNewProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 271);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.simpleButtonBrowse);
            this.Controls.Add(this.textEditPath);
            this.Controls.Add(this.labelControlLocation);
            this.Controls.Add(this.textEditFileName);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.radioGroupProjectType);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNewProject";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Project";
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupProjectType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroupProjectType;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditFileName;
        private DevExpress.XtraEditors.LabelControl labelControlLocation;
        private DevExpress.XtraEditors.TextEdit textEditPath;
        private DevExpress.XtraEditors.SimpleButton simpleButtonBrowse;
        private System.Windows.Forms.SaveFileDialog saveFileNewProject;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
    }
}