namespace StudentManagement5Good.Winform
{
    partial class SimpleReportForm
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
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlContent = new Panel();
            groupBoxReports = new GroupBox();
            btnExportStatisticsReport = new Button();
            btnExportEvidenceReport = new Button();
            btnExportStudentReport = new Button();
            cmbNamHoc = new ComboBox();
            lblNamHoc = new Label();
            pnlFooter = new Panel();
            lblStatus = new Label();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            groupBoxReports.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(41, 128, 185);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(4, 3, 4, 3);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(541, 81);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(23, 23);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(283, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📋 Xuất báo cáo đơn giản";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(groupBoxReports);
            pnlContent.Controls.Add(cmbNamHoc);
            pnlContent.Controls.Add(lblNamHoc);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 81);
            pnlContent.Margin = new Padding(4, 3, 4, 3);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(29, 29, 29, 12);
            pnlContent.Size = new Size(541, 337);
            pnlContent.TabIndex = 1;
            // 
            // groupBoxReports
            // 
            groupBoxReports.Controls.Add(btnExportStatisticsReport);
            groupBoxReports.Controls.Add(btnExportEvidenceReport);
            groupBoxReports.Controls.Add(btnExportStudentReport);
            groupBoxReports.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxReports.ForeColor = Color.FromArgb(52, 73, 94);
            groupBoxReports.Location = new Point(33, 56);
            groupBoxReports.Margin = new Padding(4, 3, 4, 3);
            groupBoxReports.Name = "groupBoxReports";
            groupBoxReports.Padding = new Padding(18, 12, 18, 17);
            groupBoxReports.Size = new Size(465, 275);
            groupBoxReports.TabIndex = 2;
            groupBoxReports.TabStop = false;
            groupBoxReports.Text = "Chọn loại báo cáo";
            // 
            // btnExportStatisticsReport
            // 
            btnExportStatisticsReport.BackColor = Color.FromArgb(155, 89, 182);
            btnExportStatisticsReport.Cursor = Cursors.Hand;
            btnExportStatisticsReport.FlatAppearance.BorderSize = 0;
            btnExportStatisticsReport.FlatAppearance.MouseDownBackColor = Color.FromArgb(142, 68, 173);
            btnExportStatisticsReport.FlatAppearance.MouseOverBackColor = Color.FromArgb(142, 68, 173);
            btnExportStatisticsReport.FlatStyle = FlatStyle.Flat;
            btnExportStatisticsReport.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExportStatisticsReport.ForeColor = Color.White;
            btnExportStatisticsReport.Location = new Point(29, 190);
            btnExportStatisticsReport.Margin = new Padding(4, 3, 4, 3);
            btnExportStatisticsReport.Name = "btnExportStatisticsReport";
            btnExportStatisticsReport.Size = new Size(403, 58);
            btnExportStatisticsReport.TabIndex = 2;
            btnExportStatisticsReport.Text = "📈 Báo cáo thống kê tổng quan";
            btnExportStatisticsReport.UseVisualStyleBackColor = false;
            btnExportStatisticsReport.Click += btnExportStatisticsReport_Click;
            // 
            // btnExportEvidenceReport
            // 
            btnExportEvidenceReport.BackColor = Color.FromArgb(46, 204, 113);
            btnExportEvidenceReport.Cursor = Cursors.Hand;
            btnExportEvidenceReport.FlatAppearance.BorderSize = 0;
            btnExportEvidenceReport.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 174, 96);
            btnExportEvidenceReport.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96);
            btnExportEvidenceReport.FlatStyle = FlatStyle.Flat;
            btnExportEvidenceReport.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExportEvidenceReport.ForeColor = Color.White;
            btnExportEvidenceReport.Location = new Point(29, 115);
            btnExportEvidenceReport.Margin = new Padding(4, 3, 4, 3);
            btnExportEvidenceReport.Name = "btnExportEvidenceReport";
            btnExportEvidenceReport.Size = new Size(403, 58);
            btnExportEvidenceReport.TabIndex = 1;
            btnExportEvidenceReport.Text = "📋 Báo cáo minh chứng";
            btnExportEvidenceReport.UseVisualStyleBackColor = false;
            btnExportEvidenceReport.Click += btnExportEvidenceReport_Click;
            // 
            // btnExportStudentReport
            // 
            btnExportStudentReport.BackColor = Color.FromArgb(52, 152, 219);
            btnExportStudentReport.Cursor = Cursors.Hand;
            btnExportStudentReport.FlatAppearance.BorderSize = 0;
            btnExportStudentReport.FlatAppearance.MouseDownBackColor = Color.FromArgb(41, 128, 185);
            btnExportStudentReport.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btnExportStudentReport.FlatStyle = FlatStyle.Flat;
            btnExportStudentReport.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExportStudentReport.ForeColor = Color.White;
            btnExportStudentReport.Location = new Point(29, 40);
            btnExportStudentReport.Margin = new Padding(4, 3, 4, 3);
            btnExportStudentReport.Name = "btnExportStudentReport";
            btnExportStudentReport.Size = new Size(403, 58);
            btnExportStudentReport.TabIndex = 0;
            btnExportStudentReport.Text = "📊 Báo cáo danh sách sinh viên 5 tốt";
            btnExportStudentReport.UseVisualStyleBackColor = false;
            btnExportStudentReport.Click += btnExportStudentReport_Click;
            // 
            // cmbNamHoc
            // 
            cmbNamHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNamHoc.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbNamHoc.FormattingEnabled = true;
            cmbNamHoc.Location = new Point(192, 25);
            cmbNamHoc.Margin = new Padding(4, 3, 4, 3);
            cmbNamHoc.Name = "cmbNamHoc";
            cmbNamHoc.Size = new Size(274, 25);
            cmbNamHoc.TabIndex = 1;
            // 
            // lblNamHoc
            // 
            lblNamHoc.AutoSize = true;
            lblNamHoc.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNamHoc.ForeColor = Color.FromArgb(52, 73, 94);
            lblNamHoc.Location = new Point(29, 29);
            lblNamHoc.Margin = new Padding(4, 0, 4, 0);
            lblNamHoc.Name = "lblNamHoc";
            lblNamHoc.Size = new Size(73, 19);
            lblNamHoc.TabIndex = 0;
            lblNamHoc.Text = "Năm học:";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(236, 240, 241);
            pnlFooter.Controls.Add(lblStatus);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 418);
            pnlFooter.Margin = new Padding(4, 3, 4, 3);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(541, 69);
            pnlFooter.TabIndex = 2;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.FromArgb(127, 140, 141);
            lblStatus.Location = new Point(29, 25);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(71, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "✅ Sẵn sàng";
            // 
            // SimpleReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(541, 487);
            Controls.Add(pnlContent);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SimpleReportForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Xuất báo cáo đơn giản";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            groupBoxReports.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblNamHoc;
        private System.Windows.Forms.ComboBox cmbNamHoc;
        private System.Windows.Forms.GroupBox groupBoxReports;
        private System.Windows.Forms.Button btnExportStudentReport;
        private System.Windows.Forms.Button btnExportEvidenceReport;
        private System.Windows.Forms.Button btnExportStatisticsReport;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblStatus;
    }
}
