namespace StudentManagement5Good.Winform
{
    partial class StudentDashboard
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
            btnLogout = new Button();
            btnProfile = new Button();
            btnRefresh = new Button();
            lblCurrentYear = new Label();
            lblOverallStatus = new Label();
            panelStudentInfo = new Panel();
            pictureBoxAvatar = new PictureBox();
            lblSchoolInfo = new Label();
            lblClassInfo = new Label();
            lblStudentId = new Label();
            lblStudentName = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            panelMain = new Panel();
            panelMinhChung = new Panel();
            listViewMinhChung = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            panelMinhChungHeader = new Panel();
            lblMinhChungCount = new Label();
            btnNopMinhChung = new Button();
            label11 = new Label();
            panelCriteria = new Panel();
            panelHoiNhap = new Panel();
            btnHoiNhapAction = new Button();
            lblHoiNhapStatus = new Label();
            label10 = new Label();
            panelTinhNguyen = new Panel();
            btnTinhNguyenAction = new Button();
            lblTinhNguyenStatus = new Label();
            label8 = new Label();
            panelTheLuc = new Panel();
            btnTheLucAction = new Button();
            lblTheLucStatus = new Label();
            label6 = new Label();
            panelHocTap = new Panel();
            btnHocTapAction = new Button();
            lblHocTapStatus = new Label();
            lblHocTap = new Label();
            panelDaoDuc = new Panel();
            btnDaoDucAction = new Button();
            lblDaoDucStatus = new Label();
            lblDaoDuc = new Label();
            label5 = new Label();
            panelHeader.SuspendLayout();
            panelStudentInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAvatar).BeginInit();
            panelMain.SuspendLayout();
            panelMinhChung.SuspendLayout();
            panelMinhChungHeader.SuspendLayout();
            panelCriteria.SuspendLayout();
            panelHoiNhap.SuspendLayout();
            panelTinhNguyen.SuspendLayout();
            panelTheLuc.SuspendLayout();
            panelHocTap.SuspendLayout();
            panelDaoDuc.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(41, 128, 185);
            panelHeader.Controls.Add(btnLogout);
            panelHeader.Controls.Add(btnProfile);
            panelHeader.Controls.Add(btnRefresh);
            panelHeader.Controls.Add(lblCurrentYear);
            panelHeader.Controls.Add(lblOverallStatus);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1200, 80);
            panelHeader.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.BackColor = Color.FromArgb(231, 76, 60);
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(1105, 25);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(75, 30);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnProfile
            // 
            btnProfile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnProfile.BackColor = Color.FromArgb(46, 204, 113);
            btnProfile.FlatAppearance.BorderSize = 0;
            btnProfile.FlatStyle = FlatStyle.Flat;
            btnProfile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnProfile.ForeColor = Color.White;
            btnProfile.Location = new Point(1030, 25);
            btnProfile.Name = "btnProfile";
            btnProfile.Size = new Size(70, 30);
            btnProfile.TabIndex = 3;
            btnProfile.Text = "Hồ sơ";
            btnProfile.UseVisualStyleBackColor = false;
            btnProfile.Click += btnProfile_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.FromArgb(52, 152, 219);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(950, 25);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 30);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblCurrentYear
            // 
            lblCurrentYear.AutoSize = true;
            lblCurrentYear.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCurrentYear.ForeColor = Color.White;
            lblCurrentYear.Location = new Point(20, 15);
            lblCurrentYear.Name = "lblCurrentYear";
            lblCurrentYear.Size = new Size(161, 21);
            lblCurrentYear.TabIndex = 0;
            lblCurrentYear.Text = "Năm học 2024-2025";
            // 
            // lblOverallStatus
            // 
            lblOverallStatus.AutoSize = true;
            lblOverallStatus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblOverallStatus.ForeColor = Color.White;
            lblOverallStatus.Location = new Point(20, 45);
            lblOverallStatus.Name = "lblOverallStatus";
            lblOverallStatus.Size = new Size(213, 25);
            lblOverallStatus.TabIndex = 1;
            lblOverallStatus.Text = "Chưa bắt đầu đánh giá";
            // 
            // panelStudentInfo
            // 
            panelStudentInfo.BackColor = Color.White;
            panelStudentInfo.Controls.Add(pictureBoxAvatar);
            panelStudentInfo.Controls.Add(lblSchoolInfo);
            panelStudentInfo.Controls.Add(lblClassInfo);
            panelStudentInfo.Controls.Add(lblStudentId);
            panelStudentInfo.Controls.Add(lblStudentName);
            panelStudentInfo.Controls.Add(label1);
            panelStudentInfo.Controls.Add(label2);
            panelStudentInfo.Controls.Add(label3);
            panelStudentInfo.Controls.Add(label4);
            panelStudentInfo.Dock = DockStyle.Top;
            panelStudentInfo.Location = new Point(0, 80);
            panelStudentInfo.Name = "panelStudentInfo";
            panelStudentInfo.Padding = new Padding(20);
            panelStudentInfo.Size = new Size(1200, 120);
            panelStudentInfo.TabIndex = 1;
            // 
            // pictureBoxAvatar
            // 
            pictureBoxAvatar.BackColor = Color.FromArgb(189, 195, 199);
            pictureBoxAvatar.Location = new Point(20, 20);
            pictureBoxAvatar.Name = "pictureBoxAvatar";
            pictureBoxAvatar.Size = new Size(80, 80);
            pictureBoxAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxAvatar.TabIndex = 0;
            pictureBoxAvatar.TabStop = false;
            // 
            // lblSchoolInfo
            // 
            lblSchoolInfo.AutoSize = true;
            lblSchoolInfo.Font = new Font("Segoe UI", 10F);
            lblSchoolInfo.ForeColor = Color.FromArgb(52, 73, 94);
            lblSchoolInfo.Location = new Point(196, 96);
            lblSchoolInfo.Name = "lblSchoolInfo";
            lblSchoolInfo.Size = new Size(205, 19);
            lblSchoolInfo.TabIndex = 7;
            lblSchoolInfo.Text = "Đại học Sư phạm Kỹ thuật HCM";
            // 
            // lblClassInfo
            // 
            lblClassInfo.AutoSize = true;
            lblClassInfo.Font = new Font("Segoe UI", 10F);
            lblClassInfo.ForeColor = Color.FromArgb(52, 73, 94);
            lblClassInfo.Location = new Point(196, 71);
            lblClassInfo.Name = "lblClassInfo";
            lblClassInfo.Size = new Size(201, 19);
            lblClassInfo.TabIndex = 5;
            lblClassInfo.Text = "CNTT01 - Công nghệ thông tin";
            // 
            // lblStudentId
            // 
            lblStudentId.AutoSize = true;
            lblStudentId.Font = new Font("Segoe UI", 10F);
            lblStudentId.ForeColor = Color.FromArgb(52, 73, 94);
            lblStudentId.Location = new Point(196, 46);
            lblStudentId.Name = "lblStudentId";
            lblStudentId.Size = new Size(73, 19);
            lblStudentId.TabIndex = 3;
            lblStudentId.Text = "21110001";
            // 
            // lblStudentName
            // 
            lblStudentName.AutoSize = true;
            lblStudentName.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblStudentName.ForeColor = Color.FromArgb(44, 62, 80);
            lblStudentName.Location = new Point(196, 10);
            lblStudentName.Name = "lblStudentName";
            lblStudentName.Size = new Size(160, 30);
            lblStudentName.TabIndex = 1;
            lblStudentName.Text = "Nguyễn Văn A";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(127, 140, 141);
            label1.Location = new Point(120, 50);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 2;
            label1.Text = "Mã số SV:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(127, 140, 141);
            label2.Location = new Point(120, 75);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 4;
            label2.Text = "Lớp - Khoa:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(127, 140, 141);
            label3.Location = new Point(120, 100);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 6;
            label3.Text = "Trường:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(127, 140, 141);
            label4.Location = new Point(120, 25);
            label4.Name = "label4";
            label4.Size = new Size(48, 15);
            label4.TabIndex = 8;
            label4.Text = "Họ tên:";
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(236, 240, 241);
            panelMain.Controls.Add(panelMinhChung);
            panelMain.Controls.Add(panelCriteria);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 200);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Size = new Size(1200, 500);
            panelMain.TabIndex = 2;
            // 
            // panelMinhChung
            // 
            panelMinhChung.BackColor = Color.White;
            panelMinhChung.Controls.Add(listViewMinhChung);
            panelMinhChung.Controls.Add(panelMinhChungHeader);
            panelMinhChung.Dock = DockStyle.Fill;
            panelMinhChung.Location = new Point(20, 220);
            panelMinhChung.Name = "panelMinhChung";
            panelMinhChung.Size = new Size(1160, 260);
            panelMinhChung.TabIndex = 1;
            // 
            // listViewMinhChung
            // 
            listViewMinhChung.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5 });
            listViewMinhChung.Dock = DockStyle.Fill;
            listViewMinhChung.Font = new Font("Segoe UI", 9F);
            listViewMinhChung.FullRowSelect = true;
            listViewMinhChung.GridLines = true;
            listViewMinhChung.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listViewMinhChung.Location = new Point(0, 60);
            listViewMinhChung.Name = "listViewMinhChung";
            listViewMinhChung.Size = new Size(1160, 200);
            listViewMinhChung.TabIndex = 1;
            listViewMinhChung.UseCompatibleStateImageBehavior = false;
            listViewMinhChung.View = View.Details;
            listViewMinhChung.DoubleClick += listViewMinhChung_DoubleClick;
            listViewMinhChung.KeyDown += listViewMinhChung_KeyDown;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Tên minh chứng";
            columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Tiêu chí";
            columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Ngày nộp";
            columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Trạng thái";
            columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Phản hồi";
            columnHeader5.Width = 400;
            // 
            // panelMinhChungHeader
            // 
            panelMinhChungHeader.BackColor = Color.FromArgb(52, 152, 219);
            panelMinhChungHeader.Controls.Add(lblMinhChungCount);
            panelMinhChungHeader.Controls.Add(btnNopMinhChung);
            panelMinhChungHeader.Controls.Add(label11);
            panelMinhChungHeader.Dock = DockStyle.Top;
            panelMinhChungHeader.Location = new Point(0, 0);
            panelMinhChungHeader.Name = "panelMinhChungHeader";
            panelMinhChungHeader.Size = new Size(1160, 60);
            panelMinhChungHeader.TabIndex = 0;
            // 
            // lblMinhChungCount
            // 
            lblMinhChungCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMinhChungCount.AutoSize = true;
            lblMinhChungCount.Font = new Font("Segoe UI", 10F);
            lblMinhChungCount.ForeColor = Color.White;
            lblMinhChungCount.Location = new Point(850, 35);
            lblMinhChungCount.Name = "lblMinhChungCount";
            lblMinhChungCount.Size = new Size(150, 19);
            lblMinhChungCount.TabIndex = 2;
            lblMinhChungCount.Text = "Tổng số minh chứng: 0";
            // 
            // btnNopMinhChung
            // 
            btnNopMinhChung.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNopMinhChung.BackColor = Color.FromArgb(46, 204, 113);
            btnNopMinhChung.FlatAppearance.BorderSize = 0;
            btnNopMinhChung.FlatStyle = FlatStyle.Flat;
            btnNopMinhChung.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNopMinhChung.ForeColor = Color.White;
            btnNopMinhChung.Location = new Point(1000, 15);
            btnNopMinhChung.Name = "btnNopMinhChung";
            btnNopMinhChung.Size = new Size(140, 35);
            btnNopMinhChung.TabIndex = 1;
            btnNopMinhChung.Text = "📁 Nộp minh chứng";
            btnNopMinhChung.UseVisualStyleBackColor = false;
            btnNopMinhChung.Click += btnNopMinhChung_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label11.ForeColor = Color.White;
            label11.Location = new Point(15, 20);
            label11.Name = "label11";
            label11.Size = new Size(220, 25);
            label11.TabIndex = 0;
            label11.Text = "📋 Quản lý Minh chứng";
            // 
            // panelCriteria
            // 
            panelCriteria.Controls.Add(panelHoiNhap);
            panelCriteria.Controls.Add(panelTinhNguyen);
            panelCriteria.Controls.Add(panelTheLuc);
            panelCriteria.Controls.Add(panelHocTap);
            panelCriteria.Controls.Add(panelDaoDuc);
            panelCriteria.Controls.Add(label5);
            panelCriteria.Dock = DockStyle.Top;
            panelCriteria.Location = new Point(20, 20);
            panelCriteria.Name = "panelCriteria";
            panelCriteria.Size = new Size(1160, 200);
            panelCriteria.TabIndex = 0;
            // 
            // panelHoiNhap
            // 
            panelHoiNhap.BackColor = Color.FromArgb(155, 89, 182);
            panelHoiNhap.Controls.Add(btnHoiNhapAction);
            panelHoiNhap.Controls.Add(lblHoiNhapStatus);
            panelHoiNhap.Controls.Add(label10);
            panelHoiNhap.Location = new Point(928, 50);
            panelHoiNhap.Name = "panelHoiNhap";
            panelHoiNhap.Size = new Size(220, 140);
            panelHoiNhap.TabIndex = 5;
            // 
            // btnHoiNhapAction
            // 
            btnHoiNhapAction.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnHoiNhapAction.BackColor = Color.FromArgb(142, 68, 173);
            btnHoiNhapAction.FlatAppearance.BorderSize = 0;
            btnHoiNhapAction.FlatStyle = FlatStyle.Flat;
            btnHoiNhapAction.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHoiNhapAction.ForeColor = Color.White;
            btnHoiNhapAction.Location = new Point(15, 100);
            btnHoiNhapAction.Name = "btnHoiNhapAction";
            btnHoiNhapAction.Size = new Size(190, 30);
            btnHoiNhapAction.TabIndex = 2;
            btnHoiNhapAction.Text = "Nộp minh chứng";
            btnHoiNhapAction.UseVisualStyleBackColor = false;
            btnHoiNhapAction.Click += btnHoiNhapAction_Click;
            // 
            // lblHoiNhapStatus
            // 
            lblHoiNhapStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblHoiNhapStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHoiNhapStatus.ForeColor = Color.White;
            lblHoiNhapStatus.Location = new Point(15, 50);
            lblHoiNhapStatus.Name = "lblHoiNhapStatus";
            lblHoiNhapStatus.Size = new Size(190, 40);
            lblHoiNhapStatus.TabIndex = 1;
            lblHoiNhapStatus.Text = "✗ Chưa đạt";
            lblHoiNhapStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label10.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label10.ForeColor = Color.White;
            label10.Location = new Point(15, 15);
            label10.Name = "label10";
            label10.Size = new Size(190, 25);
            label10.TabIndex = 0;
            label10.Text = "🌍 Hội nhập tốt";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelTinhNguyen
            // 
            panelTinhNguyen.BackColor = Color.FromArgb(230, 126, 34);
            panelTinhNguyen.Controls.Add(btnTinhNguyenAction);
            panelTinhNguyen.Controls.Add(lblTinhNguyenStatus);
            panelTinhNguyen.Controls.Add(label8);
            panelTinhNguyen.Location = new Point(696, 50);
            panelTinhNguyen.Name = "panelTinhNguyen";
            panelTinhNguyen.Size = new Size(220, 140);
            panelTinhNguyen.TabIndex = 4;
            // 
            // btnTinhNguyenAction
            // 
            btnTinhNguyenAction.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnTinhNguyenAction.BackColor = Color.FromArgb(211, 84, 0);
            btnTinhNguyenAction.FlatAppearance.BorderSize = 0;
            btnTinhNguyenAction.FlatStyle = FlatStyle.Flat;
            btnTinhNguyenAction.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTinhNguyenAction.ForeColor = Color.White;
            btnTinhNguyenAction.Location = new Point(15, 100);
            btnTinhNguyenAction.Name = "btnTinhNguyenAction";
            btnTinhNguyenAction.Size = new Size(190, 30);
            btnTinhNguyenAction.TabIndex = 2;
            btnTinhNguyenAction.Text = "Nộp minh chứng";
            btnTinhNguyenAction.UseVisualStyleBackColor = false;
            btnTinhNguyenAction.Click += btnTinhNguyenAction_Click;
            // 
            // lblTinhNguyenStatus
            // 
            lblTinhNguyenStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTinhNguyenStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTinhNguyenStatus.ForeColor = Color.White;
            lblTinhNguyenStatus.Location = new Point(15, 50);
            lblTinhNguyenStatus.Name = "lblTinhNguyenStatus";
            lblTinhNguyenStatus.Size = new Size(190, 40);
            lblTinhNguyenStatus.TabIndex = 1;
            lblTinhNguyenStatus.Text = "✗ Chưa đạt";
            lblTinhNguyenStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label8.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label8.ForeColor = Color.White;
            label8.Location = new Point(15, 15);
            label8.Name = "label8";
            label8.Size = new Size(190, 25);
            label8.TabIndex = 0;
            label8.Text = "\U0001f91d Tình nguyện tốt";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelTheLuc
            // 
            panelTheLuc.BackColor = Color.FromArgb(241, 196, 15);
            panelTheLuc.Controls.Add(btnTheLucAction);
            panelTheLuc.Controls.Add(lblTheLucStatus);
            panelTheLuc.Controls.Add(label6);
            panelTheLuc.Location = new Point(464, 50);
            panelTheLuc.Name = "panelTheLuc";
            panelTheLuc.Size = new Size(220, 140);
            panelTheLuc.TabIndex = 3;
            // 
            // btnTheLucAction
            // 
            btnTheLucAction.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnTheLucAction.BackColor = Color.FromArgb(212, 172, 13);
            btnTheLucAction.FlatAppearance.BorderSize = 0;
            btnTheLucAction.FlatStyle = FlatStyle.Flat;
            btnTheLucAction.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTheLucAction.ForeColor = Color.White;
            btnTheLucAction.Location = new Point(15, 100);
            btnTheLucAction.Name = "btnTheLucAction";
            btnTheLucAction.Size = new Size(190, 30);
            btnTheLucAction.TabIndex = 2;
            btnTheLucAction.Text = "Nộp minh chứng";
            btnTheLucAction.UseVisualStyleBackColor = false;
            btnTheLucAction.Click += btnTheLucAction_Click;
            // 
            // lblTheLucStatus
            // 
            lblTheLucStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTheLucStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTheLucStatus.ForeColor = Color.White;
            lblTheLucStatus.Location = new Point(15, 50);
            lblTheLucStatus.Name = "lblTheLucStatus";
            lblTheLucStatus.Size = new Size(190, 40);
            lblTheLucStatus.TabIndex = 1;
            lblTheLucStatus.Text = "✗ Chưa đạt";
            lblTheLucStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label6.ForeColor = Color.White;
            label6.Location = new Point(15, 15);
            label6.Name = "label6";
            label6.Size = new Size(190, 25);
            label6.TabIndex = 0;
            label6.Text = "💪 Thể lực tốt";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelHocTap
            // 
            panelHocTap.BackColor = Color.FromArgb(46, 204, 113);
            panelHocTap.Controls.Add(btnHocTapAction);
            panelHocTap.Controls.Add(lblHocTapStatus);
            panelHocTap.Controls.Add(lblHocTap);
            panelHocTap.Location = new Point(232, 50);
            panelHocTap.Name = "panelHocTap";
            panelHocTap.Size = new Size(220, 140);
            panelHocTap.TabIndex = 2;
            // 
            // btnHocTapAction
            // 
            btnHocTapAction.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnHocTapAction.BackColor = Color.FromArgb(39, 174, 96);
            btnHocTapAction.FlatAppearance.BorderSize = 0;
            btnHocTapAction.FlatStyle = FlatStyle.Flat;
            btnHocTapAction.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHocTapAction.ForeColor = Color.White;
            btnHocTapAction.Location = new Point(15, 100);
            btnHocTapAction.Name = "btnHocTapAction";
            btnHocTapAction.Size = new Size(190, 30);
            btnHocTapAction.TabIndex = 2;
            btnHocTapAction.Text = "Nộp minh chứng";
            btnHocTapAction.UseVisualStyleBackColor = false;
            btnHocTapAction.Click += btnHocTapAction_Click;
            // 
            // lblHocTapStatus
            // 
            lblHocTapStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblHocTapStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHocTapStatus.ForeColor = Color.White;
            lblHocTapStatus.Location = new Point(15, 50);
            lblHocTapStatus.Name = "lblHocTapStatus";
            lblHocTapStatus.Size = new Size(190, 40);
            lblHocTapStatus.TabIndex = 1;
            lblHocTapStatus.Text = "✗ Chưa đạt";
            lblHocTapStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHocTap
            // 
            lblHocTap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblHocTap.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHocTap.ForeColor = Color.White;
            lblHocTap.Location = new Point(15, 15);
            lblHocTap.Name = "lblHocTap";
            lblHocTap.Size = new Size(190, 25);
            lblHocTap.TabIndex = 0;
            lblHocTap.Text = "📚 Học tập tốt";
            lblHocTap.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelDaoDuc
            // 
            panelDaoDuc.BackColor = Color.FromArgb(52, 152, 219);
            panelDaoDuc.Controls.Add(btnDaoDucAction);
            panelDaoDuc.Controls.Add(lblDaoDucStatus);
            panelDaoDuc.Controls.Add(lblDaoDuc);
            panelDaoDuc.Location = new Point(0, 50);
            panelDaoDuc.Name = "panelDaoDuc";
            panelDaoDuc.Size = new Size(220, 140);
            panelDaoDuc.TabIndex = 1;
            // 
            // btnDaoDucAction
            // 
            btnDaoDucAction.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDaoDucAction.BackColor = Color.FromArgb(41, 128, 185);
            btnDaoDucAction.FlatAppearance.BorderSize = 0;
            btnDaoDucAction.FlatStyle = FlatStyle.Flat;
            btnDaoDucAction.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDaoDucAction.ForeColor = Color.White;
            btnDaoDucAction.Location = new Point(15, 100);
            btnDaoDucAction.Name = "btnDaoDucAction";
            btnDaoDucAction.Size = new Size(190, 30);
            btnDaoDucAction.TabIndex = 2;
            btnDaoDucAction.Text = "Nộp minh chứng";
            btnDaoDucAction.UseVisualStyleBackColor = false;
            btnDaoDucAction.Click += btnDaoDucAction_Click;
            // 
            // lblDaoDucStatus
            // 
            lblDaoDucStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDaoDucStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDaoDucStatus.ForeColor = Color.White;
            lblDaoDucStatus.Location = new Point(15, 50);
            lblDaoDucStatus.Name = "lblDaoDucStatus";
            lblDaoDucStatus.Size = new Size(190, 40);
            lblDaoDucStatus.TabIndex = 1;
            lblDaoDucStatus.Text = "✗ Chưa đạt";
            lblDaoDucStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDaoDuc
            // 
            lblDaoDuc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDaoDuc.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblDaoDuc.ForeColor = Color.White;
            lblDaoDuc.Location = new Point(15, 15);
            lblDaoDuc.Name = "lblDaoDuc";
            lblDaoDuc.Size = new Size(190, 25);
            lblDaoDuc.TabIndex = 0;
            lblDaoDuc.Text = "🎭 Đạo đức tốt";
            lblDaoDuc.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(44, 62, 80);
            label5.Location = new Point(0, 10);
            label5.Name = "label5";
            label5.Size = new Size(298, 30);
            label5.TabIndex = 0;
            label5.Text = "🏆 Bảng điều khiển \"5 Tốt\"";
            // 
            // StudentDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Controls.Add(panelMain);
            Controls.Add(panelStudentInfo);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1200, 700);
            Name = "StudentDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard Sinh viên 5 Tốt";
            WindowState = FormWindowState.Normal;
            Load += StudentDashboard_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelStudentInfo.ResumeLayout(false);
            panelStudentInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAvatar).EndInit();
            panelMain.ResumeLayout(false);
            panelMinhChung.ResumeLayout(false);
            panelMinhChungHeader.ResumeLayout(false);
            panelMinhChungHeader.PerformLayout();
            panelCriteria.ResumeLayout(false);
            panelCriteria.PerformLayout();
            panelHoiNhap.ResumeLayout(false);
            panelTinhNguyen.ResumeLayout(false);
            panelTheLuc.ResumeLayout(false);
            panelHocTap.ResumeLayout(false);
            panelDaoDuc.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblCurrentYear;
        private System.Windows.Forms.Label lblOverallStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelStudentInfo;
        private System.Windows.Forms.PictureBox pictureBoxAvatar;
        private System.Windows.Forms.Label lblStudentName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStudentId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblClassInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSchoolInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelCriteria;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelDaoDuc;
        private System.Windows.Forms.Label lblDaoDuc;
        private System.Windows.Forms.Label lblDaoDucStatus;
        private System.Windows.Forms.Button btnDaoDucAction;
        private System.Windows.Forms.Panel panelHocTap;
        private System.Windows.Forms.Button btnHocTapAction;
        private System.Windows.Forms.Label lblHocTapStatus;
        private System.Windows.Forms.Label lblHocTap;
        private System.Windows.Forms.Panel panelTheLuc;
        private System.Windows.Forms.Button btnTheLucAction;
        private System.Windows.Forms.Label lblTheLucStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelTinhNguyen;
        private System.Windows.Forms.Button btnTinhNguyenAction;
        private System.Windows.Forms.Label lblTinhNguyenStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panelHoiNhap;
        private System.Windows.Forms.Button btnHoiNhapAction;
        private System.Windows.Forms.Label lblHoiNhapStatus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panelMinhChung;
        private System.Windows.Forms.Panel panelMinhChungHeader;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnNopMinhChung;
        private System.Windows.Forms.Label lblMinhChungCount;
        private System.Windows.Forms.ListView listViewMinhChung;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}
