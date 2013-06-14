namespace Eleooo.Client
{
    partial class UcOrder
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
            this.tabOrder = new DevComponents.DotNetBar.SuperTabControl( );
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel( );
            this.ucCompanyItemOrder = new Eleooo.Client.UcCompanyItemOrder( );
            this.tabCompanyItem = new DevComponents.DotNetBar.SuperTabItem( );
            this.PnlMemberOrder = new DevComponents.DotNetBar.SuperTabControlPanel( );
            this.ucMemberOrder = new Eleooo.Client.UcMemberOrder( );
            this.MemberOrder = new DevComponents.DotNetBar.SuperTabItem( );
            this.pnlNonMemberOrder = new DevComponents.DotNetBar.SuperTabControlPanel( );
            this.ucNonMemberOrder = new Eleooo.Client.UcNonMemberOrder( );
            this.NonMemberOrder = new DevComponents.DotNetBar.SuperTabItem( );
            this.plTitle.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.tabOrder)).BeginInit( );
            this.tabOrder.SuspendLayout( );
            this.superTabControlPanel1.SuspendLayout( );
            this.PnlMemberOrder.SuspendLayout( );
            this.pnlNonMemberOrder.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // tabOrder
            // 
            this.tabOrder.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tabOrder.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.tabOrder.ControlBox.MenuBox.Name = "";
            this.tabOrder.ControlBox.Name = "";
            this.tabOrder.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabOrder.ControlBox.MenuBox,
            this.tabOrder.ControlBox.CloseBox});
            this.tabOrder.Controls.Add(this.PnlMemberOrder);
            this.tabOrder.Controls.Add(this.superTabControlPanel1);
            this.tabOrder.Controls.Add(this.pnlNonMemberOrder);
            this.tabOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOrder.FixedTabSize = new System.Drawing.Size(100, 30);
            this.tabOrder.ForeColor = System.Drawing.Color.Black;
            this.tabOrder.Location = new System.Drawing.Point(0, 0);
            this.tabOrder.Name = "tabOrder";
            this.tabOrder.ReorderTabsEnabled = true;
            this.tabOrder.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabOrder.SelectedTabIndex = 0;
            this.tabOrder.Size = new System.Drawing.Size(627, 481);
            this.tabOrder.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabOrder.TabIndex = 0;
            this.tabOrder.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.MemberOrder,
            this.tabCompanyItem,
            this.NonMemberOrder});
            this.tabOrder.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
            this.tabOrder.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            this.tabOrder.SelectedTabChanged += new System.EventHandler<DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs>(this.tabOrder_SelectedTabChanged);
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Controls.Add(this.ucCompanyItemOrder);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 0);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(627, 481);
            this.superTabControlPanel1.TabIndex = 0;
            this.superTabControlPanel1.TabItem = this.tabCompanyItem;
            // 
            // ucCompanyItemOrder
            // 
            this.ucCompanyItemOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCompanyItemOrder.Location = new System.Drawing.Point(0, 0);
            this.ucCompanyItemOrder.Name = "ucCompanyItemOrder";
            this.ucCompanyItemOrder.Size = new System.Drawing.Size(627, 481);
            this.ucCompanyItemOrder.TabIndex = 0;
            this.ucCompanyItemOrder.UsesBlockingAnimation = false;
            // 
            // tabCompanyItem
            // 
            this.tabCompanyItem.AttachedControl = this.superTabControlPanel1;
            this.tabCompanyItem.GlobalItem = false;
            this.tabCompanyItem.Name = "tabCompanyItem";
            this.tabCompanyItem.Text = "抢优惠消费";
            // 
            // PnlMemberOrder
            // 
            this.PnlMemberOrder.Controls.Add(this.ucMemberOrder);
            this.PnlMemberOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMemberOrder.Location = new System.Drawing.Point(0, 32);
            this.PnlMemberOrder.Name = "PnlMemberOrder";
            this.PnlMemberOrder.Size = new System.Drawing.Size(627, 449);
            this.PnlMemberOrder.TabIndex = 1;
            this.PnlMemberOrder.TabItem = this.MemberOrder;
            // 
            // ucMemberOrder
            // 
            this.ucMemberOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMemberOrder.Location = new System.Drawing.Point(0, 0);
            this.ucMemberOrder.Name = "ucMemberOrder";
            this.ucMemberOrder.Size = new System.Drawing.Size(627, 449);
            this.ucMemberOrder.TabIndex = 0;
            this.ucMemberOrder.UsesBlockingAnimation = false;
            // 
            // MemberOrder
            // 
            this.MemberOrder.AttachedControl = this.PnlMemberOrder;
            this.MemberOrder.GlobalItem = false;
            this.MemberOrder.Name = "MemberOrder";
            this.MemberOrder.Text = "会员消费";
            // 
            // pnlNonMemberOrder
            // 
            this.pnlNonMemberOrder.Controls.Add(this.ucNonMemberOrder);
            this.pnlNonMemberOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNonMemberOrder.Location = new System.Drawing.Point(0, 0);
            this.pnlNonMemberOrder.Name = "pnlNonMemberOrder";
            this.pnlNonMemberOrder.Size = new System.Drawing.Size(627, 481);
            this.pnlNonMemberOrder.TabIndex = 0;
            this.pnlNonMemberOrder.TabItem = this.NonMemberOrder;
            // 
            // ucNonMemberOrder
            // 
            this.ucNonMemberOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucNonMemberOrder.Location = new System.Drawing.Point(0, 0);
            this.ucNonMemberOrder.Name = "ucNonMemberOrder";
            this.ucNonMemberOrder.Size = new System.Drawing.Size(627, 481);
            this.ucNonMemberOrder.TabIndex = 0;
            this.ucNonMemberOrder.UsesBlockingAnimation = false;
            // 
            // NonMemberOrder
            // 
            this.NonMemberOrder.AttachedControl = this.pnlNonMemberOrder;
            this.NonMemberOrder.GlobalItem = false;
            this.NonMemberOrder.Name = "NonMemberOrder";
            this.NonMemberOrder.Text = "非会员消费";
            // 
            // UcOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabOrder);
            this.Name = "UcOrder";
            this.Size = new System.Drawing.Size(627, 481);
            this.Text = "会员消费";
            this.Load += new System.EventHandler(this.UcOrder_Load);
            this.Controls.SetChildIndex(this.plTitle, 0);
            this.Controls.SetChildIndex(this.tabOrder, 0);
            this.plTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabOrder)).EndInit( );
            this.tabOrder.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            this.PnlMemberOrder.ResumeLayout(false);
            this.pnlNonMemberOrder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTabControl tabOrder;
        private DevComponents.DotNetBar.SuperTabControlPanel PnlMemberOrder;
        private DevComponents.DotNetBar.SuperTabItem MemberOrder;
        private DevComponents.DotNetBar.SuperTabControlPanel pnlNonMemberOrder;
        private DevComponents.DotNetBar.SuperTabItem NonMemberOrder;
        private UcNonMemberOrder ucNonMemberOrder;
        private UcMemberOrder ucMemberOrder;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem tabCompanyItem;
        private UcCompanyItemOrder ucCompanyItemOrder;
    }
}
