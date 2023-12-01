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
	SELECT        dbo.�NVO�CE.id, dbo.�NVO�CE.invoiceNumber AS [Fi� Numaras�], dbo.�NVO�CE.RegDate AS [Sipari� Tarihi], dbo.�NVO�CE.TotalPrice AS [Toplam Tutar], dbo.�NVO�CE.�demeTurar�, dbo.�NVO�CE.�deme�ekli, 
                         dbo.�NVO�CE.�demeDate AS [�deme Tarihi], dbo.�NVO�CE.Bakiye AS [bu Faturadan Kalan Bakiye], dbo.CUSTOMERs.Company AS Firma, dbo.CUSTOMERs.Name AS [Yetkili �smi], 
                         dbo.USERs.UserName AS [M��teri Temsilcisi]
FROM            dbo.�NVO�CE INNER JOIN
                         dbo.CUSTOMERs ON dbo.�NVO�CE.Customer_id = dbo.CUSTOMERs.id INNER JOIN
                         dbo.USERs ON dbo.�NVO�CE.User_id = dbo.USERs.id AND dbo.CUSTOMERs.User_id = dbo.USERs.id
WHERE        (dbo.�NVO�CE.Deletestatus = 0) and 
                        ((dbo.�NVO�CE.invoiceNumber LIKE '%' + @Search + '%') OR
                         (dbo.CUSTOMERs.Company LIKE '%' + @Search + '%') OR
                         (dbo.CUSTOMERs.Name LIKE '%' + @Search + '%') OR
                         (dbo.USERs.UserName LIKE '%' + @Search + '%'))
END
GO
