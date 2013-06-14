namespace Eleooo.Client
{
    partial class UcDateCombox
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
            this.cbDate = new DevComponents.DotNetBar.Controls.ComboBoxEx( );
            this.lblMonth = new DevComponents.DotNetBar.LabelX( );
            this.lblYear = new DevComponents.DotNetBar.LabelX( );
            this.cbMonth = new DevComponents.DotNetBar.Controls.ComboBoxEx( );
            this.cbYear = new DevComponents.DotNetBar.Controls.ComboBoxEx( );
            this.lblDate = new DevComponents.DotNetBar.LabelX( );
            this.SuspendLayout( );
            // 
            // cbDate
            // 
            this.cbDate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDate.DisplayMember = "Text";
            this.cbDate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbDate.DropDownHeight = 200;
            this.cbDate.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbDate.FormatString = "N0";
            this.cbDate.FormattingEnabled = true;
            this.cbDate.IntegralHeight = false;
            this.cbDate.ItemHeight = 15;
            this.cbDate.Location = new System.Drawing.Point(185, 0);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(54, 21);
            this.cbDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbDate.TabIndex = 2;
            this.cbDate.TextChanged += new System.EventHandler(this.cbDate_TextChanged);
            // 
            // lblMonth
            // 
            // 
            // 
            // 
            this.lblMonth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMonth.Location = new System.Drawing.Point(164, 0);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(15, 21);
            this.lblMonth.TabIndex = 5;
            this.lblMonth.Text = "月";
            // 
            // lblYear
            // 
            // 
            // 
            // 
            this.lblYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblYear.Location = new System.Drawing.Point(80, 0);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(15, 21);
            this.lblYear.TabIndex = 6;
            this.lblYear.Text = "年";
            // 
            // cbMonth
            // 
            this.cbMonth.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbMonth.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMonth.DisplayMember = "Text";
            this.cbMonth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbMonth.DropDownHeight = 200;
            this.cbMonth.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbMonth.FormatString = "N0";
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.IntegralHeight = false;
            this.cbMonth.ItemHeight = 15;
            this.cbMonth.Location = new System.Drawing.Point(104, 0);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(52, 21);
            this.cbMonth.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbMonth.TabIndex = 1;
            this.cbMonth.TextChanged += new System.EventHandler(this.cbMonth_TextChanged);
            // 
            // cbYear
            // 
            this.cbYear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbYear.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbYear.DisplayMember = "Text";
            this.cbYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbYear.DropDownHeight = 200;
            this.cbYear.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbYear.FormatString = "N0";
            this.cbYear.FormattingEnabled = true;
            this.cbYear.IntegralHeight = false;
            this.cbYear.ItemHeight = 15;
            this.cbYear.Location = new System.Drawing.Point(0, 0);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(75, 21);
            this.cbYear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbYear.TabIndex = 0;
            this.cbYear.TextChanged += new System.EventHandler(this.cbYear_TextChanged);
            // 
            // lblDate
            // 
            // 
            // 
            // 
            this.lblDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDate.Location = new System.Drawing.Point(245, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(15, 21);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "日";
            // 
            // UcDateCombox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.cbMonth);
            this.Controls.Add(this.cbYear);
            this.Controls.Add(this.lblDate);
            this.Name = "UcDateCombox";
            this.Size = new System.Drawing.Size(275, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cbDate;
        private DevComponents.DotNetBar.LabelX lblMonth;
        private DevComponents.DotNetBar.LabelX lblYear;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbMonth;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbYear;
        private DevComponents.DotNetBar.LabelX lblDate;
    }
}
