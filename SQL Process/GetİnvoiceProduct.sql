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
CREATE PROCEDURE Get�nvoiceProduct
@Search int
AS
BEGIN
	SELECT        dbo.�NVO�CE.id AS iid, dbo.�NVO�CE.invoiceNumber, dbo.�NVO�CE.RegDate, dbo.�NVO�CE.CeackOutDate, dbo.�NVO�CE.�sCheckedOut, dbo.�NVO�CE.TotalPrice, dbo.�NVO�CE.�demeTurar�, dbo.�NVO�CE.�demeDate, 
                         dbo.�NVO�CE.�deme�ekli, dbo.�NVO�CE.VadeDate, dbo.�NVO�CE.Bakiye, dbo.�NVO�CE.Deletestatus, dbo.PRODUCTs.id, dbo.PRODUCTs.Name, dbo.PRODUCTs.Cap, dbo.PRODUCTs.Boy, dbo.PRODUCTs.Packing, 
                         dbo.PRODUCTs.Quality, dbo.PRODUCTs.Feature, dbo.PRODUCTs.Stock, dbo.PRODUCTs.Price, dbo.PRODUCTs.Kaplama, dbo.PRODUCTs.SaledPices, dbo.PRODUCTs.DINnumber, dbo.PRODUCTs.BrandName, 
                         dbo.PRODUCTs.Product_cod, dbo.PRODUCTs.picture, dbo.PRODUCTs.DeletStatus, dbo.PRODUCTs.Category, dbo.�NVO�CE.User_id, dbo.�NVO�CE.Customer_id
FROM            dbo.�NVO�CE INNER JOIN
                         dbo.PRODUCTs ON dbo.�NVO�CE.id = dbo.PRODUCTs.id
WHERE        ((dbo.�NVO�CE.Deletestatus = 0) AND (dbo.PRODUCTs.Stock = 0)) AND (dbo.�NVO�CE.id = @Search)
END
GO
