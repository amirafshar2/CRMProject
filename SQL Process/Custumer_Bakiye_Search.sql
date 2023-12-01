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
CREATE PROCEDURE Custumer_Bakiye_Search
@Search int
AS
BEGIN
	SELECT        dbo.CUSTOMERs.id AS c_id, dbo.ÝNVOÝCE.id, dbo.ÝNVOÝCE.invoiceNumber AS [Fatura No], dbo.ÝNVOÝCE.RegDate AS [S.Tarih], dbo.ÝNVOÝCE.CeackOutDate AS [Ö.Tarih], dbo.ÝNVOÝCE.TotalPrice AS [T.Tutar], 
                         dbo.ÝNVOÝCE.ÖdemeTurarý AS [Ö.Tutar], dbo.ÝNVOÝCE.ÖdemeDate AS [Ö.Tarihi], dbo.ÝNVOÝCE.ödemeÞekli AS [Ö.Þekli], dbo.ÝNVOÝCE.VadeDate AS [Vade.Tarihi], dbo.ÝNVOÝCE.Bakiye
FROM            dbo.ÝNVOÝCE INNER JOIN
                         dbo.CUSTOMERs ON dbo.ÝNVOÝCE.Customer_id = dbo.CUSTOMERs.id
WHERE        (dbo.CUSTOMERs.id = @Search)
END
GO
