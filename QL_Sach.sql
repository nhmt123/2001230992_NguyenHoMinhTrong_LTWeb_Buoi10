create database QL_Sach
use QL_Sach

create table TacGia
(
	MaTacGia int not null,
	TenTacGia nvarchar(50),
	DiaChi nvarchar(50),
	TieuSu nvarchar(100),
	DienThoai int,
	constraint pk_tg primary key (MaTacGia)
)

create table ChuDe 
(
	MaChuDe int not null,
	TenChuDe nvarchar(50),
	constraint pk_cd primary key (MaChuDe)
)

create table NhaXuatBan
(
	MaNXB int not null,
	TenNXB nvarchar(50),
	DiaChi nvarchar(50),
	DienThoai int,
	constraint pk_nxb primary key(MaNXB)
)

create table Sach
(
	MaSach int not null,
	TenSach nvarchar(50),
	GiaBan money,
	MoTa nvarchar(100),
	NgayCapNhat date,
	AnhBia nvarchar(50),
	SoLuongTon int,
	MaChuDe int,
	MaNXB int,
	Moi nvarchar(50),
	constraint pk_s primary key (MaSach),
	constraint fk_s_cd foreign key (MaChuDe) references ChuDe (MaChuDe),
	constraint fk_s_nxb foreign key (MaNXB) references NhaXuatBan (MaNXB)
)

create table ThamGia 
(
	MaSach int not null,
	MaTacGia int not null,
	VaiTro nvarchar(50),
	ViTri nvarchar(50),
	constraint pk_vs primary key (MaTacGia, MaSach),
	constraint fk_vs_tg foreign key (MaTacGia) references TacGia (MaTacGia),
	constraint fk_vs_s foreign key (MaSach) references Sach (MaSach)
)

create table KhachHang
(
	MaKH int not null,
	HoTen nvarchar(50),
	NgaySinh date,
	GioiTinh nvarchar(10),
	DienThoai int,
	TaiKhoan nvarchar(50),
	MatKhau nvarchar(50),
	Email nvarchar(50),
	DiaChi nvarchar(50),
	constraint pk_kh primary key (MaKH)
)

create table DonHang
(
	MaDonHang int not null,
	NgayGiao date,
	NgayDat date,
	DaThanhToan nvarchar(50),
	TinhTrangGiaoHang nvarchar(50),
	MaKH int,
	constraint pk_ddh primary key (MaDonHang),
	constraint fk_ddh_kh foreign key (MaKH)references KhachHang (MaKH)
)

create table ChiTietDonHang
(
	MaDonHang int not null,
	MaSach int not null,
	SoLuong int,
	DonGia money,
	constraint pk_ctdh primary key (MaDonHang, MaSach),
	constraint fk_ctdh_ddh foreign key (MaDonHang)references Donhang (MaDonHang),
	constraint fk_ctdh_s foreign key (MaSach)references Sach (MaSach)
)

drop table ChiTietDonHang
drop table DonHang
drop table KhachHang
drop table ThamGia
drop table Sach
drop table NhaXuatBan
drop table ChuDe
drop table TacGia

delete KhachHang

select * from ChiTietDonHang
select * from DonHang
select * from KhachHang
select * from ThamGia
select * from Sach
select * from NhaXuatBan
select * from ChuDe
select * from TacGia

insert into ChuDe values
(1, N'ÂM NHẠC'),
(2, N'CÔNG NGHỆ THÔNG TIN'),
(3, N'DANH NHÂN'),
(4, N'DU LỊCH'),
(5, N'KHOA HỌC KỸ THUẬT'),
(6, N'KHOA HỌC VẬT LÝ'),
(7, N'KHOA HỌC XÃ HỘI')

insert into NhaXuatBan values
(1, N'ĐẠI HỌC QUỐC GIA', N'456 BCA', 0897869889),
(2, N'KHOA HỌC & KỸ THUẬT', N'123 ABC', 0987869453),
(3, N'KIM ĐỒNG', N'456 ABC', 0983758435),
(4, N'NHÀ XUẤT BẢN TRẺ', N'654 ABC', 0978964980),
(5, N'NXB HỒNG ĐỨC', N'123 CBA', 0989834562),
(6, N'NXB LAO ĐỘNG - XÃ HỘI', N'234 BAC', 0879582475),
(7, N'NXB PHỤ NỮ', N'324 ABC', 0847659212)

