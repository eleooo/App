namespace Eleooo.Client
{
    partial class MainMetroForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose( );
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.components = new System.ComponentModel.Container( );
            this.metroTitle = new DevComponents.DotNetBar.Metro.MetroShell( );
            this.btnTheme = new Eleooo.Client.BindingButtonItem( );
            this.style = new DevComponents.DotNetBar.StyleManager(this.components);
            this.SuspendLayout( );
            // 
            // metroTitle
            // 
            this.metroTitle.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.metroTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroTitle.CaptionFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.metroTitle.CaptionVisible = true;
            this.metroTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroTitle.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroTitle.ForeColor = System.Drawing.Color.Black;
            this.metroTitle.HelpButtonText = "收起";
            this.metroTitle.KeyTipsFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroTitle.Location = new System.Drawing.Point(0, 1);
            this.metroTitle.Name = "metroTitle";
            this.metroTitle.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnTheme});
            this.metroTitle.SettingsButtonVisible = false;
            this.metroTitle.Size = new System.Drawing.Size(736, 35);
            this.metroTitle.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.metroTitle.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.metroTitle.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.metroTitle.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.metroTitle.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.metroTitle.SystemText.QatDialogAddButton = "&Add >>";
            this.metroTitle.SystemText.QatDialogCancelButton = "Cancel";
            this.metroTitle.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.metroTitle.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.metroTitle.SystemText.QatDialogOkButton = "OK";
            this.metroTitle.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.metroTitle.SystemText.QatDialogRemoveButton = "&Remove";
            this.metroTitle.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.metroTitle.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.metroTitle.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.metroTitle.TabIndex = 0;
            this.metroTitle.TabStripFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.metroTitle.Text = "乐多分";
            this.metroTitle.TitleText = "乐多分客户端程序";
            this.metroTitle.SettingsButtonClick += new System.EventHandler(this.metroTitle_SettingsButtonClick);
            this.metroTitle.HelpButtonClick += new System.EventHandler(this.metroTitle_HelpButtonClick);
            // 
            // btnTheme
            // 
            this.btnTheme.AutoExpandOnClick = true;
            this.btnTheme.Name = "btnTheme";
            this.btnTheme.SelectedIndex = -1;
            this.btnTheme.SelectedItem = null;
            this.btnTheme.Text = "切换主题";
            // 
            // style
            // 
            this.style.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.style.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
            // 
            // MainMetroForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 530);
            this.Controls.Add(this.metroTitle);
            this.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "MainMetroForm";
            this.Padding = new System.Windows.Forms.Padding(0, 1, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DotNetBar Metro App Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMetroForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainMetroForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager style;
        private BindingButtonItem btnTheme;
        private DevComponents.DotNetBar.Metro.MetroShell metroTitle;

    }
}

