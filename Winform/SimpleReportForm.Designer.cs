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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblNamHoc = new System.Windows.Forms.Label();
            this.cmbNamHoc = new System.Windows.Forms.ComboBox();
            this.groupBoxReports = new System.Windows.Forms.GroupBox();
            this.btnExportStudentReport = new System.Windows.Forms.Button();
            this.btnExportEvidenceReport = new System.Windows.Forms.Button();
            this.btnExportStatisticsReport = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBoxReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Xuất báo cáo đơn giản";
            // 
            // lblNamHoc
            // 
            this.lblNamHoc.AutoSize = true;
            this.lblNamHoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamHoc.Location = new System.Drawing.Point(20, 60);
            this.lblNamHoc.Name = "lblNamHoc";
            this.lblNamHoc.Size = new System.Drawing.Size(70, 17);
            this.lblNamHoc.TabIndex = 1;
            this.lblNamHoc.Text = "Năm học:";
            // 
            // cmbNamHoc
            // 
            this.cmbNamHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNamHoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNamHoc.FormattingEnabled = true;
            this.cmbNamHoc.Location = new System.Drawing.Point(100, 57);
            this.cmbNamHoc.Name = "cmbNamHoc";
            this.cmbNamHoc.Size = new System.Drawing.Size(350, 24);
            this.cmbNamHoc.TabIndex = 2;
            // 
            // groupBoxReports
            // 
            this.groupBoxReports.Controls.Add(this.btnExportStatisticsReport);
            this.groupBoxReports.Controls.Add(this.btnExportEvidenceReport);
            this.groupBoxReports.Controls.Add(this.btnExportStudentReport);
            this.groupBoxReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxReports.Location = new System.Drawing.Point(20, 100);
            this.groupBoxReports.Name = "groupBoxReports";
            this.groupBoxReports.Size = new System.Drawing.Size(450, 180);
            this.groupBoxReports.TabIndex = 3;
            this.groupBoxReports.TabStop = false;
            this.groupBoxReports.Text = "Chọn loại báo cáo";
            // 
            // btnExportStudentReport
            // 
            this.btnExportStudentReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnExportStudentReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportStudentReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportStudentReport.ForeColor = System.Drawing.Color.White;
            this.btnExportStudentReport.Location = new System.Drawing.Point(20, 30);
            this.btnExportStudentReport.Name = "btnExportStudentReport";
            this.btnExportStudentReport.Size = new System.Drawing.Size(400, 35);
            this.btnExportStudentReport.TabIndex = 0;
            this.btnExportStudentReport.Text = "📊 Báo cáo danh sách sinh viên 5 tốt";
            this.btnExportStudentReport.UseVisualStyleBackColor = false;
            this.btnExportStudentReport.Click += new System.EventHandler(this.btnExportStudentReport_Click);
            // 
            // btnExportEvidenceReport
            // 
            this.btnExportEvidenceReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnExportEvidenceReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportEvidenceReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportEvidenceReport.ForeColor = System.Drawing.Color.White;
            this.btnExportEvidenceReport.Location = new System.Drawing.Point(20, 80);
            this.btnExportEvidenceReport.Name = "btnExportEvidenceReport";
            this.btnExportEvidenceReport.Size = new System.Drawing.Size(400, 35);
            this.btnExportEvidenceReport.TabIndex = 1;
            this.btnExportEvidenceReport.Text = "📋 Báo cáo minh chứng";
            this.btnExportEvidenceReport.UseVisualStyleBackColor = false;
            this.btnExportEvidenceReport.Click += new System.EventHandler(this.btnExportEvidenceReport_Click);
            // 
            // btnExportStatisticsReport
            // 
            this.btnExportStatisticsReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnExportStatisticsReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportStatisticsReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportStatisticsReport.ForeColor = System.Drawing.Color.White;
            this.btnExportStatisticsReport.Location = new System.Drawing.Point(20, 130);
            this.btnExportStatisticsReport.Name = "btnExportStatisticsReport";
            this.btnExportStatisticsReport.Size = new System.Drawing.Size(400, 35);
            this.btnExportStatisticsReport.TabIndex = 2;
            this.btnExportStatisticsReport.Text = "📈 Báo cáo thống kê tổng quan";
            this.btnExportStatisticsReport.UseVisualStyleBackColor = false;
            this.btnExportStatisticsReport.Click += new System.EventHandler(this.btnExportStatisticsReport_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(20, 300);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 15);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Sẵn sàng";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(350, 320);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SimpleReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBoxReports);
            this.Controls.Add(this.cmbNamHoc);
            this.Controls.Add(this.lblNamHoc);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SimpleReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xuất báo cáo đơn giản";
            this.groupBoxReports.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblNamHoc;
        private System.Windows.Forms.ComboBox cmbNamHoc;
        private System.Windows.Forms.GroupBox groupBoxReports;
        private System.Windows.Forms.Button btnExportStudentReport;
        private System.Windows.Forms.Button btnExportEvidenceReport;
        private System.Windows.Forms.Button btnExportStatisticsReport;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnClose;
    }
}
