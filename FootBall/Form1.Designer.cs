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
            this.cbTeam2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTeamsData
            // 
            this.btnTeamsData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTeamsData.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnTeamsData.Location = new System.Drawing.Point(1242, 600);
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
            this.lvShow.Size = new System.Drawing.Size(1346, 363);
            this.lvShow.TabIndex = 1;
            this.lvShow.UseCompatibleStateImageBehavior = false;
            this.lvShow.SelectedIndexChanged += new System.EventHandler(this.lvShow_SelectedIndexChanged);
            // 
            // lvStatistics
            // 
            this.lvStatistics.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvStatistics.Font = new System.Drawing.Font("新細明體", 12F);
            this.lvStatistics.HideSelection = false;
            this.lvStatistics.Location = new System.Drawing.Point(0, 363);
            this.lvStatistics.Name = "lvStatistics";
            this.lvStatistics.Size = new System.Drawing.Size(1070, 304);
            this.lvStatistics.TabIndex = 2;
            this.lvStatistics.UseCompatibleStateImageBehavior = false;
            // 
            // cbYears
            // 
            this.cbYears.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbYears.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYears.Font = new System.Drawing.Font("新細明體", 14F);
            this.cbYears.FormattingEnabled = true;
            this.cbYears.Location = new System.Drawing.Point(1186, 428);
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
            this.cbTeam.Location = new System.Drawing.Point(1186, 488);
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
            this.cbCountry.Location = new System.Drawing.Point(1186, 369);
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
            this.lbSelect.Location = new System.Drawing.Point(1128, 374);
            this.lbSelect.Name = "lbSelect";
            this.lbSelect.Size = new System.Drawing.Size(108, 16);
            this.lbSelect.TabIndex = 6;
            this.lbSelect.Text = "請先選擇國家:";
            this.lbSelect.Click += new System.EventHandler(this.lbSelect_Click);
            // 
            // cbTeam2
            // 
            this.cbTeam2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTeam2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTeam2.Font = new System.Drawing.Font("新細明體", 14F);
            this.cbTeam2.FormattingEnabled = true;
            this.cbTeam2.Location = new System.Drawing.Point(1186, 552);
            this.cbTeam2.Name = "cbTeam2";
            this.cbTeam2.Size = new System.Drawing.Size(148, 27);
            this.cbTeam2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1076, 378);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "國家:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1076, 436);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "年份:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1076, 496);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "隊伍1:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1076, 560);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "隊伍2:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("新細明體", 8F);
            this.btnUpdate.Location = new System.Drawing.Point(1131, 585);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnUpdate.Size = new System.Drawing.Size(65, 26);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Font = new System.Drawing.Font("新細明體", 8F);
            this.btnExcel.Location = new System.Drawing.Point(1131, 631);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnExcel.Size = new System.Drawing.Size(65, 24);
            this.btnExcel.TabIndex = 13;
            this.btnExcel.Text = "匯出Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 667);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTeam2);
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
        private System.Windows.Forms.ComboBox cbTeam2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnExcel;
    }
}

