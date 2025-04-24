using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImaGen_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddSizeToImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE images ALTER COLUMN size TYPE text USING size::integer;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE images ALTER COLUMN size TYPE integer USING size::text;");
        }
    }
}
