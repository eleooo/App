namespace Eleooo.Client
{
    partial class UcCompanyInfo
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
            this.plCompanyPic = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.picCompany = new System.Windows.Forms.PictureBox( );
            this.plBottom = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.labelX2 = new DevComponents.DotNetBar.LabelX( );
            this.plButton = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.btnChangePic = new DevComponents.DotNetBar.ButtonX( );
            this.bsCompany = new System.Windows.Forms.BindingSource(this.components);
            this.dlgCompanyPic = new System.Windows.Forms.OpenFileDialog( );
            this.mainContainer = new DevComponents.DotNetBar.Controls.SlidePanel( );
            this.plMain = new System.Windows.Forms.Panel( );
            this.lblTel = new DevComponents.DotNetBar.LabelX( );
            this.txtTel = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.labelX1 = new DevComponents.DotNetBar.LabelX( );
            this.lblName = new DevComponents.DotNetBar.LabelX( );
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.lblPhone = new DevComponents.DotNetBar.LabelX( );
            this.txtItem = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.txtPhone = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.lblCompanyItem = new DevComponents.DotNetBar.LabelX( );
            this.lblEmail = new DevComponents.DotNetBar.LabelX( );
            this.txtLocation = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.txtEmail = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.lblLocation = new DevComponents.DotNetBar.LabelX( );
            this.txtIntro = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.btnSave = new DevComponents.DotNetBar.ButtonX( );
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.labelX3 = new DevComponents.DotNetBar.LabelX( );
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.labelX4 = new DevComponents.DotNetBar.LabelX( );
            this.textBoxX3 = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.labelX5 = new DevComponents.DotNetBar.LabelX( );
            this.plTitle.SuspendLayout( );
            this.plCompanyPic.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.picCompany)).BeginInit( );
            this.plBottom.SuspendLayout( );
            this.plButton.SuspendLayout( );
            ((System.ComponentModel.ISupportInitialize)(this.bsCompany)).BeginInit( );
            this.mainContainer.SuspendLayout( );
            this.plMain.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // plCompanyPic
            // 
            this.plCompanyPic.Controls.Add(this.picCompany);
            this.plCompanyPic.Controls.Add(this.plBottom);
            this.plCompanyPic.Dock = System.Windows.Forms.DockStyle.Right;
            this.plCompanyPic.Location = new System.Drawing.Point(408, 0);
            this.plCompanyPic.Name = "plCompanyPic";
            this.plCompanyPic.Size = new System.Drawing.Size(269, 499);
            this.plCompanyPic.TabIndex = 0;
            this.plCompanyPic.UsesBlockingAnimation = false;
            // 
            // picCompany
            // 
            this.picCompany.BackColor = System.Drawing.Color.Transparent;
            this.picCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCompany.Location = new System.Drawing.Point(0, 0);
            this.picCompany.Name = "picCompany";
            this.picCompany.Size = new System.Drawing.Size(269, 315);
            this.picCompany.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCompany.TabIndex = 1;
            this.picCompany.TabStop = false;
            // 
            // plBottom
            // 
            this.plBottom.Controls.Add(this.labelX2);
            this.plBottom.Controls.Add(this.plButton);
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Location = new System.Drawing.Point(0, 315);
            this.plBottom.Name = "plBottom";
            this.plBottom.Size = new System.Drawing.Size(269, 184);
            this.plBottom.TabIndex = 0;
            this.plBottom.UsesBlockingAnimation = false;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelX2.Location = new System.Drawing.Point(0, 45);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(269, 46);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "允许上传的类型为:.bmp.jpg.jpeg.png.gif,且小于2M的文件.";
            this.labelX2.TextLineAlignment = System.Drawing.StringAlignment.Near;
            this.labelX2.WordWrap = true;
            // 
            // plButton
            // 
            this.plButton.Controls.Add(this.btnChangePic);
            this.plButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.plButton.Location = new System.Drawing.Point(0, 0);
            this.plButton.Name = "plButton";
            this.plButton.Size = new System.Drawing.Size(269, 45);
            this.plButton.TabIndex = 0;
            this.plButton.UsesBlockingAnimation = false;
            // 
            // btnChangePic
            // 
            this.btnChangePic.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChangePic.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChangePic.Image = global::Eleooo.Client.MetroIcon.MarkAsPaid;
            this.btnChangePic.Location = new System.Drawing.Point(82, 7);
            this.btnChangePic.Name = "btnChangePic";
            this.btnChangePic.Size = new System.Drawing.Size(102, 29);
            this.btnChangePic.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChangePic.TabIndex = 0;
            this.btnChangePic.Text = "选择图片";
            this.btnChangePic.Click += new System.EventHandler(this.btnChangePic_Click);
            // 
            // bsCompany
            // 
            this.bsCompany.DataMember = "Company";
            this.bsCompany.DataSource = typeof(Eleooo.Client.UcCompanyInfo);
            // 
            // dlgCompanyPic
            // 
            this.dlgCompanyPic.Filter = "jpg文件(*.jpg)|*.jpg|jpeg文件(*.jpeg)|*.jpeg|gif文件(*.gif)|*.gif|bmp文件(*.bmp)|*.bmp|pn" +
                "g文件(*.png)|*.png";
            this.dlgCompanyPic.RestoreDirectory = true;
            this.dlgCompanyPic.Title = "请选择图片文件";
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.plMain);
            this.mainContainer.Controls.Add(this.plCompanyPic);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(677, 499);
            this.mainContainer.TabIndex = 1;
            this.mainContainer.Text = "slidePanel1";
            this.mainContainer.UsesBlockingAnimation = false;
            // 
            // plMain
            // 
            this.plMain.BackColor = System.Drawing.Color.Transparent;
            this.plMain.Controls.Add(this.textBoxX3);
            this.plMain.Controls.Add(this.labelX5);
            this.plMain.Controls.Add(this.textBoxX2);
            this.plMain.Controls.Add(this.labelX4);
            this.plMain.Controls.Add(this.textBoxX1);
            this.plMain.Controls.Add(this.labelX3);
            this.plMain.Controls.Add(this.lblTel);
            this.plMain.Controls.Add(this.txtTel);
            this.plMain.Controls.Add(this.labelX1);
            this.plMain.Controls.Add(this.lblName);
            this.plMain.Controls.Add(this.txtName);
            this.plMain.Controls.Add(this.lblPhone);
            this.plMain.Controls.Add(this.txtItem);
            this.plMain.Controls.Add(this.txtPhone);
            this.plMain.Controls.Add(this.lblCompanyItem);
            this.plMain.Controls.Add(this.lblEmail);
            this.plMain.Controls.Add(this.txtLocation);
            this.plMain.Controls.Add(this.txtEmail);
            this.plMain.Controls.Add(this.lblLocation);
            this.plMain.Controls.Add(this.txtIntro);
            this.plMain.Controls.Add(this.btnSave);
            this.plMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMain.Location = new System.Drawing.Point(0, 0);
            this.plMain.Name = "plMain";
            this.plMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.plMain.Size = new System.Drawing.Size(408, 499);
            this.plMain.TabIndex = 17;
            // 
            // lblTel
            // 
            // 
            // 
            // 
            this.lblTel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTel.Location = new System.Drawing.Point(3, 3);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(75, 23);
            this.lblTel.TabIndex = 0;
            this.lblTel.Text = "登录账号:";
            // 
            // txtTel
            // 
            this.txtTel.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTel.Border.Class = "TextBoxBorder";
            this.txtTel.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCompany, "CompanyTel", true));
            this.txtTel.Enabled = false;
            this.txtTel.ForeColor = System.Drawing.Color.Black;
            this.txtTel.Location = new System.Drawing.Point(84, 3);
            this.txtTel.Name = "txtTel";
            this.txtTel.ReadOnly = true;
            this.txtTel.Size = new System.Drawing.Size(225, 21);
            this.txtTel.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(3, 265);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 12;
            this.labelX1.Text = "公交线路:";
            // 
            // lblName
            // 
            // 
            // 
            // 
            this.lblName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblName.Location = new System.Drawing.Point(3, 34);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(75, 18);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "商家名称:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCompany, "CompanyName", true));
            this.txtName.Enabled = false;
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(84, 32);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(225, 21);
            this.txtName.TabIndex = 3;
            // 
            // lblPhone
            // 
            // 
            // 
            // 
            this.lblPhone.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPhone.Location = new System.Drawing.Point(3, 63);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(75, 23);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "联系电话:";
            // 
            // txtItem
            // 
            this.txtItem.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtItem.Border.Class = "TextBoxBorder";
            this.txtItem.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtItem.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCompany, "CompanyMemo", true));
            this.txtItem.Enabled = false;
            this.txtItem.ForeColor = System.Drawing.Color.Black;
            this.txtItem.Location = new System.Drawing.Point(84, 157);
            this.txtItem.Name = "txtItem";
            this.txtItem.ReadOnly = true;
            this.txtItem.Size = new System.Drawing.Size(225, 21);
            this.txtItem.TabIndex = 11;
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPhone.Border.Class = "TextBoxBorder";
            this.txtPhone.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCompany, "CompanyPhone", true));
            this.txtPhone.ForeColor = System.Drawing.Color.Black;
            this.txtPhone.Location = new System.Drawing.Point(84, 63);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(225, 21);
            this.txtPhone.TabIndex = 5;
            // 
            // lblCompanyItem
            // 
            // 
            // 
            // 
            this.lblCompanyItem.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCompanyItem.Location = new System.Drawing.Point(3, 157);
            this.lblCompanyItem.Name = "lblCompanyItem";
            this.lblCompanyItem.Size = new System.Drawing.Size(75, 23);
            this.lblCompanyItem.TabIndex = 10;
            this.lblCompanyItem.Text = "商家类型:";
            // 
            // lblEmail
            // 
            // 
            // 
            // 
            this.lblEmail.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblEmail.Location = new System.Drawing.Point(3, 94);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(75, 23);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "电子邮箱:";
            // 
            // txtLocation
            // 
            this.txtLocation.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtLocation.Border.Class = "TextBoxBorder";
            this.txtLocation.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLocation.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCompany, "CompanyAddress", true));
            this.txtLocation.ForeColor = System.Drawing.Color.Black;
            this.txtLocation.Location = new System.Drawing.Point(84, 124);
            this.txtLocation.MaxLength = 50;
            this.txtLocation.Multiline = true;
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(225, 23);
            this.txtLocation.TabIndex = 9;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtEmail.Border.Class = "TextBoxBorder";
            this.txtEmail.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEmail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCompany, "CompanyEmail", true));
            this.txtEmail.ForeColor = System.Drawing.Color.Black;
            this.txtEmail.Location = new System.Drawing.Point(84, 94);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(225, 21);
            this.txtEmail.TabIndex = 7;
            // 
            // lblLocation
            // 
            // 
            // 
            // 
            this.lblLocation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblLocation.Location = new System.Drawing.Point(3, 124);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(75, 23);
            this.lblLocation.TabIndex = 8;
            this.lblLocation.Text = "商家地址:";
            // 
            // txtIntro
            // 
            this.txtIntro.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtIntro.Border.Class = "TextBoxBorder";
            this.txtIntro.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtIntro.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCompany, "CompanyIntro", true));
            this.txtIntro.ForeColor = System.Drawing.Color.Black;
            this.txtIntro.Location = new System.Drawing.Point(84, 265);
            this.txtIntro.MaxLength = 250;
            this.txtIntro.Multiline = true;
            this.txtIntro.Name = "txtIntro";
            this.txtIntro.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIntro.Size = new System.Drawing.Size(225, 91);
            this.txtIntro.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.MagentaWithBackground;
            this.btnSave.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Image = global::Eleooo.Client.MetroIcon.CheckMark;
            this.btnSave.Location = new System.Drawing.Point(199, 363);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 35);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBoxX1
            // 
            this.textBoxX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCompany, "CompanyItem", true));
            this.textBoxX1.Enabled = false;
            this.textBoxX1.ForeColor = System.Drawing.Color.Black;
            this.textBoxX1.Location = new System.Drawing.Point(84, 184);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.ReadOnly = true;
            this.textBoxX1.Size = new System.Drawing.Size(225, 21);
            this.textBoxX1.TabIndex = 16;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(3, 184);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(75, 23);
            this.labelX3.TabIndex = 15;
            this.labelX3.Text = "商家描述:";
            // 
            // textBoxX2
            // 
            this.textBoxX2.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCompany, "CompanyWorkTime", true));
            this.textBoxX2.Enabled = false;
            this.textBoxX2.ForeColor = System.Drawing.Color.Black;
            this.textBoxX2.Location = new System.Drawing.Point(84, 211);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.ReadOnly = true;
            this.textBoxX2.Size = new System.Drawing.Size(225, 21);
            this.textBoxX2.TabIndex = 18;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(3, 211);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(75, 23);
            this.labelX4.TabIndex = 17;
            this.labelX4.Text = "营业时间:";
            // 
            // textBoxX3
            // 
            this.textBoxX3.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxX3.Border.Class = "TextBoxBorder";
            this.textBoxX3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCompany, "CompanyServices", true));
            this.textBoxX3.Enabled = false;
            this.textBoxX3.ForeColor = System.Drawing.Color.Black;
            this.textBoxX3.Location = new System.Drawing.Point(84, 238);
            this.textBoxX3.Name = "textBoxX3";
            this.textBoxX3.ReadOnly = true;
            this.textBoxX3.Size = new System.Drawing.Size(225, 21);
            this.textBoxX3.TabIndex = 20;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(3, 238);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(75, 23);
            this.labelX5.TabIndex = 19;
            this.labelX5.Text = "特色服务:";
            // 
            // UcCompanyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainContainer);
            this.Name = "UcCompanyInfo";
            this.Size = new System.Drawing.Size(677, 499);
            this.Controls.SetChildIndex(this.plTitle, 0);
            this.Controls.SetChildIndex(this.mainContainer, 0);
            this.plTitle.ResumeLayout(false);
            this.plCompanyPic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCompany)).EndInit( );
            this.plBottom.ResumeLayout(false);
            this.plButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsCompany)).EndInit( );
            this.mainContainer.ResumeLayout(false);
            this.plMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private DevComponents.DotNetBar.Controls.SlidePanel plCompanyPic;
        private DevComponents.DotNetBar.Controls.SlidePanel plBottom;
        private DevComponents.DotNetBar.Controls.SlidePanel plButton;
        private DevComponents.DotNetBar.ButtonX btnChangePic;
        private System.Windows.Forms.OpenFileDialog dlgCompanyPic;
        private System.Windows.Forms.BindingSource bsCompany;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.SlidePanel mainContainer;
        private System.Windows.Forms.Panel plMain;
        private DevComponents.DotNetBar.LabelX lblTel;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTel;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX lblName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.LabelX lblPhone;
        private DevComponents.DotNetBar.Controls.TextBoxX txtItem;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPhone;
        private DevComponents.DotNetBar.LabelX lblCompanyItem;
        private DevComponents.DotNetBar.LabelX lblEmail;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLocation;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEmail;
        private DevComponents.DotNetBar.LabelX lblLocation;
        private DevComponents.DotNetBar.Controls.TextBoxX txtIntro;
        private DevComponents.DotNetBar.ButtonX btnSave;
        internal System.Windows.Forms.PictureBox picCompany;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX3;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.LabelX labelX3;
    }
}
