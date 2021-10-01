USE DoctorQB

GO


INSERT dbo.BacSi
(
	passbs,
    tenbs,
	urlanh,
    sdt,
    email,
    motabs,
	quyen
)
VALUES
(   
	'bs123456',
	N'Lê Hồ Quốc Bảo', -- tenbs - nvarchar(50)
    '',
	'0917982707',  -- sdt - char(10)
    'lehoquocbao@gmail.com',  -- email - varchar(100)
    N'Bác sĩ loại vip',
	1-- motabs - nvarchar(300)
    )

	INSERT dbo.BacSi
(
    passbs,
    tenbs,
	urlanh,
    sdt,
    email,
    motabs,
	quyen
)
VALUES
(   
	'bs123456',
	N'Phùng A Phèn',-- tenbs - nvarchar(50)
	'',
    '0917982333',  -- sdt - char(10)
    'lekimbo@gmail.com',  -- email - varchar(100)
    N'Bác sĩ thực tập',
	1
    )

	INSERT dbo.KhachHang
	(
		passkh,
	    tenkh,
	    tuoi,
	    email,
	    sdt,
	    diachi
	)
	VALUES
	(   
		'kh123456',
		N'Nguyễn Khải', -- tenkh - nvarchar(50)
	    22,   -- tuoi - int
	    'khainguyen@gmail.com',  -- email - varchar(100)
	    '0985652325',  -- sdt - char(10)
	    N'Nhà ở TP HCM'  -- diachi - nvarchar(200)
	    )
	INSERT dbo.KhachHang
	(
		passkh,
	    tenkh,
	    tuoi,
	    email,
	    sdt,
	    diachi
	)
	VALUES
	(   
		'kh123456',
		N'Lý Bí', -- tenkh - nvarchar(50)
	    22,   -- tuoi - int
	    'libi232@gmail.com',  -- email - varchar(100)
	    '0236253268',  -- sdt - char(10)
	    N'Nhà ở Cà Mau'  -- diachi - nvarchar(200)
	    )

		INSERT dbo.benhAn
		(
		    idkh,
		    urlanh,
		    mota
		)
		VALUES
		(   1,  -- idkh - int
		    '', -- urlanh - varchar(300)
		    N'Đâu chân' -- mota - nvarchar(300)
		    )
			INSERT dbo.benhAn
		(
		    idkh,
		    urlanh,
		    mota
		)
		VALUES
		(   1,  -- idkh - int
		    '', -- urlanh - varchar(300)
		    N'Đâu háng' -- mota - nvarchar(300)
		    )


			--22/09/2021 10:34 CH
			INSERT dbo.DatLich
(
    idbs,
    idkh,
    ngaydat
)
VALUES
(   1,        -- idbs - int
    1,        -- idkh - int
   '2021-09-22 08:00 PM' -- ngaydat - datetime
    )


	
INSERT dbo.DatLich
(
    idbs,
    idkh,
    ngaydat,
	trieuchung
)
VALUES
(   1,        -- idbs - int
    1,        -- idkh - int
    GETDATE(), -- ngaydat - datetime
	N'Chảy máu chân'
    )

	INSERT dbo.DatLich
(
    idbs,
    idkh,
    ngaydat, 
	trieuchung
)
VALUES
(   2,        -- idbs - int
    2,        -- idkh - int
    '2021-12-26 14:02',
	N'Tế giếng'
    )

	INSERT dbo.KetQuaKham
	(
	    iddl,
	    ketqua,
	    hdthuoc,
	    tienkham
	)
	VALUES
	(   1,   -- iddl - int
	 N'Đau chân thật', -- ketqua - nvarchar(300)
	 N' đỏ 1 viên 1 buổi
		vàng 2 viên 1 buổi',  -- hdthuoc - nvarchar(300)
	    200000    -- tienkham - int
	    )

		INSERT dbo.KetQuaKham
	(
	    iddl,
	    ketqua,
	    hdthuoc,
	    tienkham
	)
	VALUES
	(   3,   -- iddl - int
	 N'chảy máu đầu', -- ketqua - nvarchar(300)
	 N' xanh 1 viên 1 buổi
		đen 2 viên 1 buổi',  -- hdthuoc - nvarchar(300)
	    200000    -- tienkham - int
	    )

		INSERT dbo.KetQuaKham
	(
	    iddl,
	    ketqua,
	    hdthuoc,
	    tienkham
	)
	VALUES
	(   2,   -- iddl - int
	 N'Đau chân thật', -- ketqua - nvarchar(300)
	 N' hồng 1 viên 1 buổi
		tím 2 viên 1 buổi',  -- hdthuoc - nvarchar(300)
	    700000    -- tienkham - int
	    )

