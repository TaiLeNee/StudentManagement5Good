# Script SQL Database - Hệ thống Quản lý Sinh viên 5 Tốt

## Tổng quan
Script SQL này tạo ra database cho hệ thống quản lý sinh viên 5 tốt, bao gồm các bảng quản lý thông tin sinh viên, tiêu chí đánh giá, minh chứng và kết quả xét duyệt.

## Cấu trúc Database

### 1. Bảng THANHPHO (Thành phố)
```sql
CREATE TABLE THANHPHO (
    maTP NVARCHAR(20) PRIMARY KEY,
    tenThanhPho NVARCHAR(100) NOT NULL,
    maVung NVARCHAR(20),
    tenVung NVARCHAR(50),
    chuTichDoanTP NVARCHAR(100),
    soDienThoai NVARCHAR(15),
    email NVARCHAR(100),
    diaChi NVARCHAR(200),
    trangThai BIT NOT NULL DEFAULT 1,
    ngayTao DATETIME NOT NULL DEFAULT GETDATE(),
    ngayCapNhat DATETIME,
    ghiChu NVARCHAR(500)
);
```

### 2. Bảng TRUONG (Trường)
```sql
CREATE TABLE TRUONG (
    maTruong NVARCHAR(20) PRIMARY KEY,
    tenTruong NVARCHAR(200) NOT NULL,
    tenVietTat NVARCHAR(50),
    loaiTruong NVARCHAR(50),
    hieuTruong NVARCHAR(100),
    biThuDoan NVARCHAR(100),
    soDienThoai NVARCHAR(15),
    email NVARCHAR(100),
    website NVARCHAR(200),
    diaChi NVARCHAR(300),
    maTP NVARCHAR(20) NOT NULL,
    trangThai BIT NOT NULL DEFAULT 1,
    ngayThanhLap DATETIME,
    ngayTao DATETIME NOT NULL DEFAULT GETDATE(),
    ngayCapNhat DATETIME,
    ghiChu NVARCHAR(500),
    
    FOREIGN KEY (maTP) REFERENCES THANHPHO(maTP)
);
```

### 3. Bảng KHOA (Khoa)
```sql
CREATE TABLE KHOA (
    maKhoa NVARCHAR(20) PRIMARY KEY,
    tenKhoa NVARCHAR(100) NOT NULL,
    truongKhoa NVARCHAR(100),
    biThuDoanKhoa NVARCHAR(100),
    soDienThoai NVARCHAR(15),
    email NVARCHAR(100),
    maTruong NVARCHAR(20) NOT NULL,
    trangThai BIT NOT NULL DEFAULT 1,
    ngayThanhLap DATETIME,
    ngayTao DATETIME NOT NULL DEFAULT GETDATE(),
    ngayCapNhat DATETIME,
    ghiChu NVARCHAR(500),
    
    FOREIGN KEY (maTruong) REFERENCES TRUONG(maTruong)
);
```

### 4. Bảng LOP (Lớp)
```sql
CREATE TABLE LOP (
    maLop NVARCHAR(20) PRIMARY KEY,
    tenLop NVARCHAR(100) NOT NULL,
    GVCN NVARCHAR(100),
    maKhoa NVARCHAR(20) NOT NULL,
    
    FOREIGN KEY (maKhoa) REFERENCES KHOA(maKhoa)
);
```

### 5. Bảng SINHVIEN (Sinh viên)
```sql
CREATE TABLE SINHVIEN (
    maSV NVARCHAR(20) PRIMARY KEY,
    hoTen NVARCHAR(100) NOT NULL,
    ngaySinh DATETIME NOT NULL,
    gioiTinh NVARCHAR(10),
    email NVARCHAR(100),
    soDienThoai NVARCHAR(15),
    maLop NVARCHAR(20) NOT NULL,
    
    FOREIGN KEY (maLop) REFERENCES LOP(maLop),
    UNIQUE (email),
    UNIQUE (soDienThoai)
);
```

