using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImaGen_BE.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_images_user_id",
                table: "images",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_images_users_user_id",
                table: "images",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_images_users_user_id",
                table: "images");

            migrationBuilder.DropIndex(
                name: "ix_images_user_id",
                table: "images");
        }
    }
}
