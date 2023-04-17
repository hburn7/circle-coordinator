using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace circle_coordinator.Migrations
{
    /// <inheritdoc />
    public partial class staffmemberstaffrolerepositories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BracketUrl",
                table: "Tournaments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CreatorId",
                table: "Tournaments",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CreatorTag",
                table: "Tournaments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiscordPermanentInviteUrl",
                table: "Tournaments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "DonationUrls",
                table: "Tournaments",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForumUrl",
                table: "Tournaments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaximumRank",
                table: "Tournaments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinimumRank",
                table: "Tournaments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayersPerTeam",
                table: "Tournaments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationCloseDate",
                table: "Tournaments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationOpenDate",
                table: "Tournaments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ruleset",
                table: "Tournaments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TournamentEndDate",
                table: "Tournaments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TournamentStartDate",
                table: "Tournaments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "TwitchUrl",
                table: "Tournaments",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "TwitterUrl",
                table: "Tournaments",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "YoutubeUrl",
                table: "Tournaments",
                type: "text[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BracketUrl",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "CreatorTag",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "DiscordPermanentInviteUrl",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "DonationUrls",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "ForumUrl",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "MaximumRank",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "MinimumRank",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "PlayersPerTeam",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "RegistrationCloseDate",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "RegistrationOpenDate",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Ruleset",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TournamentEndDate",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TournamentStartDate",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TwitchUrl",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "YoutubeUrl",
                table: "Tournaments");
        }
    }
}
