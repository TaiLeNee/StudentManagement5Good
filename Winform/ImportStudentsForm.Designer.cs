namespace StudentManagement5Good.Winform
{
    partial class ImportStudentsForm
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
            lblInstructions = new Label();
            btnDownloadTemplate = new Button();
            
            panelFile = new Panel();
            lblFile = new Label();
            txtFilePath = new TextBox();
            btnBrowseFile = new Button();
            
            panelPreview = new Panel();
            lblPreview = new Label();
            dataGridViewPreview = new DataGridView();
            lblRecordCount = new Label();
            
            panelValidation = new Panel();
            lblValidation = new Label();
            txtValidationResult = new TextBox();
            
            panelBottom = new Panel();
            progressBar = new ProgressBar();
            btnImport = new Button();
            btnCancel = new Button();
            
            panelTop.SuspendLayout();
            panelFile.SuspendLayout();
            panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPreview).BeginInit();
            panelValidation.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(41, 128, 185);
            panelTop.Controls.Add(btnDownloadTemplate);
            panelTop.Controls.Add(lblInstructions);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(984, 100);
            panelTop.TabIndex = 0;
            
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Import Sinh viên từ Excel";
            
            // 
            // lblInstructions
            // 
            lblInstructions.AutoSize = true;
            lblInstructions.Font = new Font("Segoe UI", 10F);
            lblInstructions.ForeColor = Color.White;
            lblInstructions.Location = new Point(20, 55);
            lblInstructions.Name = "lblInstructions";
            lblInstructions.Size = new Size(400, 23);
            lblInstructions.TabIndex = 1;
            lblInstructions.Text = "Hãy chọn file Excel hoặc CSV chứa danh sách sinh viên";
            
            // 
            // btnDownloadTemplate
            // 
            btnDownloadTemplate.BackColor = Color.FromArgb(46, 204, 113);
            btnDownloadTemplate.FlatAppearance.BorderSize = 0;
            btnDownloadTemplate.FlatStyle = FlatStyle.Flat;
            btnDownloadTemplate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDownloadTemplate.ForeColor = Color.White;
            btnDownloadTemplate.Location = new Point(800, 30);
            btnDownloadTemplate.Name = "btnDownloadTemplate";
            btnDownloadTemplate.Size = new Size(160, 40);
            btnDownloadTemplate.TabIndex = 2;
            btnDownloadTemplate.Text = "📥 Tải file mẫu";
            btnDownloadTemplate.UseVisualStyleBackColor = false;
            btnDownloadTemplate.Click += btnDownloadTemplate_Click;
            
            // 
            // panelFile
            // 
            panelFile.BackColor = Color.White;
            panelFile.Controls.Add(btnBrowseFile);
            panelFile.Controls.Add(txtFilePath);
            panelFile.Controls.Add(lblFile);
            panelFile.Dock = DockStyle.Top;
            panelFile.Location = new Point(0, 100);
            panelFile.Name = "panelFile";
            panelFile.Padding = new Padding(20);
            panelFile.Size = new Size(984, 80);
            panelFile.TabIndex = 1;
            
            // 
            // lblFile
            // 
            lblFile.AutoSize = true;
            lblFile.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFile.Location = new Point(20, 10);
            lblFile.Name = "lblFile";
            lblFile.Size = new Size(100, 23);
            lblFile.TabIndex = 0;
            lblFile.Text = "Chọn file:";
            
            // 
            // txtFilePath
            // 
            txtFilePath.Font = new Font("Segoe UI", 10F);
            txtFilePath.Location = new Point(20, 35);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.ReadOnly = true;
            txtFilePath.Size = new Size(700, 30);
            txtFilePath.TabIndex = 1;
            
            // 
            // btnBrowseFile
            // 
            btnBrowseFile.BackColor = Color.FromArgb(52, 152, 219);
            btnBrowseFile.FlatAppearance.BorderSize = 0;
            btnBrowseFile.FlatStyle = FlatStyle.Flat;
            btnBrowseFile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBrowseFile.ForeColor = Color.White;
            btnBrowseFile.Location = new Point(730, 35);
            btnBrowseFile.Name = "btnBrowseFile";
            btnBrowseFile.Size = new Size(120, 30);
            btnBrowseFile.TabIndex = 2;
            btnBrowseFile.Text = "Chọn file...";
            btnBrowseFile.UseVisualStyleBackColor = false;
            btnBrowseFile.Click += btnBrowseFile_Click;
            
            // 
            // panelPreview
            // 
            panelPreview.Controls.Add(dataGridViewPreview);
            panelPreview.Controls.Add(lblRecordCount);
            panelPreview.Controls.Add(lblPreview);
            panelPreview.Dock = DockStyle.Fill;
            panelPreview.Location = new Point(0, 180);
            panelPreview.Name = "panelPreview";
            panelPreview.Padding = new Padding(20, 10, 20, 10);
            panelPreview.Size = new Size(984, 250);
            panelPreview.TabIndex = 2;
            
            // 
            // lblPreview
            // 
            lblPreview.AutoSize = true;
            lblPreview.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPreview.Location = new Point(20, 10);
            lblPreview.Name = "lblPreview";
            lblPreview.Size = new Size(150, 23);
            lblPreview.TabIndex = 0;
            lblPreview.Text = "Xem trước dữ liệu:";
            
            // 
            // lblRecordCount
            // 
            lblRecordCount.AutoSize = true;
            lblRecordCount.Font = new Font("Segoe UI", 9F);
            lblRecordCount.ForeColor = Color.Gray;
            lblRecordCount.Location = new Point(180, 13);
            lblRecordCount.Name = "lblRecordCount";
            lblRecordCount.Size = new Size(100, 20);
            lblRecordCount.TabIndex = 1;
            lblRecordCount.Text = "Tổng số: 0";
            
            // 
            // dataGridViewPreview
            // 
            dataGridViewPreview.AllowUserToAddRows = false;
            dataGridViewPreview.AllowUserToDeleteRows = false;
            dataGridViewPreview.BackgroundColor = Color.White;
            dataGridViewPreview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPreview.Location = new Point(20, 40);
            dataGridViewPreview.Name = "dataGridViewPreview";
            dataGridViewPreview.ReadOnly = true;
            dataGridViewPreview.RowHeadersWidth = 51;
            dataGridViewPreview.Size = new Size(944, 200);
            dataGridViewPreview.TabIndex = 2;
            
            // 
            // panelValidation
            // 
            panelValidation.Controls.Add(txtValidationResult);
            panelValidation.Controls.Add(lblValidation);
            panelValidation.Dock = DockStyle.Bottom;
            panelValidation.Location = new Point(0, 430);
            panelValidation.Name = "panelValidation";
            panelValidation.Padding = new Padding(20, 10, 20, 10);
            panelValidation.Size = new Size(984, 150);
            panelValidation.TabIndex = 3;
            
            // 
            // lblValidation
            // 
            lblValidation.AutoSize = true;
            lblValidation.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblValidation.Location = new Point(20, 10);
            lblValidation.Name = "lblValidation";
            lblValidation.Size = new Size(150, 23);
            lblValidation.TabIndex = 0;
            lblValidation.Text = "Kết quả kiểm tra:";
            
            // 
            // txtValidationResult
            // 
            txtValidationResult.BackColor = Color.White;
            txtValidationResult.Font = new Font("Consolas", 9F);
            txtValidationResult.Location = new Point(20, 40);
            txtValidationResult.Multiline = true;
            txtValidationResult.Name = "txtValidationResult";
            txtValidationResult.ReadOnly = true;
            txtValidationResult.ScrollBars = ScrollBars.Vertical;
            txtValidationResult.Size = new Size(944, 90);
            txtValidationResult.TabIndex = 1;
            
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(236, 240, 241);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Controls.Add(btnImport);
            panelBottom.Controls.Add(progressBar);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 580);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(984, 80);
            panelBottom.TabIndex = 4;
            
            // 
            // progressBar
            // 
            progressBar.Location = new Point(20, 15);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(600, 25);
            progressBar.TabIndex = 0;
            progressBar.Visible = false;
            
            // 
            // btnImport
            // 
            btnImport.BackColor = Color.FromArgb(46, 204, 113);
            btnImport.Enabled = false;
            btnImport.FlatAppearance.BorderSize = 0;
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnImport.ForeColor = Color.White;
            btnImport.Location = new Point(650, 15);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(150, 50);
            btnImport.TabIndex = 1;
            btnImport.Text = "✅ Import";
            btnImport.UseVisualStyleBackColor = false;
            btnImport.Click += btnImport_Click;
            
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 11F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(810, 15);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(150, 50);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "❌ Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            
            // 
            // ImportStudentsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(984, 661);
            Controls.Add(panelPreview);
            Controls.Add(panelValidation);
            Controls.Add(panelBottom);
            Controls.Add(panelFile);
            Controls.Add(panelTop);
            Font = new Font("Segoe UI", 9F);
            Name = "ImportStudentsForm";
            Text = "Import Sinh viên";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelFile.ResumeLayout(false);
            panelFile.PerformLayout();
            panelPreview.ResumeLayout(false);
            panelPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPreview).EndInit();
            panelValidation.ResumeLayout(false);
            panelValidation.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblTitle;
        private Label lblInstructions;
        private Button btnDownloadTemplate;
        
        private Panel panelFile;
        private Label lblFile;
        private TextBox txtFilePath;
        private Button btnBrowseFile;
        
        private Panel panelPreview;
        private Label lblPreview;
        private Label lblRecordCount;
        private DataGridView dataGridViewPreview;
        
        private Panel panelValidation;
        private Label lblValidation;
        private TextBox txtValidationResult;
        
        private Panel panelBottom;
        private ProgressBar progressBar;
        private Button btnImport;
        private Button btnCancel;
    }
}

