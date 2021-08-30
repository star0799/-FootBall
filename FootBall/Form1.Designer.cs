namespace FootBall
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTeamsData = new System.Windows.Forms.Button();
            this.lvShow = new System.Windows.Forms.ListView();
            this.lvStatistics = new System.Windows.Forms.ListView();
            this.cbYears = new System.Windows.Forms.ComboBox();
            this.cbTeam = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnTeamsData
            // 
            this.btnTeamsData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTeamsData.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnTeamsData.Location = new System.Drawing.Point(1348, 444);
            this.btnTeamsData.Name = "btnTeamsData";
            this.btnTeamsData.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnTeamsData.Size = new System.Drawing.Size(92, 32);
            this.btnTeamsData.TabIndex = 0;
            this.btnTeamsData.Text = "球隊資訊";
            this.btnTeamsData.UseVisualStyleBackColor = true;
            this.btnTeamsData.Click += new System.EventHandler(this.button1_Click);
            // 
            // lvShow
            // 
            this.lvShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvShow.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lvShow.HideSelection = false;
            this.lvShow.Location = new System.Drawing.Point(0, 0);
            this.lvShow.Name = "lvShow";
            this.lvShow.Size = new System.Drawing.Size(1440, 438);
            this.lvShow.TabIndex = 1;
            this.lvShow.UseCompatibleStateImageBehavior = false;
            // 
            // lvStatistics
            // 
            this.lvStatistics.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvStatistics.Font = new System.Drawing.Font("新細明體", 12F);
            this.lvStatistics.HideSelection = false;
            this.lvStatistics.Location = new System.Drawing.Point(0, 438);
            this.lvStatistics.Name = "lvStatistics";
            this.lvStatistics.Size = new System.Drawing.Size(1223, 236);
            this.lvStatistics.TabIndex = 2;
            this.lvStatistics.UseCompatibleStateImageBehavior = false;
            // 
            // cbYears
            // 
            this.cbYears.FormattingEnabled = true;
            this.cbYears.Location = new System.Drawing.Point(1245, 524);
            this.cbYears.Name = "cbYears";
            this.cbYears.Size = new System.Drawing.Size(121, 20);
            this.cbYears.TabIndex = 3;
            // 
            // cbTeam
            // 
            this.cbTeam.FormattingEnabled = true;
            this.cbTeam.Location = new System.Drawing.Point(1245, 577);
            this.cbTeam.Name = "cbTeam";
            this.cbTeam.Size = new System.Drawing.Size(121, 20);
            this.cbTeam.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 674);
            this.Controls.Add(this.cbTeam);
            this.Controls.Add(this.cbYears);
            this.Controls.Add(this.lvStatistics);
            this.Controls.Add(this.lvShow);
            this.Controls.Add(this.btnTeamsData);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTeamsData;
        private System.Windows.Forms.ListView lvShow;
        private System.Windows.Forms.ListView lvStatistics;
        private System.Windows.Forms.ComboBox cbYears;
        private System.Windows.Forms.ComboBox cbTeam;
    }
}

