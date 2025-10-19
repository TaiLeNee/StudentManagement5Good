namespace StudentManagement5Good.Winform
{
    partial class AcademicYearForm
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
            lblMaNH = new Label();
            txtMaNH = new TextBox();
            lblTenNH = new Label();
            txtTenNH = new TextBox();
            lblNgayBatDau = new Label();
            dateTimePickerStart = new DateTimePicker();
            lblNgayKetThuc = new Label();
            dateTimePickerEnd = new DateTimePicker();
            
            panelBottom = new Panel();
            btnSave = new Button();
            btnCancel = new Button();
            
            panelTop.SuspendLayout();
            panelMain.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(41, 128, 185);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(584, 70);
            panelTop.TabIndex = 0;
            
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(250, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Năm học";
            
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(dateTimePickerEnd);
            panelMain.Controls.Add(lblNgayKetThuc);
            panelMain.Controls.Add(dateTimePickerStart);
            panelMain.Controls.Add(lblNgayBatDau);
            panelMain.Controls.Add(txtTenNH);
            panelMain.Controls.Add(lblTenNH);
            panelMain.Controls.Add(txtMaNH);
            panelMain.Controls.Add(lblMaNH);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 70);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(30, 20, 30, 20);
            panelMain.Size = new Size(584, 250);
            panelMain.TabIndex = 1;
            
            // 
            // lblMaNH
            // 
            lblMaNH.AutoSize = true;
            lblMaNH.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMaNH.Location = new Point(30, 30);
            lblMaNH.Name = "lblMaNH";
            lblMaNH.Size = new Size(120, 23);
            lblMaNH.TabIndex = 0;
            lblMaNH.Text = "Mã năm học:";
            
            // 
            // txtMaNH
            // 
            txtMaNH.Font = new Font("Segoe UI", 10F);
            txtMaNH.Location = new Point(200, 27);
            txtMaNH.MaxLength = 20;
            txtMaNH.Name = "txtMaNH";
            txtMaNH.Size = new Size(350, 30);
            txtMaNH.TabIndex = 1;
            
            // 
            // lblTenNH
            // 
            lblTenNH.AutoSize = true;
            lblTenNH.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTenNH.Location = new Point(30, 75);
            lblTenNH.Name = "lblTenNH";
            lblTenNH.Size = new Size(120, 23);
            lblTenNH.TabIndex = 2;
            lblTenNH.Text = "Tên năm học:";
            
            // 
            // txtTenNH
            // 
            txtTenNH.Font = new Font("Segoe UI", 10F);
            txtTenNH.Location = new Point(200, 72);
            txtTenNH.MaxLength = 100;
            txtTenNH.Name = "txtTenNH";
            txtTenNH.Size = new Size(350, 30);
            txtTenNH.TabIndex = 3;
            
            // 
            // lblNgayBatDau
            // 
            lblNgayBatDau.AutoSize = true;
            lblNgayBatDau.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNgayBatDau.Location = new Point(30, 120);
            lblNgayBatDau.Name = "lblNgayBatDau";
            lblNgayBatDau.Size = new Size(130, 23);
            lblNgayBatDau.TabIndex = 4;
            lblNgayBatDau.Text = "Ngày bắt đầu:";
            
            // 
            // dateTimePickerStart
            // 
            dateTimePickerStart.Font = new Font("Segoe UI", 10F);
            dateTimePickerStart.Format = DateTimePickerFormat.Short;
            dateTimePickerStart.Location = new Point(200, 117);
            dateTimePickerStart.Name = "dateTimePickerStart";
            dateTimePickerStart.Size = new Size(350, 30);
            dateTimePickerStart.TabIndex = 5;
            
            // 
            // lblNgayKetThuc
            // 
            lblNgayKetThuc.AutoSize = true;
            lblNgayKetThuc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNgayKetThuc.Location = new Point(30, 165);
            lblNgayKetThuc.Name = "lblNgayKetThuc";
            lblNgayKetThuc.Size = new Size(140, 23);
            lblNgayKetThuc.TabIndex = 6;
            lblNgayKetThuc.Text = "Ngày kết thúc:";
            
            // 
            // dateTimePickerEnd
            // 
            dateTimePickerEnd.Font = new Font("Segoe UI", 10F);
            dateTimePickerEnd.Format = DateTimePickerFormat.Short;
            dateTimePickerEnd.Location = new Point(200, 162);
            dateTimePickerEnd.Name = "dateTimePickerEnd";
            dateTimePickerEnd.Size = new Size(350, 30);
            dateTimePickerEnd.TabIndex = 7;
            
            
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(236, 240, 241);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 320);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(584, 80);
            panelBottom.TabIndex = 2;
            
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(280, 15);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(140, 50);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾 Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 11F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(430, 15);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(140, 50);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "❌ Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            
            // 
            // AcademicYearForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 400);
            Controls.Add(panelMain);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Font = new Font("Segoe UI", 9F);
            Name = "AcademicYearForm";
            Text = "Năm học";
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
        private Label lblMaNH;
        private TextBox txtMaNH;
        private Label lblTenNH;
        private TextBox txtTenNH;
        private Label lblNgayBatDau;
        private DateTimePicker dateTimePickerStart;
        private Label lblNgayKetThuc;
        private DateTimePicker dateTimePickerEnd;
        
        private Panel panelBottom;
        private Button btnSave;
        private Button btnCancel;
    }
}

