using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLikeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Stories_LikeCount_Min",
                table: "Stories",
                sql: "[LikeCount] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Posts_LikeCount_Min",
                table: "Posts",
                sql: "[LikeCount] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Comments_LikeCount_Min",
                table: "Comments",
                sql: "[LikeCount] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Stories_LikeCount_Min",
                table: "Stories");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Posts_LikeCount_Min",
                table: "Posts");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Comments_LikeCount_Min",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Comments");
        }
    }
}
