namespace StudentManagement5Good.Winform
{
    partial class TieuChiYeuCauForm
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
            this.lblTieuChi = new System.Windows.Forms.Label();
            this.cmbTieuChi = new System.Windows.Forms.ComboBox();
            this.lblCapXet = new System.Windows.Forms.Label();
            this.cmbCapXet = new System.Windows.Forms.ComboBox();
            this.lblNguongDat = new System.Windows.Forms.Label();
            this.txtNguongDat = new System.Windows.Forms.TextBox();
            this.lblMoTaYeuCau = new System.Windows.Forms.Label();
            this.txtMoTaYeuCau = new System.Windows.Forms.TextBox();
            this.chkBatBuoc = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataGridViewTieuChiYeuCau = new System.Windows.Forms.DataGridView();
            this.colTieuChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCapXet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNguongDat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBatBuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoTaYeuCau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblExistingData = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTieuChiYeuCau)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(320, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cấu hình Tiêu chí Yêu cầu";
            // 
            // lblTieuChi
            // 
            this.lblTieuChi.AutoSize = true;
            this.lblTieuChi.Location = new System.Drawing.Point(12, 50);
            this.lblTieuChi.Name = "lblTieuChi";
            this.lblTieuChi.Size = new System.Drawing.Size(60, 15);
            this.lblTieuChi.TabIndex = 1;
            this.lblTieuChi.Text = "Tiêu chí:";
            // 
            // cmbTieuChi
            // 
            this.cmbTieuChi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTieuChi.FormattingEnabled = true;
            this.cmbTieuChi.Location = new System.Drawing.Point(120, 47);
            this.cmbTieuChi.Name = "cmbTieuChi";
            this.cmbTieuChi.Size = new System.Drawing.Size(200, 23);
            this.cmbTieuChi.TabIndex = 2;
            // 
            // lblCapXet
            // 
            this.lblCapXet.AutoSize = true;
            this.lblCapXet.Location = new System.Drawing.Point(12, 85);
            this.lblCapXet.Name = "lblCapXet";
            this.lblCapXet.Size = new System.Drawing.Size(50, 15);
            this.lblCapXet.TabIndex = 3;
            this.lblCapXet.Text = "Cấp xét:";
            // 
            // cmbCapXet
            // 
            this.cmbCapXet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCapXet.FormattingEnabled = true;
            this.cmbCapXet.Location = new System.Drawing.Point(120, 82);
            this.cmbCapXet.Name = "cmbCapXet";
            this.cmbCapXet.Size = new System.Drawing.Size(200, 23);
            this.cmbCapXet.TabIndex = 4;
            // 
            // lblNguongDat
            // 
            this.lblNguongDat.AutoSize = true;
            this.lblNguongDat.Location = new System.Drawing.Point(12, 120);
            this.lblNguongDat.Name = "lblNguongDat";
            this.lblNguongDat.Size = new System.Drawing.Size(80, 15);
            this.lblNguongDat.TabIndex = 5;
            this.lblNguongDat.Text = "Ngưỡng đạt:";
            // 
            // txtNguongDat
            // 
            this.txtNguongDat.Location = new System.Drawing.Point(120, 117);
            this.txtNguongDat.Name = "txtNguongDat";
            this.txtNguongDat.Size = new System.Drawing.Size(100, 23);
            this.txtNguongDat.TabIndex = 6;
            // 
            // lblMoTaYeuCau
            // 
            this.lblMoTaYeuCau.AutoSize = true;
            this.lblMoTaYeuCau.Location = new System.Drawing.Point(12, 155);
            this.lblMoTaYeuCau.Name = "lblMoTaYeuCau";
            this.lblMoTaYeuCau.Size = new System.Drawing.Size(100, 15);
            this.lblMoTaYeuCau.TabIndex = 7;
            this.lblMoTaYeuCau.Text = "Mô tả yêu cầu:";
            // 
            // txtMoTaYeuCau
            // 
            this.txtMoTaYeuCau.Location = new System.Drawing.Point(120, 152);
            this.txtMoTaYeuCau.MaxLength = 500;
            this.txtMoTaYeuCau.Multiline = true;
            this.txtMoTaYeuCau.Name = "txtMoTaYeuCau";
            this.txtMoTaYeuCau.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoTaYeuCau.Size = new System.Drawing.Size(300, 60);
            this.txtMoTaYeuCau.TabIndex = 8;
            // 
            // chkBatBuoc
            // 
            this.chkBatBuoc.AutoSize = true;
            this.chkBatBuoc.Location = new System.Drawing.Point(120, 225);
            this.chkBatBuoc.Name = "chkBatBuoc";
            this.chkBatBuoc.Size = new System.Drawing.Size(80, 19);
            this.chkBatBuoc.TabIndex = 9;
            this.chkBatBuoc.Text = "Bắt buộc";
            this.chkBatBuoc.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(120, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(201, 260);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Làm mới";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(282, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dataGridViewTieuChiYeuCau
            // 
            this.dataGridViewTieuChiYeuCau.AllowUserToAddRows = false;
            this.dataGridViewTieuChiYeuCau.AllowUserToDeleteRows = false;
            this.dataGridViewTieuChiYeuCau.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTieuChiYeuCau.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTieuChi,
            this.colCapXet,
            this.colNguongDat,
            this.colBatBuoc,
            this.colMoTaYeuCau});
            this.dataGridViewTieuChiYeuCau.Location = new System.Drawing.Point(12, 320);
            this.dataGridViewTieuChiYeuCau.MultiSelect = false;
            this.dataGridViewTieuChiYeuCau.Name = "dataGridViewTieuChiYeuCau";
            this.dataGridViewTieuChiYeuCau.ReadOnly = true;
            this.dataGridViewTieuChiYeuCau.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTieuChiYeuCau.Size = new System.Drawing.Size(600, 200);
            this.dataGridViewTieuChiYeuCau.TabIndex = 13;
            this.dataGridViewTieuChiYeuCau.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTieuChiYeuCau_CellDoubleClick);
            // 
            // colTieuChi
            // 
            this.colTieuChi.HeaderText = "Tiêu chí";
            this.colTieuChi.Name = "colTieuChi";
            this.colTieuChi.ReadOnly = true;
            this.colTieuChi.Width = 120;
            // 
            // colCapXet
            // 
            this.colCapXet.HeaderText = "Cấp xét";
            this.colCapXet.Name = "colCapXet";
            this.colCapXet.ReadOnly = true;
            this.colCapXet.Width = 100;
            // 
            // colNguongDat
            // 
            this.colNguongDat.HeaderText = "Ngưỡng đạt";
            this.colNguongDat.Name = "colNguongDat";
            this.colNguongDat.ReadOnly = true;
            this.colNguongDat.Width = 80;
            // 
            // colBatBuoc
            // 
            this.colBatBuoc.HeaderText = "Bắt buộc";
            this.colBatBuoc.Name = "colBatBuoc";
            this.colBatBuoc.ReadOnly = true;
            this.colBatBuoc.Width = 80;
            // 
            // colMoTaYeuCau
            // 
            this.colMoTaYeuCau.HeaderText = "Mô tả yêu cầu";
            this.colMoTaYeuCau.Name = "colMoTaYeuCau";
            this.colMoTaYeuCau.ReadOnly = true;
            this.colMoTaYeuCau.Width = 200;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(537, 320);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblExistingData
            // 
            this.lblExistingData.AutoSize = true;
            this.lblExistingData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblExistingData.Location = new System.Drawing.Point(12, 300);
            this.lblExistingData.Name = "lblExistingData";
            this.lblExistingData.Size = new System.Drawing.Size(200, 17);
            this.lblExistingData.TabIndex = 15;
            this.lblExistingData.Text = "Danh sách cấu hình hiện có:";
            // 
            // TieuChiYeuCauForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(630, 540);
            this.Controls.Add(this.lblExistingData);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dataGridViewTieuChiYeuCau);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkBatBuoc);
            this.Controls.Add(this.txtMoTaYeuCau);
            this.Controls.Add(this.lblMoTaYeuCau);
            this.Controls.Add(this.txtNguongDat);
            this.Controls.Add(this.lblNguongDat);
            this.Controls.Add(this.cmbCapXet);
            this.Controls.Add(this.lblCapXet);
            this.Controls.Add(this.cmbTieuChi);
            this.Controls.Add(this.lblTieuChi);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TieuChiYeuCauForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý Tiêu chí Yêu cầu";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTieuChiYeuCau)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTieuChi;
        private System.Windows.Forms.ComboBox cmbTieuChi;
        private System.Windows.Forms.Label lblCapXet;
        private System.Windows.Forms.ComboBox cmbCapXet;
        private System.Windows.Forms.Label lblNguongDat;
        private System.Windows.Forms.TextBox txtNguongDat;
        private System.Windows.Forms.Label lblMoTaYeuCau;
        private System.Windows.Forms.TextBox txtMoTaYeuCau;
        private System.Windows.Forms.CheckBox chkBatBuoc;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dataGridViewTieuChiYeuCau;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTieuChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCapXet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNguongDat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBatBuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoTaYeuCau;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblExistingData;
    }
}

