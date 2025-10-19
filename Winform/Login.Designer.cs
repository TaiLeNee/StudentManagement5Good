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
            gradientPanel1 = new GradientPanel();
            pictureBox1 = new PictureBox();
            userNameTxt = new TextBox();
            passwordTxt = new TextBox();
            label1 = new Label();
            label2 = new Label();
            loginbtn = new Button();
            gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // gradientPanel1
            // 
            gradientPanel1.Controls.Add(pictureBox1);
            gradientPanel1.gradientBottom = Color.FromArgb(33, 145, 245);
            gradientPanel1.gradientTop = Color.FromArgb(9, 74, 158);
            gradientPanel1.Location = new Point(-8, 0);
            gradientPanel1.Name = "gradientPanel1";
            gradientPanel1.Size = new Size(516, 450);
            gradientPanel1.TabIndex = 0;
            gradientPanel1.Paint += gradientPanel1_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.cloud1;
            pictureBox1.Location = new Point(-345, -6);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(861, 458);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // userNameTxt
            // 
            userNameTxt.Location = new Point(524, 196);
            userNameTxt.Margin = new Padding(3, 2, 3, 2);
            userNameTxt.Name = "userNameTxt";
            userNameTxt.Size = new Size(229, 23);
            userNameTxt.TabIndex = 1;
            // 
            // passwordTxt
            // 
            passwordTxt.Location = new Point(524, 247);
            passwordTxt.Margin = new Padding(3, 2, 3, 2);
            passwordTxt.Name = "passwordTxt";
            passwordTxt.Size = new Size(229, 23);
            passwordTxt.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(524, 179);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 3;
            label1.Text = "Tên Đăng Nhập";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(524, 231);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 4;
            label2.Text = "Mật Khẩu";
            // 
            // loginbtn
            // 
            loginbtn.Location = new Point(593, 288);
            loginbtn.Margin = new Padding(3, 2, 3, 2);
            loginbtn.Name = "loginbtn";
            loginbtn.Size = new Size(82, 22);
            loginbtn.TabIndex = 5;
            loginbtn.Text = "Đăng nhập ";
            loginbtn.UseVisualStyleBackColor = true;
            loginbtn.Click += loginbtn_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(loginbtn);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(passwordTxt);
            Controls.Add(userNameTxt);
            Controls.Add(gradientPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Login";
            Text = "Form1";
            Load += Form1_Load;
            gradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GradientPanel gradientPanel1;
        private PictureBox pictureBox1;
        private TextBox userNameTxt;
        private TextBox passwordTxt;
        private Label label1;
        private Label label2;
        private Button loginbtn;
    }
}
