using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5Good.Winform
{
    public partial class UserDashboard
    {
        /// <summary>
        /// Cập nhật method btnGenerateReport_Click để sử dụng SimpleReportForm
        /// </summary>
        private void btnGenerateReport_Click_Updated(object sender, EventArgs e)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
                
                var reportForm = new SimpleReportForm(context, _currentUser);
                reportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form báo cáo: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật method btnExportExcel_Click để sử dụng SimpleReportForm
        /// </summary>
        private void btnExportExcel_Click_Updated(object sender, EventArgs e)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
                
                var reportForm = new SimpleReportForm(context, _currentUser);
                reportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form báo cáo: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
