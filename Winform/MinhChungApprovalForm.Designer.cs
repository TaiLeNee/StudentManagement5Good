namespace StudentManagement5Good.Winform
{
    partial class MinhChungApprovalForm
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
            panelHeader = new Panel();
            btnClose = new Button();
            btnRefresh = new Button();
            lblApproverRole = new Label();
            lblApproverName = new Label();
            label1 = new Label();
            panelStats = new Panel();
            lblCriteriaBreakdown = new Label();
            lblTotalCount = new Label();
            label3 = new Label();
            panelMain = new Panel();
            panelApproval = new Panel();
            groupBox2 = new GroupBox();
            btnViewFile = new Button();
            btnApprove = new Button();
            txtFeedback = new TextBox();
            lblFeedback = new Label();
            cmbApprovalStatus = new ComboBox();
            label15 = new Label();
            groupBox1 = new GroupBox();
            txtDescription = new TextBox();
            label14 = new Label();
            lblFileSize = new Label();
            label12 = new Label();
            lblFileName = new Label();
            label10 = new Label();
            lblSubmissionDate = new Label();
            label8 = new Label();
            lblEvidenceName = new Label();
            label6 = new Label();
            lblCriteria = new Label();
            label4 = new Label();
            lblStudentClass = new Label();
            lblStudentId = new Label();
            lblStudentName = new Label();
            label11 = new Label();
            label9 = new Label();
            label7 = new Label();
            label5 = new Label();
            panelList = new Panel();
            listViewEvidences = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            label2 = new Label();
            panelHeader.SuspendLayout();
            panelStats.SuspendLayout();
            panelMain.SuspendLayout();
            panelApproval.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            panelList.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(52, 152, 219);
            panelHeader.Controls.Add(btnClose);
            panelHeader.Controls.Add(btnRefresh);
            panelHeader.Controls.Add(lblApproverRole);
            panelHeader.Controls.Add(lblApproverName);
            panelHeader.Controls.Add(label1);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1400, 80);
            panelHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.FromArgb(231, 76, 60);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(1320, 25);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(70, 30);
            btnClose.TabIndex = 4;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.FromArgb(46, 204, 113);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1240, 25);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 30);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblApproverRole
            // 
            lblApproverRole.AutoSize = true;
            lblApproverRole.Font = new Font("Segoe UI", 10F);
            lblApproverRole.ForeColor = Color.White;
            lblApproverRole.Location = new Point(20, 50);
            lblApproverRole.Name = "lblApproverRole";
            lblApproverRole.Size = new Size(89, 19);
            lblApproverRole.TabIndex = 2;
            lblApproverRole.Text = "Quản trị viên";
            // 
            // lblApproverName
            // 
            lblApproverName.AutoSize = true;
            lblApproverName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblApproverName.ForeColor = Color.White;
            lblApproverName.Location = new Point(20, 25);
            lblApproverName.Name = "lblApproverName";
            lblApproverName.Size = new Size(119, 21);
            lblApproverName.TabIndex = 1;
            lblApproverName.Text = "Nguyễn Văn A";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(600, 25);
            label1.Name = "label1";
            label1.Size = new Size(242, 30);
            label1.TabIndex = 0;
            label1.Text = "🔍 Duyệt minh chứng";
            // 
            // panelStats
            // 
            panelStats.BackColor = Color.White;
            panelStats.Controls.Add(lblCriteriaBreakdown);
            panelStats.Controls.Add(lblTotalCount);
            panelStats.Controls.Add(label3);
            panelStats.Dock = DockStyle.Top;
            panelStats.Location = new Point(0, 80);
            panelStats.Name = "panelStats";
            panelStats.Padding = new Padding(20, 10, 20, 10);
            panelStats.Size = new Size(1400, 50);
            panelStats.TabIndex = 1;
            // 
            // lblCriteriaBreakdown
            // 
            lblCriteriaBreakdown.AutoSize = true;
            lblCriteriaBreakdown.Font = new Font("Segoe UI", 9F);
            lblCriteriaBreakdown.ForeColor = Color.FromArgb(127, 140, 141);
            lblCriteriaBreakdown.Location = new Point(300, 15);
            lblCriteriaBreakdown.Name = "lblCriteriaBreakdown";
            lblCriteriaBreakdown.Size = new Size(185, 15);
            lblCriteriaBreakdown.TabIndex = 2;
            lblCriteriaBreakdown.Text = "Đạo đức: 5 | Học tập: 3 | Thể lực: 2";
            // 
            // lblTotalCount
            // 
            lblTotalCount.AutoSize = true;
            lblTotalCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalCount.ForeColor = Color.FromArgb(52, 73, 94);
            lblTotalCount.Location = new Point(150, 15);
            lblTotalCount.Name = "lblTotalCount";
            lblTotalCount.Size = new Size(78, 19);
            lblTotalCount.TabIndex = 1;
            lblTotalCount.Text = "Tổng số: 0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(52, 73, 94);
            label3.Location = new Point(20, 15);
            label3.Name = "label3";
            label3.Size = new Size(119, 19);
            label3.TabIndex = 0;
            label3.Text = "Minh chứng chờ:";
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(236, 240, 241);
            panelMain.Controls.Add(panelApproval);
            panelMain.Controls.Add(panelList);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 130);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Size = new Size(1400, 570);
            panelMain.TabIndex = 2;
            // 
            // panelApproval
            // 
            panelApproval.BackColor = Color.White;
            panelApproval.Controls.Add(groupBox2);
            panelApproval.Controls.Add(groupBox1);
            panelApproval.Controls.Add(label5);
            panelApproval.Dock = DockStyle.Right;
            panelApproval.Location = new Point(880, 20);
            panelApproval.Name = "panelApproval";
            panelApproval.Padding = new Padding(20);
            panelApproval.Size = new Size(500, 530);
            panelApproval.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnViewFile);
            groupBox2.Controls.Add(btnApprove);
            groupBox2.Controls.Add(txtFeedback);
            groupBox2.Controls.Add(lblFeedback);
            groupBox2.Controls.Add(cmbApprovalStatus);
            groupBox2.Controls.Add(label15);
            groupBox2.Dock = DockStyle.Bottom;
            groupBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox2.ForeColor = Color.FromArgb(52, 73, 94);
            groupBox2.Location = new Point(20, 350);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(460, 160);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Quyết định duyệt";
            // 
            // btnViewFile
            // 
            btnViewFile.BackColor = Color.FromArgb(52, 152, 219);
            btnViewFile.Enabled = false;
            btnViewFile.FlatAppearance.BorderSize = 0;
            btnViewFile.FlatStyle = FlatStyle.Flat;
            btnViewFile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnViewFile.ForeColor = Color.White;
            btnViewFile.Location = new Point(15, 120);
            btnViewFile.Name = "btnViewFile";
            btnViewFile.Size = new Size(100, 30);
            btnViewFile.TabIndex = 5;
            btnViewFile.Text = "Xem file";
            btnViewFile.UseVisualStyleBackColor = false;
            btnViewFile.Click += btnViewFile_Click;
            // 
            // btnApprove
            // 
            btnApprove.BackColor = Color.FromArgb(46, 204, 113);
            btnApprove.Enabled = false;
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnApprove.ForeColor = Color.White;
            btnApprove.Location = new Point(350, 120);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(100, 30);
            btnApprove.TabIndex = 4;
            btnApprove.Text = "Xác nhận";
            btnApprove.UseVisualStyleBackColor = false;
            btnApprove.Click += btnApprove_Click;
            // 
            // txtFeedback
            // 
            txtFeedback.Font = new Font("Segoe UI", 9F);
            txtFeedback.Location = new Point(15, 75);
            txtFeedback.Multiline = true;
            txtFeedback.Name = "txtFeedback";
            txtFeedback.PlaceholderText = "Nhập lý do từ chối hoặc ghi chú...";
            txtFeedback.ScrollBars = ScrollBars.Vertical;
            txtFeedback.Size = new Size(435, 40);
            txtFeedback.TabIndex = 3;
            // 
            // lblFeedback
            // 
            lblFeedback.AutoSize = true;
            lblFeedback.Font = new Font("Segoe UI", 9F);
            lblFeedback.ForeColor = Color.FromArgb(52, 73, 94);
            lblFeedback.Location = new Point(15, 55);
            lblFeedback.Name = "lblFeedback";
            lblFeedback.Size = new Size(109, 15);
            lblFeedback.TabIndex = 2;
            lblFeedback.Text = "Ghi chú (tùy chọn):";
            // 
            // cmbApprovalStatus
            // 
            cmbApprovalStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbApprovalStatus.Font = new Font("Segoe UI", 9F);
            cmbApprovalStatus.FormattingEnabled = true;
            cmbApprovalStatus.Location = new Point(15, 45);
            cmbApprovalStatus.Name = "cmbApprovalStatus";
            cmbApprovalStatus.Size = new Size(200, 23);
            cmbApprovalStatus.TabIndex = 1;
            cmbApprovalStatus.SelectedIndexChanged += cmbApprovalStatus_SelectedIndexChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F);
            label15.ForeColor = Color.FromArgb(52, 73, 94);
            label15.Location = new Point(15, 25);
            label15.Name = "label15";
            label15.Size = new Size(69, 15);
            label15.TabIndex = 0;
            label15.Text = "Quyết định:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtDescription);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(lblFileSize);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(lblFileName);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(lblSubmissionDate);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(lblEvidenceName);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(lblCriteria);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(lblStudentClass);
            groupBox1.Controls.Add(lblStudentId);
            groupBox1.Controls.Add(lblStudentName);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label7);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(52, 73, 94);
            groupBox1.Location = new Point(20, 20);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(460, 290);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin chi tiết";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 9F);
            txtDescription.Location = new Point(15, 230);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(435, 50);
            txtDescription.TabIndex = 17;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F);
            label14.ForeColor = Color.FromArgb(127, 140, 141);
            label14.Location = new Point(15, 210);
            label14.Name = "label14";
            label14.Size = new Size(41, 15);
            label14.TabIndex = 16;
            label14.Text = "Mô tả:";
            // 
            // lblFileSize
            // 
            lblFileSize.AutoSize = true;
            lblFileSize.Font = new Font("Segoe UI", 9F);
            lblFileSize.ForeColor = Color.FromArgb(52, 73, 94);
            lblFileSize.Location = new Point(320, 185);
            lblFileSize.Name = "lblFileSize";
            lblFileSize.Size = new Size(29, 15);
            lblFileSize.TabIndex = 15;
            lblFileSize.Text = "N/A";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F);
            label12.ForeColor = Color.FromArgb(127, 140, 141);
            label12.Location = new Point(240, 185);
            label12.Name = "label12";
            label12.Size = new Size(67, 15);
            label12.TabIndex = 14;
            label12.Text = "Kích thước:";
            // 
            // lblFileName
            // 
            lblFileName.AutoSize = true;
            lblFileName.Font = new Font("Segoe UI", 9F);
            lblFileName.ForeColor = Color.FromArgb(52, 73, 94);
            lblFileName.Location = new Point(80, 185);
            lblFileName.Name = "lblFileName";
            lblFileName.Size = new Size(29, 15);
            lblFileName.TabIndex = 13;
            lblFileName.Text = "N/A";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F);
            label10.ForeColor = Color.FromArgb(127, 140, 141);
            label10.Location = new Point(15, 185);
            label10.Name = "label10";
            label10.Size = new Size(47, 15);
            label10.TabIndex = 12;
            label10.Text = "Tên file:";
            // 
            // lblSubmissionDate
            // 
            lblSubmissionDate.AutoSize = true;
            lblSubmissionDate.Font = new Font("Segoe UI", 9F);
            lblSubmissionDate.ForeColor = Color.FromArgb(52, 73, 94);
            lblSubmissionDate.Location = new Point(90, 160);
            lblSubmissionDate.Name = "lblSubmissionDate";
            lblSubmissionDate.Size = new Size(29, 15);
            lblSubmissionDate.TabIndex = 11;
            lblSubmissionDate.Text = "N/A";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.ForeColor = Color.FromArgb(127, 140, 141);
            label8.Location = new Point(15, 160);
            label8.Name = "label8";
            label8.Size = new Size(62, 15);
            label8.TabIndex = 10;
            label8.Text = "Ngày nộp:";
            // 
            // lblEvidenceName
            // 
            lblEvidenceName.AutoSize = true;
            lblEvidenceName.Font = new Font("Segoe UI", 9F);
            lblEvidenceName.ForeColor = Color.FromArgb(52, 73, 94);
            lblEvidenceName.Location = new Point(120, 135);
            lblEvidenceName.Name = "lblEvidenceName";
            lblEvidenceName.Size = new Size(29, 15);
            lblEvidenceName.TabIndex = 9;
            lblEvidenceName.Text = "N/A";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.ForeColor = Color.FromArgb(127, 140, 141);
            label6.Location = new Point(15, 135);
            label6.Name = "label6";
            label6.Size = new Size(96, 15);
            label6.TabIndex = 8;
            label6.Text = "Tên minh chứng:";
            // 
            // lblCriteria
            // 
            lblCriteria.AutoSize = true;
            lblCriteria.Font = new Font("Segoe UI", 9F);
            lblCriteria.ForeColor = Color.FromArgb(52, 73, 94);
            lblCriteria.Location = new Point(70, 110);
            lblCriteria.Name = "lblCriteria";
            lblCriteria.Size = new Size(29, 15);
            lblCriteria.TabIndex = 7;
            lblCriteria.Text = "N/A";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.ForeColor = Color.FromArgb(127, 140, 141);
            label4.Location = new Point(15, 110);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 6;
            label4.Text = "Tiêu chí:";
            // 
            // lblStudentClass
            // 
            lblStudentClass.AutoSize = true;
            lblStudentClass.Font = new Font("Segoe UI", 9F);
            lblStudentClass.ForeColor = Color.FromArgb(52, 73, 94);
            lblStudentClass.Location = new Point(50, 85);
            lblStudentClass.Name = "lblStudentClass";
            lblStudentClass.Size = new Size(29, 15);
            lblStudentClass.TabIndex = 5;
            lblStudentClass.Text = "N/A";
            // 
            // lblStudentId
            // 
            lblStudentId.AutoSize = true;
            lblStudentId.Font = new Font("Segoe UI", 9F);
            lblStudentId.ForeColor = Color.FromArgb(52, 73, 94);
            lblStudentId.Location = new Point(60, 60);
            lblStudentId.Name = "lblStudentId";
            lblStudentId.Size = new Size(29, 15);
            lblStudentId.TabIndex = 4;
            lblStudentId.Text = "N/A";
            // 
            // lblStudentName
            // 
            lblStudentName.AutoSize = true;
            lblStudentName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStudentName.ForeColor = Color.FromArgb(52, 73, 94);
            lblStudentName.Location = new Point(70, 35);
            lblStudentName.Name = "lblStudentName";
            lblStudentName.Size = new Size(36, 19);
            lblStudentName.TabIndex = 3;
            lblStudentName.Text = "N/A";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F);
            label11.ForeColor = Color.FromArgb(127, 140, 141);
            label11.Location = new Point(15, 85);
            label11.Name = "label11";
            label11.Size = new Size(30, 15);
            label11.TabIndex = 2;
            label11.Text = "Lớp:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F);
            label9.ForeColor = Color.FromArgb(127, 140, 141);
            label9.Location = new Point(15, 60);
            label9.Name = "label9";
            label9.Size = new Size(40, 15);
            label9.TabIndex = 1;
            label9.Text = "MSSV:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.ForeColor = Color.FromArgb(127, 140, 141);
            label7.Location = new Point(15, 35);
            label7.Name = "label7";
            label7.Size = new Size(46, 15);
            label7.TabIndex = 0;
            label7.Text = "Họ tên:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(52, 73, 94);
            label5.Location = new Point(20, 20);
            label5.Name = "label5";
            label5.Size = new Size(189, 21);
            label5.TabIndex = 0;
            label5.Text = "📋 Chi tiết minh chứng";
            // 
            // panelList
            // 
            panelList.BackColor = Color.White;
            panelList.Controls.Add(listViewEvidences);
            panelList.Controls.Add(label2);
            panelList.Dock = DockStyle.Fill;
            panelList.Location = new Point(20, 20);
            panelList.Name = "panelList";
            panelList.Padding = new Padding(20);
            panelList.Size = new Size(1360, 530);
            panelList.TabIndex = 0;
            // 
            // listViewEvidences
            // 
            listViewEvidences.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6, columnHeader7 });
            listViewEvidences.Dock = DockStyle.Fill;
            listViewEvidences.Font = new Font("Segoe UI", 9F);
            listViewEvidences.FullRowSelect = true;
            listViewEvidences.GridLines = true;
            listViewEvidences.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listViewEvidences.Location = new Point(20, 20);
            listViewEvidences.MultiSelect = false;
            listViewEvidences.Name = "listViewEvidences";
            listViewEvidences.Size = new Size(1320, 490);
            listViewEvidences.TabIndex = 1;
            listViewEvidences.UseCompatibleStateImageBehavior = false;
            listViewEvidences.View = View.Details;
            listViewEvidences.SelectedIndexChanged += listViewEvidences_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Tên minh chứng";
            columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Sinh viên";
            columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "MSSV";
            columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Lớp";
            columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Tiêu chí";
            columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Ngày nộp";
            columnHeader6.Width = 90;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Kích thước";
            columnHeader7.Width = 80;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(52, 73, 94);
            label2.Location = new Point(20, 20);
            label2.Name = "label2";
            label2.Size = new Size(213, 21);
            label2.TabIndex = 0;
            label2.Text = "📝 Danh sách minh chứng";
            // 
            // MinhChungApprovalForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 700);
            Controls.Add(panelMain);
            Controls.Add(panelStats);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1400, 700);
            Name = "MinhChungApprovalForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Duyệt minh chứng";
            Load += MinhChungApprovalForm_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelStats.ResumeLayout(false);
            panelStats.PerformLayout();
            panelMain.ResumeLayout(false);
            panelApproval.ResumeLayout(false);
            panelApproval.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panelList.ResumeLayout(false);
            panelList.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label label1;
        private Label lblApproverName;
        private Label lblApproverRole;
        private Button btnRefresh;
        private Button btnClose;
        private Panel panelStats;
        private Label label3;
        private Label lblTotalCount;
        private Label lblCriteriaBreakdown;
        private Panel panelMain;
        private Panel panelList;
        private Label label2;
        private ListView listViewEvidences;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private Panel panelApproval;
        private Label label5;
        private GroupBox groupBox1;
        private Label label7;
        private Label label9;
        private Label label11;
        private Label lblStudentName;
        private Label lblStudentId;
        private Label lblStudentClass;
        private Label lblCriteria;
        private Label label4;
        private Label lblEvidenceName;
        private Label label6;
        private Label lblSubmissionDate;
        private Label label8;
        private Label lblFileName;
        private Label label10;
        private Label lblFileSize;
        private Label label12;
        private TextBox txtDescription;
        private Label label14;
        private GroupBox groupBox2;
        private Label label15;
        private ComboBox cmbApprovalStatus;
        private Label lblFeedback;
        private TextBox txtFeedback;
        private Button btnApprove;
        private Button btnViewFile;
    }
}
