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
	SELECT        dbo.ACT�V�TY.id, dbo.ACT�V�TY.Title AS Ba�l�k, dbo.ACT�V�TY.�nfo, dbo.CUSTOMERs.Name AS M��teri, dbo.CUSTOMERs.Company AS Firma, dbo.USERs.UserName AS G�revli
FROM            dbo.ACT�V�TY INNER JOIN
                         dbo.CUSTOMERs ON dbo.ACT�V�TY.Customer_id = dbo.CUSTOMERs.id INNER JOIN
                         dbo.USERs ON dbo.ACT�V�TY.User_id = dbo.USERs.id AND dbo.CUSTOMERs.User_id = dbo.USERs.id
WHERE        (dbo.ACT�V�TY.DeletStatus = 0) AND ((dbo.CUSTOMERs.Name LIKE '%' + @Search + '%') OR
                         (dbo.CUSTOMERs.Company LIKE '%' + @Search + '%') OR
                         (dbo.USERs.UserName LIKE '%' + @Search + '%') OR
                         (dbo.ACT�V�TY.�nfo LIKE '%' + @Search + '%') OR
                         (dbo.ACT�V�TY.Title LIKE '%' + @Search + '%'))
END
GO
