
CREATE PROCEDURE Aktivitydetale
	@search int 
AS
BEGIN
	SELECT        dbo.ACT�V�TY.id, dbo.CUSTOMERs.Company AS [Mu�teri Firmas�], dbo.ACT�V�TY.User_id AS G�revliKodu, dbo.ACT�V�TY.�nfo AS A��klama, dbo.ACT�V�TY.RegDate AS [Kay�t tarihi], dbo.CUSTOMERs.Name AS [M��teri isim], 
                         dbo.CUSTOMERs.Phone AS [Tel No], dbo.CUSTOMERs.Email AS email
FROM            dbo.ACT�V�TY INNER JOIN
                         dbo.ACT�V�TY_CATEGORY ON dbo.ACT�V�TY.ActivityCategory_id = dbo.ACT�V�TY_CATEGORY.id INNER JOIN
                         dbo.CUSTOMERs ON dbo.ACT�V�TY.Customer_id = dbo.CUSTOMERs.id
WHERE        (dbo.ACT�V�TY.DeletStatus = 0) AND (dbo.ACT�V�TY.id = @Search)
END
GO
