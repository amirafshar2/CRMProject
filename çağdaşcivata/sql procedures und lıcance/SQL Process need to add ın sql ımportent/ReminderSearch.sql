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
CREATE PROCEDURE ReminderSearch
	@Search nvarchar(max)
AS
BEGIN
	SELECT        dbo.REMÝNDER.id, dbo.REMÝNDER.Title AS Konu, dbo.REMÝNDER.ReminderÝnfo AS Açýklama, dbo.REMÝNDER.ReminDate AS [Hatýrlatma Tarihi], dbo.USERs.Name AS Görevli
FROM            dbo.REMÝNDER INNER JOIN
                         dbo.USERs ON dbo.REMÝNDER.Users_id = dbo.USERs.id
WHERE      ((dbo.REMÝNDER.ÝsDone = 0) AND (dbo.REMÝNDER.DeletStatus = 0)) and ((dbo.USERs.Name like '%'+@Search+'%') or (dbo.REMÝNDER.Title like '%'+@Search+'%'))
END
GO
