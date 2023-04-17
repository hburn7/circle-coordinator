using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace circle_coordinator.Migrations
{
    /// <inheritdoc />
    public partial class TournamentGetAllByGuildId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Player_PlayersId",
                table: "PlayerTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_Replay_TournamentStage_StageId",
                table: "Replay");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Tournaments_TournamentId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPlayer_Player_TeamId",
                table: "TeamPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPlayer_Team_PlayerId",
                table: "TeamPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentStage_Tournaments_TournamentId",
                table: "TournamentStage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentStage",
                table: "TournamentStage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamPlayer",
                table: "TeamPlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Team",
                table: "Team");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Replay",
                table: "Replay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.RenameTable(
                name: "TournamentStage",
                newName: "TournamentStages");

            migrationBuilder.RenameTable(
                name: "TeamPlayer",
                newName: "TeamPlayers");

            migrationBuilder.RenameTable(
                name: "Team",
                newName: "Teams");

            migrationBuilder.RenameTable(
                name: "Replay",
                newName: "Replays");

            migrationBuilder.RenameTable(
                name: "Player",
                newName: "Players");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentStage_TournamentId",
                table: "TournamentStages",
                newName: "IX_TournamentStages_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamPlayer_TeamId",
                table: "TeamPlayers",
                newName: "IX_TeamPlayers_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Team_TournamentId",
                table: "Teams",
                newName: "IX_Teams_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Replay_StageId",
                table: "Replays",
                newName: "IX_Replays_StageId");

            migrationBuilder.AddColumn<decimal>(
                name: "GuildId",
                table: "Tournaments",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TeamPlayers",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TeamPlayers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TeamPlayers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentStages",
                table: "TournamentStages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamPlayers",
                table: "TeamPlayers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Replays",
                table: "Replays",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayers_PlayerId",
                table: "TeamPlayers",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Players_PlayersId",
                table: "PlayerTournament",
                column: "PlayersId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replays_TournamentStages_StageId",
                table: "Replays",
                column: "StageId",
                principalTable: "TournamentStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPlayers_Players_TeamId",
                table: "TeamPlayers",
                column: "TeamId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPlayers_Teams_PlayerId",
                table: "TeamPlayers",
                column: "PlayerId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Tournaments_TournamentId",
                table: "Teams",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentStages_Tournaments_TournamentId",
                table: "TournamentStages",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Players_PlayersId",
                table: "PlayerTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_Replays_TournamentStages_StageId",
                table: "Replays");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPlayers_Players_TeamId",
                table: "TeamPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPlayers_Teams_PlayerId",
                table: "TeamPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Tournaments_TournamentId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentStages_Tournaments_TournamentId",
                table: "TournamentStages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentStages",
                table: "TournamentStages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamPlayers",
                table: "TeamPlayers");

            migrationBuilder.DropIndex(
                name: "IX_TeamPlayers_PlayerId",
                table: "TeamPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Replays",
                table: "Replays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeamPlayers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TeamPlayers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TeamPlayers");

            migrationBuilder.RenameTable(
                name: "TournamentStages",
                newName: "TournamentStage");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "Team");

            migrationBuilder.RenameTable(
                name: "TeamPlayers",
                newName: "TeamPlayer");

            migrationBuilder.RenameTable(
                name: "Replays",
                newName: "Replay");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Player");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentStages_TournamentId",
                table: "TournamentStage",
                newName: "IX_TournamentStage_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_TournamentId",
                table: "Team",
                newName: "IX_Team_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamPlayers_TeamId",
                table: "TeamPlayer",
                newName: "IX_TeamPlayer_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Replays_StageId",
                table: "Replay",
                newName: "IX_Replay_StageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentStage",
                table: "TournamentStage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team",
                table: "Team",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamPlayer",
                table: "TeamPlayer",
                columns: new[] { "PlayerId", "TeamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Replay",
                table: "Replay",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Player_PlayersId",
                table: "PlayerTournament",
                column: "PlayersId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replay_TournamentStage_StageId",
                table: "Replay",
                column: "StageId",
                principalTable: "TournamentStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Tournaments_TournamentId",
                table: "Team",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPlayer_Player_TeamId",
                table: "TeamPlayer",
                column: "TeamId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPlayer_Team_PlayerId",
                table: "TeamPlayer",
                column: "PlayerId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentStage_Tournaments_TournamentId",
                table: "TournamentStage",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
