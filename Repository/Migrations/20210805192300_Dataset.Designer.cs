﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Persistence;

namespace Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210805192300_Dataset")]
    partial class Dataset
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Repository.Persistence.Models.Dataset", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastModification")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Public")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Datasets");
                });

            modelBuilder.Entity("Repository.Persistence.Models.Image", b =>
                {
                    b.Property<long>("DatasetId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<double>("Size")
                        .HasColumnType("float");

                    b.HasKey("DatasetId", "Name");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Repository.Persistence.Models.MachineLearningModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Architecture")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LongDescription")
                        .IsRequired()
                        .HasMaxLength(280)
                        .HasColumnType("nvarchar(280)");

                    b.Property<string>("Loss")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("nvarchar(140)");

                    b.HasKey("Id");

                    b.ToTable("MachineLearningModels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Architecture = "SRResNet",
                            CreationDate = new DateTime(2021, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LongDescription = "Most of the images are composed of many pixels. To enlarge an image, instead of simply duplicating the pixels and worsening image quality this implementation uses deep learning to clarify, sharpen and upscale images without losing its content and defining characteristics.",
                            Loss = "MSE",
                            Name = "Super Resolution",
                            ShortDescription = "Clarify images and enhance resolution without feature loss with super resolution powered by an SRResNet architecture and MSE loss."
                        },
                        new
                        {
                            Id = 2,
                            Architecture = "SRGAN",
                            CreationDate = new DateTime(2021, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LongDescription = "Most of the images are composed of many pixels. To enlarge an image, instead of simply duplicating the pixels and worsening image quality this implementation uses deep learning to clarify, sharpen and upscale images without losing its content and defining characteristics.",
                            Loss = "MSE",
                            Name = "Super Resolution",
                            ShortDescription = "Clarify images and enhance resolution without feature loss with super resolution powered by an SRGAN architecture and MSE loss."
                        },
                        new
                        {
                            Id = 3,
                            Architecture = "SRResNet",
                            CreationDate = new DateTime(2021, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LongDescription = "Most of the images are composed of many pixels. To enlarge an image, instead of simply duplicating the pixels and worsening image quality this implementation uses deep learning to clarify, sharpen and upscale images without losing its content and defining characteristics.",
                            Loss = "SSIM",
                            Name = "Super Resolution",
                            ShortDescription = "Clarify images and enhance resolution without feature loss with super resolution powered by an SRResNet architecture and SSIM loss."
                        });
                });

            modelBuilder.Entity("Repository.Persistence.Models.SuperResolutionModel", b =>
                {
                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<byte>("UpscaleFactor")
                        .HasColumnType("tinyint");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(260)
                        .HasColumnType("nvarchar(260)");

                    b.HasKey("ModelId", "UpscaleFactor");

                    b.HasIndex("Path")
                        .IsUnique();

                    b.ToTable("SuperResolutionModels");

                    b.HasData(
                        new
                        {
                            ModelId = 1,
                            UpscaleFactor = (byte)2,
                            Path = "Resources\\SuperResolution\\SRResNet\\MSE\\super_resolution_2x.onnx"
                        },
                        new
                        {
                            ModelId = 1,
                            UpscaleFactor = (byte)4,
                            Path = "Resources\\SuperResolution\\SRResNet\\MSE\\super_resolution_4x.onnx"
                        },
                        new
                        {
                            ModelId = 1,
                            UpscaleFactor = (byte)8,
                            Path = "Resources\\SuperResolution\\SRResNet\\MSE\\super_resolution_8x.onnx"
                        },
                        new
                        {
                            ModelId = 2,
                            UpscaleFactor = (byte)2,
                            Path = "Resources\\SuperResolution\\SRGAN\\MSE\\super_resolution_2x.onnx"
                        },
                        new
                        {
                            ModelId = 2,
                            UpscaleFactor = (byte)4,
                            Path = "Resources\\SuperResolution\\SRGAN\\MSE\\super_resolution_4x.onnx"
                        },
                        new
                        {
                            ModelId = 2,
                            UpscaleFactor = (byte)8,
                            Path = "Resources\\SuperResolution\\SRGAN\\MSE\\super_resolution_8x.onnx"
                        },
                        new
                        {
                            ModelId = 3,
                            UpscaleFactor = (byte)2,
                            Path = "Resources\\SuperResolution\\SRResNet\\SSIM\\super_resolution_2x.onnx"
                        },
                        new
                        {
                            ModelId = 3,
                            UpscaleFactor = (byte)4,
                            Path = "Resources\\SuperResolution\\SRResNet\\SSIM\\super_resolution_4x.onnx"
                        });
                });

            modelBuilder.Entity("Repository.Persistence.Models.UserProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EncryptedPassword")
                        .IsRequired()
                        .HasMaxLength(44)
                        .HasColumnType("nvarchar(44)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Repository.Persistence.Models.Dataset", b =>
                {
                    b.HasOne("Repository.Persistence.Models.UserProfile", "UserProfile")
                        .WithMany("Datasets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Repository.Persistence.Models.Image", b =>
                {
                    b.HasOne("Repository.Persistence.Models.Dataset", "Dataset")
                        .WithMany("Images")
                        .HasForeignKey("DatasetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dataset");
                });

            modelBuilder.Entity("Repository.Persistence.Models.SuperResolutionModel", b =>
                {
                    b.HasOne("Repository.Persistence.Models.MachineLearningModel", "MachineLearningModel")
                        .WithMany("SuperResolutionModels")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MachineLearningModel");
                });

            modelBuilder.Entity("Repository.Persistence.Models.Dataset", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Repository.Persistence.Models.MachineLearningModel", b =>
                {
                    b.Navigation("SuperResolutionModels");
                });

            modelBuilder.Entity("Repository.Persistence.Models.UserProfile", b =>
                {
                    b.Navigation("Datasets");
                });
#pragma warning restore 612, 618
        }
    }
}