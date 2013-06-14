namespace Eleooo.Client
{
    partial class UserControlBase
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
            this.plTitle = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.btnSlide = new DevComponents.DotNetBar.ButtonX( );
            this.lblTitle = new DevComponents.DotNetBar.LabelX( );
            this.plTitle.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // plTitle
            // 
            this.plTitle.Controls.Add(this.btnSlide);
            this.plTitle.Controls.Add(this.lblTitle);
            this.plTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTitle.Location = new System.Drawing.Point(0, 0);
            this.plTitle.Name = "plTitle";
            this.plTitle.Padding = new System.Windows.Forms.Padding(2);
            this.plTitle.Size = new System.Drawing.Size(556, 32);
            this.plTitle.TabIndex = 0;
            this.plTitle.UsesBlockingAnimation = false;
            // 
            // btnSlide
            // 
            this.btnSlide.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSlide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSlide.ColorTable = DevComponents.DotNetBar.eButtonColor.Magenta;
            this.btnSlide.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSlide.Image = global::Eleooo.Client.MetroIcon.Follow;
            this.btnSlide.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.btnSlide.Location = new System.Drawing.Point(461, 2);
            this.btnSlide.Margin = new System.Windows.Forms.Padding(0);
            this.btnSlide.Name = "btnSlide";
            this.btnSlide.Size = new System.Drawing.Size(93, 26);
            this.btnSlide.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSlide.TabIndex = 2;
            this.btnSlide.Text = "返回";
            this.btnSlide.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            // 
            // lblTitle
            // 
            // 
            // 
            // 
            this.lblTitle.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblTitle.BackgroundStyle.BorderBottomColor = System.Drawing.Color.OrangeRed;
            this.lblTitle.BackgroundStyle.BorderBottomWidth = 2;
            this.lblTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblTitle.Location = new System.Drawing.Point(2, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(552, 28);
            this.lblTitle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "会员消费";
            // 
            // UserControlBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plTitle);
            this.Name = "UserControlBase";
            this.Size = new System.Drawing.Size(556, 384);
            this.plTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblTitle;
        public DevComponents.DotNetBar.ButtonX btnSlide;
        protected DevComponents.DotNetBar.Controls.SlidePanel plTitle;


    }
}
