﻿namespace STelecom.Forms
{
    partial class StaffTabulationsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.picbRefresh = new System.Windows.Forms.PictureBox();
            this.cmbDateTimeInput = new System.Windows.Forms.ComboBox();
            this.btnSaveExcel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txB_timeCount = new System.Windows.Forms.TextBox();
            this.txB_dateTimeExit = new System.Windows.Forms.TextBox();
            this.txB_dateTimeInput = new System.Windows.Forms.TextBox();
            this.txB_user = new System.Windows.Forms.TextBox();
            this.txB_id = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picbRefresh)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // picbRefresh
            // 
            this.picbRefresh.BackColor = System.Drawing.Color.Transparent;
            this.picbRefresh.BackgroundImage = global::STelecom.Properties.Resources.icons8_синхронизация_подключения_32;
            this.picbRefresh.Location = new System.Drawing.Point(775, 14);
            this.picbRefresh.Name = "picbRefresh";
            this.picbRefresh.Size = new System.Drawing.Size(33, 30);
            this.picbRefresh.TabIndex = 108;
            this.picbRefresh.TabStop = false;
            // 
            // cmbDateTimeInput
            // 
            this.cmbDateTimeInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateTimeInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDateTimeInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbDateTimeInput.FormattingEnabled = true;
            this.cmbDateTimeInput.Items.AddRange(new object[] {
            "Модель",
            "Неисправность",
            "Автор",
            "Описание неисправности"});
            this.cmbDateTimeInput.Location = new System.Drawing.Point(12, 16);
            this.cmbDateTimeInput.Name = "cmbDateTimeInput";
            this.cmbDateTimeInput.Size = new System.Drawing.Size(228, 28);
            this.cmbDateTimeInput.TabIndex = 107;
            // 
            // btnSaveExcel
            // 
            this.btnSaveExcel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSaveExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveExcel.Location = new System.Drawing.Point(246, 16);
            this.btnSaveExcel.Name = "btnSaveExcel";
            this.btnSaveExcel.Size = new System.Drawing.Size(151, 28);
            this.btnSaveExcel.TabIndex = 106;
            this.btnSaveExcel.Text = "Сохранить";
            this.btnSaveExcel.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.txB_timeCount);
            this.panel1.Controls.Add(this.txB_dateTimeExit);
            this.panel1.Controls.Add(this.txB_dateTimeInput);
            this.panel1.Controls.Add(this.txB_user);
            this.panel1.Controls.Add(this.txB_id);
            this.panel1.Location = new System.Drawing.Point(728, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(41, 35);
            this.panel1.TabIndex = 104;
            this.panel1.Visible = false;
            // 
            // txB_timeCount
            // 
            this.txB_timeCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_timeCount.Location = new System.Drawing.Point(147, 3);
            this.txB_timeCount.MaxLength = 49;
            this.txB_timeCount.Multiline = true;
            this.txB_timeCount.Name = "txB_timeCount";
            this.txB_timeCount.Size = new System.Drawing.Size(33, 28);
            this.txB_timeCount.TabIndex = 101;
            // 
            // txB_dateTimeExit
            // 
            this.txB_dateTimeExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_dateTimeExit.Location = new System.Drawing.Point(108, 3);
            this.txB_dateTimeExit.MaxLength = 49;
            this.txB_dateTimeExit.Multiline = true;
            this.txB_dateTimeExit.Name = "txB_dateTimeExit";
            this.txB_dateTimeExit.Size = new System.Drawing.Size(33, 28);
            this.txB_dateTimeExit.TabIndex = 100;
            // 
            // txB_dateTimeInput
            // 
            this.txB_dateTimeInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_dateTimeInput.Location = new System.Drawing.Point(72, 3);
            this.txB_dateTimeInput.MaxLength = 49;
            this.txB_dateTimeInput.Multiline = true;
            this.txB_dateTimeInput.Name = "txB_dateTimeInput";
            this.txB_dateTimeInput.Size = new System.Drawing.Size(30, 28);
            this.txB_dateTimeInput.TabIndex = 99;
            // 
            // txB_user
            // 
            this.txB_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_user.Location = new System.Drawing.Point(36, 3);
            this.txB_user.MaxLength = 49;
            this.txB_user.Multiline = true;
            this.txB_user.Name = "txB_user";
            this.txB_user.Size = new System.Drawing.Size(30, 28);
            this.txB_user.TabIndex = 98;
            // 
            // txB_id
            // 
            this.txB_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_id.Location = new System.Drawing.Point(3, 3);
            this.txB_id.MaxLength = 49;
            this.txB_id.Multiline = true;
            this.txB_id.Name = "txB_id";
            this.txB_id.Size = new System.Drawing.Size(27, 28);
            this.txB_id.TabIndex = 97;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(12, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.Size = new System.Drawing.Size(796, 505);
            this.dataGridView1.TabIndex = 103;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDelete.Location = new System.Drawing.Point(403, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(151, 28);
            this.btnDelete.TabIndex = 109;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // StaffTabulationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::STelecom.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(820, 565);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.picbRefresh);
            this.Controls.Add(this.cmbDateTimeInput);
            this.Controls.Add(this.btnSaveExcel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(836, 604);
            this.MinimumSize = new System.Drawing.Size(836, 604);
            this.Name = "StaffTabulationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Табель";
            ((System.ComponentModel.ISupportInitialize)(this.picbRefresh)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picbRefresh;
        private System.Windows.Forms.ComboBox cmbDateTimeInput;
        private System.Windows.Forms.Button btnSaveExcel;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.TextBox txB_timeCount;
        internal System.Windows.Forms.TextBox txB_dateTimeExit;
        internal System.Windows.Forms.TextBox txB_dateTimeInput;
        internal System.Windows.Forms.TextBox txB_user;
        internal System.Windows.Forms.TextBox txB_id;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDelete;
    }
}