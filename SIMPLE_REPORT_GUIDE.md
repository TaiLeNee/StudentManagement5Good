# Hướng dẫn sử dụng hệ thống xuất báo cáo đơn giản

## Tổng quan

Hệ thống xuất báo cáo đơn giản đã được tạo để thay thế EPPlus, sử dụng **ClosedXML** - một thư viện miễn phí và đơn giản hơn để xuất Excel.

## Các tính năng chính

### 1. Xuất báo cáo danh sách sinh viên 5 tốt
- **Mô tả**: Xuất danh sách tất cả sinh viên với thông tin chi tiết về tiến độ đánh giá
- **Dữ liệu bao gồm**:
  - Thông tin cơ bản: Mã SV, Họ tên, Lớp, Khoa, Trường
  - Trạng thái đánh giá: Số minh chứng đã duyệt, chờ duyệt, bị từ chối
  - Kết quả cuối cùng: Danh hiệu đạt được, ngày hoàn thành

### 2. Xuất báo cáo minh chứng
- **Mô tả**: Xuất danh sách tất cả minh chứng với thông tin chi tiết
- **Dữ liệu bao gồm**:
  - Thông tin sinh viên: Mã SV, Họ tên
  - Thông tin minh chứng: Tên minh chứng, tiêu chí, ngày nộp
  - Trạng thái: Trạng thái duyệt, người duyệt, ngày duyệt, ghi chú

### 3. Xuất báo cáo thống kê tổng quan
- **Mô tả**: Xuất báo cáo thống kê với 2 sheet
- **Sheet 1 - Thống kê tổng quan**:
  - Tổng số sinh viên
  - Số sinh viên tham gia
  - Số sinh viên hoàn thành
  - Số sinh viên đạt danh hiệu
  - Tỷ lệ hoàn thành và đạt danh hiệu
- **Sheet 2 - Thống kê theo tiêu chí**:
  - Số minh chứng theo từng tiêu chí
  - Phân loại theo trạng thái (đã duyệt, chờ duyệt, bị từ chối)

## Cách sử dụng

### 1. Truy cập form xuất báo cáo
- Đăng nhập vào hệ thống với tài khoản có quyền xuất báo cáo
- Trong UserDashboard, click vào tab "Báo cáo & Thống kê"
- Click vào button "Tạo báo cáo" hoặc "Xuất Excel"

### 2. Chọn loại báo cáo
- **Báo cáo danh sách sinh viên 5 tốt**: Click button "📊 Báo cáo danh sách sinh viên 5 tốt"
- **Báo cáo minh chứng**: Click button "📋 Báo cáo minh chứng"
- **Báo cáo thống kê**: Click button "📈 Báo cáo thống kê tổng quan"

### 3. Chọn năm học
- Chọn năm học từ dropdown (mặc định là năm học hiện tại)
- Click "Xuất báo cáo" để bắt đầu

### 4. Lưu và mở file
- Hệ thống sẽ tạo file Excel trong thư mục tạm
- Chọn "Có" để mở file ngay sau khi xuất
- File sẽ có tên theo format: `BaoCao_[Loai]_[NamHoc]_[NgayGio].xlsx`

## Ưu điểm của hệ thống mới

### 1. Đơn giản hơn
- Không cần cấu hình license như EPPlus
- Sử dụng ClosedXML - thư viện miễn phí và ổn định
- Code dễ hiểu và bảo trì

### 2. Hiệu suất tốt
- Xử lý nhanh với dữ liệu lớn
- Tự động điều chỉnh độ rộng cột
- Hỗ trợ định dạng màu sắc theo trạng thái

### 3. Tính năng phong phú
- Nhiều loại báo cáo khác nhau
- Thống kê chi tiết theo tiêu chí
- Xuất file với tên tự động

## Cấu trúc file

### Services/ReportDtos.cs
- Chứa các DTO cho báo cáo
- Định nghĩa cấu trúc dữ liệu xuất

### Services/SimpleReportService.cs
- Service chính xử lý xuất báo cáo
- Các method xuất từng loại báo cáo
- Xử lý dữ liệu và tạo file Excel

### Winform/SimpleReportForm.cs
- Form giao diện xuất báo cáo
- Xử lý tương tác người dùng
- Gọi service để xuất báo cáo

## Lưu ý kỹ thuật

### 1. Dependencies
- **ClosedXML**: Thư viện chính để xử lý Excel
- **Entity Framework**: Truy vấn dữ liệu từ database
- **System.IO**: Xử lý file

### 2. Performance
- Sử dụng `AsNoTracking()` cho các query chỉ đọc
- Tối ưu hóa Include để tránh N+1 problem
- Xử lý bất đồng bộ với async/await

### 3. Error Handling
- Try-catch cho tất cả operations
- Hiển thị thông báo lỗi rõ ràng
- Fallback values cho dữ liệu null

## Troubleshooting

### 1. Lỗi "Không thể tạo file báo cáo"
- Kiểm tra quyền ghi file trong thư mục tạm
- Đảm bảo đủ dung lượng ổ cứng
- Kiểm tra kết nối database

### 2. Lỗi "Không tìm thấy dữ liệu"
- Kiểm tra năm học đã chọn có dữ liệu không
- Kiểm tra quyền truy cập dữ liệu của user
- Kiểm tra kết nối database

### 3. File Excel bị lỗi
- Đảm bảo đã cài đặt Microsoft Excel hoặc LibreOffice
- Kiểm tra file không bị corrupt
- Thử xuất lại với dữ liệu ít hơn

## Kết luận

Hệ thống xuất báo cáo đơn giản đã được thiết kế để thay thế EPPlus một cách hiệu quả, cung cấp đầy đủ tính năng cần thiết với độ phức tạp thấp hơn và hiệu suất tốt hơn.
