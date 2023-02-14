namespace STelecom.Forms
{
    partial class MenuForm
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
            this.settingAdmin = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.director = new System.Windows.Forms.Label();
            this.comparison = new System.Windows.Forms.Label();
            this.tutorialEngineers = new System.Windows.Forms.Label();
            this.sectionForeman = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.settingAdmin)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingAdmin
            // 
            this.settingAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingAdmin.BackColor = System.Drawing.Color.Transparent;
            this.settingAdmin.BackgroundImage = global::STelecom.Properties.Resources.businesssettings_thebox_theproduct_negocio_2327;
            this.settingAdmin.Location = new System.Drawing.Point(12, 12);
            this.settingAdmin.Name = "settingAdmin";
            this.settingAdmin.Size = new System.Drawing.Size(66, 69);
            this.settingAdmin.TabIndex = 4;
            this.settingAdmin.TabStop = false;
            this.settingAdmin.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.director);
            this.panel1.Controls.Add(this.settingAdmin);
            this.panel1.Controls.Add(this.comparison);
            this.panel1.Controls.Add(this.tutorialEngineers);
            this.panel1.Controls.Add(this.sectionForeman);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 461);
            this.panel1.TabIndex = 3;
            // 
            // director
            // 
            this.director.AutoSize = true;
            this.director.BackColor = System.Drawing.Color.Transparent;
            this.director.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.director.Location = new System.Drawing.Point(267, 320);
            this.director.Name = "director";
            this.director.Size = new System.Drawing.Size(282, 41);
            this.director.TabIndex = 5;
            this.director.Text = "Руководитель";
            this.director.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.director.MouseEnter += new System.EventHandler(this.Director_MouseEnter);
            this.director.MouseLeave += new System.EventHandler(this.Director_MouseLeave);
            // 
            // comparison
            // 
            this.comparison.AutoSize = true;
            this.comparison.BackColor = System.Drawing.Color.Transparent;
            this.comparison.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comparison.Location = new System.Drawing.Point(267, 236);
            this.comparison.Name = "comparison";
            this.comparison.Size = new System.Drawing.Size(282, 41);
            this.comparison.TabIndex = 3;
            this.comparison.Text = "Для куратора";
            this.comparison.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.comparison.MouseEnter += new System.EventHandler(this.Comparison_MouseEnter);
            this.comparison.MouseLeave += new System.EventHandler(this.Comparison_MouseLeave);
            // 
            // tutorialEngineers
            // 
            this.tutorialEngineers.AutoSize = true;
            this.tutorialEngineers.BackColor = System.Drawing.Color.Transparent;
            this.tutorialEngineers.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tutorialEngineers.Location = new System.Drawing.Point(303, 79);
            this.tutorialEngineers.Name = "tutorialEngineers";
            this.tutorialEngineers.Size = new System.Drawing.Size(194, 41);
            this.tutorialEngineers.TabIndex = 2;
            this.tutorialEngineers.Text = "Обучалка";
            this.tutorialEngineers.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tutorialEngineers.MouseEnter += new System.EventHandler(this.TutorialEngineers_MouseEnter);
            this.tutorialEngineers.MouseLeave += new System.EventHandler(this.TutorialEngineers_MouseLeave);
            // 
            // sectionForeman
            // 
            this.sectionForeman.AutoSize = true;
            this.sectionForeman.BackColor = System.Drawing.Color.Transparent;
            this.sectionForeman.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sectionForeman.Location = new System.Drawing.Point(352, 157);
            this.sectionForeman.Name = "sectionForeman";
            this.sectionForeman.Size = new System.Drawing.Size(106, 41);
            this.sectionForeman.TabIndex = 1;
            this.sectionForeman.Text = "База";
            this.sectionForeman.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.sectionForeman.MouseEnter += new System.EventHandler(this.SectionForeman_MouseEnter);
            this.sectionForeman.MouseLeave += new System.EventHandler(this.SectionForeman_MouseLeave);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::STelecom.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(804, 461);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(820, 500);
            this.MinimumSize = new System.Drawing.Size(820, 500);
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuForm";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.settingAdmin)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox settingAdmin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label director;
        private System.Windows.Forms.Label comparison;
        private System.Windows.Forms.Label tutorialEngineers;
        private System.Windows.Forms.Label sectionForeman;
    }
}