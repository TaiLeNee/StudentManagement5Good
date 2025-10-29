namespace StudentManagement5Good.Winform
{
    partial class StudentProfileReviewForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelLeftQueue = new System.Windows.Forms.Panel();
            this.listViewStudents = new System.Windows.Forms.ListView();
            this.columnHeaderAvatar = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderStudentInfo = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderProgress = new System.Windows.Forms.ColumnHeader();
            this.panelLeftFilters = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.cmbUnitFilter = new System.Windows.Forms.ComboBox();
            this.lblQueueTitle = new System.Windows.Forms.Label();
            this.panelRightProfile = new System.Windows.Forms.Panel();
            this.panelFinalDecision = new System.Windows.Forms.Panel();
            this.btnRejectAll = new System.Windows.Forms.Button();
            this.btnApproveAll = new System.Windows.Forms.Button();
            this.txtGeneralNote = new System.Windows.Forms.TextBox();
            this.lblGeneralNote = new System.Windows.Forms.Label();
            this.lblFinalStatus = new System.Windows.Forms.Label();
            this.tabControlCriteria = new System.Windows.Forms.TabControl();
            this.tabPageHocTap = new System.Windows.Forms.TabPage();
            this.tabPageDaoDuc = new System.Windows.Forms.TabPage();
            this.tabPageTheLuc = new System.Windows.Forms.TabPage();
            this.tabPageTinhNguyen = new System.Windows.Forms.TabPage();
            this.tabPageHoiNhap = new System.Windows.Forms.TabPage();
            this.panelStudentInfo = new System.Windows.Forms.Panel();
            this.lblStudentClass = new System.Windows.Forms.Label();
            this.lblStudentId = new System.Windows.Forms.Label();
            this.lblStudentName = new System.Windows.Forms.Label();
            this.pictureBoxAvatar = new System.Windows.Forms.PictureBox();
            this.lblNoStudentSelected = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelLeftQueue.SuspendLayout();
            this.panelLeftFilters.SuspendLayout();
            this.panelRightProfile.SuspendLayout();
            this.panelFinalDecision.SuspendLayout();
            this.tabControlCriteria.SuspendLayout();
            this.panelStudentInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panelLeftQueue);
            this.splitContainerMain.Panel1MinSize = 300;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelRightProfile);
            this.splitContainerMain.Size = new System.Drawing.Size(1400, 800);
            this.splitContainerMain.SplitterDistance = 420;
            this.splitContainerMain.TabIndex = 0;
            // 
            // panelLeftQueue
            // 
            this.panelLeftQueue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelLeftQueue.Controls.Add(this.listViewStudents);
            this.panelLeftQueue.Controls.Add(this.panelLeftFilters);
            this.panelLeftQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeftQueue.Location = new System.Drawing.Point(0, 0);
            this.panelLeftQueue.Name = "panelLeftQueue";
            this.panelLeftQueue.Padding = new System.Windows.Forms.Padding(10);
            this.panelLeftQueue.Size = new System.Drawing.Size(420, 800);
            this.panelLeftQueue.TabIndex = 0;
            // 
            // listViewStudents
            // 
            this.listViewStudents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderAvatar,
            this.columnHeaderStudentInfo,
            this.columnHeaderProgress});
            this.listViewStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewStudents.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.listViewStudents.FullRowSelect = true;
            this.listViewStudents.HideSelection = false;
            this.listViewStudents.Location = new System.Drawing.Point(10, 180);
            this.listViewStudents.MultiSelect = false;
            this.listViewStudents.Name = "listViewStudents";
            this.listViewStudents.Size = new System.Drawing.Size(400, 610);
            this.listViewStudents.TabIndex = 1;
            this.listViewStudents.UseCompatibleStateImageBehavior = false;
            this.listViewStudents.View = System.Windows.Forms.View.Details;
            this.listViewStudents.SelectedIndexChanged += new System.EventHandler(this.listViewStudents_SelectedIndexChanged);
            // 
            // columnHeaderAvatar
            // 
            this.columnHeaderAvatar.Text = "";
            this.columnHeaderAvatar.Width = 50;
            // 
            // columnHeaderStudentInfo
            // 
            this.columnHeaderStudentInfo.Text = "Sinh viên";
            this.columnHeaderStudentInfo.Width = 220;
            // 
            // columnHeaderProgress
            // 
            this.columnHeaderProgress.Text = "Tiến độ";
            this.columnHeaderProgress.Width = 120;
            // 
            // panelLeftFilters
            // 
            this.panelLeftFilters.BackColor = System.Drawing.Color.White;
            this.panelLeftFilters.Controls.Add(this.txtSearch);
            this.panelLeftFilters.Controls.Add(this.cmbStatusFilter);
            this.panelLeftFilters.Controls.Add(this.cmbUnitFilter);
            this.panelLeftFilters.Controls.Add(this.lblQueueTitle);
            this.panelLeftFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLeftFilters.Location = new System.Drawing.Point(10, 10);
            this.panelLeftFilters.Name = "panelLeftFilters";
            this.panelLeftFilters.Padding = new System.Windows.Forms.Padding(15);
            this.panelLeftFilters.Size = new System.Drawing.Size(400, 170);
            this.panelLeftFilters.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(15, 125);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(370, 25);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.Text = "🔍 Tìm theo tên hoặc mã SV...";
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cmbStatusFilter
            // 
            this.cmbStatusFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatusFilter.FormattingEnabled = true;
            this.cmbStatusFilter.Location = new System.Drawing.Point(15, 90);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(370, 25);
            this.cmbStatusFilter.TabIndex = 2;
            this.cmbStatusFilter.SelectedIndexChanged += new System.EventHandler(this.ApplyFilters);
            // 
            // cmbUnitFilter
            // 
            this.cmbUnitFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbUnitFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnitFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbUnitFilter.FormattingEnabled = true;
            this.cmbUnitFilter.Location = new System.Drawing.Point(15, 55);
            this.cmbUnitFilter.Name = "cmbUnitFilter";
            this.cmbUnitFilter.Size = new System.Drawing.Size(370, 25);
            this.cmbUnitFilter.TabIndex = 1;
            this.cmbUnitFilter.SelectedIndexChanged += new System.EventHandler(this.ApplyFilters);
            // 
            // lblQueueTitle
            // 
            this.lblQueueTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblQueueTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblQueueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblQueueTitle.Location = new System.Drawing.Point(15, 15);
            this.lblQueueTitle.Name = "lblQueueTitle";
            this.lblQueueTitle.Size = new System.Drawing.Size(370, 40);
            this.lblQueueTitle.TabIndex = 0;
            this.lblQueueTitle.Text = "📋 Hàng đợi xét duyệt";
            this.lblQueueTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelRightProfile
            // 
            this.panelRightProfile.BackColor = System.Drawing.Color.White;
            this.panelRightProfile.Controls.Add(this.panelFinalDecision);
            this.panelRightProfile.Controls.Add(this.tabControlCriteria);
            this.panelRightProfile.Controls.Add(this.panelStudentInfo);
            this.panelRightProfile.Controls.Add(this.lblNoStudentSelected);
            this.panelRightProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRightProfile.Location = new System.Drawing.Point(0, 0);
            this.panelRightProfile.Name = "panelRightProfile";
            this.panelRightProfile.Padding = new System.Windows.Forms.Padding(20);
            this.panelRightProfile.Size = new System.Drawing.Size(976, 800);
            this.panelRightProfile.TabIndex = 0;
            // 
            // panelFinalDecision
            // 
            this.panelFinalDecision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelFinalDecision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFinalDecision.Controls.Add(this.btnRejectAll);
            this.panelFinalDecision.Controls.Add(this.btnApproveAll);
            this.panelFinalDecision.Controls.Add(this.txtGeneralNote);
            this.panelFinalDecision.Controls.Add(this.lblGeneralNote);
            this.panelFinalDecision.Controls.Add(this.lblFinalStatus);
            this.panelFinalDecision.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFinalDecision.Location = new System.Drawing.Point(20, 640);
            this.panelFinalDecision.Name = "panelFinalDecision";
            this.panelFinalDecision.Padding = new System.Windows.Forms.Padding(15);
            this.panelFinalDecision.Size = new System.Drawing.Size(936, 140);
            this.panelFinalDecision.TabIndex = 3;
            // 
            // btnRejectAll
            // 
            this.btnRejectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnRejectAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRejectAll.Enabled = false;
            this.btnRejectAll.FlatAppearance.BorderSize = 0;
            this.btnRejectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRejectAll.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRejectAll.ForeColor = System.Drawing.Color.White;
            this.btnRejectAll.Location = new System.Drawing.Point(423, 15);
            this.btnRejectAll.Name = "btnRejectAll";
            this.btnRejectAll.Size = new System.Drawing.Size(248, 108);
            this.btnRejectAll.TabIndex = 4;
            this.btnRejectAll.Text = "❌ Từ chối\r\nSinh viên 5 Tốt";
            this.btnRejectAll.UseVisualStyleBackColor = false;
            this.btnRejectAll.Click += new System.EventHandler(this.btnRejectAll_Click);
            // 
            // btnApproveAll
            // 
            this.btnApproveAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnApproveAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnApproveAll.Enabled = false;
            this.btnApproveAll.FlatAppearance.BorderSize = 0;
            this.btnApproveAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApproveAll.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnApproveAll.ForeColor = System.Drawing.Color.White;
            this.btnApproveAll.Location = new System.Drawing.Point(671, 15);
            this.btnApproveAll.Name = "btnApproveAll";
            this.btnApproveAll.Size = new System.Drawing.Size(248, 108);
            this.btnApproveAll.TabIndex = 3;
            this.btnApproveAll.Text = "✅ Công nhận\r\nSinh viên 5 Tốt";
            this.btnApproveAll.UseVisualStyleBackColor = false;
            this.btnApproveAll.Click += new System.EventHandler(this.btnApproveAll_Click);
            // 
            // txtGeneralNote
            // 
            this.txtGeneralNote.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtGeneralNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGeneralNote.Location = new System.Drawing.Point(15, 55);
            this.txtGeneralNote.Multiline = true;
            this.txtGeneralNote.Name = "txtGeneralNote";
            this.txtGeneralNote.PlaceholderText = "Nhập ghi chú chung cho hồ sơ (nếu có)...";
            this.txtGeneralNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGeneralNote.Size = new System.Drawing.Size(600, 68);
            this.txtGeneralNote.TabIndex = 2;
            // 
            // lblGeneralNote
            // 
            this.lblGeneralNote.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGeneralNote.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGeneralNote.Location = new System.Drawing.Point(15, 30);
            this.lblGeneralNote.Name = "lblGeneralNote";
            this.lblGeneralNote.Size = new System.Drawing.Size(904, 25);
            this.lblGeneralNote.TabIndex = 1;
            this.lblGeneralNote.Text = "📝 Ghi chú chung:";
            this.lblGeneralNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFinalStatus
            // 
            this.lblFinalStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFinalStatus.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFinalStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblFinalStatus.Location = new System.Drawing.Point(15, 15);
            this.lblFinalStatus.Name = "lblFinalStatus";
            this.lblFinalStatus.Size = new System.Drawing.Size(904, 15);
            this.lblFinalStatus.TabIndex = 0;
            this.lblFinalStatus.Text = "⏳ Quyết định tổng kết: Chưa đủ điều kiện công nhận";
            this.lblFinalStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControlCriteria
            // 
            this.tabControlCriteria.Controls.Add(this.tabPageHocTap);
            this.tabControlCriteria.Controls.Add(this.tabPageDaoDuc);
            this.tabControlCriteria.Controls.Add(this.tabPageTheLuc);
            this.tabControlCriteria.Controls.Add(this.tabPageTinhNguyen);
            this.tabControlCriteria.Controls.Add(this.tabPageHoiNhap);
            this.tabControlCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlCriteria.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabControlCriteria.Location = new System.Drawing.Point(20, 140);
            this.tabControlCriteria.Name = "tabControlCriteria";
            this.tabControlCriteria.SelectedIndex = 0;
            this.tabControlCriteria.Size = new System.Drawing.Size(936, 660);
            this.tabControlCriteria.TabIndex = 2;
            // 
            // tabPageHocTap
            // 
            this.tabPageHocTap.AutoScroll = true;
            this.tabPageHocTap.Location = new System.Drawing.Point(4, 26);
            this.tabPageHocTap.Name = "tabPageHocTap";
            this.tabPageHocTap.Padding = new System.Windows.Forms.Padding(15);
            this.tabPageHocTap.Size = new System.Drawing.Size(928, 630);
            this.tabPageHocTap.TabIndex = 0;
            this.tabPageHocTap.Text = "📚 Học tập tốt";
            this.tabPageHocTap.UseVisualStyleBackColor = true;
            // 
            // tabPageDaoDuc
            // 
            this.tabPageDaoDuc.AutoScroll = true;
            this.tabPageDaoDuc.Location = new System.Drawing.Point(4, 26);
            this.tabPageDaoDuc.Name = "tabPageDaoDuc";
            this.tabPageDaoDuc.Padding = new System.Windows.Forms.Padding(15);
            this.tabPageDaoDuc.Size = new System.Drawing.Size(928, 630);
            this.tabPageDaoDuc.TabIndex = 1;
            this.tabPageDaoDuc.Text = "🎓 Đạo đức tốt";
            this.tabPageDaoDuc.UseVisualStyleBackColor = true;
            // 
            // tabPageTheLuc
            // 
            this.tabPageTheLuc.AutoScroll = true;
            this.tabPageTheLuc.Location = new System.Drawing.Point(4, 26);
            this.tabPageTheLuc.Name = "tabPageTheLuc";
            this.tabPageTheLuc.Padding = new System.Windows.Forms.Padding(15);
            this.tabPageTheLuc.Size = new System.Drawing.Size(928, 630);
            this.tabPageTheLuc.TabIndex = 2;
            this.tabPageTheLuc.Text = "💪 Thể lực tốt";
            this.tabPageTheLuc.UseVisualStyleBackColor = true;
            // 
            // tabPageTinhNguyen
            // 
            this.tabPageTinhNguyen.AutoScroll = true;
            this.tabPageTinhNguyen.Location = new System.Drawing.Point(4, 26);
            this.tabPageTinhNguyen.Name = "tabPageTinhNguyen";
            this.tabPageTinhNguyen.Padding = new System.Windows.Forms.Padding(15);
            this.tabPageTinhNguyen.Size = new System.Drawing.Size(928, 630);
            this.tabPageTinhNguyen.TabIndex = 3;
            this.tabPageTinhNguyen.Text = "🤝 Tình nguyện tốt";
            this.tabPageTinhNguyen.UseVisualStyleBackColor = true;
            // 
            // tabPageHoiNhap
            // 
            this.tabPageHoiNhap.AutoScroll = true;
            this.tabPageHoiNhap.Location = new System.Drawing.Point(4, 26);
            this.tabPageHoiNhap.Name = "tabPageHoiNhap";
            this.tabPageHoiNhap.Padding = new System.Windows.Forms.Padding(15);
            this.tabPageHoiNhap.Size = new System.Drawing.Size(928, 630);
            this.tabPageHoiNhap.TabIndex = 4;
            this.tabPageHoiNhap.Text = "🌐 Hội nhập tốt";
            this.tabPageHoiNhap.UseVisualStyleBackColor = true;
            // 
            // panelStudentInfo
            // 
            this.panelStudentInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.panelStudentInfo.Controls.Add(this.lblStudentClass);
            this.panelStudentInfo.Controls.Add(this.lblStudentId);
            this.panelStudentInfo.Controls.Add(this.lblStudentName);
            this.panelStudentInfo.Controls.Add(this.pictureBoxAvatar);
            this.panelStudentInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStudentInfo.Location = new System.Drawing.Point(20, 20);
            this.panelStudentInfo.Name = "panelStudentInfo";
            this.panelStudentInfo.Padding = new System.Windows.Forms.Padding(20);
            this.panelStudentInfo.Size = new System.Drawing.Size(936, 120);
            this.panelStudentInfo.TabIndex = 1;
            // 
            // lblStudentClass
            // 
            this.lblStudentClass.AutoSize = true;
            this.lblStudentClass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStudentClass.ForeColor = System.Drawing.Color.White;
            this.lblStudentClass.Location = new System.Drawing.Point(140, 80);
            this.lblStudentClass.Name = "lblStudentClass";
            this.lblStudentClass.Size = new System.Drawing.Size(120, 19);
            this.lblStudentClass.TabIndex = 3;
            this.lblStudentClass.Text = "Lớp: Công nghệ thông tin";
            // 
            // lblStudentId
            // 
            this.lblStudentId.AutoSize = true;
            this.lblStudentId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStudentId.ForeColor = System.Drawing.Color.White;
            this.lblStudentId.Location = new System.Drawing.Point(140, 53);
            this.lblStudentId.Name = "lblStudentId";
            this.lblStudentId.Size = new System.Drawing.Size(85, 19);
            this.lblStudentId.TabIndex = 2;
            this.lblStudentId.Text = "MSSV: 01";
            // 
            // lblStudentName
            // 
            this.lblStudentName.AutoSize = true;
            this.lblStudentName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblStudentName.ForeColor = System.Drawing.Color.White;
            this.lblStudentName.Location = new System.Drawing.Point(138, 20);
            this.lblStudentName.Name = "lblStudentName";
            this.lblStudentName.Size = new System.Drawing.Size(200, 25);
            this.lblStudentName.TabIndex = 1;
            this.lblStudentName.Text = "Nguyễn Văn A";
            // 
            // pictureBoxAvatar
            // 
            this.pictureBoxAvatar.BackColor = System.Drawing.Color.White;
            this.pictureBoxAvatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxAvatar.Location = new System.Drawing.Point(20, 20);
            this.pictureBoxAvatar.Name = "pictureBoxAvatar";
            this.pictureBoxAvatar.Size = new System.Drawing.Size(100, 80);
            this.pictureBoxAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAvatar.TabIndex = 0;
            this.pictureBoxAvatar.TabStop = false;
            // 
            // lblNoStudentSelected
            // 
            this.lblNoStudentSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoStudentSelected.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblNoStudentSelected.ForeColor = System.Drawing.Color.Gray;
            this.lblNoStudentSelected.Location = new System.Drawing.Point(20, 20);
            this.lblNoStudentSelected.Name = "lblNoStudentSelected";
            this.lblNoStudentSelected.Size = new System.Drawing.Size(936, 760);
            this.lblNoStudentSelected.TabIndex = 0;
            this.lblNoStudentSelected.Text = "👈 Vui lòng chọn sinh viên từ danh sách bên trái";
            this.lblNoStudentSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StudentProfileReviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this.splitContainerMain);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "StudentProfileReviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xét duyệt Hồ sơ Sinh viên 5 Tốt";
            this.Load += new System.EventHandler(this.StudentProfileReviewForm_Load);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelLeftQueue.ResumeLayout(false);
            this.panelLeftFilters.ResumeLayout(false);
            this.panelLeftFilters.PerformLayout();
            this.panelRightProfile.ResumeLayout(false);
            this.panelFinalDecision.ResumeLayout(false);
            this.panelFinalDecision.PerformLayout();
            this.tabControlCriteria.ResumeLayout(false);
            this.panelStudentInfo.ResumeLayout(false);
            this.panelStudentInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelLeftQueue;
        private System.Windows.Forms.ListView listViewStudents;
        private System.Windows.Forms.ColumnHeader columnHeaderAvatar;
        private System.Windows.Forms.ColumnHeader columnHeaderStudentInfo;
        private System.Windows.Forms.ColumnHeader columnHeaderProgress;
        private System.Windows.Forms.Panel panelLeftFilters;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.ComboBox cmbUnitFilter;
        private System.Windows.Forms.Label lblQueueTitle;
        private System.Windows.Forms.Panel panelRightProfile;
        private System.Windows.Forms.Label lblNoStudentSelected;
        private System.Windows.Forms.Panel panelStudentInfo;
        private System.Windows.Forms.Label lblStudentClass;
        private System.Windows.Forms.Label lblStudentId;
        private System.Windows.Forms.Label lblStudentName;
        private System.Windows.Forms.PictureBox pictureBoxAvatar;
        private System.Windows.Forms.TabControl tabControlCriteria;
        private System.Windows.Forms.TabPage tabPageHocTap;
        private System.Windows.Forms.TabPage tabPageDaoDuc;
        private System.Windows.Forms.TabPage tabPageTheLuc;
        private System.Windows.Forms.TabPage tabPageTinhNguyen;
        private System.Windows.Forms.TabPage tabPageHoiNhap;
        private System.Windows.Forms.Panel panelFinalDecision;
        private System.Windows.Forms.Button btnRejectAll;
        private System.Windows.Forms.Button btnApproveAll;
        private System.Windows.Forms.TextBox txtGeneralNote;
        private System.Windows.Forms.Label lblGeneralNote;
        private System.Windows.Forms.Label lblFinalStatus;
    }
}

