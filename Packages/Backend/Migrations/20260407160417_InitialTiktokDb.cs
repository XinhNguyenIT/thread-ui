using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialTiktokDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Avatar = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__1788CC4C713CF253", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserCommentLike",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserComm__12CC530E3B975D49", x => new { x.CommentId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__349DA5A60A9E773E", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK__Account__UserId__49C3F6B7",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "RelationshipFromUserToUser",
                columns: table => new
                {
                    RelationshipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUserId = table.Column<int>(type: "int", nullable: false),
                    ToUserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "FOLLOWER")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Relation__31FEB881450CA443", x => x.RelationshipId);
                    table.ForeignKey(
                        name: "FK__Relations__FromU__4BAC3F29",
                        column: x => x.FromUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Relations__ToUse__4CA06362",
                        column: x => x.ToUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    VideoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Src = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Video__BAE5126A3FB86A88", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK__Video__UserId__4AB81AF0",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    FromUserId = table.Column<int>(type: "int", nullable: false),
                    ToVideoId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<byte>(type: "tinyint", nullable: false),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comment__C3B4DFCA67E6451A", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK__Comment__FromUse__4D94879B",
                        column: x => x.FromUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Comment__ParentC__4E88ABD4",
                        column: x => x.ParentCommentId,
                        principalTable: "Comment",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK__Comment__ToVideo__4F7CD00D",
                        column: x => x.ToVideoId,
                        principalTable: "Video",
                        principalColumn: "VideoId");
                });

            migrationBuilder.CreateTable(
                name: "UserVideoLike",
                columns: table => new
                {
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserVide__6B9D9EAE2FB88AEB", x => new { x.VideoId, x.UserId });
                    table.ForeignKey(
                        name: "FK__UserVideo__UserI__5070F446",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__UserVideo__Video__5165187F",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "VideoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FromUserId",
                table: "Comment",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ToVideoId",
                table: "Comment",
                column: "ToVideoId");

            migrationBuilder.CreateIndex(
                name: "UQ__Comment__C3B4DFCB928A9202",
                table: "Comment",
                column: "CommentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipFromUserToUser_FromUserId",
                table: "RelationshipFromUserToUser",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipFromUserToUser_ToUserId",
                table: "RelationshipFromUserToUser",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVideoLike_UserId",
                table: "UserVideoLike",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_UserId",
                table: "Video",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "RelationshipFromUserToUser");

            migrationBuilder.DropTable(
                name: "UserCommentLike");

            migrationBuilder.DropTable(
                name: "UserVideoLike");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
