USE [master]
GO
/****** Object:  Database [myDoctor]    Script Date: 10/6/2021 2:10:35 PM ******/
CREATE DATABASE [myDoctor]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'myDoctor', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.BAO\MSSQL\DATA\myDoctor.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'myDoctor_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.BAO\MSSQL\DATA\myDoctor_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [myDoctor] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [myDoctor].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [myDoctor] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [myDoctor] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [myDoctor] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [myDoctor] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [myDoctor] SET ARITHABORT OFF 
GO
ALTER DATABASE [myDoctor] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [myDoctor] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [myDoctor] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [myDoctor] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [myDoctor] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [myDoctor] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [myDoctor] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [myDoctor] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [myDoctor] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [myDoctor] SET  ENABLE_BROKER 
GO
ALTER DATABASE [myDoctor] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [myDoctor] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [myDoctor] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [myDoctor] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [myDoctor] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [myDoctor] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [myDoctor] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [myDoctor] SET RECOVERY FULL 
GO
ALTER DATABASE [myDoctor] SET  MULTI_USER 
GO
ALTER DATABASE [myDoctor] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [myDoctor] SET DB_CHAINING OFF 
GO
ALTER DATABASE [myDoctor] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [myDoctor] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [myDoctor] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [myDoctor] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'myDoctor', N'ON'
GO
ALTER DATABASE [myDoctor] SET QUERY_STORE = OFF
GO
USE [myDoctor]
GO
/****** Object:  Table [dbo].[BacSi]    Script Date: 10/6/2021 2:10:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BacSi](
	[idBacSi] [int] IDENTITY(1,1) NOT NULL,
	[passbs] [varchar](40) NULL,
	[idHocVi] [int] NULL,
	[tenbs] [nvarchar](40) NULL,
	[urlanh] [varchar](200) NULL,
	[sdt] [varchar](15) NULL,
	[email] [varchar](50) NULL,
	[motabs] [nvarchar](300) NULL,
	[quyen] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idBacSi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[benhAn]    Script Date: 10/6/2021 2:10:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[benhAn](
	[idBenhAn] [int] IDENTITY(1,1) NOT NULL,
	[idKhachHang] [int] NULL,
	[urlanh] [varchar](200) NULL,
	[mota] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[idBenhAn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chitietThanNhan]    Script Date: 10/6/2021 2:10:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chitietThanNhan](
	[idctThanNhan] [int] IDENTITY(1,1) NOT NULL,
	[sdtThanNhan] [varchar](15) NULL,
	[idkhachHang] [int] NULL,
	[qHeVoiBenhNhan] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietThuoc]    Script Date: 10/6/2021 2:10:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietThuoc](
	[idctthuoc] [int] IDENTITY(1,1) NOT NULL,
	[idKetQua] [int] NULL,
	[idthuoc] [int] NULL,
	[soluong] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idctthuoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocVi]    Script Date: 10/6/2021 2:10:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocVi](
	[idHocVi] [int] IDENTITY(1,1) NOT NULL,
	[ChuyenKhoa] [nvarchar](30) NULL,
	[HocVi] [nvarchar](30) NULL,
	[ChungNhan] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idHocVi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KetQuaKham]    Script Date: 10/6/2021 2:10:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KetQuaKham](
	[idKetQua] [int] IDENTITY(1,1) NOT NULL,
	[idDatLich] [int] NULL,
	[ketqua] [nvarchar](200) NULL,
	[hdthuoc] [nvarchar](200) NULL,
	[tienkham] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idKetQua] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 10/6/2021 2:10:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[idKhachHang] [int] IDENTITY(1,1) NOT NULL,
	[passkh] [varchar](40) NULL,
	[tenkh] [nvarchar](40) NULL,
	[tuoi] [int] NULL,
	[email] [varchar](50) NULL,
	[sdt] [varchar](15) NULL,
	[diachi] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[idKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichKham]    Script Date: 10/6/2021 2:10:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichKham](
	[idDatLich] [int] IDENTITY(1,1) NOT NULL,
	[idBacSi] [int] NULL,
	[idKhachHang] [int] NULL,
	[hoten] [nvarchar](40) NULL,
	[tuoi] [int] NULL,
	[ngaydat] [datetime] NULL,
	[trieuchung] [nvarchar](100) NULL,
	[tinhTrang] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idDatLich] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichKhamNhap]    Script Date: 10/6/2021 2:10:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichKhamNhap](
	[idDatLichNhap] [int] IDENTITY(1,1) NOT NULL,
	[idBacSi] [int] NULL,
	[hoten] [nvarchar](40) NULL,
	[tuoi] [int] NULL,
	[sdt] [varchar](15) NULL,
	[ngaydat] [datetime] NULL,
	[trieuchung] [nvarchar](100) NULL,
	[tinhTrang] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idDatLichNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThanNhan]    Script Date: 10/6/2021 2:10:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThanNhan](
	[sdtThanNhan] [varchar](15) NOT NULL,
	[tenThanNhan] [nvarchar](40) NULL,
	[passThanNhan] [varchar](40) NULL,
PRIMARY KEY CLUSTERED 
(
	[sdtThanNhan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Thuoc]    Script Date: 10/6/2021 2:10:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Thuoc](
	[idthuoc] [int] IDENTITY(1,1) NOT NULL,
	[tenthuoc] [nvarchar](30) NULL,
	[mota] [nvarchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[idthuoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BacSi] ON 

INSERT [dbo].[BacSi] ([idBacSi], [passbs], [idHocVi], [tenbs], [urlanh], [sdt], [email], [motabs], [quyen]) VALUES (1, N'bs123456', 1, N'Lê Hồ Quốc Bảo', N'', N'0917982707', N'lehoquocbao9@gmail.com', N'Bác sĩ hoạt động tại phòng khám trong 12 năm', 1)
INSERT [dbo].[BacSi] ([idBacSi], [passbs], [idHocVi], [tenbs], [urlanh], [sdt], [email], [motabs], [quyen]) VALUES (2, N'bs123456', 2, N'Phùng A Bin', N'', N'0365232514', N'phungben02@gmail.com', N'Bác sĩ hoạt động tại phòng khám trong 7 năm', 1)
INSERT [dbo].[BacSi] ([idBacSi], [passbs], [idHocVi], [tenbs], [urlanh], [sdt], [email], [motabs], [quyen]) VALUES (3, N'bs123456', 3, N'Huỳnh Thị Như Tiên', N'', N'0124563254', N'tiennhu@gmail.com', N'Y tá hoạt động dc 2 năm', 0)
SET IDENTITY_INSERT [dbo].[BacSi] OFF
GO
SET IDENTITY_INSERT [dbo].[chitietThanNhan] ON 

INSERT [dbo].[chitietThanNhan] ([idctThanNhan], [sdtThanNhan], [idkhachHang], [qHeVoiBenhNhan]) VALUES (1, N'0978563001', 1, N'Cha')
INSERT [dbo].[chitietThanNhan] ([idctThanNhan], [sdtThanNhan], [idkhachHang], [qHeVoiBenhNhan]) VALUES (2, N'0978563001', 2, N'Chồng')
INSERT [dbo].[chitietThanNhan] ([idctThanNhan], [sdtThanNhan], [idkhachHang], [qHeVoiBenhNhan]) VALUES (3, N'0978563002', 3, N'Con')
INSERT [dbo].[chitietThanNhan] ([idctThanNhan], [sdtThanNhan], [idkhachHang], [qHeVoiBenhNhan]) VALUES (4, N'0978563003', 2, N'Vợ')
SET IDENTITY_INSERT [dbo].[chitietThanNhan] OFF
GO
SET IDENTITY_INSERT [dbo].[ChiTietThuoc] ON 

INSERT [dbo].[ChiTietThuoc] ([idctthuoc], [idKetQua], [idthuoc], [soluong]) VALUES (1, 4, 1, 12)
INSERT [dbo].[ChiTietThuoc] ([idctthuoc], [idKetQua], [idthuoc], [soluong]) VALUES (2, 4, 2, 7)
INSERT [dbo].[ChiTietThuoc] ([idctthuoc], [idKetQua], [idthuoc], [soluong]) VALUES (3, 5, 2, 6)
INSERT [dbo].[ChiTietThuoc] ([idctthuoc], [idKetQua], [idthuoc], [soluong]) VALUES (4, 6, 3, 6)
SET IDENTITY_INSERT [dbo].[ChiTietThuoc] OFF
GO
SET IDENTITY_INSERT [dbo].[HocVi] ON 

INSERT [dbo].[HocVi] ([idHocVi], [ChuyenKhoa], [HocVi], [ChungNhan]) VALUES (1, N'Nội soi', N'Giáo sư', N'Bác sĩ giỏi trong 2 năm')
INSERT [dbo].[HocVi] ([idHocVi], [ChuyenKhoa], [HocVi], [ChungNhan]) VALUES (2, N'Chuyên khoa 1', N'Phó giáo sư', N'Bác sĩ giỏi trong 12 năm')
INSERT [dbo].[HocVi] ([idHocVi], [ChuyenKhoa], [HocVi], [ChungNhan]) VALUES (3, N'Phục hồi sức', N'Y tá', N'Y tá xuất xắc')
SET IDENTITY_INSERT [dbo].[HocVi] OFF
GO
SET IDENTITY_INSERT [dbo].[KetQuaKham] ON 

INSERT [dbo].[KetQuaKham] ([idKetQua], [idDatLich], [ketqua], [hdthuoc], [tienkham]) VALUES (4, 1, N'Bị ruột thừa, chuyển lên bệnh viện Đà Lạt', N'không có', 24000)
INSERT [dbo].[KetQuaKham] ([idKetQua], [idDatLich], [ketqua], [hdthuoc], [tienkham]) VALUES (5, 2, N'Uống ván, chích uống ván', N'Đỏ 2 viên/1 buổi, sáng chiều', 34000)
INSERT [dbo].[KetQuaKham] ([idKetQua], [idDatLich], [ketqua], [hdthuoc], [tienkham]) VALUES (6, 3, N'Trào ngược dạ dày', N'Hồng 2 viên uống sáng chiều, Cam 1 viên uống sáng chiều', 140000)
SET IDENTITY_INSERT [dbo].[KetQuaKham] OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([idKhachHang], [passkh], [tenkh], [tuoi], [email], [sdt], [diachi]) VALUES (1, N'kh123456', N'Khách hàng 1', 14, N'kh01@gmail.com', N'0330123001', N'Phường 7, Đà Lạt')
INSERT [dbo].[KhachHang] ([idKhachHang], [passkh], [tenkh], [tuoi], [email], [sdt], [diachi]) VALUES (2, N'kh123456', N'Khách hàng 2', 24, N'kh02@gmail.com', N'0330123002', N'Phường 11, Cà Mau')
INSERT [dbo].[KhachHang] ([idKhachHang], [passkh], [tenkh], [tuoi], [email], [sdt], [diachi]) VALUES (3, N'kh123456', N'Khách hàng 3', 54, N'kh03@gmail.com', N'0330123003', N'Phường 1, TP. HCM')
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
GO
SET IDENTITY_INSERT [dbo].[LichKham] ON 

INSERT [dbo].[LichKham] ([idDatLich], [idBacSi], [idKhachHang], [hoten], [tuoi], [ngaydat], [trieuchung], [tinhTrang]) VALUES (1, 1, 1, N'Khách hàng 1', 14, CAST(N'2021-10-06T13:58:36.730' AS DateTime), N'Đâu bụng bên phải', NULL)
INSERT [dbo].[LichKham] ([idDatLich], [idBacSi], [idKhachHang], [hoten], [tuoi], [ngaydat], [trieuchung], [tinhTrang]) VALUES (2, 2, 1, N'Khách hàng 1', 14, CAST(N'2021-10-06T13:58:36.733' AS DateTime), N'Đạp đinh chân bên phải', NULL)
INSERT [dbo].[LichKham] ([idDatLich], [idBacSi], [idKhachHang], [hoten], [tuoi], [ngaydat], [trieuchung], [tinhTrang]) VALUES (3, 2, 2, N'Khách hàng 2', 24, CAST(N'2021-10-06T13:58:36.733' AS DateTime), N'Đau dạ dày', NULL)
INSERT [dbo].[LichKham] ([idDatLich], [idBacSi], [idKhachHang], [hoten], [tuoi], [ngaydat], [trieuchung], [tinhTrang]) VALUES (4, 1, 3, N'Khách hàng 3', 54, CAST(N'2021-10-06T13:58:36.733' AS DateTime), N'Ho nhiều ngày, rát ở cổ họng', NULL)
SET IDENTITY_INSERT [dbo].[LichKham] OFF
GO
INSERT [dbo].[ThanNhan] ([sdtThanNhan], [tenThanNhan], [passThanNhan]) VALUES (N'0978563001', N'Than nhan 1', N'123456')
INSERT [dbo].[ThanNhan] ([sdtThanNhan], [tenThanNhan], [passThanNhan]) VALUES (N'0978563002', N'Than nhan 2', N'123456')
INSERT [dbo].[ThanNhan] ([sdtThanNhan], [tenThanNhan], [passThanNhan]) VALUES (N'0978563003', N'Than nhan 3', N'123456')
GO
SET IDENTITY_INSERT [dbo].[Thuoc] ON 

INSERT [dbo].[Thuoc] ([idthuoc], [tenthuoc], [mota]) VALUES (1, N'Vitamin C', N'Viên màu cam, bổ sung vitamin C')
INSERT [dbo].[Thuoc] ([idthuoc], [tenthuoc], [mota]) VALUES (2, N'Vitamin ', N'Viên màu hồng, bổ sung vitamin D')
INSERT [dbo].[Thuoc] ([idthuoc], [tenthuoc], [mota]) VALUES (3, N'Vitamin A', N'Viên màu Đỏ, bổ sung vitamin A')
INSERT [dbo].[Thuoc] ([idthuoc], [tenthuoc], [mota]) VALUES (4, N'Vitamin B', N'Viên màu Đỏ, bổ sung vitamin B')
SET IDENTITY_INSERT [dbo].[Thuoc] OFF
GO
ALTER TABLE [dbo].[BacSi]  WITH CHECK ADD FOREIGN KEY([idHocVi])
REFERENCES [dbo].[HocVi] ([idHocVi])
GO
ALTER TABLE [dbo].[benhAn]  WITH CHECK ADD FOREIGN KEY([idKhachHang])
REFERENCES [dbo].[KhachHang] ([idKhachHang])
GO
ALTER TABLE [dbo].[chitietThanNhan]  WITH CHECK ADD FOREIGN KEY([idkhachHang])
REFERENCES [dbo].[KhachHang] ([idKhachHang])
GO
ALTER TABLE [dbo].[chitietThanNhan]  WITH CHECK ADD FOREIGN KEY([sdtThanNhan])
REFERENCES [dbo].[ThanNhan] ([sdtThanNhan])
GO
ALTER TABLE [dbo].[ChiTietThuoc]  WITH CHECK ADD FOREIGN KEY([idKetQua])
REFERENCES [dbo].[KetQuaKham] ([idKetQua])
GO
ALTER TABLE [dbo].[ChiTietThuoc]  WITH CHECK ADD FOREIGN KEY([idthuoc])
REFERENCES [dbo].[Thuoc] ([idthuoc])
GO
ALTER TABLE [dbo].[KetQuaKham]  WITH CHECK ADD FOREIGN KEY([idDatLich])
REFERENCES [dbo].[LichKham] ([idDatLich])
GO
ALTER TABLE [dbo].[LichKham]  WITH CHECK ADD FOREIGN KEY([idBacSi])
REFERENCES [dbo].[BacSi] ([idBacSi])
GO
ALTER TABLE [dbo].[LichKham]  WITH CHECK ADD FOREIGN KEY([idKhachHang])
REFERENCES [dbo].[KhachHang] ([idKhachHang])
GO
ALTER TABLE [dbo].[LichKhamNhap]  WITH CHECK ADD FOREIGN KEY([idBacSi])
REFERENCES [dbo].[BacSi] ([idBacSi])
GO
USE [master]
GO
ALTER DATABASE [myDoctor] SET  READ_WRITE 
GO
