namespace RazerNotifier
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.label1 = new System.Windows.Forms.Label();
            this.CountryComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ProductIdTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IntervalTextBox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.CheckTimer = new System.Windows.Forms.Timer(this.components);
            this.OutOfStockCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Country";
            // 
            // CountryComboBox
            // 
            this.CountryComboBox.FormattingEnabled = true;
            this.CountryComboBox.Items.AddRange(new object[] {
            "North America",
            "-- Canada",
            "-- United States",
            "",
            "Asia Pacific",
            "-- Australia",
            "-- Brunei",
            "-- Japan",
            "-- Malaysia",
            "-- New Zealand",
            "-- Singapore",
            "-- Hong Kong",
            "-- Taiwan",
            "",
            "Europe",
            "-- Belgium",
            "-- Croatia",
            "-- Cyprus",
            "-- Czech Republic",
            "-- Denmark",
            "-- Germany",
            "-- Spain",
            "-- Estonia",
            "-- Finland",
            "-- France",
            "-- Greece",
            "-- Hungary",
            "-- Ireland",
            "-- Italy",
            "-- Latvia",
            "-- Luxembourg",
            "-- Malta",
            "-- Netherlands",
            "-- Norway",
            "-- Austria",
            "-- Poland",
            "-- Portugal",
            "-- Slovakia",
            "-- Slovenia",
            "-- Sweden",
            "-- Switzerland",
            "-- United Kingdom"});
            this.CountryComboBox.Location = new System.Drawing.Point(110, 16);
            this.CountryComboBox.Name = "CountryComboBox";
            this.CountryComboBox.Size = new System.Drawing.Size(162, 21);
            this.CountryComboBox.TabIndex = 1;
            this.CountryComboBox.SelectedIndexChanged += new System.EventHandler(this.CountryComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Product ID";
            // 
            // ProductIdTextBox
            // 
            this.ProductIdTextBox.Location = new System.Drawing.Point(110, 49);
            this.ProductIdTextBox.Name = "ProductIdTextBox";
            this.ProductIdTextBox.Size = new System.Drawing.Size(162, 20);
            this.ProductIdTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Check Frequency";
            // 
            // IntervalTextBox
            // 
            this.IntervalTextBox.Location = new System.Drawing.Point(110, 77);
            this.IntervalTextBox.Name = "IntervalTextBox";
            this.IntervalTextBox.Size = new System.Drawing.Size(162, 20);
            this.IntervalTextBox.TabIndex = 5;
            this.IntervalTextBox.Text = "hh:mm:ss";
            this.IntervalTextBox.TextChanged += new System.EventHandler(this.IntervalTextBox_TextChanged);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(110, 126);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 6;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // CheckTimer
            // 
            this.CheckTimer.Interval = 60000;
            this.CheckTimer.Tick += new System.EventHandler(this.CheckTimer_Tick);
            // 
            // OutOfStockCheckBox
            // 
            this.OutOfStockCheckBox.AutoSize = true;
            this.OutOfStockCheckBox.Checked = true;
            this.OutOfStockCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OutOfStockCheckBox.Location = new System.Drawing.Point(79, 103);
            this.OutOfStockCheckBox.Name = "OutOfStockCheckBox";
            this.OutOfStockCheckBox.Size = new System.Drawing.Size(147, 17);
            this.OutOfStockCheckBox.TabIndex = 7;
            this.OutOfStockCheckBox.Text = "Notify even if out of stock";
            this.OutOfStockCheckBox.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.OutOfStockCheckBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.IntervalTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ProductIdTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CountryComboBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Razer Notifier";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CountryComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ProductIdTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox IntervalTextBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Timer CheckTimer;
        private System.Windows.Forms.CheckBox OutOfStockCheckBox;
    }
}

