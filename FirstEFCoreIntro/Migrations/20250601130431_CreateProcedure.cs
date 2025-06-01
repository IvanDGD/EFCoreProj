using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstEFCoreIntro.Migrations
{
    /// <inheritdoc />
    public partial class CreateProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE p_AddGroup
                    @Name NVARCHAR(450)
                AS
                BEGIN
                    INSERT INTO Groups ([Name])
                    VALUES (@Name)
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE p_AddGroup");
        }
    }
}
