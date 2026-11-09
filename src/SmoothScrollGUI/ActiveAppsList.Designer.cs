namespace SmoothScrollGUI;

partial class ActiveAppsList
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.RadioButton radioButtonDummy;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActiveAppsList));
        this.radioButtonDummy = new System.Windows.Forms.RadioButton();
        this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        this.SuspendLayout();
        //
        // radioButtonDummy
        //
        this.radioButtonDummy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.radioButtonDummy.Appearance = System.Windows.Forms.Appearance.Button;
        this.radioButtonDummy.AutoEllipsis = true;
        this.radioButtonDummy.BackColor = System.Drawing.SystemColors.Window;
        this.radioButtonDummy.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
        this.radioButtonDummy.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Highlight;
        this.radioButtonDummy.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
        this.radioButtonDummy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.radioButtonDummy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.radioButtonDummy.ForeColor = System.Drawing.SystemColors.WindowText;
        this.radioButtonDummy.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonDummy.Image")));
        this.radioButtonDummy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.radioButtonDummy.Location = new System.Drawing.Point(0, 0);
        this.radioButtonDummy.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
        this.radioButtonDummy.Name = "radioButtonDummy";
        this.radioButtonDummy.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
        this.radioButtonDummy.Size = new System.Drawing.Size(852, 66);
        this.radioButtonDummy.TabIndex = 2;
        this.radioButtonDummy.Text = "Code Project";
        this.radioButtonDummy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        this.radioButtonDummy.UseVisualStyleBackColor = false;
        //
        // openFileDialog1
        //
        this.openFileDialog1.DefaultExt = "exe";
        this.openFileDialog1.FileName = "Find an app exe";
        this.openFileDialog1.Filter = "exe files (*.exe)|*.exe";
        this.openFileDialog1.InitialDirectory = "c:\\";
        //
        // ActiveAppsList
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.Controls.Add(this.radioButtonDummy);
        this.Font = new System.Drawing.Font("Segoe UI", 8.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
        this.Name = "ActiveAppsList";
        this.Size = new System.Drawing.Size(852, 228);
        this.ResumeLayout(false);
    }
}
