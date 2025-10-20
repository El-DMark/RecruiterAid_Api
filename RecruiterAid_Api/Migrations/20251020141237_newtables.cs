using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruiterAid_Api.Migrations
{
    /// <inheritdoc />
    public partial class newtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Qualification",
                table: "JobPostings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "JobPostings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "EmploymentType",
                table: "JobPostings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ScheduledByUserId",
                table: "Interviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerUserId",
                table: "Interviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InterviewerUserId",
                table: "InterviewFeedback",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_OrganizerUserId",
                table: "Interviews",
                column: "OrganizerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_ScheduledByUserId",
                table: "Interviews",
                column: "ScheduledByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewFeedback_InterviewerUserId",
                table: "InterviewFeedback",
                column: "InterviewerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewFeedback_AppUsers_InterviewerUserId",
                table: "InterviewFeedback",
                column: "InterviewerUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_AppUsers_OrganizerUserId",
                table: "Interviews",
                column: "OrganizerUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_AppUsers_ScheduledByUserId",
                table: "Interviews",
                column: "ScheduledByUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterviewFeedback_AppUsers_InterviewerUserId",
                table: "InterviewFeedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_AppUsers_OrganizerUserId",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_AppUsers_ScheduledByUserId",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_OrganizerUserId",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_ScheduledByUserId",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_InterviewFeedback_InterviewerUserId",
                table: "InterviewFeedback");

            migrationBuilder.AlterColumn<string>(
                name: "Qualification",
                table: "JobPostings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "JobPostings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmploymentType",
                table: "JobPostings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ScheduledByUserId",
                table: "Interviews",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OrganizerUserId",
                table: "Interviews",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "InterviewerUserId",
                table: "InterviewFeedback",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