### 6. Bảng CAPXET (Cấp xét)
```sql
CREATE TABLE CAPXET (
    maCap NVARCHAR(20) PRIMARY KEY,
    tenCap NVARCHAR(100) NOT NULL
);
```

### 7. Bảng TIEUCHI (Tiêu chí)
```sql
CREATE TABLE TIEUCHI (
    maTC NVARCHAR(20) PRIMARY KEY,
    tenTieuChi NVARCHAR(100) NOT NULL,
    moTa NVARCHAR(500),
    loaiDuLieu NVARCHAR(20),
    donViTinh NVARCHAR(50)
);
```

### 8. Bảng NAMHOC (Năm học)
```sql
CREATE TABLE NAMHOC (
    maNH NVARCHAR(20) PRIMARY KEY,
    tenNamHoc NVARCHAR(20) NOT NULL,
    tuNgay DATETIME NOT NULL,
    denNgay DATETIME NOT NULL,
    
    CHECK (tuNgay < denNgay)
);
```

### 9. Bảng TIEUCHIYEUCAU (Tiêu chí yêu cầu)
```sql
CREATE TABLE TIEUCHIYEUCAU (
    maTC NVARCHAR(20),
    maCap NVARCHAR(20),
    nguongDat FLOAT,
    batBuoc BIT NOT NULL DEFAULT 0,
    moTaYeuCau NVARCHAR(500),
    
    PRIMARY KEY (maTC, maCap),
    FOREIGN KEY (maTC) REFERENCES TIEUCHI(maTC),
    FOREIGN KEY (maCap) REFERENCES CAPXET(maCap)
);
```

### 10. Bảng DANHGIA (Đánh giá)
```sql
CREATE TABLE DANHGIA (
    maDG NVARCHAR(20) PRIMARY KEY,
    maSV NVARCHAR(20) NOT NULL,
    maTC NVARCHAR(20) NOT NULL,
    maCap NVARCHAR(20) NOT NULL,
    maNH NVARCHAR(20) NOT NULL,
    giaTri NVARCHAR(255),
    datTieuChi BIT NOT NULL DEFAULT 0,
    ngayDanhGia DATETIME NOT NULL DEFAULT GETDATE(),
    nguoiDanhGia NVARCHAR(100),
    
    FOREIGN KEY (maSV) REFERENCES SINHVIEN(maSV),
    FOREIGN KEY (maTC) REFERENCES TIEUCHI(maTC),
    FOREIGN KEY (maCap) REFERENCES CAPXET(maCap),
    FOREIGN KEY (maNH) REFERENCES NAMHOC(maNH)
);
```

### 11. Bảng MINHCHUNG (Minh chứng)
```sql
CREATE TABLE MINHCHUNG (
    maMC NVARCHAR(20) PRIMARY KEY,
    maSV NVARCHAR(20) NOT NULL,
    maTC NVARCHAR(20) NOT NULL,
    maNH NVARCHAR(20) NOT NULL,
    tenMinhChung NVARCHAR(255) NOT NULL,
    duongDanFile NVARCHAR(500),
    tenFile NVARCHAR(255),
    loaiFile NVARCHAR(10),
    kichThuocFile BIGINT,
    moTa NVARCHAR(1000),
    trangThai INT NOT NULL DEFAULT 0, -- 0: ChoDuyet, 1: DaDuyet, 2: BiTuChoi, 3: CanBoSung
    lyDoTuChoi NVARCHAR(1000),
    ngayNop DATETIME NOT NULL DEFAULT GETDATE(),
    ngayDuyet DATETIME,
    nguoiDuyet NVARCHAR(50),
    ghiChu NVARCHAR(1000),
    
    FOREIGN KEY (maSV) REFERENCES SINHVIEN(maSV),
    FOREIGN KEY (maTC) REFERENCES TIEUCHI(maTC),
    FOREIGN KEY (maNH) REFERENCES NAMHOC(maNH),
    FOREIGN KEY (nguoiDuyet) REFERENCES USER(userId)
);
```

