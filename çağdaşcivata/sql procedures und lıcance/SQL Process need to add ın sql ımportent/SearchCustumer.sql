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
CREATE PROCEDURE SearchCustumer
@Search nvarchar(max)
AS
BEGIN
	SELECT        dbo.CUSTOMERs.id, dbo.CUSTOMERs.Name AS Ýsim, dbo.CUSTOMERs.Company AS Firma, dbo.CUSTOMERs.Phone AS [Telefon NO], dbo.CUSTOMERs.Email AS [E-mail Adresi], dbo.CUSTOMERs.Regdate AS [Kayýt Tarihi], 
                         dbo.CUSTOMERs.Alacak, dbo.CUSTOMERs.Bakiye, dbo.USERs.Name AS [Müþtri Temsilcisi]
FROM            dbo.CUSTOMERs INNER JOIN
                         dbo.USERs ON dbo.CUSTOMERs.User_id = dbo.USERs.id
WHERE        (dbo.CUSTOMERs.DeletStatus = 0) AND ((dbo.CUSTOMERs.Name LIKE '%' + @Search + '%') OR
                         (dbo.CUSTOMERs.Company LIKE '%' + @Search + '%') OR
                         (dbo.CUSTOMERs.Phone LIKE '%' + @Search + '%') OR
                         (dbo.CUSTOMERs.Email LIKE '%' + @Search + '%') OR
                         (dbo.USERs.Name LIKE '%' + @Search + '%'))
END
GO
