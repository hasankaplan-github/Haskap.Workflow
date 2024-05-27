using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext.Migrations
{
    /// <inheritdoc />
    public partial class Partial_Entity_Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "process1request_data",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "process1request_data");
        }
    }
}
