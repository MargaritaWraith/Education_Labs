using Microsoft.EntityFrameworkCore.Migrations;

namespace Education.DAL.Migrations
{
    public partial class DbInfrastuctureTriggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
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
");

            migrationBuilder.Sql(@"
CREATE PROCEDURE AddLabWork 
	@StudentId int, 
	@WorkId int,
	@Order int
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[StudentsLabWorks] VALUES (@StudentId, @WorkId, @Order);
END
");

            migrationBuilder.Sql(@"
CREATE PROCEDURE [dbo].[KillBadStudents]
AS
BEGIN
	DECLARE @StudentId int

	DECLARE StudentsCursor CURSOR FOR
		SELECT Id
		FROM [dbo].[Students]

	OPEN StudentsCursor

	FETCH NEXT FROM StudentsCursor INTO @StudentId

	WHILE @@FETCH_STATUS=0
		BEGIN
		  IF NOT EXISTS (SELECT * FROM [dbo].[StudentsLabWorks] WHERE StudentId = @StudentId)
			DELETE FROM [dbo].[Students] WHERE Id = @StudentId

			FETCH NEXT FROM StudentsCursor INTO @StudentId
		END

	CLOSE StudentsCursor
	DEALLOCATE StudentsCursor
END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER [dbo].[RatingTrigger];");
            migrationBuilder.Sql(@"DROP PROCEDURE [dbo].[AddLabWork];");
            migrationBuilder.Sql(@"DROP PROCEDURE [dbo].[KillBadStudents];");
        }
    }
}
