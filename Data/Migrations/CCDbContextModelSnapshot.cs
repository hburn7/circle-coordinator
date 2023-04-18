﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using circle_coordinator.Data.Context;

#nullable disable

namespace circle_coordinator.Migrations
{
    [DbContext(typeof(CCDbContext))]
    partial class CCDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PlayerTournament", b =>
                {
                    b.Property<int>("PlayersId")
                        .HasColumnType("integer");

                    b.Property<int>("TournamentsId")
                        .HasColumnType("integer");

                    b.HasKey("PlayersId", "TournamentsId");

                    b.HasIndex("TournamentsId");

                    b.ToTable("PlayerTournament");
                });

            modelBuilder.Entity("StaffMemberStaffRole", b =>
                {
                    b.Property<int>("StaffMembersId")
                        .HasColumnType("integer");

                    b.Property<int>("StaffRolesId")
                        .HasColumnType("integer");

                    b.HasKey("StaffMembersId", "StaffRolesId");

                    b.HasIndex("StaffRolesId");

                    b.ToTable("StaffMemberStaffRole");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("DiscordId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("DiscordTag")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.Replay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReplayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ReplayId"));

                    b.Property<int>("StageId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("StageId");

                    b.ToTable("Replays");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.StaffMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DiscordTag")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("DiscordUserId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<int>("OsuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OsuId"));

                    b.Property<int>("TournamentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("StaffMembers");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.StaffMemberRole", b =>
                {
                    b.Property<int>("StaffMemberId")
                        .HasColumnType("integer");

                    b.Property<int>("StaffRoleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("StaffMemberId", "StaffRoleId");

                    b.HasIndex("StaffRoleId");

                    b.ToTable("StaffMemberRoles");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.StaffRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal?>("DiscordRoleId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TournamentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("StaffRoles");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TeamId"));

                    b.Property<int>("TournamentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.TeamPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamPlayers");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BracketUrl")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("CreatorId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("CreatorTag")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DiscordPermanentInviteUrl")
                        .HasColumnType("text");

                    b.Property<string[]>("DonationUrls")
                        .HasColumnType("text[]");

                    b.Property<string>("ForumUrl")
                        .HasColumnType("text");

                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int?>("MaximumRank")
                        .HasColumnType("integer");

                    b.Property<int?>("MinimumRank")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PlayersPerTeam")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("RegistrationCloseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("RegistrationOpenDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Ruleset")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("TournamentEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TournamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TournamentId"));

                    b.Property<DateTime?>("TournamentStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string[]>("TwitchUrl")
                        .HasColumnType("text[]");

                    b.Property<string[]>("TwitterUrl")
                        .HasColumnType("text[]");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string[]>("YoutubeUrl")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.TournamentStage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("StageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StageId"));

                    b.Property<int>("TournamentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("TournamentStages");
                });

            modelBuilder.Entity("PlayerTournament", b =>
                {
                    b.HasOne("circle_coordinator.Models.Entities.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("circle_coordinator.Models.Entities.Tournament", null)
                        .WithMany()
                        .HasForeignKey("TournamentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StaffMemberStaffRole", b =>
                {
                    b.HasOne("circle_coordinator.Models.Entities.StaffMember", null)
                        .WithMany()
                        .HasForeignKey("StaffMembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("circle_coordinator.Models.Entities.StaffRole", null)
                        .WithMany()
                        .HasForeignKey("StaffRolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.Replay", b =>
                {
                    b.HasOne("circle_coordinator.Models.Entities.TournamentStage", "Stage")
                        .WithMany("Replays")
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.StaffMember", b =>
                {
                    b.HasOne("circle_coordinator.Models.Entities.Tournament", "Tournament")
                        .WithMany("StaffMembers")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.StaffMemberRole", b =>
                {
                    b.HasOne("circle_coordinator.Models.Entities.StaffMember", "StaffMember")
                        .WithMany("StaffMemberRoles")
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("circle_coordinator.Models.Entities.StaffRole", "StaffRole")
                        .WithMany("StaffMemberRoles")
                        .HasForeignKey("StaffRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StaffMember");

                    b.Navigation("StaffRole");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.StaffRole", b =>
                {
                    b.HasOne("circle_coordinator.Models.Entities.Tournament", "Tournament")
                        .WithMany()
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.Team", b =>
                {
                    b.HasOne("circle_coordinator.Models.Entities.Tournament", "Tournament")
                        .WithMany("Teams")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.TeamPlayer", b =>
                {
                    b.HasOne("circle_coordinator.Models.Entities.Team", "Team")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("circle_coordinator.Models.Entities.Player", "Player")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.TournamentStage", b =>
                {
                    b.HasOne("circle_coordinator.Models.Entities.Tournament", "Tournament")
                        .WithMany("Stages")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.Player", b =>
                {
                    b.Navigation("TeamPlayers");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.StaffMember", b =>
                {
                    b.Navigation("StaffMemberRoles");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.StaffRole", b =>
                {
                    b.Navigation("StaffMemberRoles");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.Team", b =>
                {
                    b.Navigation("TeamPlayers");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.Tournament", b =>
                {
                    b.Navigation("StaffMembers");

                    b.Navigation("Stages");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("circle_coordinator.Models.Entities.TournamentStage", b =>
                {
                    b.Navigation("Replays");
                });
#pragma warning restore 612, 618
        }
    }
}
