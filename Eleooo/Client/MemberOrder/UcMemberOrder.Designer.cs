namespace Eleooo.Client
{
    partial class UcMemberOrder
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
            this.components = new System.ComponentModel.Container( );
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            DevComponents.DotNetBar.SuperGrid.Style.Padding padding1 = new DevComponents.DotNetBar.SuperGrid.Style.Padding( );
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            DevComponents.DotNetBar.SuperGrid.Style.Background background1 = new DevComponents.DotNetBar.SuperGrid.Style.Background( );
            DevComponents.DotNetBar.SuperGrid.Style.BackColorBlend backColorBlend1 = new DevComponents.DotNetBar.SuperGrid.Style.BackColorBlend( );
            DevComponents.DotNetBar.SuperGrid.Style.Background background2 = new DevComponents.DotNetBar.SuperGrid.Style.Background( );
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcMemberOrder));
            this.plMemberInfo = new DevComponents.DotNetBar.Controls.GroupPanel( );
            this.gvUserInfo = new DevComponents.DotNetBar.SuperGrid.SuperGridControl( );
            this.bsUserInfo = new System.Windows.Forms.BindingSource(this.components);
            this.plUserInfo = new System.Windows.Forms.TextBox( );
            this.leftContainer = new DevComponents.DotNetBar.Ribbon.RibbonClientPanel( );
            this.orderContainer = new DevComponents.DotNetBar.ItemPanel( );
            this.txtPhone = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.bsOrderEntity = new System.Windows.Forms.BindingSource(this.components);
            this.txtOrderSum = new DevComponents.Editors.DoubleInput( );
            this.txtRateSale = new DevComponents.Editors.IntegerInput( );
            this.txtCash = new DevComponents.Editors.IntegerInput( );
            this.txtBalance = new DevComponents.Editors.IntegerInput( );
            this.txtBalanceCash = new DevComponents.Editors.IntegerInput( );
            this.txtSumOk = new DevComponents.Editors.IntegerInput( );
            this.cbMemberRate = new System.Windows.Forms.CheckedListBox( );
            this.cbProduct = new DevComponents.DotNetBar.Controls.ComboBoxEx( );
            this.txtMemo = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.txtPwd = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.btnSave = new DevComponents.DotNetBar.ButtonX( );
            this.btnClear = new DevComponents.DotNetBar.ButtonX( );
            this.lblMessage = new DevComponents.DotNetBar.LabelX( );
            this.lblOrderInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowPhone = new DevComponents.DotNetBar.ItemContainer( );
            this.lblPhone = new DevComponents.DotNetBar.LabelItem( );
            this.phoneContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.btnQuery = new DevComponents.DotNetBar.ButtonItem( );
            this.lblPhoneInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowOrderSum = new DevComponents.DotNetBar.ItemContainer( );
            this.lblOrderSum = new DevComponents.DotNetBar.LabelItem( );
            this.orderSumContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.lblOrderSumInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowRateSale = new DevComponents.DotNetBar.ItemContainer( );
            this.lblRateSale = new DevComponents.DotNetBar.LabelItem( );
            this.rateSaleContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.lblRateSaleInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowPayment = new DevComponents.DotNetBar.ItemContainer( );
            this.lblPayment = new DevComponents.DotNetBar.LabelItem( );
            this.cashContainer = new DevComponents.DotNetBar.ItemContainer( );
            this.lblCash = new DevComponents.DotNetBar.LabelItem( );
            this.cashTxtContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.balanceContainer = new DevComponents.DotNetBar.ItemContainer( );
            this.lblBalance = new DevComponents.DotNetBar.LabelItem( );
            this.balanceTxtContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.balanceCashContainer = new DevComponents.DotNetBar.ItemContainer( );
            this.lblBalanceCash = new DevComponents.DotNetBar.LabelItem( );
            this.balanceCashTxtContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.rowSumOk = new DevComponents.DotNetBar.ItemContainer( );
            this.lblSumOk = new DevComponents.DotNetBar.LabelItem( );
            this.sumOkContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.rowRate = new DevComponents.DotNetBar.ItemContainer( );
            this.lblRate = new DevComponents.DotNetBar.LabelItem( );
            this.rateContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.rowOrderItem = new DevComponents.DotNetBar.ItemContainer( );
            this.lblOrderItem = new DevComponents.DotNetBar.LabelItem( );
            this.productContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.rowMemo = new DevComponents.DotNetBar.ItemContainer( );
            this.lblMemo = new DevComponents.DotNetBar.LabelItem( );
            this.memoContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.rowMemberPass = new DevComponents.DotNetBar.ItemContainer( );
            this.lblPass = new DevComponents.DotNetBar.LabelItem( );
            this.pwdContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.btnReadFinger = new DevComponents.DotNetBar.ButtonItem( );
            this.lblPassInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowButton = new DevComponents.DotNetBar.ItemContainer( );
            this.btnSaveContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.btnClearContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.lblPayInfo = new DevComponents.DotNetBar.LabelItem( );
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer( );
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem( );
            this.lblHelpHeader = new DevComponents.DotNetBar.LabelItem( );
            this.rowHelp = new DevComponents.DotNetBar.ItemContainer( );
            this.lblHelp = new DevComponents.DotNetBar.LabelItem( );
            this.mainContainer = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.plTitle.SuspendLayout( );
            this.plMemberInfo.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.bsUserInfo)).BeginInit( );
            this.leftContainer.SuspendLayout( );
            this.orderContainer.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.bsOrderEntity)).BeginInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderSum)).BeginInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtRateSale)).BeginInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtCash)).BeginInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtBalance)).BeginInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtBalanceCash)).BeginInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtSumOk)).BeginInit( );
            this.mainContainer.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // btnSlide
            // 
            this.btnSlide.Location = new System.Drawing.Point(615, 2);
            // 
            // plTitle
            // 
            this.plTitle.Size = new System.Drawing.Size(654, 32);
            // 
            // plMemberInfo
            // 
            this.plMemberInfo.BackColor = System.Drawing.Color.Transparent;
            this.plMemberInfo.CanvasColor = System.Drawing.SystemColors.Control;
            this.plMemberInfo.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.plMemberInfo.Controls.Add(this.gvUserInfo);
            this.plMemberInfo.Controls.Add(this.plUserInfo);
            this.plMemberInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.plMemberInfo.Location = new System.Drawing.Point(456, 0);
            this.plMemberInfo.Margin = new System.Windows.Forms.Padding(0);
            this.plMemberInfo.Name = "plMemberInfo";
            this.plMemberInfo.Size = new System.Drawing.Size(198, 454);
            // 
            // 
            // 
            this.plMemberInfo.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.plMemberInfo.Style.BackColorGradientAngle = 90;
            this.plMemberInfo.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.plMemberInfo.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.plMemberInfo.Style.BorderBottomWidth = 1;
            this.plMemberInfo.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.plMemberInfo.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.plMemberInfo.Style.BorderLeftWidth = 1;
            this.plMemberInfo.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.plMemberInfo.Style.BorderRightWidth = 1;
            this.plMemberInfo.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.plMemberInfo.Style.BorderTopWidth = 1;
            this.plMemberInfo.Style.CornerDiameter = 0;
            this.plMemberInfo.Style.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.plMemberInfo.Style.MarginBottom = 3;
            this.plMemberInfo.Style.MarginLeft = 3;
            this.plMemberInfo.Style.MarginRight = 3;
            this.plMemberInfo.Style.MarginTop = 3;
            this.plMemberInfo.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.plMemberInfo.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.plMemberInfo.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.plMemberInfo.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.plMemberInfo.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.plMemberInfo.TabIndex = 0;
            // 
            // gvUserInfo
            // 
            this.gvUserInfo.BackColor = System.Drawing.Color.White;
            this.gvUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvUserInfo.ForeColor = System.Drawing.Color.Black;
            this.gvUserInfo.Location = new System.Drawing.Point(0, 0);
            this.gvUserInfo.Name = "gvUserInfo";
            this.gvUserInfo.PrimaryGrid.AutoGenerateColumns = false;
            this.gvUserInfo.PrimaryGrid.Caption.Text = "会员信息";
            this.gvUserInfo.PrimaryGrid.ColumnHeader.Visible = false;
            gridColumn1.AllowSelection = false;
            gridColumn1.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            gridColumn1.CellHighlightMode = DevComponents.DotNetBar.SuperGrid.Style.CellHighlightMode.None;
            gridColumn1.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleRight;
            gridColumn1.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            gridColumn1.CellStyles.Default.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            padding1.Right = 5;
            gridColumn1.CellStyles.Default.Margin = padding1;
            gridColumn1.CellStyles.Default.TextColor = System.Drawing.Color.SaddleBrown;
            gridColumn1.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            gridColumn1.DataPropertyName = "Key";
            gridColumn1.EditorType = null;
            gridColumn1.Name = "Key";
            gridColumn1.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridTextBoxXEditControl);
            gridColumn1.Width = 70;
            gridColumn2.AllowSelection = false;
            gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            gridColumn2.CellHighlightMode = DevComponents.DotNetBar.SuperGrid.Style.CellHighlightMode.None;
            gridColumn2.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            gridColumn2.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            gridColumn2.CellStyles.Default.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            gridColumn2.CellStyles.Default.TextColor = System.Drawing.Color.OrangeRed;
            gridColumn2.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            gridColumn2.DataPropertyName = "Value";
            gridColumn2.EditorType = null;
            gridColumn2.FillWeight = 200;
            gridColumn2.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            gridColumn2.Name = "Value";
            gridColumn2.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridTextBoxXEditControl);
            gridColumn2.Width = 200;
            this.gvUserInfo.PrimaryGrid.Columns.Add(gridColumn1);
            this.gvUserInfo.PrimaryGrid.Columns.Add(gridColumn2);
            this.gvUserInfo.PrimaryGrid.DataSource = this.bsUserInfo;
            this.gvUserInfo.PrimaryGrid.DefaultRowHeight = 36;
            backColorBlend1.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.SkyBlue};
            background1.BackColorBlend = backColorBlend1;
            this.gvUserInfo.PrimaryGrid.DefaultVisualStyles.AlternateRowCellStyles.Default.Background = background1;
            background2.Color1 = System.Drawing.Color.Orange;
            this.gvUserInfo.PrimaryGrid.DefaultVisualStyles.CaptionStyles.Default.Background = background2;
            this.gvUserInfo.PrimaryGrid.DefaultVisualStyles.CaptionStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gvUserInfo.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gvUserInfo.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gvUserInfo.PrimaryGrid.DefaultVisualStyles.GridPanelStyle.HorizontalLinePattern = DevComponents.DotNetBar.SuperGrid.Style.LinePattern.DashDotDot;
            this.gvUserInfo.PrimaryGrid.DefaultVisualStyles.GridPanelStyle.VerticalLinePattern = DevComponents.DotNetBar.SuperGrid.Style.LinePattern.DashDotDot;
            this.gvUserInfo.PrimaryGrid.InitialActiveRow = DevComponents.DotNetBar.SuperGrid.RelativeRow.None;
            this.gvUserInfo.PrimaryGrid.MultiSelect = false;
            this.gvUserInfo.PrimaryGrid.ReadOnly = true;
            this.gvUserInfo.PrimaryGrid.RowHighlightType = DevComponents.DotNetBar.SuperGrid.RowHighlightType.None;
            this.gvUserInfo.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.gvUserInfo.PrimaryGrid.ShowCellInfo = false;
            this.gvUserInfo.PrimaryGrid.ShowColumnHeader = false;
            this.gvUserInfo.PrimaryGrid.ShowRowHeaders = false;
            this.gvUserInfo.PrimaryGrid.SizingStyle = DevComponents.DotNetBar.SuperGrid.Style.StyleType.ReadOnlySelected;
            this.gvUserInfo.PrimaryGrid.Title.RowHeaderVisibility = DevComponents.DotNetBar.SuperGrid.RowHeaderVisibility.PanelControlled;
            this.gvUserInfo.PrimaryGrid.UseAlternateRowStyle = true;
            this.gvUserInfo.Size = new System.Drawing.Size(196, 299);
            this.gvUserInfo.SizingStyle = DevComponents.DotNetBar.SuperGrid.Style.StyleType.ReadOnlySelected;
            this.gvUserInfo.TabIndex = 3;
            // 
            // bsUserInfo
            // 
            this.bsUserInfo.DataMember = "UserInfo";
            this.bsUserInfo.DataSource = typeof(Eleooo.Client.UcMemberOrder);
            // 
            // plUserInfo
            // 
            this.plUserInfo.BackColor = System.Drawing.Color.White;
            this.plUserInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.plUserInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plUserInfo.ForeColor = System.Drawing.Color.Red;
            this.plUserInfo.Location = new System.Drawing.Point(0, 299);
            this.plUserInfo.Multiline = true;
            this.plUserInfo.Name = "plUserInfo";
            this.plUserInfo.ReadOnly = true;
            this.plUserInfo.Size = new System.Drawing.Size(196, 153);
            this.plUserInfo.TabIndex = 4;
            // 
            // leftContainer
            // 
            this.leftContainer.CanvasColor = System.Drawing.SystemColors.Control;
            this.leftContainer.Controls.Add(this.orderContainer);
            this.leftContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftContainer.Location = new System.Drawing.Point(0, 0);
            this.leftContainer.Name = "leftContainer";
            this.leftContainer.Size = new System.Drawing.Size(456, 454);
            // 
            // 
            // 
            this.leftContainer.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.leftContainer.Style.BackColorGradientAngle = 90;
            this.leftContainer.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.leftContainer.Style.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.Tile;
            this.leftContainer.Style.BorderBottomWidth = 10;
            this.leftContainer.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.leftContainer.Style.BorderLeftWidth = 10;
            this.leftContainer.Style.BorderRightWidth = 10;
            this.leftContainer.Style.BorderTopWidth = 10;
            this.leftContainer.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.leftContainer.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.leftContainer.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            // 
            // 
            // 
            this.leftContainer.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.leftContainer.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.leftContainer.TabIndex = 1;
            this.leftContainer.Text = "ribbonClientPanel1";
            // 
            // orderContainer
            // 
            this.orderContainer.AutoScroll = true;
            // 
            // 
            // 
            this.orderContainer.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.orderContainer.BackgroundStyle.BorderBottomWidth = 1;
            this.orderContainer.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.orderContainer.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.orderContainer.BackgroundStyle.BorderLeftWidth = 1;
            this.orderContainer.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.orderContainer.BackgroundStyle.BorderRightWidth = 1;
            this.orderContainer.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.orderContainer.BackgroundStyle.BorderTopWidth = 1;
            this.orderContainer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.orderContainer.BackgroundStyle.PaddingBottom = 1;
            this.orderContainer.BackgroundStyle.PaddingLeft = 1;
            this.orderContainer.BackgroundStyle.PaddingRight = 1;
            this.orderContainer.BackgroundStyle.PaddingTop = 1;
            this.orderContainer.ContainerControlProcessDialogKey = true;
            this.orderContainer.Controls.Add(this.txtPhone);
            this.orderContainer.Controls.Add(this.txtOrderSum);
            this.orderContainer.Controls.Add(this.txtRateSale);
            this.orderContainer.Controls.Add(this.txtCash);
            this.orderContainer.Controls.Add(this.txtBalance);
            this.orderContainer.Controls.Add(this.txtBalanceCash);
            this.orderContainer.Controls.Add(this.txtSumOk);
            this.orderContainer.Controls.Add(this.cbMemberRate);
            this.orderContainer.Controls.Add(this.cbProduct);
            this.orderContainer.Controls.Add(this.txtMemo);
            this.orderContainer.Controls.Add(this.txtPwd);
            this.orderContainer.Controls.Add(this.btnSave);
            this.orderContainer.Controls.Add(this.btnClear);
            this.orderContainer.Controls.Add(this.lblMessage);
            this.orderContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderContainer.FitButtonsToContainerWidth = true;
            this.orderContainer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderContainer.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblOrderInfo,
            this.rowPhone,
            this.rowOrderSum,
            this.rowRateSale,
            this.rowPayment,
            this.rowSumOk,
            this.rowRate,
            this.rowOrderItem,
            this.rowMemo,
            this.rowMemberPass,
            this.rowButton,
            this.lblPayInfo,
            this.itemContainer1,
            this.lblHelpHeader,
            this.rowHelp});
            this.orderContainer.ItemSpacing = 5;
            this.orderContainer.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.orderContainer.Location = new System.Drawing.Point(0, 0);
            this.orderContainer.MultiLine = true;
            this.orderContainer.Name = "orderContainer";
            this.orderContainer.Size = new System.Drawing.Size(456, 454);
            this.orderContainer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.orderContainer.TabIndex = 6;
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPhone.Border.Class = "TextBoxBorder";
            this.txtPhone.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderEntity, "PhoneNum", true));
            this.txtPhone.ForeColor = System.Drawing.Color.Black;
            this.txtPhone.Location = new System.Drawing.Point(77, 29);
            this.txtPhone.MaxLength = 11;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(125, 21);
            this.txtPhone.TabIndex = 0;
            this.txtPhone.TextChanged += new System.EventHandler(this.txtPhone_TextChanged);
            // 
            // bsOrderEntity
            // 
            this.bsOrderEntity.DataMember = "OrderData";
            this.bsOrderEntity.DataSource = typeof(Eleooo.Client.UcMemberOrder);
            // 
            // txtOrderSum
            // 
            // 
            // 
            // 
            this.txtOrderSum.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtOrderSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOrderSum.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtOrderSum.DisplayFormat = "0.00";
            this.txtOrderSum.Increment = 1D;
            this.txtOrderSum.Location = new System.Drawing.Point(77, 59);
            this.txtOrderSum.MinValue = 0D;
            this.txtOrderSum.Name = "txtOrderSum";
            this.txtOrderSum.ShowUpDown = true;
            this.txtOrderSum.Size = new System.Drawing.Size(125, 21);
            this.txtOrderSum.TabIndex = 1;
            this.txtOrderSum.ValueChanged += new System.EventHandler(this.txtOrderSum_ValueChanged);
            // 
            // txtRateSale
            // 
            // 
            // 
            // 
            this.txtRateSale.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtRateSale.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRateSale.ButtonCustom.DisplayPosition = 10;
            this.txtRateSale.ButtonCustom.Text = "计算";
            this.txtRateSale.ButtonCustom.Visible = true;
            this.txtRateSale.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtRateSale.DisplayFormat = "0";
            this.txtRateSale.Location = new System.Drawing.Point(77, 89);
            this.txtRateSale.MaxValue = 99;
            this.txtRateSale.MinValue = 0;
            this.txtRateSale.Name = "txtRateSale";
            this.txtRateSale.ShowUpDown = true;
            this.txtRateSale.Size = new System.Drawing.Size(125, 21);
            this.txtRateSale.TabIndex = 2;
            this.txtRateSale.ValueChanged += new System.EventHandler(this.txtRateSale_ValueChanged);
            // 
            // txtCash
            // 
            // 
            // 
            // 
            this.txtCash.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtCash.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCash.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtCash.DisplayFormat = "0";
            this.txtCash.Location = new System.Drawing.Point(109, 119);
            this.txtCash.MinValue = 0;
            this.txtCash.Name = "txtCash";
            this.txtCash.ShowUpDown = true;
            this.txtCash.Size = new System.Drawing.Size(75, 21);
            this.txtCash.TabIndex = 3;
            // 
            // txtBalance
            // 
            // 
            // 
            // 
            this.txtBalance.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtBalance.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBalance.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtBalance.DisplayFormat = "0";
            this.txtBalance.Location = new System.Drawing.Point(223, 119);
            this.txtBalance.MinValue = 0;
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.ShowUpDown = true;
            this.txtBalance.Size = new System.Drawing.Size(73, 21);
            this.txtBalance.TabIndex = 4;
            // 
            // txtBalanceCash
            // 
            // 
            // 
            // 
            this.txtBalanceCash.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtBalanceCash.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBalanceCash.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtBalanceCash.DisplayFormat = "0";
            this.txtBalanceCash.Location = new System.Drawing.Point(335, 119);
            this.txtBalanceCash.MinValue = 0;
            this.txtBalanceCash.Name = "txtBalanceCash";
            this.txtBalanceCash.ShowUpDown = true;
            this.txtBalanceCash.Size = new System.Drawing.Size(76, 21);
            this.txtBalanceCash.TabIndex = 5;
            // 
            // txtSumOk
            // 
            // 
            // 
            // 
            this.txtSumOk.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtSumOk.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSumOk.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtSumOk.Enabled = false;
            this.txtSumOk.IsInputReadOnly = true;
            this.txtSumOk.Location = new System.Drawing.Point(77, 149);
            this.txtSumOk.MinValue = 0;
            this.txtSumOk.Name = "txtSumOk";
            this.txtSumOk.Size = new System.Drawing.Size(127, 21);
            this.txtSumOk.TabIndex = 6;
            // 
            // cbMemberRate
            // 
            this.cbMemberRate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cbMemberRate.CheckOnClick = true;
            this.cbMemberRate.ColumnWidth = 60;
            this.cbMemberRate.HorizontalScrollbar = true;
            this.cbMemberRate.Location = new System.Drawing.Point(77, 181);
            this.cbMemberRate.MultiColumn = true;
            this.cbMemberRate.Name = "cbMemberRate";
            this.cbMemberRate.Size = new System.Drawing.Size(332, 16);
            this.cbMemberRate.TabIndex = 7;
            this.cbMemberRate.ThreeDCheckBoxes = true;
            this.cbMemberRate.UseCompatibleTextRendering = true;
            this.cbMemberRate.UseTabStops = false;
            this.cbMemberRate.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.cbMemberRate_ItemCheck);
            this.cbMemberRate.SelectedIndexChanged += new System.EventHandler(this.cbMemberRate_SelectedIndexChanged);
            // 
            // cbProduct
            // 
            this.cbProduct.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsOrderEntity, "OrderProduct", true));
            this.cbProduct.DisplayMember = "Selectedvalue";
            this.cbProduct.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbProduct.FormattingEnabled = true;
            this.cbProduct.ItemHeight = 15;
            this.cbProduct.Location = new System.Drawing.Point(77, 209);
            this.cbProduct.Name = "cbProduct";
            this.cbProduct.Size = new System.Drawing.Size(130, 21);
            this.cbProduct.TabIndex = 8;
            // 
            // txtMemo
            // 
            this.txtMemo.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtMemo.Border.Class = "TextBoxBorder";
            this.txtMemo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMemo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderEntity, "OrderMemo", true));
            this.txtMemo.ForeColor = System.Drawing.Color.Black;
            this.txtMemo.Location = new System.Drawing.Point(77, 239);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(332, 21);
            this.txtMemo.TabIndex = 9;
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPwd.Border.Class = "TextBoxBorder";
            this.txtPwd.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPwd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOrderEntity, "UserPwd", true));
            this.txtPwd.ForeColor = System.Drawing.Color.Black;
            this.txtPwd.Location = new System.Drawing.Point(77, 269);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(127, 21);
            this.txtPwd.TabIndex = 10;
            this.txtPwd.UseSystemPasswordChar = true;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Eleooo.Client.MetroIcon.CheckMark;
            this.btnSave.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnSave.Location = new System.Drawing.Point(125, 300);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 23);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存&S";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClear.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Image = global::Eleooo.Client.MetroIcon.Delete;
            this.btnClear.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnClear.Location = new System.Drawing.Point(252, 300);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(78, 23);
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "取消&C";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblMessage
            // 
            // 
            // 
            // 
            this.lblMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMessage.Location = new System.Drawing.Point(4, 359);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(326, 23);
            this.lblMessage.TabIndex = 15;
            // 
            // lblOrderInfo
            // 
            this.lblOrderInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.lblOrderInfo.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.lblOrderInfo.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.lblOrderInfo.Name = "lblOrderInfo";
            this.lblOrderInfo.PaddingBottom = 1;
            this.lblOrderInfo.PaddingLeft = 1;
            this.lblOrderInfo.PaddingRight = 1;
            this.lblOrderInfo.PaddingTop = 1;
            this.lblOrderInfo.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblOrderInfo.Text = "<b>消费信息</b>";
            // 
            // rowPhone
            // 
            // 
            // 
            // 
            this.rowPhone.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowPhone.ItemSpacing = 3;
            this.rowPhone.Name = "rowPhone";
            this.rowPhone.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblPhone,
            this.phoneContainer,
            this.btnQuery,
            this.lblPhoneInfo});
            // 
            // lblPhone
            // 
            this.lblPhone.Height = 25;
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Text = "会员账号:";
            this.lblPhone.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblPhone.Width = 70;
            // 
            // phoneContainer
            // 
            this.phoneContainer.AllowItemResize = true;
            this.phoneContainer.Control = this.txtPhone;
            this.phoneContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.phoneContainer.Name = "phoneContainer";
            this.phoneContainer.Text = "controlContainerItem1";
            // 
            // btnQuery
            // 
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblPhoneInfo
            // 
            this.lblPhoneInfo.Name = "lblPhoneInfo";
            // 
            // rowOrderSum
            // 
            // 
            // 
            // 
            this.rowOrderSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowOrderSum.ItemSpacing = 3;
            this.rowOrderSum.Name = "rowOrderSum";
            this.rowOrderSum.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblOrderSum,
            this.orderSumContainer,
            this.lblOrderSumInfo});
            // 
            // lblOrderSum
            // 
            this.lblOrderSum.Height = 25;
            this.lblOrderSum.Name = "lblOrderSum";
            this.lblOrderSum.Text = "消费金额:";
            this.lblOrderSum.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblOrderSum.Width = 70;
            // 
            // orderSumContainer
            // 
            this.orderSumContainer.AllowItemResize = true;
            this.orderSumContainer.Control = this.txtOrderSum;
            this.orderSumContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.orderSumContainer.Name = "orderSumContainer";
            // 
            // lblOrderSumInfo
            // 
            this.lblOrderSumInfo.Name = "lblOrderSumInfo";
            // 
            // rowRateSale
            // 
            // 
            // 
            // 
            this.rowRateSale.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowRateSale.ItemSpacing = 3;
            this.rowRateSale.Name = "rowRateSale";
            this.rowRateSale.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblRateSale,
            this.rateSaleContainer,
            this.lblRateSaleInfo});
            // 
            // lblRateSale
            // 
            this.lblRateSale.Height = 25;
            this.lblRateSale.Name = "lblRateSale";
            this.lblRateSale.Text = "消费折扣:";
            this.lblRateSale.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblRateSale.Width = 70;
            // 
            // rateSaleContainer
            // 
            this.rateSaleContainer.AllowItemResize = true;
            this.rateSaleContainer.Control = this.txtRateSale;
            this.rateSaleContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.rateSaleContainer.Name = "rateSaleContainer";
            this.rateSaleContainer.Text = "controlContainerItem1";
            // 
            // lblRateSaleInfo
            // 
            this.lblRateSaleInfo.Name = "lblRateSaleInfo";
            this.lblRateSaleInfo.Text = "1－99数字,0代表不打折";
            // 
            // rowPayment
            // 
            // 
            // 
            // 
            this.rowPayment.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowPayment.ItemSpacing = 3;
            this.rowPayment.Name = "rowPayment";
            this.rowPayment.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblPayment,
            this.cashContainer,
            this.balanceContainer,
            this.balanceCashContainer});
            // 
            // lblPayment
            // 
            this.lblPayment.BeginGroup = true;
            this.lblPayment.Height = 25;
            this.lblPayment.Name = "lblPayment";
            this.lblPayment.Text = "付款方式:";
            this.lblPayment.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblPayment.Width = 70;
            // 
            // cashContainer
            // 
            // 
            // 
            // 
            this.cashContainer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cashContainer.MultiLine = true;
            this.cashContainer.Name = "cashContainer";
            this.cashContainer.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblCash,
            this.cashTxtContainer});
            this.cashContainer.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // lblCash
            // 
            this.lblCash.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblCash.Name = "lblCash";
            this.lblCash.Text = "现金";
            // 
            // cashTxtContainer
            // 
            this.cashTxtContainer.AllowItemResize = true;
            this.cashTxtContainer.Control = this.txtCash;
            this.cashTxtContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.cashTxtContainer.Name = "cashTxtContainer";
            this.cashTxtContainer.Text = "controlContainerItem1";
            // 
            // balanceContainer
            // 
            // 
            // 
            // 
            this.balanceContainer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.balanceContainer.Name = "balanceContainer";
            this.balanceContainer.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblBalance,
            this.balanceTxtContainer});
            this.balanceContainer.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // lblBalance
            // 
            this.lblBalance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Text = "积分";
            // 
            // balanceTxtContainer
            // 
            this.balanceTxtContainer.AllowItemResize = true;
            this.balanceTxtContainer.Control = this.txtBalance;
            this.balanceTxtContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.balanceTxtContainer.Name = "balanceTxtContainer";
            this.balanceTxtContainer.Text = "controlContainerItem1";
            // 
            // balanceCashContainer
            // 
            // 
            // 
            // 
            this.balanceCashContainer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.balanceCashContainer.Name = "balanceCashContainer";
            this.balanceCashContainer.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblBalanceCash,
            this.balanceCashTxtContainer});
            this.balanceCashContainer.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // lblBalanceCash
            // 
            this.lblBalanceCash.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblBalanceCash.Name = "lblBalanceCash";
            this.lblBalanceCash.Text = "储值";
            // 
            // balanceCashTxtContainer
            // 
            this.balanceCashTxtContainer.AllowItemResize = true;
            this.balanceCashTxtContainer.Control = this.txtBalanceCash;
            this.balanceCashTxtContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.balanceCashTxtContainer.Name = "balanceCashTxtContainer";
            this.balanceCashTxtContainer.Text = "controlContainerItem1";
            // 
            // rowSumOk
            // 
            // 
            // 
            // 
            this.rowSumOk.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowSumOk.ItemSpacing = 3;
            this.rowSumOk.Name = "rowSumOk";
            this.rowSumOk.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblSumOk,
            this.sumOkContainer});
            // 
            // lblSumOk
            // 
            this.lblSumOk.Height = 25;
            this.lblSumOk.Name = "lblSumOk";
            this.lblSumOk.Text = "实际金额:";
            this.lblSumOk.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblSumOk.Width = 70;
            // 
            // sumOkContainer
            // 
            this.sumOkContainer.AllowItemResize = true;
            this.sumOkContainer.Control = this.txtSumOk;
            this.sumOkContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.sumOkContainer.Name = "sumOkContainer";
            this.sumOkContainer.Text = "controlContainerItem1";
            // 
            // rowRate
            // 
            // 
            // 
            // 
            this.rowRate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowRate.ItemSpacing = 3;
            this.rowRate.Name = "rowRate";
            this.rowRate.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblRate,
            this.rateContainer});
            // 
            // lblRate
            // 
            this.lblRate.BeginGroup = true;
            this.lblRate.Height = 25;
            this.lblRate.Name = "lblRate";
            this.lblRate.Text = "积分比例:";
            this.lblRate.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblRate.Width = 70;
            // 
            // rateContainer
            // 
            this.rateContainer.AllowItemResize = true;
            this.rateContainer.Control = this.cbMemberRate;
            this.rateContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.rateContainer.Name = "rateContainer";
            this.rateContainer.Text = "controlContainerItem1";
            // 
            // rowOrderItem
            // 
            // 
            // 
            // 
            this.rowOrderItem.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowOrderItem.ItemSpacing = 3;
            this.rowOrderItem.Name = "rowOrderItem";
            this.rowOrderItem.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblOrderItem,
            this.productContainer});
            // 
            // lblOrderItem
            // 
            this.lblOrderItem.BeginGroup = true;
            this.lblOrderItem.Height = 25;
            this.lblOrderItem.Name = "lblOrderItem";
            this.lblOrderItem.Text = "消费项目:";
            this.lblOrderItem.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblOrderItem.Width = 70;
            // 
            // productContainer
            // 
            this.productContainer.AllowItemResize = true;
            this.productContainer.Control = this.cbProduct;
            this.productContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.productContainer.Name = "productContainer";
            this.productContainer.Text = "controlContainerItem1";
            // 
            // rowMemo
            // 
            // 
            // 
            // 
            this.rowMemo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowMemo.ItemSpacing = 3;
            this.rowMemo.Name = "rowMemo";
            this.rowMemo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblMemo,
            this.memoContainer});
            // 
            // lblMemo
            // 
            this.lblMemo.Height = 25;
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Text = "消费备注:";
            this.lblMemo.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblMemo.Width = 70;
            // 
            // memoContainer
            // 
            this.memoContainer.AllowItemResize = true;
            this.memoContainer.Control = this.txtMemo;
            this.memoContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.memoContainer.Name = "memoContainer";
            this.memoContainer.Text = "controlContainerItem1";
            // 
            // rowMemberPass
            // 
            // 
            // 
            // 
            this.rowMemberPass.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowMemberPass.ItemSpacing = 3;
            this.rowMemberPass.Name = "rowMemberPass";
            this.rowMemberPass.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblPass,
            this.pwdContainer,
            this.btnReadFinger,
            this.lblPassInfo});
            // 
            // lblPass
            // 
            this.lblPass.Height = 25;
            this.lblPass.Name = "lblPass";
            this.lblPass.Text = "会员密码:";
            this.lblPass.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblPass.Width = 70;
            // 
            // pwdContainer
            // 
            this.pwdContainer.AllowItemResize = true;
            this.pwdContainer.Control = this.txtPwd;
            this.pwdContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.pwdContainer.Name = "pwdContainer";
            this.pwdContainer.Text = "controlContainerItem1";
            // 
            // btnReadFinger
            // 
            this.btnReadFinger.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.btnReadFinger.Name = "btnReadFinger";
            this.btnReadFinger.Text = "读取指纹";
            this.btnReadFinger.Click += new System.EventHandler(this.btnReadFinger_Click);
            // 
            // lblPassInfo
            // 
            this.lblPassInfo.Name = "lblPassInfo";
            // 
            // rowButton
            // 
            // 
            // 
            // 
            this.rowButton.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowButton.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.rowButton.ItemSpacing = 50;
            this.rowButton.MinimumSize = new System.Drawing.Size(0, 30);
            this.rowButton.Name = "rowButton";
            this.rowButton.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSaveContainer,
            this.btnClearContainer});
            this.rowButton.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // btnSaveContainer
            // 
            this.btnSaveContainer.AllowItemResize = true;
            this.btnSaveContainer.Control = this.btnSave;
            this.btnSaveContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.btnSaveContainer.Name = "btnSaveContainer";
            // 
            // btnClearContainer
            // 
            this.btnClearContainer.AllowItemResize = true;
            this.btnClearContainer.Control = this.btnClear;
            this.btnClearContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.btnClearContainer.Name = "btnClearContainer";
            // 
            // lblPayInfo
            // 
            this.lblPayInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.lblPayInfo.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.lblPayInfo.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.lblPayInfo.Name = "lblPayInfo";
            this.lblPayInfo.PaddingBottom = 1;
            this.lblPayInfo.PaddingLeft = 1;
            this.lblPayInfo.PaddingRight = 1;
            this.lblPayInfo.PaddingTop = 1;
            this.lblPayInfo.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblPayInfo.Text = "<b>结算信息</b>";
            // 
            // itemContainer1
            // 
            // 
            // 
            // 
            this.itemContainer1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem1});
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = true;
            this.controlContainerItem1.Control = this.lblMessage;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            this.controlContainerItem1.Text = "controlContainerItem1";
            // 
            // lblHelpHeader
            // 
            this.lblHelpHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.lblHelpHeader.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.lblHelpHeader.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.lblHelpHeader.Name = "lblHelpHeader";
            this.lblHelpHeader.PaddingBottom = 1;
            this.lblHelpHeader.PaddingLeft = 1;
            this.lblHelpHeader.PaddingRight = 1;
            this.lblHelpHeader.PaddingTop = 1;
            this.lblHelpHeader.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblHelpHeader.Text = "<b>操作说明</b>";
            // 
            // rowHelp
            // 
            // 
            // 
            // 
            this.rowHelp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowHelp.MinimumSize = new System.Drawing.Size(0, 25);
            this.rowHelp.Name = "rowHelp";
            this.rowHelp.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblHelp});
            // 
            // lblHelp
            // 
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Text = resources.GetString("lblHelp.Text");
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.leftContainer);
            this.mainContainer.Controls.Add(this.plMemberInfo);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(654, 454);
            this.mainContainer.TabIndex = 1;
            this.mainContainer.Text = "slidePanel1";
            this.mainContainer.UsesBlockingAnimation = false;
            // 
            // UcMemberOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainContainer);
            this.Name = "UcMemberOrder";
            this.Size = new System.Drawing.Size(654, 454);
            this.Load += new System.EventHandler(this.UcMemberOrder_Load);
            this.Controls.SetChildIndex(this.mainContainer, 0);
            this.Controls.SetChildIndex(this.plTitle, 0);
            this.plTitle.ResumeLayout(false);
            this.plMemberInfo.ResumeLayout(false);
            this.plMemberInfo.PerformLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.bsUserInfo)).EndInit( );
            this.leftContainer.ResumeLayout(false);
            this.orderContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsOrderEntity)).EndInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderSum)).EndInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtRateSale)).EndInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtCash)).EndInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtBalance)).EndInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtBalanceCash)).EndInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtSumOk)).EndInit( );
            this.mainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel plMemberInfo;
        private DevComponents.DotNetBar.Ribbon.RibbonClientPanel leftContainer;
        private DevComponents.DotNetBar.ItemPanel orderContainer;
        private DevComponents.Editors.DoubleInput txtOrderSum;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnClear;
        private DevComponents.DotNetBar.LabelItem lblOrderInfo;
        private DevComponents.DotNetBar.ItemContainer rowPhone;
        private DevComponents.DotNetBar.LabelItem lblPhone;
        private DevComponents.DotNetBar.ButtonItem btnQuery;
        private DevComponents.DotNetBar.LabelItem lblPhoneInfo;
        private DevComponents.DotNetBar.ItemContainer rowOrderSum;
        private DevComponents.DotNetBar.LabelItem lblOrderSum;
        private DevComponents.DotNetBar.ControlContainerItem orderSumContainer;
        private DevComponents.DotNetBar.LabelItem lblOrderSumInfo;
        private DevComponents.DotNetBar.ItemContainer rowPayment;
        private DevComponents.DotNetBar.LabelItem lblPayment;
        private DevComponents.DotNetBar.ItemContainer rowRate;
        private DevComponents.DotNetBar.LabelItem lblRate;
        private DevComponents.DotNetBar.ItemContainer rowMemo;
        private DevComponents.DotNetBar.LabelItem lblMemo;
        private DevComponents.DotNetBar.ItemContainer rowButton;
        private DevComponents.DotNetBar.ControlContainerItem btnSaveContainer;
        private DevComponents.DotNetBar.ControlContainerItem btnClearContainer;
        private DevComponents.DotNetBar.ItemContainer rowMemberPass;
        private DevComponents.DotNetBar.LabelItem lblPass;
        private DevComponents.DotNetBar.LabelItem lblPassInfo;
        private DevComponents.DotNetBar.ControlContainerItem rateContainer;
        private System.Windows.Forms.CheckedListBox cbMemberRate;
        private DevComponents.DotNetBar.ItemContainer rowHelp;
        private DevComponents.DotNetBar.LabelItem lblHelp;
        private DevComponents.DotNetBar.LabelItem lblPayInfo;
        private DevComponents.DotNetBar.LabelItem lblHelpHeader;
        private DevComponents.DotNetBar.ItemContainer rowOrderItem;
        private DevComponents.DotNetBar.LabelItem lblOrderItem;
        private DevComponents.DotNetBar.ItemContainer rowRateSale;
        private DevComponents.DotNetBar.LabelItem lblRateSale;
        private DevComponents.DotNetBar.ControlContainerItem rateSaleContainer;
        private DevComponents.Editors.IntegerInput txtRateSale;
        private DevComponents.DotNetBar.LabelItem lblRateSaleInfo;
        private DevComponents.DotNetBar.ItemContainer rowSumOk;
        private DevComponents.DotNetBar.LabelItem lblSumOk;
        private DevComponents.DotNetBar.ControlContainerItem sumOkContainer;
        private DevComponents.Editors.IntegerInput txtSumOk;
        private DevComponents.DotNetBar.ButtonItem btnReadFinger;
        private DevComponents.DotNetBar.ItemContainer cashContainer;
        private DevComponents.DotNetBar.LabelItem lblCash;
        private DevComponents.DotNetBar.ControlContainerItem cashTxtContainer;
        private DevComponents.Editors.IntegerInput txtCash;
        private DevComponents.DotNetBar.ItemContainer balanceCashContainer;
        private DevComponents.DotNetBar.LabelItem lblBalanceCash;
        private DevComponents.DotNetBar.ControlContainerItem balanceCashTxtContainer;
        private DevComponents.Editors.IntegerInput txtBalanceCash;
        private DevComponents.DotNetBar.ItemContainer balanceContainer;
        private DevComponents.DotNetBar.LabelItem lblBalance;
        private DevComponents.DotNetBar.ControlContainerItem balanceTxtContainer;
        private DevComponents.Editors.IntegerInput txtBalance;
        private System.Windows.Forms.BindingSource bsOrderEntity;
        private DevComponents.DotNetBar.ControlContainerItem phoneContainer;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPhone;
        private DevComponents.DotNetBar.ControlContainerItem productContainer;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbProduct;
        private DevComponents.DotNetBar.ControlContainerItem memoContainer;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMemo;
        private DevComponents.DotNetBar.ControlContainerItem pwdContainer;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPwd;
        private System.Windows.Forms.BindingSource bsUserInfo;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl gvUserInfo;
        private DevComponents.DotNetBar.Controls.SlidePanel mainContainer;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
        private System.Windows.Forms.TextBox plUserInfo;
        private DevComponents.DotNetBar.LabelX lblMessage;
    }
}
