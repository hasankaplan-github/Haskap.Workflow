using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "audit_history_log",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    visit_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    modification_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    modification_type = table.Column<string>(type: "text", nullable: false),
                    object_full_type = table.Column<string>(type: "text", nullable: false),
                    object_ids = table.Column<string>(type: "text", nullable: true),
                    object_original_values = table.Column<string>(type: "text", nullable: true),
                    object_new_values = table.Column<string>(type: "text", nullable: true),
                    ownership_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_history_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "process",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_process", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    credentials_user_name = table.Column<string>(type: "text", nullable: false),
                    credentials_password_hashed_value = table.Column<string>(type: "text", nullable: false),
                    credentials_password_salt_value = table.Column<string>(type: "text", nullable: false),
                    system_time_zone_id = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "view_level_exception",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    stack_trace = table.Column<string>(type: "text", nullable: true),
                    occured_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_view_level_exception", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "command",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    process_id = table.Column<Guid>(type: "uuid", nullable: false),
                    display_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_command", x => x.id);
                    table.ForeignKey(
                        name: "fk_command_process_process_id",
                        column: x => x.process_id,
                        principalTable: "process",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "state",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    display_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    process_id = table.Column<Guid>(type: "uuid", nullable: false),
                    state_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_state", x => x.id);
                    table.ForeignKey(
                        name: "fk_state_process_process_id",
                        column: x => x.process_id,
                        principalTable: "process",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_permissions_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_permissions_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_role_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "path",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    process_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_state_id = table.Column<Guid>(type: "uuid", nullable: false),
                    to_state_id = table.Column<Guid>(type: "uuid", nullable: false),
                    command_id = table.Column<Guid>(type: "uuid", nullable: false),
                    view_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_path", x => x.id);
                    table.ForeignKey(
                        name: "fk_path_command_command_id",
                        column: x => x.command_id,
                        principalTable: "command",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_path_process_process_id",
                        column: x => x.process_id,
                        principalTable: "process",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_path_state_from_state_id",
                        column: x => x.from_state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_path_state_to_state_id",
                        column: x => x.to_state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "request",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    process_id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    current_state_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_request", x => x.id);
                    table.ForeignKey(
                        name: "fk_request_process_process_id",
                        column: x => x.process_id,
                        principalTable: "process",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_request_state_current_state_id",
                        column: x => x.current_state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_request_user_owner_user_id",
                        column: x => x.owner_user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "path_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    path_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_path_role", x => x.id);
                    table.ForeignKey(
                        name: "fk_path_role_path_path_id",
                        column: x => x.path_id,
                        principalTable: "path",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_path_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "process1request_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    request_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_process1request_data", x => x.id);
                    table.ForeignKey(
                        name: "fk_process1request_data_request_request_id",
                        column: x => x.request_id,
                        principalTable: "request",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "progress",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    request_id = table.Column<Guid>(type: "uuid", nullable: false),
                    path_id = table.Column<Guid>(type: "uuid", nullable: false),
                    progress_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    owner_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_progress", x => x.id);
                    table.ForeignKey(
                        name: "fk_progress_path_path_id",
                        column: x => x.path_id,
                        principalTable: "path",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_progress_request_request_id",
                        column: x => x.request_id,
                        principalTable: "request",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "note_progress_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    progress_id = table.Column<Guid>(type: "uuid", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_note_progress_data", x => x.id);
                    table.ForeignKey(
                        name: "fk_note_progress_data_progress_progress_id",
                        column: x => x.progress_id,
                        principalTable: "progress",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_command_process_id",
                table: "command",
                column: "process_id");

            migrationBuilder.CreateIndex(
                name: "ix_note_progress_data_progress_id",
                table: "note_progress_data",
                column: "progress_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_path_command_id",
                table: "path",
                column: "command_id");

            migrationBuilder.CreateIndex(
                name: "ix_path_from_state_id",
                table: "path",
                column: "from_state_id");

            migrationBuilder.CreateIndex(
                name: "ix_path_process_id",
                table: "path",
                column: "process_id");

            migrationBuilder.CreateIndex(
                name: "ix_path_to_state_id",
                table: "path",
                column: "to_state_id");

            migrationBuilder.CreateIndex(
                name: "ix_path_role_path_id",
                table: "path_role",
                column: "path_id");

            migrationBuilder.CreateIndex(
                name: "ix_path_role_role_id",
                table: "path_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_process1request_data_request_id",
                table: "process1request_data",
                column: "request_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_progress_path_id",
                table: "progress",
                column: "path_id");

            migrationBuilder.CreateIndex(
                name: "ix_progress_request_id",
                table: "progress",
                column: "request_id");

            migrationBuilder.CreateIndex(
                name: "ix_request_current_state_id",
                table: "request",
                column: "current_state_id");

            migrationBuilder.CreateIndex(
                name: "ix_request_owner_user_id",
                table: "request",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_request_process_id",
                table: "request",
                column: "process_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_role_id",
                table: "role_permissions",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_state_process_id",
                table: "state",
                column: "process_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_credentials_user_name",
                table: "user",
                column: "credentials_user_name");

            migrationBuilder.CreateIndex(
                name: "ix_user_permissions_user_id",
                table: "user_permissions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_role_id",
                table: "user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_user_id",
                table: "user_role",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_history_log");

            migrationBuilder.DropTable(
                name: "note_progress_data");

            migrationBuilder.DropTable(
                name: "path_role");

            migrationBuilder.DropTable(
                name: "process1request_data");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "user_permissions");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "view_level_exception");

            migrationBuilder.DropTable(
                name: "progress");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "path");

            migrationBuilder.DropTable(
                name: "request");

            migrationBuilder.DropTable(
                name: "command");

            migrationBuilder.DropTable(
                name: "state");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "process");
        }
    }
}
