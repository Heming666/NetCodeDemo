using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EF.Migrations
{
    public partial class _2010502 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptCode",
                table: "Base_UserInfo");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Base_UserInfo");

            migrationBuilder.DropColumn(
                name: "DeptName",
                table: "Base_UserInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeptCode",
                table: "Base_UserInfo",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "部门编码")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Base_UserInfo",
                type: "int",
                maxLength: 8,
                nullable: true,
                comment: "部门ID");

            migrationBuilder.AddColumn<string>(
                name: "DeptName",
                table: "Base_UserInfo",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "部门名称")
                .Annotation("MySql:CharSet", "utf8");
        }
    }
}
