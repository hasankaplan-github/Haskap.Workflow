﻿// <auto-generated />
using System;
using Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Haskap.DddBase.Domain.AuditHistoryLogAggregate.AuditHistoryLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("ModificationDateUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modification_date_utc");

                    b.Property<string>("ModificationType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("modification_type");

                    b.Property<Guid?>("ModifiedUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("modified_user_id");

                    b.Property<string>("ObjectFullType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("object_full_type");

                    b.Property<string>("ObjectIds")
                        .HasColumnType("text")
                        .HasColumnName("object_ids");

                    b.Property<string>("ObjectNewValues")
                        .HasColumnType("text")
                        .HasColumnName("object_new_values");

                    b.Property<string>("ObjectOriginalValues")
                        .HasColumnType("text")
                        .HasColumnName("object_original_values");

                    b.Property<string>("OwnershipType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ownership_type");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid")
                        .HasColumnName("tenant_id");

                    b.Property<Guid?>("VisitId")
                        .HasColumnType("uuid")
                        .HasColumnName("visit_id");

                    b.HasKey("Id")
                        .HasName("pk_audit_history_log");

                    b.ToTable("audit_history_log", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.Process1Aggregate.NoteProgressData", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Note")
                        .HasColumnType("text")
                        .HasColumnName("note");

                    b.Property<Guid>("ProgressId")
                        .HasColumnType("uuid")
                        .HasColumnName("progress_id");

                    b.HasKey("Id")
                        .HasName("pk_note_progress_data");

                    b.HasIndex("ProgressId")
                        .IsUnique()
                        .HasDatabaseName("ix_note_progress_data_progress_id");

                    b.ToTable("note_progress_data", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.Process1Aggregate.RequestData", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<Guid>("RequestId")
                        .HasColumnType("uuid")
                        .HasColumnName("request_id");

                    b.HasKey("Id")
                        .HasName("pk_process1request_data");

                    b.HasIndex("RequestId")
                        .IsUnique()
                        .HasDatabaseName("ix_process1request_data_request_id");

                    b.ToTable("process1request_data", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Command", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("display_name");

                    b.Property<Guid>("ProcessId")
                        .HasColumnType("uuid")
                        .HasColumnName("process_id");

                    b.HasKey("Id")
                        .HasName("pk_command");

                    b.HasIndex("ProcessId")
                        .HasDatabaseName("ix_command_process_id");

                    b.ToTable("command", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Path", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CommandId")
                        .HasColumnType("uuid")
                        .HasColumnName("command_id");

                    b.Property<Guid>("FromStateId")
                        .HasColumnType("uuid")
                        .HasColumnName("from_state_id");

                    b.Property<Guid>("ProcessId")
                        .HasColumnType("uuid")
                        .HasColumnName("process_id");

                    b.Property<Guid>("ToStateId")
                        .HasColumnType("uuid")
                        .HasColumnName("to_state_id");

                    b.Property<string>("ViewName")
                        .HasColumnType("text")
                        .HasColumnName("view_name");

                    b.HasKey("Id")
                        .HasName("pk_path");

                    b.HasIndex("CommandId")
                        .HasDatabaseName("ix_path_command_id");

                    b.HasIndex("FromStateId")
                        .HasDatabaseName("ix_path_from_state_id");

                    b.HasIndex("ProcessId")
                        .HasDatabaseName("ix_path_process_id");

                    b.HasIndex("ToStateId")
                        .HasDatabaseName("ix_path_to_state_id");

                    b.ToTable("path", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.PathRole", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("PathId")
                        .HasColumnType("uuid")
                        .HasColumnName("path_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_path_role");

                    b.HasIndex("PathId")
                        .HasDatabaseName("ix_path_role_path_id");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_path_role_role_id");

                    b.ToTable("path_role", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Process", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_process");

                    b.ToTable("process", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Progress", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_user_id");

                    b.Property<Guid>("PathId")
                        .HasColumnType("uuid")
                        .HasColumnName("path_id");

                    b.Property<DateTime>("ProgressDateUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("progress_date_utc");

                    b.Property<Guid>("RequestId")
                        .HasColumnType("uuid")
                        .HasColumnName("request_id");

                    b.HasKey("Id")
                        .HasName("pk_progress");

                    b.HasIndex("PathId")
                        .HasDatabaseName("ix_progress_path_id");

                    b.HasIndex("RequestId")
                        .HasDatabaseName("ix_progress_request_id");

                    b.ToTable("progress", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CurrentStateId")
                        .HasColumnType("uuid")
                        .HasColumnName("current_state_id");

                    b.Property<Guid?>("OwnerUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_user_id");

                    b.Property<Guid>("ProcessId")
                        .HasColumnType("uuid")
                        .HasColumnName("process_id");

                    b.HasKey("Id")
                        .HasName("pk_request");

                    b.HasIndex("CurrentStateId")
                        .HasDatabaseName("ix_request_current_state_id");

                    b.HasIndex("OwnerUserId")
                        .HasDatabaseName("ix_request_owner_user_id");

                    b.HasIndex("ProcessId")
                        .HasDatabaseName("ix_request_process_id");

                    b.ToTable("request", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.State", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("display_name");

                    b.Property<Guid>("ProcessId")
                        .HasColumnType("uuid")
                        .HasColumnName("process_id");

                    b.Property<string>("StateType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state_type");

                    b.HasKey("Id")
                        .HasName("pk_state");

                    b.HasIndex("ProcessId")
                        .HasDatabaseName("ix_state_process_id");

                    b.ToTable("state", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.RoleAggregate.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_role");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("last_name");

                    b.Property<string>("SystemTimeZoneId")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("system_time_zone_id");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.UserAggregate.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_role");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_role_role_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_role_user_id");

                    b.ToTable("user_role", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ViewLevelExceptionAggregate.ViewLevelException", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<DateTime>("OccuredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occured_on_utc");

                    b.Property<string>("StackTrace")
                        .HasColumnType("text")
                        .HasColumnName("stack_trace");

                    b.HasKey("Id")
                        .HasName("pk_view_level_exception");

                    b.ToTable("view_level_exception", (string)null);
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.Process1Aggregate.NoteProgressData", b =>
                {
                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.Progress", null)
                        .WithOne()
                        .HasForeignKey("Haskap.Workflow.Domain.Process1Aggregate.NoteProgressData", "ProgressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_note_progress_data_progress_progress_id");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.Process1Aggregate.RequestData", b =>
                {
                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.Request", null)
                        .WithOne()
                        .HasForeignKey("Haskap.Workflow.Domain.Process1Aggregate.RequestData", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_process1request_data_request_request_id");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Command", b =>
                {
                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.Process", null)
                        .WithMany("Commands")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_command_process_process_id");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Path", b =>
                {
                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.Command", "Command")
                        .WithMany()
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_path_command_command_id");

                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.State", "FromState")
                        .WithMany()
                        .HasForeignKey("FromStateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_path_state_from_state_id");

                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.Process", null)
                        .WithMany("Paths")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_path_process_process_id");

                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.State", "ToState")
                        .WithMany()
                        .HasForeignKey("ToStateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_path_state_to_state_id");

                    b.Navigation("Command");

                    b.Navigation("FromState");

                    b.Navigation("ToState");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.PathRole", b =>
                {
                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.Path", null)
                        .WithMany("Roles")
                        .HasForeignKey("PathId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_path_role_path_path_id");

                    b.HasOne("Haskap.Workflow.Domain.RoleAggregate.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_path_role_role_role_id");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Progress", b =>
                {
                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.Path", null)
                        .WithMany()
                        .HasForeignKey("PathId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_progress_path_path_id");

                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.Request", null)
                        .WithMany("Progresses")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_progress_request_request_id");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Request", b =>
                {
                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.State", "CurrentState")
                        .WithMany()
                        .HasForeignKey("CurrentStateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_request_state_current_state_id");

                    b.HasOne("Haskap.Workflow.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_request_user_owner_user_id");

                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.Process", null)
                        .WithMany("Requests")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_request_process_process_id");

                    b.Navigation("CurrentState");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.State", b =>
                {
                    b.HasOne("Haskap.Workflow.Domain.ProcessAggregate.Process", null)
                        .WithMany("States")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_state_process_process_id");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.RoleAggregate.Role", b =>
                {
                    b.OwnsMany("Haskap.Workflow.Domain.Common.Permission", "Permissions", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("name");

                            b1.Property<Guid>("RoleId")
                                .HasColumnType("uuid")
                                .HasColumnName("role_id");

                            b1.HasKey("Id")
                                .HasName("pk_role_permissions");

                            b1.HasIndex("RoleId")
                                .HasDatabaseName("ix_role_permissions_role_id");

                            b1.ToTable("role_permissions", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RoleId")
                                .HasConstraintName("fk_role_permissions_role_role_id");
                        });

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.UserAggregate.User", b =>
                {
                    b.OwnsOne("Haskap.Workflow.Domain.UserAggregate.Credentials", "Credentials", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("UserName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("credentials_user_name");

                            b1.HasKey("UserId");

                            b1.HasIndex("UserName")
                                .HasDatabaseName("ix_user_credentials_user_name");

                            b1.ToTable("user");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_user_user_id");

                            b1.OwnsOne("Haskap.Workflow.Domain.UserAggregate.Password", "Password", b2 =>
                                {
                                    b2.Property<Guid>("CredentialsUserId")
                                        .HasColumnType("uuid")
                                        .HasColumnName("id");

                                    b2.Property<string>("HashedValue")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("credentials_password_hashed_value");

                                    b2.HasKey("CredentialsUserId");

                                    b2.ToTable("user");

                                    b2.WithOwner()
                                        .HasForeignKey("CredentialsUserId")
                                        .HasConstraintName("fk_user_user_id");

                                    b2.OwnsOne("Haskap.Workflow.Domain.UserAggregate.Salt", "Salt", b3 =>
                                        {
                                            b3.Property<Guid>("PasswordCredentialsUserId")
                                                .HasColumnType("uuid")
                                                .HasColumnName("id");

                                            b3.Property<string>("Value")
                                                .IsRequired()
                                                .HasColumnType("text")
                                                .HasColumnName("credentials_password_salt_value");

                                            b3.HasKey("PasswordCredentialsUserId");

                                            b3.ToTable("user");

                                            b3.WithOwner()
                                                .HasForeignKey("PasswordCredentialsUserId")
                                                .HasConstraintName("fk_user_user_id");
                                        });

                                    b2.Navigation("Salt")
                                        .IsRequired();
                                });

                            b1.Navigation("Password")
                                .IsRequired();
                        });

                    b.OwnsMany("Haskap.Workflow.Domain.Common.Permission", "Permissions", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("name");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid")
                                .HasColumnName("user_id");

                            b1.HasKey("Id")
                                .HasName("pk_user_permissions");

                            b1.HasIndex("UserId")
                                .HasDatabaseName("ix_user_permissions_user_id");

                            b1.ToTable("user_permissions", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_user_permissions_user_user_id");
                        });

                    b.Navigation("Credentials")
                        .IsRequired();

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.UserAggregate.UserRole", b =>
                {
                    b.HasOne("Haskap.Workflow.Domain.RoleAggregate.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_role_role_id");

                    b.HasOne("Haskap.Workflow.Domain.UserAggregate.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_user_user_id");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Path", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Process", b =>
                {
                    b.Navigation("Commands");

                    b.Navigation("Paths");

                    b.Navigation("Requests");

                    b.Navigation("States");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.ProcessAggregate.Request", b =>
                {
                    b.Navigation("Progresses");
                });

            modelBuilder.Entity("Haskap.Workflow.Domain.UserAggregate.User", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
