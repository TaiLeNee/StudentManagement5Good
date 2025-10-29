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
            lblTitle = new Label();
            
            panelMain = new Panel();
            lblUserId = new Label();
            txtUserId = new TextBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblRole = new Label();
            comboBoxRole = new ComboBox();
            lblThanhPho = new Label();
            cmbThanhPho = new ComboBox();
            lblTruong = new Label();
            cmbTruong = new ComboBox();
            lblKhoa = new Label();
            cmbKhoa = new ComboBox();
            lblLop = new Label();
            cmbLop = new ComboBox();
            checkBoxActive = new CheckBox();
            
            panelBottom = new Panel();
            btnSave = new Button();
            btnCancel = new Button();
            
            panelTop.SuspendLayout();
            panelMain.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            
            // panelTop
            panelTop.BackColor = Color.FromArgb(41, 128, 185);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Size = new Size(684, 70);
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "Người dùng";
            
            // panelMain
            panelMain.BackColor = Color.White;
            panelMain.Controls.AddRange(new Control[] {
                checkBoxActive, cmbLop, lblLop, cmbKhoa, lblKhoa, cmbTruong, lblTruong,
                cmbThanhPho, lblThanhPho, comboBoxRole, lblRole,
                txtPhone, lblPhone, txtEmail, lblEmail, txtHoTen, lblHoTen,
                txtPassword, lblPassword, txtUsername, lblUsername, txtUserId, lblUserId
            });
            panelMain.Dock = DockStyle.Fill;
            panelMain.Padding = new Padding(30, 20, 30, 20);
            
            // Labels and TextBoxes
            int yPos = 30;
            int yStep = 50;
            
            lblUserId.Text = "User ID:";
            lblUserId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserId.Location = new Point(30, yPos);
            txtUserId.Location = new Point(200, yPos - 3);
            txtUserId.Size = new Size(450, 30);
            
            yPos += yStep;
            lblUsername.Text = "Username:";
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.Location = new Point(30, yPos);
            txtUsername.Location = new Point(200, yPos - 3);
            txtUsername.Size = new Size(450, 30);
            
            yPos += yStep;
            lblPassword.Text = "Mật khẩu:";
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.Location = new Point(30, yPos);
            txtPassword.Location = new Point(200, yPos - 3);
            txtPassword.Size = new Size(450, 30);
            txtPassword.UseSystemPasswordChar = true;
            
            yPos += yStep;
            lblHoTen.Text = "Họ và Tên:";
            lblHoTen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHoTen.Location = new Point(30, yPos);
            txtHoTen.Location = new Point(200, yPos - 3);
            txtHoTen.Size = new Size(450, 30);
            
            yPos += yStep;
            lblEmail.Text = "Email:";
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.Location = new Point(30, yPos);
            txtEmail.Location = new Point(200, yPos - 3);
            txtEmail.Size = new Size(450, 30);
            
            yPos += yStep;
            lblPhone.Text = "Số điện thoại:";
            lblPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPhone.Location = new Point(30, yPos);
            txtPhone.Location = new Point(200, yPos - 3);
            txtPhone.Size = new Size(450, 30);
            
            yPos += yStep;
            lblRole.Text = "Vai trò:";
            lblRole.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRole.Location = new Point(30, yPos);
            comboBoxRole.Location = new Point(200, yPos - 3);
            comboBoxRole.Size = new Size(450, 30);
            comboBoxRole.DropDownStyle = ComboBoxStyle.DropDownList;
            
            yPos += yStep;
            lblThanhPho.Text = "Thành phố:";
            lblThanhPho.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblThanhPho.Location = new Point(30, yPos);
            lblThanhPho.Visible = false;
            cmbThanhPho.Location = new Point(200, yPos - 3);
            cmbThanhPho.Size = new Size(450, 30);
            cmbThanhPho.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbThanhPho.Visible = false;
            
            yPos += yStep;
            lblTruong.Text = "Trường:";
            lblTruong.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTruong.Location = new Point(30, yPos);
            lblTruong.Visible = false;
            cmbTruong.Location = new Point(200, yPos - 3);
            cmbTruong.Size = new Size(450, 30);
            cmbTruong.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTruong.Visible = false;
            
            yPos += yStep;
            lblKhoa.Text = "Khoa:";
            lblKhoa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKhoa.Location = new Point(30, yPos);
            lblKhoa.Visible = false;
            cmbKhoa.Location = new Point(200, yPos - 3);
            cmbKhoa.Size = new Size(450, 30);
            cmbKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKhoa.Visible = false;
            
            yPos += yStep;
            lblLop.Text = "Lớp:";
            lblLop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLop.Location = new Point(30, yPos);
            lblLop.Visible = false;
            cmbLop.Location = new Point(200, yPos - 3);
            cmbLop.Size = new Size(450, 30);
            cmbLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLop.Visible = false;
            
            yPos += yStep;
            checkBoxActive.Text = "Kích hoạt tài khoản";
            checkBoxActive.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            checkBoxActive.Location = new Point(200, yPos);
            checkBoxActive.Checked = true;
            
            // panelBottom
            panelBottom.BackColor = Color.FromArgb(236, 240, 241);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Size = new Size(684, 80);
            
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(380, 15);
            btnSave.Size = new Size(140, 50);
            btnSave.Text = "💾 Lưu";
            btnSave.Click += btnSave_Click;
            
            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 11F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(530, 15);
            btnCancel.Size = new Size(140, 50);
            btnCancel.Text = "❌ Hủy";
            btnCancel.Click += btnCancel_Click;
            
            // UserManagementForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(684, 650);
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Font = new Font("Segoe UI", 9F);
            Name = "UserManagementForm";
            Text = "Quản lý Người dùng";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblTitle;
        private Panel panelMain;
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

