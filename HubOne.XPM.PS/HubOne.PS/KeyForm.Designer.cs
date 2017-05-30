namespace HubOne.PS
{
    partial class KeyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyForm));
            this.ButtonRetrieveWorkflowMaxKey = new System.Windows.Forms.Button();
            this.TableLayoutPanelHeader = new System.Windows.Forms.TableLayoutPanel();
            this.TextBoxProgress = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.WebBrowser1 = new System.Windows.Forms.WebBrowser();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.TableLayoutPanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.TableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonRetrieveWorkflowMaxKey
            // 
            this.ButtonRetrieveWorkflowMaxKey.Dock = System.Windows.Forms.DockStyle.Top;
            this.ButtonRetrieveWorkflowMaxKey.Location = new System.Drawing.Point(38, 94);
            this.ButtonRetrieveWorkflowMaxKey.Margin = new System.Windows.Forms.Padding(38, 12, 38, 12);
            this.ButtonRetrieveWorkflowMaxKey.Name = "ButtonRetrieveWorkflowMaxKey";
            this.ButtonRetrieveWorkflowMaxKey.Size = new System.Drawing.Size(676, 41);
            this.ButtonRetrieveWorkflowMaxKey.TabIndex = 7;
            this.ButtonRetrieveWorkflowMaxKey.Text = "Fetch Key";
            this.ButtonRetrieveWorkflowMaxKey.UseVisualStyleBackColor = true;
            // 
            // TableLayoutPanelHeader
            // 
            this.TableLayoutPanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(105)))), ((int)(((byte)(200)))));
            this.TableLayoutPanelHeader.ColumnCount = 2;
            this.TableLayoutPanelHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.77108F));
            this.TableLayoutPanelHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.22891F));
            this.TableLayoutPanelHeader.Controls.Add(this.TextBoxProgress, 1, 0);
            this.TableLayoutPanelHeader.Controls.Add(this.pictureBox1, 0, 0);
            this.TableLayoutPanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanelHeader.Location = new System.Drawing.Point(2, 2);
            this.TableLayoutPanelHeader.Margin = new System.Windows.Forms.Padding(2);
            this.TableLayoutPanelHeader.Name = "TableLayoutPanelHeader";
            this.TableLayoutPanelHeader.RowCount = 1;
            this.TableLayoutPanelHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelHeader.Size = new System.Drawing.Size(748, 45);
            this.TableLayoutPanelHeader.TabIndex = 12;
            // 
            // TextBoxProgress
            // 
            this.TextBoxProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(105)))), ((int)(((byte)(200)))));
            this.TextBoxProgress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBoxProgress.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxProgress.ForeColor = System.Drawing.Color.White;
            this.TextBoxProgress.Location = new System.Drawing.Point(139, 13);
            this.TextBoxProgress.Margin = new System.Windows.Forms.Padding(7, 13, 7, 9);
            this.TextBoxProgress.Name = "TextBoxProgress";
            this.TextBoxProgress.Size = new System.Drawing.Size(534, 20);
            this.TextBoxProgress.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HubOne.PS.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 41);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.TableLayoutPanel1.ColumnCount = 1;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel1.Controls.Add(this.WebBrowser1, 0, 1);
            this.TableLayoutPanel1.Controls.Add(this.ButtonRetrieveWorkflowMaxKey, 0, 2);
            this.TableLayoutPanel1.Controls.Add(this.ButtonOk, 0, 3);
            this.TableLayoutPanel1.Controls.Add(this.TableLayoutPanelHeader, 0, 0);
            this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 4;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(752, 173);
            this.TableLayoutPanel1.TabIndex = 12;
            // 
            // WebBrowser1
            // 
            this.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser1.Location = new System.Drawing.Point(8, 57);
            this.WebBrowser1.Margin = new System.Windows.Forms.Padding(8);
            this.WebBrowser1.MinimumSize = new System.Drawing.Size(14, 17);
            this.WebBrowser1.Name = "WebBrowser1";
            this.WebBrowser1.Size = new System.Drawing.Size(736, 17);
            this.WebBrowser1.TabIndex = 9;
            this.WebBrowser1.Visible = false;
            // 
            // ButtonOk
            // 
            this.ButtonOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOk.Location = new System.Drawing.Point(319, 149);
            this.ButtonOk.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(113, 36);
            this.ButtonOk.TabIndex = 11;
            this.ButtonOk.Text = "Close To Set Key";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Visible = false;
            // 
            // KeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 173);
            this.Controls.Add(this.TableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "KeyForm";
            this.Text = "HubOne WFMX Key Fetcher";
            this.TableLayoutPanelHeader.ResumeLayout(false);
            this.TableLayoutPanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.TableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonRetrieveWorkflowMaxKey;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelHeader;
        private System.Windows.Forms.TextBox TextBoxProgress;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        private System.Windows.Forms.WebBrowser WebBrowser1;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}