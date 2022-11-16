namespace CrawlWebBDS
{
    partial class GuideForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.infoTBox = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.saveGuideInfoBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.saveGuideInfoBtn);
            this.groupBox1.Controls.Add(this.infoTBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 450);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin lưu trữ";
            // 
            // infoTBox
            // 
            this.infoTBox.Location = new System.Drawing.Point(12, 19);
            this.infoTBox.Multiline = true;
            this.infoTBox.Name = "infoTBox";
            this.infoTBox.Size = new System.Drawing.Size(776, 366);
            this.infoTBox.TabIndex = 0;
            // 
            // saveGuideInfoBtn
            // 
            this.saveGuideInfoBtn.BackColor = System.Drawing.Color.IndianRed;
            this.saveGuideInfoBtn.Location = new System.Drawing.Point(658, 391);
            this.saveGuideInfoBtn.Name = "saveGuideInfoBtn";
            this.saveGuideInfoBtn.Size = new System.Drawing.Size(130, 53);
            this.saveGuideInfoBtn.TabIndex = 1;
            this.saveGuideInfoBtn.Text = "Save ";
            this.saveGuideInfoBtn.UseVisualStyleBackColor = false;
            this.saveGuideInfoBtn.Click += new System.EventHandler(this.saveGuideInfoBtn_Click);
            // 
            // GuideForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GuideForm";
            this.Text = "GuideForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox infoTBox;
        private System.Windows.Forms.Button saveGuideInfoBtn;
    }
}