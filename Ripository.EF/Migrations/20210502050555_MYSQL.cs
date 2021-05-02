﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EF.Migrations
{
    public partial class MYSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "Base_Department",
                columns: table => new
                {
                    DeptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeptName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    DeptCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_Department", x => x.DeptId);
                },
                comment: "部门表")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "Base_UserInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "主键")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false, comment: "账户")
                        .Annotation("MySql:CharSet", "utf8"),
                    PassWord = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false, comment: "密码")
                        .Annotation("MySql:CharSet", "utf8"),
                    UserName = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false, comment: "用户昵称")
                        .Annotation("MySql:CharSet", "utf8"),
                    Gender = table.Column<int>(type: "int", nullable: true, comment: "性别"),
                    Photo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "照片")
                        .Annotation("MySql:CharSet", "utf8"),
                    Phone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true, comment: "手机号")
                        .Annotation("MySql:CharSet", "utf8"),
                    DeptId = table.Column<int>(type: "int", maxLength: 8, nullable: true, comment: "部门ID"),
                    DeptName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "部门名称")
                        .Annotation("MySql:CharSet", "utf8"),
                    DeptCode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "部门编码")
                        .Annotation("MySql:CharSet", "utf8"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeptInfoID = table.Column<int>(type: "int", nullable: true),
                    DepartmentEntityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_UserInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Base_UserInfo_Base_Department_DepartmentEntityID",
                        column: x => x.DepartmentEntityID,
                        principalTable: "Base_Department",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Base_UserInfo_Base_Department_DeptInfoID",
                        column: x => x.DeptInfoID,
                        principalTable: "Base_Department",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "用户信息表")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateIndex(
                name: "Index_ID",
                table: "Base_Department",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "Index_Account",
                table: "Base_UserInfo",
                column: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_Base_UserInfo_Account",
                table: "Base_UserInfo",
                column: "Account",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Base_UserInfo_DepartmentEntityID",
                table: "Base_UserInfo",
                column: "DepartmentEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_Base_UserInfo_DeptInfoID",
                table: "Base_UserInfo",
                column: "DeptInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Base_UserInfo_UserName",
                table: "Base_UserInfo",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Base_UserInfo");

            migrationBuilder.DropTable(
                name: "Base_Department");
        }
    }
}
