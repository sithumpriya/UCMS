using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseResultModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseResult_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseResult_CourseId",
                table: "CourseResult",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseResult");
        }
    }
}
