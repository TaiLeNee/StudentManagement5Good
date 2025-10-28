namespace StudentManagement5Good.Winform
{
    partial class TruongForm
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
            this.lblMaTruong = new System.Windows.Forms.Label();
            this.txtMaTruong = new System.Windows.Forms.TextBox();
            this.lblTenTruong = new System.Windows.Forms.Label();
            this.txtTenTruong = new System.Windows.Forms.TextBox();
            this.lblTenVietTat = new System.Windows.Forms.Label();
            this.txtTenVietTat = new System.Windows.Forms.TextBox();
            this.lblLoaiTruong = new System.Windows.Forms.Label();
            this.txtLoaiTruong = new System.Windows.Forms.TextBox();
            this.lblHieuTruong = new System.Windows.Forms.Label();
            this.txtHieuTruong = new System.Windows.Forms.TextBox();
            this.lblBiThuDoan = new System.Windows.Forms.Label();
            this.txtBiThuDoan = new System.Windows.Forms.TextBox();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.txtWebsite = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblThanhPho = new System.Windows.Forms.Label();
            this.cmbThanhPho = new System.Windows.Forms.ComboBox();
            this.lblNgayThanhLap = new System.Windows.Forms.Label();
            this.chkNgayThanhLap = new System.Windows.Forms.CheckBox();
            this.dtpNgayThanhLap = new System.Windows.Forms.DateTimePicker();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thông tin Trường";
            // 
            // lblMaTruong
            // 
            this.lblMaTruong.AutoSize = true;
            this.lblMaTruong.Location = new System.Drawing.Point(12, 50);
            this.lblMaTruong.Name = "lblMaTruong";
            this.lblMaTruong.Size = new System.Drawing.Size(70, 15);
            this.lblMaTruong.TabIndex = 1;
            this.lblMaTruong.Text = "Mã Trường:";
            // 
            // txtMaTruong
            // 
            this.txtMaTruong.Location = new System.Drawing.Point(120, 47);
            this.txtMaTruong.MaxLength = 20;
            this.txtMaTruong.Name = "txtMaTruong";
            this.txtMaTruong.Size = new System.Drawing.Size(200, 23);
            this.txtMaTruong.TabIndex = 2;
            // 
            // lblTenTruong
            // 
            this.lblTenTruong.AutoSize = true;
            this.lblTenTruong.Location = new System.Drawing.Point(12, 85);
            this.lblTenTruong.Name = "lblTenTruong";
            this.lblTenTruong.Size = new System.Drawing.Size(75, 15);
            this.lblTenTruong.TabIndex = 3;
            this.lblTenTruong.Text = "Tên Trường:";
            // 
            // txtTenTruong
            // 
            this.txtTenTruong.Location = new System.Drawing.Point(120, 82);
            this.txtTenTruong.MaxLength = 200;
            this.txtTenTruong.Name = "txtTenTruong";
            this.txtTenTruong.Size = new System.Drawing.Size(300, 23);
            this.txtTenTruong.TabIndex = 4;
            // 
            // lblTenVietTat
            // 
            this.lblTenVietTat.AutoSize = true;
            this.lblTenVietTat.Location = new System.Drawing.Point(12, 120);
            this.lblTenVietTat.Name = "lblTenVietTat";
            this.lblTenVietTat.Size = new System.Drawing.Size(80, 15);
            this.lblTenVietTat.TabIndex = 5;
            this.lblTenVietTat.Text = "Tên viết tắt:";
            // 
            // txtTenVietTat
            // 
            this.txtTenVietTat.Location = new System.Drawing.Point(120, 117);
            this.txtTenVietTat.MaxLength = 50;
            this.txtTenVietTat.Name = "txtTenVietTat";
            this.txtTenVietTat.Size = new System.Drawing.Size(200, 23);
            this.txtTenVietTat.TabIndex = 6;
            // 
            // lblLoaiTruong
            // 
            this.lblLoaiTruong.AutoSize = true;
            this.lblLoaiTruong.Location = new System.Drawing.Point(12, 155);
            this.lblLoaiTruong.Name = "lblLoaiTruong";
            this.lblLoaiTruong.Size = new System.Drawing.Size(75, 15);
            this.lblLoaiTruong.TabIndex = 7;
            this.lblLoaiTruong.Text = "Loại Trường:";
            // 
            // txtLoaiTruong
            // 
            this.txtLoaiTruong.Location = new System.Drawing.Point(120, 152);
            this.txtLoaiTruong.MaxLength = 50;
            this.txtLoaiTruong.Name = "txtLoaiTruong";
            this.txtLoaiTruong.Size = new System.Drawing.Size(200, 23);
            this.txtLoaiTruong.TabIndex = 8;
            // 
            // lblHieuTruong
            // 
            this.lblHieuTruong.AutoSize = true;
            this.lblHieuTruong.Location = new System.Drawing.Point(12, 190);
            this.lblHieuTruong.Name = "lblHieuTruong";
            this.lblHieuTruong.Size = new System.Drawing.Size(80, 15);
            this.lblHieuTruong.TabIndex = 9;
            this.lblHieuTruong.Text = "Hiệu trưởng:";
            // 
            // txtHieuTruong
            // 
            this.txtHieuTruong.Location = new System.Drawing.Point(120, 187);
            this.txtHieuTruong.MaxLength = 100;
            this.txtHieuTruong.Name = "txtHieuTruong";
            this.txtHieuTruong.Size = new System.Drawing.Size(300, 23);
            this.txtHieuTruong.TabIndex = 10;
            // 
            // lblBiThuDoan
            // 
            this.lblBiThuDoan.AutoSize = true;
            this.lblBiThuDoan.Location = new System.Drawing.Point(12, 225);
            this.lblBiThuDoan.Name = "lblBiThuDoan";
            this.lblBiThuDoan.Size = new System.Drawing.Size(85, 15);
            this.lblBiThuDoan.TabIndex = 11;
            this.lblBiThuDoan.Text = "Bí thư Đoàn:";
            // 
            // txtBiThuDoan
            // 
            this.txtBiThuDoan.Location = new System.Drawing.Point(120, 222);
            this.txtBiThuDoan.MaxLength = 100;
            this.txtBiThuDoan.Name = "txtBiThuDoan";
            this.txtBiThuDoan.Size = new System.Drawing.Size(300, 23);
            this.txtBiThuDoan.TabIndex = 12;
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Location = new System.Drawing.Point(12, 260);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(80, 15);
            this.lblSoDienThoai.TabIndex = 13;
            this.lblSoDienThoai.Text = "Số điện thoại:";
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Location = new System.Drawing.Point(120, 257);
            this.txtSoDienThoai.MaxLength = 15;
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(200, 23);
            this.txtSoDienThoai.TabIndex = 14;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(12, 295);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 15;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(120, 292);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(300, 23);
            this.txtEmail.TabIndex = 16;
            // 
            // lblWebsite
            // 
            this.lblWebsite.AutoSize = true;
            this.lblWebsite.Location = new System.Drawing.Point(12, 330);
            this.lblWebsite.Name = "lblWebsite";
            this.lblWebsite.Size = new System.Drawing.Size(55, 15);
            this.lblWebsite.TabIndex = 17;
            this.lblWebsite.Text = "Website:";
            // 
            // txtWebsite
            // 
            this.txtWebsite.Location = new System.Drawing.Point(120, 327);
            this.txtWebsite.MaxLength = 200;
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Size = new System.Drawing.Size(300, 23);
            this.txtWebsite.TabIndex = 18;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(12, 365);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(50, 15);
            this.lblDiaChi.TabIndex = 19;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(120, 362);
            this.txtDiaChi.MaxLength = 300;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(300, 23);
            this.txtDiaChi.TabIndex = 20;
            // 
            // lblThanhPho
            // 
            this.lblThanhPho.AutoSize = true;
            this.lblThanhPho.Location = new System.Drawing.Point(12, 400);
            this.lblThanhPho.Name = "lblThanhPho";
            this.lblThanhPho.Size = new System.Drawing.Size(75, 15);
            this.lblThanhPho.TabIndex = 21;
            this.lblThanhPho.Text = "Thành phố:";
            // 
            // cmbThanhPho
            // 
            this.cmbThanhPho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThanhPho.FormattingEnabled = true;
            this.cmbThanhPho.Location = new System.Drawing.Point(120, 397);
            this.cmbThanhPho.Name = "cmbThanhPho";
            this.cmbThanhPho.Size = new System.Drawing.Size(300, 23);
            this.cmbThanhPho.TabIndex = 22;
            // 
            // lblNgayThanhLap
            // 
            this.lblNgayThanhLap.AutoSize = true;
            this.lblNgayThanhLap.Location = new System.Drawing.Point(12, 435);
            this.lblNgayThanhLap.Name = "lblNgayThanhLap";
            this.lblNgayThanhLap.Size = new System.Drawing.Size(95, 15);
            this.lblNgayThanhLap.TabIndex = 23;
            this.lblNgayThanhLap.Text = "Ngày thành lập:";
            // 
            // chkNgayThanhLap
            // 
            this.chkNgayThanhLap.AutoSize = true;
            this.chkNgayThanhLap.Location = new System.Drawing.Point(120, 434);
            this.chkNgayThanhLap.Name = "chkNgayThanhLap";
            this.chkNgayThanhLap.Size = new System.Drawing.Size(15, 14);
            this.chkNgayThanhLap.TabIndex = 24;
            this.chkNgayThanhLap.UseVisualStyleBackColor = true;
            this.chkNgayThanhLap.CheckedChanged += new System.EventHandler(this.chkNgayThanhLap_CheckedChanged);
            // 
            // dtpNgayThanhLap
            // 
            this.dtpNgayThanhLap.Enabled = false;
            this.dtpNgayThanhLap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayThanhLap.Location = new System.Drawing.Point(150, 432);
            this.dtpNgayThanhLap.Name = "dtpNgayThanhLap";
            this.dtpNgayThanhLap.Size = new System.Drawing.Size(120, 23);
            this.dtpNgayThanhLap.TabIndex = 25;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(12, 470);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(55, 15);
            this.lblGhiChu.TabIndex = 26;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(120, 467);
            this.txtGhiChu.MaxLength = 500;
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(300, 60);
            this.txtGhiChu.TabIndex = 27;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangThai.Location = new System.Drawing.Point(120, 540);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(80, 19);
            this.chkTrangThai.TabIndex = 28;
            this.chkTrangThai.Text = "Hoạt động";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(264, 580);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(345, 580);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // TruongForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(450, 630);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkTrangThai);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.dtpNgayThanhLap);
            this.Controls.Add(this.chkNgayThanhLap);
            this.Controls.Add(this.lblNgayThanhLap);
            this.Controls.Add(this.cmbThanhPho);
            this.Controls.Add(this.lblThanhPho);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.lblDiaChi);
            this.Controls.Add(this.txtWebsite);
            this.Controls.Add(this.lblWebsite);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.lblSoDienThoai);
            this.Controls.Add(this.txtBiThuDoan);
            this.Controls.Add(this.lblBiThuDoan);
            this.Controls.Add(this.txtHieuTruong);
            this.Controls.Add(this.lblHieuTruong);
            this.Controls.Add(this.txtLoaiTruong);
            this.Controls.Add(this.lblLoaiTruong);
            this.Controls.Add(this.txtTenVietTat);
            this.Controls.Add(this.lblTenVietTat);
            this.Controls.Add(this.txtTenTruong);
            this.Controls.Add(this.lblTenTruong);
            this.Controls.Add(this.txtMaTruong);
            this.Controls.Add(this.lblMaTruong);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TruongForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý Trường";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaTruong;
        private System.Windows.Forms.TextBox txtMaTruong;
        private System.Windows.Forms.Label lblTenTruong;
        private System.Windows.Forms.TextBox txtTenTruong;
        private System.Windows.Forms.Label lblTenVietTat;
        private System.Windows.Forms.TextBox txtTenVietTat;
        private System.Windows.Forms.Label lblLoaiTruong;
        private System.Windows.Forms.TextBox txtLoaiTruong;
        private System.Windows.Forms.Label lblHieuTruong;
        private System.Windows.Forms.TextBox txtHieuTruong;
        private System.Windows.Forms.Label lblBiThuDoan;
        private System.Windows.Forms.TextBox txtBiThuDoan;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblWebsite;
        private System.Windows.Forms.TextBox txtWebsite;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblThanhPho;
        private System.Windows.Forms.ComboBox cmbThanhPho;
        private System.Windows.Forms.Label lblNgayThanhLap;
        private System.Windows.Forms.CheckBox chkNgayThanhLap;
        private System.Windows.Forms.DateTimePicker dtpNgayThanhLap;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}

