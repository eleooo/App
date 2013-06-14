namespace Eleooo.Client
{
    partial class UcSystem
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
            this.tabSystem = new DevComponents.DotNetBar.SuperTabControl( );
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel( );
            this.ucCompanyInfo1 = new Eleooo.Client.UcCompanyInfo( );
            this.companyInfo = new DevComponents.DotNetBar.SuperTabItem( );
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel( );
            this.ucPwdInfo1 = new Eleooo.Client.UcPwdInfo( );
            this.pwdInfo = new DevComponents.DotNetBar.SuperTabItem( );
            this.mainContainer = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.plTitle.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.tabSystem)).BeginInit( );
            this.tabSystem.SuspendLayout( );
            this.superTabControlPanel1.SuspendLayout( );
            this.superTabControlPanel2.SuspendLayout( );
            this.mainContainer.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // tabSystem
            // 
            this.tabSystem.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tabSystem.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.tabSystem.ControlBox.MenuBox.Name = "";
            this.tabSystem.ControlBox.Name = "";
            this.tabSystem.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabSystem.ControlBox.MenuBox,
            this.tabSystem.ControlBox.CloseBox});
            this.tabSystem.Controls.Add(this.superTabControlPanel1);
            this.tabSystem.Controls.Add(this.superTabControlPanel2);
            this.tabSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSystem.ForeColor = System.Drawing.Color.Black;
            this.tabSystem.Location = new System.Drawing.Point(0, 0);
            this.tabSystem.Name = "tabSystem";
            this.tabSystem.ReorderTabsEnabled = true;
            this.tabSystem.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabSystem.SelectedTabIndex = 0;
            this.tabSystem.Size = new System.Drawing.Size(618, 471);
            this.tabSystem.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabSystem.TabHorizontalSpacing = 10;
            this.tabSystem.TabIndex = 0;
            this.tabSystem.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.companyInfo,
            this.pwdInfo});
            this.tabSystem.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
            this.tabSystem.TabVerticalSpacing = 10;
            this.tabSystem.Text = "系统管理";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Controls.Add(this.ucCompanyInfo1);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 38);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(618, 433);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.companyInfo;
            // 
            // ucCompanyInfo1
            // 
            this.ucCompanyInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCompanyInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucCompanyInfo1.Name = "ucCompanyInfo1";
            this.ucCompanyInfo1.Size = new System.Drawing.Size(618, 433);
            this.ucCompanyInfo1.TabIndex = 0;
            this.ucCompanyInfo1.UsesBlockingAnimation = false;
            // 
            // companyInfo
            // 
            this.companyInfo.AttachedControl = this.superTabControlPanel1;
            this.companyInfo.GlobalItem = false;
            this.companyInfo.Name = "companyInfo";
            this.companyInfo.Text = "修改商家资料";
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.Controls.Add(this.ucPwdInfo1);
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 0);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(618, 471);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.pwdInfo;
            // 
            // ucPwdInfo1
            // 
            this.ucPwdInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPwdInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucPwdInfo1.Name = "ucPwdInfo1";
            this.ucPwdInfo1.Size = new System.Drawing.Size(618, 471);
            this.ucPwdInfo1.TabIndex = 0;
            this.ucPwdInfo1.UsesBlockingAnimation = false;
            // 
            // pwdInfo
            // 
            this.pwdInfo.AttachedControl = this.superTabControlPanel2;
            this.pwdInfo.GlobalItem = false;
            this.pwdInfo.Name = "pwdInfo";
            this.pwdInfo.Text = "修改登录密码";
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.tabSystem);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(618, 471);
            this.mainContainer.TabIndex = 1;
            this.mainContainer.Text = "slidePanel1";
            this.mainContainer.UsesBlockingAnimation = false;
            // 
            // UcSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainContainer);
            this.Name = "UcSystem";
            this.Size = new System.Drawing.Size(618, 471);
            this.Text = "系统管理";
            this.Controls.SetChildIndex(this.plTitle, 0);
            this.Controls.SetChildIndex(this.mainContainer, 0);
            this.plTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabSystem)).EndInit( );
            this.tabSystem.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.mainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTabControl tabSystem;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem pwdInfo;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem companyInfo;
        private UcPwdInfo ucPwdInfo1;
        private DevComponents.DotNetBar.Controls.SlidePanel mainContainer;
        private UcCompanyInfo ucCompanyInfo1;
    }
}
