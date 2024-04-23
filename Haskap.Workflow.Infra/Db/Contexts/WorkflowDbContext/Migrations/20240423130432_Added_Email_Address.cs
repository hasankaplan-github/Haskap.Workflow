using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext.Migrations
{
    /// <inheritdoc />
    public partial class Added_Email_Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email_address",
                table: "user",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_address",
                table: "user");
        }
    }
}
