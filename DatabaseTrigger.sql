CREATE TRIGGER [dbo].[RatingTrigger]
	ON [dbo].[StudentsLabWorks]
	AFTER INSERT, UPDATE 
AS

IF EXISTS (SELECT Rating FROM [dbo].[StudentsLabWorks]
			WHERE Rating < 1 OR Rating > 5)

BEGIN
	SET NOCOUNT ON;
	RAISERROR ('Недопустимое значение оценки',16,1);
	ROLLBACK TRANSACTION;  
	RETURN
END