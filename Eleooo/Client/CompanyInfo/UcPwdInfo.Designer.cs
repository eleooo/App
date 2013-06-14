namespace Eleooo.Client
{
    partial class UcPwdInfo
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose( );
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent( )
        {
            this.lblPwd = new DevComponents.DotNetBar.LabelX( );
            this.txtPwd = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.txtPwd1 = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.lblPwd1 = new DevComponents.DotNetBar.LabelX( );
            this.txtPwd2 = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.lblPwd2 = new DevComponents.DotNetBar.LabelX( );
            this.btnSave = new DevComponents.DotNetBar.ButtonX( );
            this.btnCancle = new DevComponents.DotNetBar.ButtonX( );
            this.mainContainer = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.plTitle.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // lblPwd
            // 
            // 
            // 
            // 
            this.lblPwd.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPwd.Location = new System.Drawing.Point(84, 83);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(75, 23);
            this.lblPwd.TabIndex = 0;
            this.lblPwd.Text = "原始密码:";
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPwd.Border.Class = "TextBoxBorder";
            this.txtPwd.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPwd.FocusHighlightColor = System.Drawing.Color.LightSeaGreen;
            this.txtPwd.ForeColor = System.Drawing.Color.Black;
            this.txtPwd.Location = new System.Drawing.Point(166, 83);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(199, 21);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.UseSystemPasswordChar = true;
            // 
            // txtPwd1
            // 
            this.txtPwd1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPwd1.Border.Class = "TextBoxBorder";
            this.txtPwd1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPwd1.FocusHighlightColor = System.Drawing.Color.LightSeaGreen;
            this.txtPwd1.ForeColor = System.Drawing.Color.Black;
            this.txtPwd1.Location = new System.Drawing.Point(166, 123);
            this.txtPwd1.Name = "txtPwd1";
            this.txtPwd1.Size = new System.Drawing.Size(199, 21);
            this.txtPwd1.TabIndex = 3;
            this.txtPwd1.UseSystemPasswordChar = true;
            // 
            // lblPwd1
            // 
            // 
            // 
            // 
            this.lblPwd1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPwd1.Location = new System.Drawing.Point(84, 123);
            this.lblPwd1.Name = "lblPwd1";
            this.lblPwd1.Size = new System.Drawing.Size(75, 23);
            this.lblPwd1.TabIndex = 2;
            this.lblPwd1.Text = "新 密 码:";
            // 
            // txtPwd2
            // 
            this.txtPwd2.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPwd2.Border.Class = "TextBoxBorder";
            this.txtPwd2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPwd2.FocusHighlightColor = System.Drawing.Color.LightSeaGreen;
            this.txtPwd2.ForeColor = System.Drawing.Color.Black;
            this.txtPwd2.Location = new System.Drawing.Point(166, 162);
            this.txtPwd2.Name = "txtPwd2";
            this.txtPwd2.Size = new System.Drawing.Size(199, 21);
            this.txtPwd2.TabIndex = 5;
            this.txtPwd2.UseSystemPasswordChar = true;
            // 
            // lblPwd2
            // 
            // 
            // 
            // 
            this.lblPwd2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPwd2.Location = new System.Drawing.Point(84, 162);
            this.lblPwd2.Name = "lblPwd2";
            this.lblPwd2.Size = new System.Drawing.Size(75, 23);
            this.lblPwd2.TabIndex = 4;
            this.lblPwd2.Text = "确认密码:";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.MagentaWithBackground;
            this.btnSave.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Image = global::Eleooo.Client.MetroIcon.CheckMark;
            this.btnSave.Location = new System.Drawing.Point(121, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 35);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.ColorTable = DevComponents.DotNetBar.eButtonColor.MagentaWithBackground;
            this.btnCancle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancle.Image = global::Eleooo.Client.MetroIcon.Delete;
            this.btnCancle.Location = new System.Drawing.Point(274, 210);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(91, 35);
            this.btnCancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancle.TabIndex = 16;
            this.btnCancle.Text = "取消(&C)";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.btnCancle);
            this.mainContainer.Controls.Add(this.btnSave);
            this.mainContainer.Controls.Add(this.txtPwd2);
            this.mainContainer.Controls.Add(this.lblPwd2);
            this.mainContainer.Controls.Add(this.txtPwd1);
            this.mainContainer.Controls.Add(this.lblPwd1);
            this.mainContainer.Controls.Add(this.txtPwd);
            this.mainContainer.Controls.Add(this.lblPwd);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Size = new System.Drawing.Size(497, 356);
            // 
            // UcPwdInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainContainer);
            this.Name = "UcPwdInfo";
            this.Size = new System.Drawing.Size(497, 356);
            this.Controls.SetChildIndex(this.plTitle, 0);
            this.Controls.SetChildIndex(this.mainContainer, 0);
            this.plTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txtPwd2;
        private DevComponents.DotNetBar.LabelX lblPwd2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPwd1;
        private DevComponents.DotNetBar.LabelX lblPwd1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPwd;
        private DevComponents.DotNetBar.LabelX lblPwd;
        private DevComponents.DotNetBar.ButtonX btnCancle;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.Controls.SlidePanel mainContainer;
    }
}
