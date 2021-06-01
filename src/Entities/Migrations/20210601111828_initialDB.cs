using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class initialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "WebBase");

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    EntityName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    EntityId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Icon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    FunctionId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    RoleId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CommandId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => new { x.CommandId, x.RoleId, x.FunctionId });
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRoleClaims_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserClaims_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AppUserLogins_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AppUserTokens_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandInFunctions",
                columns: table => new
                {
                    CommandId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FunctionId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandInFunctions", x => new { x.CommandId, x.FunctionId });
                    table.ForeignKey(
                        name: "FK_CommandInFunctions_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandInFunctions_Functions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Functions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2ba7403d-9288-4661-9450-5e27d0d5e183"), "d4e46879-b035-426c-ac1c-8fc857509d24", "Admin role", "Admin", "ADMIN" },
                    { new Guid("412eb2fd-25a7-4293-b04b-14b5ad4bc6cc"), "9e86498b-3beb-4234-b8c4-a316eb79a596", "User role", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreateDate", "Dob", "Email", "EmailConfirmed", "FirstName", "LastModifiedDate", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("09e9083f-69b6-499e-9eb0-723f2ec0a875"), 0, "18e13a3e-2f75-4eed-bcd6-abf385eba8a0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 1, 18, 18, 27, 569, DateTimeKind.Local).AddTicks(1102), "hai.tc21@gmail.com", false, "Quản trị", null, "tranhai", false, null, null, null, "aDMIN@123", null, false, null, false, "admin" },
                    { new Guid("60751aed-eb56-479d-adc6-7390248ff086"), 0, "89bff26a-175a-46b0-8c27-3679a77057af", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 1, 18, 18, 27, 569, DateTimeKind.Local).AddTicks(1102), "hai.tc21@gmail.com", false, "Trần", null, "Hải", false, null, null, null, null, null, false, null, false, "TranHai" }
                });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "VIEW", "Xem" },
                    { "CREATE", "Thêm" },
                    { "UPDATE", "Sửa" },
                    { "DELETE", "Xoá" },
                    { "APPROVE", "Duyệt" }
                });

            migrationBuilder.InsertData(
                table: "Functions",
                columns: new[] { "Id", "Icon", "Name", "ParentId", "SortOrder", "Url" },
                values: new object[,]
                {
                    { "SYSTEM_PERMISSION", "fa-desktop", "Quyền hạn", "SYSTEM", 0, "/systems/permissions" },
                    { "SYSTEM_FUNCTION", "fa-desktop", "Chức năng", "SYSTEM", 0, "/systems/functions" },
                    { "SYSTEM_ROLE", "fa-desktop", "Nhóm quyền", "SYSTEM", 0, "/systems/roles" },
                    { "SYSTEM", "fa-th-list", "Hệ thống", null, 0, "/systems" },
                    { "DASHBOARD", "fa-dashboard", "DashBoard", null, 1, "/dashboard" },
                    { "SYSTEM_USER", "fa-desktop", "Người dùng", "SYSTEM", 0, "/systems/users" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "CommandId", "FunctionId", "RoleId" },
                values: new object[,]
                {
                    { "CREATE", "SYSTEM_ROLE", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "UPDATE", "SYSTEM_PERMISSION", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "CREATE", "SYSTEM_PERMISSION", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "VIEW", "SYSTEM_FUNCTION", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "DELETE", "SYSTEM_FUNCTION", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "UPDATE", "SYSTEM_FUNCTION", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "CREATE", "SYSTEM_FUNCTION", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "VIEW", "SYSTEM_ROLE", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "DELETE", "SYSTEM_ROLE", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "UPDATE", "SYSTEM_ROLE", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "VIEW", "SYSTEM_USER", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "CREATE", "SYSTEM", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "UPDATE", "SYSTEM_USER", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "CREATE", "SYSTEM_USER", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "VIEW", "SYSTEM", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "DELETE", "SYSTEM", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "UPDATE", "SYSTEM", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "DELETE", "SYSTEM_PERMISSION", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "VIEW", "DASHBOARD", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "DELETE", "DASHBOARD", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "UPDATE", "DASHBOARD", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "CREATE", "DASHBOARD", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "DELETE", "SYSTEM_USER", "2ba7403d-9288-4661-9450-5e27d0d5e183" },
                    { "VIEW", "SYSTEM_PERMISSION", "2ba7403d-9288-4661-9450-5e27d0d5e183" }
                });

            migrationBuilder.InsertData(
                table: "CommandInFunctions",
                columns: new[] { "CommandId", "FunctionId" },
                values: new object[,]
                {
                    { "CREATE", "DASHBOARD" },
                    { "UPDATE", "SYSTEM_PERMISSION" },
                    { "CREATE", "SYSTEM_PERMISSION" },
                    { "VIEW", "SYSTEM_FUNCTION" },
                    { "DELETE", "SYSTEM_FUNCTION" },
                    { "UPDATE", "SYSTEM_FUNCTION" },
                    { "CREATE", "SYSTEM_FUNCTION" },
                    { "VIEW", "SYSTEM_ROLE" },
                    { "DELETE", "SYSTEM_ROLE" },
                    { "UPDATE", "SYSTEM_ROLE" },
                    { "CREATE", "SYSTEM_ROLE" },
                    { "VIEW", "SYSTEM_USER" },
                    { "DELETE", "SYSTEM_USER" },
                    { "UPDATE", "SYSTEM_USER" },
                    { "CREATE", "SYSTEM_USER" },
                    { "VIEW", "SYSTEM" },
                    { "DELETE", "SYSTEM" },
                    { "UPDATE", "SYSTEM" },
                    { "CREATE", "SYSTEM" },
                    { "VIEW", "DASHBOARD" },
                    { "DELETE", "DASHBOARD" },
                    { "UPDATE", "DASHBOARD" },
                    { "DELETE", "SYSTEM_PERMISSION" },
                    { "VIEW", "SYSTEM_PERMISSION" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleClaims_RoleId",
                table: "AppRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AppRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClaims_UserId",
                table: "AppUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_RoleId",
                table: "AppUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AppUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AppUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CommandInFunctions_FunctionId",
                table: "CommandInFunctions",
                column: "FunctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "CommandInFunctions");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "Functions");

            migrationBuilder.DropSequence(
                name: "WebBase");
        }
    }
}
