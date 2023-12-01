
CREATE PROCEDURE Aktivitydetale
	@search int 
AS
BEGIN
	SELECT        dbo.ACTÝVÝTY.id, dbo.CUSTOMERs.Company AS [Muþteri Firmasý], dbo.ACTÝVÝTY.User_id AS GörevliKodu, dbo.ACTÝVÝTY.Ýnfo AS Açýklama, dbo.ACTÝVÝTY.RegDate AS [Kayýt tarihi], dbo.CUSTOMERs.Name AS [Müþteri isim], 
                         dbo.CUSTOMERs.Phone AS [Tel No], dbo.CUSTOMERs.Email AS email
FROM            dbo.ACTÝVÝTY INNER JOIN
                         dbo.ACTÝVÝTY_CATEGORY ON dbo.ACTÝVÝTY.ActivityCategory_id = dbo.ACTÝVÝTY_CATEGORY.id INNER JOIN
                         dbo.CUSTOMERs ON dbo.ACTÝVÝTY.Customer_id = dbo.CUSTOMERs.id
WHERE        (dbo.ACTÝVÝTY.DeletStatus = 0) AND (dbo.ACTÝVÝTY.id = @Search)
END
GO
