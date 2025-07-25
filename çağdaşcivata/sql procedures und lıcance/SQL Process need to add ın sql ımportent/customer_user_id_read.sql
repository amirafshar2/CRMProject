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
CREATE PROCEDURE customer_user_id_read
@Search int
AS
BEGIN
	SELECT        dbo.USERs.id, dbo.CUSTOMERs.id AS Expr1
FROM            dbo.CUSTOMERs INNER JOIN
                         dbo.ÝNVOÝCE ON dbo.CUSTOMERs.id = dbo.ÝNVOÝCE.Customer_id INNER JOIN
                         dbo.USERs ON dbo.CUSTOMERs.User_id = dbo.USERs.id AND dbo.ÝNVOÝCE.User_id = dbo.USERs.id
WHERE        (dbo.ÝNVOÝCE.Deletestatus = 0) AND (dbo.CUSTOMERs.id = @Search)
END
GO
