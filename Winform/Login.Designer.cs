using StudentManagement5GoodTempp.Winform.PanelDesign;

namespace StudentManagement5Good
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelLeft = new Panel();
            lblSlogan = new Label();
            lblSystemName = new Label();
            pictureBoxLogo = new PictureBox();
            panelRight = new Panel();
            lblFooter = new Label();
            panelLoginContainer = new Panel();
            linkLabelForgotPassword = new LinkLabel();
            checkBoxRemember = new CheckBox();
            panelPasswordBox = new Panel();
            btnTogglePassword = new Button();
            lblPasswordIcon = new Label();
            passwordTxt = new TextBox();
            panelUsernameBox = new Panel();
            lblUsernameIcon = new Label();
            userNameTxt = new TextBox();
            loginbtn = new Button();
            lblPassword = new Label();
            lblUsername = new Label();
            lblSubtitle = new Label();
            lblTitle = new Label();
            panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            panelRight.SuspendLayout();
            panelLoginContainer.SuspendLayout();
            panelPasswordBox.SuspendLayout();
            panelUsernameBox.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(41, 128, 185);
            panelLeft.Controls.Add(lblSlogan);
            panelLeft.Controls.Add(lblSystemName);
            panelLeft.Controls.Add(pictureBoxLogo);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Margin = new Padding(3, 2, 3, 2);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(394, 488);
            panelLeft.TabIndex = 0;
            // 
            // lblSlogan
            // 
            lblSlogan.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            lblSlogan.ForeColor = Color.FromArgb(236, 240, 241);
            lblSlogan.Location = new Point(26, 322);
            lblSlogan.Name = "lblSlogan";
            lblSlogan.Size = new Size(341, 45);
            lblSlogan.TabIndex = 2;
            lblSlogan.Text = "\"Nơi ghi nhận nỗ lực và vinh danh thành tích\"";
            lblSlogan.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSystemName
            // 
            lblSystemName.Font = new Font("Microsoft Tai Le", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSystemName.ForeColor = Color.White;
            lblSystemName.Location = new Point(26, 255);
            lblSystemName.Name = "lblSystemName";
            lblSystemName.Size = new Size(341, 72);
            lblSystemName.TabIndex = 1;
            lblSystemName.Text = "Hệ thống quản lý\r\nSINH VIÊN 5 TỐT";
            lblSystemName.TextAlign = ContentAlignment.MiddleCenter;
            lblSystemName.Click += lblSystemName_Click;
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.BackColor = Color.FromArgb(41, 128, 185);
            pictureBoxLogo.Image = Properties.Resources._1200px_LogoUTC_removebg_preview;
            pictureBoxLogo.Location = new Point(131, 112);
            pictureBoxLogo.Margin = new Padding(3, 2, 3, 2);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(130, 130);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLogo.TabIndex = 0;
            pictureBoxLogo.TabStop = false;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.White;
            panelRight.Controls.Add(lblFooter);
            panelRight.Controls.Add(panelLoginContainer);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(394, 0);
            panelRight.Margin = new Padding(3, 2, 3, 2);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(52, 60, 52, 22);
            panelRight.Size = new Size(568, 488);
            panelRight.TabIndex = 1;
            // 
            // lblFooter
            // 
            lblFooter.Dock = DockStyle.Bottom;
            lblFooter.Font = new Font("Segoe UI", 9F);
            lblFooter.ForeColor = Color.FromArgb(189, 195, 199);
            lblFooter.Location = new Point(52, 451);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(464, 15);
            lblFooter.TabIndex = 1;
            lblFooter.Text = "© 2025 - Phát triển cho Phân hiệu UTC2";
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelLoginContainer
            // 
            panelLoginContainer.Controls.Add(linkLabelForgotPassword);
            panelLoginContainer.Controls.Add(checkBoxRemember);
            panelLoginContainer.Controls.Add(panelPasswordBox);
            panelLoginContainer.Controls.Add(panelUsernameBox);
            panelLoginContainer.Controls.Add(loginbtn);
            panelLoginContainer.Controls.Add(lblPassword);
            panelLoginContainer.Controls.Add(lblUsername);
            panelLoginContainer.Controls.Add(lblSubtitle);
            panelLoginContainer.Controls.Add(lblTitle);
            panelLoginContainer.Location = new Point(52, 60);
            panelLoginContainer.Margin = new Padding(3, 2, 3, 2);
            panelLoginContainer.Name = "panelLoginContainer";
            panelLoginContainer.Size = new Size(464, 375);
            panelLoginContainer.TabIndex = 0;
            // 
            // linkLabelForgotPassword
            // 
            linkLabelForgotPassword.ActiveLinkColor = Color.FromArgb(52, 152, 219);
            linkLabelForgotPassword.AutoSize = true;
            linkLabelForgotPassword.Font = new Font("Segoe UI", 9F);
            linkLabelForgotPassword.LinkColor = Color.FromArgb(52, 152, 219);
            linkLabelForgotPassword.Location = new Point(368, 248);
            linkLabelForgotPassword.Name = "linkLabelForgotPassword";
            linkLabelForgotPassword.Size = new Size(94, 15);
            linkLabelForgotPassword.TabIndex = 8;
            linkLabelForgotPassword.TabStop = true;
            linkLabelForgotPassword.Text = "Quên mật khẩu?";
            linkLabelForgotPassword.LinkClicked += linkLabelForgotPassword_LinkClicked;
            // 
            // checkBoxRemember
            // 
            checkBoxRemember.AutoSize = true;
            checkBoxRemember.Font = new Font("Segoe UI", 9F);
            checkBoxRemember.ForeColor = Color.FromArgb(127, 140, 141);
            checkBoxRemember.Location = new Point(9, 248);
            checkBoxRemember.Margin = new Padding(3, 2, 3, 2);
            checkBoxRemember.Name = "checkBoxRemember";
            checkBoxRemember.Size = new Size(128, 19);
            checkBoxRemember.TabIndex = 7;
            checkBoxRemember.Text = "Ghi nhớ đăng nhập";
            checkBoxRemember.UseVisualStyleBackColor = true;
            // 
            // panelPasswordBox
            // 
            panelPasswordBox.BackColor = Color.FromArgb(245, 247, 250);
            panelPasswordBox.Controls.Add(btnTogglePassword);
            panelPasswordBox.Controls.Add(lblPasswordIcon);
            panelPasswordBox.Controls.Add(passwordTxt);
            panelPasswordBox.Location = new Point(9, 202);
            panelPasswordBox.Margin = new Padding(3, 2, 3, 2);
            panelPasswordBox.Name = "panelPasswordBox";
            panelPasswordBox.Size = new Size(446, 34);
            panelPasswordBox.TabIndex = 6;
            // 
            // btnTogglePassword
            // 
            btnTogglePassword.BackColor = Color.Transparent;
            btnTogglePassword.Cursor = Cursors.Hand;
            btnTogglePassword.FlatAppearance.BorderSize = 0;
            btnTogglePassword.FlatStyle = FlatStyle.Flat;
            btnTogglePassword.Font = new Font("Segoe UI", 12F);
            btnTogglePassword.Location = new Point(407, 4);
            btnTogglePassword.Margin = new Padding(3, 2, 3, 2);
            btnTogglePassword.Name = "btnTogglePassword";
            btnTogglePassword.Size = new Size(35, 26);
            btnTogglePassword.TabIndex = 2;
            btnTogglePassword.Text = "👁️";
            btnTogglePassword.UseVisualStyleBackColor = false;
            btnTogglePassword.Click += btnTogglePassword_Click;
            // 
            // lblPasswordIcon
            // 
            lblPasswordIcon.Font = new Font("Segoe UI", 16F);
            lblPasswordIcon.Location = new Point(7, 1);
            lblPasswordIcon.Name = "lblPasswordIcon";
            lblPasswordIcon.Size = new Size(31, 30);
            lblPasswordIcon.TabIndex = 1;
            lblPasswordIcon.Text = "🔒";
            lblPasswordIcon.TextAlign = ContentAlignment.MiddleCenter;
            lblPasswordIcon.Click += lblPasswordIcon_Click;
            // 
            // passwordTxt
            // 
            passwordTxt.BackColor = Color.FromArgb(245, 247, 250);
            passwordTxt.BorderStyle = BorderStyle.None;
            passwordTxt.Font = new Font("Segoe UI", 12F);
            passwordTxt.Location = new Point(44, 8);
            passwordTxt.Margin = new Padding(3, 2, 3, 2);
            passwordTxt.Name = "passwordTxt";
            passwordTxt.Size = new Size(359, 22);
            passwordTxt.TabIndex = 0;
            passwordTxt.UseSystemPasswordChar = true;
            // 
            // panelUsernameBox
            // 
            panelUsernameBox.BackColor = Color.FromArgb(245, 247, 250);
            panelUsernameBox.Controls.Add(lblUsernameIcon);
            panelUsernameBox.Controls.Add(userNameTxt);
            panelUsernameBox.Location = new Point(9, 128);
            panelUsernameBox.Margin = new Padding(3, 2, 3, 2);
            panelUsernameBox.Name = "panelUsernameBox";
            panelUsernameBox.Size = new Size(446, 34);
            panelUsernameBox.TabIndex = 5;
            // 
            // lblUsernameIcon
            // 
            lblUsernameIcon.Font = new Font("Segoe UI", 16F);
            lblUsernameIcon.Location = new Point(8, 4);
            lblUsernameIcon.Name = "lblUsernameIcon";
            lblUsernameIcon.Size = new Size(31, 26);
            lblUsernameIcon.TabIndex = 1;
            lblUsernameIcon.Text = "👤";
            lblUsernameIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // userNameTxt
            // 
            userNameTxt.BackColor = Color.FromArgb(245, 247, 250);
            userNameTxt.BorderStyle = BorderStyle.None;
            userNameTxt.Font = new Font("Segoe UI", 12F);
            userNameTxt.Location = new Point(44, 8);
            userNameTxt.Margin = new Padding(3, 2, 3, 2);
            userNameTxt.Name = "userNameTxt";
            userNameTxt.Size = new Size(394, 22);
            userNameTxt.TabIndex = 0;
            // 
            // loginbtn
            // 
            loginbtn.BackColor = Color.FromArgb(41, 128, 185);
            loginbtn.Cursor = Cursors.Hand;
            loginbtn.FlatAppearance.BorderSize = 0;
            loginbtn.FlatStyle = FlatStyle.Flat;
            loginbtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            loginbtn.ForeColor = Color.White;
            loginbtn.Location = new Point(9, 285);
            loginbtn.Margin = new Padding(3, 2, 3, 2);
            loginbtn.Name = "loginbtn";
            loginbtn.Size = new Size(446, 38);
            loginbtn.TabIndex = 4;
            loginbtn.Text = "ĐĂNG NHẬP";
            loginbtn.UseVisualStyleBackColor = false;
            loginbtn.Click += loginbtn_Click;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(52, 73, 94);
            lblPassword.Location = new Point(9, 176);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(71, 19);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mật khẩu";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(52, 73, 94);
            lblUsername.Location = new Point(9, 101);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(107, 19);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Tên đăng nhập";
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(149, 165, 166);
            lblSubtitle.Location = new Point(14, 52);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(446, 22);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Chào mừng trở lại! Vui lòng nhập thông tin của bạn.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(7, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(334, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Đăng nhập Hệ thống";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(962, 488);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập - Hệ thống Quản lý Sinh viên 5 Tốt";
            Load += Form1_Load;
            panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            panelRight.ResumeLayout(false);
            panelLoginContainer.ResumeLayout(false);
            panelLoginContainer.PerformLayout();
            panelPasswordBox.ResumeLayout(false);
            panelPasswordBox.PerformLayout();
            panelUsernameBox.ResumeLayout(false);
            panelUsernameBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLeft;
        private PictureBox pictureBoxLogo;
        private Label lblSystemName;
        private Label lblSlogan;
        private Panel panelRight;
        private Panel panelLoginContainer;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblUsername;
        private Label lblPassword;
        private Panel panelUsernameBox;
        private Label lblUsernameIcon;
        private TextBox userNameTxt;
        private Panel panelPasswordBox;
        private Label lblPasswordIcon;
        private TextBox passwordTxt;
        private Button btnTogglePassword;
        private CheckBox checkBoxRemember;
        private LinkLabel linkLabelForgotPassword;
        private Button loginbtn;
        private Label lblFooter;
    }
}
