namespace Eleooo.Client
{
    partial class UcAreaCombox
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
            this.cbCity = new DevComponents.DotNetBar.Controls.ComboBoxEx( );
            this.cbArea = new DevComponents.DotNetBar.Controls.ComboBoxEx( );
            this.cbLocation = new DevComponents.DotNetBar.Controls.ComboBoxEx( );
            this.areaContainer = new DevComponents.DotNetBar.SuperTabStrip( );
            this.btnAdd = new DevComponents.DotNetBar.ButtonX( );
            ((System.ComponentModel.ISupportInitialize)(this.areaContainer)).BeginInit( );
            this.SuspendLayout( );
            // 
            // cbCity
            // 
            this.cbCity.DisplayMember = "Text";
            this.cbCity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbCity.DropDownHeight = 300;
            this.cbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCity.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbCity.FormattingEnabled = true;
            this.cbCity.IntegralHeight = false;
            this.cbCity.ItemHeight = 15;
            this.cbCity.Location = new System.Drawing.Point(0, 0);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(67, 21);
            this.cbCity.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbCity.TabIndex = 0;
            this.cbCity.SelectedIndexChanged += new System.EventHandler(this.cbCity_SelectedIndexChanged);
            // 
            // cbArea
            // 
            this.cbArea.DisplayMember = "Text";
            this.cbArea.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbArea.DropDownHeight = 300;
            this.cbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArea.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbArea.FormattingEnabled = true;
            this.cbArea.IntegralHeight = false;
            this.cbArea.ItemHeight = 15;
            this.cbArea.Location = new System.Drawing.Point(71, 0);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(75, 21);
            this.cbArea.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbArea.TabIndex = 1;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.cbArea_SelectedIndexChanged);
            // 
            // cbLocation
            // 
            this.cbLocation.DisplayMember = "Text";
            this.cbLocation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLocation.DropDownHeight = 300;
            this.cbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocation.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.IntegralHeight = false;
            this.cbLocation.ItemHeight = 15;
            this.cbLocation.Location = new System.Drawing.Point(150, 0);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(74, 21);
            this.cbLocation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbLocation.TabIndex = 2;
            this.cbLocation.SelectedIndexChanged += new System.EventHandler(this.cbLocation_SelectedIndexChanged);
            // 
            // areaContainer
            // 
            this.areaContainer.AutoSelectAttachedControl = false;
            this.areaContainer.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.areaContainer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.areaContainer.CloseButtonOnTabsVisible = true;
            this.areaContainer.ContainerControlProcessDialogKey = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.areaContainer.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.areaContainer.ControlBox.MenuBox.Name = "";
            this.areaContainer.ControlBox.Name = "";
            this.areaContainer.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.areaContainer.ControlBox.MenuBox,
            this.areaContainer.ControlBox.CloseBox});
            this.areaContainer.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.areaContainer.ForeColor = System.Drawing.Color.Black;
            this.areaContainer.Location = new System.Drawing.Point(4, 25);
            this.areaContainer.Name = "areaContainer";
            this.areaContainer.ReorderTabsEnabled = false;
            this.areaContainer.SelectedTabIndex = -1;
            this.areaContainer.Size = new System.Drawing.Size(277, 10);
            this.areaContainer.TabCloseButtonHot = null;
            this.areaContainer.TabFont = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.areaContainer.TabHorizontalSpacing = 0;
            this.areaContainer.TabIndex = 3;
            this.areaContainer.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
            this.areaContainer.TabVerticalSpacing = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(231, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(50, 21);
            this.btnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // UcAreaCombox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.areaContainer);
            this.Controls.Add(this.cbLocation);
            this.Controls.Add(this.cbArea);
            this.Controls.Add(this.cbCity);
            this.Name = "UcAreaCombox";
            this.Size = new System.Drawing.Size(284, 46);
            this.Load += new System.EventHandler(this.UcAreaCombox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.areaContainer)).EndInit( );
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCity;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbArea;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLocation;
        private DevComponents.DotNetBar.SuperTabStrip areaContainer;
        private DevComponents.DotNetBar.ButtonX btnAdd;
    }
}
