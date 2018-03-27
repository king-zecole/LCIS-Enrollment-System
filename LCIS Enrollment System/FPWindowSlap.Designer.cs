namespace LCIS_Enrollment_System
{
    partial class FPwindowSlap
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
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FingerprintImage = new System.Windows.Forms.PictureBox();
            this.btnFpOpen = new System.Windows.Forms.Button();
            this.btnFpClose = new System.Windows.Forms.Button();
            this.btnCaptureLeft = new System.Windows.Forms.Button();
            this.btnCaptureRight = new System.Windows.Forms.Button();
            this.btnCaptureThumb = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnFpExit = new System.Windows.Forms.Button();
            this.StatusBox = new System.Windows.Forms.ListBox();
            this.LeftImage = new System.Windows.Forms.PictureBox();
            this.RightImage = new System.Windows.Forms.PictureBox();
            this.ThumbImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.FingerprintImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThumbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // FingerprintImage
            // 
            this.FingerprintImage.BackColor = System.Drawing.Color.Transparent;
            this.FingerprintImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FingerprintImage.Location = new System.Drawing.Point(32, 21);
            this.FingerprintImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FingerprintImage.Name = "FingerprintImage";
            this.FingerprintImage.Size = new System.Drawing.Size(512, 406);
            this.FingerprintImage.TabIndex = 0;
            this.FingerprintImage.TabStop = false;
            // 
            // btnFpOpen
            // 
            this.btnFpOpen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFpOpen.Location = new System.Drawing.Point(676, 32);
            this.btnFpOpen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFpOpen.Name = "btnFpOpen";
            this.btnFpOpen.Size = new System.Drawing.Size(140, 36);
            this.btnFpOpen.TabIndex = 1;
            this.btnFpOpen.Text = "Connect";
            this.btnFpOpen.UseVisualStyleBackColor = true;
            this.btnFpOpen.Click += new System.EventHandler(this.btnFpOpen_Click);
            // 
            // btnFpClose
            // 
            this.btnFpClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFpClose.Location = new System.Drawing.Point(676, 93);
            this.btnFpClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFpClose.Name = "btnFpClose";
            this.btnFpClose.Size = new System.Drawing.Size(140, 36);
            this.btnFpClose.TabIndex = 2;
            this.btnFpClose.Text = "Disconnect";
            this.btnFpClose.UseVisualStyleBackColor = true;
            this.btnFpClose.Click += new System.EventHandler(this.btnFpClose_Click);
            // 
            // btnCaptureLeft
            // 
            this.btnCaptureLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCaptureLeft.Location = new System.Drawing.Point(18, 449);
            this.btnCaptureLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCaptureLeft.Name = "btnCaptureLeft";
            this.btnCaptureLeft.Size = new System.Drawing.Size(105, 33);
            this.btnCaptureLeft.TabIndex = 3;
            this.btnCaptureLeft.Text = "4 Left Capture";
            this.btnCaptureLeft.UseVisualStyleBackColor = true;
            this.btnCaptureLeft.Click += new System.EventHandler(this.btnCaptureLeft_Click);
            // 
            // btnCaptureRight
            // 
            this.btnCaptureRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCaptureRight.Location = new System.Drawing.Point(143, 449);
            this.btnCaptureRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCaptureRight.Name = "btnCaptureRight";
            this.btnCaptureRight.Size = new System.Drawing.Size(117, 33);
            this.btnCaptureRight.TabIndex = 4;
            this.btnCaptureRight.Text = "4 Right Capture";
            this.btnCaptureRight.UseVisualStyleBackColor = true;
            this.btnCaptureRight.Click += new System.EventHandler(this.btnCaptureRight_Click);
            // 
            // btnCaptureThumb
            // 
            this.btnCaptureThumb.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCaptureThumb.Location = new System.Drawing.Point(276, 449);
            this.btnCaptureThumb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCaptureThumb.Name = "btnCaptureThumb";
            this.btnCaptureThumb.Size = new System.Drawing.Size(120, 33);
            this.btnCaptureThumb.TabIndex = 5;
            this.btnCaptureThumb.Text = "2 Thumb Capture";
            this.btnCaptureThumb.UseVisualStyleBackColor = true;
            this.btnCaptureThumb.Click += new System.EventHandler(this.btnCaptureThumb_Click);
            // 
            // btnsave
            // 
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnsave.Location = new System.Drawing.Point(421, 449);
            this.btnsave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(135, 33);
            this.btnsave.TabIndex = 6;
            this.btnsave.Text = "Save && Submit";
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnFpExit
            // 
            this.btnFpExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFpExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFpExit.Location = new System.Drawing.Point(676, 155);
            this.btnFpExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFpExit.Name = "btnFpExit";
            this.btnFpExit.Size = new System.Drawing.Size(140, 36);
            this.btnFpExit.TabIndex = 7;
            this.btnFpExit.Text = "Back";
            this.btnFpExit.UseVisualStyleBackColor = true;
            this.btnFpExit.Click += new System.EventHandler(this.btnFpExit_Click);
            // 
            // StatusBox
            // 
            this.StatusBox.BackColor = System.Drawing.Color.DarkSlateGray;
            this.StatusBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusBox.ForeColor = System.Drawing.Color.Yellow;
            this.StatusBox.FormattingEnabled = true;
            this.StatusBox.HorizontalScrollbar = true;
            this.StatusBox.Location = new System.Drawing.Point(550, 331);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.ScrollAlwaysVisible = true;
            this.StatusBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.StatusBox.Size = new System.Drawing.Size(288, 95);
            this.StatusBox.TabIndex = 8;
            // 
            // LeftImage
            // 
            this.LeftImage.BackColor = System.Drawing.Color.Transparent;
            this.LeftImage.Location = new System.Drawing.Point(554, 56);
            this.LeftImage.Name = "LeftImage";
            this.LeftImage.Size = new System.Drawing.Size(106, 113);
            this.LeftImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LeftImage.TabIndex = 9;
            this.LeftImage.TabStop = false;
            // 
            // RightImage
            // 
            this.RightImage.BackColor = System.Drawing.Color.Transparent;
            this.RightImage.Location = new System.Drawing.Point(555, 205);
            this.RightImage.Name = "RightImage";
            this.RightImage.Size = new System.Drawing.Size(116, 113);
            this.RightImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RightImage.TabIndex = 10;
            this.RightImage.TabStop = false;
            // 
            // ThumbImage
            // 
            this.ThumbImage.BackColor = System.Drawing.Color.Transparent;
            this.ThumbImage.Location = new System.Drawing.Point(700, 205);
            this.ThumbImage.Name = "ThumbImage";
            this.ThumbImage.Size = new System.Drawing.Size(116, 113);
            this.ThumbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ThumbImage.TabIndex = 11;
            this.ThumbImage.TabStop = false;
            // 
            // FPwindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::LCIS_Enrollment_System.Properties.Resources.Bgwhite;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnFpExit;
            this.ClientSize = new System.Drawing.Size(842, 523);
            this.Controls.Add(this.ThumbImage);
            this.Controls.Add(this.RightImage);
            this.Controls.Add(this.LeftImage);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.btnFpExit);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.btnCaptureThumb);
            this.Controls.Add(this.btnCaptureRight);
            this.Controls.Add(this.btnCaptureLeft);
            this.Controls.Add(this.btnFpClose);
            this.Controls.Add(this.btnFpOpen);
            this.Controls.Add(this.FingerprintImage);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FPwindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FPwindow";
            this.Load += new System.EventHandler(this.FPwindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FingerprintImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThumbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox FingerprintImage;
        private System.Windows.Forms.Button btnFpOpen;
        private System.Windows.Forms.Button btnFpClose;
        private System.Windows.Forms.Button btnCaptureLeft;
        private System.Windows.Forms.Button btnCaptureRight;
        private System.Windows.Forms.Button btnCaptureThumb;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnFpExit;
        private System.Windows.Forms.ListBox StatusBox;
        private System.Windows.Forms.PictureBox LeftImage;
        private System.Windows.Forms.PictureBox RightImage;
        private System.Windows.Forms.PictureBox ThumbImage;
    }
}