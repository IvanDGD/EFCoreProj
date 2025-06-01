 using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstEFCoreIntro.Migrations
{
    /// <inheritdoc />
    public partial class CreateSecondProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Scholarship",
                table: "Students",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
            migrationBuilder.Sql(@"
                CREATE VIEW View_SubjectsDepartments AS
                SELECT 
                    s.Id AS SubjectId,
                    s.Name AS SubjectName,
                    s.Description AS SubjectDescription,
                    s.Time AS SubjectTime,
                    d.Id AS DepartmentId,
                    d.Name AS DepartmentName,
                    d.Description AS DepartmentDescription,
                    d.Financing
                FROM Subjects s
                JOIN Departments d ON s.DepartmentId = d.Id;
            ");
            migrationBuilder.Sql(@"
                CREATE PROCEDURE AddTeacher
                    @FullName NVARCHAR(100),
                    @Age INT,
                    @Salary REAL
                AS
                BEGIN
                    INSERT INTO Teachers (FullName, Age, Salary)
                    VALUES (@FullName, @Age, @Salary);
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Scholarship",
                table: "Students",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
            migrationBuilder.Sql("DROP VIEW View_SubjectsDepartments");
            migrationBuilder.Sql("DROP PROCEDURE p_AddTeacher");
        }
    }
}