### 12. Bảng KETQUAXETDUYET (Kết quả xét duyệt)
```sql
CREATE TABLE KETQUAXETDUYET (
    maKQ NVARCHAR(20) PRIMARY KEY,
    maSV NVARCHAR(20) NOT NULL,
    maTC NVARCHAR(20) NOT NULL,
    maCap NVARCHAR(20) NOT NULL,
    maNH NVARCHAR(20) NOT NULL,
    ketQua BIT NOT NULL DEFAULT 0,
    diem DECIMAL(10,2),
    xepLoai NVARCHAR(50),
    ghiChu NVARCHAR(1000),
    ngayXetDuyet DATETIME NOT NULL DEFAULT GETDATE(),
    nguoiXetDuyet NVARCHAR(50) NOT NULL,
    soMinhChungDaDuyet INT NOT NULL DEFAULT 0,
    tongSoMinhChung INT NOT NULL DEFAULT 0,
    lyDoKhongDat NVARCHAR(1000),
    danhSachMinhChung NVARCHAR(2000),
    trangThaiHoSo NVARCHAR(50) NOT NULL DEFAULT 'HOAN_THANH',
    
    FOREIGN KEY (maSV) REFERENCES SINHVIEN(maSV),
    FOREIGN KEY (maTC) REFERENCES TIEUCHI(maTC),
    FOREIGN KEY (maCap) REFERENCES CAPXET(maCap),
    FOREIGN KEY (maNH) REFERENCES NAMHOC(maNH),
    FOREIGN KEY (nguoiXetDuyet) REFERENCES USER(userId)
);
```

### 13. Bảng KETQUADANHHIEU (Kết quả danh hiệu)
```sql
CREATE TABLE KETQUADANHHIEU (
    maKQ NVARCHAR(20) PRIMARY KEY,
    maSV NVARCHAR(20) NOT NULL,
    maCap NVARCHAR(20) NOT NULL,
    maNH NVARCHAR(20) NOT NULL,
    datDanhHieu BIT NOT NULL DEFAULT 0,
    ngayDat DATETIME,
    ghiChu NVARCHAR(500),
    
    FOREIGN KEY (maSV) REFERENCES SINHVIEN(maSV),
    FOREIGN KEY (maCap) REFERENCES CAPXET(maCap),
    FOREIGN KEY (maNH) REFERENCES NAMHOC(maNH)
);
```

### 14. Bảng USER (Người dùng)
```sql
CREATE TABLE USER (
    userId NVARCHAR(50) PRIMARY KEY,
    username NVARCHAR(50) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    hoTen NVARCHAR(100) NOT NULL,
    email NVARCHAR(100),
    soDienThoai NVARCHAR(15),
    vaiTro NVARCHAR(20) NOT NULL, -- ADMIN, GIAOVU, CVHT, SINHVIEN, DOANKHOA, DOANTRUONG, DOANTP, DOANTU
    capQuanLy NVARCHAR(20), -- LOP, KHOA, TRUONG, TP, TU
    maKhoa NVARCHAR(20),
    maLop NVARCHAR(20),
    maTruong NVARCHAR(20),
    maTP NVARCHAR(20),
    maSV NVARCHAR(20),
    trangThai BIT NOT NULL DEFAULT 1,
    ngayTao DATETIME NOT NULL DEFAULT GETDATE(),
    ngayCapNhat DATETIME,
    lanDangNhapCuoi DATETIME,
    ghiChu NVARCHAR(500),
    
    UNIQUE (username),
    UNIQUE (email),
    FOREIGN KEY (maKhoa) REFERENCES KHOA(maKhoa),
    FOREIGN KEY (maLop) REFERENCES LOP(maLop),
    FOREIGN KEY (maTruong) REFERENCES TRUONG(maTruong),
    FOREIGN KEY (maTP) REFERENCES THANHPHO(maTP),
    FOREIGN KEY (maSV) REFERENCES SINHVIEN(maSV),
    FOREIGN KEY (capQuanLy) REFERENCES CAPXET(maCap)
);
```

