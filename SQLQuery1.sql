
CREATE DATABASE DoctorQB
GO


USE DoctorQB
GO

CREATE TABLE KhachHang
(
   idkh INT PRIMARY KEY IDENTITY,
   passkh VARCHAR(50),
   tenkh NVARCHAR(50),
   tuoi INT ,
   email VARCHAR(100),
   sdt CHAR(10),
   diachi NVARCHAR(200)
)
GO

CREATE TABLE benhAn
(
	idba INT PRIMARY KEY IDENTITY,
	idkh INT,
	urlanh VARCHAR(300),
	mota NVARCHAR(300),
	FOREIGN KEY (idkh) REFERENCES dbo.KhachHang(idkh)
)
GO

CREATE TABLE BacSi
(
	idbs INT PRIMARY KEY IDENTITY,
	passbs VARCHAR(50),
	tenbs NVARCHAR(50),
	urlanh VARCHAR(300),
	sdt CHAR(10),
	email VARCHAR(100),
	motabs NVARCHAR(300),
	quyen BIT,
)
GO

CREATE TABLE DatLich
(
	iddl INT PRIMARY KEY IDENTITY,
	idbs INT,
	idkh INT,
	ngaydat DATETIME,
	trieuchung NVARCHAR(300),
	FOREIGN KEY (idbs) REFERENCES dbo.BacSi(idbs),
	FOREIGN KEY (idkh) REFERENCES dbo.KhachHang(idkh)
)
GO
CREATE TABLE DatlichNhap
(
	iddln INT PRIMARY KEY IDENTITY,
	idbs INT,
	hoten NVARCHAR(50),
	tuoi INT,
	sdt CHAR(10),
	ngaydat DATETIME,
	trieuchung NVARCHAR(200),
	FOREIGN KEY(idbs) REFERENCES dbo.BacSi(idbs)
)


go
CREATE TABLE KetQuaKham
(
	idkq INT PRIMARY KEY IDENTITY,
	iddl INT,
	ketqua NVARCHAR(300),
	hdthuoc NVARCHAR(300),
	tienkham INT,

	FOREIGN KEY (iddl) REFERENCES dbo.DatLich(iddl)
)
GO
CREATE TABLE Thuoc
(
	idthuoc INT IDENTITY PRIMARY KEY,
	tenthuoc NVARCHAR(50),
	mota NVARCHAR(100),	
)
GO
CREATE TABLE ChiTietThuoc
(
	idctthuoc INT IDENTITY PRIMARY KEY,
	iddl INT,
	idthuoc INT,
	soluong INT,
	FOREIGN KEY (iddl) REFERENCES dbo.DatLich(iddl),
	FOREIGN KEY (idthuoc) REFERENCES dbo.Thuoc(idthuoc)
)

CREATE TABLE ThanNhan
(
	sdtThanNhan CHAR(10) PRIMARY KEY,
	tenThanNhan NVARCHAR(50),
	emailThanNhan VARCHAR(100),	
	passtn VARCHAR(50),	
)
GO

CREATE TABLE chitietThanNhan
(
	idchThanNhan INT IDENTITY,
	sdtThanNhan CHAR(10),
	idkh INT,
	FOREIGN KEY (sdtThanNhan) REFERENCES dbo.ThanNhan(sdtThanNhan),
	FOREIGN KEY (idkh) REFERENCES dbo.KhachHang(idkh)
)
 


SELECT * FROM dbo.BacSi
SELECT * FROM dbo.KhachHang
SELECT * FROM dbo.benhAn
SELECT * FROM dbo.DatLich
SELECT * FROM dbo.KetQuaKham
SELECT * FROM dbo.Thuoc
SELECT * FROM dbo.ChiTietThuoc
SELECT * FROM dbo.ThanNhan
SELECT * FROM dbo.chitietThanNhan


SELECT dbo.KetQuaKham.tienkham,dbo.KetQuaKham.hdthuoc, dbo.DatLich.ngaydat, dbo.KetQuaKham.ketqua,dbo.DatLich.idkh, dbo.KhachHang.tenkh  FROM dbo.KetQuaKham, dbo.DatLich, dbo.KhachHang
WHERE	dbo.DatLich.idkh =2  AND
		dbo.DatLich.iddl = KetQuaKham.iddl AND
        DatLich.idkh = dbo.KhachHang.idkh

SELECT DatLich.iddl, tenthuoc,soluong FROM dbo.ChiTietThuoc,dbo.Thuoc, dbo.DatLich
WHERE ChiTietThuoc.iddl=ChiTietThuoc.iddl AND ChiTietThuoc.idthuoc = Thuoc.idthuoc AND dbo.DatLich.iddl=1
		

SELECT ngaydat,ketqua,hdthuoc, tienkham FROM dbo.DatLich, dbo.KetQuaKham,dbo.KhachHang WHERE DatLich.idkh=KhachHang.idkh AND 
dbo.KhachHang.idkh=1 AND KetQuaKham.iddl=dbo.DatLich.iddl


SELECT dbo.ThanNhan.tenThanNhan, dbo.ThanNhan.sdtThanNhan FROM dbo.KhachHang, dbo.ThanNhan, dbo.chitietThanNhan
WHERE dbo.KhachHang.idkh =1 
AND dbo.KhachHang.idkh = dbo.chitietThanNhan.idkh 
AND dbo.chitietThanNhan.sdtThanNhan=dbo.ThanNhan.sdtThanNhan