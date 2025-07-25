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
CREATE PROCEDURE GetÝnvoiceProduct
@Search int
AS
BEGIN
	SELECT        dbo.ÝNVOÝCE.id AS iid, dbo.ÝNVOÝCE.invoiceNumber, dbo.ÝNVOÝCE.RegDate, dbo.ÝNVOÝCE.CeackOutDate, dbo.ÝNVOÝCE.ÝsCheckedOut, dbo.ÝNVOÝCE.TotalPrice, dbo.ÝNVOÝCE.ÖdemeTurarý, dbo.ÝNVOÝCE.ÖdemeDate, 
                         dbo.ÝNVOÝCE.ödemeÞekli, dbo.ÝNVOÝCE.VadeDate, dbo.ÝNVOÝCE.Bakiye, dbo.ÝNVOÝCE.Deletestatus, dbo.PRODUCTs.id, dbo.PRODUCTs.Name, dbo.PRODUCTs.Cap, dbo.PRODUCTs.Boy, dbo.PRODUCTs.Packing, 
                         dbo.PRODUCTs.Quality, dbo.PRODUCTs.Feature, dbo.PRODUCTs.Stock, dbo.PRODUCTs.Price, dbo.PRODUCTs.Kaplama, dbo.PRODUCTs.SaledPices, dbo.PRODUCTs.DINnumber, dbo.PRODUCTs.BrandName, 
                         dbo.PRODUCTs.Product_cod, dbo.PRODUCTs.picture, dbo.PRODUCTs.DeletStatus, dbo.PRODUCTs.Category, dbo.ÝNVOÝCE.User_id, dbo.ÝNVOÝCE.Customer_id
FROM            dbo.ÝNVOÝCE INNER JOIN
                         dbo.PRODUCTs ON dbo.ÝNVOÝCE.id = dbo.PRODUCTs.id
WHERE        ((dbo.ÝNVOÝCE.Deletestatus = 0) AND (dbo.PRODUCTs.Stock = 0)) AND (dbo.ÝNVOÝCE.id = @Search)
END
GO
