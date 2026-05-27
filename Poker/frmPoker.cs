using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Poker
{
    public partial class frmPoker : Form
    {
        private const int HandSize = 5;
        private const int MaxChanges = 3;

        private static readonly ScoreRule[] ScoreRules =
        {
            new ScoreRule("一對", 1, 10),
            new ScoreRule("兩對", 2, 20),
            new ScoreRule("三條", 3, 30),
            new ScoreRule("順子", 4, 40),
            new ScoreRule("同花", 5, 50),
            new ScoreRule("葫蘆", 6, 60),
            new ScoreRule("鐵支", 7, 80),
            new ScoreRule("同花順", 8, 120),
            new ScoreRule("皇家同花順", 9, 200),
            new ScoreRule("機會", -1, 0)
        };

        private readonly PictureBox[] picCards = new PictureBox[HandSize];
        private readonly Label[] lblCardStates = new Label[HandSize];
        private readonly bool[] replaceCards = new bool[HandSize];
        private readonly bool[] usedRules = new bool[ScoreRules.Length];
        private readonly int[] ruleScores = new int[ScoreRules.Length];
        private readonly Random rand = new Random();

        private List<int> deck = new List<int>();
        private int deckIndex;
        private int[] hand = new int[HandSize];
        private int roundCount;
        private int changesLeft;
        private int totalScore;
        private int highScore;
        private bool roundActive;

        private SoundManager soundManager;
        private CheckBox chkSound;
        private TrackBar trkVolume;
        private Label lblVolume;
        private Button btnReplay;
        private Button btnTotalScore;
        private readonly string highScorePath = Path.Combine(Application.StartupPath, "highscore.txt");

        public frmPoker()
        {
            InitializeComponent();
            ApplyTheme();
            InitializeCards();
            InitializeScoreTable();
            InitializeReplayButton();
            InitializeTotalScoreButton();
            InitializeSoundUi();
            InitializeSoundManager();
            LoadHighScore();
            this.FormClosing += frmPoker_FormClosing;
            this.Shown += frmPoker_Shown;
            ResetRoundUi();
            RefreshScoreChoices();
            RefreshScoreBoard();
            PerformLayoutFixes();
        }

        private void InitializeReplayButton()
        {
            btnReplay = new Button();
            btnReplay.Font = new Font("Microsoft JhengHei", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(136)));
            btnReplay.Location = new Point(650, 650);
            btnReplay.Name = "btnReplay";
            btnReplay.Size = new Size(120, 42);
            btnReplay.TabIndex = 8;
            btnReplay.Text = "重玩";
            btnReplay.UseVisualStyleBackColor = true;
            btnReplay.Click += btnReplay_Click;
            Controls.Add(btnReplay);
            StyleButton(btnReplay, Color.FromArgb(102, 140, 196), Color.White);
        }

        private void InitializeTotalScoreButton()
        {
            btnTotalScore = new Button();
            btnTotalScore.Font = new Font("Microsoft JhengHei", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(136)));
            btnTotalScore.Location = new Point(786, 650);
            btnTotalScore.Name = "btnTotalScore";
            btnTotalScore.Size = new Size(180, 42);
            btnTotalScore.TabIndex = 9;
            btnTotalScore.Text = "總得分 / 最高分";
            btnTotalScore.UseVisualStyleBackColor = true;
            btnTotalScore.Click += btnTotalScore_Click;
            Controls.Add(btnTotalScore);
            StyleButton(btnTotalScore, Color.FromArgb(116, 190, 142), Color.FromArgb(20, 36, 26));
        }

        private void InitializeSoundUi()
        {
            chkSound = new CheckBox();
            chkSound.AutoSize = true;
            chkSound.Text = "音效";
            chkSound.Left = 20;
            chkSound.Top = 58;
            chkSound.Checked = true;
            chkSound.ForeColor = Color.FromArgb(233, 238, 245);
            chkSound.CheckedChanged += SoundUiChanged;
            grpControls.Controls.Add(chkSound);

            lblVolume = new Label();
            lblVolume.AutoSize = true;
            lblVolume.Text = "音量";
            lblVolume.Left = 108;
            lblVolume.Top = 60;
            lblVolume.ForeColor = Color.FromArgb(179, 190, 205);
            grpControls.Controls.Add(lblVolume);

            trkVolume = new TrackBar();
            trkVolume.Left = 146;
            trkVolume.Top = 52;
            trkVolume.Width = 164;
            trkVolume.Minimum = 0;
            trkVolume.Maximum = 100;
            trkVolume.TickFrequency = 10;
            trkVolume.SmallChange = 5;
            trkVolume.LargeChange = 10;
            trkVolume.Value = 70;
            trkVolume.BackColor = Color.FromArgb(35, 41, 52);
            trkVolume.Scroll += SoundUiChanged;
            grpControls.Controls.Add(trkVolume);
        }

        private void InitializeSoundManager()
        {
            soundManager = new SoundManager();
            ApplySoundSettings();
        }

        private void SoundUiChanged(object sender, EventArgs e)
        {
            ApplySoundSettings();
            soundManager.Play(SoundEvent.Toggle);
        }

        private void ApplySoundSettings()
        {
            soundManager.Enabled = chkSound.Checked;
            soundManager.Volume = trkVolume.Value / 100f;
        }

        private void ApplyTheme()
        {
            Color page = Color.FromArgb(24, 28, 36);
            Color panel = Color.FromArgb(35, 41, 52);
            Color panelAlt = Color.FromArgb(41, 48, 61);
            Color ink = Color.FromArgb(233, 238, 245);
            Color muted = Color.FromArgb(179, 190, 205);
            Color accent = Color.FromArgb(229, 192, 93);

            BackColor = page;
            ForeColor = ink;
            foreach (GroupBox group in new[] { grpCards, grpControls, grpScoreTable })
            {
                group.BackColor = panel;
                group.ForeColor = ink;
            }

            foreach (Control control in grpControls.Controls)
            {
                Label label = control as Label;
                if (label != null)
                {
                    label.ForeColor = label.BorderStyle == BorderStyle.None ? muted : ink;
                    if (label.BorderStyle != BorderStyle.None) label.BackColor = panelAlt;
                }
            }

            lblTitle.ForeColor = ink;
            lblReplaceInfo.BackColor = Color.FromArgb(27, 32, 42);
            lblResult.BackColor = Color.FromArgb(27, 32, 42);
            lblResult.ForeColor = accent;
            dgvScoreBoard.BackgroundColor = Color.FromArgb(27, 32, 42);
            dgvScoreBoard.GridColor = Color.FromArgb(65, 75, 91);
            dgvScoreBoard.DefaultCellStyle.BackColor = Color.FromArgb(27, 32, 42);
            dgvScoreBoard.DefaultCellStyle.ForeColor = ink;
            dgvScoreBoard.DefaultCellStyle.SelectionBackColor = Color.FromArgb(65, 75, 91);
            dgvScoreBoard.DefaultCellStyle.SelectionForeColor = ink;
            dgvScoreBoard.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 48, 61);
            dgvScoreBoard.ColumnHeadersDefaultCellStyle.ForeColor = ink;
            dgvScoreBoard.EnableHeadersVisualStyles = false;
            cboScoreCategory.BackColor = Color.FromArgb(237, 239, 232);
            cboScoreCategory.ForeColor = Color.FromArgb(25, 29, 36);

            StyleButton(btnDealCard, accent, Color.FromArgb(32, 30, 24));
            StyleButton(btnChangeCard, Color.FromArgb(102, 140, 196), Color.White);
            StyleButton(btnSettle, Color.FromArgb(116, 190, 142), Color.FromArgb(20, 36, 26));
            StyleButton(btnRules, Color.FromArgb(71, 78, 92), Color.White);
            if (btnReplay != null) StyleButton(btnReplay, Color.FromArgb(102, 140, 196), Color.White);
            if (btnTotalScore != null) StyleButton(btnTotalScore, Color.FromArgb(116, 190, 142), Color.FromArgb(20, 36, 26));
        }

        private void StyleButton(Button button, Color backColor, Color foreColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.Cursor = Cursors.Hand;
        }

        private void InitializeCards()
        {
            for (int i = 0; i < HandSize; i++)
            {
                PictureBox card = new PictureBox();
                card.Name = "picCard" + i;
                card.Size = new Size(96, 132);
                card.SizeMode = PictureBoxSizeMode.StretchImage;
                card.BorderStyle = BorderStyle.FixedSingle;
                card.Left = 24 + i * 112;
                card.Top = 34;
                card.Image = GetImage("back");
                card.Tag = i;
                card.Click += Card_Click;
                grpCards.Controls.Add(card);
                picCards[i] = card;

                Label state = new Label();
                state.Name = "lblCardState" + i;
                state.Left = card.Left;
                state.Top = card.Bottom + 10;
                state.Size = new Size(card.Width, 26);
                state.TextAlign = ContentAlignment.MiddleCenter;
                state.Font = new Font("微軟正黑體", 9F, FontStyle.Bold);
                state.BackColor = Color.FromArgb(65, 75, 91);
                state.ForeColor = Color.FromArgb(230, 236, 244);
                state.Text = "保留";
                state.Tag = i;
                state.Click += Card_Click;
                grpCards.Controls.Add(state);
                lblCardStates[i] = state;
            }
        }

        private void InitializeScoreTable()
        {
            dgvScoreBoard.Columns.Clear();
            dgvScoreBoard.Columns.Add("colName", "計分類別");
            dgvScoreBoard.Columns.Add("colPreview", "本局預覽");
            dgvScoreBoard.Columns.Add("colScore", "已填分數");
            dgvScoreBoard.Columns.Add("colUsed", "狀態");
            dgvScoreBoard.Columns[0].Width = 170;
            dgvScoreBoard.Columns[1].Width = 95;
            dgvScoreBoard.Columns[2].Width = 95;
            dgvScoreBoard.Columns[3].Width = 90;
        }

        private void PerformLayoutFixes()
        {
            EnsureScoreBoardFullyVisible();
            AlignBottomButtons();
        }

        private void EnsureScoreBoardFullyVisible()
        {
            int headerHeight = dgvScoreBoard.ColumnHeadersVisible ? dgvScoreBoard.ColumnHeadersHeight : 0;
            int rowHeight = dgvScoreBoard.RowTemplate.Height > 0 ? dgvScoreBoard.RowTemplate.Height : 24;
            int neededGridHeight = headerHeight + (ScoreRules.Length * rowHeight) + 6;
            int neededGroupHeight = neededGridHeight + 50;
            if (grpScoreTable.Height < neededGroupHeight) grpScoreTable.Height = neededGroupHeight;
            dgvScoreBoard.Height = grpScoreTable.Height - 47;

            int targetHeight = grpScoreTable.Bottom + 95;
            if (ClientSize.Height != targetHeight) ClientSize = new Size(ClientSize.Width, targetHeight);
            MinimumSize = new Size(MinimumSize.Width, targetHeight + 39);
        }

        private void AlignBottomButtons()
        {
            const int gap = 12;
            int y = grpScoreTable.Bottom + 14;

            btnDealCard.Width = 160;
            btnChangeCard.Width = 120;
            btnSettle.Width = 120;
            btnRules.Width = 100;
            btnReplay.Width = 100;
            btnTotalScore.Width = 160;

            int totalWidth = btnDealCard.Width + btnChangeCard.Width + btnSettle.Width +
                             btnRules.Width + btnReplay.Width + btnTotalScore.Width + (gap * 5);
            int startX = Math.Max(24, (ClientSize.Width - totalWidth) / 2);

            btnDealCard.Location = new Point(startX, y);
            btnChangeCard.Location = new Point(btnDealCard.Right + gap, y);
            btnSettle.Location = new Point(btnChangeCard.Right + gap, y);
            btnRules.Location = new Point(btnSettle.Right + gap, y);
            btnReplay.Location = new Point(btnRules.Right + gap, y);
            btnTotalScore.Location = new Point(btnReplay.Right + gap, y);
        }

        private Image GetImage(string name)
        {
            return Properties.Resources.ResourceManager.GetObject(name) as Image;
        }

        private void BuildDeck()
        {
            deck = Enumerable.Range(0, 52).ToList();
            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                int temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
            deckIndex = 0;
        }

        private int DrawCard()
        {
            return deck[deckIndex++];
        }

        private void DealNewRound()
        {
            if (usedRules.All(x => x))
            {
                ShowGameOverDialog();
                return;
            }

            BuildDeck();
            for (int i = 0; i < HandSize; i++)
            {
                hand[i] = DrawCard();
                replaceCards[i] = false;
            }

            roundCount++;
            changesLeft = MaxChanges;
            roundActive = true;
            RefreshCards();
            RefreshScoreChoices();
            lblRound.Text = roundCount.ToString();
            lblChanges.Text = changesLeft.ToString();
            lblResult.Text = "選擇要換的牌，或直接填表結算。";
            UpdatePreviewScore();
            btnDealCard.Enabled = false;
            btnChangeCard.Enabled = true;
            btnSettle.Enabled = true;
            soundManager.Play(SoundEvent.Deal);
        }

        private void RefreshCards()
        {
            for (int i = 0; i < HandSize; i++)
            {
                picCards[i].Image = roundActive ? GetImage("pic" + (hand[i] + 1)) : GetImage("back");
            }
            UpdateReplaceIndicators();
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (!roundActive) return;
            Control control = sender as Control;
            int index = (int)control.Tag;
            replaceCards[index] = !replaceCards[index];
            UpdateReplaceIndicators();
            soundManager.Play(SoundEvent.SelectCard);
        }

        private void UpdateReplaceIndicators()
        {
            List<string> selected = new List<string>();
            for (int i = 0; i < HandSize; i++)
            {
                bool replace = replaceCards[i];
                if (lblCardStates[i] != null)
                {
                    lblCardStates[i].Text = replace ? "換牌" : "保留";
                    lblCardStates[i].BackColor = replace ? Color.FromArgb(229, 192, 93) : Color.FromArgb(65, 75, 91);
                    lblCardStates[i].ForeColor = replace ? Color.FromArgb(27, 32, 42) : Color.FromArgb(230, 236, 244);
                }
                if (picCards[i] != null) picCards[i].BorderStyle = replace ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
                if (replace) selected.Add("第 " + (i + 1) + " 張");
            }

            lblReplaceInfo.Text = selected.Count == 0
                ? "目前沒有選擇要換的牌。點手牌可切換保留/換牌。"
                : "準備換：" + string.Join("、", selected.ToArray());
        }

        private void ChangeCards()
        {
            if (!roundActive || changesLeft <= 0) return;

            List<int> selected = new List<int>();
            for (int i = 0; i < HandSize; i++)
            {
                if (replaceCards[i]) selected.Add(i);
            }

            if (selected.Count == 0)
            {
                MessageBox.Show("請先點選要換掉的手牌。", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (int index in selected)
            {
                hand[index] = DrawCard();
                replaceCards[index] = false;
            }

            changesLeft--;
            lblChanges.Text = changesLeft.ToString();
            RefreshCards();
            lblResult.Text = "換牌完成，還可以換 " + changesLeft + " 次。";
            UpdatePreviewScore();
            if (changesLeft == 0) btnChangeCard.Enabled = false;
            soundManager.Play(SoundEvent.Change);
        }

        private void SettleRound()
        {
            if (!roundActive) return;

            ScoreChoice choice = cboScoreCategory.SelectedItem as ScoreChoice;
            if (choice == null)
            {
                MessageBox.Show("請先選擇本局要填入的計分欄位。", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            HandResult result = EvaluateHand(hand);
            int score = CalculateScore(choice.Index, result);
            usedRules[choice.Index] = true;
            ruleScores[choice.Index] = score;
            totalScore += score;

            lblScore.Text = totalScore.ToString();
            lblResult.Text = "本局牌型：" + result.Name + "，填入「" + ScoreRules[choice.Index].Name + "」得 " + score + " 分。";
            roundActive = false;
            lblPreviewScore.Text = "-";
            btnDealCard.Enabled = true;
            btnChangeCard.Enabled = false;
            btnSettle.Enabled = false;
            RefreshScoreChoices();
            RefreshScoreBoard();
            soundManager.Play(score > 0 ? SoundEvent.SettleWin : SoundEvent.SettleMiss);

            if (usedRules.All(x => x))
            {
                ShowGameOverDialog();
            }
        }

        private int CalculateScore(int ruleIndex, HandResult result)
        {
            ScoreRule rule = ScoreRules[ruleIndex];
            if (rule.Category == -1)
            {
                return hand.Select(GetRankValue).Sum();
            }
            return result.Category == rule.Category ? rule.Score : 0;
        }

        private void RefreshScoreChoices()
        {
            cboScoreCategory.Items.Clear();
            for (int i = 0; i < ScoreRules.Length; i++)
            {
                if (!usedRules[i]) cboScoreCategory.Items.Add(new ScoreChoice(i, ScoreRules[i].Name));
            }
            if (cboScoreCategory.Items.Count > 0) cboScoreCategory.SelectedIndex = 0;
            UpdatePreviewScore();
        }

        private void UpdatePreviewScore()
        {
            if (!roundActive)
            {
                lblPreviewScore.Text = "-";
                RefreshScoreBoard();
                return;
            }

            ScoreChoice choice = cboScoreCategory.SelectedItem as ScoreChoice;
            if (choice == null)
            {
                lblPreviewScore.Text = "-";
                RefreshScoreBoard();
                return;
            }

            HandResult result = EvaluateHand(hand);
            int score = CalculateScore(choice.Index, result);
            lblPreviewScore.Text = score.ToString();
            RefreshScoreBoard();
        }

        private void RefreshScoreBoard()
        {
            dgvScoreBoard.Rows.Clear();
            HandResult current = roundActive ? EvaluateHand(hand) : null;
            int selectedRule = -1;
            ScoreChoice selected = cboScoreCategory.SelectedItem as ScoreChoice;
            if (selected != null) selectedRule = selected.Index;

            for (int i = 0; i < ScoreRules.Length; i++)
            {
                string preview = "-";
                if (roundActive && !usedRules[i] && current != null)
                {
                    preview = CalculateScore(i, current).ToString();
                }

                string value = usedRules[i] ? ruleScores[i].ToString() : "-";
                string status = usedRules[i] ? "已填" : "可填";
                int rowIndex = dgvScoreBoard.Rows.Add(ScoreRules[i].Name, preview, value, status);
                if (i == selectedRule)
                {
                    dgvScoreBoard.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(49, 60, 76);
                }
            }
        }

        private void ResetRoundUi()
        {
            roundActive = false;
            for (int i = 0; i < HandSize; i++)
            {
                replaceCards[i] = false;
                if (picCards[i] != null) picCards[i].Image = GetImage("back");
            }
            lblRound.Text = "0";
            lblChanges.Text = "0";
            lblScore.Text = "0";
            lblPreviewScore.Text = "-";
            lblResult.Text = "按「發牌」開始。";
            UpdateReplaceIndicators();
            btnDealCard.Enabled = true;
            btnChangeCard.Enabled = false;
            btnSettle.Enabled = false;
        }

        private void RestartGame()
        {
            for (int i = 0; i < usedRules.Length; i++)
            {
                usedRules[i] = false;
                ruleScores[i] = 0;
            }

            roundCount = 0;
            totalScore = 0;
            RefreshScoreChoices();
            RefreshScoreBoard();
            ResetRoundUi();
        }

        private void UpdateHighScoreIfNeeded()
        {
            if (totalScore <= highScore) return;
            highScore = totalScore;
            SaveHighScore();
        }

        private void ShowGameOverDialog()
        {
            UpdateHighScoreIfNeeded();
            string message = "本局總分：" + totalScore + Environment.NewLine +
                             "歷史最高分：" + highScore + Environment.NewLine + Environment.NewLine +
                             "要重玩一局嗎？";
            DialogResult result = MessageBox.Show(message, "遊戲結束", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                RestartGame();
            }
            else
            {
                lblResult.Text = "遊戲結束。可按「重玩」開始新遊戲。";
                btnDealCard.Enabled = false;
                btnChangeCard.Enabled = false;
                btnSettle.Enabled = false;
            }
        }

        private void LoadHighScore()
        {
            highScore = 0;
            if (!File.Exists(highScorePath)) return;

            try
            {
                string text = File.ReadAllText(highScorePath).Trim();
                int parsed;
                if (int.TryParse(text, out parsed) && parsed > 0)
                {
                    highScore = parsed;
                }
            }
            catch
            {
                highScore = 0;
            }
        }

        private void SaveHighScore()
        {
            try
            {
                File.WriteAllText(highScorePath, highScore.ToString());
            }
            catch
            {
            }
        }

        private HandResult EvaluateHand(int[] cards)
        {
            int[] ranks = cards.Select(GetRankValue).OrderByDescending(x => x).ToArray();
            int[] suits = cards.Select(GetSuit).ToArray();
            bool flush = suits.Distinct().Count() == 1;
            int[] distinctAsc = ranks.Distinct().OrderBy(x => x).ToArray();
            bool wheel = distinctAsc.SequenceEqual(new[] { 2, 3, 4, 5, 14 });
            bool straight = distinctAsc.Length == 5 && (distinctAsc[4] - distinctAsc[0] == 4 || wheel);
            int straightHigh = wheel ? 5 : ranks.Max();

            var groups = ranks.GroupBy(r => r)
                .Select(g => new { Rank = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ThenByDescending(g => g.Rank)
                .ToArray();

            if (flush && straight && straightHigh == 14) return new HandResult("皇家同花順", 9);
            if (flush && straight) return new HandResult("同花順", 8);
            if (groups[0].Count == 4) return new HandResult("鐵支", 7);
            if (groups[0].Count == 3 && groups[1].Count == 2) return new HandResult("葫蘆", 6);
            if (flush) return new HandResult("同花", 5);
            if (straight) return new HandResult("順子", 4);
            if (groups[0].Count == 3) return new HandResult("三條", 3);
            if (groups[0].Count == 2 && groups[1].Count == 2) return new HandResult("兩對", 2);
            if (groups[0].Count == 2) return new HandResult("一對", 1);
            return new HandResult("高牌", 0);
        }

        private int GetSuit(int card)
        {
            return card % 4;
        }

        private int GetRankIndex(int card)
        {
            return card / 4;
        }

        private int GetRankValue(int card)
        {
            int rank = GetRankIndex(card);
            return rank == 0 ? 14 : rank + 1;
        }

        private void ShowRules()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("紙牌快艇規則");
            sb.AppendLine();
            sb.AppendLine("1. 每局發 5 張手牌。");
            sb.AppendLine("2. 每局最多換牌 3 次，點手牌可切換保留/換牌。");
            sb.AppendLine("3. 結算前選擇一個計分欄位，每個欄位只能填一次。");
            sb.AppendLine("4. 若牌型符合欄位就得分，不符合得 0 分。");
            sb.AppendLine("5. 機會欄不看牌型，直接計算 5 張牌點數總和，A 算 14。");
            sb.AppendLine("6. 計分表填滿後遊戲結束，目標是拿到最高總分。");
            MessageBox.Show(sb.ToString(), "遊戲規則", MessageBoxButtons.OK, MessageBoxIcon.Information);
            soundManager.Play(SoundEvent.OpenRules);
        }

        private void btnDealCard_Click(object sender, EventArgs e) { DealNewRound(); }
        private void btnChangeCard_Click(object sender, EventArgs e) { ChangeCards(); }
        private void btnSettle_Click(object sender, EventArgs e) { SettleRound(); }
        private void btnRules_Click(object sender, EventArgs e) { ShowRules(); }
        private void btnReplay_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "目前總分：" + totalScore + Environment.NewLine +
                "歷史最高分：" + highScore + Environment.NewLine + Environment.NewLine +
                "確定要重玩嗎？",
                "重玩",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                RestartGame();
            }
        }

        private void btnTotalScore_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "目前總分：" + totalScore + Environment.NewLine + "歷史最高分：" + highScore,
                "總得分",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void frmPoker_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateHighScoreIfNeeded();
            if (soundManager != null) soundManager.Dispose();
            MessageBox.Show(
                "本次得分：" + totalScore + Environment.NewLine + "歷史最高分：" + highScore,
                "結束遊戲",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void frmPoker_Shown(object sender, EventArgs e)
        {
            PerformLayoutFixes();
        }

        private void cboScoreCategory_SelectedIndexChanged(object sender, EventArgs e) { UpdatePreviewScore(); }

        private class ScoreRule
        {
            public ScoreRule(string name, int category, int score)
            {
                Name = name;
                Category = category;
                Score = score;
            }

            public string Name { get; private set; }
            public int Category { get; private set; }
            public int Score { get; private set; }
        }

        private class ScoreChoice
        {
            public ScoreChoice(int index, string name)
            {
                Index = index;
                Name = name;
            }

            public int Index { get; private set; }
            public string Name { get; private set; }
            public override string ToString() { return Name; }
        }

        private class HandResult
        {
            public HandResult(string name, int category)
            {
                Name = name;
                Category = category;
            }

            public string Name { get; private set; }
            public int Category { get; private set; }
        }

        private enum SoundEvent
        {
            Deal,
            SelectCard,
            Change,
            SettleWin,
            SettleMiss,
            OpenRules,
            Toggle
        }

        private sealed class SoundManager : IDisposable
        {
            private readonly object gate = new object();
            private float volume = 0.7f;

            public bool Enabled { get; set; } = true;

            public float Volume
            {
                get { return volume; }
                set
                {
                    if (value < 0f) volume = 0f;
                    else if (value > 1f) volume = 1f;
                    else volume = value;
                }
            }

            public void Play(SoundEvent sound)
            {
                if (!Enabled || Volume <= 0f) return;
                byte[] wav = BuildSound(sound, Volume);
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    lock (gate)
                    {
                        using (MemoryStream ms = new MemoryStream(wav))
                        using (SoundPlayer player = new SoundPlayer(ms))
                        {
                            player.PlaySync();
                        }
                    }
                });
            }

            public void Dispose()
            {
                // no-op
            }

            private static byte[] BuildSound(SoundEvent sound, float volume)
            {
                switch (sound)
                {
                    case SoundEvent.Deal:
                        return BuildTone(new[] { 650, 780, 920 }, 38, volume, 0.45);
                    case SoundEvent.SelectCard:
                        return BuildTone(new[] { 720 }, 40, volume, 0.16);
                    case SoundEvent.Change:
                        return BuildTone(new[] { 440, 620 }, 34, volume, 0.30);
                    case SoundEvent.SettleWin:
                        return BuildTone(new[] { 520, 660, 860, 1040 }, 44, volume, 0.42);
                    case SoundEvent.SettleMiss:
                        return BuildTone(new[] { 420, 340 }, 58, volume, 0.28);
                    case SoundEvent.OpenRules:
                        return BuildTone(new[] { 500, 700 }, 30, volume, 0.24);
                    case SoundEvent.Toggle:
                        return BuildTone(new[] { 760 }, 22, volume, 0.14);
                    default:
                        return BuildTone(new[] { 600 }, 20, volume, 0.2);
                }
            }

            private static byte[] BuildTone(int[] frequencies, int msPerTone, float volume, double shape)
            {
                const int sampleRate = 22050;
                int totalSamples = sampleRate * msPerTone * frequencies.Length / 1000;
                short[] pcm = new short[totalSamples];

                int offset = 0;
                for (int i = 0; i < frequencies.Length; i++)
                {
                    int freq = frequencies[i];
                    int samples = sampleRate * msPerTone / 1000;
                    for (int s = 0; s < samples; s++)
                    {
                        double t = (double)s / samples;
                        double envelope = Math.Sin(Math.PI * t);
                        envelope = Math.Pow(envelope, shape);
                        double wave = Math.Sin((2.0 * Math.PI * freq * s) / sampleRate);
                        pcm[offset + s] = (short)(wave * envelope * volume * short.MaxValue * 0.5);
                    }
                    offset += samples;
                }

                return BuildWavFromPcm16(pcm, sampleRate);
            }

            private static byte[] BuildWavFromPcm16(short[] samples, int sampleRate)
            {
                int dataSize = samples.Length * 2;
                using (MemoryStream ms = new MemoryStream(44 + dataSize))
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(new[] { 'R', 'I', 'F', 'F' });
                    bw.Write(36 + dataSize);
                    bw.Write(new[] { 'W', 'A', 'V', 'E' });
                    bw.Write(new[] { 'f', 'm', 't', ' ' });
                    bw.Write(16);
                    bw.Write((short)1);
                    bw.Write((short)1);
                    bw.Write(sampleRate);
                    bw.Write(sampleRate * 2);
                    bw.Write((short)2);
                    bw.Write((short)16);
                    bw.Write(new[] { 'd', 'a', 't', 'a' });
                    bw.Write(dataSize);
                    for (int i = 0; i < samples.Length; i++) bw.Write(samples[i]);
                    bw.Flush();
                    return ms.ToArray();
                }
            }
        }
    }
}
