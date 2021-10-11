CREATE TABLE [dbo].[NhaCungCap]
(
	[MaNCC] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TenNCC] NVARCHAR(100) NOT NULL, 
    [DiaChi] NVARCHAR(100) NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [DienThoai] VARCHAR(12) NULL, 
    [Fax] NVARCHAR(50) NULL
)
go
CREATE TABLE [dbo].[PhieuNhap]
(
	[MaPN] INT NOT NULL PRIMARY KEY IDENTITY,
	[MaNCC] Int ,  
	[NgayNhan] datetime,
	[DaXoa] bit, 
    CONSTRAINT [FK_PhieuNhap_ToTable] FOREIGN KEY ([MaNCC]) REFERENCES [NhaCungCap]([MaNCC]) on delete cascade
)
go

create table loaithanhvien(
    MaLoaiTV int not null primary key identity,
    TenLoaiTV nvarchar(50),
    UuDai nvarchar(100)
)
go
create table thanhvien(
    MaTV int not null primary key identity,
    TaiKhoan varchar(50) not null,
    MatKhau nvarchar(50) not null,
    HoTen nvarchar(100) not null,
    DiaChi nvarchar(100),
    Email nvarchar(50),
    DienThoai varchar(12),
    CauHoi nvarchar(max),
    CauTraLoi nvarchar(max),
    MaLoaiTV int,
    constraint fk_thanhvien_toLoaitv foreign key (MaLoaiTV) references [loaithanhvien]([MaLoaiTV]) on delete cascade
)
go
create table Khachhang(
    MaKH int not null primary key identity,
    TenKH nvarchar(60) not null,
    DiaChi nvarchar(100) null,
    DienThoai varchar(12) not null,
    Email nvarchar(50) null,
    MaTV int,
    constraint fk_khachhang_toThanhvien foreign key (MaTV) references [thanhvien]([MaTV]) on delete cascade
)
go
create table loaisanpham(
    MaLoai int primary key not null identity,
    TenLoai nvarchar(50),
    Icon nvarchar(Max),
    BiDanh nvarchar(50)
)
create table sanpham(
    MaSP int primary key not null identity,
    TenSP nvarchar(200),
    DonGia decimal(18,0),
    NgayCapNhat datetime,
    CauHinh nvarchar(max),
    MoTa nvarchar(200),
    HinhAnh nvarchar(150),
    SoLuongTon int,
    LuotXem int,
    LuotBinhChon int,
    LuotBinhLuan int,
    SoLanMua int,
    Moi int,
    MaNCC int,
    MaNSX int,
    MaLoai int,
    DaXoa bit,
    constraint fk_sanpham_toLoaisanpham foreign key (MaLoai) references [loaisanpham]([MaLoai]) on delete cascade,
    constraint fk_sanpham_toNhaCungCap foreign key (MaNCC) references [Nhacungcap]([MaNCC]) on delete cascade,
    constraint fk_sanpham_toNhasx foreign key (MaNSX) references [NhaSX]([MaNSX]) on delete cascade
)
go
create table CT_phieunhap(
    MaPN int,
    MaSP int,
    DonGiaNhap decimal(18,0),
    SoLuongNhap int,
    primary key(MaPN,MaSP),
    constraint fk_ctphieunhap_toPhieunhap foreign key (MaPN) references [phieunhap]([MaPN]),
    constraint fk_ctphieunhap_toSanpham foreign key (MaSP) references [sanpham]([MaSP])
)
go
create table dondathang(
    MadonDH int not null primary key identity,
    NgayDat datetime,
    TinhTrangGiao bit,
    NgayGiao datetime,
    DaThanhToan bit,
    MaKH int,
    UuDai int,
    constraint fk_dondh_toKH foreign key (MaKH) references [KhachHang]([MaKH]) on delete cascade
)
go
create table CT_DonDH(
    MaDonDH int,
    MaSP int,
    TenSP nvarchar(200),
    SoLuong int,
    DonGia int,
    primary key(MaDonDH,MaSP),
    constraint fk_CT_DonDH_toDonDH foreign key (MaDonDH) references [dondathang]([MaDonDH]),
    constraint fk_CT_DonDH_toSanpham foreign key (MaSP) references [sanpham]([MaSP])
)
go
create table BinhLuan(
    MaBinhLuan int not null primary key identity,
    NoiDung nvarchar(max),
    MaTV int,
    MaSP int,
    constraint fk_BinhLuan_toThanhvien foreign key (MaTV) references [thanhvien]([MaTV]),
    constraint fk_BinhLuan_toSanpham foreign key (MaSP) references [sanpham]([MaSP])
)
go