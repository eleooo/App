namespace Eleooo.Client
{
    partial class UcCompanyItemOrder
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
            DevComponents.DotNetBar.SuperGrid.Style.Background background1 = new DevComponents.DotNetBar.SuperGrid.Style.Background( );
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn( );
            this.plHeader = new System.Windows.Forms.Panel( );
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX( );
            this.btnQuery = new DevComponents.DotNetBar.ButtonX( );
            this.txtFinger = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.txtPwd = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.txtPhoneNum = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.lblPwd = new DevComponents.DotNetBar.LabelX( );
            this.lblPhoneNum = new DevComponents.DotNetBar.LabelX( );
            this.plMain = new System.Windows.Forms.Panel( );
            this.gridItem = new DevComponents.DotNetBar.SuperGrid.SuperGridControl( );
            this.gpFinger = new DevComponents.DotNetBar.Controls.GroupPanel( );
            this.pbFinger = new System.Windows.Forms.PictureBox( );
            this.plFingerInfo = new DevComponents.DotNetBar.Ribbon.RibbonClientPanel( );
            this.lblFingerInfo = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.plBtnReadFinger = new DevComponents.DotNetBar.Ribbon.RibbonClientPanel( );
            this.btnReadFinger = new DevComponents.DotNetBar.ButtonX( );
            this.mainContainer = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.plTitle.SuspendLayout( );
            this.plHeader.SuspendLayout( );
            this.plMain.SuspendLayout( );
            this.gpFinger.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.pbFinger)).BeginInit( );
            this.plFingerInfo.SuspendLayout( );
            this.plBtnReadFinger.SuspendLayout( );
            this.mainContainer.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // plHeader
            // 
            this.plHeader.BackColor = System.Drawing.Color.Transparent;
            this.plHeader.Controls.Add(this.buttonX1);
            this.plHeader.Controls.Add(this.btnQuery);
            this.plHeader.Controls.Add(this.txtFinger);
            this.plHeader.Controls.Add(this.txtPwd);
            this.plHeader.Controls.Add(this.txtPhoneNum);
            this.plHeader.Controls.Add(this.lblPwd);
            this.plHeader.Controls.Add(this.lblPhoneNum);
            this.plHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.plHeader.Location = new System.Drawing.Point(0, 0);
            this.plHeader.Name = "plHeader";
            this.plHeader.Size = new System.Drawing.Size(705, 48);
            this.plHeader.TabIndex = 0;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Image = global::Eleooo.Client.MetroIcon.Delete;
            this.buttonX1.Location = new System.Drawing.Point(498, 10);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(83, 31);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 6;
            this.buttonX1.Text = "取消(&C)";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.Image = global::Eleooo.Client.MetroIcon.CheckMark;
            this.btnQuery.Location = new System.Drawing.Point(393, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(83, 31);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "确定(&S)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtFinger
            // 
            this.txtFinger.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtFinger.Border.Class = "TextBoxBorder";
            this.txtFinger.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFinger.ForeColor = System.Drawing.Color.Black;
            this.txtFinger.Location = new System.Drawing.Point(673, 10);
            this.txtFinger.Name = "txtFinger";
            this.txtFinger.Size = new System.Drawing.Size(19, 21);
            this.txtFinger.TabIndex = 4;
            this.txtFinger.Visible = false;
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPwd.Border.Class = "TextBoxBorder";
            this.txtPwd.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPwd.ForeColor = System.Drawing.Color.Black;
            this.txtPwd.Location = new System.Drawing.Point(269, 14);
            this.txtPwd.MaxLength = 6;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(106, 21);
            this.txtPwd.TabIndex = 3;
            this.txtPwd.UseSystemPasswordChar = true;
            // 
            // txtPhoneNum
            // 
            this.txtPhoneNum.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPhoneNum.Border.Class = "TextBoxBorder";
            this.txtPhoneNum.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPhoneNum.ForeColor = System.Drawing.Color.Black;
            this.txtPhoneNum.Location = new System.Drawing.Point(65, 14);
            this.txtPhoneNum.MaxLength = 11;
            this.txtPhoneNum.Name = "txtPhoneNum";
            this.txtPhoneNum.Size = new System.Drawing.Size(132, 21);
            this.txtPhoneNum.TabIndex = 2;
            // 
            // lblPwd
            // 
            // 
            // 
            // 
            this.lblPwd.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPwd.Location = new System.Drawing.Point(206, 13);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(70, 23);
            this.lblPwd.TabIndex = 1;
            this.lblPwd.Text = "会员密码:";
            // 
            // lblPhoneNum
            // 
            // 
            // 
            // 
            this.lblPhoneNum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPhoneNum.Location = new System.Drawing.Point(6, 13);
            this.lblPhoneNum.Name = "lblPhoneNum";
            this.lblPhoneNum.Size = new System.Drawing.Size(70, 23);
            this.lblPhoneNum.TabIndex = 0;
            this.lblPhoneNum.Text = "会员账号:";
            // 
            // plMain
            // 
            this.plMain.Controls.Add(this.gridItem);
            this.plMain.Controls.Add(this.gpFinger);
            this.plMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMain.Location = new System.Drawing.Point(0, 48);
            this.plMain.Name = "plMain";
            this.plMain.Size = new System.Drawing.Size(705, 400);
            this.plMain.TabIndex = 1;
            // 
            // gridItem
            // 
            this.gridItem.BackColor = System.Drawing.Color.White;
            background1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            background1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridItem.DefaultVisualStyles.ColumnHeaderStyles.Default.Background = background1;
            this.gridItem.DefaultVisualStyles.ColumnHeaderStyles.Default.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridItem.ForeColor = System.Drawing.Color.Black;
            this.gridItem.Location = new System.Drawing.Point(0, 0);
            this.gridItem.Name = "gridItem";
            this.gridItem.PrimaryGrid.AllowRowResize = true;
            this.gridItem.PrimaryGrid.AutoGenerateColumns = false;
            this.gridItem.PrimaryGrid.ColumnHeader.RowHeight = 30;
            gridColumn1.CellHighlightMode = DevComponents.DotNetBar.SuperGrid.Style.CellHighlightMode.None;
            gridColumn1.DataPropertyName = "ItemID";
            gridColumn1.DefaultNewRowCellValue = "结算";
            gridColumn1.EditorType = typeof(Eleooo.Client.CompanyItemOrderButton);
            gridColumn1.HeaderText = "结算";
            gridColumn1.MarkRowDirtyOnCellValueChange = false;
            gridColumn1.Name = "Action";
            gridColumn1.NullString = "结算";
            gridColumn1.Width = 80;
            gridColumn2.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn2.DataPropertyName = "OrderDate";
            gridColumn2.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDateTimeInputEditControl);
            gridColumn2.HeaderText = "日期";
            gridColumn2.Name = "OrderDate";
            gridColumn2.Width = 80;
            gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            gridColumn3.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            gridColumn3.DataPropertyName = "ItemTitle";
            gridColumn3.HeaderText = "优惠描述";
            gridColumn3.Name = "ItemTitle";
            gridColumn3.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridTextBoxXEditControl);
            gridColumn3.Width = 200;
            gridColumn4.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn4.DataPropertyName = "ItemPoint";
            gridColumn4.HeaderText = "兑换积分";
            gridColumn4.Name = "ItemPoint";
            gridColumn4.Width = 80;
            gridColumn5.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn5.DataPropertyName = "ItemSum";
            gridColumn5.HeaderText = "原价";
            gridColumn5.Name = "ItemSum";
            gridColumn5.Width = 80;
            gridColumn6.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn6.DataPropertyName = "Amount";
            gridColumn6.DefaultNewRowCellValue = "1";
            gridColumn6.HeaderText = "数量";
            gridColumn6.Name = "Amount";
            gridColumn6.NullString = "1";
            gridColumn6.Width = 60;
            gridColumn7.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn7.DataPropertyName = "MemberPhoneNumber";
            gridColumn7.HeaderText = "会员账号";
            gridColumn7.Name = "MemberPhoneNumber";
            gridColumn8.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn8.DataPropertyName = "ItemDate";
            gridColumn8.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDateTimeInputEditControl);
            gridColumn8.HeaderText = "预计到店日期";
            gridColumn8.Name = "ItemDate";
            gridColumn8.Width = 80;
            this.gridItem.PrimaryGrid.Columns.Add(gridColumn1);
            this.gridItem.PrimaryGrid.Columns.Add(gridColumn2);
            this.gridItem.PrimaryGrid.Columns.Add(gridColumn3);
            this.gridItem.PrimaryGrid.Columns.Add(gridColumn4);
            this.gridItem.PrimaryGrid.Columns.Add(gridColumn5);
            this.gridItem.PrimaryGrid.Columns.Add(gridColumn6);
            this.gridItem.PrimaryGrid.Columns.Add(gridColumn7);
            this.gridItem.PrimaryGrid.Columns.Add(gridColumn8);
            this.gridItem.PrimaryGrid.DefaultRowHeight = 80;
            this.gridItem.PrimaryGrid.MultiSelect = false;
            this.gridItem.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.gridItem.PrimaryGrid.ShowRowGridIndex = true;
            this.gridItem.PrimaryGrid.Title.RowHeaderVisibility = DevComponents.DotNetBar.SuperGrid.RowHeaderVisibility.PanelControlled;
            this.gridItem.PrimaryGrid.UseAlternateColumnStyle = true;
            this.gridItem.PrimaryGrid.UseAlternateRowStyle = true;
            this.gridItem.Size = new System.Drawing.Size(534, 400);
            this.gridItem.TabIndex = 3;
            this.gridItem.Text = "superGridControl1";
            // 
            // gpFinger
            // 
            this.gpFinger.BackColor = System.Drawing.Color.Transparent;
            this.gpFinger.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gpFinger.Controls.Add(this.pbFinger);
            this.gpFinger.Controls.Add(this.plFingerInfo);
            this.gpFinger.Dock = System.Windows.Forms.DockStyle.Right;
            this.gpFinger.Location = new System.Drawing.Point(534, 0);
            this.gpFinger.Name = "gpFinger";
            this.gpFinger.Size = new System.Drawing.Size(171, 400);
            // 
            // 
            // 
            this.gpFinger.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpFinger.Style.BackColorGradientAngle = 90;
            this.gpFinger.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpFinger.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpFinger.Style.BorderBottomWidth = 1;
            this.gpFinger.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpFinger.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpFinger.Style.BorderLeftWidth = 1;
            this.gpFinger.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpFinger.Style.BorderRightWidth = 1;
            this.gpFinger.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpFinger.Style.BorderTopWidth = 1;
            this.gpFinger.Style.CornerDiameter = 4;
            this.gpFinger.Style.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.gpFinger.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpFinger.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpFinger.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpFinger.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpFinger.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpFinger.TabIndex = 2;
            this.gpFinger.Text = "会员指纹信息";
            // 
            // pbFinger
            // 
            this.pbFinger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbFinger.Location = new System.Drawing.Point(0, 0);
            this.pbFinger.Name = "pbFinger";
            this.pbFinger.Size = new System.Drawing.Size(165, 213);
            this.pbFinger.TabIndex = 0;
            this.pbFinger.TabStop = false;
            // 
            // plFingerInfo
            // 
            this.plFingerInfo.CanvasColor = System.Drawing.SystemColors.Control;
            this.plFingerInfo.Controls.Add(this.lblFingerInfo);
            this.plFingerInfo.Controls.Add(this.plBtnReadFinger);
            this.plFingerInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plFingerInfo.Location = new System.Drawing.Point(0, 213);
            this.plFingerInfo.Name = "plFingerInfo";
            this.plFingerInfo.Size = new System.Drawing.Size(165, 163);
            // 
            // 
            // 
            this.plFingerInfo.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.plFingerInfo.Style.BackColorGradientAngle = 90;
            this.plFingerInfo.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.plFingerInfo.Style.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.Tile;
            this.plFingerInfo.Style.BorderBottomWidth = 1;
            this.plFingerInfo.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.plFingerInfo.Style.BorderLeftWidth = 1;
            this.plFingerInfo.Style.BorderRightWidth = 1;
            this.plFingerInfo.Style.BorderTopWidth = 1;
            this.plFingerInfo.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.plFingerInfo.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.plFingerInfo.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            // 
            // 
            // 
            this.plFingerInfo.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.plFingerInfo.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.plFingerInfo.TabIndex = 1;
            // 
            // lblFingerInfo
            // 
            this.lblFingerInfo.BackColor = System.Drawing.Color.Azure;
            // 
            // 
            // 
            this.lblFingerInfo.Border.Class = "TextBoxBorder";
            this.lblFingerInfo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblFingerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFingerInfo.ForeColor = System.Drawing.Color.Black;
            this.lblFingerInfo.Location = new System.Drawing.Point(0, 38);
            this.lblFingerInfo.Multiline = true;
            this.lblFingerInfo.Name = "lblFingerInfo";
            this.lblFingerInfo.ReadOnly = true;
            this.lblFingerInfo.Size = new System.Drawing.Size(165, 125);
            this.lblFingerInfo.TabIndex = 1;
            // 
            // plBtnReadFinger
            // 
            this.plBtnReadFinger.CanvasColor = System.Drawing.SystemColors.Control;
            this.plBtnReadFinger.Controls.Add(this.btnReadFinger);
            this.plBtnReadFinger.Dock = System.Windows.Forms.DockStyle.Top;
            this.plBtnReadFinger.Location = new System.Drawing.Point(0, 0);
            this.plBtnReadFinger.Name = "plBtnReadFinger";
            this.plBtnReadFinger.Size = new System.Drawing.Size(165, 38);
            // 
            // 
            // 
            this.plBtnReadFinger.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.plBtnReadFinger.Style.BackColorGradientAngle = 90;
            this.plBtnReadFinger.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.plBtnReadFinger.Style.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.Tile;
            this.plBtnReadFinger.Style.BorderBottomWidth = 1;
            this.plBtnReadFinger.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.plBtnReadFinger.Style.BorderLeftWidth = 1;
            this.plBtnReadFinger.Style.BorderRightWidth = 1;
            this.plBtnReadFinger.Style.BorderTopWidth = 1;
            this.plBtnReadFinger.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.plBtnReadFinger.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.plBtnReadFinger.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            // 
            // 
            // 
            this.plBtnReadFinger.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.plBtnReadFinger.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.plBtnReadFinger.TabIndex = 2;
            // 
            // btnReadFinger
            // 
            this.btnReadFinger.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReadFinger.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.btnReadFinger.Image = global::Eleooo.Client.MetroIcon.Stationery;
            this.btnReadFinger.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btnReadFinger.Location = new System.Drawing.Point(34, 6);
            this.btnReadFinger.Name = "btnReadFinger";
            this.btnReadFinger.Size = new System.Drawing.Size(110, 27);
            this.btnReadFinger.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.btnReadFinger.TabIndex = 1;
            this.btnReadFinger.Text = "读取指纹";
            this.btnReadFinger.Click += new System.EventHandler(this.btnReadFinger_Click);
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.plMain);
            this.mainContainer.Controls.Add(this.plHeader);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(705, 448);
            this.mainContainer.TabIndex = 1;
            this.mainContainer.Text = "slidePanel1";
            this.mainContainer.UsesBlockingAnimation = false;
            // 
            // UcCompanyItemOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainContainer);
            this.Name = "UcCompanyItemOrder";
            this.Size = new System.Drawing.Size(705, 448);
            this.Controls.SetChildIndex(this.mainContainer, 0);
            this.Controls.SetChildIndex(this.plTitle, 0);
            this.plTitle.ResumeLayout(false);
            this.plHeader.ResumeLayout(false);
            this.plMain.ResumeLayout(false);
            this.gpFinger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFinger)).EndInit( );
            this.plFingerInfo.ResumeLayout(false);
            this.plBtnReadFinger.ResumeLayout(false);
            this.mainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plHeader;
        private System.Windows.Forms.Panel plMain;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFinger;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPwd;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPhoneNum;
        private DevComponents.DotNetBar.LabelX lblPwd;
        private DevComponents.DotNetBar.LabelX lblPhoneNum;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.GroupPanel gpFinger;
        private System.Windows.Forms.PictureBox pbFinger;
        private DevComponents.DotNetBar.Ribbon.RibbonClientPanel plFingerInfo;
        private DevComponents.DotNetBar.Controls.TextBoxX lblFingerInfo;
        private DevComponents.DotNetBar.Ribbon.RibbonClientPanel plBtnReadFinger;
        private DevComponents.DotNetBar.ButtonX btnReadFinger;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl gridItem;
        private DevComponents.DotNetBar.Controls.SlidePanel mainContainer;
    }
}
