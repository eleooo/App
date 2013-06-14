namespace Eleooo.Client
{
    partial class UcHome
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            DevComponents.DotNetBar.SuperGrid.Style.Padding padding2 = new DevComponents.DotNetBar.SuperGrid.Style.Padding( );
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            DevComponents.DotNetBar.SuperGrid.Style.Background background3 = new DevComponents.DotNetBar.SuperGrid.Style.Background( );
            DevComponents.DotNetBar.SuperGrid.Style.BackColorBlend backColorBlend2 = new DevComponents.DotNetBar.SuperGrid.Style.BackColorBlend( );
            DevComponents.DotNetBar.SuperGrid.Style.Background background4 = new DevComponents.DotNetBar.SuperGrid.Style.Background( );
            this.plLeft = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.gridCompanyInfo = new DevComponents.DotNetBar.SuperGrid.SuperGridControl( );
            this.picCompany = new System.Windows.Forms.PictureBox( );
            this.plRight = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.plMainFunc = new DevComponents.DotNetBar.Metro.MetroTilePanel( );
            this.funcContainer = new DevComponents.DotNetBar.ItemContainer( );
            this.tileOrder = new DevComponents.DotNetBar.Metro.MetroTileItem( );
            this.tileCash = new DevComponents.DotNetBar.Metro.MetroTileItem( );
            this.tileMember = new DevComponents.DotNetBar.Metro.MetroTileItem( );
            this.tileSystem = new DevComponents.DotNetBar.Metro.MetroTileItem( );
            this.tileCompanyItem = new DevComponents.DotNetBar.Metro.MetroTileItem( );
            this.tileCompanyAds = new DevComponents.DotNetBar.Metro.MetroTileItem( );
            this.titleEleooo = new DevComponents.DotNetBar.Metro.MetroTileItem( );
            this.tileClose = new DevComponents.DotNetBar.Metro.MetroTileItem( );
            this.picLogo = new System.Windows.Forms.PictureBox( );
            this.mainContainer = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.plTitle.SuspendLayout( );
            this.plLeft.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.picCompany)).BeginInit( );
            this.plRight.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit( );
            this.mainContainer.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // plLeft
            // 
            this.plLeft.BackColor = System.Drawing.Color.White;
            this.plLeft.Controls.Add(this.gridCompanyInfo);
            this.plLeft.Controls.Add(this.picCompany);
            this.plLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.plLeft.ForeColor = System.Drawing.Color.Black;
            this.plLeft.Location = new System.Drawing.Point(0, 0);
            this.plLeft.Name = "plLeft";
            this.plLeft.Size = new System.Drawing.Size(225, 503);
            this.plLeft.TabIndex = 1;
            this.plLeft.Text = "slidePanel1";
            this.plLeft.UsesBlockingAnimation = false;
            // 
            // gridCompanyInfo
            // 
            this.gridCompanyInfo.BackColor = System.Drawing.Color.White;
            this.gridCompanyInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCompanyInfo.ForeColor = System.Drawing.Color.Black;
            this.gridCompanyInfo.Location = new System.Drawing.Point(0, 192);
            this.gridCompanyInfo.Name = "gridCompanyInfo";
            this.gridCompanyInfo.PrimaryGrid.AutoGenerateColumns = false;
            this.gridCompanyInfo.PrimaryGrid.Caption.Text = "商家信息";
            this.gridCompanyInfo.PrimaryGrid.ColumnHeader.Visible = false;
            gridColumn3.AllowSelection = false;
            gridColumn3.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            gridColumn3.CellHighlightMode = DevComponents.DotNetBar.SuperGrid.Style.CellHighlightMode.None;
            gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleRight;
            gridColumn3.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            gridColumn3.CellStyles.Default.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            padding2.Right = 5;
            gridColumn3.CellStyles.Default.Margin = padding2;
            gridColumn3.CellStyles.Default.TextColor = System.Drawing.Color.SaddleBrown;
            gridColumn3.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            gridColumn3.DataPropertyName = "Key";
            gridColumn3.EditorType = null;
            gridColumn3.Name = "Key";
            gridColumn3.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridTextBoxXEditControl);
            gridColumn3.Width = 70;
            gridColumn4.AllowSelection = false;
            gridColumn4.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            gridColumn4.CellHighlightMode = DevComponents.DotNetBar.SuperGrid.Style.CellHighlightMode.None;
            gridColumn4.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            gridColumn4.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            gridColumn4.CellStyles.Default.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            gridColumn4.CellStyles.Default.TextColor = System.Drawing.Color.OrangeRed;
            gridColumn4.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            gridColumn4.DataPropertyName = "Value";
            gridColumn4.EditorType = null;
            gridColumn4.FillWeight = 200;
            gridColumn4.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            gridColumn4.Name = "Value";
            gridColumn4.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridTextBoxXEditControl);
            gridColumn4.Width = 200;
            this.gridCompanyInfo.PrimaryGrid.Columns.Add(gridColumn3);
            this.gridCompanyInfo.PrimaryGrid.Columns.Add(gridColumn4);
            this.gridCompanyInfo.PrimaryGrid.DefaultRowHeight = 36;
            backColorBlend2.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.SkyBlue};
            background3.BackColorBlend = backColorBlend2;
            this.gridCompanyInfo.PrimaryGrid.DefaultVisualStyles.AlternateRowCellStyles.Default.Background = background3;
            background4.Color1 = System.Drawing.Color.Orange;
            this.gridCompanyInfo.PrimaryGrid.DefaultVisualStyles.CaptionStyles.Default.Background = background4;
            this.gridCompanyInfo.PrimaryGrid.DefaultVisualStyles.CaptionStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridCompanyInfo.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridCompanyInfo.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridCompanyInfo.PrimaryGrid.DefaultVisualStyles.GridPanelStyle.HorizontalLinePattern = DevComponents.DotNetBar.SuperGrid.Style.LinePattern.DashDotDot;
            this.gridCompanyInfo.PrimaryGrid.DefaultVisualStyles.GridPanelStyle.VerticalLinePattern = DevComponents.DotNetBar.SuperGrid.Style.LinePattern.DashDotDot;
            this.gridCompanyInfo.PrimaryGrid.InitialActiveRow = DevComponents.DotNetBar.SuperGrid.RelativeRow.None;
            this.gridCompanyInfo.PrimaryGrid.MultiSelect = false;
            this.gridCompanyInfo.PrimaryGrid.ReadOnly = true;
            this.gridCompanyInfo.PrimaryGrid.RowHighlightType = DevComponents.DotNetBar.SuperGrid.RowHighlightType.None;
            this.gridCompanyInfo.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.gridCompanyInfo.PrimaryGrid.ShowCellInfo = false;
            this.gridCompanyInfo.PrimaryGrid.ShowColumnHeader = false;
            this.gridCompanyInfo.PrimaryGrid.ShowRowHeaders = false;
            this.gridCompanyInfo.PrimaryGrid.SizingStyle = DevComponents.DotNetBar.SuperGrid.Style.StyleType.ReadOnlySelected;
            this.gridCompanyInfo.PrimaryGrid.Title.RowHeaderVisibility = DevComponents.DotNetBar.SuperGrid.RowHeaderVisibility.PanelControlled;
            this.gridCompanyInfo.PrimaryGrid.UseAlternateRowStyle = true;
            this.gridCompanyInfo.Size = new System.Drawing.Size(225, 311);
            this.gridCompanyInfo.SizingStyle = DevComponents.DotNetBar.SuperGrid.Style.StyleType.ReadOnlySelected;
            this.gridCompanyInfo.TabIndex = 2;
            // 
            // picCompany
            // 
            this.picCompany.Dock = System.Windows.Forms.DockStyle.Top;
            this.picCompany.Location = new System.Drawing.Point(0, 0);
            this.picCompany.Margin = new System.Windows.Forms.Padding(10);
            this.picCompany.Name = "picCompany";
            this.picCompany.Size = new System.Drawing.Size(225, 192);
            this.picCompany.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCompany.TabIndex = 1;
            this.picCompany.TabStop = false;
            // 
            // plRight
            // 
            this.plRight.BackColor = System.Drawing.Color.White;
            this.plRight.Controls.Add(this.plMainFunc);
            this.plRight.Controls.Add(this.picLogo);
            this.plRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plRight.ForeColor = System.Drawing.Color.Black;
            this.plRight.Location = new System.Drawing.Point(225, 0);
            this.plRight.Name = "plRight";
            this.plRight.Size = new System.Drawing.Size(459, 503);
            this.plRight.TabIndex = 2;
            this.plRight.Text = "slidePanel1";
            this.plRight.UsesBlockingAnimation = false;
            // 
            // plMainFunc
            // 
            this.plMainFunc.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.plMainFunc.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.plMainFunc.BackgroundStyle.BorderLeftColor = System.Drawing.Color.Orange;
            this.plMainFunc.BackgroundStyle.BorderLeftWidth = 2;
            this.plMainFunc.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.plMainFunc.BackgroundStyle.BorderTopColor = System.Drawing.Color.Orange;
            this.plMainFunc.BackgroundStyle.BorderTopWidth = 2;
            this.plMainFunc.BackgroundStyle.Class = "MetroTilePanel";
            this.plMainFunc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.plMainFunc.ContainerControlProcessDialogKey = true;
            this.plMainFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMainFunc.FitButtonsToContainerWidth = true;
            this.plMainFunc.ForeColor = System.Drawing.Color.Black;
            this.plMainFunc.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.plMainFunc.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.funcContainer});
            this.plMainFunc.Location = new System.Drawing.Point(0, 67);
            this.plMainFunc.MultiLine = true;
            this.plMainFunc.Name = "plMainFunc";
            this.plMainFunc.Size = new System.Drawing.Size(459, 436);
            this.plMainFunc.TabIndex = 4;
            // 
            // funcContainer
            // 
            // 
            // 
            // 
            this.funcContainer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.funcContainer.MultiLine = true;
            this.funcContainer.Name = "funcContainer";
            this.funcContainer.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tileOrder,
            this.tileCash,
            this.tileMember,
            this.tileSystem,
            this.tileCompanyItem,
            this.tileCompanyAds,
            this.titleEleooo,
            this.tileClose});
            // 
            // tileOrder
            // 
            this.tileOrder.AutoRotateFramesInterval = 2500;
            this.tileOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileOrder.Image = global::Eleooo.Client.Properties.Resources.OrderImage;
            this.tileOrder.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tileOrder.Name = "tileOrder";
            this.tileOrder.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            // 
            // 
            // 
            this.tileOrder.TileStyle.BackColor = System.Drawing.Color.Goldenrod;
            this.tileOrder.TileStyle.BackColor2 = System.Drawing.Color.Goldenrod;
            this.tileOrder.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tileOrder.TitleText = "会员消费";
            this.tileOrder.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.tileOrder.TitleTextFont = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // tileCash
            // 
            this.tileCash.AutoRotateFramesInterval = 2600;
            this.tileCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileCash.Image = global::Eleooo.Client.Properties.Resources.CashImage;
            this.tileCash.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tileCash.Name = "tileCash";
            this.tileCash.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            // 
            // 
            // 
            this.tileCash.TileStyle.BackColor = System.Drawing.Color.Coral;
            this.tileCash.TileStyle.BackColor2 = System.Drawing.Color.Coral;
            this.tileCash.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tileCash.TitleText = "会员储值";
            this.tileCash.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.tileCash.TitleTextFont = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // tileMember
            // 
            this.tileMember.AutoRotateFramesInterval = 2700;
            this.tileMember.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileMember.Image = global::Eleooo.Client.Properties.Resources.MemberImage;
            this.tileMember.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tileMember.Name = "tileMember";
            this.tileMember.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            // 
            // 
            // 
            this.tileMember.TileStyle.BackColor = System.Drawing.Color.HotPink;
            this.tileMember.TileStyle.BackColor2 = System.Drawing.Color.HotPink;
            this.tileMember.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tileMember.TitleText = "会员注册";
            this.tileMember.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.tileMember.TitleTextFont = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // tileSystem
            // 
            this.tileSystem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileSystem.Image = global::Eleooo.Client.Properties.Resources.SystemImage;
            this.tileSystem.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tileSystem.Name = "tileSystem";
            this.tileSystem.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            // 
            // 
            // 
            this.tileSystem.TileStyle.BackColor = System.Drawing.Color.Orange;
            this.tileSystem.TileStyle.BackColor2 = System.Drawing.Color.Orange;
            this.tileSystem.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tileSystem.TitleText = "系统管理";
            this.tileSystem.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.tileSystem.TitleTextFont = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // tileCompanyItem
            // 
            this.tileCompanyItem.AutoRotateFramesInterval = 2900;
            this.tileCompanyItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileCompanyItem.Image = global::Eleooo.Client.Properties.Resources.ItemImage;
            this.tileCompanyItem.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tileCompanyItem.Name = "tileCompanyItem";
            this.tileCompanyItem.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            // 
            // 
            // 
            this.tileCompanyItem.TileStyle.BackColor = System.Drawing.Color.CadetBlue;
            this.tileCompanyItem.TileStyle.BackColor2 = System.Drawing.Color.CadetBlue;
            this.tileCompanyItem.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tileCompanyItem.TitleText = "店面促销";
            this.tileCompanyItem.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.tileCompanyItem.TitleTextFont = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // tileCompanyAds
            // 
            this.tileCompanyAds.AutoRotateFramesInterval = 3000;
            this.tileCompanyAds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileCompanyAds.Image = global::Eleooo.Client.Properties.Resources.AdsImage;
            this.tileCompanyAds.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tileCompanyAds.Name = "tileCompanyAds";
            this.tileCompanyAds.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            // 
            // 
            // 
            this.tileCompanyAds.TileStyle.BackColor = System.Drawing.Color.SandyBrown;
            this.tileCompanyAds.TileStyle.BackColor2 = System.Drawing.Color.SandyBrown;
            this.tileCompanyAds.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tileCompanyAds.TitleText = "广告定投";
            this.tileCompanyAds.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.tileCompanyAds.TitleTextFont = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // titleEleooo
            // 
            this.titleEleooo.AutoRotateFramesInterval = 2800;
            this.titleEleooo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.titleEleooo.Image = global::Eleooo.Client.Properties.Resources.EleoooImage;
            this.titleEleooo.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.titleEleooo.Name = "titleEleooo";
            this.titleEleooo.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            // 
            // 
            // 
            this.titleEleooo.TileStyle.BackColor = System.Drawing.Color.Chocolate;
            this.titleEleooo.TileStyle.BackColor2 = System.Drawing.Color.Chocolate;
            this.titleEleooo.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // tileClose
            // 
            this.tileClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileClose.Image = global::Eleooo.Client.Properties.Resources.LogoutImage;
            this.tileClose.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tileClose.Name = "tileClose";
            this.tileClose.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            // 
            // 
            // 
            this.tileClose.TileStyle.BackColor = System.Drawing.Color.YellowGreen;
            this.tileClose.TileStyle.BackColor2 = System.Drawing.Color.YellowGreen;
            this.tileClose.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tileClose.TitleText = "安全退出";
            this.tileClose.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.tileClose.TitleTextFont = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.White;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.picLogo.ForeColor = System.Drawing.Color.Black;
            this.picLogo.Image = global::Eleooo.Client.Properties.Resources.HeadLogo;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Margin = new System.Windows.Forms.Padding(10);
            this.picLogo.Name = "picLogo";
            this.picLogo.Padding = new System.Windows.Forms.Padding(3);
            this.picLogo.Size = new System.Drawing.Size(459, 67);
            this.picLogo.TabIndex = 2;
            this.picLogo.TabStop = false;
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.plRight);
            this.mainContainer.Controls.Add(this.plLeft);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(684, 503);
            this.mainContainer.TabIndex = 1;
            this.mainContainer.Text = "slidePanel1";
            this.mainContainer.UsesBlockingAnimation = false;
            // 
            // UcHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainContainer);
            this.Name = "UcHome";
            this.Size = new System.Drawing.Size(684, 503);
            this.SlideSide = DevComponents.DotNetBar.Controls.eSlideSide.Right;
            this.Controls.SetChildIndex(this.mainContainer, 0);
            this.Controls.SetChildIndex(this.plTitle, 0);
            this.plTitle.ResumeLayout(false);
            this.plLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCompany)).EndInit( );
            this.plRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit( );
            this.mainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.SlidePanel plLeft;
        private DevComponents.DotNetBar.Controls.SlidePanel plRight;
        private DevComponents.DotNetBar.Metro.MetroTilePanel plMainFunc;
        private DevComponents.DotNetBar.ItemContainer funcContainer;
        private DevComponents.DotNetBar.Metro.MetroTileItem tileOrder;
        private DevComponents.DotNetBar.Metro.MetroTileItem tileCash;
        private DevComponents.DotNetBar.Metro.MetroTileItem tileMember;
        private DevComponents.DotNetBar.Metro.MetroTileItem titleEleooo;
        private DevComponents.DotNetBar.Metro.MetroTileItem tileCompanyItem;
        private DevComponents.DotNetBar.Metro.MetroTileItem tileCompanyAds;
        private DevComponents.DotNetBar.Metro.MetroTileItem tileSystem;
        private DevComponents.DotNetBar.Metro.MetroTileItem tileClose;
        private System.Windows.Forms.PictureBox picLogo;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl gridCompanyInfo;
        public System.Windows.Forms.PictureBox picCompany;
        private DevComponents.DotNetBar.Controls.SlidePanel mainContainer;
    }
}
