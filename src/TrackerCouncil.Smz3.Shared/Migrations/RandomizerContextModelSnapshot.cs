﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrackerCouncil.Smz3.Shared.Models;

#nullable disable

namespace TrackerCouncil.Smz3.Shared.Migrations
{
    [DbContext(typeof(RandomizerContext))]
    partial class RandomizerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.GeneratedRom", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("GeneratorVersion")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MsuPaths")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MsuRandomizationStyle")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MsuShuffleStyle")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("MultiplayerGameDetailsId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RomPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Seed")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Settings")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SpoilerPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MultiplayerGameDetailsId");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("GeneratedRoms");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.MultiplayerGameDetails", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConnectionUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GameGuid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GameUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("GeneratedRomId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("JoinedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerGuid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerKey")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GeneratedRomId")
                        .IsUnique();

                    b.ToTable("MultiplayerGames");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerBossState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AutoTracked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BossName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Defeated")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorldId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("TrackerBossStates");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerDungeonState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AutoTracked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Cleared")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasManuallyClearedTreasure")
                        .HasColumnType("INTEGER");

                    b.Property<byte?>("MarkedMedallion")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MarkedReward")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("RemainingTreasure")
                        .HasColumnType("INTEGER");

                    b.Property<byte?>("RequiredMedallion")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Reward")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorldId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("TrackerDungeonStates");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerHintState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("HintState")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HintTileCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LocationKey")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LocationString")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("LocationWorldId")
                        .HasColumnType("INTEGER");

                    b.Property<byte?>("MedallionType")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Usefulness")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorldId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("TrackerHintStates");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerHistoryEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsImportant")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsUndone")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LocationName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("ObjectName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<double>("Time")
                        .HasColumnType("REAL");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("TrackerHistoryEvents");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerItemState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrackingState")
                        .HasColumnType("INTEGER");

                    b.Property<byte?>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorldId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("TrackerItemStates");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerLocationState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Autotracked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Cleared")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Item")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemOwnerName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemWorldId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<byte?>("MarkedItem")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MarkedUsefulness")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorldId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("TrackerLocationStates");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerMarkedLocation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("TrackerMarkedLocations");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerPrerequisiteState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AutoTracked")
                        .HasColumnType("INTEGER");

                    b.Property<byte?>("MarkedItem")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<byte>("RequiredItem")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorldId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("TrackerPrerequisiteStates");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerRegionState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte?>("Medallion")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Reward")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("TrackerRegionStates");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerRewardState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AutoTracked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasReceivedReward")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MarkedReward")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("RewardType")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorldId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("TrackerRewardStates");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GanonCrystalCount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GanonsTowerCrystalCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GiftedItemCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocalWorldId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MarkedGanonCrystalCount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MarkedGanonsTowerCrystalCount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MarkedTourianBossCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PercentageCleared")
                        .HasColumnType("INTEGER");

                    b.Property<double>("SecondsElapsed")
                        .HasColumnType("REAL");

                    b.Property<DateTimeOffset>("StartDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TourianBossCount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("UpdatedDateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TrackerStates");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerTreasureState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasManuallyClearedTreasure")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("RemainingTreasure")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalTreasure")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("TrackerStateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorldId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrackerStateId");

                    b.ToTable("TrackerTreasureStates");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.GeneratedRom", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.MultiplayerGameDetails", "MultiplayerGameDetails")
                        .WithMany()
                        .HasForeignKey("MultiplayerGameDetailsId");

                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany()
                        .HasForeignKey("TrackerStateId");

                    b.Navigation("MultiplayerGameDetails");

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.MultiplayerGameDetails", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.GeneratedRom", "GeneratedRom")
                        .WithOne()
                        .HasForeignKey("TrackerCouncil.Smz3.Shared.Models.MultiplayerGameDetails", "GeneratedRomId");

                    b.Navigation("GeneratedRom");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerBossState", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany("BossStates")
                        .HasForeignKey("TrackerStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerDungeonState", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany("DungeonStates")
                        .HasForeignKey("TrackerStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerHintState", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany("Hints")
                        .HasForeignKey("TrackerStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerHistoryEvent", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany("History")
                        .HasForeignKey("TrackerStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerItemState", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany("ItemStates")
                        .HasForeignKey("TrackerStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerLocationState", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany("LocationStates")
                        .HasForeignKey("TrackerStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerMarkedLocation", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany("MarkedLocations")
                        .HasForeignKey("TrackerStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerPrerequisiteState", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany("PrerequisiteStates")
                        .HasForeignKey("TrackerStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerRegionState", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany("RegionStates")
                        .HasForeignKey("TrackerStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerRewardState", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany("RewardStates")
                        .HasForeignKey("TrackerStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerTreasureState", b =>
                {
                    b.HasOne("TrackerCouncil.Smz3.Shared.Models.TrackerState", "TrackerState")
                        .WithMany("TreasureStates")
                        .HasForeignKey("TrackerStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TrackerState");
                });

            modelBuilder.Entity("TrackerCouncil.Smz3.Shared.Models.TrackerState", b =>
                {
                    b.Navigation("BossStates");

                    b.Navigation("DungeonStates");

                    b.Navigation("Hints");

                    b.Navigation("History");

                    b.Navigation("ItemStates");

                    b.Navigation("LocationStates");

                    b.Navigation("MarkedLocations");

                    b.Navigation("PrerequisiteStates");

                    b.Navigation("RegionStates");

                    b.Navigation("RewardStates");

                    b.Navigation("TreasureStates");
                });
#pragma warning restore 612, 618
        }
    }
}
