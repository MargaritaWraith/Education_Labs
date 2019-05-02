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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER [dbo].[RatingTrigger];");
        }
    }
}
