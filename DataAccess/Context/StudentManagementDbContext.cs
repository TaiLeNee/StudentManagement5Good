using Microsoft.EntityFrameworkCore;
using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5GoodTempp.DataAccess.Context
{
    public class StudentManagementDbContext : DbContext
    {
        public StudentManagementDbContext(DbContextOptions<StudentManagementDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<ThanhPho> ThanhPhos { get; set; }
        public DbSet<Truong> Truongs { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<TieuChi> TieuChis { get; set; }
        public DbSet<CapXet> CapXets { get; set; }
        public DbSet<NamHoc> NamHocs { get; set; }
        public DbSet<TieuChiYeuCau> TieuChiYeuCaus { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<MinhChung> MinhChungs { get; set; }
        public DbSet<KetQuaXetDuyet> KetQuaXetDuyets { get; set; }
        public DbSet<KetQuaDanhHieu> KetQuaDanhHieus { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình composite key cho TieuChiYeuCau
            modelBuilder.Entity<TieuChiYeuCau>()
                .HasKey(t => new { t.MaTC, t.MaCap });

            // Cấu hình ràng buộc toàn vẹn và cascade delete
            ConfigureRelationships(modelBuilder);

            // Cấu hình ràng buộc dữ liệu
            ConfigureConstraints(modelBuilder);

            // Seed dữ liệu mẫu
            SeedData(modelBuilder);
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // ThanhPho - Truong (1-n)
            modelBuilder.Entity<Truong>()
                .HasOne(t => t.ThanhPho)
                .WithMany(tp => tp.Truongs)
                .HasForeignKey(t => t.MaTP)
                .OnDelete(DeleteBehavior.Restrict);

            // Truong - Khoa (1-n)
            modelBuilder.Entity<Khoa>()
                .HasOne(k => k.Truong)
                .WithMany(t => t.Khoas)
                .HasForeignKey(k => k.MaTruong)
                .OnDelete(DeleteBehavior.Restrict);

            // Khoa - Lop (1-n)
            modelBuilder.Entity<Lop>()
                .HasOne(l => l.Khoa)
                .WithMany(k => k.Lops)
                .HasForeignKey(l => l.MaKhoa)
                .OnDelete(DeleteBehavior.Restrict);

            // Lop - SinhVien (1-n)
            modelBuilder.Entity<SinhVien>()
                .HasOne(s => s.Lop)
                .WithMany(l => l.SinhViens)
                .HasForeignKey(s => s.MaLop)
                .OnDelete(DeleteBehavior.Restrict);

            // User relationships
            modelBuilder.Entity<User>()
                .HasOne(u => u.ThanhPho)
                .WithMany(tp => tp.Users)
                .HasForeignKey(u => u.MaTP)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Truong)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.MaTruong)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Khoa)
                .WithMany(k => k.Users)
                .HasForeignKey(u => u.MaKhoa)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Lop)
                .WithMany()
                .HasForeignKey(u => u.MaLop)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.SinhVien)
                .WithMany()
                .HasForeignKey(u => u.MaSV)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.CapXet)
                .WithMany()
                .HasForeignKey(u => u.CapQuanLy)
                .OnDelete(DeleteBehavior.SetNull);

            // TieuChi - TieuChiYeuCau (1-n)
            modelBuilder.Entity<TieuChiYeuCau>()
                .HasOne(ty => ty.TieuChi)
                .WithMany(t => t.TieuChiYeuCaus)
                .HasForeignKey(ty => ty.MaTC)
                .OnDelete(DeleteBehavior.Cascade);

            // CapXet - TieuChiYeuCau (1-n)
            modelBuilder.Entity<TieuChiYeuCau>()
                .HasOne(ty => ty.CapXet)
                .WithMany(c => c.TieuChiYeuCaus)
                .HasForeignKey(ty => ty.MaCap)
                .OnDelete(DeleteBehavior.Cascade);

            // DanhGia relationships
            modelBuilder.Entity<DanhGia>()
                .HasOne(d => d.SinhVien)
                .WithMany(s => s.DanhGias)
                .HasForeignKey(d => d.MaSV)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DanhGia>()
                .HasOne(d => d.TieuChi)
                .WithMany(t => t.DanhGias)
                .HasForeignKey(d => d.MaTC)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DanhGia>()
                .HasOne(d => d.CapXet)
                .WithMany(c => c.DanhGias)
                .HasForeignKey(d => d.MaCap)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DanhGia>()
                .HasOne(d => d.NamHoc)
                .WithMany(n => n.DanhGias)
                .HasForeignKey(d => d.MaNH)
                .OnDelete(DeleteBehavior.Restrict);

            // MinhChung - DanhGia (n-1)
            // MinhChung relationships - Thiết kế mới: MinhChung độc lập
            modelBuilder.Entity<MinhChung>()
                .HasOne(m => m.SinhVien)
                .WithMany(s => s.MinhChungs)
                .HasForeignKey(m => m.MaSV)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MinhChung>()
                .HasOne(m => m.TieuChi)
                .WithMany(t => t.MinhChungs)
                .HasForeignKey(m => m.MaTC)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MinhChung>()
                .HasOne(m => m.NamHoc)
                .WithMany(n => n.MinhChungs)
                .HasForeignKey(m => m.MaNH)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MinhChung>()
                .HasOne(m => m.NguoiDuyetUser)
                .WithMany()
                .HasForeignKey(m => m.NguoiDuyet)
                .OnDelete(DeleteBehavior.SetNull);

            // KetQuaXetDuyet relationships
            modelBuilder.Entity<KetQuaXetDuyet>()
                .HasOne(k => k.SinhVien)
                .WithMany(s => s.KetQuaXetDuyets)
                .HasForeignKey(k => k.MaSV)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KetQuaXetDuyet>()
                .HasOne(k => k.TieuChi)
                .WithMany(t => t.KetQuaXetDuyets)
                .HasForeignKey(k => k.MaTC)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KetQuaXetDuyet>()
                .HasOne(k => k.CapXet)
                .WithMany(c => c.KetQuaXetDuyets)
                .HasForeignKey(k => k.MaCap)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KetQuaXetDuyet>()
                .HasOne(k => k.NamHoc)
                .WithMany(n => n.KetQuaXetDuyets)
                .HasForeignKey(k => k.MaNH)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KetQuaXetDuyet>()
                .HasOne(k => k.NguoiXetDuyetUser)
                .WithMany()
                .HasForeignKey(k => k.NguoiXetDuyet)
                .OnDelete(DeleteBehavior.Restrict);

            // KetQuaDanhHieu relationships
            modelBuilder.Entity<KetQuaDanhHieu>()
                .HasOne(k => k.SinhVien)
                .WithMany(s => s.KetQuaDanhHieus)
                .HasForeignKey(k => k.MaSV)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KetQuaDanhHieu>()
                .HasOne(k => k.CapXet)
                .WithMany(c => c.KetQuaDanhHieus)
                .HasForeignKey(k => k.MaCap)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KetQuaDanhHieu>()
                .HasOne(k => k.NamHoc)
                .WithMany(n => n.KetQuaDanhHieus)
                .HasForeignKey(k => k.MaNH)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureConstraints(ModelBuilder modelBuilder)
        {
            // Ràng buộc cho SinhVien
            modelBuilder.Entity<SinhVien>()
                .Property(s => s.Email)
                .HasAnnotation("RegularExpression", @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            modelBuilder.Entity<SinhVien>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<SinhVien>()
                .HasIndex(s => s.SoDienThoai)
                .IsUnique();

            // Ràng buộc cho User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.NgayTao)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>()
                .Property(u => u.TrangThai)
                .HasDefaultValue(true);

            // Ràng buộc cho TieuChi
            modelBuilder.Entity<TieuChi>()
                .Property(t => t.LoaiDuLieu)
                .HasMaxLength(20);

            // Ràng buộc cho NamHoc
            modelBuilder.Entity<NamHoc>()
                .HasCheckConstraint("CK_NamHoc_TuNgay_DenNgay", "tuNgay < denNgay");

            // Ràng buộc cho DanhGia
            modelBuilder.Entity<DanhGia>()
                .Property(d => d.NgayDanhGia)
                .HasDefaultValueSql("GETDATE()");

            // Ràng buộc cho KetQuaDanhHieu
            modelBuilder.Entity<KetQuaDanhHieu>()
                .Property(k => k.DatDanhHieu)
                .HasDefaultValue(false);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed ThanhPho
            modelBuilder.Entity<ThanhPho>().HasData(
                new ThanhPho 
                { 
                    MaTP = "HN", 
                    TenThanhPho = "Hà Nội", 
                    MaVung = "BAC", 
                    TenVung = "Miền Bắc",
                    ChuTichDoanTP = "Nguyễn Văn Hà",
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0)
                },
                new ThanhPho 
                { 
                    MaTP = "HCM", 
                    TenThanhPho = "Hồ Chí Minh", 
                    MaVung = "NAM", 
                    TenVung = "Miền Nam",
                    ChuTichDoanTP = "Trần Thị Minh",
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0)
                },
                new ThanhPho 
                { 
                    MaTP = "DN", 
                    TenThanhPho = "Đà Nẵng", 
                    MaVung = "TRUNG", 
                    TenVung = "Miền Trung",
                    ChuTichDoanTP = "Lê Văn Trung",
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0)
                }
            );

            // Seed Truong
            modelBuilder.Entity<Truong>().HasData(
                new Truong 
                { 
                    MaTruong = "UTE", 
                    TenTruong = "Đại học Sư phạm Kỹ thuật TP.HCM", 
                    TenVietTat = "UTE",
                    LoaiTruong = "Đại học",
                    HieuTruong = "PGS.TS Nguyễn Văn Hiệu",
                    BiThuDoan = "ThS. Trần Văn Bí",
                    MaTP = "HCM",
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0)
                },
                new Truong 
                { 
                    MaTruong = "HUST", 
                    TenTruong = "Đại học Bách khoa Hà Nội", 
                    TenVietTat = "HUST",
                    LoaiTruong = "Đại học",
                    HieuTruong = "PGS.TS Lê Văn Hiệu",
                    BiThuDoan = "ThS. Nguyễn Thị Bí",
                    MaTP = "HN",
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0)
                }
            );

            // Seed Khoa
            modelBuilder.Entity<Khoa>().HasData(
                new Khoa 
                { 
                    MaKhoa = "CNTT", 
                    TenKhoa = "Công nghệ thông tin", 
                    TruongKhoa = "PGS.TS Nguyễn Văn A",
                    BiThuDoanKhoa = "ThS. Trần Văn B",
                    MaTruong = "UTE",
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0)
                },
                new Khoa 
                { 
                    MaKhoa = "KTXD", 
                    TenKhoa = "Kỹ thuật xây dựng", 
                    TruongKhoa = "PGS.TS Trần Văn B",
                    BiThuDoanKhoa = "ThS. Lê Thị C",
                    MaTruong = "UTE",
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0)
                },
                new Khoa 
                { 
                    MaKhoa = "QTKD", 
                    TenKhoa = "Quản trị kinh doanh", 
                    TruongKhoa = "TS. Lê Thị C",
                    BiThuDoanKhoa = "ThS. Phạm Văn D",
                    MaTruong = "HUST",
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0)
                }
            );

            // Seed CapXet
            modelBuilder.Entity<CapXet>().HasData(
                new CapXet { MaCap = "LOP", TenCap = "Cấp lớp" },
                new CapXet { MaCap = "KHOA", TenCap = "Cấp khoa" },
                new CapXet { MaCap = "TRUONG", TenCap = "Cấp trường" },
                new CapXet { MaCap = "TP", TenCap = "Cấp thành phố" },
                new CapXet { MaCap = "TU", TenCap = "Cấp trung ương" }
            );

            // Seed TieuChi
            modelBuilder.Entity<TieuChi>().HasData(
                new TieuChi
                {
                    MaTC = "TC01",
                    TenTieuChi = "Đạo đức tốt",
                    MoTa = "Có lối sống, đạo đức tốt",
                    LoaiDuLieu = "boolean",
                    DonViTinh = "Có/Không"
                },
                new TieuChi
                {
                    MaTC = "TC02",
                    TenTieuChi = "Học tập tốt",
                    MoTa = "Kết quả học tập xuất sắc",
                    LoaiDuLieu = "so",
                    DonViTinh = "Điểm"
                },
                new TieuChi
                {
                    MaTC = "TC03",
                    TenTieuChi = "Thể lực tốt",
                    MoTa = "Tích cực tham gia thể thao",
                    LoaiDuLieu = "so",
                    DonViTinh = "Giờ"
                },
                new TieuChi
                {
                    MaTC = "TC04",
                    TenTieuChi = "Tình nguyện tốt",
                    MoTa = "Tích cực tham gia hoạt động tình nguyện",
                    LoaiDuLieu = "so",
                    DonViTinh = "Giờ"
                },
                new TieuChi
                {
                    MaTC = "TC05",
                    TenTieuChi = "Hội nhập tốt",
                    MoTa = "Tích cực tham gia các hoạt động xã hội",
                    LoaiDuLieu = "so",
                    DonViTinh = "Điểm"
                }
            );

            // Seed NamHoc
            modelBuilder.Entity<NamHoc>().HasData(
                new NamHoc
                {
                    MaNH = "2023-2024",
                    TenNamHoc = "2023-2024",
                    TuNgay = new DateTime(2023, 9, 1),
                    DenNgay = new DateTime(2024, 8, 31)
                },
                new NamHoc
                {
                    MaNH = "2024-2025",
                    TenNamHoc = "2024-2025",
                    TuNgay = new DateTime(2024, 9, 1),
                    DenNgay = new DateTime(2025, 8, 31)
                }
            );

            // Seed Users (Default accounts)
            // Note: Passwords are hashed using SHA256 with salt "SALT_5TOT"
            // Original passwords: admin123, giaovu123, doan123, 123
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = "U001",
                    Username = "admin",
                    Password = "jQ3FWcF+tp1pMz4nGBxByN6G12RjozQ8GdmZmKGNl/8=", // admin123 hashed
                    HoTen = "Quản trị hệ thống",
                    Email = "admin@university.edu.vn",
                    VaiTro = UserRoles.ADMIN,
                    TrangThai = true,
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0),
                    GhiChu = "Tài khoản quản trị mặc định"
                },
                new User
                {
                    UserId = "U002",
                    Username = "giaovu_cntt",
                    Password = "EYuwFdNsB9ry7GqGj8bXgKw8vGNKsEc9vt0lxZmV8sE=", // giaovu123 hashed
                    HoTen = "Giáo vụ CNTT",
                    Email = "giaovu.cntt@university.edu.vn",
                    VaiTro = UserRoles.GIAOVU,
                    CapQuanLy = ManagementLevels.KHOA,
                    MaKhoa = "CNTT",
                    MaTruong = "UTE",
                    MaTP = "HCM",
                    TrangThai = true,
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0),
                    GhiChu = "Giáo vụ khoa Công nghệ thông tin"
                },
                new User
                {
                    UserId = "U003",
                    Username = "doankhoa_cntt",
                    Password = "2mGxKwSGxrMT9xYXGdO2KCK1E+0/lQFB7F8k2WhCn/I=", // doan123 hashed
                    HoTen = "Đoàn khoa CNTT",
                    Email = "doan.cntt@university.edu.vn",
                    VaiTro = UserRoles.DOANKHOA,
                    CapQuanLy = ManagementLevels.KHOA,
                    MaKhoa = "CNTT",
                    MaTruong = "UTE",
                    MaTP = "HCM",
                    TrangThai = true,
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0),
                    GhiChu = "Đoàn khoa Công nghệ thông tin"
                },
                new User
                {
                    UserId = "U004",
                    Username = "test",
                    Password = "v5sNesN3/GHRx5CKght8kLDb4+9hNFZGoJiWS8fUNhA=", // 123 hashed
                    HoTen = "User Test",
                    Email = "test@university.edu.vn",
                    VaiTro = UserRoles.CVHT,
                    CapQuanLy = ManagementLevels.LOP,
                    MaKhoa = "CNTT",
                    MaTruong = "UTE",
                    MaTP = "HCM",
                    TrangThai = true,
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0),
                    GhiChu = "Tài khoản test đăng nhập"
                },
                new User
                {
                    UserId = "U005",
                    Username = "doantruong_ute",
                    Password = "2mGxKwSGxrMT9xYXGdO2KCK1E+0/lQFB7F8k2WhCn/I=", // doan123 hashed
                    HoTen = "Đoàn trường UTE",
                    Email = "doan.truong@ute.edu.vn",
                    VaiTro = UserRoles.DOANTRUONG,
                    CapQuanLy = ManagementLevels.TRUONG,
                    MaTruong = "UTE",
                    MaTP = "HCM",
                    TrangThai = true,
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0),
                    GhiChu = "Đoàn trường UTE"
                },
                new User
                {
                    UserId = "U006",
                    Username = "doantp_hcm",
                    Password = "2mGxKwSGxrMT9xYXGdO2KCK1E+0/lQFB7F8k2WhCn/I=", // doan123 hashed
                    HoTen = "Đoàn thành phố HCM",
                    Email = "doan.tp@hcm.gov.vn",
                    VaiTro = UserRoles.DOANTP,
                    CapQuanLy = ManagementLevels.TP,
                    MaTP = "HCM",
                    TrangThai = true,
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0),
                    GhiChu = "Đoàn thành phố Hồ Chí Minh"
                }
            );
        }
    }
}
