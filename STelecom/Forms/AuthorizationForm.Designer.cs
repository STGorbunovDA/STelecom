namespace STelecom
{
    partial class AuthorizationForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.clearControlForm = new System.Windows.Forms.Label();
            this.hidePassword = new System.Windows.Forms.Label();
            this.openPassword = new System.Windows.Forms.Label();
            this.btnAuthorization = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.txbLogin = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AuthorizationLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // clearControlForm
            // 
            this.clearControlForm.AutoSize = true;
            this.clearControlForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.clearControlForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clearControlForm.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearControlForm.Location = new System.Drawing.Point(376, 301);
            this.clearControlForm.Name = "clearControlForm";
            this.clearControlForm.Size = new System.Drawing.Size(63, 14);
            this.clearControlForm.TabIndex = 39;
            this.clearControlForm.Text = "очистить";
            this.clearControlForm.Click += new System.EventHandler(this.ClearControlForm_Click);
            this.clearControlForm.MouseEnter += new System.EventHandler(this.ClearControlForm_MouseEnter);
            this.clearControlForm.MouseLeave += new System.EventHandler(this.ClearControlForm_MouseLeave);
            // 
            // hidePassword
            // 
            this.hidePassword.AutoSize = true;
            this.hidePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.hidePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hidePassword.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hidePassword.Location = new System.Drawing.Point(511, 301);
            this.hidePassword.Name = "hidePassword";
            this.hidePassword.Size = new System.Drawing.Size(98, 14);
            this.hidePassword.TabIndex = 38;
            this.hidePassword.Text = "скрыть пароль";
            this.hidePassword.Click += new System.EventHandler(this.HidePassword_Click);
            this.hidePassword.MouseEnter += new System.EventHandler(this.HidePassword_MouseEnter);
            this.hidePassword.MouseLeave += new System.EventHandler(this.HidePassword_MouseLeave);
            // 
            // openPassword
            // 
            this.openPassword.AutoSize = true;
            this.openPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.openPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openPassword.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openPassword.Location = new System.Drawing.Point(497, 301);
            this.openPassword.Name = "openPassword";
            this.openPassword.Size = new System.Drawing.Size(112, 14);
            this.openPassword.TabIndex = 37;
            this.openPassword.Text = "показать пароль";
            this.openPassword.Click += new System.EventHandler(this.OpenPassword_Click);
            this.openPassword.MouseEnter += new System.EventHandler(this.OpenPassword_MouseEnter);
            this.openPassword.MouseLeave += new System.EventHandler(this.OpenPassword_MouseLeave);
            // 
            // btnAuthorization
            // 
            this.btnAuthorization.BackColor = System.Drawing.Color.White;
            this.btnAuthorization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuthorization.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAuthorization.ForeColor = System.Drawing.Color.Black;
            this.btnAuthorization.Location = new System.Drawing.Point(330, 333);
            this.btnAuthorization.Name = "btnAuthorization";
            this.btnAuthorization.Size = new System.Drawing.Size(160, 48);
            this.btnAuthorization.TabIndex = 35;
            this.btnAuthorization.Text = "Enter";
            this.btnAuthorization.UseVisualStyleBackColor = false;
            this.btnAuthorization.Click += new System.EventHandler(this.BtnAuthorization_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.label4.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(511, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 22);
            this.label4.TabIndex = 34;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.label3.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(207, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 22);
            this.label3.TabIndex = 33;
            this.label3.Text = "Login";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::STelecom.Properties.Resources.lock_64;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(615, 232);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(63, 66);
            this.pictureBox2.TabIndex = 32;
            this.pictureBox2.TabStop = false;
            // 
            // txbPassword
            // 
            this.txbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txbPassword.Location = new System.Drawing.Point(411, 267);
            this.txbPassword.Multiline = true;
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.Size = new System.Drawing.Size(198, 31);
            this.txbPassword.TabIndex = 36;
            // 
            // txbLogin
            // 
            this.txbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txbLogin.Location = new System.Drawing.Point(207, 267);
            this.txbLogin.Multiline = true;
            this.txbLogin.Name = "txbLogin";
            this.txbLogin.Size = new System.Drawing.Size(198, 31);
            this.txbLogin.TabIndex = 31;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::STelecom.Properties.Resources.user_64;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(143, 237);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(58, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // AuthorizationLabel
            // 
            this.AuthorizationLabel.AutoSize = true;
            this.AuthorizationLabel.BackColor = System.Drawing.Color.Transparent;
            this.AuthorizationLabel.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AuthorizationLabel.Location = new System.Drawing.Point(277, 158);
            this.AuthorizationLabel.Name = "AuthorizationLabel";
            this.AuthorizationLabel.Size = new System.Drawing.Size(260, 41);
            this.AuthorizationLabel.TabIndex = 29;
            this.AuthorizationLabel.Text = "Авторизация";
            this.AuthorizationLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::STelecom.Properties.Resources.Untitled_2;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.clearControlForm);
            this.Controls.Add(this.hidePassword);
            this.Controls.Add(this.openPassword);
            this.Controls.Add(this.btnAuthorization);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.txbPassword);
            this.Controls.Add(this.txbLogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.AuthorizationLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "AuthorizationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.AuthorizationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label clearControlForm;
        private System.Windows.Forms.Label hidePassword;
        private System.Windows.Forms.Label openPassword;
        private System.Windows.Forms.Button btnAuthorization;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.TextBox txbLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label AuthorizationLabel;
    }
}

