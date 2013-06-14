namespace Eleooo.Client
{
    partial class UcMemberCash
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
            this.cashContainer = new DevComponents.DotNetBar.ItemPanel( );
            this.txtPhone = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.bsCashEntity = new System.Windows.Forms.BindingSource(this.components);
            this.txtCashSum = new DevComponents.Editors.DoubleInput( );
            this.txtRateSale = new DevComponents.Editors.DoubleInput( );
            this.cbMemberRate = new System.Windows.Forms.CheckedListBox( );
            this.txtPoint = new DevComponents.Editors.DoubleInput( );
            this.cbUserGrade = new DevComponents.DotNetBar.Controls.ComboBoxEx( );
            this.btnSave = new DevComponents.DotNetBar.ButtonX( );
            this.btnClear = new DevComponents.DotNetBar.ButtonX( );
            this.lblMessage = new DevComponents.DotNetBar.LabelX( );
            this.lblOrderInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowPhone = new DevComponents.DotNetBar.ItemContainer( );
            this.lblPhone = new DevComponents.DotNetBar.LabelItem( );
            this.phoneContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.btnQuery = new DevComponents.DotNetBar.ButtonItem( );
            this.lblPhoneInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowCashSum = new DevComponents.DotNetBar.ItemContainer( );
            this.lblCashSum = new DevComponents.DotNetBar.LabelItem( );
            this.CashSumContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.lblOrderSumInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowRateSale = new DevComponents.DotNetBar.ItemContainer( );
            this.lblRateSale = new DevComponents.DotNetBar.LabelItem( );
            this.rateSaleContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.lblRateSaleInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowPointRate = new DevComponents.DotNetBar.ItemContainer( );
            this.lblPointRate = new DevComponents.DotNetBar.LabelItem( );
            this.pointRateContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.rowPoint = new DevComponents.DotNetBar.ItemContainer( );
            this.lblPoint = new DevComponents.DotNetBar.LabelItem( );
            this.pointContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.rowUserGrade = new DevComponents.DotNetBar.ItemContainer( );
            this.lblUserGrade = new DevComponents.DotNetBar.LabelItem( );
            this.gradeCBContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.rowButton = new DevComponents.DotNetBar.ItemContainer( );
            this.btnSaveContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.btnClearContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.lblPayInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowMessage = new DevComponents.DotNetBar.ItemContainer( );
            this.messageContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.lblHelpHeader = new DevComponents.DotNetBar.LabelItem( );
            this.rowHelp = new DevComponents.DotNetBar.ItemContainer( );
            this.lblHelp = new DevComponents.DotNetBar.LabelItem( );
            this.plMemberInfo = new System.Windows.Forms.Panel( );
            this.gvUserInfo = new DevComponents.DotNetBar.SuperGrid.SuperGridControl( );
            this.bsUserInfo = new System.Windows.Forms.BindingSource(this.components);
            this.plUserInfo = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.mainContainer = new System.Windows.Forms.Panel( );
            this.plTitle.SuspendLayout( );
            this.cashContainer.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.bsCashEntity)).BeginInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtCashSum)).BeginInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtRateSale)).BeginInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtPoint)).BeginInit( );
            this.plMemberInfo.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.bsUserInfo)).BeginInit( );
            this.mainContainer.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // cashContainer
            // 
            this.cashContainer.AutoScroll = true;
            // 
            // 
            // 
            this.cashContainer.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.cashContainer.BackgroundStyle.BorderBottomWidth = 1;
            this.cashContainer.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.cashContainer.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.cashContainer.BackgroundStyle.BorderLeftWidth = 1;
            this.cashContainer.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.cashContainer.BackgroundStyle.BorderRightWidth = 1;
            this.cashContainer.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.cashContainer.BackgroundStyle.BorderTopWidth = 1;
            this.cashContainer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cashContainer.BackgroundStyle.PaddingBottom = 1;
            this.cashContainer.BackgroundStyle.PaddingLeft = 1;
            this.cashContainer.BackgroundStyle.PaddingRight = 1;
            this.cashContainer.BackgroundStyle.PaddingTop = 1;
            this.cashContainer.ContainerControlProcessDialogKey = true;
            this.cashContainer.Controls.Add(this.txtPhone);
            this.cashContainer.Controls.Add(this.txtCashSum);
            this.cashContainer.Controls.Add(this.txtRateSale);
            this.cashContainer.Controls.Add(this.cbMemberRate);
            this.cashContainer.Controls.Add(this.txtPoint);
            this.cashContainer.Controls.Add(this.cbUserGrade);
            this.cashContainer.Controls.Add(this.btnSave);
            this.cashContainer.Controls.Add(this.btnClear);
            this.cashContainer.Controls.Add(this.lblMessage);
            this.cashContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cashContainer.FitButtonsToContainerWidth = true;
            this.cashContainer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cashContainer.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblOrderInfo,
            this.rowPhone,
            this.rowCashSum,
            this.rowRateSale,
            this.rowPointRate,
            this.rowPoint,
            this.rowUserGrade,
            this.rowButton,
            this.lblPayInfo,
            this.rowMessage,
            this.lblHelpHeader,
            this.rowHelp});
            this.cashContainer.ItemSpacing = 5;
            this.cashContainer.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.cashContainer.Location = new System.Drawing.Point(0, 0);
            this.cashContainer.MultiLine = true;
            this.cashContainer.Name = "cashContainer";
            this.cashContainer.Size = new System.Drawing.Size(401, 370);
            this.cashContainer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cashContainer.TabIndex = 0;
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPhone.Border.Class = "TextBoxBorder";
            this.txtPhone.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCashEntity, "PhoneNum", true));
            this.txtPhone.ForeColor = System.Drawing.Color.Black;
            this.txtPhone.Location = new System.Drawing.Point(77, 29);
            this.txtPhone.MaxLength = 11;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(175, 21);
            this.txtPhone.TabIndex = 0;
            this.txtPhone.TextChanged += new System.EventHandler(this.txtPhone_TextChanged);
            // 
            // bsCashEntity
            // 
            this.bsCashEntity.DataMember = "CashData";
            this.bsCashEntity.DataSource = typeof(Eleooo.Client.UcMemberCash);
            // 
            // txtCashSum
            // 
            // 
            // 
            // 
            this.txtCashSum.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtCashSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCashSum.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtCashSum.DataBindings.Add(new System.Windows.Forms.Binding("ValueObject", this.bsCashEntity, "CashSum", true));
            this.txtCashSum.DisplayFormat = "0.00";
            this.txtCashSum.Increment = 1D;
            this.txtCashSum.Location = new System.Drawing.Point(77, 59);
            this.txtCashSum.MinValue = 0D;
            this.txtCashSum.Name = "txtCashSum";
            this.txtCashSum.ShowUpDown = true;
            this.txtCashSum.Size = new System.Drawing.Size(175, 21);
            this.txtCashSum.TabIndex = 1;
            // 
            // txtRateSale
            // 
            // 
            // 
            // 
            this.txtRateSale.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtRateSale.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRateSale.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtRateSale.DataBindings.Add(new System.Windows.Forms.Binding("ValueObject", this.bsCashEntity, "CashRate", true));
            this.txtRateSale.Increment = 1D;
            this.txtRateSale.Location = new System.Drawing.Point(77, 89);
            this.txtRateSale.MaxValue = 99D;
            this.txtRateSale.MinValue = 0D;
            this.txtRateSale.Name = "txtRateSale";
            this.txtRateSale.ShowUpDown = true;
            this.txtRateSale.Size = new System.Drawing.Size(175, 21);
            this.txtRateSale.TabIndex = 2;
            // 
            // cbMemberRate
            // 
            this.cbMemberRate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cbMemberRate.CheckOnClick = true;
            this.cbMemberRate.ColumnWidth = 60;
            this.cbMemberRate.HorizontalScrollbar = true;
            this.cbMemberRate.Location = new System.Drawing.Point(77, 121);
            this.cbMemberRate.MultiColumn = true;
            this.cbMemberRate.Name = "cbMemberRate";
            this.cbMemberRate.Size = new System.Drawing.Size(296, 16);
            this.cbMemberRate.TabIndex = 3;
            this.cbMemberRate.ThreeDCheckBoxes = true;
            this.cbMemberRate.UseCompatibleTextRendering = true;
            this.cbMemberRate.UseTabStops = false;
            this.cbMemberRate.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.cbMemberRate_ItemCheck);
            this.cbMemberRate.SelectedIndexChanged += new System.EventHandler(this.cbMemberRate_SelectedIndexChanged);
            // 
            // txtPoint
            // 
            // 
            // 
            // 
            this.txtPoint.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtPoint.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPoint.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtPoint.DataBindings.Add(new System.Windows.Forms.Binding("ValueObject", this.bsCashEntity, "CashPoint", true));
            this.txtPoint.Increment = 1D;
            this.txtPoint.Location = new System.Drawing.Point(77, 149);
            this.txtPoint.MinValue = 0D;
            this.txtPoint.Name = "txtPoint";
            this.txtPoint.ShowUpDown = true;
            this.txtPoint.Size = new System.Drawing.Size(175, 21);
            this.txtPoint.TabIndex = 4;
            // 
            // cbUserGrade
            // 
            this.cbUserGrade.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsCashEntity, "CashMemo", true));
            this.cbUserGrade.DisplayMember = "Text";
            this.cbUserGrade.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbUserGrade.FormattingEnabled = true;
            this.cbUserGrade.ItemHeight = 15;
            this.cbUserGrade.Location = new System.Drawing.Point(77, 179);
            this.cbUserGrade.Name = "cbUserGrade";
            this.cbUserGrade.Size = new System.Drawing.Size(175, 21);
            this.cbUserGrade.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbUserGrade.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Eleooo.Client.MetroIcon.CheckMark;
            this.btnSave.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnSave.Location = new System.Drawing.Point(98, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 23);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 6;
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
            this.btnClear.Location = new System.Drawing.Point(225, 210);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(78, 23);
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "取消&C";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(4, 268);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(394, 23);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.TextLineAlignment = System.Drawing.StringAlignment.Near;
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
            this.lblOrderInfo.Text = "<b>充值信息</b>";
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
            // rowCashSum
            // 
            // 
            // 
            // 
            this.rowCashSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowCashSum.ItemSpacing = 3;
            this.rowCashSum.Name = "rowCashSum";
            this.rowCashSum.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblCashSum,
            this.CashSumContainer,
            this.lblOrderSumInfo});
            // 
            // lblCashSum
            // 
            this.lblCashSum.Height = 25;
            this.lblCashSum.Name = "lblCashSum";
            this.lblCashSum.Text = "充值金额:";
            this.lblCashSum.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblCashSum.Width = 70;
            // 
            // CashSumContainer
            // 
            this.CashSumContainer.AllowItemResize = true;
            this.CashSumContainer.Control = this.txtCashSum;
            this.CashSumContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.CashSumContainer.Name = "CashSumContainer";
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
            this.lblRateSale.Text = "折扣比例:";
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
            this.lblRateSaleInfo.Text = "1－99数字";
            // 
            // rowPointRate
            // 
            // 
            // 
            // 
            this.rowPointRate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowPointRate.ItemSpacing = 3;
            this.rowPointRate.Name = "rowPointRate";
            this.rowPointRate.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblPointRate,
            this.pointRateContainer});
            // 
            // lblPointRate
            // 
            this.lblPointRate.BeginGroup = true;
            this.lblPointRate.Height = 25;
            this.lblPointRate.Name = "lblPointRate";
            this.lblPointRate.Text = "积分比例:";
            this.lblPointRate.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblPointRate.Width = 70;
            // 
            // pointRateContainer
            // 
            this.pointRateContainer.AllowItemResize = true;
            this.pointRateContainer.Control = this.cbMemberRate;
            this.pointRateContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.pointRateContainer.Name = "pointRateContainer";
            // 
            // rowPoint
            // 
            // 
            // 
            // 
            this.rowPoint.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowPoint.ItemSpacing = 3;
            this.rowPoint.Name = "rowPoint";
            this.rowPoint.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblPoint,
            this.pointContainer});
            // 
            // lblPoint
            // 
            this.lblPoint.BeginGroup = true;
            this.lblPoint.Height = 25;
            this.lblPoint.Name = "lblPoint";
            this.lblPoint.Text = "赠送积分:";
            this.lblPoint.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblPoint.Width = 70;
            // 
            // pointContainer
            // 
            this.pointContainer.AllowItemResize = true;
            this.pointContainer.Control = this.txtPoint;
            this.pointContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.pointContainer.Name = "pointContainer";
            this.pointContainer.Text = "controlContainerItem1";
            // 
            // rowUserGrade
            // 
            // 
            // 
            // 
            this.rowUserGrade.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowUserGrade.ItemSpacing = 3;
            this.rowUserGrade.Name = "rowUserGrade";
            this.rowUserGrade.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblUserGrade,
            this.gradeCBContainer});
            // 
            // lblUserGrade
            // 
            this.lblUserGrade.BeginGroup = true;
            this.lblUserGrade.Height = 25;
            this.lblUserGrade.Name = "lblUserGrade";
            this.lblUserGrade.Text = "会员级别:";
            this.lblUserGrade.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblUserGrade.Width = 70;
            // 
            // gradeCBContainer
            // 
            this.gradeCBContainer.AllowItemResize = true;
            this.gradeCBContainer.Control = this.cbUserGrade;
            this.gradeCBContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.gradeCBContainer.Name = "gradeCBContainer";
            this.gradeCBContainer.Text = "controlContainerItem1";
            // 
            // rowButton
            // 
            // 
            // 
            // 
            this.rowButton.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowButton.BeginGroup = true;
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
            this.lblPayInfo.Text = "<b>信息</b>";
            // 
            // rowMessage
            // 
            // 
            // 
            // 
            this.rowMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowMessage.Name = "rowMessage";
            this.rowMessage.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.messageContainer});
            // 
            // messageContainer
            // 
            this.messageContainer.AllowItemResize = true;
            this.messageContainer.Control = this.lblMessage;
            this.messageContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.messageContainer.Name = "messageContainer";
            this.messageContainer.Text = "controlContainerItem1";
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
            this.rowHelp.BeginGroup = true;
            this.rowHelp.MinimumSize = new System.Drawing.Size(0, 25);
            this.rowHelp.Name = "rowHelp";
            this.rowHelp.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblHelp});
            // 
            // lblHelp
            // 
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Text = "帮助:刷新(<b><font color=\"#BA1419\">F5</font></b>),查询会员(<b><font color=\"#BA1419\">F7</f" +
                "ont></b>),保存(<b><font color=\"#BA1419\">F9</font></b>)";
            // 
            // plMemberInfo
            // 
            this.plMemberInfo.Controls.Add(this.gvUserInfo);
            this.plMemberInfo.Controls.Add(this.plUserInfo);
            this.plMemberInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.plMemberInfo.Location = new System.Drawing.Point(401, 0);
            this.plMemberInfo.Margin = new System.Windows.Forms.Padding(0);
            this.plMemberInfo.Name = "plMemberInfo";
            this.plMemberInfo.Size = new System.Drawing.Size(198, 370);
            this.plMemberInfo.TabIndex = 8;
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
            this.gvUserInfo.Size = new System.Drawing.Size(198, 273);
            this.gvUserInfo.SizingStyle = DevComponents.DotNetBar.SuperGrid.Style.StyleType.ReadOnlySelected;
            this.gvUserInfo.TabIndex = 5;
            // 
            // bsUserInfo
            // 
            this.bsUserInfo.DataMember = "UserInfo";
            this.bsUserInfo.DataSource = typeof(Eleooo.Client.UcMemberCash);
            // 
            // plUserInfo
            // 
            this.plUserInfo.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.plUserInfo.Border.Class = "TextBoxBorder";
            this.plUserInfo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.plUserInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plUserInfo.ForeColor = System.Drawing.Color.OrangeRed;
            this.plUserInfo.Location = new System.Drawing.Point(0, 273);
            this.plUserInfo.Multiline = true;
            this.plUserInfo.Name = "plUserInfo";
            this.plUserInfo.ReadOnly = true;
            this.plUserInfo.Size = new System.Drawing.Size(198, 97);
            this.plUserInfo.TabIndex = 1;
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.cashContainer);
            this.mainContainer.Controls.Add(this.plMemberInfo);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(599, 370);
            this.mainContainer.TabIndex = 2;
            // 
            // UcMemberCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainContainer);
            this.Name = "UcMemberCash";
            this.Size = new System.Drawing.Size(599, 370);
            this.Text = "会员充值";
            this.Controls.SetChildIndex(this.plTitle, 0);
            this.Controls.SetChildIndex(this.mainContainer, 0);
            this.plTitle.ResumeLayout(false);
            this.cashContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsCashEntity)).EndInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtCashSum)).EndInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtRateSale)).EndInit( );
            ((System.ComponentModel.ISupportInitialize)(this.txtPoint)).EndInit( );
            this.plMemberInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsUserInfo)).EndInit( );
            this.mainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ItemPanel cashContainer;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPhone;
        private DevComponents.Editors.DoubleInput txtCashSum;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnClear;
        private DevComponents.DotNetBar.LabelItem lblOrderInfo;
        private DevComponents.DotNetBar.ItemContainer rowPhone;
        private DevComponents.DotNetBar.LabelItem lblPhone;
        private DevComponents.DotNetBar.ControlContainerItem phoneContainer;
        private DevComponents.DotNetBar.ButtonItem btnQuery;
        private DevComponents.DotNetBar.LabelItem lblPhoneInfo;
        private DevComponents.DotNetBar.ItemContainer rowCashSum;
        private DevComponents.DotNetBar.LabelItem lblCashSum;
        private DevComponents.DotNetBar.ControlContainerItem CashSumContainer;
        private DevComponents.DotNetBar.LabelItem lblOrderSumInfo;
        private DevComponents.DotNetBar.ItemContainer rowRateSale;
        private DevComponents.DotNetBar.LabelItem lblRateSale;
        private DevComponents.DotNetBar.ControlContainerItem rateSaleContainer;
        private DevComponents.DotNetBar.ItemContainer rowUserGrade;
        private DevComponents.DotNetBar.LabelItem lblUserGrade;
        private DevComponents.DotNetBar.ItemContainer rowButton;
        private DevComponents.DotNetBar.ControlContainerItem btnSaveContainer;
        private DevComponents.DotNetBar.ControlContainerItem btnClearContainer;
        private DevComponents.DotNetBar.LabelItem lblPayInfo;
        private DevComponents.DotNetBar.LabelItem lblHelpHeader;
        private DevComponents.DotNetBar.ItemContainer rowHelp;
        private DevComponents.DotNetBar.LabelItem lblHelp;
        private System.Windows.Forms.Panel plMemberInfo;
        private DevComponents.Editors.DoubleInput txtRateSale;
        private DevComponents.DotNetBar.LabelItem lblRateSaleInfo;
        private DevComponents.DotNetBar.ControlContainerItem gradeCBContainer;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbUserGrade;
        private System.Windows.Forms.BindingSource bsCashEntity;
        private System.Windows.Forms.BindingSource bsUserInfo;
        private DevComponents.DotNetBar.ItemContainer rowMessage;
        private DevComponents.DotNetBar.ControlContainerItem messageContainer;
        private DevComponents.DotNetBar.LabelX lblMessage;
        private DevComponents.DotNetBar.Controls.TextBoxX plUserInfo;
        private System.Windows.Forms.Panel mainContainer;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl gvUserInfo;
        private DevComponents.DotNetBar.ItemContainer rowPointRate;
        private DevComponents.DotNetBar.LabelItem lblPointRate;
        private DevComponents.DotNetBar.ControlContainerItem pointRateContainer;
        private DevComponents.DotNetBar.ItemContainer rowPoint;
        private DevComponents.DotNetBar.LabelItem lblPoint;
        private DevComponents.DotNetBar.ControlContainerItem pointContainer;
        private DevComponents.Editors.DoubleInput txtPoint;
        private System.Windows.Forms.CheckedListBox cbMemberRate;
    }
}
