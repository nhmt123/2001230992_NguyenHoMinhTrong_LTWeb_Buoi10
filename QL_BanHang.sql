create database QL_BanHang
go
use QL_BanHang
go

create table SanPham
(
	MaSanPham int not null,
	TenSP nvarchar(50),
	DonGia money,
	HinhAnh nvarchar(50),
	MoTa nvarchar(50),
	SoLuongTon int,
	constraint pk_sp primary key (MaSanPham)
)

create table KhachHang
(
	MaKhachHang int not null,
	TenKH nvarchar(50),
	TaiKhoan nvarchar(50),
	MaKhau nvarchar(50),
	DiaChi nvarchar(50),
	SoDienThoai int,
	constraint pk_kh primary key (MaKhachHang)
)

create table HoaDon
(
	MaHoaDon int not null,
	NgayHoaDon date,
	MaKH int,
	constraint pk_hd primary key (MaHoaDon),
	constraint fk_hd_kh foreign key (MaKH) references KhachHang (MaKhachHang)
)

create table ChiTiet
(
	MaHD int not null,
	MaSP int not null,
	SoLuong int,
	constraint pk_ct primary key (MaHD, MaSP),
	constraint fk_ct_hd foreign key (MaHD) references HoaDon (MaHoaDon),
	constraint fk_ct_sp foreign key (MaSP) references SanPham (MaSanPham),
)

insert into SanPham values
(1, N'Dế Mèn Phiêu Lưu Ký', 120000, N'hinh1.jpg', N'Truyện thiếu nhi kinh điển', 100),
(2, N'Lão Hạc', 85000, N'hinh2.jpg', N'Truyện ngắn của Nam Cao', 50),
(3, N'Số Đỏ', 150000, N'hinh3.jpg', N'Tiểu thuyết của Vũ Trọng Phụng', 75),
(4, N'Tắt Đèn', 95000, N'hinh4.jpg', N'Tiểu thuyết của Ngô Tất Tố', 30),
(5, N'Truyện Kiều', 200000, N'hinh5.jpg', N'Thơ của Nguyễn Du', 60)

insert into KhachHang values
(1, N'Nguyễn Văn A', 'NVA', N'123', N'123 Đường A, Hà Nội', 0987654321),
(2, N'Trần Thị B', 'TTB',N'123', N'456 Đường B, TP.HCM', 0912345678),
(3, N'Lê Văn C', 'LVC', N'123', N'789 Đường C, Đà Nẵng', 0905111222),
(4, N'Phạm Thị D', 'PTD',N'123', N'101 Đường D, Cần Thơ', 0939444555),
(5, N'Hoàng Văn E', 'HVE',N'123', N'202 Đường E, Hải Phòng', 0945777888)

insert into HoaDon values
(1, '2023-10-01', 1),
(2, '2023-10-02', 2),
(3, '2023-10-03', 1),
(4, '2023-10-04', 3),
(5, '2023-10-05', 5)

insert into ChiTiet values
(1, 1, 2),
(1, 3, 1),
(2, 2, 1),
(3, 5, 1),
(4, 4, 3)


drop table ChiTiet
drop table HoaDon
drop table KhachHang
drop table SanPham