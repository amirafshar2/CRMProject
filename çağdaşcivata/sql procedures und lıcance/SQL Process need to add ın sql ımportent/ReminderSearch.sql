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
	SELECT        dbo.REM�NDER.id, dbo.REM�NDER.Title AS Konu, dbo.REM�NDER.Reminder�nfo AS A��klama, dbo.REM�NDER.ReminDate AS [Hat�rlatma Tarihi], dbo.USERs.Name AS G�revli
FROM            dbo.REM�NDER INNER JOIN
                         dbo.USERs ON dbo.REM�NDER.Users_id = dbo.USERs.id
WHERE      ((dbo.REM�NDER.�sDone = 0) AND (dbo.REM�NDER.DeletStatus = 0)) and ((dbo.USERs.Name like '%'+@Search+'%') or (dbo.REM�NDER.Title like '%'+@Search+'%'))
END
GO
