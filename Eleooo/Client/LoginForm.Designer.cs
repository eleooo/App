namespace Eleooo.Client
{
    partial class LoginForm
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
                components.Dispose( );
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.txtPassword = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.btnLogin = new DevComponents.DotNetBar.ButtonX( );
            this.btnClose = new DevComponents.DotNetBar.ButtonX( );
            this.lblMessage = new DevComponents.DotNetBar.LabelX( );
            this.lblVer = new DevComponents.DotNetBar.LabelX( );
            this.lblClose = new DevComponents.DotNetBar.LabelX( );
            this.SuspendLayout( );
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(241, 90);
            this.txtName.MaxLength = 11;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(154, 21);
            this.txtName.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPassword.Border.Class = "TextBoxBorder";
            this.txtPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(242, 128);
            this.txtPassword.MaxLength = 6;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(154, 21);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLogin.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnLogin.Image = global::Eleooo.Client.Properties.Resources.login;
            this.btnLogin.ImageFixedSize = new System.Drawing.Size(75, 26);
            this.btnLogin.Location = new System.Drawing.Point(183, 165);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.btnLogin.Size = new System.Drawing.Size(88, 28);
            this.btnLogin.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = global::Eleooo.Client.Properties.Resources.login_out;
            this.btnClose.ImageFixedSize = new System.Drawing.Size(75, 26);
            this.btnClose.Location = new System.Drawing.Point(281, 165);
            this.btnClose.Name = "btnClose";
            this.btnClose.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.btnClose.Size = new System.Drawing.Size(88, 28);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.btnClose.TabIndex = 3;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMessage.ForeColor = System.Drawing.Color.Crimson;
            this.lblMessage.Location = new System.Drawing.Point(165, 228);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(244, 23);
            this.lblMessage.TabIndex = 4;
            // 
            // lblVer
            // 
            this.lblVer.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblVer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblVer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVer.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblVer.Location = new System.Drawing.Point(12, 228);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(142, 23);
            this.lblVer.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.lblVer.TabIndex = 5;
            this.lblVer.Text = "<font color=\"#1F497D\"><b>Ver</b></font><font color=\"#ED1C24\"> 1.0.0.1 Build1000</" +
                "font>";
            // 
            // lblClose
            // 
            this.lblClose.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblClose.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Location = new System.Drawing.Point(385, 8);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(20, 23);
            this.lblClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.lblClose.TabIndex = 6;
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Eleooo.Client.Properties.Resources.LoginImage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(415, 263);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.lblVer);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPassword;
        private DevComponents.DotNetBar.ButtonX btnLogin;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.LabelX lblMessage;
        private DevComponents.DotNetBar.LabelX lblVer;
        private DevComponents.DotNetBar.LabelX lblClose;
    }
}