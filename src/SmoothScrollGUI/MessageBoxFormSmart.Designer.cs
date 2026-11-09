namespace SmoothScrollGUI;

partial class MessageBoxFormSmart
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Panel panelBottom;
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.CheckBox dontShowCheck;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Panel panelFlowWrapper;
    private System.Windows.Forms.Label detailLabel;
    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Panel panelContent;

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxFormSmart));
        this.panelBottom = new System.Windows.Forms.Panel();
        this.cancelButton = new System.Windows.Forms.Button();
        this.acceptButton = new System.Windows.Forms.Button();
        this.dontShowCheck = new System.Windows.Forms.CheckBox();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.panelFlowWrapper = new System.Windows.Forms.Panel();
        this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
        this.panelContent = new System.Windows.Forms.Panel();
        this.detailLabel = new System.Windows.Forms.Label();
        this.titleLabel = new System.Windows.Forms.Label();
        this.panelBottom.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.panelFlowWrapper.SuspendLayout();
        this.flowLayoutPanel1.SuspendLayout();
        this.panelContent.SuspendLayout();
        this.SuspendLayout();
        //
        // panelBottom
        //
        this.panelBottom.BackColor = System.Drawing.SystemColors.Control;
        this.panelBottom.Controls.Add(this.cancelButton);
        this.panelBottom.Controls.Add(this.acceptButton);
        this.panelBottom.Controls.Add(this.dontShowCheck);
        this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.panelBottom.Location = new System.Drawing.Point(0, 82);
        this.panelBottom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.panelBottom.Name = "panelBottom";
        this.panelBottom.Size = new System.Drawing.Size(362, 45);
        this.panelBottom.TabIndex = 0;
        this.panelBottom.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBottom_Paint);
        //
        // cancelButton
        //
        this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Right));
        this.cancelButton.AutoSize = true;
        this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.cancelButton.Location = new System.Drawing.Point(287, 11);
        this.cancelButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
        this.cancelButton.Size = new System.Drawing.Size(63, 25);
        this.cancelButton.TabIndex = 1;
        this.cancelButton.Text = "No";
        this.cancelButton.UseVisualStyleBackColor = true;
        //
        // acceptButton
        //
        this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Right));
        this.acceptButton.AutoSize = true;
        this.acceptButton.Location = new System.Drawing.Point(213, 11);
        this.acceptButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.acceptButton.Name = "acceptButton";
        this.acceptButton.Size = new System.Drawing.Size(63, 25);
        this.acceptButton.TabIndex = 0;
        this.acceptButton.Text = "Yes";
        this.acceptButton.UseVisualStyleBackColor = true;
        this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
        //
        // dontShowCheck
        //
        this.dontShowCheck.AutoSize = true;
        this.dontShowCheck.Location = new System.Drawing.Point(83, 14);
        this.dontShowCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.dontShowCheck.Name = "dontShowCheck";
        this.dontShowCheck.Size = new System.Drawing.Size(118, 19);
        this.dontShowCheck.TabIndex = 2;
        this.dontShowCheck.Text = "Don't show again";
        this.dontShowCheck.UseVisualStyleBackColor = true;
        //
        // pictureBox1
        //
        this.pictureBox1.Location = new System.Drawing.Point(15, 15);
        this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(50, 50);
        this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.pictureBox1.TabIndex = 1;
        this.pictureBox1.TabStop = false;
        //
        // panelFlowWrapper
        //
        this.panelFlowWrapper.AutoSize = true;
        this.panelFlowWrapper.Controls.Add(this.flowLayoutPanel1);
        this.panelFlowWrapper.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelFlowWrapper.Location = new System.Drawing.Point(0, 0);
        this.panelFlowWrapper.Margin = new System.Windows.Forms.Padding(0);
        this.panelFlowWrapper.Name = "panelFlowWrapper";
        this.panelFlowWrapper.Size = new System.Drawing.Size(362, 80);
        this.panelFlowWrapper.TabIndex = 2;
        //
        // flowLayoutPanel1
        //
        this.flowLayoutPanel1.AutoSize = true;
        this.flowLayoutPanel1.Controls.Add(this.panelContent);
        this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
        this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
        this.flowLayoutPanel1.Name = "flowLayoutPanel1";
        this.flowLayoutPanel1.Size = new System.Drawing.Size(368, 80);
        this.flowLayoutPanel1.TabIndex = 4;
        //
        // panelContent
        //
        this.panelContent.AutoSize = true;
        this.panelContent.Controls.Add(this.detailLabel);
        this.panelContent.Controls.Add(this.titleLabel);
        this.panelContent.Controls.Add(this.pictureBox1);
        this.panelContent.Location = new System.Drawing.Point(0, 0);
        this.panelContent.Margin = new System.Windows.Forms.Padding(0);
        this.panelContent.Name = "panelContent";
        this.panelContent.Size = new System.Drawing.Size(274, 67);
        this.panelContent.TabIndex = 5;
        //
        // detailLabel
        //
        this.detailLabel.AutoSize = true;
        this.detailLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.detailLabel.Location = new System.Drawing.Point(80, 39);
        this.detailLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.detailLabel.MaximumSize = new System.Drawing.Size(275, 0);
        this.detailLabel.Name = "detailLabel";
        this.detailLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 12);
        this.detailLabel.Size = new System.Drawing.Size(192, 27);
        this.detailLabel.TabIndex = 3;
        this.detailLabel.Text = "Soemthing wicked this way comes.";
        //
        // titleLabel
        //
        this.titleLabel.AutoSize = true;
        this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.titleLabel.Location = new System.Drawing.Point(80, 15);
        this.titleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.titleLabel.Name = "titleLabel";
        this.titleLabel.Size = new System.Drawing.Size(122, 17);
        this.titleLabel.TabIndex = 2;
        this.titleLabel.Text = "SmoothScroll Title";
        //
        // MessageBoxFormSmart
        //
        this.AcceptButton = this.acceptButton;
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoSize = true;
        this.BackColor = System.Drawing.SystemColors.Window;
        this.CancelButton = this.cancelButton;
        this.ClientSize = new System.Drawing.Size(362, 127);
        this.Controls.Add(this.panelFlowWrapper);
        this.Controls.Add(this.panelBottom);
        this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "MessageBoxFormSmart";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "MessageBoxSmart";
        this.panelBottom.ResumeLayout(false);
        this.panelBottom.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.panelFlowWrapper.ResumeLayout(false);
        this.panelFlowWrapper.PerformLayout();
        this.flowLayoutPanel1.ResumeLayout(false);
        this.flowLayoutPanel1.PerformLayout();
        this.panelContent.ResumeLayout(false);
        this.panelContent.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
