using System.Threading.Tasks;

namespace StudentManagement5GoodTempp.Services
{
    public interface IApprovalWorkflowService
    {
        Task ChuyenHoSoLenCapTrenAsync(string maSV, string maNH, string maCapDaDat);
    }
}
