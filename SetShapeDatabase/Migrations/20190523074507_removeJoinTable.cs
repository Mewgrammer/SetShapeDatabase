using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SetShapeDatabase.Migrations
{
    public partial class removeJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingDayWorkouts");

            migrationBuilder.AddColumn<int>(
                name: "TrainingDayId",
                table: "Workouts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_TrainingDayId",
                table: "Workouts",
                column: "TrainingDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_TrainingDays_TrainingDayId",
                table: "Workouts",
                column: "TrainingDayId",
                principalTable: "TrainingDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_TrainingDays_TrainingDayId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_TrainingDayId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "TrainingDayId",
                table: "Workouts");

            migrationBuilder.CreateTable(
                name: "TrainingDayWorkouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TrainingDayId = table.Column<int>(nullable: false),
                    WorkoutId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingDayWorkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingDayWorkouts_TrainingDays_TrainingDayId",
                        column: x => x.TrainingDayId,
                        principalTable: "TrainingDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingDayWorkouts_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingDayWorkouts_TrainingDayId",
                table: "TrainingDayWorkouts",
                column: "TrainingDayId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingDayWorkouts_WorkoutId",
                table: "TrainingDayWorkouts",
                column: "WorkoutId");
        }
    }
}
