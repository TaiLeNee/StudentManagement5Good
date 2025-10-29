namespace StudentManagement5Good.Winform
{
    partial class UserManagementForm
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
            panelTop = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            panelMain = new Panel();
            groupBoxRoleUnit = new GroupBox();
            lblLop = new Label();
            cmbLop = new ComboBox();
            lblKhoa = new Label();
            cmbKhoa = new ComboBox();
            lblTruong = new Label();
            cmbTruong = new ComboBox();
            lblThanhPho = new Label();
            cmbThanhPho = new ComboBox();
            lblRole = new Label();
            comboBoxRole = new ComboBox();
            groupBoxBasicInfo = new GroupBox();
            checkBoxActive = new CheckBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblUserId = new Label();
            txtUserId = new TextBox();
            panelBottom = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            panelTop.SuspendLayout();
            panelMain.SuspendLayout();
            groupBoxRoleUnit.SuspendLayout();
            groupBoxBasicInfo.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(52, 152, 219);
            panelTop.Controls.Add(lblSubtitle);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(800, 90);
            panelTop.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(236, 240, 241);
            lblSubtitle.Location = new Point(28, 55);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(337, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Nhập thông tin chi tiết cho tài khoản người dùng mới";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(25, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(315, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "➕ Thêm Người dùng mới";
            // 
            // panelMain
            // 
            panelMain.AutoScroll = true;
            panelMain.BackColor = Color.FromArgb(245, 246, 250);
            panelMain.Controls.Add(groupBoxRoleUnit);
            panelMain.Controls.Add(groupBoxBasicInfo);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 90);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Size = new Size(800, 470);
            panelMain.TabIndex = 1;
            // 
            // groupBoxRoleUnit
            // 
            groupBoxRoleUnit.BackColor = Color.White;
            groupBoxRoleUnit.Controls.Add(lblLop);
            groupBoxRoleUnit.Controls.Add(cmbLop);
            groupBoxRoleUnit.Controls.Add(lblKhoa);
            groupBoxRoleUnit.Controls.Add(cmbKhoa);
            groupBoxRoleUnit.Controls.Add(lblTruong);
            groupBoxRoleUnit.Controls.Add(cmbTruong);
            groupBoxRoleUnit.Controls.Add(lblThanhPho);
            groupBoxRoleUnit.Controls.Add(cmbThanhPho);
            groupBoxRoleUnit.Controls.Add(lblRole);
            groupBoxRoleUnit.Controls.Add(comboBoxRole);
            groupBoxRoleUnit.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxRoleUnit.ForeColor = Color.FromArgb(52, 73, 94);
            groupBoxRoleUnit.Location = new Point(23, 260);
            groupBoxRoleUnit.Name = "groupBoxRoleUnit";
            groupBoxRoleUnit.Size = new Size(745, 300);
            groupBoxRoleUnit.TabIndex = 1;
            groupBoxRoleUnit.TabStop = false;
            groupBoxRoleUnit.Text = "👤 Vai trò & Đơn vị Quản lý";
            // 
            // lblLop
            // 
            lblLop.Font = new Font("Segoe UI", 10F);
            lblLop.ForeColor = Color.FromArgb(52, 73, 94);
            lblLop.Location = new Point(27, 65);
            lblLop.Name = "lblLop";
            lblLop.Size = new Size(65, 23);
            lblLop.TabIndex = 8;
            lblLop.Text = "Lớp: *";
            lblLop.TextAlign = ContentAlignment.MiddleLeft;
            lblLop.Visible = false;
            // 
            // cmbLop
            // 
            cmbLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLop.FlatStyle = FlatStyle.Flat;
            cmbLop.Font = new Font("Segoe UI", 10F);
            cmbLop.FormattingEnabled = true;
            cmbLop.Location = new Point(182, 63);
            cmbLop.Name = "cmbLop";
            cmbLop.Size = new Size(510, 25);
            cmbLop.TabIndex = 9;
            cmbLop.Visible = false;
            // 
            // lblKhoa
            // 
            lblKhoa.Font = new Font("Segoe UI", 10F);
            lblKhoa.ForeColor = Color.FromArgb(52, 73, 94);
            lblKhoa.Location = new Point(27, 65);
            lblKhoa.Name = "lblKhoa";
            lblKhoa.Size = new Size(65, 23);
            lblKhoa.TabIndex = 6;
            lblKhoa.Text = "Khoa: *";
            lblKhoa.TextAlign = ContentAlignment.MiddleLeft;
            lblKhoa.Visible = false;
            // 
            // cmbKhoa
            // 
            cmbKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKhoa.FlatStyle = FlatStyle.Flat;
            cmbKhoa.Font = new Font("Segoe UI", 10F);
            cmbKhoa.FormattingEnabled = true;
            cmbKhoa.Location = new Point(181, 63);
            cmbKhoa.Name = "cmbKhoa";
            cmbKhoa.Size = new Size(510, 25);
            cmbKhoa.TabIndex = 7;
            cmbKhoa.Visible = false;
            // 
            // lblTruong
            // 
            lblTruong.Font = new Font("Segoe UI", 10F);
            lblTruong.ForeColor = Color.FromArgb(52, 73, 94);
            lblTruong.Location = new Point(27, 65);
            lblTruong.Name = "lblTruong";
            lblTruong.Size = new Size(76, 23);
            lblTruong.TabIndex = 4;
            lblTruong.Text = "Trường: *";
            lblTruong.TextAlign = ContentAlignment.MiddleLeft;
            lblTruong.Visible = false;
            // 
            // cmbTruong
            // 
            cmbTruong.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTruong.FlatStyle = FlatStyle.Flat;
            cmbTruong.Font = new Font("Segoe UI", 10F);
            cmbTruong.FormattingEnabled = true;
            cmbTruong.Location = new Point(182, 63);
            cmbTruong.Name = "cmbTruong";
            cmbTruong.Size = new Size(510, 25);
            cmbTruong.TabIndex = 5;
            cmbTruong.Visible = false;
            // 
            // lblThanhPho
            // 
            lblThanhPho.Font = new Font("Segoe UI", 10F);
            lblThanhPho.ForeColor = Color.FromArgb(52, 73, 94);
            lblThanhPho.Location = new Point(27, 65);
            lblThanhPho.Name = "lblThanhPho";
            lblThanhPho.Size = new Size(87, 23);
            lblThanhPho.TabIndex = 2;
            lblThanhPho.Text = "Thành phố: *";
            lblThanhPho.TextAlign = ContentAlignment.MiddleLeft;
            lblThanhPho.Visible = false;
            // 
            // cmbThanhPho
            // 
            cmbThanhPho.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbThanhPho.FlatStyle = FlatStyle.Flat;
            cmbThanhPho.Font = new Font("Segoe UI", 10F);
            cmbThanhPho.FormattingEnabled = true;
            cmbThanhPho.Location = new Point(182, 63);
            cmbThanhPho.Name = "cmbThanhPho";
            cmbThanhPho.Size = new Size(510, 25);
            cmbThanhPho.TabIndex = 3;
            cmbThanhPho.Visible = false;
            // 
            // lblRole
            // 
            lblRole.Font = new Font("Segoe UI", 10F);
            lblRole.ForeColor = Color.FromArgb(52, 73, 94);
            lblRole.Location = new Point(25, 35);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(65, 23);
            lblRole.TabIndex = 0;
            lblRole.Text = "Vai trò: *";
            lblRole.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comboBoxRole
            // 
            comboBoxRole.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRole.FlatStyle = FlatStyle.Flat;
            comboBoxRole.Font = new Font("Segoe UI", 10F);
            comboBoxRole.FormattingEnabled = true;
            comboBoxRole.Location = new Point(182, 35);
            comboBoxRole.Name = "comboBoxRole";
            comboBoxRole.Size = new Size(510, 25);
            comboBoxRole.TabIndex = 1;
            // 
            // groupBoxBasicInfo
            // 
            groupBoxBasicInfo.BackColor = Color.White;
            groupBoxBasicInfo.Controls.Add(checkBoxActive);
            groupBoxBasicInfo.Controls.Add(lblPhone);
            groupBoxBasicInfo.Controls.Add(txtPhone);
            groupBoxBasicInfo.Controls.Add(lblEmail);
            groupBoxBasicInfo.Controls.Add(txtEmail);
            groupBoxBasicInfo.Controls.Add(lblHoTen);
            groupBoxBasicInfo.Controls.Add(txtHoTen);
            groupBoxBasicInfo.Controls.Add(lblPassword);
            groupBoxBasicInfo.Controls.Add(txtPassword);
            groupBoxBasicInfo.Controls.Add(lblUsername);
            groupBoxBasicInfo.Controls.Add(txtUsername);
            groupBoxBasicInfo.Controls.Add(lblUserId);
            groupBoxBasicInfo.Controls.Add(txtUserId);
            groupBoxBasicInfo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxBasicInfo.ForeColor = Color.FromArgb(52, 73, 94);
            groupBoxBasicInfo.Location = new Point(25, 20);
            groupBoxBasicInfo.Name = "groupBoxBasicInfo";
            groupBoxBasicInfo.Size = new Size(745, 233);
            groupBoxBasicInfo.TabIndex = 0;
            groupBoxBasicInfo.TabStop = false;
            groupBoxBasicInfo.Text = "Thông tin Cơ bản";
            // 
            // checkBoxActive
            // 
            checkBoxActive.AutoSize = true;
            checkBoxActive.Checked = true;
            checkBoxActive.CheckState = CheckState.Checked;
            checkBoxActive.Font = new Font("Segoe UI", 10F);
            checkBoxActive.ForeColor = Color.FromArgb(52, 73, 94);
            checkBoxActive.Location = new Point(180, 193);
            checkBoxActive.Name = "checkBoxActive";
            checkBoxActive.Size = new Size(146, 23);
            checkBoxActive.TabIndex = 12;
            checkBoxActive.Text = "Kích hoạt tài khoản";
            checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // lblPhone
            // 
            lblPhone.Font = new Font("Segoe UI", 10F);
            lblPhone.ForeColor = Color.FromArgb(52, 73, 94);
            lblPhone.Location = new Point(25, 163);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(150, 23);
            lblPhone.TabIndex = 10;
            lblPhone.Text = "Số điện thoại:";
            lblPhone.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new Font("Segoe UI", 10F);
            txtPhone.Location = new Point(180, 161);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(510, 25);
            txtPhone.TabIndex = 11;
            // 
            // lblEmail
            // 
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.ForeColor = Color.FromArgb(52, 73, 94);
            lblEmail.Location = new Point(25, 132);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(150, 23);
            lblEmail.TabIndex = 8;
            lblEmail.Text = "Email: *";
            lblEmail.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(180, 130);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(510, 25);
            txtEmail.TabIndex = 9;
            // 
            // lblHoTen
            // 
            lblHoTen.Font = new Font("Segoe UI", 10F);
            lblHoTen.ForeColor = Color.FromArgb(52, 73, 94);
            lblHoTen.Location = new Point(25, 101);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(150, 23);
            lblHoTen.TabIndex = 6;
            lblHoTen.Text = "Họ và Tên: *";
            lblHoTen.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtHoTen
            // 
            txtHoTen.BorderStyle = BorderStyle.FixedSingle;
            txtHoTen.Font = new Font("Segoe UI", 10F);
            txtHoTen.Location = new Point(180, 99);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(510, 25);
            txtHoTen.TabIndex = 7;
            // 
            // lblPassword
            // 
            lblPassword.Font = new Font("Segoe UI", 10F);
            lblPassword.ForeColor = Color.FromArgb(52, 73, 94);
            lblPassword.Location = new Point(24, 66);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(150, 23);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Mật khẩu: *";
            lblPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(180, 64);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(510, 25);
            txtPassword.TabIndex = 5;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblUsername
            // 
            lblUsername.Font = new Font("Segoe UI", 10F);
            lblUsername.ForeColor = Color.FromArgb(52, 73, 94);
            lblUsername.Location = new Point(25, 35);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(150, 23);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Username: *";
            lblUsername.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(180, 33);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(510, 25);
            txtUsername.TabIndex = 3;
            // 
            // lblUserId
            // 
            lblUserId.Font = new Font("Segoe UI", 10F);
            lblUserId.ForeColor = Color.FromArgb(52, 73, 94);
            lblUserId.Location = new Point(25, 35);
            lblUserId.Name = "lblUserId";
            lblUserId.Size = new Size(150, 23);
            lblUserId.TabIndex = 0;
            lblUserId.Text = "User ID:";
            lblUserId.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtUserId
            // 
            txtUserId.BackColor = Color.FromArgb(236, 240, 241);
            txtUserId.BorderStyle = BorderStyle.FixedSingle;
            txtUserId.Font = new Font("Segoe UI", 10F);
            txtUserId.Location = new Point(180, 33);
            txtUserId.Name = "txtUserId";
            txtUserId.ReadOnly = true;
            txtUserId.Size = new Size(510, 25);
            txtUserId.TabIndex = 1;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(236, 240, 241);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 560);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(800, 90);
            panelBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(635, 20);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(150, 50);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "✕ Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(470, 20);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 50);
            btnSave.TabIndex = 0;
            btnSave.Text = "✓ Thêm mới";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // UserManagementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            ClientSize = new Size(800, 650);
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "UserManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý Người dùng";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelMain.ResumeLayout(false);
            groupBoxRoleUnit.ResumeLayout(false);
            groupBoxBasicInfo.ResumeLayout(false);
            groupBoxBasicInfo.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelMain;
        private GroupBox groupBoxBasicInfo;
        private GroupBox groupBoxRoleUnit;
        private Label lblUserId;
        private TextBox txtUserId;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblRole;
        private ComboBox comboBoxRole;
        private Label lblThanhPho;
        private ComboBox cmbThanhPho;
        private Label lblTruong;
        private ComboBox cmbTruong;
        private Label lblKhoa;
        private ComboBox cmbKhoa;
        private Label lblLop;
        private ComboBox cmbLop;
        private CheckBox checkBoxActive;
        private Panel panelBottom;
        private Button btnSave;
        private Button btnCancel;
    }
}
