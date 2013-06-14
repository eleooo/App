namespace Eleooo.Client
{
    partial class UcNonMemberOrder
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
            this.orderContainer = new DevComponents.DotNetBar.ItemPanel( );
            this.txtOrderSum = new DevComponents.Editors.DoubleInput( );
            this.bsOrderEntity = new System.Windows.Forms.BindingSource(this.components);
            this.txtMemo = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.btnSave = new DevComponents.DotNetBar.ButtonX( );
            this.btnClear = new DevComponents.DotNetBar.ButtonX( );
            this.lblMessage = new DevComponents.DotNetBar.LabelX( );
            this.lblOrderInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowOrderSum = new DevComponents.DotNetBar.ItemContainer( );
            this.lblOrderSum = new DevComponents.DotNetBar.LabelItem( );
            this.orderSumContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.lblOrderSumInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowMemo = new DevComponents.DotNetBar.ItemContainer( );
            this.lblMemo = new DevComponents.DotNetBar.LabelItem( );
            this.memoContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.rowButton = new DevComponents.DotNetBar.ItemContainer( );
            this.btnSaveContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.btnClearContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.lblPayInfo = new DevComponents.DotNetBar.LabelItem( );
            this.rowMessageContainer = new DevComponents.DotNetBar.ItemContainer( );
            this.msgTxtContainer = new DevComponents.DotNetBar.ControlContainerItem( );
            this.plTitle.SuspendLayout( );
            this.orderContainer.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderSum)).BeginInit( );
            ((System.ComponentModel.ISupportInitialize)(this.bsOrderEntity)).BeginInit( );
            this.SuspendLayout( );
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
            this.orderContainer.Controls.Add(this.txtOrderSum);
            this.orderContainer.Controls.Add(this.txtMemo);
            this.orderContainer.Controls.Add(this.btnSave);
            this.orderContainer.Controls.Add(this.btnClear);
            this.orderContainer.Controls.Add(this.lblMessage);
            this.orderContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderContainer.FitButtonsToContainerWidth = true;
            this.orderContainer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderContainer.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblOrderInfo,
            this.rowOrderSum,
            this.rowMemo,
            this.rowButton,
            this.lblPayInfo,
            this.rowMessageContainer});
            this.orderContainer.ItemSpacing = 5;
            this.orderContainer.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.orderContainer.Location = new System.Drawing.Point(0, 0);
            this.orderContainer.MultiLine = true;
            this.orderContainer.Name = "orderContainer";
            this.orderContainer.Size = new System.Drawing.Size(466, 462);
            this.orderContainer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.orderContainer.TabIndex = 7;
            // 
            // txtOrderSum
            // 
            // 
            // 
            // 
            this.txtOrderSum.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtOrderSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOrderSum.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtOrderSum.DataBindings.Add(new System.Windows.Forms.Binding("ValueObject", this.bsOrderEntity, "OrderSum", true));
            this.txtOrderSum.DisplayFormat = "0.00";
            this.txtOrderSum.Increment = 1D;
            this.txtOrderSum.Location = new System.Drawing.Point(77, 29);
            this.txtOrderSum.MinValue = 0D;
            this.txtOrderSum.Name = "txtOrderSum";
            this.txtOrderSum.ShowUpDown = true;
            this.txtOrderSum.Size = new System.Drawing.Size(148, 21);
            this.txtOrderSum.TabIndex = 0;
            // 
            // bsOrderEntity
            // 
            this.bsOrderEntity.DataMember = "OrderData";
            this.bsOrderEntity.DataSource = typeof(Eleooo.Client.UcNonMemberOrder);
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
            this.txtMemo.Location = new System.Drawing.Point(77, 59);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(328, 21);
            this.txtMemo.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Eleooo.Client.MetroIcon.CheckMark;
            this.btnSave.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnSave.Location = new System.Drawing.Point(130, 90);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 23);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 2;
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
            this.btnClear.Location = new System.Drawing.Point(257, 90);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(78, 23);
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "取消&C";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(4, 148);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(400, 23);
            this.lblMessage.TabIndex = 4;
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
            this.lblOrderInfo.Text = "<b>消费信息</b>";
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
            // rowMessageContainer
            // 
            // 
            // 
            // 
            this.rowMessageContainer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowMessageContainer.Name = "rowMessageContainer";
            this.rowMessageContainer.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.msgTxtContainer});
            // 
            // msgTxtContainer
            // 
            this.msgTxtContainer.AllowItemResize = true;
            this.msgTxtContainer.Control = this.lblMessage;
            this.msgTxtContainer.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.msgTxtContainer.Name = "msgTxtContainer";
            this.msgTxtContainer.Text = "controlContainerItem1";
            // 
            // UcNonMemberOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.orderContainer);
            this.Name = "UcNonMemberOrder";
            this.Size = new System.Drawing.Size(466, 462);
            this.Load += new System.EventHandler(this.UcNonMemberOrder_Load);
            this.Controls.SetChildIndex(this.orderContainer, 0);
            this.Controls.SetChildIndex(this.plTitle, 0);
            this.plTitle.ResumeLayout(false);
            this.orderContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderSum)).EndInit( );
            ((System.ComponentModel.ISupportInitialize)(this.bsOrderEntity)).EndInit( );
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ItemPanel orderContainer;
        private DevComponents.Editors.DoubleInput txtOrderSum;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnClear;
        private DevComponents.DotNetBar.LabelItem lblOrderInfo;
        private DevComponents.DotNetBar.ItemContainer rowOrderSum;
        private DevComponents.DotNetBar.LabelItem lblOrderSum;
        private DevComponents.DotNetBar.ControlContainerItem orderSumContainer;
        private DevComponents.DotNetBar.LabelItem lblOrderSumInfo;
        private DevComponents.DotNetBar.ItemContainer rowButton;
        private DevComponents.DotNetBar.ControlContainerItem btnSaveContainer;
        private DevComponents.DotNetBar.ControlContainerItem btnClearContainer;
        private DevComponents.DotNetBar.ItemContainer rowMemo;
        private DevComponents.DotNetBar.LabelItem lblMemo;
        private DevComponents.DotNetBar.ControlContainerItem memoContainer;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMemo;
        private DevComponents.DotNetBar.LabelItem lblPayInfo;
        private System.Windows.Forms.BindingSource bsOrderEntity;
        private DevComponents.DotNetBar.ItemContainer rowMessageContainer;
        private DevComponents.DotNetBar.ControlContainerItem msgTxtContainer;
        private DevComponents.DotNetBar.LabelX lblMessage;
    }
}