## Dữ liệu mẫu (Seed Data)

### 1. Dữ liệu thành phố
```sql
INSERT INTO THANHPHO (maTP, tenThanhPho, maVung, tenVung, chuTichDoanTP, ngayTao) VALUES
('HN', N'Hà Nội', 'BAC', N'Miền Bắc', N'Nguyễn Văn Hà', '2024-01-01'),
('HCM', N'Hồ Chí Minh', 'NAM', N'Miền Nam', N'Trần Thị Minh', '2024-01-01'),
('DN', N'Đà Nẵng', 'TRUNG', N'Miền Trung', N'Lê Văn Trung', '2024-01-01');
```

### 2. Dữ liệu trường
```sql
INSERT INTO TRUONG (maTruong, tenTruong, tenVietTat, loaiTruong, hieuTruong, biThuDoan, maTP, ngayTao) VALUES
('UTE', N'Đại học Sư phạm Kỹ thuật TP.HCM', 'UTE', N'Đại học', N'PGS.TS Nguyễn Văn Hiệu', N'ThS. Trần Văn Bí', 'HCM', '2024-01-01'),
('HUST', N'Đại học Bách khoa Hà Nội', 'HUST', N'Đại học', N'PGS.TS Lê Văn Hiệu', N'ThS. Nguyễn Thị Bí', 'HN', '2024-01-01');
```

### 3. Dữ liệu khoa
```sql
INSERT INTO KHOA (maKhoa, tenKhoa, truongKhoa, biThuDoanKhoa, maTruong, ngayTao) VALUES
('CNTT', N'Công nghệ thông tin', N'PGS.TS Nguyễn Văn A', N'ThS. Trần Văn B', 'UTE', '2024-01-01'),
('KTXD', N'Kỹ thuật xây dựng', N'PGS.TS Trần Văn B', N'ThS. Lê Thị C', 'UTE', '2024-01-01'),
('QTKD', N'Quản trị kinh doanh', N'TS. Lê Thị C', N'ThS. Phạm Văn D', 'HUST', '2024-01-01');
```

### 4. Dữ liệu cấp xét
```sql
INSERT INTO CAPXET (maCap, tenCap) VALUES
('LOP', N'Cấp lớp'),
('KHOA', N'Cấp khoa'),
('TRUONG', N'Cấp trường'),
('TP', N'Cấp thành phố'),
('TU', N'Cấp trung ương');
```

### 5. Dữ liệu tiêu chí
```sql
INSERT INTO TIEUCHI (maTC, tenTieuChi, moTa, loaiDuLieu, donViTinh) VALUES
('TC01', N'Đạo đức tốt', N'Có lối sống, đạo đức tốt', 'boolean', N'Có/Không'),
('TC02', N'Học tập tốt', N'Kết quả học tập xuất sắc', 'so', N'Điểm'),
('TC03', N'Thể lực tốt', N'Tích cực tham gia thể thao', 'so', N'Giờ'),
('TC04', N'Tình nguyện tốt', N'Tích cực tham gia hoạt động tình nguyện', 'so', N'Giờ'),
('TC05', N'Hội nhập tốt', N'Tích cực tham gia các hoạt động xã hội', 'so', N'Điểm');
```

### 6. Dữ liệu năm học
```sql
INSERT INTO NAMHOC (maNH, tenNamHoc, tuNgay, denNgay) VALUES
('2023-2024', '2023-2024', '2023-09-01', '2024-08-31'),
('2024-2025', '2024-2025', '2024-09-01', '2025-08-31');
```

