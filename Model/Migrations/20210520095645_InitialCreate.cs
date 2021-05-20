using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineLearningModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    UpscaleFactor = table.Column<byte>(nullable: true),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineLearningModels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MachineLearningModels",
                columns: new[] { "Id", "Discriminator", "Name", "Path", "UpscaleFactor" },
                values: new object[,]
                {
                    { 1, "SuperResolutionModel", "Super Resolution SRResNET MSE", "MachineLearningModels\\SuperResolution_SRResNet_MSE\\super_resolution_2x.onnx", (byte)2 },
                    { 2, "SuperResolutionModel", "Super Resolution SRResNET MSE", "MachineLearningModels\\SuperResolution_SRResNet_MSE\\super_resolution_4x.onnx", (byte)4 },
                    { 3, "SuperResolutionModel", "Super Resolution SRResNET MSE", "MachineLearningModels\\SuperResolution_SRResNet_MSE\\super_resolution_8x.onnx", (byte)8 },
                    { 4, "SuperResolutionModel", "Super Resolution SRGAN MSE", "MachineLearningModels\\SuperResolution_SRGAN\\super_resolution_2x.onnx", (byte)2 },
                    { 5, "SuperResolutionModel", "Super Resolution SRGAN MSE", "MachineLearningModels\\SuperResolution_SRGAN\\super_resolution_4x.onnx", (byte)4 },
                    { 6, "SuperResolutionModel", "Super Resolution SRGAN MSE", "MachineLearningModels\\SuperResolution_SRGAN\\super_resolution_8x.onnx", (byte)8 },
                    { 7, "SuperResolutionModel", "Super Resolution SRResNET SSIM", "MachineLearningModels\\SuperResolution_SRResNet_SSIM\\super_resolution_2x.onnx", (byte)2 },
                    { 8, "SuperResolutionModel", "Super Resolution SRResNET SSIM", "MachineLearningModels\\SuperResolution_SRResNet_SSIM\\super_resolution_4x.onnx", (byte)4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineLearningModels");
        }
    }
}
