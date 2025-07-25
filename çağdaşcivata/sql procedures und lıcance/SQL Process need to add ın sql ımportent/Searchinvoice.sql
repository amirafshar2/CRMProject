-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Searchinvoice
@Search nvarchar(max)
AS
BEGIN
	SELECT        dbo.ÝNVOÝCE.id, dbo.ÝNVOÝCE.invoiceNumber AS [Fiþ Numarasý], dbo.ÝNVOÝCE.RegDate AS [Sipariþ Tarihi], dbo.ÝNVOÝCE.TotalPrice AS [Toplam Tutar], dbo.ÝNVOÝCE.ÖdemeTurarý, dbo.ÝNVOÝCE.ödemeÞekli, 
                         dbo.ÝNVOÝCE.ÖdemeDate AS [Ödeme Tarihi], dbo.ÝNVOÝCE.Bakiye AS [bu Faturadan Kalan Bakiye], dbo.CUSTOMERs.Company AS Firma, dbo.CUSTOMERs.Name AS [Yetkili Ýsmi], 
                         dbo.USERs.UserName AS [Müþteri Temsilcisi]
FROM            dbo.ÝNVOÝCE INNER JOIN
                         dbo.CUSTOMERs ON dbo.ÝNVOÝCE.Customer_id = dbo.CUSTOMERs.id INNER JOIN
                         dbo.USERs ON dbo.ÝNVOÝCE.User_id = dbo.USERs.id AND dbo.CUSTOMERs.User_id = dbo.USERs.id
WHERE        (dbo.ÝNVOÝCE.Deletestatus = 0) and 
                        ((dbo.ÝNVOÝCE.invoiceNumber LIKE '%' + @Search + '%') OR
                         (dbo.CUSTOMERs.Company LIKE '%' + @Search + '%') OR
                         (dbo.CUSTOMERs.Name LIKE '%' + @Search + '%') OR
                         (dbo.USERs.UserName LIKE '%' + @Search + '%'))
END
GO
