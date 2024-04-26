using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext.Migrations
{
    /// <inheritdoc />
    public partial class User_Is_Active_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tenant",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenant", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tenant");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "user");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "user");
        }
    }
}