INSERT dbo.Thuoc
(
    tenthuoc,
    mota
)
VALUES
(   N'Paradol', -- tenthuoc - nvarchar(50)
    N'Chữa cẩm cúm'  -- mota - nvarchar(100)
    )

INSERT dbo.Thuoc
(
    tenthuoc,
    mota
)
VALUES
(   N'Vitamin C', -- tenthuoc - nvarchar(50)
    N'Bổ sung vitamin C'  -- mota - nvarchar(100)
    )

INSERT dbo.ChiTietThuoc
(
    iddl,
    idthuoc,
    soluong
)
VALUES
(   1, -- iddl - int
    2, -- idthuoc - int
    4  -- soluong - int
    )

	INSERT dbo.ChiTietThuoc
(
    iddl,
    idthuoc,
    soluong
)
VALUES
(   1, -- iddl - int
    1, -- idthuoc - int
    2  -- soluong - int
    )

	INSERT dbo.ThanNhan
	(
	    sdtThanNhan,
	    tenThanNhan,
	    emailThanNhan,
	    passtn

	)
	VALUES
	(   '0325632547',  -- sdtThanNhan - char(10)
	    N'ten 1', -- tenThanNhan - nvarchar(50)
	    'email1@gmail.com',  -- emailThanNhan - varchar(100)
	    '1234'  -- passtn - varchar(50)
	    )

			INSERT dbo.ThanNhan
	(
	    sdtThanNhan,
	    tenThanNhan,
	    emailThanNhan,
	    passtn
	)
	VALUES
	(   '0235698747',  -- sdtThanNhan - char(10)
	    N'ten 2', -- tenThanNhan - nvarchar(50)
	    'email2@gmail.com',  -- emailThanNhan - varchar(100)
	    '1234'  -- passtn - varchar(50)

	    )
	INSERT dbo.ThanNhan
	(
	    sdtThanNhan,
	    tenThanNhan,
	    emailThanNhan,
	    passtn	 
	)
	VALUES
	(   '0452362147',  -- sdtThanNhan - char(10)
	    N'ten 3', -- tenThanNhan - nvarchar(50)
	    'email3@gmail.com',  -- emailThanNhan - varchar(100)
	    '1234'  -- passtn - varchar(50)
	
	    )
	
	INSERT dbo.chitietThanNhan
	(
	    sdtThanNhan,
	    idkh
	)
	VALUES
	(   '0452362147', -- sdtThanNhan - char(10)
	    1   -- idkh - int
	    )

			INSERT dbo.chitietThanNhan
	(
	    sdtThanNhan,
	    idkh
	)
	VALUES
	(   '0452362147', -- sdtThanNhan - char(10)
	   2   -- idkh - int
	    )
		INSERT dbo.ThanNhan
		(
		    sdtThanNhan,
		    tenThanNhan,
		    emailThanNhan,
		    passtn
		)
		VALUES
		(   '0123654789',  -- sdtThanNhan - char(10)
		    N'than nhan 5', -- tenThanNhan - nvarchar(50)
		    'email52gmail.com',  -- emailThanNhan - varchar(100)
		    '1234'   -- passtn - varchar(50)
		    )

INSERT dbo.chitietThanNhan
	(
	    sdtThanNhan,
	    idkh
	)
	VALUES
	(   '0123654789', -- sdtThanNhan - char(10)
	    1   -- idkh - int
	    )