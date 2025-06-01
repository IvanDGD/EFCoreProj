using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstEFCoreIntro.Migrations
{
    /// <inheritdoc />
    public partial class StudentGroupView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW vw_StudentGroup AS 
                SELECT s.Name As StudentName, g.Name AS GroupsName
                FROM Students s
                JOIN Groups g ON s.GroupId = g.Id
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW vw_StudentGroup");
        }
    }
}
