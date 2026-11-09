namespace SmoothScrollGUI;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    private AdvancedComboBox appList;
    private TextBoxPlus stepSizeBox;
    private System.Windows.Forms.Label stepSizeLabel;
    private CheckBoxPlus horizontalSmoothingCheck;
    private System.Windows.Forms.Panel appPanel;
    private CheckBoxPlus reverseDirectionCheck;
    private CheckBoxPlus pulseAlgorithmCheck;
    private System.Windows.Forms.Label pulseScaleLabel;
    private TextBoxPlus pulseScaleBox;
    private System.Windows.Forms.Label accelerationMaxLabel;
    private TextBoxPlus accelerationMaxBox;
    private System.Windows.Forms.Label accelerationDeltaLabel;
    private TextBoxPlus accelerationDeltaBox;
    private System.Windows.Forms.Label animationTimeLabel;
    private TextBoxPlus animationTimeBox;
    private System.Windows.Forms.Label smoothScrollLabel;
    private CheckBoxPlus enabledForAppCheck;
    private CheckBoxPlus enabledForAllAppsCheck;
    private CheckBoxPlus launchOnLoginCheck;
    private System.Windows.Forms.ToolTip toolTip1;
    private AlignedButton AddAppButton;
    private AlignedButton RemoveAppButton;
    private System.Windows.Forms.Panel toolPanel;
    private System.Windows.Forms.FlowLayoutPanel globalFlowPanel;
    private System.Windows.Forms.TableLayoutPanel appTextBoxesTable;
    private CheckBoxPlus horizontalShiftKeyCheck;
    private AlignedButton ResetAllButton;

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
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        this.stepSizeLabel = new System.Windows.Forms.Label();
        this.appPanel = new System.Windows.Forms.Panel();
        this.appTextBoxesTable = new System.Windows.Forms.TableLayoutPanel();
        this.animationTimeBox = new SmoothScrollGUI.TextBoxPlus();
        this.accelerationDeltaBox = new SmoothScrollGUI.TextBoxPlus();
        this.accelerationMaxBox = new SmoothScrollGUI.TextBoxPlus();
        this.pulseScaleBox = new SmoothScrollGUI.TextBoxPlus();
        this.accelerationMaxLabel = new System.Windows.Forms.Label();
        this.pulseScaleLabel = new System.Windows.Forms.Label();
        this.animationTimeLabel = new System.Windows.Forms.Label();
        this.accelerationDeltaLabel = new System.Windows.Forms.Label();
        this.stepSizeBox = new SmoothScrollGUI.TextBoxPlus();
        this.enabledForAppCheck = new SmoothScrollGUI.CheckBoxPlus();
        this.horizontalSmoothingCheck = new SmoothScrollGUI.CheckBoxPlus();
        this.reverseDirectionCheck = new SmoothScrollGUI.CheckBoxPlus();
        this.horizontalShiftKeyCheck = new SmoothScrollGUI.CheckBoxPlus();
        this.pulseAlgorithmCheck = new SmoothScrollGUI.CheckBoxPlus();
        this.smoothScrollLabel = new System.Windows.Forms.Label();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.launchOnLoginCheck = new SmoothScrollGUI.CheckBoxPlus();
        this.ResetAllButton = new SmoothScrollGUI.AlignedButton();
        this.AddAppButton = new SmoothScrollGUI.AlignedButton();
        this.RemoveAppButton = new SmoothScrollGUI.AlignedButton();
        this.toolPanel = new System.Windows.Forms.Panel();
        this.globalFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
        this.enabledForAllAppsCheck = new SmoothScrollGUI.CheckBoxPlus();
        this.appList = new SmoothScrollGUI.AdvancedComboBox();
        this.appPanel.SuspendLayout();
        this.appTextBoxesTable.SuspendLayout();
        this.toolPanel.SuspendLayout();
        this.globalFlowPanel.SuspendLayout();
        this.SuspendLayout();
        //
        // stepSizeLabel
        //
        this.stepSizeLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.stepSizeLabel.AutoSize = true;
        this.stepSizeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.stepSizeLabel.Location = new System.Drawing.Point(80, 3);
        this.stepSizeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
        this.stepSizeLabel.Name = "stepSizeLabel";
        this.stepSizeLabel.Size = new System.Drawing.Size(115, 25);
        this.stepSizeLabel.TabIndex = 5;
        this.stepSizeLabel.Text = "Step size [px]";
        this.stepSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.toolTip1.SetToolTip(this.stepSizeLabel, "How much to scroll for one middle mouse tick");
        //
        // appPanel
        //
        this.appPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
        this.appPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.appPanel.Controls.Add(this.appTextBoxesTable);
        this.appPanel.Controls.Add(this.enabledForAppCheck);
        this.appPanel.Controls.Add(this.horizontalSmoothingCheck);
        this.appPanel.Controls.Add(this.reverseDirectionCheck);
        this.appPanel.Controls.Add(this.horizontalShiftKeyCheck);
        this.appPanel.Controls.Add(this.pulseAlgorithmCheck);
        this.appPanel.Location = new System.Drawing.Point(40, 181);
        this.appPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.appPanel.Name = "appPanel";
        this.appPanel.Size = new System.Drawing.Size(752, 224);
        this.appPanel.TabIndex = 8;
        this.appPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BlankSpace_MouseClick);
        //
        // appTextBoxesTable
        //
        this.appTextBoxesTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.appTextBoxesTable.ColumnCount = 2;
        this.appTextBoxesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
        this.appTextBoxesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
        this.appTextBoxesTable.Controls.Add(this.stepSizeLabel, 0, 0);
        this.appTextBoxesTable.Controls.Add(this.animationTimeBox, 1, 1);
        this.appTextBoxesTable.Controls.Add(this.accelerationDeltaBox, 1, 2);
        this.appTextBoxesTable.Controls.Add(this.accelerationMaxBox, 1, 3);
        this.appTextBoxesTable.Controls.Add(this.pulseScaleBox, 1, 4);
        this.appTextBoxesTable.Controls.Add(this.accelerationMaxLabel, 0, 3);
        this.appTextBoxesTable.Controls.Add(this.pulseScaleLabel, 0, 4);
        this.appTextBoxesTable.Controls.Add(this.animationTimeLabel, 0, 1);
        this.appTextBoxesTable.Controls.Add(this.accelerationDeltaLabel, 0, 2);
        this.appTextBoxesTable.Controls.Add(this.stepSizeBox, 1, 0);
        this.appTextBoxesTable.Location = new System.Drawing.Point(42, 20);
        this.appTextBoxesTable.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
        this.appTextBoxesTable.Name = "appTextBoxesTable";
        this.appTextBoxesTable.RowCount = 5;
        this.appTextBoxesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
        this.appTextBoxesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
        this.appTextBoxesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
        this.appTextBoxesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
        this.appTextBoxesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
        this.appTextBoxesTable.Size = new System.Drawing.Size(287, 184);
        this.appTextBoxesTable.TabIndex = 40;
        //
        // animationTimeBox
        //
        this.animationTimeBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.animationTimeBox.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
        this.animationTimeBox.InDefaultState = false;
        this.animationTimeBox.Location = new System.Drawing.Point(205, 40);
        this.animationTimeBox.Margin = new System.Windows.Forms.Padding(6, 4, 4, 4);
        this.animationTimeBox.MaxLength = 4;
        this.animationTimeBox.Name = "animationTimeBox";
        this.animationTimeBox.Size = new System.Drawing.Size(68, 31);
        this.animationTimeBox.TabIndex = 2;
        this.animationTimeBox.Text = "0";
        this.animationTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.toolTip1.SetToolTip(this.animationTimeBox, "How long the animation takes after one wheel tick\r\n(Bigger number means slower animation)");
        //
        // accelerationDeltaBox
        //
        this.accelerationDeltaBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.accelerationDeltaBox.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
        this.accelerationDeltaBox.InDefaultState = false;
        this.accelerationDeltaBox.Location = new System.Drawing.Point(205, 76);
        this.accelerationDeltaBox.Margin = new System.Windows.Forms.Padding(6, 4, 4, 4);
        this.accelerationDeltaBox.MaxLength = 3;
        this.accelerationDeltaBox.Name = "accelerationDeltaBox";
        this.accelerationDeltaBox.Size = new System.Drawing.Size(68, 31);
        this.accelerationDeltaBox.TabIndex = 3;
        this.accelerationDeltaBox.Text = "0";
        this.accelerationDeltaBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.toolTip1.SetToolTip(this.accelerationDeltaBox, "Scrolling events coming more frequent than this will start acceleration\r\n(Smaller number means more frequent acceleration)");
        //
        // accelerationMaxBox
        //
        this.accelerationMaxBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.accelerationMaxBox.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
        this.accelerationMaxBox.InDefaultState = false;
        this.accelerationMaxBox.Location = new System.Drawing.Point(205, 112);
        this.accelerationMaxBox.Margin = new System.Windows.Forms.Padding(6, 4, 4, 4);
        this.accelerationMaxBox.MaxLength = 4;
        this.accelerationMaxBox.Name = "accelerationMaxBox";
        this.accelerationMaxBox.Size = new System.Drawing.Size(68, 31);
        this.accelerationMaxBox.TabIndex = 4;
        this.accelerationMaxBox.Text = "0";
        this.accelerationMaxBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.toolTip1.SetToolTip(this.accelerationMaxBox, "Turning the wheel very fast can accelerate to X times faster than Step Size\r\n(Bigger number means higher top speed)");
        //
        // pulseScaleBox
        //
        this.pulseScaleBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.pulseScaleBox.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
        this.pulseScaleBox.InDefaultState = false;
        this.pulseScaleBox.Location = new System.Drawing.Point(205, 148);
        this.pulseScaleBox.Margin = new System.Windows.Forms.Padding(6, 4, 4, 4);
        this.pulseScaleBox.MaxLength = 3;
        this.pulseScaleBox.Name = "pulseScaleBox";
        this.pulseScaleBox.Size = new System.Drawing.Size(68, 31);
        this.pulseScaleBox.TabIndex = 5;
        this.pulseScaleBox.Text = "0";
        this.pulseScaleBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.toolTip1.SetToolTip(this.pulseScaleBox, "How much time is spent \r\nDeceleration vs Accelerating (tail vs head) \r\ne.g. 4 means 4 times longer tail\r\n\r\n");
        //
        // accelerationMaxLabel
        //
        this.accelerationMaxLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.accelerationMaxLabel.AutoSize = true;
        this.accelerationMaxLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.accelerationMaxLabel.Location = new System.Drawing.Point(26, 111);
        this.accelerationMaxLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
        this.accelerationMaxLabel.Name = "accelerationMaxLabel";
        this.accelerationMaxLabel.Size = new System.Drawing.Size(169, 25);
        this.accelerationMaxLabel.TabIndex = 27;
        this.accelerationMaxLabel.Text = "Acceleration max [x]";
        this.accelerationMaxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.toolTip1.SetToolTip(this.accelerationMaxLabel, "Turning the wheel very fast can accelerate to X times faster than Step Size\r\n(Bigger number means higher top speed)");
        //
        // pulseScaleLabel
        //
        this.pulseScaleLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.pulseScaleLabel.AutoSize = true;
        this.pulseScaleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.pulseScaleLabel.Location = new System.Drawing.Point(29, 149);
        this.pulseScaleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
        this.pulseScaleLabel.Name = "pulseScaleLabel";
        this.pulseScaleLabel.Size = new System.Drawing.Size(166, 25);
        this.pulseScaleLabel.TabIndex = 30;
        this.pulseScaleLabel.Text = "Tail to head ratio [x]";
        this.pulseScaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.toolTip1.SetToolTip(this.pulseScaleLabel, "How much time is spent \r\nDecelerating vs Accelerating (tail vs head) \r\ne.g. 4 means 4 times longer tail\r\n\r\n");
        //
        // animationTimeLabel
        //
        this.animationTimeLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.animationTimeLabel.AutoSize = true;
        this.animationTimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.animationTimeLabel.Location = new System.Drawing.Point(22, 39);
        this.animationTimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
        this.animationTimeLabel.Name = "animationTimeLabel";
        this.animationTimeLabel.Size = new System.Drawing.Size(173, 25);
        this.animationTimeLabel.TabIndex = 21;
        this.animationTimeLabel.Text = "Animation time [ms]";
        this.animationTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.toolTip1.SetToolTip(this.animationTimeLabel, "How long the animation takes after one wheel tick\r\n(Bigger number means slower animation)");
        //
        // accelerationDeltaLabel
        //
        this.accelerationDeltaLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.accelerationDeltaLabel.AutoSize = true;
        this.accelerationDeltaLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.accelerationDeltaLabel.Location = new System.Drawing.Point(4, 75);
        this.accelerationDeltaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
        this.accelerationDeltaLabel.Name = "accelerationDeltaLabel";
        this.accelerationDeltaLabel.Size = new System.Drawing.Size(191, 25);
        this.accelerationDeltaLabel.TabIndex = 24;
        this.accelerationDeltaLabel.Text = "Acceleration delta [ms]";
        this.accelerationDeltaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.toolTip1.SetToolTip(this.accelerationDeltaLabel, "Scrolling events coming more frequent than this will start acceleration\r\n(Smaller number means more frequent acceleration)");
        //
        // stepSizeBox
        //
        this.stepSizeBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.stepSizeBox.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
        this.stepSizeBox.InDefaultState = false;
        this.stepSizeBox.Location = new System.Drawing.Point(205, 4);
        this.stepSizeBox.Margin = new System.Windows.Forms.Padding(6, 4, 4, 4);
        this.stepSizeBox.MaxLength = 3;
        this.stepSizeBox.Name = "stepSizeBox";
        this.stepSizeBox.Size = new System.Drawing.Size(68, 31);
        this.stepSizeBox.TabIndex = 1;
        this.stepSizeBox.Text = "0";
        this.stepSizeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.toolTip1.SetToolTip(this.stepSizeBox, "How much to scroll for one middle mouse tick");
        //
        // enabledForAppCheck
        //
        this.enabledForAppCheck.AutoSize = true;
        this.enabledForAppCheck.Checked = true;
        this.enabledForAppCheck.CheckState = System.Windows.Forms.CheckState.Checked;
        this.enabledForAppCheck.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.enabledForAppCheck.InDefaultState = false;
        this.enabledForAppCheck.Location = new System.Drawing.Point(409, 23);
        this.enabledForAppCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.enabledForAppCheck.Name = "enabledForAppCheck";
        this.enabledForAppCheck.Size = new System.Drawing.Size(187, 29);
        this.enabledForAppCheck.TabIndex = 32;
        this.enabledForAppCheck.Text = "Enable for this app";
        this.enabledForAppCheck.UseVisualStyleBackColor = true;
        this.enabledForAppCheck.CheckedChanged += new System.EventHandler(this.enabledForAppCheck_CheckedChanged);
        //
        // horizontalSmoothingCheck
        //
        this.horizontalSmoothingCheck.AutoSize = true;
        this.horizontalSmoothingCheck.Checked = true;
        this.horizontalSmoothingCheck.CheckState = System.Windows.Forms.CheckState.Checked;
        this.horizontalSmoothingCheck.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.horizontalSmoothingCheck.InDefaultState = false;
        this.horizontalSmoothingCheck.Location = new System.Drawing.Point(409, 136);
        this.horizontalSmoothingCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.horizontalSmoothingCheck.Name = "horizontalSmoothingCheck";
        this.horizontalSmoothingCheck.Size = new System.Drawing.Size(222, 29);
        this.horizontalSmoothingCheck.TabIndex = 8;
        this.horizontalSmoothingCheck.Text = "Horizontal smoothness";
        this.toolTip1.SetToolTip(this.horizontalSmoothingCheck, "Apply the same smooth animation to horizontal scrolling");
        this.horizontalSmoothingCheck.UseVisualStyleBackColor = true;
        //
        // reverseDirectionCheck
        //
        this.reverseDirectionCheck.AutoSize = true;
        this.reverseDirectionCheck.Checked = true;
        this.reverseDirectionCheck.CheckState = System.Windows.Forms.CheckState.Checked;
        this.reverseDirectionCheck.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.reverseDirectionCheck.InDefaultState = false;
        this.reverseDirectionCheck.Location = new System.Drawing.Point(409, 173);
        this.reverseDirectionCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.reverseDirectionCheck.Name = "reverseDirectionCheck";
        this.reverseDirectionCheck.Size = new System.Drawing.Size(222, 29);
        this.reverseDirectionCheck.TabIndex = 9;
        this.reverseDirectionCheck.Text = "Reverse wheel direction";
        this.toolTip1.SetToolTip(this.reverseDirectionCheck, "If enabled, both wheel directions are reversed (Up->Down, Left->Right)");
        this.reverseDirectionCheck.UseVisualStyleBackColor = true;
        //
        // horizontalShiftKeyCheck
        //
        this.horizontalShiftKeyCheck.AutoSize = true;
        this.horizontalShiftKeyCheck.Checked = true;
        this.horizontalShiftKeyCheck.CheckState = System.Windows.Forms.CheckState.Checked;
        this.horizontalShiftKeyCheck.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.horizontalShiftKeyCheck.InDefaultState = false;
        this.horizontalShiftKeyCheck.Location = new System.Drawing.Point(409, 97);
        this.horizontalShiftKeyCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.horizontalShiftKeyCheck.Name = "horizontalShiftKeyCheck";
        this.horizontalShiftKeyCheck.Size = new System.Drawing.Size(261, 29);
        this.horizontalShiftKeyCheck.TabIndex = 7;
        this.horizontalShiftKeyCheck.Text = "Shift key horizontal scrolling";
        this.toolTip1.SetToolTip(this.horizontalShiftKeyCheck, "Holding down the [Shift] key while scrolling makes it horizontal");
        this.horizontalShiftKeyCheck.UseVisualStyleBackColor = true;
        //
        // pulseAlgorithmCheck
        //
        this.pulseAlgorithmCheck.AutoSize = true;
        this.pulseAlgorithmCheck.Checked = true;
        this.pulseAlgorithmCheck.CheckState = System.Windows.Forms.CheckState.Checked;
        this.pulseAlgorithmCheck.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.pulseAlgorithmCheck.InDefaultState = false;
        this.pulseAlgorithmCheck.Location = new System.Drawing.Point(409, 60);
        this.pulseAlgorithmCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.pulseAlgorithmCheck.Name = "pulseAlgorithmCheck";
        this.pulseAlgorithmCheck.Size = new System.Drawing.Size(176, 29);
        this.pulseAlgorithmCheck.TabIndex = 6;
        this.pulseAlgorithmCheck.Text = "Animation easing";
        this.toolTip1.SetToolTip(this.pulseAlgorithmCheck, "Adds nice easing curve to the animation");
        this.pulseAlgorithmCheck.UseVisualStyleBackColor = true;
        //
        // smoothScrollLabel
        //
        this.smoothScrollLabel.AutoSize = true;
        this.smoothScrollLabel.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.smoothScrollLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
        this.smoothScrollLabel.Location = new System.Drawing.Point(300, 24);
        this.smoothScrollLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        this.smoothScrollLabel.Name = "smoothScrollLabel";
        this.smoothScrollLabel.Size = new System.Drawing.Size(228, 45);
        this.smoothScrollLabel.TabIndex = 14;
        this.smoothScrollLabel.Text = "SmoothScroll";
        //
        // launchOnLoginCheck
        //
        this.launchOnLoginCheck.AutoSize = true;
        this.launchOnLoginCheck.Checked = true;
        this.launchOnLoginCheck.CheckState = System.Windows.Forms.CheckState.Checked;
        this.launchOnLoginCheck.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.launchOnLoginCheck.InDefaultState = false;
        this.launchOnLoginCheck.Location = new System.Drawing.Point(15, 11);
        this.launchOnLoginCheck.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
        this.launchOnLoginCheck.Name = "launchOnLoginCheck";
        this.launchOnLoginCheck.Size = new System.Drawing.Size(188, 29);
        this.launchOnLoginCheck.TabIndex = 10;
        this.launchOnLoginCheck.Text = "Auto start on login";
        this.toolTip1.SetToolTip(this.launchOnLoginCheck, "Automatically start SmoothScroll when you log in to your computer");
        this.launchOnLoginCheck.UseVisualStyleBackColor = true;
        this.launchOnLoginCheck.Click += new System.EventHandler(this.globalControls_AnythingChanged);
        //
        // ResetAllButton
        //
        this.ResetAllButton.AccessibleName = "Reset All";
        this.ResetAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.ResetAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.ResetAllButton.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.ResetAllButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
        this.ResetAllButton.Location = new System.Drawing.Point(-1, -1);
        this.ResetAllButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.ResetAllButton.Name = "ResetAllButton";
        this.ResetAllButton.Size = new System.Drawing.Size(90, 30);
        this.ResetAllButton.TabIndex = 36;
        this.toolTip1.SetToolTip(this.ResetAllButton, "Reset to default app settings");
        this.ResetAllButton.UseVisualStyleBackColor = true;
        this.ResetAllButton.Click += new System.EventHandler(this.ResetAllButton_Click);
        //
        // AddAppButton
        //
        this.AddAppButton.AccessibleName = "＋";
        this.AddAppButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.AddAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.AddAppButton.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.AddAppButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
        this.AddAppButton.Location = new System.Drawing.Point(688, -1);
        this.AddAppButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.AddAppButton.Name = "AddAppButton";
        this.AddAppButton.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
        this.AddAppButton.Size = new System.Drawing.Size(32, 30);
        this.AddAppButton.TabIndex = 35;
        this.toolTip1.SetToolTip(this.AddAppButton, "Add settings for a new app");
        this.AddAppButton.UseVisualStyleBackColor = true;
        this.AddAppButton.Click += new System.EventHandler(this.AddAppButton_Click);
        //
        // RemoveAppButton
        //
        this.RemoveAppButton.AccessibleName = "－";
        this.RemoveAppButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.RemoveAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.RemoveAppButton.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.RemoveAppButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
        this.RemoveAppButton.Location = new System.Drawing.Point(719, -1);
        this.RemoveAppButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.RemoveAppButton.Name = "RemoveAppButton";
        this.RemoveAppButton.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
        this.RemoveAppButton.Size = new System.Drawing.Size(32, 30);
        this.RemoveAppButton.TabIndex = 34;
        this.toolTip1.SetToolTip(this.RemoveAppButton, "Remove settings for this app");
        this.RemoveAppButton.UseVisualStyleBackColor = true;
        this.RemoveAppButton.Click += new System.EventHandler(this.RemoveAppButton_Click);
        //
        // toolPanel
        //
        this.toolPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
        this.toolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.toolPanel.Controls.Add(this.ResetAllButton);
        this.toolPanel.Controls.Add(this.AddAppButton);
        this.toolPanel.Controls.Add(this.RemoveAppButton);
        this.toolPanel.Location = new System.Drawing.Point(40, 403);
        this.toolPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.toolPanel.Name = "toolPanel";
        this.toolPanel.Size = new System.Drawing.Size(752, 30);
        this.toolPanel.TabIndex = 16;
        //
        // globalFlowPanel
        //
        this.globalFlowPanel.AutoSize = true;
        this.globalFlowPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.globalFlowPanel.Controls.Add(this.launchOnLoginCheck);
        this.globalFlowPanel.Controls.Add(this.enabledForAllAppsCheck);
        this.globalFlowPanel.Location = new System.Drawing.Point(44, 83);
        this.globalFlowPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.globalFlowPanel.Name = "globalFlowPanel";
        this.globalFlowPanel.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
        this.globalFlowPanel.Size = new System.Drawing.Size(739, 51);
        this.globalFlowPanel.TabIndex = 37;
        //
        // enabledForAllAppsCheck
        //
        this.enabledForAllAppsCheck.AutoSize = true;
        this.enabledForAllAppsCheck.Checked = true;
        this.enabledForAllAppsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
        this.enabledForAllAppsCheck.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.enabledForAllAppsCheck.InDefaultState = false;
        this.enabledForAllAppsCheck.Location = new System.Drawing.Point(219, 11);
        this.enabledForAllAppsCheck.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
        this.enabledForAllAppsCheck.Name = "enabledForAllAppsCheck";
        this.enabledForAllAppsCheck.Size = new System.Drawing.Size(101, 29);
        this.enabledForAllAppsCheck.TabIndex = 13;
        this.enabledForAllAppsCheck.Text = "Enabled";
        this.enabledForAllAppsCheck.UseVisualStyleBackColor = true;
        this.enabledForAllAppsCheck.CheckedChanged += new System.EventHandler(this.enabledForAllAppsCheck_CheckedChanged);
        this.enabledForAllAppsCheck.Click += new System.EventHandler(this.globalControls_AnythingChanged);
        //
        // appList
        //
        this.appList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        this.appList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.appList.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.appList.FormattingEnabled = true;
        this.appList.Location = new System.Drawing.Point(40, 144);
        this.appList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.appList.Name = "appList";
        this.appList.Size = new System.Drawing.Size(752, 35);
        this.appList.TabIndex = 0;
        this.appList.SelectedIndexChanged += new System.EventHandler(this.appList_SelectedIndexChanged);
        this.appList.DropDownClosed += new System.EventHandler(this.appList_DropDownClosed);
        //
        // Form1
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.ClientSize = new System.Drawing.Size(824, 467);
        this.Controls.Add(this.globalFlowPanel);
        this.Controls.Add(this.toolPanel);
        this.Controls.Add(this.appList);
        this.Controls.Add(this.appPanel);
        this.Controls.Add(this.smoothScrollLabel);
        this.DoubleBuffered = true;
        this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "Form1";
        this.Text = "SmoothScroll Settings";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
        this.Load += new System.EventHandler(this.Form1_Load);
        this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BlankSpace_MouseClick);
        this.appPanel.ResumeLayout(false);
        this.appPanel.PerformLayout();
        this.appTextBoxesTable.ResumeLayout(false);
        this.appTextBoxesTable.PerformLayout();
        this.toolPanel.ResumeLayout(false);
        this.globalFlowPanel.ResumeLayout(false);
        this.globalFlowPanel.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
