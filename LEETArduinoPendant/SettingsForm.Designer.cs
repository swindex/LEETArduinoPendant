using System.Windows.Forms;

namespace LEETArduinoPendant
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.Label4 = new System.Windows.Forms.Label();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.radio_in = new System.Windows.Forms.RadioButton();
            this.radio_mm = new System.Windows.Forms.RadioButton();
            this.LBL_Step = new System.Windows.Forms.Label();
            this.LBL_Author = new System.Windows.Forms.Label();
            this.TableLayoutPanel1.SuspendLayout();
            this.TableLayoutPanel4.SuspendLayout();
            this.TableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.AutoSize = true;
            this.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel1.Controls.Add(this.TableLayoutPanel4, 1, 0);
            this.TableLayoutPanel1.Controls.Add(this.TableLayoutPanel2, 1, 1);
            this.TableLayoutPanel1.Controls.Add(this.LBL_Step, 0, 1);
            this.TableLayoutPanel1.Controls.Add(this.LBL_Author, 0, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 3;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanel1.Size = new System.Drawing.Size(231, 61);
            this.TableLayoutPanel1.TabIndex = 0;
            // 
            // TableLayoutPanel4
            // 
            this.TableLayoutPanel4.AutoSize = true;
            this.TableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TableLayoutPanel4.ColumnCount = 1;
            this.TableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel4.Controls.Add(this.Label4, 0, 0);
            this.TableLayoutPanel4.Controls.Add(this.LinkLabel1, 0, 1);
            this.TableLayoutPanel4.Location = new System.Drawing.Point(79, 3);
            this.TableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.TableLayoutPanel4.Name = "TableLayoutPanel4";
            this.TableLayoutPanel4.RowCount = 2;
            this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanel4.Size = new System.Drawing.Size(118, 26);
            this.TableLayoutPanel4.TabIndex = 1;
            // 
            // Label4
            // 
            this.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(3, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(78, 13);
            this.Label4.TabIndex = 4;
            this.Label4.Text = "Eldar Gerfanov";
            // 
            // LinkLabel1
            // 
            this.LinkLabel1.AutoSize = true;
            this.LinkLabel1.Location = new System.Drawing.Point(3, 13);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(112, 13);
            this.LinkLabel1.TabIndex = 5;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "https://zero-divide.net";
            this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // TableLayoutPanel2
            // 
            this.TableLayoutPanel2.AutoSize = true;
            this.TableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TableLayoutPanel2.ColumnCount = 2;
            this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel2.Controls.Add(this.radio_in, 0, 0);
            this.TableLayoutPanel2.Controls.Add(this.radio_mm, 1, 0);
            this.TableLayoutPanel2.Location = new System.Drawing.Point(82, 35);
            this.TableLayoutPanel2.Name = "TableLayoutPanel2";
            this.TableLayoutPanel2.RowCount = 1;
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel2.Size = new System.Drawing.Size(146, 23);
            this.TableLayoutPanel2.TabIndex = 1;
            // 
            // radio_in
            // 
            this.radio_in.AutoSize = true;
            this.radio_in.Checked = true;
            this.radio_in.Location = new System.Drawing.Point(3, 3);
            this.radio_in.Name = "radio_in";
            this.radio_in.Size = new System.Drawing.Size(53, 17);
            this.radio_in.TabIndex = 1;
            this.radio_in.TabStop = true;
            this.radio_in.Text = "x1 (in)";
            this.radio_in.UseVisualStyleBackColor = true;
            // 
            // radio_mm
            // 
            this.radio_mm.AutoSize = true;
            this.radio_mm.Location = new System.Drawing.Point(76, 3);
            this.radio_mm.Name = "radio_mm";
            this.radio_mm.Size = new System.Drawing.Size(67, 17);
            this.radio_mm.TabIndex = 1;
            this.radio_mm.Text = "x10 (mm)";
            this.radio_mm.UseVisualStyleBackColor = true;
            // 
            // LBL_Step
            // 
            this.LBL_Step.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_Step.AutoSize = true;
            this.LBL_Step.Location = new System.Drawing.Point(3, 40);
            this.LBL_Step.Name = "LBL_Step";
            this.LBL_Step.Size = new System.Drawing.Size(73, 13);
            this.LBL_Step.TabIndex = 2;
            this.LBL_Step.Text = "Step Multiplier";
            // 
            // LBL_Author
            // 
            this.LBL_Author.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_Author.AutoSize = true;
            this.LBL_Author.Location = new System.Drawing.Point(3, 9);
            this.LBL_Author.Name = "LBL_Author";
            this.LBL_Author.Size = new System.Drawing.Size(38, 13);
            this.LBL_Author.TabIndex = 5;
            this.LBL_Author.Text = "Author";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(300, 123);
            this.Controls.Add(this.TableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LEET Arduino Pendant Config";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.TableLayoutPanel1.ResumeLayout(false);
            this.TableLayoutPanel1.PerformLayout();
            this.TableLayoutPanel4.ResumeLayout(false);
            this.TableLayoutPanel4.PerformLayout();
            this.TableLayoutPanel2.ResumeLayout(false);
            this.TableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private TableLayoutPanel TableLayoutPanel1;
        private RadioButton radio_in;
        private TableLayoutPanel TableLayoutPanel4;
        private Label Label4;
        private LinkLabel LinkLabel1;
        private TableLayoutPanel TableLayoutPanel2;
        private RadioButton radio_mm;
        private Label LBL_Step;
        private Label LBL_Author;
    }
}
