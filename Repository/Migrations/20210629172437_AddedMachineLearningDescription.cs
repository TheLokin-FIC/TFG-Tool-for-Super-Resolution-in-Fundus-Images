using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddedMachineLearningDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "MachineLearningModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "MachineLearningModels",
                type: "nvarchar(280)",
                maxLength: 280,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "MachineLearningModels",
                type: "nvarchar(140)",
                maxLength: 140,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "MachineLearningModels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LongDescription", "ShortDescription" },
                values: new object[] { new DateTime(2021, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Most of the images are composed of many pixels. To enlarge an image, instead of simply duplicating the pixels and worsening image quality this implementation uses deep learning to clarify, sharpen and upscale images without losing its content and defining characteristics.", "Clarify images and enhance resolution without feature loss with super resolution powered by an SRResNet architecture and MSE loss." });

            migrationBuilder.UpdateData(
                table: "MachineLearningModels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LongDescription", "ShortDescription" },
                values: new object[] { new DateTime(2021, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Most of the images are composed of many pixels. To enlarge an image, instead of simply duplicating the pixels and worsening image quality this implementation uses deep learning to clarify, sharpen and upscale images without losing its content and defining characteristics.", "Clarify images and enhance resolution without feature loss with super resolution powered by an SRGAN architecture and MSE loss." });

            migrationBuilder.UpdateData(
                table: "MachineLearningModels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "LongDescription", "ShortDescription" },
                values: new object[] { new DateTime(2021, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Most of the images are composed of many pixels. To enlarge an image, instead of simply duplicating the pixels and worsening image quality this implementation uses deep learning to clarify, sharpen and upscale images without losing its content and defining characteristics.", "Clarify images and enhance resolution without feature loss with super resolution powered by an SRResNet architecture and SSIM loss." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "MachineLearningModels");

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "MachineLearningModels");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "MachineLearningModels");
        }
    }
}
