namespace SmoothScrollGUI;

partial class AboutForm
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label smoothScrollLabel;
    private System.Windows.Forms.Label versionLabel;
    private System.Windows.Forms.Button closeButton;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.smoothScrollLabel = new System.Windows.Forms.Label();
        this.versionLabel = new System.Windows.Forms.Label();
        this.closeButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.SuspendLayout();
        //
        // pictureBox1
        //
        this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.pictureBox1.BackgroundImage = global::SmoothScrollGUI.Properties.Resources.SSicon2;
        this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.pictureBox1.Location = new System.Drawing.Point(24, 19);
        this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(60, 62);
        this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.pictureBox1.TabIndex = 0;
        this.pictureBox1.TabStop = false;
        //
        // smoothScrollLabel
        //
        this.smoothScrollLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.smoothScrollLabel.AutoSize = true;
        this.smoothScrollLabel.Font = new System.Drawing.Font("Segoe UI", 16.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.smoothScrollLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
        this.smoothScrollLabel.Location = new System.Drawing.Point(92, 19);
        this.smoothScrollLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.smoothScrollLabel.Name = "smoothScrollLabel";
        this.smoothScrollLabel.Size = new System.Drawing.Size(160, 31);
        this.smoothScrollLabel.TabIndex = 15;
        this.smoothScrollLabel.Text = "SmoothScroll";
        //
        // versionLabel
        //
        this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.versionLabel.AutoSize = true;
        this.versionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.versionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
        this.versionLabel.Location = new System.Drawing.Point(135, 48);
        this.versionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 0, 10);
        this.versionLabel.Name = "versionLabel";
        this.versionLabel.Size = new System.Drawing.Size(72, 15);
        this.versionLabel.TabIndex = 16;
        this.versionLabel.Text = "Version x.x.x";
        this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // closeButton
        //
        this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.closeButton.Location = new System.Drawing.Point(239, 98);
        this.closeButton.Name = "closeButton";
        this.closeButton.Size = new System.Drawing.Size(75, 27);
        this.closeButton.TabIndex = 17;
        this.closeButton.Text = "Close";
        this.closeButton.UseVisualStyleBackColor = true;
        this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
        //
        // AboutForm
        //
        this.AcceptButton = this.closeButton;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.closeButton;
        this.ClientSize = new System.Drawing.Size(328, 140);
        this.Controls.Add(this.closeButton);
        this.Controls.Add(this.versionLabel);
        this.Controls.Add(this.smoothScrollLabel);
        this.Controls.Add(this.pictureBox1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "AboutForm";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "About SmoothScroll";
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