### 7. Dữ liệu người dùng mặc định
```sql
-- Mật khẩu đã được hash bằng SHA256 với salt "SALT_5TOT"
-- Mật khẩu gốc: admin123, giaovu123, doan123, 123
INSERT INTO USER (userId, username, password, hoTen, email, vaiTro, trangThai, ngayTao, ghiChu) VALUES
('U001', 'admin', 'jQ3FWcF+tp1pMz4nGBxByN6G12RjozQ8GdmZmKGNl/8=', N'Quản trị hệ thống', 'admin@university.edu.vn', 'ADMIN', 1, '2024-01-01', N'Tài khoản quản trị mặc định');

INSERT INTO USER (userId, username, password, hoTen, email, vaiTro, capQuanLy, maKhoa, maTruong, maTP, trangThai, ngayTao, ghiChu) VALUES
('U002', 'giaovu_cntt', 'EYuwFdNsB9ry7GqGj8bXgKw8vGNKsEc9vt0lxZmV8sE=', N'Giáo vụ CNTT', 'giaovu.cntt@university.edu.vn', 'GIAOVU', 'KHOA', 'CNTT', 'UTE', 'HCM', 1, '2024-01-01', N'Giáo vụ khoa Công nghệ thông tin'),
('U003', 'doankhoa_cntt', '2mGxKwSGxrMT9xYXGdO2KCK1E+0/lQFB7F8k2WhCn/I=', N'Đoàn khoa CNTT', 'doan.cntt@university.edu.vn', 'DOANKHOA', 'KHOA', 'CNTT', 'UTE', 'HCM', 1, '2024-01-01', N'Đoàn khoa Công nghệ thông tin'),
('U004', 'test', 'v5sNesN3/GHRx5CKght8kLDb4+9hNFZGoJiWS8fUNhA=', N'User Test', 'test@university.edu.vn', 'CVHT', 'LOP', 'CNTT', 'UTE', 'HCM', 1, '2024-01-01', N'Tài khoản test đăng nhập'),
('U005', 'doantruong_ute', '2mGxKwSGxrMT9xYXGdO2KCK1E+0/lQFB7F8k2WhCn/I=', N'Đoàn trường UTE', 'doan.truong@ute.edu.vn', 'DOANTRUONG', 'TRUONG', 'UTE', 'HCM', 1, '2024-01-01', N'Đoàn trường UTE'),
('U006', 'doantp_hcm', '2mGxKwSGxrMT9xYXGdO2KCK1E+0/lQFB7F8k2WhCn/I=', N'Đoàn thành phố HCM', 'doan.tp@hcm.gov.vn', 'DOANTP', 'TP', 'HCM', 1, '2024-01-01', N'Đoàn thành phố Hồ Chí Minh');
```

## Indexes và Constraints

### 1. Indexes cho hiệu suất
```sql
-- Index cho email sinh viên
CREATE UNIQUE INDEX IX_SINHVIEN_EMAIL ON SINHVIEN(email) WHERE email IS NOT NULL;

-- Index cho số điện thoại sinh viên
CREATE UNIQUE INDEX IX_SINHVIEN_SODT ON SINHVIEN(soDienThoai) WHERE soDienThoai IS NOT NULL;

-- Index cho username và email user
CREATE UNIQUE INDEX IX_USER_USERNAME ON USER(username);
CREATE UNIQUE INDEX IX_USER_EMAIL ON USER(email) WHERE email IS NOT NULL;

-- Index cho các khóa ngoại thường xuyên truy vấn
CREATE INDEX IX_SINHVIEN_MALOP ON SINHVIEN(maLop);
CREATE INDEX IX_MINHCHUNG_MASV ON MINHCHUNG(maSV);
CREATE INDEX IX_MINHCHUNG_MATC ON MINHCHUNG(maTC);
CREATE INDEX IX_MINHCHUNG_MANH ON MINHCHUNG(maNH);
CREATE INDEX IX_DANHGIA_MASV ON DANHGIA(maSV);
CREATE INDEX IX_KETQUAXETDUYET_MASV ON KETQUAXETDUYET(maSV);
```