insert into Sach values
(1, N'Lập Trình C# Cơ Bản', 120000, N'Hướng dẫn lập trình C# cho người mới bắt đầu', '2023-05-10', N'csharp.jpg', 50, 2, 2, N'Mới'),
(2, N'Vật Lý Vui', 85000, N'Những hiện tượng vật lý thú vị và dễ hiểu', '2023-07-22', N'vatlyvui.jpg', 30, 6, 5, N'Mới'),
(3, N'Non Nước Việt Nam 63 Tỉnh Thành', 99000, N'Sách du lịch giới thiệu các danh lam thắng cảnh Việt Nam', '2024-02-15', N'khamphavn.jpg', 40, 4, 4, N'Mới'),
(4, N'Những Nhà Khoa Học Lừng Danh', 110000, N'Giới thiệu tiểu sử các nhà khoa học nổi tiếng thế giới', '2023-10-01', N'khoahoclungdanh.jpg', 25, 3, 3, N'Cũ'),
(5, N'Giáo Trình Cơ Học Lý Thuyết', 135000, N'Sách chuyên sâu về cơ học và ứng dụng trong kỹ thuật', '2024-01-20', N'cohoc.jpg', 20, 5, 1, N'Mới'),
(6, N'Phát Triển Web Với HTML & CSS', 98000, N'Hướng dẫn thiết kế website với HTML và CSS', '2023-09-05', N'htmlcss.jpg', 60, 2, 2, N'Mới'),
(7, N'Nghệ Thuật Guitar Cơ Bản', 75000, N'Học chơi guitar từ cơ bản đến nâng cao', '2023-06-10', N'guitar.jpg', 45, 1, 7, N'Mới'),
(8, N'Tư Duy Khoa Học Xã Hội', 88000, N'Phân tích các vấn đề xã hội dưới góc nhìn khoa học', '2024-03-01', N'xahoi.jpg', 35, 7, 6, N'Mới'),
(9, N'Lập Trình Python Nâng Cao', 150000, N'Các kỹ thuật nâng cao trong ngôn ngữ Python', '2024-04-18', N'python.jpg', 15, 2, 2, N'Mới')

insert into TacGia values
(1, N'Nguyễn Văn An', N'Hà Nội', N'Tiến sĩ CNTT, giảng viên đại học quốc gia', 0987654321),
(2, N'Lê Thị Hoa', N'TP.HCM', N'Nhà nghiên cứu vật lý, chuyên viết sách phổ thông khoa học', 0912345678),
(3, N'Trần Minh Đức', N'Đà Nẵng', N'Nhà báo, chuyên viết sách du lịch và văn hóa Việt Nam', 0934567890),
(4, N'Phạm Hồng Phúc', N'Hải Phòng', N'Giảng viên cơ học ứng dụng, ĐH Bách Khoa', 0978456123),
(5, N'Vũ Thanh Tùng', N'Hà Nội', N'Lập trình viên, chuyên giảng dạy Python và C#', 0856789123),
(6, N'Hoàng Mỹ Linh', N'Cần Thơ', N'Nhạc sĩ, giảng viên guitar cổ điển', 0834567123)

insert into ThamGia values
(1, 5, N'Tác giả chính', N'Tác giả'),
(2, 2, N'Tác giả chính', N'Tác giả'),
(3, 3, N'Biên soạn', N'Tác giả'),
(4, 4, N'Chủ biên', N'Tác giả'),
(5, 1, N'Đồng tác giả', N'Hiệu đính'),
(6, 6, N'Tác giả chính', N'Tác giả'),
(7, 6, N'Tác giả chính', N'Tác giả'),
(8, 3, N'Đồng tác giả', N'Biên tập'),
(9, 5, N'Tác giả chính', N'Tác giả'),
(9, 1, N'Đồng tác giả', N'Hiệu đính')






