namespace Eleooo.Client
{
    partial class FingerTest
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
            this.lblMessage = new DevComponents.DotNetBar.LabelX( );
            this.txtFingerText = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.pbFingerPic = new System.Windows.Forms.PictureBox( );
            this.btnReadFinger = new DevComponents.DotNetBar.ButtonX( );
            this.btnMatch = new DevComponents.DotNetBar.ButtonX( );
            this.btnCancel = new DevComponents.DotNetBar.ButtonX( );
            this.txtFingerTest = new DevComponents.DotNetBar.Controls.TextBoxX( );
            this.btnMatch2Fp = new DevComponents.DotNetBar.ButtonX( );
            this.btnSetTest = new DevComponents.DotNetBar.ButtonX( );
            this.btnGenChar = new DevComponents.DotNetBar.ButtonX( );
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerPic)).BeginInit( );
            this.SuspendLayout( );
            // 
            // lblMessage
            // 
            // 
            // 
            // 
            this.lblMessage.BackgroundStyle.Class = "";
            this.lblMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMessage.Location = new System.Drawing.Point(12, 42);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(192, 23);
            this.lblMessage.TabIndex = 1;
            // 
            // txtFingerText
            // 
            // 
            // 
            // 
            this.txtFingerText.Border.Class = "TextBoxBorder";
            this.txtFingerText.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFingerText.Location = new System.Drawing.Point(230, 71);
            this.txtFingerText.Multiline = true;
            this.txtFingerText.Name = "txtFingerText";
            this.txtFingerText.ReadOnly = true;
            this.txtFingerText.Size = new System.Drawing.Size(204, 80);
            this.txtFingerText.TabIndex = 3;
            // 
            // pbFingerPic
            // 
            this.pbFingerPic.Location = new System.Drawing.Point(12, 71);
            this.pbFingerPic.Name = "pbFingerPic";
            this.pbFingerPic.Size = new System.Drawing.Size(204, 170);
            this.pbFingerPic.TabIndex = 4;
            this.pbFingerPic.TabStop = false;
            // 
            // btnReadFinger
            // 
            this.btnReadFinger.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReadFinger.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReadFinger.Location = new System.Drawing.Point(12, 11);
            this.btnReadFinger.Name = "btnReadFinger";
            this.btnReadFinger.Size = new System.Drawing.Size(75, 23);
            this.btnReadFinger.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReadFinger.TabIndex = 6;
            this.btnReadFinger.Text = "Read";
            this.btnReadFinger.Click += new System.EventHandler(this.btnReadFinger_Click);
            // 
            // btnMatch
            // 
            this.btnMatch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMatch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMatch.Location = new System.Drawing.Point(246, 11);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(75, 23);
            this.btnMatch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMatch.TabIndex = 7;
            this.btnMatch.Text = "Match";
            this.btnMatch.Click += new System.EventHandler(this.btnMatch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(129, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtFingerTest
            // 
            // 
            // 
            // 
            this.txtFingerTest.Border.Class = "TextBoxBorder";
            this.txtFingerTest.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFingerTest.Location = new System.Drawing.Point(230, 161);
            this.txtFingerTest.Multiline = true;
            this.txtFingerTest.Name = "txtFingerTest";
            this.txtFingerTest.ReadOnly = true;
            this.txtFingerTest.Size = new System.Drawing.Size(204, 80);
            this.txtFingerTest.TabIndex = 9;
            // 
            // btnMatch2Fp
            // 
            this.btnMatch2Fp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMatch2Fp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMatch2Fp.Location = new System.Drawing.Point(345, 11);
            this.btnMatch2Fp.Name = "btnMatch2Fp";
            this.btnMatch2Fp.Size = new System.Drawing.Size(75, 23);
            this.btnMatch2Fp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMatch2Fp.TabIndex = 10;
            this.btnMatch2Fp.Text = "Match2Fp";
            this.btnMatch2Fp.Click += new System.EventHandler(this.btnMatch2Fp_Click);
            // 
            // btnSetTest
            // 
            this.btnSetTest.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSetTest.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSetTest.Location = new System.Drawing.Point(345, 41);
            this.btnSetTest.Name = "btnSetTest";
            this.btnSetTest.Size = new System.Drawing.Size(75, 23);
            this.btnSetTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSetTest.TabIndex = 11;
            this.btnSetTest.Text = "SetTest";
            this.btnSetTest.Click += new System.EventHandler(this.btnSetTest_Click);
            // 
            // btnGenChar
            // 
            this.btnGenChar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGenChar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGenChar.Location = new System.Drawing.Point(246, 42);
            this.btnGenChar.Name = "btnGenChar";
            this.btnGenChar.Size = new System.Drawing.Size(75, 23);
            this.btnGenChar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGenChar.TabIndex = 12;
            this.btnGenChar.Text = "GenChar";
            this.btnGenChar.Click += new System.EventHandler(this.btnGenChar_Click);
            // 
            // FingerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 253);
            this.Controls.Add(this.btnGenChar);
            this.Controls.Add(this.btnSetTest);
            this.Controls.Add(this.btnMatch2Fp);
            this.Controls.Add(this.txtFingerTest);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnMatch);
            this.Controls.Add(this.btnReadFinger);
            this.Controls.Add(this.pbFingerPic);
            this.Controls.Add(this.txtFingerText);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FingerTest";
            this.Text = "FingerTest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FingerTest_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerPic)).EndInit( );
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblMessage;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFingerText;
        private System.Windows.Forms.PictureBox pbFingerPic;
        private DevComponents.DotNetBar.ButtonX btnReadFinger;
        private DevComponents.DotNetBar.ButtonX btnMatch;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFingerTest;
        private DevComponents.DotNetBar.ButtonX btnMatch2Fp;
        private DevComponents.DotNetBar.ButtonX btnSetTest;
        private DevComponents.DotNetBar.ButtonX btnGenChar;
    }
}