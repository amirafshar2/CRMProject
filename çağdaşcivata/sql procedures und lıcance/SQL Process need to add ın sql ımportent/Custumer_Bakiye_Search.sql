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
	SELECT        dbo.CUSTOMERs.id AS c_id, dbo.�NVO�CE.id, dbo.�NVO�CE.invoiceNumber AS [Fatura No], dbo.�NVO�CE.RegDate AS [S.Tarih], dbo.�NVO�CE.CeackOutDate AS [�.Tarih], dbo.�NVO�CE.TotalPrice AS [T.Tutar], 
                         dbo.�NVO�CE.�demeTurar� AS [�.Tutar], dbo.�NVO�CE.�demeDate AS [�.Tarihi], dbo.�NVO�CE.�deme�ekli AS [�.�ekli], dbo.�NVO�CE.VadeDate AS [Vade.Tarihi], dbo.�NVO�CE.Bakiye
FROM            dbo.�NVO�CE INNER JOIN
                         dbo.CUSTOMERs ON dbo.�NVO�CE.Customer_id = dbo.CUSTOMERs.id
WHERE        (dbo.CUSTOMERs.id = @Search)
END
GO
