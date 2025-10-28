using System.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.Services;
using StudentManagement5GoodTempp.DataAccess.Entity;
using StudentManagement5Good.Winform;

namespace StudentManagement5Good
{
    public partial class Login : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private User? _currentUser;

        // Constructor chính được DI container sử dụng
        public Login(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            InitializeComponent();
        }

        // Constructor mặc định - chỉ dùng cho designer
        public Login() 
        { 
            InitializeComponent();
            
            // Nếu không có serviceProvider, tạo một instance tạm thời cho designer
            if (_serviceProvider == null && !DesignMode)
            {
                throw new InvalidOperationException("ServiceProvider is required. Use the parameterized constructor.");
            }
        }

        public User? CurrentUser => _currentUser;

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu đang ở design mode thì không làm gì
                if (DesignMode || _serviceProvider == null) return;

                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Initialize database and apply migrations
                await context.Database.MigrateAsync();

                // Set password field to use password char
                passwordTxt.UseSystemPasswordChar = true;

                // Focus on username textbox
                userNameTxt.Focus();

                // Set form title
                this.Text = "Đăng nhập - Hệ thống Quản lý Sinh viên 5 Tốt";

                // Test connection by getting count of records (for development only)
                var studentCount = await context.SinhViens.CountAsync();
                var khoaCount = await context.Khoas.CountAsync();
                var userCount = await context.Users.CountAsync();

                // Display connection status in the form title (for development)
                this.Text += $" | DB: {userCount} users, {studentCount} students, {khoaCount} faculties";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void loginbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra ServiceProvider
                if (_serviceProvider == null)
                {
                    MessageBox.Show("Lỗi hệ thống: ServiceProvider không được khởi tạo!", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get username and password from controls
                var username = userNameTxt.Text.Trim();
                var password = passwordTxt.Text.Trim();

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", 
                                  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Show loading
                this.Cursor = Cursors.WaitCursor;
                loginbtn.Enabled = false;
                loginbtn.Text = "Đang đăng nhập...";

                // Create new scope for authentication
                using var scope = _serviceProvider.CreateScope();
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();

                // Authenticate user
                _currentUser = await userService.AuthenticateAsync(username, password);

                if (_currentUser != null)
                {
                    // Login successful
                    MessageBox.Show($"Đăng nhập thành công!\nChào mừng {_currentUser.HoTen}\nVai trò: {GetRoleDisplayName(_currentUser.VaiTro)}", 
                                  "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hide login form and show appropriate dashboard based on role
                    this.Hide();
                    
                    // Choose dashboard based on user role
                    if (_currentUser.VaiTro == UserRoles.SINHVIEN)
                    {
                        // Show Student Dashboard for students
                        var studentDashboard = new StudentDashboard(_serviceProvider, userService, studentService, _currentUser);
                        studentDashboard.ShowDialog();
                    }
                    else
                    {
                        // Show Admin/Staff Dashboard for other roles
                        var adminDashboard = new UserDashboard(_serviceProvider, userService, studentService, _currentUser);
                        adminDashboard.ShowDialog();
                    }
                    
                    // Close login form when dashboard is closed
                    this.Close();
                }
                else
                {
                    // Login failed
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", 
                                  "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    // Clear password field
                    passwordTxt.Clear();
                    passwordTxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi trong quá trình đăng nhập: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Restore UI state
                this.Cursor = Cursors.Default;
                loginbtn.Enabled = true;
                loginbtn.Text = "Đăng nhập";
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", 
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void gradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Custom paint logic if needed
        }

        #region Helper Methods

        private string GetControlValue(string controlName)
        {
            var control = this.Controls[controlName];
            return control switch
            {
                TextBox textBox => textBox.Text.Trim(),
                ComboBox comboBox => comboBox.Text.Trim(),
                _ => string.Empty
            };
        }

        private string GetRoleDisplayName(string role)
        {
            return role switch
            {
                UserRoles.ADMIN => "Quản trị hệ thống",
                UserRoles.GIAOVU => "Giáo vụ khoa",
                UserRoles.CVHT => "Cố vấn học tập",
                UserRoles.BANTHANG => "Ban thường vụ đoàn khoa",
                UserRoles.SINHVIEN => "Sinh viên",
                UserRoles.DOANKHOA => "Đoàn khoa",
                UserRoles.DOANTRUONG => "Đoàn trường",
                UserRoles.DOANTP => "Đoàn thành phố",
                UserRoles.DOANTU => "Đoàn trung ương",
                _ => role
            };
        }

        // Method for demonstration - can be removed later
        private async Task LoadStudentsExample()
        {
            try
            {
                if (_serviceProvider == null) return;

                using var scope = _serviceProvider.CreateScope();
                var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();

                // Example of using the service to get data
                var students = await studentService.GetAllStudentsAsync();

                // You could populate a DataGridView or other controls here
                // For example:
                // dataGridViewStudents.DataSource = students;

                this.Text = $"Quản lý sinh viên - Tổng số: {students.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu sinh viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
