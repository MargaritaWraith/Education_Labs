CREATE TRIGGER [RatingTrigger]
ON [dbo].[StudentsLabWorks]

AFTER
INSERT 
AS
IF ([Rating]<=5)
RETURN;
IF ([Rating]>=1)
RETURN;

BEGIN
	RAISERROR ('Недопустимое значение оценки',16,1);
	ROLLBACK TRANSACTION;  
	RETURN
END;
