using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement5Good.Winform
{
    /// <summary>
    /// UserDashboard - Partial Class cho UI của Report Module
    /// Redesign giao diện báo cáo thống kê đẹp và hiện đại
    /// </summary>
    public partial class UserDashboard
    {
        // Statistics Cards
        private Panel? pnlStatsCards;
        private Panel? cardTotalStudents;
        private Panel? cardAchievedStudents;
        private Panel? cardTotalEvidence;
        private Panel? cardApprovedEvidence;
        
        // Labels for cards
        private Label? lblCardTotal, lblCardTotalValue;
        private Label? lblCardAchieved, lblCardAchievedValue;
        private Label? lblCardEvidence, lblCardEvidenceValue;
        private Label? lblCardApproved, lblCardApprovedValue;

        /// <summary>
        /// Khởi tạo giao diện Report Module mới
        /// </summary>
        private void InitializeReportModuleUI()
        {
            // Clear existing controls (giữ lại panelReportOptions để không mất event handlers)
            var reportOptionsToKeep = panelReportOptions;
            var dataGridToKeep = dataGridViewReports;
            
            panelReportsModule.Controls.Clear();
            panelReportsModule.BackColor = Color.FromArgb(245, 247, 250);
            
            // 1. Header Title
            var lblHeader = new Label
            {
                Text = "📊 BÁO CÁO VÀ THỐNG KÊ",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = false,
                Size = new Size(900, 40),
                Location = new Point(30, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelReportsModule.Controls.Add(lblHeader);
            
            // 2. Filter Panel (redesign)
            RedesignFilterPanel(reportOptionsToKeep);
            reportOptionsToKeep.Location = new Point(30, 70);
            reportOptionsToKeep.Size = new Size(940, 100);
            panelReportsModule.Controls.Add(reportOptionsToKeep);
            
            // 3. Statistics Cards Panel
            CreateStatisticsCards();
            if (pnlStatsCards != null)
            {
                pnlStatsCards.Location = new Point(30, 185);
                pnlStatsCards.Size = new Size(940, 120);
                panelReportsModule.Controls.Add(pnlStatsCards);
            }
            
            // 4. Data Grid (với border đẹp hơn)
            RedesignDataGrid(dataGridToKeep);
            dataGridToKeep.Location = new Point(30, 320);
            dataGridToKeep.Size = new Size(940, 200);
            panelReportsModule.Controls.Add(dataGridToKeep);
        }
        
        /// <summary>
        /// Redesign Filter Panel với layout đẹp hơn
        /// </summary>
        private void RedesignFilterPanel(Panel panel)
        {
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.None;
            
            // Add shadow effect (using a border panel)
            var shadowPanel = new Panel
            {
                BackColor = Color.FromArgb(230, 234, 238),
                Location = new Point(panel.Location.X + 3, panel.Location.Y + 3),
                Size = panel.Size
            };
            
            // Reposition controls with better spacing
            int leftMargin = 20;
            int topLabelY = 15;
            int topControlY = 35;
            int controlHeight = 28;
            int spacing = 150;
            
            // Report Type
            if (panel.Controls.Contains(lblReportType))
            {
                lblReportType.Location = new Point(leftMargin, topLabelY);
                lblReportType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                lblReportType.ForeColor = Color.FromArgb(52, 73, 94);
            }
            if (panel.Controls.Contains(cmbReportType))
            {
                cmbReportType.Location = new Point(leftMargin, topControlY);
                cmbReportType.Size = new Size(160, controlHeight);
                cmbReportType.Font = new Font("Segoe UI", 9F);
            }
            
            // Report Level
            if (panel.Controls.Contains(lblReportLevel))
            {
                lblReportLevel.Location = new Point(leftMargin + spacing + 20, topLabelY);
                lblReportLevel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                lblReportLevel.ForeColor = Color.FromArgb(52, 73, 94);
            }
            if (panel.Controls.Contains(cmbReportLevel))
            {
                cmbReportLevel.Location = new Point(leftMargin + spacing + 20, topControlY);
                cmbReportLevel.Size = new Size(140, controlHeight);
                cmbReportLevel.Font = new Font("Segoe UI", 9F);
            }
            
            // Buttons - right aligned
            int buttonWidth = 120;
            int buttonSpacing = 10;
            int rightMargin = panel.Width - 20;
            
            if (panel.Controls.Contains(btnExportPDF))
            {
                btnExportPDF.Location = new Point(rightMargin - buttonWidth, topControlY);
                btnExportPDF.Size = new Size(buttonWidth, controlHeight);
                btnExportPDF.BackColor = Color.FromArgb(231, 76, 60);
                btnExportPDF.FlatStyle = FlatStyle.Flat;
                btnExportPDF.FlatAppearance.BorderSize = 0;
                btnExportPDF.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btnExportPDF.Cursor = Cursors.Hand;
            }
            
            if (panel.Controls.Contains(btnExportExcel))
            {
                btnExportExcel.Location = new Point(rightMargin - (buttonWidth * 2) - buttonSpacing, topControlY);
                btnExportExcel.Size = new Size(buttonWidth, controlHeight);
                btnExportExcel.BackColor = Color.FromArgb(39, 174, 96);
                btnExportExcel.FlatStyle = FlatStyle.Flat;
                btnExportExcel.FlatAppearance.BorderSize = 0;
                btnExportExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btnExportExcel.Cursor = Cursors.Hand;
            }
            
            if (panel.Controls.Contains(btnGenerateReport))
            {
                btnGenerateReport.Location = new Point(rightMargin - (buttonWidth * 3) - (buttonSpacing * 2), topControlY);
                btnGenerateReport.Size = new Size(buttonWidth, controlHeight);
                btnGenerateReport.BackColor = Color.FromArgb(52, 152, 219);
                btnGenerateReport.FlatStyle = FlatStyle.Flat;
                btnGenerateReport.FlatAppearance.BorderSize = 0;
                btnGenerateReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btnGenerateReport.Cursor = Cursors.Hand;
                btnGenerateReport.Text = "📊 Tạo báo cáo";
            }
            
            // Hide date pickers if not needed (you can show them based on report type)
            if (panel.Controls.Contains(dateTimePickerFrom))
                dateTimePickerFrom.Visible = false;
            if (panel.Controls.Contains(dateTimePickerTo))
                dateTimePickerTo.Visible = false;
            if (panel.Controls.Contains(lblDateFrom))
                lblDateFrom.Visible = false;
            if (panel.Controls.Contains(lblDateTo))
                lblDateTo.Visible = false;
        }
        
        /// <summary>
        /// Tạo Statistics Cards để hiển thị tổng quan
        /// </summary>
        private void CreateStatisticsCards()
        {
            pnlStatsCards = new Panel
            {
                BackColor = Color.Transparent
            };
            
            int cardWidth = 220;
            int cardHeight = 100;
            int spacing = 15;
            
            // Card 1: Total Students
            cardTotalStudents = CreateStatCard(
                "👥 Tổng sinh viên",
                "0",
                Color.FromArgb(52, 152, 219),
                new Point(0, 0),
                cardWidth,
                cardHeight
            );
            
            // Card 2: Achieved Students
            cardAchievedStudents = CreateStatCard(
                "🏆 Đạt danh hiệu",
                "0",
                Color.FromArgb(46, 204, 113),
                new Point(cardWidth + spacing, 0),
                cardWidth,
                cardHeight
            );
            
            // Card 3: Total Evidence
            cardTotalEvidence = CreateStatCard(
                "📄 Tổng minh chứng",
                "0",
                Color.FromArgb(155, 89, 182),
                new Point((cardWidth + spacing) * 2, 0),
                cardWidth,
                cardHeight
            );
            
            // Card 4: Approved Evidence
            cardApprovedEvidence = CreateStatCard(
                "✅ Đã duyệt",
                "0",
                Color.FromArgb(241, 196, 15),
                new Point((cardWidth + spacing) * 3, 0),
                cardWidth,
                cardHeight
            );
            
            pnlStatsCards.Controls.AddRange(new Control[] {
                cardTotalStudents,
                cardAchievedStudents,
                cardTotalEvidence,
                cardApprovedEvidence
            });
        }
        
        /// <summary>
        /// Tạo một Statistics Card
        /// </summary>
        private Panel CreateStatCard(string title, string value, Color accentColor, Point location, int width, int height)
        {
            var card = new Panel
            {
                BackColor = Color.White,
                Location = location,
                Size = new Size(width, height),
                BorderStyle = BorderStyle.None,
                Cursor = Cursors.Hand
            };
            
            // Accent bar on left
            var accentBar = new Panel
            {
                BackColor = accentColor,
                Location = new Point(0, 0),
                Size = new Size(5, height),
                Dock = DockStyle.Left
            };
            card.Controls.Add(accentBar);
            
            // Title label
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(15, 20),
                AutoSize = true
            };
            card.Controls.Add(lblTitle);
            
            // Value label
            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = accentColor,
                Location = new Point(15, 45),
                AutoSize = true
            };
            card.Controls.Add(lblValue);
            
            // Store reference to value label for updating
            if (title.Contains("Tổng sinh viên"))
            {
                lblCardTotal = lblTitle;
                lblCardTotalValue = lblValue;
            }
            else if (title.Contains("Đạt danh hiệu"))
            {
                lblCardAchieved = lblTitle;
                lblCardAchievedValue = lblValue;
            }
            else if (title.Contains("Tổng minh chứng"))
            {
                lblCardEvidence = lblTitle;
                lblCardEvidenceValue = lblValue;
            }
            else if (title.Contains("Đã duyệt"))
            {
                lblCardApproved = lblTitle;
                lblCardApprovedValue = lblValue;
            }
            
            // Hover effect
            card.MouseEnter += (s, e) => {
                card.BackColor = Color.FromArgb(248, 249, 250);
            };
            card.MouseLeave += (s, e) => {
                card.BackColor = Color.White;
            };
            
            return card;
        }
        
        /// <summary>
        /// Redesign DataGrid với style đẹp hơn
        /// </summary>
        private void RedesignDataGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Color.FromArgb(230, 234, 238);
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AllowUserToResizeRows = false;
            grid.RowTemplate.Height = 35;
            
            // Header style
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            grid.ColumnHeadersHeight = 40;
            
            // Cell style
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            grid.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);
            
            // Alternating row colors
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
        }
        
        /// <summary>
        /// Update Statistics Cards với data mới
        /// </summary>
        public void UpdateStatisticsCards(int totalStudents, int achievedStudents, int totalEvidence, int approvedEvidence)
        {
            if (lblCardTotalValue != null)
                lblCardTotalValue.Text = totalStudents.ToString("N0");
            
            if (lblCardAchievedValue != null)
            {
                lblCardAchievedValue.Text = achievedStudents.ToString("N0");
                // Update percentage
                if (lblCardAchieved != null)
                {
                    var percentage = totalStudents > 0 ? (achievedStudents * 100.0 / totalStudents) : 0;
                    lblCardAchieved.Text = $"🏆 Đạt danh hiệu ({percentage:F1}%)";
                }
            }
            
            if (lblCardEvidenceValue != null)
                lblCardEvidenceValue.Text = totalEvidence.ToString("N0");
            
            if (lblCardApprovedValue != null)
            {
                lblCardApprovedValue.Text = approvedEvidence.ToString("N0");
                // Update percentage
                if (lblCardApproved != null)
                {
                    var percentage = totalEvidence > 0 ? (approvedEvidence * 100.0 / totalEvidence) : 0;
                    lblCardApproved.Text = $"✅ Đã duyệt ({percentage:F1}%)";
                }
            }
        }
    }
}
