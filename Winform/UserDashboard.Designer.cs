namespace StudentManagement5Good.Winform
{
    partial class UserDashboard
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
            // Header Panel
            panelHeader = new Panel();
            lblSystemTitle = new Label();
            lblCurrentDateTime = new Label();
            lblUserInfo = new Label();
            btnRefresh = new Button();
            btnLogout = new Button();
            
            // Navigation Panel (Left Sidebar)
            panelNavigation = new Panel();
            btnDashboard = new Button();
            btnApprovalCenter = new Button();
            btnUserManagement = new Button();
            btnReportsStats = new Button();
            btnSystemConfig = new Button();
            panelUserProfile = new Panel();
            lblUserRole = new Label();
            lblUserName = new Label();
            
            // Main Content Panel
            panelMainContent = new Panel();
            
            // Dashboard Module
            panelDashboardModule = new Panel();
            lblDashboardTitle = new Label();
            panelQuickStats = new Panel();
            cardPendingApprovals = new Panel();
            lblPendingCount = new Label();
            lblPendingTitle = new Label();
            cardProcessedFiles = new Panel();
            lblProcessedCount = new Label();
            lblProcessedTitle = new Label();
            cardDeadlines = new Panel();
            lblDeadlineInfo = new Label();
            lblDeadlineTitle = new Label();
            cardSystemStatus = new Panel();
            lblSystemStatusInfo = new Label();
            lblSystemStatusTitle = new Label();
            
            // Approval Center Module
            panelApprovalModule = new Panel();
            lblApprovalTitle = new Label();
            panelFilters = new Panel();
            cmbStatusFilter = new ComboBox();
            cmbDepartmentFilter = new ComboBox();
            cmbCriteriaFilter = new ComboBox();
            txtSearchStudent = new TextBox();
            btnApplyFilters = new Button();
            btnClearFilters = new Button();
            lblStatusFilter = new Label();
            lblDepartmentFilter = new Label();
            lblCriteriaFilter = new Label();
            lblSearchStudent = new Label();
            
            dataGridViewApprovals = new DataGridView();
            colStudentId = new DataGridViewTextBoxColumn();
            colStudentName = new DataGridViewTextBoxColumn();
            colCriteria = new DataGridViewTextBoxColumn();
            colSubmissionDate = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colActions = new DataGridViewButtonColumn();
            
            panelApprovalDetails = new Panel();
            lblApprovalDetailsTitle = new Label();
            panelStudentInfo = new Panel();
            lblStudentDetails = new Label();
            panelEvidenceViewer = new Panel();
            lblEvidenceTitle = new Label();
            pictureBoxEvidence = new PictureBox();
            panelApprovalActions = new Panel();
            btnApprove = new Button();
            btnReject = new Button();
            txtRejectionReason = new TextBox();
            lblRejectionReason = new Label();
            
            // User Management Module
            panelUserManagementModule = new Panel();
            lblUserManagementTitle = new Label();
            panelUserActions = new Panel();
            btnAddUser = new Button();
            btnImportStudents = new Button();
            btnExportUsers = new Button();
            dataGridViewUsers = new DataGridView();
            colUserId = new DataGridViewTextBoxColumn();
            colUserFullName = new DataGridViewTextBoxColumn();
            colUserRole = new DataGridViewTextBoxColumn();
            colUserDepartment = new DataGridViewTextBoxColumn();
            colUserStatus = new DataGridViewTextBoxColumn();
            colUserActions = new DataGridViewButtonColumn();
            
            // Reports & Statistics Module
            panelReportsModule = new Panel();
            lblReportsTitle = new Label();
            panelReportOptions = new Panel();
            cmbReportType = new ComboBox();
            cmbReportLevel = new ComboBox();
            dateTimePickerFrom = new DateTimePicker();
            dateTimePickerTo = new DateTimePicker();
            btnGenerateReport = new Button();
            btnExportExcel = new Button();
            btnExportPDF = new Button();
            lblReportType = new Label();
            lblReportLevel = new Label();
            lblDateFrom = new Label();
            lblDateTo = new Label();
            
            chartStatistics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            dataGridViewReports = new DataGridView();
            
            // System Configuration Module
            panelSystemConfigModule = new Panel();
            lblSystemConfigTitle = new Label();
            tabControlConfig = new TabControl();
            tabPageAcademicYears = new TabPage();
            tabPageCriteria = new TabPage();
            tabPageDeadlines = new TabPage();
            tabPageSystemSettings = new TabPage();
            
            // Academic Years Tab
            dataGridViewAcademicYears = new DataGridView();
            btnAddAcademicYear = new Button();
            btnEditAcademicYear = new Button();
            btnDeleteAcademicYear = new Button();
            
            // Criteria Tab
            dataGridViewCriteria = new DataGridView();
            btnAddCriteria = new Button();
            btnEditCriteria = new Button();
            btnDeleteCriteria = new Button();
            
            SuspendLayout();
            
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(41, 128, 185);
            panelHeader.Controls.Add(btnLogout);
            panelHeader.Controls.Add(btnRefresh);
            panelHeader.Controls.Add(lblUserInfo);
            panelHeader.Controls.Add(lblCurrentDateTime);
            panelHeader.Controls.Add(lblSystemTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1400, 80);
            panelHeader.TabIndex = 0;
            
            // 
            // lblSystemTitle
            // 
            lblSystemTitle.AutoSize = true;
            lblSystemTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblSystemTitle.ForeColor = Color.White;
            lblSystemTitle.Location = new Point(20, 20);
            lblSystemTitle.Name = "lblSystemTitle";
            lblSystemTitle.Size = new Size(380, 37);
            lblSystemTitle.TabIndex = 0;
            lblSystemTitle.Text = "Hệ thống Quản lý Sinh viên 5 Tốt";
            
            // 
            // lblCurrentDateTime
            // 
            lblCurrentDateTime.AutoSize = true;
            lblCurrentDateTime.Font = new Font("Segoe UI", 10F);
            lblCurrentDateTime.ForeColor = Color.White;
            lblCurrentDateTime.Location = new Point(500, 30);
            lblCurrentDateTime.Name = "lblCurrentDateTime";
            lblCurrentDateTime.Size = new Size(150, 23);
            lblCurrentDateTime.TabIndex = 1;
            lblCurrentDateTime.Text = "Thứ 2, 14/10/2024";
            
            // 
            // lblUserInfo
            // 
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 10F);
            lblUserInfo.ForeColor = Color.White;
            lblUserInfo.Location = new Point(1000, 30);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(200, 23);
            lblUserInfo.TabIndex = 2;
            lblUserInfo.Text = "Chào mừng, Admin";
            
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(52, 152, 219);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1220, 25);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 30);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(231, 76, 60);
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(1310, 25);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(80, 30);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            
            // 
            // panelNavigation
            // 
            panelNavigation.BackColor = Color.FromArgb(52, 73, 94);
            panelNavigation.Controls.Add(panelUserProfile);
            panelNavigation.Controls.Add(btnSystemConfig);
            panelNavigation.Controls.Add(btnReportsStats);
            panelNavigation.Controls.Add(btnUserManagement);
            panelNavigation.Controls.Add(btnApprovalCenter);
            panelNavigation.Controls.Add(btnDashboard);
            panelNavigation.Dock = DockStyle.Left;
            panelNavigation.Location = new Point(0, 80);
            panelNavigation.Name = "panelNavigation";
            panelNavigation.Size = new Size(250, 720);
            panelNavigation.TabIndex = 1;
            
            // 
            // panelUserProfile
            // 
            panelUserProfile.BackColor = Color.FromArgb(44, 62, 80);
            panelUserProfile.Controls.Add(lblUserRole);
            panelUserProfile.Controls.Add(lblUserName);
            panelUserProfile.Dock = DockStyle.Top;
            panelUserProfile.Location = new Point(0, 0);
            panelUserProfile.Name = "panelUserProfile";
            panelUserProfile.Size = new Size(250, 80);
            panelUserProfile.TabIndex = 0;
            
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUserName.ForeColor = Color.White;
            lblUserName.Location = new Point(20, 15);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(150, 28);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "Nguyễn Văn A";
            
            // 
            // lblUserRole
            // 
            lblUserRole.AutoSize = true;
            lblUserRole.Font = new Font("Segoe UI", 9F);
            lblUserRole.ForeColor = Color.FromArgb(149, 165, 166);
            lblUserRole.Location = new Point(20, 45);
            lblUserRole.Name = "lblUserRole";
            lblUserRole.Size = new Size(100, 20);
            lblUserRole.TabIndex = 1;
            lblUserRole.Text = "Quản trị viên";
            
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(41, 128, 185);
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Location = new Point(0, 90);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(250, 50);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "📊 Bảng Điều Khiển";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += btnDashboard_Click;
            
            // 
            // btnApprovalCenter
            // 
            btnApprovalCenter.BackColor = Color.FromArgb(52, 73, 94);
            btnApprovalCenter.FlatAppearance.BorderSize = 0;
            btnApprovalCenter.FlatStyle = FlatStyle.Flat;
            btnApprovalCenter.Font = new Font("Segoe UI", 11F);
            btnApprovalCenter.ForeColor = Color.White;
            btnApprovalCenter.Location = new Point(0, 140);
            btnApprovalCenter.Name = "btnApprovalCenter";
            btnApprovalCenter.Size = new Size(250, 50);
            btnApprovalCenter.TabIndex = 2;
            btnApprovalCenter.Text = "✅ Trung tâm Xét duyệt";
            btnApprovalCenter.TextAlign = ContentAlignment.MiddleLeft;
            btnApprovalCenter.UseVisualStyleBackColor = false;
            btnApprovalCenter.Click += btnApprovalCenter_Click;
            
            // 
            // btnUserManagement
            // 
            btnUserManagement.BackColor = Color.FromArgb(52, 73, 94);
            btnUserManagement.FlatAppearance.BorderSize = 0;
            btnUserManagement.FlatStyle = FlatStyle.Flat;
            btnUserManagement.Font = new Font("Segoe UI", 11F);
            btnUserManagement.ForeColor = Color.White;
            btnUserManagement.Location = new Point(0, 190);
            btnUserManagement.Name = "btnUserManagement";
            btnUserManagement.Size = new Size(250, 50);
            btnUserManagement.TabIndex = 3;
            btnUserManagement.Text = "👥 Quản lý Người dùng";
            btnUserManagement.TextAlign = ContentAlignment.MiddleLeft;
            btnUserManagement.UseVisualStyleBackColor = false;
            btnUserManagement.Click += btnUserManagement_Click;
            
            // 
            // btnReportsStats
            // 
            btnReportsStats.BackColor = Color.FromArgb(52, 73, 94);
            btnReportsStats.FlatAppearance.BorderSize = 0;
            btnReportsStats.FlatStyle = FlatStyle.Flat;
            btnReportsStats.Font = new Font("Segoe UI", 11F);
            btnReportsStats.ForeColor = Color.White;
            btnReportsStats.Location = new Point(0, 240);
            btnReportsStats.Name = "btnReportsStats";
            btnReportsStats.Size = new Size(250, 50);
            btnReportsStats.TabIndex = 4;
            btnReportsStats.Text = "📈 Báo cáo & Thống kê";
            btnReportsStats.TextAlign = ContentAlignment.MiddleLeft;
            btnReportsStats.UseVisualStyleBackColor = false;
            btnReportsStats.Click += btnReportsStats_Click;
            
            // 
            // btnSystemConfig
            // 
            btnSystemConfig.BackColor = Color.FromArgb(52, 73, 94);
            btnSystemConfig.FlatAppearance.BorderSize = 0;
            btnSystemConfig.FlatStyle = FlatStyle.Flat;
            btnSystemConfig.Font = new Font("Segoe UI", 11F);
            btnSystemConfig.ForeColor = Color.White;
            btnSystemConfig.Location = new Point(0, 290);
            btnSystemConfig.Name = "btnSystemConfig";
            btnSystemConfig.Size = new Size(250, 50);
            btnSystemConfig.TabIndex = 5;
            btnSystemConfig.Text = "⚙️ Cấu hình Hệ thống";
            btnSystemConfig.TextAlign = ContentAlignment.MiddleLeft;
            btnSystemConfig.UseVisualStyleBackColor = false;
            btnSystemConfig.Click += btnSystemConfig_Click;
            
            // 
            // panelMainContent
            // 
            panelMainContent.BackColor = Color.FromArgb(236, 240, 241);
            panelMainContent.Controls.Add(panelSystemConfigModule);
            panelMainContent.Controls.Add(panelReportsModule);
            panelMainContent.Controls.Add(panelUserManagementModule);
            panelMainContent.Controls.Add(panelApprovalModule);
            panelMainContent.Controls.Add(panelDashboardModule);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(250, 80);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Size = new Size(1150, 720);
            panelMainContent.TabIndex = 2;
            
            // 
            // panelDashboardModule
            // 
            panelDashboardModule.Controls.Add(panelQuickStats);
            panelDashboardModule.Controls.Add(lblDashboardTitle);
            panelDashboardModule.Dock = DockStyle.Fill;
            panelDashboardModule.Location = new Point(0, 0);
            panelDashboardModule.Name = "panelDashboardModule";
            panelDashboardModule.Size = new Size(1150, 720);
            panelDashboardModule.TabIndex = 0;
            
            // 
            // lblDashboardTitle
            // 
            lblDashboardTitle.AutoSize = true;
            lblDashboardTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblDashboardTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblDashboardTitle.Location = new Point(30, 20);
            lblDashboardTitle.Name = "lblDashboardTitle";
            lblDashboardTitle.Size = new Size(280, 41);
            lblDashboardTitle.TabIndex = 0;
            lblDashboardTitle.Text = "Bảng Điều Khiển Chính";
            
            // 
            // panelQuickStats
            // 
            panelQuickStats.Controls.Add(cardSystemStatus);
            panelQuickStats.Controls.Add(cardDeadlines);
            panelQuickStats.Controls.Add(cardProcessedFiles);
            panelQuickStats.Controls.Add(cardPendingApprovals);
            panelQuickStats.Location = new Point(30, 80);
            panelQuickStats.Name = "panelQuickStats";
            panelQuickStats.Size = new Size(1090, 200);
            panelQuickStats.TabIndex = 1;
            
            // 
            // cardPendingApprovals
            // 
            cardPendingApprovals.BackColor = Color.FromArgb(231, 76, 60);
            cardPendingApprovals.Controls.Add(lblPendingTitle);
            cardPendingApprovals.Controls.Add(lblPendingCount);
            cardPendingApprovals.Location = new Point(0, 0);
            cardPendingApprovals.Name = "cardPendingApprovals";
            cardPendingApprovals.Size = new Size(260, 180);
            cardPendingApprovals.TabIndex = 0;
            
            // 
            // lblPendingCount
            // 
            lblPendingCount.AutoSize = true;
            lblPendingCount.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblPendingCount.ForeColor = Color.White;
            lblPendingCount.Location = new Point(20, 40);
            lblPendingCount.Name = "lblPendingCount";
            lblPendingCount.Size = new Size(85, 81);
            lblPendingCount.TabIndex = 0;
            lblPendingCount.Text = "25";
            
            // 
            // lblPendingTitle
            // 
            lblPendingTitle.AutoSize = true;
            lblPendingTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPendingTitle.ForeColor = Color.White;
            lblPendingTitle.Location = new Point(20, 130);
            lblPendingTitle.Name = "lblPendingTitle";
            lblPendingTitle.Size = new Size(180, 28);
            lblPendingTitle.TabIndex = 1;
            lblPendingTitle.Text = "Chờ duyệt minh chứng";
            
            // 
            // cardProcessedFiles
            // 
            cardProcessedFiles.BackColor = Color.FromArgb(46, 204, 113);
            cardProcessedFiles.Controls.Add(lblProcessedTitle);
            cardProcessedFiles.Controls.Add(lblProcessedCount);
            cardProcessedFiles.Location = new Point(280, 0);
            cardProcessedFiles.Name = "cardProcessedFiles";
            cardProcessedFiles.Size = new Size(260, 180);
            cardProcessedFiles.TabIndex = 1;
            
            // 
            // lblProcessedCount
            // 
            lblProcessedCount.AutoSize = true;
            lblProcessedCount.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblProcessedCount.ForeColor = Color.White;
            lblProcessedCount.Location = new Point(20, 40);
            lblProcessedCount.Name = "lblProcessedCount";
            lblProcessedCount.Size = new Size(125, 81);
            lblProcessedCount.TabIndex = 0;
            lblProcessedCount.Text = "142";
            
            // 
            // lblProcessedTitle
            // 
            lblProcessedTitle.AutoSize = true;
            lblProcessedTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblProcessedTitle.ForeColor = Color.White;
            lblProcessedTitle.Location = new Point(20, 130);
            lblProcessedTitle.Name = "lblProcessedTitle";
            lblProcessedTitle.Size = new Size(150, 28);
            lblProcessedTitle.TabIndex = 1;
            lblProcessedTitle.Text = "Đã xử lý hồ sơ";
            
            // 
            // cardDeadlines
            // 
            cardDeadlines.BackColor = Color.FromArgb(241, 196, 15);
            cardDeadlines.Controls.Add(lblDeadlineTitle);
            cardDeadlines.Controls.Add(lblDeadlineInfo);
            cardDeadlines.Location = new Point(560, 0);
            cardDeadlines.Name = "cardDeadlines";
            cardDeadlines.Size = new Size(260, 180);
            cardDeadlines.TabIndex = 2;
            
            // 
            // lblDeadlineInfo
            // 
            lblDeadlineInfo.AutoSize = true;
            lblDeadlineInfo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblDeadlineInfo.ForeColor = Color.White;
            lblDeadlineInfo.Location = new Point(20, 60);
            lblDeadlineInfo.Name = "lblDeadlineInfo";
            lblDeadlineInfo.Size = new Size(120, 32);
            lblDeadlineInfo.TabIndex = 0;
            lblDeadlineInfo.Text = "15/12/2024";
            
            // 
            // lblDeadlineTitle
            // 
            lblDeadlineTitle.AutoSize = true;
            lblDeadlineTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDeadlineTitle.ForeColor = Color.White;
            lblDeadlineTitle.Location = new Point(20, 130);
            lblDeadlineTitle.Name = "lblDeadlineTitle";
            lblDeadlineTitle.Size = new Size(180, 28);
            lblDeadlineTitle.TabIndex = 1;
            lblDeadlineTitle.Text = "Hạn chót xét duyệt";
            
            // 
            // cardSystemStatus
            // 
            cardSystemStatus.BackColor = Color.FromArgb(155, 89, 182);
            cardSystemStatus.Controls.Add(lblSystemStatusTitle);
            cardSystemStatus.Controls.Add(lblSystemStatusInfo);
            cardSystemStatus.Location = new Point(840, 0);
            cardSystemStatus.Name = "cardSystemStatus";
            cardSystemStatus.Size = new Size(250, 180);
            cardSystemStatus.TabIndex = 3;
            
            // 
            // lblSystemStatusInfo
            // 
            lblSystemStatusInfo.AutoSize = true;
            lblSystemStatusInfo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblSystemStatusInfo.ForeColor = Color.White;
            lblSystemStatusInfo.Location = new Point(20, 60);
            lblSystemStatusInfo.Name = "lblSystemStatusInfo";
            lblSystemStatusInfo.Size = new Size(150, 37);
            lblSystemStatusInfo.TabIndex = 0;
            lblSystemStatusInfo.Text = "Hoạt động";
            
            // 
            // lblSystemStatusTitle
            // 
            lblSystemStatusTitle.AutoSize = true;
            lblSystemStatusTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSystemStatusTitle.ForeColor = Color.White;
            lblSystemStatusTitle.Location = new Point(20, 130);
            lblSystemStatusTitle.Name = "lblSystemStatusTitle";
            lblSystemStatusTitle.Size = new Size(150, 28);
            lblSystemStatusTitle.TabIndex = 1;
            lblSystemStatusTitle.Text = "Trạng thái hệ thống";
            
            // 
            // panelApprovalModule
            // 
            panelApprovalModule.Controls.Add(panelApprovalDetails);
            panelApprovalModule.Controls.Add(dataGridViewApprovals);
            panelApprovalModule.Controls.Add(panelFilters);
            panelApprovalModule.Controls.Add(lblApprovalTitle);
            panelApprovalModule.Dock = DockStyle.Fill;
            panelApprovalModule.Location = new Point(0, 0);
            panelApprovalModule.Name = "panelApprovalModule";
            panelApprovalModule.Size = new Size(1150, 720);
            panelApprovalModule.TabIndex = 1;
            panelApprovalModule.Visible = false;
            
            // 
            // lblApprovalTitle
            // 
            lblApprovalTitle.AutoSize = true;
            lblApprovalTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblApprovalTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblApprovalTitle.Location = new Point(30, 20);
            lblApprovalTitle.Name = "lblApprovalTitle";
            lblApprovalTitle.Size = new Size(280, 41);
            lblApprovalTitle.TabIndex = 0;
            lblApprovalTitle.Text = "Trung tâm Xét duyệt";
            
            // 
            // panelFilters
            // 
            panelFilters.BackColor = Color.White;
            panelFilters.Controls.Add(btnClearFilters);
            panelFilters.Controls.Add(btnApplyFilters);
            panelFilters.Controls.Add(lblSearchStudent);
            panelFilters.Controls.Add(lblCriteriaFilter);
            panelFilters.Controls.Add(lblDepartmentFilter);
            panelFilters.Controls.Add(lblStatusFilter);
            panelFilters.Controls.Add(txtSearchStudent);
            panelFilters.Controls.Add(cmbCriteriaFilter);
            panelFilters.Controls.Add(cmbDepartmentFilter);
            panelFilters.Controls.Add(cmbStatusFilter);
            panelFilters.Location = new Point(30, 80);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(1090, 100);
            panelFilters.TabIndex = 1;
            
            // 
            // cmbStatusFilter
            // 
            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.Font = new Font("Segoe UI", 9F);
            cmbStatusFilter.Location = new Point(20, 40);
            cmbStatusFilter.Name = "cmbStatusFilter";
            cmbStatusFilter.Size = new Size(150, 28);
            cmbStatusFilter.TabIndex = 0;
            
            // 
            // lblStatusFilter
            // 
            lblStatusFilter.AutoSize = true;
            lblStatusFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatusFilter.Location = new Point(20, 15);
            lblStatusFilter.Name = "lblStatusFilter";
            lblStatusFilter.Size = new Size(85, 20);
            lblStatusFilter.TabIndex = 1;
            lblStatusFilter.Text = "Trạng thái:";
            
            // 
            // cmbDepartmentFilter
            // 
            cmbDepartmentFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartmentFilter.Font = new Font("Segoe UI", 9F);
            cmbDepartmentFilter.Location = new Point(190, 40);
            cmbDepartmentFilter.Name = "cmbDepartmentFilter";
            cmbDepartmentFilter.Size = new Size(150, 28);
            cmbDepartmentFilter.TabIndex = 2;
            
            // 
            // lblDepartmentFilter
            // 
            lblDepartmentFilter.AutoSize = true;
            lblDepartmentFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDepartmentFilter.Location = new Point(190, 15);
            lblDepartmentFilter.Name = "lblDepartmentFilter";
            lblDepartmentFilter.Size = new Size(75, 20);
            lblDepartmentFilter.TabIndex = 3;
            lblDepartmentFilter.Text = "Đơn vị:";
            
            // 
            // cmbCriteriaFilter
            // 
            cmbCriteriaFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCriteriaFilter.Font = new Font("Segoe UI", 9F);
            cmbCriteriaFilter.Location = new Point(360, 40);
            cmbCriteriaFilter.Name = "cmbCriteriaFilter";
            cmbCriteriaFilter.Size = new Size(150, 28);
            cmbCriteriaFilter.TabIndex = 4;
            
            // 
            // lblCriteriaFilter
            // 
            lblCriteriaFilter.AutoSize = true;
            lblCriteriaFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCriteriaFilter.Location = new Point(360, 15);
            lblCriteriaFilter.Name = "lblCriteriaFilter";
            lblCriteriaFilter.Size = new Size(70, 20);
            lblCriteriaFilter.TabIndex = 5;
            lblCriteriaFilter.Text = "Tiêu chí:";
            
            // 
            // txtSearchStudent
            // 
            txtSearchStudent.Font = new Font("Segoe UI", 9F);
            txtSearchStudent.Location = new Point(530, 40);
            txtSearchStudent.Name = "txtSearchStudent";
            txtSearchStudent.PlaceholderText = "Nhập mã số hoặc tên sinh viên...";
            txtSearchStudent.Size = new Size(250, 27);
            txtSearchStudent.TabIndex = 6;
            
            // 
            // lblSearchStudent
            // 
            lblSearchStudent.AutoSize = true;
            lblSearchStudent.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSearchStudent.Location = new Point(530, 15);
            lblSearchStudent.Name = "lblSearchStudent";
            lblSearchStudent.Size = new Size(80, 20);
            lblSearchStudent.TabIndex = 7;
            lblSearchStudent.Text = "Tìm kiếm:";
            
            // 
            // btnApplyFilters
            // 
            btnApplyFilters.BackColor = Color.FromArgb(41, 128, 185);
            btnApplyFilters.FlatAppearance.BorderSize = 0;
            btnApplyFilters.FlatStyle = FlatStyle.Flat;
            btnApplyFilters.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnApplyFilters.ForeColor = Color.White;
            btnApplyFilters.Location = new Point(800, 38);
            btnApplyFilters.Name = "btnApplyFilters";
            btnApplyFilters.Size = new Size(100, 32);
            btnApplyFilters.TabIndex = 8;
            btnApplyFilters.Text = "Áp dụng";
            btnApplyFilters.UseVisualStyleBackColor = false;
            btnApplyFilters.Click += btnApplyFilters_Click;
            
            // 
            // btnClearFilters
            // 
            btnClearFilters.BackColor = Color.FromArgb(149, 165, 166);
            btnClearFilters.FlatAppearance.BorderSize = 0;
            btnClearFilters.FlatStyle = FlatStyle.Flat;
            btnClearFilters.Font = new Font("Segoe UI", 9F);
            btnClearFilters.ForeColor = Color.White;
            btnClearFilters.Location = new Point(920, 38);
            btnClearFilters.Name = "btnClearFilters";
            btnClearFilters.Size = new Size(100, 32);
            btnClearFilters.TabIndex = 9;
            btnClearFilters.Text = "Xóa bộ lọc";
            btnClearFilters.UseVisualStyleBackColor = false;
            btnClearFilters.Click += btnClearFilters_Click;
            
            // 
            // dataGridViewApprovals
            // 
            dataGridViewApprovals.AllowUserToAddRows = false;
            dataGridViewApprovals.AllowUserToDeleteRows = false;
            dataGridViewApprovals.BackgroundColor = Color.White;
            dataGridViewApprovals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewApprovals.Columns.AddRange(new DataGridViewColumn[] { colStudentId, colStudentName, colCriteria, colSubmissionDate, colStatus, colActions });
            dataGridViewApprovals.Location = new Point(30, 200);
            dataGridViewApprovals.Name = "dataGridViewApprovals";
            dataGridViewApprovals.ReadOnly = true;
            dataGridViewApprovals.RowHeadersWidth = 51;
            dataGridViewApprovals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewApprovals.Size = new Size(700, 500);
            dataGridViewApprovals.TabIndex = 2;
            dataGridViewApprovals.CellClick += dataGridViewApprovals_CellClick;
            
            // 
            // colStudentId
            // 
            colStudentId.HeaderText = "Mã SV";
            colStudentId.MinimumWidth = 6;
            colStudentId.Name = "colStudentId";
            colStudentId.ReadOnly = true;
            colStudentId.Width = 100;
            
            // 
            // colStudentName
            // 
            colStudentName.HeaderText = "Họ tên";
            colStudentName.MinimumWidth = 6;
            colStudentName.Name = "colStudentName";
            colStudentName.ReadOnly = true;
            colStudentName.Width = 150;
            
            // 
            // colCriteria
            // 
            colCriteria.HeaderText = "Tiêu chí";
            colCriteria.MinimumWidth = 6;
            colCriteria.Name = "colCriteria";
            colCriteria.ReadOnly = true;
            colCriteria.Width = 120;
            
            // 
            // colSubmissionDate
            // 
            colSubmissionDate.HeaderText = "Ngày nộp";
            colSubmissionDate.MinimumWidth = 6;
            colSubmissionDate.Name = "colSubmissionDate";
            colSubmissionDate.ReadOnly = true;
            colSubmissionDate.Width = 100;
            
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Trạng thái";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Width = 100;
            
            // 
            // colActions
            // 
            colActions.HeaderText = "Thao tác";
            colActions.MinimumWidth = 6;
            colActions.Name = "colActions";
            colActions.ReadOnly = true;
            colActions.Text = "Xem chi tiết";
            colActions.UseColumnTextForButtonValue = true;
            colActions.Width = 100;
            
            // 
            // panelApprovalDetails
            // 
            panelApprovalDetails.BackColor = Color.White;
            panelApprovalDetails.Controls.Add(panelApprovalActions);
            panelApprovalDetails.Controls.Add(panelEvidenceViewer);
            panelApprovalDetails.Controls.Add(panelStudentInfo);
            panelApprovalDetails.Controls.Add(lblApprovalDetailsTitle);
            panelApprovalDetails.Location = new Point(750, 200);
            panelApprovalDetails.Name = "panelApprovalDetails";
            panelApprovalDetails.Size = new Size(370, 500);
            panelApprovalDetails.TabIndex = 3;
            
            // 
            // lblApprovalDetailsTitle
            // 
            lblApprovalDetailsTitle.AutoSize = true;
            lblApprovalDetailsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblApprovalDetailsTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblApprovalDetailsTitle.Location = new Point(15, 15);
            lblApprovalDetailsTitle.Name = "lblApprovalDetailsTitle";
            lblApprovalDetailsTitle.Size = new Size(180, 32);
            lblApprovalDetailsTitle.TabIndex = 0;
            lblApprovalDetailsTitle.Text = "Chi tiết xét duyệt";
            
            // 
            // panelStudentInfo
            // 
            panelStudentInfo.Controls.Add(lblStudentDetails);
            panelStudentInfo.Location = new Point(15, 60);
            panelStudentInfo.Name = "panelStudentInfo";
            panelStudentInfo.Size = new Size(340, 80);
            panelStudentInfo.TabIndex = 1;
            
            // 
            // lblStudentDetails
            // 
            lblStudentDetails.AutoSize = true;
            lblStudentDetails.Font = new Font("Segoe UI", 10F);
            lblStudentDetails.Location = new Point(0, 0);
            lblStudentDetails.Name = "lblStudentDetails";
            lblStudentDetails.Size = new Size(200, 23);
            lblStudentDetails.TabIndex = 0;
            lblStudentDetails.Text = "Chọn một hồ sơ để xem chi tiết";
            
            // 
            // panelEvidenceViewer
            // 
            panelEvidenceViewer.Controls.Add(pictureBoxEvidence);
            panelEvidenceViewer.Controls.Add(lblEvidenceTitle);
            panelEvidenceViewer.Location = new Point(15, 150);
            panelEvidenceViewer.Name = "panelEvidenceViewer";
            panelEvidenceViewer.Size = new Size(340, 200);
            panelEvidenceViewer.TabIndex = 2;
            
            // 
            // lblEvidenceTitle
            // 
            lblEvidenceTitle.AutoSize = true;
            lblEvidenceTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEvidenceTitle.Location = new Point(0, 0);
            lblEvidenceTitle.Name = "lblEvidenceTitle";
            lblEvidenceTitle.Size = new Size(100, 25);
            lblEvidenceTitle.TabIndex = 0;
            lblEvidenceTitle.Text = "Minh chứng:";
            
            // 
            // pictureBoxEvidence
            // 
            pictureBoxEvidence.BackColor = Color.FromArgb(236, 240, 241);
            pictureBoxEvidence.Location = new Point(0, 30);
            pictureBoxEvidence.Name = "pictureBoxEvidence";
            pictureBoxEvidence.Size = new Size(340, 170);
            pictureBoxEvidence.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxEvidence.TabIndex = 1;
            pictureBoxEvidence.TabStop = false;
            
            // 
            // panelApprovalActions
            // 
            panelApprovalActions.Controls.Add(lblRejectionReason);
            panelApprovalActions.Controls.Add(txtRejectionReason);
            panelApprovalActions.Controls.Add(btnReject);
            panelApprovalActions.Controls.Add(btnApprove);
            panelApprovalActions.Location = new Point(15, 360);
            panelApprovalActions.Name = "panelApprovalActions";
            panelApprovalActions.Size = new Size(340, 130);
            panelApprovalActions.TabIndex = 3;
            
            // 
            // btnApprove
            // 
            btnApprove.BackColor = Color.FromArgb(46, 204, 113);
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnApprove.ForeColor = Color.White;
            btnApprove.Location = new Point(0, 0);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(160, 40);
            btnApprove.TabIndex = 0;
            btnApprove.Text = "✅ Duyệt";
            btnApprove.UseVisualStyleBackColor = false;
            btnApprove.Click += btnApprove_Click;
            
            // 
            // btnReject
            // 
            btnReject.BackColor = Color.FromArgb(231, 76, 60);
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.FlatStyle = FlatStyle.Flat;
            btnReject.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReject.ForeColor = Color.White;
            btnReject.Location = new Point(180, 0);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(160, 40);
            btnReject.TabIndex = 1;
            btnReject.Text = "❌ Từ chối";
            btnReject.UseVisualStyleBackColor = false;
            btnReject.Click += btnReject_Click;
            
            // 
            // txtRejectionReason
            // 
            txtRejectionReason.Font = new Font("Segoe UI", 9F);
            txtRejectionReason.Location = new Point(0, 70);
            txtRejectionReason.Multiline = true;
            txtRejectionReason.Name = "txtRejectionReason";
            txtRejectionReason.PlaceholderText = "Nhập lý do từ chối (bắt buộc khi từ chối)...";
            txtRejectionReason.Size = new Size(340, 60);
            txtRejectionReason.TabIndex = 2;
            
            // 
            // lblRejectionReason
            // 
            lblRejectionReason.AutoSize = true;
            lblRejectionReason.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRejectionReason.Location = new Point(0, 50);
            lblRejectionReason.Name = "lblRejectionReason";
            lblRejectionReason.Size = new Size(100, 20);
            lblRejectionReason.TabIndex = 3;
            lblRejectionReason.Text = "Lý do từ chối:";
            
            // 
            // panelUserManagementModule
            // 
            panelUserManagementModule.Controls.Add(dataGridViewUsers);
            panelUserManagementModule.Controls.Add(panelUserActions);
            panelUserManagementModule.Controls.Add(lblUserManagementTitle);
            panelUserManagementModule.Dock = DockStyle.Fill;
            panelUserManagementModule.Location = new Point(0, 0);
            panelUserManagementModule.Name = "panelUserManagementModule";
            panelUserManagementModule.Size = new Size(1150, 720);
            panelUserManagementModule.TabIndex = 2;
            panelUserManagementModule.Visible = false;
            
            // 
            // lblUserManagementTitle
            // 
            lblUserManagementTitle.AutoSize = true;
            lblUserManagementTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblUserManagementTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblUserManagementTitle.Location = new Point(30, 20);
            lblUserManagementTitle.Name = "lblUserManagementTitle";
            lblUserManagementTitle.Size = new Size(280, 41);
            lblUserManagementTitle.TabIndex = 0;
            lblUserManagementTitle.Text = "Quản lý Người dùng";
            
            // 
            // panelUserActions
            // 
            panelUserActions.BackColor = Color.White;
            panelUserActions.Controls.Add(btnExportUsers);
            panelUserActions.Controls.Add(btnImportStudents);
            panelUserActions.Controls.Add(btnAddUser);
            panelUserActions.Location = new Point(30, 80);
            panelUserActions.Name = "panelUserActions";
            panelUserActions.Size = new Size(1090, 60);
            panelUserActions.TabIndex = 1;
            
            // 
            // btnAddUser
            // 
            btnAddUser.BackColor = Color.FromArgb(41, 128, 185);
            btnAddUser.FlatAppearance.BorderSize = 0;
            btnAddUser.FlatStyle = FlatStyle.Flat;
            btnAddUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddUser.ForeColor = Color.White;
            btnAddUser.Location = new Point(20, 15);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(150, 35);
            btnAddUser.TabIndex = 0;
            btnAddUser.Text = "➕ Thêm người dùng";
            btnAddUser.UseVisualStyleBackColor = false;
            btnAddUser.Click += btnAddUser_Click;
            
            // 
            // btnImportStudents
            // 
            btnImportStudents.BackColor = Color.FromArgb(46, 204, 113);
            btnImportStudents.FlatAppearance.BorderSize = 0;
            btnImportStudents.FlatStyle = FlatStyle.Flat;
            btnImportStudents.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnImportStudents.ForeColor = Color.White;
            btnImportStudents.Location = new Point(190, 15);
            btnImportStudents.Name = "btnImportStudents";
            btnImportStudents.Size = new Size(150, 35);
            btnImportStudents.TabIndex = 1;
            btnImportStudents.Text = "📥 Import sinh viên";
            btnImportStudents.UseVisualStyleBackColor = false;
            btnImportStudents.Click += btnImportStudents_Click;
            
            // 
            // btnExportUsers
            // 
            btnExportUsers.BackColor = Color.FromArgb(241, 196, 15);
            btnExportUsers.FlatAppearance.BorderSize = 0;
            btnExportUsers.FlatStyle = FlatStyle.Flat;
            btnExportUsers.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExportUsers.ForeColor = Color.White;
            btnExportUsers.Location = new Point(360, 15);
            btnExportUsers.Name = "btnExportUsers";
            btnExportUsers.Size = new Size(150, 35);
            btnExportUsers.TabIndex = 2;
            btnExportUsers.Text = "📤 Xuất danh sách";
            btnExportUsers.UseVisualStyleBackColor = false;
            btnExportUsers.Click += btnExportUsers_Click;
            
            // 
            // dataGridViewUsers
            // 
            dataGridViewUsers.AllowUserToAddRows = false;
            dataGridViewUsers.AllowUserToDeleteRows = false;
            dataGridViewUsers.BackgroundColor = Color.White;
            dataGridViewUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUsers.Columns.AddRange(new DataGridViewColumn[] { colUserId, colUserFullName, colUserRole, colUserDepartment, colUserStatus, colUserActions });
            dataGridViewUsers.Location = new Point(30, 160);
            dataGridViewUsers.Name = "dataGridViewUsers";
            dataGridViewUsers.ReadOnly = true;
            dataGridViewUsers.RowHeadersWidth = 51;
            dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUsers.Size = new Size(1090, 540);
            dataGridViewUsers.TabIndex = 2;
            dataGridViewUsers.CellClick += dataGridViewUsers_CellClick;
            
            // 
            // colUserId
            // 
            colUserId.HeaderText = "Mã người dùng";
            colUserId.MinimumWidth = 6;
            colUserId.Name = "colUserId";
            colUserId.ReadOnly = true;
            colUserId.Width = 120;
            
            // 
            // colUserFullName
            // 
            colUserFullName.HeaderText = "Họ và tên";
            colUserFullName.MinimumWidth = 6;
            colUserFullName.Name = "colUserFullName";
            colUserFullName.ReadOnly = true;
            colUserFullName.Width = 200;
            
            // 
            // colUserRole
            // 
            colUserRole.HeaderText = "Vai trò";
            colUserRole.MinimumWidth = 6;
            colUserRole.Name = "colUserRole";
            colUserRole.ReadOnly = true;
            colUserRole.Width = 150;
            
            // 
            // colUserDepartment
            // 
            colUserDepartment.HeaderText = "Đơn vị";
            colUserDepartment.MinimumWidth = 6;
            colUserDepartment.Name = "colUserDepartment";
            colUserDepartment.ReadOnly = true;
            colUserDepartment.Width = 200;
            
            // 
            // colUserStatus
            // 
            colUserStatus.HeaderText = "Trạng thái";
            colUserStatus.MinimumWidth = 6;
            colUserStatus.Name = "colUserStatus";
            colUserStatus.ReadOnly = true;
            colUserStatus.Width = 100;
            
            // 
            // colUserActions
            // 
            colUserActions.HeaderText = "Thao tác";
            colUserActions.MinimumWidth = 6;
            colUserActions.Name = "colUserActions";
            colUserActions.ReadOnly = true;
            colUserActions.Text = "Chỉnh sửa";
            colUserActions.UseColumnTextForButtonValue = true;
            colUserActions.Width = 100;
            
            // 
            // panelReportsModule
            // 
            panelReportsModule.Controls.Add(dataGridViewReports);
            panelReportsModule.Controls.Add(chartStatistics);
            panelReportsModule.Controls.Add(panelReportOptions);
            panelReportsModule.Controls.Add(lblReportsTitle);
            panelReportsModule.Dock = DockStyle.Fill;
            panelReportsModule.Location = new Point(0, 0);
            panelReportsModule.Name = "panelReportsModule";
            panelReportsModule.Size = new Size(1150, 720);
            panelReportsModule.TabIndex = 3;
            panelReportsModule.Visible = false;
            
            // 
            // lblReportsTitle
            // 
            lblReportsTitle.AutoSize = true;
            lblReportsTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblReportsTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblReportsTitle.Location = new Point(30, 20);
            lblReportsTitle.Name = "lblReportsTitle";
            lblReportsTitle.Size = new Size(280, 41);
            lblReportsTitle.TabIndex = 0;
            lblReportsTitle.Text = "Báo cáo & Thống kê";
            
            // 
            // panelReportOptions
            // 
            panelReportOptions.BackColor = Color.White;
            panelReportOptions.Controls.Add(lblDateTo);
            panelReportOptions.Controls.Add(lblDateFrom);
            panelReportOptions.Controls.Add(lblReportLevel);
            panelReportOptions.Controls.Add(lblReportType);
            panelReportOptions.Controls.Add(btnExportPDF);
            panelReportOptions.Controls.Add(btnExportExcel);
            panelReportOptions.Controls.Add(btnGenerateReport);
            panelReportOptions.Controls.Add(dateTimePickerTo);
            panelReportOptions.Controls.Add(dateTimePickerFrom);
            panelReportOptions.Controls.Add(cmbReportLevel);
            panelReportOptions.Controls.Add(cmbReportType);
            panelReportOptions.Location = new Point(30, 80);
            panelReportOptions.Name = "panelReportOptions";
            panelReportOptions.Size = new Size(1090, 100);
            panelReportOptions.TabIndex = 1;
            
            // 
            // cmbReportType
            // 
            cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportType.Font = new Font("Segoe UI", 9F);
            cmbReportType.Location = new Point(20, 40);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Size = new Size(150, 28);
            cmbReportType.TabIndex = 0;
            
            // 
            // lblReportType
            // 
            lblReportType.AutoSize = true;
            lblReportType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblReportType.Location = new Point(20, 15);
            lblReportType.Name = "lblReportType";
            lblReportType.Size = new Size(100, 20);
            lblReportType.TabIndex = 1;
            lblReportType.Text = "Loại báo cáo:";
            
            // 
            // cmbReportLevel
            // 
            cmbReportLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportLevel.Font = new Font("Segoe UI", 9F);
            cmbReportLevel.Location = new Point(190, 40);
            cmbReportLevel.Name = "cmbReportLevel";
            cmbReportLevel.Size = new Size(150, 28);
            cmbReportLevel.TabIndex = 2;
            
            // 
            // lblReportLevel
            // 
            lblReportLevel.AutoSize = true;
            lblReportLevel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblReportLevel.Location = new Point(190, 15);
            lblReportLevel.Name = "lblReportLevel";
            lblReportLevel.Size = new Size(75, 20);
            lblReportLevel.TabIndex = 3;
            lblReportLevel.Text = "Cấp độ:";
            
            // 
            // dateTimePickerFrom
            // 
            dateTimePickerFrom.Font = new Font("Segoe UI", 9F);
            dateTimePickerFrom.Location = new Point(360, 40);
            dateTimePickerFrom.Name = "dateTimePickerFrom";
            dateTimePickerFrom.Size = new Size(120, 27);
            dateTimePickerFrom.TabIndex = 4;
            
            // 
            // lblDateFrom
            // 
            lblDateFrom.AutoSize = true;
            lblDateFrom.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDateFrom.Location = new Point(360, 15);
            lblDateFrom.Name = "lblDateFrom";
            lblDateFrom.Size = new Size(70, 20);
            lblDateFrom.TabIndex = 5;
            lblDateFrom.Text = "Từ ngày:";
            
            // 
            // dateTimePickerTo
            // 
            dateTimePickerTo.Font = new Font("Segoe UI", 9F);
            dateTimePickerTo.Location = new Point(500, 40);
            dateTimePickerTo.Name = "dateTimePickerTo";
            dateTimePickerTo.Size = new Size(120, 27);
            dateTimePickerTo.TabIndex = 6;
            
            // 
            // lblDateTo
            // 
            lblDateTo.AutoSize = true;
            lblDateTo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDateTo.Location = new Point(500, 15);
            lblDateTo.Name = "lblDateTo";
            lblDateTo.Size = new Size(80, 20);
            lblDateTo.TabIndex = 7;
            lblDateTo.Text = "Đến ngày:";
            
            // 
            // btnGenerateReport
            // 
            btnGenerateReport.BackColor = Color.FromArgb(41, 128, 185);
            btnGenerateReport.FlatAppearance.BorderSize = 0;
            btnGenerateReport.FlatStyle = FlatStyle.Flat;
            btnGenerateReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGenerateReport.ForeColor = Color.White;
            btnGenerateReport.Location = new Point(640, 38);
            btnGenerateReport.Name = "btnGenerateReport";
            btnGenerateReport.Size = new Size(120, 32);
            btnGenerateReport.TabIndex = 8;
            btnGenerateReport.Text = "Tạo báo cáo";
            btnGenerateReport.UseVisualStyleBackColor = false;
            btnGenerateReport.Click += btnGenerateReport_Click;
            
            // 
            // btnExportExcel
            // 
            btnExportExcel.BackColor = Color.FromArgb(46, 204, 113);
            btnExportExcel.FlatAppearance.BorderSize = 0;
            btnExportExcel.FlatStyle = FlatStyle.Flat;
            btnExportExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportExcel.ForeColor = Color.White;
            btnExportExcel.Location = new Point(780, 38);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(120, 32);
            btnExportExcel.TabIndex = 9;
            btnExportExcel.Text = "Xuất Excel";
            btnExportExcel.UseVisualStyleBackColor = false;
            btnExportExcel.Click += btnExportExcel_Click;
            
            // 
            // btnExportPDF
            // 
            btnExportPDF.BackColor = Color.FromArgb(231, 76, 60);
            btnExportPDF.FlatAppearance.BorderSize = 0;
            btnExportPDF.FlatStyle = FlatStyle.Flat;
            btnExportPDF.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportPDF.ForeColor = Color.White;
            btnExportPDF.Location = new Point(920, 38);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new Size(120, 32);
            btnExportPDF.TabIndex = 10;
            btnExportPDF.Text = "Xuất PDF";
            btnExportPDF.UseVisualStyleBackColor = false;
            btnExportPDF.Click += btnExportPDF_Click;
            
            // 
            // chartStatistics
            // 
            chartStatistics.BackColor = Color.White;
            chartStatistics.Location = new Point(30, 200);
            chartStatistics.Name = "chartStatistics";
            chartStatistics.Size = new Size(540, 300);
            chartStatistics.TabIndex = 2;
            
            // 
            // dataGridViewReports
            // 
            dataGridViewReports.AllowUserToAddRows = false;
            dataGridViewReports.AllowUserToDeleteRows = false;
            dataGridViewReports.BackgroundColor = Color.White;
            dataGridViewReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewReports.Location = new Point(590, 200);
            dataGridViewReports.Name = "dataGridViewReports";
            dataGridViewReports.ReadOnly = true;
            dataGridViewReports.RowHeadersWidth = 51;
            dataGridViewReports.Size = new Size(530, 500);
            dataGridViewReports.TabIndex = 3;
            
            // 
            // panelSystemConfigModule
            // 
            panelSystemConfigModule.Controls.Add(tabControlConfig);
            panelSystemConfigModule.Controls.Add(lblSystemConfigTitle);
            panelSystemConfigModule.Dock = DockStyle.Fill;
            panelSystemConfigModule.Location = new Point(0, 0);
            panelSystemConfigModule.Name = "panelSystemConfigModule";
            panelSystemConfigModule.Size = new Size(1150, 720);
            panelSystemConfigModule.TabIndex = 4;
            panelSystemConfigModule.Visible = false;
            
            // 
            // lblSystemConfigTitle
            // 
            lblSystemConfigTitle.AutoSize = true;
            lblSystemConfigTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblSystemConfigTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblSystemConfigTitle.Location = new Point(30, 20);
            lblSystemConfigTitle.Name = "lblSystemConfigTitle";
            lblSystemConfigTitle.Size = new Size(280, 41);
            lblSystemConfigTitle.TabIndex = 0;
            lblSystemConfigTitle.Text = "Cấu hình Hệ thống";
            
            // 
            // tabControlConfig
            // 
            tabControlConfig.Controls.Add(tabPageAcademicYears);
            tabControlConfig.Controls.Add(tabPageCriteria);
            tabControlConfig.Controls.Add(tabPageDeadlines);
            tabControlConfig.Controls.Add(tabPageSystemSettings);
            tabControlConfig.Font = new Font("Segoe UI", 10F);
            tabControlConfig.Location = new Point(30, 80);
            tabControlConfig.Name = "tabControlConfig";
            tabControlConfig.SelectedIndex = 0;
            tabControlConfig.Size = new Size(1090, 620);
            tabControlConfig.TabIndex = 1;
            
            // 
            // tabPageAcademicYears
            // 
            tabPageAcademicYears.Controls.Add(btnDeleteAcademicYear);
            tabPageAcademicYears.Controls.Add(btnEditAcademicYear);
            tabPageAcademicYears.Controls.Add(btnAddAcademicYear);
            tabPageAcademicYears.Controls.Add(dataGridViewAcademicYears);
            tabPageAcademicYears.Location = new Point(4, 32);
            tabPageAcademicYears.Name = "tabPageAcademicYears";
            tabPageAcademicYears.Padding = new Padding(3);
            tabPageAcademicYears.Size = new Size(1082, 584);
            tabPageAcademicYears.TabIndex = 0;
            tabPageAcademicYears.Text = "Năm học";
            tabPageAcademicYears.UseVisualStyleBackColor = true;
            
            // 
            // dataGridViewAcademicYears
            // 
            dataGridViewAcademicYears.AllowUserToAddRows = false;
            dataGridViewAcademicYears.AllowUserToDeleteRows = false;
            dataGridViewAcademicYears.BackgroundColor = Color.White;
            dataGridViewAcademicYears.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAcademicYears.Location = new Point(20, 60);
            dataGridViewAcademicYears.Name = "dataGridViewAcademicYears";
            dataGridViewAcademicYears.ReadOnly = true;
            dataGridViewAcademicYears.RowHeadersWidth = 51;
            dataGridViewAcademicYears.Size = new Size(1040, 500);
            dataGridViewAcademicYears.TabIndex = 0;
            
            // 
            // btnAddAcademicYear
            // 
            btnAddAcademicYear.BackColor = Color.FromArgb(41, 128, 185);
            btnAddAcademicYear.FlatAppearance.BorderSize = 0;
            btnAddAcademicYear.FlatStyle = FlatStyle.Flat;
            btnAddAcademicYear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddAcademicYear.ForeColor = Color.White;
            btnAddAcademicYear.Location = new Point(20, 20);
            btnAddAcademicYear.Name = "btnAddAcademicYear";
            btnAddAcademicYear.Size = new Size(120, 35);
            btnAddAcademicYear.TabIndex = 1;
            btnAddAcademicYear.Text = "Thêm năm học";
            btnAddAcademicYear.UseVisualStyleBackColor = false;
            btnAddAcademicYear.Click += btnAddAcademicYear_Click;
            
            // 
            // btnEditAcademicYear
            // 
            btnEditAcademicYear.BackColor = Color.FromArgb(241, 196, 15);
            btnEditAcademicYear.FlatAppearance.BorderSize = 0;
            btnEditAcademicYear.FlatStyle = FlatStyle.Flat;
            btnEditAcademicYear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditAcademicYear.ForeColor = Color.White;
            btnEditAcademicYear.Location = new Point(160, 20);
            btnEditAcademicYear.Name = "btnEditAcademicYear";
            btnEditAcademicYear.Size = new Size(120, 35);
            btnEditAcademicYear.TabIndex = 2;
            btnEditAcademicYear.Text = "Chỉnh sửa";
            btnEditAcademicYear.UseVisualStyleBackColor = false;
            btnEditAcademicYear.Click += btnEditAcademicYear_Click;
            
            // 
            // btnDeleteAcademicYear
            // 
            btnDeleteAcademicYear.BackColor = Color.FromArgb(231, 76, 60);
            btnDeleteAcademicYear.FlatAppearance.BorderSize = 0;
            btnDeleteAcademicYear.FlatStyle = FlatStyle.Flat;
            btnDeleteAcademicYear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDeleteAcademicYear.ForeColor = Color.White;
            btnDeleteAcademicYear.Location = new Point(300, 20);
            btnDeleteAcademicYear.Name = "btnDeleteAcademicYear";
            btnDeleteAcademicYear.Size = new Size(120, 35);
            btnDeleteAcademicYear.TabIndex = 3;
            btnDeleteAcademicYear.Text = "Xóa";
            btnDeleteAcademicYear.UseVisualStyleBackColor = false;
            btnDeleteAcademicYear.Click += btnDeleteAcademicYear_Click;
            
            // 
            // tabPageCriteria
            // 
            tabPageCriteria.Controls.Add(btnDeleteCriteria);
            tabPageCriteria.Controls.Add(btnEditCriteria);
            tabPageCriteria.Controls.Add(btnAddCriteria);
            tabPageCriteria.Controls.Add(dataGridViewCriteria);
            tabPageCriteria.Location = new Point(4, 32);
            tabPageCriteria.Name = "tabPageCriteria";
            tabPageCriteria.Padding = new Padding(3);
            tabPageCriteria.Size = new Size(1082, 584);
            tabPageCriteria.TabIndex = 1;
            tabPageCriteria.Text = "Tiêu chí";
            tabPageCriteria.UseVisualStyleBackColor = true;
            
            // 
            // dataGridViewCriteria
            // 
            dataGridViewCriteria.AllowUserToAddRows = false;
            dataGridViewCriteria.AllowUserToDeleteRows = false;
            dataGridViewCriteria.BackgroundColor = Color.White;
            dataGridViewCriteria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCriteria.Location = new Point(20, 60);
            dataGridViewCriteria.Name = "dataGridViewCriteria";
            dataGridViewCriteria.ReadOnly = true;
            dataGridViewCriteria.RowHeadersWidth = 51;
            dataGridViewCriteria.Size = new Size(1040, 500);
            dataGridViewCriteria.TabIndex = 0;
            
            // 
            // btnAddCriteria
            // 
            btnAddCriteria.BackColor = Color.FromArgb(41, 128, 185);
            btnAddCriteria.FlatAppearance.BorderSize = 0;
            btnAddCriteria.FlatStyle = FlatStyle.Flat;
            btnAddCriteria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddCriteria.ForeColor = Color.White;
            btnAddCriteria.Location = new Point(20, 20);
            btnAddCriteria.Name = "btnAddCriteria";
            btnAddCriteria.Size = new Size(120, 35);
            btnAddCriteria.TabIndex = 1;
            btnAddCriteria.Text = "Thêm tiêu chí";
            btnAddCriteria.UseVisualStyleBackColor = false;
            btnAddCriteria.Click += btnAddCriteria_Click;
            
            // 
            // btnEditCriteria
            // 
            btnEditCriteria.BackColor = Color.FromArgb(241, 196, 15);
            btnEditCriteria.FlatAppearance.BorderSize = 0;
            btnEditCriteria.FlatStyle = FlatStyle.Flat;
            btnEditCriteria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditCriteria.ForeColor = Color.White;
            btnEditCriteria.Location = new Point(160, 20);
            btnEditCriteria.Name = "btnEditCriteria";
            btnEditCriteria.Size = new Size(120, 35);
            btnEditCriteria.TabIndex = 2;
            btnEditCriteria.Text = "Chỉnh sửa";
            btnEditCriteria.UseVisualStyleBackColor = false;
            btnEditCriteria.Click += btnEditCriteria_Click;
            
            // 
            // btnDeleteCriteria
            // 
            btnDeleteCriteria.BackColor = Color.FromArgb(231, 76, 60);
            btnDeleteCriteria.FlatAppearance.BorderSize = 0;
            btnDeleteCriteria.FlatStyle = FlatStyle.Flat;
            btnDeleteCriteria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDeleteCriteria.ForeColor = Color.White;
            btnDeleteCriteria.Location = new Point(300, 20);
            btnDeleteCriteria.Name = "btnDeleteCriteria";
            btnDeleteCriteria.Size = new Size(120, 35);
            btnDeleteCriteria.TabIndex = 3;
            btnDeleteCriteria.Text = "Xóa";
            btnDeleteCriteria.UseVisualStyleBackColor = false;
            btnDeleteCriteria.Click += btnDeleteCriteria_Click;
            
            // 
            // tabPageDeadlines
            // 
            tabPageDeadlines.Location = new Point(4, 32);
            tabPageDeadlines.Name = "tabPageDeadlines";
            tabPageDeadlines.Size = new Size(1082, 584);
            tabPageDeadlines.TabIndex = 2;
            tabPageDeadlines.Text = "Thời hạn";
            tabPageDeadlines.UseVisualStyleBackColor = true;
            
            // 
            // tabPageSystemSettings
            // 
            tabPageSystemSettings.Location = new Point(4, 32);
            tabPageSystemSettings.Name = "tabPageSystemSettings";
            tabPageSystemSettings.Size = new Size(1082, 584);
            tabPageSystemSettings.TabIndex = 3;
            tabPageSystemSettings.Text = "Cài đặt hệ thống";
            tabPageSystemSettings.UseVisualStyleBackColor = true;
            
            // 
            // UserDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            ClientSize = new Size(1400, 800);
            Controls.Add(panelMainContent);
            Controls.Add(panelNavigation);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1400, 800);
            Name = "UserDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý Sinh viên 5 Tốt - Command Center";
            WindowState = FormWindowState.Normal;
            Load += UserDashboard_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelNavigation.ResumeLayout(false);
            panelUserProfile.ResumeLayout(false);
            panelUserProfile.PerformLayout();
            panelMainContent.ResumeLayout(false);
            panelDashboardModule.ResumeLayout(false);
            panelDashboardModule.PerformLayout();
            panelQuickStats.ResumeLayout(false);
            cardPendingApprovals.ResumeLayout(false);
            cardPendingApprovals.PerformLayout();
            cardProcessedFiles.ResumeLayout(false);
            cardProcessedFiles.PerformLayout();
            cardDeadlines.ResumeLayout(false);
            cardDeadlines.PerformLayout();
            cardSystemStatus.ResumeLayout(false);
            cardSystemStatus.PerformLayout();
            panelApprovalModule.ResumeLayout(false);
            panelApprovalModule.PerformLayout();
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewApprovals).EndInit();
            panelApprovalDetails.ResumeLayout(false);
            panelApprovalDetails.PerformLayout();
            panelStudentInfo.ResumeLayout(false);
            panelStudentInfo.PerformLayout();
            panelEvidenceViewer.ResumeLayout(false);
            panelEvidenceViewer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEvidence).EndInit();
            panelApprovalActions.ResumeLayout(false);
            panelApprovalActions.PerformLayout();
            panelUserManagementModule.ResumeLayout(false);
            panelUserManagementModule.PerformLayout();
            panelUserActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).EndInit();
            panelReportsModule.ResumeLayout(false);
            panelReportsModule.PerformLayout();
            panelReportOptions.ResumeLayout(false);
            panelReportOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartStatistics).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewReports).EndInit();
            panelSystemConfigModule.ResumeLayout(false);
            panelSystemConfigModule.PerformLayout();
            tabControlConfig.ResumeLayout(false);
            tabPageAcademicYears.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewAcademicYears).EndInit();
            tabPageCriteria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewCriteria).EndInit();
            ResumeLayout(false);
        }

        #endregion

        // Header Components
        private Panel panelHeader;
        private Label lblSystemTitle;
        private Label lblCurrentDateTime;
        private Label lblUserInfo;
        private Button btnRefresh;
        private Button btnLogout;

        // Navigation Components
        private Panel panelNavigation;
        private Panel panelUserProfile;
        private Label lblUserName;
        private Label lblUserRole;
        private Button btnDashboard;
        private Button btnApprovalCenter;
        private Button btnUserManagement;
        private Button btnReportsStats;
        private Button btnSystemConfig;

        // Main Content Panel
        private Panel panelMainContent;

        // Dashboard Module
        private Panel panelDashboardModule;
        private Label lblDashboardTitle;
        private Panel panelQuickStats;
        private Panel cardPendingApprovals;
        private Label lblPendingCount;
        private Label lblPendingTitle;
        private Panel cardProcessedFiles;
        private Label lblProcessedCount;
        private Label lblProcessedTitle;
        private Panel cardDeadlines;
        private Label lblDeadlineInfo;
        private Label lblDeadlineTitle;
        private Panel cardSystemStatus;
        private Label lblSystemStatusInfo;
        private Label lblSystemStatusTitle;

        // Approval Center Module
        private Panel panelApprovalModule;
        private Label lblApprovalTitle;
        private Panel panelFilters;
        private ComboBox cmbStatusFilter;
        private ComboBox cmbDepartmentFilter;
        private ComboBox cmbCriteriaFilter;
        private TextBox txtSearchStudent;
        private Button btnApplyFilters;
        private Button btnClearFilters;
        private Label lblStatusFilter;
        private Label lblDepartmentFilter;
        private Label lblCriteriaFilter;
        private Label lblSearchStudent;
        private DataGridView dataGridViewApprovals;
        private DataGridViewTextBoxColumn colStudentId;
        private DataGridViewTextBoxColumn colStudentName;
        private DataGridViewTextBoxColumn colCriteria;
        private DataGridViewTextBoxColumn colSubmissionDate;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewButtonColumn colActions;
        private Panel panelApprovalDetails;
        private Label lblApprovalDetailsTitle;
        private Panel panelStudentInfo;
        private Label lblStudentDetails;
        private Panel panelEvidenceViewer;
        private Label lblEvidenceTitle;
        private PictureBox pictureBoxEvidence;
        private Panel panelApprovalActions;
        private Button btnApprove;
        private Button btnReject;
        private TextBox txtRejectionReason;
        private Label lblRejectionReason;

        // User Management Module
        private Panel panelUserManagementModule;
        private Label lblUserManagementTitle;
        private Panel panelUserActions;
        private Button btnAddUser;
        private Button btnImportStudents;
        private Button btnExportUsers;
        private DataGridView dataGridViewUsers;
        private DataGridViewTextBoxColumn colUserId;
        private DataGridViewTextBoxColumn colUserFullName;
        private DataGridViewTextBoxColumn colUserRole;
        private DataGridViewTextBoxColumn colUserDepartment;
        private DataGridViewTextBoxColumn colUserStatus;
        private DataGridViewButtonColumn colUserActions;

        // Reports & Statistics Module
        private Panel panelReportsModule;
        private Label lblReportsTitle;
        private Panel panelReportOptions;
        private ComboBox cmbReportType;
        private ComboBox cmbReportLevel;
        private DateTimePicker dateTimePickerFrom;
        private DateTimePicker dateTimePickerTo;
        private Button btnGenerateReport;
        private Button btnExportExcel;
        private Button btnExportPDF;
        private Label lblReportType;
        private Label lblReportLevel;
        private Label lblDateFrom;
        private Label lblDateTo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStatistics;
        private DataGridView dataGridViewReports;

        // System Configuration Module
        private Panel panelSystemConfigModule;
        private Label lblSystemConfigTitle;
        private TabControl tabControlConfig;
        private TabPage tabPageAcademicYears;
        private TabPage tabPageCriteria;
        private TabPage tabPageDeadlines;
        private TabPage tabPageSystemSettings;
        private DataGridView dataGridViewAcademicYears;
        private Button btnAddAcademicYear;
        private Button btnEditAcademicYear;
        private Button btnDeleteAcademicYear;
        private DataGridView dataGridViewCriteria;
        private Button btnAddCriteria;
        private Button btnEditCriteria;
        private Button btnDeleteCriteria;
    }
}