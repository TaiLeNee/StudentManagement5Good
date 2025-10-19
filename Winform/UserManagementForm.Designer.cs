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
            lblClass = new Label();
            comboBoxClass = new ComboBox();
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
                checkBoxActive, comboBoxClass, lblClass, comboBoxRole, lblRole,
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
            lblClass.Text = "Lớp:";
            lblClass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblClass.Location = new Point(30, yPos);
            comboBoxClass.Location = new Point(200, yPos - 3);
            comboBoxClass.Size = new Size(450, 30);
            comboBoxClass.DropDownStyle = ComboBoxStyle.DropDownList;
            
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
        private Label lblClass;
        private ComboBox comboBoxClass;
        private CheckBox checkBoxActive;
        private Panel panelBottom;
        private Button btnSave;
        private Button btnCancel;
    }
}