### 2. Check Constraints
```sql
-- Kiểm tra định dạng email
ALTER TABLE SINHVIEN ADD CONSTRAINT CK_SINHVIEN_EMAIL 
CHECK (email IS NULL OR email LIKE '%@%.%');

ALTER TABLE USER ADD CONSTRAINT CK_USER_EMAIL 
CHECK (email IS NULL OR email LIKE '%@%.%');

-- Kiểm tra vai trò user
ALTER TABLE USER ADD CONSTRAINT CK_USER_VAITRO 
CHECK (vaiTro IN ('ADMIN', 'GIAOVU', 'CVHT', 'SINHVIEN', 'DOANKHOA', 'DOANTRUONG', 'DOANTP', 'DOANTU'));

-- Kiểm tra cấp quản lý
ALTER TABLE USER ADD CONSTRAINT CK_USER_CAPQUANLY 
CHECK (capQuanLy IS NULL OR capQuanLy IN ('LOP', 'KHOA', 'TRUONG', 'TP', 'TU'));

-- Kiểm tra trạng thái minh chứng
ALTER TABLE MINHCHUNG ADD CONSTRAINT CK_MINHCHUNG_TRANGTHAI 
CHECK (trangThai IN (0, 1, 2, 3));

-- Kiểm tra trạng thái hồ sơ
ALTER TABLE KETQUAXETDUYET ADD CONSTRAINT CK_KETQUAXETDUYET_TRANGTHAIHOSO 
CHECK (trangThaiHoSo IN ('HOAN_THANH', 'CAN_BO_SUNG', 'KHONG_DAT'));
```

## Views hữu ích

### 1. View thông tin sinh viên đầy đủ
```sql
CREATE VIEW V_SINHVIEN_FULL AS
SELECT 
    sv.maSV,
    sv.hoTen,
    sv.ngaySinh,
    sv.gioiTinh,
    sv.email,
    sv.soDienThoai,
    l.tenLop,
    l.GVCN,
    k.tenKhoa,
    t.tenTruong,
    tp.tenThanhPho
FROM SINHVIEN sv
INNER JOIN LOP l ON sv.maLop = l.maLop
INNER JOIN KHOA k ON l.maKhoa = k.maKhoa
INNER JOIN TRUONG t ON k.maTruong = t.maTruong
INNER JOIN THANHPHO tp ON t.maTP = tp.maTP;
```

### 2. View kết quả xét duyệt tổng hợp
```sql
CREATE VIEW V_KETQUA_TONGHOP AS
SELECT 
    kq.maKQ,
    sv.hoTen AS tenSinhVien,
    sv.maSV,
    tc.tenTieuChi,
    cx.tenCap,
    nh.tenNamHoc,
    kq.ketQua,
    kq.diem,
    kq.xepLoai,
    kq.trangThaiHoSo,
    kq.ngayXetDuyet,
    u.hoTen AS tenNguoiXetDuyet
FROM KETQUAXETDUYET kq
INNER JOIN SINHVIEN sv ON kq.maSV = sv.maSV
INNER JOIN TIEUCHI tc ON kq.maTC = tc.maTC
INNER JOIN CAPXET cx ON kq.maCap = cx.maCap
INNER JOIN NAMHOC nh ON kq.maNH = nh.maNH
INNER JOIN USER u ON kq.nguoiXetDuyet = u.userId;
```

## Stored Procedures

### 1. Procedure tạo minh chứng mới
```sql
CREATE PROCEDURE SP_TaoMinhChung
    @maMC NVARCHAR(20),
    @maSV NVARCHAR(20),
    @maTC NVARCHAR(20),
    @maNH NVARCHAR(20),
    @tenMinhChung NVARCHAR(255),
    @duongDanFile NVARCHAR(500) = NULL,
    @tenFile NVARCHAR(255) = NULL,
    @loaiFile NVARCHAR(10) = NULL,
    @kichThuocFile BIGINT = NULL,
    @moTa NVARCHAR(1000) = NULL
AS
BEGIN
    INSERT INTO MINHCHUNG (
        maMC, maSV, maTC, maNH, tenMinhChung, 
        duongDanFile, tenFile, loaiFile, kichThuocFile, moTa
    )
    VALUES (
        @maMC, @maSV, @maTC, @maNH, @tenMinhChung,
        @duongDanFile, @tenFile, @loaiFile, @kichThuocFile, @moTa
    );
END;
```

