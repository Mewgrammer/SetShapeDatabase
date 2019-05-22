using Microsoft.EntityFrameworkCore.Migrations;

namespace SetShapeDatabase.Migrations
{
    public partial class ActiveTrainingPlanForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTrainingPlanId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrentTrainingPlanId",
                table: "Users",
                column: "CurrentTrainingPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TrainingPlans_CurrentTrainingPlanId",
                table: "Users",
                column: "CurrentTrainingPlanId",
                principalTable: "TrainingPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TrainingPlans_CurrentTrainingPlanId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CurrentTrainingPlanId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentTrainingPlanId",
                table: "Users");
        }
    }
}
