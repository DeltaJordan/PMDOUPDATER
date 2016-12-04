namespace PMDO_Launcher
{
    partial class InstallerForm1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallerForm1));
            this.btnNext1 = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rtbTerms = new System.Windows.Forms.RichTextBox();
            this.btnNext2 = new System.Windows.Forms.Button();
            this.txtInstallLoc = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblSelect = new System.Windows.Forms.Label();
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.lblLoading = new System.Windows.Forms.Label();
            this.btnFinish = new System.Windows.Forms.Button();
            this.chbxShortcut = new System.Windows.Forms.CheckBox();
            this.chbxLaunch = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNext1
            // 
            this.btnNext1.Location = new System.Drawing.Point(457, 361);
            this.btnNext1.Name = "btnNext1";
            this.btnNext1.Size = new System.Drawing.Size(106, 23);
            this.btnNext1.TabIndex = 0;
            this.btnNext1.Text = "Next";
            this.btnNext1.UseVisualStyleBackColor = true;
            this.btnNext1.Click += new System.EventHandler(this.btnNext1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(35, 361);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rtbTerms
            // 
            this.rtbTerms.Location = new System.Drawing.Point(12, 28);
            this.rtbTerms.Name = "rtbTerms";
            this.rtbTerms.Size = new System.Drawing.Size(551, 270);
            this.rtbTerms.TabIndex = 1;
            this.rtbTerms.Text = resources.GetString("rtbTerms.Text");
            this.rtbTerms.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // btnNext2
            // 
            this.btnNext2.Location = new System.Drawing.Point(457, 361);
            this.btnNext2.Name = "btnNext2";
            this.btnNext2.Size = new System.Drawing.Size(106, 23);
            this.btnNext2.TabIndex = 0;
            this.btnNext2.Text = "Next";
            this.btnNext2.UseVisualStyleBackColor = true;
            this.btnNext2.Visible = false;
            this.btnNext2.Click += new System.EventHandler(this.btnNext1_Click);
            // 
            // txtInstallLoc
            // 
            this.txtInstallLoc.Location = new System.Drawing.Point(35, 175);
            this.txtInstallLoc.Name = "txtInstallLoc";
            this.txtInstallLoc.ReadOnly = true;
            this.txtInstallLoc.Size = new System.Drawing.Size(403, 20);
            this.txtInstallLoc.TabIndex = 2;
            this.txtInstallLoc.Visible = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(457, 175);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(106, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Visible = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnNext1_Click);
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Location = new System.Drawing.Point(32, 65);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(488, 26);
            this.lblSelect.TabIndex = 3;
            this.lblSelect.Text = "Please select where you would like to install PMDO.  \r\nPlease take note of this l" +
    "ocation in case you want to find your screenshots and change the GUI\'s skin.";
            this.lblSelect.Visible = false;
            // 
            // pbLoading
            // 
            this.pbLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLoading.Image = global::PMDO_Launcher.Properties.Resources.giphy;
            this.pbLoading.Location = new System.Drawing.Point(155, 12);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(250, 272);
            this.pbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLoading.TabIndex = 5;
            this.pbLoading.TabStop = false;
            this.pbLoading.Visible = false;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoading.Location = new System.Drawing.Point(205, 287);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(152, 33);
            this.lblLoading.TabIndex = 6;
            this.lblLoading.Text = "Loading...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoading.Visible = false;
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(457, 361);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(106, 23);
            this.btnFinish.TabIndex = 0;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Visible = false;
            this.btnFinish.Click += new System.EventHandler(this.btnNext1_Click);
            // 
            // chbxShortcut
            // 
            this.chbxShortcut.AutoSize = true;
            this.chbxShortcut.Checked = true;
            this.chbxShortcut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbxShortcut.Location = new System.Drawing.Point(35, 65);
            this.chbxShortcut.Name = "chbxShortcut";
            this.chbxShortcut.Size = new System.Drawing.Size(143, 17);
            this.chbxShortcut.TabIndex = 7;
            this.chbxShortcut.Text = "Add Shortcut to Desktop";
            this.chbxShortcut.UseVisualStyleBackColor = true;
            this.chbxShortcut.Visible = false;
            // 
            // chbxLaunch
            // 
            this.chbxLaunch.AutoSize = true;
            this.chbxLaunch.Checked = true;
            this.chbxLaunch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbxLaunch.Location = new System.Drawing.Point(35, 94);
            this.chbxLaunch.Name = "chbxLaunch";
            this.chbxLaunch.Size = new System.Drawing.Size(229, 17);
            this.chbxLaunch.TabIndex = 7;
            this.chbxLaunch.Text = "Launch Pokémon Mystery Dungeon Online";
            this.chbxLaunch.UseVisualStyleBackColor = true;
            this.chbxLaunch.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(35, 323);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(528, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 8;
            // 
            // InstallerForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 402);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.chbxLaunch);
            this.Controls.Add(this.chbxShortcut);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.pbLoading);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.txtInstallLoc);
            this.Controls.Add(this.rtbTerms);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnNext2);
            this.Controls.Add(this.btnNext1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "InstallerForm1";
            this.Text = "PMDO Installer";
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNext1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RichTextBox rtbTerms;
        private System.Windows.Forms.Button btnNext2;
        private System.Windows.Forms.TextBox txtInstallLoc;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.PictureBox pbLoading;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.CheckBox chbxShortcut;
        private System.Windows.Forms.CheckBox chbxLaunch;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