### 2. Procedure duyệt minh chứng
```sql
CREATE PROCEDURE SP_DuyetMinhChung
    @maMC NVARCHAR(20),
    @nguoiDuyet NVARCHAR(50),
    @trangThai INT, -- 1: DaDuyet, 2: BiTuChoi, 3: CanBoSung
    @lyDoTuChoi NVARCHAR(1000) = NULL,
    @ghiChu NVARCHAR(1000) = NULL
AS
BEGIN
    UPDATE MINHCHUNG 
    SET 
        trangThai = @trangThai,
        ngayDuyet = GETDATE(),
        nguoiDuyet = @nguoiDuyet,
        lyDoTuChoi = @lyDoTuChoi,
        ghiChu = @ghiChu
    WHERE maMC = @maMC;
END;
```

## Triggers

### 1. Trigger cập nhật ngày cập nhật
```sql
CREATE TRIGGER TR_USER_UPDATE_DATE
ON USER
AFTER UPDATE
AS
BEGIN
    UPDATE USER 
    SET ngayCapNhat = GETDATE()
    WHERE userId IN (SELECT userId FROM inserted);
END;
```

### 2. Trigger kiểm tra tính hợp lệ của minh chứng
```sql
CREATE TRIGGER TR_MINHCHUNG_VALIDATE
ON MINHCHUNG
AFTER INSERT, UPDATE
AS
BEGIN
    -- Kiểm tra sinh viên có tồn tại không
    IF EXISTS (
        SELECT 1 FROM inserted i 
        WHERE NOT EXISTS (SELECT 1 FROM SINHVIEN sv WHERE sv.maSV = i.maSV)
    )
    BEGIN
        RAISERROR('Sinh viên không tồn tại', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;
    
    -- Kiểm tra tiêu chí có tồn tại không
    IF EXISTS (
        SELECT 1 FROM inserted i 
        WHERE NOT EXISTS (SELECT 1 FROM TIEUCHI tc WHERE tc.maTC = i.maTC)
    )
    BEGIN
        RAISERROR('Tiêu chí không tồn tại', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;
END;
```

## Backup và Maintenance

### 1. Script backup database
```sql
-- Backup toàn bộ database
BACKUP DATABASE StudentManagement5Good 
TO DISK = 'C:\Backup\StudentManagement5Good_Full.bak'
WITH FORMAT, INIT, NAME = 'StudentManagement5Good Full Backup';

-- Backup differential
BACKUP DATABASE StudentManagement5Good 
TO DISK = 'C:\Backup\StudentManagement5Good_Diff.bak'
WITH DIFFERENTIAL, FORMAT, INIT, NAME = 'StudentManagement5Good Differential Backup';
```

### 2. Script maintenance
```sql
-- Cập nhật thống kê
UPDATE STATISTICS SINHVIEN;
UPDATE STATISTICS MINHCHUNG;
UPDATE STATISTICS KETQUAXETDUYET;

-- Rebuild indexes
ALTER INDEX ALL ON SINHVIEN REBUILD;
ALTER INDEX ALL ON MINHCHUNG REBUILD;
ALTER INDEX ALL ON KETQUAXETDUYET REBUILD;
```

## Kết luận

Script SQL này cung cấp:
- Cấu trúc database hoàn chỉnh cho hệ thống quản lý sinh viên 5 tốt
- Dữ liệu mẫu để test và demo
- Indexes và constraints để đảm bảo tính toàn vẹn dữ liệu
- Views và stored procedures để tối ưu hiệu suất
- Triggers để tự động hóa một số tác vụ
- Scripts backup và maintenance

Database được thiết kế để hỗ trợ đầy đủ các chức năng của hệ thống quản lý sinh viên 5 tốt, từ quản lý thông tin cơ bản đến xét duyệt minh chứng và trao danh hiệu.