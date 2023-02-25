namespace STelecom.Forms
{
    partial class ChangeToProblemRadiostantionForm
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
            this.txbId = new System.Windows.Forms.TextBox();
            this.chbProblemEnable = new System.Windows.Forms.CheckBox();
            this.cmbProblem = new System.Windows.Forms.ComboBox();
            this.btnChageRadiostantionProblem = new System.Windows.Forms.Button();
            this.txbActions = new System.Windows.Forms.TextBox();
            this.txbInfo = new System.Windows.Forms.TextBox();
            this.cmbModel = new System.Windows.Forms.ComboBox();
            this.picbClearControl = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txbProblem = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picbClearControl)).BeginInit();
            this.SuspendLayout();
            // 
            // txbId
            // 
            this.txbId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txbId.Location = new System.Drawing.Point(494, 19);
            this.txbId.Multiline = true;
            this.txbId.Name = "txbId";
            this.txbId.Size = new System.Drawing.Size(57, 24);
            this.txbId.TabIndex = 132;
            this.txbId.Visible = false;
            // 
            // chbProblemEnable
            // 
            this.chbProblemEnable.AutoSize = true;
            this.chbProblemEnable.BackColor = System.Drawing.Color.Transparent;
            this.chbProblemEnable.Checked = true;
            this.chbProblemEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbProblemEnable.Location = new System.Drawing.Point(77, 85);
            this.chbProblemEnable.Name = "chbProblemEnable";
            this.chbProblemEnable.Size = new System.Drawing.Size(15, 14);
            this.chbProblemEnable.TabIndex = 131;
            this.chbProblemEnable.UseVisualStyleBackColor = false;
            this.chbProblemEnable.Click += new System.EventHandler(this.ChbProblemEnable_Click);
            // 
            // cmbProblem
            // 
            this.cmbProblem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProblem.Enabled = false;
            this.cmbProblem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProblem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbProblem.FormattingEnabled = true;
            this.cmbProblem.Items.AddRange(new object[] {
            "Не выключается",
            "Не включается",
            "Произвольно включается",
            "Нет приёма",
            "Нет передачи",
            "Регулятор/Переключатель",
            "Корпус"});
            this.cmbProblem.Location = new System.Drawing.Point(98, 77);
            this.cmbProblem.Name = "cmbProblem";
            this.cmbProblem.Size = new System.Drawing.Size(293, 28);
            this.cmbProblem.TabIndex = 130;
            // 
            // btnChageRadiostantionProblem
            // 
            this.btnChageRadiostantionProblem.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnChageRadiostantionProblem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChageRadiostantionProblem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnChageRadiostantionProblem.Location = new System.Drawing.Point(659, 441);
            this.btnChageRadiostantionProblem.Name = "btnChageRadiostantionProblem";
            this.btnChageRadiostantionProblem.Size = new System.Drawing.Size(119, 30);
            this.btnChageRadiostantionProblem.TabIndex = 129;
            this.btnChageRadiostantionProblem.Text = "Изменить";
            this.btnChageRadiostantionProblem.UseVisualStyleBackColor = false;
            this.btnChageRadiostantionProblem.Click += new System.EventHandler(this.BtnChageRadiostantionProblem_Click);
            // 
            // txbActions
            // 
            this.txbActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txbActions.Location = new System.Drawing.Point(14, 272);
            this.txbActions.Multiline = true;
            this.txbActions.Name = "txbActions";
            this.txbActions.Size = new System.Drawing.Size(377, 198);
            this.txbActions.TabIndex = 128;
            // 
            // txbInfo
            // 
            this.txbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txbInfo.Location = new System.Drawing.Point(409, 114);
            this.txbInfo.Multiline = true;
            this.txbInfo.Name = "txbInfo";
            this.txbInfo.Size = new System.Drawing.Size(369, 321);
            this.txbInfo.TabIndex = 127;
            // 
            // cmbModel
            // 
            this.cmbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbModel.FormattingEnabled = true;
            this.cmbModel.Location = new System.Drawing.Point(98, 16);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(293, 28);
            this.cmbModel.TabIndex = 126;
            this.cmbModel.Click += new System.EventHandler(this.CmbModel_Click);
            // 
            // picbClearControl
            // 
            this.picbClearControl.BackColor = System.Drawing.Color.Transparent;
            this.picbClearControl.BackgroundImage = global::STelecom.Properties.Resources.gui_eraser_icon_157160__1_;
            this.picbClearControl.Location = new System.Drawing.Point(757, 12);
            this.picbClearControl.Name = "picbClearControl";
            this.picbClearControl.Size = new System.Drawing.Size(33, 32);
            this.picbClearControl.TabIndex = 125;
            this.picbClearControl.TabStop = false;
            this.picbClearControl.Click += new System.EventHandler(this.PicbClearControl_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(10, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(303, 20);
            this.label3.TabIndex = 123;
            this.label3.Text = "Виды работ по устраненнию дефекта:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(10, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 122;
            this.label2.Text = "Неисп.:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(10, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 20);
            this.label11.TabIndex = 121;
            this.label11.Text = "Модель:";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.BackColor = System.Drawing.Color.Transparent;
            this.lblAuthor.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAuthor.Location = new System.Drawing.Point(557, 17);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(194, 27);
            this.lblAuthor.TabIndex = 120;
            this.lblAuthor.Text = "Горбунов Д.А.";
            // 
            // txbProblem
            // 
            this.txbProblem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txbProblem.Location = new System.Drawing.Point(14, 114);
            this.txbProblem.Multiline = true;
            this.txbProblem.Name = "txbProblem";
            this.txbProblem.Size = new System.Drawing.Size(377, 114);
            this.txbProblem.TabIndex = 119;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(501, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(202, 20);
            this.label4.TabIndex = 133;
            this.label4.Text = "Описание неисправности";
            // 
            // ChangeToProblemRadiostantionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::STelecom.Properties.Resources._999;
            this.ClientSize = new System.Drawing.Size(800, 482);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbId);
            this.Controls.Add(this.chbProblemEnable);
            this.Controls.Add(this.cmbProblem);
            this.Controls.Add(this.btnChageRadiostantionProblem);
            this.Controls.Add(this.txbActions);
            this.Controls.Add(this.txbInfo);
            this.Controls.Add(this.cmbModel);
            this.Controls.Add(this.picbClearControl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.txbProblem);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(816, 521);
            this.MinimumSize = new System.Drawing.Size(816, 521);
            this.Name = "ChangeToProblemRadiostantionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изменение неисправности радиостанции";
            this.Load += new System.EventHandler(this.ChangeToProblemRadiostantionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbClearControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txbId;
        private System.Windows.Forms.CheckBox chbProblemEnable;
        internal System.Windows.Forms.ComboBox cmbProblem;
        private System.Windows.Forms.Button btnChageRadiostantionProblem;
        internal System.Windows.Forms.TextBox txbActions;
        internal System.Windows.Forms.TextBox txbInfo;
        internal System.Windows.Forms.ComboBox cmbModel;
        private System.Windows.Forms.PictureBox picbClearControl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Label lblAuthor;
        internal System.Windows.Forms.TextBox txbProblem;
        private System.Windows.Forms.Label label4;
    }
}