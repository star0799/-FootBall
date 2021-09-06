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
            this.cbCountry = new System.Windows.Forms.ComboBox();
            this.lbSelect = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTeamsData
            // 
            this.btnTeamsData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTeamsData.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnTeamsData.Location = new System.Drawing.Point(1202, 583);
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
            this.lvShow.Size = new System.Drawing.Size(1333, 363);
            this.lvShow.TabIndex = 1;
            this.lvShow.UseCompatibleStateImageBehavior = false;
            // 
            // lvStatistics
            // 
            this.lvStatistics.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvStatistics.Font = new System.Drawing.Font("新細明體", 12F);
            this.lvStatistics.HideSelection = false;
            this.lvStatistics.Location = new System.Drawing.Point(0, 363);
            this.lvStatistics.Name = "lvStatistics";
            this.lvStatistics.Size = new System.Drawing.Size(1082, 264);
            this.lvStatistics.TabIndex = 2;
            this.lvStatistics.UseCompatibleStateImageBehavior = false;
            // 
            // cbYears
            // 
            this.cbYears.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbYears.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYears.Font = new System.Drawing.Font("新細明體", 14F);
            this.cbYears.FormattingEnabled = true;
            this.cbYears.Location = new System.Drawing.Point(1173, 454);
            this.cbYears.Margin = new System.Windows.Forms.Padding(3, 3, 30, 30);
            this.cbYears.Name = "cbYears";
            this.cbYears.Size = new System.Drawing.Size(148, 27);
            this.cbYears.TabIndex = 3;
            // 
            // cbTeam
            // 
            this.cbTeam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTeam.Font = new System.Drawing.Font("新細明體", 14F);
            this.cbTeam.FormattingEnabled = true;
            this.cbTeam.Location = new System.Drawing.Point(1173, 532);
            this.cbTeam.Name = "cbTeam";
            this.cbTeam.Size = new System.Drawing.Size(148, 27);
            this.cbTeam.TabIndex = 4;
            // 
            // cbCountry
            // 
            this.cbCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCountry.Font = new System.Drawing.Font("新細明體", 14F);
            this.cbCountry.FormattingEnabled = true;
            this.cbCountry.Location = new System.Drawing.Point(1173, 369);
            this.cbCountry.Margin = new System.Windows.Forms.Padding(3, 3, 30, 30);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(148, 27);
            this.cbCountry.TabIndex = 5;
            this.cbCountry.SelectedIndexChanged += new System.EventHandler(this.cbCountry_SelectedIndexChanged);
            // 
            // lbSelect
            // 
            this.lbSelect.AutoSize = true;
            this.lbSelect.Font = new System.Drawing.Font("新細明體", 12F);
            this.lbSelect.ForeColor = System.Drawing.Color.Red;
            this.lbSelect.Location = new System.Drawing.Point(1186, 374);
            this.lbSelect.Name = "lbSelect";
            this.lbSelect.Size = new System.Drawing.Size(108, 16);
            this.lbSelect.TabIndex = 6;
            this.lbSelect.Text = "請先選擇國家:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 627);
            this.Controls.Add(this.lbSelect);
            this.Controls.Add(this.cbCountry);
            this.Controls.Add(this.cbTeam);
            this.Controls.Add(this.cbYears);
            this.Controls.Add(this.lvStatistics);
            this.Controls.Add(this.lvShow);
            this.Controls.Add(this.btnTeamsData);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTeamsData;
        private System.Windows.Forms.ListView lvShow;
        private System.Windows.Forms.ListView lvStatistics;
        private System.Windows.Forms.ComboBox cbYears;
        private System.Windows.Forms.ComboBox cbTeam;
        private System.Windows.Forms.ComboBox cbCountry;
        private System.Windows.Forms.Label lbSelect;
    }
}

