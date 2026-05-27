namespace Poker
{
    partial class frmPoker
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpCards = new System.Windows.Forms.GroupBox();
            this.grpControls = new System.Windows.Forms.GroupBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblResultCaption = new System.Windows.Forms.Label();
            this.lblReplaceInfo = new System.Windows.Forms.Label();
            this.lblReplaceCaption = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblScoreCaption = new System.Windows.Forms.Label();
            this.lblPreviewScore = new System.Windows.Forms.Label();
            this.lblPreviewScoreCaption = new System.Windows.Forms.Label();
            this.lblChanges = new System.Windows.Forms.Label();
            this.lblChangesCaption = new System.Windows.Forms.Label();
            this.lblRound = new System.Windows.Forms.Label();
            this.lblRoundCaption = new System.Windows.Forms.Label();
            this.cboScoreCategory = new System.Windows.Forms.ComboBox();
            this.lblScoreCategory = new System.Windows.Forms.Label();
            this.grpScoreTable = new System.Windows.Forms.GroupBox();
            this.dgvScoreBoard = new System.Windows.Forms.DataGridView();
            this.btnDealCard = new System.Windows.Forms.Button();
            this.btnChangeCard = new System.Windows.Forms.Button();
            this.btnSettle = new System.Windows.Forms.Button();
            this.btnRules = new System.Windows.Forms.Button();
            this.grpControls.SuspendLayout();
            this.grpScoreTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScoreBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft JhengHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTitle.Location = new System.Drawing.Point(24, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(205, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "紙牌快艇 Yacht";
            // 
            // grpCards
            // 
            this.grpCards.Font = new System.Drawing.Font("Microsoft JhengHei", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpCards.Location = new System.Drawing.Point(24, 66);
            this.grpCards.Name = "grpCards";
            this.grpCards.Size = new System.Drawing.Size(600, 230);
            this.grpCards.TabIndex = 1;
            this.grpCards.TabStop = false;
            this.grpCards.Text = "手牌（點牌切換保留/換牌）";
            // 
            // grpControls
            // 
            this.grpControls.Controls.Add(this.lblResult);
            this.grpControls.Controls.Add(this.lblResultCaption);
            this.grpControls.Controls.Add(this.lblReplaceInfo);
            this.grpControls.Controls.Add(this.lblReplaceCaption);
            this.grpControls.Controls.Add(this.lblScore);
            this.grpControls.Controls.Add(this.lblScoreCaption);
            this.grpControls.Controls.Add(this.lblPreviewScore);
            this.grpControls.Controls.Add(this.lblPreviewScoreCaption);
            this.grpControls.Controls.Add(this.lblChanges);
            this.grpControls.Controls.Add(this.lblChangesCaption);
            this.grpControls.Controls.Add(this.lblRound);
            this.grpControls.Controls.Add(this.lblRoundCaption);
            this.grpControls.Controls.Add(this.cboScoreCategory);
            this.grpControls.Controls.Add(this.lblScoreCategory);
            this.grpControls.Font = new System.Drawing.Font("Microsoft JhengHei", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpControls.Location = new System.Drawing.Point(650, 66);
            this.grpControls.Name = "grpControls";
            this.grpControls.Size = new System.Drawing.Size(330, 400);
            this.grpControls.TabIndex = 2;
            this.grpControls.TabStop = false;
            this.grpControls.Text = "控制台";
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblResult.Location = new System.Drawing.Point(20, 312);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(290, 72);
            this.lblResult.TabIndex = 11;
            // 
            // lblResultCaption
            // 
            this.lblResultCaption.AutoSize = true;
            this.lblResultCaption.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblResultCaption.Location = new System.Drawing.Point(20, 291);
            this.lblResultCaption.Name = "lblResultCaption";
            this.lblResultCaption.Size = new System.Drawing.Size(64, 18);
            this.lblResultCaption.TabIndex = 10;
            this.lblResultCaption.Text = "本局結果";
            // 
            // lblReplaceInfo
            // 
            this.lblReplaceInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReplaceInfo.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblReplaceInfo.Location = new System.Drawing.Point(20, 228);
            this.lblReplaceInfo.Name = "lblReplaceInfo";
            this.lblReplaceInfo.Size = new System.Drawing.Size(290, 52);
            this.lblReplaceInfo.TabIndex = 9;
            // 
            // lblReplaceCaption
            // 
            this.lblReplaceCaption.AutoSize = true;
            this.lblReplaceCaption.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblReplaceCaption.Location = new System.Drawing.Point(20, 207);
            this.lblReplaceCaption.Name = "lblReplaceCaption";
            this.lblReplaceCaption.Size = new System.Drawing.Size(78, 18);
            this.lblReplaceCaption.TabIndex = 8;
            this.lblReplaceCaption.Text = "準備換牌";
            // 
            // lblScore
            // 
            this.lblScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblScore.Location = new System.Drawing.Point(98, 170);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(80, 26);
            this.lblScore.TabIndex = 7;
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScoreCaption
            // 
            this.lblScoreCaption.AutoSize = true;
            this.lblScoreCaption.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblScoreCaption.Location = new System.Drawing.Point(20, 174);
            this.lblScoreCaption.Name = "lblScoreCaption";
            this.lblScoreCaption.Size = new System.Drawing.Size(64, 18);
            this.lblScoreCaption.TabIndex = 6;
            this.lblScoreCaption.Text = "目前總分";
            // 
            // lblPreviewScore
            // 
            this.lblPreviewScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPreviewScore.Location = new System.Drawing.Point(250, 170);
            this.lblPreviewScore.Name = "lblPreviewScore";
            this.lblPreviewScore.Size = new System.Drawing.Size(60, 26);
            this.lblPreviewScore.TabIndex = 9;
            this.lblPreviewScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPreviewScoreCaption
            // 
            this.lblPreviewScoreCaption.AutoSize = true;
            this.lblPreviewScoreCaption.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPreviewScoreCaption.Location = new System.Drawing.Point(184, 174);
            this.lblPreviewScoreCaption.Name = "lblPreviewScoreCaption";
            this.lblPreviewScoreCaption.Size = new System.Drawing.Size(64, 18);
            this.lblPreviewScoreCaption.TabIndex = 8;
            this.lblPreviewScoreCaption.Text = "預覽得分";
            // 
            // lblChanges
            // 
            this.lblChanges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblChanges.Location = new System.Drawing.Point(250, 132);
            this.lblChanges.Name = "lblChanges";
            this.lblChanges.Size = new System.Drawing.Size(60, 26);
            this.lblChanges.TabIndex = 5;
            this.lblChanges.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblChangesCaption
            // 
            this.lblChangesCaption.AutoSize = true;
            this.lblChangesCaption.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblChangesCaption.Location = new System.Drawing.Point(174, 136);
            this.lblChangesCaption.Name = "lblChangesCaption";
            this.lblChangesCaption.Size = new System.Drawing.Size(64, 18);
            this.lblChangesCaption.TabIndex = 4;
            this.lblChangesCaption.Text = "剩餘換牌";
            // 
            // lblRound
            // 
            this.lblRound.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRound.Location = new System.Drawing.Point(98, 132);
            this.lblRound.Name = "lblRound";
            this.lblRound.Size = new System.Drawing.Size(60, 26);
            this.lblRound.TabIndex = 3;
            this.lblRound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRoundCaption
            // 
            this.lblRoundCaption.AutoSize = true;
            this.lblRoundCaption.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblRoundCaption.Location = new System.Drawing.Point(20, 136);
            this.lblRoundCaption.Name = "lblRoundCaption";
            this.lblRoundCaption.Size = new System.Drawing.Size(36, 18);
            this.lblRoundCaption.TabIndex = 2;
            this.lblRoundCaption.Text = "局數";
            // 
            // cboScoreCategory
            // 
            this.cboScoreCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScoreCategory.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboScoreCategory.FormattingEnabled = true;
            this.cboScoreCategory.Location = new System.Drawing.Point(98, 92);
            this.cboScoreCategory.Name = "cboScoreCategory";
            this.cboScoreCategory.Size = new System.Drawing.Size(212, 25);
            this.cboScoreCategory.TabIndex = 1;
            this.cboScoreCategory.SelectedIndexChanged += new System.EventHandler(this.cboScoreCategory_SelectedIndexChanged);
            // 
            // lblScoreCategory
            // 
            this.lblScoreCategory.AutoSize = true;
            this.lblScoreCategory.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblScoreCategory.Location = new System.Drawing.Point(20, 96);
            this.lblScoreCategory.Name = "lblScoreCategory";
            this.lblScoreCategory.Size = new System.Drawing.Size(64, 18);
            this.lblScoreCategory.TabIndex = 0;
            this.lblScoreCategory.Text = "本局填入";
            // 
            // grpScoreTable
            // 
            this.grpScoreTable.Controls.Add(this.dgvScoreBoard);
            this.grpScoreTable.Font = new System.Drawing.Font("Microsoft JhengHei", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpScoreTable.Location = new System.Drawing.Point(24, 316);
            this.grpScoreTable.Name = "grpScoreTable";
            this.grpScoreTable.Size = new System.Drawing.Size(600, 320);
            this.grpScoreTable.TabIndex = 3;
            this.grpScoreTable.TabStop = false;
            this.grpScoreTable.Text = "計分表（每格只能填一次）";
            // 
            // dgvScoreBoard
            // 
            this.dgvScoreBoard.AllowUserToAddRows = false;
            this.dgvScoreBoard.AllowUserToDeleteRows = false;
            this.dgvScoreBoard.AllowUserToResizeColumns = false;
            this.dgvScoreBoard.AllowUserToResizeRows = false;
            this.dgvScoreBoard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScoreBoard.Location = new System.Drawing.Point(20, 32);
            this.dgvScoreBoard.MultiSelect = false;
            this.dgvScoreBoard.Name = "dgvScoreBoard";
            this.dgvScoreBoard.ReadOnly = true;
            this.dgvScoreBoard.RowHeadersVisible = false;
            this.dgvScoreBoard.RowTemplate.Height = 24;
            this.dgvScoreBoard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvScoreBoard.Size = new System.Drawing.Size(560, 280);
            this.dgvScoreBoard.TabIndex = 0;
            // 
            // btnDealCard
            // 
            this.btnDealCard.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDealCard.Location = new System.Drawing.Point(24, 650);
            this.btnDealCard.Name = "btnDealCard";
            this.btnDealCard.Size = new System.Drawing.Size(190, 42);
            this.btnDealCard.TabIndex = 4;
            this.btnDealCard.Text = "發牌 / 下一局";
            this.btnDealCard.UseVisualStyleBackColor = true;
            this.btnDealCard.Click += new System.EventHandler(this.btnDealCard_Click);
            // 
            // btnChangeCard
            // 
            this.btnChangeCard.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnChangeCard.Location = new System.Drawing.Point(231, 650);
            this.btnChangeCard.Name = "btnChangeCard";
            this.btnChangeCard.Size = new System.Drawing.Size(120, 42);
            this.btnChangeCard.TabIndex = 5;
            this.btnChangeCard.Text = "換牌";
            this.btnChangeCard.UseVisualStyleBackColor = true;
            this.btnChangeCard.Click += new System.EventHandler(this.btnChangeCard_Click);
            // 
            // btnSettle
            // 
            this.btnSettle.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSettle.Location = new System.Drawing.Point(368, 650);
            this.btnSettle.Name = "btnSettle";
            this.btnSettle.Size = new System.Drawing.Size(120, 42);
            this.btnSettle.TabIndex = 6;
            this.btnSettle.Text = "填表結算";
            this.btnSettle.UseVisualStyleBackColor = true;
            this.btnSettle.Click += new System.EventHandler(this.btnSettle_Click);
            // 
            // btnRules
            // 
            this.btnRules.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnRules.Location = new System.Drawing.Point(504, 650);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(120, 42);
            this.btnRules.TabIndex = 7;
            this.btnRules.Text = "規則";
            this.btnRules.UseVisualStyleBackColor = true;
            this.btnRules.Click += new System.EventHandler(this.btnRules_Click);
            // 
            // frmPoker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 760);
            this.Controls.Add(this.btnRules);
            this.Controls.Add(this.btnSettle);
            this.Controls.Add(this.btnChangeCard);
            this.Controls.Add(this.btnDealCard);
            this.Controls.Add(this.grpScoreTable);
            this.Controls.Add(this.grpControls);
            this.Controls.Add(this.grpCards);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 799);
            this.Name = "frmPoker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "紙牌快艇";
            this.grpControls.ResumeLayout(false);
            this.grpControls.PerformLayout();
            this.grpScoreTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScoreBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpCards;
        private System.Windows.Forms.GroupBox grpControls;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblResultCaption;
        private System.Windows.Forms.Label lblReplaceInfo;
        private System.Windows.Forms.Label lblReplaceCaption;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblScoreCaption;
        private System.Windows.Forms.Label lblPreviewScore;
        private System.Windows.Forms.Label lblPreviewScoreCaption;
        private System.Windows.Forms.Label lblChanges;
        private System.Windows.Forms.Label lblChangesCaption;
        private System.Windows.Forms.Label lblRound;
        private System.Windows.Forms.Label lblRoundCaption;
        private System.Windows.Forms.ComboBox cboScoreCategory;
        private System.Windows.Forms.Label lblScoreCategory;
        private System.Windows.Forms.GroupBox grpScoreTable;
        private System.Windows.Forms.DataGridView dgvScoreBoard;
        private System.Windows.Forms.Button btnDealCard;
        private System.Windows.Forms.Button btnChangeCard;
        private System.Windows.Forms.Button btnSettle;
        private System.Windows.Forms.Button btnRules;
    }
}
