namespace StudentManagement5Good.Winform
{
    partial class TieuChiForm
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
            this.lblMaTC = new System.Windows.Forms.Label();
            this.txtMaTC = new System.Windows.Forms.TextBox();
            this.lblTenTieuChi = new System.Windows.Forms.Label();
            this.txtTenTieuChi = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblLoaiDuLieu = new System.Windows.Forms.Label();
            this.cmbLoaiDuLieu = new System.Windows.Forms.ComboBox();
            this.lblDonViTinh = new System.Windows.Forms.Label();
            this.txtDonViTinh = new System.Windows.Forms.TextBox();
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
            this.lblTitle.Text = "Thông tin Tiêu chí";
            // 
            // lblMaTC
            // 
            this.lblMaTC.AutoSize = true;
            this.lblMaTC.Location = new System.Drawing.Point(12, 50);
            this.lblMaTC.Name = "lblMaTC";
            this.lblMaTC.Size = new System.Drawing.Size(80, 15);
            this.lblMaTC.TabIndex = 1;
            this.lblMaTC.Text = "Mã Tiêu chí:";
            // 
            // txtMaTC
            // 
            this.txtMaTC.Location = new System.Drawing.Point(120, 47);
            this.txtMaTC.MaxLength = 20;
            this.txtMaTC.Name = "txtMaTC";
            this.txtMaTC.Size = new System.Drawing.Size(200, 23);
            this.txtMaTC.TabIndex = 2;
            // 
            // lblTenTieuChi
            // 
            this.lblTenTieuChi.AutoSize = true;
            this.lblTenTieuChi.Location = new System.Drawing.Point(12, 85);
            this.lblTenTieuChi.Name = "lblTenTieuChi";
            this.lblTenTieuChi.Size = new System.Drawing.Size(85, 15);
            this.lblTenTieuChi.TabIndex = 3;
            this.lblTenTieuChi.Text = "Tên Tiêu chí:";
            // 
            // txtTenTieuChi
            // 
            this.txtTenTieuChi.Location = new System.Drawing.Point(120, 82);
            this.txtTenTieuChi.MaxLength = 100;
            this.txtTenTieuChi.Name = "txtTenTieuChi";
            this.txtTenTieuChi.Size = new System.Drawing.Size(300, 23);
            this.txtTenTieuChi.TabIndex = 4;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(12, 120);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(45, 15);
            this.lblMoTa.TabIndex = 5;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(120, 117);
            this.txtMoTa.MaxLength = 500;
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoTa.Size = new System.Drawing.Size(300, 80);
            this.txtMoTa.TabIndex = 6;
            // 
            // lblLoaiDuLieu
            // 
            this.lblLoaiDuLieu.AutoSize = true;
            this.lblLoaiDuLieu.Location = new System.Drawing.Point(12, 210);
            this.lblLoaiDuLieu.Name = "lblLoaiDuLieu";
            this.lblLoaiDuLieu.Size = new System.Drawing.Size(80, 15);
            this.lblLoaiDuLieu.TabIndex = 7;
            this.lblLoaiDuLieu.Text = "Loại dữ liệu:";
            // 
            // cmbLoaiDuLieu
            // 
            this.cmbLoaiDuLieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoaiDuLieu.FormattingEnabled = true;
            this.cmbLoaiDuLieu.Location = new System.Drawing.Point(120, 207);
            this.cmbLoaiDuLieu.Name = "cmbLoaiDuLieu";
            this.cmbLoaiDuLieu.Size = new System.Drawing.Size(200, 23);
            this.cmbLoaiDuLieu.TabIndex = 8;
            // 
            // lblDonViTinh
            // 
            this.lblDonViTinh.AutoSize = true;
            this.lblDonViTinh.Location = new System.Drawing.Point(12, 245);
            this.lblDonViTinh.Name = "lblDonViTinh";
            this.lblDonViTinh.Size = new System.Drawing.Size(75, 15);
            this.lblDonViTinh.TabIndex = 9;
            this.lblDonViTinh.Text = "Đơn vị tính:";
            // 
            // txtDonViTinh
            // 
            this.txtDonViTinh.Location = new System.Drawing.Point(120, 242);
            this.txtDonViTinh.MaxLength = 50;
            this.txtDonViTinh.Name = "txtDonViTinh";
            this.txtDonViTinh.Size = new System.Drawing.Size(200, 23);
            this.txtDonViTinh.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(264, 290);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(345, 290);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // TieuChiForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(450, 340);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDonViTinh);
            this.Controls.Add(this.lblDonViTinh);
            this.Controls.Add(this.cmbLoaiDuLieu);
            this.Controls.Add(this.lblLoaiDuLieu);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.txtTenTieuChi);
            this.Controls.Add(this.lblTenTieuChi);
            this.Controls.Add(this.txtMaTC);
            this.Controls.Add(this.lblMaTC);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TieuChiForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý Tiêu chí";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaTC;
        private System.Windows.Forms.TextBox txtMaTC;
        private System.Windows.Forms.Label lblTenTieuChi;
        private System.Windows.Forms.TextBox txtTenTieuChi;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblLoaiDuLieu;
        private System.Windows.Forms.ComboBox cmbLoaiDuLieu;
        private System.Windows.Forms.Label lblDonViTinh;
        private System.Windows.Forms.TextBox txtDonViTinh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}

