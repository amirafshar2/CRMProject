USE [DBCRM]
GO
/****** Object:  StoredProcedure [dbo].[SearchProduct]    Script Date: 22.11.2023 17:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE SearchProduct
	@Search nvarchar(max)
AS
BEGIN
SELECT        id, Category AS Ürün, Name AS [Ürün Adı], Cap AS Çap, Boy, Quality AS Kalite, Kaplama, DINnumber AS DIN, Stock AS Stok, Price AS Fiyat, BrandName AS Marka, Packing AS Paket, picture AS Görsel,  
                         Feature AS Özellik
FROM            dbo.PRODUCTs
WHERE        ((DeletStatus = 0) AND (SaledPices =0)) AND  ((Name like '%'+ @Search +'%') or (Cap like  '%'+ @Search +'%') or (Boy like  '%'+ @Search +'%') or (Quality like  '%'+ @Search +'%') or (DINnumber like  '%'+ @Search +'%') or (BrandName like  '%'+ @Search +'%'))
END
