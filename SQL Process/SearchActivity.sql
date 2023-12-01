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
CREATE PROCEDURE SearchActivity
@Search nvarchar(max)
AS
BEGIN
	SELECT        dbo.ACTÝVÝTY.id, dbo.ACTÝVÝTY.Title AS Baþlýk, dbo.ACTÝVÝTY.Ýnfo, dbo.CUSTOMERs.Name AS Müþteri, dbo.CUSTOMERs.Company AS Firma, dbo.USERs.UserName AS Görevli
FROM            dbo.ACTÝVÝTY INNER JOIN
                         dbo.CUSTOMERs ON dbo.ACTÝVÝTY.Customer_id = dbo.CUSTOMERs.id INNER JOIN
                         dbo.USERs ON dbo.ACTÝVÝTY.User_id = dbo.USERs.id AND dbo.CUSTOMERs.User_id = dbo.USERs.id
WHERE        (dbo.ACTÝVÝTY.DeletStatus = 0) AND ((dbo.CUSTOMERs.Name LIKE '%' + @Search + '%') OR
                         (dbo.CUSTOMERs.Company LIKE '%' + @Search + '%') OR
                         (dbo.USERs.UserName LIKE '%' + @Search + '%') OR
                         (dbo.ACTÝVÝTY.Ýnfo LIKE '%' + @Search + '%') OR
                         (dbo.ACTÝVÝTY.Title LIKE '%' + @Search + '%'))
END
GO
