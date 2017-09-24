﻿CREATE PROCEDURE [dbo].[SelectPerson]
@IdPerson uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Name,LastName,Alias,Age,Picture,Location,Notes,FaceId FROM Person WHERE IdPerson = @IdPerson AND IsActive = 1
END

GO